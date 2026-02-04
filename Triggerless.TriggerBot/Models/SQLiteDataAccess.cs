using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Xml.Linq;
using Triggerless.TriggerBot.Models;
using static Triggerless.TriggerBot.Models.Discord;

namespace Triggerless.TriggerBot
{
    public class SQLiteDataAccess
    {
        #region Accessory Filters

        public enum Gender { Female, Male }

        public sealed class Filter
        {
            public Gender Gender { get; private set; }
            public string Name { get; private set; }
            public short[] Map { get; private set; }
            public bool IsAccessory { get; private set; }
            public Filter(Gender gender, bool isAccessory, string name, params short[] map) {
                Gender = gender; IsAccessory = isAccessory; Name = name ?? ""; Map = map ?? new short[0];
            }
            public override string ToString()
            {
                return "[" + string.Join(", ", Map.Cast<string>()) + "]";
            }

            private Filter() { }
            public short[] OldCpath { 
                get 
                {
                    if (!IsAccessory)
                    {
                        return Combine((Gender == Gender.Male ? MaleOld : FemaleOld), Map);
                    }
                    var map = Map.Length == 2 ? new short[1] { Map[1] } : Map;
                    return Combine(
                        Gender == Gender.Male ? 
                        MaleOldAcc : FemaleOldAcc,
                        map);
                } 
            }

            public short[] NewCpath
            {
                get
                {
                    if (!IsAccessory) {
                        return Combine(MiscNew, Map);
                    }
                    var genderArray = (Gender == Gender.Male) ? MaleNewAcc : FemaleNewAcc;
                    return Combine(genderArray, Map);
                }
            }

            public string OldCpathSql => JoinCpath(OldCpath);
            public string NewCpathSql => JoinCpath(NewCpath);
            private string JoinCpath(short[] numbers) => "[" + string.Join(", ", numbers) + "]";
        }

        public static readonly IReadOnlyList<Filter> Filters = new[]
        {
            new Filter(Gender.Male, true,   "Glasses Kings",       148),
            new Filter(Gender.Male, true,   "New Accessories",     165),
            new Filter(Gender.Male, true,   "Headphones",          334),
            new Filter(Gender.Male, true,   "Spice Glasses",       416),
            new Filter(Gender.Male, true,   "Boomboxes",           3139, 1072),
            new Filter(Gender.Male, true,   "Guitar",              1250),
            new Filter(Gender.Male, true,   "Guitars",             1251),
            new Filter(Gender.Male, true,   "DJ System",           3139, 1268),
            new Filter(Gender.Male, true,   "Actions",             1329),
            new Filter(Gender.Male, true,   "Cow Bells",           1437),
            new Filter(Gender.Male, true,   "Frameless Glasses",   1927),
            new Filter(Gender.Male, true,   "Music Items",         3139, 2501),
            new Filter(Gender.Male, true,   "Miscellaneous",       3095),
            new Filter(Gender.Male, false,   "Miscellaneous",      3093),
            new Filter(Gender.Male, true, "Sounds & Effects (generic, new)", 3139),
            new Filter(Gender.Male, true,   "Legacy",      165),  // <== get the right number

            new Filter(Gender.Female, true, "Spice Glasses",       3122, 144),
            new Filter(Gender.Female, true, "New Accessories",     155),
            new Filter(Gender.Female, true, "King Glasses",        372),
            new Filter(Gender.Female, true, "Rings",               3121, 615),
            new Filter(Gender.Female, true, "Boomboxes",           3120, 1071),
            new Filter(Gender.Female, true, "DJ System",           3120, 1281),
            new Filter(Gender.Female, true, "Actions",             1328),
            new Filter(Gender.Female, true, "Music Item",          2500),
            new Filter(Gender.Female, true, "Crimson Sounds",      2935),
            new Filter(Gender.Female, true, "Triggerless Music",   2952),
            new Filter(Gender.Female, true, "!!Music!!",           3120, 2956),
            new Filter(Gender.Female, true, "Misc./Other Acc",     3094),
            new Filter(Gender.Female, false, "Miscellaneous",      3092),
            new Filter(Gender.Female, true, "Sounds & Effects (generic, new)", 3120),
            new Filter(Gender.Female, true, "Jewelry (generic, new)", 3121),
            new Filter(Gender.Female, true, "Eyewear (generic, new)", 3122),
            new Filter(Gender.Female, true, "Legacy", 155) // get the right number
        };

