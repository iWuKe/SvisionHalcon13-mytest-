namespace Svision
{
    partial class OutputParameters
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
            this.checkListBoxOutputParameters = new System.Windows.Forms.CheckedListBox();
            this.SuspendLayout();
            // 
            // checkListBoxOutputParameters
            // 
            this.checkListBoxOutputParameters.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.checkListBoxOutputParameters.CheckOnClick = true;
            this.checkListBoxOutputParameters.FormattingEnabled = true;
            this.checkListBoxOutputParameters.Location = new System.Drawing.Point(4, 4);
            this.checkListBoxOutputParameters.Name = "checkListBoxOutputParameters";
            this.checkListBoxOutputParameters.Size = new System.Drawing.Size(320, 372);
            this.checkListBoxOutputParameters.TabIndex = 0;
            this.checkListBoxOutputParameters.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.checkListBoxOutputParameters_ItemCheck);
            // 
            // OutputParameters
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(328, 378);
            this.Controls.Add(this.checkListBoxOutputParameters);
            this.MinimumSize = new System.Drawing.Size(344, 416);
            this.Name = "OutputParameters";
            this.Text = "输出显示";
            this.Load += new System.EventHandler(this.OutputParameters_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckedListBox checkListBoxOutputParameters;
    }
}