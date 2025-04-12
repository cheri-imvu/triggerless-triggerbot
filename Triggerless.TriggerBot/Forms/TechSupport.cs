using ICSharpCode.SharpZipLib.Zip;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using Triggerless.TriggerBot.Models;

namespace Triggerless.TriggerBot.Forms
{
    public partial class TechSupport : Form
    {
        public TechSupport()
        {
            InitializeComponent();
        }

        private void CleanUpFTP(string folderName)
        {
            Directory.Delete(folderName, true);
        }
        private void _btnUpload_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(_txtAviName.Text)) {
                MessageBox.Show($"Avatar Name is required", "User Input Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var targetFolder = Path.Combine(appData, "Triggerless", "Transfer", "Files");
            if (!Directory.Exists(targetFolder)) Directory.CreateDirectory(targetFolder);
            var parentFolder = Directory.GetParent(targetFolder).FullName;

            
            var imvuFile = "productInfoCache.db";
            var tbFile = "appCache.sqlite";
            var imvuDB = Path.Combine(appData, "IMVU", imvuFile);
            var tbDB = Path.Combine(appData, "Triggerless", "Triggerbot", tbFile);
            try
            {
                File.Copy(imvuDB, Path.Combine(targetFolder, imvuFile), true);
                File.Copy(tbDB, Path.Combine(targetFolder, tbFile), true);
            } catch(Exception exc) { 
                MessageBox.Show($"Unable to copy one of the database files: {exc.Message}", "File System Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                CleanUpFTP(parentFolder);
                return;
            }

            if (!File.Exists(imvuDB) || !File.Exists(tbDB))
            {
                MessageBox.Show($"Unable to copy one of the database files: Files could not be copied", "File System Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                CleanUpFTP(parentFolder);
                return ;

            }

            FastZip fz = new FastZip();
            var zipName = Path.Combine(parentFolder,
                "TBot-" + _txtAviName.Text.Trim() + "-" + 
                DateTime.Now.ToString("yyyyMMdd-hhmmss") + ".zip");
            var invalidChars = Path.GetInvalidPathChars();
            string zipName2 = string.Empty;
            foreach (char c in zipName)
            {
                zipName2 += (invalidChars.Contains(c) ? '_' : c);
            }
            fz.CreateZip(zipName2, targetFolder, false, null);

            Uri remoteUri = new Uri("ftp://triggerless.com/");
            using (var wc = new WebClient())
            {
                try
                {
                    wc.Credentials = new NetworkCredential("triggerbot", "$tr1gg3rb0t$");
                    var remotePath = remoteUri.ToString() + Path.GetFileName(zipName2);
                    wc.UploadFile(remotePath, "STOR", zipName2);
                    CleanUpFTP(parentFolder);
                    _ = Discord.SendMessage("New Tech Support Upload", $"Tech Support: Upload for {_txtAviName.Text} succeeded.").Result;
                    Close();
                } catch (Exception exc) {
                    _ = Discord.SendMessage("Upload Failed", $"Tech Support: Upload for {_txtAviName.Text}  failed.").Result;
                    MessageBox.Show($"Unable to upload file: {exc.Message}");
                }
            }

        }
    }
}