        private static readonly short[] MaleOldAcc = { 106, 41, 71 };
        private static readonly short[] FemaleOldAcc = { 106, 40, 153 };
        private static readonly short[] MaleNewAcc = { 3117, 71 };
        private static readonly short[] FemaleNewAcc = { 3117, 153 };
        private static readonly short[] MiscNew = { 3110 };

        // gender prefixes
        private static short[] MaleOld => MaleOldAcc.Take(2).ToArray();
        private static short[] FemaleOld = FemaleOldAcc.Take(2).ToArray();
        private static readonly short[] AccNew = { 3117 };

        // Built once; preserves original order: all “new” first, then “old”
        
        public static readonly List<short[]> FilterCpaths = BuildFilterCpaths();

        private static List<short[]> BuildFilterCpaths()
        {
            var resultNew = Filters.Select(f => f.NewCpath).ToList();
            var resultOld = Filters.Select(f => f.OldCpath);
            resultNew.AddRange(resultOld);
            return resultNew;
        }

        private static short[] Combine(short[] a, short[] b)
        {
            var r = new short[a.Length + b.Length];
            Array.Copy(a, 0, r, 0, a.Length);
            Array.Copy(b, 0, r, a.Length, b.Length);
            return r;
        }

        // If you had this before, it still works:
        public static string AccessoryFilter =>
            " cPath IN ('" + string.Join("', '", FilterCpaths.Select(arr => arr.ToCpath())) + "')";

        #endregion

        public static SQLiteConnection GetProductCacheCxn()
        {            
            if (!File.Exists(PlugIn.Location.ProductCacheFile))
            {
                throw new ApplicationException("IMVU Classic Client is not installed");
            }

            return new SQLiteConnection($"Data Source={PlugIn.Location.ProductCacheFile}");
        }

        public void DeleteAppCache()
        {
            if (!File.Exists(PlugIn.Location.AppCacheFile)) File.Delete(PlugIn.Location.AppCacheFile);
        }

        #region DDL Updates

        private class ColumnInfo
        {
            public string name { get; set; }
        }


        private static void UpdateProductSchema()
        {
            using (var cxnAlter = new SQLiteConnection(PlugIn.Location.AppCacheConnectionString))
            {
                cxnAlter.Open();

                var columns = cxnAlter.Query<ColumnInfo>("PRAGMA table_info(products);")
                    .Select(row => row.name)
                    .ToHashSet(StringComparer.OrdinalIgnoreCase);

                if (!columns.Contains("last_played"))
                {
                    cxnAlter.Execute("ALTER TABLE products ADD COLUMN last_played DATETIME;");
                }

                if (!columns.Contains("times_played"))
                {
                    cxnAlter.Execute("ALTER TABLE products ADD COLUMN times_played INTEGER DEFAULT 0;");
                }

                // added 1.1
                if (!columns.Contains("date_imported"))
                {
                    var sqls = new[]
                    {
                        "ALTER TABLE products ADD COLUMN date_imported DATETIME;",
                        "UPDATE products SET date_imported = datetime('2026-01-01');",
                        @"CREATE TRIGGER set_date_imported
                        AFTER INSERT ON products
                        FOR EACH ROW
                        WHEN NEW.date_imported IS NULL
                        BEGIN
                            UPDATE products
                            SET date_imported = CURRENT_TIMESTAMP
                            WHERE rowid = NEW.rowid;
                        END;"
                    };
                    foreach (var sql in sqls) 
                    {
                        cxnAlter.Execute(sql);
                    }
                }
            }
        }

