using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Windows.Forms;
using Dapper;

namespace Triggerless.TriggerBot
{
    public partial class Collector
    {
        public bool Verify(ProductDisplayInfo productDisplayInfo)
        {
            if (!productDisplayInfo.Triggers.Any(t => t.LengthMS == 0)) return true;
            var sda = new SQLiteDataAccess();
            using (var conn = sda.GetAppCacheCxn())
            {
                conn.Open();
                foreach (var trigger in productDisplayInfo.Triggers.Where(t => t.LengthMS == 0))
                {
                    var where = "WHERE product_id=@productId AND prefix=@prefix AND sequence=@sequence";
                    var sql = $"SELECT location FROM product_triggers {where}";
                    var payload = new { productId = trigger.ProductId, prefix = trigger.Prefix, sequence = trigger.Sequence };
                    var location = conn.Query<string>(sql, payload).First();
                    if (string.IsNullOrWhiteSpace(location)) return false;
                    var musicUrl = GetUrl(trigger.ProductId, location);
                    using (var triggerClient = new HttpClient())
                    {
                        try
                        {
                            using (var stream = triggerClient.GetStreamAsync(musicUrl).Result)
                            {
                                var ms = new MemoryStream();
                                stream.CopyTo(ms);
                                trigger.LengthMS = NVorbis.VorbisReader.GetOggLengthMS(ms);
                                ms.Dispose();
                            }
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("Unable to retrieve OGG length from server", "HTTP Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return false;
                        }
                    }
                    sql = $"UPDATE product_triggers SET length_ms = {trigger.LengthMS} {where}";
                    conn.Execute(sql, payload);
                }

            }
            return true;
        }

        public class TriggerResult
        {
            public int Sequence { get; set; }
            public string TriggerName { get; set; }
            public string OggName { get; set; }
            public double LengthMS { get; set; }
        }
    }
}
