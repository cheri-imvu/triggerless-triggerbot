using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Triggerless.TriggerBot
{
    internal class Shared
    {
        public static Color ThemeBackcolor = Color.FromArgb(0x1E1A1A);
        public static Color ThemeForeColor = Color.FromArgb(0xF0F0F0);

        public static bool HasTriggerlessConnection = true;
        public static bool Paid { get; set; }
        public static void CheckIfPaid()
        {
            string installationType = Properties.Settings.Default.InstallationType;
            if (string.IsNullOrEmpty(installationType))
            {
                Paid = false;
                return;
            }

            if (installationType.ToLower() == "triggerboss")
            {
                Paid = true;
                return;
            }
            Paid = false;
        }

        public static string VersionNumber => Assembly.GetEntryAssembly().GetName().Version.ToString();

        public static string Copyright
        {
            get
            {
                var attributes = Assembly.GetEntryAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);


                if (attributes.Length > 0)
                {
                    return (attributes[0] as AssemblyCopyrightAttribute).Copyright;
                }
                return $"Copyright @{DateTime.Now.Year}";
            }
        }

        public static string AppData => Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

        public static string ProductCacheFile => Path.Combine(AppData, "IMVU", "productInfoCache.db");
        public static string AppCachePath => Path.Combine(AppData, "Triggerless", "TriggerBot");
        public static string AppCacheFile => Path.Combine(AppCachePath, "appCache.sqlite");
        public static string AppCacheConnectionString => $"Data Source={AppCacheFile}";

        public static string LyricSheetsPath
        {
            get
            {
                var path = Path.Combine(TriggerbotDocsPath, "LyricSheets");
                EnsurePath(path);
                return path;
            }
        }

        public static string FFmpegLocation => Path.Combine(
                Path.GetDirectoryName(Assembly.GetExecutingAssembly().
                    Location.Replace(@"\bin\Debug", "")), "ffmpeg");

        public static string TriggerbotDocsPath 
        { 
            get 
            {
                var docsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                var botPath = Path.Combine(docsPath, "Triggerbot");
                EnsurePath(botPath);
                return botPath;
            } 
        }

        public static string TriggerlessDomain = "https://www.triggerless.com";

        public static void EnsurePath(string directory)
        {
            if (!Directory.Exists(directory)) Directory.CreateDirectory (directory);
        }

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
                var tbName = Path.Combine(Shared.LyricSheetsPath, $"{product.Id}.lyrics");
                if (File.Exists(tbName))
                {
                    tbLyricList = JsonConvert.DeserializeObject<List<LyricEntry>>(File.ReadAllText(tbName));
                }
                lyricList.AddRange(tbLyricList);
                foreach (var lyricEntry in lyricList.OrderBy(l => l.Time))
                {
                    sb.AppendLine($"{lyricEntry.Time.TotalSeconds.ToString("0.000")} {lyricEntry.Lyric}");
                }

                var targetFolder = Shared.LyricSheetsPath;
                var filename = $"{product.Id}.timeit.txt";
                var filepath = Path.Combine(targetFolder, filename);
                if (File.Exists(filepath)) File.Delete(filepath);
                File.WriteAllText(filepath, sb.ToString());

                Process.Start(filepath);
            }
        }
    }
}
