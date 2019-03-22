namespace Svision
{
    partial class GetAndSaveImageTool
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
            this.pictureBoxGetAndSaveImage = new System.Windows.Forms.PictureBox();
            this.textBoxGetFolderPath = new System.Windows.Forms.TextBox();
            this.buttonGetFolderPath = new System.Windows.Forms.Button();
            this.folderBrowserDialogGetFolderPath = new System.Windows.Forms.FolderBrowserDialog();
            this.labelGetFolderPath = new System.Windows.Forms.Label();
            this.groupBoxAuto = new System.Windows.Forms.GroupBox();
            this.buttonBeginOrStopSaveImage = new System.Windows.Forms.Button();
            this.panelAuto = new System.Windows.Forms.Panel();
            this.numericUpDownTime = new System.Windows.Forms.NumericUpDown();
            this.labelTime = new System.Windows.Forms.Label();
            this.labelBeginNum = new System.Windows.Forms.Label();
            this.numericUpDownBeginNum = new System.Windows.Forms.NumericUpDown();
            this.textBoxCurrentNum = new System.Windows.Forms.TextBox();
            this.labelCurrentNum = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.timerAuto = new System.Windows.Forms.Timer(this.components);
            this.groupBoxManual = new System.Windows.Forms.GroupBox();
            this.buttonSaveImage = new System.Windows.Forms.Button();
            this.buttonGetImage = new System.Windows.Forms.Button();
            this.saveFileDialogManual = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxGetAndSaveImage)).BeginInit();
            this.groupBoxAuto.SuspendLayout();
            this.panelAuto.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownBeginNum)).BeginInit();
            this.groupBoxManual.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBoxGetAndSaveImage
            // 
            this.pictureBoxGetAndSaveImage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBoxGetAndSaveImage.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.pictureBoxGetAndSaveImage.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBoxGetAndSaveImage.Location = new System.Drawing.Point(257, 3);
            this.pictureBoxGetAndSaveImage.Name = "pictureBoxGetAndSaveImage";
            this.pictureBoxGetAndSaveImage.Size = new System.Drawing.Size(393, 363);
            this.pictureBoxGetAndSaveImage.TabIndex = 10;
            this.pictureBoxGetAndSaveImage.TabStop = false;
            this.pictureBoxGetAndSaveImage.SizeChanged += new System.EventHandler(this.pictureBoxGetAndSaveImage_SizeChanged);
            // 
            // textBoxGetFolderPath
            // 
            this.textBoxGetFolderPath.Location = new System.Drawing.Point(5, 23);
            this.textBoxGetFolderPath.Multiline = true;
            this.textBoxGetFolderPath.Name = "textBoxGetFolderPath";
            this.textBoxGetFolderPath.Size = new System.Drawing.Size(179, 61);
            this.textBoxGetFolderPath.TabIndex = 11;
            this.textBoxGetFolderPath.TextChanged += new System.EventHandler(this.textBoxGetFolderPath_TextChanged);
            // 
            // buttonGetFolderPath
            // 
            this.buttonGetFolderPath.Location = new System.Drawing.Point(190, 34);
            this.buttonGetFolderPath.Name = "buttonGetFolderPath";
            this.buttonGetFolderPath.Size = new System.Drawing.Size(39, 34);
            this.buttonGetFolderPath.TabIndex = 12;
            this.buttonGetFolderPath.Text = "浏览";
            this.buttonGetFolderPath.UseVisualStyleBackColor = true;
            this.buttonGetFolderPath.Click += new System.EventHandler(this.buttonGetFolderPath_Click);
            // 
            // labelGetFolderPath
            // 
            this.labelGetFolderPath.AutoSize = true;
            this.labelGetFolderPath.Location = new System.Drawing.Point(5, 8);
            this.labelGetFolderPath.Name = "labelGetFolderPath";
            this.labelGetFolderPath.Size = new System.Drawing.Size(149, 12);
            this.labelGetFolderPath.TabIndex = 13;
            this.labelGetFolderPath.Text = "选择图像保存文件夹路径：";
            // 
            // groupBoxAuto
            // 
            this.groupBoxAuto.Controls.Add(this.buttonBeginOrStopSaveImage);
            this.groupBoxAuto.Controls.Add(this.panelAuto);
            this.groupBoxAuto.Location = new System.Drawing.Point(4, 3);
            this.groupBoxAuto.Name = "groupBoxAuto";
            this.groupBoxAuto.Size = new System.Drawing.Size(247, 220);
            this.groupBoxAuto.TabIndex = 14;
            this.groupBoxAuto.TabStop = false;
            this.groupBoxAuto.Text = "自动拍照保存";
            // 
            // buttonBeginOrStopSaveImage
            // 
            this.buttonBeginOrStopSaveImage.Location = new System.Drawing.Point(6, 191);
            this.buttonBeginOrStopSaveImage.Name = "buttonBeginOrStopSaveImage";
            this.buttonBeginOrStopSaveImage.Size = new System.Drawing.Size(87, 23);
            this.buttonBeginOrStopSaveImage.TabIndex = 16;
            this.buttonBeginOrStopSaveImage.Text = "开始保存图像";
            this.buttonBeginOrStopSaveImage.UseVisualStyleBackColor = true;
            this.buttonBeginOrStopSaveImage.Click += new System.EventHandler(this.buttonBeginOrStopSaveImage_Click);
            // 
            // panelAuto
            // 
            this.panelAuto.Controls.Add(this.numericUpDownTime);
            this.panelAuto.Controls.Add(this.labelTime);
            this.panelAuto.Controls.Add(this.labelBeginNum);
            this.panelAuto.Controls.Add(this.numericUpDownBeginNum);
            this.panelAuto.Controls.Add(this.buttonGetFolderPath);
            this.panelAuto.Controls.Add(this.textBoxCurrentNum);
            this.panelAuto.Controls.Add(this.labelGetFolderPath);
            this.panelAuto.Controls.Add(this.textBoxGetFolderPath);
            this.panelAuto.Controls.Add(this.labelCurrentNum);
            this.panelAuto.Location = new System.Drawing.Point(6, 20);
            this.panelAuto.Name = "panelAuto";
            this.panelAuto.Size = new System.Drawing.Size(235, 165);
            this.panelAuto.TabIndex = 15;
            // 
            // numericUpDownTime
            // 
            this.numericUpDownTime.Increment = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.numericUpDownTime.Location = new System.Drawing.Point(176, 140);
            this.numericUpDownTime.Maximum = new decimal(new int[] {
            600000,
            0,
            0,
            0});
            this.numericUpDownTime.Minimum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.numericUpDownTime.Name = "numericUpDownTime";
            this.numericUpDownTime.Size = new System.Drawing.Size(56, 21);
            this.numericUpDownTime.TabIndex = 19;
            this.numericUpDownTime.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // labelTime
            // 
            this.labelTime.AutoSize = true;
            this.labelTime.Location = new System.Drawing.Point(3, 142);
            this.labelTime.Name = "labelTime";
            this.labelTime.Size = new System.Drawing.Size(149, 12);
            this.labelTime.TabIndex = 18;
            this.labelTime.Text = "保存图像时间间隔（ms）：";
            // 
            // labelBeginNum
            // 
            this.labelBeginNum.AutoSize = true;
            this.labelBeginNum.Location = new System.Drawing.Point(3, 115);
            this.labelBeginNum.Name = "labelBeginNum";
            this.labelBeginNum.Size = new System.Drawing.Size(167, 12);
            this.labelBeginNum.TabIndex = 15;
            this.labelBeginNum.Text = "保存图像起始编号(从0开始)：";
            // 
            // numericUpDownBeginNum
            // 
            this.numericUpDownBeginNum.Location = new System.Drawing.Point(176, 113);
            this.numericUpDownBeginNum.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.numericUpDownBeginNum.Name = "numericUpDownBeginNum";
            this.numericUpDownBeginNum.Size = new System.Drawing.Size(56, 21);
            this.numericUpDownBeginNum.TabIndex = 17;
            // 
            // textBoxCurrentNum
            // 
            this.textBoxCurrentNum.Location = new System.Drawing.Point(176, 89);
            this.textBoxCurrentNum.Name = "textBoxCurrentNum";
            this.textBoxCurrentNum.ReadOnly = true;
            this.textBoxCurrentNum.Size = new System.Drawing.Size(53, 21);
            this.textBoxCurrentNum.TabIndex = 16;
            // 
            // labelCurrentNum
            // 
            this.labelCurrentNum.AutoSize = true;
            this.labelCurrentNum.Location = new System.Drawing.Point(3, 92);
            this.labelCurrentNum.Name = "labelCurrentNum";
            this.labelCurrentNum.Size = new System.Drawing.Size(143, 12);
            this.labelCurrentNum.TabIndex = 14;
            this.labelCurrentNum.Text = "当前图像编号(从0开始)：";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // timerAuto
            // 
            this.timerAuto.Tick += new System.EventHandler(this.timerAuto_Tick);
            // 
            // groupBoxManual
            // 
            this.groupBoxManual.Controls.Add(this.buttonSaveImage);
            this.groupBoxManual.Controls.Add(this.buttonGetImage);
            this.groupBoxManual.Location = new System.Drawing.Point(4, 229);
            this.groupBoxManual.Name = "groupBoxManual";
            this.groupBoxManual.Size = new System.Drawing.Size(247, 50);
            this.groupBoxManual.TabIndex = 15;
            this.groupBoxManual.TabStop = false;
            this.groupBoxManual.Text = "手动拍照保存";
            // 
            // buttonSaveImage
            // 
            this.buttonSaveImage.Location = new System.Drawing.Point(101, 20);
            this.buttonSaveImage.Name = "buttonSaveImage";
            this.buttonSaveImage.Size = new System.Drawing.Size(75, 23);
            this.buttonSaveImage.TabIndex = 1;
            this.buttonSaveImage.Text = "保存图像";
            this.buttonSaveImage.UseVisualStyleBackColor = true;
            this.buttonSaveImage.Click += new System.EventHandler(this.buttonSaveImage_Click);
            // 
            // buttonGetImage
            // 
            this.buttonGetImage.Location = new System.Drawing.Point(11, 20);
            this.buttonGetImage.Name = "buttonGetImage";
            this.buttonGetImage.Size = new System.Drawing.Size(75, 23);
            this.buttonGetImage.TabIndex = 0;
            this.buttonGetImage.Text = "获取图像";
            this.buttonGetImage.UseVisualStyleBackColor = true;
            this.buttonGetImage.Click += new System.EventHandler(this.buttonGetImage_Click);
            // 
            // GetAndSaveImageTool
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(653, 378);
            this.Controls.Add(this.groupBoxManual);
            this.Controls.Add(this.groupBoxAuto);
            this.Controls.Add(this.pictureBoxGetAndSaveImage);
            this.Name = "GetAndSaveImageTool";
            this.Text = "拍照工具";
            this.Load += new System.EventHandler(this.GetAndSaveImageTool_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxGetAndSaveImage)).EndInit();
            this.groupBoxAuto.ResumeLayout(false);
            this.panelAuto.ResumeLayout(false);
            this.panelAuto.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownBeginNum)).EndInit();
            this.groupBoxManual.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxGetAndSaveImage;
        private System.Windows.Forms.TextBox textBoxGetFolderPath;
        private System.Windows.Forms.Button buttonGetFolderPath;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialogGetFolderPath;
        private System.Windows.Forms.Label labelGetFolderPath;
        private System.Windows.Forms.GroupBox groupBoxAuto;
        private System.Windows.Forms.Panel panelAuto;
        private System.Windows.Forms.NumericUpDown numericUpDownTime;
        private System.Windows.Forms.Label labelTime;
        private System.Windows.Forms.Label labelBeginNum;
        private System.Windows.Forms.NumericUpDown numericUpDownBeginNum;
        private System.Windows.Forms.TextBox textBoxCurrentNum;
        private System.Windows.Forms.Label labelCurrentNum;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button buttonBeginOrStopSaveImage;
        private System.Windows.Forms.Timer timerAuto;
        private System.Windows.Forms.GroupBox groupBoxManual;
        private System.Windows.Forms.Button buttonSaveImage;
        private System.Windows.Forms.Button buttonGetImage;
        private System.Windows.Forms.SaveFileDialog saveFileDialogManual;
    }
}