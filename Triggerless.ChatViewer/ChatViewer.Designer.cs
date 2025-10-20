namespace Triggerless.ChatViewerPlugIn
{
    partial class ChatViewer
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
            this.btnReload = new System.Windows.Forms.Button();
            this.pnlGrid = new System.Windows.Forms.Panel();
            this.grdChat = new System.Windows.Forms.DataGridView();
            this.colAvatar = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colText = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnSave = new System.Windows.Forms.Button();
            this.pnlGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdChat)).BeginInit();
            this.SuspendLayout();
            // 
            // btnReload
            // 
            this.btnReload.Location = new System.Drawing.Point(4, 4);
            this.btnReload.Margin = new System.Windows.Forms.Padding(4);
            this.btnReload.Name = "btnReload";
            this.btnReload.Size = new System.Drawing.Size(104, 46);
            this.btnReload.TabIndex = 0;
            this.btnReload.Text = "Reload";
            this.btnReload.UseVisualStyleBackColor = true;
            this.btnReload.Click += new System.EventHandler(this.Reload);
            // 
            // pnlGrid
            // 
            this.pnlGrid.Controls.Add(this.grdChat);
            this.pnlGrid.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlGrid.Location = new System.Drawing.Point(0, 57);
            this.pnlGrid.Name = "pnlGrid";
            this.pnlGrid.Size = new System.Drawing.Size(802, 535);
            this.pnlGrid.TabIndex = 1;
            // 
            // grdChat
            // 
            this.grdChat.AllowUserToAddRows = false;
            this.grdChat.AllowUserToDeleteRows = false;
            this.grdChat.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdChat.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colAvatar,
            this.colText});
            this.grdChat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdChat.Location = new System.Drawing.Point(0, 0);
            this.grdChat.Name = "grdChat";
            this.grdChat.ReadOnly = true;
            this.grdChat.Size = new System.Drawing.Size(802, 535);
            this.grdChat.TabIndex = 0;
            // 
            // colAvatar
            // 
            this.colAvatar.HeaderText = "Avatar";
            this.colAvatar.Name = "colAvatar";
            this.colAvatar.ReadOnly = true;
            this.colAvatar.Width = 200;
            // 
            // colText
            // 
            this.colText.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colText.HeaderText = "Text";
            this.colText.Name = "colText";
            this.colText.ReadOnly = true;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(130, 4);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(104, 46);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "Save As...";
            this.btnSave.UseVisualStyleBackColor = true;
            // 
            // ChatViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.pnlGrid);
            this.Controls.Add(this.btnReload);
            this.Font = new System.Drawing.Font("Liberation Sans", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "ChatViewer";
            this.Size = new System.Drawing.Size(802, 592);
            this.pnlGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdChat)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnReload;
        private System.Windows.Forms.Panel pnlGrid;
        private System.Windows.Forms.DataGridView grdChat;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAvatar;
        private System.Windows.Forms.DataGridViewTextBoxColumn colText;
    }
}
