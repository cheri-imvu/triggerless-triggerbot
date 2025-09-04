using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Triggerless.TriggerBot
{
    public class SQLiteDataAccess
    {
        public static string AccessoryFilter => @" cPath IN (
            '[106, 40, 153, 144]',
            '[106, 40, 153, 155]',
            '[106, 40, 153, 372]',
            '[106, 40, 153, 615]',
            '[106, 40, 153, 1281]',
            '[106, 40, 153, 1328]',
            '[106, 40, 153, 2500]',
            '[106, 40, 153, 2935]',
            '[106, 40, 153, 2952]',
            '[106, 40, 153, 2956]',
            '[106, 40, 153, 3094]',
            '[106, 40, 3092]',
            '[106, 41, 71, 148]',
            '[106, 41, 71, 165]',
            '[106, 41, 71, 334]',
            '[106, 41, 71, 416]',
            '[106, 41, 71, 1072]',
            '[106, 41, 71, 1250]',
            '[106, 41, 71, 1251]',
            '[106, 41, 71, 1268]',
            '[106, 41, 71, 1329]',
            '[106, 41, 71, 1437]',
            '[106, 41, 71, 1739]',
            '[106, 41, 71, 1927]',
            '[106, 41, 71, 2501]',
            '[106, 41, 71, 3095]',
            '[106, 41, 3093]'
            ) ";

        /* 
        106_40_153_144 // F Spice Glasses
        106_40_153_155 // F New Accessories
        106_40_153_372 // F King Glasses
        106_40_153_615 // F Rings
        106_40_153_1281 // F DJ System
        106_40_153_1328 // F Actions
        106_40_153_2500 // F Music Item
        106_40_153_2935 // F Crimson Sounds
        106_40_153_2952 // F Triggerless Music
        106_40_153_2956 // F !!Music!!
        106_40_153_3094 // F Miscellaneous
        106_40_3092 // F Miscellaneous (not accessory)

        106_41_71_148 // M Glasses Kings
        106_41_71_165 // M New Accessories
        106_41_71_334 // M Headphones
        106_41_71_416 // M Spice Glasses
        106_41_71_1072 // M Boomboxes
        106_41_71_1250 // M Guitar
        106_41_71_1251 // M Guitars
        106_41_71_1268 // M DJ System
        106_41_71_1329 // M Actions
        106_41_71_1437 // M Cow Bells
        106_41_71_1739 // M Tools
        106_41_71_1927 // M Frameless Glasses
        106_41_71_2501 // M Music Items
        106_41_71_3095 // M Miscellaneous
        106_41_3093 // M Miscellaneous (not accessory)
         */


        public static SQLiteConnection GetProductCacheCxn()
        {            
            if (!File.Exists(Shared.ProductCacheFile))
            {
                throw new ApplicationException("IMVU Classic Client is not installed");
            }

            return new SQLiteConnection($"Data Source={Shared.ProductCacheFile}");
        }

        public void DeleteAppCache()
        {
            if (!File.Exists(Shared.AppCacheFile)) File.Delete(Shared.AppCacheFile);
        }

        private class ColumnInfo
        {
            public string name { get; set; }
        }


        private static void UpdateProductSchema()
        {
            using (var cxnAlter = new SQLiteConnection(Shared.AppCacheConnectionString))
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
            }
        }

        private static void UpdateTagSchema()
        {
            using (var cxnAlter = new SQLiteConnection(Shared.AppCacheConnectionString))
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
                }
            }

        }

        public static SQLiteConnection GetAppCacheCxn() 
        {
            if (!Directory.Exists(Shared.AppCachePath)) { Directory.CreateDirectory(Shared.AppCachePath); }

            if (!File.Exists(Shared.AppCacheFile))
            {
                // initialize the tables
                using (var cxnCreate = new SQLiteConnection(Shared.AppCacheConnectionString))
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
            using (var cxnAlter = new SQLiteConnection(Shared.AppCacheConnectionString))
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

            return new SQLiteConnection(Shared.AppCacheConnectionString);
        }

        internal static List<ProductDisplayInfo> GetProductSearch(string searchTerm)
        {
            long currentProductId = 0;
            ProductDisplayInfo currentInfo = null;
            List<dynamic> queryList = null;
            var infoList = new List<ProductDisplayInfo>();

            string andClause = string.Empty;
            string limitClause = string.Empty;

            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                limitClause = "LIMIT 1000";
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

            using (var cxnAppCache = SQLiteDataAccess.GetAppCacheCxn())
            {
                queryList = cxnAppCache.Query(sql).ToList();
            }

            foreach (var query in queryList)
            {
                if (currentProductId != query.ProductId)
                {
                    if (currentInfo != null)
                    {
                        infoList.Add(currentInfo);
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
            if (currentInfo != null && !string.IsNullOrEmpty(andClause)) infoList.Add(currentInfo);
            return infoList;
        }
    }
}
