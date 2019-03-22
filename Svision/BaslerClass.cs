/*
Basler camera class
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Basler.Pylon;
using HalconDotNet;
namespace Svision
{
    public class BaslerClass
    {
        static Version Sfnc2_0_0 = new Version(2, 0, 0);
        static public HObject hoImage;
        static public bool isImageOk=false;
        static public bool errorImageCode = false;
        static public string errorImageStr;
        private int[] rowNunberRange = new int[2];//取值范围;
        private int[] columnNumberRange = new int[2];//取值范围;
        private double[] gainPercentRange;//取值范围;
        private double[] GammaPercentRange;//取值范围;
        private double[] exposurePercentRange;//取值范围;
        private double[] whiteBalanceBluePercentRange;//取值范围;
        private double[] whiteBalanceGreenPercentRange;//取值范围;
        private double[] whiteBalanceRedPercentRange;//取值范围;
        private int channelNumberMax;//通道最大值;

        private int rowIncrement;//变化步长;
        private int columnIncrement;//变化步长;
        private int xOffsetIncrement;//变化步长;
        private int yOffsetIncrement;//变化步长;

        private bool isGainAuto;//自动增益标志位;
        private bool isExposureAuto;//自动曝光标志位;
        private bool isWhiteBalanceAuto;//自动白平衡标志位;

        private int rowNunber;//行;
        private int columnNumber;//列;
        private int channelNumber;//通道;
        private int xOffset;//X偏移;
        private int yOffset;//Y偏移;
        private double gainPercent = 0;//增益百分数;
        private double GammaPercent = 0;//Gamma百分数;
        private double exposurePercent = 0;//曝光百分数;
        private double whiteBalanceBluePercent;//白平衡百分数（蓝）;
        private double whiteBalanceGreenPercent;//白平衡百分数（绿）;
        private double whiteBalanceRedPercent;//白平衡百分数（红）;


        public Camera camera;

        public BaslerClass()
        {
            HOperatorSet.GenEmptyObj(out hoImage);
            rowNunber = 480;
            columnNumber = 640;
            channelNumber = 1;
            xOffset = 0;
            yOffset = 0;
            gainPercent = 0;
            isGainAuto = false;
            isExposureAuto = false;
            isWhiteBalanceAuto = false;

            try
            {
                if (Svision.GetMe().cameraStr!=null)
                {
                    camera = new Camera(Svision.GetMe().cameraStr);
                }
                else
                {
                    camera = new Camera();
                }


                //软件触发
                camera.CameraOpened += Configuration.SoftwareTrigger;
                camera.Open();
                camera.StreamGrabber.ImageGrabbed += OnImageGrabbed;
                

                camera.Parameters[PLTransportLayer.HeartbeatTimeout].TrySetValue(1000, IntegerValueCorrection.Nearest);  // 1000 ms timeout
                camera.Parameters[PLCamera.PixelFormat].TrySetValue(PLCamera.PixelFormat.BayerBG8);
                string oldPixelFormat = camera.Parameters[PLCamera.PixelFormat].GetValue();
                if (oldPixelFormat == "BayerBG8")
                {
                    channelNumberMax = 3;
                }
                else
                {
                    channelNumberMax = 1;
                }
                if (channelNumber > channelNumberMax)
                {
                    channelNumber = channelNumberMax;
                }
                rowIncrement = (int)camera.Parameters[PLCamera.Height].GetIncrement();
                camera.Parameters[PLCamera.OffsetY].SetValue(0);
                camera.Parameters[PLCamera.Height].TrySetToMinimum();
                rowNunberRange[0] = (int)camera.Parameters[PLCamera.Height].GetValue();
                camera.Parameters[PLCamera.Height].TrySetToMaximum();
                rowNunberRange[1] = (int)camera.Parameters[PLCamera.Height].GetValue();

                if (rowNunber > rowNunberRange[1])
                {
                    rowNunber = rowNunberRange[1];
                }
                if (rowNunber < rowNunberRange[0])
                {
                    rowNunber = rowNunberRange[0];
                }
                if (rowNunber - rowNunberRange[0] % rowIncrement != 0)
                {
                    rowNunber = (rowNunber - rowNunberRange[0]) / rowIncrement * rowIncrement + rowNunberRange[0];
                }

                columnIncrement = (int)camera.Parameters[PLCamera.Width].GetIncrement();
                camera.Parameters[PLCamera.OffsetX].SetValue(0);

                camera.Parameters[PLCamera.Width].TrySetToMinimum();
                columnNumberRange[0] = (int)camera.Parameters[PLCamera.Width].GetValue();
                camera.Parameters[PLCamera.Width].TrySetToMaximum();
                columnNumberRange[1] = (int)camera.Parameters[PLCamera.Width].GetValue();
                if (columnNumber > columnNumberRange[1])
                {
                    columnNumber = columnNumberRange[1];
                }
                if (columnNumber < columnNumberRange[0])
                {
                    columnNumber = columnNumberRange[0];
                }
                if (columnNumber - columnNumberRange[0] % columnIncrement != 0)
                {
                    columnNumber = (columnNumber - columnNumberRange[0]) / columnIncrement * columnIncrement + columnNumberRange[0];
                }
                xOffsetIncrement = (int)camera.Parameters[PLCamera.OffsetX].GetIncrement();
                yOffsetIncrement = (int)camera.Parameters[PLCamera.OffsetY].GetIncrement();
                if (xOffset > (columnNumberRange[1] - columnNumber))
                {
                    xOffset = (columnNumberRange[1] - columnNumber);
                }
                if (xOffset % xOffsetIncrement != 0)
                {
                    xOffset = xOffset / xOffsetIncrement * xOffsetIncrement;
                }
                if (yOffset > (rowNunberRange[1] - rowNunber))
                {
                    yOffset = (rowNunberRange[1] - rowNunber);
                }
                if (yOffset % yOffsetIncrement != 0)
                {
                    yOffset = yOffset / yOffsetIncrement * yOffsetIncrement;
                }
                gainPercentRange = new double[2];
                gainPercentRange[0] = 0;
                gainPercentRange[1] = 1;
                GammaPercentRange = new double[2];
                camera.Parameters[PLCamera.GammaEnable].TrySetValue(true);
                GammaPercentRange[0] = 0;
                GammaPercentRange[1] = 1;
                exposurePercentRange = new double[2];
                exposurePercentRange[0] = 0;
                exposurePercentRange[1] = 1;
                whiteBalanceBluePercentRange = new double[2];
                whiteBalanceBluePercentRange[0] = 0;
                whiteBalanceBluePercentRange[1] = 1;
                whiteBalanceGreenPercentRange = new double[2];
                whiteBalanceGreenPercentRange[0] = 0;
                whiteBalanceGreenPercentRange[1] = 1;
                whiteBalanceRedPercentRange = new double[2];
                whiteBalanceRedPercentRange[0] = 0;
                whiteBalanceRedPercentRange[1] = 1;
                // The parameter MaxNumBuffer can be used to control the amount of buffers
                // allocated for grabbing. The default value of this parameter is 10.
                camera.Parameters[PLCameraInstance.MaxNumBuffer].SetValue(5);
                camera.Parameters[PLCamera.OffsetX].SetValue((long)xOffset);
                camera.Parameters[PLCamera.OffsetY].SetValue((long)yOffset);
                // Some parameters have restrictions. You can use GetIncrement/GetMinimum/GetMaximum to make sure you set a valid value.
                // Here, we let pylon correct the value if needed.
                camera.Parameters[PLCamera.Width].SetValue(columnNumber, IntegerValueCorrection.Nearest);
                camera.Parameters[PLCamera.Height].SetValue(rowNunber, IntegerValueCorrection.Nearest);
                if (channelNumber == 1)
                {
                    camera.Parameters[PLCamera.PixelFormat].TrySetValue(PLCamera.PixelFormat.Mono8);
                }
                else
                {
                    camera.Parameters[PLCamera.PixelFormat].TrySetValue(PLCamera.PixelFormat.BayerBG8);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public BaslerClass(int rowPara, int columnPara, int channelPara, int xOffsetPara = 0, int yOffsetPara = 0)
        {
            rowNunber = rowPara;
            columnNumber = columnPara;
            channelNumber = channelPara;
            xOffset = xOffsetPara;
            yOffset = yOffsetPara;
            isGainAuto = false;
            isExposureAuto = false;
            isWhiteBalanceAuto = false;

            try
            {
                // Create a camera object that selects the first camera device found.
                // More constructors are available for selecting a specific camera device.
                camera = new Camera();

                // Set the acquisition mode to free running continuous acquisition when the camera is opened.
                camera.CameraOpened += Configuration.AcquireContinuous;
                // Open the connection to the camera device.
                //if (camera.IsOpen)
                //{
                //    camera.Close();
                //}
                camera.Open();
                camera.Parameters[PLTransportLayer.HeartbeatTimeout].TrySetValue(1000, IntegerValueCorrection.Nearest);  // 1000 ms timeout
                // Set an enum parameter.
                //camera.Parameters[PLTransportLayer.HeartbeatTimeout].TrySetValue(1000, IntegerValueCorrection.Nearest);
                //Environment.SetEnvironmentVariable("PYLON_GIGE_HEARTBEAT", "30000");
                camera.Parameters[PLCamera.PixelFormat].TrySetValue(PLCamera.PixelFormat.BayerBG8);
                string oldPixelFormat = camera.Parameters[PLCamera.PixelFormat].GetValue();
                if (oldPixelFormat == "BayerBG8")
                {
                    channelNumberMax = 3;
                }
                else
                {
                    channelNumberMax = 1;
                }
                if (channelNumber > channelNumberMax)
                {
                    channelNumber = channelNumberMax;
                }
                rowIncrement = (int)camera.Parameters[PLCamera.Height].GetIncrement();
                camera.Parameters[PLCamera.OffsetY].SetValue(0);
                camera.Parameters[PLCamera.Height].TrySetToMinimum();
                rowNunberRange[0] = (int)camera.Parameters[PLCamera.Height].GetValue();
                camera.Parameters[PLCamera.Height].TrySetToMaximum();
                rowNunberRange[1] = (int)camera.Parameters[PLCamera.Height].GetValue();
                if (rowNunber > rowNunberRange[1])
                {
                    rowNunber = rowNunberRange[1];
                }
                if (rowNunber < rowNunberRange[0])
                {
                    rowNunber = rowNunberRange[0];
                }
                if (rowNunber - rowNunberRange[0] % rowIncrement != 0)
                {
                    rowNunber = (rowNunber - rowNunberRange[0]) / rowIncrement * rowIncrement + rowNunberRange[0];
                }
                columnIncrement = (int)camera.Parameters[PLCamera.Width].GetIncrement();
                camera.Parameters[PLCamera.OffsetX].SetValue(0);
                camera.Parameters[PLCamera.Width].TrySetToMinimum();
                columnNumberRange[0] = (int)camera.Parameters[PLCamera.Width].GetValue();
                camera.Parameters[PLCamera.Width].TrySetToMaximum();
                columnNumberRange[1] = (int)camera.Parameters[PLCamera.Width].GetValue();
                if (columnNumber > columnNumberRange[1])
                {
                    columnNumber = columnNumberRange[1];
                }
                if (columnNumber < columnNumberRange[0])
                {
                    columnNumber = columnNumberRange[0];
                }
                if (columnNumber - columnNumberRange[0] % columnIncrement != 0)
                {
                    columnNumber = (columnNumber - columnNumberRange[0]) / columnIncrement * columnIncrement + columnNumberRange[0];
                }
                xOffsetIncrement = (int)camera.Parameters[PLCamera.OffsetX].GetIncrement();
                yOffsetIncrement = (int)camera.Parameters[PLCamera.OffsetY].GetIncrement();
                if (xOffset > (columnNumberRange[1] - columnNumber))
                {
                    xOffset = (columnNumberRange[1] - columnNumber);
                }
                if (xOffset % xOffsetIncrement != 0)
                {
                    xOffset = xOffset / xOffsetIncrement * xOffsetIncrement;
                }
                if (yOffset > (rowNunberRange[1] - rowNunber))
                {
                    yOffset = (rowNunberRange[1] - rowNunber);
                }
                if (yOffset % yOffsetIncrement != 0)
                {
                    yOffset = yOffset / yOffsetIncrement * yOffsetIncrement;
                }
                gainPercentRange = new double[2];
                gainPercentRange[0] = 0;
                gainPercentRange[1] = 1;
                GammaPercentRange = new double[2];
                camera.Parameters[PLCamera.GammaEnable].TrySetValue(true);
                GammaPercentRange[0] = 0;
                GammaPercentRange[1] = 1;
                exposurePercentRange = new double[2];
                exposurePercentRange[0] = 0;
                exposurePercentRange[1] = 1;
                whiteBalanceBluePercentRange = new double[2];
                whiteBalanceBluePercentRange[0] = 0;
                whiteBalanceBluePercentRange[1] = 1;
                whiteBalanceGreenPercentRange = new double[2];
                whiteBalanceGreenPercentRange[0] = 0;
                whiteBalanceGreenPercentRange[1] = 1;
                whiteBalanceRedPercentRange = new double[2];
                whiteBalanceRedPercentRange[0] = 0;
                whiteBalanceRedPercentRange[1] = 1;
                // The parameter MaxNumBuffer can be used to control the amount of buffers
                // allocated for grabbing. The default value of this parameter is 10.
                camera.Parameters[PLCameraInstance.MaxNumBuffer].SetValue(5);
                camera.Parameters[PLCamera.OffsetX].SetValue((long)xOffset);
                camera.Parameters[PLCamera.OffsetY].SetValue((long)yOffset);
                // Some parameters have restrictions. You can use GetIncrement/GetMinimum/GetMaximum to make sure you set a valid value.
                // Here, we let pylon correct the value if needed.
                camera.Parameters[PLCamera.Width].SetValue(columnNumber / columnIncrement * columnIncrement, IntegerValueCorrection.Nearest);
                camera.Parameters[PLCamera.Height].SetValue(rowNunber / rowIncrement * rowIncrement, IntegerValueCorrection.Nearest);
                if (channelNumber == 1)
                {
                    camera.Parameters[PLCamera.PixelFormat].TrySetValue(PLCamera.PixelFormat.Mono8);
                }
                else
                {
                    camera.Parameters[PLCamera.PixelFormat].TrySetValue(PLCamera.PixelFormat.BayerBG8);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);

            }
        }
        public void getRowIncrement(out int rowIncrementValue)
        {
            //获得行最小增量
            rowIncrementValue = rowIncrement;
        }
        public void getColumnIncrement(out int columnIncrementValue)
        {
            //获得列最小增量
            columnIncrementValue = columnIncrement;
        }
        public void getRowAndColumnIncrement(out int rowIncrementValue, out int columnIncrementValue)
        {
            //获得行（列）最小增量
            rowIncrementValue = rowIncrement;
            columnIncrementValue = columnIncrement;
        }
        public void getOffsetIncrement(out int xOffsetIncrementValue, out int yOffsetIncrementValue)
        {
            xOffsetIncrementValue = xOffsetIncrement;
            yOffsetIncrementValue = yOffsetIncrement;
        }
        public void getCameraInformation(out string modelNameStr, out string deviceVendorNameStr, out string deviceModelNameStr, out string deviceFirmwareVersionStr)
        {
            // 获得相机信息
            modelNameStr = camera.CameraInfo[CameraInfoKey.ModelName];
            deviceVendorNameStr = camera.Parameters[PLCamera.DeviceVendorName].GetValue();
            deviceModelNameStr = camera.Parameters[PLCamera.DeviceModelName].GetValue();
            deviceFirmwareVersionStr = camera.Parameters[PLCamera.DeviceFirmwareVersion].GetValue();
        }
        public void getCameraParametersRange(out int[] rowRange, out int[] columnRange, out int channelMax, out int[] xOffsetRange, out int[] yOffsetRange, out double[] gainRange, out double[] GammaRange, out double[] exposureRange, out double[] whiteBalanceBlueRange, out double[] whiteBalanceRadRange, out double[] whiteBalanceGreenRange)
        {
            //获得相机参数范围
            rowRange = rowNunberRange;
            columnRange = columnNumberRange;
            channelMax = channelNumberMax;
            xOffsetRange = new int[2] { 0, columnNumberRange[1] - columnNumber };
            yOffsetRange = new int[2] { 0, rowNunberRange[1] - rowNunber };
            gainRange = gainPercentRange;
            GammaRange = GammaPercentRange;
            exposureRange = exposurePercentRange;
            whiteBalanceBlueRange = whiteBalanceBluePercentRange;
            whiteBalanceRadRange = whiteBalanceRedPercentRange;
            whiteBalanceGreenRange = whiteBalanceGreenPercentRange;
        }
        public void getCameraParametersRange(out int[] rowRange, out int[] columnRange, out int maxChannelNumber)
        {
            //获得相机基础参数范围
            rowRange = rowNunberRange;
            columnRange = columnNumberRange;
            maxChannelNumber = channelNumberMax;
        }
        public void getCameraRowRange(out int[] rowRange)
        {
            //获得相机行范围
            rowRange = rowNunberRange;
        }
        public void getCameraColumnRange(out int[] columnRange)
        {
            //获得相机列范围
            columnRange = columnNumberRange;
        }
        public void getCameraMaxChannel(out int channelMax)
        {
            //获得相机通道数最大值
            channelMax = channelNumberMax;
        }
        public void getGainRange(out double[] gainRange)
        {
            //获得增益百分比范围
            gainRange = gainPercentRange;
        }
        public void getGammaRange(out double[] GammaRange)
        {
            //获得Gamma百分比范围
            GammaRange = GammaPercentRange;
        }
        public void getExposureRange(out double[] exposureRange)
        {
            //获得相机曝光范围
            exposureRange = exposurePercentRange;
        }
        public void getWhiteBalanceBlueRange(out double[] whiteBalanceBlueRange)
        {
            //得到白平衡蓝色分量范围;
            whiteBalanceBlueRange = whiteBalanceBluePercentRange;
        }
        public void getWhiteBalanceGreenRange(out double[] whiteBalanceGreenRange)
        {
            //得到白平衡绿色分量范围;
            whiteBalanceGreenRange = whiteBalanceGreenPercentRange;
        }
        public void getWhiteBalanceRedRange(out double[] whiteBalanceRedRange)
        {
            //得到白平衡红色分量范围;
            whiteBalanceRedRange = whiteBalanceRedPercentRange;
        }
        public void getCameraOffsetRange(out int[] xOffsetRange, out int[] yOffsetRange)
        {
            //获得相机偏移参数范围
            xOffsetRange = new int[2] { 0, columnNumberRange[1] - columnNumber };
            yOffsetRange = new int[2] { 0, rowNunberRange[1] - rowNunber };
        }
        public void getCameraXOffsetRange(out int[] xOffsetRange)
        {
            //获得相机X偏移参数范围
            xOffsetRange = new int[2] { 0, columnNumberRange[1] - columnNumber };
        }
        public void getCameraYOffsetRange(out int[] yOffsetRange)
        {
            //获得相机Y偏移参数范围
            yOffsetRange = new int[2] { 0, rowNunberRange[1] - rowNunber };
        }
        public void setOffsetX(int x)
        {
            //设置相机X偏移;
            if (0 <= x && x <= columnNumberRange[1] - columnNumber && x % xOffsetIncrement == 0)
            {
                xOffset = x;
                camera.Parameters[PLCamera.OffsetX].SetValue((long)xOffset);
            }
            else
            {
                throw new Exception("OffsetX  error!");
            }
        }
        public void setOffsetY(int y)
        {
            //设置相机Y偏移;
            if (0 <= y && y <= rowNunberRange[1] - rowNunber && y % yOffsetIncrement == 0)
            {
                yOffset = y;
                camera.Parameters[PLCamera.OffsetY].SetValue((long)yOffset);
            }
            else
            {
                throw new Exception("OffsetY  error!");
            }
        }
        public void setRowNumber(int rowNum)
        {
            //设置相机行;
            if (rowNunberRange[0] <= rowNum && rowNum <= rowNunberRange[1] && (rowNum - rowNunberRange[0]) % rowIncrement == 0)
            {
                rowNunber = rowNum;
                camera.Parameters[PLCamera.Height].SetValue(rowNunber, IntegerValueCorrection.Nearest);
            }
            else
            {
                throw new Exception("rowNumber  error!");
            }
        }
        public void setColumnNumber(int columnNum)
        {
            //设置相机列;
            if (columnNumberRange[0] <= columnNum && columnNum <= columnNumberRange[1] && (columnNum - columnNumberRange[0]) % columnIncrement == 0)
            {
                columnNumber = columnNum;
                camera.Parameters[PLCamera.Width].SetValue(columnNumber, IntegerValueCorrection.Nearest);
            }
            else
            {
                throw new Exception("columnNumber  error!");
            }
        }
        public void setChannelNumber(int channelNum)
        {
            // string oldPixelFormat = camera.Parameters[PLCamera.PixelFormat].GetValue();
            try
            {
                //设置相机通道;
                if (1 <= channelNum && channelNum <= channelNumberMax)
                {
                    channelNumber = channelNum;
                    if (channelNumber == 1)
                    {
                        camera.Parameters[PLCamera.PixelFormat].TrySetValue(PLCamera.PixelFormat.Mono8);
                    }
                    else
                    {
                        camera.Parameters[PLCamera.PixelFormat].TrySetValue(PLCamera.PixelFormat.BayerBG8);
                    }
                }
                else
                {
                    throw new Exception("channelNumber  error!");
                }
            }
            catch (System.Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void setOffset(int x, int y)
        {
            //设置相机偏移;
            if (0 <= x && x <= columnNumberRange[1] - columnNumber && x % xOffsetIncrement == 0)
            {
                xOffset = x;
                camera.Parameters[PLCamera.OffsetX].SetValue((long)xOffset);
            }
            else
            {
                throw new Exception("OffsetX  error!");
            }
            if (0 <= y && y <= rowNunberRange[1] - rowNunber && y % yOffsetIncrement == 0)
            {
                yOffset = y;
                camera.Parameters[PLCamera.OffsetY].SetValue((long)yOffset);
            }
            else
            {
                throw new Exception("OffsetY  error!");
            }
        }
        public void setImageSize(int rowNum, int columnNum)
        {
            //设置图像行、列;
            if (rowNunberRange[0] <= rowNum && rowNum <= rowNunberRange[1] && (rowNum - rowNunberRange[0]) % rowIncrement == 0)
            {
                rowNunber = rowNum;
                camera.Parameters[PLCamera.Height].SetValue(rowNunber, IntegerValueCorrection.Nearest);
            }
            else
            {
                throw new Exception("rowNumber  error!");
            }
            if (columnNumberRange[0] <= columnNum && columnNum <= columnNumberRange[1] && (columnNum - columnNumberRange[0]) % columnIncrement == 0)
            {
                columnNumber = columnNum;
                camera.Parameters[PLCamera.Width].SetValue(columnNumber, IntegerValueCorrection.Nearest);
            }
            else
            {
                throw new Exception("columnNumber  error!");
            }
        }
        public void setParameters(int rowNum, int columnNum, int channelNum)
        {
            //设置相机基础参数;
            if (rowNunberRange[0] <= rowNum && rowNum <= rowNunberRange[1] && (rowNum - rowNunberRange[0]) % rowIncrement == 0)
            {
                rowNunber = rowNum;
                camera.Parameters[PLCamera.Height].SetValue(rowNunber, IntegerValueCorrection.Nearest);
            }
            else
            {
                throw new Exception("rowNumber  error!");
            }
            if (columnNumberRange[0] <= columnNum && columnNum <= columnNumberRange[1] && (columnNum - columnNumberRange[0]) % columnIncrement == 0)
            {
                columnNumber = columnNum;
                camera.Parameters[PLCamera.Width].SetValue(columnNumber, IntegerValueCorrection.Nearest);
            }
            else
            {
                throw new Exception("columnNumber  error!");
            }
            if (1 <= channelNum && channelNum <= channelNumberMax)
            {
                channelNumber = channelNum;
                if (channelNumber == 1)
                {
                    camera.Parameters[PLCamera.PixelFormat].TrySetValue(PLCamera.PixelFormat.Mono8);
                }
                else
                {
                    camera.Parameters[PLCamera.PixelFormat].TrySetValue(PLCamera.PixelFormat.BayerBG8);
                }
            }
            else
            {
                throw new Exception("channelNumber  error!");
            }
        }
        public void setParameters(int rowNum, int columnNum, int channelNum, int x, int y)
        {
            //设置相机参数;
            if (rowNunberRange[0] <= rowNum && rowNum <= rowNunberRange[1] && (rowNum - rowNunberRange[0]) % rowIncrement == 0)
            {
                rowNunber = rowNum;
                camera.Parameters[PLCamera.Height].SetValue(rowNunber, IntegerValueCorrection.Nearest);
            }
            else
            {
                throw new Exception("rowNumber  error!");
            }
            if (columnNumberRange[0] <= columnNum && columnNum <= columnNumberRange[1] && (columnNum - columnNumberRange[0]) % columnIncrement == 0)
            {
                columnNumber = columnNum;
                camera.Parameters[PLCamera.Width].SetValue(columnNumber, IntegerValueCorrection.Nearest);
            }
            else
            {
                throw new Exception("columnNumber  error!");
            }
            if (1 <= channelNum && channelNum <= channelNumberMax)
            {
                channelNumber = channelNum;
                if (channelNumber == 1)
                {
                    camera.Parameters[PLCamera.PixelFormat].TrySetValue(PLCamera.PixelFormat.Mono8);
                }
                else
                {
                    camera.Parameters[PLCamera.PixelFormat].TrySetValue(PLCamera.PixelFormat.BayerBG8);
                }
            }
            else
            {
                throw new Exception("channelNumber  error!");
            }
            if (0 <= x && x <= columnNumberRange[1] - columnNumber && x % xOffsetIncrement == 0)
            {
                xOffset = x;
                camera.Parameters[PLCamera.OffsetX].SetValue((long)xOffset);
            }
            else
            {
                throw new Exception("OffsetX  error!");
            }
            if (0 <= y && y <= rowNunberRange[1] - rowNunber && y % yOffsetIncrement == 0)
            {
                yOffset = y;
                camera.Parameters[PLCamera.OffsetY].SetValue((long)yOffset);
            }
            else
            {
                throw new Exception("OffsetY  error!");
            }
        }
        public void setGainPercent(double gainValue)
        {
            //设置相机增益百分比;
            if (isGainAuto == false)
            {
                if (gainPercentRange[0] <= gainValue && gainValue <= gainPercentRange[1])
                {
                    gainPercent = gainValue;

                    if (camera.GetSfncVersion() < Sfnc2_0_0)
                    {
                        // In previous SFNC versions, GainRaw is an integer parameter.
                        camera.Parameters[PLCamera.GainRaw].SetValuePercentOfRange(gainPercent * 100);
                    }
                    else // For SFNC 2.0 cameras, e.g. USB3 Vision cameras
                    {
                        // In SFNC 2.0, Gain is a float parameter.
                        camera.Parameters[PLUsbCamera.Gain].SetValuePercentOfRange(gainPercent * 100);
                        // For USB cameras, Gamma is always enabled.
                    }
                }
                else
                {
                    throw new Exception("gain  error!");
                }
            }
            else
            {
                throw new Exception("Gain can not be set when the GainAuto is true");
            }
        }
        public void setGammaPercent(double GammaValue)
        {
            //设置相机Gamma值百分比;
            if (GammaPercentRange[0] <= GammaValue && GammaValue <= GammaPercentRange[1])
            {
                GammaPercent = GammaValue;
                camera.Parameters[PLCamera.Gamma].TrySetValuePercentOfRange(GammaPercent * 100);
            }
            else
            {
                throw new Exception("Gamma  error!");
            }
        }
        public void setExposurePercent(double exposureTimeValue)
        {
            //设置相机曝光时间百分比;
            if (isExposureAuto == false)
            {
                if (exposurePercentRange[0] <= exposureTimeValue && exposureTimeValue <= exposurePercentRange[1])
                {
                    exposurePercent = exposureTimeValue;
                    camera.Parameters[PLCamera.ExposureTimeRaw].TrySetValuePercentOfRange(exposurePercent * 100);
                }
                else
                {
                    throw new Exception("exposure  error!");
                }
            }
            else
            {
                throw new Exception("ExposureTime can not be set when the ExposureAuto is true");
            }
        }
        public void setGainAuto(bool isGainAutoFlag)
        {
            //设置相机自动增益;
            if (isGainAutoFlag)
            {
                camera.Parameters[PLCamera.GainAuto].TrySetValue(PLCamera.GainAuto.Once);
                isGainAuto = true;
            }
            else
            {
                camera.Parameters[PLCamera.GainAuto].TrySetValue(PLCamera.GainAuto.Off);
                isGainAuto = false;
            }
        }
        public void setExposureAuto(bool isExposureAutoFlag)
        {
            //设置相机自动曝光;
            if (isExposureAutoFlag)
            {
                camera.Parameters[PLCamera.ExposureAuto].TrySetValue(PLCamera.ExposureAuto.Once);
                isExposureAuto = true;
            }
            else
            {
                camera.Parameters[PLCamera.ExposureAuto].TrySetValue(PLCamera.ExposureAuto.Off);
                isExposureAuto = false;
            }
        }
        public void setWhiteBalanceAuto(bool isWhiteBalanceAutoFlag)
        {
            //设置相机自动白平衡;
            if (isWhiteBalanceAutoFlag)
            {
                camera.Parameters[PLCamera.BalanceWhiteAuto].TrySetValue(PLCamera.BalanceWhiteAuto.Once);
                isWhiteBalanceAuto = true;
            }
            else
            {
                bool isOk = camera.Parameters[PLCamera.BalanceWhiteAuto].TrySetValue(PLCamera.BalanceWhiteAuto.Off);
                isWhiteBalanceAuto = false;
            }
        }
        public int getRowNumber()
        {

            rowNunber = (int)camera.Parameters[PLCamera.Height].GetValue();
            return rowNunber;
        }
        public int getColumnNumber()
        {
            columnNumber = (int)camera.Parameters[PLCamera.Width].GetValue();
            return columnNumber;
        }
        public int getXOffsetNumber()
        {
            xOffset = (int)camera.Parameters[PLCamera.OffsetX].GetValue();
            return xOffset;
        }
        public int getYOffsetNumber()
        {
            yOffset = (int)camera.Parameters[PLCamera.OffsetY].GetValue();
            return yOffset;
        }
        public int getChannelNumber()
        {


            string channelFormat = camera.Parameters[PLCamera.PixelFormat].GetValue();
            if (channelFormat == "BayerBG8")
            {
                channelNumber = 3;
            }
            else
            {
                channelNumber = 1;
            }
            return channelNumber;
        }
        //public bool getIsGammaAuto()
        //{
        //    return isGammaAuto;
        //}
        public double getGammaPercent()
        {
            double GammaValuePercent;
            GammaValuePercent = camera.Parameters[PLCamera.Gamma].GetValuePercentOfRange();
            return GammaValuePercent / 100;
        }
        public bool getIsGainAuto()
        {
            return isGainAuto;
        }
        public double getGainPercent()
        {
            double gainValuePercent;
            if (camera.GetSfncVersion() < Sfnc2_0_0)
            {
                // In previous SFNC versions, GainRaw is an integer parameter.
                gainValuePercent = camera.Parameters[PLCamera.GainRaw].GetValuePercentOfRange();
            }
            else // For SFNC 2.0 cameras, e.g. USB3 Vision cameras
            {
                // In SFNC 2.0, Gain is a float parameter.
                gainValuePercent = camera.Parameters[PLUsbCamera.Gain].GetValuePercentOfRange();
                // For USB cameras, Gamma is always enabled.
            }
            return gainValuePercent / 100;
        }
        public bool getIsExposureAuto()
        {
            return isExposureAuto;
        }
        public double getExposurePercent()
        {
            double ExposureValuePercent;
            ExposureValuePercent = camera.Parameters[PLCamera.ExposureTimeRaw].GetValuePercentOfRange();
            return ExposureValuePercent / 100;
        }
        public bool getIsWhiteBalanceAuto()
        {
            return isWhiteBalanceAuto;
        }
        public double getWhiteBalanceBluePercent()
        {
            double WhiteBalanceBlueValuePercent;
            camera.Parameters[PLCamera.BalanceRatioSelector].TrySetValue(PLCamera.BalanceRatioSelector.Blue);
            WhiteBalanceBlueValuePercent = camera.Parameters[PLCamera.BalanceRatioAbs].GetValuePercentOfRange();
            return WhiteBalanceBlueValuePercent / 100;
        }
        public double getWhiteBalanceRedPercent()
        {
            double WhiteBalanceRedValuePercent;
            camera.Parameters[PLCamera.BalanceRatioSelector].TrySetValue(PLCamera.BalanceRatioSelector.Red);
            WhiteBalanceRedValuePercent = camera.Parameters[PLCamera.BalanceRatioAbs].GetValuePercentOfRange();
            return WhiteBalanceRedValuePercent / 100;
        }
        public double getWhiteBalanceGreenPercent()
        {
            double WhiteBalanceGreenValuePercent;
            camera.Parameters[PLCamera.BalanceRatioSelector].TrySetValue(PLCamera.BalanceRatioSelector.Green);
            WhiteBalanceGreenValuePercent = camera.Parameters[PLCamera.BalanceRatioAbs].GetValuePercentOfRange();
            return WhiteBalanceGreenValuePercent / 100;
        }
        public void setWhiteBalanceBlue(double whiteBalanceBlue)
        {
            //设置相机白平衡蓝色分量;
            if (isWhiteBalanceAuto == false)
            {
                if (whiteBalanceBlue >= whiteBalanceBluePercentRange[0] && whiteBalanceBlue <= whiteBalanceBluePercentRange[1])
                {
                    whiteBalanceBluePercent = whiteBalanceBlue;
                    camera.Parameters[PLCamera.BalanceRatioSelector].TrySetValue(PLCamera.BalanceRatioSelector.Blue);
                    camera.Parameters[PLCamera.BalanceRatioAbs].TrySetValuePercentOfRange(whiteBalanceBluePercent * 100);
                }
                else
                {
                    throw new Exception("WhiteBalanceBlue  error!");
                }
            }
            else
            {
                throw new Exception("WhiteBalanceBlue can not be set when the WhiteBalanceAuto is true");
            }
        }
        public void setWhiteBalanceGreen(double whiteBalanceGreen)
        {
            //设置相机白平衡绿色分量;
            if (isWhiteBalanceAuto == false)
            {
                if (whiteBalanceGreen >= whiteBalanceGreenPercentRange[0] && whiteBalanceGreen <= whiteBalanceGreenPercentRange[1])
                {
                    whiteBalanceGreenPercent = whiteBalanceGreen;
                    camera.Parameters[PLCamera.BalanceRatioSelector].TrySetValue(PLCamera.BalanceRatioSelector.Green);
                    camera.Parameters[PLCamera.BalanceRatioAbs].TrySetValuePercentOfRange(whiteBalanceGreenPercent * 100);
                }
                else
                {
                    throw new Exception("WhiteBalanceGreen  error!");
                }
            }
            else
            {
                throw new Exception("WhiteBalanceGreen can not be set when the WhiteBalanceAuto is true");
            }
        }
        public void setWhiteBalanceRed(double whiteBalanceRed)
        {
            //设置相机白平衡红色分量;
            if (isWhiteBalanceAuto == false)
            {
                if (whiteBalanceRed >= whiteBalanceRedPercentRange[0] && whiteBalanceRed <= whiteBalanceRedPercentRange[1])
                {
                    whiteBalanceRedPercent = whiteBalanceRed;
                    camera.Parameters[PLCamera.BalanceRatioSelector].TrySetValue(PLCamera.BalanceRatioSelector.Red);
                    camera.Parameters[PLCamera.BalanceRatioAbs].TrySetValuePercentOfRange(whiteBalanceRedPercent * 100);
                }
                else
                {
                    throw new Exception("WhiteBalanceRed  error!");
                }
            }
            else
            {
                throw new Exception("WhiteBalanceRed can not be set when the WhiteBalanceAuto is true");
            }
        }
        public void startGrab()
        {
            if (camera.StreamGrabber.IsGrabbing == false)
            {
                camera.StreamGrabber.Start(GrabStrategy.OneByOne, GrabLoop.ProvidedByStreamGrabber);
            }

        }
        public void stopGrab()
        {
            if (camera.StreamGrabber.IsGrabbing)
            {
                camera.StreamGrabber.Stop();
            }

        }
        unsafe static void OnImageGrabbed(Object sender, ImageGrabbedEventArgs e)
        {
            // The grab result is automatically disposed when the event call back returns.
            // The grab result can be cloned using IGrabResult.Clone if you want to keep a copy of it (not shown in this sample).
            try
            {
                IGrabResult grabResult = e.GrabResult;
                // Image grabbed successfully?
                if (grabResult.GrabSucceeded)
                {
                    byte[] buffer = grabResult.PixelData as byte[];
                    fixed (byte* dataGray = buffer)
                    {
                        if (hoImage != null)
                        {
                            hoImage.Dispose();
                        }

                        HOperatorSet.GenImage1(out hoImage, "byte", (HTuple)Svision.GetMe().baslerCamera.columnNumber, (HTuple)Svision.GetMe().baslerCamera.rowNunber, new IntPtr(dataGray));
                    }
                    if (grabResult!=null)
                    {
                        grabResult.Dispose();
                    }
                    
                    //camera.StreamGrabber.Stop();
                    if (Svision.GetMe().baslerCamera.channelNumber != 1)
                    {

                        HOperatorSet.CfaToRgb(hoImage, out hoImage, "bayer_bg", "bilinear");
                    }
                    isImageOk = true;

                }
                else
                {
                    throw new Exception(grabResult.ErrorCode.ToString()+ grabResult.ErrorDescription);
                }
            }
            catch (Exception ex)
            {
                errorImageCode = true;
                errorImageStr = ex.Message;
            }
        }
        unsafe public void getFrameImageWithStart(out HObject hoImg)
        {
            //获得一帧图像;
            
            try
            {
                if (camera.WaitForFrameTriggerReady(10, TimeoutHandling.ThrowException))
                {
                    camera.ExecuteSoftwareTrigger();
                }
                while (!isImageOk)
                {
                    if (errorImageCode)
                    {
                        break;
                    }
                }
                if (errorImageCode)
                {
                    errorImageCode = false;
                    throw new Exception(errorImageStr);
                }
                //hoImg = hoImage;
                HOperatorSet.CopyImage(hoImage, out hoImg);
                isImageOk = false;
                //const uint8_t *pImageBuffer = (uint8_t *) ptrGrabResult->GetBuffer();
               
                // Stop grabbing.

                // Close the connection to the camera device.
            }
            catch (Exception e)
            {
                isImageOk = false;
                throw new Exception(e.Message);
            }
        }
        unsafe public void getFrameImage(out HObject hoImg)
        {
            //获得一帧图像;
            try
            {
                camera.StreamGrabber.Start(GrabStrategy.OneByOne, GrabLoop.ProvidedByStreamGrabber);
                if (camera.WaitForFrameTriggerReady(100, TimeoutHandling.ThrowException))
                {
                    camera.ExecuteSoftwareTrigger();
                }
                while (!isImageOk)
                {
                    if (errorImageCode)
                    {
                        break;
                    }
                }
                if (errorImageCode)
                {
                    errorImageCode = false;
                    throw new Exception(errorImageStr);
                }
                HOperatorSet.CopyImage(hoImage, out hoImg);
                isImageOk = false;
                //const uint8_t *pImageBuffer = (uint8_t *) ptrGrabResult->GetBuffer();

                // Stop grabbing.
                camera.StreamGrabber.Stop();
                // Close the connection to the camera device.
            }
            catch (Exception e)
            {
                isImageOk = false;
                throw new Exception(e.Message);
            }
        }
        public void refreshCamera()
        {
           // camera.CameraOpened += Configuration.SoftwareTrigger;
            camera.Open(1000, TimeoutHandling.ThrowException);
           // camera.StreamGrabber.ImageGrabbed += OnImageGrabbed;
            camera.Parameters[PLTransportLayer.HeartbeatTimeout].TrySetValue(1000, IntegerValueCorrection.Nearest);
            if (camera.StreamGrabber.IsGrabbing == false)
            {
                camera.StreamGrabber.Start(GrabStrategy.OneByOne, GrabLoop.ProvidedByStreamGrabber);
            }

        }
        public void refreshCameraWithOutGrab()
        {
            camera.Open(1000, TimeoutHandling.ThrowException);
            camera.Parameters[PLCameraInstance.MaxNumBuffer].SetValue(1);
            camera.Parameters[PLCameraInstance.OutputQueueSize].SetValue(1);
            camera.Parameters[PLTransportLayer.HeartbeatTimeout].TrySetValue(1000, IntegerValueCorrection.Nearest);


        }
        public void closeCamera()
        {
            if (camera.StreamGrabber.IsGrabbing)
            {
                camera.StreamGrabber.Stop();
            }
            System.Threading.Thread.Sleep(50);
            //关闭相机;
            if (camera.IsOpen)
            {
                camera.Close();
            }
        }
        ~BaslerClass()
        {
            if (hoImage!=null)
            {
                hoImage.Dispose();
            }

            if (camera != null)
            {
                if (camera.StreamGrabber.IsGrabbing)
                {
                    camera.StreamGrabber.Stop();
                }
                if (camera.IsOpen)
                {
                    camera.Close();
                }
            }
        }
    }
}
