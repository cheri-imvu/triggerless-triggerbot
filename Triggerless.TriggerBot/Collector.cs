using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using Dapper;
using Newtonsoft.Json;

namespace Triggerless.TriggerBot
{
    public partial class Collector
    {

        public class CollectorEventArgs : EventArgs { }

        public delegate void CollectorEventHandler (object sender, CollectorEventArgs e);

        public event CollectorEventHandler EventOccurred;

        public void FireEvent(CollectorEventArgs e)
        {
            if (EventOccurred != null)
            {
                EventOccurred (this, e);
            }
        }


        public static string GetUrlTemplate(long pid) => $"https://userimages-akm.imvu.com/productdata/{pid}/1/{{0}}";

        public static string GetUrl(long pid, string filename) => string.Format(GetUrlTemplate(pid), filename);


        public async Task ScanDatabases()
        {
            //Debugger.Break();
            var sda = new SQLiteDataAccess();
            var productList = new List<ProductInfo>();
            var existingProductIDs = new HashSet<long>();

            using (var connProduct = sda.GetProductCacheCxn())
            {
                connProduct.Open();
                var sql = "SELECT id as ProductId, products_name as ProductName, manufacturers_name as CreatorName, products_image as ProductImage FROM products ";
                sql += $"WHERE {SQLiteDataAccess.AccessoryFilter}; ";
                var products = connProduct.Query<ProductInfo>(sql);
                productList.AddRange(products);
            }

            using (var connAppCache = sda.GetAppCacheCxn())
            {
                connAppCache.Open();
                
                var sql = "SELECT product_id from products";
                var ints = connAppCache.Query<long>(sql);
                existingProductIDs = new HashSet<long>(ints);
            }

            var workingProducts = productList.Where(pe => !existingProductIDs.Contains(pe.ProductId)).ToList();

            // just to debug
            workingProducts = workingProducts.Skip(200).Take(100).ToList();

            var numberTotal = workingProducts.Count;
            if (numberTotal == 0) return;

            var numberComplete = 0;
            var maxConcurrentThreads = 5;
            var semaphore = new SemaphoreSlim(maxConcurrentThreads);
            var tasks = new List<Task>();
            var cycleStart = DateTime.Now;

            foreach (var product in workingProducts)
            {
                await semaphore.WaitAsync();
                try
                {
                    tasks.Add(Task.Run(async () =>
                    {
                        var result = await ScanOne(product);

                        Interlocked.Increment(ref numberComplete);
                    }));
                }
                finally
                {
                    semaphore.Release();
                }

            }

            await Task.WhenAll(tasks);
            Debug.WriteLine($"Cycle complete {workingProducts.Count} items in {(DateTime.Now - cycleStart).TotalMilliseconds} ms");

        }

