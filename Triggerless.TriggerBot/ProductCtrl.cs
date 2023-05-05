using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Triggerless.TriggerBot
{
    public partial class ProductCtrl : UserControl
    {
        public ProductCtrl()
        {
            InitializeComponent();
        }

        private ProductDisplayInfo _productInfo;

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
    }
}
