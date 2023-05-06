namespace Triggerless.TriggerBot
{
    partial class ProductCtrl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProductCtrl));
            this.picProductImage = new System.Windows.Forms.PictureBox();
            this.lblName = new System.Windows.Forms.Label();
            this.lblCreator = new System.Windows.Forms.Label();
            this.lblTriggers = new System.Windows.Forms.Label();
            this.linkOnDeck = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.picProductImage)).BeginInit();
            this.SuspendLayout();
            // 
            // picProductImage
            // 
            this.picProductImage.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("picProductImage.BackgroundImage")));
            this.picProductImage.InitialImage = ((System.Drawing.Image)(resources.GetObject("picProductImage.InitialImage")));
            this.picProductImage.Location = new System.Drawing.Point(5, 4);
            this.picProductImage.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.picProductImage.Name = "picProductImage";
            this.picProductImage.Size = new System.Drawing.Size(100, 80);
            this.picProductImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picProductImage.TabIndex = 0;
            this.picProductImage.TabStop = false;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Font = new System.Drawing.Font("Lucida Sans Unicode", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblName.Location = new System.Drawing.Point(113, 7);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(240, 18);
            this.lblName.TabIndex = 1;
            this.lblName.Text = "012345678 012345678 0123";
            // 
            // lblCreator
            // 
            this.lblCreator.AutoSize = true;
            this.lblCreator.Location = new System.Drawing.Point(113, 25);
            this.lblCreator.Name = "lblCreator";
            this.lblCreator.Size = new System.Drawing.Size(209, 18);
            this.lblCreator.TabIndex = 2;
            this.lblCreator.Text = "by CrazyMusicTriggerFiend";
            // 
            // lblTriggers
            // 
            this.lblTriggers.AutoSize = true;
            this.lblTriggers.Location = new System.Drawing.Point(113, 44);
            this.lblTriggers.Name = "lblTriggers";
            this.lblTriggers.Size = new System.Drawing.Size(180, 18);
            this.lblTriggers.TabIndex = 3;
            this.lblTriggers.Text = "Triggers: xyz1 - xyz24";
            // 
            // linkOnDeck
            // 
            this.linkOnDeck.AutoSize = true;
            this.linkOnDeck.Location = new System.Drawing.Point(258, 66);
            this.linkOnDeck.Name = "linkOnDeck";
            this.linkOnDeck.Size = new System.Drawing.Size(105, 18);
            this.linkOnDeck.TabIndex = 4;
            this.linkOnDeck.TabStop = true;
            this.linkOnDeck.Text = "ON DECK >>";
            this.linkOnDeck.VisitedLinkColor = System.Drawing.Color.Blue;
            this.linkOnDeck.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkOnDeck_LinkClicked);
            // 
            // ProductCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(248)))));
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.linkOnDeck);
            this.Controls.Add(this.lblTriggers);
            this.Controls.Add(this.lblCreator);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.picProductImage);
            this.Font = new System.Drawing.Font("Lucida Sans Unicode", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.Name = "ProductCtrl";
            this.Size = new System.Drawing.Size(364, 87);
            ((System.ComponentModel.ISupportInitialize)(this.picProductImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picProductImage;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblCreator;
        private System.Windows.Forms.Label lblTriggers;
        private System.Windows.Forms.LinkLabel linkOnDeck;
    }
}
