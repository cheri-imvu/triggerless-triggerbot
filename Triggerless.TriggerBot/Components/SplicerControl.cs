using ICSharpCode.SharpZipLib.Zip;
using NAudio.Wave;
using NAudio.WaveFormRenderer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Triggerless.XAFLib;

namespace Triggerless.TriggerBot
{
    public partial class SplicerControl : UserControl
    {
        private string _outputPath;
        private TimeSpan _duration = TimeSpan.Zero;
        private System.Drawing.Image _waveform;
        private const double INIT_VOLUME = 100;
        private double _volume = INIT_VOLUME;
        private Mp3FileReader _mp3FileReader;

        public new void Dispose()
        {
            (this as Component).Dispose();
            _mp3FileReader?.Dispose();
            _mp3FileReader = null;
        }

        public SplicerControl()
        {
            InitializeComponent();
        }

        public double AudioLength
        {
            get
            {
                try
                {
                    return double.Parse(cboAudioLength.SelectedItem.ToString());
                }
                catch(Exception)
                {
                    return 18;
                }
                
            }
            set
            {
                var valueString = value.ToString("0.0");
                var found = cboAudioLength.FindStringExact(valueString);
                if (found != -1)
                {
                    cboAudioLength.SelectedIndex = found;
                }
            }
        }

