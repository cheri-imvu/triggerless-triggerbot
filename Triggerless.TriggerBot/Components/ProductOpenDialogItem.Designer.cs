namespace Triggerless.TriggerBot
{
    partial class ProductOpenDialogItem
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
            this._productImage = new System.Windows.Forms.PictureBox();
            this._productName = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this._productImage)).BeginInit();
            this.SuspendLayout();
            // 
            // _productImage
            // 
            this._productImage.Location = new System.Drawing.Point(3, 3);
            this._productImage.Name = "_productImage";
            this._productImage.Size = new System.Drawing.Size(50, 40);
            this._productImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this._productImage.TabIndex = 0;
            this._productImage.TabStop = false;
            this._productImage.DoubleClick += new System.EventHandler(this.HandleDoubleClick);
            // 
            // _productName
            // 
            this._productName.AutoSize = true;
            this._productName.Font = new System.Drawing.Font("Lucida Sans", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._productName.Location = new System.Drawing.Point(75, 13);
            this._productName.Name = "_productName";
            this._productName.Size = new System.Drawing.Size(397, 17);
            this._productName.TabIndex = 1;
            this._productName.Text = "AbccdefghijAbcdefghijAbcd by AbcdefghijAbcdefghij";
            this._productName.DoubleClick += new System.EventHandler(this.HandleDoubleClick);
            // 
            // ProductOpenDialogItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this._productName);
            this.Controls.Add(this._productImage);
            this.Font = new System.Drawing.Font("Lucida Sans", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ProductOpenDialogItem";
            this.Size = new System.Drawing.Size(666, 48);
            this.DoubleClick += new System.EventHandler(this.HandleDoubleClick);
            ((System.ComponentModel.ISupportInitialize)(this._productImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox _productImage;
        private System.Windows.Forms.Label _productName;
    }
}
