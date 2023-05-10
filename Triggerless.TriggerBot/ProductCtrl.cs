using System;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Windows.Forms;

namespace Triggerless.TriggerBot
{


    public partial class ProductCtrl : UserControl
    {
        public event LinkClickedEventHandler OnLinkClicked;
        public delegate void LinkClickedEventHandler(object sender, LinkClickedEventArgs e);
        public class LinkClickedEventArgs : EventArgs
        {
            public LinkClickedEventArgs(ProductDisplayInfo pdi) 
            {
                ProductDisplayInfo = pdi;
            }

            public ProductDisplayInfo ProductDisplayInfo { get; set; } 
        }

        public event WearItemEventHandler OnWearItem;
        public delegate void WearItemEventHandler(object sender, WearItemEventArgs e);
        public class WearItemEventArgs : EventArgs
        {

            public WearItemEventArgs(long productId)
            {
                ProductId = productId;
            }
            public long ProductId { get; set; }
        }



        public ProductCtrl()
        {
            InitializeComponent();
        }

        private ProductDisplayInfo _productInfo;
        private bool _hideOnDeck;
        public bool HideOnDeck
        {
            get { return _hideOnDeck; } 
            set {
                linkOnDeck.Visible = !value;
                _hideOnDeck = value;
            }
        }

        public ProductDisplayInfo ProductInfo
        {
            get { return _productInfo; }
            set { 
                if (value != null)
                {
                    lblName.Text = value.Name;
                    lblCreator.Text = "by " + value.Creator;
                    if (value.ImageBytes != null && value.ImageBytes.Length > 10)
                    {
                        Image bmp = null;
                        var converter = new ImageConverter();
                        if (converter.CanConvertFrom(typeof(byte[])))
                        {
                            bmp = (Bitmap)((new ImageConverter()).ConvertFrom(value.ImageBytes));
                            picProductImage.BackgroundImage = null;
                            picProductImage.BackgroundImage = bmp;
                        }
                            
                    }

                    if (value.Triggers != null && value.Triggers.Count > 0)
                    {
                        var triggers = value.Triggers.OrderBy(t => t.Sequence).Select(t => t.Trigger).ToArray();
                        var firstTrigger = triggers.FirstOrDefault();
                        var lastTrigger = triggers.LastOrDefault();
                        lblTriggers.Text = $"Triggers: {firstTrigger} - {lastTrigger}";
                    }
                }
                _productInfo = value;
            }
        }

        private void linkOnDeck_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                var args = new LinkClickedEventArgs(_productInfo);
                OnLinkClicked?.Invoke(sender, args);
            }
        }

        private void ShowWebPage(object sender, EventArgs e)
        {
            if (_productInfo != null)
            {
                var uri = $"https://www.imvu.com/shop/product.php?products_id={_productInfo.Id}";
                var psi = new System.Diagnostics.ProcessStartInfo();
                psi.UseShellExecute = true;
                psi.FileName = uri;
                System.Diagnostics.Process.Start(psi);
            }
        }

        private void WearItem(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (_productInfo == null) return;
            OnWearItem?.Invoke(this, new WearItemEventArgs(_productInfo.Id));
        }
    }
}
