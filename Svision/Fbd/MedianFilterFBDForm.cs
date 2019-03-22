/**************************************************************************************
**
**       Filename:  MedianFilterFBDForm.cs
**
**    Description:  MedianFilter Function Block use for user code
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
using HalconDotNet;
namespace Svision
{
    public partial class MedianFilterFBDForm : Form
    {
        private int currentIndex;
        private HTuple MedianHWHandle;
        HObject image, img, imgResult;
        double resizerate;
        int rowNumber, columnNumber;
        int maskLength;
        //int oriRowNumber, oriColumnNumber;
        int oriPictureBoxShowImageWidth, oriPictureBoxShowImageHeight;
        public MedianFilterFBDForm(int cIdx)
        {
            InitializeComponent();


            currentIndex = cIdx;

            switch (UserCode.GetInstance().gProCd[currentIndex].gMFP.maskSize)
            {
                case 0:
                    radioButtonMedianFilter3.Checked = true;
                    maskLength = 3;
                    break;
                case 1:
                    radioButtonMedianFilter5.Checked = true;
                    maskLength = 5;
                    break;
                case 2:
                    radioButtonMedianFilter7.Checked = true;
                    maskLength = 7;
                    break;
                case 3:
                    radioButtonMedianFilter9.Checked = true;
                    maskLength = 9;
                    break;
                case 4:
                    radioButtonMedianFilter11.Checked = true;
                    maskLength = 11;
                    break;
                default:
                    ;
                    break;
            }
        }

        private void buttonMedianFilterFBDConfirm_Click(object sender, EventArgs e)
        {
            if (radioButtonMedianFilter3.Checked == true)
            {
                UserCode.GetInstance().gProCd[currentIndex].gMFP.maskSize = 0;
            }
            else if (radioButtonMedianFilter5.Checked == true)
            {
                UserCode.GetInstance().gProCd[currentIndex].gMFP.maskSize = 1;
            }
            else if (radioButtonMedianFilter7.Checked == true)
            {
                UserCode.GetInstance().gProCd[currentIndex].gMFP.maskSize = 2;
            }
            else if (radioButtonMedianFilter9.Checked == true)
            {
                UserCode.GetInstance().gProCd[currentIndex].gMFP.maskSize = 3;
            }
            else if (radioButtonMedianFilter11.Checked == true)
            {
                UserCode.GetInstance().gProCd[currentIndex].gMFP.maskSize = 4;
            }
            this.Hide();
        }

        private void buttonMedianFilterFBDCancel_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void MedianFilterFBDForm_Load(object sender, System.EventArgs e)
        {
            HOperatorSet.OpenWindow(0, 0, (pictureBoxMedian.Width), (pictureBoxMedian.Height), pictureBoxMedian.Handle, "visible", "", out MedianHWHandle);
            HOperatorSet.SetPart(MedianHWHandle, 0, 0, (pictureBoxMedian.Height-1), (pictureBoxMedian.Width-1));
            HDevWindowStack.Push(MedianHWHandle);

            groupBoxMedianFilterFBDMaskSize.Enabled = false;
            oriPictureBoxShowImageWidth = pictureBoxMedian.Width;
            oriPictureBoxShowImageHeight = pictureBoxMedian.Height;
        }

        private void buttongetImageFrame_Click(object sender, System.EventArgs e)
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
                UserCode.GetInstance().getImageFromProcess(out image, currentIndex);
                basicClass.getImageSize(image, out rowNumber, out columnNumber);
                double widRat = pictureBoxMedian.Width / ((double)columnNumber);
                double heiRat = pictureBoxMedian.Height / ((double)rowNumber);
                resizerate = widRat < heiRat ? widRat : heiRat;
                if (img != null)
                {
                    img.Dispose();
                }
                basicClass.resizeImage(image, out img, resizerate);
                //if (oriRowNumber != rowNumber || oriColumnNumber != columnNumber)
                //{
                //    basicClass.displayClear(MedianHWHandle);
                //}

                if (oriPictureBoxShowImageWidth != pictureBoxMedian.Width)
                {
                    basicClass.displayClear(MedianHWHandle);
                }
                if (oriPictureBoxShowImageHeight != pictureBoxMedian.Height)
                {
                    basicClass.displayClear(MedianHWHandle);
                }
                if (imgResult != null)
                {
                    imgResult.Dispose();
                }
                HOperatorSet.MedianImage(img, out imgResult, "square", maskLength, "mirrored");
                basicClass.displayhobject(imgResult, MedianHWHandle);
                groupBoxMedianFilterFBDMaskSize.Enabled = true;
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


        private void pictureBoxMedian_SizeChanged(object sender, System.EventArgs e)
        {
            try
            {
                if (MedianHWHandle!=null)
                {
                    HOperatorSet.SetWindowExtents(MedianHWHandle, 0, 0, (pictureBoxMedian.Width), (pictureBoxMedian.Height));
                    HOperatorSet.SetPart(MedianHWHandle, 0, 0, (pictureBoxMedian.Height - 1), (pictureBoxMedian.Width - 1));
                    double widRat = pictureBoxMedian.Width / ((double)columnNumber);
                    double heiRat = pictureBoxMedian.Height / ((double)rowNumber);
                    resizerate = widRat < heiRat ? widRat : heiRat;
                    if (image != null)
                    {
                        if (img != null)
                        {
                            img.Dispose();
                        }
                        basicClass.resizeImage(image, out img, resizerate);
                        if (imgResult != null)
                        {
                            imgResult.Dispose();
                        }
                        HOperatorSet.MedianImage(img, out imgResult, "square", maskLength, "mirrored");
                        basicClass.displayhobject(imgResult, MedianHWHandle);
                    }
                }
                
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void radioButtonMedianFilter3_CheckedChanged(object sender, System.EventArgs e)
        {
            try
            {
                maskLength = 3;
                if (img!=null)
                {
                    if (imgResult != null)
                    {
                        imgResult.Dispose();
                    }
                    HOperatorSet.MedianImage(img, out imgResult, "square", maskLength, "mirrored");
                    basicClass.displayhobject(imgResult, MedianHWHandle);
                }

            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void radioButtonMedianFilter5_CheckedChanged(object sender, System.EventArgs e)
        {
            try
            {
                maskLength = 5;
                if (img != null)
                {
                    if (imgResult != null)
                    {
                        imgResult.Dispose();
                    }
                    HOperatorSet.MedianImage(img, out imgResult, "square", maskLength, "mirrored");
                    basicClass.displayhobject(imgResult, MedianHWHandle);
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void radioButtonMedianFilter7_CheckedChanged(object sender, System.EventArgs e)
        {
            try
            {
                maskLength = 7;
                if (img != null)
                {
                    if (imgResult != null)
                    {
                        imgResult.Dispose();
                    }
                    HOperatorSet.MedianImage(img, out imgResult, "square", maskLength, "mirrored");
                    basicClass.displayhobject(imgResult, MedianHWHandle);
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void radioButtonMedianFilter9_CheckedChanged(object sender, System.EventArgs e)
        {
            try
            {
                maskLength = 9;
                if (img != null)
                {
                    if (imgResult != null)
                    {
                        imgResult.Dispose();
                    }
                    HOperatorSet.MedianImage(img, out imgResult, "square", maskLength, "mirrored");
                    basicClass.displayhobject(imgResult, MedianHWHandle);
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void radioButtonMedianFilter11_CheckedChanged(object sender, System.EventArgs e)
        {
            try
            {
                maskLength = 11;
                if (img != null)
                {
                    if (imgResult != null)
                    {
                        imgResult.Dispose();
                    }
                    HOperatorSet.MedianImage(img, out imgResult, "square", maskLength, "mirrored");
                    basicClass.displayhobject(imgResult, MedianHWHandle);
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
