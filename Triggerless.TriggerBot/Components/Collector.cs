using Dapper;
using Microsoft.Data.Sqlite;
using NAudio.Vorbis;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel;

using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using Triggerless.TriggerBot.Components;
using Triggerless.TriggerBot.Models;
using static Triggerless.TriggerBot.Models.Discord;

namespace Triggerless.TriggerBot
{
    public partial class Collector : Component
    {
        private readonly object _dbLock = new object();
        private readonly object _logLock = new object();
        private string _log = string.Empty;
        private StringBuilder _logBuffer = new StringBuilder();

        private void LogLine(string text)
        {
            lock (_logLock)
            {
                _logBuffer.AppendLine(text);
                Debug.WriteLine(text);
            }
        }

        public string Log { get => _log; }


        public static string GetUrlTemplate(long pid) => $"https://userimages-akm.imvu.com/productdata/{pid}/1/{{0}}";

        public static string GetUrl(long pid, string filename) => string.Format(GetUrlTemplate(pid), filename);

        public bool ClearAppCache()
        {
            var result = false;

            using (var conn = SQLiteDataAccess.GetAppCacheCxn())
            {
                try
                {
                    conn.Open();
                    var sql = "DELETE FROM product_triggers;";
                    conn.Execute(sql);
                    sql = "DELETE FROM products;";
                    conn.Execute(sql);
                    result = true;
                }
                catch 
                { 
                    result = false;
                }

            }

            return result;
        }

        public bool ExcludeSong(long productId)
        {
            bool result = false;
            using (var conn = SQLiteDataAccess.GetAppCacheCxn())
            {
                try
                {
                    conn.Open();
                    var sql = $"UPDATE products SET has_ogg = 0 WHERE product_id = {productId}";
                    conn.Execute(sql);
                    result = true;
                }
                catch (Exception)
                {
                    //return false;
                }
            }
            return result;

        }

        public class ProductSearchEntry
        {
            public ProductSearchEntry(long productId, string name, string creator)
            {
                ProductId = productId;
                Name = name;
                Creator = creator;
            }
            public long ProductId { get; set; }
            public string Name { get; set; }
            public string Creator { get; set; }
        }

