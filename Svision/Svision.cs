
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using HalconDotNet;
using TradeServer.Tcp;
using System.Threading;
using System.IO;
using Basler.Pylon;

namespace Svision
{

    public partial class Svision : Form
    {
        public bool isImageOK = false;

        public List<string> fileNameList = new List<string>();
        public int fileNumIdx = 0;

        public string tcpStr;
        public HObject image, img;
        public HTuple hvWindowHandle = null;
        public BaslerClass baslerCamera;
        static double resizerate;
        private static Svision me;
        public int rowNumber, columnNumber;
        public int oriRowNumber, oriColumnNumber;
        public int oriPictureBoxShowImageWidth, oriPictureBoxShowImageHeight;
        AsyncServer gAsyncServer;
        private Thread cameraCheck;
        private bool cCheckFlag;
        public string cameraStr=null;
        public static Svision GetMe()
        {
            return me;
        }
        public Svision()
        {

            InitializeComponent();
            cCheckFlag = false;
            me = this;
            oriColumnNumber = 640;
            oriRowNumber = 480;
            resizerate = 1.0;
            
        }
        public void CameraFinder()
        {
            try
            {
                baslerCamera = new BaslerClass();
                CheckCameraDone();
            }
            catch (Exception ex)
            {
               // MessageBox.Show(ex.Message);
                cameraCheck = new Thread(cCheck);
                cameraCheck.IsBackground = true;
                cameraCheck.Start();
            }

        }
        public void CheckCameraDone()
        {
            this.Invoke(new CCameraDone(CCameraDone_F));

        }
        delegate void CCameraDone();
        void CCameraDone_F()
        {
            try
            {
                Cursor = Cursors.Default;
                Svision.GetMe().Enabled = true;
                textBoxIsCameraOpenedAndConnected.Text = "是";
                Svision.GetMe().listBoxProcess.SelectedIndex = UserCode.GetInstance().showCurIdx;
                //baslerCamera.stopGrab();
                System.Threading.Thread.Sleep(50);
                ConfigInformation.GetInstance().tCamCfg.xOffset = 0;
                baslerCamera.setOffsetX(0);
                ConfigInformation.GetInstance().tCamCfg.yOffset = 0;
                baslerCamera.setOffsetY(0);
                ConfigInformation.GetInstance().tCamCfg.rows = 480;
                baslerCamera.setRowNumber(480);
                rowNumber = 480;
                oriRowNumber = 480;
                ConfigInformation.GetInstance().tCamCfg.columns = 640;
                baslerCamera.setColumnNumber(640);
                oriColumnNumber = 640;
                columnNumber = 480;
                ConfigInformation.GetInstance().tCamCfg.channelNum = 1;
                baslerCamera.setChannelNumber(1);
                ConfigInformation.GetInstance().tCamCfg.gammaPercent = 0.3;//Gamma百分数;
                baslerCamera.setGammaPercent(0.3);
                ConfigInformation.GetInstance().tCamCfg.isExposureAuto = false;
                baslerCamera.setExposureAuto(false);
                ConfigInformation.GetInstance().tCamCfg.exposurePercent = 0.002;//曝光百分数;
                baslerCamera.setExposurePercent(0.002);
                ConfigInformation.GetInstance().tCamCfg.isGainAuto = false;
                baslerCamera.setGainAuto(false);
                ConfigInformation.GetInstance().tCamCfg.gainPercent = 0.5;//增益百分数;
                baslerCamera.setGainPercent(0.5);
                ConfigInformation.GetInstance().tCamCfg.isWhiteBalanceAuto = false;
                baslerCamera.setWhiteBalanceAuto(false);
                ConfigInformation.GetInstance().tCamCfg.whiteBalanceBluePercent = 0.2;//白平衡百分数（蓝）;
                baslerCamera.setWhiteBalanceBlue(0.2);
                ConfigInformation.GetInstance().tCamCfg.whiteBalanceGreenPercent = 0.2;//白平衡百分数（绿）;
                baslerCamera.setWhiteBalanceGreen(0.2);
                ConfigInformation.GetInstance().tCamCfg.whiteBalanceRedPercent = 0.2;//白平衡百分数（红）;
                baslerCamera.setWhiteBalanceRed(0.2);
                //baslerCamera.startGrab();
                ConfigInformation.GetInstance().tSysCfg.loadSysInfoCfg();
                if (ConfigInformation.GetInstance().tSysCfg.isBoot)
                {
                    
                    try
                    {
                        OpenBootProject();

                        WindowState = FormWindowState.Maximized;
                        this.AutoRun_Click(this, new EventArgs());
                    }
                    catch (System.Exception ex)
                    {
                        throw new Exception(ex.Message);

                    }
                }
                baslerCamera.startGrab();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("BOOT程序导入失败！请检查！" + ex.Message);
                baslerCamera.startGrab();
            }

        }

