namespace Triggerless.TriggerBot
{
    partial class SplicerControl
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblVolume = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.radioMale = new System.Windows.Forms.RadioButton();
            this.radioFemale = new System.Windows.Forms.RadioButton();
            this.lblDuration = new System.Windows.Forms.Label();
            this.btnShowMe = new System.Windows.Forms.Button();
            this.picWaveform = new System.Windows.Forms.PictureBox();
            this.btnResetVolume = new System.Windows.Forms.Button();
            this.btnDecreaseVolume = new System.Windows.Forms.Button();
            this.btnIncreaseVolume = new System.Windows.Forms.Button();
            this.chkCheap = new System.Windows.Forms.CheckBox();
            this.label15 = new System.Windows.Forms.Label();
            this.lblAction = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.btnStart = new System.Windows.Forms.Button();
            this.checkOGGFiles = new System.Windows.Forms.CheckBox();
            this.checkIcons = new System.Windows.Forms.CheckBox();
            this.lblProfit = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.txtPrefix = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.cboAudioLength = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnSelectFile = new System.Windows.Forms.Button();
            this.txtFilename = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dlgOpenFile = new System.Windows.Forms.OpenFileDialog();
            this._audioSegmenter = new Triggerless.TriggerBot.AudioSegmenter();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label9 = new System.Windows.Forms.Label();
            this.btnDebug = new System.Windows.Forms.Button();
            this.rdoMinima = new System.Windows.Forms.RadioButton();
            this.rdoFixed = new System.Windows.Forms.RadioButton();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.rdoHQS = new System.Windows.Forms.RadioButton();
            this.label8 = new System.Windows.Forms.Label();
            this.rdoHQM = new System.Windows.Forms.RadioButton();
            this.label6 = new System.Windows.Forms.Label();
            this.rdoFMS = new System.Windows.Forms.RadioButton();
            this.label7 = new System.Windows.Forms.Label();
            this.rdoAMS = new System.Windows.Forms.RadioButton();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picWaveform)).BeginInit();
            this.panel4.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tableLayoutPanel1);
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Controls.Add(this.btnDebug);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.lblVolume);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.lblDuration);
            this.panel1.Controls.Add(this.btnShowMe);
            this.panel1.Controls.Add(this.picWaveform);
            this.panel1.Controls.Add(this.btnResetVolume);
            this.panel1.Controls.Add(this.btnDecreaseVolume);
            this.panel1.Controls.Add(this.btnIncreaseVolume);
            this.panel1.Controls.Add(this.chkCheap);
            this.panel1.Controls.Add(this.label15);
            this.panel1.Controls.Add(this.lblAction);
            this.panel1.Controls.Add(this.label12);
            this.panel1.Controls.Add(this.btnStart);
            this.panel1.Controls.Add(this.checkOGGFiles);
            this.panel1.Controls.Add(this.checkIcons);
            this.panel1.Controls.Add(this.lblProfit);
            this.panel1.Controls.Add(this.label11);
            this.panel1.Controls.Add(this.txtPrefix);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.cboAudioLength);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.btnSelectFile);
            this.panel1.Controls.Add(this.txtFilename);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Font = new System.Drawing.Font("Liberation Sans", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel1.Location = new System.Drawing.Point(4, 4);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(851, 476);
            this.panel1.TabIndex = 0;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // lblVolume
            // 
            this.lblVolume.AutoSize = true;
            this.lblVolume.Font = new System.Drawing.Font("Liberation Sans", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVolume.Location = new System.Drawing.Point(653, 127);
            this.lblVolume.Name = "lblVolume";
            this.lblVolume.Size = new System.Drawing.Size(101, 17);
            this.lblVolume.TabIndex = 60;
            this.lblVolume.Text = "Volume: 100%";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.radioMale);
            this.panel3.Controls.Add(this.radioFemale);
            this.panel3.Location = new System.Drawing.Point(38, 269);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(274, 31);
            this.panel3.TabIndex = 54;
            // 
            // radioMale
            // 
            this.radioMale.AutoSize = true;
            this.radioMale.Font = new System.Drawing.Font("Liberation Sans", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioMale.Location = new System.Drawing.Point(126, 6);
            this.radioMale.Name = "radioMale";
            this.radioMale.Size = new System.Drawing.Size(128, 21);
            this.radioMale.TabIndex = 17;
            this.radioMale.Text = "Male Accessory";
            this.radioMale.UseVisualStyleBackColor = true;
            // 
            // radioFemale
            // 
            this.radioFemale.AutoSize = true;
            this.radioFemale.Checked = true;
            this.radioFemale.Font = new System.Drawing.Font("Liberation Sans", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioFemale.Location = new System.Drawing.Point(12, 6);
            this.radioFemale.Name = "radioFemale";
            this.radioFemale.Size = new System.Drawing.Size(96, 21);
            this.radioFemale.TabIndex = 16;
            this.radioFemale.TabStop = true;
            this.radioFemale.Text = "Female  or";
            this.radioFemale.UseVisualStyleBackColor = true;
            // 
            // lblDuration
            // 
            this.lblDuration.AutoSize = true;
            this.lblDuration.Font = new System.Drawing.Font("Liberation Sans", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDuration.Location = new System.Drawing.Point(382, 48);
            this.lblDuration.Name = "lblDuration";
            this.lblDuration.Size = new System.Drawing.Size(99, 17);
            this.lblDuration.TabIndex = 53;
            this.lblDuration.Text = "Duration: 0:00";
            // 
            // btnShowMe
            // 
            this.btnShowMe.Font = new System.Drawing.Font("Liberation Sans", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnShowMe.Location = new System.Drawing.Point(549, 366);
            this.btnShowMe.Name = "btnShowMe";
            this.btnShowMe.Size = new System.Drawing.Size(113, 30);
            this.btnShowMe.TabIndex = 52;
            this.btnShowMe.Text = "Show Me...";
            this.btnShowMe.UseVisualStyleBackColor = true;
            this.btnShowMe.Click += new System.EventHandler(this.ShowMeTheFile);
            // 
            // picWaveform
            // 
            this.picWaveform.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picWaveform.Location = new System.Drawing.Point(604, 57);
            this.picWaveform.Name = "picWaveform";
            this.picWaveform.Size = new System.Drawing.Size(200, 70);
            this.picWaveform.TabIndex = 56;
            this.picWaveform.TabStop = false;
            // 
            // btnResetVolume
            // 
            this.btnResetVolume.Location = new System.Drawing.Point(810, 79);
            this.btnResetVolume.Name = "btnResetVolume";
            this.btnResetVolume.Size = new System.Drawing.Size(24, 26);
            this.btnResetVolume.TabIndex = 59;
            this.btnResetVolume.Text = "·";
            this.btnResetVolume.UseVisualStyleBackColor = true;
            this.btnResetVolume.Click += new System.EventHandler(this.ResetVolume);
            // 
            // btnDecreaseVolume
            // 
            this.btnDecreaseVolume.Location = new System.Drawing.Point(810, 104);
            this.btnDecreaseVolume.Name = "btnDecreaseVolume";
            this.btnDecreaseVolume.Size = new System.Drawing.Size(24, 26);
            this.btnDecreaseVolume.TabIndex = 58;
            this.btnDecreaseVolume.Text = "-";
            this.btnDecreaseVolume.UseVisualStyleBackColor = true;
            this.btnDecreaseVolume.Click += new System.EventHandler(this.DecreaseVolume);
            // 
            // btnIncreaseVolume
            // 
            this.btnIncreaseVolume.Location = new System.Drawing.Point(810, 54);
            this.btnIncreaseVolume.Name = "btnIncreaseVolume";
            this.btnIncreaseVolume.Size = new System.Drawing.Size(24, 26);
            this.btnIncreaseVolume.TabIndex = 57;
            this.btnIncreaseVolume.Text = "+";
            this.btnIncreaseVolume.UseVisualStyleBackColor = true;
            this.btnIncreaseVolume.Click += new System.EventHandler(this.IncreaseVolume);
            // 
            // chkCheap
            // 
            this.chkCheap.AutoSize = true;
            this.chkCheap.Font = new System.Drawing.Font("Liberation Sans", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkCheap.Location = new System.Drawing.Point(563, 305);
            this.chkCheap.Name = "chkCheap";
            this.chkCheap.Size = new System.Drawing.Size(70, 21);
            this.chkCheap.TabIndex = 61;
            this.chkCheap.Text = "Cheap";
            this.chkCheap.UseVisualStyleBackColor = true;
            this.chkCheap.CheckedChanged += new System.EventHandler(this.chkCheap_CheckedChanged);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Liberation Sans", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(28, 372);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(428, 17);
            this.label15.TabIndex = 51;
            this.label15.Text = "Your CHKN file will always be in the Documents\\Triggerbot folder.";
            // 
            // lblAction
            // 
            this.lblAction.AutoSize = true;
            this.lblAction.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblAction.Font = new System.Drawing.Font("Liberation Sans", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAction.Location = new System.Drawing.Point(244, 324);
            this.lblAction.Name = "lblAction";
            this.lblAction.Size = new System.Drawing.Size(32, 19);
            this.lblAction.TabIndex = 49;
            this.lblAction.Text = "Idle";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Liberation Sans", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(178, 324);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(52, 17);
            this.label12.TabIndex = 48;
            this.label12.Text = "Action:";
            // 
            // btnStart
            // 
            this.btnStart.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnStart.Font = new System.Drawing.Font("Liberation Sans", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStart.ForeColor = System.Drawing.Color.White;
            this.btnStart.Location = new System.Drawing.Point(38, 315);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(134, 37);
            this.btnStart.TabIndex = 47;
            this.btnStart.Text = "Let\'s Start!";
            this.btnStart.UseVisualStyleBackColor = false;
            this.btnStart.Click += new System.EventHandler(this.StartConversion);
            // 
            // checkOGGFiles
            // 
            this.checkOGGFiles.AutoSize = true;
            this.checkOGGFiles.Checked = true;
            this.checkOGGFiles.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkOGGFiles.Font = new System.Drawing.Font("Liberation Sans", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkOGGFiles.Location = new System.Drawing.Point(563, 277);
            this.checkOGGFiles.Name = "checkOGGFiles";
            this.checkOGGFiles.Size = new System.Drawing.Size(158, 21);
            this.checkOGGFiles.TabIndex = 46;
            this.checkOGGFiles.Text = "Clean up OGG Files";
            this.checkOGGFiles.UseVisualStyleBackColor = true;
            // 
            // checkIcons
            // 
            this.checkIcons.AutoSize = true;
            this.checkIcons.Checked = true;
            this.checkIcons.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkIcons.Font = new System.Drawing.Font("Liberation Sans", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkIcons.Location = new System.Drawing.Point(336, 277);
            this.checkIcons.Name = "checkIcons";
            this.checkIcons.Size = new System.Drawing.Size(177, 21);
            this.checkIcons.TabIndex = 45;
            this.checkIcons.Text = "Generate 100x80 icons";
            this.checkIcons.UseVisualStyleBackColor = true;
            // 
            // lblProfit
            // 
            this.lblProfit.AutoSize = true;
            this.lblProfit.Font = new System.Drawing.Font("Liberation Sans", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProfit.Location = new System.Drawing.Point(28, 426);
            this.lblProfit.Name = "lblProfit";
            this.lblProfit.Size = new System.Drawing.Size(607, 17);
            this.lblProfit.TabIndex = 50;
            this.lblProfit.Text = "Please note that @Triggers will make 220 cr profit for every CHKN you submit usin" +
    "g this tool.";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Liberation Sans", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(271, 235);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(449, 17);
            this.label11.TabIndex = 44;
            this.label11.Text = "Only letters are allowed. No commas, spaces or special characters.";
            // 
            // txtPrefix
            // 
            this.txtPrefix.Location = new System.Drawing.Point(156, 232);
            this.txtPrefix.Name = "txtPrefix";
            this.txtPrefix.Size = new System.Drawing.Size(100, 25);
            this.txtPrefix.TabIndex = 43;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Liberation Sans", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(31, 235);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(98, 17);
            this.label10.TabIndex = 42;
            this.label10.Text = "Trigger Prefix:";
            // 
            // cboAudioLength
            // 
            this.cboAudioLength.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboAudioLength.Font = new System.Drawing.Font("Liberation Sans", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboAudioLength.FormattingEnabled = true;
            this.cboAudioLength.Items.AddRange(new object[] {
            "20.0",
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
            this.cboAudioLength.Location = new System.Drawing.Point(442, 119);
            this.cboAudioLength.Name = "cboAudioLength";
            this.cboAudioLength.Size = new System.Drawing.Size(115, 25);
            this.cboAudioLength.TabIndex = 40;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Liberation Sans", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(259, 122);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(162, 17);
            this.label3.TabIndex = 39;
            this.label3.Text = "Fixed OGG length (sec)";
            // 
            // btnSelectFile
            // 
            this.btnSelectFile.Font = new System.Drawing.Font("Liberation Sans", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSelectFile.Location = new System.Drawing.Point(513, 72);
            this.btnSelectFile.Name = "btnSelectFile";
            this.btnSelectFile.Size = new System.Drawing.Size(75, 30);
            this.btnSelectFile.TabIndex = 38;
            this.btnSelectFile.Text = "Select...";
            this.btnSelectFile.UseVisualStyleBackColor = true;
            this.btnSelectFile.Click += new System.EventHandler(this.SelectFile);
            // 
            // txtFilename
            // 
            this.txtFilename.Enabled = false;
            this.txtFilename.Font = new System.Drawing.Font("Liberation Sans", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFilename.Location = new System.Drawing.Point(34, 72);
            this.txtFilename.Name = "txtFilename";
            this.txtFilename.Size = new System.Drawing.Size(472, 25);
            this.txtFilename.TabIndex = 37;
            this.txtFilename.TextChanged += new System.EventHandler(this.txtFilename_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Liberation Sans", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(28, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(212, 17);
            this.label2.TabIndex = 36;
            this.label2.Text = "Choose an audio file to convert:";
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Liberation Sans", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(28, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(786, 49);
            this.label1.TabIndex = 35;
            this.label1.Text = "This Audio Splicer will convert an MP3 file into CHKN file(s) you can upload to I" +
    "MVU.";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // dlgOpenFile
            // 
            this.dlgOpenFile.Filter = "MP3 files|*.mp3|All Files|*.*";
            // 
            // timer1
            // 
            this.timer1.Interval = 200;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Liberation Sans", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(28, 446);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(259, 17);
            this.label9.TabIndex = 62;
            this.label9.Text = "♥ Thank you for supporting our work! ♥";
            // 
            // btnDebug
            // 
            this.btnDebug.Font = new System.Drawing.Font("Liberation Sans", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDebug.Location = new System.Drawing.Point(691, 366);
            this.btnDebug.Name = "btnDebug";
            this.btnDebug.Size = new System.Drawing.Size(113, 30);
            this.btnDebug.TabIndex = 63;
            this.btnDebug.Text = "Debug Run...";
            this.btnDebug.UseVisualStyleBackColor = true;
            this.btnDebug.Click += new System.EventHandler(this.DebugRun);
            // 
            // rdoMinima
            // 
            this.rdoMinima.AutoSize = true;
            this.rdoMinima.Checked = true;
            this.rdoMinima.Font = new System.Drawing.Font("Liberation Sans", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdoMinima.Location = new System.Drawing.Point(17, 4);
            this.rdoMinima.Name = "rdoMinima";
            this.rdoMinima.Size = new System.Drawing.Size(93, 21);
            this.rdoMinima.TabIndex = 0;
            this.rdoMinima.TabStop = true;
            this.rdoMinima.Text = "Smart Cut";
            this.rdoMinima.UseVisualStyleBackColor = true;
            this.rdoMinima.CheckedChanged += new System.EventHandler(this.rdoMinima_CheckedChanged);
            // 
            // rdoFixed
            // 
            this.rdoFixed.AutoSize = true;
            this.rdoFixed.Font = new System.Drawing.Font("Liberation Sans", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdoFixed.Location = new System.Drawing.Point(130, 5);
            this.rdoFixed.Name = "rdoFixed";
            this.rdoFixed.Size = new System.Drawing.Size(88, 21);
            this.rdoFixed.TabIndex = 1;
            this.rdoFixed.Text = "Fixed Cut";
            this.rdoFixed.UseVisualStyleBackColor = true;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.rdoFixed);
            this.panel4.Controls.Add(this.rdoMinima);
            this.panel4.Location = new System.Drawing.Point(17, 116);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(238, 36);
            this.panel4.TabIndex = 64;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(97, 17);
            this.label4.TabIndex = 41;
            this.label4.Text = "Audio Quality:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(193, 27);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(89, 17);
            this.label5.TabIndex = 4;
            this.label5.Text = "3:36 / CHKN";
            // 
            // rdoHQS
            // 
            this.rdoHQS.AutoSize = true;
            this.rdoHQS.Location = new System.Drawing.Point(193, 3);
            this.rdoHQS.Name = "rdoHQS";
            this.rdoHQS.Size = new System.Drawing.Size(95, 21);
            this.rdoHQS.TabIndex = 0;
            this.rdoHQS.Text = "HQ Stereo";
            this.rdoHQS.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(315, 27);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(89, 17);
            this.label8.TabIndex = 7;
            this.label8.Text = "4:40 / CHKN";
            // 
            // rdoHQM
            // 
            this.rdoHQM.AutoSize = true;
            this.rdoHQM.Location = new System.Drawing.Point(315, 3);
            this.rdoHQM.Name = "rdoHQM";
            this.rdoHQM.Size = new System.Drawing.Size(87, 21);
            this.rdoHQM.TabIndex = 1;
            this.rdoHQM.Text = "HQ Mono";
            this.rdoHQM.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(437, 27);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(89, 17);
            this.label6.TabIndex = 5;
            this.label6.Text = "6:26 / CHKN";
            // 
            // rdoFMS
            // 
            this.rdoFMS.AutoSize = true;
            this.rdoFMS.Checked = true;
            this.rdoFMS.Location = new System.Drawing.Point(437, 3);
            this.rdoFMS.Name = "rdoFMS";
            this.rdoFMS.Size = new System.Drawing.Size(93, 21);
            this.rdoFMS.TabIndex = 2;
            this.rdoFMS.TabStop = true;
            this.rdoFMS.Text = "FM Stereo";
            this.rdoFMS.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(559, 27);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(89, 17);
            this.label7.TabIndex = 6;
            this.label7.Text = "7:51 / CHKN";
            // 
            // rdoAMS
            // 
            this.rdoAMS.AutoSize = true;
            this.rdoAMS.Location = new System.Drawing.Point(559, 3);
            this.rdoAMS.Name = "rdoAMS";
            this.rdoAMS.Size = new System.Drawing.Size(93, 21);
            this.rdoAMS.TabIndex = 3;
            this.rdoAMS.Text = "AM Stereo";
            this.rdoAMS.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 5;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 28F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18F));
            this.tableLayoutPanel1.Controls.Add(this.rdoAMS, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.label7, 4, 1);
            this.tableLayoutPanel1.Controls.Add(this.rdoFMS, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.label6, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.rdoHQM, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.label8, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.rdoHQS, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label5, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 0);
            this.tableLayoutPanel1.Font = new System.Drawing.Font("Liberation Sans", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tableLayoutPanel1.Location = new System.Drawing.Point(29, 161);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(679, 54);
            this.tableLayoutPanel1.TabIndex = 65;
            // 
            // SplicerControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Lucida Sans Unicode", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "SplicerControl";
            this.Size = new System.Drawing.Size(861, 485);
            this.Load += new System.EventHandler(this.SplicerControl_Load);
            this.ControlRemoved += new System.Windows.Forms.ControlEventHandler(this.SplicerControl_ControlRemoved);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picWaveform)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblVolume;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.RadioButton radioMale;
        private System.Windows.Forms.RadioButton radioFemale;
        private System.Windows.Forms.Label lblDuration;
        private System.Windows.Forms.Button btnShowMe;
        private System.Windows.Forms.PictureBox picWaveform;
        private System.Windows.Forms.Button btnResetVolume;
        private System.Windows.Forms.Button btnDecreaseVolume;
        private System.Windows.Forms.Button btnIncreaseVolume;
        private System.Windows.Forms.CheckBox chkCheap;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label lblAction;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.CheckBox checkOGGFiles;
        private System.Windows.Forms.CheckBox checkIcons;
        private System.Windows.Forms.Label lblProfit;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtPrefix;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cboAudioLength;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnSelectFile;
        private System.Windows.Forms.TextBox txtFilename;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private AudioSegmenter _audioSegmenter;
        private System.Windows.Forms.OpenFileDialog dlgOpenFile;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.RadioButton rdoAMS;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.RadioButton rdoFMS;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.RadioButton rdoHQM;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.RadioButton rdoHQS;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.RadioButton rdoFixed;
        private System.Windows.Forms.RadioButton rdoMinima;
        private System.Windows.Forms.Button btnDebug;
        private System.Windows.Forms.Label label9;
    }
}
