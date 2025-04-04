﻿namespace Triggerless.TriggerBot.Forms
{
    partial class TechSupport
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this._txtAviName = new System.Windows.Forms.TextBox();
            this._btnUpload = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.pictureBox1.Image = global::Triggerless.TriggerBot.Properties.Resources.digital;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(652, 86);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(12, 94);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(617, 84);
            this.label1.TabIndex = 1;
            this.label1.Text = "This tool will upload your IMVU inventory and Triggerless databases to @Triggers." +
    " Your IMVU Avatar name is required, otherwise, we have no way to figure out who\'" +
    "s sending this.";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(95, 190);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(137, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "IMVU Avatar name:";
            // 
            // _txtAviName
            // 
            this._txtAviName.Location = new System.Drawing.Point(271, 184);
            this._txtAviName.Name = "_txtAviName";
            this._txtAviName.Size = new System.Drawing.Size(239, 28);
            this._txtAviName.TabIndex = 0;
            // 
            // _btnUpload
            // 
            this._btnUpload.Location = new System.Drawing.Point(247, 231);
            this._btnUpload.Name = "_btnUpload";
            this._btnUpload.Size = new System.Drawing.Size(156, 34);
            this._btnUpload.TabIndex = 1;
            this._btnUpload.Text = "Upload";
            this._btnUpload.UseVisualStyleBackColor = true;
            this._btnUpload.Click += new System.EventHandler(this._btnUpload_Click);
            // 
            // TechSupport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(652, 286);
            this.Controls.Add(this._btnUpload);
            this.Controls.Add(this._txtAviName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Font = new System.Drawing.Font("Lucida Sans Unicode", 10F);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "TechSupport";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "TechSupport";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox _txtAviName;
        private System.Windows.Forms.Button _btnUpload;
    }
}