// ============================================================
// File: ICutControl.cs
// ============================================================

using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Triggerless.TriggerBot
{
    public partial class ICutControl : UserControl
    {
        public event CutsChangedEventHandler CutsChanged;
        private void FireCutsChanged()
        {
            CutsChanged?.Invoke(this, new CutsChangedEventArgs(GetCuts()));
        }
        // =========================================================
        // CONSTANTS
        // =========================================================

        private const int TIMELINE_HEIGHT = 24;
        private const int SCROLL_RESOLUTION = 100000;
        private const int MARKER_HIT_RADIUS_PX = 6;
        private const int MARKER_TRIANGLE_WIDTH = 5;
        private const int MARKER_TRIANGLE_HEIGHT = 6;
        private const int MARKER_INVALIDATE_PADDING = 10;
        private const int MARKER_DASH_LENGTH = 6;

        private const double BEAT_DETECTION_THRESHOLD = 0.008;
        private const double MIN_BEAT_SPACING_SEC = 0.12;
        private const int BEAT_SAMPLES_PER_WINDOW = 1024;

        private const int PLAYHEAD_DASH_LENGTH = 2;
        private Bitmap _viewportBitmap;
        private bool _viewportBitmapDirty = true;

        // =========================================================
        // AUDIO DATA
        // =========================================================

        private readonly List<WaveformPeak> _waveformPeaks = new List<WaveformPeak>();
        private string _loadedFile;
        private double _audioLengthSeconds;
        private readonly List<double> _beatTimes = new List<double>();

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

        private readonly List<CutMarker> _cutMarkers = new List<CutMarker>();
        private CutMarker _draggingMarker = null;

        // =========================================================
        // UNDO STACK
        // =========================================================

        private readonly Stack<ICutState> _undoStack = new Stack<ICutState>();
        private bool _suppressUndoPush = false;

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
        // PLAYBACK
        // =========================================================

        private bool _playheadSetByUser;
        private readonly Timer _playbackTimer = new Timer();
        private bool _isPlaying;
        private double _playheadTimeSeconds;
        private double _playbackStartTimeSeconds;
        private double _playbackEndTimeSeconds;
        private int _previousPlayheadX = -1;
        private IWavePlayer _waveOut;
        private WaveStream _playbackReader;

        private readonly HashSet<double> _playedSnipMarkers = new HashSet<double>();
        private const double SnipLeadTimeSeconds = 0.020;

        public ICutControl()
        {
            InitializeComponent();

            SetStyle(
                ControlStyles.AllPaintingInWmPaint
                | ControlStyles.OptimizedDoubleBuffer
                | ControlStyles.ResizeRedraw,
                true);

            DoubleBuffered = true;

            hScrollBar.Scroll += HScrollBar_Scroll;

            btnZoomIn.Click += BtnZoomIn_Click;
            btnZoomOut.Click += BtnZoomOut_Click;
            btnNext20.Click += BtnNext20_Click;

            // This event handler isn't shown in the designer, strangely.
            viewportPanel.MouseWheel += ViewportPanel_MouseWheel;

            this.TabStop = true;

            _playbackTimer.Interval = 8;
            _playbackTimer.Tick += PlaybackTimer_Tick;
            btnPlayheadTime.Text = "00:00.00";
            btnPlayheadTime.BringToFront();
            InitializeSnipSound();
        }

        private WaveFileReader _snipReader;
        private WaveOutEvent _snipWaveOut;

        private void InitializeSnipSound()
        {
            Stream resourceStream = Properties.Resources.snip;

            _snipReader = new WaveFileReader(resourceStream);

            _snipWaveOut = new WaveOutEvent();

            _snipWaveOut.Init(_snipReader);
        }

        private void CheckForUpcomingCutMarkers()
        {
            double triggerTime =
                _playheadTimeSeconds + SnipLeadTimeSeconds;

            foreach (CutMarker marker in _cutMarkers)
            {
                // only markers visible in viewport
                if (marker.TimeSeconds < _viewportStartSeconds ||
                    marker.TimeSeconds >
                        (_viewportStartSeconds +
                         _viewportDurationSeconds))
                {
                    continue;
                }

                // already triggered
                if (_playedSnipMarkers.Contains(
                    marker.TimeSeconds))
                {
                    continue;
                }

                // play slightly before marker
                if (triggerTime >= marker.TimeSeconds)
                {
                    PlaySnip();

                    _playedSnipMarkers.Add(
                        marker.TimeSeconds);
                }
            }
        }

        private void PlaySnip()
        {
            if (_snipReader == null || _snipWaveOut == null) return; 
            _snipWaveOut.Stop();
            _snipReader.Position = 0;
            _snipWaveOut.Play();
        }

        private void PushUndoState()
        {
            if (_suppressUndoPush)
            {
                return;
            }

            ICutState state = new ICutState
            {
                ViewportStartSeconds = _viewportStartSeconds,
                ViewportDurationSeconds = _viewportDurationSeconds,
                CutMarkers = CloneMarkers(_cutMarkers)
            };

            _undoStack.Push(state);
        }

        private void RestoreState(ICutState state)
        {
            _suppressUndoPush = true;

            _viewportStartSeconds = state.ViewportStartSeconds;

            _viewportDurationSeconds = state.ViewportDurationSeconds;

            _cutMarkers.Clear();

            _cutMarkers.AddRange(CloneMarkers(state.CutMarkers));

            FireCutsChanged();

            ClampViewport();

            UpdateScrollbar();

            _viewportBitmapDirty = true;

            viewportPanel.Invalidate();

            _suppressUndoPush = false;
        }

        private void Undo()
        {
            if (_undoStack.Count == 0) return;

            ICutState state = _undoStack.Pop();
            RestoreState(state);
        }

        private List<CutMarker> CloneMarkers(List<CutMarker> source)
        {
            List<CutMarker> result = new List<CutMarker>();
            foreach (CutMarker marker in source)
            {
                result.Add(new CutMarker { TimeSeconds = marker.TimeSeconds });
            }

            return result;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            double centerTime = _viewportStartSeconds
                + (_viewportDurationSeconds * 0.5);

            // =====================================================
            // CTRL + Z
            // =====================================================

            if (keyData == (Keys.Control | Keys.Z))
            {
                Undo();
                return true;
            }

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
                PushUndoState();
                start = _cutMarkers[_cutMarkers.Count - 1].TimeSeconds;
            }

            _viewportStartSeconds = ClampTime(start);
            _viewportDurationSeconds = Math.Min(20, _audioLengthSeconds);
            ResetPlayhead();
            ClampViewport();
            UpdateScrollbar();
            _viewportBitmapDirty = true;
            viewportPanel.Invalidate();
        }

        private void GenerateBeatMarkers(WaveStream reader)
        {
            _beatTimes.Clear();

            var sampleProvider = reader.ToSampleProvider();
            int sampleRate = sampleProvider.WaveFormat.SampleRate;

            const double windowMs = 20.0;
            int windowSamples = (int)(sampleRate * windowMs / 1000.0);

            float[] buffer = new float[windowSamples];

            long totalSamples = 0;

            double previousEnergy = 0;

            List<double> onsetTimes = new List<double>();

            double timeSeconds = 0;

            // -----------------------------
            // PASS 1: ONSET DETECTION ONLY
            // -----------------------------
            while (true)
            {
                int read = sampleProvider.Read(buffer, 0, buffer.Length);
                if (read == 0)
                    break;

                double energy = 0;

                for (int i = 0; i < read; i++)
                    energy += buffer[i] * buffer[i];

                energy /= read;
                energy = Math.Sqrt(energy);

                timeSeconds = totalSamples / (double)sampleRate;

                bool rising = energy > previousEnergy * 1.2;
                bool above = energy > BEAT_DETECTION_THRESHOLD;

                if (rising && above)
                {
                    onsetTimes.Add(timeSeconds);
                }

                previousEnergy = energy;
                totalSamples += read;
            }

            // -----------------------------
            // PASS 2: FIXED GRID (NO AUDIO DEPENDENCE)
            // -----------------------------
            double bpm = 120.0; // TEMPORARY: lock for stability test
            double beatInterval = 60.0 / bpm;

            double duration = reader.TotalTime.TotalSeconds;

            for (double t = 0; t < duration; t += beatInterval)
            {
                _beatTimes.Add(t);
            }

            // -----------------------------
            // OPTIONAL: store onsets separately
            // -----------------------------
            // _onsetTimes = onsetTimes;
        }

        public void LoadAudio(string filePath)
        {
            _loadedFile = filePath;

            _waveformPeaks.Clear();
            _beatTimes.Clear();
            _cutMarkers.Clear();

            using (WaveStream reader = UniversalAudioReader.Open(filePath))
            {
                _audioLengthSeconds = reader.TotalTime.TotalSeconds;
                _maxZoomSeconds = _audioLengthSeconds;

                GenerateWaveformData(reader);
            }

            using (WaveStream reader = UniversalAudioReader.Open(filePath))
            {
                GenerateBeatMarkers(reader);
            }

            _viewportStartSeconds = 0;

            _viewportDurationSeconds =
                Math.Min(20, _audioLengthSeconds);

            ClampViewport();

            UpdateScrollbar();

            RegenerateWaveformBitmap();
            _viewportBitmapDirty = true;
            viewportPanel.Invalidate();

            _undoStack.Clear();
            PushUndoState();

            // get any existing cut markers from a database.
            var cutTimes = SQLiteDataAccess.GetCutMarkers(_loadedFile);
            if (cutTimes != null && cutTimes.Any())            
            {
                _cutMarkers.Clear();
                _cutMarkers.AddRange(cutTimes.Select(t => new CutMarker { TimeSeconds = t }));
            }

            FireCutsChanged();
        }

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

                _waveformPeaks.Add(new WaveformPeak { Min = min, Max = max });
            }
        }

        private void RegenerateWaveformBitmap()
        {
            if (_waveformPeaks.Count == 0) return;

            _fullWaveformBitmap?.Dispose();
            _viewportBitmap?.Dispose();
            _viewportBitmap = null;

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
                //g.Clear(_backgroundColor);

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

                    int y1 = centerY - (int)(peak.Max * height * 0.45f);

                    int y2 = centerY - (int)(peak.Min * height * 0.45f);

                    if (y1 == y2)
                    {
                        y2 = y1 + 1;
                    }

                    g.DrawLine(pen, x, y1, x, y2);
                }
            }
        }

        private void RegenerateViewportBitmap()
        {
            if (viewportPanel.Width <= 0 ||
                viewportPanel.Height <= 0)
            {
                return;
            }

            if (_viewportBitmap != null)
            {
                _viewportBitmap.Dispose();
                _viewportBitmap = null;
            }

            _viewportBitmap = new Bitmap(
                viewportPanel.Width,
                viewportPanel.Height);

            using (Graphics g =
                Graphics.FromImage(_viewportBitmap))
            {
                //g.Clear(_backgroundColor);

                DrawTimeline(g);

                DrawVisibleWaveform(g);

                DrawBeatMarkers(g);

                DrawCutMarkers(g);
            }

            _viewportBitmapDirty = false;
        }

        private void ViewportPanel_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            if (_viewportBitmapDirty ||
                _viewportBitmap == null)
            {
                RegenerateViewportBitmap();
            }

            if (_viewportBitmap != null)
            {
                g.DrawImageUnscaled(_viewportBitmap, 0, 0);
            }

            DrawPlayHead(g);
        }

        private void DrawPlayHead(Graphics g)
        {
            if (!_playheadSetByUser && !_isPlaying)
            {
                return;
            }

            int x = TimeToX(_playheadTimeSeconds);

            bool firstColor = true;

            for (int y = 0; y < viewportPanel.Height; y += PLAYHEAD_DASH_LENGTH)
            {
                Pen pen = firstColor ? Pens.Magenta : Pens.Black;

                int y2 =
                    Math.Min(
                        y + PLAYHEAD_DASH_LENGTH - 1,
                        viewportPanel.Height - 1);

                g.DrawLine(pen, x, y, x, y2);

                firstColor = !firstColor;
            }
        }

        private void DrawVisibleWaveform(Graphics g)
        {
            if (_fullWaveformBitmap == null)
                return;

            Rectangle waveformRect = GetWaveformRect();

            double startPercent = _viewportStartSeconds / _audioLengthSeconds;

            double endPercent = (_viewportStartSeconds
                + _viewportDurationSeconds)
                / _audioLengthSeconds;

            int srcX = (int)(startPercent * _fullWaveformBitmap.Width);

            int srcWidth = (int)((endPercent - startPercent) * _fullWaveformBitmap.Width);

            if (srcWidth <= 0) return;

            Rectangle srcRect =
                new Rectangle(srcX, 0, srcWidth, _fullWaveformBitmap.Height);

            g.DrawImage(_fullWaveformBitmap, waveformRect, srcRect, GraphicsUnit.Pixel);
        }

        private void DrawBeatMarkers(Graphics g)
        {
            if (_beatTimes.Count == 0)
            {
                return;
            }

            Rectangle waveformRect = GetWaveformRect();
            Color beatColor = Color.FromArgb(225, Color.Brown);
            if (_viewportDurationSeconds > 20)
            {
                beatColor = Color.FromArgb(100, Color.Green);
            }

            using (Pen pen = new Pen(beatColor))
            {
                foreach (double beatTime in _beatTimes)
                {
                    if (beatTime < _viewportStartSeconds)
                    {
                        continue;
                    }

                    if (beatTime >
                        _viewportStartSeconds + _viewportDurationSeconds)
                    {
                        break;
                    }

                    int x = TimeToX(beatTime);

                    g.DrawLine(pen, x, waveformRect.Top, x, waveformRect.Bottom);
                }
            }
        }

        private void DrawCutMarkers(Graphics g)
        {
            foreach (CutMarker marker in _cutMarkers)
            {
                DrawSingleCutMarker(g, marker);
            }
        }

        private void DrawSingleCutMarker(Graphics g, CutMarker marker)
        {
            int x = TimeToX(marker.TimeSeconds);

            // =====================================================
            // BLACK / YELLOW DASHED LINE
            // =====================================================

            int y = MARKER_TRIANGLE_HEIGHT;
            bool yellow = true;

            while (y < viewportPanel.Height)
            {
                int y2 = Math.Min(
                    y + MARKER_DASH_LENGTH,
                    viewportPanel.Height);

                using (Pen dashPen = new Pen(
                    yellow ? Color.Yellow : Color.Black,
                    1))
                {
                    g.DrawLine(dashPen, x, y, x, y2);
                }

                y += MARKER_DASH_LENGTH;
                yellow = !yellow;
            }

            // =====================================================
            // TRIANGLE
            // =====================================================

            Point[] tri =
            {
                new Point(x - MARKER_TRIANGLE_WIDTH, 0),
                new Point(x + MARKER_TRIANGLE_WIDTH, 0),
                new Point(x, MARKER_TRIANGLE_HEIGHT)
            };

            g.FillPolygon(Brushes.Yellow, tri);
            g.DrawPolygon(Pens.Black, tri);
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

        private void ViewportPanel_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;

            CutMarker nearest = FindMarkerAtPixel(e.X);

            if (nearest != null)
            {
                SQLiteDataAccess.DeleteCutMarker(_loadedFile, nearest.TimeSeconds);
                PushUndoState();
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
            _viewportBitmapDirty = true;
            viewportPanel.Invalidate(dirty);
        }

        private void ViewportPanel_MouseUp(object sender, MouseEventArgs e)
        {
            if (_draggingMarker != null)
            {
                FinishMarkerDrag();
            }
            else
            {
                MovePlayheadToX(e.X);
            }
        }

        private void MovePlayheadToX(int x)
        {
            _playheadTimeSeconds =
                ClampTime(XToTime(x));

            _playheadSetByUser = true;
            _playheadTimeSeconds = ClampTime(_playheadTimeSeconds);

            viewportPanel.Invalidate();
        }

        private void FinishMarkerDrag()
        {
            SQLiteDataAccess.AddCutMarker(_loadedFile, _draggingMarker.TimeSeconds);
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
            _viewportBitmapDirty = true;
            viewportPanel.Invalidate();
            FireCutsChanged();
        }

        private void ViewportPanel_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;

            PushUndoState();

            CutMarker nearest = FindMarkerAtPixel(e.X);

            if (nearest != null)
            {
                _cutMarkers.Remove(nearest);
                SQLiteDataAccess.DeleteCutMarker(_loadedFile, nearest.TimeSeconds);
                FireCutsChanged();
            }
            else
            {
                _cutMarkers.Add(
                    new CutMarker
                    {
                        TimeSeconds = ClampTime(XToTime(e.X))
                    });
                SQLiteDataAccess.AddCutMarker(_loadedFile, ClampTime(XToTime(e.X)));
                FireCutsChanged();
            }
            ResetPlayhead();
            _viewportBitmapDirty = true;
            viewportPanel.Invalidate();

        }

        private CutMarker FindMarkerAtPixel(int x)
        {
            CutMarker closest = null;

            int closestDist = MARKER_HIT_RADIUS_PX;

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
                    * SCROLL_RESOLUTION);

            if (largeChange < 1)
                largeChange = 1;

            hScrollBar.LargeChange =
                largeChange;

            hScrollBar.SmallChange =
                Math.Max(
                    1,
                    largeChange / 10);

            hScrollBar.Maximum =
                SCROLL_RESOLUTION
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
            ResetPlayhead();
            ClampViewport();
            _viewportBitmapDirty = true;
            viewportPanel.Invalidate();
        }

        private void BtnZoomIn_Click(object sender, EventArgs e)
        {
            ZoomViewport(0.5, _viewportStartSeconds + (_viewportDurationSeconds * 0.5));
        }

        private void BtnZoomOut_Click(object sender, EventArgs e)
        {
            ZoomViewport(2.0, _viewportStartSeconds + (_viewportDurationSeconds * 0.5));
        }

        private void BtnZoomRight_Click(object sender, EventArgs e)
        {
            ZoomViewport(0.5, _viewportStartSeconds + (_viewportDurationSeconds * 0.75));
        }

        private void ZoomViewport(double factor, double anchorTime)
        {
            PushUndoState();

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

            ResetPlayhead();

            ClampViewport();

            UpdateScrollbar();

            _viewportBitmapDirty = true;
            viewportPanel.Invalidate();
        } 

        private Rectangle GetWaveformRect()
        {
            return new Rectangle(
                0,
                TIMELINE_HEIGHT,
                viewportPanel.Width,
                viewportPanel.Height
                    - TIMELINE_HEIGHT);
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
                x - MARKER_INVALIDATE_PADDING,
                0,
                MARKER_INVALIDATE_PADDING * 2,
                viewportPanel.Height);
        }

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

        private void ViewportPanel_Resize(object sender, EventArgs e)
        {
            ResetPlayhead();
            RegenerateWaveformBitmap();
            _viewportBitmapDirty = true;

            viewportPanel.Invalidate();
        }

        private void PlaybackTimer_Tick(object sender, EventArgs e)
        {
            if (!_isPlaying || _playbackReader == null)
                return;

            _playheadTimeSeconds =
                _playbackReader.CurrentTime.TotalSeconds;

            CheckForUpcomingCutMarkers();

            if (_playheadTimeSeconds >= _playbackEndTimeSeconds)
            {
                StopPlayback();
                return;
            }

            btnPlayheadTime.Text = TimeSpan
                .FromSeconds(_playheadTimeSeconds)
                .ToString(@"mm\:ss\.ff");

            int x = TimeToX(_playheadTimeSeconds);

            if (_previousPlayheadX >= 0)
            {
                viewportPanel.Invalidate(
                    new Rectangle(_previousPlayheadX - 1, 0, 2, Height)
                    , false
                );
            }

            viewportPanel.Invalidate(
                new Rectangle(x - 1, 0, 2, Height)
                , false
            );

            _previousPlayheadX = x;
        }

        private void ResetPlayhead()
        {
            if (_isPlaying) StopPlayback();

            _playheadSetByUser = false;
            _playheadTimeSeconds = _viewportStartSeconds;

            viewportPanel.Invalidate();
        }

        private void BtnPlay_Click(object sender, EventArgs e)
        {
            if (_loadedFile == null) return;

            double startTime;

            if (_playheadSetByUser)
            {
                startTime = _playheadTimeSeconds;
            }
            else
            {
                startTime = _viewportStartSeconds;
            }

            StartPlayback(startTime);
        }

        private void StartPlayback(double startTime)
        {
            StopPlayback();

            _playbackReader =
                UniversalAudioReader.Open(_loadedFile);

            _waveOut = new WaveOutEvent();

            _waveOut.Init(_playbackReader);

            _playbackReader.CurrentTime =
                TimeSpan.FromSeconds(startTime);

            _playbackStartTimeSeconds = startTime;

            _playbackEndTimeSeconds =
                _viewportStartSeconds +
                _viewportDurationSeconds;

            _playheadTimeSeconds = startTime;

            _playedSnipMarkers.Clear();

            _waveOut.Play();

            _isPlaying = true;

            _playbackTimer.Start();

            viewportPanel.Invalidate();
        }

        private void StopPlayback()
        {
            _playbackTimer.Stop();

            _waveOut?.Stop();
            _waveOut?.Dispose();
            _waveOut = null;

            _playbackReader?.Dispose();
            _playbackReader = null;

            _isPlaying = false;
            btnPause.Text = "Pause";
        }

        private void BtnPause_Click(object sender, EventArgs e)
        {
            if (_waveOut == null)
            {
                return;
            }

            // =========================================
            // RESUME
            // =========================================

            if (!_isPlaying)
            {
                _waveOut.Play();

                _playbackTimer.Start();

                _isPlaying = true;

                btnPause.Text = "Pause";

                return;
            }

            // =========================================
            // PAUSE
            // =========================================

            _waveOut.Pause();

            _playbackTimer.Stop();

            _isPlaying = false;

            btnPause.Text = "Resume";
        }

        public List<Cut> GetCuts()
        {
            List<Cut> cuts = new List<Cut>();
            double lastCutTime = 0;
            for (int i = 0; i < _cutMarkers.Count; i++)
            {
                cuts.Add(new Cut
                {
                    Index = i + 1,
                    StartTimeSeconds = lastCutTime,
                    EndTimeSeconds = _cutMarkers[i].TimeSeconds
                });
                lastCutTime = _cutMarkers[i].TimeSeconds;
            }
            // fence post: add final cut from last marker to end of audio
            if (lastCutTime < _audioLengthSeconds)
            {
                cuts.Add(new Cut
                {
                    Index = cuts.Count + 1,
                    StartTimeSeconds = lastCutTime,
                    EndTimeSeconds = _audioLengthSeconds
                });
            }
            return cuts;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                components?.Dispose();

                _viewportBitmap?.Dispose();
                _viewportBitmap = null;
                _fullWaveformBitmap?.Dispose();
                _fullWaveformBitmap = null;
                _waveOut?.Dispose();
                _waveOut = null;
                _playbackReader?.Dispose();
                _playbackReader = null;
                _snipReader?.Dispose();
                _snipWaveOut?.Dispose();

                if (_playbackTimer != null)
                {
                    _playbackTimer.Stop();
                    _playbackTimer.Dispose();
                }
            }

            base.Dispose(disposing);
        }

        internal void HighlightCut(Cut cut)
        {
            PushUndoState();

            _viewportStartSeconds = cut.StartTimeSeconds;
            _viewportDurationSeconds = cut.LengthSeconds;
            ResetPlayhead();
            ClampViewport();
            UpdateScrollbar();
            _viewportBitmapDirty = true;
            viewportPanel.Invalidate();
        }

    }

    public struct WaveformPeak
    {
        public float Min;
        public float Max;
    }

    public class CutMarker
    {
        public double TimeSeconds { get; set; }
    }

    public class Cut
    {
        public int Index { get; set; }
        public double StartTimeSeconds { get; set; }
        public double EndTimeSeconds { get; set; }
        public double LengthSeconds => EndTimeSeconds - StartTimeSeconds;
    }

    public class ICutState
    {
        public double ViewportStartSeconds { get; set; }
        public double ViewportDurationSeconds { get; set; }
        public List<CutMarker> CutMarkers { get; set; }
    }

    public class CutsChangedEventArgs : EventArgs
    {
        public List<Cut> Cuts { get; set; } = new List<Cut>();
        public CutsChangedEventArgs(IEnumerable<Cut> cuts)
        {
            Cuts.AddRange(cuts);
        }
    }

    public delegate void CutsChangedEventHandler(object sender, CutsChangedEventArgs e);
}