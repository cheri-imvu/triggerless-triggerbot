using System.ComponentModel;
using System.Drawing;
using System.Threading;
using TabControl = Triggerless.TriggerBot.Components.ColorTabControl;

namespace Triggerless.TriggerBot
{
    partial class TriggerBotMainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TriggerBotMainForm));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tabAppContainer = new System.Windows.Forms.TabControl();
            this.tabPlayback = new System.Windows.Forms.TabPage();
            this.splitter = new System.Windows.Forms.SplitContainer();
            this.pnlSearch = new System.Windows.Forms.Panel();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.pnlRight = new System.Windows.Forms.Panel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.chkHideTriggers = new System.Windows.Forms.CheckBox();
            this.btnLyricLag = new System.Windows.Forms.Button();
            this.chkKeepOnTop = new System.Windows.Forms.CheckBox();
            this.chkMinimizeOnPlay = new System.Windows.Forms.CheckBox();
            this.chkLyrics = new System.Windows.Forms.CheckBox();
            this.btnAllAddnTriggers = new System.Windows.Forms.Button();
            this.lblCurrPlayingTrigger = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.pnlLag = new System.Windows.Forms.Panel();
            this.lblLagMinus = new System.Windows.Forms.Label();
            this.lblLagMinusMinus = new System.Windows.Forms.Label();
            this.lblLagPlusPlus = new System.Windows.Forms.Label();
            this.lblLagPlus = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblLag = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.progTrigger = new System.Windows.Forms.ProgressBar();
            this.trackBarLag = new System.Windows.Forms.TrackBar();
            this.btnAbort = new System.Windows.Forms.Button();
            this.btnPlay = new System.Windows.Forms.Button();
            this.cboAdditionalTriggers = new System.Windows.Forms.ComboBox();
            this.lblAdditional = new System.Windows.Forms.Label();
            this.gridTriggers = new System.Windows.Forms.DataGridView();
            this.colTrigger = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLengthMS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblNowPlaying = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.pnlBanner = new System.Windows.Forms.Panel();
            this.picBanner = new System.Windows.Forms.PictureBox();
            this.pnlOnDeck = new System.Windows.Forms.Panel();
            this.label27 = new System.Windows.Forms.Label();
            this.lblSendToNP = new System.Windows.Forms.Label();
            this.chkAutoCue = new System.Windows.Forms.CheckBox();
            this.btnEjectFromDeck = new System.Windows.Forms.Button();
            this.btnLoadToPlaying = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.pnlCollector = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.progScan = new System.Windows.Forms.ProgressBar();
            this.lblProgress = new System.Windows.Forms.Label();
            this.lblProduct = new System.Windows.Forms.Label();
            this.tabConvertChkn = new System.Windows.Forms.TabPage();
            this.tabLyrics = new System.Windows.Forms.TabPage();
            this.tabTools = new System.Windows.Forms.TabPage();
            this.pnlTools = new System.Windows.Forms.Panel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label26 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.btnViewLog = new System.Windows.Forms.Button();
            this.btnScanNew = new System.Windows.Forms.Button();
            this.btnTechSupport = new System.Windows.Forms.Button();
            this.label16 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.btnRescanAll = new System.Windows.Forms.Button();
            this.label19 = new System.Windows.Forms.Label();
            this.btnDeepScan = new System.Windows.Forms.Button();
            this.label17 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.tabAbout = new System.Windows.Forms.TabPage();
            this.pnlAbout = new System.Windows.Forms.Panel();
            this.linkDiscord = new System.Windows.Forms.LinkLabel();
            this.lblDiscord = new System.Windows.Forms.Label();
            this.picDiscord = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lnkPage = new System.Windows.Forms.LinkLabel();
            this.label23 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.lblVersion = new System.Windows.Forms.Label();
            this.lblCopyright = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this._toolTip = new System.Windows.Forms.ToolTip(this.components);
            this._triggerTimer = new System.Timers.Timer();
            this._progressTimer = new System.Windows.Forms.Timer(this.components);
            this._lyricTimer = new System.Windows.Forms.Timer(this.components);
            this.lblNoResults = new System.Windows.Forms.Label();
            this.pnlSearchResults = new Triggerless.TriggerBot.Components.VirtualScrollPanel();
            this.productOnDeck = new Triggerless.TriggerBot.ProductCtrl();
            this._splicer = new Triggerless.TriggerBot.SplicerControl();
            this.lyricsCtrl1 = new Triggerless.TriggerBot.Components.LyricsCtrl();
            this.underConstructionCtrl1 = new Triggerless.TriggerBot.Components.UnderConstructionCtrl();
            this._collector = new Triggerless.TriggerBot.Collector();
            this.tabAppContainer.SuspendLayout();
            this.tabPlayback.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitter)).BeginInit();
            this.splitter.Panel1.SuspendLayout();
            this.splitter.Panel2.SuspendLayout();
            this.splitter.SuspendLayout();
            this.pnlSearch.SuspendLayout();
            this.pnlRight.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.pnlLag.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarLag)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridTriggers)).BeginInit();
            this.pnlBanner.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBanner)).BeginInit();
            this.pnlOnDeck.SuspendLayout();
            this.pnlCollector.SuspendLayout();
            this.tabConvertChkn.SuspendLayout();
            this.tabLyrics.SuspendLayout();
            this.tabTools.SuspendLayout();
            this.pnlTools.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tabAbout.SuspendLayout();
            this.pnlAbout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picDiscord)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._triggerTimer)).BeginInit();
            this.SuspendLayout();
            // 
            // tabAppContainer
            // 
            this.tabAppContainer.Controls.Add(this.tabPlayback);
            this.tabAppContainer.Controls.Add(this.tabConvertChkn);
            this.tabAppContainer.Controls.Add(this.tabLyrics);
            this.tabAppContainer.Controls.Add(this.tabTools);
            this.tabAppContainer.Controls.Add(this.tabAbout);
            this.tabAppContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabAppContainer.Location = new System.Drawing.Point(0, 0);
            this.tabAppContainer.Name = "tabAppContainer";
            this.tabAppContainer.SelectedIndex = 0;
            this.tabAppContainer.Size = new System.Drawing.Size(1058, 660);
            this.tabAppContainer.TabIndex = 0;
            this.tabAppContainer.Selected += new System.Windows.Forms.TabControlEventHandler(this.tabAppContainer_Selected);
            this.tabAppContainer.Deselecting += new System.Windows.Forms.TabControlCancelEventHandler(this.tabAppContainer_Deselecting);
            // 
            // tabPlayback
            // 
            this.tabPlayback.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(29)))), ((int)(((byte)(51)))));
            this.tabPlayback.Controls.Add(this.splitter);
            this.tabPlayback.Location = new System.Drawing.Point(4, 30);
            this.tabPlayback.Name = "tabPlayback";
            this.tabPlayback.Padding = new System.Windows.Forms.Padding(3);
            this.tabPlayback.Size = new System.Drawing.Size(1050, 626);
            this.tabPlayback.TabIndex = 0;
            this.tabPlayback.Text = " Playback  ♫ ";
            // 
            // splitter
            // 
            this.splitter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitter.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitter.Location = new System.Drawing.Point(3, 3);
            this.splitter.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.splitter.Name = "splitter";
            // 
            // splitter.Panel1
            // 
            this.splitter.Panel1.Controls.Add(this.pnlSearchResults);
            this.splitter.Panel1.Controls.Add(this.pnlSearch);
            this.splitter.Panel1MinSize = 396;
            // 
            // splitter.Panel2
            // 
            this.splitter.Panel2.Controls.Add(this.pnlRight);
            this.splitter.Panel2.Controls.Add(this.pnlCollector);
            this.splitter.Size = new System.Drawing.Size(1044, 620);
            this.splitter.SplitterDistance = 396;
            this.splitter.TabIndex = 3;
            // 
            // pnlSearch
            // 
            this.pnlSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(29)))), ((int)(((byte)(51)))));
            this.pnlSearch.Controls.Add(this.txtSearch);
            this.pnlSearch.Controls.Add(this.btnSearch);
            this.pnlSearch.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSearch.Location = new System.Drawing.Point(0, 0);
            this.pnlSearch.Name = "pnlSearch";
            this.pnlSearch.Size = new System.Drawing.Size(396, 45);
            this.pnlSearch.TabIndex = 3;
            // 
            // txtSearch
            // 
            this.txtSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearch.Font = new System.Drawing.Font("Liberation Sans", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearch.Location = new System.Drawing.Point(42, 10);
            this.txtSearch.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(228, 30);
            this.txtSearch.TabIndex = 1;
            this._toolTip.SetToolTip(this.txtSearch, "Enter Search Term");
            this.txtSearch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSearch_KeyPress);
            // 
            // btnSearch
            // 
            this.btnSearch.Font = new System.Drawing.Font("Webdings", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.btnSearch.Image = ((System.Drawing.Image)(resources.GetObject("btnSearch.Image")));
            this.btnSearch.Location = new System.Drawing.Point(283, 6);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(31, 33);
            this.btnSearch.TabIndex = 2;
            this._toolTip.SetToolTip(this.btnSearch, "Search Title, Creator & Triggers");
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.DoSearch);
            // 
            // pnlRight
            // 
            this.pnlRight.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(29)))), ((int)(((byte)(51)))));
            this.pnlRight.Controls.Add(this.tableLayoutPanel3);
            this.pnlRight.Controls.Add(this.btnAllAddnTriggers);
            this.pnlRight.Controls.Add(this.lblCurrPlayingTrigger);
            this.pnlRight.Controls.Add(this.label5);
            this.pnlRight.Controls.Add(this.pnlLag);
            this.pnlRight.Controls.Add(this.btnAbort);
            this.pnlRight.Controls.Add(this.btnPlay);
            this.pnlRight.Controls.Add(this.cboAdditionalTriggers);
            this.pnlRight.Controls.Add(this.lblAdditional);
            this.pnlRight.Controls.Add(this.gridTriggers);
            this.pnlRight.Controls.Add(this.lblNowPlaying);
            this.pnlRight.Controls.Add(this.label3);
            this.pnlRight.Controls.Add(this.pnlBanner);
            this.pnlRight.Controls.Add(this.pnlOnDeck);
            this.pnlRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlRight.Location = new System.Drawing.Point(0, 0);
            this.pnlRight.Name = "pnlRight";
            this.pnlRight.Size = new System.Drawing.Size(644, 620);
            this.pnlRight.TabIndex = 9;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 3;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel3.Controls.Add(this.chkHideTriggers, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.btnLyricLag, 1, 1);
            this.tableLayoutPanel3.Controls.Add(this.chkKeepOnTop, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.chkMinimizeOnPlay, 2, 0);
            this.tableLayoutPanel3.Controls.Add(this.chkLyrics, 0, 1);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(263, 235);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(374, 54);
            this.tableLayoutPanel3.TabIndex = 25;
            // 
            // chkHideTriggers
            // 
            this.chkHideTriggers.AutoSize = true;
            this.chkHideTriggers.Location = new System.Drawing.Point(0, 0);
            this.chkHideTriggers.Margin = new System.Windows.Forms.Padding(0);
            this.chkHideTriggers.Name = "chkHideTriggers";
            this.chkHideTriggers.Size = new System.Drawing.Size(112, 25);
            this.chkHideTriggers.TabIndex = 18;
            this.chkHideTriggers.Text = "Hide Trigs";
            this._toolTip.SetToolTip(this.chkHideTriggers, "Hide triggers");
            this.chkHideTriggers.UseVisualStyleBackColor = true;
            // 
            // btnLyricLag
            // 
            this.btnLyricLag.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnLyricLag.Location = new System.Drawing.Point(112, 27);
            this.btnLyricLag.Margin = new System.Windows.Forms.Padding(0);
            this.btnLyricLag.Name = "btnLyricLag";
            this.btnLyricLag.Size = new System.Drawing.Size(44, 24);
            this.btnLyricLag.TabIndex = 24;
            this.btnLyricLag.Text = "L▶";
            this._toolTip.SetToolTip(this.btnLyricLag, "Push Lyrics Forward");
            this.btnLyricLag.UseVisualStyleBackColor = true;
            this.btnLyricLag.Click += new System.EventHandler(this.btnAddLyricLag_Click);
            // 
            // chkKeepOnTop
            // 
            this.chkKeepOnTop.AutoSize = true;
            this.chkKeepOnTop.Location = new System.Drawing.Point(112, 0);
            this.chkKeepOnTop.Margin = new System.Windows.Forms.Padding(0);
            this.chkKeepOnTop.Name = "chkKeepOnTop";
            this.chkKeepOnTop.Size = new System.Drawing.Size(112, 25);
            this.chkKeepOnTop.TabIndex = 19;
            this.chkKeepOnTop.Text = "Keep On Top";
            this._toolTip.SetToolTip(this.chkKeepOnTop, "Keep This On Top while playing");
            this.chkKeepOnTop.UseVisualStyleBackColor = true;
            this.chkKeepOnTop.CheckedChanged += new System.EventHandler(this.chkStayOnTop_Clicked);
            // 
            // chkMinimizeOnPlay
            // 
            this.chkMinimizeOnPlay.AutoSize = true;
            this.chkMinimizeOnPlay.Location = new System.Drawing.Point(224, 0);
            this.chkMinimizeOnPlay.Margin = new System.Windows.Forms.Padding(0);
            this.chkMinimizeOnPlay.Name = "chkMinimizeOnPlay";
            this.chkMinimizeOnPlay.Size = new System.Drawing.Size(150, 25);
            this.chkMinimizeOnPlay.TabIndex = 20;
            this.chkMinimizeOnPlay.Text = "Minimize On Play";
            this._toolTip.SetToolTip(this.chkMinimizeOnPlay, "Minimize window when playing");
            this.chkMinimizeOnPlay.UseVisualStyleBackColor = true;
            // 
            // chkLyrics
            // 
            this.chkLyrics.AutoSize = true;
            this.chkLyrics.Location = new System.Drawing.Point(0, 27);
            this.chkLyrics.Margin = new System.Windows.Forms.Padding(0);
            this.chkLyrics.Name = "chkLyrics";
            this.chkLyrics.Size = new System.Drawing.Size(112, 25);
            this.chkLyrics.TabIndex = 23;
            this.chkLyrics.Text = "Use Lyrics";
            this.chkLyrics.UseVisualStyleBackColor = true;
            // 
            // btnAllAddnTriggers
            // 
            this.btnAllAddnTriggers.Location = new System.Drawing.Point(587, 133);
            this.btnAllAddnTriggers.Name = "btnAllAddnTriggers";
            this.btnAllAddnTriggers.Size = new System.Drawing.Size(66, 28);
            this.btnAllAddnTriggers.TabIndex = 21;
            this.btnAllAddnTriggers.Text = "more...";
            this.btnAllAddnTriggers.UseVisualStyleBackColor = true;
            this.btnAllAddnTriggers.Click += new System.EventHandler(this.AddnTriggersButtonClick);
            // 
            // lblCurrPlayingTrigger
            // 
            this.lblCurrPlayingTrigger.AutoSize = true;
            this.lblCurrPlayingTrigger.Location = new System.Drawing.Point(423, 206);
            this.lblCurrPlayingTrigger.Name = "lblCurrPlayingTrigger";
            this.lblCurrPlayingTrigger.Size = new System.Drawing.Size(104, 21);
            this.lblCurrPlayingTrigger.TabIndex = 17;
            this.lblCurrPlayingTrigger.Text = "--Pending--";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(260, 206);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(203, 21);
            this.label5.TabIndex = 16;
            this.label5.Text = "Current Playing Trigger";
            // 
            // pnlLag
            // 
            this.pnlLag.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlLag.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(29)))), ((int)(((byte)(51)))));
            this.pnlLag.Controls.Add(this.lblLagMinus);
            this.pnlLag.Controls.Add(this.lblLagMinusMinus);
            this.pnlLag.Controls.Add(this.lblLagPlusPlus);
            this.pnlLag.Controls.Add(this.lblLagPlus);
            this.pnlLag.Controls.Add(this.label8);
            this.pnlLag.Controls.Add(this.label14);
            this.pnlLag.Controls.Add(this.label13);
            this.pnlLag.Controls.Add(this.label12);
            this.pnlLag.Controls.Add(this.label11);
            this.pnlLag.Controls.Add(this.label10);
            this.pnlLag.Controls.Add(this.label9);
            this.pnlLag.Controls.Add(this.label6);
            this.pnlLag.Controls.Add(this.label4);
            this.pnlLag.Controls.Add(this.lblLag);
            this.pnlLag.Controls.Add(this.label7);
            this.pnlLag.Controls.Add(this.progTrigger);
            this.pnlLag.Controls.Add(this.trackBarLag);
            this.pnlLag.Location = new System.Drawing.Point(255, 355);
            this.pnlLag.Name = "pnlLag";
            this.pnlLag.Size = new System.Drawing.Size(382, 140);
            this.pnlLag.TabIndex = 15;
            this.pnlLag.Visible = false;
            // 
            // lblLagMinus
            // 
            this.lblLagMinus.AutoSize = true;
            this.lblLagMinus.BackColor = System.Drawing.Color.Gray;
            this.lblLagMinus.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblLagMinus.Font = new System.Drawing.Font("Lucida Console", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLagMinus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.lblLagMinus.Location = new System.Drawing.Point(147, 8);
            this.lblLagMinus.Name = "lblLagMinus";
            this.lblLagMinus.Size = new System.Drawing.Size(40, 24);
            this.lblLagMinus.TabIndex = 35;
            this.lblLagMinus.Text = " -";
            this.lblLagMinus.Click += new System.EventHandler(this.TrackBarDec);
            // 
            // lblLagMinusMinus
            // 
            this.lblLagMinusMinus.AutoSize = true;
            this.lblLagMinusMinus.BackColor = System.Drawing.Color.Gray;
            this.lblLagMinusMinus.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblLagMinusMinus.Font = new System.Drawing.Font("Lucida Console", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLagMinusMinus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.lblLagMinusMinus.Location = new System.Drawing.Point(112, 8);
            this.lblLagMinusMinus.Name = "lblLagMinusMinus";
            this.lblLagMinusMinus.Size = new System.Drawing.Size(40, 24);
            this.lblLagMinusMinus.TabIndex = 34;
            this.lblLagMinusMinus.Text = "--";
            this.lblLagMinusMinus.Click += new System.EventHandler(this.TrackBarDecDec);
            // 
            // lblLagPlusPlus
            // 
            this.lblLagPlusPlus.AutoSize = true;
            this.lblLagPlusPlus.BackColor = System.Drawing.Color.Gray;
            this.lblLagPlusPlus.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblLagPlusPlus.Font = new System.Drawing.Font("Lucida Console", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLagPlusPlus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.lblLagPlusPlus.Location = new System.Drawing.Point(301, 8);
            this.lblLagPlusPlus.Name = "lblLagPlusPlus";
            this.lblLagPlusPlus.Size = new System.Drawing.Size(40, 24);
            this.lblLagPlusPlus.TabIndex = 33;
            this.lblLagPlusPlus.Text = "++";
            this.lblLagPlusPlus.Click += new System.EventHandler(this.TrackBarIncInc);
            // 
            // lblLagPlus
            // 
            this.lblLagPlus.AutoSize = true;
            this.lblLagPlus.BackColor = System.Drawing.Color.Gray;
            this.lblLagPlus.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblLagPlus.Font = new System.Drawing.Font("Lucida Console", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLagPlus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.lblLagPlus.Location = new System.Drawing.Point(267, 8);
            this.lblLagPlus.Name = "lblLagPlus";
            this.lblLagPlus.Size = new System.Drawing.Size(40, 24);
            this.lblLagPlus.TabIndex = 32;
            this.lblLagPlus.Text = "+ ";
            this.lblLagPlus.Click += new System.EventHandler(this.TrackBarInc);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Black;
            this.label8.Font = new System.Drawing.Font("Lucida Console", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.Lime;
            this.label8.Location = new System.Drawing.Point(368, 63);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(25, 15);
            this.label8.TabIndex = 31;
            this.label8.Text = "12";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.BackColor = System.Drawing.Color.Black;
            this.label14.Font = new System.Drawing.Font("Lucida Console", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.ForeColor = System.Drawing.Color.Lime;
            this.label14.Location = new System.Drawing.Point(309, 63);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(25, 15);
            this.label14.TabIndex = 30;
            this.label14.Text = "10";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.BackColor = System.Drawing.Color.Black;
            this.label13.Font = new System.Drawing.Font("Lucida Console", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.Lime;
            this.label13.Location = new System.Drawing.Point(251, 63);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(16, 15);
            this.label13.TabIndex = 29;
            this.label13.Text = "8";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.Black;
            this.label12.Font = new System.Drawing.Font("Lucida Console", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.Lime;
            this.label12.Location = new System.Drawing.Point(191, 63);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(16, 15);
            this.label12.TabIndex = 28;
            this.label12.Text = "6";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.Black;
            this.label11.Font = new System.Drawing.Font("Lucida Console", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.Lime;
            this.label11.Location = new System.Drawing.Point(132, 63);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(16, 15);
            this.label11.TabIndex = 27;
            this.label11.Text = "4";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.Black;
            this.label10.Font = new System.Drawing.Font("Lucida Console", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.Lime;
            this.label10.Location = new System.Drawing.Point(73, 63);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(16, 15);
            this.label10.TabIndex = 26;
            this.label10.Text = "2";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Black;
            this.label9.Font = new System.Drawing.Font("Lucida Console", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.Lime;
            this.label9.Location = new System.Drawing.Point(13, 63);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(16, 15);
            this.label9.TabIndex = 25;
            this.label9.Text = "0";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(222, 7);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(57, 21);
            this.label6.TabIndex = 24;
            this.label6.Text = "msec";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Liberation Sans", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(6, 7);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(101, 21);
            this.label4.TabIndex = 22;
            this.label4.Text = "Lag Factor";
            // 
            // lblLag
            // 
            this.lblLag.AutoSize = true;
            this.lblLag.Location = new System.Drawing.Point(190, 7);
            this.lblLag.Name = "lblLag";
            this.lblLag.Size = new System.Drawing.Size(48, 21);
            this.lblLag.TabIndex = 23;
            this.lblLag.Text = "6.00";
            this.lblLag.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Liberation Sans", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(6, 86);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(153, 21);
            this.label7.TabIndex = 16;
            this.label7.Text = "Trigger Progress";
            // 
            // progTrigger
            // 
            this.progTrigger.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progTrigger.Location = new System.Drawing.Point(19, 114);
            this.progTrigger.Name = "progTrigger";
            this.progTrigger.Size = new System.Drawing.Size(343, 9);
            this.progTrigger.TabIndex = 3;
            // 
            // trackBarLag
            // 
            this.trackBarLag.AutoSize = false;
            this.trackBarLag.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.trackBarLag.LargeChange = 4;
            this.trackBarLag.Location = new System.Drawing.Point(5, 31);
            this.trackBarLag.Maximum = 48;
            this.trackBarLag.Name = "trackBarLag";
            this.trackBarLag.Size = new System.Drawing.Size(383, 42);
            this.trackBarLag.TabIndex = 12;
            this.trackBarLag.TickFrequency = 4;
            this.trackBarLag.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.trackBarLag.Value = 24;
            this.trackBarLag.ValueChanged += new System.EventHandler(this.LagControlChanged);
            // 
            // btnAbort
            // 
            this.btnAbort.Enabled = false;
            this.btnAbort.Image = ((System.Drawing.Image)(resources.GetObject("btnAbort.Image")));
            this.btnAbort.Location = new System.Drawing.Point(298, 167);
            this.btnAbort.Name = "btnAbort";
            this.btnAbort.Size = new System.Drawing.Size(30, 32);
            this.btnAbort.TabIndex = 11;
            this._toolTip.SetToolTip(this.btnAbort, "Stop Playing this Tune");
            this.btnAbort.UseVisualStyleBackColor = true;
            this.btnAbort.Click += new System.EventHandler(this.AbortPlaying);
            // 
            // btnPlay
            // 
            this.btnPlay.Enabled = false;
            this.btnPlay.Font = new System.Drawing.Font("Lucida Sans Unicode", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPlay.Image = ((System.Drawing.Image)(resources.GetObject("btnPlay.Image")));
            this.btnPlay.Location = new System.Drawing.Point(260, 167);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(30, 32);
            this.btnPlay.TabIndex = 10;
            this._toolTip.SetToolTip(this.btnPlay, "Start Playing Triggers");
            this.btnPlay.UseVisualStyleBackColor = true;
            this.btnPlay.Click += new System.EventHandler(this.StartPlayingClicked);
            // 
            // cboAdditionalTriggers
            // 
            this.cboAdditionalTriggers.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboAdditionalTriggers.FormattingEnabled = true;
            this.cboAdditionalTriggers.Location = new System.Drawing.Point(255, 136);
            this.cboAdditionalTriggers.Name = "cboAdditionalTriggers";
            this.cboAdditionalTriggers.Size = new System.Drawing.Size(313, 29);
            this.cboAdditionalTriggers.TabIndex = 9;
            // 
            // lblAdditional
            // 
            this.lblAdditional.AutoSize = true;
            this.lblAdditional.Location = new System.Drawing.Point(254, 114);
            this.lblAdditional.Name = "lblAdditional";
            this.lblAdditional.Size = new System.Drawing.Size(168, 21);
            this.lblAdditional.TabIndex = 8;
            this.lblAdditional.Text = "Additional Triggers";
            // 
            // gridTriggers
            // 
            this.gridTriggers.AllowUserToAddRows = false;
            this.gridTriggers.AllowUserToDeleteRows = false;
            this.gridTriggers.AllowUserToResizeColumns = false;
            this.gridTriggers.AllowUserToResizeRows = false;
            this.gridTriggers.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.gridTriggers.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(29)))), ((int)(((byte)(51)))));
            this.gridTriggers.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Liberation Sans", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridTriggers.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.gridTriggers.ColumnHeadersHeight = 25;
            this.gridTriggers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.gridTriggers.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colTrigger,
            this.colLengthMS});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(55)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Liberation Sans", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gridTriggers.DefaultCellStyle = dataGridViewCellStyle2;
            this.gridTriggers.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.gridTriggers.EnableHeadersVisualStyles = false;
            this.gridTriggers.Location = new System.Drawing.Point(4, 113);
            this.gridTriggers.MultiSelect = false;
            this.gridTriggers.Name = "gridTriggers";
            this.gridTriggers.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(29)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Liberation Sans", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gridTriggers.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.gridTriggers.RowHeadersWidth = 5;
            this.gridTriggers.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.gridTriggers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridTriggers.Size = new System.Drawing.Size(255, 373);
            this.gridTriggers.TabIndex = 7;
            this._toolTip.SetToolTip(this.gridTriggers, "Double-click on row to start from a certain trigger.");
            this.gridTriggers.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridTriggers_CellDoubleClick);
            // 
            // colTrigger
            // 
            this.colTrigger.HeaderText = "Trigger";
            this.colTrigger.MinimumWidth = 6;
            this.colTrigger.Name = "colTrigger";
            this.colTrigger.ReadOnly = true;
            this.colTrigger.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colTrigger.Width = 125;
            // 
            // colLengthMS
            // 
            this.colLengthMS.HeaderText = "OGG Length (sec)";
            this.colLengthMS.MinimumWidth = 6;
            this.colLengthMS.Name = "colLengthMS";
            this.colLengthMS.ReadOnly = true;
            this.colLengthMS.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colLengthMS.Width = 150;
            // 
            // lblNowPlaying
            // 
            this.lblNowPlaying.AutoSize = true;
            this.lblNowPlaying.Font = new System.Drawing.Font("Liberation Sans", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNowPlaying.Location = new System.Drawing.Point(121, 89);
            this.lblNowPlaying.Name = "lblNowPlaying";
            this.lblNowPlaying.Size = new System.Drawing.Size(104, 21);
            this.lblNowPlaying.TabIndex = 6;
            this.lblNowPlaying.Text = "--Pending--";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Liberation Sans", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(5, 89);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(142, 21);
            this.label3.TabIndex = 5;
            this.label3.Text = "NOW PLAYING";
            // 
            // pnlBanner
            // 
            this.pnlBanner.BackColor = System.Drawing.Color.Black;
            this.pnlBanner.Controls.Add(this.picBanner);
            this.pnlBanner.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlBanner.Location = new System.Drawing.Point(0, 0);
            this.pnlBanner.Name = "pnlBanner";
            this.pnlBanner.Size = new System.Drawing.Size(644, 83);
            this.pnlBanner.TabIndex = 4;
            this.pnlBanner.Resize += new System.EventHandler(this.pnlBanner_Resize);
            // 
            // picBanner
            // 
            this.picBanner.Image = ((System.Drawing.Image)(resources.GetObject("picBanner.Image")));
            this.picBanner.Location = new System.Drawing.Point(124, 0);
            this.picBanner.Name = "picBanner";
            this.picBanner.Size = new System.Drawing.Size(405, 86);
            this.picBanner.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picBanner.TabIndex = 0;
            this.picBanner.TabStop = false;
            // 
            // pnlOnDeck
            // 
            this.pnlOnDeck.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(55)))), ((int)(((byte)(94)))));
            this.pnlOnDeck.Controls.Add(this.label27);
            this.pnlOnDeck.Controls.Add(this.lblSendToNP);
            this.pnlOnDeck.Controls.Add(this.chkAutoCue);
            this.pnlOnDeck.Controls.Add(this.btnEjectFromDeck);
            this.pnlOnDeck.Controls.Add(this.btnLoadToPlaying);
            this.pnlOnDeck.Controls.Add(this.label2);
            this.pnlOnDeck.Controls.Add(this.productOnDeck);
            this.pnlOnDeck.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlOnDeck.Location = new System.Drawing.Point(0, 492);
            this.pnlOnDeck.Name = "pnlOnDeck";
            this.pnlOnDeck.Size = new System.Drawing.Size(644, 128);
            this.pnlOnDeck.TabIndex = 3;
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Font = new System.Drawing.Font("Liberation Sans", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label27.Location = new System.Drawing.Point(432, 42);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(142, 21);
            this.label27.TabIndex = 8;
            this.label27.Text = "NOW PLAYING";
            // 
            // lblSendToNP
            // 
            this.lblSendToNP.AutoSize = true;
            this.lblSendToNP.Font = new System.Drawing.Font("Liberation Sans", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSendToNP.Location = new System.Drawing.Point(372, 42);
            this.lblSendToNP.Name = "lblSendToNP";
            this.lblSendToNP.Size = new System.Drawing.Size(81, 21);
            this.lblSendToNP.TabIndex = 7;
            this.lblSendToNP.Text = "Send To";
            // 
            // chkAutoCue
            // 
            this.chkAutoCue.AutoSize = true;
            this.chkAutoCue.Location = new System.Drawing.Point(95, 9);
            this.chkAutoCue.Name = "chkAutoCue";
            this.chkAutoCue.Size = new System.Drawing.Size(301, 25);
            this.chkAutoCue.TabIndex = 5;
            this.chkAutoCue.Text = "Auto play after this song is done";
            this.chkAutoCue.UseVisualStyleBackColor = true;
            // 
            // btnEjectFromDeck
            // 
            this.btnEjectFromDeck.Enabled = false;
            this.btnEjectFromDeck.Image = ((System.Drawing.Image)(resources.GetObject("btnEjectFromDeck.Image")));
            this.btnEjectFromDeck.Location = new System.Drawing.Point(335, 89);
            this.btnEjectFromDeck.Name = "btnEjectFromDeck";
            this.btnEjectFromDeck.Size = new System.Drawing.Size(30, 32);
            this.btnEjectFromDeck.TabIndex = 4;
            this._toolTip.SetToolTip(this.btnEjectFromDeck, "Remove from On Deck");
            this.btnEjectFromDeck.UseVisualStyleBackColor = true;
            this.btnEjectFromDeck.Click += new System.EventHandler(this.RemoveFromDeck);
            // 
            // btnLoadToPlaying
            // 
            this.btnLoadToPlaying.Enabled = false;
            this.btnLoadToPlaying.Font = new System.Drawing.Font("Lucida Sans Unicode", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLoadToPlaying.Image = ((System.Drawing.Image)(resources.GetObject("btnLoadToPlaying.Image")));
            this.btnLoadToPlaying.Location = new System.Drawing.Point(335, 35);
            this.btnLoadToPlaying.Name = "btnLoadToPlaying";
            this.btnLoadToPlaying.Size = new System.Drawing.Size(30, 32);
            this.btnLoadToPlaying.TabIndex = 3;
            this._toolTip.SetToolTip(this.btnLoadToPlaying, "Add to Now Playing");
            this.btnLoadToPlaying.UseVisualStyleBackColor = true;
            this.btnLoadToPlaying.Click += new System.EventHandler(this.MoveToPlaying);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Liberation Sans", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(5, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 21);
            this.label2.TabIndex = 1;
            this.label2.Text = "ON DECK";
            // 
            // pnlCollector
            // 
            this.pnlCollector.Controls.Add(this.label1);
            this.pnlCollector.Controls.Add(this.progScan);
            this.pnlCollector.Controls.Add(this.lblProgress);
            this.pnlCollector.Controls.Add(this.lblProduct);
            this.pnlCollector.Location = new System.Drawing.Point(13, 10);
            this.pnlCollector.Name = "pnlCollector";
            this.pnlCollector.Size = new System.Drawing.Size(311, 104);
            this.pnlCollector.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Lucida Sans Unicode", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(68, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(241, 23);
            this.label1.TabIndex = 5;
            this.label1.Text = "Update from Inventory";
            // 
            // progScan
            // 
            this.progScan.Location = new System.Drawing.Point(3, 49);
            this.progScan.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.progScan.Name = "progScan";
            this.progScan.Size = new System.Drawing.Size(306, 25);
            this.progScan.TabIndex = 4;
            // 
            // lblProgress
            // 
            this.lblProgress.AutoSize = true;
            this.lblProgress.Location = new System.Drawing.Point(79, 76);
            this.lblProgress.Name = "lblProgress";
            this.lblProgress.Size = new System.Drawing.Size(98, 21);
            this.lblProgress.TabIndex = 2;
            this.lblProgress.Text = "Progress: ";
            // 
            // lblProduct
            // 
            this.lblProduct.AutoSize = true;
            this.lblProduct.Location = new System.Drawing.Point(3, 30);
            this.lblProduct.Name = "lblProduct";
            this.lblProduct.Size = new System.Drawing.Size(86, 21);
            this.lblProduct.TabIndex = 3;
            this.lblProduct.Text = "Product: ";
            // 
            // tabConvertChkn
            // 
            this.tabConvertChkn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(29)))), ((int)(((byte)(51)))));
            this.tabConvertChkn.BackgroundImage = global::Triggerless.TriggerBot.Properties.Resources.bg2;
            this.tabConvertChkn.Controls.Add(this._splicer);
            this.tabConvertChkn.Location = new System.Drawing.Point(4, 30);
            this.tabConvertChkn.Name = "tabConvertChkn";
            this.tabConvertChkn.Padding = new System.Windows.Forms.Padding(3);
            this.tabConvertChkn.Size = new System.Drawing.Size(1050, 626);
            this.tabConvertChkn.TabIndex = 1;
            this.tabConvertChkn.Text = " Audio ▷ CHKN ";
            // 
            // tabLyrics
            // 
            this.tabLyrics.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(29)))), ((int)(((byte)(51)))));
            this.tabLyrics.Controls.Add(this.lyricsCtrl1);
            this.tabLyrics.Controls.Add(this.underConstructionCtrl1);
            this.tabLyrics.Location = new System.Drawing.Point(4, 30);
            this.tabLyrics.Name = "tabLyrics";
            this.tabLyrics.Size = new System.Drawing.Size(1050, 626);
            this.tabLyrics.TabIndex = 4;
            this.tabLyrics.Text = "Lyric Sheets ✏️ ";
            // 
            // tabTools
            // 
            this.tabTools.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(29)))), ((int)(((byte)(51)))));
            this.tabTools.BackgroundImage = global::Triggerless.TriggerBot.Properties.Resources.bg2;
            this.tabTools.Controls.Add(this.pnlTools);
            this.tabTools.Location = new System.Drawing.Point(4, 30);
            this.tabTools.Name = "tabTools";
            this.tabTools.Padding = new System.Windows.Forms.Padding(3);
            this.tabTools.Size = new System.Drawing.Size(1050, 626);
            this.tabTools.TabIndex = 2;
            this.tabTools.Text = " Tools 🔧 ";
            // 
            // pnlTools
            // 
            this.pnlTools.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(29)))), ((int)(((byte)(51)))));
            this.pnlTools.Controls.Add(this.tableLayoutPanel2);
            this.pnlTools.Location = new System.Drawing.Point(20, 20);
            this.pnlTools.Name = "pnlTools";
            this.pnlTools.Size = new System.Drawing.Size(620, 528);
            this.pnlTools.TabIndex = 6;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 64.64088F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35.35912F));
            this.tableLayoutPanel2.Controls.Add(this.label26, 0, 8);
            this.tableLayoutPanel2.Controls.Add(this.label15, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnViewLog, 1, 8);
            this.tableLayoutPanel2.Controls.Add(this.btnScanNew, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnTechSupport, 1, 6);
            this.tableLayoutPanel2.Controls.Add(this.label16, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.label18, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.label25, 0, 6);
            this.tableLayoutPanel2.Controls.Add(this.btnRescanAll, 1, 2);
            this.tableLayoutPanel2.Controls.Add(this.label19, 0, 5);
            this.tableLayoutPanel2.Controls.Add(this.btnDeepScan, 1, 4);
            this.tableLayoutPanel2.Controls.Add(this.label17, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.label22, 0, 4);
            this.tableLayoutPanel2.Controls.Add(this.label24, 0, 7);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(19, 13);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 10;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(589, 502);
            this.tableLayoutPanel2.TabIndex = 13;
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Font = new System.Drawing.Font("Liberation Sans", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label26.Location = new System.Drawing.Point(3, 407);
            this.label26.Margin = new System.Windows.Forms.Padding(3, 7, 3, 0);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(176, 21);
            this.label26.TabIndex = 14;
            this.label26.Text = "Last Scan Results";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Liberation Sans", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(3, 7);
            this.label15.Margin = new System.Windows.Forms.Padding(3, 7, 3, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(282, 21);
            this.label15.TabIndex = 0;
            this.label15.Text = "Scan for new Music Products";
            // 
            // btnViewLog
            // 
            this.btnViewLog.BackColor = System.Drawing.SystemColors.ControlDark;
            this.btnViewLog.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnViewLog.Location = new System.Drawing.Point(380, 400);
            this.btnViewLog.Margin = new System.Windows.Forms.Padding(0);
            this.btnViewLog.Name = "btnViewLog";
            this.btnViewLog.Size = new System.Drawing.Size(129, 31);
            this.btnViewLog.TabIndex = 12;
            this.btnViewLog.Text = "View Scan Log";
            this.btnViewLog.UseVisualStyleBackColor = false;
            this.btnViewLog.Click += new System.EventHandler(this.btnViewLog_Click);
            // 
            // btnScanNew
            // 
            this.btnScanNew.BackColor = System.Drawing.SystemColors.ControlDark;
            this.btnScanNew.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnScanNew.Location = new System.Drawing.Point(380, 0);
            this.btnScanNew.Margin = new System.Windows.Forms.Padding(0);
            this.btnScanNew.Name = "btnScanNew";
            this.btnScanNew.Size = new System.Drawing.Size(130, 31);
            this.btnScanNew.TabIndex = 2;
            this.btnScanNew.Text = "Scan New";
            this.btnScanNew.UseVisualStyleBackColor = false;
            this.btnScanNew.Click += new System.EventHandler(this.ScanInventory);
            // 
            // btnTechSupport
            // 
            this.btnTechSupport.BackColor = System.Drawing.SystemColors.ControlDark;
            this.btnTechSupport.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnTechSupport.Location = new System.Drawing.Point(380, 300);
            this.btnTechSupport.Margin = new System.Windows.Forms.Padding(0);
            this.btnTechSupport.Name = "btnTechSupport";
            this.btnTechSupport.Size = new System.Drawing.Size(129, 31);
            this.btnTechSupport.TabIndex = 11;
            this.btnTechSupport.Text = "Tech Support";
            this.btnTechSupport.UseVisualStyleBackColor = false;
            this.btnTechSupport.Click += new System.EventHandler(this.btnTechSupport_Click);
            // 
            // label16
            // 
            this.tableLayoutPanel2.SetColumnSpan(this.label16, 2);
            this.label16.Location = new System.Drawing.Point(3, 40);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(494, 41);
            this.label16.TabIndex = 1;
            this.label16.Text = "Hint: in IMVU, go to Dress Up, Recently Added, to update newly available inventor" +
    "y products.";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Liberation Sans", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(3, 107);
            this.label18.Margin = new System.Windows.Forms.Padding(3, 7, 3, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(246, 21);
            this.label18.TabIndex = 3;
            this.label18.Text = "Total Rescan of Inventory";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Font = new System.Drawing.Font("Liberation Sans", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label25.Location = new System.Drawing.Point(3, 307);
            this.label25.Margin = new System.Windows.Forms.Padding(3, 7, 3, 0);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(136, 21);
            this.label25.TabIndex = 9;
            this.label25.Text = "Tech Support";
            // 
            // btnRescanAll
            // 
            this.btnRescanAll.BackColor = System.Drawing.SystemColors.ControlDark;
            this.btnRescanAll.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnRescanAll.Location = new System.Drawing.Point(380, 100);
            this.btnRescanAll.Margin = new System.Windows.Forms.Padding(0);
            this.btnRescanAll.Name = "btnRescanAll";
            this.btnRescanAll.Size = new System.Drawing.Size(129, 31);
            this.btnRescanAll.TabIndex = 5;
            this.btnRescanAll.Text = "Rescan All";
            this.btnRescanAll.UseVisualStyleBackColor = false;
            this.btnRescanAll.Click += new System.EventHandler(this.RescanAll);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.tableLayoutPanel2.SetColumnSpan(this.label19, 2);
            this.label19.Location = new System.Drawing.Point(3, 240);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(574, 42);
            this.label19.TabIndex = 7;
            this.label19.Text = "You can scan product files that might have been missed. Works for all products, i" +
    "ncluding clothing and furniture.";
            // 
            // btnDeepScan
            // 
            this.btnDeepScan.BackColor = System.Drawing.SystemColors.ControlDark;
            this.btnDeepScan.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnDeepScan.Location = new System.Drawing.Point(380, 200);
            this.btnDeepScan.Margin = new System.Windows.Forms.Padding(0);
            this.btnDeepScan.Name = "btnDeepScan";
            this.btnDeepScan.Size = new System.Drawing.Size(129, 31);
            this.btnDeepScan.TabIndex = 8;
            this.btnDeepScan.Text = "Deep Scan";
            this.btnDeepScan.UseVisualStyleBackColor = false;
            this.btnDeepScan.Click += new System.EventHandler(this.btnDeepScan_Click);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.tableLayoutPanel2.SetColumnSpan(this.label17, 2);
            this.label17.Location = new System.Drawing.Point(3, 140);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(569, 42);
            this.label17.TabIndex = 4;
            this.label17.Text = "This will wipe out all Triggerbot data, and rescan from your product cache. This " +
    "can fix some problems and will not affect IMVU.";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("Liberation Sans", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.Location = new System.Drawing.Point(3, 207);
            this.label22.Margin = new System.Windows.Forms.Padding(3, 7, 3, 0);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(226, 21);
            this.label22.TabIndex = 6;
            this.label22.Text = "Deep Scan of Inventory";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.tableLayoutPanel2.SetColumnSpan(this.label24, 2);
            this.label24.Location = new System.Drawing.Point(3, 340);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(519, 42);
            this.label24.TabIndex = 10;
            this.label24.Text = "This will send a copy of the state of your IMVU inventory and Triggerbot to @Trig" +
    "gers, for debugging purposes";
            // 
            // tabAbout
            // 
            this.tabAbout.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(29)))), ((int)(((byte)(51)))));
            this.tabAbout.BackgroundImage = global::Triggerless.TriggerBot.Properties.Resources.bg2;
            this.tabAbout.Controls.Add(this.pnlAbout);
            this.tabAbout.Location = new System.Drawing.Point(4, 30);
            this.tabAbout.Name = "tabAbout";
            this.tabAbout.Padding = new System.Windows.Forms.Padding(3);
            this.tabAbout.Size = new System.Drawing.Size(1050, 626);
            this.tabAbout.TabIndex = 3;
            this.tabAbout.Text = " About... ";
            // 
            // pnlAbout
            // 
            this.pnlAbout.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(29)))), ((int)(((byte)(51)))));
            this.pnlAbout.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlAbout.Controls.Add(this.linkDiscord);
            this.pnlAbout.Controls.Add(this.lblDiscord);
            this.pnlAbout.Controls.Add(this.picDiscord);
            this.pnlAbout.Controls.Add(this.tableLayoutPanel1);
            this.pnlAbout.Font = new System.Drawing.Font("Liberation Sans", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlAbout.Location = new System.Drawing.Point(266, 80);
            this.pnlAbout.Name = "pnlAbout";
            this.pnlAbout.Size = new System.Drawing.Size(442, 436);
            this.pnlAbout.TabIndex = 14;
            // 
            // linkDiscord
            // 
            this.linkDiscord.ActiveLinkColor = System.Drawing.Color.Blue;
            this.linkDiscord.Font = new System.Drawing.Font("Liberation Sans", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkDiscord.LinkColor = System.Drawing.Color.Violet;
            this.linkDiscord.Location = new System.Drawing.Point(167, 374);
            this.linkDiscord.Margin = new System.Windows.Forms.Padding(3, 9, 3, 3);
            this.linkDiscord.Name = "linkDiscord";
            this.linkDiscord.Size = new System.Drawing.Size(231, 19);
            this.linkDiscord.TabIndex = 22;
            this.linkDiscord.TabStop = true;
            this.linkDiscord.Text = "https://discord.gg/AY83wS33";
            this.linkDiscord.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.linkDiscord.VisitedLinkColor = System.Drawing.Color.Blue;
            this.linkDiscord.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkDiscord_LinkClicked);
            // 
            // lblDiscord
            // 
            this.lblDiscord.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDiscord.BackColor = System.Drawing.Color.Transparent;
            this.lblDiscord.Font = new System.Drawing.Font("Liberation Sans", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDiscord.Location = new System.Drawing.Point(53, 373);
            this.lblDiscord.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDiscord.Name = "lblDiscord";
            this.lblDiscord.Size = new System.Drawing.Size(129, 21);
            this.lblDiscord.TabIndex = 23;
            this.lblDiscord.Text = "Discord server:";
            this.lblDiscord.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // picDiscord
            // 
            this.picDiscord.Image = ((System.Drawing.Image)(resources.GetObject("picDiscord.Image")));
            this.picDiscord.Location = new System.Drawing.Point(19, 368);
            this.picDiscord.Name = "picDiscord";
            this.picDiscord.Size = new System.Drawing.Size(32, 32);
            this.picDiscord.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picDiscord.TabIndex = 24;
            this.picDiscord.TabStop = false;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.lnkPage, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.label23, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label20, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.lblVersion, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.lblCopyright, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.label21, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.pictureBox1, 0, 0);
            this.tableLayoutPanel1.Font = new System.Drawing.Font("Lucida Sans", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tableLayoutPanel1.Location = new System.Drawing.Point(15, 24);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 8;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 98F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 37F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 92F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 19F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(400, 338);
            this.tableLayoutPanel1.TabIndex = 21;
            // 
            // lnkPage
            // 
            this.lnkPage.ActiveLinkColor = System.Drawing.Color.Lavender;
            this.lnkPage.Font = new System.Drawing.Font("Liberation Sans", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkPage.LinkColor = System.Drawing.Color.Violet;
            this.lnkPage.Location = new System.Drawing.Point(3, 308);
            this.lnkPage.Name = "lnkPage";
            this.lnkPage.Size = new System.Drawing.Size(394, 21);
            this.lnkPage.TabIndex = 20;
            this.lnkPage.TabStop = true;
            this.lnkPage.Text = "https://triggerless.com/triggerbot/";
            this.lnkPage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lnkPage.VisitedLinkColor = System.Drawing.Color.Blue;
            this.lnkPage.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkPage_Clicked);
            // 
            // label23
            // 
            this.label23.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label23.Font = new System.Drawing.Font("Liberation Sans", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label23.Location = new System.Drawing.Point(4, 100);
            this.label23.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(393, 21);
            this.label23.TabIndex = 15;
            this.label23.Text = "Triggerless Triggerbot";
            this.label23.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label20
            // 
            this.label20.Font = new System.Drawing.Font("Liberation Sans", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.Location = new System.Drawing.Point(4, 216);
            this.label20.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(393, 92);
            this.label20.TabIndex = 19;
            this.label20.Text = "Using C#, FFmpeg, NAudio, NVorbis, Dapper, SharpZipLib, SQLite, Triggerless.XAFLi" +
    "b and ChatGPT ";
            this.label20.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblVersion
            // 
            this.lblVersion.Font = new System.Drawing.Font("Liberation Sans", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVersion.Location = new System.Drawing.Point(4, 124);
            this.lblVersion.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(393, 21);
            this.lblVersion.TabIndex = 16;
            this.lblVersion.Text = "Version 0.0.0.0";
            this.lblVersion.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCopyright
            // 
            this.lblCopyright.Font = new System.Drawing.Font("Liberation Sans", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCopyright.Location = new System.Drawing.Point(4, 179);
            this.lblCopyright.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCopyright.Name = "lblCopyright";
            this.lblCopyright.Size = new System.Drawing.Size(393, 21);
            this.lblCopyright.TabIndex = 17;
            this.lblCopyright.Text = "Copyright 2023-2025 Triggerless.com";
            this.lblCopyright.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label21
            // 
            this.label21.Font = new System.Drawing.Font("Liberation Sans", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.Location = new System.Drawing.Point(4, 152);
            this.label21.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(393, 21);
            this.label21.TabIndex = 18;
            this.label21.Text = "\"Let the Music Do the Talking!\"";
            this.label21.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(4, 4);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(393, 81);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 14;
            this.pictureBox1.TabStop = false;
            // 
            // _triggerTimer
            // 
            this._triggerTimer.SynchronizingObject = this.picBanner;
            this._triggerTimer.Elapsed += new System.Timers.ElapsedEventHandler(this._triggerTimer_Elapsed);
            // 
            // _progressTimer
            // 
            this._progressTimer.Tick += new System.EventHandler(this.TriggerMadeProgress);
            // 
            // _lyricTimer
            // 
            this._lyricTimer.Tick += new System.EventHandler(this._lyricTimer_Tick);
            // 
            // lblNoResults
            // 
            this.lblNoResults.AutoSize = true;
            this.lblNoResults.Font = new System.Drawing.Font("Liberation Sans", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNoResults.Location = new System.Drawing.Point(94, 120);
            this.lblNoResults.Name = "lblNoResults";
            this.lblNoResults.Size = new System.Drawing.Size(307, 27);
            this.lblNoResults.TabIndex = 0;
            this.lblNoResults.Text = "No Search Results to Show";
            // 
            // pnlSearchResults
            // 
            this.pnlSearchResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlSearchResults.Location = new System.Drawing.Point(0, 45);
            this.pnlSearchResults.Name = "pnlSearchResults";
            this.pnlSearchResults.Size = new System.Drawing.Size(396, 575);
            this.pnlSearchResults.TabIndex = 5;
            this.pnlSearchResults.Resize += new System.EventHandler(this.pnlSearchResults_Resize);
            // 
            // productOnDeck
            // 
            this.productOnDeck.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(29)))), ((int)(((byte)(51)))));
            this.productOnDeck.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.productOnDeck.Font = new System.Drawing.Font("Lucida Sans Unicode", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.productOnDeck.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.productOnDeck.HideOnDeck = true;
            this.productOnDeck.Location = new System.Drawing.Point(4, 36);
            this.productOnDeck.Margin = new System.Windows.Forms.Padding(4);
            this.productOnDeck.Name = "productOnDeck";
            this.productOnDeck.ProductInfo = null;
            this.productOnDeck.Size = new System.Drawing.Size(324, 85);
            this.productOnDeck.TabIndex = 2;
            this._toolTip.SetToolTip(this.productOnDeck, "Trigger Product On Deck");
            this.productOnDeck.Visible = false;
            // 
            // _splicer
            // 
            this._splicer.AudioLength = 19.9D;
            this._splicer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(29)))), ((int)(((byte)(51)))));
            this._splicer.Font = new System.Drawing.Font("Lucida Sans Unicode", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._splicer.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this._splicer.Location = new System.Drawing.Point(34, 28);
            this._splicer.Margin = new System.Windows.Forms.Padding(4);
            this._splicer.Name = "_splicer";
            this._splicer.Size = new System.Drawing.Size(961, 512);
            this._splicer.TabIndex = 0;
            // 
            // lyricsCtrl1
            // 
            this.lyricsCtrl1.BackColor = System.Drawing.Color.MediumAquamarine;
            this.lyricsCtrl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lyricsCtrl1.Font = new System.Drawing.Font("Lucida Sans", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lyricsCtrl1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lyricsCtrl1.Location = new System.Drawing.Point(0, 0);
            this.lyricsCtrl1.Margin = new System.Windows.Forms.Padding(4);
            this.lyricsCtrl1.Name = "lyricsCtrl1";
            this.lyricsCtrl1.Size = new System.Drawing.Size(1050, 631);
            this.lyricsCtrl1.TabIndex = 0;
            // 
            // underConstructionCtrl1
            // 
            this.underConstructionCtrl1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("underConstructionCtrl1.BackgroundImage")));
            this.underConstructionCtrl1.Location = new System.Drawing.Point(0, 0);
            this.underConstructionCtrl1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.underConstructionCtrl1.Name = "underConstructionCtrl1";
            this.underConstructionCtrl1.Size = new System.Drawing.Size(89, 94);
            this.underConstructionCtrl1.TabIndex = 1;
            // 
            // _collector
            // 
            this._collector.CollectorEvent += new Triggerless.TriggerBot.Collector.CollectorEventHandler(this.OnCollectorEvent);
            // 
            // TriggerBotMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(29)))), ((int)(((byte)(51)))));
            this.ClientSize = new System.Drawing.Size(1058, 660);
            this.Controls.Add(this.lblNoResults);
            this.Controls.Add(this.tabAppContainer);
            this.Font = new System.Drawing.Font("Liberation Sans", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "TriggerBotMainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Triggerless TriggerBot";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TriggerBotMainForm_FormClosing);
            this.Load += new System.EventHandler(this.LoadForm);
            this.Shown += new System.EventHandler(this.ScanInventory);
            this.tabAppContainer.ResumeLayout(false);
            this.tabPlayback.ResumeLayout(false);
            this.splitter.Panel1.ResumeLayout(false);
            this.splitter.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitter)).EndInit();
            this.splitter.ResumeLayout(false);
            this.pnlSearch.ResumeLayout(false);
            this.pnlSearch.PerformLayout();
            this.pnlRight.ResumeLayout(false);
            this.pnlRight.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.pnlLag.ResumeLayout(false);
            this.pnlLag.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarLag)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridTriggers)).EndInit();
            this.pnlBanner.ResumeLayout(false);
            this.pnlBanner.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBanner)).EndInit();
            this.pnlOnDeck.ResumeLayout(false);
            this.pnlOnDeck.PerformLayout();
            this.pnlCollector.ResumeLayout(false);
            this.pnlCollector.PerformLayout();
            this.tabConvertChkn.ResumeLayout(false);
            this.tabLyrics.ResumeLayout(false);
            this.tabTools.ResumeLayout(false);
            this.pnlTools.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tabAbout.ResumeLayout(false);
            this.pnlAbout.ResumeLayout(false);
            this.pnlAbout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picDiscord)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._triggerTimer)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabAppContainer;
        private System.Windows.Forms.TabPage tabPlayback;
        private System.Windows.Forms.SplitContainer splitter;
        private System.Windows.Forms.Panel pnlSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.ToolTip _toolTip;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Panel pnlRight;
        private System.Windows.Forms.CheckBox chkHideTriggers;
        private System.Windows.Forms.Label lblCurrPlayingTrigger;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel pnlLag;
        private System.Windows.Forms.Label lblLagMinus;
        private System.Windows.Forms.Label lblLagMinusMinus;
        private System.Windows.Forms.Label lblLagPlusPlus;
        private System.Windows.Forms.Label lblLagPlus;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblLag;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ProgressBar progTrigger;
        private System.Windows.Forms.TrackBar trackBarLag;
        private System.Windows.Forms.Button btnAbort;
        private System.Windows.Forms.Button btnPlay;
        private System.Windows.Forms.ComboBox cboAdditionalTriggers;
        private System.Windows.Forms.Label lblAdditional;
        private System.Windows.Forms.DataGridView gridTriggers;
        private System.Windows.Forms.Label lblNowPlaying;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel pnlBanner;
        private System.Windows.Forms.PictureBox picBanner;
        private System.Windows.Forms.Panel pnlOnDeck;
        private System.Windows.Forms.CheckBox chkAutoCue;
        private System.Windows.Forms.Button btnEjectFromDeck;
        private System.Windows.Forms.Button btnLoadToPlaying;
        private System.Windows.Forms.Label label2;
        private ProductCtrl productOnDeck;
        private System.Windows.Forms.Panel pnlCollector;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ProgressBar progScan;
        private System.Windows.Forms.Label lblProgress;
        private System.Windows.Forms.Label lblProduct;
        private System.Windows.Forms.TabPage tabConvertChkn;
        private System.Timers.Timer _triggerTimer;
        private Collector _collector;
        private System.Windows.Forms.Timer _progressTimer;
        private System.Windows.Forms.TabPage tabTools;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Button btnScanNew;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Button btnRescanAll;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.CheckBox chkMinimizeOnPlay;
        private System.Windows.Forms.CheckBox chkKeepOnTop;
        private System.Windows.Forms.TabPage tabAbout;
        private System.Windows.Forms.Panel pnlAbout;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label lblCopyright;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.LinkLabel lnkPage;
        private System.Windows.Forms.Panel pnlTools;
        private SplicerControl _splicer;
        private System.Windows.Forms.Button btnAllAddnTriggers;
        private System.Windows.Forms.Button btnDeepScan;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Button btnTechSupport;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Button btnViewLog;
        private System.Windows.Forms.TabPage tabLyrics;
        private Components.LyricsCtrl lyricsCtrl1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.LinkLabel linkDiscord;
        private System.Windows.Forms.Label lblDiscord;
        private System.Windows.Forms.PictureBox picDiscord;
        private System.Windows.Forms.CheckBox chkLyrics;
        private System.Windows.Forms.Timer _lyricTimer;
        private System.Windows.Forms.Button btnLyricLag;
        private Components.UnderConstructionCtrl underConstructionCtrl1;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label lblSendToNP;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTrigger;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLengthMS;
        private Components.VirtualScrollPanel pnlSearchResults;
        private System.Windows.Forms.Label lblNoResults;
    }
}

