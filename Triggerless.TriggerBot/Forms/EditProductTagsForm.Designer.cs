namespace Triggerless.TriggerBot.Forms
{
    partial class EditProductTagsForm
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
            this.components = new System.ComponentModel.Container();
            this.picProductImage = new System.Windows.Forms.PictureBox();
            this.lblName = new System.Windows.Forms.Label();
            this.txtNewTag = new System.Windows.Forms.TextBox();
            this.btnNewTag = new System.Windows.Forms.Button();
            this.lvTags = new System.Windows.Forms.ListView();
            this.colName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.menuPopup = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.deleteTagToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.cancelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lblCreator = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picProductImage)).BeginInit();
            this.menuPopup.SuspendLayout();
            this.SuspendLayout();
            // 
            // picProductImage
            // 
            this.picProductImage.Location = new System.Drawing.Point(18, 15);
            this.picProductImage.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.picProductImage.Name = "picProductImage";
            this.picProductImage.Size = new System.Drawing.Size(100, 80);
            this.picProductImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picProductImage.TabIndex = 0;
            this.picProductImage.TabStop = false;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Font = new System.Drawing.Font("Liberation Sans", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblName.Location = new System.Drawing.Point(130, 43);
            this.lblName.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(65, 21);
            this.lblName.TabIndex = 1;
            this.lblName.Text = "label1";
            // 
            // txtNewTag
            // 
            this.txtNewTag.Location = new System.Drawing.Point(72, 107);
            this.txtNewTag.Margin = new System.Windows.Forms.Padding(2);
            this.txtNewTag.Name = "txtNewTag";
            this.txtNewTag.Size = new System.Drawing.Size(211, 26);
            this.txtNewTag.TabIndex = 0;
            // 
            // btnNewTag
            // 
            this.btnNewTag.Font = new System.Drawing.Font("Liberation Sans", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNewTag.ForeColor = System.Drawing.Color.Black;
            this.btnNewTag.Location = new System.Drawing.Point(287, 106);
            this.btnNewTag.Margin = new System.Windows.Forms.Padding(2);
            this.btnNewTag.Name = "btnNewTag";
            this.btnNewTag.Size = new System.Drawing.Size(28, 27);
            this.btnNewTag.TabIndex = 1;
            this.btnNewTag.Text = "+";
            this.btnNewTag.UseVisualStyleBackColor = true;
            this.btnNewTag.Click += new System.EventHandler(this.btnNewTag_Click);
            // 
            // lvTags
            // 
            this.lvTags.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(29)))), ((int)(((byte)(51)))));
            this.lvTags.CheckBoxes = true;
            this.lvTags.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colName,
            this.colId});
            this.lvTags.ContextMenuStrip = this.menuPopup;
            this.lvTags.Font = new System.Drawing.Font("Liberation Sans", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvTags.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lvTags.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lvTags.HideSelection = false;
            this.lvTags.Location = new System.Drawing.Point(69, 144);
            this.lvTags.Margin = new System.Windows.Forms.Padding(2);
            this.lvTags.Name = "lvTags";
            this.lvTags.Size = new System.Drawing.Size(246, 303);
            this.lvTags.TabIndex = 4;
            this.lvTags.UseCompatibleStateImageBehavior = false;
            this.lvTags.View = System.Windows.Forms.View.Details;
            this.lvTags.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.CheckChanging);
            this.lvTags.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lvTags_MouseDown);
            // 
            // colName
            // 
            this.colName.Text = "Tag Name";
            this.colName.Width = 200;
            // 
            // colId
            // 
            this.colId.Text = "Tag Id";
            // 
            // menuPopup
            // 
            this.menuPopup.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteTagToolStripMenuItem,
            this.toolStripMenuItem1,
            this.cancelToolStripMenuItem});
            this.menuPopup.Name = "menuPopup";
            this.menuPopup.Size = new System.Drawing.Size(130, 54);
            // 
            // deleteTagToolStripMenuItem
            // 
            this.deleteTagToolStripMenuItem.Name = "deleteTagToolStripMenuItem";
            this.deleteTagToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.deleteTagToolStripMenuItem.Text = "Delete Tag";
            this.deleteTagToolStripMenuItem.Click += new System.EventHandler(this.deleteTagToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(126, 6);
            // 
            // cancelToolStripMenuItem
            // 
            this.cancelToolStripMenuItem.Name = "cancelToolStripMenuItem";
            this.cancelToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.cancelToolStripMenuItem.Text = "Cancel";
            // 
            // lblCreator
            // 
            this.lblCreator.AutoSize = true;
            this.lblCreator.Font = new System.Drawing.Font("Liberation Sans", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCreator.Location = new System.Drawing.Point(130, 71);
            this.lblCreator.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblCreator.Name = "lblCreator";
            this.lblCreator.Size = new System.Drawing.Size(61, 21);
            this.lblCreator.TabIndex = 5;
            this.lblCreator.Text = "label1";
            // 
            // btnClose
            // 
            this.btnClose.ForeColor = System.Drawing.Color.Black;
            this.btnClose.Location = new System.Drawing.Point(130, 452);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(106, 34);
            this.btnClose.TabIndex = 7;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Liberation Sans", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(130, 15);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(176, 21);
            this.label1.TabIndex = 8;
            this.label1.Text = "Edit Product Tags";
            // 
            // EditProductTagsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(29)))), ((int)(((byte)(51)))));
            this.ClientSize = new System.Drawing.Size(390, 511);
            this.ControlBox = false;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lblCreator);
            this.Controls.Add(this.lvTags);
            this.Controls.Add(this.btnNewTag);
            this.Controls.Add(this.txtNewTag);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.picProductImage);
            this.Font = new System.Drawing.Font("Liberation Sans", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EditProductTagsForm";
            this.Shown += new System.EventHandler(this.EditProductTagsForm_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.picProductImage)).EndInit();
            this.menuPopup.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picProductImage;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.TextBox txtNewTag;
        private System.Windows.Forms.Button btnNewTag;
        private System.Windows.Forms.ListView lvTags;
        private System.Windows.Forms.Label lblCreator;
        private System.Windows.Forms.ColumnHeader colName;
        private System.Windows.Forms.ColumnHeader colId;
        private System.Windows.Forms.ContextMenuStrip menuPopup;
        private System.Windows.Forms.ToolStripMenuItem deleteTagToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem cancelToolStripMenuItem;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label label1;
    }
}