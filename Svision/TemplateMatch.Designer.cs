namespace Svision
{
    partial class TemplateMatch
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
            this.TMImage = new System.Windows.Forms.PictureBox();
            this.TMTab = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.LoadImage = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.TMregion = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.TMImage)).BeginInit();
            this.TMTab.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // TMImage
            // 
            this.TMImage.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.TMImage.Location = new System.Drawing.Point(13, 17);
            this.TMImage.Name = "TMImage";
            this.TMImage.Size = new System.Drawing.Size(717, 538);
            this.TMImage.TabIndex = 0;
            this.TMImage.TabStop = false;
            // 
            // TMTab
            // 
            this.TMTab.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.TMTab.Controls.Add(this.tabPage1);
            this.TMTab.Controls.Add(this.tabPage2);
            this.TMTab.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.TMTab.ItemSize = new System.Drawing.Size(156, 35);
            this.TMTab.Location = new System.Drawing.Point(740, 17);
            this.TMTab.Name = "TMTab";
            this.TMTab.SelectedIndex = 0;
            this.TMTab.Size = new System.Drawing.Size(335, 538);
            this.TMTab.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.TMTab.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.pictureBox1);
            this.tabPage1.Controls.Add(this.TMregion);
            this.tabPage1.Controls.Add(this.LoadImage);
            this.tabPage1.Location = new System.Drawing.Point(4, 39);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(327, 495);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "模板设置";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // LoadImage
            // 
            this.LoadImage.Location = new System.Drawing.Point(18, 20);
            this.LoadImage.Name = "LoadImage";
            this.LoadImage.Size = new System.Drawing.Size(120, 29);
            this.LoadImage.TabIndex = 0;
            this.LoadImage.Text = "加载训练图像";
            this.LoadImage.UseVisualStyleBackColor = true;
            this.LoadImage.Click += new System.EventHandler(this.LoadImage_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 39);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(327, 495);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "识别设置";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // TMregion
            // 
            this.TMregion.Location = new System.Drawing.Point(18, 78);
            this.TMregion.Name = "TMregion";
            this.TMregion.Size = new System.Drawing.Size(120, 29);
            this.TMregion.TabIndex = 1;
            this.TMregion.Text = "创建模板区域";
            this.TMregion.UseVisualStyleBackColor = true;
            this.TMregion.Click += new System.EventHandler(this.TMregion_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(167, 20);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(140, 87);
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // TemplateMatch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1084, 572);
            this.Controls.Add(this.TMTab);
            this.Controls.Add(this.TMImage);
            this.Name = "TemplateMatch";
            this.Text = "TemplateMatch";
            this.Load += new System.EventHandler(this.TemplateMatch_Load);
            ((System.ComponentModel.ISupportInitialize)(this.TMImage)).EndInit();
            this.TMTab.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox TMImage;
        private System.Windows.Forms.TabControl TMTab;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button LoadImage;
        private System.Windows.Forms.Button TMregion;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}