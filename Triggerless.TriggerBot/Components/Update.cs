using Microsoft.WindowsAPICodePack.Shell;
using Newtonsoft.Json.Linq;
using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Reflection;
using System.Security.Cryptography;
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

        private string BaseUrl => $"{Shared.TriggerlessDomain}/triggerbot/";
        public static Version CurrentVersion => Assembly.GetEntryAssembly()?.GetName().Version;
        private string JsonUrl => $"{BaseUrl}/current-version.json";
        private string _setup = string.Empty;
        private Version _latestVersion = null;
        private byte[] _expectedMD5 = new byte[16];
        public void CheckForUpdate()
        {
            // First download the JSON from the web
            if (!Shared.HasTriggerlessConnection)
            {
                return; // No internet connection, skip update check
            }
            string jsonText = null;
            try
            {
                using (WebClient client = new WebClient())
                {
                    jsonText = client.DownloadString(JsonUrl);
                }

            }
            catch (Exception)
            {
                Shared.HasTriggerlessConnection = false;
                return;
            }
            JObject jsonObject = JObject.Parse(jsonText);
            _setup = jsonObject["setup"].ToString();
            _latestVersion = new Version(jsonObject["version"].ToString());
            var whatsNew = jsonObject["whatsNew"]?.ToString().Replace("|", Environment.NewLine);

            // Get expected MD5 hash
            var md5String = jsonObject["md5"].ToString();
            if (md5String.Length != 32)
            {
                throw new ArgumentException("MD5 Exception. Unable to update");
            }

            for (var i = 0; i < md5String.Length; i += 2)
            {
                _expectedMD5[i / 2] = Convert.ToByte(md5String.Substring(i, 2), 16);
            }



            // Check web version vs. Current version, and return if not needed.

            bool needsUpgrade = false;
            if (CurrentVersion != null)
            {
                needsUpgrade = (_latestVersion > CurrentVersion);
            }
            if (!needsUpgrade) { return; }

            // If web version is higher, throw up the modal dialog for what to do next

            var updateForm = new UpdateForm();
            updateForm.VersionString = _latestVersion.ToString();
            updateForm.WhatsNewText = whatsNew;
            var result = updateForm.ShowDialog();
            if (result == DialogResult.No) return;


            // If they didn't say Ignore, See if new setup file is in Downloads, and if not, download it

            string setupFilePath = Path.Combine(_downloadsPath, _setup);
            if (!File.Exists(setupFilePath))
            {
                using (WebClient client = new WebClient())
                {
                    client.DownloadFile($"{BaseUrl}/{_setup}", setupFilePath);
                }
            }

            // Test if the MD5 matches the on in the JSON
            byte[] actualMD5 = new byte[16];
            using (var md5Provider = new MD5CryptoServiceProvider())
            {
                actualMD5 = md5Provider.ComputeHash(File.ReadAllBytes(setupFilePath));
            }
            bool hashesMatch = false;
            if (actualMD5.Length == _expectedMD5.Length)
            {
                hashesMatch = true;
                for (int i = 0; i < actualMD5.Length; i++)
                {
                    hashesMatch &= (_expectedMD5[i] == actualMD5[i]);
                }
            }

            if (!hashesMatch) 
            {
                File.Delete(setupFilePath);
                throw new ApplicationException("The setup file appears to be corrupted");
            } 


            // If they said Update Now, do the update, otherwise set the flag for WindowClosing

            if (result == DialogResult.OK)
            {
                RunSetupFile();
                return;
            }

            _runSetupOnExit = true;
        }

        public void RunSetupFileIfRequired()
        {
            if (_runSetupOnExit)
            {
                RunSetupFile();
            }
        }

        private void RunSetupFile()
        {
            string setupFilePath = Path.Combine(_downloadsPath, _setup);
            Process.Start(setupFilePath);
            Application.Exit(); // Terminate the current instance of the application
        }
    }

}
