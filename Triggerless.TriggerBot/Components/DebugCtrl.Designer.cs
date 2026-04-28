namespace Triggerless.TriggerBot
{
    partial class DebugCtrl
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
            this.txtDebug = new System.Windows.Forms.TextBox();
            this.Send = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnPaste = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtDebug
            // 
            this.txtDebug.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(29)))), ((int)(((byte)(51)))));
            this.txtDebug.Location = new System.Drawing.Point(44, 74);
            this.txtDebug.Name = "txtDebug";
            this.txtDebug.Size = new System.Drawing.Size(239, 26);
            this.txtDebug.TabIndex = 0;
            // 
            // Send
            // 
            this.Send.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(29)))), ((int)(((byte)(51)))));
            this.Send.Location = new System.Drawing.Point(289, 70);
            this.Send.Name = "Send";
            this.Send.Size = new System.Drawing.Size(88, 38);
            this.Send.TabIndex = 1;
            this.Send.Text = "Send";
            this.Send.UseVisualStyleBackColor = true;
            this.Send.Click += new System.EventHandler(this.Send_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(41, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "Send to IMVU";
            // 
            // btnPaste
            // 
            this.btnPaste.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(29)))), ((int)(((byte)(51)))));
            this.btnPaste.Location = new System.Drawing.Point(444, 72);
            this.btnPaste.Name = "btnPaste";
            this.btnPaste.Size = new System.Drawing.Size(85, 35);
            this.btnPaste.TabIndex = 3;
            this.btnPaste.Text = "Paste";
            this.btnPaste.UseVisualStyleBackColor = true;
            this.btnPaste.Click += new System.EventHandler(this.btnPaste_Click);
            // 
            // DebugCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(29)))), ((int)(((byte)(51)))));
            this.Controls.Add(this.btnPaste);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Send);
            this.Controls.Add(this.txtDebug);
            this.Font = new System.Drawing.Font("Liberation Sans", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "DebugCtrl";
            this.Size = new System.Drawing.Size(790, 428);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtDebug;
        private System.Windows.Forms.Button Send;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnPaste;
    }
}
