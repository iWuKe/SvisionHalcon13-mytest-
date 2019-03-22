namespace Svision
{
    partial class FileInputSet
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
            this.labelRow = new System.Windows.Forms.Label();
            this.labelColumn = new System.Windows.Forms.Label();
            this.labelChannel = new System.Windows.Forms.Label();
            this.numericUpDownRow = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownColumn = new System.Windows.Forms.NumericUpDown();
            this.labelFilePath = new System.Windows.Forms.Label();
            this.folderBrowserDialogGetPath = new System.Windows.Forms.FolderBrowserDialog();
            this.textBoxFilePath = new System.Windows.Forms.TextBox();
            this.buttonGetFilePath = new System.Windows.Forms.Button();
            this.comboBoxChannel = new System.Windows.Forms.ComboBox();
            this.buttonConfirm = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonShowImage = new System.Windows.Forms.Button();
            this.listBoxFileList = new System.Windows.Forms.ListBox();
            this.labelImageList = new System.Windows.Forms.Label();
            this.pictureBoxImage = new System.Windows.Forms.PictureBox();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.labelImageInfo = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRow)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownColumn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxImage)).BeginInit();
            this.SuspendLayout();
            // 
            // labelRow
            // 
            this.labelRow.AutoSize = true;
            this.labelRow.Location = new System.Drawing.Point(10, 7);
            this.labelRow.Name = "labelRow";
            this.labelRow.Size = new System.Drawing.Size(53, 12);
            this.labelRow.TabIndex = 0;
            this.labelRow.Text = "图像行数";
            // 
            // labelColumn
            // 
            this.labelColumn.AutoSize = true;
            this.labelColumn.Location = new System.Drawing.Point(131, 9);
            this.labelColumn.Name = "labelColumn";
            this.labelColumn.Size = new System.Drawing.Size(53, 12);
            this.labelColumn.TabIndex = 1;
            this.labelColumn.Text = "图像列数";
            // 
            // labelChannel
            // 
            this.labelChannel.AutoSize = true;
            this.labelChannel.Location = new System.Drawing.Point(10, 32);
            this.labelChannel.Name = "labelChannel";
            this.labelChannel.Size = new System.Drawing.Size(65, 12);
            this.labelChannel.TabIndex = 2;
            this.labelChannel.Text = "图像通道数";
            // 
            // numericUpDownRow
            // 
            this.numericUpDownRow.Location = new System.Drawing.Point(69, 5);
            this.numericUpDownRow.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDownRow.Name = "numericUpDownRow";
            this.numericUpDownRow.Size = new System.Drawing.Size(56, 21);
            this.numericUpDownRow.TabIndex = 3;
            // 
            // numericUpDownColumn
            // 
            this.numericUpDownColumn.Location = new System.Drawing.Point(190, 5);
            this.numericUpDownColumn.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDownColumn.Name = "numericUpDownColumn";
            this.numericUpDownColumn.Size = new System.Drawing.Size(56, 21);
            this.numericUpDownColumn.TabIndex = 4;
            // 
            // labelFilePath
            // 
            this.labelFilePath.AutoSize = true;
            this.labelFilePath.Location = new System.Drawing.Point(10, 55);
            this.labelFilePath.Name = "labelFilePath";
            this.labelFilePath.Size = new System.Drawing.Size(89, 12);
            this.labelFilePath.TabIndex = 7;
            this.labelFilePath.Text = "文件路径设置：";
            // 
            // textBoxFilePath
            // 
            this.textBoxFilePath.Location = new System.Drawing.Point(12, 75);
            this.textBoxFilePath.Multiline = true;
            this.textBoxFilePath.Name = "textBoxFilePath";
            this.textBoxFilePath.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.textBoxFilePath.Size = new System.Drawing.Size(234, 46);
            this.textBoxFilePath.TabIndex = 8;
            this.textBoxFilePath.TextChanged += new System.EventHandler(this.textBoxFilePath_TextChanged);
            // 
            // buttonGetFilePath
            // 
            this.buttonGetFilePath.Location = new System.Drawing.Point(188, 42);
            this.buttonGetFilePath.Name = "buttonGetFilePath";
            this.buttonGetFilePath.Size = new System.Drawing.Size(58, 27);
            this.buttonGetFilePath.TabIndex = 9;
            this.buttonGetFilePath.Text = "浏览";
            this.buttonGetFilePath.UseVisualStyleBackColor = true;
            this.buttonGetFilePath.Click += new System.EventHandler(this.buttonGetFilePath_Click);
            // 
            // comboBoxChannel
            // 
            this.comboBoxChannel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxChannel.FormattingEnabled = true;
            this.comboBoxChannel.Items.AddRange(new object[] {
            "1",
            "3"});
            this.comboBoxChannel.Location = new System.Drawing.Point(81, 29);
            this.comboBoxChannel.MaxDropDownItems = 2;
            this.comboBoxChannel.Name = "comboBoxChannel";
            this.comboBoxChannel.Size = new System.Drawing.Size(55, 20);
            this.comboBoxChannel.TabIndex = 10;
            // 
            // buttonConfirm
            // 
            this.buttonConfirm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonConfirm.Location = new System.Drawing.Point(13, 405);
            this.buttonConfirm.Name = "buttonConfirm";
            this.buttonConfirm.Size = new System.Drawing.Size(75, 23);
            this.buttonConfirm.TabIndex = 11;
            this.buttonConfirm.Text = "确认并退出";
            this.buttonConfirm.UseVisualStyleBackColor = true;
            this.buttonConfirm.Click += new System.EventHandler(this.buttonConfirm_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonCancel.Location = new System.Drawing.Point(162, 407);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(84, 23);
            this.buttonCancel.TabIndex = 12;
            this.buttonCancel.Text = "取消";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonShowImage
            // 
            this.buttonShowImage.Location = new System.Drawing.Point(131, 124);
            this.buttonShowImage.Name = "buttonShowImage";
            this.buttonShowImage.Size = new System.Drawing.Size(51, 23);
            this.buttonShowImage.TabIndex = 15;
            this.buttonShowImage.Text = "查看";
            this.buttonShowImage.UseVisualStyleBackColor = true;
            this.buttonShowImage.Click += new System.EventHandler(this.buttonShowImage_Click);
            // 
            // listBoxFileList
            // 
            this.listBoxFileList.FormattingEnabled = true;
            this.listBoxFileList.HorizontalScrollbar = true;
            this.listBoxFileList.ItemHeight = 12;
            this.listBoxFileList.Location = new System.Drawing.Point(12, 167);
            this.listBoxFileList.Name = "listBoxFileList";
            this.listBoxFileList.ScrollAlwaysVisible = true;
            this.listBoxFileList.Size = new System.Drawing.Size(234, 232);
            this.listBoxFileList.TabIndex = 16;
            this.listBoxFileList.SelectedIndexChanged += new System.EventHandler(this.listBoxFileList_SelectedIndexChanged);
            // 
            // labelImageList
            // 
            this.labelImageList.AutoSize = true;
            this.labelImageList.Location = new System.Drawing.Point(12, 124);
            this.labelImageList.Name = "labelImageList";
            this.labelImageList.Size = new System.Drawing.Size(113, 12);
            this.labelImageList.TabIndex = 17;
            this.labelImageList.Text = "所有图像文件列表：";
            // 
            // pictureBoxImage
            // 
            this.pictureBoxImage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBoxImage.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.pictureBoxImage.Location = new System.Drawing.Point(258, 12);
            this.pictureBoxImage.Name = "pictureBoxImage";
            this.pictureBoxImage.Size = new System.Drawing.Size(426, 416);
            this.pictureBoxImage.TabIndex = 18;
            this.pictureBoxImage.TabStop = false;
            this.pictureBoxImage.SizeChanged += new System.EventHandler(this.pictureBoxImage_SizeChanged);
            // 
            // buttonDelete
            // 
            this.buttonDelete.Location = new System.Drawing.Point(199, 124);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(47, 23);
            this.buttonDelete.TabIndex = 19;
            this.buttonDelete.Text = "删除";
            this.buttonDelete.UseVisualStyleBackColor = true;
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // labelImageInfo
            // 
            this.labelImageInfo.AutoSize = true;
            this.labelImageInfo.Location = new System.Drawing.Point(14, 149);
            this.labelImageInfo.Name = "labelImageInfo";
            this.labelImageInfo.Size = new System.Drawing.Size(89, 12);
            this.labelImageInfo.TabIndex = 20;
            this.labelImageInfo.Text = "行   列   通道";
            // 
            // FileInputSet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(696, 442);
            this.Controls.Add(this.labelImageInfo);
            this.Controls.Add(this.buttonDelete);
            this.Controls.Add(this.pictureBoxImage);
            this.Controls.Add(this.labelImageList);
            this.Controls.Add(this.listBoxFileList);
            this.Controls.Add(this.buttonShowImage);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonConfirm);
            this.Controls.Add(this.comboBoxChannel);
            this.Controls.Add(this.buttonGetFilePath);
            this.Controls.Add(this.textBoxFilePath);
            this.Controls.Add(this.labelFilePath);
            this.Controls.Add(this.numericUpDownColumn);
            this.Controls.Add(this.numericUpDownRow);
            this.Controls.Add(this.labelChannel);
            this.Controls.Add(this.labelColumn);
            this.Controls.Add(this.labelRow);
            this.Name = "FileInputSet";
            this.Text = "文件设置";
            this.Load += new System.EventHandler(this.FileInputSet_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRow)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownColumn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelRow;
        private System.Windows.Forms.Label labelColumn;
        private System.Windows.Forms.Label labelChannel;
        private System.Windows.Forms.NumericUpDown numericUpDownRow;
        private System.Windows.Forms.NumericUpDown numericUpDownColumn;
        private System.Windows.Forms.Label labelFilePath;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialogGetPath;
        private System.Windows.Forms.TextBox textBoxFilePath;
        private System.Windows.Forms.Button buttonGetFilePath;
        private System.Windows.Forms.ComboBox comboBoxChannel;
        private System.Windows.Forms.Button buttonConfirm;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonShowImage;
        private System.Windows.Forms.ListBox listBoxFileList;
        private System.Windows.Forms.Label labelImageList;
        private System.Windows.Forms.PictureBox pictureBoxImage;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.Label labelImageInfo;
    }
}