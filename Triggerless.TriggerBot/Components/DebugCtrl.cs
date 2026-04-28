using System;
using System.Windows.Forms;
using Triggerless.TriggerBot.Models;

namespace Triggerless.TriggerBot
{
    public partial class DebugCtrl : UserControl
    {
        public DebugCtrl()
        {
            InitializeComponent();
        }

        private void Send_Click(object sender, EventArgs e)
        {
            ImvuWindow.SendText(txtDebug.Text);
        }

        private void btnPaste_Click(object sender, EventArgs e)
        {
            //StyledMessageBox.Show(Clipboard.GetText(), "Clipboard Contents", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ImvuWindow.Paste();
        }
    }
}
