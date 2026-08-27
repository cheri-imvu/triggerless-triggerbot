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


        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.pnlContent = new System.Windows.Forms.Panel();
            this.label10 = new System.Windows.Forms.Label();
            this.dlgOpenFile = new System.Windows.Forms.OpenFileDialog();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.roundedPanel2 = new RoundedPanel();
            this.label8 = new System.Windows.Forms.Label();
            this.btnStart = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.lblProfit = new System.Windows.Forms.Label();
            this.btnShowMe = new System.Windows.Forms.Button();
            this.label15 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.lblCutStageIdle = new System.Windows.Forms.Label();
            this.roundedPanel1 = new RoundedPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.checkIcons = new System.Windows.Forms.CheckBox();
            this.checkOGGFiles = new System.Windows.Forms.CheckBox();
            this.chkCheap = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.radioMale = new System.Windows.Forms.RadioButton();
            this.radioFemale = new System.Windows.Forms.RadioButton();
            this.pnlTriggerPrefix = new RoundedPanel();
            this.label5 = new System.Windows.Forms.Label();
            this.txtPrefix = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.pnlAdjustVolume = new RoundedPanel();
            this.label7 = new System.Windows.Forms.Label();
            this.picWaveform = new System.Windows.Forms.PictureBox();
            this.btnIncreaseVolume = new System.Windows.Forms.Button();
            this.btnDecreaseVolume = new System.Windows.Forms.Button();
            this.btnResetVolume = new System.Windows.Forms.Button();
            this.lblVolume = new System.Windows.Forms.Label();
            this.pnlCutMethod = new RoundedPanel();
            this.label4 = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.rdoFixed = new System.Windows.Forms.RadioButton();
            this.rdoCustom = new System.Windows.Forms.RadioButton();
            this.rdoMinima = new System.Windows.Forms.RadioButton();
            this.cboAudioLength = new System.Windows.Forms.ComboBox();
            this.lblFixed = new System.Windows.Forms.Label();
            this.btnCustom = new System.Windows.Forms.Button();
            this.picHelp = new System.Windows.Forms.PictureBox();
            this.pnlSoundQuality = new RoundedPanel();
            this.label3 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.rdoHQS = new System.Windows.Forms.RadioButton();
            this.rdoHQM = new System.Windows.Forms.RadioButton();
            this.rdoFMS = new System.Windows.Forms.RadioButton();
            this.c = new RoundedPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSelectFile = new System.Windows.Forms.Button();
            this.rpnlTop = new RoundedPanel();
            this.lblSelectedFile = new System.Windows.Forms.Label();
            this.lblChooseFile = new System.Windows.Forms.Label();
            this.lblDuration = new System.Windows.Forms.Label();
            this._audioSegmenter = new Triggerless.TriggerBot.AudioSegmenter();
            this.pnlContent.SuspendLayout();
            this.roundedPanel2.SuspendLayout();
            this.roundedPanel1.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.panel3.SuspendLayout();
            this.pnlTriggerPrefix.SuspendLayout();
            this.pnlAdjustVolume.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picWaveform)).BeginInit();
            this.pnlCutMethod.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picHelp)).BeginInit();
            this.pnlSoundQuality.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.c.SuspendLayout();
            this.rpnlTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlContent
            // 
            this.pnlContent.Controls.Add(this.roundedPanel2);
            this.pnlContent.Controls.Add(this.roundedPanel1);
            this.pnlContent.Controls.Add(this.pnlTriggerPrefix);
            this.pnlContent.Controls.Add(this.pnlAdjustVolume);
            this.pnlContent.Controls.Add(this.pnlCutMethod);
            this.pnlContent.Controls.Add(this.pnlSoundQuality);
            this.pnlContent.Controls.Add(this.c);
            this.pnlContent.Controls.Add(this.rpnlTop);
            this.pnlContent.Controls.Add(this.label10);
            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContent.Font = new System.Drawing.Font("Liberation Sans", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlContent.Location = new System.Drawing.Point(0, 0);
            this.pnlContent.Margin = new System.Windows.Forms.Padding(4);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Size = new System.Drawing.Size(924, 516);
            this.pnlContent.TabIndex = 0;
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
            // dlgOpenFile
            // 
            this.dlgOpenFile.Filter = "All supported audio files|*.mp3;*.wav;*.wma;*.aac;*.m4a;*.mp4;*.asf;*.3gp;*.flac;" +
    "*.ogg|MP3 Audio|*.mp3|FLAC Audio|*.flac";
            // 
            // timer1
            // 
            this.timer1.Interval = 200;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // roundedPanel2
            // 
            this.roundedPanel2.BorderColor = System.Drawing.Color.DodgerBlue;
            this.roundedPanel2.BorderRadius = 25;
            this.roundedPanel2.BorderWidth = 3;
            this.roundedPanel2.Controls.Add(this.label8);
            this.roundedPanel2.Controls.Add(this.btnStart);
            this.roundedPanel2.Controls.Add(this.label12);
            this.roundedPanel2.Controls.Add(this.lblProfit);
            this.roundedPanel2.Controls.Add(this.btnShowMe);
            this.roundedPanel2.Controls.Add(this.label15);
            this.roundedPanel2.Controls.Add(this.label9);
            this.roundedPanel2.Controls.Add(this.lblCutStageIdle);
            this.roundedPanel2.Location = new System.Drawing.Point(613, 48);
            this.roundedPanel2.Name = "roundedPanel2";
            this.roundedPanel2.Padding = new System.Windows.Forms.Padding(10);
            this.roundedPanel2.Size = new System.Drawing.Size(303, 456);
            this.roundedPanel2.TabIndex = 75;
            // 
            // label8
            // 
            this.label8.Dock = System.Windows.Forms.DockStyle.Top;
            this.label8.Font = new System.Drawing.Font("Liberation Sans", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.DodgerBlue;
            this.label8.Location = new System.Drawing.Point(10, 10);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(283, 25);
            this.label8.TabIndex = 0;
            this.label8.Text = "7. Create CHKN";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnStart
            // 
            this.btnStart.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnStart.Font = new System.Drawing.Font("Liberation Sans", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStart.ForeColor = System.Drawing.Color.White;
            this.btnStart.Location = new System.Drawing.Point(83, 43);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(134, 37);
            this.btnStart.TabIndex = 47;
            this.btnStart.Text = "Let\'s Start!";
            this.btnStart.UseVisualStyleBackColor = false;
            this.btnStart.Click += new System.EventHandler(this.StartConversion);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Liberation Sans", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(13, 100);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(52, 17);
            this.label12.TabIndex = 48;
            this.label12.Text = "Action:";
            // 
            // lblProfit
            // 
            this.lblProfit.Font = new System.Drawing.Font("Liberation Sans", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProfit.Location = new System.Drawing.Point(22, 257);
            this.lblProfit.Name = "lblProfit";
            this.lblProfit.Size = new System.Drawing.Size(244, 60);
            this.lblProfit.TabIndex = 50;
            this.lblProfit.Text = "Please note that @Triggers will make 250 cr profit for every CHKN you submit usin" +
    "g this tool.";
            // 
            // btnShowMe
            // 
            this.btnShowMe.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnShowMe.Font = new System.Drawing.Font("Liberation Sans", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnShowMe.ForeColor = System.Drawing.Color.Black;
            this.btnShowMe.Location = new System.Drawing.Point(93, 129);
            this.btnShowMe.Name = "btnShowMe";
            this.btnShowMe.Size = new System.Drawing.Size(113, 30);
            this.btnShowMe.TabIndex = 52;
            this.btnShowMe.Text = "Show Me...";
            this.btnShowMe.UseVisualStyleBackColor = false;
            this.btnShowMe.Click += new System.EventHandler(this.ShowMeTheFile);
            // 
            // label15
            // 
            this.label15.Font = new System.Drawing.Font("Liberation Sans", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(22, 207);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(250, 40);
            this.label15.TabIndex = 51;
            this.label15.Text = "Your CHKN file will always be in the Documents\\Triggerbot folder.";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Liberation Sans", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(22, 317);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(259, 17);
            this.label9.TabIndex = 62;
            this.label9.Text = "♥ Thank you for supporting our work! ♥";
            // 
            // lblCutStageIdle
            // 
            this.lblCutStageIdle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblCutStageIdle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblCutStageIdle.Font = new System.Drawing.Font("Liberation Sans", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCutStageIdle.ForeColor = System.Drawing.Color.Black;
            this.lblCutStageIdle.Location = new System.Drawing.Point(83, 94);
            this.lblCutStageIdle.Name = "lblCutStageIdle";
            this.lblCutStageIdle.Padding = new System.Windows.Forms.Padding(5);
            this.lblCutStageIdle.Size = new System.Drawing.Size(198, 26);
            this.lblCutStageIdle.TabIndex = 49;
            this.lblCutStageIdle.Text = "Idle";
            // 
            // roundedPanel1
            // 
            this.roundedPanel1.BorderColor = System.Drawing.Color.DodgerBlue;
            this.roundedPanel1.BorderRadius = 25;
            this.roundedPanel1.BorderWidth = 3;
            this.roundedPanel1.Controls.Add(this.tableLayoutPanel3);
            this.roundedPanel1.Controls.Add(this.label6);
            this.roundedPanel1.Controls.Add(this.panel3);
            this.roundedPanel1.Location = new System.Drawing.Point(295, 352);
            this.roundedPanel1.Name = "roundedPanel1";
            this.roundedPanel1.Padding = new System.Windows.Forms.Padding(10);
            this.roundedPanel1.Size = new System.Drawing.Size(313, 152);
            this.roundedPanel1.TabIndex = 74;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.Controls.Add(this.checkIcons, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.checkOGGFiles, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.chkCheap, 0, 2);
            this.tableLayoutPanel3.Font = new System.Drawing.Font("Liberation Sans", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tableLayoutPanel3.Location = new System.Drawing.Point(114, 38);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 3;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 53.06123F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 46.93877F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(183, 92);
            this.tableLayoutPanel3.TabIndex = 66;
            // 
            // checkIcons
            // 
            this.checkIcons.AutoSize = true;
            this.checkIcons.Checked = true;
            this.checkIcons.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkIcons.Font = new System.Drawing.Font("Liberation Sans", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkIcons.Location = new System.Drawing.Point(3, 3);
            this.checkIcons.Name = "checkIcons";
            this.checkIcons.Size = new System.Drawing.Size(177, 21);
            this.checkIcons.TabIndex = 45;
            this.checkIcons.Text = "Generate 100x80 icons";
            this.checkIcons.UseVisualStyleBackColor = true;
            // 
            // checkOGGFiles
            // 
            this.checkOGGFiles.AutoSize = true;
            this.checkOGGFiles.Checked = true;
            this.checkOGGFiles.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkOGGFiles.Font = new System.Drawing.Font("Liberation Sans", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkOGGFiles.Location = new System.Drawing.Point(3, 34);
            this.checkOGGFiles.Name = "checkOGGFiles";
            this.checkOGGFiles.Size = new System.Drawing.Size(158, 21);
            this.checkOGGFiles.TabIndex = 46;
            this.checkOGGFiles.Text = "Clean up OGG Files";
            this.checkOGGFiles.UseVisualStyleBackColor = true;
            // 
            // chkCheap
            // 
            this.chkCheap.AutoSize = true;
            this.chkCheap.Font = new System.Drawing.Font("Liberation Sans", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkCheap.Location = new System.Drawing.Point(3, 62);
            this.chkCheap.Name = "chkCheap";
            this.chkCheap.Size = new System.Drawing.Size(70, 21);
            this.chkCheap.TabIndex = 61;
            this.chkCheap.Text = "Cheap";
            this.chkCheap.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.Dock = System.Windows.Forms.DockStyle.Top;
            this.label6.Font = new System.Drawing.Font("Liberation Sans", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.DodgerBlue;
            this.label6.Location = new System.Drawing.Point(10, 10);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(293, 25);
            this.label6.TabIndex = 0;
            this.label6.Text = "6. Options";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.radioMale);
            this.panel3.Controls.Add(this.radioFemale);
            this.panel3.Location = new System.Drawing.Point(13, 37);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(95, 93);
            this.panel3.TabIndex = 54;
            // 
            // radioMale
            // 
            this.radioMale.AutoSize = true;
            this.radioMale.Font = new System.Drawing.Font("Liberation Sans", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioMale.Location = new System.Drawing.Point(12, 35);
            this.radioMale.Name = "radioMale";
            this.radioMale.Size = new System.Drawing.Size(56, 21);
            this.radioMale.TabIndex = 17;
            this.radioMale.Text = "Male";
            this.radioMale.UseVisualStyleBackColor = true;
            // 
            // radioFemale
            // 
            this.radioFemale.AutoSize = true;
            this.radioFemale.Checked = true;
            this.radioFemale.Font = new System.Drawing.Font("Liberation Sans", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioFemale.Location = new System.Drawing.Point(12, 2);
            this.radioFemale.Name = "radioFemale";
            this.radioFemale.Size = new System.Drawing.Size(75, 21);
            this.radioFemale.TabIndex = 16;
            this.radioFemale.TabStop = true;
            this.radioFemale.Text = "Female";
            this.radioFemale.UseVisualStyleBackColor = true;
            // 
            // pnlTriggerPrefix
            // 
            this.pnlTriggerPrefix.BorderColor = System.Drawing.Color.DodgerBlue;
            this.pnlTriggerPrefix.BorderRadius = 25;
            this.pnlTriggerPrefix.BorderWidth = 3;
            this.pnlTriggerPrefix.Controls.Add(this.label5);
            this.pnlTriggerPrefix.Controls.Add(this.txtPrefix);
            this.pnlTriggerPrefix.Controls.Add(this.label11);
            this.pnlTriggerPrefix.Location = new System.Drawing.Point(293, 225);
            this.pnlTriggerPrefix.Name = "pnlTriggerPrefix";
            this.pnlTriggerPrefix.Padding = new System.Windows.Forms.Padding(10);
            this.pnlTriggerPrefix.Size = new System.Drawing.Size(315, 122);
            this.pnlTriggerPrefix.TabIndex = 73;
            // 
            // label5
            // 
            this.label5.Dock = System.Windows.Forms.DockStyle.Top;
            this.label5.Font = new System.Drawing.Font("Liberation Sans", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.DodgerBlue;
            this.label5.Location = new System.Drawing.Point(10, 10);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(295, 25);
            this.label5.TabIndex = 0;
            this.label5.Text = "5. Trigger Prefix";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtPrefix
            // 
            this.txtPrefix.Font = new System.Drawing.Font("Liberation Sans", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPrefix.Location = new System.Drawing.Point(106, 45);
            this.txtPrefix.Name = "txtPrefix";
            this.txtPrefix.Size = new System.Drawing.Size(100, 25);
            this.txtPrefix.TabIndex = 43;
            this.txtPrefix.TextChanged += new System.EventHandler(this.txtPrefix_TextChanged);
            // 
            // label11
            // 
            this.label11.Font = new System.Drawing.Font("Liberation Sans", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(25, 73);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(265, 39);
            this.label11.TabIndex = 44;
            this.label11.Text = "Only letters are allowed. No commas, spaces or special characters.";
            // 
            // pnlAdjustVolume
            // 
            this.pnlAdjustVolume.BorderColor = System.Drawing.Color.DodgerBlue;
            this.pnlAdjustVolume.BorderRadius = 25;
            this.pnlAdjustVolume.BorderWidth = 3;
            this.pnlAdjustVolume.Controls.Add(this.label7);
            this.pnlAdjustVolume.Controls.Add(this.picWaveform);
            this.pnlAdjustVolume.Controls.Add(this.btnIncreaseVolume);
            this.pnlAdjustVolume.Controls.Add(this.btnDecreaseVolume);
            this.pnlAdjustVolume.Controls.Add(this.btnResetVolume);
            this.pnlAdjustVolume.Controls.Add(this.lblVolume);
            this.pnlAdjustVolume.Location = new System.Drawing.Point(6, 190);
            this.pnlAdjustVolume.Name = "pnlAdjustVolume";
            this.pnlAdjustVolume.Padding = new System.Windows.Forms.Padding(10);
            this.pnlAdjustVolume.Size = new System.Drawing.Size(281, 151);
            this.pnlAdjustVolume.TabIndex = 70;
            // 
            // label7
            // 
            this.label7.Dock = System.Windows.Forms.DockStyle.Top;
            this.label7.Font = new System.Drawing.Font("Liberation Sans", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.DodgerBlue;
            this.label7.Location = new System.Drawing.Point(10, 10);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(261, 25);
            this.label7.TabIndex = 0;
            this.label7.Text = "2. Adjust Volume";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // picWaveform
            // 
            this.picWaveform.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picWaveform.Location = new System.Drawing.Point(26, 39);
            this.picWaveform.Name = "picWaveform";
            this.picWaveform.Size = new System.Drawing.Size(200, 70);
            this.picWaveform.TabIndex = 56;
            this.picWaveform.TabStop = false;
            // 
            // btnIncreaseVolume
            // 
            this.btnIncreaseVolume.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnIncreaseVolume.ForeColor = System.Drawing.Color.Black;
            this.btnIncreaseVolume.Location = new System.Drawing.Point(232, 36);
            this.btnIncreaseVolume.Name = "btnIncreaseVolume";
            this.btnIncreaseVolume.Size = new System.Drawing.Size(24, 26);
            this.btnIncreaseVolume.TabIndex = 57;
            this.btnIncreaseVolume.Text = "+";
            this.btnIncreaseVolume.UseVisualStyleBackColor = false;
            this.btnIncreaseVolume.Click += new System.EventHandler(this.IncreaseVolume);
            // 
            // btnDecreaseVolume
            // 
            this.btnDecreaseVolume.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnDecreaseVolume.ForeColor = System.Drawing.Color.Black;
            this.btnDecreaseVolume.Location = new System.Drawing.Point(232, 86);
            this.btnDecreaseVolume.Name = "btnDecreaseVolume";
            this.btnDecreaseVolume.Size = new System.Drawing.Size(24, 26);
            this.btnDecreaseVolume.TabIndex = 58;
            this.btnDecreaseVolume.Text = "-";
            this.btnDecreaseVolume.UseVisualStyleBackColor = false;
            this.btnDecreaseVolume.Click += new System.EventHandler(this.DecreaseVolume);
            // 
            // btnResetVolume
            // 
            this.btnResetVolume.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnResetVolume.ForeColor = System.Drawing.Color.Black;
            this.btnResetVolume.Location = new System.Drawing.Point(232, 61);
            this.btnResetVolume.Name = "btnResetVolume";
            this.btnResetVolume.Size = new System.Drawing.Size(24, 26);
            this.btnResetVolume.TabIndex = 59;
            this.btnResetVolume.Text = "·";
            this.btnResetVolume.UseVisualStyleBackColor = false;
            this.btnResetVolume.Click += new System.EventHandler(this.ResetVolume);
            // 
            // lblVolume
            // 
            this.lblVolume.AutoSize = true;
            this.lblVolume.Font = new System.Drawing.Font("Liberation Sans", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVolume.Location = new System.Drawing.Point(75, 109);
            this.lblVolume.Name = "lblVolume";
            this.lblVolume.Size = new System.Drawing.Size(101, 17);
            this.lblVolume.TabIndex = 60;
            this.lblVolume.Text = "Volume: 100%";
            // 
            // pnlCutMethod
            // 
            this.pnlCutMethod.BorderColor = System.Drawing.Color.DodgerBlue;
            this.pnlCutMethod.BorderRadius = 25;
            this.pnlCutMethod.BorderWidth = 3;
            this.pnlCutMethod.Controls.Add(this.label4);
            this.pnlCutMethod.Controls.Add(this.tableLayoutPanel2);
            this.pnlCutMethod.Controls.Add(this.cboAudioLength);
            this.pnlCutMethod.Controls.Add(this.lblFixed);
            this.pnlCutMethod.Controls.Add(this.btnCustom);
            this.pnlCutMethod.Controls.Add(this.picHelp);
            this.pnlCutMethod.Location = new System.Drawing.Point(293, 48);
            this.pnlCutMethod.Name = "pnlCutMethod";
            this.pnlCutMethod.Padding = new System.Windows.Forms.Padding(10);
            this.pnlCutMethod.Size = new System.Drawing.Size(315, 173);
            this.pnlCutMethod.TabIndex = 72;
            // 
            // label4
            // 
            this.label4.Dock = System.Windows.Forms.DockStyle.Top;
            this.label4.Font = new System.Drawing.Font("Liberation Sans", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.DodgerBlue;
            this.label4.Location = new System.Drawing.Point(10, 10);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(295, 25);
            this.label4.TabIndex = 0;
            this.label4.Text = "4. Cut Method";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.rdoFixed, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.rdoCustom, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.rdoMinima, 0, 0);
            this.tableLayoutPanel2.Font = new System.Drawing.Font("Liberation Sans", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tableLayoutPanel2.Location = new System.Drawing.Point(26, 40);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 3;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(255, 83);
            this.tableLayoutPanel2.TabIndex = 65;
            // 
            // rdoFixed
            // 
            this.rdoFixed.AutoSize = true;
            this.rdoFixed.Font = new System.Drawing.Font("Liberation Sans", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdoFixed.Location = new System.Drawing.Point(3, 57);
            this.rdoFixed.Name = "rdoFixed";
            this.rdoFixed.Size = new System.Drawing.Size(188, 21);
            this.rdoFixed.TabIndex = 1;
            this.rdoFixed.Text = "Fixed Cut (equal lengths)";
            this.rdoFixed.UseVisualStyleBackColor = true;
            this.rdoFixed.CheckedChanged += new System.EventHandler(this.RadioChanged);
            // 
            // rdoCustom
            // 
            this.rdoCustom.AutoSize = true;
            this.rdoCustom.Font = new System.Drawing.Font("Liberation Sans", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdoCustom.Location = new System.Drawing.Point(3, 30);
            this.rdoCustom.Name = "rdoCustom";
            this.rdoCustom.Size = new System.Drawing.Size(222, 21);
            this.rdoCustom.TabIndex = 2;
            this.rdoCustom.Text = "I-Cut (advanced, Audacity-like)";
            this.rdoCustom.UseVisualStyleBackColor = true;
            this.rdoCustom.CheckedChanged += new System.EventHandler(this.RadioChanged);
            // 
            // rdoMinima
            // 
            this.rdoMinima.AutoSize = true;
            this.rdoMinima.Checked = true;
            this.rdoMinima.Font = new System.Drawing.Font("Liberation Sans", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdoMinima.Location = new System.Drawing.Point(3, 3);
            this.rdoMinima.Name = "rdoMinima";
            this.rdoMinima.Size = new System.Drawing.Size(240, 21);
            this.rdoMinima.TabIndex = 0;
            this.rdoMinima.TabStop = true;
            this.rdoMinima.Text = "Smart Cut (easy, recommended)";
            this.rdoMinima.UseVisualStyleBackColor = true;
            this.rdoMinima.CheckedChanged += new System.EventHandler(this.RadioChanged);
            // 
            // cboAudioLength
            // 
            this.cboAudioLength.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboAudioLength.Font = new System.Drawing.Font("Liberation Sans", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboAudioLength.FormattingEnabled = true;
            this.cboAudioLength.Location = new System.Drawing.Point(195, 132);
            this.cboAudioLength.Name = "cboAudioLength";
            this.cboAudioLength.Size = new System.Drawing.Size(81, 25);
            this.cboAudioLength.TabIndex = 40;
            this.cboAudioLength.Visible = false;
            // 
            // lblFixed
            // 
            this.lblFixed.AutoSize = true;
            this.lblFixed.Font = new System.Drawing.Font("Liberation Sans", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFixed.Location = new System.Drawing.Point(33, 135);
            this.lblFixed.Name = "lblFixed";
            this.lblFixed.Size = new System.Drawing.Size(162, 17);
            this.lblFixed.TabIndex = 39;
            this.lblFixed.Text = "Fixed OGG length (sec)";
            this.lblFixed.Visible = false;
            // 
            // btnCustom
            // 
            this.btnCustom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnCustom.Font = new System.Drawing.Font("Liberation Sans", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCustom.ForeColor = System.Drawing.Color.Black;
            this.btnCustom.Location = new System.Drawing.Point(51, 129);
            this.btnCustom.Name = "btnCustom";
            this.btnCustom.Size = new System.Drawing.Size(95, 30);
            this.btnCustom.TabIndex = 66;
            this.btnCustom.Text = "Cuts...";
            this.btnCustom.UseVisualStyleBackColor = false;
            this.btnCustom.Visible = false;
            this.btnCustom.Click += new System.EventHandler(this.btnCustom_Click);
            // 
            // picHelp
            // 
            this.picHelp.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picHelp.Image = global::Triggerless.TriggerBot.Properties.Resources.help24;
            this.picHelp.Location = new System.Drawing.Point(23, 132);
            this.picHelp.Name = "picHelp";
            this.picHelp.Size = new System.Drawing.Size(25, 25);
            this.picHelp.TabIndex = 67;
            this.picHelp.TabStop = false;
            this.picHelp.Visible = false;
            this.picHelp.Click += new System.EventHandler(this.picHelp_Click);
            // 
            // pnlSoundQuality
            // 
            this.pnlSoundQuality.BorderColor = System.Drawing.Color.DodgerBlue;
            this.pnlSoundQuality.BorderRadius = 25;
            this.pnlSoundQuality.BorderWidth = 3;
            this.pnlSoundQuality.Controls.Add(this.label3);
            this.pnlSoundQuality.Controls.Add(this.tableLayoutPanel1);
            this.pnlSoundQuality.Location = new System.Drawing.Point(7, 345);
            this.pnlSoundQuality.Name = "pnlSoundQuality";
            this.pnlSoundQuality.Padding = new System.Windows.Forms.Padding(10);
            this.pnlSoundQuality.Size = new System.Drawing.Size(280, 159);
            this.pnlSoundQuality.TabIndex = 71;
            // 
            // label3
            // 
            this.label3.Dock = System.Windows.Forms.DockStyle.Top;
            this.label3.Font = new System.Drawing.Font("Liberation Sans", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.DodgerBlue;
            this.label3.Location = new System.Drawing.Point(10, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(260, 25);
            this.label3.TabIndex = 0;
            this.label3.Text = "3. Sound Quality";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.rdoHQS, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.rdoHQM, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.rdoFMS, 0, 2);
            this.tableLayoutPanel1.Font = new System.Drawing.Font("Liberation Sans", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tableLayoutPanel1.Location = new System.Drawing.Point(26, 40);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(220, 92);
            this.tableLayoutPanel1.TabIndex = 65;
            // 
            // rdoHQS
            // 
            this.rdoHQS.AutoSize = true;
            this.rdoHQS.Location = new System.Drawing.Point(3, 3);
            this.rdoHQS.Name = "rdoHQS";
            this.rdoHQS.Size = new System.Drawing.Size(190, 21);
            this.rdoHQS.TabIndex = 0;
            this.rdoHQS.Text = "HQ Stereo (3:36 / CHKN)";
            this.rdoHQS.UseVisualStyleBackColor = true;
            // 
            // rdoHQM
            // 
            this.rdoHQM.AutoSize = true;
            this.rdoHQM.Location = new System.Drawing.Point(3, 33);
            this.rdoHQM.Name = "rdoHQM";
            this.rdoHQM.Size = new System.Drawing.Size(182, 21);
            this.rdoHQM.TabIndex = 1;
            this.rdoHQM.Text = "HQ Mono (4:40 / CHKN)";
            this.rdoHQM.UseVisualStyleBackColor = true;
            // 
            // rdoFMS
            // 
            this.rdoFMS.AutoSize = true;
            this.rdoFMS.Checked = true;
            this.rdoFMS.Location = new System.Drawing.Point(3, 63);
            this.rdoFMS.Name = "rdoFMS";
            this.rdoFMS.Size = new System.Drawing.Size(188, 21);
            this.rdoFMS.TabIndex = 2;
            this.rdoFMS.TabStop = true;
            this.rdoFMS.Text = "FM Stereo (6:26 / CHKN)";
            this.rdoFMS.UseVisualStyleBackColor = true;
            // 
            // c
            // 
            this.c.BorderColor = System.Drawing.Color.DodgerBlue;
            this.c.BorderRadius = 25;
            this.c.BorderWidth = 3;
            this.c.Controls.Add(this.label2);
            this.c.Controls.Add(this.label1);
            this.c.Controls.Add(this.btnSelectFile);
            this.c.Location = new System.Drawing.Point(7, 48);
            this.c.Name = "c";
            this.c.Padding = new System.Windows.Forms.Padding(10);
            this.c.Size = new System.Drawing.Size(280, 137);
            this.c.TabIndex = 69;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Liberation Sans", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(26, 89);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(243, 39);
            this.label2.TabIndex = 52;
            this.label2.Text = "Accepted formats: MP3, FLAC, OGG, WMA, AAC, M4A, WAV.";
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Liberation Sans", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DodgerBlue;
            this.label1.Location = new System.Drawing.Point(10, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(260, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "1. Select Audio";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnSelectFile
            // 
            this.btnSelectFile.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnSelectFile.Font = new System.Drawing.Font("Liberation Sans", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSelectFile.ForeColor = System.Drawing.Color.Black;
            this.btnSelectFile.Location = new System.Drawing.Point(103, 51);
            this.btnSelectFile.Name = "btnSelectFile";
            this.btnSelectFile.Size = new System.Drawing.Size(75, 30);
            this.btnSelectFile.TabIndex = 38;
            this.btnSelectFile.Text = "Select...";
            this.btnSelectFile.UseVisualStyleBackColor = false;
            this.btnSelectFile.Click += new System.EventHandler(this.SelectFile);
            // 
            // rpnlTop
            // 
            this.rpnlTop.BorderColor = System.Drawing.Color.DodgerBlue;
            this.rpnlTop.BorderRadius = 5;
            this.rpnlTop.BorderWidth = 2;
            this.rpnlTop.Controls.Add(this.lblSelectedFile);
            this.rpnlTop.Controls.Add(this.lblChooseFile);
            this.rpnlTop.Controls.Add(this.lblDuration);
            this.rpnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.rpnlTop.Location = new System.Drawing.Point(0, 0);
            this.rpnlTop.Name = "rpnlTop";
            this.rpnlTop.Size = new System.Drawing.Size(924, 45);
            this.rpnlTop.TabIndex = 68;
            // 
            // lblSelectedFile
            // 
            this.lblSelectedFile.AutoSize = true;
            this.lblSelectedFile.Font = new System.Drawing.Font("Liberation Sans", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSelectedFile.Location = new System.Drawing.Point(152, 4);
            this.lblSelectedFile.Name = "lblSelectedFile";
            this.lblSelectedFile.Size = new System.Drawing.Size(50, 17);
            this.lblSelectedFile.TabIndex = 38;
            this.lblSelectedFile.Text = "(none)";
            // 
            // lblChooseFile
            // 
            this.lblChooseFile.AutoSize = true;
            this.lblChooseFile.Font = new System.Drawing.Font("Liberation Sans", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblChooseFile.Location = new System.Drawing.Point(14, 4);
            this.lblChooseFile.Name = "lblChooseFile";
            this.lblChooseFile.Size = new System.Drawing.Size(135, 17);
            this.lblChooseFile.TabIndex = 37;
            this.lblChooseFile.Text = "Selected Audio File:";
            // 
            // lblDuration
            // 
            this.lblDuration.AutoSize = true;
            this.lblDuration.Font = new System.Drawing.Font("Liberation Sans", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDuration.Location = new System.Drawing.Point(14, 23);
            this.lblDuration.Name = "lblDuration";
            this.lblDuration.Size = new System.Drawing.Size(99, 17);
            this.lblDuration.TabIndex = 53;
            this.lblDuration.Text = "Duration: 0:00";
            // 
            // SplicerControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(29)))), ((int)(((byte)(51)))));
            this.Controls.Add(this.pnlContent);
            this.Font = new System.Drawing.Font("Lucida Sans Unicode", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "SplicerControl";
            this.Size = new System.Drawing.Size(924, 516);
            this.Load += new System.EventHandler(this.SplicerControl_Load);
            this.ControlRemoved += new System.Windows.Forms.ControlEventHandler(this.SplicerControl_ControlRemoved);
            this.pnlContent.ResumeLayout(false);
            this.pnlContent.PerformLayout();
            this.roundedPanel2.ResumeLayout(false);
            this.roundedPanel2.PerformLayout();
            this.roundedPanel1.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.pnlTriggerPrefix.ResumeLayout(false);
            this.pnlTriggerPrefix.PerformLayout();
            this.pnlAdjustVolume.ResumeLayout(false);
            this.pnlAdjustVolume.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picWaveform)).EndInit();
            this.pnlCutMethod.ResumeLayout(false);
            this.pnlCutMethod.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picHelp)).EndInit();
            this.pnlSoundQuality.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.c.ResumeLayout(false);
            this.rpnlTop.ResumeLayout(false);
            this.rpnlTop.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlContent;
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
        private System.Windows.Forms.Label lblCutStageIdle;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.CheckBox checkOGGFiles;
        private System.Windows.Forms.CheckBox checkIcons;
        private System.Windows.Forms.Label lblProfit;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtPrefix;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cboAudioLength;
        private System.Windows.Forms.Label lblFixed;
        private System.Windows.Forms.Button btnSelectFile;
        private AudioSegmenter _audioSegmenter;
        private System.Windows.Forms.OpenFileDialog dlgOpenFile;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.RadioButton rdoHQM;
        private System.Windows.Forms.RadioButton rdoHQS;
        private System.Windows.Forms.RadioButton rdoFixed;
        private System.Windows.Forms.RadioButton rdoMinima;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.RadioButton rdoFMS;
        private System.Windows.Forms.Button btnCustom;
        private System.Windows.Forms.RadioButton rdoCustom;
        private System.Windows.Forms.PictureBox picHelp;
        private RoundedPanel rpnlTop;
        private System.Windows.Forms.Label lblChooseFile;
        private System.Windows.Forms.Label lblSelectedFile;
        private RoundedPanel c;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private RoundedPanel pnlAdjustVolume;
        private System.Windows.Forms.Label label7;
        private RoundedPanel pnlSoundQuality;
        private System.Windows.Forms.Label label3;
        private RoundedPanel pnlCutMethod;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private RoundedPanel pnlTriggerPrefix;
        private System.Windows.Forms.Label label5;
        private RoundedPanel roundedPanel1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private RoundedPanel roundedPanel2;
        private System.Windows.Forms.Label label8;
    }
}
