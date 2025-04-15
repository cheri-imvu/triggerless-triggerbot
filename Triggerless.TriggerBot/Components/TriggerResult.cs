using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dapper;

namespace Triggerless.TriggerBot
{
    public partial class Collector
    {
        public async Task<bool> Verify(ProductDisplayInfo productDisplayInfo)
        {
            if (!productDisplayInfo.Triggers.Any(t => t.LengthMS == 0)) return true;
            var sda = new SQLiteDataAccess();
            using (var conn = sda.GetAppCacheCxn())
            {
                conn.Open();
                string sql = "";
                var where = "WHERE product_id=@productId AND prefix=@prefix AND sequence=@sequence";
                using (var triggerClient = new HttpClient())
                {
                    foreach (var trigger in productDisplayInfo.Triggers.Where(t => t.LengthMS == 0))
                    {
                        var musicUrl = GetUrl(trigger.ProductId, trigger.Location);
                        try
                        {
                            using (var ms = new MemoryStream())
                            using (var stream = await triggerClient.GetStreamAsync(musicUrl).ConfigureAwait(false))
                            {
                                stream.CopyTo(ms);
                                trigger.LengthMS = NVorbis.VorbisReader.GetOggLengthMS(ms);
                            }
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("Unable to retrieve OGG length from server", "HTTP Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return false;
                        }

                        sql = $"UPDATE product_triggers SET length_ms = {trigger.LengthMS} {where}";
                        var payload = new
                        {
                            productId = trigger.ProductId,
                            prefix = trigger.Prefix,
                            sequence = trigger.Sequence
                        };
                        lock (_dbLock)
                        {
                            conn.Execute(sql, payload);
                        }
                    }
                }
            }
            return true;
        }

    }
}
