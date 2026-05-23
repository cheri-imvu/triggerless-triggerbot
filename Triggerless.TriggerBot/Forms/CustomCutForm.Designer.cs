namespace Triggerless.TriggerBot.Forms
{
    partial class CustomCutForm
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
            this.waveformEditorControl1 = new Triggerless.TriggerBot.WaveformEditorControl();
            this.SuspendLayout();
            // 
            // waveformEditorControl1
            // 
            this.waveformEditorControl1.Location = new System.Drawing.Point(12, 12);
            this.waveformEditorControl1.Name = "waveformEditorControl1";
            this.waveformEditorControl1.Size = new System.Drawing.Size(907, 169);
            this.waveformEditorControl1.TabIndex = 0;
            // 
            // CustomCutForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1034, 554);
            this.Controls.Add(this.waveformEditorControl1);
            this.Name = "CustomCutForm";
            this.Text = "CustomCutForm";
            this.Load += new System.EventHandler(this.CustomCutForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private WaveformEditorControl waveformEditorControl1;
    }
}