using System.Windows.Forms;

namespace Triggerless.TriggerBot.Components
{
    public partial class AutoDJListItem : UserControl
    {
        private ProductDisplayInfo _product;
        public AutoDJListItem()
        {
            InitializeComponent();
        }

        public AutoDJListItem(ProductDisplayInfo product)
        {
            InitializeComponent();
            Product = product;
        }

        public ProductDisplayInfo Product
        {
            get => _product;
            set
            {
                _product = value;
                if (_product != null)
                {
                    lblSongTitle.Text = _product.Name;
                    lblCreatorName.Text = _product.Creator;
                }
                else
                {
                    lblCreatorName.Text = string.Empty;
                    lblCreatorName.Text = string.Empty;
                }
            }
        }
    }
}
