namespace Triggerless.TriggerBot
{
    partial class AudioSplicerForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AudioSplicerForm));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dlgOpenFile = new System.Windows.Forms.OpenFileDialog();
            this.txtFilename = new System.Windows.Forms.TextBox();
            this.btnSelectFile = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.cboAudioLength = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.txtPrefix = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.radioFemale = new System.Windows.Forms.RadioButton();
            this.radioMale = new System.Windows.Forms.RadioButton();
            this.checkIcons = new System.Windows.Forms.CheckBox();
            this.checkOGGFiles = new System.Windows.Forms.CheckBox();
            this.btnStart = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.lblAction = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.btnShowMe = new System.Windows.Forms.Button();
            this.lblDuration = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.rdoAMS = new System.Windows.Forms.RadioButton();
            this.rdoFMS = new System.Windows.Forms.RadioButton();
            this.rdoHQM = new System.Windows.Forms.RadioButton();
            this.rdoHQS = new System.Windows.Forms.RadioButton();
            this.picWaveform = new System.Windows.Forms.PictureBox();
            this._audioSegmenter = new Triggerless.TriggerBot.AudioSegmenter();
            this.btnIncreaseVolume = new System.Windows.Forms.Button();
            this.btnDecreaseVolume = new System.Windows.Forms.Button();
            this.btnResetVolume = new System.Windows.Forms.Button();
            this.lblVolume = new System.Windows.Forms.Label();
            this.chkCheap = new System.Windows.Forms.CheckBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picWaveform)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(2, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(779, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "This Audio Splicer will convert an MP3, FLAC, OGG or WAV file into CHKN file(s) y" +
    "ou can upload to IMVU.";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(2, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(247, 18);
            this.label2.TabIndex = 1;
            this.label2.Text = "Choose an audio file to convert:";
            // 
            // dlgOpenFile
            // 
            this.dlgOpenFile.Filter = "MP3 files|*.mp3|All Files|*.*";
            // 
            // txtFilename
            // 
            this.txtFilename.Enabled = false;
            this.txtFilename.Location = new System.Drawing.Point(8, 64);
            this.txtFilename.Name = "txtFilename";
            this.txtFilename.Size = new System.Drawing.Size(472, 30);
            this.txtFilename.TabIndex = 2;
            // 
            // btnSelectFile
            // 
            this.btnSelectFile.Location = new System.Drawing.Point(487, 64);
            this.btnSelectFile.Name = "btnSelectFile";
            this.btnSelectFile.Size = new System.Drawing.Size(75, 30);
            this.btnSelectFile.TabIndex = 3;
            this.btnSelectFile.Text = "Select...";
            this.btnSelectFile.UseVisualStyleBackColor = true;
            this.btnSelectFile.Click += new System.EventHandler(this.SelectFile);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(5, 114);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(256, 18);
            this.label3.TabIndex = 4;
            this.label3.Text = "Maximum OGG audio length (sec)";
            // 
            // cboAudioLength
            // 
            this.cboAudioLength.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboAudioLength.FormattingEnabled = true;
            this.cboAudioLength.Items.AddRange(new object[] {
            "19.9",
            "19.5",
            "19.0",
            "18.5",
            "18.0",
            "17.5",
            "17.0",
            "16.5",
            "16.0",
            "15.0",
            "14.0",
            "13.0",
            "12.0",
            "11.0",
            "10.0",
            "9.0"});
            this.cboAudioLength.Location = new System.Drawing.Point(270, 111);
            this.cboAudioLength.Name = "cboAudioLength";
            this.cboAudioLength.Size = new System.Drawing.Size(128, 26);
            this.cboAudioLength.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(5, 154);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(112, 18);
            this.label4.TabIndex = 6;
            this.label4.Text = "Audio Quality:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(5, 227);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(117, 18);
            this.label10.TabIndex = 13;
            this.label10.Text = "Trigger Prefix:";
            // 
            // txtPrefix
            // 
            this.txtPrefix.Location = new System.Drawing.Point(130, 224);
            this.txtPrefix.Name = "txtPrefix";
            this.txtPrefix.Size = new System.Drawing.Size(100, 30);
            this.txtPrefix.TabIndex = 14;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(245, 227);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(500, 18);
            this.label11.TabIndex = 15;
            this.label11.Text = "Only letters are allowed. No commas, spaces or special characters.";
            // 
            // radioFemale
            // 
            this.radioFemale.AutoSize = true;
            this.radioFemale.Checked = true;
            this.radioFemale.Location = new System.Drawing.Point(12, 6);
            this.radioFemale.Name = "radioFemale";
            this.radioFemale.Size = new System.Drawing.Size(104, 22);
            this.radioFemale.TabIndex = 16;
            this.radioFemale.TabStop = true;
            this.radioFemale.Text = "Female  or";
            this.radioFemale.UseVisualStyleBackColor = true;
            this.radioFemale.CheckedChanged += new System.EventHandler(this.FemaleMaleChanged);
            // 
            // radioMale
            // 
            this.radioMale.AutoSize = true;
            this.radioMale.Location = new System.Drawing.Point(126, 6);
            this.radioMale.Name = "radioMale";
            this.radioMale.Size = new System.Drawing.Size(138, 22);
            this.radioMale.TabIndex = 17;
            this.radioMale.Text = "Male Accessory";
            this.radioMale.UseVisualStyleBackColor = true;
            this.radioMale.CheckedChanged += new System.EventHandler(this.FemaleMaleChanged);
            // 
            // checkIcons
            // 
            this.checkIcons.AutoSize = true;
            this.checkIcons.Checked = true;
            this.checkIcons.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkIcons.Location = new System.Drawing.Point(310, 269);
            this.checkIcons.Name = "checkIcons";
            this.checkIcons.Size = new System.Drawing.Size(197, 22);
            this.checkIcons.TabIndex = 18;
            this.checkIcons.Text = "Generate 100x80 icons";
            this.checkIcons.UseVisualStyleBackColor = true;
            this.checkIcons.Click += new System.EventHandler(this.GenerateIconsChanged);
            // 
            // checkOGGFiles
            // 
            this.checkOGGFiles.AutoSize = true;
            this.checkOGGFiles.Checked = true;
            this.checkOGGFiles.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkOGGFiles.Location = new System.Drawing.Point(537, 269);
            this.checkOGGFiles.Name = "checkOGGFiles";
            this.checkOGGFiles.Size = new System.Drawing.Size(168, 22);
            this.checkOGGFiles.TabIndex = 19;
            this.checkOGGFiles.Text = "Clean up OGG Files";
            this.checkOGGFiles.UseVisualStyleBackColor = true;
            this.checkOGGFiles.CheckedChanged += new System.EventHandler(this.CleanUpOggChanged);
            // 
            // btnStart
            // 
            this.btnStart.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnStart.Font = new System.Drawing.Font("Lucida Sans Unicode", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStart.ForeColor = System.Drawing.Color.White;
            this.btnStart.Location = new System.Drawing.Point(12, 307);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(122, 37);
            this.btnStart.TabIndex = 20;
            this.btnStart.Text = "Let\'s Start!";
            this.btnStart.UseVisualStyleBackColor = false;
            this.btnStart.Click += new System.EventHandler(this.StartConversion);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(152, 316);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(60, 18);
            this.label12.TabIndex = 21;
            this.label12.Text = "Action:";
            // 
            // lblAction
            // 
            this.lblAction.AutoSize = true;
            this.lblAction.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblAction.Location = new System.Drawing.Point(218, 316);
            this.lblAction.Name = "lblAction";
            this.lblAction.Size = new System.Drawing.Size(37, 20);
            this.lblAction.TabIndex = 22;
            this.lblAction.Text = "Idle";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(2, 418);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(701, 18);
            this.label14.TabIndex = 23;
            this.label14.Text = "Please note that @Triggers will make 220 cr profit for every CHKN you submit usin" +
    "g this tool.";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(2, 364);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(502, 18);
            this.label15.TabIndex = 24;
            this.label15.Text = "Your CHKN file will always be in the Documents\\Triggerbot folder.";
            // 
            // btnShowMe
            // 
            this.btnShowMe.Location = new System.Drawing.Point(523, 358);
            this.btnShowMe.Name = "btnShowMe";
            this.btnShowMe.Size = new System.Drawing.Size(113, 30);
            this.btnShowMe.TabIndex = 25;
            this.btnShowMe.Text = "Show Me...";
            this.btnShowMe.UseVisualStyleBackColor = true;
            this.btnShowMe.Click += new System.EventHandler(this.ShowMeTheFile);
            // 
            // lblDuration
            // 
            this.lblDuration.AutoSize = true;
            this.lblDuration.Location = new System.Drawing.Point(356, 43);
            this.lblDuration.Name = "lblDuration";
            this.lblDuration.Size = new System.Drawing.Size(113, 18);
            this.lblDuration.TabIndex = 26;
            this.lblDuration.Text = "Duration: 0:00";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.radioMale);
            this.panel1.Controls.Add(this.radioFemale);
            this.panel1.Location = new System.Drawing.Point(12, 261);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(274, 31);
            this.panel1.TabIndex = 27;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.rdoAMS);
            this.panel2.Controls.Add(this.rdoFMS);
            this.panel2.Controls.Add(this.rdoHQM);
            this.panel2.Controls.Add(this.rdoHQS);
            this.panel2.Location = new System.Drawing.Point(128, 150);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(616, 58);
            this.panel2.TabIndex = 28;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(158, 28);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(101, 18);
            this.label8.TabIndex = 7;
            this.label8.Text = "4:40 / CHKN";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(422, 28);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(101, 18);
            this.label7.TabIndex = 6;
            this.label7.Text = "7:51 / CHKN";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(294, 28);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(101, 18);
            this.label6.TabIndex = 5;
            this.label6.Text = "6:26 / CHKN";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 28);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(101, 18);
            this.label5.TabIndex = 4;
            this.label5.Text = "3:36 / CHKN";
            // 
            // rdoAMS
            // 
            this.rdoAMS.AutoSize = true;
            this.rdoAMS.Location = new System.Drawing.Point(425, 4);
            this.rdoAMS.Name = "rdoAMS";
            this.rdoAMS.Size = new System.Drawing.Size(102, 22);
            this.rdoAMS.TabIndex = 3;
            this.rdoAMS.Text = "AM Stereo";
            this.rdoAMS.UseVisualStyleBackColor = true;
            // 
            // rdoFMS
            // 
            this.rdoFMS.AutoSize = true;
            this.rdoFMS.Checked = true;
            this.rdoFMS.Location = new System.Drawing.Point(297, 3);
            this.rdoFMS.Name = "rdoFMS";
            this.rdoFMS.Size = new System.Drawing.Size(100, 22);
            this.rdoFMS.TabIndex = 2;
            this.rdoFMS.TabStop = true;
            this.rdoFMS.Text = "FM Stereo";
            this.rdoFMS.UseVisualStyleBackColor = true;
            // 
            // rdoHQM
            // 
            this.rdoHQM.AutoSize = true;
            this.rdoHQM.Location = new System.Drawing.Point(161, 3);
            this.rdoHQM.Name = "rdoHQM";
            this.rdoHQM.Size = new System.Drawing.Size(96, 22);
            this.rdoHQM.TabIndex = 1;
            this.rdoHQM.Text = "HQ Mono";
            this.rdoHQM.UseVisualStyleBackColor = true;
            // 
            // rdoHQS
            // 
            this.rdoHQS.AutoSize = true;
            this.rdoHQS.Location = new System.Drawing.Point(11, 3);
            this.rdoHQS.Name = "rdoHQS";
            this.rdoHQS.Size = new System.Drawing.Size(102, 22);
            this.rdoHQS.TabIndex = 0;
            this.rdoHQS.Text = "HQ Stereo";
            this.rdoHQS.UseVisualStyleBackColor = true;
            // 
            // picWaveform
            // 
            this.picWaveform.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picWaveform.Location = new System.Drawing.Point(578, 49);
            this.picWaveform.Name = "picWaveform";
            this.picWaveform.Size = new System.Drawing.Size(200, 70);
            this.picWaveform.TabIndex = 29;
            this.picWaveform.TabStop = false;
            // 
            // btnIncreaseVolume
            // 
            this.btnIncreaseVolume.Location = new System.Drawing.Point(784, 46);
            this.btnIncreaseVolume.Name = "btnIncreaseVolume";
            this.btnIncreaseVolume.Size = new System.Drawing.Size(24, 26);
            this.btnIncreaseVolume.TabIndex = 30;
            this.btnIncreaseVolume.Text = "+";
            this.btnIncreaseVolume.UseVisualStyleBackColor = true;
            this.btnIncreaseVolume.Click += new System.EventHandler(this.IncreaseVolume);
            // 
            // btnDecreaseVolume
            // 
            this.btnDecreaseVolume.Location = new System.Drawing.Point(784, 96);
            this.btnDecreaseVolume.Name = "btnDecreaseVolume";
            this.btnDecreaseVolume.Size = new System.Drawing.Size(24, 26);
            this.btnDecreaseVolume.TabIndex = 31;
            this.btnDecreaseVolume.Text = "-";
            this.btnDecreaseVolume.UseVisualStyleBackColor = true;
            this.btnDecreaseVolume.Click += new System.EventHandler(this.DecreaseVolume);
            // 
            // btnResetVolume
            // 
            this.btnResetVolume.Location = new System.Drawing.Point(784, 71);
            this.btnResetVolume.Name = "btnResetVolume";
            this.btnResetVolume.Size = new System.Drawing.Size(24, 26);
            this.btnResetVolume.TabIndex = 32;
            this.btnResetVolume.Text = "·";
            this.btnResetVolume.UseVisualStyleBackColor = true;
            this.btnResetVolume.Click += new System.EventHandler(this.ResetVolume);
            // 
            // lblVolume
            // 
            this.lblVolume.AutoSize = true;
            this.lblVolume.Location = new System.Drawing.Point(627, 119);
            this.lblVolume.Name = "lblVolume";
            this.lblVolume.Size = new System.Drawing.Size(111, 18);
            this.lblVolume.TabIndex = 33;
            this.lblVolume.Text = "Volume: 100%";
            // 
            // chkCheap
            // 
            this.chkCheap.AutoSize = true;
            this.chkCheap.Location = new System.Drawing.Point(537, 297);
            this.chkCheap.Name = "chkCheap";
            this.chkCheap.Size = new System.Drawing.Size(74, 22);
            this.chkCheap.TabIndex = 34;
            this.chkCheap.Text = "Cheap";
            this.chkCheap.UseVisualStyleBackColor = true;
            // 
            // AudioSplicerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(810, 445);
            this.Controls.Add(this.chkCheap);
            this.Controls.Add(this.lblVolume);
            this.Controls.Add(this.btnResetVolume);
            this.Controls.Add(this.btnDecreaseVolume);
            this.Controls.Add(this.btnIncreaseVolume);
            this.Controls.Add(this.picWaveform);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lblDuration);
            this.Controls.Add(this.btnShowMe);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.lblAction);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.checkOGGFiles);
            this.Controls.Add(this.checkIcons);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.txtPrefix);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cboAudioLength);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnSelectFile);
            this.Controls.Add(this.txtFilename);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Lucida Sans Unicode", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.Name = "AudioSplicerForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Triggerless Audio Splicer";
            this.Load += new System.EventHandler(this.AudioSplicerForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picWaveform)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.OpenFileDialog dlgOpenFile;
        private System.Windows.Forms.TextBox txtFilename;
        private System.Windows.Forms.Button btnSelectFile;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cboAudioLength;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtPrefix;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.RadioButton radioFemale;
        private System.Windows.Forms.RadioButton radioMale;
        private System.Windows.Forms.CheckBox checkIcons;
        private System.Windows.Forms.CheckBox checkOGGFiles;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label lblAction;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Button btnShowMe;
        private AudioSegmenter _audioSegmenter;
        private System.Windows.Forms.Label lblDuration;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RadioButton rdoHQS;
        private System.Windows.Forms.RadioButton rdoAMS;
        private System.Windows.Forms.RadioButton rdoFMS;
        private System.Windows.Forms.RadioButton rdoHQM;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.PictureBox picWaveform;
        private System.Windows.Forms.Button btnIncreaseVolume;
        private System.Windows.Forms.Button btnDecreaseVolume;
        private System.Windows.Forms.Button btnResetVolume;
        private System.Windows.Forms.Label lblVolume;
        private System.Windows.Forms.CheckBox chkCheap;
    }
}