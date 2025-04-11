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

        public ProductDisplayInfo Product 
        { 
            get => _product;
            set 
            { 
                _product = value;
                if (_product != null)
                {
                    _productName.Text = $"{_product.Name} by {_product.Creator}";
                    using (var s = new MemoryStream(_product.ImageBytes))
                    {
                        _productImage.Image = Image.FromStream(s);
                    }
                }
                else
                {
                    _productName.Text = "(Unknown) by (Unknown)";
                    _productImage.Image?.Dispose();
                    _productImage.Image = null;
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
                }
                FireItemSelectedEvent(false);
            }
        }

        public ProductOpenDialogItem()
        {
            InitializeComponent();
            this.Disposed += OnDispose; // why you make me do this, studio?
        }

        private void OnDispose(object sender, EventArgs e)
        {
            _productImage.Image?.Dispose();
            base.Dispose();
        }

        private void MouseDownAnywhere(object sender, MouseEventArgs e)
        {
            Selected = true;
        }

        #region Event Handling
        public class ProductItemSelectedEventArgs : EventArgs
        {
            public ProductDisplayInfo Product { get; private set; }
            public bool DoubleClicked { get; private set; }
            public ProductItemSelectedEventArgs(ProductDisplayInfo product, bool doubleClicked)
            {
                this.Product = product;
                DoubleClicked = doubleClicked;
            }
        }
        public delegate void ProductItemSelectedHandler(object sender, ProductItemSelectedEventArgs e);
        public event ProductItemSelectedHandler ProductItemSelected;
        private void FireItemSelectedEvent(bool doubleClicked)
        {
            ProductItemSelected?.Invoke(this, new ProductItemSelectedEventArgs(_product, doubleClicked));
        }

        #endregion

        private void HandleDoubleClick(object sender, EventArgs e)
        {
            FireItemSelectedEvent(true);
        }
    }

}
