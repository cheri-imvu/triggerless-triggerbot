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
            this._productName = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // _productName
            // 
            this._productName.AutoSize = true;
            this._productName.Font = new System.Drawing.Font("Lucida Sans", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._productName.Location = new System.Drawing.Point(4, 1);
            this._productName.Name = "_productName";
            this._productName.Size = new System.Drawing.Size(397, 17);
            this._productName.TabIndex = 1;
            this._productName.Text = "AbccdefghijAbcdefghijAbcd by AbcdefghijAbcdefghij";
            this._productName.Click += new System.EventHandler(this.HandleClick);
            this._productName.DoubleClick += new System.EventHandler(this.HandleDoubleClick);
            // 
            // ProductOpenDialogItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this._productName);
            this.Font = new System.Drawing.Font("Lucida Sans", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(1);
            this.Name = "ProductOpenDialogItem";
            this.Size = new System.Drawing.Size(666, 26);
            this.Click += new System.EventHandler(this.HandleClick);
            this.DoubleClick += new System.EventHandler(this.HandleDoubleClick);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label _productName;
    }
}
