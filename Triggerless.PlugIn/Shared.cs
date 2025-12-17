using System;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace Triggerless.PlugIn
{
    public static class Shared
    {
        public static Color ThemeBackcolor = Color.FromArgb(0x1E1A1A);
        public static Color ThemeForeColor = Color.FromArgb(0xF0F0F0);

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

        public static string[] ReadAllLinesUnlocked(string path)
        {
            return ReadAllTextUnlocked(path)
                .Split(
                Environment.NewLine.ToCharArray(), 
                StringSplitOptions.RemoveEmptyEntries
            );
        }

        public static string ReadAllTextUnlocked(string path)
        {
            var encoding = Encoding.UTF8; // adjust if your log uses a different encoding
            using (var fs = new FileStream(
                path,
                FileMode.Open,
                FileAccess.Read,
                FileShare.ReadWrite | FileShare.Delete,         // <-- key bit
                bufferSize: 4096,
                options: FileOptions.SequentialScan))
            {
                using (var reader = new StreamReader(fs, encoding))
                {
                    return reader.ReadToEnd();
                }
            }
        }
    }

    public static class Location
    {
        public static string AppData => Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        public static string ImvuLocation => Path.Combine(AppData, "IMVUClient");
        public static string ImvuFileLocation => Path.Combine(AppData, "IMVU");
        public static string ProductCacheFile => Path.Combine(ImvuFileLocation, "productInfoCache.db");
        public static string AppCachePath => Path.Combine(AppData, "Triggerless", "TriggerBot");
        public static string AppCacheFile => Path.Combine(AppCachePath, "appCache.sqlite");
        public static string AppCacheConnectionString => $"Data Source={AppCacheFile}";
        public static string FFmpegLocation => Path.Combine(
        Path.GetDirectoryName(Assembly.GetExecutingAssembly().
            Location.Replace(@"\bin\Debug", "")), "ffmpeg");
        public static void EnsurePath(string directory)
        {
            if (!Directory.Exists(directory)) Directory.CreateDirectory(directory);
        }

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
        public static string LyricSheetsPath
        {
            get
            {
                var path = Path.Combine(TriggerbotDocsPath, "LyricSheets");
                EnsurePath(path);
                return path;
            }
        }

        private static readonly Guid FOLDERID_Downloads = Guid.Parse("374DE290-123F-4565-9164-39C4925E467B");

        [DllImport("shell32.dll")]
        private static extern int SHGetKnownFolderPath(
            [MarshalAs(UnmanagedType.LPStruct)] Guid rfid, uint dwFlags, IntPtr hToken, out IntPtr ppszPath);

        private static string _downloadsPath = string.Empty;
        public static string DownloadsPath
        {
            get
            {
                if (_downloadsPath == string.Empty)
                {
                    IntPtr p;
                    var hr = SHGetKnownFolderPath(FOLDERID_Downloads, 0, IntPtr.Zero, out p);
                    if (hr != 0) Marshal.ThrowExceptionForHR(hr);

                    try
                    {
                        var s = Marshal.PtrToStringUni(p);
                        if (s == null)
                            throw new InvalidOperationException("Failed to retrieve Downloads path.");
                        _downloadsPath = s;
                    }
                    finally
                    {
                        Marshal.FreeCoTaskMem(p);
                    }
                }
                return _downloadsPath;
            }
        }

        public static string PlugInsPath 
        { 
            get {
                var thisAssy = Assembly.GetEntryAssembly();
                var exeLocation = thisAssy.Location;
                var path = Path.Combine(Path.GetDirectoryName(exeLocation), "plugins");
                if (!Directory.Exists(path)) Directory.CreateDirectory(path);
                return path;                
            } 
        }
    }
}
