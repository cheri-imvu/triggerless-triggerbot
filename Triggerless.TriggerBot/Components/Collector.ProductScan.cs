using Dapper;
using Microsoft.Data.Sqlite;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using static Triggerless.TriggerBot.Models.Discord;

namespace Triggerless.TriggerBot
{
    partial class Collector
    {
        private class JsonScanResult : ScanResult
        {
            public ContentsJsonItem[] Contents { get; set; }
        }
        private async Task<JsonScanResult> HandleJson(SqliteConnection connAppCache, ProductSearchInfo product, bool record = true, long overridePid = 0)
        {
            JsonScanResult result = new JsonScanResult { Result = ScanResultType.Pending };
            _httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            var pid = overridePid != 0 ? overridePid : product.ProductId;
            var url = GetUrl(pid, "_contents.json");
            string httpResult;
            ContentsJsonItem[] jsonContents;
            try
            {
                httpResult = _httpClient.GetStringAsync(url).Result;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                bool is404 = ex.Message.Contains("404") || (ex.InnerException != null && ex.InnerException.Message.Contains("404"));
                if (is404)
                {
                    if (record)
                    {
                        //NOTE: Very early products may give a 404 error. We should save to DB with has_ogg = 0
                        var insertPayload = new { product_id = product.ProductId, has_ogg = 0, title = product.ProductName, creator = product.CreatorName };
                        var sql = "INSERT INTO products (product_id, has_ogg, title, creator) VALUES (@product_id, @has_ogg, @title, @creator);";
                        lock (_dbLock)
                        {
                            connAppCache.Execute(sql, insertPayload);
                        }
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
                result.Contents = jsonContents;
                result.Result = ScanResultType.Success;
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
            return result;
        }

        private ScanResult AnyOggs(ContentsJsonItem[] contents, SqliteConnection connAppCache, ProductSearchInfo product)
        {
            var result = new ScanResult { Result = ScanResultType.Pending };
            if (!contents.Any(c => c.Name.ToLowerInvariant().EndsWith(".ogg"))) // no OGG files found
            {
                result.Message = $"No OGG files found in product {product.ProductId}";
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
            result.Result = ScanResultType.Success; return result;
        }

        private async Task<ScanResult> DownloadImage(SqliteConnection connAppCache, ProductSearchInfo product)
        {
            var result = new ScanResult { Result = ScanResultType.Pending };
            byte[] imageBytes;
            try
            {
                _httpClient.DefaultRequestHeaders.Clear();
                imageBytes = await _httpClient.GetByteArrayAsync(product.ProductImage);

                // about 75 ms here

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
                result.Result = ScanResultType.Success;
            }
            catch (Exception exc)
            {
                LogLine($"**ERROR: GET Image for {product.ProductName} not downloaded: {exc.Message}");
                result.Message = "Unable to retrieve _product icon";
                result.Result = ScanResultType.NetworkError;
                return result;
            }
            return result;

        }

        private class TriggerListScanResult : ScanResult
        {
            public List<TriggerEntry> Triggers { get; set; } = new List<TriggerEntry>();
            public long ParentId { get; set; }  
        }

        private async Task<TriggerListScanResult> GetTriggerListFromIndex(SqliteConnection connAppCache, ProductSearchInfo product, JsonScanResult jsonResult)
        {
            // Read Index.xml and associate triggers
            TriggerListScanResult result = new TriggerListScanResult { 
                Result = ScanResultType.Pending 
            };
            long parentId = 0;

            var pid = product.ProductId;
            var docIndex = new XmlDocument();
            var url = GetUrl(pid, "index.xml");
            try
            {
                var indexXml = _httpClient.GetStreamAsync(url).Result;
                docIndex.Load(indexXml);
                // 100-120 ms here

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
            //var triggerList = new List<TriggerEntry>();

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
                        result.ParentId = parentId;
                    }
                }

                if (item.Name.StartsWith("Action"))
                {
                    var nameEl = item.SelectSingleNode("Name") as XmlElement;
                    if (nameEl == null) continue;

                    var soundEl = item.SelectSingleNode("Sound") as XmlElement;
                    if (soundEl == null) continue;

                    var oggName = soundEl.InnerText;
                    if (!oggName.ToLowerInvariant().EndsWith(".ogg")) continue;

                    try
                    {
                        string location = String.Empty;
                        if (jsonResult.Contents.Any(j => j.Name.ToLowerInvariant() == oggName.ToLowerInvariant()))
                        {
                            location = jsonResult.Contents.First(j => j.Name.ToLowerInvariant() == oggName.ToLowerInvariant()).Location;
                        }
                        else // This means that the _contents.json file in IMVU has been corrupted
                             // and we'll try to see if we can get it by name
                        {
                            var oggUrl = url.Replace("index.xml", $"{oggName}");
                            var headRequest = new HttpRequestMessage(HttpMethod.Head, oggUrl);
                            var headResponse = await _httpClient.SendAsync(headRequest).ConfigureAwait(false);
                            if (headResponse.IsSuccessStatusCode)
                            {
                                if (headResponse.Content.Headers.ContentLength > 0)
                                {
                                    location = oggName;
                                }
                            }
                        }

                        if (location != String.Empty)
                        {
                            var triggerEntry = new TriggerEntry
                            {
                                ProductId = product.ProductId,
                                OggName = oggName,
                                TriggerName = nameEl.InnerText,
                                Location = location,
                                Sequence = 0,
                                SourceId = product.ProductId,
                                AncestorId = parentId,
                            };
                            result.Triggers.Add(triggerEntry);
                        }
                    }
                    catch (Exception)
                    {
                        result.Result = ScanResultType.JsonError;
                        result.Message = $"Malformed _product file for {product.ProductId}";
                        lock (_dbLock)
                        {
                            connAppCache.Execute($"UPDATE products SET has_ogg = 0 WHERE product_id = {product.ProductId}");
                        }
                        return result;
                    }
                }
            }
            result.Result = ScanResultType.Success;
            result.ParentId = parentId;
            return result;
        }

        private TriggerListScanResult AssignTriggerSequences(List<TriggerEntry> triggerList)
        {
            TriggerListScanResult result = new TriggerListScanResult { 
                Result = ScanResultType.Pending, 
                Triggers = triggerList
            };

            if (!triggerList.Any())
            {
                result.Message = "Triggers were not found in XML file";
                result.Result = ScanResultType.ZeroTriggers;
                LogLine($"  Done. No triggers found.");
                return result;
            }

            var numbers = "0123456789".ToCharArray();
            foreach (var trigger in triggerList)
            {
                bool hitNumber = false;
                string numberString = string.Empty;
                var pieces = trigger.TriggerName.Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                string firstPiece = pieces.Length > 0 ? pieces[0] : string.Empty;
                if (firstPiece == string.Empty)
                {
                    result.Message = "At least one trigger has a null prefix";
                    result.Result = ScanResultType.NullPrefixes;
                    LogLine(result.Message);
                    return result;
                }

                foreach (var c in firstPiece)
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
                        int seq;
                        if (int.TryParse(numberString, out seq))
                        {
                            trigger.Sequence = seq;
                        }
                        else
                        {
                            trigger.Sequence = -1;
                        }
                    }
                }
            }
            if (triggerList.Any(t => t.Sequence == -1))
            {
                result.Message = "Unable to assign sequence numbers to all triggers";
                result.Result = ScanResultType.NoUsefulTriggers;
                LogLine($"  Done. {result.Message}");
                return result;
            }
            else
            {
                result.Result = ScanResultType.Success;
            }
            return result;
        }

