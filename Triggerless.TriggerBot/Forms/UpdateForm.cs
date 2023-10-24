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
    public partial class UpdateForm : Form
    {
        public UpdateForm()
        {
            InitializeComponent();
        }

        public string WhatsNewText {
            get => txtWhatsNew.Text;
            set => txtWhatsNew.Text = value;
        }

        private void btnUpdateImmediately_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void btnUpdateOnExit_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Yes;
        }

        private void btnIgnore_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.No;
        }

        public string VersionString { get; set; }

        private void LoadForm(object sender, EventArgs e)
        {
            lblVersion.Text = lblVersion.Text.Replace("{version}", $"({VersionString})");
        }
    }
}
