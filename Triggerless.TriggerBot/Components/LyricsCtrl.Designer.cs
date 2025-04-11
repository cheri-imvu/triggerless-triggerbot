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
            this.label1 = new System.Windows.Forms.Label();
            this.btnSelectProduct = new System.Windows.Forms.Button();
            this.picProductImage = new System.Windows.Forms.PictureBox();
            this.lblProductName = new System.Windows.Forms.Label();
            this.lblCreatorName = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picProductImage)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Lucida Sans", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(27, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(238, 22);
            this.label1.TabIndex = 0;
            this.label1.Text = "Select Trigger Product:";
            // 
            // btnSelectProduct
            // 
            this.btnSelectProduct.Enabled = false;
            this.btnSelectProduct.Location = new System.Drawing.Point(309, 15);
            this.btnSelectProduct.Name = "btnSelectProduct";
            this.btnSelectProduct.Size = new System.Drawing.Size(64, 39);
            this.btnSelectProduct.TabIndex = 1;
            this.btnSelectProduct.Text = "...";
            this.btnSelectProduct.UseVisualStyleBackColor = true;
            this.btnSelectProduct.Click += new System.EventHandler(this.btnSelectProduct_Click);
            // 
            // picProductImage
            // 
            this.picProductImage.Location = new System.Drawing.Point(478, 5);
            this.picProductImage.Name = "picProductImage";
            this.picProductImage.Size = new System.Drawing.Size(100, 80);
            this.picProductImage.TabIndex = 2;
            this.picProductImage.TabStop = false;
            // 
            // lblProductName
            // 
            this.lblProductName.AutoSize = true;
            this.lblProductName.Location = new System.Drawing.Point(596, 26);
            this.lblProductName.Name = "lblProductName";
            this.lblProductName.Size = new System.Drawing.Size(258, 22);
            this.lblProductName.TabIndex = 3;
            this.lblProductName.Text = "My Awesome Trigger Tune";
            // 
            // lblCreatorName
            // 
            this.lblCreatorName.AutoSize = true;
            this.lblCreatorName.Location = new System.Drawing.Point(596, 55);
            this.lblCreatorName.Name = "lblCreatorName";
            this.lblCreatorName.Size = new System.Drawing.Size(166, 22);
            this.lblCreatorName.TabIndex = 4;
            this.lblCreatorName.Text = "by TriggerQueen";
            // 
            // LyricsCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightCyan;
            this.Controls.Add(this.lblCreatorName);
            this.Controls.Add(this.lblProductName);
            this.Controls.Add(this.picProductImage);
            this.Controls.Add(this.btnSelectProduct);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Lucida Sans", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "LyricsCtrl";
            this.Size = new System.Drawing.Size(920, 661);
            this.Load += new System.EventHandler(this.LyricsCtrl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picProductImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSelectProduct;
        private System.Windows.Forms.PictureBox picProductImage;
        private System.Windows.Forms.Label lblProductName;
        private System.Windows.Forms.Label lblCreatorName;
    }
}
