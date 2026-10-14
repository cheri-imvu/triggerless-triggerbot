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
            this.lblName = new System.Windows.Forms.Label();
            this.lblCreator = new System.Windows.Forms.Label();
            this.lblTriggers = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.linkLabelTag = new System.Windows.Forms.LinkLabel();
            this.picLips = new System.Windows.Forms.PictureBox();
            this.picDeleteItem = new System.Windows.Forms.PictureBox();
            this.picProductImage = new System.Windows.Forms.PictureBox();
            this.picAutoFront = new System.Windows.Forms.PictureBox();
            this.picAutoQueue = new System.Windows.Forms.PictureBox();
            this.picOnDeck = new System.Windows.Forms.PictureBox();
            this.picWear = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picLips)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picDeleteItem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picProductImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picAutoFront)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picAutoQueue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picOnDeck)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picWear)).BeginInit();
            this.SuspendLayout();
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblName.Font = new System.Drawing.Font("Liberation Sans", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblName.ForeColor = System.Drawing.Color.White;
            this.lblName.Location = new System.Drawing.Point(113, 3);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(192, 17);
            this.lblName.TabIndex = 1;
            this.lblName.Text = "012345678 012345678 0123";
            this.lblName.Click += new System.EventHandler(this.lblName_Click);
            this.lblName.MouseEnter += new System.EventHandler(this.lblName_MouseEnter);
            this.lblName.MouseLeave += new System.EventHandler(this.lblName_MouseLeave);
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
            // linkLabelTag
            // 
            this.linkLabelTag.BackColor = System.Drawing.Color.Transparent;
            this.linkLabelTag.Font = new System.Drawing.Font("Liberation Sans", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabelTag.LinkColor = System.Drawing.Color.Aqua;
            this.linkLabelTag.Location = new System.Drawing.Point(315, -2);
            this.linkLabelTag.Name = "linkLabelTag";
            this.linkLabelTag.Size = new System.Drawing.Size(29, 33);
            this.linkLabelTag.TabIndex = 8;
            this.linkLabelTag.TabStop = true;
            this.linkLabelTag.Text = "#";
            this.toolTip1.SetToolTip(this.linkLabelTag, "Edit Tags");
            this.linkLabelTag.VisitedLinkColor = System.Drawing.Color.Aqua;
            this.linkLabelTag.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelTag_LinkClicked);
            this.linkLabelTag.MouseEnter += new System.EventHandler(this.LinkEnter);
            this.linkLabelTag.MouseLeave += new System.EventHandler(this.LinkLeave);
            // 
            // picLips
            // 
            this.picLips.Image = ((System.Drawing.Image)(resources.GetObject("picLips.Image")));
            this.picLips.Location = new System.Drawing.Point(327, 35);
            this.picLips.Name = "picLips";
            this.picLips.Size = new System.Drawing.Size(24, 23);
            this.picLips.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picLips.TabIndex = 7;
            this.picLips.TabStop = false;
            this.toolTip1.SetToolTip(this.picLips, "This song has lyrics.");
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
            // picAutoFront
            // 
            this.picAutoFront.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picAutoFront.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picAutoFront.Image = global::Triggerless.TriggerBot.Properties.Resources.bolt_2;
            this.picAutoFront.Location = new System.Drawing.Point(327, 61);
            this.picAutoFront.Name = "picAutoFront";
            this.picAutoFront.Size = new System.Drawing.Size(24, 24);
            this.picAutoFront.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picAutoFront.TabIndex = 9;
            this.picAutoFront.TabStop = false;
            this.toolTip1.SetToolTip(this.picAutoFront, "AUTO DJ - Add to Front");
            // 
            // picAutoQueue
            // 
            this.picAutoQueue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picAutoQueue.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picAutoQueue.Image = global::Triggerless.TriggerBot.Properties.Resources.bolt_1;
            this.picAutoQueue.Location = new System.Drawing.Point(297, 61);
            this.picAutoQueue.Name = "picAutoQueue";
            this.picAutoQueue.Size = new System.Drawing.Size(24, 24);
            this.picAutoQueue.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picAutoQueue.TabIndex = 10;
            this.picAutoQueue.TabStop = false;
            this.toolTip1.SetToolTip(this.picAutoQueue, "AUTO DJ - Add to Back");
            // 
            // picOnDeck
            // 
            this.picOnDeck.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picOnDeck.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picOnDeck.Image = global::Triggerless.TriggerBot.Properties.Resources.home_plate;
            this.picOnDeck.Location = new System.Drawing.Point(267, 61);
            this.picOnDeck.Name = "picOnDeck";
            this.picOnDeck.Size = new System.Drawing.Size(24, 24);
            this.picOnDeck.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picOnDeck.TabIndex = 11;
            this.picOnDeck.TabStop = false;
            this.toolTip1.SetToolTip(this.picOnDeck, "SEND TO ON DECK");
            this.picOnDeck.Click += new System.EventHandler(this.SendToOnDeck);
            // 
            // picWear
            // 
            this.picWear.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picWear.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picWear.Image = global::Triggerless.TriggerBot.Properties.Resources.shirt;
            this.picWear.Location = new System.Drawing.Point(239, 61);
            this.picWear.Name = "picWear";
            this.picWear.Size = new System.Drawing.Size(24, 24);
            this.picWear.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picWear.TabIndex = 12;
            this.picWear.TabStop = false;
            this.toolTip1.SetToolTip(this.picWear, "WEAR ITEM");
            this.picWear.Click += new System.EventHandler(this.WearItem);
            // 
            // ProductCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(29)))), ((int)(((byte)(51)))));
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.picWear);
            this.Controls.Add(this.picOnDeck);
            this.Controls.Add(this.picAutoQueue);
            this.Controls.Add(this.picAutoFront);
            this.Controls.Add(this.linkLabelTag);
            this.Controls.Add(this.picLips);
            this.Controls.Add(this.picDeleteItem);
            this.Controls.Add(this.lblTriggers);
            this.Controls.Add(this.lblCreator);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.picProductImage);
            this.Font = new System.Drawing.Font("Lucida Sans Unicode", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.Name = "ProductCtrl";
            this.Size = new System.Drawing.Size(376, 87);
            ((System.ComponentModel.ISupportInitialize)(this.picLips)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picDeleteItem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picProductImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picAutoFront)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picAutoQueue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picOnDeck)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picWear)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picProductImage;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblCreator;
        private System.Windows.Forms.Label lblTriggers;
        private System.Windows.Forms.PictureBox picDeleteItem;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.PictureBox picLips;
        private System.Windows.Forms.LinkLabel linkLabelTag;
        private System.Windows.Forms.PictureBox picAutoFront;
        private System.Windows.Forms.PictureBox picAutoQueue;
        private System.Windows.Forms.PictureBox picOnDeck;
        private System.Windows.Forms.PictureBox picWear;
    }
}
