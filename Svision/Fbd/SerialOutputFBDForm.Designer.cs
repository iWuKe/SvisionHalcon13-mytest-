namespace Svision
{
    partial class SerialOutputFBDForm
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
            this.tabControlSerialOutput = new System.Windows.Forms.TabControl();
            this.tabPageSerialOutputOutput = new System.Windows.Forms.TabPage();
            this.buttonClearAll = new System.Windows.Forms.Button();
            this.groupBoxBatchOperation = new System.Windows.Forms.GroupBox();
            this.buttonBatchOperation = new System.Windows.Forms.Button();
            this.numericUpDownRecycleNumber = new System.Windows.Forms.NumericUpDown();
            this.labelRecycleNumber = new System.Windows.Forms.Label();
            this.groupBoxOperator = new System.Windows.Forms.GroupBox();
            this.buttonDown = new System.Windows.Forms.Button();
            this.checkedListBoxBatchOperation = new System.Windows.Forms.CheckedListBox();
            this.buttonUp = new System.Windows.Forms.Button();
            this.checkBoxFirstFieldIsNumber = new System.Windows.Forms.CheckBox();
            this.comboBoxSerialOutputExpressionSub = new System.Windows.Forms.ComboBox();
            this.comboBoxSerialOutputExpressionMain = new System.Windows.Forms.ComboBox();
            this.buttonSerialOutputDelete = new System.Windows.Forms.Button();
            this.buttonSerialOutputInsert = new System.Windows.Forms.Button();
            this.labelSerialOutputExpression = new System.Windows.Forms.Label();
            this.dataGridViewSerialOutputComData = new System.Windows.Forms.DataGridView();
            this.tabPageSerialOutputFormat = new System.Windows.Forms.TabPage();
            this.comboBoxSerialOutputRecordSeparator = new System.Windows.Forms.ComboBox();
            this.labelSerialOutputRecordSeparator = new System.Windows.Forms.Label();
            this.comboBoxOutputFieldSeparator = new System.Windows.Forms.ComboBox();
            this.labelSerialOutputFieldSeparator = new System.Windows.Forms.Label();
            this.groupBoxSerialOutputEraseZero = new System.Windows.Forms.GroupBox();
            this.radioButtonSerialOutputEraseZeroNo = new System.Windows.Forms.RadioButton();
            this.radioButtonSerialOutputEraseZeroYes = new System.Windows.Forms.RadioButton();
            this.groupBoxSerialBoxNegative = new System.Windows.Forms.GroupBox();
            this.radioButtonSerialOutputNegative8 = new System.Windows.Forms.RadioButton();
            this.radioButtonSerialOutputNegativeMinus = new System.Windows.Forms.RadioButton();
            this.comboBoxSerialOutputFloatBit = new System.Windows.Forms.ComboBox();
            this.comboBoxSerialOutputIntBit = new System.Windows.Forms.ComboBox();
            this.labelSerialOutputFloatBit = new System.Windows.Forms.Label();
            this.labelSerialOutputIntBit = new System.Windows.Forms.Label();
            this.labelSerialOutput = new System.Windows.Forms.Label();
            this.groupBoxSerialOutputFormat = new System.Windows.Forms.GroupBox();
            this.radioButtonSerialOutputFormatBin = new System.Windows.Forms.RadioButton();
            this.radioButtonSerialOutputFormatAscii = new System.Windows.Forms.RadioButton();
            this.groupBoxSerialOutputComMode = new System.Windows.Forms.GroupBox();
            this.radioButtonSerialOutputComModeEthernet = new System.Windows.Forms.RadioButton();
            this.radioButtonSerialOutputComModeRS232 = new System.Windows.Forms.RadioButton();
            this.buttonSerialOutputConfirm = new System.Windows.Forms.Button();
            this.buttonSerialOutputCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tabControlSerialOutput.SuspendLayout();
            this.tabPageSerialOutputOutput.SuspendLayout();
            this.groupBoxBatchOperation.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRecycleNumber)).BeginInit();
            this.groupBoxOperator.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSerialOutputComData)).BeginInit();
            this.tabPageSerialOutputFormat.SuspendLayout();
            this.groupBoxSerialOutputEraseZero.SuspendLayout();
            this.groupBoxSerialBoxNegative.SuspendLayout();
            this.groupBoxSerialOutputFormat.SuspendLayout();
            this.groupBoxSerialOutputComMode.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControlSerialOutput
            // 
            this.tabControlSerialOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlSerialOutput.Controls.Add(this.tabPageSerialOutputOutput);
            this.tabControlSerialOutput.Controls.Add(this.tabPageSerialOutputFormat);
            this.tabControlSerialOutput.Location = new System.Drawing.Point(13, 13);
            this.tabControlSerialOutput.Name = "tabControlSerialOutput";
            this.tabControlSerialOutput.SelectedIndex = 0;
            this.tabControlSerialOutput.Size = new System.Drawing.Size(436, 465);
            this.tabControlSerialOutput.TabIndex = 0;
            // 
            // tabPageSerialOutputOutput
            // 
            this.tabPageSerialOutputOutput.Controls.Add(this.label1);
            this.tabPageSerialOutputOutput.Controls.Add(this.buttonClearAll);
            this.tabPageSerialOutputOutput.Controls.Add(this.groupBoxBatchOperation);
            this.tabPageSerialOutputOutput.Controls.Add(this.comboBoxSerialOutputExpressionSub);
            this.tabPageSerialOutputOutput.Controls.Add(this.comboBoxSerialOutputExpressionMain);
            this.tabPageSerialOutputOutput.Controls.Add(this.buttonSerialOutputDelete);
            this.tabPageSerialOutputOutput.Controls.Add(this.buttonSerialOutputInsert);
            this.tabPageSerialOutputOutput.Controls.Add(this.labelSerialOutputExpression);
            this.tabPageSerialOutputOutput.Controls.Add(this.dataGridViewSerialOutputComData);
            this.tabPageSerialOutputOutput.Location = new System.Drawing.Point(4, 22);
            this.tabPageSerialOutputOutput.Name = "tabPageSerialOutputOutput";
            this.tabPageSerialOutputOutput.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageSerialOutputOutput.Size = new System.Drawing.Size(428, 439);
            this.tabPageSerialOutputOutput.TabIndex = 0;
            this.tabPageSerialOutputOutput.Text = "输出";
            this.tabPageSerialOutputOutput.UseVisualStyleBackColor = true;
            // 
            // buttonClearAll
            // 
            this.buttonClearAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClearAll.Location = new System.Drawing.Point(335, 252);
            this.buttonClearAll.Name = "buttonClearAll";
            this.buttonClearAll.Size = new System.Drawing.Size(87, 23);
            this.buttonClearAll.TabIndex = 11;
            this.buttonClearAll.Text = "清空输出列表";
            this.buttonClearAll.UseVisualStyleBackColor = true;
            this.buttonClearAll.Click += new System.EventHandler(this.buttonClearAll_Click);
            // 
            // groupBoxBatchOperation
            // 
            this.groupBoxBatchOperation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxBatchOperation.Controls.Add(this.buttonBatchOperation);
            this.groupBoxBatchOperation.Controls.Add(this.numericUpDownRecycleNumber);
            this.groupBoxBatchOperation.Controls.Add(this.labelRecycleNumber);
            this.groupBoxBatchOperation.Controls.Add(this.groupBoxOperator);
            this.groupBoxBatchOperation.Controls.Add(this.checkBoxFirstFieldIsNumber);
            this.groupBoxBatchOperation.Location = new System.Drawing.Point(6, 307);
            this.groupBoxBatchOperation.Name = "groupBoxBatchOperation";
            this.groupBoxBatchOperation.Size = new System.Drawing.Size(412, 129);
            this.groupBoxBatchOperation.TabIndex = 10;
            this.groupBoxBatchOperation.TabStop = false;
            this.groupBoxBatchOperation.Text = "批量操作";
            // 
            // buttonBatchOperation
            // 
            this.buttonBatchOperation.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonBatchOperation.Location = new System.Drawing.Point(234, 77);
            this.buttonBatchOperation.Name = "buttonBatchOperation";
            this.buttonBatchOperation.Size = new System.Drawing.Size(111, 23);
            this.buttonBatchOperation.TabIndex = 8;
            this.buttonBatchOperation.Text = "批量操作插入";
            this.buttonBatchOperation.UseVisualStyleBackColor = true;
            this.buttonBatchOperation.Click += new System.EventHandler(this.buttonBatchOperation_Click);
            // 
            // numericUpDownRecycleNumber
            // 
            this.numericUpDownRecycleNumber.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDownRecycleNumber.Location = new System.Drawing.Point(329, 50);
            this.numericUpDownRecycleNumber.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numericUpDownRecycleNumber.Name = "numericUpDownRecycleNumber";
            this.numericUpDownRecycleNumber.Size = new System.Drawing.Size(65, 21);
            this.numericUpDownRecycleNumber.TabIndex = 7;
            // 
            // labelRecycleNumber
            // 
            this.labelRecycleNumber.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.labelRecycleNumber.AutoSize = true;
            this.labelRecycleNumber.Location = new System.Drawing.Point(234, 52);
            this.labelRecycleNumber.Name = "labelRecycleNumber";
            this.labelRecycleNumber.Size = new System.Drawing.Size(89, 12);
            this.labelRecycleNumber.TabIndex = 6;
            this.labelRecycleNumber.Text = "循环目标个数：";
            // 
            // groupBoxOperator
            // 
            this.groupBoxOperator.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxOperator.Controls.Add(this.buttonDown);
            this.groupBoxOperator.Controls.Add(this.checkedListBoxBatchOperation);
            this.groupBoxOperator.Controls.Add(this.buttonUp);
            this.groupBoxOperator.Location = new System.Drawing.Point(6, 14);
            this.groupBoxOperator.Name = "groupBoxOperator";
            this.groupBoxOperator.Size = new System.Drawing.Size(211, 109);
            this.groupBoxOperator.TabIndex = 5;
            this.groupBoxOperator.TabStop = false;
            this.groupBoxOperator.Text = "循环字段操作";
            // 
            // buttonDown
            // 
            this.buttonDown.Location = new System.Drawing.Point(6, 44);
            this.buttonDown.Name = "buttonDown";
            this.buttonDown.Size = new System.Drawing.Size(50, 23);
            this.buttonDown.TabIndex = 4;
            this.buttonDown.Text = "下移";
            this.buttonDown.UseVisualStyleBackColor = true;
            this.buttonDown.Click += new System.EventHandler(this.buttonDown_Click);
            // 
            // checkedListBoxBatchOperation
            // 
            this.checkedListBoxBatchOperation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.checkedListBoxBatchOperation.FormattingEnabled = true;
            this.checkedListBoxBatchOperation.HorizontalScrollbar = true;
            this.checkedListBoxBatchOperation.Location = new System.Drawing.Point(62, 16);
            this.checkedListBoxBatchOperation.MultiColumn = true;
            this.checkedListBoxBatchOperation.Name = "checkedListBoxBatchOperation";
            this.checkedListBoxBatchOperation.Size = new System.Drawing.Size(143, 84);
            this.checkedListBoxBatchOperation.TabIndex = 0;
            this.checkedListBoxBatchOperation.SelectedIndexChanged += new System.EventHandler(this.checkedListBoxBatchOperation_SelectedIndexChanged);
            this.checkedListBoxBatchOperation.SelectedValueChanged += new System.EventHandler(this.checkedListBoxBatchOperation_SelectedValueChanged);
            // 
            // buttonUp
            // 
            this.buttonUp.Location = new System.Drawing.Point(6, 15);
            this.buttonUp.Name = "buttonUp";
            this.buttonUp.Size = new System.Drawing.Size(50, 23);
            this.buttonUp.TabIndex = 3;
            this.buttonUp.Text = "上移";
            this.buttonUp.UseVisualStyleBackColor = true;
            this.buttonUp.Click += new System.EventHandler(this.buttonUp_Click);
            // 
            // checkBoxFirstFieldIsNumber
            // 
            this.checkBoxFirstFieldIsNumber.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxFirstFieldIsNumber.AutoSize = true;
            this.checkBoxFirstFieldIsNumber.Location = new System.Drawing.Point(234, 29);
            this.checkBoxFirstFieldIsNumber.Name = "checkBoxFirstFieldIsNumber";
            this.checkBoxFirstFieldIsNumber.Size = new System.Drawing.Size(144, 16);
            this.checkBoxFirstFieldIsNumber.TabIndex = 1;
            this.checkBoxFirstFieldIsNumber.Text = "第一个字段为目标个数";
            this.checkBoxFirstFieldIsNumber.UseVisualStyleBackColor = true;
            // 
            // comboBoxSerialOutputExpressionSub
            // 
            this.comboBoxSerialOutputExpressionSub.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.comboBoxSerialOutputExpressionSub.Font = new System.Drawing.Font("宋体", 11F);
            this.comboBoxSerialOutputExpressionSub.FormattingEnabled = true;
            this.comboBoxSerialOutputExpressionSub.Location = new System.Drawing.Point(90, 280);
            this.comboBoxSerialOutputExpressionSub.Name = "comboBoxSerialOutputExpressionSub";
            this.comboBoxSerialOutputExpressionSub.Size = new System.Drawing.Size(181, 23);
            this.comboBoxSerialOutputExpressionSub.TabIndex = 8;
            // 
            // comboBoxSerialOutputExpressionMain
            // 
            this.comboBoxSerialOutputExpressionMain.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.comboBoxSerialOutputExpressionMain.Font = new System.Drawing.Font("宋体", 11F);
            this.comboBoxSerialOutputExpressionMain.FormattingEnabled = true;
            this.comboBoxSerialOutputExpressionMain.Location = new System.Drawing.Point(71, 251);
            this.comboBoxSerialOutputExpressionMain.Name = "comboBoxSerialOutputExpressionMain";
            this.comboBoxSerialOutputExpressionMain.Size = new System.Drawing.Size(121, 23);
            this.comboBoxSerialOutputExpressionMain.TabIndex = 7;
            this.comboBoxSerialOutputExpressionMain.SelectedIndexChanged += new System.EventHandler(this.comboBoxSerialOutputExpressionMain_SelectedIndexChanged);
            // 
            // buttonSerialOutputDelete
            // 
            this.buttonSerialOutputDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSerialOutputDelete.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonSerialOutputDelete.Location = new System.Drawing.Point(357, 280);
            this.buttonSerialOutputDelete.Name = "buttonSerialOutputDelete";
            this.buttonSerialOutputDelete.Size = new System.Drawing.Size(61, 24);
            this.buttonSerialOutputDelete.TabIndex = 4;
            this.buttonSerialOutputDelete.Text = "单项删除";
            this.buttonSerialOutputDelete.UseVisualStyleBackColor = true;
            this.buttonSerialOutputDelete.Click += new System.EventHandler(this.buttonSerialOutputDelete_Click);
            // 
            // buttonSerialOutputInsert
            // 
            this.buttonSerialOutputInsert.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSerialOutputInsert.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonSerialOutputInsert.Location = new System.Drawing.Point(287, 280);
            this.buttonSerialOutputInsert.Name = "buttonSerialOutputInsert";
            this.buttonSerialOutputInsert.Size = new System.Drawing.Size(64, 24);
            this.buttonSerialOutputInsert.TabIndex = 3;
            this.buttonSerialOutputInsert.Text = "单项插入";
            this.buttonSerialOutputInsert.UseVisualStyleBackColor = true;
            this.buttonSerialOutputInsert.Click += new System.EventHandler(this.buttonSerialOutputInsert_Click);
            // 
            // labelSerialOutputExpression
            // 
            this.labelSerialOutputExpression.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelSerialOutputExpression.AutoSize = true;
            this.labelSerialOutputExpression.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Bold);
            this.labelSerialOutputExpression.Location = new System.Drawing.Point(10, 254);
            this.labelSerialOutputExpression.Name = "labelSerialOutputExpression";
            this.labelSerialOutputExpression.Size = new System.Drawing.Size(55, 15);
            this.labelSerialOutputExpression.TabIndex = 4;
            this.labelSerialOutputExpression.Text = "表达式";
            // 
            // dataGridViewSerialOutputComData
            // 
            this.dataGridViewSerialOutputComData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewSerialOutputComData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewSerialOutputComData.Location = new System.Drawing.Point(6, 6);
            this.dataGridViewSerialOutputComData.MultiSelect = false;
            this.dataGridViewSerialOutputComData.Name = "dataGridViewSerialOutputComData";
            this.dataGridViewSerialOutputComData.RowTemplate.Height = 23;
            this.dataGridViewSerialOutputComData.Size = new System.Drawing.Size(416, 239);
            this.dataGridViewSerialOutputComData.TabIndex = 1;
            this.dataGridViewSerialOutputComData.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewSerialOutputComData_CellClick);
            this.dataGridViewSerialOutputComData.SizeChanged += new System.EventHandler(this.dataGridViewSerialOutputComData_SizeChanged);
            // 
            // tabPageSerialOutputFormat
            // 
            this.tabPageSerialOutputFormat.Controls.Add(this.comboBoxSerialOutputRecordSeparator);
            this.tabPageSerialOutputFormat.Controls.Add(this.labelSerialOutputRecordSeparator);
            this.tabPageSerialOutputFormat.Controls.Add(this.comboBoxOutputFieldSeparator);
            this.tabPageSerialOutputFormat.Controls.Add(this.labelSerialOutputFieldSeparator);
            this.tabPageSerialOutputFormat.Controls.Add(this.groupBoxSerialOutputEraseZero);
            this.tabPageSerialOutputFormat.Controls.Add(this.groupBoxSerialBoxNegative);
            this.tabPageSerialOutputFormat.Controls.Add(this.comboBoxSerialOutputFloatBit);
            this.tabPageSerialOutputFormat.Controls.Add(this.comboBoxSerialOutputIntBit);
            this.tabPageSerialOutputFormat.Controls.Add(this.labelSerialOutputFloatBit);
            this.tabPageSerialOutputFormat.Controls.Add(this.labelSerialOutputIntBit);
            this.tabPageSerialOutputFormat.Controls.Add(this.labelSerialOutput);
            this.tabPageSerialOutputFormat.Controls.Add(this.groupBoxSerialOutputFormat);
            this.tabPageSerialOutputFormat.Controls.Add(this.groupBoxSerialOutputComMode);
            this.tabPageSerialOutputFormat.Location = new System.Drawing.Point(4, 22);
            this.tabPageSerialOutputFormat.Name = "tabPageSerialOutputFormat";
            this.tabPageSerialOutputFormat.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageSerialOutputFormat.Size = new System.Drawing.Size(428, 439);
            this.tabPageSerialOutputFormat.TabIndex = 1;
            this.tabPageSerialOutputFormat.Text = "格式";
            this.tabPageSerialOutputFormat.UseVisualStyleBackColor = true;
            // 
            // comboBoxSerialOutputRecordSeparator
            // 
            this.comboBoxSerialOutputRecordSeparator.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSerialOutputRecordSeparator.FormattingEnabled = true;
            this.comboBoxSerialOutputRecordSeparator.Items.AddRange(new object[] {
            "无",
            "逗号",
            "空格"});
            this.comboBoxSerialOutputRecordSeparator.Location = new System.Drawing.Point(167, 385);
            this.comboBoxSerialOutputRecordSeparator.Name = "comboBoxSerialOutputRecordSeparator";
            this.comboBoxSerialOutputRecordSeparator.Size = new System.Drawing.Size(115, 20);
            this.comboBoxSerialOutputRecordSeparator.TabIndex = 12;
            // 
            // labelSerialOutputRecordSeparator
            // 
            this.labelSerialOutputRecordSeparator.AutoSize = true;
            this.labelSerialOutputRecordSeparator.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.labelSerialOutputRecordSeparator.Location = new System.Drawing.Point(28, 385);
            this.labelSerialOutputRecordSeparator.Name = "labelSerialOutputRecordSeparator";
            this.labelSerialOutputRecordSeparator.Size = new System.Drawing.Size(70, 12);
            this.labelSerialOutputRecordSeparator.TabIndex = 11;
            this.labelSerialOutputRecordSeparator.Text = "记录分隔符";
            // 
            // comboBoxOutputFieldSeparator
            // 
            this.comboBoxOutputFieldSeparator.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxOutputFieldSeparator.FormattingEnabled = true;
            this.comboBoxOutputFieldSeparator.Items.AddRange(new object[] {
            "逗号",
            "空格"});
            this.comboBoxOutputFieldSeparator.Location = new System.Drawing.Point(167, 354);
            this.comboBoxOutputFieldSeparator.Name = "comboBoxOutputFieldSeparator";
            this.comboBoxOutputFieldSeparator.Size = new System.Drawing.Size(115, 20);
            this.comboBoxOutputFieldSeparator.TabIndex = 10;
            // 
            // labelSerialOutputFieldSeparator
            // 
            this.labelSerialOutputFieldSeparator.AutoSize = true;
            this.labelSerialOutputFieldSeparator.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.labelSerialOutputFieldSeparator.Location = new System.Drawing.Point(28, 354);
            this.labelSerialOutputFieldSeparator.Name = "labelSerialOutputFieldSeparator";
            this.labelSerialOutputFieldSeparator.Size = new System.Drawing.Size(70, 12);
            this.labelSerialOutputFieldSeparator.TabIndex = 9;
            this.labelSerialOutputFieldSeparator.Text = "字段分隔符";
            // 
            // groupBoxSerialOutputEraseZero
            // 
            this.groupBoxSerialOutputEraseZero.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxSerialOutputEraseZero.Controls.Add(this.radioButtonSerialOutputEraseZeroNo);
            this.groupBoxSerialOutputEraseZero.Controls.Add(this.radioButtonSerialOutputEraseZeroYes);
            this.groupBoxSerialOutputEraseZero.Enabled = false;
            this.groupBoxSerialOutputEraseZero.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.groupBoxSerialOutputEraseZero.Location = new System.Drawing.Point(21, 296);
            this.groupBoxSerialOutputEraseZero.Name = "groupBoxSerialOutputEraseZero";
            this.groupBoxSerialOutputEraseZero.Size = new System.Drawing.Size(379, 46);
            this.groupBoxSerialOutputEraseZero.TabIndex = 8;
            this.groupBoxSerialOutputEraseZero.TabStop = false;
            this.groupBoxSerialOutputEraseZero.Text = "消零";
            // 
            // radioButtonSerialOutputEraseZeroNo
            // 
            this.radioButtonSerialOutputEraseZeroNo.AutoSize = true;
            this.radioButtonSerialOutputEraseZeroNo.Checked = true;
            this.radioButtonSerialOutputEraseZeroNo.Location = new System.Drawing.Point(179, 20);
            this.radioButtonSerialOutputEraseZeroNo.Name = "radioButtonSerialOutputEraseZeroNo";
            this.radioButtonSerialOutputEraseZeroNo.Size = new System.Drawing.Size(36, 16);
            this.radioButtonSerialOutputEraseZeroNo.TabIndex = 3;
            this.radioButtonSerialOutputEraseZeroNo.TabStop = true;
            this.radioButtonSerialOutputEraseZeroNo.Text = "无";
            this.radioButtonSerialOutputEraseZeroNo.UseVisualStyleBackColor = true;
            // 
            // radioButtonSerialOutputEraseZeroYes
            // 
            this.radioButtonSerialOutputEraseZeroYes.AutoSize = true;
            this.radioButtonSerialOutputEraseZeroYes.Location = new System.Drawing.Point(18, 20);
            this.radioButtonSerialOutputEraseZeroYes.Name = "radioButtonSerialOutputEraseZeroYes";
            this.radioButtonSerialOutputEraseZeroYes.Size = new System.Drawing.Size(36, 16);
            this.radioButtonSerialOutputEraseZeroYes.TabIndex = 2;
            this.radioButtonSerialOutputEraseZeroYes.Text = "有";
            this.radioButtonSerialOutputEraseZeroYes.UseVisualStyleBackColor = true;
            // 
            // groupBoxSerialBoxNegative
            // 
            this.groupBoxSerialBoxNegative.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxSerialBoxNegative.Controls.Add(this.radioButtonSerialOutputNegative8);
            this.groupBoxSerialBoxNegative.Controls.Add(this.radioButtonSerialOutputNegativeMinus);
            this.groupBoxSerialBoxNegative.Enabled = false;
            this.groupBoxSerialBoxNegative.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.groupBoxSerialBoxNegative.Location = new System.Drawing.Point(21, 232);
            this.groupBoxSerialBoxNegative.Name = "groupBoxSerialBoxNegative";
            this.groupBoxSerialBoxNegative.Size = new System.Drawing.Size(379, 47);
            this.groupBoxSerialBoxNegative.TabIndex = 4;
            this.groupBoxSerialBoxNegative.TabStop = false;
            this.groupBoxSerialBoxNegative.Text = "负数表示";
            // 
            // radioButtonSerialOutputNegative8
            // 
            this.radioButtonSerialOutputNegative8.AutoSize = true;
            this.radioButtonSerialOutputNegative8.Location = new System.Drawing.Point(179, 20);
            this.radioButtonSerialOutputNegative8.Name = "radioButtonSerialOutputNegative8";
            this.radioButtonSerialOutputNegative8.Size = new System.Drawing.Size(30, 16);
            this.radioButtonSerialOutputNegative8.TabIndex = 3;
            this.radioButtonSerialOutputNegative8.Text = "8";
            this.radioButtonSerialOutputNegative8.UseVisualStyleBackColor = true;
            // 
            // radioButtonSerialOutputNegativeMinus
            // 
            this.radioButtonSerialOutputNegativeMinus.AutoSize = true;
            this.radioButtonSerialOutputNegativeMinus.Checked = true;
            this.radioButtonSerialOutputNegativeMinus.Location = new System.Drawing.Point(18, 20);
            this.radioButtonSerialOutputNegativeMinus.Name = "radioButtonSerialOutputNegativeMinus";
            this.radioButtonSerialOutputNegativeMinus.Size = new System.Drawing.Size(30, 16);
            this.radioButtonSerialOutputNegativeMinus.TabIndex = 2;
            this.radioButtonSerialOutputNegativeMinus.TabStop = true;
            this.radioButtonSerialOutputNegativeMinus.Text = "-";
            this.radioButtonSerialOutputNegativeMinus.UseVisualStyleBackColor = true;
            // 
            // comboBoxSerialOutputFloatBit
            // 
            this.comboBoxSerialOutputFloatBit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSerialOutputFloatBit.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.comboBoxSerialOutputFloatBit.FormattingEnabled = true;
            this.comboBoxSerialOutputFloatBit.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3",
            "4"});
            this.comboBoxSerialOutputFloatBit.Location = new System.Drawing.Point(167, 199);
            this.comboBoxSerialOutputFloatBit.Name = "comboBoxSerialOutputFloatBit";
            this.comboBoxSerialOutputFloatBit.Size = new System.Drawing.Size(115, 20);
            this.comboBoxSerialOutputFloatBit.TabIndex = 7;
            // 
            // comboBoxSerialOutputIntBit
            // 
            this.comboBoxSerialOutputIntBit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSerialOutputIntBit.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.comboBoxSerialOutputIntBit.FormattingEnabled = true;
            this.comboBoxSerialOutputIntBit.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10"});
            this.comboBoxSerialOutputIntBit.Location = new System.Drawing.Point(167, 166);
            this.comboBoxSerialOutputIntBit.Name = "comboBoxSerialOutputIntBit";
            this.comboBoxSerialOutputIntBit.Size = new System.Drawing.Size(115, 20);
            this.comboBoxSerialOutputIntBit.TabIndex = 6;
            // 
            // labelSerialOutputFloatBit
            // 
            this.labelSerialOutputFloatBit.AutoSize = true;
            this.labelSerialOutputFloatBit.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.labelSerialOutputFloatBit.Location = new System.Drawing.Point(28, 199);
            this.labelSerialOutputFloatBit.Name = "labelSerialOutputFloatBit";
            this.labelSerialOutputFloatBit.Size = new System.Drawing.Size(70, 12);
            this.labelSerialOutputFloatBit.TabIndex = 5;
            this.labelSerialOutputFloatBit.Text = "小数位数：";
            // 
            // labelSerialOutputIntBit
            // 
            this.labelSerialOutputIntBit.AutoSize = true;
            this.labelSerialOutputIntBit.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.labelSerialOutputIntBit.Location = new System.Drawing.Point(28, 169);
            this.labelSerialOutputIntBit.Name = "labelSerialOutputIntBit";
            this.labelSerialOutputIntBit.Size = new System.Drawing.Size(70, 12);
            this.labelSerialOutputIntBit.TabIndex = 4;
            this.labelSerialOutputIntBit.Text = "整数位数：";
            // 
            // labelSerialOutput
            // 
            this.labelSerialOutput.AutoSize = true;
            this.labelSerialOutput.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.labelSerialOutput.Location = new System.Drawing.Point(28, 70);
            this.labelSerialOutput.Name = "labelSerialOutput";
            this.labelSerialOutput.Size = new System.Drawing.Size(57, 12);
            this.labelSerialOutput.TabIndex = 3;
            this.labelSerialOutput.Text = "格式设定";
            // 
            // groupBoxSerialOutputFormat
            // 
            this.groupBoxSerialOutputFormat.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxSerialOutputFormat.Controls.Add(this.radioButtonSerialOutputFormatBin);
            this.groupBoxSerialOutputFormat.Controls.Add(this.radioButtonSerialOutputFormatAscii);
            this.groupBoxSerialOutputFormat.Enabled = false;
            this.groupBoxSerialOutputFormat.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.groupBoxSerialOutputFormat.Location = new System.Drawing.Point(21, 96);
            this.groupBoxSerialOutputFormat.Name = "groupBoxSerialOutputFormat";
            this.groupBoxSerialOutputFormat.Size = new System.Drawing.Size(379, 46);
            this.groupBoxSerialOutputFormat.TabIndex = 2;
            this.groupBoxSerialOutputFormat.TabStop = false;
            this.groupBoxSerialOutputFormat.Text = "输出形式";
            // 
            // radioButtonSerialOutputFormatBin
            // 
            this.radioButtonSerialOutputFormatBin.AutoSize = true;
            this.radioButtonSerialOutputFormatBin.Enabled = false;
            this.radioButtonSerialOutputFormatBin.Location = new System.Drawing.Point(179, 20);
            this.radioButtonSerialOutputFormatBin.Name = "radioButtonSerialOutputFormatBin";
            this.radioButtonSerialOutputFormatBin.Size = new System.Drawing.Size(62, 16);
            this.radioButtonSerialOutputFormatBin.TabIndex = 3;
            this.radioButtonSerialOutputFormatBin.Text = "二进制";
            this.radioButtonSerialOutputFormatBin.UseVisualStyleBackColor = true;
            this.radioButtonSerialOutputFormatBin.Click += new System.EventHandler(this.radioButtonSerialOutputFormatBin_Click);
            // 
            // radioButtonSerialOutputFormatAscii
            // 
            this.radioButtonSerialOutputFormatAscii.AutoSize = true;
            this.radioButtonSerialOutputFormatAscii.Checked = true;
            this.radioButtonSerialOutputFormatAscii.Location = new System.Drawing.Point(27, 20);
            this.radioButtonSerialOutputFormatAscii.Name = "radioButtonSerialOutputFormatAscii";
            this.radioButtonSerialOutputFormatAscii.Size = new System.Drawing.Size(58, 16);
            this.radioButtonSerialOutputFormatAscii.TabIndex = 2;
            this.radioButtonSerialOutputFormatAscii.TabStop = true;
            this.radioButtonSerialOutputFormatAscii.Text = "ASCII";
            this.radioButtonSerialOutputFormatAscii.UseVisualStyleBackColor = true;
            this.radioButtonSerialOutputFormatAscii.Click += new System.EventHandler(this.radioButtonSerialOutputFormatAscii_Click);
            // 
            // groupBoxSerialOutputComMode
            // 
            this.groupBoxSerialOutputComMode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxSerialOutputComMode.BackColor = System.Drawing.Color.Transparent;
            this.groupBoxSerialOutputComMode.Controls.Add(this.radioButtonSerialOutputComModeEthernet);
            this.groupBoxSerialOutputComMode.Controls.Add(this.radioButtonSerialOutputComModeRS232);
            this.groupBoxSerialOutputComMode.Enabled = false;
            this.groupBoxSerialOutputComMode.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.groupBoxSerialOutputComMode.Location = new System.Drawing.Point(21, 21);
            this.groupBoxSerialOutputComMode.Name = "groupBoxSerialOutputComMode";
            this.groupBoxSerialOutputComMode.Size = new System.Drawing.Size(379, 46);
            this.groupBoxSerialOutputComMode.TabIndex = 1;
            this.groupBoxSerialOutputComMode.TabStop = false;
            this.groupBoxSerialOutputComMode.Text = "通讯方式";
            // 
            // radioButtonSerialOutputComModeEthernet
            // 
            this.radioButtonSerialOutputComModeEthernet.AutoSize = true;
            this.radioButtonSerialOutputComModeEthernet.Checked = true;
            this.radioButtonSerialOutputComModeEthernet.Location = new System.Drawing.Point(179, 20);
            this.radioButtonSerialOutputComModeEthernet.Name = "radioButtonSerialOutputComModeEthernet";
            this.radioButtonSerialOutputComModeEthernet.Size = new System.Drawing.Size(62, 16);
            this.radioButtonSerialOutputComModeEthernet.TabIndex = 1;
            this.radioButtonSerialOutputComModeEthernet.TabStop = true;
            this.radioButtonSerialOutputComModeEthernet.Text = "以太网";
            this.radioButtonSerialOutputComModeEthernet.UseVisualStyleBackColor = true;
            // 
            // radioButtonSerialOutputComModeRS232
            // 
            this.radioButtonSerialOutputComModeRS232.AutoSize = true;
            this.radioButtonSerialOutputComModeRS232.Enabled = false;
            this.radioButtonSerialOutputComModeRS232.Location = new System.Drawing.Point(27, 20);
            this.radioButtonSerialOutputComModeRS232.Name = "radioButtonSerialOutputComModeRS232";
            this.radioButtonSerialOutputComModeRS232.Size = new System.Drawing.Size(58, 16);
            this.radioButtonSerialOutputComModeRS232.TabIndex = 0;
            this.radioButtonSerialOutputComModeRS232.TabStop = true;
            this.radioButtonSerialOutputComModeRS232.Text = "RS232";
            this.radioButtonSerialOutputComModeRS232.UseVisualStyleBackColor = true;
            // 
            // buttonSerialOutputConfirm
            // 
            this.buttonSerialOutputConfirm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSerialOutputConfirm.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.buttonSerialOutputConfirm.Location = new System.Drawing.Point(259, 484);
            this.buttonSerialOutputConfirm.Name = "buttonSerialOutputConfirm";
            this.buttonSerialOutputConfirm.Size = new System.Drawing.Size(93, 23);
            this.buttonSerialOutputConfirm.TabIndex = 1;
            this.buttonSerialOutputConfirm.Text = "确定并退出";
            this.buttonSerialOutputConfirm.UseVisualStyleBackColor = true;
            this.buttonSerialOutputConfirm.Click += new System.EventHandler(this.buttonSerialOutputConfirm_Click);
            // 
            // buttonSerialOutputCancel
            // 
            this.buttonSerialOutputCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSerialOutputCancel.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.buttonSerialOutputCancel.Location = new System.Drawing.Point(358, 484);
            this.buttonSerialOutputCancel.Name = "buttonSerialOutputCancel";
            this.buttonSerialOutputCancel.Size = new System.Drawing.Size(87, 23);
            this.buttonSerialOutputCancel.TabIndex = 2;
            this.buttonSerialOutputCancel.Text = "取消并退出";
            this.buttonSerialOutputCancel.UseVisualStyleBackColor = true;
            this.buttonSerialOutputCancel.Click += new System.EventHandler(this.buttonSerialOutputCancel_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(10, 283);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 15);
            this.label1.TabIndex = 12;
            this.label1.Text = "单项内容";
            // 
            // SerialOutputFBDForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(461, 519);
            this.Controls.Add(this.buttonSerialOutputCancel);
            this.Controls.Add(this.buttonSerialOutputConfirm);
            this.Controls.Add(this.tabControlSerialOutput);
            this.MinimumSize = new System.Drawing.Size(477, 557);
            this.Name = "SerialOutputFBDForm";
            this.Text = "串行输出";
            this.tabControlSerialOutput.ResumeLayout(false);
            this.tabPageSerialOutputOutput.ResumeLayout(false);
            this.tabPageSerialOutputOutput.PerformLayout();
            this.groupBoxBatchOperation.ResumeLayout(false);
            this.groupBoxBatchOperation.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRecycleNumber)).EndInit();
            this.groupBoxOperator.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSerialOutputComData)).EndInit();
            this.tabPageSerialOutputFormat.ResumeLayout(false);
            this.tabPageSerialOutputFormat.PerformLayout();
            this.groupBoxSerialOutputEraseZero.ResumeLayout(false);
            this.groupBoxSerialOutputEraseZero.PerformLayout();
            this.groupBoxSerialBoxNegative.ResumeLayout(false);
            this.groupBoxSerialBoxNegative.PerformLayout();
            this.groupBoxSerialOutputFormat.ResumeLayout(false);
            this.groupBoxSerialOutputFormat.PerformLayout();
            this.groupBoxSerialOutputComMode.ResumeLayout(false);
            this.groupBoxSerialOutputComMode.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControlSerialOutput;
        private System.Windows.Forms.TabPage tabPageSerialOutputOutput;
        private System.Windows.Forms.TabPage tabPageSerialOutputFormat;
        private System.Windows.Forms.GroupBox groupBoxSerialOutputComMode;
        private System.Windows.Forms.RadioButton radioButtonSerialOutputComModeEthernet;
        private System.Windows.Forms.RadioButton radioButtonSerialOutputComModeRS232;
        private System.Windows.Forms.GroupBox groupBoxSerialOutputFormat;
        private System.Windows.Forms.RadioButton radioButtonSerialOutputFormatBin;
        private System.Windows.Forms.RadioButton radioButtonSerialOutputFormatAscii;
        private System.Windows.Forms.Label labelSerialOutput;
        private System.Windows.Forms.Label labelSerialOutputIntBit;
        private System.Windows.Forms.Label labelSerialOutputFloatBit;
        private System.Windows.Forms.ComboBox comboBoxSerialOutputIntBit;
        private System.Windows.Forms.ComboBox comboBoxSerialOutputFloatBit;
        private System.Windows.Forms.GroupBox groupBoxSerialBoxNegative;
        private System.Windows.Forms.RadioButton radioButtonSerialOutputNegative8;
        private System.Windows.Forms.RadioButton radioButtonSerialOutputNegativeMinus;
        private System.Windows.Forms.GroupBox groupBoxSerialOutputEraseZero;
        private System.Windows.Forms.RadioButton radioButtonSerialOutputEraseZeroNo;
        private System.Windows.Forms.RadioButton radioButtonSerialOutputEraseZeroYes;
        private System.Windows.Forms.ComboBox comboBoxOutputFieldSeparator;
        private System.Windows.Forms.Label labelSerialOutputFieldSeparator;
        private System.Windows.Forms.ComboBox comboBoxSerialOutputRecordSeparator;
        private System.Windows.Forms.Label labelSerialOutputRecordSeparator;
        private System.Windows.Forms.DataGridView dataGridViewSerialOutputComData;
        private System.Windows.Forms.Label labelSerialOutputExpression;
        private System.Windows.Forms.Button buttonSerialOutputConfirm;
        private System.Windows.Forms.Button buttonSerialOutputCancel;
        private System.Windows.Forms.Button buttonSerialOutputDelete;
        private System.Windows.Forms.Button buttonSerialOutputInsert;
        private System.Windows.Forms.ComboBox comboBoxSerialOutputExpressionMain;
        private System.Windows.Forms.ComboBox comboBoxSerialOutputExpressionSub;
        private System.Windows.Forms.GroupBox groupBoxBatchOperation;
        private System.Windows.Forms.CheckedListBox checkedListBoxBatchOperation;
        private System.Windows.Forms.CheckBox checkBoxFirstFieldIsNumber;
        private System.Windows.Forms.Button buttonDown;
        private System.Windows.Forms.Button buttonUp;
        private System.Windows.Forms.GroupBox groupBoxOperator;
        private System.Windows.Forms.Button buttonBatchOperation;
        private System.Windows.Forms.NumericUpDown numericUpDownRecycleNumber;
        private System.Windows.Forms.Label labelRecycleNumber;
        private System.Windows.Forms.Button buttonClearAll;
        private System.Windows.Forms.Label label1;
    }
}