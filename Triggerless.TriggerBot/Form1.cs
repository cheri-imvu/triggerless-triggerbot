using Dapper;
using System;
using System.Collections.Generic;
using System.Drawing;
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

        private void ProductLinkClicked(object sender, ProductCtrl.LinkClickedEventArgs args)
        {
            var msg = $"Product Name is {args.ProductDisplayInfo.Name}";
            MessageBox.Show(msg);
        }

        private async void ScanInventory(object sender, EventArgs e)
        {
            pnlCollector.BringToFront();
            btnSearch.Enabled = false;
            var c = new Collector();
            c.CollectorEvent += new Collector.CollectorEventHandler(C_CollectorEvent);
            await c.ScanDatabasesAsync();
            btnSearch.Enabled = true;
            pnlCollector.SendToBack();
            DoSearch(null, null);
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
                       ORDER BY p.product_id, pt.sequence;";

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

        private void DoSearch(object sender, EventArgs e)
        {
            flowDisplay.Controls.Clear();
            flowDisplay.SuspendLayout();
            var productInfos = new List<ProductDisplayInfo>();

            long currentProductId = 0;
            ProductDisplayInfo currentInfo = null;
            List<dynamic> queryList = null;
            var infoList = new List<ProductDisplayInfo>();

            var sda = new SQLiteDataAccess();
            string andClause = string.Empty;
            string limitClause = string.Empty;

            if (string.IsNullOrEmpty(txtSearch.Text))
            {
                limitClause = "LIMIT 500";
            } 
            else
            {
                var search = txtSearch.Text.Trim();
                andClause = $@" AND (
                    p.title LIKE '%{search}%' OR
                    p.creator LIKE '%{search}%' OR
                    pt.prefix LIKE '%{search}%'
                 )";
            }

            var sql = $@"SELECT p.product_id AS ProductId,
                       p.title AS Name,
                       p.creator AS Creator,
                       p.image_bytes AS ImageBytes,
                       pt.prefix AS Prefix,
                       pt.sequence AS Sequence,
                       pt.trigger AS Trigger,
                       pt.length_ms AS LengthMS
                       FROM products p 
                       INNER JOIN product_triggers pt ON (p.product_id = pt.product_id)
                       WHERE p.has_ogg = 1
                        {andClause}
                       ORDER BY p.product_id DESC, pt.sequence ASC
                        {limitClause}
                        ;";

            using (var cxnAppCache = sda.GetAppCacheCxn())
            {
                queryList = cxnAppCache.Query(sql).ToList();
            }

            if (!queryList.Any()) return;

            foreach (var query in queryList)
            {
                if (currentProductId != query.ProductId)
                {
                    if (currentInfo != null)
                    {
                        infoList.Add(currentInfo);
                    }
                    currentProductId = query.ProductId;
                    currentInfo = new ProductDisplayInfo();
                    currentInfo.Id = query.ProductId;
                    currentInfo.Name = query.Name;
                    currentInfo.ImageBytes = query.ImageBytes;
                    currentInfo.Creator = query.Creator;
                }
                var triggerInfo = new TriggerDisplayInfo();
                triggerInfo.Prefix = query.Prefix;
                triggerInfo.Sequence = (int)query.Sequence;
                triggerInfo.LengthMS = query.LengthMS;
                triggerInfo.Trigger = query.Trigger;
                triggerInfo.ProductId = query.ProductId;
                currentInfo.Triggers.Add(triggerInfo);
            }
            if (currentInfo != null && !string.IsNullOrEmpty(andClause)) infoList.Add(currentInfo);

            foreach (var info in infoList)
            {
                var newControl = new ProductCtrl();
                newControl.BorderStyle = BorderStyle.FixedSingle;
                newControl.Font = new Font("Lucida Sans Unicode", 11F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
                newControl.Location = new Point(5, 194);
                newControl.Margin = new Padding(5, 4, 5, 4);
                newControl.Name = $"productCtrl_{info.Id}";
                newControl.ProductInfo = info;
                newControl.Size = new Size(364, 87);
                newControl.OnLinkClicked += ProductLinkClicked;
                flowDisplay.Controls.Add(newControl);
            }
            flowDisplay.ResumeLayout(true);
        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Return) && btnSearch.Enabled)
            {
                DoSearch(null, null);
                e.Handled = true;
            }

            if (e.KeyChar == Convert.ToChar(Keys.Escape) && btnSearch.Enabled)
            {
                txtSearch.Text = string.Empty; 
                e.Handled = true;
            }
        }
    }
}
