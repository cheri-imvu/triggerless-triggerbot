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
    public partial class RenameSongForm : Form
    {
        public ProductDisplayInfo ProductInfo { get; set; }


        public RenameSongForm()
        {
            InitializeComponent();
        }

        private void RenameSongForm_Load(object sender, EventArgs e)
        {
            if (ProductInfo != null) 
            {
                txtName.Text = ProductInfo.Name;
            }
            //DialogResult = DialogResult.Cancel;
        }

        private void RenameSongForm_Shown(object sender, EventArgs e)
        {
            txtName.Focus();
            txtName.SelectAll();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (txtName.Text.Length < 9)
            {
                txtName.Focus();
                txtName.SelectAll();
                return;
            }
            ProductInfo.Name = txtName.Text;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
