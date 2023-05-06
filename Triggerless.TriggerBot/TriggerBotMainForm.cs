using Dapper;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using ManagedWinapi.Windows;

namespace Triggerless.TriggerBot
{
    public partial class TriggerBotMainForm : Form
    {
        private bool _hush = true;
        private IntPtr _imvuMainWindow = IntPtr.Zero;
        private IntPtr _imvuChatWindow = IntPtr.Zero;
        private ProductDisplayInfo _currentProductInfo = null;
        private bool _isPlaying = false;
        public TriggerBotMainForm()
        {
            InitializeComponent();
        }

        private bool CheckForImvu()
        {
            _imvuChatWindow = IntPtr.Zero;
            Process[] p = Process.GetProcesses();
            Process imvuProc = null;
            foreach (var proc in p)
            {
                if (proc.ToString().ToLower().Contains("imvuclient"))
                {
                    imvuProc = proc;
                    break;
                }
            }
            if (imvuProc == null)
            {
                if (!_hush) MessageBox.Show("TriggerBot won't work if IMVU isn't running.", 
                    "IMVU isn't running", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            _imvuMainWindow = imvuProc.MainWindowHandle;

            var sysWindow = new SystemWindow(_imvuMainWindow);
            var descendants = sysWindow.AllDescendantWindows;
            foreach (var descendant in descendants)
            {
                if (descendant.ClassName == "ImvuNativeWindow" && descendant.Title == "Floating Tool")
                {
                    _imvuChatWindow = descendant.Parent.HWnd;
                    break;
                }
            }

            if (_imvuChatWindow == IntPtr.Zero)
            {
                MessageBox.Show("TriggerBot won't work if you're not in a chat room",
                    "IMVU isn't running", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private void ProductLinkClicked(object sender, ProductCtrl.LinkClickedEventArgs args)
        {
            productOnDeck.ProductInfo = args.ProductDisplayInfo;
            productOnDeck.Visible = true;
            btnEjectFromDeck.Enabled = true;
            btnLoadToPlaying.Enabled = true;
        }

        private async void ScanInventory(object sender, EventArgs e) //Form1.Shown
        {
            pnlCollector.BringToFront();
            btnSearch.Enabled = false;
            await _collector.ScanDatabasesAsync();
            btnSearch.Enabled = true;
            pnlCollector.SendToBack();
            DoSearch(null, null);
            CheckForImvu();
        }

        private void OnCollectorEvent(object sender, Collector.CollectorEventArgs e)
        {
            if (InvokeRequired)
            {
                Collector.CollectorEventHandler cb = new Collector.CollectorEventHandler(OnCollectorEvent);
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

        private void button1_Click(object sender, EventArgs e)
        {
        }

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool BringWindowToTop(IntPtr hWnd);

        private void RelocateBanner(object sender, EventArgs e)
        {
            var sizePanel = pnlBanner.Size;
            var sizeBanner = picBanner.Size;
            var left = (sizePanel.Width - sizeBanner.Width)/2;
            picBanner.Location = new Point(left, picBanner.Location.Y);
        }

        private void MoveToPlaying(object sender, EventArgs e)
        {
            if (!_isPlaying && (CheckForImvu() || _hush))
            {
                productOnDeck.Visible = false;
                btnLoadToPlaying.Enabled = false;
                btnEjectFromDeck.Enabled = false;
                _currentProductInfo = productOnDeck.ProductInfo;
                lblNowPlaying.Text = $"\"{_currentProductInfo.Name}\" by {_currentProductInfo.Creator}";
                FillTriggerGrid();
            }

        }

        private void RemoveFromDeck(object sender, EventArgs e)
        {
            productOnDeck.Visible = false;
            btnLoadToPlaying.Enabled = false;
            btnEjectFromDeck.Enabled = false;
        }

        private void FillTriggerGrid()
        {
            gridTriggers.Rows.Clear();
            if (_currentProductInfo != null)
            {
                foreach (var trigger in _currentProductInfo.Triggers)
                {
                    gridTriggers.Rows.Add(trigger.Trigger, (trigger.LengthMS / 1000).ToString("0.000"));
                }
                foreach (DataGridViewRow row in gridTriggers.Rows) row.Selected = false;
                if (gridTriggers.Rows.Count > 0) gridTriggers.Rows[0].Selected = true;
            }
        }
    }
}
