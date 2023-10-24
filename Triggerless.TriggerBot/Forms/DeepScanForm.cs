using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Triggerless.TriggerBot.Forms
{
    public partial class DeepScanForm : Form
    {

        public List<long> SelectedProductIds { get; set; }
        public Collector Collector { get; set; }

        public DeepScanForm()
        {
            InitializeComponent();
        }

        private void DoSearch(object sender, EventArgs e)
        {
            var search = txtSearch.Text;
            if (string.IsNullOrWhiteSpace(search) )
            {
                MessageBox.Show("Please enter a search term", "Search Term Required");
                txtSearch.Focus();
            }

            if (Collector != null)
            {
                gridProduct.Rows.Clear();
                foreach (var item in Collector.DeepScanList(search))
                {
                    gridProduct.Rows.Add(new object[] { item.ProductId, false, item.Creator, item.Name });
                }
            }
        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Return))
            {
                e.Handled = true;
                DoSearch(this, new EventArgs());
            }
        }

        private void DoDeepScan(object sender, EventArgs e)
        {
            SelectedProductIds = new List<long>();
            foreach (DataGridViewRow row in gridProduct.Rows)
            {
                if ((bool)row.Cells[colItemCheck.Index].Value)
                {
                    if (long.TryParse(row.Cells[colProductId.Index].Value.ToString(), out long number))
                    {
                        SelectedProductIds.Add(number);
                    }
                }
            }
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
