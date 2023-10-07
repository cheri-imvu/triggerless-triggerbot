using Dapper;
using System;
using System.Windows.Forms;

namespace Triggerless.TriggerBot
{
    public partial class AddnTriggersForm : Form
    {
        public ProductDisplayInfo Product { get; set; }
        private const int COL_ADDN_TRIGGERS = 3;

        public AddnTriggersForm()
        {
            InitializeComponent();
        }

        private void LoadForm(object sender, EventArgs e)
        {
            if (Product == null)
            {
                Close();
                return;
            }

            lblTitle.Text = Product.Name + " by " + Product.Creator;
            foreach (var trigger in Product.Triggers)
            {
                gridTriggers.Rows.Add(trigger.Prefix, trigger.Sequence, trigger.Trigger, trigger.AddnTriggers);
            }

        }

        private void gridTriggers_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (gridTriggers.CurrentCell.ColumnIndex == 3)
            {
                gridTriggers.BeginEdit(true);
            }
        }

        private bool HasGridChanged()
        {
            for (int iRow = 0; iRow < gridTriggers.Rows.Count; iRow++)
            {
                if ((string)gridTriggers.Rows[iRow].Cells[COL_ADDN_TRIGGERS].Value !=
                    Product.Triggers[iRow].AddnTriggers) return true;
            }
            return false;
        }

        private void CancelClick(object sender, EventArgs e)
        {
            if (HasGridChanged())
            {
                DialogResult dlgResult = DialogResult.Yes;
                dlgResult = MessageBox.Show("Save pending changes?", "Save Changes?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dlgResult == DialogResult.Yes)
                {
                    SaveChanges();
                }
            }
            Close();
        }

        private void SaveChanges()
        {
            var sda = new SQLiteDataAccess();
            using (var conn = sda.GetAppCacheCxn())
            {
                conn.Open();
                for (int iRow = 0; iRow < gridTriggers.Rows.Count; iRow++)
                {
                    var cellValue = (string)gridTriggers.Rows[iRow].Cells[COL_ADDN_TRIGGERS].Value;
                    if (cellValue != Product.Triggers[iRow].AddnTriggers)
                    {
                        Product.Triggers[iRow].AddnTriggers = cellValue; // side effect, changes original value
                        var trigger = Product.Triggers[iRow];
                        var value = String.IsNullOrWhiteSpace(cellValue) ? "NULL" : $"'{cellValue.Replace("'","''")}'";
                        var sql = $"UPDATE product_triggers SET addn_triggers = {value} WHERE product_id = {trigger.ProductId} AND prefix = '{trigger.Prefix}' AND sequence = {trigger.Sequence}";
                        conn.Execute(sql);
                    }
                }
            }
        }

        private void btnSaveClicked(object sender, EventArgs e)
        {
            SaveChanges();
            Close();
        }
    }
}
