using System;
using System.Windows.Forms;

namespace Triggerless.TriggerBot.Components
{

    public class VirtualScrollPanel : Panel
    {
        public event EventHandler ScrollPositionChanged;

        public VirtualScrollPanel()
        {
            this.DoubleBuffered = true;
            this.SetStyle(
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.UserPaint |
                ControlStyles.OptimizedDoubleBuffer,
                true);

            this.UpdateStyles();
        }

        protected override void WndProc(ref Message m)
        {
            const int WM_VSCROLL = 0x0115;
            const int WM_MOUSEWHEEL = 0x020A;

            base.WndProc(ref m);

            if (m.Msg == WM_VSCROLL || m.Msg == WM_MOUSEWHEEL)
            {
                ScrollPositionChanged?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
