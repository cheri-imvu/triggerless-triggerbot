using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TuneTriggerer
{
    public class ProductSearch
    {
        private byte[] ConvertImageToBytes(Image image)
        {
            using (var ms = new MemoryStream())
            {
                image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                return ms.ToArray();
            }
        }

        private Image ConvertBytesToImage(byte[] data)
        {
            using (var ms = new MemoryStream(data))
            {
                return Image.FromStream(ms);
            }
        }

        // Read bytes for a SQLite reader
        private static byte[] GetBytes(SQLiteDataReader reader, int index)
        {
            const int CHUNK_SIZE = 2 * 1024;
            byte[] buffer = new byte[CHUNK_SIZE];
            long bytesRead;
            long fieldOffset = 0;
            using (MemoryStream stream = new MemoryStream())
            {
                while ((bytesRead = reader.GetBytes(index, fieldOffset, buffer, 0, buffer.Length)) > 0)
                {
                    byte[] actualRead = new byte[bytesRead];
                    Buffer.BlockCopy(buffer, 0, actualRead, 0, (int)bytesRead);
                    stream.Write(actualRead, 0, actualRead.Length);
                    fieldOffset += bytesRead;
                }
                return stream.ToArray();
            }
        }

        public class Results: List<ProductEntry> { }

        public async Task<Results> SearchAsync(string searchTerm)
        {
            var result = new Results();
            var appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var productCacheFile = Path.Combine(appData, "IMVU", "productInfoCache.db");
            var appCachePath = Path.Combine(appData, "Triggerless", "TuneTriggerer");
            if (!Directory.Exists(appCachePath)) { Directory.CreateDirectory(appCachePath); }
            var appCacheFile = Path.Combine(appCachePath, "appCache.sqlite");

            if (!File.Exists(productCacheFile))
            {
                throw new Exception("IMVU Classic is not installed");
            }

            if (!File.Exists(appCacheFile))
            {
                var connString = $"Data Source={appCacheFile}";
                using (var cxnCreate = new SQLiteConnection(connString))
                {
                    try
                    {
                        cxnCreate.Open();
                        var sqlCreate = "CREATE TABLE images (" +
                            "product_id BIGINT PRIMARY KEY, image BLOB);";
                        var cmd = new SQLiteCommand(sqlCreate, cxnCreate);
                        cmd.ExecuteNonQuery();

                        sqlCreate = "CREATE TABLE triggers (" +
                            "product_id BIGINT, sequence INTEGER, trigger VARCHAR(24), length_ms INTEGER, PRIMARY KEY(product_id ASC, sequence ASC)";
                        cmd.CommandText = sqlCreate;
                        cmd.ExecuteNonQuery();
                    }
                    catch(Exception ex)
                    {
                        throw new Exception($"Exception: {ex.Message} from {ex.Source}");
                    }
                }
            }

            using (var cxnSearch = new SQLiteConnection($"Data Source={productCacheFile}"))
            {
                cxnSearch.Open();
                var sqlSearch = $"SELECT id, products_name, manufacturers_name, products_image FROM products WHERE {SQLiteDataAccess.AccessoryFilter}";
                if (!string.IsNullOrEmpty(searchTerm))
                {
                    sqlSearch += $" AND (products_name LIKE '%{searchTerm.Trim()}%' OR manufacturers_name LIKE '%{searchTerm.Trim()}%')";
                }
                sqlSearch += " ORDER BY id ASC;";

                var cmdSearch = new SQLiteCommand(sqlSearch, cxnSearch);
                var readerSearch = cmdSearch.ExecuteReader();
                while (readerSearch.Read())
                {
                    var entry = new ProductEntry
                    {
                        Id = readerSearch.GetInt32(0),
                        Name = readerSearch.GetString(1),
                        Creator = readerSearch.GetString(2),
                        ImageLocation = readerSearch.GetString(3)
                    };
                    result.Add(entry);
                }
            }

            using (var client = new HttpClient())
            using (var cxnApp = new SQLiteConnection($"Data Source={appCacheFile}"))
            {
                cxnApp.Open();
                foreach (var entry in result)
                {
                    var sqlImage = $"SELECT image FROM images WHERE product_id = {entry.Id}";
                    var cmdImage = new SQLiteCommand (sqlImage, cxnApp);
                    using (var readerImage = cmdImage.ExecuteReader(System.Data.CommandBehavior.SingleRow)) 
                    {
                        if (readerImage.Read())
                        {
                            byte[] bytes = GetBytes(readerImage, 0);
                            entry.ProductImage = ConvertBytesToImage(bytes);
                        }
                        else
                        {
                            entry.ProductImage = new Bitmap(await client.GetStreamAsync($"{entry.ImageLocation}"));
                            var sqlInsert = $"INSERT INTO images (product_id, image) VALUES (@product_id, @image)";
                            var cmdInsert = new SQLiteCommand(sqlInsert, cxnApp);
                            cmdInsert.Parameters.AddWithValue("@product_id", entry.Id);
                            cmdInsert.Parameters.AddWithValue("@image", ConvertImageToBytes(entry.ProductImage));
                            cmdInsert.ExecuteNonQuery();
                        }
                    }
                }
            }
            return result;
        }
    }

}
