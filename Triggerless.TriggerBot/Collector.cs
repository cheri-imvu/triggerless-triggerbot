using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using Dapper;
using Newtonsoft.Json;

namespace Triggerless.TriggerBot
{
    public partial class Collector : Component
    {
        private object _lock = new object();


        public static string GetUrlTemplate(long pid) => $"https://userimages-akm.imvu.com/productdata/{pid}/1/{{0}}";

        public static string GetUrl(long pid, string filename) => string.Format(GetUrlTemplate(pid), filename);


        public async Task ScanDatabasesAsync()
        {
            var sda = new SQLiteDataAccess();
            var productList = new List<ProductSearchInfo>();
            var existingProductIDs = new HashSet<long>();

            using (var connProduct = sda.GetProductCacheCxn())
            {
                connProduct.Open();
                var sql = "SELECT id as ProductId, products_name as ProductName, manufacturers_name as CreatorName, products_image as ProductImage FROM products ";
                sql += $"WHERE {SQLiteDataAccess.AccessoryFilter}; ";
                var products = connProduct.Query<ProductSearchInfo>(sql);
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

            var numberTotal = workingProducts.Count;
            if (numberTotal == 0)
            {
                FireEvent(new CollectorEventArgs
                {
                    CompletedProducts = 0,
                    TotalProducts = numberTotal,
                    Message = "Nothing to update"
                });
                return;
            }

            var numberComplete = 0;
            var maxConcurrentThreads = 5;
            var semaphore = new SemaphoreSlim(maxConcurrentThreads);
            var tasks = new List<Task>();
            var cycleStart = DateTime.Now;

            using (var cxnAppCache = sda.GetAppCacheCxn())
            {
                foreach (var product in workingProducts)
                {
                    await semaphore.WaitAsync();
                    try
                    {
                        tasks.Add(Task.Run(async () =>
                        {
                            // run sychronously for now
                            var result = await ScanOne(product, cxnAppCache);
                            Debug.WriteLine($"{product.ProductName}\t{result.Result}\t{result.Message}");

                            Interlocked.Increment(ref numberComplete);
                            FireEvent(new CollectorEventArgs
                            {
                                CompletedProducts = numberComplete,
                                TotalProducts = numberTotal,
                                Message = $"{product.ProductName}"
                            });

                        }));
                    }
                    finally
                    {
                        semaphore.Release();
                    }

                }

                await Task.WhenAll(tasks);

                var sqlCleanup = @"
                    UPDATE products SET has_ogg = 0 WHERE has_ogg = 1
                    AND NOT EXISTS (SELECT product_id FROM product_triggers WHERE product_id = products.product_id);
                ";
                lock (_lock)
                {
                    cxnAppCache.Execute(sqlCleanup);
                }

            }

            Debug.WriteLine($"Cycle complete {workingProducts.Count} items in {(DateTime.Now - cycleStart).TotalMilliseconds} ms");

        }

        public async Task<ScanResult> ScanOne(ProductSearchInfo product, System.Data.SQLite.SQLiteConnection connAppCache)
        {
            var result = new ScanResult { Result = ScanResultType.Pending };
            var sda = new SQLiteDataAccess();

            // See if any ogg files exist
            using (var client = new HttpClient())
            {
                #region JsonContents

                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                var url = GetUrl(product.ProductId, "_contents.json");
                string httpResult;
                ContentsJsonItem[] jsonContents;
                try
                {
                    httpResult = await client.GetStringAsync(url);
                }
                catch (Exception ex)
                {
                    result.Message = ex.Message;
                    bool is404 = ex.Message.Contains("404") || (ex.InnerException != null && ex.InnerException.Message.Contains("404"));
                    if (is404)
                    {
                        //NOTE: Very early products may give a 404 error. We should save to DB with has_ogg = 0
                        var insertPayload = new { product_id = product.ProductId, has_ogg = 0, title = product.ProductName, creator = product.CreatorName };
                        var sql = "INSERT INTO products (product_id, has_ogg, title, creator) VALUES (@product_id, @has_ogg, @title, @creator);";
                        lock (_lock)
                        {
                            connAppCache.Execute(sql, insertPayload);
                        }
                        result.Message = "Product could not be downloaded (404)";
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
                    lock (_lock)
                    {
                        connAppCache.Execute(sql, insertPayload);
                    }
                    
                    return result;
                }
                #endregion

                #region Any OGG Files?
                if (!jsonContents.Any(c => c.Name.ToLower().EndsWith(".ogg"))) // no OGG files found
                {
                    result.Message = $"No OGG files found in product {product.ProductId}";
                    result.Result = ScanResultType.NoUsefulTriggers;
                    var insertPayload = new { product_id = product.ProductId, has_ogg = 0, title = product.ProductName, creator = product.CreatorName };
                    var sql = "INSERT INTO products (product_id, has_ogg, title, creator) VALUES (@product_id, @has_ogg, @title, @creator);";
                    lock (_lock)
                    {
                        connAppCache.Execute(sql, insertPayload);
                    }
                    return result;
                }
                #endregion

                #region Product Image
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
                    lock (_lock) { 
                        connAppCache.Execute(sql,insertPayload); 
                    }
                } 
                catch (Exception)
                {
                    result.Message = "Unable to retrieve product icon";
                    result.Result = ScanResultType.NetworkError;
                    return result;
                }
                #endregion

                #region Read Index.xml and associate triggers

                var pid = product.ProductId;
                var docIndex = new XmlDocument();
                try
                {
                    docIndex.Load(await client.GetStreamAsync(GetUrl(pid, "index.xml")));
                }
                catch (Exception)
                {
                    result.Result = ScanResultType.XmlError;
                    result.Message = "Unable to retrieve index.xml file";
                    return result;
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

                        try
                        {
                            var triggerEntry = new TriggerEntry
                            {
                                ProductId = product.ProductId,
                                OggName = oggName,
                                TriggerName = nameEl.InnerText,
                                Location = jsonContents.Where(j => j.Name.ToLower() == oggName.ToLower()).First()?.Location,
                                Sequence = 0
                            };

                            triggerList.Add(triggerEntry);
                        }
                        catch (Exception) {
                            result.Result = ScanResultType.JsonError;
                            result.Message = "Malformed product file";
                            lock (_lock)
                            {
                                connAppCache.Execute($"UPDATE products SET has_ogg = 0 WHERE product_id = {product.ProductId}");
                            }
                            return result;
                        }
                    }
                }
                #endregion

                #region OGG file in Template?
                if (!triggerList.Any())
                {
                    result.Message = "Triggers were not found in XML file";
                    result.Result = ScanResultType.NoUsefulTriggers;
                    return result;
                }
                #endregion

                #region Split Triggers and Prefixes

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
                #endregion

                #region Filter Out Crappy Triggers
                triggerList = triggerList.Where(t => t.Sequence != -1).OrderBy(t => t.Prefix).ThenBy(t => t.Sequence).ToList();

                if (triggerList.Any(t => t.Prefix is null))
                {
                    lock (_lock)
                    {
                        connAppCache.Execute($"UPDATE products SET has_ogg = 0 WHERE product_id = {product.ProductId}");
                    }
                    result.Result = ScanResultType.NoUsefulTriggers;
                    result.Message = $"Null prefixes are not allowed";
                    return result;
                }

                var distinctPrefixes = triggerList.Select(t => t.Prefix.ToLower()).Distinct().Count();
                if (distinctPrefixes != 1)
                {
                    lock (_lock)
                    {
                        connAppCache.Execute($"UPDATE products SET has_ogg = 0 WHERE product_id = {product.ProductId}");
                    }
                    result.Result = ScanResultType.NoUsefulTriggers;
                    result.Message = $"Too many trigger prefixes ({distinctPrefixes})";
                    return result;
                }


                if (triggerList.Count == 0)
                {
                    lock (_lock)
                    {
                        connAppCache.Execute($"UPDATE products SET has_ogg = 0 WHERE product_id = {product.ProductId}");
                    }
                    result.Result = ScanResultType.NoUsefulTriggers;
                    result.Message = $"No Useful triggers found";
                    return result;
                }

                if (triggerList.Count < 4)
                {
                    lock (_lock)
                    {
                        connAppCache.Execute($"UPDATE products SET has_ogg = 0 WHERE product_id = {product.ProductId}");
                    }
                    result.Result = ScanResultType.NoUsefulTriggers;
                    result.Message = $"4 Triggers required to qualify as a song";
                    return result;
                }
                #endregion

                #region Retrive OGG file lengths

                var maxConcurrentThreads = 2;
                var semaphore = new SemaphoreSlim(maxConcurrentThreads);
                var tasks = new List<Task>();

                foreach (var trigger in triggerList)
                {
                    var start = DateTime.Now;
                    //await semaphore.WaitAsync();
                    await semaphore.WaitAsync();
                    try
                    {
                        tasks.Add(Task.Run(async () =>
                        {
                            var urlLookup = trigger.Location;
                            var musicUrl = GetUrl(product.ProductId, urlLookup);
                            using (var triggerClient = new HttpClient())
                            {
                                try
                                {
                                    using (var stream = await triggerClient.GetStreamAsync(musicUrl))
                                    {
                                        var ms = new MemoryStream();
                                        stream.CopyTo(ms);
                                        trigger.LengthMS = NVorbis.VorbisReader.GetOggLengthMS(ms);
                                        ms.Dispose();
                                    }
                                }
                                catch (Exception)
                                {
                                    /*
                                    result.Result = ScanResultType.NetworkError;
                                    result.Message = "Unable to download an OGG file";
                                    var sqlDelete = $"DELETE FROM product_triggers WHERE product_id = {product.ProductId};";
                                    lock (_lock)
                                    {
                                        connAppCache.Execute(sqlDelete);
                                    }
                                    return result;
                                    */

                                }
                            }
                            Debug.WriteLine($"\t{trigger.OggName} ({(DateTime.Now - start).TotalMilliseconds} ms)");
                        }));
                    }
                    catch (Exception exc)
                    {
                        Debug.WriteLine($"{exc.Message}");
                    }
                    finally
                    {
                        semaphore.Release();
                    }
                }


                await Task.WhenAll(tasks);
                #endregion

                #region Save Triggers to DB
                var sqlInsertTrigger = "INSERT INTO product_triggers (product_id, prefix, sequence, trigger, ogg_name, length_ms, location) VALUES " +
                    "(@ProductId, @Prefix, @Sequence, @TriggerName, @OggName, @LengthMS, @Location);";

                var triggers = triggerList.OrderBy(t => t.Sequence).ThenBy(t => t.TriggerName).ToList();
                int seq = 1;
                foreach (var t in triggers)
                {
                    t.Sequence = seq++;
                }

                lock (_lock)
                {
                    foreach (var trigger in triggers)
                    {
                        connAppCache.Execute(sqlInsertTrigger, trigger);
                    }
                }
                result.Result = ScanResultType.Success;
                result.Message = $"{triggerList.Count} triggers found and added";

                #endregion
            }

            return result;
        }    
    }
}
