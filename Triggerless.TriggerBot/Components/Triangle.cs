using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Triggerless.TriggerBot
{
    public partial class Triangle : UserControl
    {
        public Triangle()
        {
            InitializeComponent();
        }

        public Orientation Direction { get; set; } = Orientation.Down; 

        private void Triangle_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            e.Graphics.Clear(Parent.BackColor);
            

            List<Point> points = new List<Point>();
            switch (Direction)
            {
                case Orientation.Down:
                    points.Add(new Point(Width / 2, Height));
                    points.Add(new Point(0, 0));
                    points.Add(new Point(Width, 0));
                    points.Add(new Point(Width / 2, Height));
                    break;
                case Orientation.Up:
                    points.Add(new Point(Width / 2, 0));
                    points.Add(new Point(Width, Height));
                    points.Add(new Point(0, Height));
                    points.Add(new Point(Width / 2, 0));
                    break;
                case Orientation.Left:
                    points.Add(new Point(0, Height / 2));
                    points.Add(new Point(Width, 0));
                    points.Add(new Point(Width, Height));
                    points.Add(new Point(0, Height / 2));
                    break;
                case Orientation.Right:
                    points.Add(new Point(Width, Height / 2));
                    points.Add(new Point(0, Height));
                    points.Add(new Point(0, 0));
                    points.Add(new Point(Width, Height / 2));
                    break;
            }
            e.Graphics.FillPolygon(Brushes.Black, points.ToArray());
            //return;

            points.Clear();

            switch (Direction)
            {
                case Orientation.Down:
                    points.Add(new Point(Width / 2, Height - 2));
                    points.Add(new Point(1, 1));
                    points.Add(new Point(Width - 1, 1));
                    points.Add(new Point(Width / 2, Height - 2));
                    break;
                case Orientation.Up:
                    points.Add(new Point(Width / 2, 2));
                    points.Add(new Point(Width - 1, Height - 2));
                    points.Add(new Point(1, Height - 2));
                    points.Add(new Point(Width / 2, 2));
                    break;
                case Orientation.Left:
                    points.Add(new Point(2, Height / 2 - 1));
                    points.Add(new Point(Width - 2, 1));
                    points.Add(new Point(Width - 1, Height - 1));
                    points.Add(new Point(2, Height / 2 - 1));
                    break;
                case Orientation.Right:
                    points.Add(new Point(Width - 2, Height / 2 - 1));
                    points.Add(new Point(1, Height));
                    points.Add(new Point(1, 1));
                    points.Add(new Point(Width - 2, Height / 2 - 1));
                    break;
            }

            Color color = ForeColor;
            using (Brush b = new SolidBrush(color))
            {
                e.Graphics.FillPolygon(b, points.ToArray());
            }

        }

        public enum Orientation
        {
            Down, Up, Left, Right
        }

        private void Triangle_Click(object sender, EventArgs e)
        {
            this.Focus();
            this.Update();
        }
    }
}