        private static void UpdateTagSchema()
        {
            using (var cxnAlter = new SQLiteConnection(PlugIn.Location.AppCacheConnectionString))
            {
                cxnAlter.Open();
                var columns = cxnAlter.Query<ColumnInfo>("PRAGMA table_info(tags);")
                    .Select(row => row.name)
                    .ToHashSet(StringComparer.OrdinalIgnoreCase);
                if (!columns.Any())
                {
                    using (var tx = cxnAlter.BeginTransaction())
                    using (var cmd = cxnAlter.CreateCommand())
                    {

                        cmd.Transaction = tx;

                        // Enforce FKs for this session (good practice even if none yet)
                        cmd.CommandText = "PRAGMA foreign_keys = ON;";
                        cmd.ExecuteNonQuery();

                        // --- tags ---
                        // tag_id: auto-numbered from 1, PRIMARY KEY
                        // tag_name: TEXT, unique (case-insensitive via COLLATE NOCASE index)
                        // modified_date: defaults to CURRENT_TIMESTAMP (UTC)
                        cmd.CommandText = @"
                        CREATE TABLE IF NOT EXISTS tags (
                            tag_id        INTEGER PRIMARY KEY AUTOINCREMENT,
                            tag_name      TEXT NOT NULL,
                            modified_date DATETIME NOT NULL DEFAULT (CURRENT_TIMESTAMP)
                        );";
                        cmd.ExecuteNonQuery();

                        // Case-insensitive unique index on tag_name
                        cmd.CommandText = @"
                        CREATE UNIQUE INDEX IF NOT EXISTS 
                        UX_tags_tag_name_nocase ON tags(tag_name COLLATE NOCASE);";
                        cmd.ExecuteNonQuery();

                        // --- product_tags ---
                        // pt_id: auto-numbered from 1, PRIMARY KEY
                        // unique(product_id, tag_id)
                        // modified_date: defaults to CURRENT_TIMESTAMP (UTC)
                        cmd.CommandText = @"
                        CREATE TABLE IF NOT EXISTS product_tags (
                            pt_id         INTEGER PRIMARY KEY AUTOINCREMENT,
                            product_id    INTEGER NOT NULL,
                            tag_id        INTEGER NOT NULL,
                            modified_date DATETIME NOT NULL DEFAULT (CURRENT_TIMESTAMP)
                        );";
                        cmd.ExecuteNonQuery();

                        cmd.CommandText = @"CREATE UNIQUE INDEX IF NOT EXISTS UX_product_tags_product_tag ON product_tags(product_id, tag_id);";
                        cmd.ExecuteNonQuery();

                        tx.Commit();

                        // Add a few example tags
                        var startTags = new[] { "Country", "Hip-Hop", "Holiday", "Metal", "House" };
                        foreach (var tag in startTags)
                        {
                            cxnAlter.Execute($"INSERT INTO tags (tag_name) VALUES ('{tag}')");
                        }
                    }

                    // use for later modifications
                    /*
                    if (!columns.Contains("new_col"))
                    {
                        cxnAlter.Execute("ALTER TABLE tags ADD COLUMN new_col TEXT;");
                    }
                    */
                }
            }

        }

        #endregion DDL Updates

