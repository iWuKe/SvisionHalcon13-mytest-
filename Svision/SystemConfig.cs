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
    public partial class SystemConfig : Form
    {
        public SystemConfig()
        {
            InitializeComponent();
            this.textBoxSystemCfgBootProgram.Text = ConfigInformation.GetInstance().tSysCfg.bootPath;
            this.checkBoxSystemCfgEnableBoot.Checked = ConfigInformation.GetInstance().tSysCfg.isBoot;
            
        }

        private void buttonSystemCfgBootProgram_Click(object sender, EventArgs e)
        {
            try
            {
                string localFilePath;
                if (folderBrowserDialogSystemConfig.ShowDialog() == DialogResult.OK)
                {

                    localFilePath = folderBrowserDialogSystemConfig.SelectedPath;

                    this.textBoxSystemCfgBootProgram.Text = localFilePath;
                }

            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void buttonSystemCfgBootConfirm_Click(object sender, EventArgs e)
        {
            try
            {

                if (this.checkBoxSystemCfgEnableBoot.Checked == true)
                {
                    if ((this.textBoxSystemCfgBootProgram.Text == String.Empty) || (this.textBoxSystemCfgBootProgram.Text == " "))
                    {
                        MessageBox.Show("请输入正确的工程路径");
                        return;
                    } 
                    else
                    {
                        ConfigInformation.GetInstance().tSysCfg.bootPath = this.textBoxSystemCfgBootProgram.Text;
                        ConfigInformation.GetInstance().tSysCfg.isBoot = this.checkBoxSystemCfgEnableBoot.Checked;
                        
                        ConfigInformation.GetInstance().tSysCfg.saveSysInfoCfg();
                        this.Close();
                    }
                } 
                else
                {
                    ConfigInformation.GetInstance().tSysCfg.bootPath = this.textBoxSystemCfgBootProgram.Text;
                    ConfigInformation.GetInstance().tSysCfg.isBoot = this.checkBoxSystemCfgEnableBoot.Checked;
                    
                    ConfigInformation.GetInstance().tSysCfg.saveSysInfoCfg();
                    this.Close();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

           
        }

        private void buttonSystemCfgBootCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void checkBoxSystemCfgEnableBoot_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxSystemCfgEnableBoot.Checked)
            {
                if (this.textBoxSystemCfgBootProgram.Text == String.Empty || this.textBoxSystemCfgBootProgram.Text == " ")
                {
                    checkBoxSystemCfgEnableBoot.Checked = false;
                    MessageBox.Show("未输入boot程序路径，请先输入路径再勾选启用boot程序！");
                }
            }
        }
    }
}
