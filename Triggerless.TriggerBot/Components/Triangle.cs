using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

namespace Triggerless.TriggerBot
{
    // Best fix: true transparency via Region (non-rectangular window)
    public partial class Triangle : Control
    {
        public enum Orientation { Down, Up, Left, Right }

        private Orientation _direction = Orientation.Down;

        /// <summary>Triangle pointing direction.</summary>
        public Orientation Direction
        {
            get => _direction;
            set
            {
                if (_direction == value) return;
                _direction = value;
                UpdateRegion();
                Invalidate();
            }
        }

        /// <summary>
        /// Horizontal pixel coordinate of the triangle's center relative to parent.
        /// Setter repositions the control so its center sits at this X.
        /// </summary>
        public int Position
        {
            get => Left + Width / 2;
            set => Left = value - Width / 2;
        }

        /// <summary>Optional border thickness. Set to 0 for no border.</summary>
        public int BorderThickness { get; set; } = 2;

        /// <summary>Optional border color when BorderThickness &gt; 0.</summary>
        public Color BorderColor { get; set; } = Color.Maroon;

        public Triangle()
        {
            SetStyle(ControlStyles.UserPaint
                     | ControlStyles.AllPaintingInWmPaint
                     | ControlStyles.OptimizedDoubleBuffer
                     | ControlStyles.ResizeRedraw
                     | ControlStyles.SupportsTransparentBackColor, true);

            BackColor = Color.Transparent;   // region does the heavy lifting
            ForeColor = Color.Red;           // default fill color
            Size = new Size(16, 10);         // sensible default

            // If this file was originally created as a UserControl and you have a .Designer.cs,
            // keep the next line. Otherwise, it's safe to remove.
            // InitializeComponent();

            UpdateRegion();
        }

        protected override CreateParams CreateParams
        {
            get
            {
                var cp = base.CreateParams;
                cp.ExStyle |= 0x20; // WS_EX_TRANSPARENT (lets parent paint first)
                return cp;
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            UpdateRegion();
            Invalidate();
        }

        protected override void OnForeColorChanged(EventArgs e)
        {
            base.OnForeColorChanged(e);
            Invalidate();
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            // Intentionally do nothing: no background fill.
            // The Region makes the outside truly transparent.
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            var tri = GetTrianglePoints(Direction, Width, Height, inset: 0);

            // Fill the triangle
            using (var brush = new SolidBrush(ForeColor))
            {
                e.Graphics.FillPolygon(brush, tri);
            }

            // Optional border
            if (BorderThickness > 0)
            {
                using (var pen = new Pen(BorderColor, BorderThickness))
                {
                    // Align border inside the region a bit
                    pen.Alignment = PenAlignment.Inset;
                    e.Graphics.DrawPolygon(pen, tri);
                }
            }
        }

        private void UpdateRegion()
        {
            // Avoid degenerate regions
            int w = Math.Max(2, Width);
            int h = Math.Max(2, Height);

            using (var path = new GraphicsPath())
            {
                path.AddPolygon(GetTrianglePoints(Direction, w, h, inset: 0));
                Region?.Dispose();
                Region = new Region(path);
            }
        }

        private static Point[] GetTrianglePoints(Orientation dir, int w, int h, int inset)
        {
            // Clamp inset to keep points inside bounds
            inset = Math.Max(0, Math.Min(inset, Math.Min(w, h) / 4));

            // Use -1 to stay within client area when drawing borders
            int w1 = Math.Max(1, w - 1);
            int h1 = Math.Max(1, h - 1);

            switch (dir)
            {
                case Orientation.Down:
                    return new[]
                    {
                        new Point(w1 / 2, h1 - inset),
                        new Point(inset, inset),
                        new Point(w1 - inset, inset)
                    };

                case Orientation.Up:
                    return new[]
                    {
                        new Point(w1 / 2, inset),
                        new Point(w1 - inset, h1 - inset),
                        new Point(inset, h1 - inset)
                    };

                case Orientation.Left:
                    return new[]
                    {
                        new Point(inset, h1 / 2),
                        new Point(w1 - inset, inset),
                        new Point(w1 - inset, h1 - inset)
                    };

                case Orientation.Right:
                default:
                    return new[]
                    {
                        new Point(w1 - inset, h1 / 2),
                        new Point(inset, h1 - inset),
                        new Point(inset, inset)
                    };
            }
        }
    }
}
