

using Dapper;
using ManagedWinapi.Windows;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Triggerless.TriggerBot.Forms;
using WindowsInput;
using static Triggerless.TriggerBot.ProductCtrl;
using KeyCode = WindowsInput.Native.VirtualKeyCode;

namespace Triggerless.TriggerBot
{
    public partial class TriggerBotMainForm : Form
    {
        private Update _updater;
        private IInputSimulator _sim = new InputSimulator();

        public TriggerBotMainForm()
        {
            InitializeComponent();
            _updater = new Update();
        }

        #region IMVU Presence and Interation
        // IMVU presence
        private bool _hush = false;
        private IntPtr _imvuMainWindow = IntPtr.Zero;
        private IntPtr _imvuChatWindow = IntPtr.Zero;

        // IMVU detection and interaction
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool BringWindowToTop(IntPtr hWnd);

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
                if (!_hush) MessageBox.Show("TriggerBot can't play the triggers if IMVU isn't running. LOL", 
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

        private void WearItem(object sender, WearItemEventArgs e)
        {
            if (_imvuChatWindow == IntPtr.Zero) return;
            var line = $" *use {e.ProductId}";
            DispatchText(line);

        }
#endregion

        #region Inventory Update
        // Inventory Update
        private async void ScanInventory(object sender, EventArgs e) //TechSupport.Shown
        {
            tabAppContainer.SelectedTab = tabPlayback;
            pnlCollector.BringToFront();
            btnSearch.Enabled = false;
            //await _collector.ScanDatabasesSync();
            _collector.ScanDatabasesSync();
            btnSearch.Enabled = true;
            pnlCollector.SendToBack();
            DoSearch(null, null);
            CheckForImvu();
        }

        // Inventory Update
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
        #endregion

        #region Product Search
        // Product Search
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
                var search = txtSearch.Text.Trim().Replace("'", "''");
                if (search.ToLower() == "triggerboss")
                {
                    _splicer.ShowCheap();
                    Properties.Settings.Default.InstallationType = "triggerboss";
                }
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
                       pt.length_ms AS LengthMS,
                       pt.addn_triggers AS AddnTriggers
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

            if (!queryList.Any())
            {
                flowDisplay.ResumeLayout(true);
                return;
            }

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
                triggerInfo.AddnTriggers = query.AddnTriggers;
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
                newControl.OnDeckLinkClicked += SendToDeck;
                newControl.OnWearItem += WearItem;
                newControl.OnExcludeSong += ExcludeSong;
                
                flowDisplay.Controls.Add(newControl);
            }
            flowDisplay.ResumeLayout(true);
        }

