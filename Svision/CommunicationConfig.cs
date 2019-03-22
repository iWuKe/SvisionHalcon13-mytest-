/**************************************************************************************
**
**       Filename:  CommunicationConfig.cs
**
**    Description:  Vision Software Communication Config
**
**        Version:  1.0
**        Created:  2016-2-17
**       Revision:  v02.0007
**       Compiler:  vs2010
**        Company:  SIASUN
**
****************************************************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Svision
{
    public partial class CommunicationConfig : Form
    {
        public CommunicationConfig()
        {
            try
            {
                InitializeComponent();
                InitCom();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void InitCom()
        {
            switch (ConfigInformation.GetInstance().tComCfg.ComMainType)
            {
                case CComConfig.ComTypeEnum.MODBUS_TCP:
                    radioButtonTCP.Checked = false;
                    radioButtonModbusTCP.Checked = true;
                    radioButtonUDP.Checked = false;
                    tabControlComCfg.SelectedTab = tabPageComCfgModbusTCP;
                    break;
                case CComConfig.ComTypeEnum.TCP_CLIENT:
                    radioButtonTCP.Checked = true;
                    radioButtonModbusTCP.Checked = false;
                    radioButtonUDP.Checked = false;
                    tabControlComCfg.SelectedTab = tabPageComCfgTCP;
                    radioButtonComCfgTcpServer.Checked = false;
                    radioButtonComCfgTcpClient.Checked = true;
                    textBoxComCfgTcpServerIP.Text = ConfigInformation.GetInstance().tComCfg.TCPServerIP;
                    textBoxComCfgTcpServerPort.Text = ConfigInformation.GetInstance().tComCfg.TCPServerPort;
                    textBoxComCfgTcpLocalPort.Enabled = false;
                    textBoxComCfgTcpLocalIP.Enabled = false;
                    if (ConfigInformation.GetInstance().tComCfg.TrigMode == CComConfig.TrigModeEnum.COM_TRIG)
                    {
                        radioButtonComCfgTcpTrigModeComTrig.Checked = true;
                        textBoxComCfgTcpTrigCom.Text = ConfigInformation.GetInstance().tComCfg.TrigStr;
                        textBoxComCfgTcpTrigTime.Enabled = false;
                    }
                    else
                    {
                        radioButtonComCfgTcpTrigModeTimeTrig.Checked = true;
                        textBoxComCfgTcpTrigTime.Text = ConfigInformation.GetInstance().tComCfg.TrigTime.ToString();
                        textBoxComCfgTcpTrigCom.Enabled = false;
                    }
                    break;
                case CComConfig.ComTypeEnum.TCP_SERVER:
                    radioButtonTCP.Checked = true;
                    radioButtonModbusTCP.Checked = false;
                    radioButtonUDP.Checked = false;
                    tabControlComCfg.SelectedTab = tabPageComCfgTCP;
                    radioButtonComCfgTcpServer.Checked = true;
                    radioButtonComCfgTcpClient.Checked = false;

                    textBoxComCfgTcpLocalIP.Text = ConfigInformation.GetInstance().tComCfg.TCPLocalIP;
                    textBoxComCfgTcpLocalPort.Text = ConfigInformation.GetInstance().tComCfg.TCPLocalPort;

                    if (ConfigInformation.GetInstance().tComCfg.TrigMode == CComConfig.TrigModeEnum.COM_TRIG)
                    {
                        radioButtonComCfgTcpTrigModeComTrig.Checked = true;
                        textBoxComCfgTcpTrigCom.Text = ConfigInformation.GetInstance().tComCfg.TrigStr;
                        textBoxComCfgTcpTrigTime.Enabled = false;
                    }
                    else
                    {
                        radioButtonComCfgTcpTrigModeTimeTrig.Checked = true;
                        textBoxComCfgTcpTrigTime.Text = ConfigInformation.GetInstance().tComCfg.TrigTime.ToString();
                        textBoxComCfgTcpTrigCom.Enabled = false;
                    }

                    textBoxComCfgTcpServerIP.Enabled = false;
                    textBoxComCfgTcpServerPort.Enabled = false;
                    break;
                case CComConfig.ComTypeEnum.UDP:
                    radioButtonTCP.Checked = true;
                    radioButtonModbusTCP.Checked = false;
                    radioButtonUDP.Checked = false;
                    tabControlComCfg.SelectedTab = tabPageComCfgUDP;
                    break;
                default:
                    break;
            }
        }

        private void buttonComCfgConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                if (radioButtonTCP.Checked)                                                 //TCP
                {
                    if (radioButtonComCfgTcpServer.Checked)             //TCP_Server
                    {
                        if (textBoxComCfgTcpLocalPort.Text == String.Empty ||
                            textBoxComCfgTcpLocalIP.Text == String.Empty)
                        {
                            MessageBox.Show("请输入正确的本地IP地址和端口号！");
                            return;
                        }
                        else
                        {
                            if (radioButtonComCfgTcpTrigModeComTrig.Checked && (textBoxComCfgTcpTrigCom.Text == String.Empty))
                            {
                                MessageBox.Show("请输入正确的触发报文！");
                                return;
                            }
                            else if (radioButtonComCfgTcpTrigModeTimeTrig.Checked && (textBoxComCfgTcpTrigTime.Text == String.Empty))
                            {
                                MessageBox.Show("请输入正确的触发时间！");
                                return;
                            }
                            else
                            {
                                ConfigInformation.GetInstance().tComCfg.ComMainType = CComConfig.ComTypeEnum.TCP_SERVER;
                                ConfigInformation.GetInstance().tComCfg.TCPLocalPort = textBoxComCfgTcpLocalPort.Text;
                                ConfigInformation.GetInstance().tComCfg.TCPLocalIP = textBoxComCfgTcpLocalIP.Text;
                                if (radioButtonComCfgTcpTrigModeTimeTrig.Checked)
                                {
                                    ConfigInformation.GetInstance().tComCfg.TrigMode = CComConfig.TrigModeEnum.TIME_TRIG;
                                    ConfigInformation.GetInstance().tComCfg.TrigTime = int.Parse(textBoxComCfgTcpTrigTime.Text);
                                }
                                else
                                {
                                    ConfigInformation.GetInstance().tComCfg.TrigMode = CComConfig.TrigModeEnum.COM_TRIG;
                                    ConfigInformation.GetInstance().tComCfg.TrigStr = textBoxComCfgTcpTrigCom.Text;
                                }
                            }
                        }
                    }
                    else if (radioButtonComCfgTcpClient.Checked)        //TCP_Client
                    {
                        if ((textBoxComCfgTcpServerPort.Text == String.Empty) | (textBoxComCfgTcpServerIP.Text == String.Empty))
                        {
                            MessageBox.Show("请正确的输入远程服务器IP地址，端口号和触发条件");
                            return;
                        }
                        else
                        {
                            if (radioButtonComCfgTcpTrigModeComTrig.Checked && (textBoxComCfgTcpTrigCom.Text == String.Empty))
                            {
                                MessageBox.Show("请输入正确的触发报文！");
                                return;
                            }
                            else if (radioButtonComCfgTcpTrigModeTimeTrig.Checked && (textBoxComCfgTcpTrigTime.Text == String.Empty))
                            {
                                MessageBox.Show("请输入正确的触发时间！");
                                return;
                            }
                            else
                            {
                                ConfigInformation.GetInstance().tComCfg.ComMainType = CComConfig.ComTypeEnum.TCP_CLIENT;
                                ConfigInformation.GetInstance().tComCfg.TCPServerPort = textBoxComCfgTcpServerPort.Text;
                                ConfigInformation.GetInstance().tComCfg.TCPServerIP = textBoxComCfgTcpServerIP.Text;
                                if (radioButtonComCfgTcpTrigModeTimeTrig.Checked)
                                {
                                    ConfigInformation.GetInstance().tComCfg.TrigMode = CComConfig.TrigModeEnum.TIME_TRIG;
                                    ConfigInformation.GetInstance().tComCfg.TrigTime = int.Parse(textBoxComCfgTcpTrigTime.Text);
                                }
                                else
                                {
                                    ConfigInformation.GetInstance().tComCfg.TrigMode = CComConfig.TrigModeEnum.COM_TRIG;
                                    ConfigInformation.GetInstance().tComCfg.TrigStr = textBoxComCfgTcpTrigCom.Text;
                                }
                            }
                        }
                    }
                }
                else if (radioButtonUDP.Checked)                                             //UDP
                {

                }
                else if (radioButtonModbusTCP.Checked)                                      //ModbusTCP
                {

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            this.Close();
        }

        private void buttonComCfgCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void radioButtonTCP_Click(object sender, EventArgs e)
        {
            tabControlComCfg.SelectedTab = tabPageComCfgTCP;
        }

        private void radioButtonModbusTCP_Click(object sender, EventArgs e)
        {
            tabControlComCfg.SelectedTab = tabPageComCfgModbusTCP;
        }

        private void radioButtonUDP_Click(object sender, EventArgs e)
        {
            tabControlComCfg.SelectedTab = tabPageComCfgUDP;
        }

        private void radioButtonComCfgTcpServer_Click(object sender, EventArgs e)
        {
            textBoxComCfgTcpServerIP.Enabled = false;
            textBoxComCfgTcpServerPort.Enabled = false;
            textBoxComCfgTcpLocalPort.Enabled = true;
            textBoxComCfgTcpLocalIP.Enabled = true;
        }

        private void radioButtonComCfgTcpClient_Click(object sender, EventArgs e)
        {
            textBoxComCfgTcpLocalPort.Enabled = false;
            textBoxComCfgTcpLocalIP.Enabled = false;
            textBoxComCfgTcpServerIP.Enabled = true;
            textBoxComCfgTcpServerPort.Enabled = true;
        }

        private void radioButtonComCfgTcpTrigModeComTrig_Click(object sender, EventArgs e)
        {
            textBoxComCfgTcpTrigCom.Enabled = true;
            textBoxComCfgTcpTrigTime.Enabled = false;
        }

        private void radioButtonComCfgTcpTrigModeTimeTrig_Click(object sender, EventArgs e)
        {
            textBoxComCfgTcpTrigCom.Enabled = false;
            textBoxComCfgTcpTrigTime.Enabled = true;
        }
    }
}
