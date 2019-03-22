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
    public partial class MorphologyProcessingFBDForm : Form
    {
        private int currentIndex;
        private HTuple MorphologyProcessingHWHandle;
        HObject image, img, imgRegionResult, imageRegion,imgResult;
        double resizerate;
        int rowNumber, columnNumber;
        int imgrowNumber, imgcolumnNumber;
        //int oriRowNumber, oriColumnNumber;
        int oriPictureBoxShowImageWidth, oriPictureBoxShowImageHeight;
        public MorphologyProcessingFBDForm(int idx)
        {
            InitializeComponent();
            currentIndex = idx;
            comboBoxMorphology.SelectedIndex = UserCode.GetInstance().gProCd[currentIndex].gMPP.processID;
            if (UserCode.GetInstance().gProCd[currentIndex].gMPP.elementID == 0)
            {
                radioButtonRectangle.Checked = true;
            } 
            else
            {
                radioButtonCircle.Checked = true;
            }
            numericUpDownWidth.Value = (decimal)UserCode.GetInstance().gProCd[currentIndex].gMPP.width;
            numericUpDownHeight.Value = (decimal)UserCode.GetInstance().gProCd[currentIndex].gMPP.height;
            numericUpDownRadius.Value = (decimal)UserCode.GetInstance().gProCd[currentIndex].gMPP.radius;
        }

        private void MorphologyProcessingFBDForm_Load(object sender, EventArgs e)
        {

            HOperatorSet.OpenWindow(0, 0, (pictureBoxMorphologyProcessing.Width), (pictureBoxMorphologyProcessing.Height), pictureBoxMorphologyProcessing.Handle, "visible", "", out MorphologyProcessingHWHandle);
            HOperatorSet.SetPart(MorphologyProcessingHWHandle, 0, 0, (pictureBoxMorphologyProcessing.Height - 1), (pictureBoxMorphologyProcessing.Width - 1));
            HDevWindowStack.Push(MorphologyProcessingHWHandle);

            //panelenable.Enabled = false;
            oriPictureBoxShowImageWidth = pictureBoxMorphologyProcessing.Width;
            oriPictureBoxShowImageHeight = pictureBoxMorphologyProcessing.Height;
            MessageBox.Show("注意：该模块输入默认为二值图像，否则将会被强制转化为二值图像！");
        }
        private void showImage()
        {
            if (imgRegionResult!=null)
            {
                switch (comboBoxMorphology.SelectedIndex)
                {
                    case 0:
                        if (radioButtonRectangle.Checked)
                        {
                            HOperatorSet.ErosionRectangle1(imgRegionResult, out imageRegion, (double)numericUpDownWidth.Value, (double)numericUpDownHeight.Value);
                        }
                        else
                        {
                            HOperatorSet.ErosionCircle(imgRegionResult, out imageRegion, (double)numericUpDownRadius.Value);
                        }

                        break;
                    case 1:
                        if (radioButtonRectangle.Checked)
                        {
                            HOperatorSet.DilationRectangle1(imgRegionResult, out imageRegion, (double)numericUpDownWidth.Value, (double)numericUpDownHeight.Value);
                        }
                        else
                        {
                            HOperatorSet.DilationCircle(imgRegionResult, out imageRegion, (double)numericUpDownRadius.Value);
                        }
                        break;
                    case 2:
                        if (radioButtonRectangle.Checked)
                        {
                            HOperatorSet.OpeningRectangle1(imgRegionResult, out imageRegion, (double)numericUpDownWidth.Value, (double)numericUpDownHeight.Value);
                        }
                        else
                        {
                            HOperatorSet.OpeningCircle(imgRegionResult, out imageRegion, (double)numericUpDownRadius.Value);
                        }
                        break;
                    case 3:
                        if (radioButtonRectangle.Checked)
                        {
                            HOperatorSet.ClosingRectangle1(imgRegionResult, out imageRegion, (double)numericUpDownWidth.Value, (double)numericUpDownHeight.Value);
                        }
                        else
                        {
                            HOperatorSet.ClosingCircle(imgRegionResult, out imageRegion, (double)numericUpDownRadius.Value);
                        }
                        break;

                    default:
                        break;
                }

                if (imgResult != null)
                {
                    imgResult.Dispose();
                }
                HOperatorSet.RegionToBin(imageRegion, out imgResult, 255, 0, imgcolumnNumber, imgrowNumber);
                basicClass.displayhobject(imgResult, MorphologyProcessingHWHandle);
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
                UserCode.GetInstance().getImageFromProcess(out image, currentIndex);
                //Svision.GetMe().baslerCamera.getFrameImage(out image);
                basicClass.getImageSize(image, out rowNumber, out columnNumber);
                double widRat = pictureBoxMorphologyProcessing.Width / ((double)columnNumber);
                double heiRat = pictureBoxMorphologyProcessing.Height / ((double)rowNumber);
                resizerate = widRat < heiRat ? widRat : heiRat;
                if (img != null)
                {
                    img.Dispose();
                }
                basicClass.resizeImage(image, out img, resizerate);
                basicClass.getImageSize(img, out imgrowNumber, out imgcolumnNumber);
                //if (oriRowNumber != rowNumber || oriColumnNumber != columnNumber)
                //{
                //    basicClass.displayClear(ThresholdHWHandle);
                //}

                if (oriPictureBoxShowImageWidth != pictureBoxMorphologyProcessing.Width)
                {
                    basicClass.displayClear(MorphologyProcessingHWHandle);
                }
                if (oriPictureBoxShowImageHeight != pictureBoxMorphologyProcessing.Height)
                {
                    basicClass.displayClear(MorphologyProcessingHWHandle);
                }
                
                basicClass.thresholdImage(img, out imgRegionResult,128,255);
                showImage();


               // panelenable.Enabled = true;
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

        private void pictureBoxMorphologyProcessing_SizeChanged(object sender, EventArgs e)
        {
            if (MorphologyProcessingHWHandle!=null)
            {
                HOperatorSet.SetWindowExtents(MorphologyProcessingHWHandle, 0, 0, (pictureBoxMorphologyProcessing.Width), (pictureBoxMorphologyProcessing.Height));
                HOperatorSet.SetPart(MorphologyProcessingHWHandle, 0, 0, (pictureBoxMorphologyProcessing.Height - 1), (pictureBoxMorphologyProcessing.Width - 1));
                double widRat = pictureBoxMorphologyProcessing.Width / ((double)columnNumber);
                double heiRat = pictureBoxMorphologyProcessing.Height / ((double)rowNumber);
                resizerate = widRat < heiRat ? widRat : heiRat;
                if (image != null)
                {
                    if (img != null)
                    {
                        img.Dispose();
                    }
                    basicClass.resizeImage(image, out img, resizerate);
                    basicClass.getImageSize(img, out imgrowNumber, out imgcolumnNumber);
                    basicClass.thresholdImage(img, out imgRegionResult, 128, 255);
                    showImage();
                }
            }
            
        }

        private void buttonConfirm_Click(object sender, EventArgs e)
        {
            UserCode.GetInstance().gProCd[currentIndex].gMPP.processID = comboBoxMorphology.SelectedIndex;
            if (radioButtonRectangle.Checked == true)
            {
                UserCode.GetInstance().gProCd[currentIndex].gMPP.elementID = 0;
            }
            else
            {
                UserCode.GetInstance().gProCd[currentIndex].gMPP.elementID = 1;
            }
            UserCode.GetInstance().gProCd[currentIndex].gMPP.width = (int)numericUpDownWidth.Value;
            UserCode.GetInstance().gProCd[currentIndex].gMPP.height = (int)numericUpDownHeight.Value;
            UserCode.GetInstance().gProCd[currentIndex].gMPP.radius = (double)numericUpDownRadius.Value;
            this.Hide();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void radioButtonRectangle_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonRectangle.Checked==true)
            {
                panelRectangle.Enabled = true;
            } 
            else
            {
                panelRectangle.Enabled = false;
            }
            showImage();
        }

        private void radioButtonCircle_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonCircle.Checked == true)
            {
                panelCircle.Enabled = true;
            }
            else
            {
                panelCircle.Enabled = false;
            }
            showImage();
        }

        private void numericUpDownWidth_ValueChanged(object sender, EventArgs e)
        {
            showImage();
        }

        private void numericUpDownHeight_ValueChanged(object sender, EventArgs e)
        {
            showImage();
        }

        private void numericUpDownRadius_ValueChanged(object sender, EventArgs e)
        {
            showImage();
        }

        private void comboBoxMorphology_SelectedIndexChanged(object sender, EventArgs e)
        {
            showImage();
        }

       
    }
}
