using NAudio.Wave;
using NAudio.WaveFormRenderer;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Triggerless.TriggerBot
{
    public partial class PlaybackForm : Form
    {
        public Mp3FileReader Mp3FileReader { get; set; } = null;
        private bool _isRendering;
        public PlaybackForm()
        {
            InitializeComponent();
        }

        private Dictionary<int, int> _trackBarValues = new Dictionary<int, int>
        {
            {0, 1}, {1, 2}, {2, 5}, {3, 10}, {4, 20}, {5, 30}, {6, 60}
        };

        private void RenderTest(double secondsPerViewport)
        {
            if (_isRendering) { return; }
            _isRendering = true;
            picWaveform.Image?.Dispose();
            int zoom = _trackBarValues[comboBox1.SelectedIndex];


            Mp3FileReader.Position = 0;
            double totalSeconds = Mp3FileReader.TotalTime.TotalSeconds;
            double numberOfViewports = totalSeconds / secondsPerViewport;
            double viewportSizeInPixels = 1200;
            double totalHorizontalPixels = numberOfViewports * viewportSizeInPixels;

            pnlWave.Width = (int)Math.Round(totalHorizontalPixels);
            picWaveform.Location = new Point(0, 0);

            Bitmap bmp = new Bitmap(pnlWave.Width, pnlWave.Height);
            double pixelsPerSecond = pnlWave.Width / totalSeconds;
            var g = Graphics.FromImage(bmp);

            for (int iSecondPixel = 0; iSecondPixel < pnlWave.Width; iSecondPixel += (int)pixelsPerSecond)
            {
                g.DrawLine(Pens.Black, iSecondPixel, 0, iSecondPixel, pnlWave.Height);
                for (int iTenth = 1; iTenth < 10; iTenth++)
                {
                    //g.DrawLine(Pens.Gray, iSecondPixel + iTenth/iSecondPixel, 0, iSecondPixel + iTenth/iSecondPixel, pnlWave.Height);
                }
            }

            picWaveform.Size = new Size(pnlWave.Width, pnlWave.Height);
            g.DrawImage(WaveformCreate(), 0, 0);

            int iSec = 0;
            var font = new Font("Arial", 14, FontStyle.Bold, GraphicsUnit.Pixel);
            int modValue = zoom > 15 ? 5 : 1;
            for (int iSecondPixel = 0; iSecondPixel < pnlWave.Width; iSecondPixel += (int)pixelsPerSecond)
            {
                var timeStr = new TimeSpan(0, iSec / 60, iSec % 60).ToString("mm':'ss");
                if (iSec++ % modValue != 0) continue;
                var fontSize = g.MeasureString(timeStr, font);
                g.FillRectangle(Brushes.White, iSecondPixel - fontSize.Width / 2 + 1, pnlWave.Size.Height - fontSize.Height, fontSize.Width, fontSize.Height);
                g.DrawString(timeStr, font, Brushes.Black, new PointF(iSecondPixel - fontSize.Width / 2 + 1, pnlWave.Size.Height - fontSize.Height));
            }
            font.Dispose();

            picWaveform.Image = bmp;
            _isRendering = false;

        }

        private System.Drawing.Image WaveformCreate()
        {
            //var maxPeakProvider = new MaxPeakProvider();
            //var rmsPeakProvider = new RmsPeakProvider(200);
            //var samplingPeakProvider = new SamplingPeakProvider(200);
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
            return renderer.Render(Mp3FileReader, averagePeakProvider, renderSettings);
        }


        private void PlaybackForm_Load(object sender, EventArgs e)
        {
            if (Mp3FileReader is null)
            {
                Close();
                return;
            }

            comboBox1.SelectedIndex = 4;
            RenderTest(_trackBarValues[comboBox1.SelectedIndex]);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int zoom = _trackBarValues[comboBox1.SelectedIndex];
            //lblZoom.Text = $"{zoom} sec";
            RenderTest(zoom);
        }
    }
}
