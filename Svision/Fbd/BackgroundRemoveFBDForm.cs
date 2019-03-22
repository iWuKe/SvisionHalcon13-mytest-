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
    public partial class BackgroundRemoveFBDForm : Form
    {
        private int currentIndex;
        private bool isColor;
        private HTuple BackgroundRemoveHWHandle;
        HObject image, img, imgRegionResult, imgResult;
        HObject image1, image2, image3, img1RegionResult, img2RegionResult, img3RegionResult, img1Result, img2Result, img3Result;
        double resizerate;
        int rowNumber, columnNumber;
        //int oriRowNumber, oriColumnNumber;
        int oriPictureBoxShowImageWidth, oriPictureBoxShowImageHeight;
        public BackgroundRemoveFBDForm(int idx)
        {
            InitializeComponent();
            currentIndex = idx;
            numericUpDownMaxGray.Value = (decimal)UserCode.GetInstance().gProCd[currentIndex].gBRP.grayValue[0];
            numericUpDownMinGray.Value = (decimal)UserCode.GetInstance().gProCd[currentIndex].gBRP.grayValue[1];
            if (Svision.GetMe().baslerCamera.getChannelNumber() == 1)
            {
                isColor = false;
            }
            else
            {
                isColor = true;//isColor
            }
            //isColor = UserCode.GetInstance().gProCd[currentIndex].boolData[0];
            checkBoxAllColor.Checked = UserCode.GetInstance().gProCd[currentIndex].gBRP.isAllColor;

            numericUpDownRedMaxGray.Value = (decimal)UserCode.GetInstance().gProCd[currentIndex].gBRP.grayValue[2];
            numericUpDownRedMinGray.Value = (decimal)UserCode.GetInstance().gProCd[currentIndex].gBRP.grayValue[3];
            numericUpDownGreenMaxGray.Value = (decimal)UserCode.GetInstance().gProCd[currentIndex].gBRP.grayValue[4];
            numericUpDownGreenMinGray.Value = (decimal)UserCode.GetInstance().gProCd[currentIndex].gBRP.grayValue[5];
            numericUpDownBlueMaxGray.Value = (decimal)UserCode.GetInstance().gProCd[currentIndex].gBRP.grayValue[6];
            numericUpDownBlueMinGray.Value = (decimal)UserCode.GetInstance().gProCd[currentIndex].gBRP.grayValue[7];

        }

        private void BackgroundRemoveFBDForm_Load(object sender, EventArgs e)
        {
            HOperatorSet.OpenWindow(0, 0, (pictureBoxBackgroundRemove.Width), (pictureBoxBackgroundRemove.Height), pictureBoxBackgroundRemove.Handle, "visible", "", out BackgroundRemoveHWHandle);
            HOperatorSet.SetPart(BackgroundRemoveHWHandle, 0, 0, (pictureBoxBackgroundRemove.Height - 1), (pictureBoxBackgroundRemove.Width - 1));
            HDevWindowStack.Push(BackgroundRemoveHWHandle);

           
            oriPictureBoxShowImageWidth = pictureBoxBackgroundRemove.Width;
            oriPictureBoxShowImageHeight = pictureBoxBackgroundRemove.Height;
        }

        private void pictureBoxBackgroundRemove_SizeChanged(object sender, EventArgs e)
        {
            if (BackgroundRemoveHWHandle!=null)
            {
                HOperatorSet.SetWindowExtents(BackgroundRemoveHWHandle, 0, 0, (pictureBoxBackgroundRemove.Width), (pictureBoxBackgroundRemove.Height));
                HOperatorSet.SetPart(BackgroundRemoveHWHandle, 0, 0, (pictureBoxBackgroundRemove.Height - 1), (pictureBoxBackgroundRemove.Width - 1));
                double widRat = pictureBoxBackgroundRemove.Width / ((double)columnNumber);
                double heiRat = pictureBoxBackgroundRemove.Height / ((double)rowNumber);
                resizerate = widRat < heiRat ? widRat : heiRat;
                if (image != null)
                {
                    basicClass.thresholdImage(image, out imgRegionResult, (float)numericUpDownMinGray.Value, (float)numericUpDownMaxGray.Value);
                    if (imgResult != null)
                    {
                        imgResult.Dispose();
                    }
                    HOperatorSet.ReduceDomain(image, imgRegionResult, out imgResult);
                    if (img != null)
                    {
                        img.Dispose();
                    }
                    basicClass.resizeImage(imgResult, out img, resizerate);

                    basicClass.displayhobject(img, BackgroundRemoveHWHandle);
                }
            }
            
        }
        private void showResultImage()
        {
            if (isColor)
            {
                groupBoxColor.Enabled = true;
                HOperatorSet.Decompose3(image, out image1, out image2, out image3);
                basicClass.thresholdImage(image1, out img1RegionResult, (float)numericUpDownRedMinGray.Value, (float)numericUpDownRedMaxGray.Value);
                if (img1Result != null)
                {
                    img1Result.Dispose();
                }
                HOperatorSet.ReduceDomain(image1, img1RegionResult, out img1Result);
                
                basicClass.thresholdImage(image2, out img2RegionResult, (float)numericUpDownGreenMinGray.Value, (float)numericUpDownGreenMaxGray.Value);
                if (img2Result != null)
                {
                    img2Result.Dispose();
                }
                HOperatorSet.ReduceDomain(image2, img2RegionResult, out img2Result);
                
                basicClass.thresholdImage(image3, out img3RegionResult, (float)numericUpDownBlueMinGray.Value, (float)numericUpDownBlueMaxGray.Value);
                if (img3Result != null)
                {
                    img3Result.Dispose();
                }
                HOperatorSet.ReduceDomain(image3, img3RegionResult, out img3Result);
                if (imgResult != null)
                {
                    imgResult.Dispose();
                }
                HOperatorSet.Compose3(img1Result, img2Result, img3Result, out imgResult);
                if (img != null)
                {
                    img.Dispose();
                }
                basicClass.resizeImage(imgResult, out img, resizerate);
            }
            else
            {
                basicClass.thresholdImage(image, out imgRegionResult, (float)numericUpDownMinGray.Value, (float)numericUpDownMaxGray.Value);
                if (imgResult != null)
                {
                    imgResult.Dispose();
                }
                HOperatorSet.ReduceDomain(image, imgRegionResult, out imgResult);
                if (img != null)
                {
                    img.Dispose();
                }
                basicClass.resizeImage(imgResult, out img, resizerate);
                groupBoxGray.Enabled = true;
            }
            basicClass.displayClear(BackgroundRemoveHWHandle);
            basicClass.displayhobject(img, BackgroundRemoveHWHandle);
        }
        private void buttonGetImage_Click(object sender, EventArgs e)
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
                double widRat = pictureBoxBackgroundRemove.Width / ((double)columnNumber);
                double heiRat = pictureBoxBackgroundRemove.Height / ((double)rowNumber);
                resizerate = widRat < heiRat ? widRat : heiRat;
               
                   
                if (oriPictureBoxShowImageWidth != pictureBoxBackgroundRemove.Width)
                {
                    basicClass.displayClear(BackgroundRemoveHWHandle);
                }
                if (oriPictureBoxShowImageHeight != pictureBoxBackgroundRemove.Height)
                {
                    basicClass.displayClear(BackgroundRemoveHWHandle);
                }
                showResultImage();
                
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

        private void buttonConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                UserCode.GetInstance().gProCd[currentIndex].gBRP.grayValue[0] = (float)numericUpDownMaxGray.Value;
                UserCode.GetInstance().gProCd[currentIndex].gBRP.grayValue[1] = (float)numericUpDownMinGray.Value;



                UserCode.GetInstance().gProCd[currentIndex].gBRP.isAllColor = checkBoxAllColor.Checked;

                UserCode.GetInstance().gProCd[currentIndex].gBRP.grayValue[2] = (float)numericUpDownRedMaxGray.Value;
                UserCode.GetInstance().gProCd[currentIndex].gBRP.grayValue[3] = (float)numericUpDownRedMinGray.Value;
                UserCode.GetInstance().gProCd[currentIndex].gBRP.grayValue[4] = (float)numericUpDownGreenMaxGray.Value;
                UserCode.GetInstance().gProCd[currentIndex].gBRP.grayValue[5] = (float)numericUpDownGreenMinGray.Value;
                UserCode.GetInstance().gProCd[currentIndex].gBRP.grayValue[6] = (float)numericUpDownBlueMaxGray.Value;
                UserCode.GetInstance().gProCd[currentIndex].gBRP.grayValue[7] = (float)numericUpDownBlueMinGray.Value;
                this.Hide();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void numericUpDownMaxGray_ValueChanged(object sender, EventArgs e)
        {
            trackBarMaxGray.Value = (int)numericUpDownMaxGray.Value;
            trackBarMinGray.Maximum = (int)numericUpDownMaxGray.Value;
            numericUpDownMinGray.Maximum = (decimal)numericUpDownMaxGray.Value;
            //int rown, columnn;
            if (image != null)
            {
                showResultImage();
            }
        }

        private void trackBarMaxGray_Scroll(object sender, EventArgs e)
        {
            numericUpDownMaxGray.Value = (decimal)this.trackBarMaxGray.Value;
            numericUpDownMinGray.Maximum = (decimal)this.trackBarMaxGray.Value;
            trackBarMinGray.Maximum = (int)this.trackBarMaxGray.Value;
        }

        private void numericUpDownMinGray_ValueChanged(object sender, EventArgs e)
        {
            trackBarMinGray.Value = (int)numericUpDownMinGray.Value;
            if (image != null)
            {
                showResultImage();
            }
        }

        private void trackBarMinGray_Scroll(object sender, EventArgs e)
        {
            numericUpDownMinGray.Value = (decimal)this.trackBarMinGray.Value;
        }

        private void numericUpDownRedMaxGray_ValueChanged(object sender, EventArgs e)
        {
            trackBarRedMaxGray.Value = (int)numericUpDownRedMaxGray.Value;
            trackBarRedMinGray.Maximum = (int)numericUpDownRedMaxGray.Value;
            
            numericUpDownRedMinGray.Maximum = (decimal)numericUpDownRedMaxGray.Value;
            if (checkBoxAllColor.Checked == true)
            {
                numericUpDownBlueMaxGray.Value = numericUpDownRedMaxGray.Value;
                numericUpDownGreenMaxGray.Value = numericUpDownRedMaxGray.Value;
            }
            //int rown, columnn;
            if (image != null)
            {
                showResultImage();
            }
        }



        private void numericUpDownRedMinGray_ValueChanged(object sender, EventArgs e)
        {
            trackBarRedMinGray.Value = (int)numericUpDownRedMinGray.Value;
            if (checkBoxAllColor.Checked == true)
            {
                numericUpDownGreenMinGray.Value = numericUpDownRedMinGray.Value;
                numericUpDownBlueMinGray.Value = numericUpDownRedMinGray.Value;
            }
            if (image != null)
            {
                showResultImage();
            }
        }

        private void trackBarRedMinGray_Scroll(object sender, EventArgs e)
        {
            numericUpDownRedMinGray.Value = (decimal)this.trackBarRedMinGray.Value;

        }

        private void numericUpDownGreenMaxGray_ValueChanged(object sender, EventArgs e)
        {
            trackBarGreenMaxGray.Value = (int)numericUpDownGreenMaxGray.Value;
            trackBarGreenMinGray.Maximum = (int)numericUpDownGreenMaxGray.Value;
            numericUpDownGreenMinGray.Maximum = (decimal)numericUpDownGreenMaxGray.Value;
            if (checkBoxAllColor.Checked == true)
            {
                numericUpDownRedMaxGray.Value = numericUpDownGreenMaxGray.Value;
                numericUpDownBlueMaxGray.Value = numericUpDownGreenMaxGray.Value;
            }
            //int rown, columnn;
            if (image != null)
            {
                showResultImage();
            }
        }

        private void trackBarGreenMaxGray_Scroll(object sender, EventArgs e)
        {
            numericUpDownGreenMaxGray.Value = (decimal)this.trackBarGreenMaxGray.Value;
            numericUpDownGreenMinGray.Maximum = (decimal)this.trackBarGreenMaxGray.Value;
            trackBarGreenMinGray.Maximum = (int)this.trackBarGreenMaxGray.Value;
        }

        private void numericUpDownGreenMinGray_ValueChanged(object sender, EventArgs e)
        {
            trackBarGreenMinGray.Value = (int)numericUpDownGreenMinGray.Value;
            if (checkBoxAllColor.Checked == true)
            {
                numericUpDownRedMinGray.Value = numericUpDownGreenMinGray.Value;
                numericUpDownBlueMinGray.Value = numericUpDownGreenMinGray.Value;
            }
            if (image != null)
            {
                showResultImage();
            }
        }

        private void trackBarGreenMinGray_Scroll(object sender, EventArgs e)
        {
            numericUpDownGreenMinGray.Value = (decimal)this.trackBarGreenMinGray.Value;
        }

        private void numericUpDownBlueMaxGray_ValueChanged(object sender, EventArgs e)
        {
            trackBarBlueMaxGray.Value = (int)numericUpDownBlueMaxGray.Value;
            trackBarBlueMinGray.Maximum = (int)numericUpDownBlueMaxGray.Value;
            numericUpDownBlueMinGray.Maximum = (decimal)numericUpDownBlueMaxGray.Value;
            if (checkBoxAllColor.Checked == true)
            {
                numericUpDownRedMaxGray.Value = numericUpDownBlueMaxGray.Value;
                numericUpDownGreenMaxGray.Value = numericUpDownBlueMaxGray.Value;
            }
            if (image != null)
            {
                showResultImage();
            }
        }

        private void trackBarBlueMaxGray_Scroll(object sender, EventArgs e)
        {
            numericUpDownBlueMaxGray.Value = (decimal)this.trackBarBlueMaxGray.Value;
            numericUpDownBlueMinGray.Maximum = (decimal)this.trackBarBlueMaxGray.Value;
            trackBarBlueMinGray.Maximum = (int)this.trackBarBlueMaxGray.Value;
        }

        private void numericUpDownBlueMinGray_ValueChanged(object sender, EventArgs e)
        {
            trackBarBlueMinGray.Value = (int)numericUpDownBlueMinGray.Value;
            if (checkBoxAllColor.Checked == true)
            {
                numericUpDownRedMinGray.Value = numericUpDownBlueMinGray.Value;
                numericUpDownGreenMinGray.Value = numericUpDownBlueMinGray.Value;
            }
            if (image != null)
            {
                showResultImage();
            }
        }

        private void trackBarBlueMinGray_Scroll(object sender, EventArgs e)
        {
            numericUpDownBlueMinGray.Value = (decimal)this.trackBarBlueMinGray.Value;
        }

        private void checkBoxAllColor_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxAllColor.Checked==true&&isColor==true)
            {
                numericUpDownRedMaxGray.Value = (decimal)255;
                numericUpDownRedMinGray.Value = (decimal)0;
                numericUpDownGreenMaxGray.Value = (decimal)255;
                numericUpDownGreenMinGray.Value = (decimal)0;
                numericUpDownBlueMaxGray.Value = (decimal)255;
                numericUpDownBlueMinGray.Value = (decimal)0;
            } 
            else
            {
            }
        }

        private void trackBarRedMaxGray_Scroll(object sender, EventArgs e)
        {
            numericUpDownRedMaxGray.Value = (decimal)this.trackBarRedMaxGray.Value;
            numericUpDownRedMinGray.Maximum = (decimal)this.trackBarRedMaxGray.Value;
            trackBarRedMinGray.Maximum = (int)this.trackBarRedMaxGray.Value;
        }
    }
}
