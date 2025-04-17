using NAudio.Wave;
using NAudio.WaveFormRenderer;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Windows.Forms;
using Triggerless.TriggerBot.Forms;
using Triggerless.TriggerBot.Models;

namespace Triggerless.TriggerBot.Components
{


    public partial class LyricsCtrl : UserControl
    {

        private Mp3FileReader _mp3FileReader;
        private Image _waveform;
        private IWavePlayer _wavePlayer;
        private int _clickedRowIndex = -1;
        private PictureBox picOverlay;
        private Pen _penYellow = new Pen(Color.Yellow, 1);
        private Pen _penBlack = new Pen(Color.Black, 1);

        private ProductDisplayInfo _product;
        public LyricsCtrl()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.Selectable, true);
            this.TabStop = true;

            // Make the overlay PictureBox a child of the waveform PictureBox
            picOverlay = new PictureBox();
            picOverlay.Parent = picWave;
            picOverlay.Location = new Point(0, 0);
            
            // Set the overlay's background to transparent so the underlying waveform shows through.
            picOverlay.BackColor = Color.Transparent;

            // Optionally, you can enable double-buffering on the overlay to reduce flicker.
            picOverlay.DoubleBuffered(true);
            picWave.MouseDown += WaveClicked;

        }


        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Control | Keys.B))
            {
                SetTimeMarker();
                return true; // mark as handled
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void LyricsCtrl_Load(object sender, EventArgs e)
        {
            picOverlay.Size = picWave.Size;
        }

        private void btnSelectProduct_Click(object sender, EventArgs e)
        {
            var dlg = new ProductOpenDialog();
            var result = dlg.ShowDialog();
            if (result == DialogResult.OK && dlg.SelectedProduct != null)
            {
                _product = dlg.SelectedProduct;
                lblProductName.Text = _product.Name;
                lblCreatorName.Text = $"by {_product.Creator}";
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
            GetExistingLyrics();

        }

        private void WaveformCreate(string filename)
        {
            lblTimer.Text = "00:00.000";
            if (_wavePlayer != null)
            {
                if (_wavePlayer.PlaybackState != PlaybackState.Stopped) 
                {
                    _wavePlayer.Stop();
                }
                _wavePlayer.Dispose();
                _wavePlayer = null;
            }            
            _timer.Stop();

            picWave.Image?.Dispose();
            _waveform?.Dispose();
            picWave.Image = null;
            picWave.Update();
            _waveform = null;

            _mp3FileReader?.Dispose();
            _mp3FileReader = new Mp3FileReader(filename);

            var wavePath = Path.Combine(Shared.LyricSheetsPath, $"{_product.Id}.wave.png");
            if (File.Exists(wavePath))
            {
                _waveform = Image.FromFile(wavePath);
            } 
            else
            {
                var maxPeakProvider = new MaxPeakProvider();
                var rmsPeakProvider = new RmsPeakProvider(200);
                var samplingPeakProvider = new SamplingPeakProvider(200);
                var averagePeakProvider = new AveragePeakProvider(4);
                var renderSettings = new StandardWaveFormRendererSettings
                {
                    Width = picWave.Width,
                    TopHeight = picWave.Height / 2,
                    BottomHeight = picWave.Height / 2,
                    BackgroundColor = Color.AliceBlue,
                    TopPeakPen = Pens.Blue,
                    BottomPeakPen = Pens.Blue,

                };
                var renderer = new WaveFormRenderer();
                Cursor = Cursors.WaitCursor;
                _waveform = renderer.Render(_mp3FileReader, averagePeakProvider, renderSettings);
                _waveform.Save(wavePath);
                Cursor = Cursors.Default;
            }

            picWave.Image = _waveform;
        }

        private void btnGetLyrics_Click(object sender, EventArgs e)
        {
            if (_product == null) { 
                MessageBox.Show("Choose a Trigger Tune first."); return;
            }
            var f = new LyricsPaste();
            var result = f.ShowDialog();
            if (result == DialogResult.OK)
            {
                ProcessLyrics(f.CopiedText);
            }
        }

        private void ProcessLyrics(string copiedText)
        {
            // Place an overwrite warning before proceeding.

            var lines = copiedText.Split(new string[] {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries);
            gridLyrics.Rows.Clear();
            gridLyrics.SuspendLayout();
            foreach (var line in lines)
            {
                gridLyrics.Rows.Add("", line);
            }
            gridLyrics.ResumeLayout();
            gridLyrics.Select();
            gridLyrics.CurrentCell = gridLyrics.Rows[0].Cells[0];

        }

        private void SetTimeMarker()
        {
            gridLyrics.CurrentCell.Value = _mp3FileReader.CurrentTime.ToString(@"mm\:ss\.fff");
            var lastCell = gridLyrics.CurrentCell;
            if (gridLyrics.CurrentRow.Index < gridLyrics.RowCount - 1)
            {
                gridLyrics.CurrentCell = gridLyrics.Rows[gridLyrics.CurrentCell.RowIndex + 1].Cells[0];
                gridLyrics.FirstDisplayedCell = lastCell;
            }
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            if (_product == null)
            {
                MessageBox.Show("Choose a Trigger Tune first."); return;
            }
            try
            {
                // Initialize output device if it's not already set.
                if (_wavePlayer == null)
                {
                    _wavePlayer = new WaveOutEvent();
                    _wavePlayer.PlaybackStopped += EndOfPlayback;
                    if (_mp3FileReader != null)
                    {
                        _wavePlayer.Init(_mp3FileReader);
                    }
                }

                // Initialize MP3 reader if necessary.
                if (_mp3FileReader == null)
                {
                    _mp3FileReader = new Mp3FileReader($"{Shared.LyricSheetsPath}\\{_product.Id}.mp3");
                    _wavePlayer.Init(_mp3FileReader);
                }
                else if (_mp3FileReader.Position == _mp3FileReader.Length)
                {
                    // If end-of-file is reached, rewind.
                    _mp3FileReader.CurrentTime = TimeSpan.Zero;
                }

                // Start playback and the stopwatch if not already running.
                if (_wavePlayer.PlaybackState != PlaybackState.Playing)
                {
                    _wavePlayer.Play();
                    _timer.Start();
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error playing file: " + ex.Message);
            }


        }

        private void EndOfPlayback(object sender, StoppedEventArgs e)
        {
            _timer.Stop();
            lblTimer.Text = "00:00.000";
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            if (_product == null)
            {
                MessageBox.Show("Choose a Trigger Tune first."); return;
            }
            try
            {
                if (_wavePlayer != null && _wavePlayer.PlaybackState == PlaybackState.Playing)
                {
                    _wavePlayer.Pause();
                    _timer.Stop();
                }
                else if (_wavePlayer != null && _wavePlayer.PlaybackState == PlaybackState.Paused)
                {
                    _wavePlayer.Play();
                    _timer.Start();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error pausing playback: " + ex.Message);
            }

        }

        private void UpdateTimeLabel(object sender, EventArgs e)
        {
            if (_mp3FileReader == null) return;

            // Display the elapsed time in the format mm:ss.xxx
            lblTimer.Text = $"{_mp3FileReader.CurrentTime:mm\\:ss\\.fff}";
            DrawOverlay();
        }

        private void SaveLyrics()
        {
            List<LyricEntry> list = new List<LyricEntry>();
            for (int i = 0; i < gridLyrics.Rows.Count; i++)
            {
                var timeString = gridLyrics.Rows[i].Cells[0].Value?.ToString();
                var lyric = gridLyrics.Rows[i].Cells[1].Value?.ToString();
                if (string.IsNullOrWhiteSpace(lyric)) continue;
                if (string.IsNullOrWhiteSpace(timeString)) continue;
                bool parsed = TimeSpan.TryParseExact(timeString, @"mm\:ss\.fff", CultureInfo.InvariantCulture, out var timeSpan);
                if (!parsed)
                {
                    gridLyrics.CurrentCell = gridLyrics.Rows[i].Cells[0];
                    MessageBox.Show("This cell has the time in the wrong format. Please fix it.", "Wrong Format", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                list.Add(new LyricEntry { Time = timeSpan, Lyric = lyric });
            }
            if (list.Count == 0) return;
            var jsonText= JsonConvert.SerializeObject(list, Formatting.Indented);
            var filename = Path.Combine(Shared.LyricSheetsPath, $"{_product.Id}.lyrics");
            if (File.Exists(filename)) File.Delete(filename);
            File.WriteAllText(filename, jsonText);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveLyrics();
        }

        private void GetExistingLyrics()
        {
            if (_product == null) return;
            gridLyrics.Rows.Clear();
            var filename = Path.Combine(Shared.LyricSheetsPath, $"{_product.Id}.lyrics");
            if (!File.Exists(filename)) return;
            var list = JsonConvert.DeserializeObject<List<LyricEntry>>(File.ReadAllText(filename));
            foreach ( var entry in list )
            { 
                gridLyrics.Rows.Add(entry.Time.ToString(@"mm\:ss\.fff"), entry.Lyric);
            }

        }

        private void mnuInsertAbove_Click(object sender, EventArgs e)
        {
            if (_clickedRowIndex >= 0)
            {
                gridLyrics.Rows.Insert(_clickedRowIndex);
                _clickedRowIndex++; // keep context on originally clicked row
            }
        }

        private void ctxMenu_Opening(object sender, CancelEventArgs e)
        {
            // Only allow the menu to open if a row header was right-clicked.
            if (_clickedRowIndex < 0)
            {
                e.Cancel = true;
                return;
            }

            mnuInsertAbove.Enabled = _clickedRowIndex >= 0;
            mnuInsertBelow.Enabled = _clickedRowIndex < gridLyrics.Rows.Count - 1;
        }

        private void mnuDelete_Click(object sender, EventArgs e)
        {
            if (_clickedRowIndex >= 0 && _clickedRowIndex < gridLyrics.Rows.Count)
            {
                gridLyrics.Rows.RemoveAt(_clickedRowIndex);
                _clickedRowIndex = -1;
            }
        }

        private void gridLyrics_MouseDown(object sender, MouseEventArgs e)
        {
            // Process only right clicks.
            if (e.Button == MouseButtons.Right)
            {
                var hitTestInfo = gridLyrics.HitTest(e.X, e.Y);

                // Check if the hit is on the row header.
                if (hitTestInfo.Type == DataGridViewHitTestType.RowHeader && hitTestInfo.RowIndex >= 0)
                {
                    // Store the clicked row index.
                    _clickedRowIndex = hitTestInfo.RowIndex;

                    // Optionally select the row.
                    gridLyrics.ClearSelection();
                    gridLyrics.Rows[_clickedRowIndex].Selected = true;
                }
                else
                {
                    // Ensure that if the user right-clicks elsewhere, the context menu doesn't appear.
                    _clickedRowIndex = -1;
                }
            }

        }

        private void mnuInsertBelow_Click(object sender, EventArgs e)
        {
            if (_clickedRowIndex >= 0)
            {
                gridLyrics.Rows.Insert(_clickedRowIndex + 1);
            }
        }

        private void DrawOverlay()
        {
            var totalTime = _mp3FileReader.TotalTime.TotalMilliseconds;
            if (totalTime == 0) return;
            var currentTime = _mp3FileReader.CurrentTime.TotalMilliseconds;
            int x = Convert.ToInt32(picOverlay.Width * currentTime / totalTime);

            // Create a new bitmap with alpha channel
            Bitmap bmp = new Bitmap(picOverlay.Width, picOverlay.Height, PixelFormat.Format32bppArgb);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                // Clear the bitmap to transparent
                g.Clear(Color.Transparent);
                g.DrawLine(_penBlack, x, 0, x, picOverlay.Height);
                g.DrawLine(_penYellow, x+1, 0, x+1, picOverlay.Height);
            }
            // Dispose the old image if it exists to avoid memory leaks.
            if (picOverlay.Image != null)
            {
                picOverlay.Image.Dispose();
            }
            // Set the newly drawn bitmap as the overlay image.
            picOverlay.Image = bmp;
        }

        private void WaveClicked(object sender, MouseEventArgs e)
        {
            if (_wavePlayer != null || _mp3FileReader == null) return;
            if (_wavePlayer.PlaybackState == PlaybackState.Playing)
            {
                btnPause_Click(sender, new EventArgs());
            }
            double fraction = e.X / picOverlay.Width;
            TimeSpan newTime = TimeSpan.FromMilliseconds(fraction * _mp3FileReader.TotalTime.TotalMilliseconds);
            DrawOverlay();
            lblTimer.Text = newTime.ToString(@"mm\:ss\.fff");
            _mp3FileReader.CurrentTime = newTime;
        }


        private void LyricsCtrl_ControlRemoved(object sender, ControlEventArgs e)
        {
            _penBlack?.Dispose();
            _penYellow?.Dispose();
            _waveform?.Dispose();
            _mp3FileReader?.Dispose();
            _wavePlayer?.Dispose();
            picOverlay.Image?.Dispose();
            picWave.Image?.Dispose();
        }

        private void btnTimeIt_Click(object sender, EventArgs e)
        {
            if (ParentForm.TopMost) ParentForm.WindowState = FormWindowState.Minimized;
            Shared.GenerateTimeItText(_product);
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            if (_mp3FileReader == null || _wavePlayer == null) return;
            if ((_wavePlayer.PlaybackState == PlaybackState.Playing) || _wavePlayer.PlaybackState == PlaybackState.Paused) 
            {
                _wavePlayer.Stop();
                _mp3FileReader.CurrentTime = TimeSpan.Zero;
                lblTimer.Text = "00:00.000";
                DrawOverlay();
            }
        }
    }
}
