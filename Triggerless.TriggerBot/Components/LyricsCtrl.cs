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
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Triggerless.TriggerBot.Forms;
using Triggerless.TriggerBot.Models;

namespace Triggerless.TriggerBot.Components
{
    /// <summary>
    /// A UserControl that is placed on a tab page on the main form
    /// that is used to create lyric sheets in Triggerbot.
    /// </summary>

    public partial class LyricsCtrl : UserControl
    {

        private Mp3FileReader _mp3FileReader; // NAudio MP3 file reader
        private Image _waveform;                // Waveform image
        private IWavePlayer _wavePlayer;        // Audio playback
        private int _clickedRowIndex = -1;      // Used for context menu
        //private LyricsCursorPanel _cursorOverlay;          // This implements the scrolling bar during playback
        private const string TIMESPAN_FORMAT = @"mm\:ss\.fff"; // Common format we use for TimeSpan

        private ProductDisplayInfo _product;    // Currently select product

        /// <summary>
        /// Constructor, also sets up overlay by placing a PictureBox
        /// within another PictureBox (thanks ChatGPT)
        /// </summary>
        /// 
        public LyricsCtrl()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.Selectable, true);
            this.TabStop = true;

            // Make the overlay PictureBox a child of the waveform PictureBox
            /*
            _cursorOverlay = new LyricsCursorPanel
            {
                Parent = picWave,
                Size = picWave.Size,
                Location = new Point(0, 0),
                BackColor = Color.Green,
            };
            _cursorOverlay.MouseDown += picWave_MouseDown;
            _cursorOverlay.BringToFront();
            */
            //picWave.MouseDown += picWave_MouseDown;
            Disposed += OnDisposed;
        }

        private void OnDisposed(object sender, EventArgs e)
        {
            _waveform?.Dispose();
            _mp3FileReader?.Dispose();
            _wavePlayer?.Dispose();
            picWave.Image?.Dispose();

        }

        /// <summary>
        /// This is to capture the Ctrl-B key combination anywhere in
        /// this control to get the time of play and format it into a
        /// TimeSpan.
        /// </summary>
        /// <param name="msg">Windows message</param>
        /// <param name="keyData">What got key pressed</param>
        /// <returns>true, if we caught Ctrl-B</returns>
        /// 
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Control | Keys.B))
            {
                SetTimeMarker();
                return true; // mark as handled
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        /// <summary>
        /// Just resize _cursorOverlay after this control is loaded
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        private void LyricsCtrl_Load(object sender, EventArgs e)
        {
            //_cursorOverlay.Size = picWave.Size;
            triangle1.Position = 0;
            triangle1.BringToFront();

            ctlNeedsSave.Dirty = false;
        }

        /// <summary>
        /// This brings up the ProductOpenDialog for user to choose a product. If the user
        /// chose one, we reset everything and start over with a new _product. We did run into
        /// a problem with animated GIF causing an Application crash, but found a way around
        /// it by only grabbing the first frame of the GIF and using that for the product image.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        private void btnSelectProduct_Click(object sender, EventArgs e)
        {
            var parentTopMost = ParentForm.TopMost;
            ParentForm.TopMost = false;
            var dlg = new ProductOpenDialog();
            var result = dlg.ShowDialog();
            ParentForm.TopMost = parentTopMost;
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
                ctlNeedsSave.Dirty = false;

            }
        }

        /// <summary>
        /// This was the solution ChatGPT came up with to extract the first
        /// frame of the animated GIF. It works.
        /// </summary>
        /// <param name="imageBytes">the binary image data from the database</param>
        /// <returns>the first frame as a bitmap Image</returns>
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

        /// <summary>
        /// This method creates an MP3 file for playback by downloading all the OGG
        /// files from the IMVU content server, then uses FFmpeg to construct them
        /// back into a single MP3 file for playback. If the MP3 file already exists,
        /// we just use that one.
        /// </summary>
        /// <returns>The path to the MP3 file</returns>
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
        }

        /// <summary>
        /// This is just a list of things we have to do when a new product
        /// is selected by the user.
        /// </summary>
        private void InitProduct()
        {
            if (_product == null)
            {
                MessageBox.Show("Choose a Trigger Tune first."); 
                return;
            }
            try
            {
                picWave.Visible = false;
                CleanupPrevious();
                var mp3Path = GetOrCreateMP3();
                if (!InitReaderAndPlayer()) return;
                WaveformCreate(mp3Path);
                GetExistingLyrics();
                picWave.Visible = true;
            }
            catch (Exception ex)
            {
                Application.Exit();
            }

        }

        private bool InitReaderAndPlayer()
        {
            var mp3FileName = Path.Combine(Shared.LyricSheetsPath, $"{_product.Id}.mp3");
            if (!File.Exists(mp3FileName))
            {
                MessageBox.Show($"Could not open {mp3FileName}", 
                    "File Read Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (_mp3FileReader == null) // should be true at this point
            {
                _mp3FileReader = new Mp3FileReader(mp3FileName);
            }
            lblCreatorName.Text = 
                $"by {_product.Creator} ({_mp3FileReader.TotalTime.ToString(TIMESPAN_FORMAT)})";

            if (_wavePlayer == null)
            {
                _wavePlayer = new WaveOutEvent();
                _wavePlayer.PlaybackStopped += EndOfPlayback;
                _wavePlayer.Init(_mp3FileReader);
                triangle1.Position = 0;
                _mp3FileReader.CurrentTime = TimeSpan.Zero;
            }
            lblTimer.Text = _mp3FileReader.CurrentTime.ToString(TIMESPAN_FORMAT);
            return true;
        }

        private void CleanupPrevious()
        {
            lblTimer.Text = TimeSpan.Zero.ToString(TIMESPAN_FORMAT);
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
            _mp3FileReader = null;
            triangle1.Position = 0;
        }

        /// <summary>
        /// This creates the waveform image using NAudio.WaveFormRenderer.
        /// It's a little slow and clunky, but gets the job done. This method
        /// also initializes the state of the _wavePlayer for MP3 playback. If 
        /// we already have the .WAVE.PNG file we just use that one.
        /// </summary>
        /// <param name="mp3FileName">Full path to the MP3 file</param>
        private void WaveformCreate(string mp3FileName)
        {

            var wavePath = Path.Combine(Shared.LyricSheetsPath, $"{_product.Id}.wave.png");
            if (!File.Exists(wavePath))
            {
                /*
                var maxPeakProvider = new MaxPeakProvider();
                var rmsPeakProvider = new RmsPeakProvider(200);
                var samplingPeakProvider = new SamplingPeakProvider(200);
                var averagePeakProvider = new AveragePeakProvider(4);
                var renderSettings = new StandardWaveFormRendererSettings
                {
                    Width = picWave.Width,
                    TopHeight = picWave.Height / 2,
                    BottomHeight = picWave.Height / 2,
                    BackgroundColor = Color.Transparent,
                    TopPeakPen = Pens.Blue,
                    BottomPeakPen = Pens.Blue,

                };
                var renderer = new WaveFormRenderer();
                Cursor = Cursors.WaitCursor;
                _waveform = renderer.Render(_mp3FileReader, averagePeakProvider, renderSettings);
                _waveform.Save(wavePath);
                Cursor = Cursors.Default; */

                FastWaveform.SavePngFromFile(mp3FileName, 
                    wavePath, picWave.Width, picWave.Height, 5000, Color.Blue);
            }
            _waveform = Image.FromFile(wavePath);
            picWave.Image = _waveform;
            triangle1.Position = 0;
            triangle1.BringToFront();
        }

        /// <summary>
        /// This brings up the LyricsPaste dialog, and if user selects OK,
        /// we call ProcessLyrics to populate the lyrics grid.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// This does the actual processing of the text that's in the Clipboard.
        /// See issue #6 for upcoming changes.
        /// </summary>
        /// <param name="copiedText">text that was in the Clipboard, or manually entered to LyricsPaste dialog</param>
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
            ctlNeedsSave.Dirty = true;

        }

        /// <summary>
        /// This gets the current playback time and sets the current cell value to
        /// that time, and advances to the next cell, if there is one.
        /// </summary>
        private void SetTimeMarker()
        {
            gridLyrics.CurrentCell.Value = _mp3FileReader.CurrentTime.ToString(TIMESPAN_FORMAT);
            ctlNeedsSave.Dirty = true;

            var lastCell = gridLyrics.CurrentCell;
            if (gridLyrics.CurrentRow.Index < gridLyrics.RowCount - 1)
            {
                gridLyrics.CurrentCell = gridLyrics.Rows[gridLyrics.CurrentCell.RowIndex + 1].Cells[0];
                gridLyrics.FirstDisplayedCell = lastCell;
            }
        }

        /// <summary>
        /// Let's play a tune. This initializes the wave player for playback
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPlay_Click(object sender, EventArgs e)
        {
            try
            {
                switch (_wavePlayer.PlaybackState)
                {
                    case PlaybackState.Stopped:
                        _mp3FileReader.CurrentTime = GetCurrentTime();
                        _wavePlayer.Play();
                        break;
                    case PlaybackState.Playing:
                        _wavePlayer.Pause();
                        _mp3FileReader.CurrentTime = GetCurrentTime();
                        _wavePlayer.Play();
                        break;
                    case PlaybackState.Paused:
                        _mp3FileReader.CurrentTime = GetCurrentTime();
                        _wavePlayer.Play();
                        break;
                    default:
                        break;
                }
                _timer.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error playing file: " + ex.Message);
            }
        }

        /// <summary>
        /// Event handler that is triggered when playback is stopped.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EndOfPlayback(object sender, StoppedEventArgs e)
        {
            _timer.Stop();
            lblTimer.Text = TimeSpan.Zero.ToString(TIMESPAN_FORMAT);
            triangle1.Position = 0;
            if (_mp3FileReader != null) _mp3FileReader.CurrentTime = new TimeSpan(0L);
        }

        /// <summary>
        /// Event handler from Pause button click. This pauses or resumes playback.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPause_Click(object sender, EventArgs e)
        {
            if (_product == null || _wavePlayer == null)
            {
                MessageBox.Show("Choose a Trigger Tune first."); return;
            }
            try
            {
                if (_wavePlayer.PlaybackState == PlaybackState.Playing)
                {
                    _wavePlayer.Pause();
                    _timer.Stop();
                }
                else if (_wavePlayer.PlaybackState == PlaybackState.Paused)
                {
                    _mp3FileReader.CurrentTime = GetCurrentTime();
                    _wavePlayer.Play();
                    _timer.Start();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error pausing playback: " + ex.Message);
            }
        }

        /// <summary>
        /// This timer Tick event is used to update the time label and the
        /// scrolling line on the waveform.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _timer_Tick(object sender, EventArgs e)
        {
            if (_mp3FileReader == null) return;

            // Display the elapsed time in the format mm:ss.xxx
            lblTimer.Text = _mp3FileReader.CurrentTime.ToString(TIMESPAN_FORMAT);
            UpdateCursor();
        }

        /// <summary>
        /// This saves the JSON file (*.lyrics) to the Shared.LyricsPath directory.
        /// If the format of the time stamp is incorrect, we prompt for a fix.
        /// However, we should be more forgiving, see Issue #8 
        /// https://github.com/cheri-imvu/triggerless-triggerbot/issues/8
        /// </summary>
        private void SaveLyrics()
        {
            List<LyricEntry> list = new List<LyricEntry>();
            TimeSpan defaultTime = TimeSpan.FromMinutes(99);
            for (int i = 0; i < gridLyrics.Rows.Count; i++)
            {
                var timeString = gridLyrics.Rows[i].Cells[0].Value?.ToString();
                var lyric = gridLyrics.Rows[i].Cells[1].Value?.ToString();
                if (string.IsNullOrWhiteSpace(timeString))
                {
                    timeString = defaultTime.ToString(TIMESPAN_FORMAT);
                    defaultTime = defaultTime.Add(TimeSpan.FromMilliseconds(1));
                }
                bool parsed = TimeSpan.TryParseExact(timeString, TIMESPAN_FORMAT, CultureInfo.InvariantCulture, out var timeSpan);
                if (!parsed)
                {
                    // has colon instead of period
                    Regex rxColon = new Regex(@"^\d{2}\:\d{2}\:\d{3}$", RegexOptions.None);
                    if (rxColon.IsMatch(timeString))
                    {
                        timeString = timeString.Substring(0, 5) + "." + timeString.Substring(6, 3);
                        gridLyrics.Rows[i].Cells[0].Value = timeString;
                        timeSpan = TimeSpan.ParseExact(timeString, TIMESPAN_FORMAT, CultureInfo.InvariantCulture);
                    } 
                    else
                    {
                        gridLyrics.CurrentCell = gridLyrics.Rows[i].Cells[0];
                        MessageBox.Show("This cell has the time in the wrong format. Please fix it.", "Wrong Format", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        gridLyrics.BeginEdit(false);
                        return;
                    }
                }
                list.Add(new LyricEntry { Time = timeSpan, Lyric = lyric });
            }
            if (list.Count == 0) return;
            list = list.OrderBy(e => e.Time).ToList();
            var jsonText= JsonConvert.SerializeObject(list, Formatting.Indented);
            var filename = Path.Combine(Shared.LyricSheetsPath, $"{_product.Id}.lyrics");
            if (File.Exists(filename)) File.Delete(filename);
            File.WriteAllText(filename, jsonText);
            ctlNeedsSave.Dirty = false;
        }

        /// <summary>
        /// Button click handler that kicks off SaveLyrics
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveLyrics();
        }

        /// <summary>
        /// If we already have the .lyrics file, we deserialize the JSON to 
        /// populate the grid.
        /// </summary>
        private void GetExistingLyrics()
        {
            if (_product == null) return;
            gridLyrics.Rows.Clear();
            var filename = Path.Combine(Shared.LyricSheetsPath, $"{_product.Id}.lyrics");
            if (!File.Exists(filename)) return;
            var list = JsonConvert.DeserializeObject<List<LyricEntry>>(File.ReadAllText(filename));
            foreach ( var entry in list )
            { 
                gridLyrics.Rows.Add(entry.Time.ToString(TIMESPAN_FORMAT), entry.Lyric);
            }
            ctlNeedsSave.Dirty = false;
        }

        /// <summary>
        /// Respond to menu click, Insert Row Above
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuInsertAbove_Click(object sender, EventArgs e)
        {
            if (_clickedRowIndex >= 0)
            {
                gridLyrics.Rows.Insert(_clickedRowIndex);
                _clickedRowIndex++; // keep context on originally clicked row
                ctlNeedsSave.Dirty = true;
            }
        }

        /// <summary>
        /// Respond to context menu opening, and gray out unavailable options
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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


        /// <summary>
        /// Responding to deleting a row
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuDelete_Click(object sender, EventArgs e)
        {
            if (_clickedRowIndex >= 0 && _clickedRowIndex < gridLyrics.Rows.Count)
            {
                gridLyrics.Rows.RemoveAt(_clickedRowIndex);
                _clickedRowIndex = -1;
                ctlNeedsSave.Dirty = true;
            }
        }

        /// <summary>
        /// Event handler detects where mouse was clicked, and if the right
        /// click was in the left margin, allow this click to trigger the context
        /// menu, otherwise just discard the message and move on.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Respond to menu click Insert Row Below
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuInsertBelow_Click(object sender, EventArgs e)
        {
            if (_clickedRowIndex >= 0)
            {
                gridLyrics.Rows.Insert(_clickedRowIndex + 1);
                ctlNeedsSave.Dirty = true;
            }
        }

        /// <summary>
        /// This moves the triangle.
        /// </summary>
        private void UpdateCursor()
        {
            var totalTime = _mp3FileReader.TotalTime.TotalMilliseconds;
            if (totalTime == 0) return;
            var currentTime = _mp3FileReader.CurrentTime.TotalMilliseconds;
            triangle1.Position = Convert.ToInt32(picWave.Width * currentTime / totalTime);
        }

        private void picWave_MouseDown(object sender, MouseEventArgs e)
        {
            if (_waveform == null || _wavePlayer == null) return;

            double fraction = (double)e.X / (double)picWave.Width;
            triangle1.Position = e.X;
            TimeSpan newTime = TimeSpan.FromMilliseconds(fraction * _mp3FileReader.TotalTime.TotalMilliseconds);
            _mp3FileReader.CurrentTime = newTime;
            UpdateCursor();
            lblTimer.Text = newTime.ToString(TIMESPAN_FORMAT);
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
                _timer.Stop();
                _mp3FileReader.CurrentTime = TimeSpan.Zero;
                lblTimer.Text = TimeSpan.Zero.ToString(TIMESPAN_FORMAT);
                UpdateCursor();
            }
        }

        private void AdjustTimeMarkers(int ms)
        {
            DataGridViewRowCollection rows;
            if (gridLyrics.SelectedRows.Count == 0)
            {
                rows = gridLyrics.Rows;
            }
            else
            {
                rows = new DataGridViewRowCollection(gridLyrics);

                foreach (DataGridViewRow row in gridLyrics.Rows)
                {
                    if (row.State == DataGridViewElementStates.Selected)
                        rows.Add(gridLyrics.Rows[row.Index]);
                }
            }
                
            foreach (DataGridViewRow row in rows)
            {
                object value = row.Cells[0].Value;
                if (value == null) continue;

                string s = value.ToString();
                if (string.IsNullOrWhiteSpace(s)) continue;

                TimeSpan ts = TimeSpan.Zero;
                bool parsed = TimeSpan.TryParseExact(s, TIMESPAN_FORMAT, CultureInfo.InvariantCulture, out ts);
                if (!parsed) continue;

                TimeSpan newTS = ts.Add(TimeSpan.FromMilliseconds(ms));

                row.Cells[0].Value = newTS.ToString(TIMESPAN_FORMAT);
            }
            ctlNeedsSave.Dirty = true;
        }

        private void btnMsPlus_Click(object sender, EventArgs e)
        {
            int ms = 0;
            bool parsed = int.TryParse(txtMS.Text, out ms);
            if (!parsed) return;
            AdjustTimeMarkers(ms);
        }

        private void btnMsMinus_Click(object sender, EventArgs e)
        {
            int ms = 0;
            bool parsed = int.TryParse(txtMS.Text, out ms);
            if (!parsed) return;
            AdjustTimeMarkers(-ms);
        }

        private void gridLyrics_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            ctlNeedsSave.Dirty = true;
        }

        private void btnDeleteLyrics_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("You sure?", "Delete Lyrics", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            if (result == DialogResult.OK)
            {
                File.Delete(Path.Combine(Shared.LyricSheetsPath, $"{_product.Id}.lyrics"));
                gridLyrics.Rows.Clear();
                ctlNeedsSave.Dirty = false;
            }
        }

        private TimeSpan GetCurrentTime()
        {
            if (_mp3FileReader == null) return TimeSpan.Zero;

            long totalTicks = _mp3FileReader.TotalTime.Ticks;
            double fraction = (double)triangle1.Position / (double)picWave.Width;
            long currentTicks = Convert.ToInt64(fraction * totalTicks);
            return new TimeSpan(currentTicks);
        }

    }
}
