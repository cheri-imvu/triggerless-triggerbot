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
            this.label1 = new System.Windows.Forms.Label();
            this.btnUpdateImmediately = new System.Windows.Forms.Button();
            this.btnUpdateOnExit = new System.Windows.Forms.Button();
            this.btnIgnore = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(140, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(332, 44);
            this.label1.TabIndex = 0;
            this.label1.Text = "A new version of Triggerbot {version} is available. What would you like to do?";
            // 
            // btnUpdateImmediately
            // 
            this.btnUpdateImmediately.Location = new System.Drawing.Point(15, 81);
            this.btnUpdateImmediately.Name = "btnUpdateImmediately";
            this.btnUpdateImmediately.Size = new System.Drawing.Size(170, 34);
            this.btnUpdateImmediately.TabIndex = 1;
            this.btnUpdateImmediately.Text = "Update Now";
            this.btnUpdateImmediately.UseVisualStyleBackColor = true;
            this.btnUpdateImmediately.Click += new System.EventHandler(this.btnUpdateImmediately_Click);
            // 
            // btnUpdateOnExit
            // 
            this.btnUpdateOnExit.Location = new System.Drawing.Point(191, 81);
            this.btnUpdateOnExit.Name = "btnUpdateOnExit";
            this.btnUpdateOnExit.Size = new System.Drawing.Size(170, 34);
            this.btnUpdateOnExit.TabIndex = 2;
            this.btnUpdateOnExit.Text = "Update when I finish";
            this.btnUpdateOnExit.UseVisualStyleBackColor = true;
            this.btnUpdateOnExit.Click += new System.EventHandler(this.btnUpdateOnExit_Click);
            // 
            // btnIgnore
            // 
            this.btnIgnore.Location = new System.Drawing.Point(367, 81);
            this.btnIgnore.Name = "btnIgnore";
            this.btnIgnore.Size = new System.Drawing.Size(170, 34);
            this.btnIgnore.TabIndex = 3;
            this.btnIgnore.Text = "Ignore for now";
            this.btnIgnore.UseVisualStyleBackColor = true;
            this.btnIgnore.Click += new System.EventHandler(this.btnIgnore_Click);
            // 
            // UpdateForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(560, 140);
            this.Controls.Add(this.btnIgnore);
            this.Controls.Add(this.btnUpdateOnExit);
            this.Controls.Add(this.btnUpdateImmediately);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Lucida Sans Unicode", 11F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UpdateForm";
            this.ShowInTaskbar = false;
            this.Text = "New Update Available";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnUpdateImmediately;
        private System.Windows.Forms.Button btnUpdateOnExit;
        private System.Windows.Forms.Button btnIgnore;
    }
}