        public void cCheck()
        {
            while (!cCheckFlag)
            {
                try
                {
                    baslerCamera = new BaslerClass();
                }
                catch (Exception ex)
                {

                }
                if (baslerCamera != null)
                {
                    CheckCameraDone();
                    cCheckFlag = true;
                }
            }
        }
        ~Svision()
        {
            cCheckFlag = true;
            baslerCamera.closeCamera();
            HOperatorSet.ClearAllShapeModels();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            //if (HDevWindowStack.IsOpen())
            //    HOperatorSet.CloseWindow(HDevWindowStack.Pop());
            HOperatorSet.OpenWindow(0, 0, (pictureBoxShowImage.Width), (pictureBoxShowImage.Height), pictureBoxShowImage.Handle, "visible", "", out hvWindowHandle);
            HOperatorSet.SetPart(hvWindowHandle, 0, 0, (pictureBoxShowImage.Height), (pictureBoxShowImage.Width));
            HDevWindowStack.Push(hvWindowHandle);
            oriPictureBoxShowImageWidth = pictureBoxShowImage.Width;
            oriPictureBoxShowImageHeight = pictureBoxShowImage.Height;
            Cursor = Cursors.WaitCursor;
           // System.Threading.Thread.Sleep(3000);
            HTuple hv_FileHandle, cameraID,hv_IsEOF;
            if (File.Exists(System.Environment.CurrentDirectory + "/camera.dat"))
            {
                HOperatorSet.OpenFile(System.Environment.CurrentDirectory + "/camera.dat", "input", out hv_FileHandle);
                HOperatorSet.FreadString(hv_FileHandle, out cameraID, out hv_IsEOF);
                if (hv_IsEOF[0].I==0)
                {
                    cameraStr=(string)cameraID;
                    if (cameraStr.Length>8)
                    {
                        cameraStr = cameraStr.Remove(8);
                    }
                   
                }
            }
            if (cameraStr!=null)
            {
                Svision.GetMe().Text = "Svision.Camera ID" + cameraStr;
            }
            
            CameraFinder();
            
        }
        public void ShowTCPMessage()
        {
            this.Invoke(new MessageBoxShowTCP(MessageBoxShow_TCP));
        }
        delegate void MessageBoxShowTCP( );
        void MessageBoxShow_TCP( )
        {
            Svision.GetMe().textBoxTCPShow.Text = Svision.GetMe().tcpStr;
        }
        public void StopAutoRunAndShowMessage(string msg)
        {
            this.Invoke(new MessageBoxShow(MessageBoxShow_F), new object[] { msg });

        }
        delegate void MessageBoxShow(string msg);
        void MessageBoxShow_F(string msg)
        {
            System.Threading.Thread.Sleep(100);
            if (baslerCamera != null && baslerCamera.camera.IsOpen && baslerCamera.camera.IsConnected)
            {
                textBoxIsCameraOpenedAndConnected.Text = "是";
                Menu.Enabled = true;
                ProcessEdit.Enabled = true;
                ShowResults.Enabled = true;
                checkBoxIsFile.Enabled = true;
                CCamera.Enabled = true;
                Camera.Enabled = true;
                cameraPara.Enabled = true;
                ComCfg.Enabled = true;
                checkBoxDoNotShowImage.Enabled = true;
                listBoxProcess.Enabled = true;
                this.ControlBox = true;
                AutoRun.Text = "自动运行";
                this.textBoxResultShow.Text = " ";
                this.textBoxTCPShow.Text = " ";
                this.textBoxTime.Text = " ";
                this.textBoxShowTime.Text = " ";
                HOperatorSet.ClearWindow(this.hvWindowHandle);
                MessageBox.Show(msg, "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {

                Menu.Enabled = true;
                ProcessEdit.Enabled = true;
                    ShowResults.Enabled = true;
                    checkBoxIsFile.Enabled = true;
                    CCamera.Enabled = true;
                    Camera.Enabled = true;
                    cameraPara.Enabled = true;
                    ComCfg.Enabled = true;
                    checkBoxDoNotShowImage.Enabled = true;
                    listBoxProcess.Enabled = true;
                    this.ControlBox = true;
                    AutoRun.Text = "自动运行";
                    this.textBoxResultShow.Text = " ";
                    this.textBoxTCPShow.Text = " ";
                    this.textBoxTime.Text = " ";
                    this.textBoxShowTime.Text = " ";
                    HOperatorSet.ClearWindow(this.hvWindowHandle);
                    textBoxIsCameraOpenedAndConnected.Text = "否";
                    MessageBox.Show("相机连接已断开，请检查连接网线！开始自动刷新相机状态！");
                    textBoxIsCameraOpenedAndConnected.Text = "正在刷新";
                    Svision.GetMe().Enabled = false;
                    cameraRefresh();
                    Svision.GetMe().Enabled = true;
                    textBoxIsCameraOpenedAndConnected.Text = "是";
                   // MessageBox.Show(msg, "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
            }

            
        }

        private void OpenBootProject()
        {
            try
            {
                string localFilePath, fileNameExt;
                localFilePath = ConfigInformation.GetInstance().tSysCfg.bootPath;
                fileNameExt = localFilePath.Substring(localFilePath.LastIndexOf("\\") + 1);
                string fileNameWhole = fileNameExt + ".xml";
                string filePathWhole = localFilePath + "\\" + fileNameWhole;
                FileOperate tFo = new FileOperate();
                tFo.OpenXmlToProj(filePathWhole, localFilePath, fileNameWhole);
            }
            catch (System.Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        private void ReadImageSet_Click(object sender, EventArgs e)
        {
            try
            {
                FileInputSet OP = new FileInputSet();
                OP.ShowDialog();

            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            //OpenFileDialog openimage = new OpenFileDialog();
            //openimage.Filter = "JPEG文件|*.jpg*|BMP文件|*.bmp*|TIFF文件|*.tiff*";
            //openimage.RestoreDirectory = true;
            //openimage.FilterIndex = 1;
            //if (openimage.ShowDialog() == DialogResult.OK)
            //{
            //    ImagePath = openimage.FileName;
            //    basicClass.readImageHobject(out image, ImagePath);
            //    HOperatorSet.DispObj(img, hvWindowHandle);              
            //}
        }

        private void ShowResults_Click(object sender, EventArgs e)
        {
            OutputParameters OP = new OutputParameters();
            OP.ShowDialog();
        }

        private void AutoRun_Click(object sender, EventArgs e)
        {
            //if (oriColumnNumber == ConfigInformation.GetInstance().tCamCfg.columns && oriRowNumber == ConfigInformation.GetInstance().tCamCfg.rows)
            //{
            bool canAutoRunning = true;
            for (int i = 0; i < 20; i++)
            {
                if (UserCode.GetInstance().isOverFlag[i] != 0 && UserCode.GetInstance().isOverFlag[i] != 20 && UserCode.GetInstance().isOverFlag[i] != 21 && UserCode.GetInstance().isOverFlag[i] != 22)
                {
                    if (UserCode.GetInstance().isOverFlag[i] != 32)
                    {
                        switch (UserCode.GetInstance().isOverFlag[i])
                        {
                            case 10:
                                {
                                    canAutoRunning = false;
                                    MessageBox.Show("第" + i.ToString() + "行Blob分析功能块设置工作未完成，无法正常自动运行！请完成设置并确认！");
                                }
                                break;
                            case 11:
                                {
                                    canAutoRunning = false;
                                    MessageBox.Show("第" + i.ToString() + "行形状搜索功能块设置工作未完成，无法正常自动运行！请完成设置并确认！");
                                }
                                break;
                            case 12:
                                {
                                    canAutoRunning = false;
                                    MessageBox.Show("第" + i.ToString() + "行可变形状搜索功能块设置工作未完成，无法正常自动运行！请完成设置并确认！");
                                }
                                break;
                            case 31:
                                {
                                    canAutoRunning = false;
                                    MessageBox.Show("第" + i.ToString() + "行串行输出功能块设置工作未完成，无法正常自动运行！请完成设置并确认！");
                                }
                                break;
                            default:
                                break;
                        }
                    }
                    else
                    {
                        if (UserCode.GetInstance().isOverFlag[UserCode.GetInstance().gProCd[i].gSOP.sendDataInfoList[0].row] != 20
                            && UserCode.GetInstance().isOverFlag[UserCode.GetInstance().gProCd[i].gSOP.sendDataInfoList[0].row] != 21
                             && UserCode.GetInstance().isOverFlag[UserCode.GetInstance().gProCd[i].gSOP.sendDataInfoList[0].row] != 22)
                        {
                            canAutoRunning = false;
                            MessageBox.Show("第" + UserCode.GetInstance().gProCd[i].gSOP.sendDataInfoList[0].row.ToString() + "行功能块设置工作未完成或不存在，无法正常自动运行！");
                        }
                        else
                        {
                            switch ((ProCodeCls.MainFunction)UserCode.GetInstance().gProCd[i].gSOP.sendDataInfoList[0].funcID)
                            {
                                case ProCodeCls.MainFunction.MeasureBlobAnalysisFBD:
                                    {
                                        if (UserCode.GetInstance().isOverFlag[UserCode.GetInstance().gProCd[i].gSOP.sendDataInfoList[0].row] != 20)
                                        {
                                            canAutoRunning = false;
                                            MessageBox.Show("第" + UserCode.GetInstance().gProCd[i].gSOP.sendDataInfoList[0].row.ToString() +
                                                "行功能块设置内容与串行输出模块设置不一致,无法正常自动运行！请打开作业编辑中相应串行输出模块更新程序并确认!");
                                        }
                                        else
                                        {
                                            for (int j = 0; j < UserCode.GetInstance().gProCd[i].gSOP.sendDataInfoList.Count; j++)
                                            {
                                                if (UserCode.GetInstance().gProCd[i].gSOP.sendDataInfoList[j].idx >=
                                                    UserCode.GetInstance().gProCd[UserCode.GetInstance().gProCd[i].gSOP.sendDataInfoList[0].row].gBP.regionNum
                                                    * UserCode.GetInstance().gProCd[UserCode.GetInstance().gProCd[i].gSOP.sendDataInfoList[0].row].gBP.selectItemCount + 1)
                                                {
                                                    canAutoRunning = false;
                                                }
                                            }
                                            if (canAutoRunning == false)
                                            {
                                                MessageBox.Show("第" + UserCode.GetInstance().gProCd[i].gSOP.sendDataInfoList[0].row.ToString() + 
                                                    "行功能块设置内容与串行输出模块设置不一致,无法正常自动运行！请打开作业编辑中相应串行输出模块更新程序并确认!");
                                            }
                                        }
                                    }
                                    break;
                                case ProCodeCls.MainFunction.MeasureShapeSearchFBD:
                                    {
                                        if (UserCode.GetInstance().isOverFlag[UserCode.GetInstance().gProCd[i].gSOP.sendDataInfoList[0].row] != 21)
                                        {
                                            canAutoRunning = false;
                                            MessageBox.Show("第" + UserCode.GetInstance().gProCd[i].gSOP.sendDataInfoList[0].row.ToString() +
                                                "行功能块设置内容与串行输出模块设置不一致,无法正常自动运行！请打开作业编辑中相应串行输出模块更新程序并确认!");
                                        }
                                        else
                                        {
                                            for (int j = 0; j < UserCode.GetInstance().gProCd[i].gSOP.sendDataInfoList.Count; j++)
                                            {
                                                if (UserCode.GetInstance().gProCd[i].gSOP.sendDataInfoList[j].idx >=
                                                    UserCode.GetInstance().gProCd[UserCode.GetInstance().gProCd[i].gSOP.sendDataInfoList[0].row].gSSP.Max_Object_Num * 5 + 1)
                                                {
                                                    canAutoRunning = false;
                                                }
                                            }
                                            if (canAutoRunning == false)
                                            {
                                                MessageBox.Show("第" + UserCode.GetInstance().gProCd[i].gSOP.sendDataInfoList[0].row.ToString() +
                                                    "行功能块设置内容与串行输出模块设置不一致,无法正常自动运行！请打开作业编辑中相应串行输出模块更新程序并确认!");
                                            }
                                        }
                                    }
                                    break;
                                case ProCodeCls.MainFunction.MeasureAnisoShapeSearchFBD:
                                    {
                                        if (UserCode.GetInstance().isOverFlag[UserCode.GetInstance().gProCd[i].gSOP.sendDataInfoList[0].row] != 22)
                                        {
                                            canAutoRunning = false;
                                            MessageBox.Show("第" + UserCode.GetInstance().gProCd[i].gSOP.sendDataInfoList[0].row.ToString() +
                                                "行功能块设置内容与串行输出模块设置不一致,无法正常自动运行！请打开作业编辑中相应串行输出模块更新程序并确认!");
                                        }
                                        else
                                        {
                                            for (int j = 0; j < UserCode.GetInstance().gProCd[i].gSOP.sendDataInfoList.Count; j++)
                                            {
                                                if (UserCode.GetInstance().gProCd[i].gSOP.sendDataInfoList[j].idx >=
                                                    UserCode.GetInstance().gProCd[UserCode.GetInstance().gProCd[i].gSOP.sendDataInfoList[0].row].gASSP.Max_Object_Num * 7 + 1)
                                                {
                                                    canAutoRunning = false;
                                                }
                                            }
                                            if (canAutoRunning == false)
                                            {
                                                MessageBox.Show("第" + UserCode.GetInstance().gProCd[i].gSOP.sendDataInfoList[0].row.ToString() +
                                                    "行功能块设置内容与串行输出模块设置不一致,无法正常自动运行！请打开作业编辑中相应串行输出模块更新程序并确认!");
                                            }
                                        }
                                    }
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                }
            }
            if (canAutoRunning)
            {
                try
                {
                    if (ConfigInformation.GetInstance().tComCfg.TrigMode == CComConfig.TrigModeEnum.TIME_TRIG)
                    {
                        if (UserCode.GetInstance().tRun)
                        {
                            
                            UserCode.GetInstance().stopThread();
                            this.AutoRun.Text = "自动运行";
                            UserCode.GetInstance().firstRun = true;
                            Menu.Enabled = true;
                            ProcessEdit.Enabled = true;
                            ShowResults.Enabled = true;
                            checkBoxIsFile.Enabled = true;
                            CCamera.Enabled = true;
                            Camera.Enabled = true;
                            cameraPara.Enabled = true;
                            ComCfg.Enabled = true;
                            checkBoxDoNotShowImage.Enabled = true;
                            listBoxProcess.Enabled = true;
                            this.ControlBox = true;

                        }
                        else
                        {
                            Menu.Enabled = false;
                            ProcessEdit.Enabled = false;
                            ShowResults.Enabled = false;
                            checkBoxIsFile.Enabled = false;
                            CCamera.Enabled = false;
                            Camera.Enabled = false;
                            cameraPara.Enabled = false;
                            ComCfg.Enabled = false;
                            checkBoxDoNotShowImage.Enabled = false;
                            listBoxProcess.Enabled = false;
                            this.ControlBox = false;
                            basicClass.displayClear(Svision.GetMe().hvWindowHandle);
                            Svision.GetMe().baslerCamera.startGrab();
                            HObject tempImg;
                            Svision.GetMe().baslerCamera.getFrameImageWithStart(out tempImg);
                            tempImg.Dispose();
                            System.Threading.Thread.Sleep(1000);
                            this.AutoRun.Text = "停止运行";
                            UserCode.GetInstance().startThread();

                        }
                    }
                    else if (ConfigInformation.GetInstance().tComCfg.TrigMode == CComConfig.TrigModeEnum.COM_TRIG)
                    {
                        if (gAsyncServer == null)
                        {
                            Svision.GetMe().baslerCamera.startGrab();
                            HObject tempImg;
                            Svision.GetMe().baslerCamera.getFrameImageWithStart(out tempImg);
                            tempImg.Dispose();
                            System.Threading.Thread.Sleep(1000);
                            basicClass.displayClear(Svision.GetMe().hvWindowHandle);
                            UserCode.GetInstance().secondsCommunication1 = 0;
                            UserCode.GetInstance().secondsCommunication2 = 0;
                            byte[] ipadd = new byte[4];
                            for (int i = 0; i < 4; i++)
                            {
                                ipadd[i] = byte.Parse(ConfigInformation.GetInstance().tComCfg.TCPLocalIP.Split('.')[i]);
                            }
                            IPAddress t_ip_add = new IPAddress(ipadd);
                            gAsyncServer = new AsyncServer(t_ip_add, int.Parse(ConfigInformation.GetInstance().tComCfg.TCPLocalPort), 1);
                            gAsyncServer.DataReceived += new EventHandler<EventArgs>(gAsyncServer_DataReceived);
                            gAsyncServer.Start(1);
                            this.AutoRun.Text = "停止运行";
                            Svision.GetMe().tcpStr = "服务器已开启" + DateTime.Now.ToLongTimeString().ToString() + System.Environment.NewLine;
                            Svision.GetMe().ShowTCPMessage();
                            Menu.Enabled = false;
                            ProcessEdit.Enabled = false;
                            ShowResults.Enabled = false;
                            checkBoxIsFile.Enabled = false;
                            CCamera.Enabled = false;
                            Camera.Enabled = false;
                            cameraPara.Enabled = false;
                            ComCfg.Enabled = false;
                            checkBoxDoNotShowImage.Enabled = false;
                            listBoxProcess.Enabled = false;
                            this.ControlBox = false;
                        }
                        else
                        {
                           
                            gAsyncServer.Stop();
                            gAsyncServer.CloseAllClient();
                            gAsyncServer.Dispose();
                            this.AutoRun.Text = "自动运行";
                            UserCode.GetInstance().firstRun = true;
                            Menu.Enabled = true;
                            ProcessEdit.Enabled = true;
                            ShowResults.Enabled = true;
                            checkBoxIsFile.Enabled = true;
                            CCamera.Enabled = true;
                            Camera.Enabled = true;
                            cameraPara.Enabled = true;
                            ComCfg.Enabled = true;
                            checkBoxDoNotShowImage.Enabled = true;
                            listBoxProcess.Enabled = true;
                            this.ControlBox = true;
                            gAsyncServer = null;
                            UserCode.GetInstance().secondsCommunication1 = 0;
                            UserCode.GetInstance().secondsCommunication2 = 0;
                            GC.Collect();
                            Svision.GetMe().tcpStr = "服务器已关闭" + DateTime.Now.ToLongTimeString().ToString() + System.Environment.NewLine;
                            Svision.GetMe().ShowTCPMessage();

                        }
                    }
                }
                catch (System.Exception ex)
                {
                    if (ConfigInformation.GetInstance().tComCfg.TrigMode == CComConfig.TrigModeEnum.TIME_TRIG)
                    {
                        UserCode.GetInstance().stopThread();
                    }
                    else if (ConfigInformation.GetInstance().tComCfg.TrigMode == CComConfig.TrigModeEnum.COM_TRIG)
                    {                        
                        UserCode.GetInstance().secondsCommunication1 = 0;
                        UserCode.GetInstance().secondsCommunication2 = 0;
                        if (gAsyncServer!=null)
                        {
                            gAsyncServer.Stop();
                            gAsyncServer.CloseAllClient();
                            gAsyncServer.Dispose();
                        }
                        gAsyncServer = null;
                        GC.Collect();                        
                        Svision.GetMe().tcpStr = "服务器已关闭" + DateTime.Now.ToLongTimeString().ToString() + System.Environment.NewLine;
                        Svision.GetMe().ShowTCPMessage();

                    }
                    UserCode.GetInstance().firstRun = true;
                    UserCode.GetInstance().importantFlagIsRunning = false;
                    StopAutoRunAndShowMessage(ex.Message);
                    
                }
            }
                



        }

        public void gAsyncServer_DataReceived(object sender, EventArgs e)
        {
            try
            {
                //HOperatorSet.CountSeconds(out UserCode.GetInstance().secondsCommunication2);
                //UserCode.GetInstance().secondsTotalCommunication = UserCode.GetInstance().secondsCommunication2 - UserCode.GetInstance().secondsCommunication1;
                Session tSession = (Session)sender;
                byte[] tRecvBuf;
                if (tSession.recvLen > 0)
                {
                    tRecvBuf = new byte[tSession.recvLen - 1];
                    for (int i = 0; i < tSession.recvLen - 1; i++)
                    {
                        tRecvBuf[i] = tSession.RecvDataBuffer[i];
                    }
                    string tRecvStr = System.Text.Encoding.Default.GetString(tRecvBuf);
                    
                    if (ConfigInformation.GetInstance().tComCfg.TrigStr == tRecvStr)
                    {

                        if (UserCode.GetInstance().importantFlagIsRunning == true)
                        {
                            throw new Exception("通讯触发周期过短，短于处理及显示时间之和，请重新设置!");
                        }
                        UserCode.GetInstance().mainCodeRun();

                        if (gAsyncServer.SocketConnected(tSession.ClientSocket))
                        {
                            if (UserCode.GetInstance().OKNG)
                            {
                                gAsyncServer.Send(tSession, System.Text.Encoding.Default.GetBytes("OK\n"));
                               // System.Threading.Thread.Sleep(5);
                                gAsyncServer.Send(tSession, System.Text.Encoding.Default.GetBytes(UserCode.GetInstance().SendBuf));
                            }
                            else
                            {
                                gAsyncServer.Send(tSession, System.Text.Encoding.Default.GetBytes("NG\n"));
                            }
                        }
                        UserCode.GetInstance().SendBuf = "";
                        UserCode.GetInstance().cBufferLength = 0;
                    }
                    else if (tRecvStr.StartsWith(ConfigInformation.GetInstance().tComCfg.TrigStr+"FS"))
                    {
                        if (UserCode.GetInstance().importantFlagIsRunning == true)
                        {
                            if (gAsyncServer.SocketConnected(tSession.ClientSocket))
                            {
                                gAsyncServer.Send(tSession, System.Text.Encoding.Default.GetBytes("NG0002\n"));
                            }
                        }
                        else
                        {
                            string[] tRecvStrSp = tRecvStr.Split('.');
                            int[] tum=new int[tRecvStrSp.Length-1];
                            try
                            {
                                for (int ii = 1; ii < tRecvStrSp.Length; ii++)
                                {
                                    tum[ii-1] = int.Parse(tRecvStrSp[ii]);
                                }

                            }
                            catch (System.Exception ex)
                            {
                                if (gAsyncServer.SocketConnected(tSession.ClientSocket))
                                {
                                    gAsyncServer.Send(tSession, System.Text.Encoding.Default.GetBytes("NG0003\n"));
                                }
                                return;
                            }
                            if (tum.Length<2)
                            {
                                if (gAsyncServer.SocketConnected(tSession.ClientSocket))
                                {
                                    gAsyncServer.Send(tSession, System.Text.Encoding.Default.GetBytes("NG0003\n"));
                                }
                                return;
                            }
                            if (UserCode.GetInstance().isOverFlag[tum[0]]==21)
                            {
                                bool fsFlag = true;
                                for (int ii = 1; ii < tum.Length;ii++ )
                                {
                                    if (UserCode.GetInstance().gProCd[tum[0]].gSSP.shapeModel.Length<=tum[ii])
                                    {
                                        fsFlag = false;
                                    }
                                }
                                if (fsFlag)
                                {
                                    try
                                    {
                                        UserCode.GetInstance().changeModelCodeRun(tum);
                                    }
                                    catch (System.Exception ex)
                                    {
                                        if (gAsyncServer.SocketConnected(tSession.ClientSocket))
                                        {
                                            gAsyncServer.Send(tSession, System.Text.Encoding.Default.GetBytes("NG0005"+ex+"\n"));
                                        }
                                        return;
                                    }
                                    if (gAsyncServer.SocketConnected(tSession.ClientSocket))
                                    {
                                        gAsyncServer.Send(tSession, System.Text.Encoding.Default.GetBytes("OKOVER\n"));
                                    }
                                } 
                                else
                                {
                                    if (gAsyncServer.SocketConnected(tSession.ClientSocket))
                                    {
                                        gAsyncServer.Send(tSession, System.Text.Encoding.Default.GetBytes("NG0001\n"));
                                    }
                                }
                            } 
                            else
                            {
                                if (gAsyncServer.SocketConnected(tSession.ClientSocket))
                                {
                                    gAsyncServer.Send(tSession, System.Text.Encoding.Default.GetBytes("NG0000\n"));
                                }
                            }
                            
                        }
                    }
                    else if (tRecvStr.StartsWith(ConfigInformation.GetInstance().tComCfg.TrigStr + "FAS"))
                    {
                        if (UserCode.GetInstance().importantFlagIsRunning == true)
                        {
                            if (gAsyncServer.SocketConnected(tSession.ClientSocket))
                            {
                                gAsyncServer.Send(tSession, System.Text.Encoding.Default.GetBytes("NG0002\n"));
                            }
                        }
                        else
                        {
                            string[] tRecvStrSp = tRecvStr.Split('.');
                            int[] tum = new int[tRecvStrSp.Length - 1];
                            try
                            {
                                for (int ii = 1; ii < tRecvStrSp.Length; ii++)
                                {
                                    tum[ii - 1] = int.Parse(tRecvStrSp[ii]);
                                }

                            }
                            catch (System.Exception ex)
                            {
                                if (gAsyncServer.SocketConnected(tSession.ClientSocket))
                                {
                                    gAsyncServer.Send(tSession, System.Text.Encoding.Default.GetBytes("NG0003\n"));
                                }
                                return;
                            }
                            if (tum.Length < 2)
                            {
                                if (gAsyncServer.SocketConnected(tSession.ClientSocket))
                                {
                                    gAsyncServer.Send(tSession, System.Text.Encoding.Default.GetBytes("NG0003\n"));
                                }
                                return;
                            }
                            if (UserCode.GetInstance().isOverFlag[tum[0]] == 22)
                            {
                                bool fsFlag = true;
                                for (int ii = 1; ii < tum.Length; ii++)
                                {
                                    if (UserCode.GetInstance().gProCd[tum[0]].gASSP.shapeModel.Length <= tum[ii])
                                    {
                                        fsFlag = false;
                                    }
                                }
                                if (fsFlag)
                                {
                                    try
                                    {
                                        UserCode.GetInstance().changeAnisoModelCodeRun(tum);
                                    }
                                    catch (System.Exception ex)
                                    {
                                        if (gAsyncServer.SocketConnected(tSession.ClientSocket))
                                        {
                                            gAsyncServer.Send(tSession, System.Text.Encoding.Default.GetBytes("NG0005"+ex+"\n"));
                                        }
                                        return;
                                    }
                                    if (gAsyncServer.SocketConnected(tSession.ClientSocket))
                                    {
                                        gAsyncServer.Send(tSession, System.Text.Encoding.Default.GetBytes("OKOVER\n"));
                                    }
                                }
                                else
                                {
                                    if (gAsyncServer.SocketConnected(tSession.ClientSocket))
                                    {
                                        gAsyncServer.Send(tSession, System.Text.Encoding.Default.GetBytes("NG0001\n"));
                                    }
                                }
                            }
                            else
                            {
                                if (gAsyncServer.SocketConnected(tSession.ClientSocket))
                                {
                                    gAsyncServer.Send(tSession, System.Text.Encoding.Default.GetBytes("NG0000\n"));
                                }
                            }
                        }
                    }
                    else
                    {
                        if (gAsyncServer.SocketConnected(tSession.ClientSocket))
                        {
                            gAsyncServer.Send(tSession, System.Text.Encoding.Default.GetBytes("NG0004\n"));
                        }
                    }
                }
                //UserCode.GetInstance().secondsCommunication1 = UserCode.GetInstance().secondsCommunication2;
            }
            catch (System.Net.Sockets.SocketException exx)
            {
                if (exx.ErrorCode == 10053)
                {
                }
                UserCode.GetInstance().importantFlagIsRunning = false;
            }
            catch (System.Exception ex)
            {
                

                UserCode.GetInstance().firstRun = true;
                UserCode.GetInstance().secondsCommunication1 = 0;
                UserCode.GetInstance().secondsCommunication2 = 0;
                gAsyncServer.Stop();
                gAsyncServer.CloseAllClient();
                gAsyncServer.Dispose();
                //baslerCamera.stopGrab();
                UserCode.GetInstance().importantFlagIsRunning = false;
                StopAutoRunAndShowMessage(ex.Message);
                gAsyncServer = null;
                GC.Collect();
                Svision.GetMe().tcpStr = "服务器已关闭" + DateTime.Now.ToLongTimeString().ToString() + System.Environment.NewLine;
                Svision.GetMe().ShowTCPMessage();
            }
            finally
            {
                UserCode.GetInstance().importantFlagIsRunning = false;
            }
        }

        private void Camera_Click(object sender, EventArgs e)
        {
            if (baslerCamera != null && baslerCamera.camera.IsOpen && baslerCamera.camera.IsConnected)
            {
                try
                {

                    textBoxIsCameraOpenedAndConnected.Text = "是";
                    if (image != null)
                    {
                        image.Dispose();
                    }
                    baslerCamera.getFrameImageWithStart(out image);
                    if (ConfigInformation.GetInstance().tCalCfg.calibrationIsRadialDistortionFlag)
                    {
                        HOperatorSet.MapImage(image, ConfigInformation.GetInstance().tCalCfg.ho_MapFixed, out image);
                    }
                    basicClass.getImageSize(image, out rowNumber, out columnNumber);
                    double widRat = pictureBoxShowImage.Width / ((double)columnNumber);
                    double heiRat = pictureBoxShowImage.Height / ((double)rowNumber);
                    resizerate = widRat < heiRat ? widRat : heiRat;
                    if (img != null)
                    {
                        img.Dispose();
                    }
                    basicClass.resizeImage(image, out img, resizerate);
                    if (oriPictureBoxShowImageWidth != pictureBoxShowImage.Width)
                    {
                        basicClass.displayClear(hvWindowHandle);
                    }
                    if (oriPictureBoxShowImageHeight != pictureBoxShowImage.Height)
                    {
                        basicClass.displayClear(hvWindowHandle);
                    }
                    basicClass.displayhobject(img, hvWindowHandle);

                }
                catch (Exception ex)
                {
                    System.Threading.Thread.Sleep(100);
                    if (baslerCamera != null && baslerCamera.camera.IsOpen && baslerCamera.camera.IsConnected)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    else
                    {
                        textBoxIsCameraOpenedAndConnected.Text = "否";
                        MessageBox.Show("相机连接已断开，请检查连接网线！开始自动刷新相机状态！");
                        textBoxIsCameraOpenedAndConnected.Text = "正在刷新";
                        Svision.GetMe().Enabled = false;
                        cameraRefresh();
                        Svision.GetMe().Enabled = true;
                        textBoxIsCameraOpenedAndConnected.Text = "是";
                    }
                    
                }
            }
            else
            {
                textBoxIsCameraOpenedAndConnected.Text = "否";                
                MessageBox.Show("相机连接已断开，请检查连接网线！开始自动刷新相机状态！");
                textBoxIsCameraOpenedAndConnected.Text = "正在刷新";  
                Svision.GetMe().Enabled = false;
                cameraRefresh();
                Svision.GetMe().Enabled = true;
                textBoxIsCameraOpenedAndConnected.Text = "是";

            }
        }
        public void cameraRefresh()
        {


            while (!(baslerCamera != null && baslerCamera.camera.IsOpen && baslerCamera.camera.IsConnected))
            {
                try
                {
                    
                    baslerCamera.closeCamera();
                    baslerCamera.refreshCamera();
                }
                catch (Exception ex)
                {

                }
                
            }
            baslerCamera.startGrab(); 

              
        }
        private void ProcessEdit_Click(object sender, EventArgs e)
        {
            /*
            ProcessEdit Pro = new ProcessEdit();
            Pro.Show();
            */
            NewProcessEdit Pro = new NewProcessEdit();
            Pro.ShowDialog();
        }
        private void CCameratimer_Tick(object sender, EventArgs e)
        {
            if (baslerCamera != null && baslerCamera.camera.IsOpen && baslerCamera.camera.IsConnected)
            {
                textBoxIsCameraOpenedAndConnected.Text = "是";
                try
                {
                    if (image != null)
                    {
                        image.Dispose();
                    }
                    baslerCamera.getFrameImageWithStart(out image);
                    if (ConfigInformation.GetInstance().tCalCfg.calibrationIsRadialDistortionFlag)
                    {
                        HOperatorSet.MapImage(image, ConfigInformation.GetInstance().tCalCfg.ho_MapFixed, out image);
                    }
                    basicClass.getImageSize(image, out rowNumber, out columnNumber);
                    double widRat = pictureBoxShowImage.Width / ((double)columnNumber);
                    double heiRat = pictureBoxShowImage.Height / ((double)rowNumber);
                    resizerate = widRat < heiRat ? widRat : heiRat;
                    if (img != null)
                    {
                        img.Dispose();
                    }
                    basicClass.resizeImage(image, out img, resizerate);
                    if (oriPictureBoxShowImageWidth != pictureBoxShowImage.Width)
                    {
                        basicClass.displayClear(hvWindowHandle);
                    }
                    if (oriPictureBoxShowImageHeight != pictureBoxShowImage.Height)
                    {
                        basicClass.displayClear(hvWindowHandle);
                    }
                    basicClass.displayhobject(img, hvWindowHandle);
                }
                catch (Exception ex)
                {
                    CCameratimer.Enabled = false;
                    System.Threading.Thread.Sleep(100);
                    if (baslerCamera != null && baslerCamera.camera.IsOpen && baslerCamera.camera.IsConnected)
                    {
                        
                        Menu.Enabled = true;
                        ProcessEdit.Enabled = true;
                        ShowResults.Enabled = true;
                        checkBoxIsFile.Enabled = true;
                        AutoRun.Enabled = true;
                        Camera.Enabled = true;
                        cameraPara.Enabled = true;
                        ComCfg.Enabled = true;
                        checkBoxDoNotShowImage.Enabled = true;
                        CCamera.Text = "实时采集";
                        MessageBox.Show(ex.Message);
                    }
                    else
                    {
                        Menu.Enabled = true;
                        ProcessEdit.Enabled = true;
                        ShowResults.Enabled = true;
                        checkBoxIsFile.Enabled = true;
                        AutoRun.Enabled = true;
                        Camera.Enabled = true;
                        cameraPara.Enabled = true;
                        ComCfg.Enabled = true;
                        checkBoxDoNotShowImage.Enabled = true;
                        CCamera.Text = "实时采集";
                        textBoxIsCameraOpenedAndConnected.Text = "否";
                        MessageBox.Show("相机连接已断开，请检查连接网线！开始自动刷新相机状态！");
                        textBoxIsCameraOpenedAndConnected.Text = "正在刷新";
                        Svision.GetMe().Enabled = false;
                        cameraRefresh();
                        Svision.GetMe().Enabled = true;
                        textBoxIsCameraOpenedAndConnected.Text = "是";
                    }

                }
            }
            else
            {
                CCameratimer.Enabled = false;
                Menu.Enabled = true;
                ProcessEdit.Enabled = true;
                ShowResults.Enabled = true;
                checkBoxIsFile.Enabled = true;
                AutoRun.Enabled = true;
                Camera.Enabled = true;
                cameraPara.Enabled = true;
                ComCfg.Enabled = true;
                checkBoxDoNotShowImage.Enabled = true;                
                CCamera.Text = "实时采集";
                textBoxIsCameraOpenedAndConnected.Text = "否";
                MessageBox.Show("相机连接已断开，请检查连接网线！开始自动刷新相机状态！");
                textBoxIsCameraOpenedAndConnected.Text = "正在刷新";
                Svision.GetMe().Enabled = false;
                cameraRefresh();
                Svision.GetMe().Enabled = true;
                textBoxIsCameraOpenedAndConnected.Text = "是";
            }

        }

        private void CCamera_Click(object sender, EventArgs e)
        {
            if (CCameratimer.Enabled == false)
            {
                Menu.Enabled = false;
                ProcessEdit.Enabled = false;
                ShowResults.Enabled = false;
                checkBoxIsFile.Enabled = false;
                AutoRun.Enabled = false;
                Camera.Enabled = false;
                cameraPara.Enabled = false;
                ComCfg.Enabled = false;
                checkBoxDoNotShowImage.Enabled = false;
                textBoxTime.Text = " ";
                CCameratimer.Enabled = true;
                CCamera.Text = "停止采集";
            }
            else
            {
                Menu.Enabled = true;
                ProcessEdit.Enabled = true;
                ShowResults.Enabled = true;
                checkBoxIsFile.Enabled = true;
                AutoRun.Enabled = true;
                Camera.Enabled = true;
                cameraPara.Enabled = true;
                ComCfg.Enabled = true;
                checkBoxDoNotShowImage.Enabled = true;
                CCameratimer.Enabled = false;
                CCamera.Text = "实时采集";
            }

        }

        private void cameraPara_Click(object sender, EventArgs e)
        {
            try
            {
                if (baslerCamera != null && baslerCamera.camera.IsOpen && baslerCamera.camera.IsConnected)
                {
                    baslerCamera.stopGrab();
                    System.Threading.Thread.Sleep(50);
                    CameraParaSet Pro = new CameraParaSet();
                    textBoxIsCameraOpenedAndConnected.Text = "是";
                    Pro.ShowDialog();
                    baslerCamera.startGrab();
                    basicClass.displayClear(hvWindowHandle);
                    Pro.cameraParaTimer.Enabled = false;

                    if (ConfigInformation.GetInstance().tCalCfg.ho_MapFixed != null)
                    {
                        HTuple width1, height1;
                        HOperatorSet.GetImageSize(ConfigInformation.GetInstance().tCalCfg.ho_MapFixed, out width1, out height1);
                        if (width1[0].D != oriColumnNumber || height1[0].D != oriRowNumber)
                        {
                            if (ConfigInformation.GetInstance().tCalCfg.calibrationIsRadialDistortionFlag == true)
                            {
                                ConfigInformation.GetInstance().tCalCfg.isCheckBoxRadialDistortion = false;
                                ConfigInformation.GetInstance().tCalCfg.calibrationIsRadialDistortionFlag = false;
                                MessageBox.Show("警告：由于当前图像行列数值改变，导致其与校正镜头畸变时图像尺寸不符，因此镜头畸变校正模块设置已清空，若需要可重新设置！");
                            }
                        }
                    }
                }
                else
                {
                    textBoxIsCameraOpenedAndConnected.Text = "否";
                    MessageBox.Show("相机连接已断开，请检查连接网线！开始自动刷新相机状态！");
                    textBoxIsCameraOpenedAndConnected.Text = "正在刷新";
                    Svision.GetMe().Enabled = false;
                    cameraRefresh();
                    Svision.GetMe().Enabled = true;
                    textBoxIsCameraOpenedAndConnected.Text = "是";
                }
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
                    textBoxIsCameraOpenedAndConnected.Text = "否";
                    MessageBox.Show("相机连接已断开，请检查连接网线！开始自动刷新相机状态！");
                    textBoxIsCameraOpenedAndConnected.Text = "正在刷新";
                    Svision.GetMe().Enabled = false;
                    cameraRefresh();
                    Svision.GetMe().Enabled = true;
                    textBoxIsCameraOpenedAndConnected.Text = "是";
                }
            }
            
        }

        private void Menu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
        private void ComCfg_Click(object sender, EventArgs e)
        {
            try
            {
                CommunicationConfig ComCfg = new CommunicationConfig();
                ComCfg.ShowDialog();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void 标定ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (baslerCamera != null && baslerCamera.camera.IsOpen && baslerCamera.camera.IsConnected)
                {
                    textBoxIsCameraOpenedAndConnected.Text = "是";
                    CameraCalibration CamClibt = new CameraCalibration();
                    CamClibt.ShowDialog();
                    //baslerCamera.stopGrab();
                }
                else
                {
                    textBoxIsCameraOpenedAndConnected.Text = "否";
                    MessageBox.Show("相机连接已断开，请检查连接网线！开始自动刷新相机状态！");
                    textBoxIsCameraOpenedAndConnected.Text = "正在刷新";
                    Svision.GetMe().Enabled = false;
                    cameraRefresh();
                    Svision.GetMe().Enabled = true;
                    textBoxIsCameraOpenedAndConnected.Text = "是";
                }
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
                    textBoxIsCameraOpenedAndConnected.Text = "否";
                    MessageBox.Show("相机连接已断开，请检查连接网线！开始自动刷新相机状态！");
                    textBoxIsCameraOpenedAndConnected.Text = "正在刷新";
                    Svision.GetMe().Enabled = false;
                    cameraRefresh();
                    Svision.GetMe().Enabled = true;
                    textBoxIsCameraOpenedAndConnected.Text = "是";
                }
            }
            
         
        }

        private void pictureBoxShowImage_SizeChanged(object sender, EventArgs e)
        {
            try
            {
                if (hvWindowHandle != null)
                {
                    HOperatorSet.SetWindowExtents(hvWindowHandle, 0, 0, (pictureBoxShowImage.Width), (pictureBoxShowImage.Height));
                    HOperatorSet.SetPart(hvWindowHandle, 0, 0, (pictureBoxShowImage.Height), (pictureBoxShowImage.Width));
                    if (image != null)
                    {
                        basicClass.getImageSize(image, out rowNumber, out columnNumber);
                        double widRat = pictureBoxShowImage.Width / ((double)columnNumber);
                        double heiRat = pictureBoxShowImage.Height / ((double)rowNumber);
                        resizerate = widRat < heiRat ? widRat : heiRat;
                        if (img != null)
                        {
                            img.Dispose();
                        }
                        basicClass.resizeImage(image, out img, resizerate);
                        basicClass.displayClear(hvWindowHandle);
                        basicClass.displayhobject(img, hvWindowHandle);

                    }
                    oriPictureBoxShowImageWidth = pictureBoxShowImage.Width;
                    oriPictureBoxShowImageHeight = pictureBoxShowImage.Height;
                }
            }
            catch (System.Exception ex)
            {

            }
            

        }

        private void 新建NToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult result = MessageBox.Show("即将新建空白工程，清空并还原所有默认设置，是否确定？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (result == DialogResult.OK)
                {
                    
                    listBoxProcess.Items.Clear();
                    listBoxProcess.Items.Insert(0, "0.相机输入");
                    for (int i = 1; i < 20; i++)
                    {
                        UserCode.GetInstance().isOverFlag[i] = 0;
                        UserCode.GetInstance().gProCd[i].clear();
                        UserCode.GetInstance().gProCd[i].FuncID = ProCodeCls.MainFunction.NullFBD;
                        UserCode.GetInstance().gProCd[i].inImage = new HObject();
                        UserCode.GetInstance().gProCd[i].outImage = new HObject();
                    }
                    ConfigInformation.GetInstance().tFileCfg.rows = 480;
                    ConfigInformation.GetInstance().tFileCfg.columns = 640;
                    ConfigInformation.GetInstance().tFileCfg.channelNum = 1;
                    UserCode.GetInstance().SendBuf = "";
                    UserCode.GetInstance().cBufferLength = 0;
                    ConfigInformation.GetInstance().tSysCfg.isBoot = false;
                    ConfigInformation.GetInstance().tSysCfg.bootPath = " ";
                    ConfigInformation.GetInstance().tSysCfg.saveSysInfoCfg();
                    ConfigInformation.GetInstance().tComCfg.ComMainType = CComConfig.ComTypeEnum.TCP_SERVER;
                    ConfigInformation.GetInstance().tComCfg.ComTCPServer = true;
                    ConfigInformation.GetInstance().tComCfg.TCPServerIP = "192.168.100.100";
                    ConfigInformation.GetInstance().tComCfg.TCPServerPort = "6789";
                    ConfigInformation.GetInstance().tComCfg.TCPLocalIP = "192.168.100.123";
                    ConfigInformation.GetInstance().tComCfg.TCPLocalPort = "9876";
                    ConfigInformation.GetInstance().tComCfg.TrigMode = CComConfig.TrigModeEnum.COM_TRIG;
                    ConfigInformation.GetInstance().tComCfg.TrigStr = "M";
                    ConfigInformation.GetInstance().tComCfg.TrigTime = 500;
                    baslerCamera.stopGrab();
                    System.Threading.Thread.Sleep(50);
                    ConfigInformation.GetInstance().tCamCfg.xOffset = 0;
                    baslerCamera.setOffsetX(0);
                    ConfigInformation.GetInstance().tCamCfg.yOffset = 0;
                    baslerCamera.setOffsetY(0);
                    ConfigInformation.GetInstance().tCamCfg.rows = 480;
                    baslerCamera.setRowNumber(480);
                    rowNumber = 480;
                    oriRowNumber = 480;
                    ConfigInformation.GetInstance().tCamCfg.columns = 640;
                    baslerCamera.setColumnNumber(640);
                    oriColumnNumber = 640;
                    columnNumber = 480;
                    ConfigInformation.GetInstance().tCamCfg.channelNum = 1;
                    baslerCamera.setChannelNumber(1);                   
                    ConfigInformation.GetInstance().tCamCfg.gammaPercent = 0.3;//Gamma百分数;
                    baslerCamera.setGammaPercent(0.3);                   
                    ConfigInformation.GetInstance().tCamCfg.isExposureAuto = false;
                    baslerCamera.setExposureAuto(false);
                    ConfigInformation.GetInstance().tCamCfg.exposurePercent = 0.002;//曝光百分数;
                    baslerCamera.setExposurePercent(0.002);
                    ConfigInformation.GetInstance().tCamCfg.isGainAuto = false;
                    baslerCamera.setGainAuto(false);
                    ConfigInformation.GetInstance().tCamCfg.gainPercent = 0.5;//增益百分数;
                    baslerCamera.setGainPercent(0.5);
                    ConfigInformation.GetInstance().tCamCfg.isWhiteBalanceAuto = false;
                    baslerCamera.setWhiteBalanceAuto(false);
                    ConfigInformation.GetInstance().tCamCfg.whiteBalanceBluePercent = 0.2;//白平衡百分数（蓝）;
                    baslerCamera.setWhiteBalanceBlue(0.2);
                    ConfigInformation.GetInstance().tCamCfg.whiteBalanceGreenPercent = 0.2;//白平衡百分数（绿）;
                    baslerCamera.setWhiteBalanceGreen(0.2);
                    ConfigInformation.GetInstance().tCamCfg.whiteBalanceRedPercent = 0.2;//白平衡百分数（红）;
                    baslerCamera.setWhiteBalanceRed(0.2);
                    baslerCamera.startGrab();
                    ConfigInformation.GetInstance().tCalCfg.calibrationDistance = 0.014;
                    ConfigInformation.GetInstance().tCalCfg.calibrationPoint = 8;
                    ConfigInformation.GetInstance().tCalCfg.focus = 5;
                    ConfigInformation.GetInstance().tCalCfg.calibrationRate = 0.5;
                    ConfigInformation.GetInstance().tCalCfg.pixelSize = 4.4;
                    ConfigInformation.GetInstance().tCalCfg.isCheckBoxRadialDistortion = false;
                    ConfigInformation.GetInstance().tCalCfg.calibrationParaConfirmFlag = false;
                    ConfigInformation.GetInstance().tCalCfg.calibrationIsRadialDistortionFlag = false;
                    ConfigInformation.GetInstance().tCalCfg.ho_MapFixed = null;
                    ConfigInformation.GetInstance().tCalCfg.mx = null;
                    ConfigInformation.GetInstance().tCalCfg.useThreePointCalibration = false;
                    ConfigInformation.GetInstance().tCalCfg.p1x = 0;
                    ConfigInformation.GetInstance().tCalCfg.p2x = 0;
                    ConfigInformation.GetInstance().tCalCfg.p3x = 0;
                    ConfigInformation.GetInstance().tCalCfg.r1x = 0;
                    ConfigInformation.GetInstance().tCalCfg.r2x = 0;
                    ConfigInformation.GetInstance().tCalCfg.r3x = 0;
                    ConfigInformation.GetInstance().tCalCfg.p1y = 0;
                    ConfigInformation.GetInstance().tCalCfg.p2y = 0;
                    ConfigInformation.GetInstance().tCalCfg.p3y = 0;
                    ConfigInformation.GetInstance().tCalCfg.r1y = 0;
                    ConfigInformation.GetInstance().tCalCfg.r2y = 0;
                    ConfigInformation.GetInstance().tCalCfg.r3y = 0;
                    ConfigInformation.GetInstance().tCalCfg.Sx = 0;
                    ConfigInformation.GetInstance().tCalCfg.Sy = 0;
                    ConfigInformation.GetInstance().tCalCfg.thea = 0;
                    ConfigInformation.GetInstance().tCalCfg.Tx = 0;
                    ConfigInformation.GetInstance().tCalCfg.Ty = 0;
                    Svision.GetMe().checkBoxDoNotShowImage.Checked = false;
                    if (baslerCamera != null && baslerCamera.camera.IsOpen && baslerCamera.camera.IsConnected)
                    {
                        textBoxIsCameraOpenedAndConnected.Text = "是";
                    }
                    else
                    {
                        textBoxIsCameraOpenedAndConnected.Text = "否";
                    }
                    isImageOK = false;

                    fileNameList = null;
                    fileNumIdx = 0;
                    Svision.GetMe().listBoxProcess.SelectedIndex = 0;
                    tcpStr = null;
                    checkBoxIsFile.Checked = false;
                    this.textBoxTCPShow.Text = " ";
                    this.textBoxResultShow.Text = " ";
                    this.textBoxTime.Text = " ";
                    this.textBoxShowTime.Text = " ";
                    HOperatorSet.ClearWindow(this.hvWindowHandle);
                    MessageBox.Show("新建空白工程，清空并还原所有默认设置已完成！");

                }
                else
                {

                }
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
                    textBoxIsCameraOpenedAndConnected.Text = "否";
                    MessageBox.Show("相机连接已断开，请检查连接网线！开始自动刷新相机状态！");
                    textBoxIsCameraOpenedAndConnected.Text = "正在刷新";
                    Svision.GetMe().Enabled = false;
                    cameraRefresh();
                    Svision.GetMe().Enabled = true;
                    textBoxIsCameraOpenedAndConnected.Text = "是";
                }
            }

        }
        private void 保存SToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string localFilePath, fileNameExt;
                if (folderBrowserDialogOpenProject.ShowDialog() == DialogResult.OK)
                {
                    localFilePath = folderBrowserDialogOpenProject.SelectedPath;
                    fileNameExt = localFilePath.Substring(localFilePath.LastIndexOf("\\") + 1);
                    string fileNameWhole = fileNameExt + ".xml";
                    string filePathWhole = localFilePath + "\\" + fileNameWhole;
                    if (File.Exists(filePathWhole))
                    {
                        DialogResult result = MessageBox.Show("该文件夹路径下已有工程被保存，确定用新工程覆盖原有工程？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                        if (result == DialogResult.OK)
                        {
                            Directory.Delete(localFilePath, true);
                            Directory.CreateDirectory(localFilePath);
                            FileOperate tFo = new FileOperate();
                            tFo.SaveProjToXml(filePathWhole, localFilePath, fileNameWhole);
                            MessageBox.Show("保存工程成功！");
                        }
                        else
                        { 
                            MessageBox.Show("工程未保存！");
                        }
                    }
                    else
                    {
                        FileOperate tFo = new FileOperate();
                        tFo.SaveProjToXml(filePathWhole, localFilePath, fileNameWhole);
                        MessageBox.Show("保存工程成功！");

                    }
                     

                    
                }


            }
            catch (System.Exception ex)
            {
                MessageBox.Show("保存工程失败！错误原因：" + ex.Message);
            }
        }
        private void 打开OToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string localFilePath, fileNameExt;
                if (folderBrowserDialogOpenProject.ShowDialog() == DialogResult.OK)
                {
                    localFilePath = folderBrowserDialogOpenProject.SelectedPath;
                    fileNameExt = localFilePath.Substring(localFilePath.LastIndexOf("\\") + 1);
                    string fileNameWhole = fileNameExt + ".xml";
                    string filePathWhole = localFilePath + "\\" + fileNameWhole;
                    baslerCamera.stopGrab();
                    System.Threading.Thread.Sleep(50);
                    FileOperate tFo = new FileOperate();
                    tFo.OpenXmlToProj(filePathWhole, localFilePath, fileNameWhole);
                    textBoxFileNum.Text = "";
                    textBoxResultShow.Text = "";
                    textBoxShowTime.Text = "";
                    textBoxTime.Text = "";
                    textBoxTCPShow.Text = "";
                    HOperatorSet.ClearWindow(hvWindowHandle);
                    MessageBox.Show("读取工程成功！");
                    baslerCamera.startGrab();
                }
            }
            catch (System.Exception ex)
            {
                System.Threading.Thread.Sleep(100);
                if (Svision.GetMe().baslerCamera != null && Svision.GetMe().baslerCamera.camera.IsOpen && Svision.GetMe().baslerCamera.camera.IsConnected)
                {
                    Svision.GetMe().textBoxIsCameraOpenedAndConnected.Text = "是";
                    MessageBox.Show("导入工程失败！请检查软件设置是否正确！错误原因：" + ex.Message);
                }
                else
                {
                    textBoxIsCameraOpenedAndConnected.Text = "否";
                    MessageBox.Show("导入工程失败！相机连接已断开，请检查连接网线！开始自动刷新相机状态！");
                    textBoxIsCameraOpenedAndConnected.Text = "正在刷新";
                    Svision.GetMe().Enabled = false;
                    cameraRefresh();
                    Svision.GetMe().Enabled = true;
                    textBoxIsCameraOpenedAndConnected.Text = "是";
                }
            }
        }

