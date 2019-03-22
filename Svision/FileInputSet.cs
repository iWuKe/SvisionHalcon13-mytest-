using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using HalconDotNet;

namespace Svision
{
    public partial class FileInputSet : Form
    {
        List<string> fnFileNameList;

        string autoImagePath;
        int ImageNum = 0;
        private HTuple FileInputHWHandle;
        HObject image, img;
        double resizerate;
        int rowNumber, columnNumber;
        int oriPictureBoxShowImageWidth, oriPictureBoxShowImageHeight;
        public FileInputSet()
        {
            InitializeComponent();
        }
        ~FileInputSet()
        {
            if (image != null)
            {
                image.Dispose();
            }
            if (img != null)
            {
                img.Dispose();
            }

        }
        private void buttonGetFilePath_Click(object sender, EventArgs e)
        {

            try
            {
                
                if (folderBrowserDialogGetPath.ShowDialog() == DialogResult.OK)
                {

                    autoImagePath = folderBrowserDialogGetPath.SelectedPath;

                    this.textBoxFilePath.Text = autoImagePath;
                    listBoxFileList.Items.Clear();
                    ImageNum = 0;
                    string[] fn = Directory.GetFiles(autoImagePath);
                    fnFileNameList = new List<string>();
                    for (int i = 0; i < fn.Length;i++ )
                    {
                        string fileExtension = System.IO.Path.GetExtension(fn[i]);
                        if (fileExtension == ".bmp"||fileExtension == ".jpg"||fileExtension == ".png"||fileExtension == ".jpeg"||fileExtension == ".tiff")
                        {
                            listBoxFileList.Items.Insert(ImageNum,fn[i].Substring(fn[i].LastIndexOf("\\") + 1));
                            fnFileNameList.Add(fn[i]);
                            ImageNum++;
                        }
                    }
                    listBoxFileList.SelectedIndex = 0;


                }

            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void FileInputSet_Load(object sender, EventArgs e)
        {
            HOperatorSet.OpenWindow(0, 0, (pictureBoxImage.Width), (pictureBoxImage.Height), pictureBoxImage.Handle, "visible", "", out FileInputHWHandle);
            HOperatorSet.SetPart(FileInputHWHandle, 0, 0, (pictureBoxImage.Height), (pictureBoxImage.Width));
            HDevWindowStack.Push(FileInputHWHandle);
            oriPictureBoxShowImageWidth = pictureBoxImage.Width;
            oriPictureBoxShowImageHeight = pictureBoxImage.Height;
            numericUpDownColumn.Value = ConfigInformation.GetInstance().tFileCfg.columns;
            numericUpDownRow.Value = ConfigInformation.GetInstance().tFileCfg.rows;
            if (ConfigInformation.GetInstance().tFileCfg.channelNum==1)
            {
                comboBoxChannel.SelectedIndex=0;
            } 
            else
            {
                comboBoxChannel.SelectedIndex = 1;
            }
            
        }

        private void textBoxFilePath_TextChanged(object sender, EventArgs e)
        {
            if (textBoxFilePath.Text != null && Directory.Exists(textBoxFilePath.Text))
            {
                autoImagePath = textBoxFilePath.Text;
            }
            else
            {
                textBoxFilePath.Text = autoImagePath;
                MessageBox.Show("图像导入文件夹路径有误！请修改！");
            }
        }

        private void pictureBoxImage_SizeChanged(object sender, EventArgs e)
        {
            try
            {

                if (FileInputHWHandle != null)
                {
                    HOperatorSet.SetWindowExtents(FileInputHWHandle, 0, 0, (pictureBoxImage.Width), (pictureBoxImage.Height));
                    HOperatorSet.SetPart(FileInputHWHandle, 0, 0, (pictureBoxImage.Height - 1), (pictureBoxImage.Width - 1));
                    if (image != null)
                    {
                        basicClass.getImageSize(image, out rowNumber, out columnNumber);
                        double widRat = pictureBoxImage.Width / ((double)columnNumber);
                        double heiRat = pictureBoxImage.Height / ((double)rowNumber);
                        resizerate = widRat < heiRat ? widRat : heiRat;
                        if (img != null)
                        {
                            img.Dispose();
                        }
                        basicClass.resizeImage(image, out img, resizerate);
                        basicClass.displayClear(FileInputHWHandle);
                        basicClass.displayhobject(img, FileInputHWHandle);
                        oriPictureBoxShowImageWidth = pictureBoxImage.Width;
                        oriPictureBoxShowImageHeight = pictureBoxImage.Height;
                    }
                }
            }
            catch (System.Exception ex)
            {

            }
        }

        private void buttonShowImage_Click(object sender, EventArgs e)
        {
            try
            {
                if (image != null)
                {
                    image.Dispose();
                }
                HTuple imageChannelNum;
                HOperatorSet.ReadImage(out image, (HTuple)fnFileNameList[listBoxFileList.SelectedIndex]);
                basicClass.getImageSize(image, out rowNumber, out columnNumber);
                HOperatorSet.CountChannels(image, out imageChannelNum);
                labelImageInfo.Text = rowNumber.ToString() + "行 " + columnNumber.ToString() + "列 " + ((int)imageChannelNum).ToString() + "通道 ";
                double widRat = pictureBoxImage.Width / ((double)columnNumber);
                double heiRat = pictureBoxImage.Height / ((double)rowNumber);
                resizerate = widRat < heiRat ? widRat : heiRat;
                if (img != null)
                {
                    img.Dispose();
                }
                basicClass.resizeImage(image, out img, resizerate);
                basicClass.displayClear(FileInputHWHandle);
                basicClass.displayhobject(img, FileInputHWHandle);
                if (image != null)
                {
                    image.Dispose();
                }
                if (img != null)
                {
                    img.Dispose();
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void listBoxFileList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            try
            {
                fnFileNameList.RemoveAt(listBoxFileList.SelectedIndex);
                listBoxFileList.Items.RemoveAt(listBoxFileList.SelectedIndex);                
                ImageNum--;
                basicClass.displayClear(FileInputHWHandle);
            }
            catch (System.Exception ex)
            {

            }
        }

        private void buttonConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                int channeltest;
                if (comboBoxChannel.SelectedIndex == 0)
                {
                    channeltest = 1;
                }
                else
                {
                    channeltest = 3;
                }
                int rNum, cNum;
                HTuple imgCNum;
                HOperatorSet.ReadImage(out image, (HTuple)fnFileNameList[listBoxFileList.SelectedIndex]);
                basicClass.getImageSize(image, out rNum, out cNum);
                HOperatorSet.CountChannels(image, out imgCNum);
                labelImageInfo.Text = rNum.ToString() + "行 " + cNum.ToString() + "列 " + ((int)imgCNum).ToString() + "通道 ";
                if (rNum == (int)numericUpDownRow.Value && cNum == (int)numericUpDownColumn.Value &&
                    imgCNum == channeltest)
                {
                    double widRat = pictureBoxImage.Width / ((double)cNum);
                    double heiRat = pictureBoxImage.Height / ((double)rNum);
                    resizerate = widRat < heiRat ? widRat : heiRat;
                    if (img != null)
                    {
                        img.Dispose();
                    }
                    basicClass.resizeImage(image, out img, resizerate);
                    basicClass.displayClear(FileInputHWHandle);
                    basicClass.displayhobject(img, FileInputHWHandle);

                    HOperatorSet.CopyImage(image, out UserCode.GetInstance().fileImages);
                    Svision.GetMe().fileNameList.Clear();
                    Svision.GetMe().fileNameList = fnFileNameList;
                    ConfigInformation.GetInstance().tFileCfg.columns = (int)numericUpDownColumn.Value;
                    ConfigInformation.GetInstance().tFileCfg.rows = (int)numericUpDownRow.Value;
                    if (comboBoxChannel.SelectedIndex == 0)
                    {
                        ConfigInformation.GetInstance().tFileCfg.channelNum = 1;
                    }
                    else
                    {
                        ConfigInformation.GetInstance().tFileCfg.channelNum = 3;
                    }
                    Svision.GetMe().fileNumIdx = listBoxFileList.SelectedIndex;
                    Svision.GetMe().textBoxFileNum.Text = (Svision.GetMe().fileNumIdx + 1).ToString();
                    Svision.GetMe().textBoxAllFileNum.Text = ImageNum.ToString();
                    if (Svision.GetMe().checkBoxIsFile.Checked)
                    {
                        Svision.GetMe().oriRowNumber = ConfigInformation.GetInstance().tFileCfg.rows;
                        Svision.GetMe().oriColumnNumber = ConfigInformation.GetInstance().tFileCfg.columns;
                    }
                    if (image != null)
                    {
                        image.Dispose();
                    }
                    if (img != null)
                    {
                        img.Dispose();
                    }
                    this.Close();
                }
                else
                {
                    if (image != null)
                    {
                        image.Dispose();
                    }
                    if (img != null)
                    {
                        img.Dispose();
                    }
                    throw new Exception("所选择图像参数与当前图像文件参数设置不符！请修改！");
                }



            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Svision.GetMe().textBoxFileNum.Text = (Svision.GetMe().fileNumIdx + 1).ToString();
            this.Close();
        }











    }
}
