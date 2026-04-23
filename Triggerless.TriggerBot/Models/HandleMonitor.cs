using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using Triggerless.Native;

namespace Triggerless.TriggerBot.Models
{
    public static class HandleMonitor
    {
        private static object _lockObject = new object();
        private static bool _running = false;
        private static string _fileName = null;

        // uiFlags: 0 = GDI, 1 = USER
        public static (int gdi, int user) GetHandleCounts()
        {
            var proc = Process.GetCurrentProcess();
            int gdi = User32.GetGuiResources(proc.Handle, 0);
            int user = User32.GetGuiResources(proc.Handle, 1);
            return (gdi, user);
        }

        public static void LogHandles(string message)
        {

            if (!_running)
            {
                _fileName = Path.Combine(PlugIn.Location.TriggerbotDocsPath, "handles.log");
                if (File.Exists(_fileName)) File.Delete(_fileName);
                _running = true;
            }

            (int gdi, int user) = GetHandleCounts();
            var localtime = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.fff");
            var line = $"{localtime}\t{message}\tUser:{user}\tGDI:{gdi}";
            lock (_lockObject) 
            {
                File.AppendAllText(_fileName, line + Environment.NewLine);
            }
        }
    }
}