        public async Task<ScanResult> ScanOne(ProductInfo product)
        {
            var result = new ScanResult { Result = ScanResultType.Pending };
            var sda = new SQLiteDataAccess();

            // See if any ogg files exist
            using (var connAppCache = sda.GetAppCacheCxn())
            using (var client = new HttpClient())
            {
                await connAppCache.OpenAsync();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                var url = GetUrl(product.ProductId, "_contents.json");
                string httpResult;
                ContentsJsonItem[] jsonContents;
                try
                {
                    httpResult = await client.GetStringAsync(url);                    
                }
                catch (HttpRequestException ex)
                {
                    result.Message = ex.Message;
                    if (ex.Message.Contains("404"))
                    {
                        //NOTE: Very early products may give a 404 error. We should save to DB with has_ogg = 0
                        var insertPayload = new { product_id = product.ProductId, has_ogg = 0, title = product.ProductName, creator = product.CreatorName };
                        var sql = "INSERT INTO products (product_id, has_ogg, title, creator) VALUES (@product_id, @has_ogg, @title, @creator);";
                        await connAppCache.ExecuteAsync(sql, insertPayload);
                    }
                    result.Result = ScanResultType.NetworkError;                    
                    return result;
                }

                try
                {
                    jsonContents = JsonConvert.DeserializeObject<ContentsJsonItem[]>(httpResult);
                }
                catch (Exception ex)
                {
                    result.Message = ex.Message;
                    result.Result = ScanResultType.JsonError;
                    var insertPayload = new {product_id = product.ProductId, has_ogg = 0, title = product.ProductName, creator = product.CreatorName };
                    var sql = "INSERT INTO products (product_id, has_ogg, title, creator) VALUES (@product_id, @has_ogg, @title, @creator);";
                    await connAppCache.ExecuteAsync(sql, insertPayload);
                    return result;
                }

                if (!jsonContents.Any(c => c.Name.ToLower().EndsWith(".ogg"))) // no OGG files found
                {
                    result.Message = $"No OGG files found in product {product.ProductId}";
                    result.Result = ScanResultType.Success;
                    var insertPayload = new { product_id = product.ProductId, has_ogg = 0, title = product.ProductName, creator = product.CreatorName };
                    var sql = "INSERT INTO products (product_id, has_ogg, title, creator) VALUES (@product_id, @has_ogg, @title, @creator);";
                    await connAppCache.ExecuteAsync(sql, insertPayload);
                    return result;
                }

                try
                {
                    client.DefaultRequestHeaders.Clear();
                    var imageBytes = await client.GetByteArrayAsync(product.ProductImage);
                    var insertPayload = new {
                        product_id = product.ProductId,
                        has_ogg = 1,
                        title = product.ProductName,
                        creator = product.CreatorName,
                        image_bytes = imageBytes
                    };
                    var sql = "INSERT INTO products (product_id, has_ogg, title, creator, image_bytes) VALUES (@product_id, @has_ogg, @title, @creator, @image_bytes);";
                    await connAppCache.ExecuteAsync(sql, insertPayload);
                } 
                catch (Exception exc)
                {
                    Debug.WriteLine($"{exc.Message}\r\n{exc.StackTrace}");
                    Debugger.Break();
                    throw exc; // not sure what to do otherwise, we'll see
                }

                var lengthDict = new ConcurrentDictionary<string, double>();
                var maxConcurrentThreads = 5;
                var semaphore = new SemaphoreSlim(maxConcurrentThreads);
                var tasks = new List<Task>();
                foreach (var jsonItem in jsonContents.Where(ji => ji.Name.ToLower().EndsWith(".ogg")))
                {
                    var start = DateTime.Now;
                    await semaphore.WaitAsync();
                    try
                    {
                        tasks.Add(Task.Run(async () =>
                        {
                            var urlLookup = (string.IsNullOrEmpty(jsonItem.Url)) ? jsonItem.Name : jsonItem.Url;
                            var musicUrl = GetUrl(product.ProductId, urlLookup);
                            using (var triggerClient = new HttpClient())
                            {
                                try
                                {
                                    using (var stream = await triggerClient.GetStreamAsync(musicUrl))
                                    {
                                        var ms = new MemoryStream();
                                        stream.CopyTo(ms);
                                        lengthDict[jsonItem.Name] = NVorbis.VorbisReader.GetOggLengthMS(ms);
                                        ms.Dispose();
                                    }
                                }
                                catch (Exception exc)
                                {
                                    Debug.WriteLine(exc.Message);
                                    Debug.WriteLine(exc.StackTrace);
                                    Debugger.Break();

                                }
                            }
                        Debug.WriteLine($"{jsonItem.Name} {(DateTime.Now - start).TotalMilliseconds} ms)");
                        }));
                    }
                    finally
                    {
                        semaphore.Release();
                    }
                }
                await Task.WhenAll(tasks);

                // Now we have all the lengths, time to find the actual triggers

                var pid = product.ProductId;
                var docIndex = new XmlDocument();
                try
                {
                    docIndex.Load(await client.GetStreamAsync(GetUrl(pid, "index.xml")));
                }
                catch (Exception exc)
                {
                    Debug.WriteLine($"{exc.Message}\r\n{exc.StackTrace}");
                    Debugger.Break();
                }
                var root = docIndex.DocumentElement;
                long parentId = 0;
                var triggerList = new List<TriggerEntry>();

                foreach (XmlElement item in root.ChildNodes)
                {
                    if (item.Name == "__DATAIMPORT")
                    {
                        parentId = long.Parse(item.InnerText.Replace("product://", "").Replace("/index.xml", ""));
                    }

                    if (item.Name.StartsWith("Action"))
                    {
                        var nameEl = item.SelectSingleNode("Name") as XmlElement;
                        if (nameEl == null) continue;

                        var soundEl = item.SelectSingleNode("Sound") as XmlElement;
                        if (soundEl == null) continue;

                        var oggName = soundEl.InnerText;
                        if (!oggName.ToLower().EndsWith(".ogg")) continue;

                        triggerList.Add(new TriggerEntry { 
                            ProductId = product.ProductId,
                            OggName = oggName,
                            TriggerName = nameEl.InnerText,
                            LengthMS = lengthDict[oggName],
                            Sequence = 0
                        });
                    }
                }



                // split triggers by prefix and sequence

                var numbers = "0123456789".ToCharArray();
                foreach (var trigger in triggerList)
                {
                    bool hitNumber = false;
                    string numberString = string.Empty;
                    foreach (var c in trigger.TriggerName)
                    {
                        if (numbers.Contains(c))
                        {
                            hitNumber = true;
                            numberString += c;
                        }
                        else
                        {
                            if (!hitNumber)
                            {
                                trigger.Prefix += c;
                            }
                            else
                            {
                                trigger.Sequence = -1;
                            }
                        }
                        if (string.IsNullOrEmpty(numberString))
                        {
                            trigger.Sequence = -1;
                        } 
                        else
                        {
                            trigger.Sequence = int.Parse(numberString);
                        }
                    }
                }

                triggerList = triggerList.Where(t => t.Sequence != -1).OrderBy(t => t.Prefix).ThenBy(t => t.Sequence).ToList();

                var sqlInsertTrigger = "INSERT INTO product_triggers (product_id, prefix, sequence, trigger, ogg_name, length_ms) VALUES " +
                    "(@ProductId, @Prefix, @Sequence, @TriggerName, @OggName, @LengthMS);";
                foreach(var trigger in triggerList)
                {
                    connAppCache.Execute(sqlInsertTrigger, trigger);
                }
            }

            return result;
        }    
    }
}