        private void ExcludeSong(object sender, ExcludeSongEventArgs e)
        {
            var msg = $"Are you sure you want to remove {e.Title} from search? Nothing will be deleted from your IMVU inventory.";
            var result = MessageBox.Show(msg, "Remove Tune?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.No) return;

            if (_collector.ExcludeSong(e.ProductId))
            {
                flowDisplay.Controls.RemoveByKey($"productCtrl_{e.ProductId}");
            } else
            {
                MessageBox.Show($"Unable to remove {e.Title} at this time", "Database Glitch", MessageBoxButtons.OK);
            }
            //throw new NotImplementedException();
        }

        // Product Search
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
        #endregion

        #region GUI 
        private void LoadForm(object sender, EventArgs e)
        {
            Text = $"Triggerless Triggerbot {Shared.VersionNumber}";
            lblVersion.Text = $"Version {Shared.VersionNumber}";
            lblCopyright.Text = Shared.Copyright;
            Shared.CheckIfPaid();
            _updater.CheckForUpdate();
            LoadSettings();
        }

        private void LoadSettings()
        {
            var sets = Properties.Settings.Default;
            txtSearch.Text = sets.LastSearch;
            DoSearch(null, null);

            chkHideTriggers.Checked = sets.HideTriggers;
            chkMinimizeOnPlay.Checked = sets.MinimizeOnPlay;
            chkKeepOnTop.Checked = sets.KeepOnTop;
            _lagMS = sets.InitialLagMS;
            trackBarLag.Value = LagMsToTrackBarValue();
            _splicer.AudioLength = sets.DefaultTriggerLength;
        }

        private void TriggerBotMainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            var sets = Properties.Settings.Default;
            sets.KeepOnTop = chkKeepOnTop.Checked;
            sets.MinimizeOnPlay = chkMinimizeOnPlay.Checked;
            sets.HideTriggers = chkHideTriggers.Checked;
            sets.DefaultTriggerLength = 18;
            sets.InitialLagMS = _lagMS;
            sets.LastSearch = txtSearch.Text;
            sets.DefaultTriggerLength = _splicer.AudioLength;
            sets.Save();

            if (_isPlaying)
            {
                var result = MessageBox.Show("You're playing a tune, are you sure?",
                    "Exit Program?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.No)
                {
                    e.Cancel = true;
                    return;
                }
                CleanupAfterPlay();
            }

            var reasons = new CloseReason[] {
                CloseReason.WindowsShutDown,
                CloseReason.TaskManagerClosing,
                CloseReason.ApplicationExitCall
            };
            if (reasons.Contains(e.CloseReason)) return;
            _updater.RunSetupFileIfRequired();
        }

        private void RelocateBanner(object sender, EventArgs e)
        {
            var sizePanel = pnlBanner.Size;
            var sizeBanner = picBanner.Size;
            var left = (sizePanel.Width - sizeBanner.Width)/2;
            picBanner.Location = new Point(left, picBanner.Location.Y);
        }

        private void chkStayOnTop_Clicked(object sender, EventArgs e)
        {
            this.TopMost = chkKeepOnTop.Checked;
        }

        #endregion

        #region Product Selection
        // Product Selection
        private void MoveToPlaying(object sender, EventArgs e)
        {
            if (!_isPlaying && (CheckForImvu() || _hush))
            {
                productOnDeck.Visible = false;
                btnLoadToPlaying.Enabled = false;
                btnEjectFromDeck.Enabled = false;
                btnPlay.Enabled = true;
                _currProductInfo = productOnDeck.ProductInfo;
                _currTriggerIndex = 0;
                lblNowPlaying.Text = $"\"{_currProductInfo.Name}\" by {_currProductInfo.Creator}";
                FillTriggerGrid();
                trackBarLag.Value = LagMsToTrackBarValue();
                lblLag.Text = _lagMS.ToString("0.00");
                pnlLag.Visible = true;
                cboAdditionalTriggers.Text = _currProductInfo.Triggers[_currTriggerIndex].AddnTriggers;
            }

        }

        // Product Selection
        private void RemoveFromDeck(object sender, EventArgs e)
        {
            productOnDeck.Visible = false;
            btnLoadToPlaying.Enabled = false;
            btnEjectFromDeck.Enabled = false;
        }

        // Product Selection
        private void FillTriggerGrid()
        {
            gridTriggers.Rows.Clear();
            if (_currProductInfo != null)
            {
                foreach (var trigger in _currProductInfo.Triggers)
                {
                    gridTriggers.Rows.Add(trigger.Trigger, (trigger.LengthMS / 1000).ToString("0.000"));
                }
                foreach (DataGridViewRow row in gridTriggers.Rows) row.Selected = false;
                if (gridTriggers.Rows.Count > 0) gridTriggers.Rows[0].Selected = true;
            }
        }

        // Product Selection
        private async void SendToDeck(object sender, ProductCtrl.LinkClickedEventArgs args)
        {
            // Sometimes LengthMS gets set to zero, no idea why. We need to double check the list
            var coll = new Collector();
            if (!await coll.Verify(args.ProductDisplayInfo))
            {
                MessageBox.Show("The data for this product cannot be verified.", "Bad Product Info", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            productOnDeck.ProductInfo = args.ProductDisplayInfo;
            productOnDeck.Visible = true;
            btnEjectFromDeck.Enabled = true;
            btnLoadToPlaying.Enabled = true;
        }
        #endregion

        #region Trigger Generation

        private ProductDisplayInfo _currProductInfo = null;
        private bool _isPlaying = false;
        private int _currTriggerIndex;
        private int _numberOfTriggers;
        private List<string> _usedAdditionals = new List<string>();

        private void DispatchText(string text)
        {
            if (_imvuChatWindow == IntPtr.Zero) return;
            BringWindowToTop(_imvuChatWindow);
            SetForegroundWindow(_imvuChatWindow);

            // Old Version
            //SendKeys.SendWait(text + "~");

            // New Version

            // get current clipboard text
            string currClipText = string.Empty;
            if (Clipboard.ContainsText())
            {
                currClipText = Clipboard.GetText();
            }
            _sim.Keyboard.KeyPress(KeyCode.HOME);
            _sim.Keyboard.ModifiedKeyStroke(KeyCode.SHIFT, KeyCode.END);
            _sim.Keyboard.ModifiedKeyStroke(KeyCode.CONTROL, KeyCode.VK_X);
            _sim.Keyboard.TextEntry(text);
            _sim.Keyboard.KeyPress(KeyCode.RETURN);
            if (Clipboard.ContainsText())
            {
                string prevText =  Clipboard.GetText();
                if (prevText != currClipText && !String.IsNullOrEmpty(prevText))
                {
                    _sim.Keyboard.TextEntry(prevText);
                }
            }
        }

        // Trigger Generation
        private void _triggerTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            _triggerTimer.Stop();
            Interlocked.Increment(ref _currTriggerIndex);
            if (_currTriggerIndex < _numberOfTriggers)
            {
                cboAdditionalTriggers.Text = _currProductInfo.Triggers[_currTriggerIndex].AddnTriggers;
                cboAdditionalTriggers.Update();
                PullTrigger();
                return;
            }
            CleanupAfterPlay();
        }

        public void PullTrigger()
        {
            _triggerTimer.Interval = _currProductInfo.Triggers[_currTriggerIndex].LengthMS - _lagMS;
            _triggerTimer.Start();
            _triggerStartTime = DateTime.Now;
            DispatchText(GetTriggerLine());

            _progressTimer.Stop();
            progTrigger.Value = 0;
            _progressTimer.Start();

            gridTriggers.Rows[_currTriggerIndex].Selected = true;
            lblCurrPlayingTrigger.Text = GetTriggerLine();
            if (!string.IsNullOrWhiteSpace(cboAdditionalTriggers.Text))
            {
                while (_usedAdditionals.Contains(cboAdditionalTriggers.Text))
                {
                    _usedAdditionals.Remove(cboAdditionalTriggers.Text);
                }
                _usedAdditionals.Insert(0, cboAdditionalTriggers.Text);
                cboAdditionalTriggers.Items.Clear();
                cboAdditionalTriggers.Items.AddRange(_usedAdditionals.ToArray());
            }
            cboAdditionalTriggers.Text = string.Empty;
        }

        private void StartPlayingClicked(object sender, EventArgs e)
        {
            StartPlaying();
        }

        private void StartPlaying(int currentTriggerIndex = 0)
        {
            if (_currProductInfo == null) // sanity check
            {
                MessageBox.Show("Playback Error, no trigger product selected");
                return;
            }
            _isPlaying = true;
            BringWindowToTop(_imvuChatWindow);
            _currTriggerIndex = currentTriggerIndex;
            _numberOfTriggers = _currProductInfo.Triggers.Count;
            PullTrigger();
            if (chkMinimizeOnPlay.Checked) WindowState = FormWindowState.Minimized;
            btnAbort.Enabled = true;
            btnPlay.Enabled = false;
        }

        private string GetTriggerLine()
        {
            var hideTriggers = chkHideTriggers.Checked;
            var hider = "*imvu:trigger ";
            bool hasAdditionalTriggers = !string.IsNullOrEmpty(cboAdditionalTriggers.Text);
            string result = string.Empty; // sometimes the first char gets cut off.
            if (hasAdditionalTriggers)
            {
                result = hideTriggers ? $"  {hider}{TrimTrigger()}" : $"  /{TrimTrigger()}";
                string[] addnTriggers = cboAdditionalTriggers.Text.Trim()
                    .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (addnTriggers.Length > 0)
                {
                    for (int i = 0; i < addnTriggers.Length; i++)
                    {
                        result += " ";
                        if (hideTriggers)
                        {
                            var addnTrigger = addnTriggers[i].Replace("/", "");
                            result += $"{hider}{addnTrigger}";
                        }
                        else
                        {
                            if (!addnTriggers[i].StartsWith("/")) result += "/";
                            result += addnTriggers[i];
                        }
                    }
                }
            }
            else
            {
                result = hideTriggers ? $"  {hider}{TrimTrigger()}" : $"  {TrimTrigger()}";
            }
            if (result.Contains("~")) result = result.Replace("~", "{~}");
            return result;
        }

        private string TrimTrigger()
        {
            var hideTriggers = chkHideTriggers.Checked;

            string result = _currProductInfo.Triggers[_currTriggerIndex].Trigger;
            if (hideTriggers) result = result.Replace("/", "");
            
            int commaPos = result.IndexOf(",");
            if (commaPos == -1) return result;
            return result.Substring(0, commaPos);
        }

        private void AbortPlaying(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to abort?", "Abort Trigger Play?", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                chkAutoCue.Checked = false;
                CleanupAfterPlay();
            }
        }

        private void CleanupAfterPlay()
        {
            _triggerTimer.Enabled = false;
            _progressTimer.Enabled = false;
            _isPlaying = false;
            _currProductInfo = null;
            gridTriggers.Rows.Clear();
            lblNowPlaying.Text = "--Pending--";
            btnPlay.Enabled = false;
            btnAbort.Enabled = false;
            pnlLag.Visible = false;
            lblCurrPlayingTrigger.Text = "--Pending--";
            progScan.Value = 0;

            if (productOnDeck.Visible && chkAutoCue.Checked)
            {
                MoveToPlaying(null, null);
                StartPlayingClicked(null, null);
                chkAutoCue.Checked = false;
            }
        }

        #region LagControl and TrackBar

        //---- LAG VALUES -----
        private const double LAG_BAR_START = 0;
        private const double LAG_BAR_END = 12;
        private const double LAG_MS_DEFAULT = 4;
        private const int LAG_TICKS_PER_MS = 4;

        private double _lagMS = LAG_MS_DEFAULT;
        private DateTime _triggerStartTime = DateTime.MinValue;

        private int LagMsToTrackBarValue()
        {
            int result = trackBarLag.Value;
            if (_lagMS < LAG_BAR_START || _lagMS > LAG_BAR_END) return result;
            result = (int)((_lagMS - LAG_BAR_START) * LAG_TICKS_PER_MS);

            return result;
        }

        private double TrackBarValueToLagMs()
        {
            double result = _lagMS;
            result = (double)trackBarLag.Value / LAG_TICKS_PER_MS + LAG_BAR_START;

            return result;
        }

        private void LagControlChanged(object sender, EventArgs e)
        {
            _lagMS = TrackBarValueToLagMs();
            lblLag.Text = _lagMS.ToString("0.00");
        }

        private void TriggerMadeProgress(object sender, EventArgs e)
        {
            var triggerMsecElapsed = (DateTime.Now - _triggerStartTime).TotalMilliseconds;
            var percentProgress = 100 * triggerMsecElapsed / _currProductInfo.Triggers[_currTriggerIndex].LengthMS;
            percentProgress = Math.Min(percentProgress, 100);
            progTrigger.Value = (int)Math.Round(percentProgress);
        }

        private void TrackBarInc(object sender, EventArgs e)
        {
            if (trackBarLag.Value >= trackBarLag.Maximum) return;
            trackBarLag.Value++;
        }

        private void TrackBarIncInc(object sender, EventArgs e)
        {
            if (trackBarLag.Value >= trackBarLag.Maximum) return;
            if (trackBarLag.Value > trackBarLag.Maximum - trackBarLag.LargeChange)
            {
                TrackBarInc(sender, e);
                return;
            }
            trackBarLag.Value += trackBarLag.LargeChange;
        }

        private void TrackBarDec(object sender, EventArgs e)
        {
            if (trackBarLag.Value <= trackBarLag.Minimum) return;
            trackBarLag.Value--;
        }

        private void TrackBarDecDec(object sender, EventArgs e)
        {
            if (trackBarLag.Value <= trackBarLag.Minimum) return;
            if (trackBarLag.Value < trackBarLag.Minimum + trackBarLag.LargeChange)
            {
                TrackBarDec(sender, e);
                return;
            }
            trackBarLag.Value -= trackBarLag.LargeChange;
        }

        #endregion

        #endregion

        
        private void RescanAll(object sender, EventArgs e)
        {
            var msg = "Are you sure you want to rescan? This will delete all Triggerbot data and scan the inventory and web all over again, and could take some time./n/nAre you certain?";
            var dlgResult = MessageBox.Show(msg, "Rescan All Data?", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            if (dlgResult == DialogResult.No) { return; }

            tabAppContainer.SelectedTab = tabPlayback;
            if (_collector.ClearAppCache())
            {
                ScanInventory(null, null);
            }


        }

        private void lnkPage_Clicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(lnkPage.Text);
        }

        private void AddnTriggersButtonClick(object sender, EventArgs e)
        {
            if (_currProductInfo == null) return;
            var modalForm = new AddnTriggersForm() { Product = _currProductInfo };
            modalForm.ShowDialog(this);
            cboAdditionalTriggers.Text = _currProductInfo.Triggers[_currTriggerIndex].AddnTriggers;

        }

        private void _splicer_Load(object sender, EventArgs e)
        {

        }

        private void gridTriggers_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int row = e.RowIndex; //header is -1, first row is 0
            if (row < 0) return;
            StartPlaying(row);
        }

