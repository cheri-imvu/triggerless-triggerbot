namespace Triggerless.TriggerBot
{
    partial class UpdateForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UpdateForm));
            this.lblVersion = new System.Windows.Forms.Label();
            this.btnUpdateImmediately = new System.Windows.Forms.Button();
            this.btnUpdateOnExit = new System.Windows.Forms.Button();
            this.btnIgnore = new System.Windows.Forms.Button();
            this.txtWhatsNew = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // lblVersion
            // 
            this.lblVersion.BackColor = System.Drawing.Color.Transparent;
            this.lblVersion.Location = new System.Drawing.Point(124, 19);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(363, 44);
            this.lblVersion.TabIndex = 0;
            this.lblVersion.Text = "A new version of Triggerbot {version} is available. What would you like to do?";
            // 
            // btnUpdateImmediately
            // 
            this.btnUpdateImmediately.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnUpdateImmediately.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnUpdateImmediately.Location = new System.Drawing.Point(75, 233);
            this.btnUpdateImmediately.Name = "btnUpdateImmediately";
            this.btnUpdateImmediately.Size = new System.Drawing.Size(151, 32);
            this.btnUpdateImmediately.TabIndex = 1;
            this.btnUpdateImmediately.Text = "Update Now";
            this.btnUpdateImmediately.UseVisualStyleBackColor = true;
            this.btnUpdateImmediately.Click += new System.EventHandler(this.btnUpdateImmediately_Click);
            // 
            // btnUpdateOnExit
            // 
            this.btnUpdateOnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnUpdateOnExit.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnUpdateOnExit.Location = new System.Drawing.Point(231, 233);
            this.btnUpdateOnExit.Name = "btnUpdateOnExit";
            this.btnUpdateOnExit.Size = new System.Drawing.Size(151, 32);
            this.btnUpdateOnExit.TabIndex = 2;
            this.btnUpdateOnExit.Text = "Update Later";
            this.btnUpdateOnExit.UseVisualStyleBackColor = true;
            this.btnUpdateOnExit.Click += new System.EventHandler(this.btnUpdateOnExit_Click);
            // 
            // btnIgnore
            // 
            this.btnIgnore.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnIgnore.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnIgnore.Location = new System.Drawing.Point(388, 233);
            this.btnIgnore.Name = "btnIgnore";
            this.btnIgnore.Size = new System.Drawing.Size(151, 32);
            this.btnIgnore.TabIndex = 3;
            this.btnIgnore.Text = "Ignore for now";
            this.btnIgnore.UseVisualStyleBackColor = true;
            this.btnIgnore.Click += new System.EventHandler(this.btnIgnore_Click);
            // 
            // txtWhatsNew
            // 
            this.txtWhatsNew.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.txtWhatsNew.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(29)))), ((int)(((byte)(51)))));
            this.txtWhatsNew.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.txtWhatsNew.Location = new System.Drawing.Point(80, 66);
            this.txtWhatsNew.Multiline = true;
            this.txtWhatsNew.Name = "txtWhatsNew";
            this.txtWhatsNew.Size = new System.Drawing.Size(459, 140);
            this.txtWhatsNew.TabIndex = 4;
            // 
            // UpdateForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(29)))), ((int)(((byte)(51)))));
            this.ClientSize = new System.Drawing.Size(615, 277);
            this.Controls.Add(this.txtWhatsNew);
            this.Controls.Add(this.btnIgnore);
            this.Controls.Add(this.btnUpdateOnExit);
            this.Controls.Add(this.btnUpdateImmediately);
            this.Controls.Add(this.lblVersion);
            this.Font = new System.Drawing.Font("Liberation Sans", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UpdateForm";
            this.ShowInTaskbar = false;
            this.Text = "New Update Available";
            this.Load += new System.EventHandler(this.LoadForm);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.Button btnUpdateImmediately;
        private System.Windows.Forms.Button btnUpdateOnExit;
        private System.Windows.Forms.Button btnIgnore;
        private System.Windows.Forms.TextBox txtWhatsNew;
    }
}