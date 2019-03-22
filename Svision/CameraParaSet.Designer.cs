namespace Svision
{
    partial class CameraParaSet
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
            this.components = new System.ComponentModel.Container();
            this.cameraParapictureBox = new System.Windows.Forms.PictureBox();
            this.cameraParaTimer = new System.Windows.Forms.Timer(this.components);
            this.heithtLabel = new System.Windows.Forms.Label();
            this.widthLabel = new System.Windows.Forms.Label();
            this.numericUpDownheight = new System.Windows.Forms.NumericUpDown();
            this.trackBarHeight = new System.Windows.Forms.TrackBar();
            this.numericUpDownWidth = new System.Windows.Forms.NumericUpDown();
            this.trackBarWidth = new System.Windows.Forms.TrackBar();
            this.cameraParaPartpictureBox = new System.Windows.Forms.PictureBox();
            this.numericUpDownXOffset = new System.Windows.Forms.NumericUpDown();
            this.labelXOffset = new System.Windows.Forms.Label();
            this.labelYOffset = new System.Windows.Forms.Label();
            this.numericUpDownYOffset = new System.Windows.Forms.NumericUpDown();
            this.trackBarXOffset = new System.Windows.Forms.TrackBar();
            this.trackBarYOffset = new System.Windows.Forms.TrackBar();
            this.groupBoxChannel = new System.Windows.Forms.GroupBox();
            this.radioButtonColour = new System.Windows.Forms.RadioButton();
            this.radioButtonGray = new System.Windows.Forms.RadioButton();
            this.panelDetail = new System.Windows.Forms.Panel();
            this.numericUpDownGamma = new System.Windows.Forms.NumericUpDown();
            this.groupBoxWhiteBalance = new System.Windows.Forms.GroupBox();
            this.numericUpDownGreenBalance = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownRedBalance = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownBlueBalance = new System.Windows.Forms.NumericUpDown();
            this.labelGreenBalance = new System.Windows.Forms.Label();
            this.labelRedBalance = new System.Windows.Forms.Label();
            this.labelBlueBalance = new System.Windows.Forms.Label();
            this.checkBoxWhiteBalanceAuto = new System.Windows.Forms.CheckBox();
            this.groupBoxExposure = new System.Windows.Forms.GroupBox();
            this.numericUpDownExposure = new System.Windows.Forms.NumericUpDown();
            this.checkBoxExposureAuto = new System.Windows.Forms.CheckBox();
            this.checkBoxGainAuto = new System.Windows.Forms.CheckBox();
            this.groupBoxGain = new System.Windows.Forms.GroupBox();
            this.numericUpDownGain = new System.Windows.Forms.NumericUpDown();
            this.labelGamma = new System.Windows.Forms.Label();
            this.checkBoxDetail = new System.Windows.Forms.CheckBox();
            this.textBoxCameraInformation = new System.Windows.Forms.TextBox();
            this.labelCameraInformation = new System.Windows.Forms.Label();
            this.buttonCamParaSetConfirm = new System.Windows.Forms.Button();
            this.buttonCamParaSetCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.cameraParapictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownheight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cameraParaPartpictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownXOffset)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownYOffset)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarXOffset)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarYOffset)).BeginInit();
            this.groupBoxChannel.SuspendLayout();
            this.panelDetail.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownGamma)).BeginInit();
            this.groupBoxWhiteBalance.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownGreenBalance)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRedBalance)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownBlueBalance)).BeginInit();
            this.groupBoxExposure.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownExposure)).BeginInit();
            this.groupBoxGain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownGain)).BeginInit();
            this.SuspendLayout();
            // 
            // cameraParapictureBox
            // 
            this.cameraParapictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cameraParapictureBox.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.cameraParapictureBox.Cursor = System.Windows.Forms.Cursors.Default;
            this.cameraParapictureBox.Location = new System.Drawing.Point(392, 271);
            this.cameraParapictureBox.Name = "cameraParapictureBox";
            this.cameraParapictureBox.Size = new System.Drawing.Size(256, 192);
            this.cameraParapictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.cameraParapictureBox.TabIndex = 0;
            this.cameraParapictureBox.TabStop = false;
            this.cameraParapictureBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureMouseDown);
            this.cameraParapictureBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureMouseMove);
            // 
            // cameraParaTimer
            // 
            this.cameraParaTimer.Tick += new System.EventHandler(this.cameraParaTimer_Tick);
            // 
            // heithtLabel
            // 
            this.heithtLabel.AutoSize = true;
            this.heithtLabel.Location = new System.Drawing.Point(12, 9);
            this.heithtLabel.Name = "heithtLabel";
            this.heithtLabel.Size = new System.Drawing.Size(53, 12);
            this.heithtLabel.TabIndex = 1;
            this.heithtLabel.Text = "相机行数";
            // 
            // widthLabel
            // 
            this.widthLabel.AutoSize = true;
            this.widthLabel.Location = new System.Drawing.Point(12, 38);
            this.widthLabel.Name = "widthLabel";
            this.widthLabel.Size = new System.Drawing.Size(53, 12);
            this.widthLabel.TabIndex = 2;
            this.widthLabel.Text = "相机列数";
            // 
            // numericUpDownheight
            // 
            this.numericUpDownheight.Location = new System.Drawing.Point(68, 7);
            this.numericUpDownheight.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDownheight.Name = "numericUpDownheight";
            this.numericUpDownheight.Size = new System.Drawing.Size(54, 21);
            this.numericUpDownheight.TabIndex = 5;
            this.numericUpDownheight.ValueChanged += new System.EventHandler(this.NumericValueChangeHeight);
            // 
            // trackBarHeight
            // 
            this.trackBarHeight.Location = new System.Drawing.Point(128, 5);
            this.trackBarHeight.Maximum = 4000;
            this.trackBarHeight.Name = "trackBarHeight";
            this.trackBarHeight.Size = new System.Drawing.Size(87, 45);
            this.trackBarHeight.TabIndex = 6;
            this.trackBarHeight.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBarHeight.ValueChanged += new System.EventHandler(this.trackBarChangedHeight);
            // 
            // numericUpDownWidth
            // 
            this.numericUpDownWidth.Location = new System.Drawing.Point(68, 34);
            this.numericUpDownWidth.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDownWidth.Name = "numericUpDownWidth";
            this.numericUpDownWidth.Size = new System.Drawing.Size(54, 21);
            this.numericUpDownWidth.TabIndex = 7;
            this.numericUpDownWidth.ValueChanged += new System.EventHandler(this.numericValueChangeWidth);
            // 
            // trackBarWidth
            // 
            this.trackBarWidth.Location = new System.Drawing.Point(128, 34);
            this.trackBarWidth.Maximum = 4000;
            this.trackBarWidth.Name = "trackBarWidth";
            this.trackBarWidth.Size = new System.Drawing.Size(87, 45);
            this.trackBarWidth.TabIndex = 8;
            this.trackBarWidth.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBarWidth.ValueChanged += new System.EventHandler(this.trackBarChangeWidth);
            // 
            // cameraParaPartpictureBox
            // 
            this.cameraParaPartpictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cameraParaPartpictureBox.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.cameraParaPartpictureBox.Location = new System.Drawing.Point(245, 7);
            this.cameraParaPartpictureBox.Name = "cameraParaPartpictureBox";
            this.cameraParaPartpictureBox.Size = new System.Drawing.Size(403, 258);
            this.cameraParaPartpictureBox.TabIndex = 9;
            this.cameraParaPartpictureBox.TabStop = false;
            this.cameraParaPartpictureBox.SizeChanged += new System.EventHandler(this.cameraParaPartpictureBox_SizeChanged);
            // 
            // numericUpDownXOffset
            // 
            this.numericUpDownXOffset.Location = new System.Drawing.Point(68, 61);
            this.numericUpDownXOffset.Maximum = new decimal(new int[] {
            4000,
            0,
            0,
            0});
            this.numericUpDownXOffset.Name = "numericUpDownXOffset";
            this.numericUpDownXOffset.Size = new System.Drawing.Size(54, 21);
            this.numericUpDownXOffset.TabIndex = 10;
            this.numericUpDownXOffset.ValueChanged += new System.EventHandler(this.numericValueChangeXOffset);
            // 
            // labelXOffset
            // 
            this.labelXOffset.AutoSize = true;
            this.labelXOffset.Location = new System.Drawing.Point(12, 67);
            this.labelXOffset.Name = "labelXOffset";
            this.labelXOffset.Size = new System.Drawing.Size(35, 12);
            this.labelXOffset.TabIndex = 11;
            this.labelXOffset.Text = "X偏移";
            // 
            // labelYOffset
            // 
            this.labelYOffset.AutoSize = true;
            this.labelYOffset.Location = new System.Drawing.Point(12, 89);
            this.labelYOffset.Name = "labelYOffset";
            this.labelYOffset.Size = new System.Drawing.Size(35, 12);
            this.labelYOffset.TabIndex = 12;
            this.labelYOffset.Text = "Y偏移";
            // 
            // numericUpDownYOffset
            // 
            this.numericUpDownYOffset.Location = new System.Drawing.Point(68, 88);
            this.numericUpDownYOffset.Maximum = new decimal(new int[] {
            4000,
            0,
            0,
            0});
            this.numericUpDownYOffset.Name = "numericUpDownYOffset";
            this.numericUpDownYOffset.Size = new System.Drawing.Size(54, 21);
            this.numericUpDownYOffset.TabIndex = 13;
            this.numericUpDownYOffset.ValueChanged += new System.EventHandler(this.numericValueChangeYOffset);
            // 
            // trackBarXOffset
            // 
            this.trackBarXOffset.Location = new System.Drawing.Point(128, 56);
            this.trackBarXOffset.Maximum = 4000;
            this.trackBarXOffset.Name = "trackBarXOffset";
            this.trackBarXOffset.Size = new System.Drawing.Size(87, 45);
            this.trackBarXOffset.TabIndex = 14;
            this.trackBarXOffset.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBarXOffset.ValueChanged += new System.EventHandler(this.trackBarChangeXOffset);
            // 
            // trackBarYOffset
            // 
            this.trackBarYOffset.Location = new System.Drawing.Point(128, 85);
            this.trackBarYOffset.Maximum = 4000;
            this.trackBarYOffset.Name = "trackBarYOffset";
            this.trackBarYOffset.Size = new System.Drawing.Size(87, 45);
            this.trackBarYOffset.TabIndex = 15;
            this.trackBarYOffset.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBarYOffset.ValueChanged += new System.EventHandler(this.trackBarChangeYOffset);
            // 
            // groupBoxChannel
            // 
            this.groupBoxChannel.Controls.Add(this.radioButtonColour);
            this.groupBoxChannel.Controls.Add(this.radioButtonGray);
            this.groupBoxChannel.Location = new System.Drawing.Point(14, 115);
            this.groupBoxChannel.Name = "groupBoxChannel";
            this.groupBoxChannel.Size = new System.Drawing.Size(201, 42);
            this.groupBoxChannel.TabIndex = 16;
            this.groupBoxChannel.TabStop = false;
            this.groupBoxChannel.Text = "黑白/彩色";
            // 
            // radioButtonColour
            // 
            this.radioButtonColour.AutoSize = true;
            this.radioButtonColour.Location = new System.Drawing.Point(114, 20);
            this.radioButtonColour.Name = "radioButtonColour";
            this.radioButtonColour.Size = new System.Drawing.Size(71, 16);
            this.radioButtonColour.TabIndex = 1;
            this.radioButtonColour.Text = "彩色图像";
            this.radioButtonColour.UseVisualStyleBackColor = true;
            this.radioButtonColour.CheckedChanged += new System.EventHandler(this.radioChangeColour);
            // 
            // radioButtonGray
            // 
            this.radioButtonGray.AutoSize = true;
            this.radioButtonGray.Checked = true;
            this.radioButtonGray.Location = new System.Drawing.Point(7, 20);
            this.radioButtonGray.Name = "radioButtonGray";
            this.radioButtonGray.Size = new System.Drawing.Size(71, 16);
            this.radioButtonGray.TabIndex = 0;
            this.radioButtonGray.TabStop = true;
            this.radioButtonGray.Text = "黑白图像";
            this.radioButtonGray.UseVisualStyleBackColor = true;
            this.radioButtonGray.CheckedChanged += new System.EventHandler(this.radioChangeGray);
            // 
            // panelDetail
            // 
            this.panelDetail.Controls.Add(this.numericUpDownGamma);
            this.panelDetail.Controls.Add(this.groupBoxWhiteBalance);
            this.panelDetail.Controls.Add(this.checkBoxWhiteBalanceAuto);
            this.panelDetail.Controls.Add(this.groupBoxExposure);
            this.panelDetail.Controls.Add(this.checkBoxExposureAuto);
            this.panelDetail.Controls.Add(this.checkBoxGainAuto);
            this.panelDetail.Controls.Add(this.groupBoxGain);
            this.panelDetail.Controls.Add(this.labelGamma);
            this.panelDetail.Location = new System.Drawing.Point(14, 185);
            this.panelDetail.Name = "panelDetail";
            this.panelDetail.Size = new System.Drawing.Size(213, 278);
            this.panelDetail.TabIndex = 17;
            // 
            // numericUpDownGamma
            // 
            this.numericUpDownGamma.DecimalPlaces = 3;
            this.numericUpDownGamma.Location = new System.Drawing.Point(148, 2);
            this.numericUpDownGamma.Name = "numericUpDownGamma";
            this.numericUpDownGamma.Size = new System.Drawing.Size(62, 21);
            this.numericUpDownGamma.TabIndex = 10;
            this.numericUpDownGamma.ValueChanged += new System.EventHandler(this.numericUpDownGamma_ValueChanged);
            // 
            // groupBoxWhiteBalance
            // 
            this.groupBoxWhiteBalance.Controls.Add(this.numericUpDownGreenBalance);
            this.groupBoxWhiteBalance.Controls.Add(this.numericUpDownRedBalance);
            this.groupBoxWhiteBalance.Controls.Add(this.numericUpDownBlueBalance);
            this.groupBoxWhiteBalance.Controls.Add(this.labelGreenBalance);
            this.groupBoxWhiteBalance.Controls.Add(this.labelRedBalance);
            this.groupBoxWhiteBalance.Controls.Add(this.labelBlueBalance);
            this.groupBoxWhiteBalance.Location = new System.Drawing.Point(3, 178);
            this.groupBoxWhiteBalance.Name = "groupBoxWhiteBalance";
            this.groupBoxWhiteBalance.Size = new System.Drawing.Size(198, 96);
            this.groupBoxWhiteBalance.TabIndex = 9;
            this.groupBoxWhiteBalance.TabStop = false;
            this.groupBoxWhiteBalance.Text = "白平衡百分比（0%~100%）";
            // 
            // numericUpDownGreenBalance
            // 
            this.numericUpDownGreenBalance.DecimalPlaces = 3;
            this.numericUpDownGreenBalance.Location = new System.Drawing.Point(78, 67);
            this.numericUpDownGreenBalance.Name = "numericUpDownGreenBalance";
            this.numericUpDownGreenBalance.Size = new System.Drawing.Size(71, 21);
            this.numericUpDownGreenBalance.TabIndex = 8;
            this.numericUpDownGreenBalance.ValueChanged += new System.EventHandler(this.numericUpDownGreenBalance_ValueChanged);
            // 
            // numericUpDownRedBalance
            // 
            this.numericUpDownRedBalance.DecimalPlaces = 3;
            this.numericUpDownRedBalance.Location = new System.Drawing.Point(78, 42);
            this.numericUpDownRedBalance.Name = "numericUpDownRedBalance";
            this.numericUpDownRedBalance.Size = new System.Drawing.Size(71, 21);
            this.numericUpDownRedBalance.TabIndex = 7;
            this.numericUpDownRedBalance.ValueChanged += new System.EventHandler(this.numericUpDownRedBalance_ValueChanged);
            // 
            // numericUpDownBlueBalance
            // 
            this.numericUpDownBlueBalance.DecimalPlaces = 3;
            this.numericUpDownBlueBalance.Location = new System.Drawing.Point(78, 17);
            this.numericUpDownBlueBalance.Name = "numericUpDownBlueBalance";
            this.numericUpDownBlueBalance.Size = new System.Drawing.Size(71, 21);
            this.numericUpDownBlueBalance.TabIndex = 6;
            this.numericUpDownBlueBalance.ValueChanged += new System.EventHandler(this.numericUpDownBlueBalance_ValueChanged);
            // 
            // labelGreenBalance
            // 
            this.labelGreenBalance.AutoSize = true;
            this.labelGreenBalance.Location = new System.Drawing.Point(19, 72);
            this.labelGreenBalance.Name = "labelGreenBalance";
            this.labelGreenBalance.Size = new System.Drawing.Size(53, 12);
            this.labelGreenBalance.TabIndex = 5;
            this.labelGreenBalance.Text = "绿色通道";
            // 
            // labelRedBalance
            // 
            this.labelRedBalance.AutoSize = true;
            this.labelRedBalance.Location = new System.Drawing.Point(19, 46);
            this.labelRedBalance.Name = "labelRedBalance";
            this.labelRedBalance.Size = new System.Drawing.Size(53, 12);
            this.labelRedBalance.TabIndex = 4;
            this.labelRedBalance.Text = "红色通道";
            // 
            // labelBlueBalance
            // 
            this.labelBlueBalance.AutoSize = true;
            this.labelBlueBalance.Location = new System.Drawing.Point(19, 20);
            this.labelBlueBalance.Name = "labelBlueBalance";
            this.labelBlueBalance.Size = new System.Drawing.Size(53, 12);
            this.labelBlueBalance.TabIndex = 3;
            this.labelBlueBalance.Text = "蓝色通道";
            // 
            // checkBoxWhiteBalanceAuto
            // 
            this.checkBoxWhiteBalanceAuto.AutoSize = true;
            this.checkBoxWhiteBalanceAuto.Location = new System.Drawing.Point(3, 156);
            this.checkBoxWhiteBalanceAuto.Name = "checkBoxWhiteBalanceAuto";
            this.checkBoxWhiteBalanceAuto.Size = new System.Drawing.Size(84, 16);
            this.checkBoxWhiteBalanceAuto.TabIndex = 8;
            this.checkBoxWhiteBalanceAuto.Text = "自动白平衡";
            this.checkBoxWhiteBalanceAuto.UseVisualStyleBackColor = true;
            this.checkBoxWhiteBalanceAuto.CheckedChanged += new System.EventHandler(this.checkBoxChangeWhiteBalanceAuto);
            // 
            // groupBoxExposure
            // 
            this.groupBoxExposure.Controls.Add(this.numericUpDownExposure);
            this.groupBoxExposure.Location = new System.Drawing.Point(3, 111);
            this.groupBoxExposure.Name = "groupBoxExposure";
            this.groupBoxExposure.Size = new System.Drawing.Size(198, 39);
            this.groupBoxExposure.TabIndex = 7;
            this.groupBoxExposure.TabStop = false;
            this.groupBoxExposure.Text = "曝光百分比（0%~100%）";
            // 
            // numericUpDownExposure
            // 
            this.numericUpDownExposure.DecimalPlaces = 3;
            this.numericUpDownExposure.Location = new System.Drawing.Point(78, 14);
            this.numericUpDownExposure.Name = "numericUpDownExposure";
            this.numericUpDownExposure.Size = new System.Drawing.Size(71, 21);
            this.numericUpDownExposure.TabIndex = 1;
            this.numericUpDownExposure.ValueChanged += new System.EventHandler(this.numericUpDownExposure_ValueChanged);
            // 
            // checkBoxExposureAuto
            // 
            this.checkBoxExposureAuto.AutoSize = true;
            this.checkBoxExposureAuto.Location = new System.Drawing.Point(3, 89);
            this.checkBoxExposureAuto.Name = "checkBoxExposureAuto";
            this.checkBoxExposureAuto.Size = new System.Drawing.Size(72, 16);
            this.checkBoxExposureAuto.TabIndex = 6;
            this.checkBoxExposureAuto.Text = "自动曝光";
            this.checkBoxExposureAuto.UseVisualStyleBackColor = true;
            this.checkBoxExposureAuto.CheckedChanged += new System.EventHandler(this.checkBoxChangeExposureAuto);
            // 
            // checkBoxGainAuto
            // 
            this.checkBoxGainAuto.AutoSize = true;
            this.checkBoxGainAuto.Location = new System.Drawing.Point(3, 19);
            this.checkBoxGainAuto.Name = "checkBoxGainAuto";
            this.checkBoxGainAuto.Size = new System.Drawing.Size(72, 16);
            this.checkBoxGainAuto.TabIndex = 5;
            this.checkBoxGainAuto.Text = "自动增益";
            this.checkBoxGainAuto.UseVisualStyleBackColor = true;
            this.checkBoxGainAuto.CheckedChanged += new System.EventHandler(this.checkChangeGainAuto);
            // 
            // groupBoxGain
            // 
            this.groupBoxGain.Controls.Add(this.numericUpDownGain);
            this.groupBoxGain.Location = new System.Drawing.Point(3, 41);
            this.groupBoxGain.Name = "groupBoxGain";
            this.groupBoxGain.Size = new System.Drawing.Size(198, 42);
            this.groupBoxGain.TabIndex = 4;
            this.groupBoxGain.TabStop = false;
            this.groupBoxGain.Text = "增益百分比（0%~100%）";
            // 
            // numericUpDownGain
            // 
            this.numericUpDownGain.DecimalPlaces = 3;
            this.numericUpDownGain.Location = new System.Drawing.Point(78, 15);
            this.numericUpDownGain.Name = "numericUpDownGain";
            this.numericUpDownGain.Size = new System.Drawing.Size(71, 21);
            this.numericUpDownGain.TabIndex = 1;
            this.numericUpDownGain.ValueChanged += new System.EventHandler(this.numericUpDownGain_ValueChanged);
            // 
            // labelGamma
            // 
            this.labelGamma.AutoSize = true;
            this.labelGamma.Location = new System.Drawing.Point(1, 4);
            this.labelGamma.Name = "labelGamma";
            this.labelGamma.Size = new System.Drawing.Size(143, 12);
            this.labelGamma.TabIndex = 0;
            this.labelGamma.Text = "Gamma百分比（0%~100%）:";
            // 
            // checkBoxDetail
            // 
            this.checkBoxDetail.AutoSize = true;
            this.checkBoxDetail.Cursor = System.Windows.Forms.Cursors.Default;
            this.checkBoxDetail.Location = new System.Drawing.Point(14, 163);
            this.checkBoxDetail.Name = "checkBoxDetail";
            this.checkBoxDetail.Size = new System.Drawing.Size(48, 16);
            this.checkBoxDetail.TabIndex = 18;
            this.checkBoxDetail.Text = "高级";
            this.checkBoxDetail.UseVisualStyleBackColor = true;
            this.checkBoxDetail.CheckedChanged += new System.EventHandler(this.checkBoxChangeDetail);
            // 
            // textBoxCameraInformation
            // 
            this.textBoxCameraInformation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxCameraInformation.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBoxCameraInformation.Location = new System.Drawing.Point(235, 293);
            this.textBoxCameraInformation.Multiline = true;
            this.textBoxCameraInformation.Name = "textBoxCameraInformation";
            this.textBoxCameraInformation.ReadOnly = true;
            this.textBoxCameraInformation.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxCameraInformation.Size = new System.Drawing.Size(141, 133);
            this.textBoxCameraInformation.TabIndex = 19;
            // 
            // labelCameraInformation
            // 
            this.labelCameraInformation.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelCameraInformation.AutoSize = true;
            this.labelCameraInformation.Location = new System.Drawing.Point(233, 275);
            this.labelCameraInformation.Name = "labelCameraInformation";
            this.labelCameraInformation.Size = new System.Drawing.Size(53, 12);
            this.labelCameraInformation.TabIndex = 20;
            this.labelCameraInformation.Text = "相机信息";
            // 
            // buttonCamParaSetConfirm
            // 
            this.buttonCamParaSetConfirm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonCamParaSetConfirm.Location = new System.Drawing.Point(235, 437);
            this.buttonCamParaSetConfirm.Name = "buttonCamParaSetConfirm";
            this.buttonCamParaSetConfirm.Size = new System.Drawing.Size(73, 20);
            this.buttonCamParaSetConfirm.TabIndex = 21;
            this.buttonCamParaSetConfirm.Text = "确认并退出";
            this.buttonCamParaSetConfirm.UseVisualStyleBackColor = true;
            this.buttonCamParaSetConfirm.Click += new System.EventHandler(this.buttonCamParaSetConfirm_Click);
            // 
            // buttonCamParaSetCancel
            // 
            this.buttonCamParaSetCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCamParaSetCancel.Location = new System.Drawing.Point(312, 437);
            this.buttonCamParaSetCancel.Name = "buttonCamParaSetCancel";
            this.buttonCamParaSetCancel.Size = new System.Drawing.Size(74, 20);
            this.buttonCamParaSetCancel.TabIndex = 22;
            this.buttonCamParaSetCancel.Text = "取消并退出";
            this.buttonCamParaSetCancel.UseVisualStyleBackColor = true;
            this.buttonCamParaSetCancel.Click += new System.EventHandler(this.buttonCamParaSetCancel_Click);
            // 
            // CameraParaSet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(650, 471);
            this.Controls.Add(this.buttonCamParaSetCancel);
            this.Controls.Add(this.buttonCamParaSetConfirm);
            this.Controls.Add(this.labelCameraInformation);
            this.Controls.Add(this.textBoxCameraInformation);
            this.Controls.Add(this.checkBoxDetail);
            this.Controls.Add(this.panelDetail);
            this.Controls.Add(this.groupBoxChannel);
            this.Controls.Add(this.trackBarYOffset);
            this.Controls.Add(this.trackBarXOffset);
            this.Controls.Add(this.numericUpDownYOffset);
            this.Controls.Add(this.labelYOffset);
            this.Controls.Add(this.labelXOffset);
            this.Controls.Add(this.numericUpDownXOffset);
            this.Controls.Add(this.cameraParaPartpictureBox);
            this.Controls.Add(this.cameraParapictureBox);
            this.Controls.Add(this.trackBarWidth);
            this.Controls.Add(this.numericUpDownWidth);
            this.Controls.Add(this.trackBarHeight);
            this.Controls.Add(this.numericUpDownheight);
            this.Controls.Add(this.widthLabel);
            this.Controls.Add(this.heithtLabel);
            this.MinimumSize = new System.Drawing.Size(666, 509);
            this.Name = "CameraParaSet";
            this.Text = " 相机设置";
            this.Load += new System.EventHandler(this.CameraParaSet_Load);
            ((System.ComponentModel.ISupportInitialize)(this.cameraParapictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownheight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cameraParaPartpictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownXOffset)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownYOffset)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarXOffset)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarYOffset)).EndInit();
            this.groupBoxChannel.ResumeLayout(false);
            this.groupBoxChannel.PerformLayout();
            this.panelDetail.ResumeLayout(false);
            this.panelDetail.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownGamma)).EndInit();
            this.groupBoxWhiteBalance.ResumeLayout(false);
            this.groupBoxWhiteBalance.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownGreenBalance)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRedBalance)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownBlueBalance)).EndInit();
            this.groupBoxExposure.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownExposure)).EndInit();
            this.groupBoxGain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownGain)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox cameraParapictureBox;
        private System.Windows.Forms.Label heithtLabel;
        private System.Windows.Forms.Label widthLabel;
        private System.Windows.Forms.NumericUpDown numericUpDownheight;
        private System.Windows.Forms.TrackBar trackBarHeight;
        private System.Windows.Forms.NumericUpDown numericUpDownWidth;
        private System.Windows.Forms.TrackBar trackBarWidth;
        private System.Windows.Forms.PictureBox cameraParaPartpictureBox;
        private System.Windows.Forms.NumericUpDown numericUpDownXOffset;
        private System.Windows.Forms.Label labelXOffset;
        private System.Windows.Forms.Label labelYOffset;
        private System.Windows.Forms.NumericUpDown numericUpDownYOffset;
        private System.Windows.Forms.TrackBar trackBarXOffset;
        private System.Windows.Forms.TrackBar trackBarYOffset;
        private System.Windows.Forms.GroupBox groupBoxChannel;
        private System.Windows.Forms.RadioButton radioButtonColour;
        private System.Windows.Forms.RadioButton radioButtonGray;
        private System.Windows.Forms.Panel panelDetail;
        private System.Windows.Forms.CheckBox checkBoxDetail;
        private System.Windows.Forms.Label labelGamma;
        private System.Windows.Forms.GroupBox groupBoxGain;
        private System.Windows.Forms.CheckBox checkBoxGainAuto;
        private System.Windows.Forms.CheckBox checkBoxExposureAuto;
        private System.Windows.Forms.GroupBox groupBoxExposure;
        private System.Windows.Forms.GroupBox groupBoxWhiteBalance;
        private System.Windows.Forms.Label labelGreenBalance;
        private System.Windows.Forms.Label labelRedBalance;
        private System.Windows.Forms.Label labelBlueBalance;
        private System.Windows.Forms.CheckBox checkBoxWhiteBalanceAuto;
        private System.Windows.Forms.TextBox textBoxCameraInformation;
        private System.Windows.Forms.Label labelCameraInformation;
        private System.Windows.Forms.Button buttonCamParaSetConfirm;
        private System.Windows.Forms.Button buttonCamParaSetCancel;
        public System.Windows.Forms.Timer cameraParaTimer;
        private System.Windows.Forms.NumericUpDown numericUpDownExposure;
        private System.Windows.Forms.NumericUpDown numericUpDownGain;
        private System.Windows.Forms.NumericUpDown numericUpDownGreenBalance;
        private System.Windows.Forms.NumericUpDown numericUpDownRedBalance;
        private System.Windows.Forms.NumericUpDown numericUpDownBlueBalance;
        private System.Windows.Forms.NumericUpDown numericUpDownGamma;
    }
}