        private async void btnDeepScan_Click(object sender, EventArgs e)
        {
            using (var f = new DeepScanForm())
            {
                f.Collector = _collector;
                var dlgResult = f.ShowDialog(this);
                if (dlgResult == DialogResult.OK)
                {
                    await _collector.DeepScanThese(f.SelectedProductIds);
                }
            }                
        }

        private void btnTechSupport_Click(object sender, EventArgs e)
        {
            using (var f = new TechSupport())
            {
                var dlgResult = f.ShowDialog(this);
            }
        }

        private void linkTimeItText_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (_currProductInfo != null)
            {
                var sb = new StringBuilder();
                double seconds = 0F;
                
                foreach (var trigger in _currProductInfo.Triggers)
                {
                    var line = $"<{seconds.ToString("0.000")}>*imvu:trigger {trigger.Trigger}";
                    sb.AppendLine(line);
                    seconds += trigger.LengthMS / 1000;
                }

                var appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                var targetFolder = Path.Combine(appData, "Triggerless", "Transfer", "Files");
                if (!Directory.Exists(targetFolder)) Directory.CreateDirectory(targetFolder);
                var guid = Guid.NewGuid().ToString("N").Substring(0, 10);
                var filename = guid + ".txt";
                var filepath = Path.Combine(targetFolder, filename);
                File.WriteAllText(filepath, sb.ToString());

                Process.Start(filepath);
                if (chkKeepOnTop.Checked) this.WindowState = FormWindowState.Minimized;
            }

        }

        private void btnViewLog_Click(object sender, EventArgs e)
        {
            var f = new LogForm();
            f.LogText = _collector.Log;
            f.TopMost = this.TopMost;
            f.ShowDialog();
        }
    }
}
