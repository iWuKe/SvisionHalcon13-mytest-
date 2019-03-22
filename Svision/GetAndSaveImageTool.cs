using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HalconDotNet;
using System.IO;

namespace Svision
{
    public partial class GetAndSaveImageTool : Form
    {
        int currentImageNum = 0;
        private string autoImagePath = null;
        private HTuple GetAndSaveImageHWHandle;
        HObject image, img;
        double resizerate;
        int rowNumber, columnNumber;
        int oriPictureBoxShowImageWidth, oriPictureBoxShowImageHeight;
        public GetAndSaveImageTool()
        {
            try
            {
                InitializeComponent();
            }
            catch (System.Exception ex)
            {
                System.Threading.Thread.Sleep(100);
                if (Svision.GetMe().baslerCamera != null && Svision.GetMe().baslerCamera.camera.IsOpen && Svision.GetMe().baslerCamera.camera.IsConnected)
                {
                    Svision.GetMe().textBoxIsCameraOpenedAndConnected.Text = "是";
                    MessageBox.Show(ex.Message);
                }
                else
                {
                    Cursor = Cursors.WaitCursor;
                    Svision.GetMe().textBoxIsCameraOpenedAndConnected.Text = "否";
                    MessageBox.Show("相机连接已断开，请检查连接网线！开始自动刷新相机状态！");
                    Svision.GetMe().textBoxIsCameraOpenedAndConnected.Text = "正在刷新";
                    Svision.GetMe().cameraRefresh();
                    Svision.GetMe().textBoxIsCameraOpenedAndConnected.Text = "是";
                    Cursor = Cursors.Default;
                }
            }
            
        }

        private void GetAndSaveImageTool_Load(object sender, EventArgs e)
        {
            HOperatorSet.OpenWindow(0, 0, (pictureBoxGetAndSaveImage.Width), (pictureBoxGetAndSaveImage.Height), pictureBoxGetAndSaveImage.Handle, "visible", "", out GetAndSaveImageHWHandle);
            HOperatorSet.SetPart(GetAndSaveImageHWHandle, 0, 0, (pictureBoxGetAndSaveImage.Height), (pictureBoxGetAndSaveImage.Width));
            HDevWindowStack.Push(GetAndSaveImageHWHandle);
            oriPictureBoxShowImageWidth = pictureBoxGetAndSaveImage.Width;
            oriPictureBoxShowImageHeight = pictureBoxGetAndSaveImage.Height;
        }
        ~GetAndSaveImageTool()
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
        private void pictureBoxGetAndSaveImage_SizeChanged(object sender, EventArgs e)
        {
            try
            {

                if (GetAndSaveImageHWHandle != null)
                {
                    HOperatorSet.SetWindowExtents(GetAndSaveImageHWHandle, 0, 0, (pictureBoxGetAndSaveImage.Width), (pictureBoxGetAndSaveImage.Height));
                    HOperatorSet.SetPart(GetAndSaveImageHWHandle, 0, 0, (pictureBoxGetAndSaveImage.Height - 1), (pictureBoxGetAndSaveImage.Width - 1));
                    if (image != null)
                    {
                        basicClass.getImageSize(image, out rowNumber, out columnNumber);
                        double widRat = pictureBoxGetAndSaveImage.Width / ((double)columnNumber);
                        double heiRat = pictureBoxGetAndSaveImage.Height / ((double)rowNumber);
                        resizerate = widRat < heiRat ? widRat : heiRat;
                        if (img!=null)
                        {
                            img.Dispose();
                        }
                        basicClass.resizeImage(image, out img, resizerate);
                        basicClass.displayClear(GetAndSaveImageHWHandle);
                        basicClass.displayhobject(img, GetAndSaveImageHWHandle);
                        oriPictureBoxShowImageWidth = pictureBoxGetAndSaveImage.Width;
                        oriPictureBoxShowImageHeight = pictureBoxGetAndSaveImage.Height;
                    }
                }
            }
            catch (System.Exception ex)
            {

            }
        }

