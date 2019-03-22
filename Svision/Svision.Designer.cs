using HalconDotNet;
namespace Svision
{
    partial class Svision
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);

        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Svision));
            this.ReadImageSet = new System.Windows.Forms.Button();
            this.pictureBoxShowImage = new System.Windows.Forms.PictureBox();
            this.Camera = new System.Windows.Forms.Button();
            this.tabImage = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.ComCfg = new System.Windows.Forms.Button();
            this.cameraPara = new System.Windows.Forms.Button();
            this.CCamera = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.labelXieGang = new System.Windows.Forms.Label();
            this.textBoxAllFileNum = new System.Windows.Forms.TextBox();
            this.textBoxFileNum = new System.Windows.Forms.TextBox();
            this.panelManualFileInput = new System.Windows.Forms.Panel();
            this.buttonPreImage = new System.Windows.Forms.Button();
            this.buttonNextImage = new System.Windows.Forms.Button();
            this.BoxImage = new System.Windows.Forms.GroupBox();
            this.ProcessEdit = new System.Windows.Forms.Button();
            this.ShowResults = new System.Windows.Forms.Button();
            this.AutoRun = new System.Windows.Forms.Button();
            this.ResultsBox = new System.Windows.Forms.GroupBox();
            this.textBoxResultShow = new System.Windows.Forms.TextBox();
            this.Menu = new System.Windows.Forms.MenuStrip();
            this.文件FToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.新建NToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.打开OToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.保存SToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.工具TToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.选项OToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.系统配置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.标定ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.拍照toolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.帮助HToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.内容CToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.关于AToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.编辑EToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.撤消UToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.重复RToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.剪切TToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.复制CToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.粘贴PToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.全选AToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CCameratimer = new System.Windows.Forms.Timer(this.components);
            this.textBoxTime = new System.Windows.Forms.TextBox();
            this.labelTime = new System.Windows.Forms.Label();
            this.labelIsCameraOpenedAndConnected = new System.Windows.Forms.Label();
            this.textBoxIsCameraOpenedAndConnected = new System.Windows.Forms.TextBox();
            this.folderBrowserDialogOpenProject = new System.Windows.Forms.FolderBrowserDialog();
            this.textBoxShowTime = new System.Windows.Forms.TextBox();
            this.labelShowTime = new System.Windows.Forms.Label();
            this.checkBoxDoNotShowImage = new System.Windows.Forms.CheckBox();
            this.groupBoxTCPResult = new System.Windows.Forms.GroupBox();
            this.textBoxTCPShow = new System.Windows.Forms.TextBox();
            this.listBoxProcess = new System.Windows.Forms.ListBox();
            this.labelProcess = new System.Windows.Forms.Label();
            this.checkBoxIsFile = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxShowImage)).BeginInit();
            this.tabImage.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.panelManualFileInput.SuspendLayout();
            this.BoxImage.SuspendLayout();
            this.ResultsBox.SuspendLayout();
            this.Menu.SuspendLayout();
            this.groupBoxTCPResult.SuspendLayout();
            this.SuspendLayout();
            // 
            // ReadImageSet
            // 
            this.ReadImageSet.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ReadImageSet.Enabled = false;
            this.ReadImageSet.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ReadImageSet.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ReadImageSet.Location = new System.Drawing.Point(6, 6);
            this.ReadImageSet.Name = "ReadImageSet";
            this.ReadImageSet.Size = new System.Drawing.Size(66, 28);
            this.ReadImageSet.TabIndex = 0;
            this.ReadImageSet.Text = "文件设置";
            this.ReadImageSet.UseVisualStyleBackColor = false;
            this.ReadImageSet.Click += new System.EventHandler(this.ReadImageSet_Click);
            // 
            // pictureBoxShowImage
            // 
            this.pictureBoxShowImage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBoxShowImage.BackColor = System.Drawing.Color.White;
            this.pictureBoxShowImage.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBoxShowImage.BackgroundImage")));
            this.pictureBoxShowImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBoxShowImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxShowImage.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxShowImage.Image")));
            this.pictureBoxShowImage.InitialImage = ((System.Drawing.Image)(resources.GetObject("pictureBoxShowImage.InitialImage")));
            this.pictureBoxShowImage.Location = new System.Drawing.Point(12, 64);
            this.pictureBoxShowImage.Name = "pictureBoxShowImage";
            this.pictureBoxShowImage.Size = new System.Drawing.Size(374, 366);
            this.pictureBoxShowImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxShowImage.TabIndex = 1;
            this.pictureBoxShowImage.TabStop = false;
            this.pictureBoxShowImage.SizeChanged += new System.EventHandler(this.pictureBoxShowImage_SizeChanged);
            // 
            // Camera
            // 
            this.Camera.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.Camera.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Camera.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Camera.Location = new System.Drawing.Point(6, 6);
            this.Camera.Name = "Camera";
            this.Camera.Size = new System.Drawing.Size(66, 30);
            this.Camera.TabIndex = 2;
            this.Camera.Text = "单次采集";
            this.Camera.UseVisualStyleBackColor = false;
            this.Camera.Click += new System.EventHandler(this.Camera_Click);
            // 
            // tabImage
            // 
            this.tabImage.Appearance = System.Windows.Forms.TabAppearance.Buttons;
            this.tabImage.Controls.Add(this.tabPage1);
            this.tabImage.Controls.Add(this.tabPage2);
            this.tabImage.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tabImage.ItemSize = new System.Drawing.Size(80, 30);
            this.tabImage.Location = new System.Drawing.Point(2, 18);
            this.tabImage.Name = "tabImage";
            this.tabImage.SelectedIndex = 0;
            this.tabImage.Size = new System.Drawing.Size(168, 113);
            this.tabImage.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabImage.TabIndex = 3;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.ComCfg);
            this.tabPage1.Controls.Add(this.cameraPara);
            this.tabPage1.Controls.Add(this.CCamera);
            this.tabPage1.Controls.Add(this.Camera);
            this.tabPage1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.tabPage1.Location = new System.Drawing.Point(4, 34);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(160, 75);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "相机";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // ComCfg
            // 
            this.ComCfg.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ComCfg.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ComCfg.Location = new System.Drawing.Point(85, 39);
            this.ComCfg.Name = "ComCfg";
            this.ComCfg.Size = new System.Drawing.Size(68, 30);
            this.ComCfg.TabIndex = 6;
            this.ComCfg.Text = "通讯配置";
            this.ComCfg.UseVisualStyleBackColor = false;
            this.ComCfg.Click += new System.EventHandler(this.ComCfg_Click);
            // 
            // cameraPara
            // 
            this.cameraPara.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.cameraPara.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cameraPara.Location = new System.Drawing.Point(6, 39);
            this.cameraPara.Name = "cameraPara";
            this.cameraPara.Size = new System.Drawing.Size(66, 30);
            this.cameraPara.TabIndex = 5;
            this.cameraPara.Text = "相机设置";
            this.cameraPara.UseVisualStyleBackColor = false;
            this.cameraPara.Click += new System.EventHandler(this.cameraPara_Click);
            // 
            // CCamera
            // 
            this.CCamera.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.CCamera.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.CCamera.Location = new System.Drawing.Point(85, 6);
            this.CCamera.Name = "CCamera";
            this.CCamera.Size = new System.Drawing.Size(68, 30);
            this.CCamera.TabIndex = 4;
            this.CCamera.Text = "实时采集";
            this.CCamera.UseVisualStyleBackColor = false;
            this.CCamera.Click += new System.EventHandler(this.CCamera_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.labelXieGang);
            this.tabPage2.Controls.Add(this.textBoxAllFileNum);
            this.tabPage2.Controls.Add(this.textBoxFileNum);
            this.tabPage2.Controls.Add(this.panelManualFileInput);
            this.tabPage2.Controls.Add(this.ReadImageSet);
            this.tabPage2.Location = new System.Drawing.Point(4, 34);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(160, 75);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "文件";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // labelXieGang
            // 
            this.labelXieGang.AutoSize = true;
            this.labelXieGang.Location = new System.Drawing.Point(102, 14);
            this.labelXieGang.Name = "labelXieGang";
            this.labelXieGang.Size = new System.Drawing.Size(11, 12);
            this.labelXieGang.TabIndex = 5;
            this.labelXieGang.Text = "/";
            // 
            // textBoxAllFileNum
            // 
            this.textBoxAllFileNum.Location = new System.Drawing.Point(119, 9);
            this.textBoxAllFileNum.Name = "textBoxAllFileNum";
            this.textBoxAllFileNum.ReadOnly = true;
            this.textBoxAllFileNum.Size = new System.Drawing.Size(35, 21);
            this.textBoxAllFileNum.TabIndex = 4;
            // 
            // textBoxFileNum
            // 
            this.textBoxFileNum.Location = new System.Drawing.Point(78, 9);
            this.textBoxFileNum.Name = "textBoxFileNum";
            this.textBoxFileNum.ReadOnly = true;
            this.textBoxFileNum.Size = new System.Drawing.Size(18, 21);
            this.textBoxFileNum.TabIndex = 3;
            // 
            // panelManualFileInput
            // 
            this.panelManualFileInput.Controls.Add(this.buttonPreImage);
            this.panelManualFileInput.Controls.Add(this.buttonNextImage);
            this.panelManualFileInput.Enabled = false;
            this.panelManualFileInput.Location = new System.Drawing.Point(6, 36);
            this.panelManualFileInput.Name = "panelManualFileInput";
            this.panelManualFileInput.Size = new System.Drawing.Size(148, 33);
            this.panelManualFileInput.TabIndex = 2;
            // 
            // buttonPreImage
            // 
            this.buttonPreImage.Location = new System.Drawing.Point(3, 3);
            this.buttonPreImage.Name = "buttonPreImage";
            this.buttonPreImage.Size = new System.Drawing.Size(60, 23);
            this.buttonPreImage.TabIndex = 1;
            this.buttonPreImage.Text = "上一幅";
            this.buttonPreImage.UseVisualStyleBackColor = true;
            this.buttonPreImage.Click += new System.EventHandler(this.buttonPreImage_Click);
            // 
            // buttonNextImage
            // 
            this.buttonNextImage.Location = new System.Drawing.Point(82, 3);
            this.buttonNextImage.Name = "buttonNextImage";
            this.buttonNextImage.Size = new System.Drawing.Size(63, 23);
            this.buttonNextImage.TabIndex = 0;
            this.buttonNextImage.Text = "下一幅";
            this.buttonNextImage.UseVisualStyleBackColor = true;
            this.buttonNextImage.Click += new System.EventHandler(this.buttonNextImage_Click);
            // 
            // BoxImage
            // 
            this.BoxImage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BoxImage.Controls.Add(this.tabImage);
            this.BoxImage.Location = new System.Drawing.Point(391, 28);
            this.BoxImage.Name = "BoxImage";
            this.BoxImage.Size = new System.Drawing.Size(173, 138);
            this.BoxImage.TabIndex = 4;
            this.BoxImage.TabStop = false;
            this.BoxImage.Text = "图像输入";
            // 
            // ProcessEdit
            // 
            this.ProcessEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ProcessEdit.Location = new System.Drawing.Point(570, 44);
            this.ProcessEdit.Name = "ProcessEdit";
            this.ProcessEdit.Size = new System.Drawing.Size(62, 30);
            this.ProcessEdit.TabIndex = 5;
            this.ProcessEdit.Text = "作业编辑";
            this.ProcessEdit.UseVisualStyleBackColor = true;
            this.ProcessEdit.Click += new System.EventHandler(this.ProcessEdit_Click);
            // 
            // ShowResults
            // 
            this.ShowResults.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ShowResults.Location = new System.Drawing.Point(570, 80);
            this.ShowResults.Name = "ShowResults";
            this.ShowResults.Size = new System.Drawing.Size(62, 26);
            this.ShowResults.TabIndex = 6;
            this.ShowResults.Text = "输出显示";
            this.ShowResults.UseVisualStyleBackColor = true;
            this.ShowResults.Click += new System.EventHandler(this.ShowResults_Click);
            // 
            // AutoRun
            // 
            this.AutoRun.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.AutoRun.Location = new System.Drawing.Point(570, 114);
            this.AutoRun.Name = "AutoRun";
            this.AutoRun.Size = new System.Drawing.Size(62, 23);
            this.AutoRun.TabIndex = 8;
            this.AutoRun.Text = "自动运行";
            this.AutoRun.UseVisualStyleBackColor = true;
            this.AutoRun.Click += new System.EventHandler(this.AutoRun_Click);
            // 
            // ResultsBox
            // 
            this.ResultsBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ResultsBox.Controls.Add(this.textBoxResultShow);
            this.ResultsBox.Location = new System.Drawing.Point(486, 255);
            this.ResultsBox.Name = "ResultsBox";
            this.ResultsBox.Size = new System.Drawing.Size(153, 175);
            this.ResultsBox.TabIndex = 10;
            this.ResultsBox.TabStop = false;
            this.ResultsBox.Text = "结果显示";
            // 
            // textBoxResultShow
            // 
            this.textBoxResultShow.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxResultShow.Location = new System.Drawing.Point(6, 20);
            this.textBoxResultShow.Multiline = true;
            this.textBoxResultShow.Name = "textBoxResultShow";
            this.textBoxResultShow.ReadOnly = true;
            this.textBoxResultShow.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxResultShow.Size = new System.Drawing.Size(141, 144);
            this.textBoxResultShow.TabIndex = 0;
            // 
            // Menu
            // 
            this.Menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.文件FToolStripMenuItem,
            this.工具TToolStripMenuItem,
            this.帮助HToolStripMenuItem,
            this.编辑EToolStripMenuItem});
            this.Menu.Location = new System.Drawing.Point(0, 0);
            this.Menu.Name = "Menu";
            this.Menu.Size = new System.Drawing.Size(645, 25);
            this.Menu.TabIndex = 12;
            this.Menu.Text = "Menu";
            this.Menu.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.Menu_ItemClicked);
            // 
            // 文件FToolStripMenuItem
            // 
            this.文件FToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.新建NToolStripMenuItem,
            this.打开OToolStripMenuItem,
            this.toolStripSeparator,
            this.保存SToolStripMenuItem});
            this.文件FToolStripMenuItem.Name = "文件FToolStripMenuItem";
            this.文件FToolStripMenuItem.Size = new System.Drawing.Size(58, 21);
            this.文件FToolStripMenuItem.Text = "文件(&F)";
            // 
            // 新建NToolStripMenuItem
            // 
            this.新建NToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("新建NToolStripMenuItem.Image")));
            this.新建NToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.新建NToolStripMenuItem.Name = "新建NToolStripMenuItem";
            this.新建NToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.新建NToolStripMenuItem.Size = new System.Drawing.Size(213, 22);
            this.新建NToolStripMenuItem.Text = "新建空白工程(&N)";
            this.新建NToolStripMenuItem.Click += new System.EventHandler(this.新建NToolStripMenuItem_Click);
            // 
            // 打开OToolStripMenuItem
            // 
            this.打开OToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("打开OToolStripMenuItem.Image")));
            this.打开OToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.打开OToolStripMenuItem.Name = "打开OToolStripMenuItem";
            this.打开OToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.打开OToolStripMenuItem.Size = new System.Drawing.Size(213, 22);
            this.打开OToolStripMenuItem.Text = "打开工程(&O)";
            this.打开OToolStripMenuItem.Click += new System.EventHandler(this.打开OToolStripMenuItem_Click);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(210, 6);
            // 
            // 保存SToolStripMenuItem
            // 
            this.保存SToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("保存SToolStripMenuItem.Image")));
            this.保存SToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.保存SToolStripMenuItem.Name = "保存SToolStripMenuItem";
            this.保存SToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.保存SToolStripMenuItem.Size = new System.Drawing.Size(213, 22);
            this.保存SToolStripMenuItem.Text = "保存工程(&S)";
            this.保存SToolStripMenuItem.Click += new System.EventHandler(this.保存SToolStripMenuItem_Click);
            // 
            // 工具TToolStripMenuItem
            // 
            this.工具TToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.选项OToolStripMenuItem,
            this.标定ToolStripMenuItem,
            this.拍照toolStripMenuItem});
            this.工具TToolStripMenuItem.Name = "工具TToolStripMenuItem";
            this.工具TToolStripMenuItem.Size = new System.Drawing.Size(59, 21);
            this.工具TToolStripMenuItem.Text = "工具(&T)";
            this.工具TToolStripMenuItem.Click += new System.EventHandler(this.工具TToolStripMenuItem_Click);
            // 
            // 选项OToolStripMenuItem
            // 
            this.选项OToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.系统配置ToolStripMenuItem});
            this.选项OToolStripMenuItem.Name = "选项OToolStripMenuItem";
            this.选项OToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.选项OToolStripMenuItem.Text = "选项(&O)";
            // 
            // 系统配置ToolStripMenuItem
            // 
            this.系统配置ToolStripMenuItem.Name = "系统配置ToolStripMenuItem";
            this.系统配置ToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.系统配置ToolStripMenuItem.Text = "系统配置(&K)";
            this.系统配置ToolStripMenuItem.Click += new System.EventHandler(this.系统配置ToolStripMenuItem_Click);
            // 
            // 标定ToolStripMenuItem
            // 
            this.标定ToolStripMenuItem.Name = "标定ToolStripMenuItem";
            this.标定ToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.标定ToolStripMenuItem.Text = "标定与校正(&P)";
            this.标定ToolStripMenuItem.Click += new System.EventHandler(this.标定ToolStripMenuItem_Click);
            // 
            // 拍照toolStripMenuItem
            // 
            this.拍照toolStripMenuItem.Name = "拍照toolStripMenuItem";
            this.拍照toolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.拍照toolStripMenuItem.Text = "拍照工具(&Q)";
            this.拍照toolStripMenuItem.Click += new System.EventHandler(this.拍照toolStripMenuItem_Click);
            // 
            // 帮助HToolStripMenuItem
            // 
            this.帮助HToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.内容CToolStripMenuItem,
            this.toolStripSeparator5,
            this.关于AToolStripMenuItem});
            this.帮助HToolStripMenuItem.Name = "帮助HToolStripMenuItem";
            this.帮助HToolStripMenuItem.Size = new System.Drawing.Size(61, 21);
            this.帮助HToolStripMenuItem.Text = "帮助(&H)";
            // 
            // 内容CToolStripMenuItem
            // 
            this.内容CToolStripMenuItem.Name = "内容CToolStripMenuItem";
            this.内容CToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.内容CToolStripMenuItem.Text = "内容(&C)";
            this.内容CToolStripMenuItem.Click += new System.EventHandler(this.内容CToolStripMenuItem_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(122, 6);
            // 
            // 关于AToolStripMenuItem
            // 
            this.关于AToolStripMenuItem.Name = "关于AToolStripMenuItem";
            this.关于AToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.关于AToolStripMenuItem.Text = "关于(&A)...";
            this.关于AToolStripMenuItem.Click += new System.EventHandler(this.关于AToolStripMenuItem_Click);
            // 
            // 编辑EToolStripMenuItem
            // 
            this.编辑EToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.撤消UToolStripMenuItem,
            this.重复RToolStripMenuItem,
            this.toolStripSeparator3,
            this.剪切TToolStripMenuItem,
            this.复制CToolStripMenuItem,
            this.粘贴PToolStripMenuItem,
            this.toolStripSeparator4,
            this.全选AToolStripMenuItem});
            this.编辑EToolStripMenuItem.Name = "编辑EToolStripMenuItem";
            this.编辑EToolStripMenuItem.Size = new System.Drawing.Size(59, 21);
            this.编辑EToolStripMenuItem.Text = "编辑(&E)";
            this.编辑EToolStripMenuItem.Visible = false;
            // 
            // 撤消UToolStripMenuItem
            // 
            this.撤消UToolStripMenuItem.Name = "撤消UToolStripMenuItem";
            this.撤消UToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
            this.撤消UToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.撤消UToolStripMenuItem.Text = "撤消(&U)";
            // 
            // 重复RToolStripMenuItem
            // 
            this.重复RToolStripMenuItem.Name = "重复RToolStripMenuItem";
            this.重复RToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Y)));
            this.重复RToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.重复RToolStripMenuItem.Text = "重复(&R)";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(158, 6);
            // 
            // 剪切TToolStripMenuItem
            // 
            this.剪切TToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("剪切TToolStripMenuItem.Image")));
            this.剪切TToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.剪切TToolStripMenuItem.Name = "剪切TToolStripMenuItem";
            this.剪切TToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.剪切TToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.剪切TToolStripMenuItem.Text = "剪切(&T)";
            // 
            // 复制CToolStripMenuItem
            // 
            this.复制CToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("复制CToolStripMenuItem.Image")));
            this.复制CToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.复制CToolStripMenuItem.Name = "复制CToolStripMenuItem";
            this.复制CToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.复制CToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.复制CToolStripMenuItem.Text = "复制(&C)";
            // 
            // 粘贴PToolStripMenuItem
            // 
            this.粘贴PToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("粘贴PToolStripMenuItem.Image")));
            this.粘贴PToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.粘贴PToolStripMenuItem.Name = "粘贴PToolStripMenuItem";
            this.粘贴PToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.粘贴PToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.粘贴PToolStripMenuItem.Text = "粘贴(&P)";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(158, 6);
            // 
            // 全选AToolStripMenuItem
            // 
            this.全选AToolStripMenuItem.Name = "全选AToolStripMenuItem";
            this.全选AToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.全选AToolStripMenuItem.Text = "全选(&A)";
            // 
            // CCameratimer
            // 
            this.CCameratimer.Tick += new System.EventHandler(this.CCameratimer_Tick);
            // 
            // textBoxTime
            // 
            this.textBoxTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxTime.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.textBoxTime.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBoxTime.ForeColor = System.Drawing.SystemColors.Highlight;
            this.textBoxTime.Location = new System.Drawing.Point(283, 32);
            this.textBoxTime.Name = "textBoxTime";
            this.textBoxTime.ReadOnly = true;
            this.textBoxTime.Size = new System.Drawing.Size(102, 26);
            this.textBoxTime.TabIndex = 13;
            // 
            // labelTime
            // 
            this.labelTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelTime.AutoSize = true;
            this.labelTime.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelTime.Location = new System.Drawing.Point(198, 38);
            this.labelTime.Name = "labelTime";
            this.labelTime.Size = new System.Drawing.Size(70, 12);
            this.labelTime.TabIndex = 14;
            this.labelTime.Text = "处理时间：";
            // 
            // labelIsCameraOpenedAndConnected
            // 
            this.labelIsCameraOpenedAndConnected.AutoSize = true;
            this.labelIsCameraOpenedAndConnected.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelIsCameraOpenedAndConnected.Location = new System.Drawing.Point(12, 37);
            this.labelIsCameraOpenedAndConnected.Name = "labelIsCameraOpenedAndConnected";
            this.labelIsCameraOpenedAndConnected.Size = new System.Drawing.Size(96, 12);
            this.labelIsCameraOpenedAndConnected.TabIndex = 15;
            this.labelIsCameraOpenedAndConnected.Text = "相机是否连接：";
            // 
            // textBoxIsCameraOpenedAndConnected
            // 
            this.textBoxIsCameraOpenedAndConnected.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.textBoxIsCameraOpenedAndConnected.ForeColor = System.Drawing.Color.Red;
            this.textBoxIsCameraOpenedAndConnected.Location = new System.Drawing.Point(114, 34);
            this.textBoxIsCameraOpenedAndConnected.Name = "textBoxIsCameraOpenedAndConnected";
            this.textBoxIsCameraOpenedAndConnected.ReadOnly = true;
            this.textBoxIsCameraOpenedAndConnected.Size = new System.Drawing.Size(78, 21);
            this.textBoxIsCameraOpenedAndConnected.TabIndex = 16;
            this.textBoxIsCameraOpenedAndConnected.Text = "否";
            // 
            // textBoxShowTime
            // 
            this.textBoxShowTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxShowTime.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.textBoxShowTime.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBoxShowTime.ForeColor = System.Drawing.SystemColors.Highlight;
            this.textBoxShowTime.Location = new System.Drawing.Point(460, 169);
            this.textBoxShowTime.Name = "textBoxShowTime";
            this.textBoxShowTime.ReadOnly = true;
            this.textBoxShowTime.Size = new System.Drawing.Size(77, 26);
            this.textBoxShowTime.TabIndex = 17;
            // 
            // labelShowTime
            // 
            this.labelShowTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelShowTime.AutoSize = true;
            this.labelShowTime.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelShowTime.Location = new System.Drawing.Point(390, 176);
            this.labelShowTime.Name = "labelShowTime";
            this.labelShowTime.Size = new System.Drawing.Size(64, 12);
            this.labelShowTime.TabIndex = 18;
            this.labelShowTime.Text = "显示用时:";
            // 
            // checkBoxDoNotShowImage
            // 
            this.checkBoxDoNotShowImage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxDoNotShowImage.AutoSize = true;
            this.checkBoxDoNotShowImage.Location = new System.Drawing.Point(558, 172);
            this.checkBoxDoNotShowImage.Name = "checkBoxDoNotShowImage";
            this.checkBoxDoNotShowImage.Size = new System.Drawing.Size(84, 16);
            this.checkBoxDoNotShowImage.TabIndex = 19;
            this.checkBoxDoNotShowImage.Text = "不显示图像";
            this.checkBoxDoNotShowImage.UseVisualStyleBackColor = true;
            // 
            // groupBoxTCPResult
            // 
            this.groupBoxTCPResult.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxTCPResult.Controls.Add(this.textBoxTCPShow);
            this.groupBoxTCPResult.Location = new System.Drawing.Point(392, 201);
            this.groupBoxTCPResult.Name = "groupBoxTCPResult";
            this.groupBoxTCPResult.Size = new System.Drawing.Size(247, 48);
            this.groupBoxTCPResult.TabIndex = 20;
            this.groupBoxTCPResult.TabStop = false;
            this.groupBoxTCPResult.Text = "通讯触发事件";
            // 
            // textBoxTCPShow
            // 
            this.textBoxTCPShow.Location = new System.Drawing.Point(6, 20);
            this.textBoxTCPShow.Name = "textBoxTCPShow";
            this.textBoxTCPShow.ReadOnly = true;
            this.textBoxTCPShow.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxTCPShow.Size = new System.Drawing.Size(235, 21);
            this.textBoxTCPShow.TabIndex = 0;
            // 
            // listBoxProcess
            // 
            this.listBoxProcess.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxProcess.Font = new System.Drawing.Font("隶书", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.listBoxProcess.FormattingEnabled = true;
            this.listBoxProcess.HorizontalScrollbar = true;
            this.listBoxProcess.ItemHeight = 16;
            this.listBoxProcess.Items.AddRange(new object[] {
            "0.相机输入"});
            this.listBoxProcess.Location = new System.Drawing.Point(391, 279);
            this.listBoxProcess.Name = "listBoxProcess";
            this.listBoxProcess.Size = new System.Drawing.Size(88, 148);
            this.listBoxProcess.TabIndex = 21;
            this.listBoxProcess.SelectedIndexChanged += new System.EventHandler(this.listBoxProcess_SelectedIndexChanged);
            this.listBoxProcess.DoubleClick += new System.EventHandler(this.listBoxProcess_DoubleClick);
            // 
            // labelProcess
            // 
            this.labelProcess.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelProcess.AutoSize = true;
            this.labelProcess.Location = new System.Drawing.Point(390, 255);
            this.labelProcess.Name = "labelProcess";
            this.labelProcess.Size = new System.Drawing.Size(83, 12);
            this.labelProcess.TabIndex = 22;
            this.labelProcess.Text = "作业编辑显示:";
            // 
            // checkBoxIsFile
            // 
            this.checkBoxIsFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxIsFile.AutoSize = true;
            this.checkBoxIsFile.Location = new System.Drawing.Point(570, 143);
            this.checkBoxIsFile.Name = "checkBoxIsFile";
            this.checkBoxIsFile.Size = new System.Drawing.Size(72, 16);
            this.checkBoxIsFile.TabIndex = 23;
            this.checkBoxIsFile.Text = "文件操作";
            this.checkBoxIsFile.UseVisualStyleBackColor = true;
            this.checkBoxIsFile.CheckedChanged += new System.EventHandler(this.checkBoxIsFile_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(38, 402);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(322, 17);
            this.label1.TabIndex = 24;
            this.label1.Text = "Copyright © 2000-2018 Siasun. All Rights Reserved.";
            // 
            // Svision
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.HighlightText;
            this.ClientSize = new System.Drawing.Size(645, 442);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBoxShowImage);
            this.Controls.Add(this.checkBoxIsFile);
            this.Controls.Add(this.labelProcess);
            this.Controls.Add(this.listBoxProcess);
            this.Controls.Add(this.groupBoxTCPResult);
            this.Controls.Add(this.checkBoxDoNotShowImage);
            this.Controls.Add(this.labelShowTime);
            this.Controls.Add(this.textBoxShowTime);
            this.Controls.Add(this.textBoxIsCameraOpenedAndConnected);
            this.Controls.Add(this.labelIsCameraOpenedAndConnected);
            this.Controls.Add(this.labelTime);
            this.Controls.Add(this.textBoxTime);
            this.Controls.Add(this.ResultsBox);
            this.Controls.Add(this.AutoRun);
            this.Controls.Add(this.ShowResults);
            this.Controls.Add(this.ProcessEdit);
            this.Controls.Add(this.BoxImage);
            this.Controls.Add(this.Menu);
            this.Enabled = false;
            this.MainMenuStrip = this.Menu;
            this.MinimumSize = new System.Drawing.Size(661, 300);
            this.Name = "Svision";
            this.Text = "Svision";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Svision_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxShowImage)).EndInit();
            this.tabImage.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.panelManualFileInput.ResumeLayout(false);
            this.BoxImage.ResumeLayout(false);
            this.ResultsBox.ResumeLayout(false);
            this.ResultsBox.PerformLayout();
            this.Menu.ResumeLayout(false);
            this.Menu.PerformLayout();
            this.groupBoxTCPResult.ResumeLayout(false);
            this.groupBoxTCPResult.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ReadImageSet;
        public System.Windows.Forms.PictureBox pictureBoxShowImage;
        private System.Windows.Forms.Button Camera;
        private System.Windows.Forms.TabControl tabImage;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button CCamera;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.GroupBox BoxImage;
        private System.Windows.Forms.Button ProcessEdit;
        private System.Windows.Forms.Button ShowResults;
        private System.Windows.Forms.GroupBox ResultsBox;
        private System.Windows.Forms.MenuStrip Menu;
        private System.Windows.Forms.ToolStripMenuItem 文件FToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 新建NToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 打开OToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripMenuItem 保存SToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 编辑EToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 撤消UToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 重复RToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem 剪切TToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 复制CToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 粘贴PToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem 全选AToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 工具TToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 选项OToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 帮助HToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 内容CToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem 关于AToolStripMenuItem;
        private System.Windows.Forms.Timer CCameratimer;
        private System.Windows.Forms.Button cameraPara;
        private System.Windows.Forms.Button ComCfg;
        private System.Windows.Forms.ToolStripMenuItem 标定ToolStripMenuItem;
        public System.Windows.Forms.TextBox textBoxResultShow;
        private System.Windows.Forms.ToolStripMenuItem 系统配置ToolStripMenuItem;
        private System.Windows.Forms.Label labelTime;
        public System.Windows.Forms.TextBox textBoxTime;
        public System.Windows.Forms.Button AutoRun;
        private System.Windows.Forms.Label labelIsCameraOpenedAndConnected;
        public System.Windows.Forms.TextBox textBoxIsCameraOpenedAndConnected;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialogOpenProject;
        public System.Windows.Forms.TextBox textBoxShowTime;
        private System.Windows.Forms.Label labelShowTime;
        public System.Windows.Forms.CheckBox checkBoxDoNotShowImage;
        private System.Windows.Forms.GroupBox groupBoxTCPResult;
        public System.Windows.Forms.TextBox textBoxTCPShow;
        public System.Windows.Forms.ListBox listBoxProcess;
        private System.Windows.Forms.Label labelProcess;
        private System.Windows.Forms.ToolStripMenuItem 拍照toolStripMenuItem;
        private System.Windows.Forms.Panel panelManualFileInput;
        private System.Windows.Forms.Button buttonPreImage;
        private System.Windows.Forms.Button buttonNextImage;
        public System.Windows.Forms.CheckBox checkBoxIsFile;
        public System.Windows.Forms.TextBox textBoxFileNum;
        private System.Windows.Forms.Label labelXieGang;
        public System.Windows.Forms.TextBox textBoxAllFileNum;
        private System.Windows.Forms.Label label1;
    }
}

