namespace Triggerless.TriggerBot
{
    partial class WaveformEditorControl
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Panel viewportPanel;
        private System.Windows.Forms.HScrollBar hScrollBar;

        private System.Windows.Forms.Button btnZoomIn;
        private System.Windows.Forms.Button btnZoomOut;
        private System.Windows.Forms.Button btnNext20;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.viewportPanel = new System.Windows.Forms.Panel();
            this.hScrollBar = new System.Windows.Forms.HScrollBar();
            this.btnZoomIn = new System.Windows.Forms.Button();
            this.btnZoomOut = new System.Windows.Forms.Button();
            this.btnNext20 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // viewportPanel
            // 
            this.viewportPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.viewportPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.viewportPanel.Location = new System.Drawing.Point(0, 0);
            this.viewportPanel.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.viewportPanel.Name = "viewportPanel";
            this.viewportPanel.Size = new System.Drawing.Size(772, 260);
            this.viewportPanel.TabIndex = 0;
            // 
            // hScrollBar
            // 
            this.hScrollBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.hScrollBar.Location = new System.Drawing.Point(0, 260);
            this.hScrollBar.Name = "hScrollBar";
            this.hScrollBar.Size = new System.Drawing.Size(772, 18);
            this.hScrollBar.TabIndex = 1;
            // 
            // btnZoomIn
            // 
            this.btnZoomIn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnZoomIn.Location = new System.Drawing.Point(0, 282);
            this.btnZoomIn.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnZoomIn.Name = "btnZoomIn";
            this.btnZoomIn.Size = new System.Drawing.Size(64, 24);
            this.btnZoomIn.TabIndex = 2;
            this.btnZoomIn.Text = "Zoom In";
            this.btnZoomIn.UseVisualStyleBackColor = true;
            // 
            // btnZoomOut
            // 
            this.btnZoomOut.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnZoomOut.Location = new System.Drawing.Point(68, 282);
            this.btnZoomOut.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnZoomOut.Name = "btnZoomOut";
            this.btnZoomOut.Size = new System.Drawing.Size(64, 24);
            this.btnZoomOut.TabIndex = 3;
            this.btnZoomOut.Text = "Zoom Out";
            this.btnZoomOut.UseVisualStyleBackColor = true;
            // 
            // btnNext20
            // 
            this.btnNext20.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnNext20.Location = new System.Drawing.Point(146, 282);
            this.btnNext20.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnNext20.Name = "btnNext20";
            this.btnNext20.Size = new System.Drawing.Size(64, 24);
            this.btnNext20.TabIndex = 3;
            this.btnNext20.Text = "20 ▶";
            this.btnNext20.UseVisualStyleBackColor = true;
            // 
            // WaveformEditorControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(29)))), ((int)(((byte)(51)))));
            this.Controls.Add(this.btnNext20);
            this.Controls.Add(this.viewportPanel);
            this.Controls.Add(this.hScrollBar);
            this.Controls.Add(this.btnZoomIn);
            this.Controls.Add(this.btnZoomOut);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "WaveformEditorControl";
            this.Size = new System.Drawing.Size(772, 312);
            this.ResumeLayout(false);

        }
    }
}
