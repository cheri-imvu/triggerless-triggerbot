using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Triggerless.TriggerBot.Forms
{
    public partial class LyricsPaste : Form
    {
        public LyricsPaste()
        {
            InitializeComponent();
        }
        public string CopiedText => txtLyrics.Text;

        private void pnlTop_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pnlBottom_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnPaste_Click(object sender, EventArgs e)
        {
            if (Clipboard.ContainsText())
            {
                txtLyrics.Text = Clipboard.GetText();
            }
            else
            {
                 MessageBox.Show("No text in the Clipboard", "Text Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {

            DialogResult= DialogResult.Cancel;
            Close();
        }

        private void LyricsPaste_Load(object sender, EventArgs e)
        {
            if (Clipboard.ContainsText())
            {
                txtLyrics.Text = Clipboard.GetText();
            }

        }
    }
}
