using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Diagnostics;
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


        public SQLiteConnection GetProductCacheCxn()
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

        public SQLiteConnection GetAppCacheCxn() 
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


            return new SQLiteConnection(Shared.AppCacheConnectionString);
        }
    }
}
