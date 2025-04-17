using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;

namespace Triggerless.TriggerBot.Components
{
    public partial class LyricsStopwatch : Stopwatch, IComponent
    {
        private const double LAG_MS = 120;
        private const double TIMER_INTERVAL = 25;

        private int _listIndex = -1;
        private List<LyricEntry> _list = new List<LyricEntry>();
        private System.Timers.Timer _timer;
        private TimeSpan _nextFire = TimeSpan.Zero;
        private TimeSpan _totalTime = TimeSpan.Zero;

        public LyricsStopwatch()
        {
            InitializeComponent();
            SetupTimer();
        }

        public LyricsStopwatch(IContainer container)
        {
            container.Add(this);
            InitializeComponent();
            SetupTimer();
        }

        private void SetupTimer()
        {
            _timer = new System.Timers.Timer(TIMER_INTERVAL);
            _timer.Enabled = false;
            _timer.Elapsed += _timer_Elapsed;
        }

        private void _timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (this.Elapsed > _totalTime)
            {
                _timer.Stop();
                this.Stop();
                base.Reset();
                return;
            }

            if (this.Elapsed > _nextFire - TimeSpan.FromMilliseconds(LAG_MS))
            {
                FireEvent(_nextFire, _list[_listIndex].Lyric);
                if (_listIndex < _list.Count - 1)
                {
                    _listIndex++;
                    _nextFire = _list[_listIndex].Time;
                } else
                {

                }
            }
        }

        private ProductDisplayInfo _product;

        public event EventHandler Disposed;

        public ProductDisplayInfo Product
        {
            get => _product;
            set
            {
                _list.Clear();
                _listIndex = -1;
                _product = value;
                _nextFire = TimeSpan.Zero;
                _totalTime = TimeSpan.Zero;
                base.Reset();
                _timer.Stop();

                if (value == null) return;

                if (!_product.HasLyrics) { return; }
                _list = _product.Lyrics;
                _listIndex = 0;
                _totalTime = TimeSpan.FromMinutes(4);
                var mp3Path = Path.Combine(Shared.LyricSheetsPath, $"{_product.Id}.mp3");
                if (File.Exists(mp3Path))
                {
                    using (var reader = new Mp3FileReader(mp3Path))
                    {
                        _totalTime = reader.TotalTime;
                    }
                }

            }
        }

        private ISite _site;
        public ISite Site { get => _site; set => _site = value; }

        public new void Stop()
        {
            base.Stop();
            base.Reset();
            _timer.Stop();
        }

        public new void Start()
        {
            base.Start();
            _timer.Start();
        }
        public void Dispose()
        {
            _timer?.Stop();
            _timer?.Dispose();
        }
        public event LyricStopwatchEventHandler LyricReady;
        private void FireEvent(TimeSpan time, string lyric)
        {
            LyricReady?.Invoke(this, new LyricStopwatchEventArgs(time, lyric));
        }
    }
    public class LyricStopwatchEventArgs : EventArgs
    {
        public TimeSpan Time { get; private set; }
        public string Lyric { get; private set; }
        public LyricStopwatchEventArgs(TimeSpan time, string lyric)
        {
            Time = time;
            Lyric = lyric;
        }
    }
    public delegate void LyricStopwatchEventHandler(object sender, LyricStopwatchEventArgs e);
}
