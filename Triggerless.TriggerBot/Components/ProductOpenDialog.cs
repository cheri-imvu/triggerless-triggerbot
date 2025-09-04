using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

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

            foreach (var product in infoList.OrderBy(p => p.Name.ToLower()))
            {
                var newControl = new ProductOpenDialogItem();
                newControl.Product = product;
                newControl.BorderStyle = BorderStyle.FixedSingle;
                newControl.Font = new Font("Liberation Sans", 11F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
                newControl.Location = new Point(5, 194);
                newControl.Margin = new Padding(3,3,3,3);
                newControl.Name = $"productCtrl_{product.Id}";
                newControl.Width = _flowProducts.Width - 20;
                newControl.ProductItemSelected += ProductSelected;
                newControl.ItemDoubleClicked += ItemDoubleClicked;
                if (product.HasLyrics) 
                {
                    newControl.BackColor = Color.LightSkyBlue;
                    newControl.ForeColor = Color.FromArgb(0, 29, 51);
                } 
                _flowProducts.Controls.Add(newControl);
            }
            _flowProducts.ResumeLayout(true);
        }

        private void ItemDoubleClicked(object sender, EventArgs e)
        {
            OK(this, new EventArgs());
        }

        private void ProductSelected(object sender, ProductOpenDialogItem.ProductItemSelectedEventArgs e)
        {
            _selectedProduct = e.Product;
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
                e.Handled = true;
            }
        }

        private void ProductOpenDialog_Load(object sender, EventArgs e)
        {
        }

        private void ProductOpenDialog_Shown(object sender, EventArgs e)
        {
            txtSearch.Focus();
        }

        private void pnlMiddle_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
