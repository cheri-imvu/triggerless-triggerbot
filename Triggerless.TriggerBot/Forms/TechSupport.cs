using ICSharpCode.SharpZipLib.Zip;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
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

        private void CleanUpLocalFiles(string folderName)
        {
            Directory.Delete(folderName, true);
        }
        private async void _btnUpload_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(_txtAviName.Text)) {
                StyledMessageBox.Show(this, $"Avatar Name is required", "User Input Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            pnlUploading.Visible = true;
            var appData = Shared.AppData;
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
                StyledMessageBox.Show(this, $"Unable to copy one of the database files: {exc.Message}", "File System Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                CleanUpLocalFiles(parentFolder);
                pnlUploading.Visible = false;
                return;
            }

            if (!File.Exists(imvuDB) || !File.Exists(tbDB))
            {
                StyledMessageBox.Show(this, $"Unable to copy one of the database files: Files could not be copied", "File System Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                CleanUpLocalFiles(parentFolder);
                pnlUploading.Visible = false;
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
            /* OLD FTP Code
                        Uri remoteUri = new Uri("ftp://triggerless.com/");
                        using (var wc = new WebClient())
                        {
                            try
                            {
                                wc.Credentials = new NetworkCredential("triggerbot", "$tr1gg3rb0t$");
                                var remotePath = remoteUri.ToString() + Path.GetFileName(zipName2);
                                wc.UploadFile(remotePath, "STOR", zipName2);
                                CleanUpLocalFiles(parentFolder);
                                _ = Discord.SendMessage("New Tech Support Upload", $"Tech Support: Upload for {_txtAviName.Text} succeeded.").Result;
                                Close();
                            } catch (Exception exc) {
                                _ = Discord.SendMessage("Upload Failed", $"Tech Support: Upload for {_txtAviName.Text}  failed.").Result;
                                StyledMessageBox.Show($"Unable to upload file: {exc.Message}");
                            }
                        }
            */
            try
            {
                await UploadZipAsync(zipName2);
            } 
            catch (Exception ex)
            {
                await Discord.SendMessage("Upload Failed (Debug)", ex.ToString());
            }

        }

        private async Task UploadZipAsync(string zipFilePath)
        {
            var fileName = Path.GetFileName(zipFilePath);
            var uri = "https://www.triggerless.com/api/upload/techsupport";

            using (var client = new HttpClient())
            using (var content = new MultipartFormDataContent())
            using (var fs = File.OpenRead(zipFilePath))
            {
                int lastPercent = 0;
                var streamContent = new ProgressableStreamContent(fs, 4096, (sent, total) =>
                {
                    var percent = (int)(sent * 100 / total);
                    if (percent != lastPercent)
                    {
                        progPercent.Invoke((Action)(() => progPercent.Value = percent));
                        lblPercent.Invoke((Action)(() => lblPercent.Text = $"{percent}%"));
                        lastPercent = percent;
                    }
                });

                content.Add(streamContent, "file", fileName);

                try
                {
                    var response = await client.PostAsync(uri, content);
                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        await Discord.SendMessage("Debug Upload Failed.", response.Headers.ToString() +Environment.NewLine + response.Content.ToString());
                        return;
                    }

                    CleanUpLocalFiles(Path.GetDirectoryName(zipFilePath));
                    await Discord.SendMessage("New Tech Support Upload", $"Tech Support: Upload for {_txtAviName.Text} succeeded.");
                    Invoke((Action)(() => Close()));
                }
                catch (Exception ex)
                {
                    await Discord.SendMessage("Upload Failed", $"Tech Support: Upload for {_txtAviName.Text} failed.");
                    StyledMessageBox.Show(this, $"Upload failed: {ex}");
                }
            }
        }

    }
}
