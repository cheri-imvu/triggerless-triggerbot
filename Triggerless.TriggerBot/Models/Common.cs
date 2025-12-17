using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Triggerless.PlugIn;

namespace Triggerless.TriggerBot.Models
{
    internal static class Common
    {
        public static bool HasTriggerlessConnection = true;

        internal static void GenerateTimeItText(ProductDisplayInfo product)
        {
            if (product != null)
            {
                var sb = new StringBuilder();
                double seconds = 0F;
                var lyricList = new List<LyricEntry>();

                foreach (var trigger in product.Triggers)
                {
                    lyricList.Add(new LyricEntry
                    {
                        Time = TimeSpan.FromSeconds(seconds),
                        Lyric = $"*imvu:trigger {trigger.Trigger}"
                    });
                    var line = $"<{seconds.ToString("0.000")}>*imvu:trigger {trigger.Trigger}";
                    seconds += trigger.LengthMS / 1000;
                }

                var tbLyricList = new List<LyricEntry>();
                var tbName = Path.Combine(Location.LyricSheetsPath, $"{product.Id}.lyrics");
                if (File.Exists(tbName))
                {
                    tbLyricList = JsonConvert.DeserializeObject<List<LyricEntry>>(File.ReadAllText(tbName));
                }
                lyricList.AddRange(tbLyricList);
                foreach (var lyricEntry in lyricList.OrderBy(l => l.Time))
                {
                    sb.AppendLine($"{lyricEntry.Time.TotalSeconds.ToString("0.000")} {lyricEntry.Lyric}");
                }

                var targetFolder = Location.LyricSheetsPath;
                var filename = $"{product.Id}.timeit.txt";
                var filepath = Path.Combine(targetFolder, filename);
                if (File.Exists(filepath)) File.Delete(filepath);
                File.WriteAllText(filepath, sb.ToString());

                Process.Start(filepath);
            }
        }
        public static bool Paid { get; set; }
        public static void CheckIfPaid()
        {
            string installationType = Properties.Settings.Default.InstallationType;
            if (string.IsNullOrEmpty(installationType))
            {
                Paid = false;
                return;
            }

            if (installationType.ToLowerInvariant() == "triggerboss")
            {
                Paid = true;
                return;
            }
            Paid = false;
        }

    }
}
