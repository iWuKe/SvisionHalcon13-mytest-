/**************************************************************************************
**
**       Filename:  ThresholdFBDForm.cs
**
**    Description:  Threshold Function Block use for user code
**
**        Version:  1.0
**        Created:  2016-1-29
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
using HalconDotNet;

namespace Svision
{
    public partial class ThresholdFBDForm : Form
    {
        private int currentIdx;
        private HTuple ThresholdHWHandle;
        HObject image, img, imgRegionResult, imgResult;
        double resizerate;
        int rowNumber, columnNumber;
        //int oriRowNumber, oriColumnNumber;
        int oriPictureBoxShowImageWidth, oriPictureBoxShowImageHeight;
        public ThresholdFBDForm(int tCIdx)
        {
            InitializeComponent();
            this.trackBarMaxGray.SetRange(0, 255);
            this.trackBarMinGray.SetRange(0, 255);
            currentIdx = tCIdx;
            numericUpDownMaxGray.Value = (decimal)UserCode.GetInstance().gProCd[currentIdx].gTP.maxValue;
            numericUpDownMinGray.Value = (decimal)UserCode.GetInstance().gProCd[currentIdx].gTP.minValue;
        }

        public void trackBarMaxGray_Scroll(object sender, EventArgs e)
        {
            numericUpDownMaxGray.Value = (decimal)this.trackBarMaxGray.Value;
            numericUpDownMinGray.Maximum = (decimal)this.trackBarMaxGray.Value;
            trackBarMinGray.Maximum = (int)this.trackBarMaxGray.Value;
        }

        public void trackBarMinGray_Scroll(object sender, EventArgs e)
        {
            numericUpDownMinGray.Value = (decimal)this.trackBarMinGray.Value;
        }

        public void buttonConfirm_Click(object sender, EventArgs e)
        {
            float tMaxGray = (float)numericUpDownMaxGray.Value;
            float tMinGray = (float)numericUpDownMinGray.Value;
            if (tMinGray < tMaxGray)
            {
                UserCode.GetInstance().gProCd[currentIdx].gTP.maxValue= tMaxGray;
                UserCode.GetInstance().gProCd[currentIdx].gTP.minValue= tMinGray;
                this.Close();
            }
            else
            {
                MessageBox.Show("输入的最小灰度值需要大于最大灰度值！");
            }
        }

        public void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void ThresholdFBDForm_Load(object sender, EventArgs e)
        {
            HOperatorSet.OpenWindow(0, 0, (pictureBoxThreshold.Width), (pictureBoxThreshold.Height), pictureBoxThreshold.Handle, "visible", "", out ThresholdHWHandle);
            HOperatorSet.SetPart(ThresholdHWHandle, 0, 0, (pictureBoxThreshold.Height-1), (pictureBoxThreshold.Width-1));
            HDevWindowStack.Push(ThresholdHWHandle);

            panelenable.Enabled = false;
            oriPictureBoxShowImageWidth = pictureBoxThreshold.Width;
            oriPictureBoxShowImageHeight = pictureBoxThreshold.Height;
        }

        private void pictureBoxThreshold_SizeChanged(object sender, EventArgs e)
        {
            try
            {
                if (ThresholdHWHandle != null)
                {
                    HOperatorSet.SetWindowExtents(ThresholdHWHandle, 0, 0, (pictureBoxThreshold.Width), (pictureBoxThreshold.Height));
                    HOperatorSet.SetPart(ThresholdHWHandle, 0, 0, (pictureBoxThreshold.Height - 1), (pictureBoxThreshold.Width - 1));
                    double widRat = pictureBoxThreshold.Width / ((double)columnNumber);
                    double heiRat = pictureBoxThreshold.Height / ((double)rowNumber);
                    resizerate = widRat < heiRat ? widRat : heiRat;
                    if (image != null)
                    {
                        if (img != null)
                        {
                            img.Dispose();
                        }
                        basicClass.resizeImage(image, out img, resizerate);
                        basicClass.thresholdImage(img, out imgRegionResult, (float)numericUpDownMinGray.Value, (float)numericUpDownMaxGray.Value);
                        basicClass.displayhobject(imgRegionResult, ThresholdHWHandle);
                    }
                }
            }
            catch (System.Exception ex)
            {
            	
            }
            
            
        }

        private void buttonGetImageFrame_Click(object sender, EventArgs e)
        {
            try
            {
                //oriRowNumber = rowNumber;
                //oriColumnNumber = columnNumber;
                if (image != null)
                {
                    image.Dispose();
                }
                //Svision.GetMe().baslerCamera.getFrameImage(out image);
                UserCode.GetInstance().getImageFromProcess(out image, currentIdx);
                basicClass.getImageSize(image, out rowNumber, out columnNumber);
                double widRat = pictureBoxThreshold.Width / ((double)columnNumber);
                double heiRat = pictureBoxThreshold.Height / ((double)rowNumber);
                resizerate = widRat < heiRat ? widRat : heiRat;
                if (img != null)
                {
                    img.Dispose();
                }
                basicClass.resizeImage(image, out img, resizerate);
                //if (oriRowNumber != rowNumber || oriColumnNumber != columnNumber)
                //{
                //    basicClass.displayClear(ThresholdHWHandle);
                //}

                if (oriPictureBoxShowImageWidth != pictureBoxThreshold.Width)
                {
                    basicClass.displayClear(ThresholdHWHandle);
                }
                if (oriPictureBoxShowImageHeight != pictureBoxThreshold.Height)
                {
                    basicClass.displayClear(ThresholdHWHandle);
                }
                int rown, columnn;
                basicClass.thresholdImage(img, out imgRegionResult, (float)numericUpDownMinGray.Value, (float)numericUpDownMaxGray.Value);
                basicClass.getImageSize(img, out rown, out columnn);
                if (imgResult != null)
                {
                    imgResult.Dispose();
                }
                HOperatorSet.RegionToBin(imgRegionResult, out imgResult, 255, 0, columnn, rown);
                basicClass.displayhobject(imgResult, ThresholdHWHandle);
                panelenable.Enabled = true;
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


        private void numericUpDownMaxGray_ValueChanged(object sender, EventArgs e)
        {
            trackBarMaxGray.Value = (int)numericUpDownMaxGray.Value;
            trackBarMinGray.Maximum = (int)numericUpDownMaxGray.Value;
            numericUpDownMinGray.Maximum = (decimal)numericUpDownMaxGray.Value;
            int rown,columnn;
            if (img!=null)
            {
                basicClass.thresholdImage(img, out imgRegionResult, (float)numericUpDownMinGray.Value, (float)numericUpDownMaxGray.Value);
                basicClass.getImageSize(img, out rown, out columnn);
                if (imgResult!=null)
                {
                    imgResult.Dispose();
                }
                HOperatorSet.RegionToBin(imgRegionResult, out imgResult, 255, 0, columnn, rown);
                basicClass.displayhobject(imgResult, ThresholdHWHandle);
            }
        }

        private void numericUpDownMinGray_ValueChanged(object sender, EventArgs e)
        {
            trackBarMinGray.Value = (int)numericUpDownMinGray.Value;
            int rown, columnn;
            if (img != null)
            {
                basicClass.thresholdImage(img, out imgRegionResult, (float)numericUpDownMinGray.Value, (float)numericUpDownMaxGray.Value);
                basicClass.getImageSize(img, out rown, out columnn);
                if (imgResult != null)
                {
                    imgResult.Dispose();
                }
                HOperatorSet.RegionToBin(imgRegionResult, out imgResult, 255, 0, columnn, rown);
                basicClass.displayhobject(imgResult, ThresholdHWHandle);
            }
        }



    }
}
