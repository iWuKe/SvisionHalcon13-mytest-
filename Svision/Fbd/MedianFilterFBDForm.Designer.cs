namespace Svision
{
    partial class MedianFilterFBDForm
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
            this.pictureBoxMedian = new System.Windows.Forms.PictureBox();
            this.groupBoxMedianFilterFBDMaskSize = new System.Windows.Forms.GroupBox();
            this.radioButtonMedianFilter11 = new System.Windows.Forms.RadioButton();
            this.radioButtonMedianFilter9 = new System.Windows.Forms.RadioButton();
            this.radioButtonMedianFilter7 = new System.Windows.Forms.RadioButton();
            this.radioButtonMedianFilter5 = new System.Windows.Forms.RadioButton();
            this.radioButtonMedianFilter3 = new System.Windows.Forms.RadioButton();
            this.buttonMedianFilterFBDConfirm = new System.Windows.Forms.Button();
            this.buttonMedianFilterFBDCancel = new System.Windows.Forms.Button();
            this.buttongetImageFrame = new System.Windows.Forms.Button();
            this.openFileDialogOpenImage = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMedian)).BeginInit();
            this.groupBoxMedianFilterFBDMaskSize.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBoxMedian
            // 
            this.pictureBoxMedian.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBoxMedian.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.pictureBoxMedian.Location = new System.Drawing.Point(183, 12);
            this.pictureBoxMedian.Name = "pictureBoxMedian";
            this.pictureBoxMedian.Size = new System.Drawing.Size(418, 370);
            this.pictureBoxMedian.TabIndex = 0;
            this.pictureBoxMedian.TabStop = false;
            this.pictureBoxMedian.SizeChanged += new System.EventHandler(this.pictureBoxMedian_SizeChanged);
            // 
            // groupBoxMedianFilterFBDMaskSize
            // 
            this.groupBoxMedianFilterFBDMaskSize.Controls.Add(this.radioButtonMedianFilter11);
            this.groupBoxMedianFilterFBDMaskSize.Controls.Add(this.radioButtonMedianFilter9);
            this.groupBoxMedianFilterFBDMaskSize.Controls.Add(this.radioButtonMedianFilter7);
            this.groupBoxMedianFilterFBDMaskSize.Controls.Add(this.radioButtonMedianFilter5);
            this.groupBoxMedianFilterFBDMaskSize.Controls.Add(this.radioButtonMedianFilter3);
            this.groupBoxMedianFilterFBDMaskSize.Enabled = false;
            this.groupBoxMedianFilterFBDMaskSize.Font = new System.Drawing.Font("宋体", 11F);
            this.groupBoxMedianFilterFBDMaskSize.Location = new System.Drawing.Point(12, 92);
            this.groupBoxMedianFilterFBDMaskSize.Name = "groupBoxMedianFilterFBDMaskSize";
            this.groupBoxMedianFilterFBDMaskSize.Size = new System.Drawing.Size(155, 232);
            this.groupBoxMedianFilterFBDMaskSize.TabIndex = 1;
            this.groupBoxMedianFilterFBDMaskSize.TabStop = false;
            this.groupBoxMedianFilterFBDMaskSize.Text = "掩模大小";
            // 
            // radioButtonMedianFilter11
            // 
            this.radioButtonMedianFilter11.AutoSize = true;
            this.radioButtonMedianFilter11.Location = new System.Drawing.Point(13, 188);
            this.radioButtonMedianFilter11.Name = "radioButtonMedianFilter11";
            this.radioButtonMedianFilter11.Size = new System.Drawing.Size(81, 19);
            this.radioButtonMedianFilter11.TabIndex = 4;
            this.radioButtonMedianFilter11.TabStop = true;
            this.radioButtonMedianFilter11.Text = "11 X 11";
            this.radioButtonMedianFilter11.UseVisualStyleBackColor = true;
            this.radioButtonMedianFilter11.CheckedChanged += new System.EventHandler(this.radioButtonMedianFilter11_CheckedChanged);
            // 
            // radioButtonMedianFilter9
            // 
            this.radioButtonMedianFilter9.AutoSize = true;
            this.radioButtonMedianFilter9.Location = new System.Drawing.Point(13, 148);
            this.radioButtonMedianFilter9.Name = "radioButtonMedianFilter9";
            this.radioButtonMedianFilter9.Size = new System.Drawing.Size(65, 19);
            this.radioButtonMedianFilter9.TabIndex = 3;
            this.radioButtonMedianFilter9.TabStop = true;
            this.radioButtonMedianFilter9.Text = "9 X 9";
            this.radioButtonMedianFilter9.UseVisualStyleBackColor = true;
            this.radioButtonMedianFilter9.CheckedChanged += new System.EventHandler(this.radioButtonMedianFilter9_CheckedChanged);
            // 
            // radioButtonMedianFilter7
            // 
            this.radioButtonMedianFilter7.AutoSize = true;
            this.radioButtonMedianFilter7.Location = new System.Drawing.Point(13, 107);
            this.radioButtonMedianFilter7.Name = "radioButtonMedianFilter7";
            this.radioButtonMedianFilter7.Size = new System.Drawing.Size(65, 19);
            this.radioButtonMedianFilter7.TabIndex = 2;
            this.radioButtonMedianFilter7.TabStop = true;
            this.radioButtonMedianFilter7.Text = "7 X 7";
            this.radioButtonMedianFilter7.UseVisualStyleBackColor = true;
            this.radioButtonMedianFilter7.CheckedChanged += new System.EventHandler(this.radioButtonMedianFilter7_CheckedChanged);
            // 
            // radioButtonMedianFilter5
            // 
            this.radioButtonMedianFilter5.AutoSize = true;
            this.radioButtonMedianFilter5.Location = new System.Drawing.Point(13, 66);
            this.radioButtonMedianFilter5.Name = "radioButtonMedianFilter5";
            this.radioButtonMedianFilter5.Size = new System.Drawing.Size(65, 19);
            this.radioButtonMedianFilter5.TabIndex = 1;
            this.radioButtonMedianFilter5.TabStop = true;
            this.radioButtonMedianFilter5.Text = "5 X 5";
            this.radioButtonMedianFilter5.UseVisualStyleBackColor = true;
            this.radioButtonMedianFilter5.CheckedChanged += new System.EventHandler(this.radioButtonMedianFilter5_CheckedChanged);
            // 
            // radioButtonMedianFilter3
            // 
            this.radioButtonMedianFilter3.AutoSize = true;
            this.radioButtonMedianFilter3.Location = new System.Drawing.Point(13, 26);
            this.radioButtonMedianFilter3.Name = "radioButtonMedianFilter3";
            this.radioButtonMedianFilter3.Size = new System.Drawing.Size(65, 19);
            this.radioButtonMedianFilter3.TabIndex = 0;
            this.radioButtonMedianFilter3.TabStop = true;
            this.radioButtonMedianFilter3.Text = "3 X 3";
            this.radioButtonMedianFilter3.UseVisualStyleBackColor = true;
            this.radioButtonMedianFilter3.CheckedChanged += new System.EventHandler(this.radioButtonMedianFilter3_CheckedChanged);
            // 
            // buttonMedianFilterFBDConfirm
            // 
            this.buttonMedianFilterFBDConfirm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonMedianFilterFBDConfirm.Font = new System.Drawing.Font("宋体", 9F);
            this.buttonMedianFilterFBDConfirm.Location = new System.Drawing.Point(12, 340);
            this.buttonMedianFilterFBDConfirm.Name = "buttonMedianFilterFBDConfirm";
            this.buttonMedianFilterFBDConfirm.Size = new System.Drawing.Size(75, 26);
            this.buttonMedianFilterFBDConfirm.TabIndex = 2;
            this.buttonMedianFilterFBDConfirm.Text = "确定并退出";
            this.buttonMedianFilterFBDConfirm.UseVisualStyleBackColor = true;
            this.buttonMedianFilterFBDConfirm.Click += new System.EventHandler(this.buttonMedianFilterFBDConfirm_Click);
            // 
            // buttonMedianFilterFBDCancel
            // 
            this.buttonMedianFilterFBDCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonMedianFilterFBDCancel.Font = new System.Drawing.Font("宋体", 9F);
            this.buttonMedianFilterFBDCancel.Location = new System.Drawing.Point(93, 340);
            this.buttonMedianFilterFBDCancel.Name = "buttonMedianFilterFBDCancel";
            this.buttonMedianFilterFBDCancel.Size = new System.Drawing.Size(74, 26);
            this.buttonMedianFilterFBDCancel.TabIndex = 3;
            this.buttonMedianFilterFBDCancel.Text = "取消并退出";
            this.buttonMedianFilterFBDCancel.UseVisualStyleBackColor = true;
            this.buttonMedianFilterFBDCancel.Click += new System.EventHandler(this.buttonMedianFilterFBDCancel_Click);
            // 
            // buttongetImageFrame
            // 
            this.buttongetImageFrame.Location = new System.Drawing.Point(12, 12);
            this.buttongetImageFrame.Name = "buttongetImageFrame";
            this.buttongetImageFrame.Size = new System.Drawing.Size(94, 27);
            this.buttongetImageFrame.TabIndex = 4;
            this.buttongetImageFrame.Text = "获取一幅图像";
            this.buttongetImageFrame.UseVisualStyleBackColor = true;
            this.buttongetImageFrame.Click += new System.EventHandler(this.buttongetImageFrame_Click);
            // 
            // openFileDialogOpenImage
            // 
            this.openFileDialogOpenImage.FileName = "openFileDialog1";
            // 
            // MedianFilterFBDForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(613, 394);
            this.Controls.Add(this.buttongetImageFrame);
            this.Controls.Add(this.buttonMedianFilterFBDCancel);
            this.Controls.Add(this.buttonMedianFilterFBDConfirm);
            this.Controls.Add(this.groupBoxMedianFilterFBDMaskSize);
            this.Controls.Add(this.pictureBoxMedian);
            this.MinimumSize = new System.Drawing.Size(200, 432);
            this.Name = "MedianFilterFBDForm";
            this.Text = "中值滤波";
            this.Load += new System.EventHandler(this.MedianFilterFBDForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMedian)).EndInit();
            this.groupBoxMedianFilterFBDMaskSize.ResumeLayout(false);
            this.groupBoxMedianFilterFBDMaskSize.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxMedian;
        private System.Windows.Forms.GroupBox groupBoxMedianFilterFBDMaskSize;
        private System.Windows.Forms.RadioButton radioButtonMedianFilter11;
        private System.Windows.Forms.RadioButton radioButtonMedianFilter9;
        private System.Windows.Forms.RadioButton radioButtonMedianFilter7;
        private System.Windows.Forms.RadioButton radioButtonMedianFilter5;
        private System.Windows.Forms.RadioButton radioButtonMedianFilter3;
        private System.Windows.Forms.Button buttonMedianFilterFBDConfirm;
        private System.Windows.Forms.Button buttonMedianFilterFBDCancel;
        private System.Windows.Forms.Button buttongetImageFrame;
        private System.Windows.Forms.OpenFileDialog openFileDialogOpenImage;
    }
}