// ============================================================
// File: WaveformEditorControl.cs
// ============================================================

using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Triggerless.TriggerBot
{
    public partial class WaveformEditorControl : UserControl
    {
        // =========================================================
        // CONSTANTS
        // =========================================================

        private const int TimelineHeight = 24;
        private const int ScrollResolution = 100000;
        private const int MarkerHitRadiusPx = 6;
        private const int MarkerTriangleWidth = 5;
        private const int MarkerTriangleHeight = 6;
        private const int MarkerInvalidatePadding = 10;

        // =========================================================
        // AUDIO DATA
        // =========================================================

        private readonly List<WaveformPeak> _waveformPeaks = 
            new List<WaveformPeak>();
        private string _loadedFile;
        private double _audioLengthSeconds;

        // =========================================================
        // VIEWPORT
        // =========================================================

        private double _viewportStartSeconds = 0;
        private double _viewportDurationSeconds = 20;
        private double _minZoomSeconds = 0.5;
        private double _maxZoomSeconds = 20;

        // =========================================================
        // MARKERS
        // =========================================================

        private readonly List<CutMarker> _cutMarkers =
            new List<CutMarker>();

        private CutMarker _draggingMarker = null;

        // =========================================================
        // BITMAP CACHE
        // =========================================================

        private Bitmap _fullWaveformBitmap;

        // =========================================================
        // COLORS
        // =========================================================

        private readonly Color _backgroundColor = Color.FromArgb(230, 230, 230);
        private readonly Color _waveformColor = Color.FromArgb(0, 0, 204);
        private readonly Color _baselineColor = Color.FromArgb(120, 120, 180);

        // =========================================================
        // CONSTRUCTOR
        // =========================================================

        public WaveformEditorControl()
        {
            InitializeComponent();

            SetStyle(
                ControlStyles.AllPaintingInWmPaint
                | ControlStyles.UserPaint
                | ControlStyles.OptimizedDoubleBuffer
                | ControlStyles.ResizeRedraw,
                true);

            DoubleBuffered = true;

            viewportPanel.Paint += ViewportPanel_Paint;
            viewportPanel.Resize += ViewportPanel_Resize;

            viewportPanel.MouseDown += ViewportPanel_MouseDown;
            viewportPanel.MouseMove += ViewportPanel_MouseMove;
            viewportPanel.MouseUp += ViewportPanel_MouseUp;
            viewportPanel.MouseDoubleClick += ViewportPanel_MouseDoubleClick;

            hScrollBar.Scroll += HScrollBar_Scroll;

            btnZoomIn.Click += BtnZoomIn_Click;
            btnZoomOut.Click += BtnZoomOut_Click;
            btnNext20.Click += BtnNext20_Click;

            viewportPanel.MouseWheel += ViewportPanel_MouseWheel;
            viewportPanel.MouseEnter += ViewportPanel_MouseEnter;
            this.TabStop = true;

        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            double centerTime = _viewportStartSeconds
                + (_viewportDurationSeconds * 0.5);

            // =====================================================
            // CTRL + PLUS
            // =====================================================

            if (keyData == (Keys.Control | Keys.Oemplus)
                || keyData == (Keys.Control | Keys.Add))
            {
                ZoomViewport(
                    0.5,
                    centerTime);

                return true;
            }

            // =====================================================
            // CTRL + MINUS
            // =====================================================

            if (keyData == (Keys.Control | Keys.OemMinus)
                || keyData == (Keys.Control | Keys.Subtract))
            {
                ZoomViewport(2.0, centerTime);
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void ViewportPanel_MouseWheel(object sender, MouseEventArgs e)
        {
            double factor = e.Delta > 0 ? 0.5 : 2.0;
            double mouseTime = XToTime(e.X);
            ZoomViewport(factor, mouseTime);
        }
        private void ViewportPanel_MouseEnter(object sender, EventArgs e)
        {
            viewportPanel.Focus();
        }

        private void BtnNext20_Click(object sender, EventArgs e)
        {
            double start = 0;

            if (_cutMarkers.Count > 0)
            {
                start = _cutMarkers[_cutMarkers.Count - 1].TimeSeconds;
            }

            _viewportStartSeconds = ClampTime(start);
            _viewportDurationSeconds = Math.Min(20, _audioLengthSeconds);
            ClampViewport();
            UpdateScrollbar();
            viewportPanel.Invalidate();
        }
        // =========================================================
        // PUBLIC
        // =========================================================

        public void LoadAudio(string filePath)
        {
            _loadedFile = filePath;

            _waveformPeaks.Clear();

            using (WaveStream reader = UniversalAudioReader.Open(filePath))
            {
                _audioLengthSeconds = reader.TotalTime.TotalSeconds;
                _maxZoomSeconds = _audioLengthSeconds;
                GenerateWaveformData(reader);
            }

            _viewportStartSeconds = 0;

            _viewportDurationSeconds = Math.Min(20, _audioLengthSeconds);

            ClampViewport();

            UpdateScrollbar();

            RegenerateWaveformBitmap();

            viewportPanel.Invalidate();
        }

        // =========================================================
        // WAVEFORM GENERATION
        // =========================================================

        private void GenerateWaveformData(WaveStream reader)
        {
            ISampleProvider sampleProvider = reader.ToSampleProvider();

            int samplesPerPeak = 1024;

            float[] buffer = new float[samplesPerPeak];

            while (true)
            {
                int read = sampleProvider.Read(
                    buffer, 0, buffer.Length
                );

                if (read == 0) break;

                float min = 0;
                float max = 0;

                for (int i = 0; i < read; i++)
                {
                    float sample = buffer[i];

                    if (sample < min) min = sample;
                    if (sample > max) max = sample;
                }

                _waveformPeaks.Add(new WaveformPeak{Min = min, Max = max});
            }
        }

        // =========================================================
        // BITMAP CACHE
        // =========================================================

        private void RegenerateWaveformBitmap()
        {
            if (_waveformPeaks.Count == 0) return;

            _fullWaveformBitmap?.Dispose();

            Rectangle waveformRect = GetWaveformRect();

            if (waveformRect.Width <= 1 || waveformRect.Height <= 1)
            {
                return;
            }

            int bitmapWidth =
                Math.Max(
                    waveformRect.Width,
                    (int)(_audioLengthSeconds * 100));

            int bitmapHeight = waveformRect.Height;

            _fullWaveformBitmap =
                new Bitmap(
                    bitmapWidth,
                    bitmapHeight);

            using (Graphics g = Graphics.FromImage(_fullWaveformBitmap))
            {
                g.SmoothingMode = SmoothingMode.None;
                g.PixelOffsetMode = PixelOffsetMode.Half;
                g.Clear(_backgroundColor);

                DrawWaveformBitmap(g, bitmapWidth, bitmapHeight);
            }
        }

        private void DrawWaveformBitmap(Graphics g, int width, int height)
        {
            if (_waveformPeaks.Count == 0) return;
            if (width <= 1 || height <= 1) return;
            int centerY = height / 2;

            // =====================================================
            // BASELINE
            // =====================================================

            using (Pen baselinePen = new Pen(_baselineColor))
            {
                g.DrawLine(
                    baselinePen,
                    0,
                    centerY,
                    width - 1,
                    centerY);
            }

            // =====================================================
            // WAVEFORM
            // =====================================================

            using (Pen pen = new Pen(_waveformColor))
            {
                for (int x = 0; x < width; x++)
                {
                    double normalizedX = x / (double)(width - 1);

                    double time = normalizedX * _audioLengthSeconds;

                    int peakIndex =
                        (int)(
                            (time / _audioLengthSeconds)
                            * (_waveformPeaks.Count - 1));

                    if (peakIndex < 0) peakIndex = 0;

                    if (peakIndex >= _waveformPeaks.Count)
                        peakIndex = _waveformPeaks.Count - 1;

                    WaveformPeak peak = _waveformPeaks[peakIndex];

                    int y1 =
                        centerY
                        - (int)(
                            peak.Max
                            * height
                            * 0.45f);

                    int y2 =
                        centerY
                        - (int)(
                            peak.Min
                            * height
                            * 0.45f);

                    if (y1 == y2)
                    {
                        y2 = y1 + 1;
                    }

                    g.DrawLine(
                        pen,
                        x,
                        y1,
                        x,
                        y2);
                }
            }
        }

        // =========================================================
        // PAINTING
        // =========================================================

        private void ViewportPanel_Paint(
            object sender,
            PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            g.SmoothingMode = SmoothingMode.None;

            g.InterpolationMode = InterpolationMode.NearestNeighbor;

            g.PixelOffsetMode = PixelOffsetMode.Half;

            g.Clear(_backgroundColor);

            DrawTimeline(g);

            DrawVisibleWaveform(g);

            DrawCutMarkers(g);
        }

        private void DrawVisibleWaveform(Graphics g)
        {
            if (_fullWaveformBitmap == null)
                return;

            Rectangle waveformRect = GetWaveformRect();

            double startPercent =
                _viewportStartSeconds
                / _audioLengthSeconds;

            double endPercent =
                (_viewportStartSeconds
                + _viewportDurationSeconds)
                / _audioLengthSeconds;

            int srcX =
                (int)(
                    startPercent
                    * _fullWaveformBitmap.Width);

            int srcWidth =
                (int)(
                    (endPercent - startPercent)
                    * _fullWaveformBitmap.Width);

            if (srcWidth <= 0) return;

            Rectangle srcRect =
                new Rectangle(
                    srcX,
                    0,
                    srcWidth,
                    _fullWaveformBitmap.Height);

            g.DrawImage(
                _fullWaveformBitmap,
                waveformRect,
                srcRect,
                GraphicsUnit.Pixel);
        }

        private void DrawCutMarkers(Graphics g)
        {
            using (Pen pen = new Pen(Color.Yellow, 1))
            using (SolidBrush brush = new SolidBrush(Color.Yellow))
            {
                foreach (CutMarker marker in _cutMarkers)
                {
                    DrawSingleCutMarker(g, pen, brush, marker);
                }
            }
        }

        private void DrawSingleCutMarker(Graphics g, Pen pen, Brush brush, CutMarker marker)
        {
            int x = TimeToX(marker.TimeSeconds);

            g.DrawLine(pen, x, 0, x, viewportPanel.Height);

            Point[] tri =
            {
                new Point(x - MarkerTriangleWidth, 0),
                new Point(x + MarkerTriangleWidth, 0),
                new Point(x, MarkerTriangleHeight)
            };

            g.FillPolygon(brush, tri);
        }

        private void DrawTimeline(Graphics g)
        {
            using (Pen pen = new Pen(Color.Black))
            using (SolidBrush brush = new SolidBrush(Color.Black))
            {
                double tickSpacing = CalculateTickSpacing();

                double startTick =
                    Math.Floor(
                        _viewportStartSeconds
                        / tickSpacing)
                    * tickSpacing;

                for (double t = startTick;
                    t <= _viewportStartSeconds + _viewportDurationSeconds;
                    t += tickSpacing)
                {
                    int x = TimeToX(t);

                    g.DrawLine(pen, x, 0, x, 8);

                    string label = FormatTime(t);

                    g.DrawString(label, Font, brush, x + 2, 8);
                }
            }
        }

        // =========================================================
        // MARKER INTERACTION
        // =========================================================

        private void ViewportPanel_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;

            CutMarker nearest = FindMarkerAtPixel(e.X);

            if (nearest != null)
            {
                _draggingMarker = nearest;
            }
        }

        private void ViewportPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (_draggingMarker == null) return;

            int oldX = TimeToX(_draggingMarker.TimeSeconds);

            _draggingMarker.TimeSeconds = ClampTime(XToTime(e.X));

            int newX = TimeToX(_draggingMarker.TimeSeconds);

            Rectangle dirty =
                Rectangle.Union(
                    GetMarkerInvalidateRect(oldX),
                    GetMarkerInvalidateRect(newX));

            viewportPanel.Invalidate(dirty);
        }

        private void ViewportPanel_MouseUp(object sender, MouseEventArgs e)
        {
            if (_draggingMarker != null)
            {
                _draggingMarker = null;

                _cutMarkers.Sort(
                    delegate (
                        CutMarker a,
                        CutMarker b)
                    {
                        return
                            a.TimeSeconds.CompareTo(
                                b.TimeSeconds);
                    });

                viewportPanel.Invalidate();
            }
        }

        private void ViewportPanel_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;

            CutMarker nearest = FindMarkerAtPixel(e.X);

            if (nearest != null)
            {
                _cutMarkers.Remove(nearest);
            }
            else
            {
                _cutMarkers.Add(
                    new CutMarker
                    {
                        TimeSeconds = ClampTime(XToTime(e.X))
                    });
            }

            viewportPanel.Invalidate();
        }

        private CutMarker FindMarkerAtPixel(int x)
        {
            CutMarker closest = null;

            int closestDist = MarkerHitRadiusPx;

            foreach (CutMarker marker in _cutMarkers)
            {
                int mx = TimeToX(marker.TimeSeconds);

                int dist = Math.Abs(mx - x);

                if (dist <= closestDist)
                {
                    closest = marker;
                    closestDist = dist;
                }
            }

            return closest;
        }

        // =========================================================
        // SCROLLING
        // =========================================================

        private void UpdateScrollbar()
        {
            double maxStart =
                Math.Max(
                    0,
                    _audioLengthSeconds
                    - _viewportDurationSeconds);

            hScrollBar.Minimum = 0;

            int largeChange =
                (int)(
                    (_viewportDurationSeconds
                    / _audioLengthSeconds)
                    * ScrollResolution);

            if (largeChange < 1)
                largeChange = 1;

            hScrollBar.LargeChange =
                largeChange;

            hScrollBar.SmallChange =
                Math.Max(
                    1,
                    largeChange / 10);

            hScrollBar.Maximum =
                ScrollResolution
                + largeChange
                - 1;

            int maxScrollbarValue =
                hScrollBar.Maximum
                - hScrollBar.LargeChange
                + 1;

            int value = 0;

            if (maxStart > 0)
            {
                double percent = _viewportStartSeconds / maxStart;

                value = (int)(percent * maxScrollbarValue);
            }

            value =
                Math.Max(
                    hScrollBar.Minimum,
                    Math.Min(
                        maxScrollbarValue,
                        value));

            hScrollBar.Value = value;
        }

        private void HScrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            double maxStart =
                Math.Max(
                    0,
                    _audioLengthSeconds
                    - _viewportDurationSeconds);

            int maxScrollbarValue =
                hScrollBar.Maximum
                - hScrollBar.LargeChange
                + 1;

            if (maxScrollbarValue <= 0)
            {
                _viewportStartSeconds = 0;
            }
            else
            {
                double percent = e.NewValue / (double)maxScrollbarValue;

                _viewportStartSeconds = percent * maxStart;
            }

            ClampViewport();

            viewportPanel.Invalidate();
        }

        // =========================================================
        // ZOOM
        // =========================================================

        private void BtnZoomIn_Click(
            object sender,
            EventArgs e)
        {
            ZoomViewport(0.5, _viewportStartSeconds + (_viewportDurationSeconds * 0.5));
        }

        private void BtnZoomOut_Click(
            object sender,
            EventArgs e)
        {
            ZoomViewport(2.0, _viewportStartSeconds + (_viewportDurationSeconds * 0.5));
        }

        private void ZoomViewport(double factor, double anchorTime)
        {
            double newDuration = _viewportDurationSeconds * factor;

            if (newDuration < _minZoomSeconds)
            {
                newDuration = _minZoomSeconds;
            }

            if (newDuration > _maxZoomSeconds)
            {
                newDuration = _maxZoomSeconds;
            }

            double anchorPercent = (anchorTime - _viewportStartSeconds) / _viewportDurationSeconds;

            _viewportStartSeconds = anchorTime - (anchorPercent * newDuration);

            _viewportDurationSeconds = newDuration;

            ClampViewport();

            UpdateScrollbar();

            viewportPanel.Invalidate();
        }
        // =========================================================
        // TIMELINE MATH
        // =========================================================

        private Rectangle GetWaveformRect()
        {
            return new Rectangle(
                0,
                TimelineHeight,
                viewportPanel.Width,
                viewportPanel.Height
                    - TimelineHeight);
        }

        private int TimeToX(double seconds)
        {
            double relative =
                (seconds
                - _viewportStartSeconds)
                / _viewportDurationSeconds;

            return (int)(
                relative
                * viewportPanel.Width);
        }

        private double XToTime(int x)
        {
            if (viewportPanel.Width <= 1)
                return _viewportStartSeconds;

            double normalized =
                x / (double)(
                    viewportPanel.Width - 1);

            return
                _viewportStartSeconds
                + (normalized
                * _viewportDurationSeconds);
        }

        private double ClampTime(double time)
        {
            if (time < 0) return 0;

            if (time > _audioLengthSeconds) return _audioLengthSeconds;

            return time;
        }

        private void ClampViewport()
        {
            if (_viewportStartSeconds < 0)
            {
                _viewportStartSeconds = 0;
            }

            double maxStart =
                Math.Max(
                    0,
                    _audioLengthSeconds
                    - _viewportDurationSeconds);

            if (_viewportStartSeconds > maxStart)
            {
                _viewportStartSeconds =
                    maxStart;
            }
        }

        private Rectangle GetMarkerInvalidateRect(int x)
        {
            return new Rectangle(
                x - MarkerInvalidatePadding,
                0,
                MarkerInvalidatePadding * 2,
                viewportPanel.Height);
        }

        // =========================================================
        // TIMELINE LABELS
        // =========================================================

        private double CalculateTickSpacing()
        {
            if (_viewportDurationSeconds <= 2) return 0.1;

            if (_viewportDurationSeconds <= 5) return 0.5;

            if (_viewportDurationSeconds <= 20) return 1;

            if (_viewportDurationSeconds <= 60) return 5;

            return 10;
        }

        private string FormatTime(double seconds)
        {
            // =====================================================
            // HIGH ZOOM:
            // show m:ss.t
            // =====================================================

            if (_viewportDurationSeconds <= 10)
            {
                TimeSpan ts = TimeSpan.FromSeconds(seconds);

                return $"{(int)ts.TotalMinutes}:{ts.Seconds:00}.{ts.Milliseconds / 100}";
            }

            // =====================================================
            // NORMAL ZOOM:
            // show m:ss
            // =====================================================

            TimeSpan normalTs = TimeSpan.FromSeconds(seconds);

            return $"{(int)normalTs.TotalMinutes}:{normalTs.Seconds:00}";
        }
        // =========================================================
        // RESIZE
        // =========================================================

        private void ViewportPanel_Resize(object sender, EventArgs e)
        {
            RegenerateWaveformBitmap();
            viewportPanel.Invalidate();
        }
    }

    // =========================================================
    // WAVEFORM PEAK
    // =========================================================

    public struct WaveformPeak
    {
        public float Min;

        public float Max;
    }

    // =========================================================
    // CUT MARKER
    // =========================================================

    public class CutMarker
    {
        public double TimeSeconds {get; set;}
    }
}