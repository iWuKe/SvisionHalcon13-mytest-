namespace Svision
{
    partial class BackgroundRemoveFBDForm
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
            this.pictureBoxBackgroundRemove = new System.Windows.Forms.PictureBox();
            this.buttonGetImage = new System.Windows.Forms.Button();
            this.buttonConfirm = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.numericUpDownMinGray = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownMaxGray = new System.Windows.Forms.NumericUpDown();
            this.labelMaxGray = new System.Windows.Forms.Label();
            this.trackBarMaxGray = new System.Windows.Forms.TrackBar();
            this.labelMinGray = new System.Windows.Forms.Label();
            this.trackBarMinGray = new System.Windows.Forms.TrackBar();
            this.groupBoxGray = new System.Windows.Forms.GroupBox();
            this.groupBoxColor = new System.Windows.Forms.GroupBox();
            this.checkBoxAllColor = new System.Windows.Forms.CheckBox();
            this.groupBoxBlue = new System.Windows.Forms.GroupBox();
            this.labelBlueMaxGray = new System.Windows.Forms.Label();
            this.numericUpDownBlueMinGray = new System.Windows.Forms.NumericUpDown();
            this.labelBlueMinGray = new System.Windows.Forms.Label();
            this.numericUpDownBlueMaxGray = new System.Windows.Forms.NumericUpDown();
            this.trackBarBlueMinGray = new System.Windows.Forms.TrackBar();
            this.trackBarBlueMaxGray = new System.Windows.Forms.TrackBar();
            this.groupBoxGreen = new System.Windows.Forms.GroupBox();
            this.labelGreenMaxGray = new System.Windows.Forms.Label();
            this.numericUpDownGreenMinGray = new System.Windows.Forms.NumericUpDown();
            this.labelGreenMinGray = new System.Windows.Forms.Label();
            this.numericUpDownGreenMaxGray = new System.Windows.Forms.NumericUpDown();
            this.trackBarGreenMinGray = new System.Windows.Forms.TrackBar();
            this.trackBarGreenMaxGray = new System.Windows.Forms.TrackBar();
            this.groupBoxRed = new System.Windows.Forms.GroupBox();
            this.labelRedMaxGray = new System.Windows.Forms.Label();
            this.numericUpDownRedMinGray = new System.Windows.Forms.NumericUpDown();
            this.trackBarRedMinGray = new System.Windows.Forms.TrackBar();
            this.labelRedMinGray = new System.Windows.Forms.Label();
            this.trackBarRedMaxGray = new System.Windows.Forms.TrackBar();
            this.numericUpDownRedMaxGray = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBackgroundRemove)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMinGray)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMaxGray)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarMaxGray)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarMinGray)).BeginInit();
            this.groupBoxGray.SuspendLayout();
            this.groupBoxColor.SuspendLayout();
            this.groupBoxBlue.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownBlueMinGray)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownBlueMaxGray)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarBlueMinGray)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarBlueMaxGray)).BeginInit();
            this.groupBoxGreen.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownGreenMinGray)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownGreenMaxGray)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarGreenMinGray)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarGreenMaxGray)).BeginInit();
            this.groupBoxRed.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRedMinGray)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarRedMinGray)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarRedMaxGray)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRedMaxGray)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBoxBackgroundRemove
            // 
            this.pictureBoxBackgroundRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBoxBackgroundRemove.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.pictureBoxBackgroundRemove.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBoxBackgroundRemove.Location = new System.Drawing.Point(223, 12);
            this.pictureBoxBackgroundRemove.Name = "pictureBoxBackgroundRemove";
            this.pictureBoxBackgroundRemove.Size = new System.Drawing.Size(401, 420);
            this.pictureBoxBackgroundRemove.TabIndex = 0;
            this.pictureBoxBackgroundRemove.TabStop = false;
            this.pictureBoxBackgroundRemove.SizeChanged += new System.EventHandler(this.pictureBoxBackgroundRemove_SizeChanged);
            // 
            // buttonGetImage
            // 
            this.buttonGetImage.Location = new System.Drawing.Point(4, 12);
            this.buttonGetImage.Name = "buttonGetImage";
            this.buttonGetImage.Size = new System.Drawing.Size(97, 24);
            this.buttonGetImage.TabIndex = 1;
            this.buttonGetImage.Text = "获取一幅图像";
            this.buttonGetImage.UseVisualStyleBackColor = true;
            this.buttonGetImage.Click += new System.EventHandler(this.buttonGetImage_Click);
            // 
            // buttonConfirm
            // 
            this.buttonConfirm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonConfirm.Location = new System.Drawing.Point(18, 400);
            this.buttonConfirm.Name = "buttonConfirm";
            this.buttonConfirm.Size = new System.Drawing.Size(75, 23);
            this.buttonConfirm.TabIndex = 2;
            this.buttonConfirm.Text = "确定并退出";
            this.buttonConfirm.UseVisualStyleBackColor = true;
            this.buttonConfirm.Click += new System.EventHandler(this.buttonConfirm_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonCancel.Location = new System.Drawing.Point(132, 400);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 3;
            this.buttonCancel.Text = "取消并退出";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // numericUpDownMinGray
            // 
            this.numericUpDownMinGray.Location = new System.Drawing.Point(71, 42);
            this.numericUpDownMinGray.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numericUpDownMinGray.Name = "numericUpDownMinGray";
            this.numericUpDownMinGray.Size = new System.Drawing.Size(44, 21);
            this.numericUpDownMinGray.TabIndex = 14;
            this.numericUpDownMinGray.ValueChanged += new System.EventHandler(this.numericUpDownMinGray_ValueChanged);
            // 
            // numericUpDownMaxGray
            // 
            this.numericUpDownMaxGray.Location = new System.Drawing.Point(71, 15);
            this.numericUpDownMaxGray.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numericUpDownMaxGray.Name = "numericUpDownMaxGray";
            this.numericUpDownMaxGray.Size = new System.Drawing.Size(44, 21);
            this.numericUpDownMaxGray.TabIndex = 13;
            this.numericUpDownMaxGray.ValueChanged += new System.EventHandler(this.numericUpDownMaxGray_ValueChanged);
            // 
            // labelMaxGray
            // 
            this.labelMaxGray.AutoSize = true;
            this.labelMaxGray.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelMaxGray.Location = new System.Drawing.Point(12, 17);
            this.labelMaxGray.Name = "labelMaxGray";
            this.labelMaxGray.Size = new System.Drawing.Size(53, 12);
            this.labelMaxGray.TabIndex = 9;
            this.labelMaxGray.Text = "最大灰度";
            // 
            // trackBarMaxGray
            // 
            this.trackBarMaxGray.Location = new System.Drawing.Point(121, 15);
            this.trackBarMaxGray.Maximum = 255;
            this.trackBarMaxGray.Name = "trackBarMaxGray";
            this.trackBarMaxGray.Size = new System.Drawing.Size(74, 45);
            this.trackBarMaxGray.TabIndex = 11;
            this.trackBarMaxGray.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBarMaxGray.Scroll += new System.EventHandler(this.trackBarMaxGray_Scroll);
            // 
            // labelMinGray
            // 
            this.labelMinGray.AutoSize = true;
            this.labelMinGray.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelMinGray.Location = new System.Drawing.Point(12, 47);
            this.labelMinGray.Name = "labelMinGray";
            this.labelMinGray.Size = new System.Drawing.Size(53, 12);
            this.labelMinGray.TabIndex = 10;
            this.labelMinGray.Text = "最小灰度";
            // 
            // trackBarMinGray
            // 
            this.trackBarMinGray.Location = new System.Drawing.Point(121, 43);
            this.trackBarMinGray.Maximum = 255;
            this.trackBarMinGray.Name = "trackBarMinGray";
            this.trackBarMinGray.Size = new System.Drawing.Size(74, 45);
            this.trackBarMinGray.TabIndex = 12;
            this.trackBarMinGray.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBarMinGray.Scroll += new System.EventHandler(this.trackBarMinGray_Scroll);
            // 
            // groupBoxGray
            // 
            this.groupBoxGray.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBoxGray.Controls.Add(this.labelMaxGray);
            this.groupBoxGray.Controls.Add(this.numericUpDownMinGray);
            this.groupBoxGray.Controls.Add(this.labelMinGray);
            this.groupBoxGray.Controls.Add(this.numericUpDownMaxGray);
            this.groupBoxGray.Controls.Add(this.trackBarMinGray);
            this.groupBoxGray.Controls.Add(this.trackBarMaxGray);
            this.groupBoxGray.Enabled = false;
            this.groupBoxGray.Location = new System.Drawing.Point(4, 42);
            this.groupBoxGray.Name = "groupBoxGray";
            this.groupBoxGray.Size = new System.Drawing.Size(213, 74);
            this.groupBoxGray.TabIndex = 6;
            this.groupBoxGray.TabStop = false;
            this.groupBoxGray.Text = "黑白图像背景滤除";
            // 
            // groupBoxColor
            // 
            this.groupBoxColor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBoxColor.Controls.Add(this.checkBoxAllColor);
            this.groupBoxColor.Controls.Add(this.groupBoxBlue);
            this.groupBoxColor.Controls.Add(this.groupBoxGreen);
            this.groupBoxColor.Controls.Add(this.groupBoxRed);
            this.groupBoxColor.Enabled = false;
            this.groupBoxColor.Location = new System.Drawing.Point(4, 122);
            this.groupBoxColor.Name = "groupBoxColor";
            this.groupBoxColor.Size = new System.Drawing.Size(213, 275);
            this.groupBoxColor.TabIndex = 7;
            this.groupBoxColor.TabStop = false;
            this.groupBoxColor.Text = "彩色图像背景滤除";
            // 
            // checkBoxAllColor
            // 
            this.checkBoxAllColor.AutoSize = true;
            this.checkBoxAllColor.Location = new System.Drawing.Point(6, 20);
            this.checkBoxAllColor.Name = "checkBoxAllColor";
            this.checkBoxAllColor.Size = new System.Drawing.Size(96, 16);
            this.checkBoxAllColor.TabIndex = 3;
            this.checkBoxAllColor.Text = "共同通道调制";
            this.checkBoxAllColor.UseVisualStyleBackColor = true;
            this.checkBoxAllColor.CheckedChanged += new System.EventHandler(this.checkBoxAllColor_CheckedChanged);
            // 
            // groupBoxBlue
            // 
            this.groupBoxBlue.Controls.Add(this.labelBlueMaxGray);
            this.groupBoxBlue.Controls.Add(this.numericUpDownBlueMinGray);
            this.groupBoxBlue.Controls.Add(this.labelBlueMinGray);
            this.groupBoxBlue.Controls.Add(this.numericUpDownBlueMaxGray);
            this.groupBoxBlue.Controls.Add(this.trackBarBlueMinGray);
            this.groupBoxBlue.Controls.Add(this.trackBarBlueMaxGray);
            this.groupBoxBlue.Location = new System.Drawing.Point(6, 198);
            this.groupBoxBlue.Name = "groupBoxBlue";
            this.groupBoxBlue.Size = new System.Drawing.Size(199, 74);
            this.groupBoxBlue.TabIndex = 2;
            this.groupBoxBlue.TabStop = false;
            this.groupBoxBlue.Text = "蓝色通道背景滤除";
            // 
            // labelBlueMaxGray
            // 
            this.labelBlueMaxGray.AutoSize = true;
            this.labelBlueMaxGray.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelBlueMaxGray.Location = new System.Drawing.Point(6, 22);
            this.labelBlueMaxGray.Name = "labelBlueMaxGray";
            this.labelBlueMaxGray.Size = new System.Drawing.Size(53, 12);
            this.labelBlueMaxGray.TabIndex = 15;
            this.labelBlueMaxGray.Text = "最大灰度";
            // 
            // numericUpDownBlueMinGray
            // 
            this.numericUpDownBlueMinGray.Location = new System.Drawing.Point(65, 47);
            this.numericUpDownBlueMinGray.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numericUpDownBlueMinGray.Name = "numericUpDownBlueMinGray";
            this.numericUpDownBlueMinGray.Size = new System.Drawing.Size(44, 21);
            this.numericUpDownBlueMinGray.TabIndex = 20;
            this.numericUpDownBlueMinGray.ValueChanged += new System.EventHandler(this.numericUpDownBlueMinGray_ValueChanged);
            // 
            // labelBlueMinGray
            // 
            this.labelBlueMinGray.AutoSize = true;
            this.labelBlueMinGray.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelBlueMinGray.Location = new System.Drawing.Point(6, 49);
            this.labelBlueMinGray.Name = "labelBlueMinGray";
            this.labelBlueMinGray.Size = new System.Drawing.Size(53, 12);
            this.labelBlueMinGray.TabIndex = 16;
            this.labelBlueMinGray.Text = "最小灰度";
            // 
            // numericUpDownBlueMaxGray
            // 
            this.numericUpDownBlueMaxGray.Location = new System.Drawing.Point(65, 20);
            this.numericUpDownBlueMaxGray.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numericUpDownBlueMaxGray.Name = "numericUpDownBlueMaxGray";
            this.numericUpDownBlueMaxGray.Size = new System.Drawing.Size(44, 21);
            this.numericUpDownBlueMaxGray.TabIndex = 19;
            this.numericUpDownBlueMaxGray.ValueChanged += new System.EventHandler(this.numericUpDownBlueMaxGray_ValueChanged);
            // 
            // trackBarBlueMinGray
            // 
            this.trackBarBlueMinGray.Location = new System.Drawing.Point(115, 47);
            this.trackBarBlueMinGray.Maximum = 255;
            this.trackBarBlueMinGray.Name = "trackBarBlueMinGray";
            this.trackBarBlueMinGray.Size = new System.Drawing.Size(74, 45);
            this.trackBarBlueMinGray.TabIndex = 18;
            this.trackBarBlueMinGray.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBarBlueMinGray.Scroll += new System.EventHandler(this.trackBarBlueMinGray_Scroll);
            // 
            // trackBarBlueMaxGray
            // 
            this.trackBarBlueMaxGray.Location = new System.Drawing.Point(115, 21);
            this.trackBarBlueMaxGray.Maximum = 255;
            this.trackBarBlueMaxGray.Name = "trackBarBlueMaxGray";
            this.trackBarBlueMaxGray.Size = new System.Drawing.Size(74, 45);
            this.trackBarBlueMaxGray.TabIndex = 17;
            this.trackBarBlueMaxGray.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBarBlueMaxGray.Scroll += new System.EventHandler(this.trackBarBlueMaxGray_Scroll);
            // 
            // groupBoxGreen
            // 
            this.groupBoxGreen.Controls.Add(this.labelGreenMaxGray);
            this.groupBoxGreen.Controls.Add(this.numericUpDownGreenMinGray);
            this.groupBoxGreen.Controls.Add(this.labelGreenMinGray);
            this.groupBoxGreen.Controls.Add(this.numericUpDownGreenMaxGray);
            this.groupBoxGreen.Controls.Add(this.trackBarGreenMinGray);
            this.groupBoxGreen.Controls.Add(this.trackBarGreenMaxGray);
            this.groupBoxGreen.Location = new System.Drawing.Point(6, 126);
            this.groupBoxGreen.Name = "groupBoxGreen";
            this.groupBoxGreen.Size = new System.Drawing.Size(199, 68);
            this.groupBoxGreen.TabIndex = 1;
            this.groupBoxGreen.TabStop = false;
            this.groupBoxGreen.Text = "绿色通道背景滤除";
            // 
            // labelGreenMaxGray
            // 
            this.labelGreenMaxGray.AutoSize = true;
            this.labelGreenMaxGray.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelGreenMaxGray.Location = new System.Drawing.Point(6, 17);
            this.labelGreenMaxGray.Name = "labelGreenMaxGray";
            this.labelGreenMaxGray.Size = new System.Drawing.Size(53, 12);
            this.labelGreenMaxGray.TabIndex = 15;
            this.labelGreenMaxGray.Text = "最大灰度";
            // 
            // numericUpDownGreenMinGray
            // 
            this.numericUpDownGreenMinGray.Location = new System.Drawing.Point(65, 42);
            this.numericUpDownGreenMinGray.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numericUpDownGreenMinGray.Name = "numericUpDownGreenMinGray";
            this.numericUpDownGreenMinGray.Size = new System.Drawing.Size(44, 21);
            this.numericUpDownGreenMinGray.TabIndex = 20;
            this.numericUpDownGreenMinGray.ValueChanged += new System.EventHandler(this.numericUpDownGreenMinGray_ValueChanged);
            // 
            // labelGreenMinGray
            // 
            this.labelGreenMinGray.AutoSize = true;
            this.labelGreenMinGray.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelGreenMinGray.Location = new System.Drawing.Point(6, 46);
            this.labelGreenMinGray.Name = "labelGreenMinGray";
            this.labelGreenMinGray.Size = new System.Drawing.Size(53, 12);
            this.labelGreenMinGray.TabIndex = 16;
            this.labelGreenMinGray.Text = "最小灰度";
            // 
            // numericUpDownGreenMaxGray
            // 
            this.numericUpDownGreenMaxGray.Location = new System.Drawing.Point(65, 15);
            this.numericUpDownGreenMaxGray.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numericUpDownGreenMaxGray.Name = "numericUpDownGreenMaxGray";
            this.numericUpDownGreenMaxGray.Size = new System.Drawing.Size(44, 21);
            this.numericUpDownGreenMaxGray.TabIndex = 19;
            this.numericUpDownGreenMaxGray.ValueChanged += new System.EventHandler(this.numericUpDownGreenMaxGray_ValueChanged);
            // 
            // trackBarGreenMinGray
            // 
            this.trackBarGreenMinGray.Location = new System.Drawing.Point(115, 46);
            this.trackBarGreenMinGray.Maximum = 255;
            this.trackBarGreenMinGray.Name = "trackBarGreenMinGray";
            this.trackBarGreenMinGray.Size = new System.Drawing.Size(74, 45);
            this.trackBarGreenMinGray.TabIndex = 18;
            this.trackBarGreenMinGray.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBarGreenMinGray.Scroll += new System.EventHandler(this.trackBarGreenMinGray_Scroll);
            // 
            // trackBarGreenMaxGray
            // 
            this.trackBarGreenMaxGray.Location = new System.Drawing.Point(115, 15);
            this.trackBarGreenMaxGray.Maximum = 255;
            this.trackBarGreenMaxGray.Name = "trackBarGreenMaxGray";
            this.trackBarGreenMaxGray.Size = new System.Drawing.Size(74, 45);
            this.trackBarGreenMaxGray.TabIndex = 17;
            this.trackBarGreenMaxGray.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBarGreenMaxGray.Scroll += new System.EventHandler(this.trackBarGreenMaxGray_Scroll);
            // 
            // groupBoxRed
            // 
            this.groupBoxRed.Controls.Add(this.labelRedMaxGray);
            this.groupBoxRed.Controls.Add(this.numericUpDownRedMinGray);
            this.groupBoxRed.Controls.Add(this.trackBarRedMinGray);
            this.groupBoxRed.Controls.Add(this.labelRedMinGray);
            this.groupBoxRed.Controls.Add(this.trackBarRedMaxGray);
            this.groupBoxRed.Controls.Add(this.numericUpDownRedMaxGray);
            this.groupBoxRed.Location = new System.Drawing.Point(6, 42);
            this.groupBoxRed.Name = "groupBoxRed";
            this.groupBoxRed.Size = new System.Drawing.Size(199, 78);
            this.groupBoxRed.TabIndex = 0;
            this.groupBoxRed.TabStop = false;
            this.groupBoxRed.Text = "红色通道背景滤除";
            // 
            // labelRedMaxGray
            // 
            this.labelRedMaxGray.AutoSize = true;
            this.labelRedMaxGray.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelRedMaxGray.Location = new System.Drawing.Point(6, 25);
            this.labelRedMaxGray.Name = "labelRedMaxGray";
            this.labelRedMaxGray.Size = new System.Drawing.Size(53, 12);
            this.labelRedMaxGray.TabIndex = 15;
            this.labelRedMaxGray.Text = "最大灰度";
            // 
            // numericUpDownRedMinGray
            // 
            this.numericUpDownRedMinGray.Location = new System.Drawing.Point(65, 50);
            this.numericUpDownRedMinGray.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numericUpDownRedMinGray.Name = "numericUpDownRedMinGray";
            this.numericUpDownRedMinGray.Size = new System.Drawing.Size(44, 21);
            this.numericUpDownRedMinGray.TabIndex = 20;
            this.numericUpDownRedMinGray.ValueChanged += new System.EventHandler(this.numericUpDownRedMinGray_ValueChanged);
            // 
            // trackBarRedMinGray
            // 
            this.trackBarRedMinGray.Location = new System.Drawing.Point(115, 50);
            this.trackBarRedMinGray.Maximum = 255;
            this.trackBarRedMinGray.Name = "trackBarRedMinGray";
            this.trackBarRedMinGray.Size = new System.Drawing.Size(74, 45);
            this.trackBarRedMinGray.TabIndex = 17;
            this.trackBarRedMinGray.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBarRedMinGray.Scroll += new System.EventHandler(this.trackBarRedMinGray_Scroll);
            // 
            // labelRedMinGray
            // 
            this.labelRedMinGray.AutoSize = true;
            this.labelRedMinGray.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelRedMinGray.Location = new System.Drawing.Point(6, 52);
            this.labelRedMinGray.Name = "labelRedMinGray";
            this.labelRedMinGray.Size = new System.Drawing.Size(53, 12);
            this.labelRedMinGray.TabIndex = 16;
            this.labelRedMinGray.Text = "最小灰度";
            // 
            // trackBarRedMaxGray
            // 
            this.trackBarRedMaxGray.Location = new System.Drawing.Point(115, 17);
            this.trackBarRedMaxGray.Maximum = 255;
            this.trackBarRedMaxGray.Name = "trackBarRedMaxGray";
            this.trackBarRedMaxGray.Size = new System.Drawing.Size(74, 45);
            this.trackBarRedMaxGray.TabIndex = 18;
            this.trackBarRedMaxGray.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBarRedMaxGray.Scroll += new System.EventHandler(this.trackBarRedMaxGray_Scroll);
            // 
            // numericUpDownRedMaxGray
            // 
            this.numericUpDownRedMaxGray.Location = new System.Drawing.Point(65, 23);
            this.numericUpDownRedMaxGray.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numericUpDownRedMaxGray.Name = "numericUpDownRedMaxGray";
            this.numericUpDownRedMaxGray.Size = new System.Drawing.Size(44, 21);
            this.numericUpDownRedMaxGray.TabIndex = 19;
            this.numericUpDownRedMaxGray.ValueChanged += new System.EventHandler(this.numericUpDownRedMaxGray_ValueChanged);
            // 
            // BackgroundRemoveFBDForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(630, 442);
            this.Controls.Add(this.groupBoxColor);
            this.Controls.Add(this.groupBoxGray);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonConfirm);
            this.Controls.Add(this.buttonGetImage);
            this.Controls.Add(this.pictureBoxBackgroundRemove);
            this.MinimumSize = new System.Drawing.Size(646, 480);
            this.Name = "BackgroundRemoveFBDForm";
            this.Text = "背景消除";
            this.Load += new System.EventHandler(this.BackgroundRemoveFBDForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBackgroundRemove)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMinGray)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMaxGray)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarMaxGray)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarMinGray)).EndInit();
            this.groupBoxGray.ResumeLayout(false);
            this.groupBoxGray.PerformLayout();
            this.groupBoxColor.ResumeLayout(false);
            this.groupBoxColor.PerformLayout();
            this.groupBoxBlue.ResumeLayout(false);
            this.groupBoxBlue.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownBlueMinGray)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownBlueMaxGray)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarBlueMinGray)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarBlueMaxGray)).EndInit();
            this.groupBoxGreen.ResumeLayout(false);
            this.groupBoxGreen.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownGreenMinGray)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownGreenMaxGray)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarGreenMinGray)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarGreenMaxGray)).EndInit();
            this.groupBoxRed.ResumeLayout(false);
            this.groupBoxRed.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRedMinGray)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarRedMinGray)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarRedMaxGray)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRedMaxGray)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxBackgroundRemove;
        private System.Windows.Forms.Button buttonGetImage;
        private System.Windows.Forms.Button buttonConfirm;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.NumericUpDown numericUpDownMinGray;
        private System.Windows.Forms.NumericUpDown numericUpDownMaxGray;
        private System.Windows.Forms.Label labelMaxGray;
        private System.Windows.Forms.TrackBar trackBarMaxGray;
        private System.Windows.Forms.Label labelMinGray;
        private System.Windows.Forms.GroupBox groupBoxGray;
        private System.Windows.Forms.TrackBar trackBarMinGray;
        private System.Windows.Forms.GroupBox groupBoxColor;
        private System.Windows.Forms.GroupBox groupBoxBlue;
        private System.Windows.Forms.Label labelBlueMaxGray;
        private System.Windows.Forms.NumericUpDown numericUpDownBlueMinGray;
        private System.Windows.Forms.Label labelBlueMinGray;
        private System.Windows.Forms.NumericUpDown numericUpDownBlueMaxGray;
        private System.Windows.Forms.TrackBar trackBarBlueMinGray;
        private System.Windows.Forms.TrackBar trackBarBlueMaxGray;
        private System.Windows.Forms.GroupBox groupBoxGreen;
        private System.Windows.Forms.Label labelGreenMaxGray;
        private System.Windows.Forms.NumericUpDown numericUpDownGreenMinGray;
        private System.Windows.Forms.Label labelGreenMinGray;
        private System.Windows.Forms.NumericUpDown numericUpDownGreenMaxGray;
        private System.Windows.Forms.TrackBar trackBarGreenMinGray;
        private System.Windows.Forms.TrackBar trackBarGreenMaxGray;
        private System.Windows.Forms.GroupBox groupBoxRed;
        private System.Windows.Forms.Label labelRedMaxGray;
        private System.Windows.Forms.NumericUpDown numericUpDownRedMinGray;
        private System.Windows.Forms.TrackBar trackBarRedMinGray;
        private System.Windows.Forms.Label labelRedMinGray;
        private System.Windows.Forms.TrackBar trackBarRedMaxGray;
        private System.Windows.Forms.NumericUpDown numericUpDownRedMaxGray;
        private System.Windows.Forms.CheckBox checkBoxAllColor;
    }
}