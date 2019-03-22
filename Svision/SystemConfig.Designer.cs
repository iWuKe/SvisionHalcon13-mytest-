namespace Svision
{
    partial class SystemConfig
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
            this.checkBoxSystemCfgEnableBoot = new System.Windows.Forms.CheckBox();
            this.textBoxSystemCfgBootProgram = new System.Windows.Forms.TextBox();
            this.labelSystemCfgBootProgram = new System.Windows.Forms.Label();
            this.buttonSystemCfgBootProgram = new System.Windows.Forms.Button();
            this.buttonSystemCfgBootConfirm = new System.Windows.Forms.Button();
            this.buttonSystemCfgBootCancel = new System.Windows.Forms.Button();
            this.folderBrowserDialogSystemConfig = new System.Windows.Forms.FolderBrowserDialog();
            this.SuspendLayout();
            // 
            // checkBoxSystemCfgEnableBoot
            // 
            this.checkBoxSystemCfgEnableBoot.AutoSize = true;
            this.checkBoxSystemCfgEnableBoot.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.checkBoxSystemCfgEnableBoot.Location = new System.Drawing.Point(27, 51);
            this.checkBoxSystemCfgEnableBoot.Margin = new System.Windows.Forms.Padding(4);
            this.checkBoxSystemCfgEnableBoot.Name = "checkBoxSystemCfgEnableBoot";
            this.checkBoxSystemCfgEnableBoot.Size = new System.Drawing.Size(120, 16);
            this.checkBoxSystemCfgEnableBoot.TabIndex = 0;
            this.checkBoxSystemCfgEnableBoot.Text = "是否启用BOOT程序";
            this.checkBoxSystemCfgEnableBoot.UseVisualStyleBackColor = true;
            this.checkBoxSystemCfgEnableBoot.CheckedChanged += new System.EventHandler(this.checkBoxSystemCfgEnableBoot_CheckedChanged);
            // 
            // textBoxSystemCfgBootProgram
            // 
            this.textBoxSystemCfgBootProgram.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxSystemCfgBootProgram.Location = new System.Drawing.Point(27, 170);
            this.textBoxSystemCfgBootProgram.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxSystemCfgBootProgram.Name = "textBoxSystemCfgBootProgram";
            this.textBoxSystemCfgBootProgram.Size = new System.Drawing.Size(322, 27);
            this.textBoxSystemCfgBootProgram.TabIndex = 16;
            // 
            // labelSystemCfgBootProgram
            // 
            this.labelSystemCfgBootProgram.AutoSize = true;
            this.labelSystemCfgBootProgram.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelSystemCfgBootProgram.Location = new System.Drawing.Point(24, 116);
            this.labelSystemCfgBootProgram.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelSystemCfgBootProgram.Name = "labelSystemCfgBootProgram";
            this.labelSystemCfgBootProgram.Size = new System.Drawing.Size(77, 12);
            this.labelSystemCfgBootProgram.TabIndex = 17;
            this.labelSystemCfgBootProgram.Text = "BOOT程序路径";
            // 
            // buttonSystemCfgBootProgram
            // 
            this.buttonSystemCfgBootProgram.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonSystemCfgBootProgram.Location = new System.Drawing.Point(267, 108);
            this.buttonSystemCfgBootProgram.Margin = new System.Windows.Forms.Padding(4);
            this.buttonSystemCfgBootProgram.Name = "buttonSystemCfgBootProgram";
            this.buttonSystemCfgBootProgram.Size = new System.Drawing.Size(60, 26);
            this.buttonSystemCfgBootProgram.TabIndex = 18;
            this.buttonSystemCfgBootProgram.Text = "浏览";
            this.buttonSystemCfgBootProgram.UseVisualStyleBackColor = true;
            this.buttonSystemCfgBootProgram.Click += new System.EventHandler(this.buttonSystemCfgBootProgram_Click);
            // 
            // buttonSystemCfgBootConfirm
            // 
            this.buttonSystemCfgBootConfirm.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonSystemCfgBootConfirm.Location = new System.Drawing.Point(185, 226);
            this.buttonSystemCfgBootConfirm.Name = "buttonSystemCfgBootConfirm";
            this.buttonSystemCfgBootConfirm.Size = new System.Drawing.Size(82, 25);
            this.buttonSystemCfgBootConfirm.TabIndex = 20;
            this.buttonSystemCfgBootConfirm.Text = "确定并退出";
            this.buttonSystemCfgBootConfirm.UseVisualStyleBackColor = true;
            this.buttonSystemCfgBootConfirm.Click += new System.EventHandler(this.buttonSystemCfgBootConfirm_Click);
            // 
            // buttonSystemCfgBootCancel
            // 
            this.buttonSystemCfgBootCancel.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonSystemCfgBootCancel.Location = new System.Drawing.Point(273, 226);
            this.buttonSystemCfgBootCancel.Name = "buttonSystemCfgBootCancel";
            this.buttonSystemCfgBootCancel.Size = new System.Drawing.Size(76, 25);
            this.buttonSystemCfgBootCancel.TabIndex = 21;
            this.buttonSystemCfgBootCancel.Text = "取消并退出";
            this.buttonSystemCfgBootCancel.UseVisualStyleBackColor = true;
            this.buttonSystemCfgBootCancel.Click += new System.EventHandler(this.buttonSystemCfgBootCancel_Click);
            // 
            // SystemConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(362, 273);
            this.Controls.Add(this.buttonSystemCfgBootCancel);
            this.Controls.Add(this.buttonSystemCfgBootConfirm);
            this.Controls.Add(this.buttonSystemCfgBootProgram);
            this.Controls.Add(this.labelSystemCfgBootProgram);
            this.Controls.Add(this.textBoxSystemCfgBootProgram);
            this.Controls.Add(this.checkBoxSystemCfgEnableBoot);
            this.Font = new System.Drawing.Font("宋体", 13F);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(378, 311);
            this.Name = "SystemConfig";
            this.Text = "系统配置";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox checkBoxSystemCfgEnableBoot;
        private System.Windows.Forms.TextBox textBoxSystemCfgBootProgram;
        private System.Windows.Forms.Label labelSystemCfgBootProgram;
        private System.Windows.Forms.Button buttonSystemCfgBootProgram;
        private System.Windows.Forms.Button buttonSystemCfgBootConfirm;
        private System.Windows.Forms.Button buttonSystemCfgBootCancel;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialogSystemConfig;
    }
}