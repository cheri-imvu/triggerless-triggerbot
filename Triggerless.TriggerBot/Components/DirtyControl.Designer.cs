namespace Triggerless.TriggerBot.Components
{
    partial class DirtyControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DirtyControl));
            this.picDirty = new System.Windows.Forms.PictureBox();
            this.picClean = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picDirty)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picClean)).BeginInit();
            this.SuspendLayout();
            // 
            // picDirty
            // 
            this.picDirty.Image = ((System.Drawing.Image)(resources.GetObject("picDirty.Image")));
            this.picDirty.InitialImage = null;
            this.picDirty.Location = new System.Drawing.Point(0, 0);
            this.picDirty.Margin = new System.Windows.Forms.Padding(0);
            this.picDirty.Name = "picDirty";
            this.picDirty.Size = new System.Drawing.Size(24, 24);
            this.picDirty.TabIndex = 0;
            this.picDirty.TabStop = false;
            this.picDirty.Visible = false;
            this.picDirty.Click += new System.EventHandler(this.picDirty_Click);
            // 
            // picClean
            // 
            this.picClean.Image = ((System.Drawing.Image)(resources.GetObject("picClean.Image")));
            this.picClean.InitialImage = null;
            this.picClean.Location = new System.Drawing.Point(0, 0);
            this.picClean.Margin = new System.Windows.Forms.Padding(0);
            this.picClean.Name = "picClean";
            this.picClean.Size = new System.Drawing.Size(24, 24);
            this.picClean.TabIndex = 1;
            this.picClean.TabStop = false;
            // 
            // DirtyControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.picClean);
            this.Controls.Add(this.picDirty);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "DirtyControl";
            this.Size = new System.Drawing.Size(152, 131);
            this.Load += new System.EventHandler(this.DirtyControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picDirty)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picClean)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox picDirty;
        private System.Windows.Forms.PictureBox picClean;
    }
}
