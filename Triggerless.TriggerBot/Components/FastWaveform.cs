using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;


namespace Triggerless.TriggerBot
{
    public static class FastWaveform
    {
        /// <summary>
        /// Safest/fastest: decode from file (does not disturb your active Mp3FileReader).
        /// </summary>
        public static void SavePngFromFile(string mp3Path, string pngPath, int width, int height,
                                           int maxPoints = 5000, Color? waveColor = null)
        {
            if (width < 2 || height < 2) throw new ArgumentException("width/height too small");
            var color = waveColor ?? Color.RoyalBlue;

            using (var reader = new AudioFileReader(mp3Path)) // float 32
            using (var bmp = RenderWaveform(reader, width, height, maxPoints, color))
            {
                bmp.Save(pngPath, ImageFormat.Png);
            }
        }

        private static Bitmap RenderWaveform(ISampleProvider source, int width, int height, int maxPoints, Color waveColor)
        {
            int channels = source.WaveFormat.Channels;

            // 1) Make coarse raw peaks over fixed-size time blocks (fast, low memory)
            //    ~4096 frames/window ≈ 93ms at 44.1k — tweak as desired.
            const int framesPerRaw = 4096;
            var rawPeaks = new List<float>(4096);

            float[] buf = new float[framesPerRaw * channels];
            int framesAccumulated = 0;
            float blockMax = 0f;

            while (true)
            {
                int read = source.Read(buf, 0, buf.Length);
                if (read <= 0) break;

                int framesRead = read / channels;
                int offset = 0;

                for (int f = 0; f < framesRead; f++)
                {
                    // Combine stereo: max of |sample| across channels
                    float amp = 0f;
                    for (int c = 0; c < channels; c++)
                    {
                        float v = buf[offset + c];
                        float a = v < 0 ? -v : v;
                        if (a > amp) amp = a;
                    }
                    offset += channels;

                    if (amp > blockMax) blockMax = amp;

                    framesAccumulated++;
                    if (framesAccumulated >= framesPerRaw)
                    {
                        rawPeaks.Add(blockMax);
                        framesAccumulated = 0;
                        blockMax = 0f;
                    }
                }
            }

            // Flush final partial block
            if (framesAccumulated > 0) rawPeaks.Add(blockMax);
            if (rawPeaks.Count == 0) rawPeaks.Add(0f);

            // 2) Downsample rawPeaks to the number of columns we’ll draw (≤ width and ≤ maxPoints)
            int columns = Math.Max(1, Math.Min(width, maxPoints));
            var cols = DownsampleMax(rawPeaks, columns);

            // 3) Draw vertical bars symmetrically around center on a transparent canvas
            var bmp = new Bitmap(width, height, PixelFormat.Format32bppArgb);
            using (var g = Graphics.FromImage(bmp))
            {
                g.SmoothingMode = SmoothingMode.None;
                g.PixelOffsetMode = PixelOffsetMode.Half;
                g.Clear(Color.Transparent);

                int mid = height / 2;

                // Map columns array onto full width (stretch if columns < width)
                using (var pen = new Pen(waveColor, 1f))
                {
                    for (int x = 0; x < width; x++)
                    {
                        // Pick appropriate column bin for this pixel
                        int col = (int)((long)x * columns / width);
                        float amp = cols[col]; // 0..1 (float sample range)
                        if (amp <= 0f) continue;

                        int y = (int)Math.Round(amp * (mid - 1));
                        int y1 = mid - y;
                        int y2 = mid + y;
                        g.DrawLine(pen, x, y1, x, y2);
                    }
                }
            }

            return bmp;
        }

        /// <summary>
        /// Downsample by taking max over proportional ranges (covers 100% including last bin).
        /// </summary>
        private static float[] DownsampleMax(List<float> src, int dstCount)
        {
            int n = src.Count;
            var dst = new float[dstCount];

            for (int i = 0; i < dstCount; i++)
            {
                // range [start, end) mapped by exact proportion; guarantees last bin reaches n
                int start = (int)((long)i * n / dstCount);
                int end = (int)((long)(i + 1) * n / dstCount);
                if (end <= start) end = Math.Min(start + 1, n);

                float m = 0f;
                for (int k = start; k < end; k++)
                    if (src[k] > m) m = src[k];

                dst[i] = m;
            }
            return dst;
        }
    }
}