        private void 系统配置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                SystemConfig tSysCfg = new SystemConfig();
                tSysCfg.ShowDialog();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void 关于AToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                About tAbout = new About();
                tAbout.ShowDialog();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void listBoxProcess_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (UserCode.GetInstance().gProCd[listBoxProcess.SelectedIndex].FuncID != ProCodeCls.MainFunction.NullFBD)
                {
                    switch (UserCode.GetInstance().gProCd[listBoxProcess.SelectedIndex].FuncID)
                    {

                        case ProCodeCls.MainFunction.InputCameraInputFBD:        //CameraInput
                            break;
                        case ProCodeCls.MainFunction.InputFileInputFBD:          //FileInput
                            break;

                        case ProCodeCls.MainFunction.CalibrationBackgroundRemoveFBD:      //Filter
                            BackgroundRemoveFBDForm tBackFbd = new BackgroundRemoveFBDForm(listBoxProcess.SelectedIndex);
                            tBackFbd.ShowDialog();
                            break;
                        case ProCodeCls.MainFunction.CalibrationThresholdFBD:      //Threshold
                            ThresholdFBDForm tThreFbd = new ThresholdFBDForm(listBoxProcess.SelectedIndex);
                            tThreFbd.ShowDialog();
                            break;
                        case ProCodeCls.MainFunction.MeasureShapeSearchFBD:      //Match
                            FindShapeModelFBDForm tFdMdFbd = new FindShapeModelFBDForm(listBoxProcess.SelectedIndex);
                            tFdMdFbd.ShowDialog();
                            break;
                        case ProCodeCls.MainFunction.MeasureAnisoShapeSearchFBD:      //Match
                            FindAnisoShapeModelFBDForm tFdAMdFbd = new FindAnisoShapeModelFBDForm(listBoxProcess.SelectedIndex);
                            tFdAMdFbd.ShowDialog();
                            break;
                        case ProCodeCls.MainFunction.OutputSerialOutputFBD:      //SerialCom
                            SerialOutputFBDForm tSerOptFbd = new SerialOutputFBDForm(listBoxProcess.SelectedIndex);
                            tSerOptFbd.ShowDialog();
                            for (int i = 19; i >= 0; i--)
                            {
                                if (UserCode.GetInstance().gProCd[i].FuncID != ProCodeCls.MainFunction.NullFBD
                                    && UserCode.GetInstance().gProCd[i].FuncID != ProCodeCls.MainFunction.OutputSerialOutputFBD)
                                {
                                    UserCode.GetInstance().showCurIdx = i;
                                    break;
                                }
                            }
                            Svision.GetMe().listBoxProcess.SelectedIndex = UserCode.GetInstance().showCurIdx;
                            break;
                        case ProCodeCls.MainFunction.CalibrationMedianFilterFBD:
                            MedianFilterFBDForm tMeFltFbd = new MedianFilterFBDForm(listBoxProcess.SelectedIndex);
                            tMeFltFbd.ShowDialog();
                            break;
                        case ProCodeCls.MainFunction.CalibrationMorphologyProcessingFBD:
                            MorphologyProcessingFBDForm tMorFbd = new MorphologyProcessingFBDForm(listBoxProcess.SelectedIndex);
                            tMorFbd.ShowDialog();
                            break;
                        case ProCodeCls.MainFunction.MeasureBlobAnalysisFBD:
                            BlobAnalysisFBDForm tBlobFbd = new BlobAnalysisFBDForm(listBoxProcess.SelectedIndex);
                            tBlobFbd.ShowDialog();
                            break;

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void listBoxProcess_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (listBoxProcess.SelectedIndex >= 0)
                {
                    if (UserCode.GetInstance().gProCd[listBoxProcess.SelectedIndex].FuncID != ProCodeCls.MainFunction.OutputSerialOutputFBD)
                    {
                        UserCode.GetInstance().showCurIdx = listBoxProcess.SelectedIndex;
                    }
                }

            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
             
      
            
        }

        private void 内容CToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(System.Environment.CurrentDirectory + "/SVISION2中文操作手册.pdf");
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void 拍照toolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (baslerCamera != null && baslerCamera.camera.IsOpen && baslerCamera.camera.IsConnected)
                {
                    textBoxIsCameraOpenedAndConnected.Text = "是";
                    GetAndSaveImageTool getAsave = new GetAndSaveImageTool();
                    getAsave.ShowDialog();
                    //baslerCamera.stopGrab();
                }
                else
                {
                    textBoxIsCameraOpenedAndConnected.Text = "否";
                    MessageBox.Show("相机连接已断开，请检查连接网线！开始自动刷新相机状态！");
                    textBoxIsCameraOpenedAndConnected.Text = "正在刷新";
                    Svision.GetMe().Enabled = false;
                    cameraRefresh();
                    Svision.GetMe().Enabled = true;
                    textBoxIsCameraOpenedAndConnected.Text = "是";
                }
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
                    textBoxIsCameraOpenedAndConnected.Text = "否";
                    MessageBox.Show("相机连接已断开，请检查连接网线！开始自动刷新相机状态！");
                    textBoxIsCameraOpenedAndConnected.Text = "正在刷新";
                    Svision.GetMe().Enabled = false;
                    cameraRefresh();
                    Svision.GetMe().Enabled = true;
                    textBoxIsCameraOpenedAndConnected.Text = "是";
                }
            }
            

        }

        private void checkBoxIsFile_CheckedChanged(object sender, EventArgs e)
        {
            textBoxFileNum.Text = "";
            textBoxResultShow.Text = "";
            textBoxShowTime.Text = "";
            textBoxTime.Text = "";
            textBoxTCPShow.Text = "";
            HOperatorSet.ClearWindow(hvWindowHandle);
            if (checkBoxIsFile.Checked)
            {
                ReadImageSet.Enabled = true;
                panelManualFileInput.Enabled = true;
                AutoRun.Enabled = false;
                Camera.Enabled = false;
                CCamera.Enabled = false;
                cameraPara.Enabled = false;
                ComCfg.Enabled = false;
                工具TToolStripMenuItem.Enabled = false;
                UserCode.GetInstance().gProCd[0].FuncID = UserCode.GetInstance().codeInfo["文件输入"];
                Svision.GetMe().listBoxProcess.SelectedIndex = 0;
                Svision.GetMe().listBoxProcess.Items.Insert(1, "0.文件输入");
                Svision.GetMe().listBoxProcess.Items.RemoveAt(0);
                Svision.GetMe().listBoxProcess.SelectedIndex = 0;
                Svision.GetMe().oriRowNumber = ConfigInformation.GetInstance().tFileCfg.rows;
                Svision.GetMe().oriColumnNumber = ConfigInformation.GetInstance().tFileCfg.columns;
                baslerCamera.stopGrab();
            } 
            else
            {
                ReadImageSet.Enabled = false;
                panelManualFileInput.Enabled = false;
                AutoRun.Enabled = true;
                Camera.Enabled = true;
                CCamera.Enabled = true;
                cameraPara.Enabled = true;
                ComCfg.Enabled = true;
                工具TToolStripMenuItem.Enabled = true;
                UserCode.GetInstance().gProCd[0].FuncID = UserCode.GetInstance().codeInfo["相机输入"];
                Svision.GetMe().listBoxProcess.SelectedIndex = 0;
                Svision.GetMe().listBoxProcess.Items.Insert(1, "0.相机输入");
                Svision.GetMe().listBoxProcess.Items.RemoveAt(0);
                Svision.GetMe().listBoxProcess.SelectedIndex = 0;
                Svision.GetMe().oriRowNumber = ConfigInformation.GetInstance().tCamCfg.rows;
                Svision.GetMe().oriColumnNumber = ConfigInformation.GetInstance().tCamCfg.columns;
                baslerCamera.startGrab();
            }
        }

        private void buttonPreImage_Click(object sender, EventArgs e)
        {
            try
            {
                if (fileNameList.Count > 0)
                {
                    if (fileNumIdx - 1 >= 0)
                    {
                        fileNumIdx = fileNumIdx - 1;
                        textBoxFileNum.Text = (fileNumIdx + 1).ToString();
                        if (File.Exists(fileNameList[fileNumIdx]))
                        {
                            HObject imgg;
                            int rNum, cNum;
                            HTuple imgCNum;

                            HOperatorSet.ReadImage(out imgg, (HTuple)fileNameList[fileNumIdx]);
                            basicClass.getImageSize(imgg, out rNum, out cNum);
                            HOperatorSet.CountChannels(imgg, out imgCNum);
                            if (rNum == ConfigInformation.GetInstance().tFileCfg.rows &&
                                cNum == ConfigInformation.GetInstance().tFileCfg.columns &&
                                imgCNum == ConfigInformation.GetInstance().tFileCfg.channelNum)
                            {
                                HOperatorSet.CopyImage(imgg, out UserCode.GetInstance().fileImages);
                                if (UserCode.GetInstance().importantFlagIsRunning == false)
                                {
                                    UserCode.GetInstance().FileCodeRun();
                                }
                                else
                                {
                                    if (imgg != null)
                                    {
                                        imgg.Dispose();
                                    }
                                    fileNumIdx = fileNumIdx + 1;
                                    textBoxFileNum.Text = (fileNumIdx + 1).ToString();
                                    throw new Exception("软件处理未完成！请稍后继续！");
                                }
                            }
                            else
                            {
                                if (imgg != null)
                                {
                                    imgg.Dispose();
                                }
                                HOperatorSet.ClearWindow(hvWindowHandle);
                                throw new Exception("当前图像文件行列及通道参数与文件设置内容不符！请调整！");
                            }
                            if (imgg != null)
                            {
                                imgg.Dispose();
                            }
                        }
                        else
                        {
                            HOperatorSet.ClearWindow(hvWindowHandle);
                            throw new Exception("当前图像文件路径不存在！");
                        }

                    }
                    else
                    {
                        textBoxFileNum.Text = (fileNumIdx + 1).ToString();
                        throw new Exception("当前已是第一幅图像！");
                    }
                }
            }
            catch (System.Exception ex)
            {
                UserCode.GetInstance().importantFlagIsRunning = false;
                MessageBox.Show(ex.Message);
            }
            
        }

        private void buttonNextImage_Click(object sender, EventArgs e)
        {
            try
            {
                if (fileNameList.Count > 0)
                {
                    if (fileNumIdx + 1 <=fileNameList.Count-1)
                    {
                        fileNumIdx = fileNumIdx + 1;
                        textBoxFileNum.Text = (fileNumIdx + 1).ToString();
                        if (File.Exists(fileNameList[fileNumIdx]))
                        {
                            HObject imgg;
                            int rNum, cNum;
                            HTuple imgCNum;

                            HOperatorSet.ReadImage(out imgg, (HTuple)fileNameList[fileNumIdx]);
                            basicClass.getImageSize(imgg, out rNum, out cNum);
                            HOperatorSet.CountChannels(imgg, out imgCNum);
                            if (rNum == ConfigInformation.GetInstance().tFileCfg.rows &&
                                cNum == ConfigInformation.GetInstance().tFileCfg.columns &&
                                imgCNum == ConfigInformation.GetInstance().tFileCfg.channelNum)
                            {
                                HOperatorSet.CopyImage(imgg, out UserCode.GetInstance().fileImages);
                                if (UserCode.GetInstance().importantFlagIsRunning == false)
                                {
                                    UserCode.GetInstance().FileCodeRun();
                                }
                                else
                                {
                                    if (imgg != null)
                                    {
                                        imgg.Dispose();
                                    }
                                    fileNumIdx = fileNumIdx - 1;
                                    textBoxFileNum.Text = (fileNumIdx + 1).ToString();
                                    throw new Exception("软件处理未完成！请稍后继续！");
                                }
                            }
                            else
                            {
                                if (imgg != null)
                                {
                                    imgg.Dispose();
                                }
                                HOperatorSet.ClearWindow(hvWindowHandle);
                                throw new Exception("当前图像文件行列及通道参数与文件设置内容不符！请调整！");
                            }
                            if (imgg != null)
                            {
                                imgg.Dispose();
                            }
                        }
                        else
                        {
                            HOperatorSet.ClearWindow(hvWindowHandle);
                            throw new Exception("当前图像文件路径不存在！");
                        }

                    }
                    else
                    {
                        textBoxFileNum.Text = (fileNumIdx + 1).ToString();
                        throw new Exception("当前已是最后一幅图像！");
                    }
                }
            }
            catch (System.Exception ex)
            {
                UserCode.GetInstance().importantFlagIsRunning = false;
                MessageBox.Show(ex.Message);
            }
        }

        private void Svision_FormClosing(object sender, FormClosingEventArgs e)
        {
            cCheckFlag = true;
            if (baslerCamera!=null)
            {
                baslerCamera.closeCamera();
            }
            
            HOperatorSet.ClearAllShapeModels();
        }

        private void 工具TToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }



        



    }

}
