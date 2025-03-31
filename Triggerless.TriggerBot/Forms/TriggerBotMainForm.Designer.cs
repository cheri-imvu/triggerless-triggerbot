using System.ComponentModel;

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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tabAppContainer = new System.Windows.Forms.TabControl();
            this.tabPlayback = new System.Windows.Forms.TabPage();
            this.splitter = new System.Windows.Forms.SplitContainer();
            this.flowDisplay = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlSearch = new System.Windows.Forms.Panel();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.pnlRight = new System.Windows.Forms.Panel();
            this.linkTimeItText = new System.Windows.Forms.LinkLabel();
            this.btnAllAddnTriggers = new System.Windows.Forms.Button();
            this.chkMinimizeOnPlay = new System.Windows.Forms.CheckBox();
            this.chkKeepOnTop = new System.Windows.Forms.CheckBox();
            this.chkHideTriggers = new System.Windows.Forms.CheckBox();
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
            this.chkAutoCue = new System.Windows.Forms.CheckBox();
            this.btnEjectFromDeck = new System.Windows.Forms.Button();
            this.btnLoadToPlaying = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.productOnDeck = new Triggerless.TriggerBot.ProductCtrl();
            this.pnlCollector = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.progScan = new System.Windows.Forms.ProgressBar();
            this.lblProgress = new System.Windows.Forms.Label();
            this.lblProduct = new System.Windows.Forms.Label();
            this.tabTools = new System.Windows.Forms.TabPage();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnTechSupport = new System.Windows.Forms.Button();
            this.label24 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.btnDeepScan = new System.Windows.Forms.Button();
            this.label19 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.btnRescanAll = new System.Windows.Forms.Button();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.btnScanNew = new System.Windows.Forms.Button();
            this.label18 = new System.Windows.Forms.Label();
            this.tblConvertChkn = new System.Windows.Forms.TabPage();
            this._splicer = new Triggerless.TriggerBot.SplicerControl();
            this.tabAbout = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lnkPage = new System.Windows.Forms.LinkLabel();
            this.label20 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.lblCopyright = new System.Windows.Forms.Label();
            this.lblVersion = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this._toolTip = new System.Windows.Forms.ToolTip(this.components);
            this._triggerTimer = new System.Timers.Timer();
            this._progressTimer = new System.Windows.Forms.Timer(this.components);
            this._collector = new Triggerless.TriggerBot.Collector();
            this.btnViewLog = new System.Windows.Forms.Button();
            this.tabAppContainer.SuspendLayout();
            this.tabPlayback.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitter)).BeginInit();
            this.splitter.Panel1.SuspendLayout();
            this.splitter.Panel2.SuspendLayout();
            this.splitter.SuspendLayout();
            this.pnlSearch.SuspendLayout();
            this.pnlRight.SuspendLayout();
            this.pnlLag.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarLag)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridTriggers)).BeginInit();
            this.pnlBanner.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBanner)).BeginInit();
            this.pnlOnDeck.SuspendLayout();
            this.pnlCollector.SuspendLayout();
            this.tabTools.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tblConvertChkn.SuspendLayout();
            this.tabAbout.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._triggerTimer)).BeginInit();
            this.SuspendLayout();
            // 
            // tabAppContainer
            // 
            this.tabAppContainer.Controls.Add(this.tabPlayback);
            this.tabAppContainer.Controls.Add(this.tabTools);
            this.tabAppContainer.Controls.Add(this.tblConvertChkn);
            this.tabAppContainer.Controls.Add(this.tabAbout);
            this.tabAppContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabAppContainer.Location = new System.Drawing.Point(0, 0);
            this.tabAppContainer.Name = "tabAppContainer";
            this.tabAppContainer.SelectedIndex = 0;
            this.tabAppContainer.Size = new System.Drawing.Size(1149, 699);
            this.tabAppContainer.TabIndex = 0;
            // 
            // tabPlayback
            // 
            this.tabPlayback.Controls.Add(this.splitter);
            this.tabPlayback.Location = new System.Drawing.Point(4, 27);
            this.tabPlayback.Name = "tabPlayback";
            this.tabPlayback.Padding = new System.Windows.Forms.Padding(3);
            this.tabPlayback.Size = new System.Drawing.Size(1141, 668);
            this.tabPlayback.TabIndex = 0;
            this.tabPlayback.Text = " Playback ";
            this.tabPlayback.UseVisualStyleBackColor = true;
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
            this.splitter.Panel1.Controls.Add(this.flowDisplay);
            this.splitter.Panel1.Controls.Add(this.pnlSearch);
            this.splitter.Panel1MinSize = 390;
            // 
            // splitter.Panel2
            // 
            this.splitter.Panel2.Controls.Add(this.pnlRight);
            this.splitter.Panel2.Controls.Add(this.pnlCollector);
            this.splitter.Size = new System.Drawing.Size(1135, 662);
            this.splitter.SplitterDistance = 390;
            this.splitter.SplitterWidth = 5;
            this.splitter.TabIndex = 3;
            // 
            // flowDisplay
            // 
            this.flowDisplay.AutoScroll = true;
            this.flowDisplay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowDisplay.Location = new System.Drawing.Point(0, 48);
            this.flowDisplay.Name = "flowDisplay";
            this.flowDisplay.Size = new System.Drawing.Size(390, 614);
            this.flowDisplay.TabIndex = 4;
            // 
            // pnlSearch
            // 
            this.pnlSearch.Controls.Add(this.txtSearch);
            this.pnlSearch.Controls.Add(this.btnSearch);
            this.pnlSearch.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSearch.Location = new System.Drawing.Point(0, 0);
            this.pnlSearch.Name = "pnlSearch";
            this.pnlSearch.Size = new System.Drawing.Size(390, 48);
            this.pnlSearch.TabIndex = 3;
            // 
            // txtSearch
            // 
            this.txtSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearch.Font = new System.Drawing.Font("Verdana", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearch.Location = new System.Drawing.Point(47, 11);
            this.txtSearch.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(239, 24);
            this.txtSearch.TabIndex = 1;
            this._toolTip.SetToolTip(this.txtSearch, "Enter Search Term");
            this.txtSearch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSearch_KeyPress);
            // 
            // btnSearch
            // 
            this.btnSearch.Font = new System.Drawing.Font("Webdings", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.btnSearch.Image = ((System.Drawing.Image)(resources.GetObject("btnSearch.Image")));
            this.btnSearch.Location = new System.Drawing.Point(318, 6);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(35, 35);
            this.btnSearch.TabIndex = 2;
            this._toolTip.SetToolTip(this.btnSearch, "Search Title, Creator & Triggers");
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.DoSearch);
            // 
            // pnlRight
            // 
            this.pnlRight.Controls.Add(this.linkTimeItText);
            this.pnlRight.Controls.Add(this.btnAllAddnTriggers);
            this.pnlRight.Controls.Add(this.chkMinimizeOnPlay);
            this.pnlRight.Controls.Add(this.chkKeepOnTop);
            this.pnlRight.Controls.Add(this.chkHideTriggers);
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
            this.pnlRight.Size = new System.Drawing.Size(740, 662);
            this.pnlRight.TabIndex = 9;
            // 
            // linkTimeItText
            // 
            this.linkTimeItText.ActiveLinkColor = System.Drawing.Color.Blue;
            this.linkTimeItText.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.linkTimeItText.Font = new System.Drawing.Font("Lucida Sans Unicode", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkTimeItText.Location = new System.Drawing.Point(588, 324);
            this.linkTimeItText.Name = "linkTimeItText";
            this.linkTimeItText.Size = new System.Drawing.Size(132, 30);
            this.linkTimeItText.TabIndex = 22;
            this.linkTimeItText.TabStop = true;
            this.linkTimeItText.Text = "TiimeIt Text";
            this.linkTimeItText.VisitedLinkColor = System.Drawing.Color.Blue;
            this.linkTimeItText.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkTimeItText_LinkClicked);
            // 
            // btnAllAddnTriggers
            // 
            this.btnAllAddnTriggers.Location = new System.Drawing.Point(660, 141);
            this.btnAllAddnTriggers.Name = "btnAllAddnTriggers";
            this.btnAllAddnTriggers.Size = new System.Drawing.Size(74, 30);
            this.btnAllAddnTriggers.TabIndex = 21;
            this.btnAllAddnTriggers.Text = "more...";
            this.btnAllAddnTriggers.UseVisualStyleBackColor = true;
            this.btnAllAddnTriggers.Click += new System.EventHandler(this.AddnTriggersButtonClick);
            // 
            // chkMinimizeOnPlay
            // 
            this.chkMinimizeOnPlay.AutoSize = true;
            this.chkMinimizeOnPlay.Location = new System.Drawing.Point(563, 294);
            this.chkMinimizeOnPlay.Name = "chkMinimizeOnPlay";
            this.chkMinimizeOnPlay.Size = new System.Drawing.Size(153, 22);
            this.chkMinimizeOnPlay.TabIndex = 20;
            this.chkMinimizeOnPlay.Text = "Minimize On Play";
            this.chkMinimizeOnPlay.UseVisualStyleBackColor = true;
            // 
            // chkKeepOnTop
            // 
            this.chkKeepOnTop.AutoSize = true;
            this.chkKeepOnTop.Location = new System.Drawing.Point(429, 294);
            this.chkKeepOnTop.Name = "chkKeepOnTop";
            this.chkKeepOnTop.Size = new System.Drawing.Size(126, 22);
            this.chkKeepOnTop.TabIndex = 19;
            this.chkKeepOnTop.Text = "Keep On Top";
            this.chkKeepOnTop.UseVisualStyleBackColor = true;
            this.chkKeepOnTop.CheckedChanged += new System.EventHandler(this.chkStayOnTop_Clicked);
            // 
            // chkHideTriggers
            // 
            this.chkHideTriggers.AutoSize = true;
            this.chkHideTriggers.Location = new System.Drawing.Point(295, 294);
            this.chkHideTriggers.Name = "chkHideTriggers";
            this.chkHideTriggers.Size = new System.Drawing.Size(128, 22);
            this.chkHideTriggers.TabIndex = 18;
            this.chkHideTriggers.Text = "Hide Triggers";
            this.chkHideTriggers.UseVisualStyleBackColor = true;
            // 
            // lblCurrPlayingTrigger
            // 
            this.lblCurrPlayingTrigger.AutoSize = true;
            this.lblCurrPlayingTrigger.Location = new System.Drawing.Point(292, 262);
            this.lblCurrPlayingTrigger.Name = "lblCurrPlayingTrigger";
            this.lblCurrPlayingTrigger.Size = new System.Drawing.Size(104, 18);
            this.lblCurrPlayingTrigger.TabIndex = 17;
            this.lblCurrPlayingTrigger.Text = "--Pending--";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(292, 236);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(181, 18);
            this.label5.TabIndex = 16;
            this.label5.Text = "Current Playing Trigger";
            // 
            // pnlLag
            // 
            this.pnlLag.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlLag.BackColor = System.Drawing.SystemColors.Control;
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
            this.pnlLag.Location = new System.Drawing.Point(287, 376);
            this.pnlLag.Name = "pnlLag";
            this.pnlLag.Size = new System.Drawing.Size(446, 148);
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
            this.lblLagMinus.Location = new System.Drawing.Point(165, 8);
            this.lblLagMinus.Name = "lblLagMinus";
            this.lblLagMinus.Size = new System.Drawing.Size(33, 19);
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
            this.lblLagMinusMinus.Location = new System.Drawing.Point(126, 8);
            this.lblLagMinusMinus.Name = "lblLagMinusMinus";
            this.lblLagMinusMinus.Size = new System.Drawing.Size(33, 19);
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
            this.lblLagPlusPlus.Location = new System.Drawing.Point(339, 8);
            this.lblLagPlusPlus.Name = "lblLagPlusPlus";
            this.lblLagPlusPlus.Size = new System.Drawing.Size(33, 19);
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
            this.lblLagPlus.Location = new System.Drawing.Point(300, 8);
            this.lblLagPlus.Name = "lblLagPlus";
            this.lblLagPlus.Size = new System.Drawing.Size(33, 19);
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
            this.label8.Location = new System.Drawing.Point(414, 67);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(19, 12);
            this.label8.TabIndex = 31;
            this.label8.Text = "12";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.BackColor = System.Drawing.Color.Black;
            this.label14.Font = new System.Drawing.Font("Lucida Console", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.ForeColor = System.Drawing.Color.Lime;
            this.label14.Location = new System.Drawing.Point(348, 67);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(19, 12);
            this.label14.TabIndex = 30;
            this.label14.Text = "10";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.BackColor = System.Drawing.Color.Black;
            this.label13.Font = new System.Drawing.Font("Lucida Console", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.Lime;
            this.label13.Location = new System.Drawing.Point(282, 67);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(12, 12);
            this.label13.TabIndex = 29;
            this.label13.Text = "8";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.Black;
            this.label12.Font = new System.Drawing.Font("Lucida Console", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.Lime;
            this.label12.Location = new System.Drawing.Point(215, 67);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(12, 12);
            this.label12.TabIndex = 28;
            this.label12.Text = "6";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.Black;
            this.label11.Font = new System.Drawing.Font("Lucida Console", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.Lime;
            this.label11.Location = new System.Drawing.Point(149, 67);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(12, 12);
            this.label11.TabIndex = 27;
            this.label11.Text = "4";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.Black;
            this.label10.Font = new System.Drawing.Font("Lucida Console", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.Lime;
            this.label10.Location = new System.Drawing.Point(82, 67);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(12, 12);
            this.label10.TabIndex = 26;
            this.label10.Text = "2";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Black;
            this.label9.Font = new System.Drawing.Font("Lucida Console", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.Lime;
            this.label9.Location = new System.Drawing.Point(15, 67);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(12, 12);
            this.label9.TabIndex = 25;
            this.label9.Text = "0";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(250, 7);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(46, 18);
            this.label6.TabIndex = 24;
            this.label6.Text = "msec";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Lucida Sans Unicode", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(7, 7);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(85, 18);
            this.label4.TabIndex = 22;
            this.label4.Text = "Lag Factor";
            // 
            // lblLag
            // 
            this.lblLag.AutoSize = true;
            this.lblLag.Location = new System.Drawing.Point(214, 7);
            this.lblLag.Name = "lblLag";
            this.lblLag.Size = new System.Drawing.Size(40, 18);
            this.lblLag.TabIndex = 23;
            this.lblLag.Text = "6.00";
            this.lblLag.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Lucida Sans Unicode", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(7, 91);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(132, 18);
            this.label7.TabIndex = 16;
            this.label7.Text = "Trigger Progress";
            // 
            // progTrigger
            // 
            this.progTrigger.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progTrigger.Location = new System.Drawing.Point(21, 121);
            this.progTrigger.Name = "progTrigger";
            this.progTrigger.Size = new System.Drawing.Size(402, 10);
            this.progTrigger.TabIndex = 3;
            // 
            // trackBarLag
            // 
            this.trackBarLag.AutoSize = false;
            this.trackBarLag.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.trackBarLag.LargeChange = 4;
            this.trackBarLag.Location = new System.Drawing.Point(6, 33);
            this.trackBarLag.Maximum = 48;
            this.trackBarLag.Name = "trackBarLag";
            this.trackBarLag.Size = new System.Drawing.Size(431, 45);
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
            this.btnAbort.Location = new System.Drawing.Point(335, 189);
            this.btnAbort.Name = "btnAbort";
            this.btnAbort.Size = new System.Drawing.Size(34, 34);
            this.btnAbort.TabIndex = 11;
            this._toolTip.SetToolTip(this.btnAbort, "Abort Triggers");
            this.btnAbort.UseVisualStyleBackColor = true;
            this.btnAbort.Click += new System.EventHandler(this.AbortPlaying);
            // 
            // btnPlay
            // 
            this.btnPlay.Enabled = false;
            this.btnPlay.Font = new System.Drawing.Font("Lucida Sans Unicode", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPlay.Image = ((System.Drawing.Image)(resources.GetObject("btnPlay.Image")));
            this.btnPlay.Location = new System.Drawing.Point(293, 189);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(34, 34);
            this.btnPlay.TabIndex = 10;
            this._toolTip.SetToolTip(this.btnPlay, "Play Triggers");
            this.btnPlay.UseVisualStyleBackColor = true;
            this.btnPlay.Click += new System.EventHandler(this.StartPlayingClicked);
            // 
            // cboAdditionalTriggers
            // 
            this.cboAdditionalTriggers.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboAdditionalTriggers.FormattingEnabled = true;
            this.cboAdditionalTriggers.Location = new System.Drawing.Point(287, 144);
            this.cboAdditionalTriggers.Name = "cboAdditionalTriggers";
            this.cboAdditionalTriggers.Size = new System.Drawing.Size(368, 26);
            this.cboAdditionalTriggers.TabIndex = 9;
            // 
            // lblAdditional
            // 
            this.lblAdditional.AutoSize = true;
            this.lblAdditional.Location = new System.Drawing.Point(286, 121);
            this.lblAdditional.Name = "lblAdditional";
            this.lblAdditional.Size = new System.Drawing.Size(150, 18);
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
            this.gridTriggers.BackgroundColor = System.Drawing.SystemColors.Control;
            this.gridTriggers.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Lucida Sans Unicode", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridTriggers.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.gridTriggers.ColumnHeadersHeight = 25;
            this.gridTriggers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.gridTriggers.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colTrigger,
            this.colLengthMS});
            this.gridTriggers.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.gridTriggers.EnableHeadersVisualStyles = false;
            this.gridTriggers.Location = new System.Drawing.Point(4, 120);
            this.gridTriggers.MultiSelect = false;
            this.gridTriggers.Name = "gridTriggers";
            this.gridTriggers.ReadOnly = true;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.AppWorkspace;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Lucida Sans Unicode", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gridTriggers.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.gridTriggers.RowHeadersWidth = 5;
            this.gridTriggers.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.gridTriggers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridTriggers.Size = new System.Drawing.Size(277, 400);
            this.gridTriggers.TabIndex = 7;
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
            this.lblNowPlaying.Font = new System.Drawing.Font("Lucida Sans Unicode", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNowPlaying.Location = new System.Drawing.Point(136, 94);
            this.lblNowPlaying.Name = "lblNowPlaying";
            this.lblNowPlaying.Size = new System.Drawing.Size(104, 18);
            this.lblNowPlaying.TabIndex = 6;
            this.lblNowPlaying.Text = "--Pending--";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Lucida Sans Unicode", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(6, 94);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(122, 18);
            this.label3.TabIndex = 5;
            this.label3.Text = "NOW PLAYING";
            // 
            // pnlBanner
            // 
            this.pnlBanner.BackColor = System.Drawing.Color.Silver;
            this.pnlBanner.Controls.Add(this.picBanner);
            this.pnlBanner.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlBanner.Location = new System.Drawing.Point(0, 0);
            this.pnlBanner.Name = "pnlBanner";
            this.pnlBanner.Size = new System.Drawing.Size(740, 88);
            this.pnlBanner.TabIndex = 4;
            // 
            // picBanner
            // 
            this.picBanner.Image = ((System.Drawing.Image)(resources.GetObject("picBanner.Image")));
            this.picBanner.Location = new System.Drawing.Point(139, 0);
            this.picBanner.Name = "picBanner";
            this.picBanner.Size = new System.Drawing.Size(339, 86);
            this.picBanner.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picBanner.TabIndex = 0;
            this.picBanner.TabStop = false;
            // 
            // pnlOnDeck
            // 
            this.pnlOnDeck.BackColor = System.Drawing.Color.Silver;
            this.pnlOnDeck.Controls.Add(this.chkAutoCue);
            this.pnlOnDeck.Controls.Add(this.btnEjectFromDeck);
            this.pnlOnDeck.Controls.Add(this.btnLoadToPlaying);
            this.pnlOnDeck.Controls.Add(this.label2);
            this.pnlOnDeck.Controls.Add(this.productOnDeck);
            this.pnlOnDeck.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlOnDeck.Location = new System.Drawing.Point(0, 526);
            this.pnlOnDeck.Name = "pnlOnDeck";
            this.pnlOnDeck.Size = new System.Drawing.Size(740, 136);
            this.pnlOnDeck.TabIndex = 3;
            // 
            // chkAutoCue
            // 
            this.chkAutoCue.AutoSize = true;
            this.chkAutoCue.Location = new System.Drawing.Point(107, 10);
            this.chkAutoCue.Name = "chkAutoCue";
            this.chkAutoCue.Size = new System.Drawing.Size(268, 22);
            this.chkAutoCue.TabIndex = 5;
            this.chkAutoCue.Text = "Auto play after this song is done";
            this.chkAutoCue.UseVisualStyleBackColor = true;
            // 
            // btnEjectFromDeck
            // 
            this.btnEjectFromDeck.Enabled = false;
            this.btnEjectFromDeck.Image = ((System.Drawing.Image)(resources.GetObject("btnEjectFromDeck.Image")));
            this.btnEjectFromDeck.Location = new System.Drawing.Point(377, 94);
            this.btnEjectFromDeck.Name = "btnEjectFromDeck";
            this.btnEjectFromDeck.Size = new System.Drawing.Size(34, 34);
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
            this.btnLoadToPlaying.Location = new System.Drawing.Point(377, 37);
            this.btnLoadToPlaying.Name = "btnLoadToPlaying";
            this.btnLoadToPlaying.Size = new System.Drawing.Size(34, 34);
            this.btnLoadToPlaying.TabIndex = 3;
            this._toolTip.SetToolTip(this.btnLoadToPlaying, "Add to Now Playing");
            this.btnLoadToPlaying.UseVisualStyleBackColor = true;
            this.btnLoadToPlaying.Click += new System.EventHandler(this.MoveToPlaying);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Lucida Sans Unicode", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(6, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 18);
            this.label2.TabIndex = 1;
            this.label2.Text = "ON DECK";
            // 
            // productOnDeck
            // 
            this.productOnDeck.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(248)))));
            this.productOnDeck.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.productOnDeck.Font = new System.Drawing.Font("Lucida Sans Unicode", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.productOnDeck.HideOnDeck = true;
            this.productOnDeck.Location = new System.Drawing.Point(5, 38);
            this.productOnDeck.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.productOnDeck.Name = "productOnDeck";
            this.productOnDeck.ProductInfo = null;
            this.productOnDeck.Size = new System.Drawing.Size(364, 90);
            this.productOnDeck.TabIndex = 2;
            this._toolTip.SetToolTip(this.productOnDeck, "Trigger Product On Deck");
            this.productOnDeck.Visible = false;
            // 
            // pnlCollector
            // 
            this.pnlCollector.Controls.Add(this.label1);
            this.pnlCollector.Controls.Add(this.progScan);
            this.pnlCollector.Controls.Add(this.lblProgress);
            this.pnlCollector.Controls.Add(this.lblProduct);
            this.pnlCollector.Location = new System.Drawing.Point(15, 11);
            this.pnlCollector.Name = "pnlCollector";
            this.pnlCollector.Size = new System.Drawing.Size(350, 110);
            this.pnlCollector.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Lucida Sans Unicode", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(77, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(197, 18);
            this.label1.TabIndex = 5;
            this.label1.Text = "Update from Inventory";
            // 
            // progScan
            // 
            this.progScan.Location = new System.Drawing.Point(3, 52);
            this.progScan.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.progScan.Name = "progScan";
            this.progScan.Size = new System.Drawing.Size(344, 26);
            this.progScan.TabIndex = 4;
            // 
            // lblProgress
            // 
            this.lblProgress.AutoSize = true;
            this.lblProgress.Location = new System.Drawing.Point(89, 80);
            this.lblProgress.Name = "lblProgress";
            this.lblProgress.Size = new System.Drawing.Size(82, 18);
            this.lblProgress.TabIndex = 2;
            this.lblProgress.Text = "Progress: ";
            // 
            // lblProduct
            // 
            this.lblProduct.AutoSize = true;
            this.lblProduct.Location = new System.Drawing.Point(3, 32);
            this.lblProduct.Name = "lblProduct";
            this.lblProduct.Size = new System.Drawing.Size(76, 18);
            this.lblProduct.TabIndex = 3;
            this.lblProduct.Text = "Product: ";
            // 
            // tabTools
            // 
            this.tabTools.BackgroundImage = global::Triggerless.TriggerBot.Properties.Resources.bg2;
            this.tabTools.Controls.Add(this.panel2);
            this.tabTools.Location = new System.Drawing.Point(4, 27);
            this.tabTools.Name = "tabTools";
            this.tabTools.Padding = new System.Windows.Forms.Padding(3);
            this.tabTools.Size = new System.Drawing.Size(1141, 668);
            this.tabTools.TabIndex = 2;
            this.tabTools.Text = " Tools ";
            this.tabTools.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Gainsboro;
            this.panel2.Controls.Add(this.btnViewLog);
            this.panel2.Controls.Add(this.btnTechSupport);
            this.panel2.Controls.Add(this.label24);
            this.panel2.Controls.Add(this.label25);
            this.panel2.Controls.Add(this.btnDeepScan);
            this.panel2.Controls.Add(this.label19);
            this.panel2.Controls.Add(this.label22);
            this.panel2.Controls.Add(this.label15);
            this.panel2.Controls.Add(this.btnRescanAll);
            this.panel2.Controls.Add(this.label16);
            this.panel2.Controls.Add(this.label17);
            this.panel2.Controls.Add(this.btnScanNew);
            this.panel2.Controls.Add(this.label18);
            this.panel2.Location = new System.Drawing.Point(22, 21);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(646, 415);
            this.panel2.TabIndex = 6;
            // 
            // btnTechSupport
            // 
            this.btnTechSupport.BackColor = System.Drawing.SystemColors.ControlDark;
            this.btnTechSupport.Location = new System.Drawing.Point(250, 282);
            this.btnTechSupport.Name = "btnTechSupport";
            this.btnTechSupport.Size = new System.Drawing.Size(145, 33);
            this.btnTechSupport.TabIndex = 11;
            this.btnTechSupport.Text = "Tech Support";
            this.btnTechSupport.UseVisualStyleBackColor = false;
            this.btnTechSupport.Click += new System.EventHandler(this.btnTechSupport_Click);
            // 
            // label24
            // 
            this.label24.Location = new System.Drawing.Point(18, 318);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(571, 41);
            this.label24.TabIndex = 10;
            this.label24.Text = "This will send a copy of the state of your IMVU inventory and Triggerbot to @Trig" +
    "gers, for debugging purposes";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(18, 290);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(108, 18);
            this.label25.TabIndex = 9;
            this.label25.Text = "Tech Support";
            // 
            // btnDeepScan
            // 
            this.btnDeepScan.BackColor = System.Drawing.SystemColors.ControlDark;
            this.btnDeepScan.Location = new System.Drawing.Point(250, 202);
            this.btnDeepScan.Name = "btnDeepScan";
            this.btnDeepScan.Size = new System.Drawing.Size(145, 33);
            this.btnDeepScan.TabIndex = 8;
            this.btnDeepScan.Text = "Deep Scan";
            this.btnDeepScan.UseVisualStyleBackColor = false;
            this.btnDeepScan.Click += new System.EventHandler(this.btnDeepScan_Click);
            // 
            // label19
            // 
            this.label19.Location = new System.Drawing.Point(18, 238);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(571, 41);
            this.label19.TabIndex = 7;
            this.label19.Text = "You can scan product files that might have been missed. Works for all products, i" +
    "ncluding clothing and furniture.";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(18, 210);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(180, 18);
            this.label22.TabIndex = 6;
            this.label22.Text = "Deep Scan of Inventory";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(12, 24);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(234, 18);
            this.label15.TabIndex = 0;
            this.label15.Text = "Scan for new Music Products...";
            // 
            // btnRescanAll
            // 
            this.btnRescanAll.BackColor = System.Drawing.SystemColors.ControlDark;
            this.btnRescanAll.Location = new System.Drawing.Point(250, 112);
            this.btnRescanAll.Name = "btnRescanAll";
            this.btnRescanAll.Size = new System.Drawing.Size(145, 33);
            this.btnRescanAll.TabIndex = 5;
            this.btnRescanAll.Text = "Rescan All";
            this.btnRescanAll.UseVisualStyleBackColor = false;
            this.btnRescanAll.Click += new System.EventHandler(this.RescanAll);
            // 
            // label16
            // 
            this.label16.Location = new System.Drawing.Point(12, 57);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(556, 43);
            this.label16.TabIndex = 1;
            this.label16.Text = "Hint: in IMVU, go to Dress Up, Recently Added, to update new available inventory " +
    "products.";
            // 
            // label17
            // 
            this.label17.Location = new System.Drawing.Point(18, 153);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(565, 39);
            this.label17.TabIndex = 4;
            this.label17.Text = "This will wipe out all Triggerbot data, and rescan from your product cache. This " +
    "can fix some problems and will not affect IMVU.";
            // 
            // btnScanNew
            // 
            this.btnScanNew.BackColor = System.Drawing.SystemColors.ControlDark;
            this.btnScanNew.Location = new System.Drawing.Point(249, 17);
            this.btnScanNew.Name = "btnScanNew";
            this.btnScanNew.Size = new System.Drawing.Size(146, 33);
            this.btnScanNew.TabIndex = 2;
            this.btnScanNew.Text = "Scan New";
            this.btnScanNew.UseVisualStyleBackColor = false;
            this.btnScanNew.Click += new System.EventHandler(this.ScanInventory);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(18, 120);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(196, 18);
            this.label18.TabIndex = 3;
            this.label18.Text = "Total Rescan of Inventory";
            // 
            // tblConvertChkn
            // 
            this.tblConvertChkn.BackgroundImage = global::Triggerless.TriggerBot.Properties.Resources.bg2;
            this.tblConvertChkn.Controls.Add(this._splicer);
            this.tblConvertChkn.Location = new System.Drawing.Point(4, 27);
            this.tblConvertChkn.Name = "tblConvertChkn";
            this.tblConvertChkn.Padding = new System.Windows.Forms.Padding(3);
            this.tblConvertChkn.Size = new System.Drawing.Size(1141, 668);
            this.tblConvertChkn.TabIndex = 1;
            this.tblConvertChkn.Text = " MP3 Audio Splicer ";
            this.tblConvertChkn.UseVisualStyleBackColor = true;
            // 
            // _splicer
            // 
            this._splicer.AudioLength = 19.9D;
            this._splicer.BackColor = System.Drawing.Color.Gainsboro;
            this._splicer.Font = new System.Drawing.Font("Lucida Sans Unicode", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._splicer.Location = new System.Drawing.Point(38, 30);
            this._splicer.Margin = new System.Windows.Forms.Padding(4);
            this._splicer.Name = "_splicer";
            this._splicer.Size = new System.Drawing.Size(860, 476);
            this._splicer.TabIndex = 0;
            this._splicer.Load += new System.EventHandler(this._splicer_Load);
            // 
            // tabAbout
            // 
            this.tabAbout.BackgroundImage = global::Triggerless.TriggerBot.Properties.Resources.bg2;
            this.tabAbout.Controls.Add(this.panel1);
            this.tabAbout.Location = new System.Drawing.Point(4, 27);
            this.tabAbout.Name = "tabAbout";
            this.tabAbout.Padding = new System.Windows.Forms.Padding(3);
            this.tabAbout.Size = new System.Drawing.Size(1141, 668);
            this.tabAbout.TabIndex = 3;
            this.tabAbout.Text = " About ";
            this.tabAbout.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Gainsboro;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.lnkPage);
            this.panel1.Controls.Add(this.label20);
            this.panel1.Controls.Add(this.label21);
            this.panel1.Controls.Add(this.lblCopyright);
            this.panel1.Controls.Add(this.lblVersion);
            this.panel1.Controls.Add(this.label23);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Location = new System.Drawing.Point(269, 57);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(413, 442);
            this.panel1.TabIndex = 14;
            // 
            // lnkPage
            // 
            this.lnkPage.ActiveLinkColor = System.Drawing.Color.Blue;
            this.lnkPage.AutoSize = true;
            this.lnkPage.Location = new System.Drawing.Point(59, 365);
            this.lnkPage.Name = "lnkPage";
            this.lnkPage.Size = new System.Drawing.Size(275, 18);
            this.lnkPage.TabIndex = 20;
            this.lnkPage.TabStop = true;
            this.lnkPage.Text = "https://triggerless.com/triggerbot/";
            this.lnkPage.VisitedLinkColor = System.Drawing.Color.Blue;
            this.lnkPage.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkPage_Clicked);
            // 
            // label20
            // 
            this.label20.Location = new System.Drawing.Point(62, 283);
            this.label20.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(318, 117);
            this.label20.TabIndex = 19;
            this.label20.Text = "Using C#, FFMPEG, NAudio, NVorbis, Dapper, SharpZipLib, SQLite, Triggerless.XAFLi" +
    "b and ChatGPT ";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(59, 200);
            this.label21.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(237, 18);
            this.label21.TabIndex = 18;
            this.label21.Text = "\"Let the Music Do the Talking!\"";
            // 
            // lblCopyright
            // 
            this.lblCopyright.AutoSize = true;
            this.lblCopyright.Location = new System.Drawing.Point(62, 245);
            this.lblCopyright.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCopyright.Name = "lblCopyright";
            this.lblCopyright.Size = new System.Drawing.Size(247, 18);
            this.lblCopyright.TabIndex = 17;
            this.lblCopyright.Text = "Copyright 2023 Triggerless.com";
            // 
            // lblVersion
            // 
            this.lblVersion.AutoSize = true;
            this.lblVersion.Location = new System.Drawing.Point(119, 172);
            this.lblVersion.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(119, 18);
            this.lblVersion.TabIndex = 16;
            this.lblVersion.Text = "Version 0.0.0.0";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(92, 145);
            this.label23.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(176, 18);
            this.label23.TabIndex = 15;
            this.label23.Text = "Triggerless Triggerbot";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(34, 22);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(339, 86);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
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
            // _collector
            // 
            this._collector.CollectorEvent += new Triggerless.TriggerBot.Collector.CollectorEventHandler(this.OnCollectorEvent);
            // 
            // btnViewLog
            // 
            this.btnViewLog.BackColor = System.Drawing.SystemColors.ControlDark;
            this.btnViewLog.Location = new System.Drawing.Point(249, 362);
            this.btnViewLog.Name = "btnViewLog";
            this.btnViewLog.Size = new System.Drawing.Size(145, 33);
            this.btnViewLog.TabIndex = 12;
            this.btnViewLog.Text = "View Log";
            this.btnViewLog.UseVisualStyleBackColor = false;
            this.btnViewLog.Click += new System.EventHandler(this.btnViewLog_Click);
            // 
            // TriggerBotMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1149, 699);
            this.Controls.Add(this.tabAppContainer);
            this.Font = new System.Drawing.Font("Lucida Sans Unicode", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.tabTools.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.tblConvertChkn.ResumeLayout(false);
            this.tabAbout.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._triggerTimer)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabAppContainer;
        private System.Windows.Forms.TabPage tabPlayback;
        private System.Windows.Forms.SplitContainer splitter;
        private System.Windows.Forms.FlowLayoutPanel flowDisplay;
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
        private System.Windows.Forms.DataGridViewTextBoxColumn colTrigger;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLengthMS;
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
        private System.Windows.Forms.TabPage tblConvertChkn;
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
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label lblCopyright;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.LinkLabel lnkPage;
        private System.Windows.Forms.Panel panel2;
        private SplicerControl _splicer;
        private System.Windows.Forms.Button btnAllAddnTriggers;
        private System.Windows.Forms.Button btnDeepScan;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Button btnTechSupport;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.LinkLabel linkTimeItText;
        private System.Windows.Forms.Button btnViewLog;
    }
}

