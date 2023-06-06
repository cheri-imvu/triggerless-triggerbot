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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.splitter = new System.Windows.Forms.SplitContainer();
            this.flowDisplay = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlSearch = new System.Windows.Forms.Panel();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.pnlRight = new System.Windows.Forms.Panel();
            this.lblCurrPlayingTrigger = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.pnlLag = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.progTrigger = new System.Windows.Forms.ProgressBar();
            this.label6 = new System.Windows.Forms.Label();
            this.trackBarLag = new System.Windows.Forms.TrackBar();
            this.label4 = new System.Windows.Forms.Label();
            this.lblLag = new System.Windows.Forms.Label();
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
            this._triggerTimer = new System.Timers.Timer();
            this._toolTip = new System.Windows.Forms.ToolTip(this.components);
            this._topMenu = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sliceAudioFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.minimizeMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stayOnTopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.actionsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.scanForNewProductsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.totalRescanToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.debugToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.doSomething1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.webPageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutTriggerbotToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._progressTimer = new System.Windows.Forms.Timer(this.components);
            this._collector = new Triggerless.TriggerBot.Collector();
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
            ((System.ComponentModel.ISupportInitialize)(this._triggerTimer)).BeginInit();
            this._topMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitter
            // 
            this.splitter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitter.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitter.Location = new System.Drawing.Point(0, 28);
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
            this.splitter.Size = new System.Drawing.Size(1015, 671);
            this.splitter.SplitterDistance = 390;
            this.splitter.SplitterWidth = 5;
            this.splitter.TabIndex = 1;
            // 
            // flowDisplay
            // 
            this.flowDisplay.AutoScroll = true;
            this.flowDisplay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowDisplay.Location = new System.Drawing.Point(0, 48);
            this.flowDisplay.Name = "flowDisplay";
            this.flowDisplay.Size = new System.Drawing.Size(390, 623);
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
            this.txtSearch.Size = new System.Drawing.Size(239, 28);
            this.txtSearch.TabIndex = 1;
            this._toolTip.SetToolTip(this.txtSearch, "Enter Search Term");
            this.txtSearch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSearch_KeyPress);
            // 
            // btnSearch
            // 
            this.btnSearch.Font = new System.Drawing.Font("Webdings", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.btnSearch.Image = ((System.Drawing.Image)(resources.GetObject("btnSearch.Image")));
            this.btnSearch.Location = new System.Drawing.Point(293, 6);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(35, 35);
            this.btnSearch.TabIndex = 2;
            this._toolTip.SetToolTip(this.btnSearch, "Search Title, Creator & Triggers");
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.DoSearch);
            // 
            // pnlRight
            // 
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
            this.pnlRight.Size = new System.Drawing.Size(620, 671);
            this.pnlRight.TabIndex = 9;
            // 
            // lblCurrPlayingTrigger
            // 
            this.lblCurrPlayingTrigger.AutoSize = true;
            this.lblCurrPlayingTrigger.Location = new System.Drawing.Point(292, 262);
            this.lblCurrPlayingTrigger.Name = "lblCurrPlayingTrigger";
            this.lblCurrPlayingTrigger.Size = new System.Drawing.Size(130, 23);
            this.lblCurrPlayingTrigger.TabIndex = 17;
            this.lblCurrPlayingTrigger.Text = "--Pending--";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(292, 236);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(230, 23);
            this.label5.TabIndex = 16;
            this.label5.Text = "Current Playing Trigger";
            // 
            // pnlLag
            // 
            this.pnlLag.Controls.Add(this.label7);
            this.pnlLag.Controls.Add(this.progTrigger);
            this.pnlLag.Controls.Add(this.label6);
            this.pnlLag.Controls.Add(this.trackBarLag);
            this.pnlLag.Controls.Add(this.label4);
            this.pnlLag.Controls.Add(this.lblLag);
            this.pnlLag.Location = new System.Drawing.Point(287, 428);
            this.pnlLag.Name = "pnlLag";
            this.pnlLag.Size = new System.Drawing.Size(327, 105);
            this.pnlLag.TabIndex = 15;
            this.pnlLag.Visible = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 60);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(167, 23);
            this.label7.TabIndex = 16;
            this.label7.Text = "Trigger Progress";
            // 
            // progTrigger
            // 
            this.progTrigger.Location = new System.Drawing.Point(4, 81);
            this.progTrigger.Name = "progTrigger";
            this.progTrigger.Size = new System.Drawing.Size(315, 12);
            this.progTrigger.TabIndex = 3;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(109, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(59, 23);
            this.label6.TabIndex = 15;
            this.label6.Text = "msec";
            // 
            // trackBarLag
            // 
            this.trackBarLag.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.trackBarLag.Location = new System.Drawing.Point(6, 21);
            this.trackBarLag.Maximum = 100;
            this.trackBarLag.Name = "trackBarLag";
            this.trackBarLag.Size = new System.Drawing.Size(317, 56);
            this.trackBarLag.TabIndex = 12;
            this.trackBarLag.ValueChanged += new System.EventHandler(this.LagControlChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(107, 23);
            this.label4.TabIndex = 13;
            this.label4.Text = "Lag Factor";
            // 
            // lblLag
            // 
            this.lblLag.AutoSize = true;
            this.lblLag.Location = new System.Drawing.Point(88, 0);
            this.lblLag.Name = "lblLag";
            this.lblLag.Size = new System.Drawing.Size(34, 23);
            this.lblLag.TabIndex = 14;
            this.lblLag.Text = "99";
            this.lblLag.TextAlign = System.Drawing.ContentAlignment.TopRight;
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
            this.btnPlay.Click += new System.EventHandler(this.StartPlaying);
            // 
            // cboAdditionalTriggers
            // 
            this.cboAdditionalTriggers.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboAdditionalTriggers.FormattingEnabled = true;
            this.cboAdditionalTriggers.Location = new System.Drawing.Point(287, 144);
            this.cboAdditionalTriggers.Name = "cboAdditionalTriggers";
            this.cboAdditionalTriggers.Size = new System.Drawing.Size(327, 30);
            this.cboAdditionalTriggers.TabIndex = 9;
            // 
            // lblAdditional
            // 
            this.lblAdditional.AutoSize = true;
            this.lblAdditional.Location = new System.Drawing.Point(286, 121);
            this.lblAdditional.Name = "lblAdditional";
            this.lblAdditional.Size = new System.Drawing.Size(192, 23);
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
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Lucida Sans Unicode", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.gridTriggers.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.gridTriggers.EnableHeadersVisualStyles = false;
            this.gridTriggers.Location = new System.Drawing.Point(4, 120);
            this.gridTriggers.MultiSelect = false;
            this.gridTriggers.Name = "gridTriggers";
            this.gridTriggers.ReadOnly = true;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.AppWorkspace;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Lucida Sans Unicode", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gridTriggers.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.gridTriggers.RowHeadersWidth = 5;
            this.gridTriggers.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.gridTriggers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridTriggers.Size = new System.Drawing.Size(277, 409);
            this.gridTriggers.TabIndex = 7;
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
            this.lblNowPlaying.Size = new System.Drawing.Size(130, 23);
            this.lblNowPlaying.TabIndex = 6;
            this.lblNowPlaying.Text = "--Pending--";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Lucida Sans Unicode", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(6, 94);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(151, 23);
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
            this.pnlBanner.Size = new System.Drawing.Size(620, 88);
            this.pnlBanner.TabIndex = 4;
            this.pnlBanner.Resize += new System.EventHandler(this.RelocateBanner);
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
            this.pnlOnDeck.Location = new System.Drawing.Point(0, 535);
            this.pnlOnDeck.Name = "pnlOnDeck";
            this.pnlOnDeck.Size = new System.Drawing.Size(620, 136);
            this.pnlOnDeck.TabIndex = 3;
            // 
            // chkAutoCue
            // 
            this.chkAutoCue.AutoSize = true;
            this.chkAutoCue.Location = new System.Drawing.Point(107, 10);
            this.chkAutoCue.Name = "chkAutoCue";
            this.chkAutoCue.Size = new System.Drawing.Size(339, 27);
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
            this.label2.Size = new System.Drawing.Size(101, 23);
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
            this.label1.Size = new System.Drawing.Size(241, 23);
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
            this.lblProgress.Size = new System.Drawing.Size(104, 23);
            this.lblProgress.TabIndex = 2;
            this.lblProgress.Text = "Progress: ";
            // 
            // lblProduct
            // 
            this.lblProduct.AutoSize = true;
            this.lblProduct.Location = new System.Drawing.Point(3, 32);
            this.lblProduct.Name = "lblProduct";
            this.lblProduct.Size = new System.Drawing.Size(94, 23);
            this.lblProduct.TabIndex = 3;
            this.lblProduct.Text = "Product: ";
            // 
            // _triggerTimer
            // 
            this._triggerTimer.SynchronizingObject = this.picBanner;
            this._triggerTimer.Elapsed += new System.Timers.ElapsedEventHandler(this._triggerTimer_Elapsed);
            // 
            // _topMenu
            // 
            this._topMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this._topMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.optionsToolStripMenuItem,
            this.actionsMenuItem,
            this.debugToolStripMenuItem,
            this.helpToolStripMenuItem});
            this._topMenu.Location = new System.Drawing.Point(0, 0);
            this._topMenu.Name = "_topMenu";
            this._topMenu.Size = new System.Drawing.Size(1015, 28);
            this._topMenu.TabIndex = 2;
            this._topMenu.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sliceAudioFileToolStripMenuItem,
            this.toolStripMenuItem1,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(46, 24);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // sliceAudioFileToolStripMenuItem
            // 
            this.sliceAudioFileToolStripMenuItem.Name = "sliceAudioFileToolStripMenuItem";
            this.sliceAudioFileToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.sliceAudioFileToolStripMenuItem.Text = "Slice Audio File";
            this.sliceAudioFileToolStripMenuItem.Click += new System.EventHandler(this.OpenAudioSlicer);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(221, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.minimizeMenuItem,
            this.stayOnTopToolStripMenuItem});
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(75, 24);
            this.optionsToolStripMenuItem.Text = "Options";
            // 
            // minimizeMenuItem
            // 
            this.minimizeMenuItem.CheckOnClick = true;
            this.minimizeMenuItem.Name = "minimizeMenuItem";
            this.minimizeMenuItem.Size = new System.Drawing.Size(224, 26);
            this.minimizeMenuItem.Text = "Minimize On Play";
            // 
            // stayOnTopToolStripMenuItem
            // 
            this.stayOnTopToolStripMenuItem.CheckOnClick = true;
            this.stayOnTopToolStripMenuItem.Name = "stayOnTopToolStripMenuItem";
            this.stayOnTopToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.stayOnTopToolStripMenuItem.Text = "Stay on Top";
            this.stayOnTopToolStripMenuItem.Click += new System.EventHandler(this.stayOnTopToolStripMenuItem_Click);
            // 
            // actionsMenuItem
            // 
            this.actionsMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.scanForNewProductsToolStripMenuItem,
            this.totalRescanToolStripMenuItem});
            this.actionsMenuItem.Name = "actionsMenuItem";
            this.actionsMenuItem.Size = new System.Drawing.Size(72, 24);
            this.actionsMenuItem.Text = "Actions";
            // 
            // scanForNewProductsToolStripMenuItem
            // 
            this.scanForNewProductsToolStripMenuItem.Name = "scanForNewProductsToolStripMenuItem";
            this.scanForNewProductsToolStripMenuItem.Size = new System.Drawing.Size(251, 26);
            this.scanForNewProductsToolStripMenuItem.Text = "Scan for New Products...";
            this.scanForNewProductsToolStripMenuItem.Click += new System.EventHandler(this.ScanInventory);
            // 
            // totalRescanToolStripMenuItem
            // 
            this.totalRescanToolStripMenuItem.Name = "totalRescanToolStripMenuItem";
            this.totalRescanToolStripMenuItem.Size = new System.Drawing.Size(251, 26);
            this.totalRescanToolStripMenuItem.Text = "Totally Rescan Inventory";
            // 
            // debugToolStripMenuItem
            // 
            this.debugToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.doSomething1ToolStripMenuItem});
            this.debugToolStripMenuItem.Name = "debugToolStripMenuItem";
            this.debugToolStripMenuItem.Size = new System.Drawing.Size(68, 24);
            this.debugToolStripMenuItem.Text = "Debug";
            // 
            // doSomething1ToolStripMenuItem
            // 
            this.doSomething1ToolStripMenuItem.Name = "doSomething1ToolStripMenuItem";
            this.doSomething1ToolStripMenuItem.Size = new System.Drawing.Size(200, 26);
            this.doSomething1ToolStripMenuItem.Text = "Do Something 1";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.webPageToolStripMenuItem,
            this.aboutTriggerbotToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(55, 24);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // webPageToolStripMenuItem
            // 
            this.webPageToolStripMenuItem.Name = "webPageToolStripMenuItem";
            this.webPageToolStripMenuItem.Size = new System.Drawing.Size(216, 26);
            this.webPageToolStripMenuItem.Text = "Web Page...";
            // 
            // aboutTriggerbotToolStripMenuItem
            // 
            this.aboutTriggerbotToolStripMenuItem.Name = "aboutTriggerbotToolStripMenuItem";
            this.aboutTriggerbotToolStripMenuItem.Size = new System.Drawing.Size(216, 26);
            this.aboutTriggerbotToolStripMenuItem.Text = "About Triggerbot...";
            this.aboutTriggerbotToolStripMenuItem.Click += new System.EventHandler(this.aboutTriggerbotToolStripMenuItem_Click);
            // 
            // _progressTimer
            // 
            this._progressTimer.Tick += new System.EventHandler(this.TriggerMadeProgress);
            // 
            // _collector
            // 
            this._collector.CollectorEvent += new Triggerless.TriggerBot.Collector.CollectorEventHandler(this.OnCollectorEvent);
            // 
            // TriggerBotMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 22F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1015, 699);
            this.Controls.Add(this.splitter);
            this.Controls.Add(this._topMenu);
            this.Font = new System.Drawing.Font("Lucida Sans Unicode", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this._topMenu;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "TriggerBotMainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Triggerless TriggerBot";
            this.Shown += new System.EventHandler(this.ScanInventory);
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
            ((System.ComponentModel.ISupportInitialize)(this._triggerTimer)).EndInit();
            this._topMenu.ResumeLayout(false);
            this._topMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.SplitContainer splitter;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.ProgressBar progScan;
        private System.Windows.Forms.Label lblProduct;
        private System.Windows.Forms.Label lblProgress;
        private System.Windows.Forms.Panel pnlCollector;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pnlSearch;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.FlowLayoutPanel flowDisplay;
        private System.Windows.Forms.Panel pnlRight;
        private System.Windows.Forms.PictureBox picBanner;
        private Collector _collector;
        private System.Windows.Forms.Panel pnlOnDeck;
        private System.Windows.Forms.Label label2;
        private ProductCtrl productOnDeck;
        private System.Windows.Forms.Panel pnlBanner;
        private System.Windows.Forms.Button btnEjectFromDeck;
        private System.Windows.Forms.Button btnLoadToPlaying;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblNowPlaying;
        private System.Windows.Forms.DataGridView gridTriggers;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTrigger;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLengthMS;
        private System.Windows.Forms.ComboBox cboAdditionalTriggers;
        private System.Windows.Forms.Label lblAdditional;
        private System.Timers.Timer _triggerTimer;
        private System.Windows.Forms.ToolTip _toolTip;
        private System.Windows.Forms.Button btnAbort;
        private System.Windows.Forms.Button btnPlay;
        private System.Windows.Forms.MenuStrip _topMenu;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sliceAudioFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stayOnTopToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem debugToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem doSomething1ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem webPageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutTriggerbotToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem minimizeMenuItem;
        private System.Windows.Forms.Panel pnlLag;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TrackBar trackBarLag;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblLag;
        private System.Windows.Forms.Label lblCurrPlayingTrigger;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ProgressBar progTrigger;
        private System.Windows.Forms.Timer _progressTimer;
        private System.Windows.Forms.ToolStripMenuItem actionsMenuItem;
        private System.Windows.Forms.ToolStripMenuItem scanForNewProductsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem totalRescanToolStripMenuItem;
        private System.Windows.Forms.CheckBox chkAutoCue;
    }
}

