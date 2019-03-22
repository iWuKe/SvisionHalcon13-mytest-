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
    public partial class CameraParaSet : Form
    {
        public HObject image, img, imgPart, rectDomain;
        private double resRat = 1;
        private double resRatPart = 1;
        private HTuple cameraParaHWHandle = null;
        private HTuple cameraParaPartHWHandle = null;
        private double rectLocationX = 0;
        private double rectLocationY = 0;

        private int prvRow = 0;              //刚进入相机参数设置时，相机行数
        private int prvColumn = 0;              //刚进入相机参数设置时，相机列数
        private int prvXoffset = 0;              //刚进入相机参数设置时，相机X偏移
        private int prvYoffset = 0;              //刚进入相机参数设置时，相机Y偏移
        public int prvChannelNum;
        private double prvGainPercent = 0;//增益百分数;
        private double prvGammaPercent = 0;//Gamma百分数;
        private double prvExposurePercent = 0;//曝光百分数;
        private double prvWhiteBalanceBluePercent = 0;//白平衡百分数（蓝）;
        private double prvWhiteBalanceGreenPercent = 0;//白平衡百分数（绿）;
        private double prvWhiteBalanceRedPercent = 0;//白平衡百分数（红）;
        public bool prvIsExposureAuto = false;
        public bool prvIsGainAuto = false;
        public bool prvIsWhiteBalanceAuto = false;



        int[] XOffsetRange, YOffsetRange;
        int channelMax;
        Point oriLocation, preLocation;
        public CameraParaSet()
        {
            InitializeComponent();
        }
        ~CameraParaSet()
        {
            cameraParaTimer.Enabled = false;
            Svision.GetMe().oriColumnNumber = (int)numericUpDownWidth.Value;
            Svision.GetMe().oriRowNumber = (int)numericUpDownheight.Value;


        }

        private void CameraParaSet_Load(object sender, EventArgs e)
        {
            try
            {
                HOperatorSet.OpenWindow(0, 0, 256, 192, cameraParapictureBox.Handle, "visible", "", out cameraParaHWHandle);
                HOperatorSet.SetPart(cameraParaHWHandle, 0, 0, 191, 255);
                HDevWindowStack.Push(cameraParaHWHandle);
                HOperatorSet.SetLineWidth(cameraParaHWHandle, 1);
                HOperatorSet.SetColor(cameraParaHWHandle, "green");
                HOperatorSet.OpenWindow(0, 0, cameraParaPartpictureBox.Width, cameraParaPartpictureBox.Height, cameraParaPartpictureBox.Handle, "visible", "", out cameraParaPartHWHandle);
                HOperatorSet.SetPart(cameraParaPartHWHandle, 0, 0, cameraParaPartpictureBox.Height - 1, cameraParaPartpictureBox.Width - 1);
                HDevWindowStack.Push(cameraParaPartHWHandle);
                
                //Load Camera Config from configInformation and set these to the camera
                //row
                //  numericUpDownheight.Value = trackBarHeight.Value = Svision.GetMe().baslerCamera.getRowNumber();

                int[] rowRange;
                int rowIncrement;
                Svision.GetMe().baslerCamera.getCameraRowRange(out rowRange);
                Svision.GetMe().baslerCamera.getRowIncrement(out rowIncrement);
                numericUpDownheight.Maximum = rowRange[1];
                numericUpDownheight.Minimum = rowRange[0];
                numericUpDownheight.Increment = rowIncrement;
                numericUpDownheight.InterceptArrowKeys = true;
                trackBarHeight.Maximum = rowRange[1];
                trackBarHeight.Minimum = rowRange[0];
                trackBarHeight.SmallChange = rowIncrement;
                trackBarHeight.LargeChange = rowIncrement * 10;
                numericUpDownheight.Value = ConfigInformation.GetInstance().tCamCfg.rows;
                prvRow = trackBarHeight.Value;
                //column
                //       numericUpDownWidth.Value = trackBarWidth.Value = Svision.GetMe().baslerCamera.getColumnNumber();

                int[] columnRange;
                int columnIncrement;
                Svision.GetMe().baslerCamera.getCameraColumnRange(out columnRange);
                Svision.GetMe().baslerCamera.getColumnIncrement(out columnIncrement);
                numericUpDownWidth.Maximum = columnRange[1];
                numericUpDownWidth.Minimum = columnRange[0];
                numericUpDownWidth.Increment = columnIncrement;
                numericUpDownWidth.InterceptArrowKeys = true;
                trackBarWidth.Maximum = columnRange[1];
                trackBarWidth.Minimum = columnRange[0];
                trackBarWidth.SmallChange = columnIncrement;
                trackBarWidth.LargeChange = columnIncrement * 10;
                numericUpDownWidth.Value = ConfigInformation.GetInstance().tCamCfg.columns;
                prvColumn = trackBarWidth.Value;
                //Offset

                int XOffsetIncrement, YOffsetIncrement;
                Svision.GetMe().baslerCamera.getCameraOffsetRange(out XOffsetRange, out YOffsetRange);
                Svision.GetMe().baslerCamera.getOffsetIncrement(out XOffsetIncrement, out YOffsetIncrement);
                numericUpDownXOffset.Maximum = XOffsetRange[1];
                numericUpDownXOffset.Minimum = XOffsetRange[0];
                numericUpDownXOffset.Increment = XOffsetIncrement;
                numericUpDownXOffset.InterceptArrowKeys = true;
                numericUpDownYOffset.Maximum = YOffsetRange[1];
                numericUpDownYOffset.Minimum = YOffsetRange[0];
                numericUpDownYOffset.Increment = YOffsetIncrement;
                numericUpDownYOffset.InterceptArrowKeys = true;
                trackBarXOffset.Maximum = XOffsetRange[1];
                trackBarXOffset.Minimum = XOffsetRange[0];
                trackBarXOffset.SmallChange = XOffsetIncrement;
                trackBarXOffset.LargeChange = XOffsetIncrement * 10;
                trackBarYOffset.Maximum = YOffsetRange[1];
                trackBarYOffset.Minimum = YOffsetRange[0];
                trackBarYOffset.SmallChange = YOffsetIncrement;
                trackBarYOffset.LargeChange = YOffsetIncrement * 10;
                numericUpDownXOffset.Value = ConfigInformation.GetInstance().tCamCfg.xOffset;
                numericUpDownYOffset.Value = ConfigInformation.GetInstance().tCamCfg.yOffset;
                //        numericUpDownXOffset.Value = trackBarXOffset.Value = Svision.GetMe().baslerCamera.getXOffsetNumber();
                prvXoffset = trackBarXOffset.Value;
                //     numericUpDownYOffset.Value = trackBarYOffset.Value = Svision.GetMe().baslerCamera.getYOffsetNumber();
                prvYoffset = trackBarYOffset.Value;
                //channel

                Svision.GetMe().baslerCamera.getCameraMaxChannel(out channelMax);
                if (ConfigInformation.GetInstance().tCamCfg.channelNum == 1)
                {
                    Svision.GetMe().baslerCamera.setChannelNumber(1);
                    radioButtonGray.Checked = true;
                    prvChannelNum = 1;
                }
                else if (ConfigInformation.GetInstance().tCamCfg.channelNum == 3 && channelMax == 3)
                {
                    Svision.GetMe().baslerCamera.setChannelNumber(3);
                    radioButtonColour.Checked = true;
                    prvChannelNum = 3;
                }
                else
                {
                    Svision.GetMe().baslerCamera.setChannelNumber(1);
                    radioButtonGray.Checked = true;
                    prvChannelNum = 1;
                }

                if (channelMax == 1)
                {
                    radioButtonColour.Enabled = false;
                }
                //gamma
                numericUpDownGamma.Value = (decimal)(ConfigInformation.GetInstance().tCamCfg.gammaPercent * 100);
                prvGammaPercent = ConfigInformation.GetInstance().tCamCfg.gammaPercent;

                //gain
                if (ConfigInformation.GetInstance().tCamCfg.isGainAuto)
                {
                    prvIsGainAuto = true;
                    checkBoxGainAuto.CheckState = CheckState.Checked;
                    groupBoxGain.Enabled = false;
                }
                else
                {
                    prvIsGainAuto = false;
                    Svision.GetMe().baslerCamera.setGainAuto(false);
                    checkBoxGainAuto.CheckState = CheckState.Unchecked;
                    numericUpDownGain.Value = (decimal)(ConfigInformation.GetInstance().tCamCfg.gainPercent * 100);
                    prvGainPercent = ConfigInformation.GetInstance().tCamCfg.gainPercent;
                    groupBoxGain.Enabled = true;
                }

                //exposure
                if (ConfigInformation.GetInstance().tCamCfg.isExposureAuto)
                {
                    checkBoxExposureAuto.CheckState = CheckState.Checked;
                    groupBoxExposure.Enabled = false;
                    prvIsExposureAuto = true;
                }
                else
                {
                    prvIsExposureAuto = false;
                    Svision.GetMe().baslerCamera.setExposureAuto(false);
                    checkBoxExposureAuto.CheckState = CheckState.Unchecked;
                    numericUpDownExposure.Value = (decimal)(ConfigInformation.GetInstance().tCamCfg.exposurePercent * 100);
                    prvExposurePercent = ConfigInformation.GetInstance().tCamCfg.exposurePercent;
                    groupBoxExposure.Enabled = true;
                }

                //white balance
                if (ConfigInformation.GetInstance().tCamCfg.isWhiteBalanceAuto)
                {
                    checkBoxWhiteBalanceAuto.CheckState = CheckState.Checked;
                    groupBoxWhiteBalance.Enabled = false;
                    prvIsWhiteBalanceAuto = true;
                }
                else
                {
                    prvIsWhiteBalanceAuto = false;
                    Svision.GetMe().baslerCamera.setWhiteBalanceAuto(false);
                    checkBoxWhiteBalanceAuto.CheckState = CheckState.Unchecked;
                    if (Svision.GetMe().baslerCamera.getChannelNumber() == 3)
                    {
                        groupBoxWhiteBalance.Enabled = true;
                        prvWhiteBalanceBluePercent = ConfigInformation.GetInstance().tCamCfg.whiteBalanceBluePercent;
                        prvWhiteBalanceGreenPercent = ConfigInformation.GetInstance().tCamCfg.whiteBalanceGreenPercent;
                        prvWhiteBalanceRedPercent = ConfigInformation.GetInstance().tCamCfg.whiteBalanceRedPercent;

                        //prvWhiteBalanceBluePercent = ConfigInformation.GetInstance().tCamCfg.whiteBalanceBluePercent;
                        //prvWhiteBalanceGreenPercent = ConfigInformation.GetInstance().tCamCfg.whiteBalanceGreenPercent;
                        //prvWhiteBalanceRedPercent = ConfigInformation.GetInstance().tCamCfg.whiteBalanceRedPercent;


                        numericUpDownBlueBalance.Value = (decimal)(Svision.GetMe().baslerCamera.getWhiteBalanceBluePercent() * 100);
                        numericUpDownRedBalance.Value = (decimal)(Svision.GetMe().baslerCamera.getWhiteBalanceRedPercent() * 100);
                        numericUpDownGreenBalance.Value = (decimal)(Svision.GetMe().baslerCamera.getWhiteBalanceGreenPercent() * 100);
                        
                    }
                    else
                    {
                        groupBoxWhiteBalance.Enabled = false;
                        //numericUpDownBlueBalance.Value = (int)(Svision.GetMe().baslerCamera.getWhiteBalanceBluePercent() * 100);
                        //numericUpDownRedBalance.Value = (int)(Svision.GetMe().baslerCamera.getWhiteBalanceRedPercent() * 100);
                        //numericUpDownGreenBalance.Value = (int)(Svision.GetMe().baslerCamera.getWhiteBalanceGreenPercent() * 100);
                        //prvWhiteBalanceBluePercent = Svision.GetMe().baslerCamera.getWhiteBalanceBluePercent();
                        //prvWhiteBalanceGreenPercent = Svision.GetMe().baslerCamera.getWhiteBalanceGreenPercent();
                        //prvWhiteBalanceRedPercent = Svision.GetMe().baslerCamera.getWhiteBalanceRedPercent();
                    }

                }


                panelDetail.Enabled = false;
                //camera information
                string modelNameStr, deviceVendorNameStr, deviceModelNameStr, deviceFirmwareVersionStr;
                Svision.GetMe().baslerCamera.getCameraInformation(out modelNameStr, out deviceVendorNameStr, out deviceModelNameStr, out deviceFirmwareVersionStr);
                textBoxCameraInformation.Text = "相机型号: " + modelNameStr + "\r\n" + "\r\n" + "供应商: " + deviceVendorNameStr + "\r\n" + "\r\n" + "设备型号: " + deviceModelNameStr + "\r\n" + "\r\n" + "设备固件版本:" + deviceFirmwareVersionStr + "\r\n";
                cameraParaTimer.Enabled = true;
            }
            catch (System.Exception ex)
            {
                cameraParaTimer.Enabled = false;
                System.Threading.Thread.Sleep(100);
                if (Svision.GetMe().baslerCamera != null && Svision.GetMe().baslerCamera.camera.IsOpen && Svision.GetMe().baslerCamera.camera.IsConnected)
                {
                    
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
                    cameraParaTimer.Enabled = true;

                }
                
            }


        }

        private void cameraParaTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                if (image != null)
                {
                    image.Dispose();
                }
                int rowNum, columnNum;
                numericUpDownExposure.Value = (decimal)(Svision.GetMe().baslerCamera.getExposurePercent() * 100);
                numericUpDownGain.Value = (decimal)(Svision.GetMe().baslerCamera.getGainPercent() * 100);
                Svision.GetMe().baslerCamera.getFrameImage(out image);
                if (ConfigInformation.GetInstance().tCalCfg.calibrationIsRadialDistortionFlag)
                {
                    HOperatorSet.MapImage(image, ConfigInformation.GetInstance().tCalCfg.ho_MapFixed, out image);
                }
                //HOperatorSet.WriteImage(image, "bmp", 0, "H:/vision_dll/trunk/C#_CODE/UI/Svision/a.bmp");
                basicClass.getImageSize(image, out rowNum, out columnNum);
                double widRat = 256 / ((double)columnNum);
                double heiRat = 192 / ((double)rowNum);
                double resRatNew = widRat < heiRat ? widRat : heiRat;
                if (resRat != resRatNew)
                {
                    basicClass.displayClear(cameraParaHWHandle);
                    resRat = resRatNew;
                }
                rectLocationX = preLocation.X;
                rectLocationY = preLocation.Y;
                if (preLocation.X <= 0)
                {
                    rectLocationX = 0;
                }
                if (preLocation.Y <= 0)
                {
                    rectLocationY = 0;
                }
                if (preLocation.X + cameraParaPartpictureBox.Width * resRat > Math.Min((columnNum - 1) * resRat - 1, 255))
                {
                    rectLocationX = Math.Min((columnNum - cameraParaPartpictureBox.Width - 1) * resRat - 1, 255 - cameraParaPartpictureBox.Width * resRat);
                }
                if (preLocation.Y + cameraParaPartpictureBox.Height * resRat > Math.Min((rowNum - 1) * resRat - 1, 191))
                {
                    rectLocationY = Math.Min((rowNum - cameraParaPartpictureBox.Height - 1) * resRat - 1, 191 - cameraParaPartpictureBox.Height * resRat);
                }
                double widRatPart = cameraParaPartpictureBox.Width / ((double)columnNum);
                double heiRatPart = cameraParaPartpictureBox.Height / ((double)rowNum);
                double resRatPartNew = widRatPart < heiRatPart ? widRatPart : heiRatPart;
                if (resRatPart != resRatPartNew)
                {
                    basicClass.displayClear(cameraParaPartHWHandle);
                    resRatPart = resRatPartNew;
                }
                if (img!=null)
                {
                    img.Dispose();
                }
                basicClass.resizeImage(image, out img, resRat);
                HOperatorSet.DispObj(img, cameraParaHWHandle);
                if (resRatPart > 1)
                {
                    if (imgPart != null)
                    {
                        imgPart.Dispose();
                    }
                    HOperatorSet.CopyImage(image, out imgPart);
                    HOperatorSet.DispObj(imgPart, cameraParaPartHWHandle);
                }
                else
                {
                    if (rowNum * resRat > 6 && columnNum * resRat > 8)
                    {
                        basicClass.displayRectangle1XldScreen(cameraParaHWHandle, Math.Max(rectLocationY, 1), Math.Max(rectLocationX, 1), rectLocationY + cameraParaPartpictureBox.Height * resRat - 2, rectLocationX + cameraParaPartpictureBox.Width * resRat - 2);
                        basicClass.genRectangle1(out rectDomain, rectLocationY / resRat, rectLocationX / resRat, Math.Min(rectLocationY / resRat + cameraParaPartpictureBox.Height - 1, rowNum - 1), Math.Min(rectLocationX / resRat + cameraParaPartpictureBox.Width - 1, columnNum - 1));
                        if (imgPart != null)
                        {
                            imgPart.Dispose();
                        }
                        basicClass.reduceDomain(image, rectDomain, out imgPart);
                        HOperatorSet.CropDomain(imgPart, out imgPart);
                        HOperatorSet.DispObj(imgPart, cameraParaPartHWHandle);
                    }

                }
            }
            catch (System.Exception ex)
            {
                cameraParaTimer.Enabled = false;
                System.Threading.Thread.Sleep(100);
                if (Svision.GetMe().baslerCamera != null && Svision.GetMe().baslerCamera.camera.IsOpen && Svision.GetMe().baslerCamera.camera.IsConnected)
                {
                    
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
                    cameraParaTimer.Enabled = true;

                }
                
            }


        }

        private void NumericValueChangeHeight(object sender, EventArgs e)
        {
            try
            {
                numericUpDownYOffset.Value = trackBarYOffset.Value = 0;
                basicClass.displayClear(cameraParaHWHandle);
                basicClass.displayClear(cameraParaPartHWHandle);
                Svision.GetMe().baslerCamera.setRowNumber((int)numericUpDownheight.Value);
                trackBarHeight.Value = (int)numericUpDownheight.Value;

                Svision.GetMe().baslerCamera.getCameraOffsetRange(out XOffsetRange, out YOffsetRange);
                numericUpDownYOffset.Maximum = YOffsetRange[1];
                trackBarYOffset.Maximum = YOffsetRange[1];
                Svision.GetMe().oriRowNumber = (int)numericUpDownheight.Value;

            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void trackBarChangedHeight(object sender, EventArgs e)
        {
            try
            {
                int rowIncrement;
                Svision.GetMe().baslerCamera.getRowIncrement(out rowIncrement);
                if (trackBarHeight.Value % rowIncrement != 0)
                {
                    trackBarHeight.Value = trackBarHeight.Value / rowIncrement * rowIncrement;
                }
                numericUpDownheight.Value = trackBarHeight.Value;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void numericValueChangeWidth(object sender, EventArgs e)
        {
            try
            {
                numericUpDownXOffset.Value = trackBarXOffset.Value = 0;
                basicClass.displayClear(cameraParaHWHandle);
                basicClass.displayClear(cameraParaPartHWHandle);
                Svision.GetMe().baslerCamera.setColumnNumber((int)numericUpDownWidth.Value);
                trackBarWidth.Value = (int)numericUpDownWidth.Value;

                Svision.GetMe().baslerCamera.getCameraOffsetRange(out XOffsetRange, out YOffsetRange);
                numericUpDownXOffset.Maximum = XOffsetRange[1];
                trackBarXOffset.Maximum = XOffsetRange[1];
                Svision.GetMe().oriColumnNumber = (int)numericUpDownWidth.Value;

            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }

        private void trackBarChangeWidth(object sender, EventArgs e)
        {
            try
            {
                int columnIncrement;
                Svision.GetMe().baslerCamera.getColumnIncrement(out columnIncrement);
                if (trackBarWidth.Value % columnIncrement != 0)
                {
                    trackBarWidth.Value = trackBarWidth.Value / columnIncrement * columnIncrement;
                }
                numericUpDownWidth.Value = trackBarWidth.Value;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void pictureMouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                Cursor = Cursors.SizeAll;
                oriLocation = e.Location;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void pictureMouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Left)
                {
                    preLocation.X = e.Location.X;
                    preLocation.Y = e.Location.Y;
                }
                else
                {
                    Cursor = Cursors.Default;
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void numericValueChangeXOffset(object sender, EventArgs e)
        {
            try
            {
                Svision.GetMe().baslerCamera.setOffsetX((int)numericUpDownXOffset.Value);
                trackBarXOffset.Value = (int)numericUpDownXOffset.Value;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void numericValueChangeYOffset(object sender, EventArgs e)
        {
            try
            {
                Svision.GetMe().baslerCamera.setOffsetY((int)numericUpDownYOffset.Value);
                trackBarYOffset.Value = (int)numericUpDownYOffset.Value;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void trackBarChangeXOffset(object sender, EventArgs e)
        {
            try
            {
                int xOffsetIncrement, yOffsetIncrement;
                Svision.GetMe().baslerCamera.getOffsetIncrement(out xOffsetIncrement, out yOffsetIncrement);
                if (trackBarXOffset.Value % xOffsetIncrement != 0)
                {
                    trackBarXOffset.Value = trackBarXOffset.Value / xOffsetIncrement * xOffsetIncrement;
                }
                numericUpDownXOffset.Value = trackBarXOffset.Value;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void trackBarChangeYOffset(object sender, EventArgs e)
        {
            try
            {
                int xOffsetIncrement, yOffsetIncrement;
                Svision.GetMe().baslerCamera.getOffsetIncrement(out xOffsetIncrement, out yOffsetIncrement);
                if (trackBarYOffset.Value % yOffsetIncrement != 0)
                {
                    trackBarYOffset.Value = trackBarYOffset.Value / yOffsetIncrement * yOffsetIncrement;
                }
                numericUpDownYOffset.Value = trackBarYOffset.Value;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void radioChangeGray(object sender, EventArgs e)
        {
            try
            {
                if (radioButtonGray.Checked == true)
                {
                    Svision.GetMe().baslerCamera.setChannelNumber(1);
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void radioChangeColour(object sender, EventArgs e)
        {
            try
            {
                if (channelMax == 3)
                {
                    if (radioButtonColour.Checked == true)
                    {
                        Svision.GetMe().baslerCamera.setChannelNumber(3);
                        if (checkBoxWhiteBalanceAuto.Checked)
                        {
                            Svision.GetMe().baslerCamera.setWhiteBalanceAuto(true);
                            groupBoxWhiteBalance.Enabled = false;
                        }
                        else
                        {
                            Svision.GetMe().baslerCamera.setWhiteBalanceAuto(false);
                            numericUpDownBlueBalance.Value = (decimal)(Svision.GetMe().baslerCamera.getWhiteBalanceBluePercent() * 100);
                            numericUpDownRedBalance.Value = (decimal)(Svision.GetMe().baslerCamera.getWhiteBalanceRedPercent() * 100);
                            numericUpDownGreenBalance.Value = (decimal)(Svision.GetMe().baslerCamera.getWhiteBalanceGreenPercent() * 100);
                        }
                    }
                }
                else
                {
                    radioButtonColour.Checked = false;
                }
                
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

       
        private void checkChangeGainAuto(object sender, EventArgs e)
        {
            try
            {
                if (checkBoxGainAuto.Checked == true)
                {
                    Svision.GetMe().baslerCamera.setGainAuto(true);
                    numericUpDownGain.Value = (decimal)(Svision.GetMe().baslerCamera.getGainPercent() * 100);
                    groupBoxGain.Enabled = false;
                }
                else
                {
                    Svision.GetMe().baslerCamera.setGainAuto(false);
                    numericUpDownGain.Value = (decimal)(Svision.GetMe().baslerCamera.getGainPercent() * 100);
                    groupBoxGain.Enabled = true;
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void checkBoxChangeExposureAuto(object sender, EventArgs e)
        {
            try
            {
                if (checkBoxExposureAuto.Checked == true)
                {
                    Svision.GetMe().baslerCamera.setExposureAuto(true);
                    numericUpDownExposure.Value = (decimal)(Svision.GetMe().baslerCamera.getExposurePercent() * 100);
                    groupBoxExposure.Enabled = false;
                }
                else
                {
                    Svision.GetMe().baslerCamera.setExposureAuto(false);
                    numericUpDownExposure.Value = (decimal)(Svision.GetMe().baslerCamera.getExposurePercent() * 100);
                    groupBoxExposure.Enabled = true;
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
      


        private void checkBoxChangeWhiteBalanceAuto(object sender, EventArgs e)
        {
            try
            {
                if (checkBoxWhiteBalanceAuto.Checked == true)
                {
                    if (Svision.GetMe().baslerCamera.getChannelNumber() == 3)
                    {
                        Svision.GetMe().baslerCamera.setWhiteBalanceAuto(true);
                        numericUpDownBlueBalance.Value = (decimal)(Svision.GetMe().baslerCamera.getWhiteBalanceBluePercent() * 100);
                        numericUpDownRedBalance.Value = (decimal)(Svision.GetMe().baslerCamera.getWhiteBalanceRedPercent() * 100);
                        numericUpDownGreenBalance.Value = (decimal)(Svision.GetMe().baslerCamera.getWhiteBalanceGreenPercent() * 100);
                        groupBoxWhiteBalance.Enabled = false;
                    }
                    else
                    {
                        Svision.GetMe().baslerCamera.setWhiteBalanceAuto(true);
                        groupBoxWhiteBalance.Enabled = false;
                    }

                }
                else
                {
                    Svision.GetMe().baslerCamera.setWhiteBalanceAuto(false);
                    if (Svision.GetMe().baslerCamera.getChannelNumber() == 3)
                    {
                        numericUpDownBlueBalance.Value = (decimal)(Svision.GetMe().baslerCamera.getWhiteBalanceBluePercent() * 100);
                        numericUpDownRedBalance.Value = (int)(Svision.GetMe().baslerCamera.getWhiteBalanceRedPercent() * 100);
                        numericUpDownGreenBalance.Value = (int)(Svision.GetMe().baslerCamera.getWhiteBalanceGreenPercent() * 100);
                        groupBoxWhiteBalance.Enabled = true;
                    }
                    else
                    {
                        groupBoxWhiteBalance.Enabled = false;
                    }

                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void checkBoxChangeDetail(object sender, EventArgs e)
        {
            try
            {
                if (checkBoxDetail.Checked == true)
                {
                    panelDetail.Enabled = true;
                }
                else
                {
                    panelDetail.Enabled = false;
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void buttonCamParaSetConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                cameraParaTimer.Enabled = false;

                ConfigInformation.GetInstance().tCamCfg.rows = (int)numericUpDownheight.Value;
                ConfigInformation.GetInstance().tCamCfg.columns = (int)numericUpDownWidth.Value;
                ConfigInformation.GetInstance().tCamCfg.xOffset = (int)numericUpDownXOffset.Value;
                ConfigInformation.GetInstance().tCamCfg.yOffset = (int)numericUpDownYOffset.Value;
                ConfigInformation.GetInstance().tCamCfg.channelNum = Svision.GetMe().baslerCamera.getChannelNumber();
                ConfigInformation.GetInstance().tCamCfg.gammaPercent = ((double)numericUpDownGamma.Value) / 100;
                if (checkBoxGainAuto.Checked)
                {
                    ConfigInformation.GetInstance().tCamCfg.isGainAuto = true;
                    ConfigInformation.GetInstance().tCamCfg.gainPercent = Svision.GetMe().baslerCamera.getGainPercent();
                }
                else
                {
                    ConfigInformation.GetInstance().tCamCfg.isGainAuto = false;
                    ConfigInformation.GetInstance().tCamCfg.gainPercent = Svision.GetMe().baslerCamera.getGainPercent();
                }
                if (checkBoxExposureAuto.Checked)
                {
                    ConfigInformation.GetInstance().tCamCfg.isExposureAuto = true;
                    ConfigInformation.GetInstance().tCamCfg.exposurePercent = Svision.GetMe().baslerCamera.getExposurePercent();
                }
                else
                {
                    ConfigInformation.GetInstance().tCamCfg.isExposureAuto = false;
                    ConfigInformation.GetInstance().tCamCfg.exposurePercent = Svision.GetMe().baslerCamera.getExposurePercent();
                }
                if (checkBoxWhiteBalanceAuto.Checked)
                {
                    ConfigInformation.GetInstance().tCamCfg.isWhiteBalanceAuto = true;
                    if (Svision.GetMe().baslerCamera.getChannelNumber() == 3)
                    {
                        ConfigInformation.GetInstance().tCamCfg.whiteBalanceBluePercent = ((double)numericUpDownBlueBalance.Value) / 100;
                        ConfigInformation.GetInstance().tCamCfg.whiteBalanceGreenPercent = ((double)numericUpDownGreenBalance.Value) / 100;
                        ConfigInformation.GetInstance().tCamCfg.whiteBalanceRedPercent = ((double)numericUpDownRedBalance.Value) / 100;
                    }
                }
                else
                {
                    ConfigInformation.GetInstance().tCamCfg.isWhiteBalanceAuto = false;
                    if (Svision.GetMe().baslerCamera.getChannelNumber() == 3)
                    {
                        ConfigInformation.GetInstance().tCamCfg.whiteBalanceBluePercent = ((double)numericUpDownBlueBalance.Value) / 100;
                        ConfigInformation.GetInstance().tCamCfg.whiteBalanceGreenPercent = ((double)numericUpDownGreenBalance.Value) / 100;
                        ConfigInformation.GetInstance().tCamCfg.whiteBalanceRedPercent = ((double)numericUpDownRedBalance.Value) / 100;
                    }
                }
                this.Close();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            //Svision.GetMe().Enabled = tru;
            
        }

        private void buttonCamParaSetCancel_Click(object sender, EventArgs e)
        {
            try
            {
                cameraParaTimer.Enabled = false;
                numericUpDownheight.Value = prvRow;
                numericUpDownWidth.Value = prvColumn;
                numericUpDownXOffset.Value = prvXoffset;
                numericUpDownYOffset.Value = prvYoffset;
                ConfigInformation.GetInstance().tCamCfg.rows = (int)numericUpDownheight.Value;
                ConfigInformation.GetInstance().tCamCfg.columns = (int)numericUpDownWidth.Value;
                ConfigInformation.GetInstance().tCamCfg.xOffset = (int)numericUpDownXOffset.Value;
                ConfigInformation.GetInstance().tCamCfg.yOffset = (int)numericUpDownYOffset.Value;
                //channel


                if (prvChannelNum == 1)
                {
                    Svision.GetMe().baslerCamera.setChannelNumber(1);

                    ConfigInformation.GetInstance().tCamCfg.channelNum = 1;
                }
                else if (prvChannelNum == 3 && channelMax == 3)
                {
                    Svision.GetMe().baslerCamera.setChannelNumber(3);
                    ConfigInformation.GetInstance().tCamCfg.channelNum = 3;
                }
                else
                {
                    Svision.GetMe().baslerCamera.setChannelNumber(1);
                    ConfigInformation.GetInstance().tCamCfg.channelNum = 1;
                }

                //gamma
                numericUpDownGamma.Value = (decimal)(prvGammaPercent * 100);
                ConfigInformation.GetInstance().tCamCfg.gammaPercent = prvGammaPercent;

                //gain
                if (prvIsGainAuto)
                {
                    ConfigInformation.GetInstance().tCamCfg.isGainAuto = true;
                    checkBoxGainAuto.CheckState = CheckState.Checked;
                }
                else
                {
                    ConfigInformation.GetInstance().tCamCfg.isGainAuto = false;
                    checkBoxGainAuto.CheckState = CheckState.Unchecked;
                    numericUpDownGain.Value = (decimal)(prvGainPercent * 100);
                    ConfigInformation.GetInstance().tCamCfg.gainPercent = prvGainPercent;
                }

                //exposure
                if (prvIsExposureAuto)
                {
                    checkBoxExposureAuto.CheckState = CheckState.Checked;
                    ConfigInformation.GetInstance().tCamCfg.isExposureAuto = true;
                }
                else
                {
                    ConfigInformation.GetInstance().tCamCfg.isExposureAuto = false;
                    checkBoxExposureAuto.CheckState = CheckState.Unchecked;
                    numericUpDownExposure.Value = (decimal)(prvExposurePercent * 100);
                    ConfigInformation.GetInstance().tCamCfg.exposurePercent = prvExposurePercent;
                }

                //white balance
                if (prvIsWhiteBalanceAuto)
                {
                    checkBoxWhiteBalanceAuto.CheckState = CheckState.Checked;
                    ConfigInformation.GetInstance().tCamCfg.isWhiteBalanceAuto = true;
                    if (Svision.GetMe().baslerCamera.getChannelNumber() == 3)
                    {
                        ConfigInformation.GetInstance().tCamCfg.whiteBalanceBluePercent = prvWhiteBalanceBluePercent;
                        ConfigInformation.GetInstance().tCamCfg.whiteBalanceGreenPercent = prvWhiteBalanceGreenPercent;
                        ConfigInformation.GetInstance().tCamCfg.whiteBalanceRedPercent = prvWhiteBalanceRedPercent;
                    }
                }
                else
                {
                    ConfigInformation.GetInstance().tCamCfg.isWhiteBalanceAuto = false;
                    checkBoxWhiteBalanceAuto.CheckState = CheckState.Unchecked;
                    if (Svision.GetMe().baslerCamera.getChannelNumber() == 3)
                    {
                        ConfigInformation.GetInstance().tCamCfg.whiteBalanceBluePercent = prvWhiteBalanceBluePercent;
                        ConfigInformation.GetInstance().tCamCfg.whiteBalanceGreenPercent = prvWhiteBalanceGreenPercent;
                        ConfigInformation.GetInstance().tCamCfg.whiteBalanceRedPercent = prvWhiteBalanceRedPercent;
                        numericUpDownBlueBalance.Value = (decimal)(Svision.GetMe().baslerCamera.getWhiteBalanceBluePercent() * 100);
                        numericUpDownRedBalance.Value = (decimal)(Svision.GetMe().baslerCamera.getWhiteBalanceRedPercent() * 100);
                        numericUpDownGreenBalance.Value = (decimal)(Svision.GetMe().baslerCamera.getWhiteBalanceGreenPercent() * 100);

                    }

                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            this.Close();
        }

        private void cameraParaPartpictureBox_SizeChanged(object sender, EventArgs e)
        {
            try
            {
                if (cameraParaHWHandle != null)
                {
                    HOperatorSet.SetWindowExtents(cameraParaPartHWHandle, 0, 0, (cameraParaPartpictureBox.Width), (cameraParaPartpictureBox.Height));
                    HOperatorSet.SetPart(cameraParaPartHWHandle, 0, 0, (cameraParaPartpictureBox.Height - 1), (cameraParaPartpictureBox.Width - 1));
                    basicClass.displayClear(cameraParaPartHWHandle);
                    if (image != null)
                    {

                        int rowNum, columnNum;
                        basicClass.getImageSize(image, out rowNum, out columnNum);
                        double widRat = 256 / ((double)columnNum);
                        double heiRat = 192 / ((double)rowNum);
                        double resRatNew = widRat < heiRat ? widRat : heiRat;
                        if (resRat != resRatNew)
                        {
                            basicClass.displayClear(cameraParaHWHandle);
                            resRat = resRatNew;
                        }
                        rectLocationX = preLocation.X;
                        rectLocationY = preLocation.Y;
                        if (preLocation.X <= 0)
                        {
                            rectLocationX = 0;
                        }
                        if (preLocation.Y <= 0)
                        {
                            rectLocationY = 0;
                        }
                        if (preLocation.X + cameraParaPartpictureBox.Width * resRat > Math.Min((columnNum - 1) * resRat - 1, 255))
                        {
                            rectLocationX = Math.Min((columnNum - cameraParaPartpictureBox.Width - 1) * resRat - 1, 255 - cameraParaPartpictureBox.Width * resRat);
                        }
                        if (preLocation.Y + cameraParaPartpictureBox.Height * resRat > Math.Min((rowNum - 1) * resRat - 1, 191))
                        {
                            rectLocationY = Math.Min((rowNum - cameraParaPartpictureBox.Height - 1) * resRat - 1, 191 - cameraParaPartpictureBox.Height * resRat);
                        }
                        double widRatPart = cameraParaPartpictureBox.Width / ((double)columnNum);
                        double heiRatPart = cameraParaPartpictureBox.Height / ((double)rowNum);
                        double resRatPartNew = widRatPart < heiRatPart ? widRatPart : heiRatPart;
                        if (resRatPart != resRatPartNew)
                        {
                            basicClass.displayClear(cameraParaPartHWHandle);
                            resRatPart = resRatPartNew;
                        }
                        if (img != null)
                        {
                            img.Dispose();
                        }
                        basicClass.resizeImage(image, out img, resRat);
                        HOperatorSet.DispObj(img, cameraParaHWHandle);
                        if (resRatPart > 1)
                        {
                            if (imgPart != null)
                            {
                                imgPart.Dispose();
                            }
                            HOperatorSet.CopyImage(image, out imgPart);
                            HOperatorSet.DispObj(imgPart, cameraParaPartHWHandle);
                        }
                        else
                        {
                            if (rowNum * resRat > 6 && columnNum * resRat > 8)
                            {
                                basicClass.displayRectangle1XldScreen(cameraParaHWHandle, Math.Max(rectLocationY, 1), Math.Max(rectLocationX, 1), rectLocationY + cameraParaPartpictureBox.Height * resRat - 2, rectLocationX + cameraParaPartpictureBox.Width * resRat - 2);
                                basicClass.genRectangle1(out rectDomain, rectLocationY / resRat, rectLocationX / resRat, Math.Min(rectLocationY / resRat + cameraParaPartpictureBox.Height - 1, rowNum - 1), Math.Min(rectLocationX / resRat + cameraParaPartpictureBox.Width - 1, columnNum - 1));
                                if (imgPart != null)
                                {
                                    imgPart.Dispose();
                                }
                                basicClass.reduceDomain(image, rectDomain, out imgPart);
                                HOperatorSet.CropDomain(imgPart, out imgPart);
                                HOperatorSet.DispObj(imgPart, cameraParaPartHWHandle);
                            }

                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
            	
            }
            

        }

        private void numericUpDownExposure_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (Svision.GetMe().baslerCamera.getIsExposureAuto() == false)
                {
                    Svision.GetMe().baslerCamera.setExposurePercent((double)numericUpDownExposure.Value / 100.0);
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void numericUpDownGain_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (Svision.GetMe().baslerCamera.getIsGainAuto() == false)
                {
                    Svision.GetMe().baslerCamera.setGainPercent((double)numericUpDownGain.Value / 100.0);
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void numericUpDownBlueBalance_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (Svision.GetMe().baslerCamera.getIsWhiteBalanceAuto() == false)
                {
                    Svision.GetMe().baslerCamera.setWhiteBalanceBlue((double)numericUpDownBlueBalance.Value / 100.0);
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void numericUpDownRedBalance_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (Svision.GetMe().baslerCamera.getIsWhiteBalanceAuto() == false)
                {
                    Svision.GetMe().baslerCamera.setWhiteBalanceRed((double)numericUpDownRedBalance.Value / 100.0);
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void numericUpDownGreenBalance_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (Svision.GetMe().baslerCamera.getIsWhiteBalanceAuto() == false)
                {
                    Svision.GetMe().baslerCamera.setWhiteBalanceGreen((double)numericUpDownGreenBalance.Value / 100.0);
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void numericUpDownGamma_ValueChanged(object sender, EventArgs e)
        {

            try
            {
                Svision.GetMe().baslerCamera.setGammaPercent((double)numericUpDownGamma.Value / 100.0);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
