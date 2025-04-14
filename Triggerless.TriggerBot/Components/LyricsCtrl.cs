using DSharpPlus;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
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
                picProductImage.Image?.Dispose();

                // this flaky logic is to overcome that animated GIFs
                // will cause a GDI+ error, we have to convert to a PNG
                // first before assigning it to the PictureBox

                byte[] gifBytes = "GIF89ad".ToArray().Select(x => (byte)x).ToArray();
                byte[] imgBytes = _product.ImageBytes.Take(gifBytes.Length).ToArray();
                bool isAniGif = gifBytes.Zip(imgBytes, (x, y) => x == y)
                    .Aggregate(true, (acc, next) => acc && next);

                var tempMS = new MemoryStream(_product.ImageBytes);
                Image tempImage = tempImage = Image.FromStream(tempMS);

                picProductImage.Image = isAniGif ? ExtractFirstFrame(_product.ImageBytes) : tempImage;
                tempMS.Dispose();

                InitProduct();
            }
        }

        private Image ExtractFirstFrame(byte[] imageBytes)
        {
            using (var ms = new MemoryStream(imageBytes))
            using (var original = Image.FromStream(ms))
            {
                // Select the first frame if it's animated
                var dimension = new FrameDimension(original.FrameDimensionsList[0]);
                original.SelectActiveFrame(dimension, 0);

                // Create a new Bitmap with a non-indexed pixel format
                var firstFrame = new Bitmap(original.Width, original.Height, PixelFormat.Format32bppArgb);

                using (var g = Graphics.FromImage(firstFrame))
                {
                    g.DrawImage(original, 0, 0, original.Width, original.Height);
                }

                return firstFrame;
            }
        }

        private void InitProduct()
        {
            // throw new NotImplementedException();

            // See if we already have the MP3 for this product

            // If not, Grab the OGG files for this product

            // and reassemble them to create a temporary MP3 file using ffmpeg

            // Initialize the audio player control

            // Initialize the lyrics grid 

            // Save a copy of the grid state, in case this doesn't get saved

            // Place lyric markers from the grid data on the audio player

            // Clear the Undo stack


        }
    }
}
