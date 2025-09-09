namespace Triggerless.TriggerBot
{
    partial class AddnTriggersForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddnTriggersForm));
            this.gridTriggers = new System.Windows.Forms.DataGridView();
            this.colPrefix = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSequence = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTrigger = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAddnTriggers = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.gridTriggers)).BeginInit();
            this.SuspendLayout();
            // 
            // gridTriggers
            // 
            this.gridTriggers.AllowUserToAddRows = false;
            this.gridTriggers.AllowUserToDeleteRows = false;
            this.gridTriggers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridTriggers.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colPrefix,
            this.colSequence,
            this.colTrigger,
            this.colAddnTriggers});
            this.gridTriggers.Location = new System.Drawing.Point(22, 45);
            this.gridTriggers.Margin = new System.Windows.Forms.Padding(4);
            this.gridTriggers.Name = "gridTriggers";
            this.gridTriggers.RowHeadersWidth = 51;
            this.gridTriggers.Size = new System.Drawing.Size(709, 405);
            this.gridTriggers.TabIndex = 0;
            this.gridTriggers.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridTriggers_CellEnter);
            // 
            // colPrefix
            // 
            this.colPrefix.HeaderText = "Prefix";
            this.colPrefix.MinimumWidth = 6;
            this.colPrefix.Name = "colPrefix";
            this.colPrefix.ReadOnly = true;
            this.colPrefix.Visible = false;
            this.colPrefix.Width = 125;
            // 
            // colSequence
            // 
            this.colSequence.HeaderText = "Sequence";
            this.colSequence.MinimumWidth = 6;
            this.colSequence.Name = "colSequence";
            this.colSequence.ReadOnly = true;
            this.colSequence.Visible = false;
            this.colSequence.Width = 125;
            // 
            // colTrigger
            // 
            this.colTrigger.HeaderText = "Trigger";
            this.colTrigger.MinimumWidth = 6;
            this.colTrigger.Name = "colTrigger";
            this.colTrigger.ReadOnly = true;
            this.colTrigger.Width = 125;
            // 
            // colAddnTriggers
            // 
            this.colAddnTriggers.HeaderText = "Additional Triggers";
            this.colAddnTriggers.MinimumWidth = 6;
            this.colAddnTriggers.Name = "colAddnTriggers";
            this.colAddnTriggers.Width = 500;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Location = new System.Drawing.Point(19, 9);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(204, 18);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "Song Title Name by Avatar";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(480, 475);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(112, 44);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSaveClicked);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(607, 475);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(112, 44);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.CancelClick);
            // 
            // AddnTriggersForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(779, 535);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.gridTriggers);
            this.Font = new System.Drawing.Font("Lucida Sans Unicode", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "AddnTriggersForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Additional Triggers";
            this.Load += new System.EventHandler(this.LoadForm);
            ((System.ComponentModel.ISupportInitialize)(this.gridTriggers)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView gridTriggers;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPrefix;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSequence;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTrigger;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAddnTriggers;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
    }
}