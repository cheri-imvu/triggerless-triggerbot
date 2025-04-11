using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Triggerless.TriggerBot.Components
{
    public partial class LyricsCtrl : UserControl
    {
        private ProductDisplayInfo _product;
        public LyricsCtrl()
        {
            InitializeComponent();
        }

        private void LyricsCtrl_Load(object sender, EventArgs e)
        {

        }

        private void btnSelectProduct_Click(object sender, EventArgs e)
        {
            var dlg = new ProductOpenDialog();
            var result = dlg.ShowDialog();
            if (result == DialogResult.OK && dlg.SelectedProduct != null)
            {
                _product = dlg.SelectedProduct;
                lblProductName.Text = _product.Name;
                lblCreatorName.Text = _product.Creator;
                using (var ms = new MemoryStream(_product.ImageBytes))
                {
                    picProductImage.Image?.Dispose();
                    picProductImage.Image = Image.FromStream(ms);
                }
                InitProduct();
            }
        }

        private void InitProduct()
        {
            throw new NotImplementedException();
        }
    }
}
