using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
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
    public class Collector
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


        public static string GetUrlTemplate(int pid) => $"https://userimages-akm.imvu.com/productdata/{pid}/1/{{0}}";

        public static string GetUrl(long pid, string filename) => string.Format(GetUrlTemplate(pid), filename);

        public class ProductInfo
        {
            public long ProductId { get; set; }
            public string ProductName { get; set; }
            public string CreatorName { get; set; }
            public string ProductImage { get; set; }
        }

        public class ScanResult {
            public long ProductId { get; set;}
            public ScanResultType Result { get; set; }
            public string Message { get; set; } 
            public List<TriggerResult> TriggerResults { get; set; }
        }

        public class TriggerResult
        {
            public int Sequence { get; set; }
            public string TriggerName { get; set; }
            public string OggName { get; set; }
            public double LengthMS { get; set; }
        }

        public enum ScanResultType
        {
            Success = 0,
            DatabaseError = 1,
            NetworkError = 2,
            DecodingError = 3,
            SystemError = 4,
            Pending = 5,
            JsonError = 6
        }

        public class TriggerEntry
        {
            public long ProductId { get; set; }

        }

        public class ContentsJsonItem
        {
            [JsonProperty("url")] public string Url { get; set; }
            [JsonProperty("original_dimensions")] public string OriginalDimensions { get; set; }
            [JsonProperty("name")] public string Name { get; set; }
            [JsonProperty("tags")] public string[] Tags { get; set; }
        }


        public async Task ScanDatabases()
        {
            var sda = new SQLiteDataAccess();
            var productList = new List<ProductInfo>();
            var existingProductIDs = new HashSet<long>();

            using (var connProduct = sda.GetProductCacheCxn())
            {
                connProduct.Open();
                var sql = "SELECT product_id as ProductId, products_name as ProductName, manufacturers_name as CreatorName, products_image as ProductImage FROM products ";
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

            var numberTotal = workingProducts.Count;
            if (numberTotal == 0) return;

            var numberComplete = 0;
            var maxConcurrentThreads = 4;
            var semaphore = new SemaphoreSlim(maxConcurrentThreads);
            var tasks = new List<Task>();

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


        }

        public async Task<ScanResult> ScanOne(ProductInfo product)
        {
            var result = new ScanResult { Result = ScanResultType.Pending };
            var sda = new SQLiteDataAccess();

            // See if any ogg files exist
            using (var connAppCache = sda.GetAppCacheCxn())
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                var url = GetUrl(product.ProductId, "_contents.json");
                string httpResult;
                ContentsJsonItem[] contents;
                try
                {
                    httpResult = await client.GetStringAsync(url);
                }
                catch (Exception ex)
                {
                    result.Message = ex.Message;
                    result.Result = ScanResultType.NetworkError;
                    return result;
                }

                try
                {
                    contents = JsonConvert.DeserializeObject<ContentsJsonItem[]>(httpResult);
                }
                catch (Exception ex)
                {
                    result.Message = ex.Message;
                    result.Result = ScanResultType.JsonError;
                    var insertPayload = new {product_id = product.ProductId, has_ogg = 0, title = product.ProductName, creator = product.CreatorName };
                    var sql = "INSERT INTO products (product_id, has_ogg, title, creator) VALUES (@product_id, @has_ogg, @title, @creator)";
                    await connAppCache.ExecuteAsync(sql, insertPayload);
                    return result;
                }

                if (!contents.Any(c => c.Name.ToLower().EndsWith(".ogg"))) // no OGG files found
                {
                    result.Message = $"No OGG files found in product {product.ProductId}";
                    result.Result = ScanResultType.Success;
                    var insertPayload = new { product_id = product.ProductId, has_ogg = 0, title = product.ProductName, creator = product.CreatorName };
                    var sql = "INSERT INTO products (product_id, has_ogg, title, creator) VALUES (@product_id, @has_ogg, @title, @creator)";
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
                    var sql = "INSERT INTO products (product_id, has_ogg, title, creator, image_bytes) VALUES (@product_id, @has_ogg, @title, @creator, @image_bytes)";
                    await connAppCache.ExecuteAsync(sql, insertPayload);
                } 
                catch (Exception)
                {
                    throw; // not sure what to do otherwise, we'll see
                }

                var lengthDict = new ConcurrentDictionary<string, double>();
                var maxConcurrentThreads = 4;
                var semaphore = new SemaphoreSlim(maxConcurrentThreads);
                var tasks = new List<Task>();

                foreach (var item in contents)
                {
                    await semaphore.WaitAsync();
                    try
                    {
                        tasks.Add(Task.Run(async () =>
                        {
                            var urlLookup = (string.IsNullOrEmpty(item.Url)) ? item.Name : item.Url;
                            var musicUrl = GetUrl(product.ProductId, urlLookup);
                            using (var triggerClient = new HttpClient())
                            {
                                using (var stream = await triggerClient.GetStreamAsync(musicUrl))
                                {
                                    lengthDict[item.Name] = NVorbis.VorbisReader.GetOggLengthMS(stream);
                                }
                            }
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
                docIndex.Load(await client.GetStreamAsync(GetUrl(pid, "index.xml")));
                var root = docIndex.DocumentElement;
                long parentId = 0;

                foreach (XmlElement item in root.ChildNodes)
                {
                    if (item.Name == "__DATAIMPORT")
                    {
                        parentId = long.Parse(item.InnerText.Replace("product://", "").Replace("/index.xml", ""));
                    }

                    if (item.Name.StartsWith("Action"))
                    {
                        var nameEl = item.FirstChild as XmlElement;
                        if (nameEl != null) continue;

                        var soundEl = item.SelectSingleNode("Sound") as XmlElement;
                        if (soundEl != null) continue;


                    }
                }

            }



            return result;
        }    
    }
}