        private async Task<TriggerListScanResult> FindAncestorTriggers(
            SqliteConnection connAppCache, List<TriggerEntry> triggerList, 
            ProductSearchInfo product)
        {
            var result = new TriggerListScanResult { 
                Result = ScanResultType.Pending 
            };

            // Base case: if there are no triggers, return what we have with a ZeroTriggers status
            if (!triggerList.Any())
            {
                result = new TriggerListScanResult
                {
                    Result = ScanResultType.ZeroTriggers,
                    Triggers = triggerList,
                    Message = "No triggers to find ancestor for"
                };
                return result;
            }

            var accumulatedTriggers = triggerList;
            long targetPid = product.ProductId;
            long currentPid = product.ProductId;
            long ancestorId = triggerList.First().AncestorId;

            var currentProduct = new ProductSearchInfo { 
                ProductId = currentPid ,
                ProductName = product.ProductName,
                CreatorName = product.CreatorName
            };

            Func<List<TriggerEntry>, int> getMinSequence = triggers =>
                triggers.Select(t => t.Sequence).Min();

            // Iteratively find ancestors until we find a product with Trigger 1,
            // or we run out of ancestors, or encounter an error. n most cases we
            // will not need to enter the loop at all.
            // 

            int minSequence = getMinSequence(accumulatedTriggers);
            while (getMinSequence(accumulatedTriggers) > 1)
            {
                currentPid = ancestorId;
                // Get the JSON for the ancestor product
                
                currentProduct.ProductId = currentPid;
                var jsonResult = await HandleJson(
                    connAppCache, currentProduct, false, currentPid).ConfigureAwait(false);

                if (jsonResult.Result != ScanResultType.Success)
                {
                    result = new TriggerListScanResult
                    {
                        Result = ScanResultType.AncestorUnavailable,
                        Triggers = triggerList,
                        Message = $"Unable to retrieve ancestor product {ancestorId} JSON"
                    };
                    return result;
                }

                if (jsonResult.Contents == null || jsonResult.Contents.Length == 0)
                {
                    // The ancestor product has no JSON contents, so we can't find any triggers.
                    // This is normal and typical, assume that this product doesn't
                    // have any ancestor triggers and return what we have with a Success status.
                    result.Result = ScanResultType.Success;
                    result.Triggers = accumulatedTriggers;
                    result.Message = $"Ancestor product {ancestorId} has no contents, assuming no triggers.";
                    return result;
                }

                bool ancestorHasOggs = jsonResult.Contents.Any(j =>
                    (j.Name != null && j.Name.ToLowerInvariant().EndsWith(".ogg")) ||
                    (j.Url != null && j.Url.ToLowerInvariant().EndsWith(".ogg")));

                if (!ancestorHasOggs)
                {
                    result.Result = ScanResultType.Success;
                    result.Triggers = accumulatedTriggers;
                    result.Message = $"Ancestor product {ancestorId} has no ogg files, assuming no triggers.";
                    return result;
                }

                // Get the triggers for the ancestor product from the index.xml
                var ancestorResult = await GetTriggerListFromIndex(
                    connAppCache, currentProduct, jsonResult).ConfigureAwait(false);
                if (ancestorResult.Result != ScanResultType.Success)
                {
                    result = new TriggerListScanResult
                    {
                        Result = ScanResultType.AncestorUnavailable,
                        Message = $"Unable to retrieve ancestor product {ancestorId} triggers: {ancestorResult.Message}"
                    };
                    return result;
                }
                var ancestorTriggers = ancestorResult.Triggers;

                // Patch these so that they have the same pinned product ID,
                // so we can use them 
                ancestorId = ancestorTriggers.First().AncestorId; // we will need this for the next loop if we don't find Trigger 1 in this ancestor
                foreach (var trigger in ancestorTriggers) 
                {
                    trigger.ProductId = targetPid;
                    trigger.SourceId = currentPid;
                    trigger.AncestorId = ancestorId; // they should all have the same ancestor ID, so we can just take it from the first one
                }


                // Assign trigger sequences based on trigger names
                var sequencedResult = AssignTriggerSequences(ancestorTriggers);  
                if (sequencedResult.Result != ScanResultType.Success)
                {
                    result = new TriggerListScanResult
                    {
                        Result = ScanResultType.AncestorUnavailable,
                        Message = $"Unable to assign trigger sequences for ancestor product {currentPid}: {sequencedResult.Message}"
                    };
                    return result;
                }

                accumulatedTriggers.AddRange(sequencedResult.Triggers);
                accumulatedTriggers = accumulatedTriggers.OrderBy(t => t.Sequence).ToList();

                currentPid = ancestorId;
            }

            // We've found a product with Trigger 1, so we can stop.
            // We return Success even if there are missing sequence numbers.

            result = new TriggerListScanResult
            {
                Result = ScanResultType.Success,
                Triggers = accumulatedTriggers,
                Message = "Found Trigger 1"
            };
            result.Triggers = result.Triggers.OrderBy(t => t.Sequence).ToList();
            return result;

        }

        private ScanResult SaveToDb(SqliteConnection connAppCache, List<TriggerEntry> triggerList, ProductSearchInfo product)
        {
            ScanResult result = new ScanResult { Result = ScanResultType.Pending };
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
            return result;
        }
    }
}
