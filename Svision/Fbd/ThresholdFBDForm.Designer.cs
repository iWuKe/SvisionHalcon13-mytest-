namespace Svision
{
    partial class ThresholdFBDForm
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
            this.labelMaxGray = new System.Windows.Forms.Label();
            this.labelMinGray = new System.Windows.Forms.Label();
            this.trackBarMaxGray = new System.Windows.Forms.TrackBar();
            this.trackBarMinGray = new System.Windows.Forms.TrackBar();
            this.buttonConfirm = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.pictureBoxThreshold = new System.Windows.Forms.PictureBox();
            this.buttonGetImageFrame = new System.Windows.Forms.Button();
            this.panelenable = new System.Windows.Forms.Panel();
            this.numericUpDownMinGray = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownMaxGray = new System.Windows.Forms.NumericUpDown();
            this.openFileDialogOpenImage = new System.Windows.Forms.OpenFileDialog();
            this.hWindowControl1 = new HalconDotNet.HWindowControl();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarMaxGray)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarMinGray)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxThreshold)).BeginInit();
            this.panelenable.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMinGray)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMaxGray)).BeginInit();
            this.SuspendLayout();
            // 
            // labelMaxGray
            // 
            this.labelMaxGray.AutoSize = true;
            this.labelMaxGray.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelMaxGray.Location = new System.Drawing.Point(8, 12);
            this.labelMaxGray.Name = "labelMaxGray";
            this.labelMaxGray.Size = new System.Drawing.Size(53, 12);
            this.labelMaxGray.TabIndex = 3;
            this.labelMaxGray.Text = "最大灰度";
            // 
            // labelMinGray
            // 
            this.labelMinGray.AutoSize = true;
            this.labelMinGray.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelMinGray.Location = new System.Drawing.Point(8, 131);
            this.labelMinGray.Name = "labelMinGray";
            this.labelMinGray.Size = new System.Drawing.Size(53, 12);
            this.labelMinGray.TabIndex = 4;
            this.labelMinGray.Text = "最小灰度";
            // 
            // trackBarMaxGray
            // 
            this.trackBarMaxGray.Location = new System.Drawing.Point(3, 63);
            this.trackBarMaxGray.Maximum = 255;
            this.trackBarMaxGray.Name = "trackBarMaxGray";
            this.trackBarMaxGray.Size = new System.Drawing.Size(79, 45);
            this.trackBarMaxGray.TabIndex = 5;
            this.trackBarMaxGray.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBarMaxGray.Scroll += new System.EventHandler(this.trackBarMaxGray_Scroll);
            // 
            // trackBarMinGray
            // 
            this.trackBarMinGray.Location = new System.Drawing.Point(3, 183);
            this.trackBarMinGray.Maximum = 255;
            this.trackBarMinGray.Name = "trackBarMinGray";
            this.trackBarMinGray.Size = new System.Drawing.Size(79, 45);
            this.trackBarMinGray.TabIndex = 6;
            this.trackBarMinGray.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBarMinGray.Scroll += new System.EventHandler(this.trackBarMinGray_Scroll);
            // 
            // buttonConfirm
            // 
            this.buttonConfirm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonConfirm.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonConfirm.Location = new System.Drawing.Point(16, 318);
            this.buttonConfirm.Name = "buttonConfirm";
            this.buttonConfirm.Size = new System.Drawing.Size(80, 27);
            this.buttonConfirm.TabIndex = 7;
            this.buttonConfirm.Text = "确定并退出";
            this.buttonConfirm.UseVisualStyleBackColor = true;
            this.buttonConfirm.Click += new System.EventHandler(this.buttonConfirm_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonCancel.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonCancel.Location = new System.Drawing.Point(16, 351);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(81, 27);
            this.buttonCancel.TabIndex = 8;
            this.buttonCancel.Text = "取消并退出";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // pictureBoxThreshold
            // 
            this.pictureBoxThreshold.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBoxThreshold.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.pictureBoxThreshold.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBoxThreshold.Location = new System.Drawing.Point(131, 12);
            this.pictureBoxThreshold.Name = "pictureBoxThreshold";
            this.pictureBoxThreshold.Size = new System.Drawing.Size(479, 366);
            this.pictureBoxThreshold.TabIndex = 9;
            this.pictureBoxThreshold.TabStop = false;
            this.pictureBoxThreshold.SizeChanged += new System.EventHandler(this.pictureBoxThreshold_SizeChanged);
            // 
            // buttonGetImageFrame
            // 
            this.buttonGetImageFrame.Location = new System.Drawing.Point(15, 12);
            this.buttonGetImageFrame.Name = "buttonGetImageFrame";
            this.buttonGetImageFrame.Size = new System.Drawing.Size(92, 25);
            this.buttonGetImageFrame.TabIndex = 10;
            this.buttonGetImageFrame.Text = "获取一幅图像";
            this.buttonGetImageFrame.UseVisualStyleBackColor = true;
            this.buttonGetImageFrame.Click += new System.EventHandler(this.buttonGetImageFrame_Click);
            // 
            // panelenable
            // 
            this.panelenable.Controls.Add(this.hWindowControl1);
            this.panelenable.Controls.Add(this.numericUpDownMinGray);
            this.panelenable.Controls.Add(this.numericUpDownMaxGray);
            this.panelenable.Controls.Add(this.labelMaxGray);
            this.panelenable.Controls.Add(this.trackBarMaxGray);
            this.panelenable.Controls.Add(this.labelMinGray);
            this.panelenable.Controls.Add(this.trackBarMinGray);
            this.panelenable.Enabled = false;
            this.panelenable.Location = new System.Drawing.Point(16, 74);
            this.panelenable.Name = "panelenable";
            this.panelenable.Size = new System.Drawing.Size(110, 231);
            this.panelenable.TabIndex = 12;
            // 
            // numericUpDownMinGray
            // 
            this.numericUpDownMinGray.Location = new System.Drawing.Point(10, 156);
            this.numericUpDownMinGray.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numericUpDownMinGray.Name = "numericUpDownMinGray";
            this.numericUpDownMinGray.Size = new System.Drawing.Size(74, 21);
            this.numericUpDownMinGray.TabIndex = 8;
            this.numericUpDownMinGray.ValueChanged += new System.EventHandler(this.numericUpDownMinGray_ValueChanged);
            // 
            // numericUpDownMaxGray
            // 
            this.numericUpDownMaxGray.Location = new System.Drawing.Point(10, 36);
            this.numericUpDownMaxGray.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numericUpDownMaxGray.Name = "numericUpDownMaxGray";
            this.numericUpDownMaxGray.Size = new System.Drawing.Size(74, 21);
            this.numericUpDownMaxGray.TabIndex = 7;
            this.numericUpDownMaxGray.ValueChanged += new System.EventHandler(this.numericUpDownMaxGray_ValueChanged);
            // 
            // openFileDialogOpenImage
            // 
            this.openFileDialogOpenImage.FileName = "openFileDialog1";
            // 
            // hWindowControl1
            // 
            this.hWindowControl1.BackColor = System.Drawing.Color.Black;
            this.hWindowControl1.BorderColor = System.Drawing.Color.Black;
            this.hWindowControl1.ImagePart = new System.Drawing.Rectangle(0, 0, 640, 480);
            this.hWindowControl1.Location = new System.Drawing.Point(45, 47);
            this.hWindowControl1.Name = "hWindowControl1";
            this.hWindowControl1.Size = new System.Drawing.Size(8, 9);
            this.hWindowControl1.TabIndex = 9;
            this.hWindowControl1.WindowSize = new System.Drawing.Size(8, 9);
            // 
            // ThresholdFBDForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(622, 390);
            this.Controls.Add(this.panelenable);
            this.Controls.Add(this.buttonGetImageFrame);
            this.Controls.Add(this.pictureBoxThreshold);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonConfirm);
            this.MinimumSize = new System.Drawing.Size(400, 428);
            this.Name = "ThresholdFBDForm";
            this.Text = "二值化";
            this.Load += new System.EventHandler(this.ThresholdFBDForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.trackBarMaxGray)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarMinGray)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxThreshold)).EndInit();
            this.panelenable.ResumeLayout(false);
            this.panelenable.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMinGray)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMaxGray)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labelMaxGray;
        private System.Windows.Forms.Label labelMinGray;
        private System.Windows.Forms.TrackBar trackBarMaxGray;
        private System.Windows.Forms.TrackBar trackBarMinGray;
        private System.Windows.Forms.Button buttonConfirm;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.PictureBox pictureBoxThreshold;
        private System.Windows.Forms.Button buttonGetImageFrame;
        private System.Windows.Forms.Panel panelenable;
        private System.Windows.Forms.OpenFileDialog openFileDialogOpenImage;
        private System.Windows.Forms.NumericUpDown numericUpDownMaxGray;
        private System.Windows.Forms.NumericUpDown numericUpDownMinGray;
        private HalconDotNet.HWindowControl hWindowControl1;


    }
}