using Dapper;
using System;
using System.Linq;
using System.Windows.Forms;

namespace Triggerless.TriggerBot
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private async void ScanInventory(object sender, EventArgs e)
        {
            var c = new Collector();
            c.CollectorEvent += new Collector.CollectorEventHandler(C_CollectorEvent);
            await c.ScanDatabasesAsync();
        }

        private void C_CollectorEvent(object sender, Collector.CollectorEventArgs e)
        {
            if (InvokeRequired)
            {
                Collector.CollectorEventHandler cb = new Collector.CollectorEventHandler(C_CollectorEvent);
                Invoke(cb, new object[] {sender, e });
                return;
            }

            if (e.TotalProducts > 0) 
            {
                progScan.Maximum = e.TotalProducts;
                progScan.Value = e.CompletedProducts;
                lblProgress.Text = $"Progress: {e.CompletedProducts}/{e.TotalProducts}";
                lblProduct.Text = $"Product: {e.Message}";
                progScan.Update();
                lblProduct.Update();
                lblProgress.Update();
            }
        }

        
        long _imageTestCounter = 0;
        long[] _ids = null;
        private void button1_Click(object sender, EventArgs e)
        {
            var sda = new SQLiteDataAccess();
            using (var cxn = sda.GetAppCacheCxn())
            {
                if (_ids == null) _ids = cxn.Query<long>("SELECT product_id from products where has_ogg = 1").ToArray();
                var productId = _ids[_imageTestCounter];
                lblProdId.Text = productId.ToString();
                _imageTestCounter = (_imageTestCounter + 1) % _ids.Length;
                
                {
                    var sql = $@"SELECT p.product_id,
                       p.title,
                       p.creator,
                       p.image_bytes,
                       pt.prefix,
                       pt.sequence,
                       pt.trigger,
                       pt.length_ms
                       FROM products p 
                       INNER JOIN product_triggers pt ON (p.product_id = pt.product_id)
                       WHERE p.product_id = {productId}
                       ORDER BY pt.sequence;";

                    var query = cxn.Query(sql);
                    var productInfo = new ProductDisplayInfo();
                    foreach (var item in query)
                    {
                        if (string.IsNullOrEmpty(productInfo.Name))
                        {
                            productInfo.Name = item.title;
                            productInfo.Creator = item.creator;
                            productInfo.ImageBytes = item.image_bytes;
                            productInfo.Id = item.product_id;
                        }
                        productInfo.Triggers.Add(new TriggerDisplayInfo
                        {
                            LengthMS = (double)item.length_ms,
                            Prefix = item.prefix,
                            ProductId = (long)item.product_id,
                            Sequence = (int)item.sequence,
                            Trigger = item.trigger
                        });
                    }

                    productCtrl1.ProductInfo = productInfo;
                }
            }
        }
    }
}
