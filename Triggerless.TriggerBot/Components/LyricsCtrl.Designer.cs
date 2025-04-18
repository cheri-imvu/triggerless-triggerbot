﻿namespace Triggerless.TriggerBot.Components
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
            this.btnSelectProduct = new System.Windows.Forms.Button();
            this.lblProductName = new System.Windows.Forms.Label();
            this.lblCreatorName = new System.Windows.Forms.Label();
            this.pnlTop = new System.Windows.Forms.Panel();
            this.lblTimer = new System.Windows.Forms.Label();
            this.btnPause = new System.Windows.Forms.Button();
            this.btnPlay = new System.Windows.Forms.Button();
            this.picProductImage = new System.Windows.Forms.PictureBox();
            this.pnlWave = new System.Windows.Forms.Panel();
            this.picWave = new System.Windows.Forms.PictureBox();
            this.pnlGrid = new System.Windows.Forms.Panel();
            this.gridLyrics = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLyric = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ctxMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuInsertAbove = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuInsertBelow = new System.Windows.Forms.ToolStripMenuItem();
            this.pnlControls = new System.Windows.Forms.Panel();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnGetLyrics = new System.Windows.Forms.Button();
            this._timer = new System.Windows.Forms.Timer(this.components);
            this.btnTimeIt = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.pnlTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picProductImage)).BeginInit();
            this.pnlWave.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picWave)).BeginInit();
            this.pnlGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridLyrics)).BeginInit();
            this.ctxMenu.SuspendLayout();
            this.pnlControls.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSelectProduct
            // 
            this.btnSelectProduct.Location = new System.Drawing.Point(7, 18);
            this.btnSelectProduct.Name = "btnSelectProduct";
            this.btnSelectProduct.Size = new System.Drawing.Size(175, 39);
            this.btnSelectProduct.TabIndex = 1;
            this.btnSelectProduct.Text = "Select Trigger Tune";
            this.btnSelectProduct.UseVisualStyleBackColor = true;
            this.btnSelectProduct.Click += new System.EventHandler(this.btnSelectProduct_Click);
            // 
            // lblProductName
            // 
            this.lblProductName.AutoSize = true;
            this.lblProductName.Location = new System.Drawing.Point(301, 8);
            this.lblProductName.Name = "lblProductName";
            this.lblProductName.Size = new System.Drawing.Size(202, 17);
            this.lblProductName.TabIndex = 3;
            this.lblProductName.Text = "My Awesome Trigger Tune";
            // 
            // lblCreatorName
            // 
            this.lblCreatorName.AutoSize = true;
            this.lblCreatorName.Location = new System.Drawing.Point(301, 31);
            this.lblCreatorName.Name = "lblCreatorName";
            this.lblCreatorName.Size = new System.Drawing.Size(130, 17);
            this.lblCreatorName.TabIndex = 4;
            this.lblCreatorName.Text = "by TriggerQueen";
            // 
            // pnlTop
            // 
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
            this.btnPause.Location = new System.Drawing.Point(598, 17);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(53, 50);
            this.btnPause.TabIndex = 6;
            this.btnPause.UseVisualStyleBackColor = true;
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
            // 
            // btnPlay
            // 
            this.btnPlay.Image = ((System.Drawing.Image)(resources.GetObject("btnPlay.Image")));
            this.btnPlay.Location = new System.Drawing.Point(539, 17);
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
            this.pnlWave.Controls.Add(this.picWave);
            this.pnlWave.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlWave.Location = new System.Drawing.Point(0, 93);
            this.pnlWave.Name = "pnlWave";
            this.pnlWave.Size = new System.Drawing.Size(1053, 38);
            this.pnlWave.TabIndex = 7;
            // 
            // picWave
            // 
            this.picWave.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picWave.Location = new System.Drawing.Point(0, 0);
            this.picWave.Name = "picWave";
            this.picWave.Size = new System.Drawing.Size(1053, 38);
            this.picWave.TabIndex = 5;
            this.picWave.TabStop = false;
            // 
            // pnlGrid
            // 
            this.pnlGrid.Controls.Add(this.gridLyrics);
            this.pnlGrid.Font = new System.Drawing.Font("Lucida Sans", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlGrid.Location = new System.Drawing.Point(16, 137);
            this.pnlGrid.Name = "pnlGrid";
            this.pnlGrid.Size = new System.Drawing.Size(793, 474);
            this.pnlGrid.TabIndex = 8;
            // 
            // gridLyrics
            // 
            this.gridLyrics.BackgroundColor = System.Drawing.Color.LightSeaGreen;
            this.gridLyrics.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridLyrics.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.colLyric});
            this.gridLyrics.ContextMenuStrip = this.ctxMenu;
            this.gridLyrics.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridLyrics.Location = new System.Drawing.Point(0, 0);
            this.gridLyrics.Margin = new System.Windows.Forms.Padding(12);
            this.gridLyrics.Name = "gridLyrics";
            this.gridLyrics.RowTemplate.Height = 26;
            this.gridLyrics.Size = new System.Drawing.Size(793, 474);
            this.gridLyrics.TabIndex = 0;
            this.gridLyrics.MouseDown += new System.Windows.Forms.MouseEventHandler(this.gridLyrics_MouseDown);
            // 
            // Column1
            // 
            this.Column1.FillWeight = 20F;
            this.Column1.HeaderText = "Time";
            this.Column1.Name = "Column1";
            this.Column1.Width = 130;
            // 
            // colLyric
            // 
            this.colLyric.FillWeight = 80F;
            this.colLyric.HeaderText = "Lyric (Ctrl-B to set times)";
            this.colLyric.MinimumWidth = 25;
            this.colLyric.Name = "colLyric";
            this.colLyric.Width = 600;
            // 
            // ctxMenu
            // 
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
            this.pnlControls.Controls.Add(this.btnTimeIt);
            this.pnlControls.Controls.Add(this.btnSave);
            this.pnlControls.Controls.Add(this.btnGetLyrics);
            this.pnlControls.Location = new System.Drawing.Point(825, 140);
            this.pnlControls.Name = "pnlControls";
            this.pnlControls.Size = new System.Drawing.Size(184, 470);
            this.pnlControls.TabIndex = 9;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(14, 67);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(156, 33);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "Save Lyrics";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnGetLyrics
            // 
            this.btnGetLyrics.Location = new System.Drawing.Point(14, 12);
            this.btnGetLyrics.Name = "btnGetLyrics";
            this.btnGetLyrics.Size = new System.Drawing.Size(156, 33);
            this.btnGetLyrics.TabIndex = 0;
            this.btnGetLyrics.Text = "Get Lyrics";
            this.btnGetLyrics.UseVisualStyleBackColor = true;
            this.btnGetLyrics.Click += new System.EventHandler(this.btnGetLyrics_Click);
            // 
            // _timer
            // 
            this._timer.Tick += new System.EventHandler(this.UpdateTimeLabel);
            // 
            // btnTimeIt
            // 
            this.btnTimeIt.Location = new System.Drawing.Point(14, 122);
            this.btnTimeIt.Name = "btnTimeIt";
            this.btnTimeIt.Size = new System.Drawing.Size(156, 33);
            this.btnTimeIt.TabIndex = 2;
            this.btnTimeIt.Text = "Export TimeIt";
            this.btnTimeIt.UseVisualStyleBackColor = true;
            this.btnTimeIt.Click += new System.EventHandler(this.btnTimeIt_Click);
            // 
            // btnStop
            // 
            this.btnStop.Image = ((System.Drawing.Image)(resources.GetObject("btnStop.Image")));
            this.btnStop.Location = new System.Drawing.Point(657, 17);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(53, 50);
            this.btnStop.TabIndex = 8;
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // LyricsCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.MediumAquamarine;
            this.Controls.Add(this.pnlControls);
            this.Controls.Add(this.pnlGrid);
            this.Controls.Add(this.pnlWave);
            this.Controls.Add(this.pnlTop);
            this.Font = new System.Drawing.Font("Lucida Sans", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "LyricsCtrl";
            this.Size = new System.Drawing.Size(1053, 661);
            this.Load += new System.EventHandler(this.LyricsCtrl_Load);
            this.ControlRemoved += new System.Windows.Forms.ControlEventHandler(this.LyricsCtrl_ControlRemoved);
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picProductImage)).EndInit();
            this.pnlWave.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picWave)).EndInit();
            this.pnlGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridLyrics)).EndInit();
            this.ctxMenu.ResumeLayout(false);
            this.pnlControls.ResumeLayout(false);
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
        private System.Windows.Forms.Button btnGetLyrics;
        private System.Windows.Forms.Timer _timer;
        private System.Windows.Forms.Label lblTimer;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLyric;
        private System.Windows.Forms.ContextMenuStrip ctxMenu;
        private System.Windows.Forms.ToolStripMenuItem mnuInsertAbove;
        private System.Windows.Forms.ToolStripMenuItem mnuDelete;
        private System.Windows.Forms.ToolStripMenuItem mnuInsertBelow;
        private System.Windows.Forms.Button btnTimeIt;
        private System.Windows.Forms.Button btnStop;
    }
}
