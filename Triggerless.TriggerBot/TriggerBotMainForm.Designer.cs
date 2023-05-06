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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.splitter = new System.Windows.Forms.SplitContainer();
            this.flowDisplay = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlSearch = new System.Windows.Forms.Panel();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.pnlRight = new System.Windows.Forms.Panel();
            this.gridTriggers = new System.Windows.Forms.DataGridView();
            this.colTrigger = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLengthMS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblNowPlaying = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.pnlBanner = new System.Windows.Forms.Panel();
            this.picBanner = new System.Windows.Forms.PictureBox();
            this.pnlOnDeck = new System.Windows.Forms.Panel();
            this.btnEjectFromDeck = new System.Windows.Forms.Button();
            this.btnLoadToPlaying = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.productOnDeck = new Triggerless.TriggerBot.ProductCtrl();
            this.pnlCollector = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.progScan = new System.Windows.Forms.ProgressBar();
            this.lblProgress = new System.Windows.Forms.Label();
            this.lblProduct = new System.Windows.Forms.Label();
            this._collector = new Triggerless.TriggerBot.Collector();
            this.lblAdditional = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.timer1 = new System.Timers.Timer();
            ((System.ComponentModel.ISupportInitialize)(this.splitter)).BeginInit();
            this.splitter.Panel1.SuspendLayout();
            this.splitter.Panel2.SuspendLayout();
            this.splitter.SuspendLayout();
            this.pnlSearch.SuspendLayout();
            this.pnlRight.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridTriggers)).BeginInit();
            this.pnlBanner.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBanner)).BeginInit();
            this.pnlOnDeck.SuspendLayout();
            this.pnlCollector.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitter
            // 
            this.splitter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitter.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitter.Location = new System.Drawing.Point(0, 0);
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
            this.splitter.Size = new System.Drawing.Size(1015, 699);
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
            this.flowDisplay.Size = new System.Drawing.Size(390, 651);
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
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.DoSearch);
            // 
            // pnlRight
            // 
            this.pnlRight.Controls.Add(this.comboBox1);
            this.pnlRight.Controls.Add(this.lblAdditional);
            this.pnlRight.Controls.Add(this.gridTriggers);
            this.pnlRight.Controls.Add(this.lblNowPlaying);
            this.pnlRight.Controls.Add(this.label3);
            this.pnlRight.Controls.Add(this.pnlBanner);
            this.pnlRight.Controls.Add(this.pnlOnDeck);
            this.pnlRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlRight.Location = new System.Drawing.Point(0, 0);
            this.pnlRight.Name = "pnlRight";
            this.pnlRight.Size = new System.Drawing.Size(620, 699);
            this.pnlRight.TabIndex = 9;
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
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Lucida Sans Unicode", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridTriggers.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.gridTriggers.ColumnHeadersHeight = 25;
            this.gridTriggers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.gridTriggers.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colTrigger,
            this.colLengthMS});
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Lucida Sans Unicode", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gridTriggers.DefaultCellStyle = dataGridViewCellStyle5;
            this.gridTriggers.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.gridTriggers.EnableHeadersVisualStyles = false;
            this.gridTriggers.Location = new System.Drawing.Point(4, 120);
            this.gridTriggers.Name = "gridTriggers";
            this.gridTriggers.ReadOnly = true;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.AppWorkspace;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Lucida Sans Unicode", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gridTriggers.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.gridTriggers.RowHeadersWidth = 5;
            this.gridTriggers.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.gridTriggers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridTriggers.Size = new System.Drawing.Size(277, 437);
            this.gridTriggers.TabIndex = 7;
            // 
            // colTrigger
            // 
            this.colTrigger.HeaderText = "Trigger";
            this.colTrigger.Name = "colTrigger";
            this.colTrigger.ReadOnly = true;
            this.colTrigger.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // colLengthMS
            // 
            this.colLengthMS.HeaderText = "OGG Length (sec)";
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
            this.pnlOnDeck.Controls.Add(this.btnEjectFromDeck);
            this.pnlOnDeck.Controls.Add(this.btnLoadToPlaying);
            this.pnlOnDeck.Controls.Add(this.label2);
            this.pnlOnDeck.Controls.Add(this.productOnDeck);
            this.pnlOnDeck.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlOnDeck.Location = new System.Drawing.Point(0, 563);
            this.pnlOnDeck.Name = "pnlOnDeck";
            this.pnlOnDeck.Size = new System.Drawing.Size(620, 136);
            this.pnlOnDeck.TabIndex = 3;
            // 
            // btnEjectFromDeck
            // 
            this.btnEjectFromDeck.Image = ((System.Drawing.Image)(resources.GetObject("btnEjectFromDeck.Image")));
            this.btnEjectFromDeck.Location = new System.Drawing.Point(377, 94);
            this.btnEjectFromDeck.Name = "btnEjectFromDeck";
            this.btnEjectFromDeck.Size = new System.Drawing.Size(34, 34);
            this.btnEjectFromDeck.TabIndex = 4;
            this.btnEjectFromDeck.UseVisualStyleBackColor = true;
            this.btnEjectFromDeck.Click += new System.EventHandler(this.RemoveFromDeck);
            // 
            // btnLoadToPlaying
            // 
            this.btnLoadToPlaying.Font = new System.Drawing.Font("Lucida Sans Unicode", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLoadToPlaying.Image = ((System.Drawing.Image)(resources.GetObject("btnLoadToPlaying.Image")));
            this.btnLoadToPlaying.Location = new System.Drawing.Point(377, 37);
            this.btnLoadToPlaying.Name = "btnLoadToPlaying";
            this.btnLoadToPlaying.Size = new System.Drawing.Size(34, 34);
            this.btnLoadToPlaying.TabIndex = 3;
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
            // _collector
            // 
            this._collector.CollectorEvent += new Triggerless.TriggerBot.Collector.CollectorEventHandler(this.OnCollectorEvent);
            // 
            // lblAdditional
            // 
            this.lblAdditional.AutoSize = true;
            this.lblAdditional.Location = new System.Drawing.Point(286, 97);
            this.lblAdditional.Name = "lblAdditional";
            this.lblAdditional.Size = new System.Drawing.Size(150, 18);
            this.lblAdditional.TabIndex = 8;
            this.lblAdditional.Text = "Additional Triggers";
            // 
            // comboBox1
            // 
            this.comboBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(287, 120);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(327, 26);
            this.comboBox1.TabIndex = 9;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1015, 699);
            this.Controls.Add(this.splitter);
            this.Font = new System.Drawing.Font("Lucida Sans Unicode", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Form1";
            this.Text = "Triggerless TriggerBot";
            this.Shown += new System.EventHandler(this.ScanInventory);
            this.Move += new System.EventHandler(this.SaveSettings);
            this.Resize += new System.EventHandler(this.SaveSettings);
            this.splitter.Panel1.ResumeLayout(false);
            this.splitter.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitter)).EndInit();
            this.splitter.ResumeLayout(false);
            this.pnlSearch.ResumeLayout(false);
            this.pnlSearch.PerformLayout();
            this.pnlRight.ResumeLayout(false);
            this.pnlRight.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridTriggers)).EndInit();
            this.pnlBanner.ResumeLayout(false);
            this.pnlBanner.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBanner)).EndInit();
            this.pnlOnDeck.ResumeLayout(false);
            this.pnlOnDeck.PerformLayout();
            this.pnlCollector.ResumeLayout(false);
            this.pnlCollector.PerformLayout();
            this.ResumeLayout(false);

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
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label lblAdditional;
        private System.Timers.Timer timer1;
    }
}

