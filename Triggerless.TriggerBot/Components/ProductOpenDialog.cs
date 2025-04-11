using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Menu;

namespace Triggerless.TriggerBot
{
    public partial class ProductOpenDialog : Form
    {
        private static string SEARCH_TERM = string.Empty;
        private ProductDisplayInfo _selectedProduct;

        public ProductDisplayInfo SelectedProduct => _selectedProduct;

        public ProductOpenDialog()
        {
            InitializeComponent();
            InitializeContents();
        }

        private void InitializeContents()
        {
            SearchAndUpdateUI(SEARCH_TERM);
            txtSearch.Text = SEARCH_TERM;
        }

        private void SearchAndUpdateUI(string searchTerm)
        {
            var infoList = SQLiteDataAccess.GetProductSearch(searchTerm);
            _flowProducts.Controls.Clear();
            _flowProducts.SuspendLayout();
            if (!infoList.Any())
            {
                _flowProducts.ResumeLayout();
                return;
            }

            foreach (var product in infoList)
            {
                var newControl = new ProductOpenDialogItem();
                newControl.Product = product;
                newControl.BorderStyle = BorderStyle.FixedSingle;
                newControl.Font = new Font("Lucida Sans Unicode", 11F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
                newControl.Location = new Point(5, 194);
                newControl.Margin = new Padding(5, 4, 5, 4);
                newControl.Name = $"productCtrl_{product.Id}";
                newControl.Size = new Size(_flowProducts.Width - 20, 48);
                newControl.ProductItemSelected += ProductSelected;
                _flowProducts.Controls.Add(newControl);
            }
            _flowProducts.ResumeLayout(true);
        }

        private void ProductSelected(object sender, ProductOpenDialogItem.ProductItemSelectedEventArgs e)
        {
            var selection = sender as ProductOpenDialogItem;
            if (selection != null) return;
            _selectedProduct = selection.Product;

            if (!e.DoubleClicked)
            {
                lblProductName.Text = $"{_selectedProduct.Name} by {_selectedProduct.Creator}";
            }
            else
            {
                OK(sender, new EventArgs());
            }
        }

        private void OK(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void Cancel(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            SEARCH_TERM = txtSearch.Text;
            SearchAndUpdateUI(SEARCH_TERM);
        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                SEARCH_TERM = txtSearch.Text;
                SearchAndUpdateUI(SEARCH_TERM);
            }
        }
    }
}