        public List<ProductSearchEntry> DeepScanList(string searchTerm)
        {
            List<ProductSearchEntry> result = new List<ProductSearchEntry>();

            IEnumerable<long> productIdsWithTriggers;
            using (var appConnection = SQLiteDataAccess.GetAppCacheCxn())
            { 
                appConnection.Open();
                var sql = $@"
                    SELECT product_id FROM products WHERE has_ogg = 1
                ";
                productIdsWithTriggers = appConnection.Query<long>(sql);
            }

            using (var imvuConnection = SQLiteDataAccess.GetProductCacheCxn())
            {
                imvuConnection.Open();
                var builder = new StringBuilder();
                builder.Append($@"
                    SELECT id AS ProductId, 
                    products_name AS Name, 
                    manufacturers_name AS Creator
                    FROM products
                    WHERE (manufacturers_name LIKE '%{searchTerm}%'
                    OR products_name LIKE '%{searchTerm}%')
                    AND id NOT IN (
                ");
                string delimiter = string.Empty;
                foreach (var id in productIdsWithTriggers)
                {
                    builder.Append(delimiter + id);
                    delimiter = ", ";
                }
                builder.Append(")");
                foreach (var item in imvuConnection.Query<ProductSearchEntry>(builder.ToString()))
                {
                    result.Add(item);
                }
            }
            return result;
        }

        public async Task DeepScanThese(List<long> selectedProductIds)
        {
            if (selectedProductIds.Count == 0) return;

            StringBuilder builder = new StringBuilder();
            var delimiter = string.Empty;
            builder.Append("WHERE product_id IN (");
            foreach (var productId in selectedProductIds)
            {
                builder.Append(delimiter +  productId);
                delimiter = ", ";
            }
            builder.Append(")");
            string whereClause = builder.ToString();

            using (var appConnection = SQLiteDataAccess.GetAppCacheCxn())
            {
                appConnection.Open();
                var sql = $"DELETE FROM product_triggers {whereClause}";
                appConnection.Execute(sql);
                sql = $"DELETE FROM products {whereClause}";
                appConnection.Execute(sql);
            }

            await ScanDatabasesAsync(whereClause.Replace("product_id", "id"));
        }

        private HttpClient _httpClient;

        public async Task ScanDatabasesAsync(string whereClause = null)
        {
            var productList = new List<ProductSearchInfo>();
            var existingProductIDs = new HashSet<long>();

            using (var connProduct = SQLiteDataAccess.GetProductCacheCxn())
            {
                connProduct.Open();
                var sql = "SELECT id as ProductId, products_name as ProductName, manufacturers_name as CreatorName, products_image as ProductImage FROM products ";
                sql += whereClause == null ?
                    $"WHERE {SQLiteDataAccess.AccessoryFilter}; " :
                    whereClause;
                var products = connProduct.Query<ProductSearchInfo>(sql);
                productList.AddRange(products);
            }

            using (var connAppCache = SQLiteDataAccess.GetAppCacheCxn())
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
                //return;
            }

            var numberComplete = 0;
            var cycleStart = DateTime.Now;

            if (_logBuffer == null) { _logBuffer = new StringBuilder(); }
            _logBuffer.Clear();

            var stats = new Dictionary<ScanResultType, int>();
            foreach (ScanResultType srt in (ScanResultType[])Enum.GetValues(typeof(ScanResultType))) // i hate this syntax
            {
                stats[srt] = 0;
            }
            double longest = 0;
            string longestName = "No Product";
            ScanResultType longestResultType = ScanResultType.Pending;

            using (var cxnAppCache = SQLiteDataAccess.GetAppCacheCxn())
            using (_httpClient = new HttpClient())
            {

                foreach (var product in workingProducts)
                {
                    var start = DateTime.Now;
                    FireEvent(new CollectorEventArgs
                    {
                        CompletedProducts = numberComplete + 1,
                        TotalProducts = numberTotal,
                        Message = $"{product.ProductName}"
                    });

                    try 
                    {
                        var result = await ScanProductAsync(product, cxnAppCache);
                        stats[result.Result]++;
                        var elapsed = (DateTime.Now - start).TotalMilliseconds;
                        if (elapsed > longest) 
                        { 
                            longest = elapsed;
                            longestName = product.ProductName;
                            longestResultType = result.Result;
                        }
                        LogLine($"  Product complete: {product.ProductName}\t{result.Result}\t{result.Message} took {elapsed} ms");

                        Interlocked.Increment(ref numberComplete);
                    }
                    catch (Exception ex) 
                    {
                        LogLine($"**ERROR: {ex.Message}");
                    }   
                }

                var sqlCleanup = @"
                    UPDATE products SET has_ogg = 0 WHERE has_ogg = 1
                    AND NOT EXISTS (SELECT product_id FROM product_triggers WHERE product_id = products.product_id);
                ";
                lock (_dbLock)
                {
                    cxnAppCache.Execute(sqlCleanup);
                }

            }

            var timeElapsed = (DateTime.Now - cycleStart);
            LogLine("\n----------------------- SUMMARY --------------------------------------------");
            LogLine($@"Cycle complete {workingProducts.Count} items. Time elapsed {timeElapsed:mm\:ss\.fff}");

            foreach (var entry in stats)
            {
                var line = "  " + entry.Key.ToString().PadRight("ProductUnavailable".Length) + entry.Value.ToString().PadLeft(5);
                LogLine(line);
            }
            LogLine($"The slowest product was {longestName} which took ({longest} msec) with result {longestResultType}");

            _log = _logBuffer.ToString();
            _logBuffer.Clear();

            if (workingProducts.Count > 0)
            {
                dynamic payload = new
                {
                    Version = PlugIn.Shared.VersionNumber,
                    ImvuVersion = PlugIn.Shared.ImvuVersion,
                    ElapsedSecs = timeElapsed.TotalSeconds,
                    ProductCount = workingProducts.Count,
                    LongestName = longestName,
                    LongestResultType = longestResultType,
                    LongestMSecs = longest
                };
                await TriggerlessApiClient.SendEventAsync(
                    TriggerlessApiClient.EventType.ScanComplete, payload);
            }
        }

        public async Task<ScanResult> ScanProductAsync(ProductSearchInfo product, Microsoft.Data.Sqlite.SqliteConnection connAppCache)
        {

            var processorCount = Environment.ProcessorCount;
            var result = new ScanResult { Result = ScanResultType.Pending };
            var sda = new SQLiteDataAccess();
            const int MIN_NUMBER_OF_OGGS = 2;

            // See if any ogg files exist

            #region JsonContents
            var jsonResult = await HandleJson(connAppCache, product).ConfigureAwait(false);
            #endregion

            ScanResult anyOggsResult = AnyOggs(jsonResult.Contents, connAppCache, product);
            if (anyOggsResult.Result != ScanResultType.Success)
            {
                return anyOggsResult;
            }

            // about 35 ms here
            ScanResult imgResult = await DownloadImage(connAppCache, product).ConfigureAwait(false); ;
            if (imgResult.Result != ScanResultType.Success)
            {
                return imgResult;
            }

            TriggerListScanResult triggerListResult =
                    await GetTriggerListFromIndex(connAppCache, product, jsonResult).ConfigureAwait(false);
            if (triggerListResult.Result != ScanResultType.Success)
            {
                return triggerListResult;
            }

            triggerListResult = AssignTriggerSequences(triggerListResult.Triggers);
            if (triggerListResult.Result != ScanResultType.Success)
            {
                return triggerListResult;
            }

            var triggerList = triggerListResult.Triggers;

            TriggerListScanResult ancestorResult = await FindAncestorTriggers(connAppCache, triggerList, product).ConfigureAwait(false);
            if (ancestorResult.Result != ScanResultType.Success)
            {
                return ancestorResult;
            }
            triggerList = ancestorResult.Triggers;

            #region Filter Out Crappy Triggers
            triggerList = triggerList.Where(t => t.Sequence != -1).OrderBy(t => t.Prefix).ThenBy(t => t.Sequence).ToList();

            if (triggerList.Any(t => t.Prefix is null))
            {
                lock (_dbLock)
                {
                    connAppCache.Execute($"UPDATE products SET has_ogg = 0 WHERE product_id = {product.ProductId}");
                }
                result.Result = ScanResultType.NullPrefixes;
                result.Message = $"Null prefixes are not allowed";
                LogLine($"  Done. {product.ProductName} {result.Result}");
                return result;
            }

            var distinctPrefixes = triggerList.Select(t => t.Prefix.ToLowerInvariant()).Distinct().Count();
            if (distinctPrefixes != 1)
            {
                lock (_dbLock)
                {
                    connAppCache.Execute($"UPDATE products SET has_ogg = 0 WHERE product_id = {product.ProductId}");
                }
                result.Result = ScanResultType.MixedPrefixes;
                result.Message = $"Too many trigger prefixes ({distinctPrefixes})";
                LogLine($"  Done. {product.ProductName} {result.Result}");
                return result;
            }


            if (triggerList.Count == 0)
            {
                lock (_dbLock)
                {
                    connAppCache.Execute($"UPDATE products SET has_ogg = 0 WHERE product_id = {product.ProductId}");
                }
                result.Result = ScanResultType.ZeroTriggers;
                result.Message = $"No Useful triggers found";
                LogLine($"  Done. {product.ProductName} {result.Result}");
                return result;
            }

            if (triggerList.Count < MIN_NUMBER_OF_OGGS)
            {
                lock (_dbLock)
                {
                    connAppCache.Execute($"UPDATE products SET has_ogg = 0 WHERE product_id = {product.ProductId}");
                }
                result.Result = ScanResultType.NotEnoughOggs;
                result.Message = $"{MIN_NUMBER_OF_OGGS} Triggers required to qualify as a song";
                LogLine($"  Done. {product.ProductName} {result.Result} {MIN_NUMBER_OF_OGGS} required {triggerList.Count} found");
                return result;
            }
            #endregion



            #region Retrieve OGG file lengths

            bool hasOggLengths = false;

            // 150-200 ms here

            // 1.1 addition - use online thing

            bool successAll = true;
            var scanUrl = $"{PlugIn.Location.TriggerlessDomain}/api/bot/ogg/lengths";
            var payload = new CollectorPayload
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                ImageLocation = product.ProductImage,
                //ImageBytes = imageBytes,
                CreatorName = product.CreatorName,
                DateCreated = DateTime.UtcNow,
                AddedBy = Program.Cid
            };
            foreach (var entry in triggerList)
            {
                payload.Triggers.Add(new Models.TriggerEntry
                {
                    ProductId = entry.SourceId,
                    TriggerName = entry.TriggerName,
                    Prefix = entry.Prefix,
                    Sequence = entry.Sequence,
                    OggName = entry.OggName,
                    Location = entry.Location,
                    SourceId = entry.SourceId,
                });
            }
            var json = JsonConvert.SerializeObject(payload);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                var httpResponse = await _httpClient.PostAsync(scanUrl, content).ConfigureAwait(false);
                using (Stream stream = await httpResponse.Content.ReadAsStreamAsync().ConfigureAwait(false))
                using (StreamReader reader = new StreamReader(stream)) 
                {
                    json = reader.ReadToEnd();
                }
                // this is where most of the time is spent 300-800 ms

                var response = JsonConvert.DeserializeObject<CollectorResponsePayload>(json);
                if (response != null && response.Result == ScanResultType.Success && response.Triggers.Any())
                {
                    foreach (var trigger in response.Triggers)
                    {
                        var sequence = trigger.Sequence;
                        var entry = triggerList.First(x => x.Sequence == sequence);
                        if (entry != null)
                        {
                            entry.LengthMS = trigger.LengthMS;
                        }
                    }
                    hasOggLengths = true;
                }
            }
            catch (Exception)
            {
                hasOggLengths = false;
            }