        private void buttonGetFolderPath_Click(object sender, EventArgs e)
        {
            try
            {

                if (folderBrowserDialogGetFolderPath.ShowDialog() == DialogResult.OK)
                {

                    autoImagePath = folderBrowserDialogGetFolderPath.SelectedPath;

                    this.textBoxGetFolderPath.Text = autoImagePath;
                }

            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonBeginOrStopSaveImage_Click(object sender, EventArgs e)
        {
            if (timerAuto.Enabled == false)
            {
                if (autoImagePath != null && Directory.Exists(autoImagePath))
                {
                    timerAuto.Interval = (int)numericUpDownTime.Value;
                    panelAuto.Enabled = false;
                    groupBoxManual.Enabled = false;
                    timerAuto.Enabled = true;
                    buttonBeginOrStopSaveImage.Text = "停止保存图像";
                } 
                else
                {
                    MessageBox.Show("图像保存文件夹路径有误！请修改！");
                }
                
            }
            else
            {
                groupBoxManual.Enabled = true;
                panelAuto.Enabled = true;
                timerAuto.Enabled = false;
                buttonBeginOrStopSaveImage.Text = "开始保存图像";
            }
        }

        private void timerAuto_Tick(object sender, EventArgs e)
        {
            if (Svision.GetMe().baslerCamera != null && Svision.GetMe().baslerCamera.camera.IsOpen && Svision.GetMe().baslerCamera.camera.IsConnected)
            {
                Svision.GetMe().textBoxIsCameraOpenedAndConnected.Text = "是";
                try
                {
                    if (image != null)
                    {
                        image.Dispose();
                    }
                    Svision.GetMe().baslerCamera.getFrameImageWithStart(out image);
                    if (ConfigInformation.GetInstance().tCalCfg.calibrationIsRadialDistortionFlag)
                    {
                        HOperatorSet.MapImage(image, ConfigInformation.GetInstance().tCalCfg.ho_MapFixed, out image);
                    }
                    basicClass.getImageSize(image, out rowNumber, out columnNumber);
                    double widRat = pictureBoxGetAndSaveImage.Width / ((double)columnNumber);
                    double heiRat = pictureBoxGetAndSaveImage.Height / ((double)rowNumber);
                    resizerate = widRat < heiRat ? widRat : heiRat;
                    if (img != null)
                    {
                        img.Dispose();
                    }
                    basicClass.resizeImage(image, out img, resizerate);
                    if (oriPictureBoxShowImageWidth != pictureBoxGetAndSaveImage.Width)
                    {
                        basicClass.displayClear(GetAndSaveImageHWHandle);
                    }
                    if (oriPictureBoxShowImageHeight != pictureBoxGetAndSaveImage.Height)
                    {
                        basicClass.displayClear(GetAndSaveImageHWHandle);
                    }
                    basicClass.displayhobject(img, GetAndSaveImageHWHandle);
                    textBoxCurrentNum.Text = currentImageNum.ToString();
                    HOperatorSet.WriteImage(image, "bmp", 0, autoImagePath + "//image" + System.String.Format("{0:D5}", currentImageNum) + ".bmp");
                    currentImageNum++;
                }
                catch (Exception ex)
                {
                    timerAuto.Enabled = false;
                    System.Threading.Thread.Sleep(100);
                    if (Svision.GetMe().baslerCamera != null && Svision.GetMe().baslerCamera.camera.IsOpen && Svision.GetMe().baslerCamera.camera.IsConnected)
                    {
                        groupBoxManual.Enabled = true;
                        panelAuto.Enabled = true;
                        
                        buttonBeginOrStopSaveImage.Text = "开始保存图像";
                        MessageBox.Show(ex.Message);
                    }
                    else
                    {
                        groupBoxManual.Enabled = false;
                        panelAuto.Enabled = false;
                        buttonBeginOrStopSaveImage.Text = "开始保存图像";
                        Cursor = Cursors.WaitCursor;
                        Svision.GetMe().textBoxIsCameraOpenedAndConnected.Text = "否";
                        MessageBox.Show("相机连接已断开，请检查连接网线！开始自动刷新相机状态！");
                        Svision.GetMe().textBoxIsCameraOpenedAndConnected.Text = "正在刷新";
                        Svision.GetMe().cameraRefresh();
                        Svision.GetMe().textBoxIsCameraOpenedAndConnected.Text = "是";
                        Cursor = Cursors.Default;

                        timerAuto.Enabled = true;
                        buttonBeginOrStopSaveImage.Text = "停止保存图像";

                    }

                }
            }
        }

        private void textBoxGetFolderPath_TextChanged(object sender, EventArgs e)
        {
            if (textBoxGetFolderPath.Text != null && Directory.Exists(textBoxGetFolderPath.Text))
            {
                autoImagePath = textBoxGetFolderPath.Text;
            } 
            else
            {
               textBoxGetFolderPath.Text = autoImagePath  ;
               MessageBox.Show("图像保存文件夹路径有误！请修改");
            }
        }

        private void buttonGetImage_Click(object sender, EventArgs e)
        {
            if (Svision.GetMe().baslerCamera != null && Svision.GetMe().baslerCamera.camera.IsOpen && Svision.GetMe().baslerCamera.camera.IsConnected)
            {
                Svision.GetMe().textBoxIsCameraOpenedAndConnected.Text = "是";
                try
                {
                    if (image != null)
                    {
                        image.Dispose();
                    }
                    Svision.GetMe().baslerCamera.getFrameImageWithStart(out image);
                    if (ConfigInformation.GetInstance().tCalCfg.calibrationIsRadialDistortionFlag)
                    {
                        HOperatorSet.MapImage(image, ConfigInformation.GetInstance().tCalCfg.ho_MapFixed, out image);
                    }
                    basicClass.getImageSize(image, out rowNumber, out columnNumber);
                    double widRat = pictureBoxGetAndSaveImage.Width / ((double)columnNumber);
                    double heiRat = pictureBoxGetAndSaveImage.Height / ((double)rowNumber);
                    resizerate = widRat < heiRat ? widRat : heiRat;
                    if (img != null)
                    {
                        img.Dispose();
                    }
                    basicClass.resizeImage(image, out img, resizerate);
                    if (oriPictureBoxShowImageWidth != pictureBoxGetAndSaveImage.Width)
                    {
                        basicClass.displayClear(GetAndSaveImageHWHandle);
                    }
                    if (oriPictureBoxShowImageHeight != pictureBoxGetAndSaveImage.Height)
                    {
                        basicClass.displayClear(GetAndSaveImageHWHandle);
                    }
                    basicClass.displayhobject(img, GetAndSaveImageHWHandle);
                }
                catch (Exception ex)
                {
                    System.Threading.Thread.Sleep(100);
                    if (Svision.GetMe().baslerCamera != null && Svision.GetMe().baslerCamera.camera.IsOpen && Svision.GetMe().baslerCamera.camera.IsConnected)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    else
                    {
                        groupBoxManual.Enabled = false;
                        groupBoxAuto.Enabled = false;

                        Cursor = Cursors.WaitCursor;
                        Svision.GetMe().textBoxIsCameraOpenedAndConnected.Text = "否";
                        MessageBox.Show("相机连接已断开，请检查连接网线！开始自动刷新相机状态！");
                        Svision.GetMe().textBoxIsCameraOpenedAndConnected.Text = "正在刷新";
                        Svision.GetMe().cameraRefresh();
                        Svision.GetMe().textBoxIsCameraOpenedAndConnected.Text = "是";
                        Cursor = Cursors.Default;
                        groupBoxManual.Enabled = true;
                        groupBoxAuto.Enabled = true;

                    }

                }
            }
        }

        private void buttonSaveImage_Click(object sender, EventArgs e)
        {
            try
            {
                saveFileDialogManual.Filter = "Image Files|*.bmp;*.jpg;*.jpeg;*.png;*.tiff|所有文件（*.*)|*.*";
                saveFileDialogManual.FilterIndex = 1;
                saveFileDialogManual.RestoreDirectory = true;
                if (saveFileDialogManual.ShowDialog() == DialogResult.OK)
                {
                    HOperatorSet.WriteImage(image, "bmp", 0, saveFileDialogManual.FileName);
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            
        }
    }
}
