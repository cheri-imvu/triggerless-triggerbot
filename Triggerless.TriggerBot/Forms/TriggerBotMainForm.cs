using ManagedWinapi.Windows;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using Triggerless.TriggerBot.Components;
using Triggerless.TriggerBot.Forms;
using Triggerless.TriggerBot.Models;
using Triggerless.PlugIn;
using WindowsInput;
using static Triggerless.TriggerBot.ProductCtrl;
using KeyCode = WindowsInput.Native.VirtualKeyCode;
using System.Globalization;

namespace Triggerless.TriggerBot
{
    public partial class TriggerBotMainForm : Form
    {
        private Update _updater;
        private IInputSimulator _sim = new InputSimulator();

        public TriggerBotMainForm()
        {
            InitializeComponent();
            //HandleMonitor.LogHandles("after InitializeComponent");

            this.FormBorderStyle = FormBorderStyle.Sizable;
            _updater = new Update();
            linkDiscord.Text = Discord.GetInviteLink().Result;
        }

        #region Dark Mode Title Bar

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            ApplyImmersiveDarkAccordingToSystem();
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            const int WM_SETTINGCHANGE = 0x001A;
            const int WM_THEMECHANGED = 0x031A;

            if (m.Msg == WM_SETTINGCHANGE || m.Msg == WM_THEMECHANGED)
                ApplyImmersiveDarkAccordingToSystem();
        }

        private void ApplyImmersiveDarkAccordingToSystem()
        {
            bool useDark = Theme.IsAppsDarkMode();
            Theme.SetImmersiveDarkMode(this.Handle, useDark);
        }

        static class Theme
        {
            // Windows stores “apps theme” here: 0 = Dark, 1 = Light
            public static bool IsAppsDarkMode()
            {
                const string keyPath = @"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize";
                using (var key = Registry.CurrentUser.OpenSubKey(keyPath))
                {
                    var v = key?.GetValue("AppsUseLightTheme");
                    return (v is int i) ? (i == 0) : false; // default to Light if missing
                }
            }

            public static void SetImmersiveDarkMode(IntPtr hwnd, bool enabled)
            {
                if (hwnd == IntPtr.Zero) return;
                int on = enabled ? 1 : 0;

                // Win10 1809 used 19; Win10 1903+/Win11 use 20. Try both, ignore failures.
                DwmSetWindowAttribute(hwnd, (DWMWINDOWATTRIBUTE)20, ref on, sizeof(int));
                DwmSetWindowAttribute(hwnd, (DWMWINDOWATTRIBUTE)19, ref on, sizeof(int));
            }

            private enum DWMWINDOWATTRIBUTE
            {
                DWMWA_USE_IMMERSIVE_DARK_MODE_BEFORE_20 = 19,
                DWMWA_USE_IMMERSIVE_DARK_MODE = 20
            }

            [DllImport("dwmapi.dll")]
            private static extern int DwmSetWindowAttribute(
                IntPtr hwnd, DWMWINDOWATTRIBUTE attribute, ref int pvAttribute, int cbAttribute);
        }
        #endregion

        #region IMVU Presence and Interation

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool IsWindow(IntPtr hWnd);

        // IMVU presence
        private IntPtr _imvuMainWindow = IntPtr.Zero;
        private IntPtr _imvuChatWindow = IntPtr.Zero;

        // IMVU detection and interaction
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool BringWindowToTop(IntPtr hWnd);

