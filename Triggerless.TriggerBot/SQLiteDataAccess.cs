using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Triggerless.TriggerBot
{
    public class SQLiteDataAccess
    {
        public static string AccessoryFilter => @" cPath IN (
            '[106, 40, 153, 2956]',
            '[106, 40, 153, 615]',
            '[106, 40, 153, 2500]',
            '[106, 40, 153, 1328]',
            '[106, 40, 153, 144]',
            '[106, 40, 153, 2952]',
            '[106, 40, 153, 1281]',
            '[106, 40, 153, 2935]',
            '[106, 40, 153, 372]',
            '[106, 40, 153, 3094]',
            '[106, 41, 71, 2501]',
            '[106, 41, 71, 1329]',
            '[106, 41, 71, 148]',
            '[106, 41, 71, 1072]',
            '[106, 41, 71, 1437]',
            '[106, 41, 71, 1268]',
            '[106, 41, 71, 1927]',
            '[106, 41, 71, 1250]',
            '[106, 41, 71, 1251]',
            '[106, 41, 71, 334]',
            '[106, 41, 71, 3095]',
            '[106, 41, 71, 416]'
            ) ";

        private string AppData => Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        public SQLiteConnection GetProductCacheCxn()
        {
            var productCacheFile = Path.Combine(AppData, "IMVU", "productInfoCache.db");
            if (File.Exists(productCacheFile))
            {
                throw new ApplicationException("IMVU Classic Client is not installed");
            }

            return new SQLiteConnection($"Data Source={productCacheFile}");
        }

        public SQLiteConnection GetAppCacheCxn() 
        {
            var appCachePath = Path.Combine(AppData, "Triggerless", "TriggerBot");
            if (!Directory.Exists(appCachePath)) { Directory.CreateDirectory(appCachePath); }
            var appCacheFile = Path.Combine(appCachePath, "appCache.sqlite");
            if (!File.Exists(appCacheFile))
            {
                // initialize the tables
                using (var cxnCreate = new SQLiteConnection($"Data Source={appCacheFile}"))
                {
                    try
                    {
                        cxnCreate.Open();

                        var sqlCreate = "CREATE TABLE products (" +
                            "product_id BIGINT PRIMARY KEY, image_bytes BLOB, has_ogg BOOLEAN NOT NULL, title VARCHAR(32), creator VARCHAR(32));";
                        var cmd = new SQLiteCommand(sqlCreate, cxnCreate);
                        cmd.ExecuteNonQuery();

                        sqlCreate = "CREATE TABLE triggers (" +
                            "product_id BIGINT, sequence INTEGER, trigger VARCHAR(24), length_ms INTEGER, PRIMARY KEY(product_id ASC, sequence ASC)";
                        cmd.CommandText = sqlCreate;
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception($"Exception: {ex.Message} from {ex.Source}");
                    }
                }                
            }
            return new SQLiteConnection(appCacheFile);
        }
    }
}
