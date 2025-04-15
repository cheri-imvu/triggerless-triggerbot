using System;
using System.IO;
using System.Reflection;

namespace Triggerless.TriggerBot
{
    internal class Shared
    {
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

        public static void EnsurePath(string directory)
        {
            if (!Directory.Exists(directory)) Directory.CreateDirectory (directory);
        }

    }
}
