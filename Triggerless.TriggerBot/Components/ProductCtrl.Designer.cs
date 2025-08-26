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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProductCtrl));
            this.picProductImage = new System.Windows.Forms.PictureBox();
            this.lblName = new System.Windows.Forms.Label();
            this.lblCreator = new System.Windows.Forms.Label();
            this.lblTriggers = new System.Windows.Forms.Label();
            this.linkOnDeck = new System.Windows.Forms.LinkLabel();
            this.linkWearItem = new System.Windows.Forms.LinkLabel();
            this.picDeleteItem = new System.Windows.Forms.PictureBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.picLips = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picProductImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picDeleteItem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picLips)).BeginInit();
            this.SuspendLayout();
            // 
            // picProductImage
            // 
            this.picProductImage.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("picProductImage.BackgroundImage")));
            this.picProductImage.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picProductImage.InitialImage = ((System.Drawing.Image)(resources.GetObject("picProductImage.InitialImage")));
            this.picProductImage.Location = new System.Drawing.Point(5, 4);
            this.picProductImage.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.picProductImage.Name = "picProductImage";
            this.picProductImage.Size = new System.Drawing.Size(100, 80);
            this.picProductImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picProductImage.TabIndex = 0;
            this.picProductImage.TabStop = false;
            this.toolTip1.SetToolTip(this.picProductImage, "Browse Product Page");
            this.picProductImage.Click += new System.EventHandler(this.ProductImageClicked);
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Font = new System.Drawing.Font("Liberation Sans", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblName.Location = new System.Drawing.Point(113, 3);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(192, 17);
            this.lblName.TabIndex = 1;
            this.lblName.Text = "012345678 012345678 0123";
            this.lblName.Click += new System.EventHandler(this.lblName_Click);
            // 
            // lblCreator
            // 
            this.lblCreator.AutoSize = true;
            this.lblCreator.Font = new System.Drawing.Font("Liberation Sans", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCreator.Location = new System.Drawing.Point(113, 25);
            this.lblCreator.Name = "lblCreator";
            this.lblCreator.Size = new System.Drawing.Size(185, 17);
            this.lblCreator.TabIndex = 2;
            this.lblCreator.Text = "by CrazyMusicTriggerFiend";
            // 
            // lblTriggers
            // 
            this.lblTriggers.AutoSize = true;
            this.lblTriggers.Font = new System.Drawing.Font("Liberation Sans", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTriggers.Location = new System.Drawing.Point(113, 44);
            this.lblTriggers.Name = "lblTriggers";
            this.lblTriggers.Size = new System.Drawing.Size(150, 17);
            this.lblTriggers.TabIndex = 3;
            this.lblTriggers.Text = "Triggers: xyz1 - xyz24";
            // 
            // linkOnDeck
            // 
            this.linkOnDeck.Font = new System.Drawing.Font("Liberation Sans", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkOnDeck.Location = new System.Drawing.Point(237, 64);
            this.linkOnDeck.Name = "linkOnDeck";
            this.linkOnDeck.Size = new System.Drawing.Size(128, 18);
            this.linkOnDeck.TabIndex = 4;
            this.linkOnDeck.TabStop = true;
            this.linkOnDeck.Text = "ON DECK >>";
            this.linkOnDeck.VisitedLinkColor = System.Drawing.Color.Blue;
            this.linkOnDeck.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkOnDeck_LinkClicked);
            this.linkOnDeck.MouseEnter += new System.EventHandler(this.LinkEnter);
            this.linkOnDeck.MouseLeave += new System.EventHandler(this.LinkLeave);
            // 
            // linkWearItem
            // 
            this.linkWearItem.Font = new System.Drawing.Font("Liberation Sans", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkWearItem.Location = new System.Drawing.Point(113, 64);
            this.linkWearItem.Name = "linkWearItem";
            this.linkWearItem.Size = new System.Drawing.Size(106, 18);
            this.linkWearItem.TabIndex = 5;
            this.linkWearItem.TabStop = true;
            this.linkWearItem.Text = "Wear Item";
            this.linkWearItem.VisitedLinkColor = System.Drawing.Color.Blue;
            this.linkWearItem.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.WearItem);
            this.linkWearItem.MouseEnter += new System.EventHandler(this.LinkEnter);
            this.linkWearItem.MouseLeave += new System.EventHandler(this.LinkLeave);
            // 
            // picDeleteItem
            // 
            this.picDeleteItem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.picDeleteItem.Image = global::Triggerless.TriggerBot.Properties.Resources.exclude16;
            this.picDeleteItem.Location = new System.Drawing.Point(357, 0);
            this.picDeleteItem.Name = "picDeleteItem";
            this.picDeleteItem.Size = new System.Drawing.Size(16, 16);
            this.picDeleteItem.TabIndex = 6;
            this.picDeleteItem.TabStop = false;
            this.picDeleteItem.Click += new System.EventHandler(this.ExcludeSong);
            // 
            // picLips
            // 
            this.picLips.Image = ((System.Drawing.Image)(resources.GetObject("picLips.Image")));
            this.picLips.Location = new System.Drawing.Point(334, 35);
            this.picLips.Name = "picLips";
            this.picLips.Size = new System.Drawing.Size(24, 23);
            this.picLips.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picLips.TabIndex = 7;
            this.picLips.TabStop = false;
            // 
            // ProductCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(248)))));
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.picLips);
            this.Controls.Add(this.picDeleteItem);
            this.Controls.Add(this.linkWearItem);
            this.Controls.Add(this.linkOnDeck);
            this.Controls.Add(this.lblTriggers);
            this.Controls.Add(this.lblCreator);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.picProductImage);
            this.Font = new System.Drawing.Font("Lucida Sans Unicode", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.Name = "ProductCtrl";
            this.Size = new System.Drawing.Size(376, 87);
            ((System.ComponentModel.ISupportInitialize)(this.picProductImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picDeleteItem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picLips)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picProductImage;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblCreator;
        private System.Windows.Forms.Label lblTriggers;
        private System.Windows.Forms.LinkLabel linkOnDeck;
        private System.Windows.Forms.LinkLabel linkWearItem;
        private System.Windows.Forms.PictureBox picDeleteItem;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.PictureBox picLips;
    }
}
