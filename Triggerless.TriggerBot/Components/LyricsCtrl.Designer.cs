namespace Triggerless.TriggerBot.Components
{
    partial class LyricsCtrl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LyricsCtrl));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnSelectProduct = new System.Windows.Forms.Button();
            this.lblProductName = new System.Windows.Forms.Label();
            this.lblCreatorName = new System.Windows.Forms.Label();
            this.pnlTop = new System.Windows.Forms.Panel();
            this.btnStop = new System.Windows.Forms.Button();
            this.lblTimer = new System.Windows.Forms.Label();
            this.btnPause = new System.Windows.Forms.Button();
            this.btnPlay = new System.Windows.Forms.Button();
            this.picProductImage = new System.Windows.Forms.PictureBox();
            this.pnlWave = new System.Windows.Forms.Panel();
            this.triangle1 = new Triggerless.TriggerBot.Triangle();
            this.picWave = new System.Windows.Forms.PictureBox();
            this.picWait = new System.Windows.Forms.PictureBox();
            this.pnlGrid = new System.Windows.Forms.Panel();
            this.gridLyrics = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLyric = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ctxMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuInsertAbove = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuInsertBelow = new System.Windows.Forms.ToolStripMenuItem();
            this.pnlControls = new System.Windows.Forms.Panel();
            this.btnExportLyrics = new System.Windows.Forms.Button();
            this.btnImportLyrics = new System.Windows.Forms.Button();
            this.btnDeleteLyrics = new System.Windows.Forms.Button();
            this.lblNudge = new System.Windows.Forms.Label();
            this.ctlNeedsSave = new Triggerless.TriggerBot.Components.DirtyControl();
            this.btnMsMinus = new System.Windows.Forms.Button();
            this.btnMsPlus = new System.Windows.Forms.Button();
            this.lblMS = new System.Windows.Forms.Label();
            this.txtMS = new System.Windows.Forms.TextBox();
            this.btnTimeIt = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnPasteLyrics = new System.Windows.Forms.Button();
            this._timer = new System.Windows.Forms.Timer(this.components);
            this.splitter = new System.Windows.Forms.SplitContainer();
            this.pnlTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picProductImage)).BeginInit();
            this.pnlWave.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picWave)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picWait)).BeginInit();
            this.pnlGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridLyrics)).BeginInit();
            this.ctxMenu.SuspendLayout();
            this.pnlControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitter)).BeginInit();
            this.splitter.Panel1.SuspendLayout();
            this.splitter.Panel2.SuspendLayout();
            this.splitter.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSelectProduct
            // 
            this.btnSelectProduct.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnSelectProduct.Font = new System.Drawing.Font("Liberation Sans", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSelectProduct.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnSelectProduct.Location = new System.Drawing.Point(7, 18);
            this.btnSelectProduct.Name = "btnSelectProduct";
            this.btnSelectProduct.Size = new System.Drawing.Size(175, 39);
            this.btnSelectProduct.TabIndex = 1;
            this.btnSelectProduct.Text = "Select Trigger Tune";
            this.btnSelectProduct.UseVisualStyleBackColor = false;
            this.btnSelectProduct.Click += new System.EventHandler(this.btnSelectProduct_Click);
            // 
            // lblProductName
            // 
            this.lblProductName.AutoSize = true;
            this.lblProductName.Font = new System.Drawing.Font("Liberation Sans", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProductName.Location = new System.Drawing.Point(301, 8);
            this.lblProductName.Name = "lblProductName";
            this.lblProductName.Size = new System.Drawing.Size(174, 17);
            this.lblProductName.TabIndex = 3;
            this.lblProductName.Text = "Non-existent Trigger Tune";
            // 
            // lblCreatorName
            // 
            this.lblCreatorName.AutoSize = true;
            this.lblCreatorName.Font = new System.Drawing.Font("Liberation Sans", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCreatorName.Location = new System.Drawing.Point(301, 31);
            this.lblCreatorName.Name = "lblCreatorName";
            this.lblCreatorName.Size = new System.Drawing.Size(94, 17);
            this.lblCreatorName.TabIndex = 4;
            this.lblCreatorName.Text = "by NoCreator";
            // 
            // pnlTop
            // 
            this.pnlTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.pnlTop.Controls.Add(this.btnStop);
            this.pnlTop.Controls.Add(this.lblTimer);
            this.pnlTop.Controls.Add(this.btnPause);
            this.pnlTop.Controls.Add(this.btnPlay);
            this.pnlTop.Controls.Add(this.btnSelectProduct);
            this.pnlTop.Controls.Add(this.picProductImage);
            this.pnlTop.Controls.Add(this.lblCreatorName);
            this.pnlTop.Controls.Add(this.lblProductName);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(1053, 93);
            this.pnlTop.TabIndex = 6;
            // 
            // btnStop
            // 
            this.btnStop.Image = ((System.Drawing.Image)(resources.GetObject("btnStop.Image")));
            this.btnStop.Location = new System.Drawing.Point(666, 17);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(53, 50);
            this.btnStop.TabIndex = 8;
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // lblTimer
            // 
            this.lblTimer.BackColor = System.Drawing.Color.MidnightBlue;
            this.lblTimer.Font = new System.Drawing.Font("Lucida Console", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTimer.ForeColor = System.Drawing.Color.Yellow;
            this.lblTimer.Location = new System.Drawing.Point(301, 66);
            this.lblTimer.Name = "lblTimer";
            this.lblTimer.Size = new System.Drawing.Size(150, 24);
            this.lblTimer.TabIndex = 7;
            this.lblTimer.Text = "00:00.000";
            this.lblTimer.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnPause
            // 
            this.btnPause.Image = ((System.Drawing.Image)(resources.GetObject("btnPause.Image")));
            this.btnPause.Location = new System.Drawing.Point(607, 17);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(53, 50);
            this.btnPause.TabIndex = 6;
            this.btnPause.UseVisualStyleBackColor = true;
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
            // 
            // btnPlay
            // 
            this.btnPlay.Image = ((System.Drawing.Image)(resources.GetObject("btnPlay.Image")));
            this.btnPlay.Location = new System.Drawing.Point(548, 17);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(53, 50);
            this.btnPlay.TabIndex = 5;
            this.btnPlay.UseVisualStyleBackColor = true;
            this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click);
            // 
            // picProductImage
            // 
            this.picProductImage.Image = ((System.Drawing.Image)(resources.GetObject("picProductImage.Image")));
            this.picProductImage.Location = new System.Drawing.Point(190, 3);
            this.picProductImage.Name = "picProductImage";
            this.picProductImage.Size = new System.Drawing.Size(100, 80);
            this.picProductImage.TabIndex = 2;
            this.picProductImage.TabStop = false;
            // 
            // pnlWave
            // 
            this.pnlWave.Controls.Add(this.triangle1);
            this.pnlWave.Controls.Add(this.picWave);
            this.pnlWave.Controls.Add(this.picWait);
            this.pnlWave.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlWave.Location = new System.Drawing.Point(0, 93);
            this.pnlWave.Name = "pnlWave";
            this.pnlWave.Size = new System.Drawing.Size(1053, 38);
            this.pnlWave.TabIndex = 7;
            // 
            // triangle1
            // 
            this.triangle1.BackColor = System.Drawing.Color.Transparent;
            this.triangle1.BorderColor = System.Drawing.Color.Maroon;
            this.triangle1.BorderThickness = 2;
            this.triangle1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.triangle1.Direction = Triggerless.TriggerBot.Triangle.Orientation.Down;
            this.triangle1.ForeColor = System.Drawing.Color.Yellow;
            this.triangle1.Location = new System.Drawing.Point(-15, 0);
            this.triangle1.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.triangle1.Name = "triangle1";
            this.triangle1.Position = -8;
            this.triangle1.Size = new System.Drawing.Size(15, 20);
            this.triangle1.TabIndex = 10;
            // 
            // picWave
            // 
            this.picWave.BackColor = System.Drawing.SystemColors.Control;
            this.picWave.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picWave.Location = new System.Drawing.Point(0, 0);
            this.picWave.Name = "picWave";
            this.picWave.Size = new System.Drawing.Size(1053, 38);
            this.picWave.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picWave.TabIndex = 5;
            this.picWave.TabStop = false;
            this.picWave.Visible = false;
            this.picWave.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picWave_MouseDown);
            // 
            // picWait
            // 
            this.picWait.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picWait.Image = ((System.Drawing.Image)(resources.GetObject("picWait.Image")));
            this.picWait.Location = new System.Drawing.Point(0, 0);
            this.picWait.Name = "picWait";
            this.picWait.Size = new System.Drawing.Size(1053, 38);
            this.picWait.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picWait.TabIndex = 9;
            this.picWait.TabStop = false;
            // 
            // pnlGrid
            // 
            this.pnlGrid.Controls.Add(this.gridLyrics);
            this.pnlGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlGrid.Font = new System.Drawing.Font("Lucida Sans", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlGrid.Location = new System.Drawing.Point(0, 0);
            this.pnlGrid.Name = "pnlGrid";
            this.pnlGrid.Size = new System.Drawing.Size(850, 530);
            this.pnlGrid.TabIndex = 8;
            // 
            // gridLyrics
            // 
            this.gridLyrics.AllowUserToResizeRows = false;
            this.gridLyrics.BackgroundColor = System.Drawing.Color.Gray;
            this.gridLyrics.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridLyrics.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.colLyric});
            this.gridLyrics.ContextMenuStrip = this.ctxMenu;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Gray;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Lucida Sans", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gridLyrics.DefaultCellStyle = dataGridViewCellStyle1;
            this.gridLyrics.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridLyrics.Location = new System.Drawing.Point(0, 0);
            this.gridLyrics.Margin = new System.Windows.Forms.Padding(12);
            this.gridLyrics.Name = "gridLyrics";
            this.gridLyrics.RowHeadersWidth = 51;
            this.gridLyrics.RowTemplate.Height = 26;
            this.gridLyrics.Size = new System.Drawing.Size(850, 530);
            this.gridLyrics.TabIndex = 0;
            this.gridLyrics.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridLyrics_CellContentClick);
            this.gridLyrics.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridLyrics_CellValueChanged);
            this.gridLyrics.MouseDown += new System.Windows.Forms.MouseEventHandler(this.gridLyrics_MouseDown);
            // 
            // Column1
            // 
            this.Column1.FillWeight = 20F;
            this.Column1.HeaderText = "Time";
            this.Column1.MinimumWidth = 6;
            this.Column1.Name = "Column1";
            this.Column1.Width = 130;
            // 
            // colLyric
            // 
            this.colLyric.FillWeight = 80F;
            this.colLyric.HeaderText = "Lyric (Ctrl-B to set times)";
            this.colLyric.MinimumWidth = 25;
            this.colLyric.Name = "colLyric";
            this.colLyric.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colLyric.Width = 750;
            // 
            // ctxMenu
            // 
            this.ctxMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.ctxMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuInsertAbove,
            this.mnuDelete,
            this.mnuInsertBelow});
            this.ctxMenu.Name = "ctxMenu";
            this.ctxMenu.Size = new System.Drawing.Size(167, 70);
            this.ctxMenu.Opening += new System.ComponentModel.CancelEventHandler(this.ctxMenu_Opening);
            // 
            // mnuInsertAbove
            // 
            this.mnuInsertAbove.Name = "mnuInsertAbove";
            this.mnuInsertAbove.Size = new System.Drawing.Size(166, 22);
            this.mnuInsertAbove.Text = "Insert Row Above";
            this.mnuInsertAbove.Click += new System.EventHandler(this.mnuInsertAbove_Click);
            // 
            // mnuDelete
            // 
            this.mnuDelete.Name = "mnuDelete";
            this.mnuDelete.Size = new System.Drawing.Size(166, 22);
            this.mnuDelete.Text = "Delete Row";
            this.mnuDelete.Click += new System.EventHandler(this.mnuDelete_Click);
            // 
            // mnuInsertBelow
            // 
            this.mnuInsertBelow.Name = "mnuInsertBelow";
            this.mnuInsertBelow.Size = new System.Drawing.Size(166, 22);
            this.mnuInsertBelow.Text = "Insert Row Below";
            this.mnuInsertBelow.Click += new System.EventHandler(this.mnuInsertBelow_Click);
            // 
            // pnlControls
            // 
            this.pnlControls.Controls.Add(this.btnExportLyrics);
            this.pnlControls.Controls.Add(this.btnImportLyrics);
            this.pnlControls.Controls.Add(this.btnDeleteLyrics);
            this.pnlControls.Controls.Add(this.lblNudge);
            this.pnlControls.Controls.Add(this.ctlNeedsSave);
            this.pnlControls.Controls.Add(this.btnMsMinus);
            this.pnlControls.Controls.Add(this.btnMsPlus);
            this.pnlControls.Controls.Add(this.lblMS);
            this.pnlControls.Controls.Add(this.txtMS);
            this.pnlControls.Controls.Add(this.btnTimeIt);
            this.pnlControls.Controls.Add(this.btnSave);
            this.pnlControls.Controls.Add(this.btnPasteLyrics);
            this.pnlControls.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlControls.Location = new System.Drawing.Point(0, 0);
            this.pnlControls.Name = "pnlControls";
            this.pnlControls.Size = new System.Drawing.Size(199, 530);
            this.pnlControls.TabIndex = 9;
            // 
            // btnExportLyrics
            // 
            this.btnExportLyrics.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnExportLyrics.Font = new System.Drawing.Font("Liberation Sans", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExportLyrics.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnExportLyrics.Location = new System.Drawing.Point(14, 107);
            this.btnExportLyrics.Name = "btnExportLyrics";
            this.btnExportLyrics.Size = new System.Drawing.Size(156, 33);
            this.btnExportLyrics.TabIndex = 11;
            this.btnExportLyrics.Text = "Export Lyrics";
            this.btnExportLyrics.UseVisualStyleBackColor = false;
            // 
            // btnImportLyrics
            // 
            this.btnImportLyrics.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnImportLyrics.Font = new System.Drawing.Font("Liberation Sans", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImportLyrics.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnImportLyrics.Location = new System.Drawing.Point(14, 147);
            this.btnImportLyrics.Name = "btnImportLyrics";
            this.btnImportLyrics.Size = new System.Drawing.Size(156, 33);
            this.btnImportLyrics.TabIndex = 10;
            this.btnImportLyrics.Text = "Import Lyrics";
            this.btnImportLyrics.UseVisualStyleBackColor = false;
            // 
            // btnDeleteLyrics
            // 
            this.btnDeleteLyrics.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnDeleteLyrics.Font = new System.Drawing.Font("Liberation Sans", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDeleteLyrics.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnDeleteLyrics.Location = new System.Drawing.Point(14, 291);
            this.btnDeleteLyrics.Name = "btnDeleteLyrics";
            this.btnDeleteLyrics.Size = new System.Drawing.Size(156, 33);
            this.btnDeleteLyrics.TabIndex = 9;
            this.btnDeleteLyrics.Text = "Delete Lyrics";
            this.btnDeleteLyrics.UseVisualStyleBackColor = false;
            this.btnDeleteLyrics.Click += new System.EventHandler(this.btnDeleteLyrics_Click);
            // 
            // lblNudge
            // 
            this.lblNudge.AutoSize = true;
            this.lblNudge.Font = new System.Drawing.Font("Liberation Sans", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNudge.Location = new System.Drawing.Point(12, 232);
            this.lblNudge.Name = "lblNudge";
            this.lblNudge.Size = new System.Drawing.Size(54, 17);
            this.lblNudge.TabIndex = 8;
            this.lblNudge.Text = "Nudge:";
            // 
            // ctlNeedsSave
            // 
            this.ctlNeedsSave.BackColor = System.Drawing.Color.Transparent;
            this.ctlNeedsSave.Dirty = false;
            this.ctlNeedsSave.Location = new System.Drawing.Point(178, 71);
            this.ctlNeedsSave.Margin = new System.Windows.Forms.Padding(0);
            this.ctlNeedsSave.Name = "ctlNeedsSave";
            this.ctlNeedsSave.Size = new System.Drawing.Size(24, 24);
            this.ctlNeedsSave.TabIndex = 7;
            // 
            // btnMsMinus
            // 
            this.btnMsMinus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnMsMinus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnMsMinus.Location = new System.Drawing.Point(146, 253);
            this.btnMsMinus.Name = "btnMsMinus";
            this.btnMsMinus.Size = new System.Drawing.Size(29, 26);
            this.btnMsMinus.TabIndex = 6;
            this.btnMsMinus.Text = "-";
            this.btnMsMinus.UseVisualStyleBackColor = false;
            this.btnMsMinus.Click += new System.EventHandler(this.btnMsMinus_Click);
            // 
            // btnMsPlus
            // 
            this.btnMsPlus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnMsPlus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnMsPlus.Location = new System.Drawing.Point(113, 253);
            this.btnMsPlus.Name = "btnMsPlus";
            this.btnMsPlus.Size = new System.Drawing.Size(29, 26);
            this.btnMsPlus.TabIndex = 5;
            this.btnMsPlus.Text = "+";
            this.btnMsPlus.UseVisualStyleBackColor = false;
            this.btnMsPlus.Click += new System.EventHandler(this.btnMsPlus_Click);
            // 
            // lblMS
            // 
            this.lblMS.AutoSize = true;
            this.lblMS.Font = new System.Drawing.Font("Liberation Sans", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMS.Location = new System.Drawing.Point(72, 258);
            this.lblMS.Name = "lblMS";
            this.lblMS.Size = new System.Drawing.Size(29, 17);
            this.lblMS.TabIndex = 4;
            this.lblMS.Text = "ms";
            // 
            // txtMS
            // 
            this.txtMS.Font = new System.Drawing.Font("Liberation Sans", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMS.Location = new System.Drawing.Point(14, 255);
            this.txtMS.Name = "txtMS";
            this.txtMS.Size = new System.Drawing.Size(52, 25);
            this.txtMS.TabIndex = 3;
            this.txtMS.Text = "100";
            this.txtMS.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // btnTimeIt
            // 
            this.btnTimeIt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnTimeIt.Font = new System.Drawing.Font("Liberation Sans", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTimeIt.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnTimeIt.Location = new System.Drawing.Point(14, 331);
            this.btnTimeIt.Name = "btnTimeIt";
            this.btnTimeIt.Size = new System.Drawing.Size(156, 33);
            this.btnTimeIt.TabIndex = 2;
            this.btnTimeIt.Text = "Export TimeIt";
            this.btnTimeIt.UseVisualStyleBackColor = false;
            this.btnTimeIt.Click += new System.EventHandler(this.btnTimeIt_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnSave.Font = new System.Drawing.Font("Liberation Sans", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnSave.Location = new System.Drawing.Point(14, 67);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(156, 33);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "Save Lyrics";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnPasteLyrics
            // 
            this.btnPasteLyrics.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnPasteLyrics.Font = new System.Drawing.Font("Liberation Sans", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPasteLyrics.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnPasteLyrics.Location = new System.Drawing.Point(14, 12);
            this.btnPasteLyrics.Name = "btnPasteLyrics";
            this.btnPasteLyrics.Size = new System.Drawing.Size(156, 33);
            this.btnPasteLyrics.TabIndex = 0;
            this.btnPasteLyrics.Text = "Paste Lyrics";
            this.btnPasteLyrics.UseVisualStyleBackColor = false;
            this.btnPasteLyrics.Click += new System.EventHandler(this.btnGetLyrics_Click);
            // 
            // _timer
            // 
            this._timer.Tick += new System.EventHandler(this._timer_Tick);
            // 
            // splitter
            // 
            this.splitter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitter.Location = new System.Drawing.Point(0, 131);
            this.splitter.Name = "splitter";
            // 
            // splitter.Panel1
            // 
            this.splitter.Panel1.Controls.Add(this.pnlGrid);
            this.splitter.Panel1MinSize = 400;
            // 
            // splitter.Panel2
            // 
            this.splitter.Panel2.Controls.Add(this.pnlControls);
            this.splitter.Size = new System.Drawing.Size(1053, 530);
            this.splitter.SplitterDistance = 850;
            this.splitter.TabIndex = 10;
            // 
            // LyricsCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Controls.Add(this.splitter);
            this.Controls.Add(this.pnlWave);
            this.Controls.Add(this.pnlTop);
            this.Font = new System.Drawing.Font("Lucida Sans", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "LyricsCtrl";
            this.Size = new System.Drawing.Size(1053, 661);
            this.Load += new System.EventHandler(this.LyricsCtrl_Load);
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picProductImage)).EndInit();
            this.pnlWave.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picWave)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picWait)).EndInit();
            this.pnlGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridLyrics)).EndInit();
            this.ctxMenu.ResumeLayout(false);
            this.pnlControls.ResumeLayout(false);
            this.pnlControls.PerformLayout();
            this.splitter.Panel1.ResumeLayout(false);
            this.splitter.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitter)).EndInit();
            this.splitter.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnSelectProduct;
        private System.Windows.Forms.PictureBox picProductImage;
        private System.Windows.Forms.Label lblProductName;
        private System.Windows.Forms.Label lblCreatorName;
        private System.Windows.Forms.PictureBox picWave;
        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Panel pnlWave;
        private System.Windows.Forms.Button btnPlay;
        private System.Windows.Forms.Button btnPause;
        private System.Windows.Forms.Panel pnlGrid;
        private System.Windows.Forms.DataGridView gridLyrics;
        private System.Windows.Forms.Panel pnlControls;
        private System.Windows.Forms.Button btnPasteLyrics;
        private System.Windows.Forms.Timer _timer;
        private System.Windows.Forms.Label lblTimer;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.ContextMenuStrip ctxMenu;
        private System.Windows.Forms.ToolStripMenuItem mnuInsertAbove;
        private System.Windows.Forms.ToolStripMenuItem mnuDelete;
        private System.Windows.Forms.ToolStripMenuItem mnuInsertBelow;
        private System.Windows.Forms.Button btnTimeIt;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnMsMinus;
        private System.Windows.Forms.Button btnMsPlus;
        private System.Windows.Forms.Label lblMS;
        private System.Windows.Forms.TextBox txtMS;
        private System.Windows.Forms.SplitContainer splitter;
        private DirtyControl ctlNeedsSave;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLyric;
        private System.Windows.Forms.Label lblNudge;
        private System.Windows.Forms.Button btnDeleteLyrics;
        private Triangle triangle1;
        private System.Windows.Forms.PictureBox picWait;
        private System.Windows.Forms.Button btnExportLyrics;
        private System.Windows.Forms.Button btnImportLyrics;
    }
}
