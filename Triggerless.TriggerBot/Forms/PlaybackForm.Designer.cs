namespace Triggerless.TriggerBot
{
    partial class PlaybackForm
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
            this.pnlViewPort = new System.Windows.Forms.Panel();
            this.pnlWave = new System.Windows.Forms.Panel();
            this.picWaveform = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.triangle1 = new Triggerless.TriggerBot.Triangle();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.pnlViewPort.SuspendLayout();
            this.pnlWave.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picWaveform)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlViewPort
            // 
            this.pnlViewPort.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlViewPort.AutoScroll = true;
            this.pnlViewPort.BackColor = System.Drawing.Color.Silver;
            this.pnlViewPort.Controls.Add(this.pnlWave);
            this.pnlViewPort.Location = new System.Drawing.Point(13, 70);
            this.pnlViewPort.Margin = new System.Windows.Forms.Padding(0);
            this.pnlViewPort.Name = "pnlViewPort";
            this.pnlViewPort.Size = new System.Drawing.Size(1274, 185);
            this.pnlViewPort.TabIndex = 0;
            // 
            // pnlWave
            // 
            this.pnlWave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.pnlWave.Controls.Add(this.picWaveform);
            this.pnlWave.Location = new System.Drawing.Point(0, 0);
            this.pnlWave.Margin = new System.Windows.Forms.Padding(0);
            this.pnlWave.Name = "pnlWave";
            this.pnlWave.Size = new System.Drawing.Size(655, 104);
            this.pnlWave.TabIndex = 0;
            // 
            // picWaveform
            // 
            this.picWaveform.Location = new System.Drawing.Point(41, 4);
            this.picWaveform.Margin = new System.Windows.Forms.Padding(4);
            this.picWaveform.Name = "picWaveform";
            this.picWaveform.Size = new System.Drawing.Size(144, 90);
            this.picWaveform.TabIndex = 2;
            this.picWaveform.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(156, 291);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 16);
            this.label1.TabIndex = 3;
            this.label1.Text = "Zoom";
            // 
            // triangle1
            // 
            this.triangle1.BackColor = System.Drawing.Color.Transparent;
            this.triangle1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.triangle1.Direction = Triggerless.TriggerBot.Triangle.Orientation.Up;
            this.triangle1.ForeColor = System.Drawing.Color.LawnGreen;
            this.triangle1.Location = new System.Drawing.Point(981, 277);
            this.triangle1.Margin = new System.Windows.Forms.Padding(5);
            this.triangle1.Name = "triangle1";
            this.triangle1.Size = new System.Drawing.Size(32, 30);
            this.triangle1.TabIndex = 1;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "1 sec",
            "2 sec",
            "5 sec",
            "10 sec",
            "20 sec",
            "30 sec",
            "60 sec"});
            this.comboBox1.Location = new System.Drawing.Point(222, 288);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(102, 24);
            this.comboBox1.TabIndex = 5;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // PlaybackForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1296, 443);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.triangle1);
            this.Controls.Add(this.pnlViewPort);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "PlaybackForm";
            this.Text = "PlaybackForm";
            this.Load += new System.EventHandler(this.PlaybackForm_Load);
            this.pnlViewPort.ResumeLayout(false);
            this.pnlWave.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picWaveform)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlViewPort;
        private System.Windows.Forms.Panel pnlWave;
        private Triangle triangle1;
        private System.Windows.Forms.PictureBox picWaveform;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox1;
    }
}