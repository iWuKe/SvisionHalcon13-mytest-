namespace Svision
{
    partial class NewProcessEdit
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
            this.PrcEditControlPannel = new System.Windows.Forms.FlowLayoutPanel();
            this.btnInsert = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnMoveUp = new System.Windows.Forms.Button();
            this.btnMoveDown = new System.Windows.Forms.Button();
            this.DgvMainCode = new System.Windows.Forms.DataGridView();
            this.treeViewFuncBlk = new System.Windows.Forms.TreeView();
            this.PrcEditControlPannel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DgvMainCode)).BeginInit();
            this.SuspendLayout();
            // 
            // PrcEditControlPannel
            // 
            this.PrcEditControlPannel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.PrcEditControlPannel.Controls.Add(this.btnInsert);
            this.PrcEditControlPannel.Controls.Add(this.btnDelete);
            this.PrcEditControlPannel.Controls.Add(this.btnMoveUp);
            this.PrcEditControlPannel.Controls.Add(this.btnMoveDown);
            this.PrcEditControlPannel.Location = new System.Drawing.Point(4, 12);
            this.PrcEditControlPannel.Name = "PrcEditControlPannel";
            this.PrcEditControlPannel.Size = new System.Drawing.Size(111, 373);
            this.PrcEditControlPannel.TabIndex = 1;
            // 
            // btnInsert
            // 
            this.btnInsert.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnInsert.Location = new System.Drawing.Point(3, 3);
            this.btnInsert.Name = "btnInsert";
            this.btnInsert.Size = new System.Drawing.Size(67, 25);
            this.btnInsert.TabIndex = 0;
            this.btnInsert.Text = "插入";
            this.btnInsert.UseVisualStyleBackColor = true;
            this.btnInsert.Click += new System.EventHandler(this.btnInsert_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnDelete.Location = new System.Drawing.Point(3, 34);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(67, 25);
            this.btnDelete.TabIndex = 1;
            this.btnDelete.Text = "删除";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnMoveUp
            // 
            this.btnMoveUp.Enabled = false;
            this.btnMoveUp.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnMoveUp.Location = new System.Drawing.Point(3, 65);
            this.btnMoveUp.Name = "btnMoveUp";
            this.btnMoveUp.Size = new System.Drawing.Size(67, 25);
            this.btnMoveUp.TabIndex = 2;
            this.btnMoveUp.Text = "上移";
            this.btnMoveUp.UseVisualStyleBackColor = true;
            // 
            // btnMoveDown
            // 
            this.btnMoveDown.Enabled = false;
            this.btnMoveDown.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnMoveDown.Location = new System.Drawing.Point(3, 96);
            this.btnMoveDown.Name = "btnMoveDown";
            this.btnMoveDown.Size = new System.Drawing.Size(67, 25);
            this.btnMoveDown.TabIndex = 3;
            this.btnMoveDown.Text = "下移";
            this.btnMoveDown.UseVisualStyleBackColor = true;
            // 
            // DgvMainCode
            // 
            this.DgvMainCode.AllowUserToOrderColumns = true;
            this.DgvMainCode.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DgvMainCode.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DgvMainCode.Location = new System.Drawing.Point(121, 12);
            this.DgvMainCode.MultiSelect = false;
            this.DgvMainCode.Name = "DgvMainCode";
            this.DgvMainCode.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.DgvMainCode.RowTemplate.Height = 23;
            this.DgvMainCode.Size = new System.Drawing.Size(222, 373);
            this.DgvMainCode.TabIndex = 0;
            this.DgvMainCode.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvMainCode_CellClick);
            this.DgvMainCode.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvMainCode_CellMouseDoubleClick);
            this.DgvMainCode.SizeChanged += new System.EventHandler(this.DgvMainCode_SizeChanged);
            // 
            // treeViewFuncBlk
            // 
            this.treeViewFuncBlk.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeViewFuncBlk.ForeColor = System.Drawing.SystemColors.WindowText;
            this.treeViewFuncBlk.Location = new System.Drawing.Point(349, 12);
            this.treeViewFuncBlk.Name = "treeViewFuncBlk";
            this.treeViewFuncBlk.Size = new System.Drawing.Size(231, 373);
            this.treeViewFuncBlk.TabIndex = 0;
            this.treeViewFuncBlk.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewFuncBlk_AfterSelect);
            // 
            // NewProcessEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(592, 391);
            this.Controls.Add(this.PrcEditControlPannel);
            this.Controls.Add(this.DgvMainCode);
            this.Controls.Add(this.treeViewFuncBlk);
            this.MinimumSize = new System.Drawing.Size(608, 429);
            this.Name = "NewProcessEdit";
            this.Text = "作业编辑";
            this.PrcEditControlPannel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DgvMainCode)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel PrcEditControlPannel;
        private System.Windows.Forms.TreeView treeViewFuncBlk;
        private System.Windows.Forms.Button btnInsert;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnMoveUp;
        private System.Windows.Forms.Button btnMoveDown;
        private System.Windows.Forms.DataGridView DgvMainCode;
    }
}