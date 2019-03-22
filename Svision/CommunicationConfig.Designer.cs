namespace Svision
{
    partial class CommunicationConfig
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
            this.groupBoxComMode = new System.Windows.Forms.GroupBox();
            this.radioButtonModbusTCP = new System.Windows.Forms.RadioButton();
            this.radioButtonUDP = new System.Windows.Forms.RadioButton();
            this.radioButtonTCP = new System.Windows.Forms.RadioButton();
            this.tabControlComCfg = new System.Windows.Forms.TabControl();
            this.tabPageComCfgTCP = new System.Windows.Forms.TabPage();
            this.textBoxComCfgTcpLocalIP = new System.Windows.Forms.TextBox();
            this.labelComCfgTcpLocalIP = new System.Windows.Forms.Label();
            this.textBoxComCfgTcpTrigTime = new System.Windows.Forms.TextBox();
            this.labelComCfgTcpTrigTime = new System.Windows.Forms.Label();
            this.groupBoxTrigMode = new System.Windows.Forms.GroupBox();
            this.radioButtonComCfgTcpTrigModeTimeTrig = new System.Windows.Forms.RadioButton();
            this.radioButtonComCfgTcpTrigModeComTrig = new System.Windows.Forms.RadioButton();
            this.textBoxComCfgTcpTrigCom = new System.Windows.Forms.TextBox();
            this.labelComCfgTcpTrigCom = new System.Windows.Forms.Label();
            this.textBoxComCfgTcpServerPort = new System.Windows.Forms.TextBox();
            this.labelComCfgTcpServerPort = new System.Windows.Forms.Label();
            this.textBoxComCfgTcpServerIP = new System.Windows.Forms.TextBox();
            this.labelComCfgTcpServerIP = new System.Windows.Forms.Label();
            this.labelComCfgTcpClient = new System.Windows.Forms.Label();
            this.textBoxComCfgTcpLocalPort = new System.Windows.Forms.TextBox();
            this.labelComCfgTcpLocalPort = new System.Windows.Forms.Label();
            this.labelComCfgTcpServer = new System.Windows.Forms.Label();
            this.groupBoxComCfgTcpCS = new System.Windows.Forms.GroupBox();
            this.radioButtonComCfgTcpClient = new System.Windows.Forms.RadioButton();
            this.radioButtonComCfgTcpServer = new System.Windows.Forms.RadioButton();
            this.tabPageComCfgUDP = new System.Windows.Forms.TabPage();
            this.tabPageComCfgModbusTCP = new System.Windows.Forms.TabPage();
            this.buttonComCfgConfirm = new System.Windows.Forms.Button();
            this.buttonComCfgCancel = new System.Windows.Forms.Button();
            this.groupBoxComMode.SuspendLayout();
            this.tabControlComCfg.SuspendLayout();
            this.tabPageComCfgTCP.SuspendLayout();
            this.groupBoxTrigMode.SuspendLayout();
            this.groupBoxComCfgTcpCS.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxComMode
            // 
            this.groupBoxComMode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxComMode.Controls.Add(this.radioButtonModbusTCP);
            this.groupBoxComMode.Controls.Add(this.radioButtonUDP);
            this.groupBoxComMode.Controls.Add(this.radioButtonTCP);
            this.groupBoxComMode.Enabled = false;
            this.groupBoxComMode.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBoxComMode.Location = new System.Drawing.Point(6, 12);
            this.groupBoxComMode.Name = "groupBoxComMode";
            this.groupBoxComMode.Size = new System.Drawing.Size(335, 54);
            this.groupBoxComMode.TabIndex = 0;
            this.groupBoxComMode.TabStop = false;
            this.groupBoxComMode.Text = "通讯模式";
            // 
            // radioButtonModbusTCP
            // 
            this.radioButtonModbusTCP.AutoSize = true;
            this.radioButtonModbusTCP.Location = new System.Drawing.Point(236, 29);
            this.radioButtonModbusTCP.Name = "radioButtonModbusTCP";
            this.radioButtonModbusTCP.Size = new System.Drawing.Size(86, 16);
            this.radioButtonModbusTCP.TabIndex = 2;
            this.radioButtonModbusTCP.Text = "ModbusTCP";
            this.radioButtonModbusTCP.UseVisualStyleBackColor = true;
            this.radioButtonModbusTCP.Click += new System.EventHandler(this.radioButtonModbusTCP_Click);
            // 
            // radioButtonUDP
            // 
            this.radioButtonUDP.AutoSize = true;
            this.radioButtonUDP.Location = new System.Drawing.Point(128, 29);
            this.radioButtonUDP.Name = "radioButtonUDP";
            this.radioButtonUDP.Size = new System.Drawing.Size(44, 16);
            this.radioButtonUDP.TabIndex = 1;
            this.radioButtonUDP.Text = "UDP";
            this.radioButtonUDP.UseVisualStyleBackColor = true;
            this.radioButtonUDP.Click += new System.EventHandler(this.radioButtonUDP_Click);
            // 
            // radioButtonTCP
            // 
            this.radioButtonTCP.AutoSize = true;
            this.radioButtonTCP.Checked = true;
            this.radioButtonTCP.Location = new System.Drawing.Point(24, 29);
            this.radioButtonTCP.Name = "radioButtonTCP";
            this.radioButtonTCP.Size = new System.Drawing.Size(44, 16);
            this.radioButtonTCP.TabIndex = 0;
            this.radioButtonTCP.TabStop = true;
            this.radioButtonTCP.Text = "TCP";
            this.radioButtonTCP.UseVisualStyleBackColor = true;
            this.radioButtonTCP.Click += new System.EventHandler(this.radioButtonTCP_Click);
            // 
            // tabControlComCfg
            // 
            this.tabControlComCfg.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlComCfg.Controls.Add(this.tabPageComCfgTCP);
            this.tabControlComCfg.Controls.Add(this.tabPageComCfgUDP);
            this.tabControlComCfg.Controls.Add(this.tabPageComCfgModbusTCP);
            this.tabControlComCfg.Location = new System.Drawing.Point(6, 74);
            this.tabControlComCfg.Name = "tabControlComCfg";
            this.tabControlComCfg.SelectedIndex = 0;
            this.tabControlComCfg.Size = new System.Drawing.Size(335, 418);
            this.tabControlComCfg.TabIndex = 1;
            // 
            // tabPageComCfgTCP
            // 
            this.tabPageComCfgTCP.Controls.Add(this.textBoxComCfgTcpLocalIP);
            this.tabPageComCfgTCP.Controls.Add(this.labelComCfgTcpLocalIP);
            this.tabPageComCfgTCP.Controls.Add(this.textBoxComCfgTcpTrigTime);
            this.tabPageComCfgTCP.Controls.Add(this.labelComCfgTcpTrigTime);
            this.tabPageComCfgTCP.Controls.Add(this.groupBoxTrigMode);
            this.tabPageComCfgTCP.Controls.Add(this.textBoxComCfgTcpTrigCom);
            this.tabPageComCfgTCP.Controls.Add(this.labelComCfgTcpTrigCom);
            this.tabPageComCfgTCP.Controls.Add(this.textBoxComCfgTcpServerPort);
            this.tabPageComCfgTCP.Controls.Add(this.labelComCfgTcpServerPort);
            this.tabPageComCfgTCP.Controls.Add(this.textBoxComCfgTcpServerIP);
            this.tabPageComCfgTCP.Controls.Add(this.labelComCfgTcpServerIP);
            this.tabPageComCfgTCP.Controls.Add(this.labelComCfgTcpClient);
            this.tabPageComCfgTCP.Controls.Add(this.textBoxComCfgTcpLocalPort);
            this.tabPageComCfgTCP.Controls.Add(this.labelComCfgTcpLocalPort);
            this.tabPageComCfgTCP.Controls.Add(this.labelComCfgTcpServer);
            this.tabPageComCfgTCP.Controls.Add(this.groupBoxComCfgTcpCS);
            this.tabPageComCfgTCP.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tabPageComCfgTCP.Location = new System.Drawing.Point(4, 22);
            this.tabPageComCfgTCP.Name = "tabPageComCfgTCP";
            this.tabPageComCfgTCP.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageComCfgTCP.Size = new System.Drawing.Size(327, 392);
            this.tabPageComCfgTCP.TabIndex = 0;
            this.tabPageComCfgTCP.Text = "TCP";
            this.tabPageComCfgTCP.UseVisualStyleBackColor = true;
            // 
            // textBoxComCfgTcpLocalIP
            // 
            this.textBoxComCfgTcpLocalIP.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxComCfgTcpLocalIP.Location = new System.Drawing.Point(154, 83);
            this.textBoxComCfgTcpLocalIP.Name = "textBoxComCfgTcpLocalIP";
            this.textBoxComCfgTcpLocalIP.Size = new System.Drawing.Size(154, 26);
            this.textBoxComCfgTcpLocalIP.TabIndex = 15;
            // 
            // labelComCfgTcpLocalIP
            // 
            this.labelComCfgTcpLocalIP.AutoSize = true;
            this.labelComCfgTcpLocalIP.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelComCfgTcpLocalIP.Location = new System.Drawing.Point(71, 90);
            this.labelComCfgTcpLocalIP.Name = "labelComCfgTcpLocalIP";
            this.labelComCfgTcpLocalIP.Size = new System.Drawing.Size(65, 12);
            this.labelComCfgTcpLocalIP.TabIndex = 14;
            this.labelComCfgTcpLocalIP.Text = "本地IP地址";
            // 
            // textBoxComCfgTcpTrigTime
            // 
            this.textBoxComCfgTcpTrigTime.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxComCfgTcpTrigTime.Location = new System.Drawing.Point(154, 349);
            this.textBoxComCfgTcpTrigTime.Name = "textBoxComCfgTcpTrigTime";
            this.textBoxComCfgTcpTrigTime.Size = new System.Drawing.Size(151, 26);
            this.textBoxComCfgTcpTrigTime.TabIndex = 13;
            // 
            // labelComCfgTcpTrigTime
            // 
            this.labelComCfgTcpTrigTime.AutoSize = true;
            this.labelComCfgTcpTrigTime.Font = new System.Drawing.Font("宋体", 9F);
            this.labelComCfgTcpTrigTime.Location = new System.Drawing.Point(29, 349);
            this.labelComCfgTcpTrigTime.Name = "labelComCfgTcpTrigTime";
            this.labelComCfgTcpTrigTime.Size = new System.Drawing.Size(65, 12);
            this.labelComCfgTcpTrigTime.TabIndex = 12;
            this.labelComCfgTcpTrigTime.Text = "定时时间ms";
            // 
            // groupBoxTrigMode
            // 
            this.groupBoxTrigMode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxTrigMode.Controls.Add(this.radioButtonComCfgTcpTrigModeTimeTrig);
            this.groupBoxTrigMode.Controls.Add(this.radioButtonComCfgTcpTrigModeComTrig);
            this.groupBoxTrigMode.Font = new System.Drawing.Font("宋体", 9F);
            this.groupBoxTrigMode.Location = new System.Drawing.Point(25, 252);
            this.groupBoxTrigMode.Name = "groupBoxTrigMode";
            this.groupBoxTrigMode.Size = new System.Drawing.Size(283, 47);
            this.groupBoxTrigMode.TabIndex = 11;
            this.groupBoxTrigMode.TabStop = false;
            this.groupBoxTrigMode.Text = "触发模式";
            // 
            // radioButtonComCfgTcpTrigModeTimeTrig
            // 
            this.radioButtonComCfgTcpTrigModeTimeTrig.AutoSize = true;
            this.radioButtonComCfgTcpTrigModeTimeTrig.Location = new System.Drawing.Point(162, 20);
            this.radioButtonComCfgTcpTrigModeTimeTrig.Name = "radioButtonComCfgTcpTrigModeTimeTrig";
            this.radioButtonComCfgTcpTrigModeTimeTrig.Size = new System.Drawing.Size(71, 16);
            this.radioButtonComCfgTcpTrigModeTimeTrig.TabIndex = 1;
            this.radioButtonComCfgTcpTrigModeTimeTrig.TabStop = true;
            this.radioButtonComCfgTcpTrigModeTimeTrig.Text = "定时触发";
            this.radioButtonComCfgTcpTrigModeTimeTrig.UseVisualStyleBackColor = true;
            this.radioButtonComCfgTcpTrigModeTimeTrig.Click += new System.EventHandler(this.radioButtonComCfgTcpTrigModeTimeTrig_Click);
            // 
            // radioButtonComCfgTcpTrigModeComTrig
            // 
            this.radioButtonComCfgTcpTrigModeComTrig.AutoSize = true;
            this.radioButtonComCfgTcpTrigModeComTrig.Location = new System.Drawing.Point(14, 20);
            this.radioButtonComCfgTcpTrigModeComTrig.Name = "radioButtonComCfgTcpTrigModeComTrig";
            this.radioButtonComCfgTcpTrigModeComTrig.Size = new System.Drawing.Size(71, 16);
            this.radioButtonComCfgTcpTrigModeComTrig.TabIndex = 0;
            this.radioButtonComCfgTcpTrigModeComTrig.TabStop = true;
            this.radioButtonComCfgTcpTrigModeComTrig.Text = "通讯触发";
            this.radioButtonComCfgTcpTrigModeComTrig.UseVisualStyleBackColor = true;
            this.radioButtonComCfgTcpTrigModeComTrig.Click += new System.EventHandler(this.radioButtonComCfgTcpTrigModeComTrig_Click);
            // 
            // textBoxComCfgTcpTrigCom
            // 
            this.textBoxComCfgTcpTrigCom.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxComCfgTcpTrigCom.Location = new System.Drawing.Point(154, 305);
            this.textBoxComCfgTcpTrigCom.Name = "textBoxComCfgTcpTrigCom";
            this.textBoxComCfgTcpTrigCom.Size = new System.Drawing.Size(151, 26);
            this.textBoxComCfgTcpTrigCom.TabIndex = 10;
            // 
            // labelComCfgTcpTrigCom
            // 
            this.labelComCfgTcpTrigCom.AutoSize = true;
            this.labelComCfgTcpTrigCom.Font = new System.Drawing.Font("宋体", 9F);
            this.labelComCfgTcpTrigCom.Location = new System.Drawing.Point(29, 312);
            this.labelComCfgTcpTrigCom.Name = "labelComCfgTcpTrigCom";
            this.labelComCfgTcpTrigCom.Size = new System.Drawing.Size(77, 12);
            this.labelComCfgTcpTrigCom.TabIndex = 9;
            this.labelComCfgTcpTrigCom.Text = "普通触发报文";
            // 
            // textBoxComCfgTcpServerPort
            // 
            this.textBoxComCfgTcpServerPort.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxComCfgTcpServerPort.Location = new System.Drawing.Point(155, 220);
            this.textBoxComCfgTcpServerPort.Name = "textBoxComCfgTcpServerPort";
            this.textBoxComCfgTcpServerPort.Size = new System.Drawing.Size(153, 26);
            this.textBoxComCfgTcpServerPort.TabIndex = 8;
            // 
            // labelComCfgTcpServerPort
            // 
            this.labelComCfgTcpServerPort.AutoSize = true;
            this.labelComCfgTcpServerPort.Font = new System.Drawing.Font("宋体", 9F);
            this.labelComCfgTcpServerPort.Location = new System.Drawing.Point(71, 220);
            this.labelComCfgTcpServerPort.Name = "labelComCfgTcpServerPort";
            this.labelComCfgTcpServerPort.Size = new System.Drawing.Size(77, 12);
            this.labelComCfgTcpServerPort.TabIndex = 7;
            this.labelComCfgTcpServerPort.Text = "服务器端口号";
            // 
            // textBoxComCfgTcpServerIP
            // 
            this.textBoxComCfgTcpServerIP.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxComCfgTcpServerIP.Location = new System.Drawing.Point(155, 176);
            this.textBoxComCfgTcpServerIP.Name = "textBoxComCfgTcpServerIP";
            this.textBoxComCfgTcpServerIP.Size = new System.Drawing.Size(153, 26);
            this.textBoxComCfgTcpServerIP.TabIndex = 6;
            // 
            // labelComCfgTcpServerIP
            // 
            this.labelComCfgTcpServerIP.AutoSize = true;
            this.labelComCfgTcpServerIP.Font = new System.Drawing.Font("宋体", 9F);
            this.labelComCfgTcpServerIP.Location = new System.Drawing.Point(71, 183);
            this.labelComCfgTcpServerIP.Name = "labelComCfgTcpServerIP";
            this.labelComCfgTcpServerIP.Size = new System.Drawing.Size(53, 12);
            this.labelComCfgTcpServerIP.TabIndex = 5;
            this.labelComCfgTcpServerIP.Text = "服务器IP";
            // 
            // labelComCfgTcpClient
            // 
            this.labelComCfgTcpClient.AutoSize = true;
            this.labelComCfgTcpClient.Font = new System.Drawing.Font("宋体", 9F);
            this.labelComCfgTcpClient.Location = new System.Drawing.Point(29, 160);
            this.labelComCfgTcpClient.Name = "labelComCfgTcpClient";
            this.labelComCfgTcpClient.Size = new System.Drawing.Size(65, 12);
            this.labelComCfgTcpClient.TabIndex = 4;
            this.labelComCfgTcpClient.Text = "客户端配置";
            // 
            // textBoxComCfgTcpLocalPort
            // 
            this.textBoxComCfgTcpLocalPort.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxComCfgTcpLocalPort.Location = new System.Drawing.Point(154, 124);
            this.textBoxComCfgTcpLocalPort.Name = "textBoxComCfgTcpLocalPort";
            this.textBoxComCfgTcpLocalPort.Size = new System.Drawing.Size(154, 26);
            this.textBoxComCfgTcpLocalPort.TabIndex = 3;
            // 
            // labelComCfgTcpLocalPort
            // 
            this.labelComCfgTcpLocalPort.AutoSize = true;
            this.labelComCfgTcpLocalPort.Font = new System.Drawing.Font("宋体", 9F);
            this.labelComCfgTcpLocalPort.Location = new System.Drawing.Point(71, 131);
            this.labelComCfgTcpLocalPort.Name = "labelComCfgTcpLocalPort";
            this.labelComCfgTcpLocalPort.Size = new System.Drawing.Size(65, 12);
            this.labelComCfgTcpLocalPort.TabIndex = 2;
            this.labelComCfgTcpLocalPort.Text = "本地端口号";
            // 
            // labelComCfgTcpServer
            // 
            this.labelComCfgTcpServer.AutoSize = true;
            this.labelComCfgTcpServer.Font = new System.Drawing.Font("宋体", 9F);
            this.labelComCfgTcpServer.Location = new System.Drawing.Point(29, 68);
            this.labelComCfgTcpServer.Name = "labelComCfgTcpServer";
            this.labelComCfgTcpServer.Size = new System.Drawing.Size(65, 12);
            this.labelComCfgTcpServer.TabIndex = 1;
            this.labelComCfgTcpServer.Text = "服务器配置";
            // 
            // groupBoxComCfgTcpCS
            // 
            this.groupBoxComCfgTcpCS.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxComCfgTcpCS.Controls.Add(this.radioButtonComCfgTcpClient);
            this.groupBoxComCfgTcpCS.Controls.Add(this.radioButtonComCfgTcpServer);
            this.groupBoxComCfgTcpCS.Enabled = false;
            this.groupBoxComCfgTcpCS.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBoxComCfgTcpCS.Location = new System.Drawing.Point(25, 16);
            this.groupBoxComCfgTcpCS.Name = "groupBoxComCfgTcpCS";
            this.groupBoxComCfgTcpCS.Size = new System.Drawing.Size(283, 49);
            this.groupBoxComCfgTcpCS.TabIndex = 0;
            this.groupBoxComCfgTcpCS.TabStop = false;
            this.groupBoxComCfgTcpCS.Text = "C/S";
            // 
            // radioButtonComCfgTcpClient
            // 
            this.radioButtonComCfgTcpClient.AutoSize = true;
            this.radioButtonComCfgTcpClient.Location = new System.Drawing.Point(192, 20);
            this.radioButtonComCfgTcpClient.Name = "radioButtonComCfgTcpClient";
            this.radioButtonComCfgTcpClient.Size = new System.Drawing.Size(59, 16);
            this.radioButtonComCfgTcpClient.TabIndex = 1;
            this.radioButtonComCfgTcpClient.TabStop = true;
            this.radioButtonComCfgTcpClient.Text = "客户端";
            this.radioButtonComCfgTcpClient.UseVisualStyleBackColor = true;
            this.radioButtonComCfgTcpClient.Click += new System.EventHandler(this.radioButtonComCfgTcpClient_Click);
            // 
            // radioButtonComCfgTcpServer
            // 
            this.radioButtonComCfgTcpServer.AutoSize = true;
            this.radioButtonComCfgTcpServer.Checked = true;
            this.radioButtonComCfgTcpServer.Location = new System.Drawing.Point(17, 20);
            this.radioButtonComCfgTcpServer.Name = "radioButtonComCfgTcpServer";
            this.radioButtonComCfgTcpServer.Size = new System.Drawing.Size(59, 16);
            this.radioButtonComCfgTcpServer.TabIndex = 0;
            this.radioButtonComCfgTcpServer.TabStop = true;
            this.radioButtonComCfgTcpServer.Text = "服务器";
            this.radioButtonComCfgTcpServer.UseVisualStyleBackColor = true;
            this.radioButtonComCfgTcpServer.Click += new System.EventHandler(this.radioButtonComCfgTcpServer_Click);
            // 
            // tabPageComCfgUDP
            // 
            this.tabPageComCfgUDP.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold);
            this.tabPageComCfgUDP.Location = new System.Drawing.Point(4, 22);
            this.tabPageComCfgUDP.Name = "tabPageComCfgUDP";
            this.tabPageComCfgUDP.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageComCfgUDP.Size = new System.Drawing.Size(327, 392);
            this.tabPageComCfgUDP.TabIndex = 1;
            this.tabPageComCfgUDP.Text = "UDP";
            this.tabPageComCfgUDP.UseVisualStyleBackColor = true;
            // 
            // tabPageComCfgModbusTCP
            // 
            this.tabPageComCfgModbusTCP.Font = new System.Drawing.Font("宋体", 12F);
            this.tabPageComCfgModbusTCP.Location = new System.Drawing.Point(4, 22);
            this.tabPageComCfgModbusTCP.Name = "tabPageComCfgModbusTCP";
            this.tabPageComCfgModbusTCP.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageComCfgModbusTCP.Size = new System.Drawing.Size(327, 392);
            this.tabPageComCfgModbusTCP.TabIndex = 2;
            this.tabPageComCfgModbusTCP.Text = "ModbusTCP";
            this.tabPageComCfgModbusTCP.UseVisualStyleBackColor = true;
            // 
            // buttonComCfgConfirm
            // 
            this.buttonComCfgConfirm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonComCfgConfirm.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonComCfgConfirm.Location = new System.Drawing.Point(12, 498);
            this.buttonComCfgConfirm.Name = "buttonComCfgConfirm";
            this.buttonComCfgConfirm.Size = new System.Drawing.Size(80, 30);
            this.buttonComCfgConfirm.TabIndex = 2;
            this.buttonComCfgConfirm.Text = "确定并退出";
            this.buttonComCfgConfirm.UseVisualStyleBackColor = true;
            this.buttonComCfgConfirm.Click += new System.EventHandler(this.buttonComCfgConfirm_Click);
            // 
            // buttonComCfgCancel
            // 
            this.buttonComCfgCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonComCfgCancel.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonComCfgCancel.Location = new System.Drawing.Point(242, 498);
            this.buttonComCfgCancel.Name = "buttonComCfgCancel";
            this.buttonComCfgCancel.Size = new System.Drawing.Size(86, 30);
            this.buttonComCfgCancel.TabIndex = 3;
            this.buttonComCfgCancel.Text = "取消并退出";
            this.buttonComCfgCancel.UseVisualStyleBackColor = true;
            this.buttonComCfgCancel.Click += new System.EventHandler(this.buttonComCfgCancel_Click);
            // 
            // CommunicationConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(349, 539);
            this.Controls.Add(this.buttonComCfgCancel);
            this.Controls.Add(this.buttonComCfgConfirm);
            this.Controls.Add(this.tabControlComCfg);
            this.Controls.Add(this.groupBoxComMode);
            this.MinimumSize = new System.Drawing.Size(365, 577);
            this.Name = "CommunicationConfig";
            this.Text = "通讯设置";
            this.groupBoxComMode.ResumeLayout(false);
            this.groupBoxComMode.PerformLayout();
            this.tabControlComCfg.ResumeLayout(false);
            this.tabPageComCfgTCP.ResumeLayout(false);
            this.tabPageComCfgTCP.PerformLayout();
            this.groupBoxTrigMode.ResumeLayout(false);
            this.groupBoxTrigMode.PerformLayout();
            this.groupBoxComCfgTcpCS.ResumeLayout(false);
            this.groupBoxComCfgTcpCS.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxComMode;
        private System.Windows.Forms.RadioButton radioButtonModbusTCP;
        private System.Windows.Forms.RadioButton radioButtonUDP;
        private System.Windows.Forms.RadioButton radioButtonTCP;
        private System.Windows.Forms.TabControl tabControlComCfg;
        private System.Windows.Forms.TabPage tabPageComCfgTCP;
        private System.Windows.Forms.TabPage tabPageComCfgUDP;
        private System.Windows.Forms.TabPage tabPageComCfgModbusTCP;
        private System.Windows.Forms.Button buttonComCfgConfirm;
        private System.Windows.Forms.Button buttonComCfgCancel;
        private System.Windows.Forms.GroupBox groupBoxComCfgTcpCS;
        private System.Windows.Forms.RadioButton radioButtonComCfgTcpClient;
        private System.Windows.Forms.RadioButton radioButtonComCfgTcpServer;
        private System.Windows.Forms.Label labelComCfgTcpServer;
        private System.Windows.Forms.Label labelComCfgTcpLocalPort;
        private System.Windows.Forms.TextBox textBoxComCfgTcpLocalPort;
        private System.Windows.Forms.Label labelComCfgTcpClient;
        private System.Windows.Forms.TextBox textBoxComCfgTcpServerPort;
        private System.Windows.Forms.Label labelComCfgTcpServerPort;
        private System.Windows.Forms.TextBox textBoxComCfgTcpServerIP;
        private System.Windows.Forms.Label labelComCfgTcpServerIP;
        private System.Windows.Forms.Label labelComCfgTcpTrigCom;
        private System.Windows.Forms.TextBox textBoxComCfgTcpTrigCom;
        private System.Windows.Forms.GroupBox groupBoxTrigMode;
        private System.Windows.Forms.RadioButton radioButtonComCfgTcpTrigModeTimeTrig;
        private System.Windows.Forms.RadioButton radioButtonComCfgTcpTrigModeComTrig;
        private System.Windows.Forms.TextBox textBoxComCfgTcpTrigTime;
        private System.Windows.Forms.Label labelComCfgTcpTrigTime;
        private System.Windows.Forms.TextBox textBoxComCfgTcpLocalIP;
        private System.Windows.Forms.Label labelComCfgTcpLocalIP;
    }
}