using Dapper;
using Newtonsoft.Json;
using NVorbis;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel;
using System.Data.Entity.Migrations.Sql;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using Triggerless.TriggerBot.Models;

namespace Triggerless.TriggerBot
{
    public partial class Collector : Component
    {
        private object _dbLock = new object();
        private object _logLock = new object();
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

        //public Task ScanDatabasesAsync(string whereClause = null)
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
                    // run sychronously for now
                    //var result = await ScanOne(_product, cxnAppCache);
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
        }

        public async Task<ScanResult> ScanProductAsync(ProductSearchInfo product, System.Data.SQLite.SQLiteConnection connAppCache)
        {
            var processorCount = Environment.ProcessorCount;
            var result = new ScanResult { Result = ScanResultType.Pending };
            var sda = new SQLiteDataAccess();
            const int MIN_NUMBER_OF_OGGS = 2;

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
                    httpResult = client.GetStringAsync(url).Result;
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
                        lock (_dbLock)
                        {
                            connAppCache.Execute(sql, insertPayload);
                        }
                        result.Message = "  Unavailable Product could not be downloaded (404)";
                        LogLine($"  Done. {product.ProductName} {result.Message}");
                        result.Result = ScanResultType.ProductUnavailable;
                    }
                    else
                    {
                        LogLine($"**Done. ERROR {product.ProductName} {ex.Message}");
                        result.Result = ScanResultType.NetworkError;
                    }
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
                    var insertPayload = new { product_id = product.ProductId, has_ogg = 0, title = product.ProductName, creator = product.CreatorName };
                    var sql = "INSERT INTO products (product_id, has_ogg, title, creator) VALUES (@product_id, @has_ogg, @title, @creator);";
                    lock (_dbLock)
                    {
                        connAppCache.Execute(sql, insertPayload);
                    }
                    LogLine($"**ERROR: {product.ProductName} JSON can't be deserialized");
                    return result;
                }
                #endregion

                #region Any OGG Files?
                if (!jsonContents.Any(c => c.Name.ToLower().EndsWith(".ogg"))) // no OGG files found
                {
                    result.Message = $"No OGG files found in _product {product.ProductId}";
                    result.Result = ScanResultType.NoUsefulTriggers;
                    var insertPayload = new { product_id = product.ProductId, has_ogg = 0, title = product.ProductName, creator = product.CreatorName };
                    var sql = "INSERT INTO products (product_id, has_ogg, title, creator) VALUES (@product_id, @has_ogg, @title, @creator);";
                    lock (_dbLock)
                    {
                        connAppCache.Execute(sql, insertPayload);
                    }
                    LogLine($"  Done: {result.Result} {product.ProductName}");
                    return result;
                }
                #endregion

                #region Product Image
                try
                {
                    client.DefaultRequestHeaders.Clear();
                    var imageBytes = client.GetByteArrayAsync(product.ProductImage).Result;

                    var insertPayload = new
                    {
                        product_id = product.ProductId,
                        has_ogg = 1,
                        title = product.ProductName,
                        creator = product.CreatorName,
                        image_bytes = imageBytes
                    };
                    var sql = "INSERT INTO products (product_id, has_ogg, title, creator, image_bytes) VALUES (@product_id, @has_ogg, @title, @creator, @image_bytes);";
                    lock (_dbLock)
                    {
                        connAppCache.Execute(sql, insertPayload);
                    }
                }
                catch (Exception exc)
                {
                    LogLine($"**ERROR: GET Image for {product.ProductName} not downloaded: {exc.Message}");
                    result.Message = "Unable to retrieve _product icon";
                    result.Result = ScanResultType.NetworkError;
                    return result;
                }
                #endregion

                #region Read Index.xml and associate triggers

                var pid = product.ProductId;
                var docIndex = new XmlDocument();
                try
                {
                    var indexXml = client.GetStreamAsync(GetUrl(pid, "index.xml")).Result;
                    docIndex.Load(indexXml);
                }
                catch (XmlException exc) 
                {
                    result.Result = ScanResultType.XmlError;
                    result.Message = "ERROR: Malformed index XML";
                    LogLine($"**{result.Message} {product.ProductName} {exc.Message}");
                    return result;

                }
                catch (Exception)
                {
                    LogLine($"ERROR: GET for Index failed {product.ProductName}");
                    result.Result = ScanResultType.NetworkError;
                    result.Message = "Unable to retrieve index.xml file";
                    return result;
                }

                var root = docIndex.DocumentElement;
                long parentId;
                var triggerList = new List<TriggerEntry>();

                foreach (XmlElement item in root.ChildNodes)
                {
                    if (item.Name == "__DATAIMPORT")
                    {
                        long tryparentId;
                        string substituted = item.InnerText.Replace("product://", "").Replace("/index.xml", "");
                        var parsed = long.TryParse(substituted, out tryparentId);
                        if (!parsed)
                        {
                            var msg = $"**Error: Can't parse parent ID from {substituted} ";
                            result.Result = ScanResultType.XmlError;
                            result.Message = msg;
                            LogLine(msg);
                            return result;
                        }
                        else
                        {
                            parentId = tryparentId;
                        }
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
                        catch (Exception)
                        {
                            result.Result = ScanResultType.JsonError;
                            result.Message = "Malformed _product file";
                            lock (_dbLock)
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
                    result.Result = ScanResultType.ZeroTriggers;
                    LogLine($"  Done. {product.ProductName} {result.Result}");
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
                    lock (_dbLock)
                    {
                        connAppCache.Execute($"UPDATE products SET has_ogg = 0 WHERE product_id = {product.ProductId}");
                    }
                    result.Result = ScanResultType.NullPrefixes;
                    result.Message = $"Null prefixes are not allowed";
                    LogLine($"  Done. {product.ProductName} {result.Result}");
                    return result;
                }

                var distinctPrefixes = triggerList.Select(t => t.Prefix.ToLower()).Distinct().Count();
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

                int maxThreads = 2 * processorCount / 3;
                var semaphore = new SemaphoreSlim(maxThreads);
                bool successAll = true;

                var tasks = triggerList.Select(async trigger =>
                {
                    var tryCount = 0;
                    var tryMax = 10;
                    await semaphore.WaitAsync().ConfigureAwait(false);
                    try
                    {
                        var start = DateTime.Now;
                        var urlLookup = trigger.Location;
                        var musicUrl = GetUrl(product.ProductId, urlLookup);

                        using (var triggerClient = new HttpClient())
                        {
                            bool bSuccess = false;
                            while (tryCount < tryMax)
                            {
                                try
                                {
                                    await Task.Delay(50).ConfigureAwait(false);
                                    trigger.LengthMS = await GetOggLengthMsAsync(client, musicUrl).ConfigureAwait(false);

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

                #endregion

                if (!successAll)
                {
                    result.Result = ScanResultType.NetworkError;
                    result.Message = "At least one trigger could not be downloaded.";
                    return result;
                }

                #region Save Triggers to DB
                var sqlInsertTrigger = "INSERT INTO product_triggers (product_id, prefix, sequence, trigger, ogg_name, length_ms, location) VALUES " +
                        "(@ProductId, @Prefix, @Sequence, @TriggerName, @OggName, @LengthMS, @Location);";

                var triggers = triggerList.OrderBy(t => t.Sequence).ThenBy(t => t.TriggerName).ToList();
                int seq = 1;
                foreach (var t in triggers)
                {
                    t.Sequence = seq++;
                }

                lock (_dbLock)
                {
                    foreach (var trigger in triggers)
                    {
                        connAppCache.Execute(sqlInsertTrigger, trigger);
                    }
                }
                result.Result = ScanResultType.Success;
                result.Message = $"{triggerList.Count} triggers found and added";
                LogLine($"  Done. {product.ProductName} {result.Message}");
                #endregion

                return result;

            }
        }

        public static async Task<double> GetOggLengthMsAsync(HttpClient client, string url)
        {
            // let caller handle any exceptions
            using (var net = await client.GetStreamAsync(url).ConfigureAwait(false))
            using (var ms = new MemoryStream())
            {
                await net.CopyToAsync(ms).ConfigureAwait(false);
                ms.Position = 0; // rewind before reading

                using (var vorb = new VorbisReader(ms, false)) // don't close ms automatically
                {
                    return vorb.TotalTime.TotalMilliseconds;
                }
            }
        }



    }
}
