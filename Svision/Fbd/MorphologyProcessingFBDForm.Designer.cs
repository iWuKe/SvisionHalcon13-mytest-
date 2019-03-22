namespace Svision
{
    partial class MorphologyProcessingFBDForm
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
            this.pictureBoxMorphologyProcessing = new System.Windows.Forms.PictureBox();
            this.buttonGetImageFrame = new System.Windows.Forms.Button();
            this.comboBoxMorphology = new System.Windows.Forms.ComboBox();
            this.labelRadius = new System.Windows.Forms.Label();
            this.numericUpDownRadius = new System.Windows.Forms.NumericUpDown();
            this.panelElement = new System.Windows.Forms.Panel();
            this.panelRectangle = new System.Windows.Forms.Panel();
            this.numericUpDownWidth = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownHeight = new System.Windows.Forms.NumericUpDown();
            this.labelwidth = new System.Windows.Forms.Label();
            this.labelHeight = new System.Windows.Forms.Label();
            this.radioButtonCircle = new System.Windows.Forms.RadioButton();
            this.radioButtonRectangle = new System.Windows.Forms.RadioButton();
            this.panelCircle = new System.Windows.Forms.Panel();
            this.buttonConfirm = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMorphologyProcessing)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRadius)).BeginInit();
            this.panelElement.SuspendLayout();
            this.panelRectangle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHeight)).BeginInit();
            this.panelCircle.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBoxMorphologyProcessing
            // 
            this.pictureBoxMorphologyProcessing.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBoxMorphologyProcessing.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.pictureBoxMorphologyProcessing.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBoxMorphologyProcessing.Location = new System.Drawing.Point(173, 8);
            this.pictureBoxMorphologyProcessing.Name = "pictureBoxMorphologyProcessing";
            this.pictureBoxMorphologyProcessing.Size = new System.Drawing.Size(432, 360);
            this.pictureBoxMorphologyProcessing.TabIndex = 1;
            this.pictureBoxMorphologyProcessing.TabStop = false;
            this.pictureBoxMorphologyProcessing.SizeChanged += new System.EventHandler(this.pictureBoxMorphologyProcessing_SizeChanged);
            // 
            // buttonGetImageFrame
            // 
            this.buttonGetImageFrame.Location = new System.Drawing.Point(12, 12);
            this.buttonGetImageFrame.Name = "buttonGetImageFrame";
            this.buttonGetImageFrame.Size = new System.Drawing.Size(117, 29);
            this.buttonGetImageFrame.TabIndex = 11;
            this.buttonGetImageFrame.Text = "获取一幅图像";
            this.buttonGetImageFrame.UseVisualStyleBackColor = true;
            this.buttonGetImageFrame.Click += new System.EventHandler(this.buttonGetImageFrame_Click);
            // 
            // comboBoxMorphology
            // 
            this.comboBoxMorphology.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxMorphology.FormattingEnabled = true;
            this.comboBoxMorphology.Items.AddRange(new object[] {
            "图像腐蚀",
            "图像膨胀",
            "图像开运算",
            "图像闭运算"});
            this.comboBoxMorphology.Location = new System.Drawing.Point(12, 60);
            this.comboBoxMorphology.Name = "comboBoxMorphology";
            this.comboBoxMorphology.Size = new System.Drawing.Size(121, 20);
            this.comboBoxMorphology.TabIndex = 12;
            this.comboBoxMorphology.SelectedIndexChanged += new System.EventHandler(this.comboBoxMorphology_SelectedIndexChanged);
            // 
            // labelRadius
            // 
            this.labelRadius.AutoSize = true;
            this.labelRadius.Location = new System.Drawing.Point(11, 24);
            this.labelRadius.Name = "labelRadius";
            this.labelRadius.Size = new System.Drawing.Size(41, 12);
            this.labelRadius.TabIndex = 13;
            this.labelRadius.Text = "半径：";
            // 
            // numericUpDownRadius
            // 
            this.numericUpDownRadius.DecimalPlaces = 1;
            this.numericUpDownRadius.Location = new System.Drawing.Point(58, 22);
            this.numericUpDownRadius.Maximum = new decimal(new int[] {
            5115,
            0,
            0,
            65536});
            this.numericUpDownRadius.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.numericUpDownRadius.Name = "numericUpDownRadius";
            this.numericUpDownRadius.Size = new System.Drawing.Size(56, 21);
            this.numericUpDownRadius.TabIndex = 14;
            this.numericUpDownRadius.Value = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.numericUpDownRadius.ValueChanged += new System.EventHandler(this.numericUpDownRadius_ValueChanged);
            // 
            // panelElement
            // 
            this.panelElement.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.panelElement.Controls.Add(this.panelRectangle);
            this.panelElement.Controls.Add(this.radioButtonCircle);
            this.panelElement.Controls.Add(this.radioButtonRectangle);
            this.panelElement.Location = new System.Drawing.Point(12, 86);
            this.panelElement.Name = "panelElement";
            this.panelElement.Size = new System.Drawing.Size(155, 253);
            this.panelElement.TabIndex = 15;
            // 
            // panelRectangle
            // 
            this.panelRectangle.Controls.Add(this.numericUpDownWidth);
            this.panelRectangle.Controls.Add(this.numericUpDownHeight);
            this.panelRectangle.Controls.Add(this.labelwidth);
            this.panelRectangle.Controls.Add(this.labelHeight);
            this.panelRectangle.Location = new System.Drawing.Point(3, 35);
            this.panelRectangle.Name = "panelRectangle";
            this.panelRectangle.Size = new System.Drawing.Size(149, 73);
            this.panelRectangle.TabIndex = 19;
            // 
            // numericUpDownWidth
            // 
            this.numericUpDownWidth.Location = new System.Drawing.Point(58, 9);
            this.numericUpDownWidth.Maximum = new decimal(new int[] {
            511,
            0,
            0,
            0});
            this.numericUpDownWidth.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownWidth.Name = "numericUpDownWidth";
            this.numericUpDownWidth.Size = new System.Drawing.Size(57, 21);
            this.numericUpDownWidth.TabIndex = 15;
            this.numericUpDownWidth.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownWidth.ValueChanged += new System.EventHandler(this.numericUpDownWidth_ValueChanged);
            // 
            // numericUpDownHeight
            // 
            this.numericUpDownHeight.Location = new System.Drawing.Point(59, 37);
            this.numericUpDownHeight.Maximum = new decimal(new int[] {
            511,
            0,
            0,
            0});
            this.numericUpDownHeight.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownHeight.Name = "numericUpDownHeight";
            this.numericUpDownHeight.Size = new System.Drawing.Size(56, 21);
            this.numericUpDownHeight.TabIndex = 18;
            this.numericUpDownHeight.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownHeight.ValueChanged += new System.EventHandler(this.numericUpDownHeight_ValueChanged);
            // 
            // labelwidth
            // 
            this.labelwidth.AutoSize = true;
            this.labelwidth.Location = new System.Drawing.Point(11, 11);
            this.labelwidth.Name = "labelwidth";
            this.labelwidth.Size = new System.Drawing.Size(41, 12);
            this.labelwidth.TabIndex = 16;
            this.labelwidth.Text = "宽度：";
            // 
            // labelHeight
            // 
            this.labelHeight.AutoSize = true;
            this.labelHeight.Location = new System.Drawing.Point(12, 39);
            this.labelHeight.Name = "labelHeight";
            this.labelHeight.Size = new System.Drawing.Size(41, 12);
            this.labelHeight.TabIndex = 17;
            this.labelHeight.Text = "高度：";
            // 
            // radioButtonCircle
            // 
            this.radioButtonCircle.AutoSize = true;
            this.radioButtonCircle.Location = new System.Drawing.Point(3, 114);
            this.radioButtonCircle.Name = "radioButtonCircle";
            this.radioButtonCircle.Size = new System.Drawing.Size(95, 16);
            this.radioButtonCircle.TabIndex = 1;
            this.radioButtonCircle.Text = "圆形结构元素";
            this.radioButtonCircle.UseVisualStyleBackColor = true;
            this.radioButtonCircle.CheckedChanged += new System.EventHandler(this.radioButtonCircle_CheckedChanged);
            // 
            // radioButtonRectangle
            // 
            this.radioButtonRectangle.AutoSize = true;
            this.radioButtonRectangle.Checked = true;
            this.radioButtonRectangle.Location = new System.Drawing.Point(3, 3);
            this.radioButtonRectangle.Name = "radioButtonRectangle";
            this.radioButtonRectangle.Size = new System.Drawing.Size(95, 16);
            this.radioButtonRectangle.TabIndex = 0;
            this.radioButtonRectangle.TabStop = true;
            this.radioButtonRectangle.Text = "矩形结构元素";
            this.radioButtonRectangle.UseVisualStyleBackColor = true;
            this.radioButtonRectangle.CheckedChanged += new System.EventHandler(this.radioButtonRectangle_CheckedChanged);
            // 
            // panelCircle
            // 
            this.panelCircle.Controls.Add(this.labelRadius);
            this.panelCircle.Controls.Add(this.numericUpDownRadius);
            this.panelCircle.Enabled = false;
            this.panelCircle.Location = new System.Drawing.Point(15, 222);
            this.panelCircle.Name = "panelCircle";
            this.panelCircle.Size = new System.Drawing.Size(149, 59);
            this.panelCircle.TabIndex = 20;
            // 
            // buttonConfirm
            // 
            this.buttonConfirm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonConfirm.Location = new System.Drawing.Point(12, 345);
            this.buttonConfirm.Name = "buttonConfirm";
            this.buttonConfirm.Size = new System.Drawing.Size(75, 23);
            this.buttonConfirm.TabIndex = 16;
            this.buttonConfirm.Text = "确认并退出";
            this.buttonConfirm.UseVisualStyleBackColor = true;
            this.buttonConfirm.Click += new System.EventHandler(this.buttonConfirm_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonCancel.Location = new System.Drawing.Point(92, 345);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 17;
            this.buttonCancel.Text = "取消并退出";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // MorphologyProcessingFBDForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(617, 380);
            this.Controls.Add(this.panelCircle);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonConfirm);
            this.Controls.Add(this.panelElement);
            this.Controls.Add(this.comboBoxMorphology);
            this.Controls.Add(this.buttonGetImageFrame);
            this.Controls.Add(this.pictureBoxMorphologyProcessing);
            this.MinimumSize = new System.Drawing.Size(400, 418);
            this.Name = "MorphologyProcessingFBDForm";
            this.Text = "图像形态学处理";
            this.Load += new System.EventHandler(this.MorphologyProcessingFBDForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMorphologyProcessing)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRadius)).EndInit();
            this.panelElement.ResumeLayout(false);
            this.panelElement.PerformLayout();
            this.panelRectangle.ResumeLayout(false);
            this.panelRectangle.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHeight)).EndInit();
            this.panelCircle.ResumeLayout(false);
            this.panelCircle.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxMorphologyProcessing;
        private System.Windows.Forms.Button buttonGetImageFrame;
        private System.Windows.Forms.ComboBox comboBoxMorphology;
        private System.Windows.Forms.Label labelRadius;
        private System.Windows.Forms.NumericUpDown numericUpDownRadius;
        private System.Windows.Forms.Panel panelElement;
        private System.Windows.Forms.RadioButton radioButtonCircle;
        private System.Windows.Forms.RadioButton radioButtonRectangle;
        private System.Windows.Forms.NumericUpDown numericUpDownWidth;
        private System.Windows.Forms.Panel panelCircle;
        private System.Windows.Forms.Panel panelRectangle;
        private System.Windows.Forms.NumericUpDown numericUpDownHeight;
        private System.Windows.Forms.Label labelwidth;
        private System.Windows.Forms.Label labelHeight;
        private System.Windows.Forms.Button buttonConfirm;
        private System.Windows.Forms.Button buttonCancel;
    }
}