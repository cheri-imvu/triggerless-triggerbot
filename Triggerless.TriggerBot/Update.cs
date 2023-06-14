using Microsoft.WindowsAPICodePack.Shell;
using Newtonsoft.Json.Linq;
using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Triggerless.TriggerBot
{
    public class Update
    {
        private string _downloadsPath;
        private bool _runSetupOnExit;

        public Update()
        {
            _downloadsPath = KnownFolders.Downloads.Path;
            _runSetupOnExit = false;
        }

        private string BaseUrl => "http://triggerless.com/triggerbot/";
        private string JsonUrl => $"{BaseUrl}/current-version.json";
        private string _setup = string.Empty;
        private Version _latestVersion = null;

        public void CheckForUpdate()
        {
            string setupFileName = GetSetupFileNameFromJson(JsonUrl).Result;
            string setupFilePath = Path.Combine(_downloadsPath, setupFileName);

            if (!File.Exists(setupFilePath))
            {
                DownloadSetupFile($"{BaseUrl}/{setupFileName}", setupFilePath).RunSynchronously();
            }
        }

        private async Task<string> GetSetupFileNameFromJson(string url)
        {
            using (WebClient client = new WebClient())
            {
                string jsonText = await client.DownloadStringTaskAsync(url);
                JObject jsonObject = JObject.Parse(jsonText);
                _setup = jsonObject["setup"].ToString();
                _latestVersion = new Version(jsonObject["version"].ToString());
                return _setup;
            }
        }

        private async Task DownloadSetupFile(string url, string filePath)
        {
            using (WebClient client = new WebClient())
            {
                await client.DownloadFileTaskAsync(new Uri(url), filePath);
            }
        }

        public static Version CurrentVersion => Assembly.GetEntryAssembly()?.GetName().Version;

        public bool IsUpgradeAvailable()
        {
            if (CurrentVersion != null)
            {
                return _latestVersion > CurrentVersion;
            }

            return false;
        }

        public void SetRunSetupOnExit(bool value)
        {
            _runSetupOnExit = value;
        }

        public bool ShouldRunSetupOnExit()
        {
            return _runSetupOnExit;
        }

        public void RunSetupFile()
        {
            string setupFilePath = Path.Combine(_downloadsPath, GetSetupFileNameFromJson("version.json").GetAwaiter().GetResult());
            Process.Start(setupFilePath);
            Application.Exit(); // Terminate the current instance of the application
        }
    }

}
