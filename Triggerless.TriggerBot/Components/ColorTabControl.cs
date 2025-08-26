using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Triggerless.TriggerBot.Components
{
    public class ColorTabControl : TabControl
    {
        private Color _tabBackSelected = Color.FromArgb(45, 45, 48);
        private Color _tabForeSelected = Color.White;
        private Color _tabBackUnselected = SystemColors.Control;
        private Color _tabForeUnselected = SystemColors.ControlText;

        public ColorTabControl()
        {
            // Keep constructor minimal/safe for Designer
            DrawMode = TabDrawMode.OwnerDrawFixed;
            SizeMode = TabSizeMode.Normal;
            // Avoid transparent backcolors causing designer flicker
            SetStyle(ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.UserPaint |
                     ControlStyles.OptimizedDoubleBuffer, true);
        }

        // If someone changes DrawMode in Designer/code, enforce owner-draw again
        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            if (DrawMode != TabDrawMode.OwnerDrawFixed)
                DrawMode = TabDrawMode.OwnerDrawFixed;
        }

        [Category("Appearance"), Description("Background color for the selected tab header.")]
        public Color TabBackColorSelected
        {
            get => _tabBackSelected;
            set { _tabBackSelected = value; Invalidate(); }
        }

        [Category("Appearance"), Description("Text color for the selected tab header.")]
        public Color TabForeColorSelected
        {
            get => _tabForeSelected;
            set { _tabForeSelected = value; Invalidate(); }
        }

        [Category("Appearance"), Description("Background color for unselected tab headers.")]
        public Color TabBackColorUnselected
        {
            get => _tabBackUnselected;
            set { _tabBackUnselected = value; Invalidate(); }
        }

        [Category("Appearance"), Description("Text color for unselected tab headers.")]
        public Color TabForeColorUnselected
        {
            get => _tabForeUnselected;
            set { _tabForeUnselected = value; Invalidate(); }
        }

        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            // Always call base first (safe for Designer)
            base.OnDrawItem(e);

            // Design-time guard & defensive checks to keep Designer happy
            if (!IsHandleCreated ||
                e.Index < 0 ||
                e.Index >= TabPages.Count ||
                IsInDesigner())
            {
                // Let default drawing happen in Designer (prevents crashes)
                return;
            }

            var selected = (e.Index == SelectedIndex);
            var page = TabPages[e.Index];

            var back = selected ? _tabBackSelected : _tabBackUnselected;
            var fore = selected ? _tabForeSelected : _tabForeUnselected;

            // Fill the tab header background
            using (var b = new SolidBrush(back))
                e.Graphics.FillRectangle(b, e.Bounds);

            // Optional subtle top highlight for selected tab
            if (Alignment == TabAlignment.Top && selected)
            {
                var hl = Color.FromArgb(
                    Math.Min(back.R + 24, 255),
                    Math.Min(back.G + 24, 255),
                    Math.Min(back.B + 24, 255));
                using (var pen = new Pen(hl))
                    e.Graphics.DrawLine(pen, e.Bounds.Left, e.Bounds.Top, e.Bounds.Right, e.Bounds.Top);
            }

            // Text rectangle with a bit of padding
            var textRect = Rectangle.Inflate(e.Bounds, -6, -4);

            // Draw text
            TextRenderer.DrawText(
                e.Graphics,
                page.Text,
                e.Font,
                textRect,
                fore,
                TextFormatFlags.HorizontalCenter |
                TextFormatFlags.VerticalCenter |
                TextFormatFlags.EndEllipsis |
                TextFormatFlags.SingleLine);
        }

        // Some designer hosts don’t set DesignMode reliably for child controls—use this helper
        private bool IsInDesigner()
        {
            if (DesignMode) return true;
            var site = Site;
            return site != null && site.DesignMode;
        }
    }
}