        private bool CheckForImvu(bool force)
        {
            if (_imvuMainWindow != IntPtr.Zero && _imvuChatWindow != IntPtr.Zero) {
                if (IsWindow(_imvuMainWindow) && IsWindow(_imvuChatWindow))
                {
                    return true;
                }
            }

            _imvuChatWindow = IntPtr.Zero;
            Process[] p = Process.GetProcesses();
            Process imvuProc = null;
            foreach (var proc in p)
            {
                //Debug.WriteLine($"{proc}");
                if (proc.ToString().ToLowerInvariant().Contains("imvuclient"))
                {
                    imvuProc = proc;
                    break;
                }
            }

            if (imvuProc == null)
            {
                if (force) 
                {
                    var message = "TriggerBot can't play the triggers if IMVU Classic isn't running. Launch it now?";
                    var dlgResult = StyledMessageBox.Show(this, message,
                        "IMVU isn't running", MessageBoxButtons.YesNo, 
                        MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                    if (dlgResult == DialogResult.Yes)
                    {
                        var exeName = Path.Combine(PlugIn.Location.ImvuLocation, "IMVUQualityAgent.exe");
                        if (!File.Exists(exeName)) 
                        {
                            StyledMessageBox.Show("You have to install IMVU Classic first.", "Install IMVU Classic", MessageBoxButtons.OK);
                            Process.Start("https://www.imvu.com/download.php");
                        }
                        else
                        {
                            Process.Start(exeName);
                        }
                    }
                } 
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
                StyledMessageBox.Show(this, "TriggerBot won't work if you're not in a chat room",
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
        private async void ScanInventory(object sender, EventArgs e)
        {
            await TriggerlessApiClient.SendEventAsync(
                TriggerlessApiClient.EventType.AppStart,
                new
                {
                    Opening = true,
                    Version = PlugIn.Shared.VersionNumber.ToString(),
                }
            );

            tabAppContainer.SelectedTab = tabPlayback;
            pnlCollector.BringToFront();
            btnSearch.Enabled = false;
            await _collector.ScanDatabasesAsync();
            btnSearch.Enabled = true;
            pnlCollector.SendToBack();
            DoSearch(null, null);
            CheckForImvu(false);
            SettingsLoad();
        }

        // Inventory Update
        private void OnCollectorEvent(object sender, Collector.CollectorEventArgs e)
        {
            if (InvokeRequired)
            {
                Collector.CollectorEventHandler cb = new Collector.CollectorEventHandler(OnCollectorEvent);
                Invoke(cb, new object[] { sender, e });
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
            var start = DateTime.Now;
            Func<double> msec = () => (DateTime.Now - start).TotalMilliseconds;
            Action<string> say = (s) => {
                Debug.WriteLine($"{s}: {msec():0.0}");
            };
            say("Start of Search");

            var searchTerm = txtSearch.Text.Trim().Replace("'", "''");
            if (searchTerm.ToLowerInvariant() == "triggerboss")
            {
                _splicer.ShowCheap();
                Properties.Settings.Default.InstallationType = "triggerboss";
                tabAppContainer.SelectedTab = tabConvertChkn;
                return;
            }

            var pcList = new List<ProductCtrl>();
            try
            {
                for (int j = flowSearchResults.Controls.Count - 1; j >= 0; j--)
                {
                    var existingCtrl = flowSearchResults.Controls[j] as ProductCtrl;
                    if (existingCtrl != null)
                    {
                        pcList.Add(existingCtrl);
                    }
                }
                foreach (ProductCtrl pc in pcList) pc.Dispose();
            }
            catch (Exception ex) 
            {
                Debug.WriteLine(ex);
            }

            say("FlowPanel cleared");
            //flowSearchResults.SuspendLayout();
            //flowSearchResults.Visible = false;          // cheapest way to suppress a bunch of paints
            //flowSearchResults.AutoScroll = false;

            // 2) (optional but nice) suspend global painting via WM_SETREDRAW
            //flowSearchResults.SuspendDrawing();
            say("Ready to search");

            // We can only reasonably render 600 products before running out of resources.
            List<ProductDisplayInfo> infoList = SQLiteDataAccess.GetProductSearch(searchTerm).Take(600).ToList();
            say("Search complete");

            if (!infoList.Any())
            {
                flowSearchResults.ResumeLayout(true);
                return;
            }

            progSearch.Parent = this;
            progSearch.Minimum = 0;
            progSearch.BringToFront();
            progSearch.Top = flowSearchResults.Top + 120;
            progSearch.Left = (flowSearchResults.Width - progSearch.Width) / 2 - 12;
            progSearch.Value = progSearch.Minimum;
            progSearch.Maximum = infoList.Count;


            say("About to create controls");
            Font fontToUse = new Font("Liberation Sans", 11F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            var controls = new List<ProductCtrl>();

            int ct = 0;
            foreach (var info in infoList)
            {
                say(" Creating control");
                var newControl = new ProductCtrl();
                say(" Control created");
                newControl.BorderStyle = BorderStyle.FixedSingle;
                newControl.Font = fontToUse;
                newControl.Location = new Point(5, 194);
                newControl.Margin = new Padding(5, 4, 5, 4);
                newControl.Name = $"productCtrl_{info.Id}";
                newControl.ProductInfo = info;
                newControl.Size = new Size(364, 87);
                newControl.OnDeckLinkClicked += SendToDeck;
                newControl.OnWearItem += WearItem;
                newControl.OnExcludeSong += ExcludeSong;
                controls.Add(newControl);
                ct++;
                if (controls.Count == 10)
                {
                    //HandleMonitor.LogHandles("adding 10 ProductCtrl's");
                    flowSearchResults.Controls.AddRange(controls.ToArray());
                    controls.Clear();
                    flowSearchResults.Update();
                    progSearch.Visible = true;
                    progSearch.Value = ct;
                    progSearch.Update();
                }
                //Application.DoEvents();
                say("Control added to List");
            }
            flowSearchResults.Controls.AddRange(controls.ToArray());
            //HandleMonitor.LogHandles($"{flowSearchResults.Controls.Count} product Ctrls created");
            controls.Clear();
            flowSearchResults.Update();
            progSearch.Visible = false;
            //flowSearchResults.Controls.AddRange(controls.ToArray());
            say("Controls added.");
            //flowSearchResults.ResumeDrawing(invalidate: true);   // extension below
            say("Resumed Drawing");
            //flowSearchResults.AutoScroll = true;
            say("Autoscroll resumed");
            //flowSearchResults.Visible = true;
            say("Visible now");
            //flowSearchResults.ResumeLayout(performLayout: true); 
            say("Layout resumed");
        }

        private void ExcludeSong(object sender, ExcludeSongEventArgs e)
        {
            var msg = $"Are you sure you want to remove {e.Title} from searchTerm? Nothing will be deleted from your IMVU inventory.";
            var result = StyledMessageBox.Show(this, msg, "Remove Tune?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.No) return;

            if (_collector.ExcludeSong(e.ProductId))
            {
                flowSearchResults.Controls.RemoveByKey($"productCtrl_{e.ProductId}");
            } else
            {
                StyledMessageBox.Show(this, $"Unable to remove {e.Title} at this time", "Database Glitch", MessageBoxButtons.OK);
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
            Text = $"Triggerless Triggerbot {PlugIn.Shared.VersionNumber}";
            pnlBanner_Resize(this, null);
            lblVersion.Text = $"Version {PlugIn.Shared.VersionNumber}";
            lblCopyright.Text = PlugIn.Shared.Copyright;
            Common.CheckIfPaid();
            _updater.CheckForUpdate();
            /*
             * Not using this feature yet.
            InjectPlugIns();
            */
        }

        private void SettingsLoad()
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
            switch (sets.LastTab)
            {
                case "Playback":
                    tabAppContainer.SelectedTab = tabPlayback;
                    break;
                case "Tools":
                    tabAppContainer.SelectedTab = tabTools;
                    break;
                case "Splice":
                    tabAppContainer.SelectedTab = tabConvertChkn;
                    break;
                case "Lyrics":
                    tabAppContainer.SelectedTab = tabLyrics;
                    break;
                case "About":
                    tabAppContainer.SelectedTab = tabAbout;
                    break;
                default:
                    tabAppContainer.SelectedTab = tabPlayback;
                    break;
            }
        }

        private void SettingsSave()
        {
            var sets = Properties.Settings.Default;
            sets.KeepOnTop = chkKeepOnTop.Checked;
            sets.MinimizeOnPlay = chkMinimizeOnPlay.Checked;
            sets.HideTriggers = chkHideTriggers.Checked;
            sets.DefaultTriggerLength = 18;
            sets.InitialLagMS = _lagMS;
            sets.LastSearch = txtSearch.Text;
            sets.DefaultTriggerLength = _splicer.AudioLength;
            switch (tabAppContainer.SelectedTab?.Name)
            {
                case nameof(tabPlayback): sets.LastTab = "Playback"; break;
                case nameof(tabTools): sets.LastTab = "Tools"; break;
                case nameof(tabConvertChkn): sets.LastTab = "Splice"; break;
                case nameof(tabLyrics): sets.LastTab = "Lyrics"; break;
                case nameof(tabAbout): sets.LastTab = "About"; break;
                default: sets.LastTab = "Playback"; break;
            }
            sets.Save();
        }

        private async void TriggerBotMainForm_FormClosing(object sender, FormClosingEventArgs e)
        {

            if (_isPlaying)
            {
                var result = StyledMessageBox.Show(this, "You're playing a tune, are you sure?",
                    "Exit Program?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.No)
                {
                    e.Cancel = true;
                    return;
                }
                CleanupAfterPlay();
            }
            SettingsSave();

            var reasons = new CloseReason[] {
                CloseReason.WindowsShutDown,
                CloseReason.TaskManagerClosing,
                CloseReason.ApplicationExitCall
            };
            using (var trigClient = new TriggerlessApiClient())
            {
                await trigClient.SendEvent(TriggerlessApiClient.EventType.AppCleanExit, new {Closing = true });
            }
            if (reasons.Contains(e.CloseReason)) return;
            _updater.RunSetupFileIfRequired();
        }

        private void RelocateBanner(object sender, EventArgs e)
        {
            var sizePanel = pnlBanner.Size;
            var sizeBanner = picBanner.Size;
            var left = (sizePanel.Width - sizeBanner.Width) / 2;
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
            if (!_isPlaying && (CheckForImvu(true)))
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

                var hasLyrics = _currProductInfo.HasLyrics;
                chkLyrics.Enabled = hasLyrics;
                chkLyrics.Checked = hasLyrics;
                _lyrics = hasLyrics ? _currProductInfo.Lyrics : null;
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
                StyledMessageBox.Show(this, "The data for this _product cannot be verified.", "Bad Product Info", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        private int _lyricsIndex = -1;
        private List<LyricEntry> _lyrics = null;
        private int _lyricsLagMS = 0;

        private object _kbdLock = new object();

        public void DispatchText(string text)
        {
            if (_imvuChatWindow == IntPtr.Zero) return;
            BringWindowToTop(_imvuChatWindow);
            SetForegroundWindow(_imvuChatWindow);

            lock (_kbdLock)
            {
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
                    string prevText = Clipboard.GetText();
                    if (prevText != currClipText && !String.IsNullOrEmpty(prevText))
                    {
                        _sim.Keyboard.TextEntry(prevText);
                    }
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
            const double BASE_LAG = 25;
            var lagToUse = _lagMS + BASE_LAG;
            if (_currTriggerIndex == 0)
            {
                lagToUse += BASE_LAG;
            }
            _triggerTimer.Interval = _currProductInfo.Triggers[_currTriggerIndex].LengthMS - lagToUse;
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
                StyledMessageBox.Show(this, "Playback Error, no trigger _product selected");
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
            if (chkLyrics.Checked && currentTriggerIndex == 0)
            {
                _lyricsIndex = 0;
                _lyricTimer.Interval = (int)_lyrics[_lyricsIndex].Time.TotalMilliseconds - _lyricsLagMS;
                _lyricTimer.Start();
            }
        }

        private string GetTriggerLine()
        {
            var hideTriggers = chkHideTriggers.Checked;
            const string HIDER = "*imvu:trigger ";
            bool hasAdditionalTriggers = !string.IsNullOrEmpty(cboAdditionalTriggers.Text);
            string result = string.Empty; // sometimes the first char gets cut off.
            if (hasAdditionalTriggers)
            {
                result = hideTriggers ? $"  {HIDER}{TrimTrigger()}" : $"  /{TrimTrigger()}";
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
                            result += $"{HIDER}{addnTrigger}";
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
                result = hideTriggers ? $"  {HIDER}{TrimTrigger()}" : $"  {TrimTrigger()}";
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
            if (StyledMessageBox.Show(this, "Are you sure you want to abort?", "Abort Trigger Play?",
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

            _lyricTimer.Enabled = false;
            _lyricsIndex = -1;

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
        private double _tempLyricLagMS = 0;
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
            var msg = "Are you sure you want to rescan? This will delete all Triggerbot data and scan the inventory and web all over again, and could take some time.\n\nAre you certain?";
            var dlgResult = StyledMessageBox.Show(this, msg, "Rescan All Data?",
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

        private void btnViewLog_Click(object sender, EventArgs e)
        {
            var f = new LogForm();
            f.LogText = _collector.Log;
            f.TopMost = this.TopMost;
            f.ShowDialog();
        }

        private void linkDiscord_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(linkDiscord.Text);
        }

        private void _lyricTimer_Tick(object sender, EventArgs e)
        {
            if (_lyricsIndex == -1) return;
            if (_lyricsIndex == _lyrics.Count - 1)
            {
                _lyricTimer.Stop();
                var lyric = _lyrics[_lyricsIndex].Lyric;
                if (chkLyrics.Checked) DispatchText(lyric);
                _lyricsIndex = -1;
                _lyricTimer.Interval = 1000;
            }
            if (_lyricsIndex > -1 && _lyricsIndex < _lyrics.Count - 1)
            {
                _lyricTimer.Stop();
                var lyric = _lyrics[_lyricsIndex].Lyric;
                if (chkLyrics.Checked) DispatchText(lyric);
                _lyricsIndex++;
                double ms = (_lyrics[_lyricsIndex].Time - _lyrics[_lyricsIndex - 1].Time).TotalMilliseconds;
                double multiplier = GetMultiplier(
                    _lyrics[_lyricsIndex].Time,
                    _lyrics[_lyricsIndex - 1].Time
                );
                int newInterval = (int)(ms * multiplier - _lagMS - _tempLyricLagMS);
                if (newInterval > 0)
                {
                    _lyricTimer.Interval = (int)(ms * multiplier - _lagMS - _tempLyricLagMS);
                }
                Interlocked.Exchange(ref _tempLyricLagMS, 0);
                _lyricTimer.Start();
            }
        }

        private double GetMultiplier(TimeSpan future, TimeSpan now)
        {
            // a little math here, the longer the difference between triggers,
            // the closer to 1 the multiplier should be.
            double b = 0.92; // this is the multiplier at 0 secs
            double rise = 1.55; // this controls how fast we rise toward the asymptote
            double secFuture = future.TotalSeconds;
            double secNow = now.TotalSeconds;
            double secDuration = secFuture - secNow;
            return 1 - (1 - b) * Math.Exp(-rise);
        }

        private void btnAddLyricLag_Click(object sender, EventArgs e)
        {
            Interlocked.Exchange(ref _tempLyricLagMS, _tempLyricLagMS + 500);
        }

        private void tabAppContainer_Selected(object sender, TabControlEventArgs e)
        {
            this.TopMost = (e.TabPage == tabPlayback) && chkKeepOnTop.Checked;
        }

        private void pnlBanner_Resize(object sender, EventArgs e)
        {
            picBanner.Left = (pnlBanner.Width - picBanner.Width) / 2;
        }

        private async void btnDiscordSend_Click(object sender, EventArgs e)
        {
            // validate form {{
            if (String.IsNullOrWhiteSpace(txtDiscordSubject.Text))
            {
                StyledMessageBox.Show(Program.MainForm, "Please provide a Subject", "Discord Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDiscordSubject.Focus();
                return;
            }
            if (String.IsNullOrWhiteSpace(txtDiscordMessage.Text))
            {
                StyledMessageBox.Show(Program.MainForm, "Please provide a Message", "Discord Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDiscordMessage.Focus();
                return;
            }
            var result = await Discord.SendMessage(txtDiscordSubject.Text, txtDiscordMessage.Text);
            var title = "Discord Result";
            MessageBoxIcon icon = result.Status == Discord.ResultStatus.Success ? MessageBoxIcon.Information : MessageBoxIcon.Warning;
            StyledMessageBox.Show(this, result.Message, title, MessageBoxButtons.OK, icon);
        }

        private void tabAppContainer_Deselecting(object sender, TabControlCancelEventArgs e)
        {
            // e.TabPage is the page we're LEAVING
            if (e.TabPage == tabLyrics && lyricsCtrl1.IsDirty)
            {
                string message = "Save changes?"
                    + Environment.NewLine + "Yes = Save changes"
                    + Environment.NewLine + "No = Ignore and Continue"
                    + Environment.NewLine + "Cancel = Keep editing";
                string title = "Unsaved lyrics changes";

                var result = StyledMessageBox.Show(
                    Program.MainForm, message, title,
                    MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Warning,
                    MessageBoxDefaultButton.Button1);

                switch (result)
                {
                    case DialogResult.Yes:
                        try
                        {
                            lyricsCtrl1.Save();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(this,
                                "Save failed:\n\n" + ex.Message,
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            e.Cancel = true; // stay on tab if save fails
                        }
                        break;

                    case DialogResult.No:
                        // ignore and allow switching
                        break;

                    case DialogResult.Cancel:
                        e.Cancel = true; // stay on the lyrics tab
                        break;
                }
            }
        }

        private void InjectPlugIns()
        {
            var filenames = Directory.GetFiles(PlugIn.Location.PlugInsPath, "*.dll");
            var thisAssy = Assembly.GetExecutingAssembly();

            foreach (var filename in filenames)
            {
                try
                {
                    var a = Assembly.LoadFrom(filename);
                    if (a == thisAssy) continue;
                    var plugInTypes = a.DefinedTypes.Where(t => t.ImplementedInterfaces.Contains(typeof(IPlugIn)));
                    if (!plugInTypes.Any()) continue;
                    foreach (Type pluginType in plugInTypes)
                    {
                        var context = new PlugInContext();
                        var plugIn = PlugInFactory.CreateInstance(pluginType);
                        if (plugIn.CanPlugIn)
                        {
                            TabPage newPage = new TabPage(plugIn.Title);
                            int newIndex = tabAppContainer.TabPages.Count - 1;
                            tabAppContainer.TabPages.Insert(newIndex, newPage);
                            context.Parent = newPage;
                            plugIn.OnPlugIn(context);
                            newPage.Tag = context;
                        }
                    }
                }
                catch (Exception) 
                {
                    // not sure what to do but ignore it.
                }
            }
        }
    }
}