        public static SQLiteConnection GetAppCacheCxn() 
        {
            if (!Directory.Exists(PlugIn.Location.AppCachePath)) { 
                Directory.CreateDirectory(PlugIn.Location.AppCachePath); 
            }

            if (!File.Exists(PlugIn.Location.AppCacheFile))
            {
                // initialize the tables
                using (var cxnCreate = new SQLiteConnection(PlugIn.Location.AppCacheConnectionString))
                {
                    try
                    {
                        cxnCreate.Open();

                        var sqlCreate = "CREATE TABLE products (" +
                            "product_id BIGINT PRIMARY KEY, image_bytes BLOB, has_ogg BOOLEAN NOT NULL, title NVARCHAR(32), creator VARCHAR(32));";
                        var cmd = new SQLiteCommand(sqlCreate, cxnCreate);
                        cmd.ExecuteNonQuery();

                        sqlCreate = "CREATE TABLE product_triggers (" +
                            "product_id BIGINT, prefix VARCHAR(24), sequence INTEGER, trigger VARCHAR(24), ogg_name VARCHAR(64), location VARCHAR(64), length_ms REAL, addn_triggers VARCHAR(64), PRIMARY KEY(product_id ASC, prefix ASC, sequence ASC));";
                        cmd.CommandText = sqlCreate;
                        cmd.ExecuteNonQuery();


                    }
                    catch (Exception ex)
                    {
                        throw new Exception($"Exception: {ex.Message} from {ex.Source}");
                    }
                }                
            } 

            //Updates
            using (var cxnAlter = new SQLiteConnection(PlugIn.Location.AppCacheConnectionString))
            {
                cxnAlter.Open();

                // > 0.8.4 add addn_triggers field to product_triggers
                var sqlAddnTriggers = "select count(*) as theCount from pragma_table_info('product_triggers') where name = 'addn_triggers';";
                var cmd = new SQLiteCommand(sqlAddnTriggers, cxnAlter);
                try
                {
                    var count = cmd.ExecuteScalar(System.Data.CommandBehavior.SingleResult);
                    if (Convert.ToInt32(count) == 0)
                    {
                        sqlAddnTriggers = "ALTER TABLE product_triggers ADD COLUMN addn_triggers VARCHAR(64) NULL;";
                        cmd.CommandText = sqlAddnTriggers;
                        cmd.ExecuteNonQuery();
                    }
                } 
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            }

            UpdateProductSchema();
            try
            {
                UpdateTagSchema();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            

            return new SQLiteConnection(PlugIn.Location.AppCacheConnectionString);
        }

        internal static void UpdateProductPlay(long productId)
        {
            var sql = $@"
                UPDATE products SET
                last_played = CURRENT_TIMESTAMP,
                times_played = times_played + 1
                WHERE product_id = {productId}
                ";
            using (var cxnAppCache = GetAppCacheCxn()) 
            { 
                cxnAppCache.Open();
                cxnAppCache.Execute(sql);
            }
        }


        internal static List<ProductDisplayInfo> GetProductSearch(string searchTerm, List<ProductTag> searchTags = null)
        {
            long currentProductId = 0;
            ProductDisplayInfo currentInfo = null;
            List<dynamic> queryList = null;
            var infoList = new List<ProductDisplayInfo>();

            string andClause = string.Empty;
            string limitClause = string.Empty;

            if (string.IsNullOrWhiteSpace(searchTerm) && (searchTags == null || searchTags.Count == 0))
            {
                limitClause = "LIMIT 3000";
            }
            else
            {
                andClause = $@" AND (
                    p.title LIKE '%{searchTerm}%' OR
                    p.creator LIKE '%{searchTerm}%' OR
                    pt.prefix LIKE '%{searchTerm}%'
                 )";
            }

            var sql = $@"SELECT p.product_id AS ProductId,
                       p.title AS Name,
                       p.creator AS Creator,
                       p.image_bytes AS ImageBytes,
                       pt.prefix AS Prefix,
                       pt.sequence AS Sequence,
                       pt.trigger AS Trigger,
                       pt.length_ms AS LengthMS,
                       pt.location As Location,
                       pt.addn_triggers AS AddnTriggers
                       FROM products p 
                       INNER JOIN product_triggers pt ON (p.product_id = pt.product_id)
                       WHERE p.has_ogg = 1
                        {andClause}
                       ORDER BY p.product_id DESC, pt.sequence ASC
                        {limitClause}
                        ;";

            using (var cxnAppCache = GetAppCacheCxn())
            {
                queryList = cxnAppCache.Query(sql).ToList();
            }

            foreach (var query in queryList)
            {
                if (currentProductId != query.ProductId)
                {
                    if (currentInfo != null)
                    {
                        currentInfo.Tags = TagsGetForProduct(currentInfo.Id);
                        if (searchTags == null || searchTags.Count == 0)
                        {
                            infoList.Add(currentInfo);
                        }
                        else
                        {
                            foreach (var searchTag in searchTags)
                            {
                                var foundTags = currentInfo.Tags;
                                if (foundTags != null && foundTags.Count > 0)
                                {
                                    var matches = foundTags.Where(
                                        productTag => productTag.Id == searchTag.Id
                                        );
                                    if (matches.Any())
                                    {
                                        infoList.Add(currentInfo);
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    currentProductId = query.ProductId;
                    currentInfo = new ProductDisplayInfo();
                    currentInfo.Id = query.ProductId;
                    currentInfo.Name = query.Name;
                    currentInfo.ImageBytes = query.ImageBytes;
                    currentInfo.Creator = query.Creator;
                }
                var triggerInfo = new TriggerDisplayInfo();
                triggerInfo.Prefix = query.Prefix;
                triggerInfo.Sequence = (int)query.Sequence;
                triggerInfo.LengthMS = query.LengthMS;
                triggerInfo.Location = query.Location;
                triggerInfo.Trigger = query.Trigger;
                triggerInfo.ProductId = query.ProductId;
                triggerInfo.AddnTriggers = query.AddnTriggers;
                currentInfo.Triggers.Add(triggerInfo);
            }
            if (currentInfo != null && !string.IsNullOrEmpty(andClause))
            {
                currentInfo.Tags = TagsGetForProduct(currentInfo.Id);
                if (searchTags == null || searchTags.Count == 0)
                {
                    infoList.Add(currentInfo);
                }
                else
                {
                    foreach (var searchTag in searchTags)
                    {
                        var foundTags = currentInfo.Tags;
                        if (foundTags != null && foundTags.Count > 0)
                        {
                            var matches = foundTags.Where(
                                productTag => productTag.Id == searchTag.Id
                                );
                            if (matches.Any())
                            {
                                infoList.Add(currentInfo);
                                break;
                            }
                        }
                    }
                }
            }
            return infoList;
        }


        #region Product Tags

        /*
         The mute functions are so that we don'productTag try to change the product tags
        while the UI is being built. We only want to change the database when the
        user manually clicks a checkbox. The ItemCheck event fires when you set the 
        Checked value programmatically, and we don'productTag want that. You have to call
        MuteCheck every time you change the check state in code.
         */

        private static bool _muteCheck = false;

        internal static void MuteCheck()
        {
            _muteCheck = true;
        }
        internal static void UnMuteCheck()
        {
            _muteCheck = false;
        }


        internal static List<ProductTag> TagsGetAll()
        {
            var sql = "SELECT tag_id AS Id, tag_name AS Name FROM tags ORDER BY tag_name";
            using (var cxnAppCache = GetAppCacheCxn())
            {
                cxnAppCache.Open();
                return cxnAppCache.Query<ProductTag>(sql).ToList();
            }
        }

        internal static bool TagAssignToProduct(long productId, int tagId, bool assign)
        {
            if (_muteCheck)
            {
                _muteCheck = true;
                return true;
            }
            bool result = false;
            var sqlTrue = "INSERT INTO product_tags (product_id, tag_id) VALUES (@pid, @tid);";
            var sqlFalse = "DELETE FROM product_tags WHERE product_id = @pid AND tag_id = @tid;";
            var sql = assign ? sqlTrue : sqlFalse;
            try
            {
                using (var cxnAppCache = GetAppCacheCxn())
                {
                    cxnAppCache.Open();
                    cxnAppCache.Execute(sql, new { pid = productId, tid = tagId });
                }
                result = true;
            }
            catch (Exception exc)
            {
                var message = "Unable to change the searchTag assignment to this tune";
                var title = "Database Error";
                StyledMessageBox.Show(message, title);
            }
            return result;
        }

        internal static List<ProductTag> TagsGetForProduct(long productId)
        {
            using (SQLiteConnection cxnAppCache = GetAppCacheCxn())
            {
                cxnAppCache.Open();

                var sqlTag = @"
                    SELECT productTag.tag_id AS Id, productTag.tag_name AS Name
                    FROM product_tags pt INNER JOIN tags productTag ON pt.tag_id = productTag.tag_id
                    WHERE pt.product_id = @pid
                    ORDER BY productTag.tag_name;";
                var result = cxnAppCache.Query<ProductTag>(sqlTag, new { pid = productId }).ToList();
                return result;
            }
        }

        internal static ProductTag TagGetById(int tagId)
        {
            ProductTag result = null;
            var sql = "SELECT tag_id AS Id, tag_name AS Name FROM tags WHERE tag_id = @tid";
            using (var cxnAppCache = GetAppCacheCxn())
            {
                cxnAppCache.Open();
                try
                {
                    var tag = cxnAppCache.Query<ProductTag>(sql, new { tid = tagId });
                    result = tag.First();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }
            }
            return result;
        }

        internal static ProductTag TagCreateNew(string name)
        {
            ProductTag result = null;
            using (var cxnAppCache = GetAppCacheCxn())
            {
                cxnAppCache.Open();
                var sql = "INSERT INTO tags (tag_name) VALUES (@name);";
                try
                {
                    cxnAppCache.Execute(sql, new { name = name });
                    int lastId = cxnAppCache.ExecuteScalar<int>("SELECT last_insert_rowid();");
                    if (lastId > 0)
                    {
                        result = new ProductTag { Id = lastId, Name = name };
                    }
                }
                catch (Exception exc)
                {
                    Debug.WriteLine(exc);
                }
            }

            return result;
        }

        internal static void TagDelete(int tagId)
        {
            using (var cxnAppCache = GetAppCacheCxn())
            {
                cxnAppCache.Open();

                try
                {
                    var sql = "DELETE FROM product_tags WHERE tag_id = @tid);";
                    cxnAppCache.Execute(sql, new { tid = tagId });
                    sql = "DELETE FROM tags WHERE tag_id = @tid);";
                    cxnAppCache.Execute(sql, new { tid = tagId });
                }
                catch (Exception exc)
                {
                    Debug.WriteLine(exc);
                }
            }
        }

        internal static void RenameSong(long productId, string name)
        {
            using (var cxnAppCache = GetAppCacheCxn())
            {
                cxnAppCache.Open();

                try
                {
                    var sql = "UPDATE products SET title = @title WHERE product_id = @pid ;";
                    cxnAppCache.Execute(sql, new { 
                        title = name, 
                        pid = productId
                    });
                }
                catch (Exception exc)
                {
                    Debug.WriteLine(exc);
                }
            }

        }

        #endregion
    }
}
