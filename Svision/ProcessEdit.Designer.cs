namespace Svision
{
    partial class ProcessEdit
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
            this.TemplaMatch = new System.Windows.Forms.Button();
            this.ProcessBox = new System.Windows.Forms.GroupBox();
            this.PretreatmentBox = new System.Windows.Forms.GroupBox();
            this.ImageBox = new System.Windows.Forms.GroupBox();
            this.OutputBox = new System.Windows.Forms.GroupBox();
            this.ProcessBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // TemplaMatch
            // 
            this.TemplaMatch.Location = new System.Drawing.Point(11, 34);
            this.TemplaMatch.Name = "TemplaMatch";
            this.TemplaMatch.Size = new System.Drawing.Size(75, 23);
            this.TemplaMatch.TabIndex = 0;
            this.TemplaMatch.Text = "模板匹配";
            this.TemplaMatch.UseVisualStyleBackColor = true;
            this.TemplaMatch.Click += new System.EventHandler(this.TemplaMatch_Click);
            // 
            // ProcessBox
            // 
            this.ProcessBox.Controls.Add(this.TemplaMatch);
            this.ProcessBox.Location = new System.Drawing.Point(1, 215);
            this.ProcessBox.Name = "ProcessBox";
            this.ProcessBox.Size = new System.Drawing.Size(352, 91);
            this.ProcessBox.TabIndex = 1;
            this.ProcessBox.TabStop = false;
            this.ProcessBox.Text = "处理工具";
            // 
            // PretreatmentBox
            // 
            this.PretreatmentBox.Location = new System.Drawing.Point(1, 109);
            this.PretreatmentBox.Name = "PretreatmentBox";
            this.PretreatmentBox.Size = new System.Drawing.Size(352, 100);
            this.PretreatmentBox.TabIndex = 2;
            this.PretreatmentBox.TabStop = false;
            this.PretreatmentBox.Text = "预处理工具";
            // 
            // ImageBox
            // 
            this.ImageBox.Location = new System.Drawing.Point(1, 3);
            this.ImageBox.Name = "ImageBox";
            this.ImageBox.Size = new System.Drawing.Size(340, 100);
            this.ImageBox.TabIndex = 3;
            this.ImageBox.TabStop = false;
            this.ImageBox.Text = "图像工具";
            // 
            // OutputBox
            // 
            this.OutputBox.Location = new System.Drawing.Point(1, 312);
            this.OutputBox.Name = "OutputBox";
            this.OutputBox.Size = new System.Drawing.Size(352, 100);
            this.OutputBox.TabIndex = 4;
            this.OutputBox.TabStop = false;
            this.OutputBox.Text = "输出工具";
            // 
            // ProcessEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(353, 413);
            this.Controls.Add(this.OutputBox);
            this.Controls.Add(this.ImageBox);
            this.Controls.Add(this.PretreatmentBox);
            this.Controls.Add(this.ProcessBox);
            this.Name = "ProcessEdit";
            this.Text = "流程编辑";
            this.ProcessBox.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button TemplaMatch;
        private System.Windows.Forms.GroupBox ProcessBox;
        private System.Windows.Forms.GroupBox PretreatmentBox;
        private System.Windows.Forms.GroupBox ImageBox;
        private System.Windows.Forms.GroupBox OutputBox;
    }
}