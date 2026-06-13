namespace Triggerless.TriggerBot
{
    partial class ICutControl
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Panel viewportPanel;
        private System.Windows.Forms.HScrollBar hScrollBar;

        private System.Windows.Forms.Button btnZoomIn;
        private System.Windows.Forms.Button btnZoomOut;
        private System.Windows.Forms.Button btnNext20;

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.viewportPanel = new System.Windows.Forms.Panel();
            this.hScrollBar = new System.Windows.Forms.HScrollBar();
            this.btnZoomIn = new System.Windows.Forms.Button();
            this.btnZoomOut = new System.Windows.Forms.Button();
            this.btnNext20 = new System.Windows.Forms.Button();
            this.btnPlay = new System.Windows.Forms.Button();
            this.btnPause = new System.Windows.Forms.Button();
            this.btnPlayheadTime = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // viewportPanel
            // 
            this.viewportPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.viewportPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.viewportPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.viewportPanel.Location = new System.Drawing.Point(0, 0);
            this.viewportPanel.Margin = new System.Windows.Forms.Padding(2);
            this.viewportPanel.Name = "viewportPanel";
            this.viewportPanel.Size = new System.Drawing.Size(900, 300);
            this.viewportPanel.TabIndex = 0;
            this.viewportPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.ViewportPanel_Paint);
            this.viewportPanel.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.ViewportPanel_MouseDoubleClick);
            this.viewportPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ViewportPanel_MouseDown);
            this.viewportPanel.MouseEnter += new System.EventHandler(this.ViewportPanel_MouseEnter);
            this.viewportPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ViewportPanel_MouseMove);
            this.viewportPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ViewportPanel_MouseUp);
            this.viewportPanel.Resize += new System.EventHandler(this.ViewportPanel_Resize);
            // 
            // hScrollBar
            // 
            this.hScrollBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.hScrollBar.Location = new System.Drawing.Point(0, 300);
            this.hScrollBar.Name = "hScrollBar";
            this.hScrollBar.Size = new System.Drawing.Size(901, 18);
            this.hScrollBar.TabIndex = 1;
            this.toolTip1.SetToolTip(this.hScrollBar, "Pan Left/Right");
            // 
            // btnZoomIn
            // 
            this.btnZoomIn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnZoomIn.Location = new System.Drawing.Point(13, 325);
            this.btnZoomIn.Margin = new System.Windows.Forms.Padding(2);
            this.btnZoomIn.Name = "btnZoomIn";
            this.btnZoomIn.Size = new System.Drawing.Size(75, 28);
            this.btnZoomIn.TabIndex = 2;
            this.btnZoomIn.Text = "Zoom In";
            this.toolTip1.SetToolTip(this.btnZoomIn, "Zoom In 2x");
            this.btnZoomIn.UseVisualStyleBackColor = true;
            // 
            // btnZoomOut
            // 
            this.btnZoomOut.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnZoomOut.Location = new System.Drawing.Point(92, 325);
            this.btnZoomOut.Margin = new System.Windows.Forms.Padding(2);
            this.btnZoomOut.Name = "btnZoomOut";
            this.btnZoomOut.Size = new System.Drawing.Size(75, 28);
            this.btnZoomOut.TabIndex = 3;
            this.btnZoomOut.Text = "Zoom Out";
            this.toolTip1.SetToolTip(this.btnZoomOut, "Zoom Out 2x");
            this.btnZoomOut.UseVisualStyleBackColor = true;
            // 
            // btnNext20
            // 
            this.btnNext20.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnNext20.Location = new System.Drawing.Point(189, 325);
            this.btnNext20.Margin = new System.Windows.Forms.Padding(2);
            this.btnNext20.Name = "btnNext20";
            this.btnNext20.Size = new System.Drawing.Size(75, 28);
            this.btnNext20.TabIndex = 3;
            this.btnNext20.Text = "20 ▶";
            this.toolTip1.SetToolTip(this.btnNext20, "Last marker + 20 sec of audio");
            this.btnNext20.UseVisualStyleBackColor = true;
            // 
            // btnPlay
            // 
            this.btnPlay.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnPlay.Location = new System.Drawing.Point(463, 325);
            this.btnPlay.Margin = new System.Windows.Forms.Padding(2);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(75, 28);
            this.btnPlay.TabIndex = 4;
            this.btnPlay.Text = "▶";
            this.toolTip1.SetToolTip(this.btnPlay, "Play Audio");
            this.btnPlay.UseVisualStyleBackColor = true;
            this.btnPlay.Click += new System.EventHandler(this.BtnPlay_Click);
            // 
            // btnPause
            // 
            this.btnPause.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnPause.Location = new System.Drawing.Point(543, 325);
            this.btnPause.Margin = new System.Windows.Forms.Padding(2);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(75, 28);
            this.btnPause.TabIndex = 5;
            this.btnPause.Text = "Pause";
            this.toolTip1.SetToolTip(this.btnPause, "Pause/Resume Audio");
            this.btnPause.UseVisualStyleBackColor = true;
            this.btnPause.Click += new System.EventHandler(this.BtnPause_Click);
            // 
            // btnPlayheadTime
            // 
            this.btnPlayheadTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnPlayheadTime.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnPlayheadTime.Font = new System.Drawing.Font("Liberation Sans", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPlayheadTime.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnPlayheadTime.Location = new System.Drawing.Point(634, 323);
            this.btnPlayheadTime.Margin = new System.Windows.Forms.Padding(2);
            this.btnPlayheadTime.Name = "btnPlayheadTime";
            this.btnPlayheadTime.Size = new System.Drawing.Size(118, 28);
            this.btnPlayheadTime.TabIndex = 7;
            this.btnPlayheadTime.Text = "00:00.00";
            this.toolTip1.SetToolTip(this.btnPlayheadTime, "Playhead position");
            this.btnPlayheadTime.UseVisualStyleBackColor = true;
            // 
            // ICutControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(29)))), ((int)(((byte)(51)))));
            this.Controls.Add(this.btnPlayheadTime);
            this.Controls.Add(this.btnPause);
            this.Controls.Add(this.btnPlay);
            this.Controls.Add(this.btnNext20);
            this.Controls.Add(this.viewportPanel);
            this.Controls.Add(this.hScrollBar);
            this.Controls.Add(this.btnZoomIn);
            this.Controls.Add(this.btnZoomOut);
            this.Font = new System.Drawing.Font("Liberation Sans", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "ICutControl";
            this.Size = new System.Drawing.Size(901, 360);
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Button btnPlay;
        private System.Windows.Forms.Button btnPause;
        private System.Windows.Forms.Button btnPlayheadTime;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}