        private void SelectFile(object sender, EventArgs e)
        {
            _mp3FileReader?.Dispose();
            _mp3FileReader = null;

            if (string.IsNullOrWhiteSpace(dlgOpenFile.InitialDirectory))
            {
                var musicDir = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic);
            }
            dlgOpenFile.Multiselect = false;
            var result = dlgOpenFile.ShowDialog(this);
            if (result == DialogResult.OK)
            {
                dlgOpenFile.InitialDirectory = Path.GetDirectoryName(dlgOpenFile.FileName);
                try
                {
                    _mp3FileReader = new Mp3FileReader(dlgOpenFile.FileName);
                    _duration = _mp3FileReader.TotalTime;
                    txtFilename.Text = dlgOpenFile.FileName;
                    lblDuration.Text = $"Duration: {_duration.Minutes:00}:{_duration.Seconds:00}";

                    if (_duration.TotalSeconds > new TimeSpan(0, 6, 26).TotalSeconds)
                    {
                        rdoAMS.Checked = true;
                    }
                    else if (_duration.TotalSeconds > new TimeSpan(0, 3, 36).TotalSeconds)
                    {
                        rdoFMS.Checked = true;
                    }
                    else
                    {
                        rdoHQS.Checked = true;
                    }
                    WaveformCreate(txtFilename.Text);

                }
                catch (Exception)
                {
                    _mp3FileReader?.Dispose();
                    _mp3FileReader = null;
                    _duration = TimeSpan.Zero;
                    txtFilename.Text = string.Empty;
                    MessageBox.Show("Unable to read MP3 file. Skipping this file", "Invalid MP3", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

        }

        private void ShowMeTheFile(object sender, EventArgs e)
        {
            if (Directory.Exists(_outputPath))
            {
                string arguments = $"/select, \"{_outputPath}\\{txtPrefix.Text.Trim()}.chkn\"";
                Process.Start("explorer.exe", arguments);
            }
            else
            {

            }
        }

        private void StartConversion(object sender, EventArgs e)
        {
            #region Form Validation
            if (string.IsNullOrWhiteSpace(txtFilename.Text))
            {
                MessageBox.Show("Please select a file to slice", "Unable to Continue", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                btnSelectFile.Focus();
                return;
            }

            if (!File.Exists(txtFilename.Text))
            {
                MessageBox.Show($"The file '{txtFilename.Text}' doesn't exist", "Unable to Continue", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                btnSelectFile.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtPrefix.Text))
            {
                MessageBox.Show("A prefix must be specified", "Unable to Continue", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPrefix.Focus();
                txtPrefix.SelectAll();
                return;
            }

            if (Regex.Match(txtPrefix.Text.Trim(), @"[\s,0-9<>:""/\\|?*]", RegexOptions.None).Success)
            {
                MessageBox.Show("The chosen prefix does not conform to the rules.", "Unable to Continue", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPrefix.SelectAll();
                txtPrefix.Focus();
                return;
            }
            #endregion

            #region Audio Slicing
            var triggerPrefix = txtPrefix.Text.Trim();
            var increment = 1;
            _outputPath = Path.Combine(Shared.TriggerbotDocsPath, triggerPrefix);
            while (Directory.Exists(_outputPath))
            {
                _outputPath = Path.Combine(Shared.TriggerbotDocsPath, triggerPrefix + $"({increment})");
                increment++;
            }
            Directory.CreateDirectory(_outputPath);

            lblAction.Text = "Slicing Audio File";
            lblAction.Update();

            try
            {
                if (rdoMinima.Checked)
                {
                    _audioSegmenter.SegmentBySmartCut(
                        txtFilename.Text,
                        _outputPath,
                        triggerPrefix
                    );
                }
                else
                {
                    _audioSegmenter.SegmentAudio(
                        txtFilename.Text,
                        _outputPath,
                        TimeSpan.FromSeconds(double.Parse(cboAudioLength.SelectedItem.ToString())),
                        triggerPrefix
                    );
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Unable to slice file for the following reason:\n{ex.Message}. Make sure you're not playing the file while trying to slice it.",
                    "Unable to Slice", MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblAction.Text = "Aborted Audio Slice";
                return;
            }
            #endregion

            #region OGG Conversion
            lblAction.Text = "Creating OGG files";
            lblAction.Update();

            try
            {
                foreach (var filename in Directory.GetFiles(_outputPath, "*.wav"))
                {
                    var inputFile = filename;
                    var outputFile = filename.Replace(".wav", ".ogg");
                    var ffmpegLocation = Shared.FFmpegLocation;
                    int option = rdoAMS.Checked ? 0 :
                        rdoFMS.Checked ? 1 :
                        rdoHQM.Checked ? 2 :
                        rdoHQS.Checked ? 3 : 1;
                    _audioSegmenter.RunFFmpeg(ffmpegLocation, inputFile, outputFile, option, _volume / 100);
                }

            }
            catch (Exception exc)
            {
                MessageBox.Show($"Unable to convert to OGG for the following reason: {exc.Message}",
                    "Conversion Interrupted", MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblAction.Text = "Aborted OGG conversion";
                return;
            }

            lblAction.Text = "Removing temp files";
            foreach (var filename in Directory.GetFiles(_outputPath, "*.wav"))
            {
                File.Delete(filename);
            }

            foreach (var filename in Directory.GetFiles(_outputPath, "*.wav"))
            {
                File.Delete(filename);
            }

            foreach (var filename in Directory.GetFiles(_outputPath, "*.ogg"))
            {
                if (new FileInfo(filename).Length <= 4096) File.Delete(filename);
            }


            #endregion

            #region Create CHKN
            lblAction.Text = "Packaging CHKN file";
            lblAction.Update();
            var templates = new List<Template>();
            var listsOfFiles = new List<List<string>>();
            var parentId = chkCheap.Checked ?
                radioFemale.Checked ? 38766202 : 48704863 :
                radioFemale.Checked ? 63535754 : 63540074;

            templates.Add(new Template { ParentProductID = parentId });
            listsOfFiles.Add(new List<string>());
            var templateIndex = 0;
            long cumulativeSize = 0;
            const long maxSize = 2 * 1024 * 1024 - 20000;

            foreach (var filename in Directory.GetFiles(_outputPath, "*.ogg"))
            {
                var template = templates[templateIndex];
                var currentList = listsOfFiles[templateIndex];

                var nextFileSize = new FileInfo(filename).Length;
                if (cumulativeSize + nextFileSize > maxSize)
                {
                    templates.Add(new Template { ParentProductID = parentId });
                    templateIndex++;
                    template = templates[templateIndex];
                    listsOfFiles.Add(new List<string>());
                    currentList = listsOfFiles[templateIndex];
                    cumulativeSize = 0;
                }

                var action = new XAFLib.Action();
                string triggerName = triggerPrefix;
                triggerName += int.Parse(Path.GetFileNameWithoutExtension(filename).Replace(triggerPrefix, ""));
                action.Name = triggerName;
                action.Sound.Name = Path.GetFileName(filename);
                action.Definition.ActionDefinition.ActionAttributes.ActionTerminationIterations = 0;
                template.Actions.Add(action);
                currentList.Add(filename);
                cumulativeSize += nextFileSize;
            }

            var jsonFile = Path.Combine(_outputPath, "imvu-internal.json");
            if (File.Exists(jsonFile)) File.Delete(jsonFile);
            File.WriteAllText(jsonFile, "{}"); ;

            var tempDir = Path.Combine(_outputPath, "temp");

            templateIndex = 0;
            foreach (var template in templates)
            {
                if (Directory.Exists(tempDir))
                {
                    Directory.Delete(tempDir, true);
                }
                Directory.CreateDirectory(tempDir);
                File.Copy(jsonFile, Path.Combine(tempDir, Path.GetFileName(jsonFile)));
                File.WriteAllText(Path.Combine(tempDir, "index.xml"), template.GetIndexXml());
                foreach (var filename in listsOfFiles[templateIndex])
                {
                    File.Copy(filename, Path.Combine(tempDir, Path.GetFileName(filename)));
                }

                var fz = new FastZip();
                var chknFileNameOnly = listsOfFiles.Count == 1 ?
                    $"{triggerPrefix}.chkn" : $"{triggerPrefix}-{templateIndex + 1}.chkn";
                var chknFile = Path.Combine(_outputPath, chknFileNameOnly);
                fz.CreateZip(chknFile, tempDir, false, "", "");
                templateIndex++;
            }
            if (File.Exists(jsonFile)) File.Delete(jsonFile);
            Directory.Delete(tempDir, true);

            if (checkOGGFiles.Checked)
            {
                foreach (var filename in Directory.GetFiles(_outputPath, "*.ogg"))
                {
                    File.Delete(filename);
                }
            }

            #endregion

            #region Create PNG
            if (checkIcons.Checked)
            {
                lblAction.Text = "Creating Icons";
                lblAction.Update();
                var chknIndex = 0;
                foreach (var filename in Directory.GetFiles(_outputPath, "*.chkn"))
                {
                    var pngFile = filename.Replace(".chkn", ".png");
                    string text1 = string.Empty;
                    string text2 = string.Empty;
                    var currentList = listsOfFiles[chknIndex];
                    if (listsOfFiles.Count > 1)
                    {
                        text1 = $"Pt. {chknIndex + 1} / {listsOfFiles.Count}";
                    }
                    else
                    {
                        text1 = "Full song";
                    }

                    var firstName = Path.GetFileNameWithoutExtension(currentList.First());
                    var lastName = Path.GetFileNameWithoutExtension(currentList.Last());
                    var firstNumber = int.Parse(firstName.Replace(triggerPrefix, String.Empty));
                    var lastNumber = int.Parse(lastName.Replace(triggerPrefix, String.Empty));
                    text2 = $"{triggerPrefix}{firstNumber} - {lastNumber}";
                    chknIndex++;

                    CreateTextImage(text1, text2, pngFile);
                }
            }

            lblAction.Text = "Complete";
            #endregion
        }

        private void WaveformCreate(string filename)
        {
            var maxPeakProvider = new MaxPeakProvider();
            var rmsPeakProvider = new RmsPeakProvider(200);
            var samplingPeakProvider = new SamplingPeakProvider(200);
            var averagePeakProvider = new AveragePeakProvider(4);
            var renderSettings = new StandardWaveFormRendererSettings
            {
                Width = picWaveform.Width,
                TopHeight = picWaveform.Height / 2,
                BottomHeight = picWaveform.Height / 2,
                BackgroundColor = Color.Transparent,
                TopPeakPen = Pens.Blue,
                BottomPeakPen = Pens.Blue,
                
            };
            var renderer = new WaveFormRenderer();
            Cursor = Cursors.WaitCursor;
            if (_mp3FileReader is null) _mp3FileReader = new Mp3FileReader(filename);
            _waveform = renderer.Render(_mp3FileReader, averagePeakProvider, renderSettings);
            picWaveform.Image = _waveform;
            _volume = 100;
            Cursor = Cursors.Default;
            UpdateVolume();
        }

        private void CreateTextImage(string text1, string text2, string outputFilename)
        {
            int width = 100;
            int height = 80;

            using (Bitmap image = new Bitmap(width, height, PixelFormat.Format32bppArgb))
            {
                using (Graphics g = Graphics.FromImage(image))
                {
                    g.SmoothingMode = SmoothingMode.AntiAlias;
                    g.Clear(Color.Transparent);

                    using (Font font = new Font("Impact", 14f, FontStyle.Regular))
                    {
                        GraphicsPath path1;
                        GraphicsPath path2;
                        using (path1 = new GraphicsPath())
                        {
                            using (path2 = new GraphicsPath())
                            {
                                using (StringFormat sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center })
                                {
                                    RectangleF rect1 = new RectangleF(0, 0, width, height / 2);
                                    RectangleF rect2 = new RectangleF(0, height / 2, width, height / 2);

                                    path1.AddString(text1, font.FontFamily, (int)font.Style, g.DpiY * font.Size / 72, rect1, sf);
                                    path2.AddString(text2, font.FontFamily, (int)font.Style, g.DpiY * font.Size / 72, rect2, sf);
                                }

                                using (Pen outlinePen = new Pen(Color.Black, 2) { LineJoin = LineJoin.Round })
                                {
                                    g.DrawPath(outlinePen, path1);
                                    g.DrawPath(outlinePen, path2);
                                }

                                using (Brush textBrush = new SolidBrush(Color.White))
                                {
                                    g.FillPath(textBrush, path1);
                                    g.FillPath(textBrush, path2);
                                }
                            }
                        }

                    }
                }

                image.Save(outputFilename, ImageFormat.Png);
            }
        }

        public void CreateChkn(string folder, string outputFilename, Template template, IEnumerable<string> filenames)
        {
            var indexFilename = Path.Combine(folder, "index.xml");
            if (File.Exists(indexFilename)) File.Delete(indexFilename);
            File.WriteAllText(indexFilename, template.GetIndexXml());

            var jsonFile = Path.Combine(folder, "imvu-internal.json");
            if (File.Exists(jsonFile)) File.Delete(jsonFile);
            File.WriteAllText(jsonFile, "{}");

            var chkn = Path.Combine(folder, $"{outputFilename}");
            if (File.Exists(chkn)) File.Delete(chkn);
            var fz = new FastZip();
            fz.CompressionLevel = ICSharpCode.SharpZipLib.Zip.Compression.Deflater.CompressionLevel.BEST_COMPRESSION;

            if (filenames == null)
            {
                fz.CreateZip(chkn, folder, false, null);
                return;
            }
            var ff = new FilenameFilter(filenames);
            fz.CreateZip(chkn, folder, false, ff, null);
        }


        private string _botPath;

        private void SplicerControl_Load(object sender, EventArgs e)
        {
            cboAudioLength.SelectedIndex = 0;
            var docsPath = Shared.TriggerbotDocsPath;
            cboAudioLength.SelectedIndex = 1;
            var amtProfit = Shared.Paid ? 0 : 220;
            lblProfit.Text = $"Please note that @Triggers will make {amtProfit} cr profit for every CHKN you submit using this tool.";
            chkCheap.Visible = Shared.Paid;
        }

        private const double CROP_STEP = 25;

        private void IncreaseVolume(object sender, EventArgs e)
        {
            WaveformRenderNewVolume(_volume + CROP_STEP);
        }

        private void DecreaseVolume(object sender, EventArgs e)
        {
            if (_volume > CROP_STEP)
            {
                WaveformRenderNewVolume(_volume - CROP_STEP);
            }
        }

        private void ResetVolume(object sender, EventArgs e)
        {
            picWaveform.Image = _waveform;
            _volume = 100;
            UpdateVolume();
        }

        private void UpdateVolume()
        {
            lblVolume.Text = $"Volume: {_volume}%";
        }

        private void WaveformRenderNewVolume(double newVolume)
        {
            string tempPath = Path.GetTempPath();

            string filename = Path.Combine(tempPath, "original-image.png");
            if (File.Exists(filename)) { File.Delete(filename); }
            _waveform.Save(filename, ImageFormat.Png);

            double volumeChange = newVolume - INIT_VOLUME;
            double stretchRatio = 1 + volumeChange / INIT_VOLUME; // Calculate the stretch ratio

            float newHeight = (float)(_waveform.Height * stretchRatio);
            float cropAmount = (newHeight - _waveform.Height);

            // Create a new Bitmap object with the stretched size
            var stretchedImage = new Bitmap(_waveform.Width, (int)newHeight);

            // Create a Graphics object from the stretched image
            using (Graphics g = Graphics.FromImage(stretchedImage))
            {
                // Set the interpolation mode to achieve a smoother stretch
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;

                // Draw the stretched image
                g.DrawImage(_waveform, new RectangleF(0, 0, _waveform.Width, newHeight));
            }

            filename = Path.Combine(tempPath, "stretched-image.png");
            if (File.Exists(filename)) { File.Delete(filename); }
            stretchedImage.Save(filename, ImageFormat.Png);

            // Create a new Bitmap object with the desired size
            Bitmap croppedImage = CropBitmap(stretchedImage, _waveform.Height);

            filename = Path.Combine(tempPath, "cropped-image.png");
            if (File.Exists(filename)) { File.Delete(filename); }
            croppedImage.Save(filename, ImageFormat.Png);

            // Assign the cropped image to picWaveform.Image
            picWaveform.Image = croppedImage;

            // Update the volume
            _volume = newVolume;
            UpdateVolume();
        }

        private Bitmap CropBitmap(Bitmap source, float cropHeight)
        {
            float sourceWidth = source.Width;
            float sourceHeight = source.Height;

            // Calculate the crop position and size
            float cropTop = (sourceHeight - cropHeight) / 2;
            RectangleF cropRect = new RectangleF(0, cropTop, sourceWidth, cropHeight);

            // Create a new Bitmap object with the desired size
            Bitmap destination = new Bitmap((int)sourceWidth, (int)cropHeight);

            // Create a Graphics object from the destination Bitmap
            using (Graphics g = Graphics.FromImage(destination))
            {
                // Draw the cropped portion onto the new image
                g.DrawImage(source, new RectangleF(0, 0, sourceWidth, cropHeight), cropRect, GraphicsUnit.Pixel);
            }

            return destination;
        }

        private void SplicerControl_ControlRemoved(object sender, ControlEventArgs e)
        {
            _mp3FileReader?.Dispose();
            _mp3FileReader = null;
        }

        private void DebugRun(object sender, EventArgs e)
        {
            var playbackForm = new PlaybackForm();
            playbackForm.Mp3FileReader = _mp3FileReader;
            playbackForm.Owner = this.ParentForm;
            playbackForm.StartPosition = FormStartPosition.CenterParent;
            playbackForm.ShowDialog(this);
            playbackForm.Dispose();

        }

        internal void ShowCheap()
        {
            chkCheap.Visible = true;
        }

        private void chkCheap_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void txtFilename_TextChanged(object sender, EventArgs e)
        {

        }

        private void rdoMinima_CheckedChanged(object sender, EventArgs e)
        {

        }
    }

}
