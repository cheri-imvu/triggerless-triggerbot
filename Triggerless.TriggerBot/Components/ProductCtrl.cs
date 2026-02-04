using System;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Policy;
using System.Windows.Forms;
using Triggerless.TriggerBot.Forms;

namespace Triggerless.TriggerBot
{


    public partial class ProductCtrl : UserControl
    {
        public event LinkClickedEventHandler OnDeckLinkClicked;
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

        public event LinkClickedEventHandler OnTagLinkClicked;

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
                            picProductImage.Image = null;
                            picProductImage.Image = bmp;
                        }
                            
                    }

                    if (value.Triggers != null && value.Triggers.Count > 0)
                    {
                        var triggers = value.Triggers.OrderBy(t => t.Sequence).Select(t => t.Trigger).ToArray();
                        var firstTrigger = triggers.FirstOrDefault();
                        var lastTrigger = triggers.LastOrDefault();
                        lblTriggers.Text = $"Triggers: {firstTrigger} - {lastTrigger}";
                    }

                    picLips.Visible = value.HasLyrics;
                }
                _productInfo = value;
            }
        }

        private void linkOnDeck_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                var args = new LinkClickedEventArgs(_productInfo);
                OnDeckLinkClicked?.Invoke(sender, args);
            }
        }

        private void ShowWebPage(object sender, EventArgs e)
        {

        }

        private void WearItem(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (_productInfo == null) return;
            OnWearItem?.Invoke(this, new WearItemEventArgs(_productInfo.Id));
        }

        private void ProductImageClicked(object sender, EventArgs e)
        {
            var pictureBox = sender as PictureBox;
            if (pictureBox == null) return;

            var productControl = pictureBox.Parent as ProductCtrl; 
            if (productControl == null) return;

            var pdi = productControl.ProductInfo;
            var url = $"https://www.imvu.com/shop/product.php?products_id={pdi.Id}";
            Process.Start(url); 

        }

        private void ExcludeSong(object sender, EventArgs e)
        {
            var productCtrl = this as ProductCtrl;
            var productId = this.ProductInfo.Id;
            var title = this.ProductInfo.Name;

            if (OnExcludeSong != null) OnExcludeSong(this, new ExcludeSongEventArgs(productId, title));
        }

        public class ExcludeSongEventArgs: EventArgs
        {
            public long ProductId { get; set; }
            public string Title { get; set; }   

            public ExcludeSongEventArgs(long id, string title)
            {
                ProductId = id;
                Title = title;
            }
        }

        public delegate void ExcludeSongEventHandler(object sender, ExcludeSongEventArgs e);
        public event ExcludeSongEventHandler OnExcludeSong;

        private void LinkEnter(object sender, EventArgs e)
        {
            var link = sender as LinkLabel;
            if (link == null) return;
            link.BackColor = Color.Black;
            link.ForeColor = Color.LimeGreen;
            link.VisitedLinkColor = link.ForeColor;
        }

        private void LinkLeave(object sender, EventArgs e)
        {
            var link = sender as LinkLabel;
            if (link == null) return;
            link.BackColor = Color.Transparent;
            link.ForeColor = Color.Aqua;
            link.VisitedLinkColor = link.ForeColor;
        }

        private void linkLabelTag_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                var args = new LinkClickedEventArgs(_productInfo);
                OnTagLinkClicked?.Invoke(sender, args);
            }
        }

        private void lblName_MouseEnter(object sender, EventArgs e)
        {
            lblName.BackColor = Color.Black;
            lblName.ForeColor = Color.Yellow;
        }

        private void lblName_MouseLeave(object sender, EventArgs e)
        {
            lblName.BackColor = Color.FromArgb(0, 29, 51);
            lblName.ForeColor = Color.FromArgb(224, 224, 224);
        }

        private void lblName_Click(object sender, EventArgs e)
        {
            var f = new RenameSongForm();
            f.ProductInfo = _productInfo;
            var dr = f.ShowDialog();
            if (dr == DialogResult.OK)
            {
                lblName.Text = f.ProductInfo.Name;
                SQLiteDataAccess.RenameSong(f.ProductInfo.Id, f.ProductInfo.Name);
            }
        }
    }
}
