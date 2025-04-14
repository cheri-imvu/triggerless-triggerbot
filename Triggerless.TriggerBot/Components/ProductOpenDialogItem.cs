using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Triggerless.TriggerBot
{
    public partial class ProductOpenDialogItem : UserControl
    {
        private bool _selected = false;
        private ProductDisplayInfo _product;
        public event EventHandler ItemDoubleClicked;

        public ProductDisplayInfo Product 
        { 
            get => _product;
            set 
            { 
                _product = value;
                if (_product != null)
                {
                    _productName.Text = $"{_product.Name} by {_product.Creator}";
                }
                else
                {
                    _productName.Text = "(Unknown) by (Unknown)";
                }
            }
        }
        public bool Selected
        {
            get { return _selected; }
            set
            {
                _selected = value;
                this.BackColor = _selected ? SystemColors.MenuHighlight : SystemColors.Control;
                if (_selected)
                {
                    var parent = this.Parent;
                    foreach (ProductOpenDialogItem sibling in parent.Controls)
                    {
                        if (sibling != null && sibling != this)
                        {
                            sibling.Selected = false;
                        }
                    }
                    FireProductSelected();
                }
            }
        }

        public ProductOpenDialogItem()
        {
            InitializeComponent();
        }

        private void MouseDownAnywhere(object sender, MouseEventArgs e)
        {
            Selected = true;
        }

        #region Event Handling
        public class ProductItemSelectedEventArgs : EventArgs
        {
            public ProductDisplayInfo Product { get; private set; }

            public ProductItemSelectedEventArgs(ProductDisplayInfo product)
            {
                this.Product = product;
            }
        }
        public delegate void ProductItemSelectedHandler(object sender, ProductItemSelectedEventArgs e);
        public event ProductItemSelectedHandler ProductItemSelected;
        private void FireProductSelected()
        {
            if (ProductItemSelected != null)
            {
                ProductItemSelected(this, new ProductItemSelectedEventArgs(_product));
            }
        }

        #endregion

        private void HandleDoubleClick(object sender, EventArgs e)
        {
            Selected = true;
            ItemDoubleClicked?.Invoke(this, EventArgs.Empty);
        }

        private void HandleClick(object sender, EventArgs e)
        {
            this.Selected = true;
        }
    }

}
