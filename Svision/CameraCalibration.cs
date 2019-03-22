using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HalconDotNet;
using MathNet.Numerics.LinearAlgebra;
namespace Svision
{
    public partial class CameraCalibration : Form
    {
        HTuple CalibrationHWHandle, CalibrationPartHWHandle, hv_CalibDataID, hv_I;
        public HObject image, imageTemp, imageTempShow, imageRealRegion, imageDisplayPartRegion, img, imgPart, imgPartTemp, rectDomain, ho_Caltab, ho_Circles, ho_MapFixed;
        static double resizerate = 1;
        bool calibrationIsRadialDistortionFlag, calibrationParaConfirmFlag;
        Point oriLocation;
        PointF centerLocation;
        //PointF modelPoint;
        int rowNum, columnNum;
        static int rowOriNum, columnOriNum;
        double resRat;
        HTuple regionCenterRow, regionCenterColumn;
        double Sx, Sy, thea, Tx, Ty;
        Matrix mx;
        public CameraCalibration()
        {
            try
            {
                InitializeComponent();
                rowOriNum = Svision.GetMe().baslerCamera.getRowNumber();
                columnOriNum = Svision.GetMe().baslerCamera.getColumnNumber();
                double widRat = 256 / ((double)columnOriNum);
                double heiRat = 192 / ((double)rowOriNum);
                resRat = widRat < heiRat ? widRat : heiRat;
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
        ~CameraCalibration()
        {
            if (image != null)
            {
                image.Dispose();
            }
            if (imageTemp != null)
            {
                imageTemp.Dispose();
            }
            if (imageTempShow != null)
            {
                imageTempShow.Dispose();
            }
            if (img != null)
            {
                img.Dispose();
            }
            if (imgPart != null)
            {
                imgPart.Dispose();
            }
            if (imgPartTemp != null)
            {
                imgPartTemp.Dispose();
            }
        }

        private void CameraCalibration_Load(object sender, EventArgs e)
        {
            HOperatorSet.OpenWindow(0, 0, 256, 192, pictureBoxCalibration.Handle, "visible", "", out CalibrationHWHandle);
            HOperatorSet.SetPart(CalibrationHWHandle, 0, 0, 191, 255);
            HDevWindowStack.Push(CalibrationHWHandle);
            //HOperatorSet.SetLineWidth(CalibrationHWHandle, 1);
            //HOperatorSet.SetColor(CalibrationHWHandle, "green");
            HOperatorSet.OpenWindow(0, 0, (pictureBoxPartCalibration.Width), (pictureBoxPartCalibration.Height), pictureBoxPartCalibration.Handle, "visible", "", out CalibrationPartHWHandle);
            HOperatorSet.SetPart(CalibrationPartHWHandle, 0, 0, (pictureBoxPartCalibration.Height - 1), (pictureBoxPartCalibration.Width - 1));
            HDevWindowStack.Push(CalibrationPartHWHandle);

            this.pictureBoxCalibration.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.pictureBoxCalibration_MouseWheel);
            numericUpDownPointX.Maximum = (decimal)columnOriNum;
            numericUpDownPointY.Maximum = (decimal)rowOriNum;
            //set para
            if (ConfigInformation.GetInstance().tCalCfg.ho_MapFixed != null && ConfigInformation.GetInstance().tCalCfg.ho_MapFixed.IsInitialized())
            {
                HOperatorSet.CopyObj(ConfigInformation.GetInstance().tCalCfg.ho_MapFixed, out ho_MapFixed, 1, -1);
            }
            if (ConfigInformation.GetInstance().tCalCfg.mx != null && ConfigInformation.GetInstance().tCalCfg.useThreePointCalibration==true)
            {
                mx=ConfigInformation.GetInstance().tCalCfg.mx.Clone();
                checkBoxUseThreePointCalibration.Checked = true;
                numericUpDownPP1X.Value = (decimal)ConfigInformation.GetInstance().tCalCfg.p1x;
                numericUpDownPP2X.Value = (decimal)ConfigInformation.GetInstance().tCalCfg.p2x;
                numericUpDownPP3X.Value = (decimal)ConfigInformation.GetInstance().tCalCfg.p3x;
                numericUpDownPR1X.Value = (decimal)ConfigInformation.GetInstance().tCalCfg.r1x;
                numericUpDownPR2X.Value = (decimal)ConfigInformation.GetInstance().tCalCfg.r2x;
                numericUpDownPR3X.Value = (decimal)ConfigInformation.GetInstance().tCalCfg.r3x;
                numericUpDownPP1Y.Value = (decimal)ConfigInformation.GetInstance().tCalCfg.p1y;
                numericUpDownPP2Y.Value = (decimal)ConfigInformation.GetInstance().tCalCfg.p2y;
                numericUpDownPP3Y.Value = (decimal)ConfigInformation.GetInstance().tCalCfg.p3y;
                numericUpDownPR1Y.Value = (decimal)ConfigInformation.GetInstance().tCalCfg.r1y;
                numericUpDownPR2Y.Value = (decimal)ConfigInformation.GetInstance().tCalCfg.r2y;
                numericUpDownPR3Y.Value = (decimal)ConfigInformation.GetInstance().tCalCfg.r3y;
                Sx = ConfigInformation.GetInstance().tCalCfg.Sx;
                Sy = ConfigInformation.GetInstance().tCalCfg.Sy;
                thea = ConfigInformation.GetInstance().tCalCfg.thea;
                Tx = ConfigInformation.GetInstance().tCalCfg.Tx;
                Ty = ConfigInformation.GetInstance().tCalCfg.Ty;
                textBoxThreePointCalibration.Text = "X方向比例系数：" + Sx.ToString() + "\r\n" +
                    "方向比例系数：" + Sy.ToString() + "\r\n" +
                    "机器人坐标系旋转到视觉坐标系的旋转角度（逆时针为正）：" + thea.ToString() + "\r\n" +
                    "X方向平移变换系数：" + Tx.ToString() + "\r\n" + "Y方向平移变换系数：" + Ty.ToString() + "\r\n";
            }
            numericUpDownCalibrationDistance.Value = (decimal)ConfigInformation.GetInstance().tCalCfg.calibrationDistance;
            numericUpDownCalibrationPoint.Value = (decimal)ConfigInformation.GetInstance().tCalCfg.calibrationPoint;
            numericUpDownFocus.Value = (decimal)ConfigInformation.GetInstance().tCalCfg.focus;
            numericUpDownCalibrationRate.Value = (decimal)ConfigInformation.GetInstance().tCalCfg.calibrationRate;
            numericUpDownPixelSize.Value = (decimal)ConfigInformation.GetInstance().tCalCfg.pixelSize;
            calibrationParaConfirmFlag = ConfigInformation.GetInstance().tCalCfg.calibrationParaConfirmFlag;
            calibrationIsRadialDistortionFlag = ConfigInformation.GetInstance().tCalCfg.calibrationIsRadialDistortionFlag;
            if (ConfigInformation.GetInstance().tCalCfg.isCheckBoxRadialDistortion)
            {
                checkBoxRadialDistortion.Checked = true;
            } 
            else
            {
                checkBoxRadialDistortion.Checked = false;
            }
        }
        private void pictureBoxCalibrationMouseDown(object sender, MouseEventArgs e)
        {
            if (e.Location.X >= 0 && e.Location.X <= columnNum * resRat/*columnNum * resRat - 320 * resRat / resizerate*/ && e.Location.Y >= 0 && e.Location.Y <= rowNum * resRat/*rowNum * resRat - 240 * resRat / resizerate*/)
            {
                try
                {
                    Cursor = Cursors.Hand;
                    oriLocation = e.Location;
                    centerLocation.Y = (float)oriLocation.Y;
                    centerLocation.X = (float)oriLocation.X;
                    if (oriLocation.Y * resizerate / resRat - ((float)(pictureBoxPartCalibration.Height - 1 + 0) / 2.0) < 0 && oriLocation.X * resizerate / resRat - ((float)(pictureBoxPartCalibration.Width - 1 + 0) / 2.0) < 0)
                    {
                        HObject temp;
                        basicClass.genRectangle1(out rectDomain, 0, 0, (pictureBoxPartCalibration.Height - 1), (pictureBoxPartCalibration.Width - 1));
                        centerLocation.Y = (float)(((float)(System.Math.Min(pictureBoxPartCalibration.Height, rowNum * resizerate) - 1 + 0) / 2.0) * resRat / resizerate);
                        centerLocation.X = (float)(((float)(System.Math.Min(pictureBoxPartCalibration.Width, columnNum * resizerate) - 1 + 0) / 2.0) * resRat / resizerate);

                        basicClass.reduceDomain(imgPart, rectDomain, out temp);
                        if (imgPartTemp != null)
                        {
                            imgPartTemp.Dispose();
                        }
                        HOperatorSet.CropDomain(temp, out imgPartTemp);
                        HOperatorSet.DispObj(imgPartTemp, CalibrationPartHWHandle);
                        temp.Dispose();
                    }
                    else if (oriLocation.Y * resizerate / resRat - ((float)(pictureBoxPartCalibration.Height - 1 + 0) / 2.0) <= 0 && oriLocation.X * resizerate / resRat - ((float)(pictureBoxPartCalibration.Width - 1 + 0) / 2.0) >= 0 && oriLocation.X * resizerate / resRat + ((float)(pictureBoxPartCalibration.Width - 1 + 0) / 2.0) < columnNum * resizerate - 1)
                    {
                        HObject temp;
                        basicClass.genRectangle1(out rectDomain, 0, oriLocation.X * resizerate / resRat - ((float)(pictureBoxPartCalibration.Width - 1 + 0) / 2.0), (pictureBoxPartCalibration.Height - 1), oriLocation.X * resizerate / resRat + ((float)(pictureBoxPartCalibration.Width - 1 + 0) / 2.0));
                        centerLocation.Y = (float)(((float)(System.Math.Min(pictureBoxPartCalibration.Height, rowNum * resizerate) - 1 + 0) / 2.0) * resRat / resizerate);
                        centerLocation.X = (float)oriLocation.X;
                        basicClass.reduceDomain(imgPart, rectDomain, out temp);
                        if (imgPartTemp != null)
                        {
                            imgPartTemp.Dispose();
                        }
                        HOperatorSet.CropDomain(temp, out imgPartTemp);
                        HOperatorSet.DispObj(imgPartTemp, CalibrationPartHWHandle);
                        temp.Dispose();
                    }
                    else if (oriLocation.Y * resizerate / resRat - ((float)(pictureBoxPartCalibration.Height - 1 + 0) / 2.0) > 0 && oriLocation.Y * resizerate / resRat + ((float)(pictureBoxPartCalibration.Height - 1 + 0) / 2.0) <= rowNum * resizerate - 1 && oriLocation.X * resizerate / resRat - ((float)(pictureBoxPartCalibration.Width - 1 + 0) / 2.0) < 0)
                    {
                        HObject temp;
                        basicClass.genRectangle1(out rectDomain, oriLocation.Y * resizerate / resRat - ((float)(pictureBoxPartCalibration.Height - 1 + 0) / 2.0), 0, oriLocation.Y * resizerate / resRat + ((float)(pictureBoxPartCalibration.Height - 1 + 0) / 2.0), (pictureBoxPartCalibration.Width - 1));
                        centerLocation.Y = (float)oriLocation.Y;
                        centerLocation.X = (float)(((float)(System.Math.Min(pictureBoxPartCalibration.Width, columnNum * resizerate) - 1 + 0) / 2.0) * resRat / resizerate);
                        basicClass.reduceDomain(imgPart, rectDomain, out temp);
                        if (imgPartTemp != null)
                        {
                            imgPartTemp.Dispose();
                        }
                        HOperatorSet.CropDomain(temp, out imgPartTemp);
                        HOperatorSet.DispObj(imgPartTemp, CalibrationPartHWHandle);
                        temp.Dispose();
                    }
                    else if (oriLocation.Y * resizerate / resRat + ((float)(pictureBoxPartCalibration.Height - 1 + 0) / 2.0) > rowNum * resizerate - 1 && oriLocation.X * resizerate / resRat - ((float)(pictureBoxPartCalibration.Width - 1 + 0) / 2.0) < 0)
                    {
                        HObject temp;
                        basicClass.genRectangle1(out rectDomain, rowNum * resizerate - 1 - (pictureBoxPartCalibration.Height - 1), 0, rowNum * resizerate - 1, (pictureBoxPartCalibration.Width - 1));
                        centerLocation.Y = (float)((rowNum * resizerate - ((float)(System.Math.Min(pictureBoxPartCalibration.Height, rowNum * resizerate) - 1 + 0) / 2.0) - 1) * resRat / resizerate);
                        centerLocation.X = (float)(((float)(System.Math.Min(pictureBoxPartCalibration.Width, columnNum * resizerate) - 1 + 0) / 2.0) * resRat / resizerate);
                        basicClass.reduceDomain(imgPart, rectDomain, out temp);
                        if (imgPartTemp != null)
                        {
                            imgPartTemp.Dispose();
                        }
                        HOperatorSet.CropDomain(temp, out imgPartTemp);
                        HOperatorSet.DispObj(imgPartTemp, CalibrationPartHWHandle);
                        temp.Dispose();
                    }
                    else if (oriLocation.Y * resizerate / resRat + ((float)(pictureBoxPartCalibration.Height - 1 + 0) / 2.0) > rowNum * resizerate - 1 && oriLocation.X * resizerate / resRat - ((float)(pictureBoxPartCalibration.Width - 1 + 0) / 2.0) > 0 && oriLocation.X * resizerate / resRat + ((float)(pictureBoxPartCalibration.Width - 1 + 0) / 2.0) < columnNum * resizerate - 1)
                    {
                        HObject temp;
                        basicClass.genRectangle1(out rectDomain, rowNum * resizerate - (pictureBoxPartCalibration.Height), oriLocation.X * resizerate / resRat - ((float)(pictureBoxPartCalibration.Width - 1 + 0) / 2.0), rowNum * resizerate - 1, oriLocation.X * resizerate / resRat + ((float)(pictureBoxPartCalibration.Width - 1 + 0) / 2.0));
                        centerLocation.Y = (float)((rowNum * resizerate - ((float)(System.Math.Min(pictureBoxPartCalibration.Height, rowNum * resizerate) - 1 + 0) / 2.0 + 1)) * resRat / resizerate);
                        centerLocation.X = (float)oriLocation.X;
                        basicClass.reduceDomain(imgPart, rectDomain, out temp);
                        if (imgPartTemp != null)
                        {
                            imgPartTemp.Dispose();
                        }
                        HOperatorSet.CropDomain(temp, out imgPartTemp);
                        HOperatorSet.DispObj(imgPartTemp, CalibrationPartHWHandle);
                        temp.Dispose();
                    }
                    else if (oriLocation.Y * resizerate / resRat + ((float)(pictureBoxPartCalibration.Height - 1 + 0) / 2.0) >= rowNum * resizerate - 1 && oriLocation.X * resizerate / resRat + ((float)(pictureBoxPartCalibration.Width - 1 + 0) / 2.0) >= columnNum * resizerate - 1)
                    {
                        HObject temp;
                        basicClass.genRectangle1(out rectDomain, rowNum * resizerate - (pictureBoxPartCalibration.Height), columnNum * resizerate - (pictureBoxPartCalibration.Width), rowNum * resizerate - 1, columnNum * resizerate - 1);
                        centerLocation.Y = (float)((rowNum * resizerate - ((float)(System.Math.Min(pictureBoxPartCalibration.Height, rowNum * resizerate) - 1 + 0) / 2.0 + 1)) * resRat / resizerate);
                        centerLocation.X = (float)((columnNum * resizerate - ((float)(System.Math.Min(pictureBoxPartCalibration.Width, columnNum * resizerate) - 1 + 0) / 2.0 + 1)) * resRat / resizerate);
                        basicClass.reduceDomain(imgPart, rectDomain, out temp);
                        if (imgPartTemp != null)
                        {
                            imgPartTemp.Dispose();
                        }
                        HOperatorSet.CropDomain(temp, out imgPartTemp);
                        HOperatorSet.DispObj(imgPartTemp, CalibrationPartHWHandle);
                        temp.Dispose();
                    }
                    else if (oriLocation.Y * resizerate / resRat + ((float)(pictureBoxPartCalibration.Height - 1 + 0) / 2.0) < rowNum * resizerate - 1 && oriLocation.Y * resizerate / resRat - ((float)(pictureBoxPartCalibration.Height - 1 + 0) / 2.0) > 0 && oriLocation.X * resizerate / resRat + ((float)(pictureBoxPartCalibration.Width - 1 + 0) / 2.0) > columnNum * resizerate - 1)
                    {
                        HObject temp;
                        basicClass.genRectangle1(out rectDomain, oriLocation.Y * resizerate / resRat - ((float)(pictureBoxPartCalibration.Height - 1 + 0) / 2.0), columnNum * resizerate - (pictureBoxPartCalibration.Width), oriLocation.Y * resizerate / resRat + ((float)(pictureBoxPartCalibration.Height - 1 + 0) / 2.0), columnNum * resizerate - 1);
                        centerLocation.Y = (float)oriLocation.Y;
                        centerLocation.X = (float)((columnNum * resizerate - ((float)(System.Math.Min(pictureBoxPartCalibration.Width, columnNum * resizerate) - 1 + 0) / 2.0 + 1)) * resRat / resizerate);
                        basicClass.reduceDomain(imgPart, rectDomain, out temp);
                        if (imgPartTemp != null)
                        {
                            imgPartTemp.Dispose();
                        }
                        HOperatorSet.CropDomain(temp, out imgPartTemp);
                        HOperatorSet.DispObj(imgPartTemp, CalibrationPartHWHandle);
                        temp.Dispose();
                    }
                    else if (oriLocation.Y * resizerate / resRat - ((float)(pictureBoxPartCalibration.Height - 1 + 0) / 2.0) <= 0 && oriLocation.X * resizerate / resRat + ((float)(pictureBoxPartCalibration.Width - 1 + 0) / 2.0) >= columnNum * resizerate - 1)
                    {
                        HObject temp;
                        basicClass.genRectangle1(out rectDomain, 0, columnNum * resizerate - (pictureBoxPartCalibration.Width), (pictureBoxPartCalibration.Height - 1), columnNum * resizerate - 1);
                        centerLocation.Y = (float)(((float)(System.Math.Min(pictureBoxPartCalibration.Height, rowNum * resizerate) - 1 + 0) / 2.0) * resRat / resizerate);
                        centerLocation.X = (float)((columnNum * resizerate - ((float)(System.Math.Min(pictureBoxPartCalibration.Width, columnNum * resizerate) - 1 + 0) / 2.0 + 1)) * resRat / resizerate);
                        basicClass.reduceDomain(imgPart, rectDomain, out temp);
                        if (imgPartTemp != null)
                        {
                            imgPartTemp.Dispose();
                        }
                        HOperatorSet.CropDomain(temp, out imgPartTemp);
                        HOperatorSet.DispObj(imgPartTemp, CalibrationPartHWHandle);
                        temp.Dispose();
                    }
                    else
                    {
                        HObject temp;
                        basicClass.genRectangle1(out rectDomain, oriLocation.Y * resizerate / resRat - ((float)(pictureBoxPartCalibration.Height - 1 + 0) / 2.0), oriLocation.X * resizerate / resRat - ((float)(pictureBoxPartCalibration.Width - 1 + 0) / 2.0), oriLocation.Y * resizerate / resRat + ((float)(pictureBoxPartCalibration.Height - 1 + 0) / 2.0), oriLocation.X * resizerate / resRat + ((float)(pictureBoxPartCalibration.Width - 1 + 0) / 2.0));
                        basicClass.reduceDomain(imgPart, rectDomain, out temp);
                        if (imgPartTemp != null)
                        {
                            imgPartTemp.Dispose();
                        }
                        HOperatorSet.CropDomain(temp, out imgPartTemp);
                        HOperatorSet.DispObj(imgPartTemp, CalibrationPartHWHandle);
                        temp.Dispose();
                    }
                    this.pictureBoxCalibration.Focus();
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void pictureBoxCalibrationMouseMove(object sender, MouseEventArgs e)
        {
            if (e.Location.X >= 0 && e.Location.X <= columnNum * resRat/*columnNum * resRat - 320 * resRat / resizerate*/ && e.Location.Y >= 0 && e.Location.Y <= rowNum * resRat/*rowNum * resRat - 240 * resRat / resizerate*/)
            {
                try
                {
                    if (e.Button == MouseButtons.Left)
                    {
                        //Point preLocation;
                        //preLocation.X = e.Location.X;
                        //preLocation.Y = e.Location.Y;
                        Cursor = Cursors.Hand;
                        oriLocation = e.Location;
                        centerLocation.Y = (float)oriLocation.Y;
                        centerLocation.X = (float)oriLocation.X;
                        if (oriLocation.Y * resizerate / resRat - ((float)(pictureBoxPartCalibration.Height - 1 + 0) / 2.0) < 0 && oriLocation.X * resizerate / resRat - ((float)(pictureBoxPartCalibration.Width - 1 + 0) / 2.0) < 0)
                        {
                            HObject temp;
                            basicClass.genRectangle1(out rectDomain, 0, 0, (pictureBoxPartCalibration.Height - 1), (pictureBoxPartCalibration.Width - 1));
                            centerLocation.Y = (float)(((float)(System.Math.Min(pictureBoxPartCalibration.Height, rowNum * resizerate) - 1 + 0) / 2.0) * resRat / resizerate);
                            centerLocation.X = (float)(((float)(System.Math.Min(pictureBoxPartCalibration.Width, columnNum * resizerate) - 1 + 0) / 2.0) * resRat / resizerate);

                            basicClass.reduceDomain(imgPart, rectDomain, out temp);
                            if (imgPartTemp != null)
                            {
                                imgPartTemp.Dispose();
                            }
                            HOperatorSet.CropDomain(temp, out imgPartTemp);
                            HOperatorSet.DispObj(imgPartTemp, CalibrationPartHWHandle);
                            temp.Dispose();
                        }
                        else if (oriLocation.Y * resizerate / resRat - ((float)(pictureBoxPartCalibration.Height - 1 + 0) / 2.0) <= 0 && oriLocation.X * resizerate / resRat - ((float)(pictureBoxPartCalibration.Width - 1 + 0) / 2.0) >= 0 && oriLocation.X * resizerate / resRat + ((float)(pictureBoxPartCalibration.Width - 1 + 0) / 2.0) < columnNum * resizerate - 1)
                        {
                            HObject temp;
                            basicClass.genRectangle1(out rectDomain, 0, oriLocation.X * resizerate / resRat - ((float)(pictureBoxPartCalibration.Width - 1 + 0) / 2.0), (pictureBoxPartCalibration.Height - 1), oriLocation.X * resizerate / resRat + ((float)(pictureBoxPartCalibration.Width - 1 + 0) / 2.0));
                            centerLocation.Y = (float)(((float)(System.Math.Min(pictureBoxPartCalibration.Height, rowNum * resizerate) - 1 + 0) / 2.0) * resRat / resizerate);
                            centerLocation.X = (float)oriLocation.X;
                            basicClass.reduceDomain(imgPart, rectDomain, out temp);
                            if (imgPartTemp != null)
                            {
                                imgPartTemp.Dispose();
                            }
                            HOperatorSet.CropDomain(temp, out imgPartTemp);
                            HOperatorSet.DispObj(imgPartTemp, CalibrationPartHWHandle);
                            temp.Dispose();
                        }
                        else if (oriLocation.Y * resizerate / resRat - ((float)(pictureBoxPartCalibration.Height - 1 + 0) / 2.0) > 0 && oriLocation.Y * resizerate / resRat + ((float)(pictureBoxPartCalibration.Height - 1 + 0) / 2.0) <= rowNum * resizerate - 1 && oriLocation.X * resizerate / resRat - ((float)(pictureBoxPartCalibration.Width - 1 + 0) / 2.0) < 0)
                        {
                            HObject temp;
                            basicClass.genRectangle1(out rectDomain, oriLocation.Y * resizerate / resRat - ((float)(pictureBoxPartCalibration.Height - 1 + 0) / 2.0), 0, oriLocation.Y * resizerate / resRat + ((float)(pictureBoxPartCalibration.Height - 1 + 0) / 2.0), (pictureBoxPartCalibration.Width - 1));
                            centerLocation.Y = (float)oriLocation.Y;
                            centerLocation.X = (float)(((float)(System.Math.Min(pictureBoxPartCalibration.Width, columnNum * resizerate) - 1 + 0) / 2.0) * resRat / resizerate);
                            basicClass.reduceDomain(imgPart, rectDomain, out temp);
                            if (imgPartTemp != null)
                            {
                                imgPartTemp.Dispose();
                            }
                            HOperatorSet.CropDomain(temp, out imgPartTemp);
                            HOperatorSet.DispObj(imgPartTemp, CalibrationPartHWHandle);
                            temp.Dispose();
                        }
                        else if (oriLocation.Y * resizerate / resRat + ((float)(pictureBoxPartCalibration.Height - 1 + 0) / 2.0) > rowNum * resizerate - 1 && oriLocation.X * resizerate / resRat - ((float)(pictureBoxPartCalibration.Width - 1 + 0) / 2.0) < 0)
                        {
                            HObject temp;
                            basicClass.genRectangle1(out rectDomain, rowNum * resizerate - 1 - (pictureBoxPartCalibration.Height - 1), 0, rowNum * resizerate - 1, (pictureBoxPartCalibration.Width - 1));
                            centerLocation.Y = (float)((rowNum * resizerate - ((float)(System.Math.Min(pictureBoxPartCalibration.Height, rowNum * resizerate) - 1 + 0) / 2.0) - 1) * resRat / resizerate);
                            centerLocation.X = (float)(((float)(System.Math.Min(pictureBoxPartCalibration.Width, columnNum * resizerate) - 1 + 0) / 2.0) * resRat / resizerate);
                            basicClass.reduceDomain(imgPart, rectDomain, out temp);
                            if (imgPartTemp != null)
                            {
                                imgPartTemp.Dispose();
                            }
                            HOperatorSet.CropDomain(temp, out imgPartTemp);
                            HOperatorSet.DispObj(imgPartTemp, CalibrationPartHWHandle);
                            temp.Dispose();
                        }
                        else if (oriLocation.Y * resizerate / resRat + ((float)(pictureBoxPartCalibration.Height - 1 + 0) / 2.0) > rowNum * resizerate - 1 && oriLocation.X * resizerate / resRat - ((float)(pictureBoxPartCalibration.Width - 1 + 0) / 2.0) > 0 && oriLocation.X * resizerate / resRat + ((float)(pictureBoxPartCalibration.Width - 1 + 0) / 2.0) < columnNum * resizerate - 1)
                        {
                            HObject temp;
                            basicClass.genRectangle1(out rectDomain, rowNum * resizerate - (pictureBoxPartCalibration.Height), oriLocation.X * resizerate / resRat - ((float)(pictureBoxPartCalibration.Width - 1 + 0) / 2.0), rowNum * resizerate - 1, oriLocation.X * resizerate / resRat + ((float)(pictureBoxPartCalibration.Width - 1 + 0) / 2.0));
                            centerLocation.Y = (float)((rowNum * resizerate - ((float)(System.Math.Min(pictureBoxPartCalibration.Height, rowNum * resizerate) - 1 + 0) / 2.0 + 1)) * resRat / resizerate);
                            centerLocation.X = (float)oriLocation.X;
                            basicClass.reduceDomain(imgPart, rectDomain, out temp);
                            if (imgPartTemp != null)
                            {
                                imgPartTemp.Dispose();
                            }
                            HOperatorSet.CropDomain(temp, out imgPartTemp);
                            HOperatorSet.DispObj(imgPartTemp, CalibrationPartHWHandle);
                            temp.Dispose();
                        }
                        else if (oriLocation.Y * resizerate / resRat + ((float)(pictureBoxPartCalibration.Height - 1 + 0) / 2.0) >= rowNum * resizerate - 1 && oriLocation.X * resizerate / resRat + ((float)(pictureBoxPartCalibration.Width - 1 + 0) / 2.0) >= columnNum * resizerate - 1)
                        {
                            HObject temp;
                            basicClass.genRectangle1(out rectDomain, rowNum * resizerate - (pictureBoxPartCalibration.Height), columnNum * resizerate - (pictureBoxPartCalibration.Width), rowNum * resizerate - 1, columnNum * resizerate - 1);
                            centerLocation.Y = (float)((rowNum * resizerate - ((float)(System.Math.Min(pictureBoxPartCalibration.Height, rowNum * resizerate) - 1 + 0) / 2.0 + 1)) * resRat / resizerate);
                            centerLocation.X = (float)((columnNum * resizerate - ((float)(System.Math.Min(pictureBoxPartCalibration.Width, columnNum * resizerate) - 1 + 0) / 2.0 + 1)) * resRat / resizerate);
                            basicClass.reduceDomain(imgPart, rectDomain, out temp);
                            if (imgPartTemp != null)
                            {
                                imgPartTemp.Dispose();
                            }
                            HOperatorSet.CropDomain(temp, out imgPartTemp);
                            HOperatorSet.DispObj(imgPartTemp, CalibrationPartHWHandle);
                            temp.Dispose();
                        }
                        else if (oriLocation.Y * resizerate / resRat + ((float)(pictureBoxPartCalibration.Height - 1 + 0) / 2.0) < rowNum * resizerate - 1 && oriLocation.Y * resizerate / resRat - ((float)(pictureBoxPartCalibration.Height - 1 + 0) / 2.0) > 0 && oriLocation.X * resizerate / resRat + ((float)(pictureBoxPartCalibration.Width - 1 + 0) / 2.0) > columnNum * resizerate - 1)
                        {
                            HObject temp;
                            basicClass.genRectangle1(out rectDomain, oriLocation.Y * resizerate / resRat - ((float)(pictureBoxPartCalibration.Height - 1 + 0) / 2.0), columnNum * resizerate - (pictureBoxPartCalibration.Width), oriLocation.Y * resizerate / resRat + ((float)(pictureBoxPartCalibration.Height - 1 + 0) / 2.0), columnNum * resizerate - 1);
                            centerLocation.Y = (float)oriLocation.Y;
                            centerLocation.X = (float)((columnNum * resizerate - ((float)(System.Math.Min(pictureBoxPartCalibration.Width, columnNum * resizerate) - 1 + 0) / 2.0 + 1)) * resRat / resizerate);
                            basicClass.reduceDomain(imgPart, rectDomain, out temp);
                            if (imgPartTemp != null)
                            {
                                imgPartTemp.Dispose();
                            }
                            HOperatorSet.CropDomain(temp, out imgPartTemp);
                            HOperatorSet.DispObj(imgPartTemp, CalibrationPartHWHandle);
                            temp.Dispose();
                        }
                        else if (oriLocation.Y * resizerate / resRat - ((float)(pictureBoxPartCalibration.Height - 1 + 0) / 2.0) <= 0 && oriLocation.X * resizerate / resRat + ((float)(pictureBoxPartCalibration.Width - 1 + 0) / 2.0) >= columnNum * resizerate - 1)
                        {
                            HObject temp;
                            basicClass.genRectangle1(out rectDomain, 0, columnNum * resizerate - (pictureBoxPartCalibration.Width), (pictureBoxPartCalibration.Height - 1), columnNum * resizerate - 1);
                            centerLocation.Y = (float)(((float)(System.Math.Min(pictureBoxPartCalibration.Height, rowNum * resizerate) - 1 + 0) / 2.0) * resRat / resizerate);
                            centerLocation.X = (float)((columnNum * resizerate - ((float)(System.Math.Min(pictureBoxPartCalibration.Width, columnNum * resizerate) - 1 + 0) / 2.0 + 1)) * resRat / resizerate);
                            basicClass.reduceDomain(imgPart, rectDomain, out temp);
                            if (imgPartTemp != null)
                            {
                                imgPartTemp.Dispose();
                            }
                            HOperatorSet.CropDomain(temp, out imgPartTemp);
                            HOperatorSet.DispObj(imgPartTemp, CalibrationPartHWHandle);
                            temp.Dispose();
                        }
                        else
                        {
                            HObject temp;
                            basicClass.genRectangle1(out rectDomain, oriLocation.Y * resizerate / resRat - ((float)(pictureBoxPartCalibration.Height - 1 + 0) / 2.0), oriLocation.X * resizerate / resRat - ((float)(pictureBoxPartCalibration.Width - 1 + 0) / 2.0), oriLocation.Y * resizerate / resRat + ((float)(pictureBoxPartCalibration.Height - 1 + 0) / 2.0), oriLocation.X * resizerate / resRat + ((float)(pictureBoxPartCalibration.Width - 1 + 0) / 2.0));
                            basicClass.reduceDomain(imgPart, rectDomain, out temp);
                            if (imgPartTemp != null)
                            {
                                imgPartTemp.Dispose();
                            }
                            HOperatorSet.CropDomain(temp, out imgPartTemp);
                            HOperatorSet.DispObj(imgPartTemp, CalibrationPartHWHandle);
                            temp.Dispose();
                        }

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
        }
        private void pictureBoxCalibration_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Location.X >= 0 && e.Location.X <= columnNum * resRat/*columnNum * resRat - 320 * resRat / resizerate*/ && e.Location.Y >= 0 && e.Location.Y <= rowNum * resRat/*rowNum * resRat - 240 * resRat / resizerate*/)
            {
                try
                {
                    if (e.Delta > 0)
                    {
                        resizerate = resizerate * 1.2;
                    }
                    else
                    {
                        resizerate = resizerate * 0.8;
                    }
                    if (resizerate >= 4)
                    {
                        resizerate = 4;
                    }
                    if (resizerate <= 0.125)
                    {
                        resizerate = 0.125;
                    }
                    if (rowNum * resizerate < (pictureBoxPartCalibration.Height) || columnNum * resizerate < (pictureBoxPartCalibration.Width))
                    {
                        basicClass.displayClear(CalibrationPartHWHandle);
                    }
                    if (imgPart != null)
                    {
                        imgPart.Dispose();
                    }
                    basicClass.resizeImage(imageTempShow, out imgPart, resizerate);
                    // HOperatorSet.DispObj(imgPart, CalibrationPartHWHandle);
                    oriLocation = e.Location;
                    centerLocation.Y = (float)oriLocation.Y;
                    centerLocation.X = (float)oriLocation.X;
                    if (oriLocation.Y * resizerate / resRat - ((float)(pictureBoxPartCalibration.Height - 1 + 0) / 2.0) < 0 && oriLocation.X * resizerate / resRat - ((float)(pictureBoxPartCalibration.Width - 1 + 0) / 2.0) < 0)
                    {
                        HObject temp;
                        basicClass.genRectangle1(out rectDomain, 0, 0, (pictureBoxPartCalibration.Height - 1), (pictureBoxPartCalibration.Width - 1));
                        centerLocation.Y = (float)(((float)(System.Math.Min(pictureBoxPartCalibration.Height, rowNum * resizerate) - 1 + 0) / 2.0) * resRat / resizerate);
                        centerLocation.X = (float)(((float)(System.Math.Min(pictureBoxPartCalibration.Width, columnNum * resizerate) - 1 + 0) / 2.0) * resRat / resizerate);

                        basicClass.reduceDomain(imgPart, rectDomain, out temp);
                        if (imgPartTemp != null)
                        {
                            imgPartTemp.Dispose();
                        }
                        HOperatorSet.CropDomain(temp, out imgPartTemp);
                        HOperatorSet.DispObj(imgPartTemp, CalibrationPartHWHandle);
                        temp.Dispose();
                    }
                    else if (oriLocation.Y * resizerate / resRat - ((float)(pictureBoxPartCalibration.Height - 1 + 0) / 2.0) <= 0 && oriLocation.X * resizerate / resRat - ((float)(pictureBoxPartCalibration.Width - 1 + 0) / 2.0) >= 0 && oriLocation.X * resizerate / resRat + ((float)(pictureBoxPartCalibration.Width - 1 + 0) / 2.0) < columnNum * resizerate - 1)
                    {
                        HObject temp;
                        basicClass.genRectangle1(out rectDomain, 0, oriLocation.X * resizerate / resRat - ((float)(pictureBoxPartCalibration.Width - 1 + 0) / 2.0), (pictureBoxPartCalibration.Height - 1), oriLocation.X * resizerate / resRat + ((float)(pictureBoxPartCalibration.Width - 1 + 0) / 2.0));
                        centerLocation.Y = (float)(((float)(System.Math.Min(pictureBoxPartCalibration.Height, rowNum * resizerate) - 1 + 0) / 2.0) * resRat / resizerate);
                        centerLocation.X = (float)oriLocation.X;
                        basicClass.reduceDomain(imgPart, rectDomain, out temp);
                        if (imgPartTemp != null)
                        {
                            imgPartTemp.Dispose();
                        }
                        HOperatorSet.CropDomain(temp, out imgPartTemp);
                        HOperatorSet.DispObj(imgPartTemp, CalibrationPartHWHandle);
                        temp.Dispose();
                    }
                    else if (oriLocation.Y * resizerate / resRat - ((float)(pictureBoxPartCalibration.Height - 1 + 0) / 2.0) > 0 && oriLocation.Y * resizerate / resRat + ((float)(pictureBoxPartCalibration.Height - 1 + 0) / 2.0) <= rowNum * resizerate - 1 && oriLocation.X * resizerate / resRat - ((float)(pictureBoxPartCalibration.Width - 1 + 0) / 2.0) < 0)
                    {
                        HObject temp;
                        basicClass.genRectangle1(out rectDomain, oriLocation.Y * resizerate / resRat - ((float)(pictureBoxPartCalibration.Height - 1 + 0) / 2.0), 0, oriLocation.Y * resizerate / resRat + ((float)(pictureBoxPartCalibration.Height - 1 + 0) / 2.0), (pictureBoxPartCalibration.Width - 1));
                        centerLocation.Y = (float)oriLocation.Y;
                        centerLocation.X = (float)(((float)(System.Math.Min(pictureBoxPartCalibration.Width, columnNum * resizerate) - 1 + 0) / 2.0) * resRat / resizerate);
                        basicClass.reduceDomain(imgPart, rectDomain, out temp);
                        if (imgPartTemp != null)
                        {
                            imgPartTemp.Dispose();
                        }
                        HOperatorSet.CropDomain(temp, out imgPartTemp);
                        HOperatorSet.DispObj(imgPartTemp, CalibrationPartHWHandle);
                        temp.Dispose();
                    }
                    else if (oriLocation.Y * resizerate / resRat + ((float)(pictureBoxPartCalibration.Height - 1 + 0) / 2.0) > rowNum * resizerate - 1 && oriLocation.X * resizerate / resRat - ((float)(pictureBoxPartCalibration.Width - 1 + 0) / 2.0) < 0)
                    {
                        HObject temp;
                        basicClass.genRectangle1(out rectDomain, rowNum * resizerate - 1 - (pictureBoxPartCalibration.Height - 1), 0, rowNum * resizerate - 1, (pictureBoxPartCalibration.Width - 1));
                        centerLocation.Y = (float)((rowNum * resizerate - ((float)(System.Math.Min(pictureBoxPartCalibration.Height, rowNum * resizerate) - 1 + 0) / 2.0) - 1) * resRat / resizerate);
                        centerLocation.X = (float)(((float)(System.Math.Min(pictureBoxPartCalibration.Width, columnNum * resizerate) - 1 + 0) / 2.0) * resRat / resizerate);
                        basicClass.reduceDomain(imgPart, rectDomain, out temp);
                        if (imgPartTemp != null)
                        {
                            imgPartTemp.Dispose();
                        }
                        HOperatorSet.CropDomain(temp, out imgPartTemp);
                        HOperatorSet.DispObj(imgPartTemp, CalibrationPartHWHandle);
                        temp.Dispose();
                    }
                    else if (oriLocation.Y * resizerate / resRat + ((float)(pictureBoxPartCalibration.Height - 1 + 0) / 2.0) > rowNum * resizerate - 1 && oriLocation.X * resizerate / resRat - ((float)(pictureBoxPartCalibration.Width - 1 + 0) / 2.0) > 0 && oriLocation.X * resizerate / resRat + ((float)(pictureBoxPartCalibration.Width - 1 + 0) / 2.0) < columnNum * resizerate - 1)
                    {
                        HObject temp;
                        basicClass.genRectangle1(out rectDomain, rowNum * resizerate - (pictureBoxPartCalibration.Height), oriLocation.X * resizerate / resRat - ((float)(pictureBoxPartCalibration.Width - 1 + 0) / 2.0), rowNum * resizerate - 1, oriLocation.X * resizerate / resRat + ((float)(pictureBoxPartCalibration.Width - 1 + 0) / 2.0));
                        centerLocation.Y = (float)((rowNum * resizerate - ((float)(System.Math.Min(pictureBoxPartCalibration.Height, rowNum * resizerate) - 1 + 0) / 2.0 + 1)) * resRat / resizerate);
                        centerLocation.X = (float)oriLocation.X;
                        basicClass.reduceDomain(imgPart, rectDomain, out temp);
                        if (imgPartTemp != null)
                        {
                            imgPartTemp.Dispose();
                        }
                        HOperatorSet.CropDomain(temp, out imgPartTemp);
                        HOperatorSet.DispObj(imgPartTemp, CalibrationPartHWHandle);
                        temp.Dispose();
                    }
                    else if (oriLocation.Y * resizerate / resRat + ((float)(pictureBoxPartCalibration.Height - 1 + 0) / 2.0) >= rowNum * resizerate - 1 && oriLocation.X * resizerate / resRat + ((float)(pictureBoxPartCalibration.Width - 1 + 0) / 2.0) >= columnNum * resizerate - 1)
                    {
                        HObject temp;
                        basicClass.genRectangle1(out rectDomain, rowNum * resizerate - (pictureBoxPartCalibration.Height), columnNum * resizerate - (pictureBoxPartCalibration.Width), rowNum * resizerate - 1, columnNum * resizerate - 1);
                        centerLocation.Y = (float)((rowNum * resizerate - ((float)(System.Math.Min(pictureBoxPartCalibration.Height, rowNum * resizerate) - 1 + 0) / 2.0 + 1)) * resRat / resizerate);
                        centerLocation.X = (float)((columnNum * resizerate - ((float)(System.Math.Min(pictureBoxPartCalibration.Width, columnNum * resizerate) - 1 + 0) / 2.0 + 1)) * resRat / resizerate);
                        basicClass.reduceDomain(imgPart, rectDomain, out temp);
                        if (imgPartTemp != null)
                        {
                            imgPartTemp.Dispose();
                        }
                        HOperatorSet.CropDomain(temp, out imgPartTemp);
                        HOperatorSet.DispObj(imgPartTemp, CalibrationPartHWHandle);
                        temp.Dispose();
                    }
                    else if (oriLocation.Y * resizerate / resRat + ((float)(pictureBoxPartCalibration.Height - 1 + 0) / 2.0) < rowNum * resizerate - 1 && oriLocation.Y * resizerate / resRat - ((float)(pictureBoxPartCalibration.Height - 1 + 0) / 2.0) > 0 && oriLocation.X * resizerate / resRat + ((float)(pictureBoxPartCalibration.Width - 1 + 0) / 2.0) > columnNum * resizerate - 1)
                    {
                        HObject temp;
                        basicClass.genRectangle1(out rectDomain, oriLocation.Y * resizerate / resRat - ((float)(pictureBoxPartCalibration.Height - 1 + 0) / 2.0), columnNum * resizerate - (pictureBoxPartCalibration.Width), oriLocation.Y * resizerate / resRat + ((float)(pictureBoxPartCalibration.Height - 1 + 0) / 2.0), columnNum * resizerate - 1);
                        centerLocation.Y = (float)oriLocation.Y;
                        centerLocation.X = (float)((columnNum * resizerate - ((float)(System.Math.Min(pictureBoxPartCalibration.Width, columnNum * resizerate) - 1 + 0) / 2.0 + 1)) * resRat / resizerate);
                        basicClass.reduceDomain(imgPart, rectDomain, out temp);
                        if (imgPartTemp != null)
                        {
                            imgPartTemp.Dispose();
                        }
                        HOperatorSet.CropDomain(temp, out imgPartTemp);
                        HOperatorSet.DispObj(imgPartTemp, CalibrationPartHWHandle);
                        temp.Dispose();
                    }
                    else if (oriLocation.Y * resizerate / resRat - ((float)(pictureBoxPartCalibration.Height - 1 + 0) / 2.0) <= 0 && oriLocation.X * resizerate / resRat + ((float)(pictureBoxPartCalibration.Width - 1 + 0) / 2.0) >= columnNum * resizerate - 1)
                    {
                        HObject temp;
                        basicClass.genRectangle1(out rectDomain, 0, columnNum * resizerate - (pictureBoxPartCalibration.Width), (pictureBoxPartCalibration.Height - 1), columnNum * resizerate - 1);
                        centerLocation.Y = (float)(((float)(System.Math.Min(pictureBoxPartCalibration.Height, rowNum * resizerate) - 1 + 0) / 2.0) * resRat / resizerate);
                        centerLocation.X = (float)((columnNum * resizerate - ((float)(System.Math.Min(pictureBoxPartCalibration.Width, columnNum * resizerate) - 1 + 0) / 2.0 + 1)) * resRat / resizerate);
                        basicClass.reduceDomain(imgPart, rectDomain, out temp);
                        if (imgPartTemp != null)
                        {
                            imgPartTemp.Dispose();
                        }
                        HOperatorSet.CropDomain(temp, out imgPartTemp);
                        HOperatorSet.DispObj(imgPartTemp, CalibrationPartHWHandle);
                        temp.Dispose();
                    }
                    else
                    {
                        HObject temp;
                        basicClass.genRectangle1(out rectDomain, oriLocation.Y * resizerate / resRat - ((float)(pictureBoxPartCalibration.Height - 1 + 0) / 2.0), oriLocation.X * resizerate / resRat - ((float)(pictureBoxPartCalibration.Width - 1 + 0) / 2.0), oriLocation.Y * resizerate / resRat + ((float)(pictureBoxPartCalibration.Height - 1 + 0) / 2.0), oriLocation.X * resizerate / resRat + ((float)(pictureBoxPartCalibration.Width - 1 + 0) / 2.0));
                        basicClass.reduceDomain(imgPart, rectDomain, out temp);
                        if (imgPartTemp != null)
                        {
                            imgPartTemp.Dispose();
                        }
                        HOperatorSet.CropDomain(temp, out imgPartTemp);
                        HOperatorSet.DispObj(imgPartTemp, CalibrationPartHWHandle);
                        temp.Dispose();
                    }
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void buttonGetImage_Click(object sender, EventArgs e)
        {
            try
            {
                HObject imageCopy;
                Svision.GetMe().baslerCamera.getFrameImageWithStart(out imageCopy);
                if (calibrationIsRadialDistortionFlag)
                {
                    HOperatorSet.MapImage(imageCopy, ho_MapFixed, out imageCopy);
                }
                basicClass.getImageSize(imageCopy, out rowNum, out columnNum);
                if (rowNum == rowOriNum && columnNum == columnOriNum)
                {
                    if (image != null)
                    {
                        image.Dispose();
                    }
                    HOperatorSet.CopyImage(imageCopy, out image);
                    HTuple channelOfImage;
                    HOperatorSet.CountChannels(image, out channelOfImage);
                    if (channelOfImage == 1)
                    {
                        HObject image1, image2;
                        HOperatorSet.CopyImage(image, out image1);
                        HOperatorSet.CopyImage(image, out image2);
                        if (imageTemp != null)
                        {
                            imageTemp.Dispose();
                        }
                        HOperatorSet.Compose3(image, image1, image2, out imageTemp);
                        if (imageTempShow != null)
                        {
                            imageTempShow.Dispose();
                        }
                        HOperatorSet.CopyImage(imageTemp, out imageTempShow);
                        image1.Dispose();
                        image2.Dispose();
                    }
                    else
                    {
                        if (imageTemp != null)
                        {
                            imageTemp.Dispose();
                        }
                        HOperatorSet.CopyImage(image, out imageTemp);
                        if (imageTempShow != null)
                        {
                            imageTempShow.Dispose();
                        }
                        HOperatorSet.CopyImage(imageTemp, out imageTempShow);
                    }
                    if (img != null)
                    {
                        img.Dispose();
                    }
                    basicClass.resizeImage(imageTemp, out img, resRat);
                    basicClass.displayhobject(img, CalibrationHWHandle);
                    if (imgPart != null)
                    {
                        imgPart.Dispose();
                    }
                    basicClass.resizeImage(imageTemp, out imgPart, resizerate);
                    HObject temp;
                    basicClass.genRectangle1(out rectDomain, 0, 0, (pictureBoxPartCalibration.Height - 1), (pictureBoxPartCalibration.Width - 1));
                    basicClass.reduceDomain(imgPart, rectDomain, out temp);
                    centerLocation.Y = (float)(((float)(System.Math.Min(pictureBoxPartCalibration.Height, rowNum * resizerate) - 1 + 0) / 2.0) * resRat / resizerate);
                    centerLocation.X = (float)(((float)(System.Math.Min(pictureBoxPartCalibration.Width, columnNum * resizerate) - 1 + 0) / 2.0) * resRat / resizerate);
                    if (imgPartTemp != null)
                    {
                        imgPartTemp.Dispose();
                    }
                    HOperatorSet.CropDomain(temp, out imgPartTemp);
                    HOperatorSet.DispObj(imgPartTemp, CalibrationPartHWHandle);
                    oriLocation.X = (int)centerLocation.X;
                    oriLocation.Y = (int)centerLocation.Y;
                    temp.Dispose();
                }
                else
                {
                    MessageBox.Show("原始图像行和列不可更改！");
                }

                imageCopy.Dispose();
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
        private void partImageWindowShow(Point oriLocation)
        {

            if (centerLocation.X >= 0 && centerLocation.X <= columnNum * resRat && centerLocation.Y >= 0 && centerLocation.Y <= rowNum * resRat)
            {
                try
                {
                    centerLocation.Y = (float)oriLocation.Y;
                    centerLocation.X = (float)oriLocation.X;
                    if (oriLocation.Y * resizerate / resRat - ((float)(pictureBoxPartCalibration.Height - 1 + 0) / 2.0) < 0 && oriLocation.X * resizerate / resRat - ((float)(pictureBoxPartCalibration.Width - 1 + 0) / 2.0) < 0)
                    {
                        HObject temp;
                        basicClass.genRectangle1(out rectDomain, 0, 0, (pictureBoxPartCalibration.Height - 1), (pictureBoxPartCalibration.Width - 1));
                        centerLocation.Y = (float)(((float)(System.Math.Min(pictureBoxPartCalibration.Height, rowNum * resizerate) - 1 + 0) / 2.0) * resRat / resizerate);
                        centerLocation.X = (float)(((float)(System.Math.Min(pictureBoxPartCalibration.Width, columnNum * resizerate) - 1 + 0) / 2.0) * resRat / resizerate);

                        basicClass.reduceDomain(imgPart, rectDomain, out temp);
                        if (imgPartTemp != null)
                        {
                            imgPartTemp.Dispose();
                        }
                        HOperatorSet.CropDomain(temp, out imgPartTemp);
                        HOperatorSet.DispObj(imgPartTemp, CalibrationPartHWHandle);
                        temp.Dispose();
                    }
                    else if (oriLocation.Y * resizerate / resRat - ((float)(pictureBoxPartCalibration.Height - 1 + 0) / 2.0) <= 0 && oriLocation.X * resizerate / resRat - ((float)(pictureBoxPartCalibration.Width - 1 + 0) / 2.0) >= 0 && oriLocation.X * resizerate / resRat + ((float)(pictureBoxPartCalibration.Width - 1 + 0) / 2.0) < columnNum * resizerate - 1)
                    {
                        HObject temp;
                        basicClass.genRectangle1(out rectDomain, 0, oriLocation.X * resizerate / resRat - ((float)(pictureBoxPartCalibration.Width - 1 + 0) / 2.0), (pictureBoxPartCalibration.Height - 1), oriLocation.X * resizerate / resRat + ((float)(pictureBoxPartCalibration.Width - 1 + 0) / 2.0));
                        centerLocation.Y = (float)(((float)(System.Math.Min(pictureBoxPartCalibration.Height, rowNum * resizerate) - 1 + 0) / 2.0) * resRat / resizerate);
                        centerLocation.X = (float)oriLocation.X;
                        basicClass.reduceDomain(imgPart, rectDomain, out temp);
                        if (imgPartTemp != null)
                        {
                            imgPartTemp.Dispose();
                        }
                        HOperatorSet.CropDomain(temp, out imgPartTemp);
                        HOperatorSet.DispObj(imgPartTemp, CalibrationPartHWHandle);
                        temp.Dispose();
                    }
                    else if (oriLocation.Y * resizerate / resRat - ((float)(pictureBoxPartCalibration.Height - 1 + 0) / 2.0) > 0 && oriLocation.Y * resizerate / resRat + ((float)(pictureBoxPartCalibration.Height - 1 + 0) / 2.0) <= rowNum * resizerate - 1 && oriLocation.X * resizerate / resRat - ((float)(pictureBoxPartCalibration.Width - 1 + 0) / 2.0) < 0)
                    {
                        HObject temp;
                        basicClass.genRectangle1(out rectDomain, oriLocation.Y * resizerate / resRat - ((float)(pictureBoxPartCalibration.Height - 1 + 0) / 2.0), 0, oriLocation.Y * resizerate / resRat + ((float)(pictureBoxPartCalibration.Height - 1 + 0) / 2.0), (pictureBoxPartCalibration.Width - 1));
                        centerLocation.Y = (float)oriLocation.Y;
                        centerLocation.X = (float)(((float)(System.Math.Min(pictureBoxPartCalibration.Width, columnNum * resizerate) - 1 + 0) / 2.0) * resRat / resizerate);
                        basicClass.reduceDomain(imgPart, rectDomain, out temp);
                        if (imgPartTemp != null)
                        {
                            imgPartTemp.Dispose();
                        }
                        HOperatorSet.CropDomain(temp, out imgPartTemp);
                        HOperatorSet.DispObj(imgPartTemp, CalibrationPartHWHandle);
                        temp.Dispose();
                    }
                    else if (oriLocation.Y * resizerate / resRat + ((float)(pictureBoxPartCalibration.Height - 1 + 0) / 2.0) > rowNum * resizerate - 1 && oriLocation.X * resizerate / resRat - ((float)(pictureBoxPartCalibration.Width - 1 + 0) / 2.0) < 0)
                    {
                        HObject temp;
                        basicClass.genRectangle1(out rectDomain, rowNum * resizerate - 1 - (pictureBoxPartCalibration.Height - 1), 0, rowNum * resizerate - 1, (pictureBoxPartCalibration.Width - 1));
                        centerLocation.Y = (float)((rowNum * resizerate - ((float)(System.Math.Min(pictureBoxPartCalibration.Height, rowNum * resizerate) - 1 + 0) / 2.0) - 1) * resRat / resizerate);
                        centerLocation.X = (float)(((float)(System.Math.Min(pictureBoxPartCalibration.Width, columnNum * resizerate) - 1 + 0) / 2.0) * resRat / resizerate);
                        basicClass.reduceDomain(imgPart, rectDomain, out temp);
                        if (imgPartTemp != null)
                        {
                            imgPartTemp.Dispose();
                        }
                        HOperatorSet.CropDomain(temp, out imgPartTemp);
                        HOperatorSet.DispObj(imgPartTemp, CalibrationPartHWHandle);
                        temp.Dispose();
                    }
                    else if (oriLocation.Y * resizerate / resRat + ((float)(pictureBoxPartCalibration.Height - 1 + 0) / 2.0) > rowNum * resizerate - 1 && oriLocation.X * resizerate / resRat - ((float)(pictureBoxPartCalibration.Width - 1 + 0) / 2.0) > 0 && oriLocation.X * resizerate / resRat + ((float)(pictureBoxPartCalibration.Width - 1 + 0) / 2.0) < columnNum * resizerate - 1)
                    {
                        HObject temp;
                        basicClass.genRectangle1(out rectDomain, rowNum * resizerate - (pictureBoxPartCalibration.Height), oriLocation.X * resizerate / resRat - ((float)(pictureBoxPartCalibration.Width - 1 + 0) / 2.0), rowNum * resizerate - 1, oriLocation.X * resizerate / resRat + ((float)(pictureBoxPartCalibration.Width - 1 + 0) / 2.0));
                        centerLocation.Y = (float)((rowNum * resizerate - ((float)(System.Math.Min(pictureBoxPartCalibration.Height, rowNum * resizerate) - 1 + 0) / 2.0 + 1)) * resRat / resizerate);
                        centerLocation.X = (float)oriLocation.X;
                        basicClass.reduceDomain(imgPart, rectDomain, out temp);
                        if (imgPartTemp != null)
                        {
                            imgPartTemp.Dispose();
                        }
                        HOperatorSet.CropDomain(temp, out imgPartTemp);
                        HOperatorSet.DispObj(imgPartTemp, CalibrationPartHWHandle);
                        temp.Dispose();
                    }
                    else if (oriLocation.Y * resizerate / resRat + ((float)(pictureBoxPartCalibration.Height - 1 + 0) / 2.0) >= rowNum * resizerate - 1 && oriLocation.X * resizerate / resRat + ((float)(pictureBoxPartCalibration.Width - 1 + 0) / 2.0) >= columnNum * resizerate - 1)
                    {
                        HObject temp;
                        basicClass.genRectangle1(out rectDomain, rowNum * resizerate - (pictureBoxPartCalibration.Height), columnNum * resizerate - (pictureBoxPartCalibration.Width), rowNum * resizerate - 1, columnNum * resizerate - 1);
                        centerLocation.Y = (float)((rowNum * resizerate - ((float)(System.Math.Min(pictureBoxPartCalibration.Height, rowNum * resizerate) - 1 + 0) / 2.0 + 1)) * resRat / resizerate);
                        centerLocation.X = (float)((columnNum * resizerate - ((float)(System.Math.Min(pictureBoxPartCalibration.Width, columnNum * resizerate) - 1 + 0) / 2.0 + 1)) * resRat / resizerate);
                        basicClass.reduceDomain(imgPart, rectDomain, out temp);
                        if (imgPartTemp != null)
                        {
                            imgPartTemp.Dispose();
                        }
                        HOperatorSet.CropDomain(temp, out imgPartTemp);
                        HOperatorSet.DispObj(imgPartTemp, CalibrationPartHWHandle);
                        temp.Dispose();
                    }
                    else if (oriLocation.Y * resizerate / resRat + ((float)(pictureBoxPartCalibration.Height - 1 + 0) / 2.0) < rowNum * resizerate - 1 && oriLocation.Y * resizerate / resRat - ((float)(pictureBoxPartCalibration.Height - 1 + 0) / 2.0) > 0 && oriLocation.X * resizerate / resRat + ((float)(pictureBoxPartCalibration.Width - 1 + 0) / 2.0) > columnNum * resizerate - 1)
                    {
                        HObject temp;
                        basicClass.genRectangle1(out rectDomain, oriLocation.Y * resizerate / resRat - ((float)(pictureBoxPartCalibration.Height - 1 + 0) / 2.0), columnNum * resizerate - (pictureBoxPartCalibration.Width), oriLocation.Y * resizerate / resRat + ((float)(pictureBoxPartCalibration.Height - 1 + 0) / 2.0), columnNum * resizerate - 1);
                        centerLocation.Y = (float)oriLocation.Y;
                        centerLocation.X = (float)((columnNum * resizerate - ((float)(System.Math.Min(pictureBoxPartCalibration.Width, columnNum * resizerate) - 1 + 0) / 2.0 + 1)) * resRat / resizerate);
                        basicClass.reduceDomain(imgPart, rectDomain, out temp);
                        if (imgPartTemp != null)
                        {
                            imgPartTemp.Dispose();
                        }
                        HOperatorSet.CropDomain(temp, out imgPartTemp);
                        HOperatorSet.DispObj(imgPartTemp, CalibrationPartHWHandle);
                        temp.Dispose();
                    }
                    else if (oriLocation.Y * resizerate / resRat - ((float)(pictureBoxPartCalibration.Height - 1 + 0) / 2.0) <= 0 && oriLocation.X * resizerate / resRat + ((float)(pictureBoxPartCalibration.Width - 1 + 0) / 2.0) >= columnNum * resizerate - 1)
                    {
                        HObject temp;
                        basicClass.genRectangle1(out rectDomain, 0, columnNum * resizerate - (pictureBoxPartCalibration.Width), (pictureBoxPartCalibration.Height - 1), columnNum * resizerate - 1);
                        centerLocation.Y = (float)(((float)(System.Math.Min(pictureBoxPartCalibration.Height, rowNum * resizerate) - 1 + 0) / 2.0) * resRat / resizerate);
                        centerLocation.X = (float)((columnNum * resizerate - ((float)(System.Math.Min(pictureBoxPartCalibration.Width, columnNum * resizerate) - 1 + 0) / 2.0 + 1)) * resRat / resizerate);
                        basicClass.reduceDomain(imgPart, rectDomain, out temp);
                        if (imgPartTemp != null)
                        {
                            imgPartTemp.Dispose();
                        }
                        HOperatorSet.CropDomain(temp, out imgPartTemp);
                        HOperatorSet.DispObj(imgPartTemp, CalibrationPartHWHandle);
                        temp.Dispose();
                    }
                    else
                    {
                        HObject temp;
                        basicClass.genRectangle1(out rectDomain, oriLocation.Y * resizerate / resRat - ((float)(pictureBoxPartCalibration.Height - 1 + 0) / 2.0), oriLocation.X * resizerate / resRat - ((float)(pictureBoxPartCalibration.Width - 1 + 0) / 2.0), oriLocation.Y * resizerate / resRat + ((float)(pictureBoxPartCalibration.Height - 1 + 0) / 2.0), oriLocation.X * resizerate / resRat + ((float)(pictureBoxPartCalibration.Width - 1 + 0) / 2.0));
                        basicClass.reduceDomain(imgPart, rectDomain, out temp);
                        if (imgPartTemp != null)
                        {
                            imgPartTemp.Dispose();
                        }
                        HOperatorSet.CropDomain(temp, out imgPartTemp);
                        HOperatorSet.DispObj(imgPartTemp, CalibrationPartHWHandle);
                        temp.Dispose();
                    }
                    this.pictureBoxCalibration.Focus();
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }


        }

        private void pictureBoxCalibrationSizeChanged(object sender, EventArgs e)
        {
            if (CalibrationPartHWHandle != null)
            {
                HOperatorSet.SetWindowExtents(CalibrationPartHWHandle, 0, 0, (pictureBoxPartCalibration.Width), (pictureBoxPartCalibration.Height));
                HOperatorSet.SetPart(CalibrationPartHWHandle, 0, 0, (pictureBoxPartCalibration.Height - 1), (pictureBoxPartCalibration.Width - 1));
                if (imgPart != null)
                {
                    partImageWindowShow(oriLocation);
                }
            }

        }

        private void buttonSelectPoint_Click(object sender, EventArgs e)
        {
            if (image != null && imgPartTemp != null)
            {
                panelbutton.Enabled = false;
                groupBoxPoint.Enabled = false;
                groupBoxThreePoint.Enabled = false;
                pictureBoxCalibration.Enabled = false;
                this.ControlBox = false;
                HTuple pointDisplayX, pointDisplayY, radiuss;
                this.pictureBoxPartCalibration.Focus();
                HOperatorSet.SetColor(CalibrationPartHWHandle, "green");
                HOperatorSet.SetLineWidth(CalibrationPartHWHandle, 4);
                HOperatorSet.DrawCircle(CalibrationPartHWHandle, out pointDisplayY, out pointDisplayX, out radiuss);
                //HOperatorSet.DrawPoint(CalibrationPartHWHandle, out pointDisplayY, out pointDisplayX);
                HTuple hvHomMat2D;
                HOperatorSet.HomMat2dIdentity(out hvHomMat2D);
                HOperatorSet.VectorAngleToRigid(0, 0, 0, centerLocation.Y / resRat * resizerate + (-((float)(System.Math.Min(pictureBoxPartCalibration.Height, rowNum * resizerate) - 1 + 0) / 2.0)), centerLocation.X / resRat * resizerate + (-((float)(System.Math.Min(pictureBoxPartCalibration.Width, columnNum * resizerate) - 1 + 0) / 2.0)), 0, out hvHomMat2D);
                HOperatorSet.HomMat2dScale(hvHomMat2D, 1.0 / resizerate, 1.0 / resizerate, 0, 0, out hvHomMat2D);
                HOperatorSet.AffineTransPixel(hvHomMat2D, pointDisplayY, pointDisplayX, out regionCenterRow, out regionCenterColumn);
                HTuple hv_grayval = new HTuple();
                hv_grayval[0] = 255;
                hv_grayval[1] = 0;
                hv_grayval[2] = 0;
                HObject regionLineh, regionLinev, regionCross;
                HOperatorSet.GenRectangle1(out regionLineh, regionCenterRow - 3, regionCenterColumn - 20, regionCenterRow + 3, regionCenterColumn + 20);
                HOperatorSet.GenRectangle1(out regionLinev, regionCenterRow - 20, regionCenterColumn - 3, regionCenterRow + 20, regionCenterColumn + 3);
                HOperatorSet.Union2(regionLineh, regionLinev, out regionCross);
                //modelPoint.Y = (float)regionCenterRow[0].D;
                //modelPoint.X = (float)regionCenterColumn[0].D;
                if (imageTempShow != null)
                {
                    imageTempShow.Dispose();
                }
                HOperatorSet.PaintRegion(regionCross, imageTemp, out imageTempShow, hv_grayval, "fill");
                if (imgPart != null)
                {
                    imgPart.Dispose();
                }
                basicClass.resizeImage(imageTempShow, out imgPart, resizerate);
                if (img != null)
                {
                    img.Dispose();
                }
                basicClass.resizeImage(imageTempShow, out img, resRat);
                basicClass.displayhobject(img, CalibrationHWHandle);
                partImageWindowShow(oriLocation);
                pictureBoxCalibration.Enabled = true;
                this.ControlBox = true;
                numericUpDownPointX.Value = (decimal)regionCenterColumn[0].D;
                numericUpDownPointY.Value = (decimal)regionCenterRow[0].D;
                panelbutton.Enabled = true;
                groupBoxPoint.Enabled = true;
                groupBoxThreePoint.Enabled = true;
            }
        }

        private void numericUpDownPointX_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                pictureBoxCalibration.Enabled = false;
                HOperatorSet.SetColor(CalibrationPartHWHandle, "red");
                HOperatorSet.SetLineWidth(CalibrationPartHWHandle, 4);
                regionCenterColumn = ((double)numericUpDownPointX.Value);
                //modelPoint.X = (float)regionCenterColumn[0].D;
                HTuple hv_grayval = new HTuple();
                hv_grayval[0] = 255;
                hv_grayval[1] = 0;
                hv_grayval[2] = 0;
                HObject regionLineh, regionLinev, regionCross;
                HOperatorSet.GenRectangle1(out regionLineh, regionCenterRow - 3, regionCenterColumn - 20, regionCenterRow + 3, regionCenterColumn + 20);
                HOperatorSet.GenRectangle1(out regionLinev, regionCenterRow - 20, regionCenterColumn - 3, regionCenterRow + 20, regionCenterColumn + 3);
                HOperatorSet.Union2(regionLineh, regionLinev, out regionCross);
                //modelPoint.Y = (float)regionCenterRow[0].D;
                //modelPoint.X = (float)regionCenterColumn[0].D;
                if (imageTempShow != null)
                {
                    imageTempShow.Dispose();
                }
                HOperatorSet.PaintRegion(regionCross, imageTemp, out imageTempShow, hv_grayval, "fill");
                if (imgPart != null)
                {
                    imgPart.Dispose();
                }
                basicClass.resizeImage(imageTempShow, out imgPart, resizerate);
                if (img != null)
                {
                    img.Dispose();
                }
                basicClass.resizeImage(imageTempShow, out img, resRat);
                basicClass.displayhobject(img, CalibrationHWHandle);
                partImageWindowShow(oriLocation);
                pictureBoxCalibration.Enabled = true;
            }

            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }

        private void numericUpDownPointY_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                pictureBoxCalibration.Enabled = false;
                HOperatorSet.SetColor(CalibrationPartHWHandle, "red");
                HOperatorSet.SetLineWidth(CalibrationPartHWHandle, 4);
                regionCenterRow[0].D = ((double)numericUpDownPointY.Value);
                //modelPoint.Y = (float)regionCenterRow[0].D;
                HTuple hv_grayval = new HTuple();
                hv_grayval[0] = 255;
                hv_grayval[1] = 0;
                hv_grayval[2] = 0;
                HObject regionLineh, regionLinev, regionCross;
                HOperatorSet.GenRectangle1(out regionLineh, regionCenterRow - 3, regionCenterColumn - 20, regionCenterRow + 3, regionCenterColumn + 20);
                HOperatorSet.GenRectangle1(out regionLinev, regionCenterRow - 20, regionCenterColumn - 3, regionCenterRow + 20, regionCenterColumn + 3);
                HOperatorSet.Union2(regionLineh, regionLinev, out regionCross);
                //modelPoint.Y = (float)regionCenterRow[0].D;
                //modelPoint.X = (float)regionCenterColumn[0].D;
                if (imageTempShow != null)
                {
                    imageTempShow.Dispose();
                }
                HOperatorSet.PaintRegion(regionCross, imageTemp, out imageTempShow, hv_grayval, "fill");
                if (imgPart != null)
                {
                    imgPart.Dispose();
                }
                basicClass.resizeImage(imageTempShow, out imgPart, resizerate);
                if (img != null)
                {
                    img.Dispose();
                }
                basicClass.resizeImage(imageTempShow, out img, resRat);
                basicClass.displayhobject(img, CalibrationHWHandle);
                partImageWindowShow(oriLocation);
                pictureBoxCalibration.Enabled = true;
            }

            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }

        private void checkBoxRadialDistortion_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (checkBoxRadialDistortion.Checked == true)
                {
                    panelRadialDistortion.Enabled = true;
                    panelbutton.Enabled = false;
                    groupBoxPoint.Enabled = false;
                    groupBoxThreePoint.Enabled = false;

                    if (calibrationParaConfirmFlag)
                    {
                        buttonCalibrationBoardConfirm.Text = "修改";
                        numericUpDownCalibrationDistance.Enabled = false;
                        numericUpDownCalibrationPoint.Enabled = false;
                        numericUpDownCalibrationRate.Enabled = false;
                        numericUpDownFocus.Enabled = false;
                        numericUpDownPixelSize.Enabled = false;
                        checkBoxOpposite.Enabled = false;
                    } 
                    else
                    {
                        groupBoxCalibrationBoard.Enabled = true;
                        numericUpDownCalibrationDistance.Enabled = true;
                        numericUpDownCalibrationPoint.Enabled = true;
                        numericUpDownCalibrationRate.Enabled = true;
                        numericUpDownFocus.Enabled = true;
                        numericUpDownPixelSize.Enabled = true;
                        checkBoxOpposite.Enabled = true;
                        buttonCalibrationBoardConfirm.Text = "确定";
                    }
                    if (calibrationIsRadialDistortionFlag)
                    {
                        groupBoxCalibrationBoard.Enabled = false;
                        buttonGetImageChangePara.Enabled = false;
                        buttonCalibrationConfirm.Enabled = true;
                        textBoxNum.Enabled = false;
                        textBoxNum.Text = " ";
                        buttonCalibrationConfirm.Text = "重新矫正畸变";
                        
                    } 
                    else
                    {
                        buttonGetImageChangePara.Enabled = false;
                        buttonCalibrationConfirm.Enabled = false;
                        if (hv_CalibDataID != null)
                        {
                            HOperatorSet.ClearCalibData(hv_CalibDataID);
                            hv_CalibDataID = null;
                        }
                        hv_I = 1;
                        textBoxNum.Enabled = false;
                        textBoxNum.Text = " ";
                        buttonCalibrationConfirm.Text = "矫正畸变完毕";
                    }

                }
                else
                {
                    buttonGetImageChangePara.Enabled = false;
                    buttonCalibrationConfirm.Enabled = false;

                    numericUpDownCalibrationDistance.Enabled = true;
                    numericUpDownCalibrationPoint.Enabled = true;
                    numericUpDownCalibrationRate.Enabled = true;
                    numericUpDownFocus.Enabled = true;
                    numericUpDownPixelSize.Enabled = true;
 
                    buttonCalibrationBoardConfirm.Text = "确定";
                    calibrationParaConfirmFlag = false;
                    groupBoxCalibrationBoard.Enabled = true;

                    if (hv_CalibDataID != null)
                    {
                        HOperatorSet.ClearCalibData(hv_CalibDataID);
                        hv_CalibDataID = null;
                    }
                    hv_I = 1;
                    textBoxNum.Enabled = false;
                    textBoxNum.Text = " ";
                    buttonCalibrationConfirm.Text = "矫正畸变完毕";
                    panelRadialDistortion.Enabled = false;
                    panelbutton.Enabled = true;
                    groupBoxPoint.Enabled = true;
                    groupBoxThreePoint.Enabled = true;
                    calibrationIsRadialDistortionFlag = false;
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonCalibrationBoardConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                if (calibrationParaConfirmFlag == false)
                {
                    hv_CalibDataID = null;
                    HOperatorSet.GenCaltab((int)numericUpDownCalibrationPoint.Value, (int)numericUpDownCalibrationPoint.Value, (float)numericUpDownCalibrationDistance.Value,
                        (float)numericUpDownCalibrationRate.Value, System.Environment.CurrentDirectory + "/CalibrationBroad.descr", System.Environment.CurrentDirectory + "/CalibrationBroad.ps");
                    HOperatorSet.CreateCalibData("calibration_object", 1, 1, out hv_CalibDataID);
                    HOperatorSet.SetCalibDataCamParam(hv_CalibDataID, 0, "area_scan_division", ((((((
                        (new HTuple(0.001 * (int)numericUpDownFocus.Value)).TupleConcat(0)).TupleConcat(0.000001 * (float)numericUpDownPixelSize.Value)).TupleConcat(0.000001 * (float)numericUpDownPixelSize.Value))
                        .TupleConcat((int)(columnOriNum * 0.5))).TupleConcat((int)(rowOriNum * 0.5))).TupleConcat(columnOriNum)).TupleConcat(rowOriNum));
                    HOperatorSet.SetCalibDataCalibObject(hv_CalibDataID, 0, System.Environment.CurrentDirectory + "/CalibrationBroad.descr");
                    buttonGetImageChangePara.Enabled = true;
                    buttonCalibrationConfirm.Enabled = true;
                    hv_I = 1;
                    textBoxNum.Text = " ";
                    calibrationParaConfirmFlag = true;
                    numericUpDownCalibrationDistance.Enabled = false;
                    numericUpDownCalibrationPoint.Enabled = false;
                    numericUpDownCalibrationRate.Enabled = false;
                    numericUpDownFocus.Enabled = false;
                    numericUpDownPixelSize.Enabled = false;
                    checkBoxOpposite.Enabled = false;
                    buttonCalibrationBoardConfirm.Text = "修改";
                } 
                else
                {
                    buttonGetImageChangePara.Enabled = false;
                    buttonCalibrationConfirm.Enabled = false;
                    calibrationParaConfirmFlag = false;
                    numericUpDownCalibrationDistance.Enabled = true;
                    numericUpDownCalibrationPoint.Enabled = true;
                    numericUpDownCalibrationRate.Enabled = true;
                    numericUpDownFocus.Enabled = true;
                    numericUpDownPixelSize.Enabled = true;
                    
                    checkBoxOpposite.Enabled = true;
                    buttonCalibrationBoardConfirm.Text = "确定";
                }
                
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void buttonGetImageChangePara_Click(object sender, EventArgs e)
        {
            try
            {
                HObject imageCopy;
                Svision.GetMe().baslerCamera.getFrameImageWithStart(out imageCopy);

                basicClass.getImageSize(imageCopy, out rowNum, out columnNum);
                if (rowNum == rowOriNum && columnNum == columnOriNum)
                {
                    if (image != null)
                    {
                        image.Dispose();
                    }
                    HOperatorSet.CopyImage(imageCopy, out image);
                    if (checkBoxOpposite.Checked == true)
                    {
                        HOperatorSet.InvertImage(image, out image);
                    }
                    HTuple channelOfImage;
                    HOperatorSet.CountChannels(image, out channelOfImage);
                    if (channelOfImage == 1)
                    {
                        HObject image1, image2;
                        HOperatorSet.CopyImage(image, out image1);
                        HOperatorSet.CopyImage(image, out image2);
                        if (imageTemp != null)
                        {
                            imageTemp.Dispose();
                        }
                        HOperatorSet.Compose3(image, image1, image2, out imageTemp);
                        if (imageTempShow != null)
                        {
                            imageTempShow.Dispose();
                        }
                        HOperatorSet.CopyImage(imageTemp, out imageTempShow);
                        image1.Dispose();
                        image2.Dispose();
                    }
                    else
                    {
                        if (imageTemp != null)
                        {
                            imageTemp.Dispose();
                        }
                        HOperatorSet.CopyImage(image, out imageTemp);
                        if (imageTempShow != null)
                        {
                            imageTempShow.Dispose();
                        }
                        HOperatorSet.CopyImage(imageTemp, out imageTempShow);
                    }
                    
                }
                else
                {
                    MessageBox.Show("原始图像行和列不可更改！");
                }

                imageCopy.Dispose();

                
                //Find the calibration table
                HOperatorSet.FindCalibObject(image, hv_CalibDataID, 0, 0, hv_I, new HTuple(), new HTuple());
                if (ho_Caltab!=null)
                {
                    ho_Caltab.Dispose();
                }
                
                HOperatorSet.GetCalibDataObservContours(out ho_Caltab, hv_CalibDataID, "caltab",0, 0, hv_I);
                HTuple hv_Row, hv_Column, hv_Index, hv_Pose;
                HOperatorSet.GetCalibDataObservPoints(hv_CalibDataID, 0, 0, hv_I, out hv_Row,
                    out hv_Column, out hv_Index, out hv_Pose);
                if (ho_Circles!=null)
                {
                    ho_Circles.Dispose();
                }
                
                HOperatorSet.GenCircle(out ho_Circles, hv_Row, hv_Column, HTuple.TupleGenConst(
                    new HTuple(hv_Row.TupleLength()), 2.0));

                HTuple hv_grayval = new HTuple();
                HTuple hv_grayvalp = new HTuple();
                hv_grayvalp[0] = 255;
                hv_grayvalp[1] = 0;
                hv_grayvalp[2] = 0;
                hv_grayval[0] = 0;
                hv_grayval[1] = 255;
                hv_grayval[2] = 0;
                HObject temp,ho_Caltab_Region;
                HOperatorSet.GenRegionContourXld(ho_Caltab, out ho_Caltab_Region, "filled");
                HOperatorSet.PaintRegion(ho_Caltab_Region, imageTemp, out temp, hv_grayvalp, "margin");
                ho_Caltab_Region.Dispose();
                if (imageTempShow != null)
                {
                    imageTempShow.Dispose();
                }
                HOperatorSet.PaintRegion(ho_Circles, temp, out imageTempShow, hv_grayval, "margin");
                if (imgPart != null)
                {
                    imgPart.Dispose();
                }
                basicClass.resizeImage(imageTempShow, out imgPart, resizerate);
                if (img != null)
                {
                    img.Dispose();
                }
                basicClass.resizeImage(imageTempShow, out img, resRat);
                basicClass.displayhobject(img, CalibrationHWHandle);
                partImageWindowShow(oriLocation);
                temp.Dispose();
                textBoxNum.Text = hv_I[0].I.ToString();
                hv_I = hv_I + 1;
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

        private void buttonCalibrationConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                if (calibrationIsRadialDistortionFlag)
                {
                    if (hv_CalibDataID!=null)
                    {
                        HOperatorSet.ClearCalibData(hv_CalibDataID);
                    }
                    
                    panelbutton.Enabled = false;
                    groupBoxPoint.Enabled = false;
                    groupBoxThreePoint.Enabled = false;
                    groupBoxCalibrationBoard.Enabled = true;
                    buttonGetImageChangePara.Enabled = false;
                    hv_I = 1;
                    textBoxNum.Enabled = false;
                    textBoxNum.Text = " ";
                    calibrationIsRadialDistortionFlag = false;
                    buttonCalibrationConfirm.Enabled = false;
                    buttonCalibrationConfirm.Text = "矫正畸变完毕";
                } 
                else
                {
                    HTuple hv_Errors, hv_CamParVirtualFixed, hv_CamParam;
                    HOperatorSet.CalibrateCameras(hv_CalibDataID, out hv_Errors);
                    HOperatorSet.GetCalibData(hv_CalibDataID, "camera", 0, "params", out hv_CamParam);
                    //Alternatively, the operator change_radial_distortion_cam_par can be used
                    HOperatorSet.ChangeRadialDistortionCamPar("fixed", hv_CamParam, 0, out hv_CamParVirtualFixed);
                    if (ho_MapFixed != null)
                    {
                        ho_MapFixed.Dispose();
                    }

                    HOperatorSet.GenRadialDistortionMap(out ho_MapFixed, hv_CamParam, hv_CamParVirtualFixed,
                        "bilinear");
                    panelbutton.Enabled = true;
                    groupBoxPoint.Enabled = true;
                    groupBoxThreePoint.Enabled = true;
                    groupBoxCalibrationBoard.Enabled = false;
                    buttonGetImageChangePara.Enabled = false;
                    textBoxNum.Enabled = false;
                    calibrationIsRadialDistortionFlag = true;
                    buttonCalibrationConfirm.Enabled = true;
                    buttonCalibrationConfirm.Text = "重新矫正畸变";
                }

            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                //if (checkBoxRadialDistortion.Checked == true && calibrationIsRadialDistortionFlag == false)
                //{
                //    throw new Exception("请完成畸变校正过程，再确认参数！")
                //} 
                //else
                //{
                if (ho_MapFixed != null && ho_MapFixed.IsInitialized())
                {
                    HOperatorSet.CopyObj(ho_MapFixed, out ConfigInformation.GetInstance().tCalCfg.ho_MapFixed, 1, -1);
                }
                if (mx != null && checkBoxUseThreePointCalibration.Checked == true)
                {
                    ConfigInformation.GetInstance().tCalCfg.mx = mx.Clone();
                    ConfigInformation.GetInstance().tCalCfg.useThreePointCalibration = true;
                    ConfigInformation.GetInstance().tCalCfg.p1x = (double)numericUpDownPP1X.Value;
                    ConfigInformation.GetInstance().tCalCfg.p2x = (double)numericUpDownPP2X.Value;
                    ConfigInformation.GetInstance().tCalCfg.p3x = (double)numericUpDownPP3X.Value;
                    ConfigInformation.GetInstance().tCalCfg.r1x = (double)numericUpDownPR1X.Value;
                    ConfigInformation.GetInstance().tCalCfg.r2x = (double)numericUpDownPR2X.Value;
                    ConfigInformation.GetInstance().tCalCfg.r3x = (double)numericUpDownPR3X.Value;
                    ConfigInformation.GetInstance().tCalCfg.p1y = (double)numericUpDownPP1Y.Value;
                    ConfigInformation.GetInstance().tCalCfg.p2y = (double)numericUpDownPP2Y.Value;
                    ConfigInformation.GetInstance().tCalCfg.p3y = (double)numericUpDownPP3Y.Value;
                    ConfigInformation.GetInstance().tCalCfg.r1y = (double)numericUpDownPR1Y.Value;
                    ConfigInformation.GetInstance().tCalCfg.r2y = (double)numericUpDownPR2Y.Value;
                    ConfigInformation.GetInstance().tCalCfg.r3y = (double)numericUpDownPR3Y.Value;
                    ConfigInformation.GetInstance().tCalCfg.Sx = Sx;
                    ConfigInformation.GetInstance().tCalCfg.Sy = Sy;
                    ConfigInformation.GetInstance().tCalCfg.thea = thea;
                    ConfigInformation.GetInstance().tCalCfg.Tx = Tx;
                    ConfigInformation.GetInstance().tCalCfg.Ty = Ty;
                }
                ConfigInformation.GetInstance().tCalCfg.calibrationDistance = (double)numericUpDownCalibrationDistance.Value;
                ConfigInformation.GetInstance().tCalCfg.calibrationPoint = (int)numericUpDownCalibrationPoint.Value;
                ConfigInformation.GetInstance().tCalCfg.focus = (int)numericUpDownFocus.Value;
                ConfigInformation.GetInstance().tCalCfg.calibrationRate = (double)numericUpDownCalibrationRate.Value;
                ConfigInformation.GetInstance().tCalCfg.pixelSize = (double)numericUpDownPixelSize.Value;
                ConfigInformation.GetInstance().tCalCfg.calibrationParaConfirmFlag = calibrationParaConfirmFlag;
                ConfigInformation.GetInstance().tCalCfg.calibrationIsRadialDistortionFlag = calibrationIsRadialDistortionFlag;
                if (checkBoxRadialDistortion.Checked)
                {
                    ConfigInformation.GetInstance().tCalCfg.isCheckBoxRadialDistortion = true;
                }
                else
                {
                    ConfigInformation.GetInstance().tCalCfg.isCheckBoxRadialDistortion = false;
                }
                this.Close();
                //}
               
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonTranslatePointOK_Click(object sender, EventArgs e)
        {
            try
            {
                //写入系数，构建系数矩阵
                double[][] a = { new double[] { (double)numericUpDownPP1X.Value, (double)numericUpDownPP1Y.Value, 0, 0,1,0 },
                             new double[] { 0, 0, (double)numericUpDownPP1X.Value, (double)numericUpDownPP1Y.Value,0,1 },
                             new double[] { (double)numericUpDownPP2X.Value, (double)numericUpDownPP2Y.Value, 0, 0,1,0 },
                             new double[] { 0, 0, (double)numericUpDownPP2X.Value, (double)numericUpDownPP2Y.Value,0,1 },
                             new double[] { (double)numericUpDownPP3X.Value, (double)numericUpDownPP3Y.Value, 0, 0,1,0 },
                             new double[] { 0, 0, (double)numericUpDownPP3X.Value, (double)numericUpDownPP3Y.Value,0,1 }
                           };
                Matrix ma = Matrix.Create(a);

                //右侧矩阵
                double[][] b = { new double[] { (double)numericUpDownPR1X.Value },
                             new double[] { (double)numericUpDownPR1Y.Value },
                             new double[] { (double)numericUpDownPR2X.Value },
                             new double[] { (double)numericUpDownPR2Y.Value },
                             new double[] { (double)numericUpDownPR3X.Value },
                             new double[] { (double)numericUpDownPR3Y.Value }
                           };
                Matrix mb = Matrix.Create(b);
                //var resultLU = ma.LUDecomposition.Solve(mb);
                //var resultQR = ma.QRDecomposition.Solve(mb);
                //矩阵求解方法solve
                mx = ma.Solve(mb);
                Sx = System.Math.Sqrt(mx[0, 0] * mx[0, 0] + mx[1, 0] * mx[1, 0]);
                Sy = System.Math.Sqrt(mx[2, 0] * mx[2, 0] + mx[3, 0] * mx[3, 0]);
                if (Sx==0||Sy==0)
                {
                    mx = null;
                    throw new Exception("输入变换数据有误！比例变换系数不可为0！");
                }
                if (mx[0, 0]!=0)
                {
                    
                    thea = System.Math.Atan(mx[1, 0] / mx[0, 0]);
                    Sx = mx[0, 0] / System.Math.Cos(thea);
                    Sy = mx[3, 0] / System.Math.Cos(thea);
                    Tx = mx[4, 0];
                    Ty = mx[5, 0];
                }
                else 
                {
                    Sx = mx[1, 0];
                    Sy = -mx[2, 0];
                    thea = 90 / 180 * 3.1415926535;
                    Tx = mx[4, 0];
                    Ty = mx[5, 0];
                }





                textBoxThreePointCalibration.Text = "X方向比例系数：" + "\r\n" + Sx.ToString() + "\r\n" +
                    "Y方向比例系数：" + "\r\n" + Sy.ToString() + "\r\n" +
                    "机器人坐标系旋转到视觉坐标系的旋转角度（弧度角，逆时针为正）：" + "\r\n" + thea.ToString() + "\r\n" +
                    "X方向平移变换系数：" + "\r\n" + Tx.ToString() + "\r\n" + "Y方向平移变换系数：" + "\r\n" + Ty.ToString() + "\r\n";
            }
            catch (System.Exception ex)
            {
                mx = null;
                MessageBox.Show("生成失败："+ex.Message);
            }

        }

        private void checkBoxUseThreePointCalibration_CheckedChanged(object sender, EventArgs e)
        {
            if (mx==null&&checkBoxUseThreePointCalibration.Checked)
            {
                checkBoxUseThreePointCalibration.Checked = false;
                MessageBox.Show("未生成变换矩阵！无法使用三点标定法转换坐标！");
            }
        }





















    }
}
