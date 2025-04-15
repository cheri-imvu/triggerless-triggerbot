using DSharpPlus;
using NAudio.Wave;
using NAudio.WaveFormRenderer;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Triggerless.TriggerBot.Components
{
    public partial class LyricsCtrl : UserControl
    {

        private Mp3FileReader _mp3FileReader;
        private Image _waveform;

        private ProductDisplayInfo _product;
        public LyricsCtrl()
        {
            InitializeComponent();
        }

        private void LyricsCtrl_Load(object sender, EventArgs e)
        {

        }

        private void btnSelectProduct_Click(object sender, EventArgs e)
        {
            var dlg = new ProductOpenDialog();
            var result = dlg.ShowDialog();
            if (result == DialogResult.OK && dlg.SelectedProduct != null)
            {
                _product = dlg.SelectedProduct;
                lblProductName.Text = _product.Name;
                lblCreatorName.Text = _product.Creator;
                picProductImage.Image?.Dispose();

                // this flaky logic is to overcome that animated GIFs
                // will cause a GDI+ error, we have to convert to a PNG
                // first before assigning it to the PictureBox

                byte[] gifBytes = "GIF89ad".ToArray().Select(x => (byte)x).ToArray();
                byte[] imgBytes = _product.ImageBytes.Take(gifBytes.Length).ToArray();
                bool isAniGif = gifBytes.Zip(imgBytes, (x, y) => x == y)
                    .Aggregate(true, (acc, next) => acc && next);

                var tempMS = new MemoryStream(_product.ImageBytes);
                Image tempImage = tempImage = Image.FromStream(tempMS);

                picProductImage.Image = isAniGif ? ExtractFirstFrame(_product.ImageBytes) : tempImage;
                tempMS.Dispose();

                InitProduct();
            }
        }

        private Image ExtractFirstFrame(byte[] imageBytes)
        {
            using (var ms = new MemoryStream(imageBytes))
            using (var original = Image.FromStream(ms))
            {
                // Select the first frame if it's animated
                var dimension = new FrameDimension(original.FrameDimensionsList[0]);
                original.SelectActiveFrame(dimension, 0);

                // Create a new Bitmap with a non-indexed pixel format
                var firstFrame = new Bitmap(original.Width, original.Height, PixelFormat.Format32bppArgb);

                using (var g = Graphics.FromImage(firstFrame))
                {
                    g.DrawImage(original, 0, 0, original.Width, original.Height);
                }

                return firstFrame;
            }
        }

        private string GetOrCreateMP3()
        {
            // Ensure we have a LyricSheets directory
            string targetPath = Shared.LyricSheetsPath;

            // See if we already have the MP3 for this product
            string mp3Name = $"{_product.Id}.mp3";
            string mp3Path = Path.Combine(targetPath, mp3Name);
            List<string> oggFiles = new List<string>();

            // If not, Grab the OGG files for this product
            if (!File.Exists(mp3Path))
            {
                var sb = new StringBuilder();
                using (var client = new HttpClient())
                {
                    foreach (var trigger in _product.Triggers.OrderBy(t => t.Sequence))
                    {

                        string oggName = $"{_product.Id}_{trigger.Sequence:000}.ogg";
                        string oggPath = Path.Combine(targetPath, oggName);
                        oggFiles.Add(oggPath);
                        string url = Collector.GetUrl(_product.Id, trigger.Location);
                        byte[] oggBytes = client.GetByteArrayAsync(url).Result;
                        File.WriteAllBytes(oggPath, oggBytes);
                        sb.AppendLine($"file '{oggName}'");
                    }
                }

                string listFile = mp3Name.Replace(".mp3", ".list");
                string listPath = Path.Combine(targetPath, listFile);
                File.WriteAllText(listPath, sb.ToString());

                // ffmpeg -f concat -safe 0 -i files.txt -c:a libmp3lame output.mp3
                string args = $"-f concat -safe 0 -i {listFile} -c:a libmp3lame {mp3Name}";

                // Create a ProcessStartInfo object
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = $"{Shared.FFmpegLocation}\\ffmpeg.exe",
                    Arguments = args,
                    UseShellExecute = false,
                    RedirectStandardOutput = false,
                    RedirectStandardError = false,
                    CreateNoWindow = true,
                    WorkingDirectory = targetPath
                };


                // Start the process
                using (Process process = new Process { StartInfo = startInfo })
                {
                    process.Start();
                    process.WaitForExit();
                }

                File.Delete(listPath);
                foreach (var oggFile in oggFiles) File.Delete(oggFile);
            } //!File.Exists...
            return mp3Path;

            // and reassemble them to create a temporary MP3 file using ffmpeg

            // Initialize the audio player control

            // Initialize the lyrics grid 

            // Save a copy of the grid state, in case this doesn't get saved

            // Place lyric markers from the grid data on the audio player

            // Clear the Undo stack

        }

        private void InitProduct()
        {
            if (_product == null) return;
            var mp3Path = GetOrCreateMP3();
            WaveformCreate(mp3Path);

        }

        private void WaveformCreate(string filename)
        {
            picWaveform.Image?.Dispose();
            _waveform?.Dispose();
            picWaveform.Image = null;
            picWaveform.Update();
            _waveform = null;
            var maxPeakProvider = new MaxPeakProvider();
            var rmsPeakProvider = new RmsPeakProvider(200);
            var samplingPeakProvider = new SamplingPeakProvider(200);
            var averagePeakProvider = new AveragePeakProvider(4);
            var renderSettings = new StandardWaveFormRendererSettings
            {
                Width = picWaveform.Width,
                TopHeight = picWaveform.Height / 2,
                BottomHeight = picWaveform.Height / 2,
                BackgroundColor = Color.AliceBlue,
                TopPeakPen = Pens.Blue,
                BottomPeakPen = Pens.Blue,

            };
            var renderer = new WaveFormRenderer();
            Cursor = Cursors.WaitCursor;
            _mp3FileReader?.Dispose();
            _mp3FileReader = new Mp3FileReader(filename);
            _waveform = renderer.Render(_mp3FileReader, averagePeakProvider, renderSettings);
            picWaveform.Image = _waveform;
            Cursor = Cursors.Default;
        }

    }
}
