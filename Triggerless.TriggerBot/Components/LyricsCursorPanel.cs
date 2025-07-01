using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace Triggerless.TriggerBot.Components
{
    public partial class LyricsCursorPanel : Panel
    {
        public int CursorX { get; set; } = 0;
        private Pen _penYellow;  // Used by _cursorOverlay
        private Pen _penBlack;    // Used by _cursorOverlay


        public LyricsCursorPanel()
        {
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.BackColor = Color.Green;
            //this.DoubleBuffered = true;
            _penYellow = new Pen(Color.Yellow, 2);
            _penBlack = new Pen(Color.Black, 2);
            Disposed += OnDisposed;
        }

        private Image _image;
        public Image Image
        {
            get => _image;
            set
            {
                _image = value;
                Invalidate();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            // Create a new bitmap with alpha channel
            Bitmap bmp = new Bitmap(this.Width, this.Height, PixelFormat.Format32bppArgb);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                // Clear the bitmap to transparent
                g.Clear(Color.Green);
                if (_image != null) g.DrawImage(_image, new Point(0, 0));
                g.DrawLine(_penBlack, CursorX - 1, 0, CursorX - 1, this.Height);
                g.DrawLine(_penYellow, CursorX + 1, 0, CursorX + 1, this.Height);
            }
            //base.OnPaint(e);
        }

        private void OnDisposed(object sender, EventArgs e)
        {
            _penBlack?.Dispose();
            _penYellow?.Dispose();
        }

    }
}