            // If we couldn't do this online, it could be that triggerless.com is down
            // Go back to the 1.0 way of doing this

            if (!hasOggLengths)
            {
                int maxThreads = 2 * processorCount / 3;
                var semaphore = new SemaphoreSlim(maxThreads);

                var tasks = triggerList.Select(async trigger =>
                {
                    var tryCount = 0;
                    var tryMax = 10;
                    await semaphore.WaitAsync().ConfigureAwait(false);
                    try
                    {
                        var start = DateTime.Now;
                        var urlLookup = trigger.Location;
                        var pid = trigger.SourceId == 0 ? trigger.ProductId : trigger.SourceId;
                        var musicUrl = GetUrl(pid, urlLookup);

                        using (var triggerClient = new HttpClient())
                        {
                            bool bSuccess = false;
                            while (tryCount < tryMax)
                            {
                                try
                                {
                                    await Task.Delay(50).ConfigureAwait(false);
                                    trigger.LengthMS = await GetOggLengthMsAsync(_httpClient, musicUrl).ConfigureAwait(false);

                                    LogLine($"    TRIGGER OK: {trigger.OggName} ({(DateTime.Now - start).TotalMilliseconds} ms) {product.ProductName}");
                                    bSuccess = true;
                                    break;
                                }
                                catch (Exception exc)
                                {
                                    tryCount++;
                                    LogLine($"  **TRIGGER GET failed Try {tryCount}: {product.ProductName} {trigger.OggName} {trigger.TriggerName}");
                                    LogLine($"  **>> {exc.Message}");
                                    await Task.Delay(50 * 2 * tryCount).ConfigureAwait(false);
                                }
                            }
                            if (!bSuccess)
                            {
                                string ouch = $"Unable to read trigger {trigger.TriggerName} for {product.ProductName} (pid = {product.ProductId}) after {tryMax} tries";
                                _ = await Discord.SendMessage("Scan Failure", ouch).ConfigureAwait(false);
                                LogLine($"  !!OUCH: {ouch}");
                                successAll = false;
                            }
                        }
                    }
                    catch (HttpRequestException)
                    {
                        var msg = $"  **TRIGGER GET failed on last try {product.ProductName} {trigger.TriggerName} {trigger.OggName}";
                        LogLine(msg);
                        _ = await Discord.SendMessage("Trigger Download Failure", msg).ConfigureAwait(false);
                    }
                    catch (Exception exc)
                    {
                        LogLine($"**ERROR: {product.ProductName} {exc.Message}");
                    }
                    finally
                    {
                        semaphore.Release();
                    }
                }).ToImmutableArray();

                await Task.WhenAll(tasks).ConfigureAwait(false);

            }

            #endregion

            if (!successAll)
            {
                result.Result = ScanResultType.NetworkError;
                result.Message = "At least one trigger could not be downloaded.";
                return result;
            }

            ScanResult dbResult = SaveToDb(connAppCache, triggerList, product);

            return dbResult;
        }



        public static async Task<double> GetOggLengthMsAsync(HttpClient client, string url)
        {
            // let caller handle any exceptions
            using (var net = await client.GetStreamAsync(url).ConfigureAwait(false))
            using (var ms = new MemoryStream())
            {
                await net.CopyToAsync(ms).ConfigureAwait(false);
                ms.Position = 0; // rewind before reading

                using (var vorb = new VorbisWaveReader(ms, false)) // don't close ms automatically
                {
                    return vorb.TotalTime.TotalMilliseconds;
                }
            }
        }



    }
}
