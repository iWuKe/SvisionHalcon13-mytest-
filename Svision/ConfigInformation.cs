/**************************************************************************************
**
**       Filename:  ConfigInformation.cs
**
**    Description:  All of the vision configuration in this file 
**
**        Version:  1.0
**        Created:  2016-2-23
**       Revision:  2.0007
**       Compiler:  vs2010
**        Company:  SIASUN
**
****************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using HalconDotNet;
using MathNet.Numerics.LinearAlgebra;
namespace Svision
{
    public class CComConfig
    {
        public enum ComTypeEnum
        {
            TCP_SERVER,
            TCP_CLIENT,
            UDP,
            MODBUS_TCP
        }

        public enum TrigModeEnum
        {
            COM_TRIG,
            TIME_TRIG
        }

        public ComTypeEnum ComMainType;
        public bool ComTCPServer;       //true----->Server      false------>Client
        public String TCPServerIP;      //当视觉作为客户端时，FWQ的IP地址
        public String TCPServerPort;    //当视觉作为客户端时，FWQ的端口号
        public String TCPLocalIP;       //当视觉作为FWQ时，IP地址
        public String TCPLocalPort;     //当视觉作为FWQ时，监听的端口号
        public TrigModeEnum TrigMode;            //触发模式 0---->通讯触发    1---->定时触发
        public String TrigStr;          //当视觉选择通讯触发时，应该接收的触发字符串
        public int TrigTime;            //触发时间  (单位ms)

        public Dictionary<ComTypeEnum, String> ComTypeDic;
        public Dictionary<TrigModeEnum, String> TrigModeDic;

        public CComConfig()
        {
            ComMainType = CComConfig.ComTypeEnum.TCP_SERVER;
            ComTCPServer = true;
            TCPServerIP = "192.168.100.100";
            TCPServerPort = "6789";
            TCPLocalIP = "192.168.100.123";
            TCPLocalPort = "9876";
            TrigMode = CComConfig.TrigModeEnum.COM_TRIG;
            TrigStr = "M";
            TrigTime = 500;

            ComTypeDic = new Dictionary<ComTypeEnum, String>();
            TrigModeDic = new Dictionary<TrigModeEnum, String>();

            ComTypeDic.Add(ComTypeEnum.TCP_SERVER, "TCP_SERVER");
            ComTypeDic.Add(ComTypeEnum.TCP_CLIENT, "TCP_CLIENT");
            ComTypeDic.Add(ComTypeEnum.UDP, "UDP");
            ComTypeDic.Add(ComTypeEnum.MODBUS_TCP, "MODBUS_TCP");

            TrigModeDic.Add(TrigModeEnum.COM_TRIG, "COM_TRIG");
            TrigModeDic.Add(TrigModeEnum.TIME_TRIG, "TIME_TRIG");
        }
    }
    public class FileConfig
    {
        public int rows;
        public int columns;
       
        public int channelNum;

        public FileConfig()
        {
            rows = 480;
            columns = 640;
            
            channelNum = 1;
            
        }
        //public void clear()
        //{
        //    rows = 640;
        //    columns = 480;
        //    xOffset = 0;
        //    yOffset = 0;
        //    channelNum = 1;
        //    gainPercent = 0;//增益百分数;
        //    gammaPercent = 0;//Gamma百分数;
        //    exposurePercent = 0;//曝光百分数;
        //    isExposureAuto = false;
        //    isGainAuto = false;
        //    isWhiteBalanceAuto = false;
        //    whiteBalanceBluePercent = 0.2;//白平衡百分数（蓝）;
        //    whiteBalanceGreenPercent = 0.2;//白平衡百分数（绿）;
        //    whiteBalanceRedPercent = 0.2;//白平衡百分数（红）;
        //}
    }
    public class CCameraConfig
    {
        public int rows;
        public int columns;
        public int xOffset;
        public int yOffset;

        public int channelNum;
        public double gainPercent = 0.5;//增益百分数;
        public double gammaPercent = 0.3;//Gamma百分数;
        public double exposurePercent = 0.002;//曝光百分数;
        public double whiteBalanceBluePercent;//白平衡百分数（蓝）;
        public double whiteBalanceGreenPercent;//白平衡百分数（绿）;
        public double whiteBalanceRedPercent;//白平衡百分数（红）;
        public bool isExposureAuto;
        public bool isGainAuto;
        public bool isWhiteBalanceAuto;

        public CCameraConfig()
        {
            rows = 480;
            columns = 640;
            xOffset = 0;
            yOffset = 0;
            channelNum = 1;
            gainPercent = 0.5;//增益百分数;
            gammaPercent = 0.3;//Gamma百分数;
            exposurePercent = 0.002;//曝光百分数;
            isExposureAuto = false;
            isGainAuto = false;
            isWhiteBalanceAuto = false;
            whiteBalanceBluePercent = 0.2;//白平衡百分数（蓝）;
            whiteBalanceGreenPercent = 0.2;//白平衡百分数（绿）;
            whiteBalanceRedPercent = 0.2;//白平衡百分数（红）;
        }
        //public void clear()
        //{
        //    rows = 640;
        //    columns = 480;
        //    xOffset = 0;
        //    yOffset = 0;
        //    channelNum = 1;
        //    gainPercent = 0;//增益百分数;
        //    gammaPercent = 0;//Gamma百分数;
        //    exposurePercent = 0;//曝光百分数;
        //    isExposureAuto = false;
        //    isGainAuto = false;
        //    isWhiteBalanceAuto = false;
        //    whiteBalanceBluePercent = 0.2;//白平衡百分数（蓝）;
        //    whiteBalanceGreenPercent = 0.2;//白平衡百分数（绿）;
        //    whiteBalanceRedPercent = 0.2;//白平衡百分数（红）;
        //}
    }
    public class CCalibrationConfig
    {
        public double calibrationDistance;
        public int calibrationPoint;
        public int focus;
        public double calibrationRate;

        public double pixelSize;
        public bool isCheckBoxRadialDistortion ;
        public bool calibrationParaConfirmFlag ;
        public bool calibrationIsRadialDistortionFlag ;
        public HObject ho_MapFixed;
        public Matrix mx;
        public bool useThreePointCalibration;
        public double p1x, p1y, p2x, p2y, p3x, p3y, r1x, r1y, r2x, r2y, r3x, r3y;
        public double Sx, Sy, thea, Tx, Ty;
        public CCalibrationConfig()
        {
            calibrationDistance = 0.014;
            calibrationPoint = 8;
            focus = 5;
            calibrationRate = 0.5;
            useThreePointCalibration = false;
            pixelSize = 4.4;
            isCheckBoxRadialDistortion = false;
            calibrationParaConfirmFlag = false;
            calibrationIsRadialDistortionFlag = false;
            ho_MapFixed = null;
            mx = null;
            p1x= p1y= p2x= p2y= p3x= p3y= r1x= r1y= r2x= r2y= r3x= r3y=0;
            Sx= Sy= thea= Tx= Ty=0;
        }
        //public void clear()
        //{
        //    rows = 640;
        //    columns = 480;
        //    xOffset = 0;
        //    yOffset = 0;
        //    channelNum = 1;
        //    gainPercent = 0;//增益百分数;
        //    gammaPercent = 0;//Gamma百分数;
        //    exposurePercent = 0;//曝光百分数;
        //    isExposureAuto = false;
        //    isGainAuto = false;
        //    isWhiteBalanceAuto = false;
        //    whiteBalanceBluePercent = 0.2;//白平衡百分数（蓝）;
        //    whiteBalanceGreenPercent = 0.2;//白平衡百分数（绿）;
        //    whiteBalanceRedPercent = 0.2;//白平衡百分数（红）;
        //}
    }
    class SystemInfoConfig
    {
        public bool isBoot;
        public String bootPath;
        private String xmlPath; 

        public SystemInfoConfig()
        {
            xmlPath = System.Environment.CurrentDirectory + "/SystemCfg.xml";
        }

        public void saveSysInfoCfg()
        {
            XmlDocument tXmlDoc = new XmlDocument();
            XmlDeclaration xmldel;
            xmldel = tXmlDoc.CreateXmlDeclaration("1.0", null, null);
            tXmlDoc.AppendChild(xmldel);
            XmlElement xmlelem = tXmlDoc.CreateElement("", "SystemConfiguration", "");
            tXmlDoc.AppendChild(xmlelem);

            XmlElement rootCameraCfg = tXmlDoc.CreateElement("BootConfig");
            xmlelem.AppendChild(rootCameraCfg);
            
            //save camera information
            {
                XmlElement isBootElement = tXmlDoc.CreateElement("isBoot");
                isBootElement.SetAttribute("isBoot", this.isBoot.ToString());
                XmlElement bootPathElement = tXmlDoc.CreateElement("bootPath");
                bootPathElement.SetAttribute("bootPath", this.bootPath.ToString());
                rootCameraCfg.AppendChild(isBootElement);
                rootCameraCfg.AppendChild(bootPathElement);
            }

            tXmlDoc.Save(xmlPath);            
        }

        public void loadSysInfoCfg()
        {
            XmlDocument tXmlDoc = new XmlDocument();

            try
            {
                if (File.Exists(xmlPath))
                {
                    tXmlDoc.Load(xmlPath);

                    XmlNode rootNode = tXmlDoc.SelectSingleNode("SystemConfiguration");
                    //load camera config
                    {
                        XmlNode CameraNode = rootNode.SelectSingleNode("BootConfig");
                        this.isBoot = bool.Parse(CameraNode.SelectSingleNode("isBoot").Attributes["isBoot"].InnerText);
                        this.bootPath = CameraNode.SelectSingleNode("bootPath").Attributes["bootPath"].InnerText;
                    }
                }
                
            }
            catch (System.Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }

    class ConfigInformation
    {

        private static ConfigInformation tCfgInfo;
        public CComConfig tComCfg;
        public CCameraConfig tCamCfg;
        public FileConfig tFileCfg;
        public CfgInfoOperate tCfgInfoOprt;
        public SystemInfoConfig tSysCfg;
        public CCalibrationConfig tCalCfg;
        public ConfigInformation()
        {
            tComCfg = new CComConfig();
            tCamCfg = new CCameraConfig();
            tFileCfg = new FileConfig();
            tCfgInfoOprt = new CfgInfoOperate();
            tSysCfg = new SystemInfoConfig();
            tCalCfg = new CCalibrationConfig();
        }

        ~ConfigInformation()
        {

        }

        public static ConfigInformation GetInstance()
        {
            if (tCfgInfo == null)
            {
                tCfgInfo = new ConfigInformation();
            }

            return tCfgInfo;
        }
    }

    class CfgInfoOperate
    {
        public FileStream gFileS=null;

        public CfgInfoOperate()
        {

        }

        ~CfgInfoOperate()
        {

        }

        public void SaveCfgInfo(String tmpPath)
        {
            XmlDocument tXmlDoc = new XmlDocument();
            XmlDeclaration xmldel;
            xmldel = tXmlDoc.CreateXmlDeclaration("1.0", null, null);
            tXmlDoc.AppendChild(xmldel);
            XmlElement xmlelem = tXmlDoc.CreateElement("", "Configuration", "");
            tXmlDoc.AppendChild(xmlelem);

            XmlElement rootCameraCfg = tXmlDoc.CreateElement("CameraConfig");
            XmlElement rootFileCfg = tXmlDoc.CreateElement("FileConfig");
            XmlElement rootCalibrationCfg = tXmlDoc.CreateElement("CalibrationConfig");
            XmlElement rootCommunicateCfg = tXmlDoc.CreateElement("CommunicateConfig");
            
            xmlelem.AppendChild(rootCommunicateCfg);
            xmlelem.AppendChild(rootCameraCfg);
            xmlelem.AppendChild(rootFileCfg);
            xmlelem.AppendChild(rootCalibrationCfg);
            
            //save com information
            {
                XmlElement ComMainTypeElement = tXmlDoc.CreateElement("ComMainType");
                ComMainTypeElement.SetAttribute("ComMainType", ConfigInformation.GetInstance().tComCfg.ComMainType.ToString());
                XmlElement ComTCPServerElement = tXmlDoc.CreateElement("ComTCPServer");
                ComTCPServerElement.SetAttribute("ComTCPServer", ConfigInformation.GetInstance().tComCfg.ComTCPServer.ToString());
                XmlElement TCPServerIPElement = tXmlDoc.CreateElement("TCPServerIP");
                TCPServerIPElement.SetAttribute("TCPServerIP", ConfigInformation.GetInstance().tComCfg.TCPServerIP.ToString());
                XmlElement TCPServerPortElement = tXmlDoc.CreateElement("TCPServerPort");
                TCPServerPortElement.SetAttribute("TCPServerPort", ConfigInformation.GetInstance().tComCfg.TCPServerPort.ToString());
                XmlElement TCPLocalIPElement = tXmlDoc.CreateElement("TCPLocalIP");
                TCPLocalIPElement.SetAttribute("TCPLocalIP", ConfigInformation.GetInstance().tComCfg.TCPLocalIP.ToString());
                XmlElement TCPLocalPortElement = tXmlDoc.CreateElement("TCPLocalPort");
                TCPLocalPortElement.SetAttribute("TCPLocalPort", ConfigInformation.GetInstance().tComCfg.TCPLocalPort.ToString());
                XmlElement TrigModeElement = tXmlDoc.CreateElement("TrigMode");
                TrigModeElement.SetAttribute("TrigMode", ConfigInformation.GetInstance().tComCfg.TrigMode.ToString());
                XmlElement TrigStrElement = tXmlDoc.CreateElement("TrigStr");
                TrigStrElement.SetAttribute("TrigStr", ConfigInformation.GetInstance().tComCfg.TrigStr.ToString());
                XmlElement TrigTimeElement = tXmlDoc.CreateElement("TrigTime");
                TrigTimeElement.SetAttribute("TrigTime", ConfigInformation.GetInstance().tComCfg.TrigTime.ToString());

                rootCommunicateCfg.AppendChild(ComMainTypeElement);
                rootCommunicateCfg.AppendChild(ComTCPServerElement);
                rootCommunicateCfg.AppendChild(TCPServerIPElement);
                rootCommunicateCfg.AppendChild(TCPServerPortElement);
                rootCommunicateCfg.AppendChild(TCPLocalIPElement);
                rootCommunicateCfg.AppendChild(TCPLocalPortElement);
                rootCommunicateCfg.AppendChild(TrigModeElement);
                rootCommunicateCfg.AppendChild(TrigStrElement);
                rootCommunicateCfg.AppendChild(TrigTimeElement);
            }
            //save camera information
            {

                XmlElement RowsElement = tXmlDoc.CreateElement("Rows");
                RowsElement.SetAttribute("Rows", ConfigInformation.GetInstance().tCamCfg.rows.ToString());

                XmlElement ColumnsElement = tXmlDoc.CreateElement("Columns");
                ColumnsElement.SetAttribute("Columns", ConfigInformation.GetInstance().tCamCfg.columns.ToString());

                XmlElement XoffsetElement = tXmlDoc.CreateElement("XOffset");
                XoffsetElement.SetAttribute("XOffset", ConfigInformation.GetInstance().tCamCfg.xOffset.ToString());

                XmlElement YoffsetElement = tXmlDoc.CreateElement("YOffset");
                YoffsetElement.SetAttribute("YOffset", ConfigInformation.GetInstance().tCamCfg.yOffset.ToString());

                XmlElement channelNumElement = tXmlDoc.CreateElement("ChannelNum");
                channelNumElement.SetAttribute("ChannelNum", ConfigInformation.GetInstance().tCamCfg.channelNum.ToString());

                XmlElement gainPercentElement = tXmlDoc.CreateElement("GainPercent");
                gainPercentElement.SetAttribute("GainPercent", ConfigInformation.GetInstance().tCamCfg.gainPercent.ToString());

                XmlElement gammaPercentElement = tXmlDoc.CreateElement("GammaPercent");
                gammaPercentElement.SetAttribute("GammaPercent", ConfigInformation.GetInstance().tCamCfg.gammaPercent.ToString());

                XmlElement exposurePercentElement = tXmlDoc.CreateElement("ExposurePercent");
                exposurePercentElement.SetAttribute("ExposurePercent", ConfigInformation.GetInstance().tCamCfg.exposurePercent.ToString());

                XmlElement whiteBalanceBluePercentElement = tXmlDoc.CreateElement("WhiteBalanceBluePercent");
                whiteBalanceBluePercentElement.SetAttribute("WhiteBalanceBluePercent", ConfigInformation.GetInstance().tCamCfg.whiteBalanceBluePercent.ToString());

                XmlElement whiteBalanceGreenPercentElement = tXmlDoc.CreateElement("WhiteBalanceGreenPercent");
                whiteBalanceGreenPercentElement.SetAttribute("WhiteBalanceGreenPercent", ConfigInformation.GetInstance().tCamCfg.whiteBalanceGreenPercent.ToString());

                XmlElement whiteBalanceRedPercentElement = tXmlDoc.CreateElement("WhiteBalanceRedPercent");
                whiteBalanceRedPercentElement.SetAttribute("WhiteBalanceRedPercent", ConfigInformation.GetInstance().tCamCfg.whiteBalanceRedPercent.ToString());

                XmlElement isExposureAutoElement = tXmlDoc.CreateElement("IsExposureAuto");
                isExposureAutoElement.SetAttribute("IsExposureAuto", ConfigInformation.GetInstance().tCamCfg.isExposureAuto.ToString());

                XmlElement isGainAutoElement = tXmlDoc.CreateElement("IsGainAuto");
                isGainAutoElement.SetAttribute("IsGainAuto", ConfigInformation.GetInstance().tCamCfg.isGainAuto.ToString());

                XmlElement isWhiteBalanceAutoElement = tXmlDoc.CreateElement("IsWhiteBalanceAuto");
                isWhiteBalanceAutoElement.SetAttribute("IsWhiteBalanceAuto", ConfigInformation.GetInstance().tCamCfg.isWhiteBalanceAuto.ToString());


                rootCameraCfg.AppendChild(RowsElement);
                rootCameraCfg.AppendChild(ColumnsElement);
                rootCameraCfg.AppendChild(XoffsetElement);
                rootCameraCfg.AppendChild(YoffsetElement);
                rootCameraCfg.AppendChild(channelNumElement);
                rootCameraCfg.AppendChild(gainPercentElement);
                rootCameraCfg.AppendChild(gammaPercentElement);
                rootCameraCfg.AppendChild(exposurePercentElement);
                rootCameraCfg.AppendChild(whiteBalanceBluePercentElement);
                rootCameraCfg.AppendChild(whiteBalanceGreenPercentElement);
                rootCameraCfg.AppendChild(whiteBalanceRedPercentElement);
                rootCameraCfg.AppendChild(isExposureAutoElement);
                rootCameraCfg.AppendChild(isGainAutoElement);
                rootCameraCfg.AppendChild(isWhiteBalanceAutoElement);

            }
            //save file information
            {

                XmlElement RowsElement = tXmlDoc.CreateElement("Rows");
                RowsElement.SetAttribute("Rows", ConfigInformation.GetInstance().tCamCfg.rows.ToString());
                XmlElement ColumnsElement            = tXmlDoc.CreateElement("Columns");
                ColumnsElement.SetAttribute("Columns", ConfigInformation.GetInstance().tCamCfg.columns.ToString());
                XmlElement channelNumElement = tXmlDoc.CreateElement("ChannelNum");
                channelNumElement.SetAttribute("ChannelNum", ConfigInformation.GetInstance().tCamCfg.channelNum.ToString());
                XmlElement isFileInputElement = tXmlDoc.CreateElement("IsFileInput");
                isFileInputElement.SetAttribute("IsFileInput", Svision.GetMe().checkBoxIsFile.Checked.ToString());
                rootFileCfg.AppendChild(RowsElement);
                rootFileCfg.AppendChild(ColumnsElement);
                rootFileCfg.AppendChild(channelNumElement);
                rootFileCfg.AppendChild(isFileInputElement);
                

            }
            //save calibration information
            {
                XmlElement calibrationDistanceElement = tXmlDoc.CreateElement("calibrationDistance");
                calibrationDistanceElement.SetAttribute("calibrationDistance", ConfigInformation.GetInstance().tCalCfg.calibrationDistance.ToString());

                XmlElement calibrationPointElement = tXmlDoc.CreateElement("calibrationPoint");
                calibrationPointElement.SetAttribute("calibrationPoint", ConfigInformation.GetInstance().tCalCfg.calibrationPoint.ToString());

                XmlElement focusElement = tXmlDoc.CreateElement("focus");
                focusElement.SetAttribute("focus", ConfigInformation.GetInstance().tCalCfg.focus.ToString());

                XmlElement calibrationRateElement = tXmlDoc.CreateElement("calibrationRate");
                calibrationRateElement.SetAttribute("calibrationRate", ConfigInformation.GetInstance().tCalCfg.calibrationRate.ToString());

                XmlElement pixelSizeElement = tXmlDoc.CreateElement("pixelSize");
                pixelSizeElement.SetAttribute("pixelSize", ConfigInformation.GetInstance().tCalCfg.pixelSize.ToString());

                XmlElement isCheckBoxRadialDistortionElement = tXmlDoc.CreateElement("isCheckBoxRadialDistortion");
                isCheckBoxRadialDistortionElement.SetAttribute("isCheckBoxRadialDistortion", ConfigInformation.GetInstance().tCalCfg.isCheckBoxRadialDistortion.ToString());

                XmlElement calibrationParaConfirmFlagElement = tXmlDoc.CreateElement("calibrationParaConfirmFlag");
                calibrationParaConfirmFlagElement.SetAttribute("calibrationParaConfirmFlag", ConfigInformation.GetInstance().tCalCfg.calibrationParaConfirmFlag.ToString());

                XmlElement calibrationIsRadialDistortionFlagElement = tXmlDoc.CreateElement("calibrationIsRadialDistortionFlag");
                calibrationIsRadialDistortionFlagElement.SetAttribute("calibrationIsRadialDistortionFlag", ConfigInformation.GetInstance().tCalCfg.calibrationIsRadialDistortionFlag.ToString());
                if (ConfigInformation.GetInstance().tCalCfg.ho_MapFixed != null && ConfigInformation.GetInstance().tCalCfg.ho_MapFixed.IsInitialized())
                {
                    HOperatorSet.WriteImage(ConfigInformation.GetInstance().tCalCfg.ho_MapFixed, "tiff", 0, tmpPath.Remove(tmpPath.LastIndexOf("\\") + 1) + "_calibrationMap.tiff");
                }//XmlElement ho_MapFixedPathElement = tXmlDoc.CreateElement("ho_MapFixedPath");
                //ho_MapFixedPathElement.SetAttribute("ho_MapFixedPath", ConfigInformation.GetInstance().tCalCfg.ho_MapFixed.ToString());
                rootCalibrationCfg.AppendChild(calibrationDistanceElement);
                rootCalibrationCfg.AppendChild(calibrationPointElement);
                rootCalibrationCfg.AppendChild(focusElement);
                rootCalibrationCfg.AppendChild(calibrationRateElement);
                rootCalibrationCfg.AppendChild(pixelSizeElement);
                rootCalibrationCfg.AppendChild(isCheckBoxRadialDistortionElement);
                rootCalibrationCfg.AppendChild(calibrationParaConfirmFlagElement);
                rootCalibrationCfg.AppendChild(calibrationIsRadialDistortionFlagElement);
                //rootCameraCfg.AppendChild(whiteBalanceBluePercentElement);
                XmlElement useThreePointCalibrationElement = tXmlDoc.CreateElement("useThreePointCalibration");
                useThreePointCalibrationElement.SetAttribute("useThreePointCalibration", ConfigInformation.GetInstance().tCalCfg.useThreePointCalibration.ToString());

                XmlElement p1xElement = tXmlDoc.CreateElement("p1x");
                p1xElement.SetAttribute("p1x", ConfigInformation.GetInstance().tCalCfg.p1x.ToString());

                XmlElement p2xElement = tXmlDoc.CreateElement("p2x");
                p2xElement.SetAttribute("p2x", ConfigInformation.GetInstance().tCalCfg.p2x.ToString());

                XmlElement p3xElement = tXmlDoc.CreateElement("p3x");
                p3xElement.SetAttribute("p3x", ConfigInformation.GetInstance().tCalCfg.p3x.ToString());

                XmlElement r1xElement = tXmlDoc.CreateElement("r1x");
                r1xElement.SetAttribute("r1x", ConfigInformation.GetInstance().tCalCfg.r1x.ToString());

                XmlElement r2xElement = tXmlDoc.CreateElement("r2x");
                r2xElement.SetAttribute("r2x", ConfigInformation.GetInstance().tCalCfg.r2x.ToString());

                XmlElement r3xElement = tXmlDoc.CreateElement("r3x");
                r3xElement.SetAttribute("r3x", ConfigInformation.GetInstance().tCalCfg.r3x.ToString());

                XmlElement p1yElement = tXmlDoc.CreateElement("p1y");
                p1yElement.SetAttribute("p1y", ConfigInformation.GetInstance().tCalCfg.p1y.ToString());

                XmlElement p2yElement = tXmlDoc.CreateElement("p2y");
                p2yElement.SetAttribute("p2y", ConfigInformation.GetInstance().tCalCfg.p2y.ToString());

                XmlElement p3yElement = tXmlDoc.CreateElement("p3y");
                p3yElement.SetAttribute("p3y", ConfigInformation.GetInstance().tCalCfg.p3y.ToString());

                XmlElement r1yElement = tXmlDoc.CreateElement("r1y");
                r1yElement.SetAttribute("r1y", ConfigInformation.GetInstance().tCalCfg.r1y.ToString());

                XmlElement r2yElement = tXmlDoc.CreateElement("r2y");
                r2yElement.SetAttribute("r2y", ConfigInformation.GetInstance().tCalCfg.r2y.ToString());

                XmlElement r3yElement = tXmlDoc.CreateElement("r3y");
                r3yElement.SetAttribute("r3y", ConfigInformation.GetInstance().tCalCfg.r3y.ToString());

                XmlElement SxElement = tXmlDoc.CreateElement("Sx");
                SxElement.SetAttribute("Sx", ConfigInformation.GetInstance().tCalCfg.Sx.ToString());

                XmlElement SyElement = tXmlDoc.CreateElement("Sy");
                SyElement.SetAttribute("Sy", ConfigInformation.GetInstance().tCalCfg.Sy.ToString());

                XmlElement theaElement = tXmlDoc.CreateElement("thea");
                theaElement.SetAttribute("thea", ConfigInformation.GetInstance().tCalCfg.thea.ToString());

                XmlElement TxElement = tXmlDoc.CreateElement("Tx");
                TxElement.SetAttribute("Tx", ConfigInformation.GetInstance().tCalCfg.Tx.ToString());

                XmlElement TyElement = tXmlDoc.CreateElement("Ty");
                TyElement.SetAttribute("Ty", ConfigInformation.GetInstance().tCalCfg.Ty.ToString());
                rootCalibrationCfg.AppendChild(useThreePointCalibrationElement);
                rootCalibrationCfg.AppendChild(p1xElement);
                rootCalibrationCfg.AppendChild(p2xElement);
                rootCalibrationCfg.AppendChild(p3xElement);
                rootCalibrationCfg.AppendChild(r1xElement);
                rootCalibrationCfg.AppendChild(r2xElement);
                rootCalibrationCfg.AppendChild(r3xElement);
                rootCalibrationCfg.AppendChild(p1yElement);
                rootCalibrationCfg.AppendChild(p2yElement);
                rootCalibrationCfg.AppendChild(p3yElement);
                rootCalibrationCfg.AppendChild(r1yElement);
                rootCalibrationCfg.AppendChild(r2yElement);
                rootCalibrationCfg.AppendChild(r3yElement);
                rootCalibrationCfg.AppendChild(SxElement);
                rootCalibrationCfg.AppendChild(SyElement);
                rootCalibrationCfg.AppendChild(theaElement);
                rootCalibrationCfg.AppendChild(TxElement);
                rootCalibrationCfg.AppendChild(TyElement);
                if (ConfigInformation.GetInstance().tCalCfg.mx != null)
                {
                    XmlElement mx1Element = tXmlDoc.CreateElement("mx1");
                    mx1Element.SetAttribute("mx1", ConfigInformation.GetInstance().tCalCfg.mx[0,0].ToString());

                    XmlElement mx2Element = tXmlDoc.CreateElement("mx2");
                    mx2Element.SetAttribute("mx2", ConfigInformation.GetInstance().tCalCfg.mx[1, 0].ToString());

                    XmlElement mx3Element = tXmlDoc.CreateElement("mx3");
                    mx3Element.SetAttribute("mx3", ConfigInformation.GetInstance().tCalCfg.mx[2, 0].ToString());

                    XmlElement mx4Element = tXmlDoc.CreateElement("mx4");
                    mx4Element.SetAttribute("mx4", ConfigInformation.GetInstance().tCalCfg.mx[3, 0].ToString());

                    XmlElement mx5Element = tXmlDoc.CreateElement("mx5");
                    mx5Element.SetAttribute("mx5", ConfigInformation.GetInstance().tCalCfg.mx[4, 0].ToString());

                    XmlElement mx6Element = tXmlDoc.CreateElement("mx6");
                    mx6Element.SetAttribute("mx6", ConfigInformation.GetInstance().tCalCfg.mx[5, 0].ToString());
                    rootCalibrationCfg.AppendChild(mx1Element);
                    rootCalibrationCfg.AppendChild(mx2Element);
                    rootCalibrationCfg.AppendChild(mx3Element);
                    rootCalibrationCfg.AppendChild(mx4Element);
                    rootCalibrationCfg.AppendChild(mx5Element);
                    rootCalibrationCfg.AppendChild(mx6Element);

                }
                
                
            }
            

            tXmlDoc.Save(tmpPath);
        }

        public void LoadCfgInfo(String tmpPath)
        {
            XmlDocument tXmlDoc = new XmlDocument();
            
            try
            {
                tXmlDoc.Load(tmpPath);
                XmlNode rootNode = tXmlDoc.SelectSingleNode("Configuration");
                //load com config
                {
                    XmlNode ComNode = rootNode.SelectSingleNode("CommunicateConfig");

                    if (ComNode.SelectSingleNode("ComMainType").Attributes["ComMainType"].InnerText ==
                        ConfigInformation.GetInstance().tComCfg.ComTypeDic[CComConfig.ComTypeEnum.TCP_SERVER])
                    {
                        ConfigInformation.GetInstance().tComCfg.ComMainType = CComConfig.ComTypeEnum.TCP_SERVER;
                    }
                    else if (ComNode.SelectSingleNode("ComMainType").Attributes["ComMainType"].InnerText ==
                        ConfigInformation.GetInstance().tComCfg.ComTypeDic[CComConfig.ComTypeEnum.TCP_CLIENT])
                    {
                        ConfigInformation.GetInstance().tComCfg.ComMainType = CComConfig.ComTypeEnum.TCP_CLIENT;
                    }
                    else if (ComNode.SelectSingleNode("ComMainType").Attributes["ComMainType"].InnerText ==
                        ConfigInformation.GetInstance().tComCfg.ComTypeDic[CComConfig.ComTypeEnum.UDP])
                    {
                        ConfigInformation.GetInstance().tComCfg.ComMainType = CComConfig.ComTypeEnum.UDP;
                    }
                    else if (ComNode.SelectSingleNode("ComMainType").Attributes["ComMainType"].InnerText ==
                        ConfigInformation.GetInstance().tComCfg.ComTypeDic[CComConfig.ComTypeEnum.MODBUS_TCP])
                    {
                        ConfigInformation.GetInstance().tComCfg.ComMainType = CComConfig.ComTypeEnum.MODBUS_TCP;
                    }

                    ConfigInformation.GetInstance().tComCfg.ComTCPServer =
                        bool.Parse(ComNode.SelectSingleNode("ComTCPServer").Attributes["ComTCPServer"].InnerText);

                    ConfigInformation.GetInstance().tComCfg.TCPServerIP =
                        ComNode.SelectSingleNode("TCPServerIP").Attributes["TCPServerIP"].InnerText;
                    ConfigInformation.GetInstance().tComCfg.TCPServerPort =
                        ComNode.SelectSingleNode("TCPServerPort").Attributes["TCPServerPort"].InnerText;
                    ConfigInformation.GetInstance().tComCfg.TCPLocalIP =
                        ComNode.SelectSingleNode("TCPLocalIP").Attributes["TCPLocalIP"].InnerText;
                    ConfigInformation.GetInstance().tComCfg.TCPLocalPort =
                        ComNode.SelectSingleNode("TCPLocalPort").Attributes["TCPLocalPort"].InnerText;

                    if (ComNode.SelectSingleNode("TrigMode").Attributes["TrigMode"].InnerText ==
                        ConfigInformation.GetInstance().tComCfg.TrigModeDic[CComConfig.TrigModeEnum.COM_TRIG])
                    {
                        ConfigInformation.GetInstance().tComCfg.TrigMode = CComConfig.TrigModeEnum.COM_TRIG;
                    }
                    else if (ComNode.SelectSingleNode("TrigMode").Attributes["TrigMode"].InnerText ==
                        ConfigInformation.GetInstance().tComCfg.TrigModeDic[CComConfig.TrigModeEnum.TIME_TRIG])
                    {
                        ConfigInformation.GetInstance().tComCfg.TrigMode = CComConfig.TrigModeEnum.TIME_TRIG;
                    }

                    ConfigInformation.GetInstance().tComCfg.TrigStr =
                        ComNode.SelectSingleNode("TrigStr").Attributes["TrigStr"].InnerText;
                    ConfigInformation.GetInstance().tComCfg.TrigTime =
                        int.Parse(ComNode.SelectSingleNode("TrigTime").Attributes["TrigTime"].InnerText);
                }
                //load camera config
                {
                    
                    XmlNode CameraNode = rootNode.SelectSingleNode("CameraConfig");
                    //Row
                    int rowTest = int.Parse(CameraNode.SelectSingleNode("Rows").Attributes["Rows"].InnerText);
                    int[] rowRange;
                    Svision.GetMe().baslerCamera.getCameraRowRange(out rowRange);
                    if (rowTest>rowRange[1])
                    {
                        throw new Exception("错误！当前导入工程设置的相机行数大于当前使用相机行数的最大值！相机配置、标定配置和文件配置部分内容导入失败！");
                    }
                    Svision.GetMe().baslerCamera.setRowNumber(int.Parse(CameraNode.SelectSingleNode("Rows").Attributes["Rows"].InnerText));
                    ConfigInformation.GetInstance().tCamCfg.rows = int.Parse(CameraNode.SelectSingleNode("Rows").Attributes["Rows"].InnerText);
                    Svision.GetMe().oriRowNumber = ConfigInformation.GetInstance().tCamCfg.rows;
                    //Column
                    int columnTest = int.Parse(CameraNode.SelectSingleNode("Columns").Attributes["Columns"].InnerText);
                    int[] columnRange;
                    Svision.GetMe().baslerCamera.getCameraColumnRange(out columnRange);
                    if (columnTest > columnRange[1])
                    {
                        throw new Exception("错误！当前导入工程设置的相机行数大于当前使用相机列数的最大值！相机配置、标定配置和文件配置部分内容导入失败！");
                    }
                    Svision.GetMe().baslerCamera.setColumnNumber( int.Parse(CameraNode.SelectSingleNode("Columns").Attributes["Columns"].InnerText));
                    ConfigInformation.GetInstance().tCamCfg.columns = int.Parse(CameraNode.SelectSingleNode("Columns").Attributes["Columns"].InnerText);
                    Svision.GetMe().oriColumnNumber = ConfigInformation.GetInstance().tCamCfg.columns;
                    //XOffset
                    int XOffsetTest = int.Parse(CameraNode.SelectSingleNode("XOffset").Attributes["XOffset"].InnerText);
                    int[] XOffsetRange;
                    Svision.GetMe().baslerCamera.getCameraXOffsetRange(out XOffsetRange);
                    if (XOffsetTest > XOffsetRange[1])
                    {
                        throw new Exception("错误！当前导入工程设置的相机行偏移大于当前使用相机行偏移的最大值！相机配置、标定配置和文件配置部分内容导入失败！");
                    }
                    Svision.GetMe().baslerCamera.setOffsetX(int.Parse(CameraNode.SelectSingleNode("XOffset").Attributes["XOffset"].InnerText));
                    ConfigInformation.GetInstance().tCamCfg.xOffset = int.Parse(CameraNode.SelectSingleNode("XOffset").Attributes["XOffset"].InnerText);
                    //YOffset
                    int YOffsetTest = int.Parse(CameraNode.SelectSingleNode("YOffset").Attributes["YOffset"].InnerText);
                    int[] YOffsetRange;
                    Svision.GetMe().baslerCamera.getCameraYOffsetRange(out YOffsetRange);
                    if (YOffsetTest > YOffsetRange[1])
                    {
                        throw new Exception("错误！当前导入工程设置的相机列偏移大于当前使用相机列偏移的最大值！相机配置、标定配置和文件配置部分内容导入失败！");
                    }
                    Svision.GetMe().baslerCamera.setOffsetY(int.Parse(CameraNode.SelectSingleNode("YOffset").Attributes["YOffset"].InnerText));
                    ConfigInformation.GetInstance().tCamCfg.yOffset = int.Parse(CameraNode.SelectSingleNode("YOffset").Attributes["YOffset"].InnerText);
                    //ChannelNum
                    int channelMax;
                    Svision.GetMe().baslerCamera.getCameraMaxChannel(out channelMax);
                    if (int.Parse(CameraNode.SelectSingleNode("ChannelNum").Attributes["ChannelNum"].InnerText) == 1)
                    {
                        Svision.GetMe().baslerCamera.setChannelNumber(1);
                        ConfigInformation.GetInstance().tCamCfg.channelNum = int.Parse(CameraNode.SelectSingleNode("ChannelNum").Attributes["ChannelNum"].InnerText);
                    }
                    else if (int.Parse(CameraNode.SelectSingleNode("ChannelNum").Attributes["ChannelNum"].InnerText) == 3 && channelMax == 3)
                    {
                        Svision.GetMe().baslerCamera.setChannelNumber(3);
                        ConfigInformation.GetInstance().tCamCfg.channelNum = int.Parse(CameraNode.SelectSingleNode("ChannelNum").Attributes["ChannelNum"].InnerText);
                    }
                    else
                    {
                        throw new Exception("导入相机通道设置失败！当前相机为黑白相机，无法设置获取彩色图像！");
                    }
                    
                    //Gamma
                    Svision.GetMe().baslerCamera.setGammaPercent(double.Parse(CameraNode.SelectSingleNode("GammaPercent").Attributes["GammaPercent"].InnerText));
                    ConfigInformation.GetInstance().tCamCfg.gammaPercent = double.Parse(CameraNode.SelectSingleNode("GammaPercent").Attributes["GammaPercent"].InnerText);

                    //Gain
                    if (bool.Parse(CameraNode.SelectSingleNode("IsGainAuto").Attributes["IsGainAuto"].InnerText))
                    {
                        Svision.GetMe().baslerCamera.setGainAuto(true);
                        ConfigInformation.GetInstance().tCamCfg.isGainAuto = bool.Parse(CameraNode.SelectSingleNode("IsGainAuto").Attributes["IsGainAuto"].InnerText);
                        ConfigInformation.GetInstance().tCamCfg.gainPercent = Svision.GetMe().baslerCamera.getGainPercent(); 
                    }
                    else
                    {
                        Svision.GetMe().baslerCamera.setGainAuto(false);
                        ConfigInformation.GetInstance().tCamCfg.isGainAuto = bool.Parse(CameraNode.SelectSingleNode("IsGainAuto").Attributes["IsGainAuto"].InnerText);
                        Svision.GetMe().baslerCamera.setGainPercent(double.Parse(CameraNode.SelectSingleNode("GainPercent").Attributes["GainPercent"].InnerText));
                        ConfigInformation.GetInstance().tCamCfg.gainPercent = double.Parse(CameraNode.SelectSingleNode("GainPercent").Attributes["GainPercent"].InnerText);
                    }

                    //Exposure
                    if (bool.Parse(CameraNode.SelectSingleNode("IsExposureAuto").Attributes["IsExposureAuto"].InnerText))
                    {
                        Svision.GetMe().baslerCamera.setExposureAuto(true);
                        ConfigInformation.GetInstance().tCamCfg.exposurePercent = Svision.GetMe().baslerCamera.getExposurePercent();
                        ConfigInformation.GetInstance().tCamCfg.isExposureAuto = bool.Parse(CameraNode.SelectSingleNode("IsExposureAuto").Attributes["IsExposureAuto"].InnerText);
                    }
                    else
                    {
                        Svision.GetMe().baslerCamera.setExposureAuto(false);
                        ConfigInformation.GetInstance().tCamCfg.isExposureAuto = bool.Parse(CameraNode.SelectSingleNode("IsExposureAuto").Attributes["IsExposureAuto"].InnerText);
                        Svision.GetMe().baslerCamera.setExposurePercent(double.Parse(CameraNode.SelectSingleNode("ExposurePercent").Attributes["ExposurePercent"].InnerText));
                        ConfigInformation.GetInstance().tCamCfg.exposurePercent = double.Parse(CameraNode.SelectSingleNode("ExposurePercent").Attributes["ExposurePercent"].InnerText);
                    }

                    //WhiteBalance
                    if (bool.Parse(CameraNode.SelectSingleNode("IsWhiteBalanceAuto").Attributes["IsWhiteBalanceAuto"].InnerText))
                    {
                        Svision.GetMe().baslerCamera.setWhiteBalanceAuto(true);
                        ConfigInformation.GetInstance().tCamCfg.isWhiteBalanceAuto = bool.Parse(CameraNode.SelectSingleNode("IsWhiteBalanceAuto").Attributes["IsWhiteBalanceAuto"].InnerText);
                    }
                    else
                    {
                        Svision.GetMe().baslerCamera.setWhiteBalanceAuto(false);
                        ConfigInformation.GetInstance().tCamCfg.isWhiteBalanceAuto = bool.Parse(CameraNode.SelectSingleNode("IsWhiteBalanceAuto").Attributes["IsWhiteBalanceAuto"].InnerText);
                        if (Svision.GetMe().baslerCamera.getChannelNumber() == 3)
                        {
                            Svision.GetMe().baslerCamera.setWhiteBalanceBlue(double.Parse(CameraNode.SelectSingleNode("WhiteBalanceBluePercent").Attributes["WhiteBalanceBluePercent"].InnerText));
                            Svision.GetMe().baslerCamera.setWhiteBalanceGreen(double.Parse(CameraNode.SelectSingleNode("WhiteBalanceGreenPercent").Attributes["WhiteBalanceGreenPercent"].InnerText));
                            Svision.GetMe().baslerCamera.setWhiteBalanceRed(double.Parse(CameraNode.SelectSingleNode("WhiteBalanceRedPercent").Attributes["WhiteBalanceRedPercent"].InnerText));

                            ConfigInformation.GetInstance().tCamCfg.whiteBalanceBluePercent = double.Parse(CameraNode.SelectSingleNode("WhiteBalanceBluePercent").Attributes["WhiteBalanceBluePercent"].InnerText);
                            ConfigInformation.GetInstance().tCamCfg.whiteBalanceGreenPercent = double.Parse(CameraNode.SelectSingleNode("WhiteBalanceGreenPercent").Attributes["WhiteBalanceGreenPercent"].InnerText);
                            ConfigInformation.GetInstance().tCamCfg.whiteBalanceRedPercent = double.Parse(CameraNode.SelectSingleNode("WhiteBalanceRedPercent").Attributes["WhiteBalanceRedPercent"].InnerText);
                        }


                    }

            
                }
                //load file config
                {
                    XmlNode FileNode = null;
                    bool isFileOK = true;
                    try
                    {
                        FileNode = rootNode.SelectSingleNode("FileConfig");
                    }
                    catch (System.Exception ex)
                    {
                        isFileOK = false;
                    }
                    if (isFileOK&&FileNode!=null)
                    {
                        ConfigInformation.GetInstance().tFileCfg.rows = int.Parse(FileNode.SelectSingleNode("Rows").Attributes["Rows"].InnerText);
                        ConfigInformation.GetInstance().tFileCfg.columns = int.Parse(FileNode.SelectSingleNode("Columns").Attributes["Columns"].InnerText);
                        ConfigInformation.GetInstance().tFileCfg.channelNum = int.Parse(FileNode.SelectSingleNode("ChannelNum").Attributes["ChannelNum"].InnerText);
                        Svision.GetMe().checkBoxIsFile.Checked = bool.Parse(FileNode.SelectSingleNode("IsFileInput").Attributes["IsFileInput"].InnerText);
                        if (Svision.GetMe().checkBoxIsFile.Checked)
                        {
                            Svision.GetMe().oriRowNumber = ConfigInformation.GetInstance().tFileCfg.rows;
                            Svision.GetMe().oriColumnNumber = ConfigInformation.GetInstance().tFileCfg.columns;
                        }
                    }
                }
                //load calibration config
                {
                    XmlNode CalNode = rootNode.SelectSingleNode("CalibrationConfig");

                    ConfigInformation.GetInstance().tCalCfg.calibrationDistance = double.Parse(CalNode.SelectSingleNode("calibrationDistance").Attributes["calibrationDistance"].InnerText);
                    ConfigInformation.GetInstance().tCalCfg.calibrationPoint = int.Parse(CalNode.SelectSingleNode("calibrationPoint").Attributes["calibrationPoint"].InnerText);
                    ConfigInformation.GetInstance().tCalCfg.focus = int.Parse(CalNode.SelectSingleNode("focus").Attributes["focus"].InnerText);
                    ConfigInformation.GetInstance().tCalCfg.calibrationRate = double.Parse(CalNode.SelectSingleNode("calibrationRate").Attributes["calibrationRate"].InnerText);
                    ConfigInformation.GetInstance().tCalCfg.pixelSize = double.Parse(CalNode.SelectSingleNode("pixelSize").Attributes["pixelSize"].InnerText);
                    ConfigInformation.GetInstance().tCalCfg.isCheckBoxRadialDistortion = bool.Parse(CalNode.SelectSingleNode("isCheckBoxRadialDistortion").Attributes["isCheckBoxRadialDistortion"].InnerText);
                    ConfigInformation.GetInstance().tCalCfg.calibrationParaConfirmFlag = bool.Parse(CalNode.SelectSingleNode("calibrationParaConfirmFlag").Attributes["calibrationParaConfirmFlag"].InnerText);
                    ConfigInformation.GetInstance().tCalCfg.calibrationIsRadialDistortionFlag = bool.Parse(CalNode.SelectSingleNode("calibrationIsRadialDistortionFlag").Attributes["calibrationIsRadialDistortionFlag"].InnerText);
                    if (ConfigInformation.GetInstance().tCalCfg.calibrationIsRadialDistortionFlag)
                    {
                        HOperatorSet.ReadImage(out ConfigInformation.GetInstance().tCalCfg.ho_MapFixed, (HTuple)tmpPath.Remove(tmpPath.LastIndexOf("\\") + 1) + "_calibrationMap.tiff");
                    }
                    XmlNode useThreePointCalibrationNode = null;
                    bool isuseThreePointCalibrationOK = true;
                    try
                    {
                        useThreePointCalibrationNode = CalNode.SelectSingleNode("useThreePointCalibration");
                    }
                    catch (System.Exception ex)
                    {
                        isuseThreePointCalibrationOK = false;
                    }
                    if (isuseThreePointCalibrationOK && useThreePointCalibrationNode != null)
                    {

                        ConfigInformation.GetInstance().tCalCfg.useThreePointCalibration = bool.Parse(CalNode.SelectSingleNode("useThreePointCalibration").Attributes["useThreePointCalibration"].InnerText);
                        ConfigInformation.GetInstance().tCalCfg.p1x = double.Parse(CalNode.SelectSingleNode("p1x").Attributes["p1x"].InnerText);
                        ConfigInformation.GetInstance().tCalCfg.p2x = double.Parse(CalNode.SelectSingleNode("p2x").Attributes["p2x"].InnerText);
                        ConfigInformation.GetInstance().tCalCfg.p3x = double.Parse(CalNode.SelectSingleNode("p3x").Attributes["p3x"].InnerText);
                        ConfigInformation.GetInstance().tCalCfg.r1x = double.Parse(CalNode.SelectSingleNode("r1x").Attributes["r1x"].InnerText);
                        ConfigInformation.GetInstance().tCalCfg.r2x = double.Parse(CalNode.SelectSingleNode("r2x").Attributes["r2x"].InnerText);
                        ConfigInformation.GetInstance().tCalCfg.r3x = double.Parse(CalNode.SelectSingleNode("r3x").Attributes["r3x"].InnerText);
                        ConfigInformation.GetInstance().tCalCfg.p1y = double.Parse(CalNode.SelectSingleNode("p1y").Attributes["p1y"].InnerText);
                        ConfigInformation.GetInstance().tCalCfg.p2y = double.Parse(CalNode.SelectSingleNode("p2y").Attributes["p2y"].InnerText);
                        ConfigInformation.GetInstance().tCalCfg.p3y = double.Parse(CalNode.SelectSingleNode("p3y").Attributes["p3y"].InnerText);
                        ConfigInformation.GetInstance().tCalCfg.r1y = double.Parse(CalNode.SelectSingleNode("r1y").Attributes["r1y"].InnerText);
                        ConfigInformation.GetInstance().tCalCfg.r2y = double.Parse(CalNode.SelectSingleNode("r2y").Attributes["r2y"].InnerText);
                        ConfigInformation.GetInstance().tCalCfg.r3y = double.Parse(CalNode.SelectSingleNode("r3y").Attributes["r3y"].InnerText);
                        ConfigInformation.GetInstance().tCalCfg.Sx = double.Parse(CalNode.SelectSingleNode("Sx").Attributes["Sx"].InnerText);
                        ConfigInformation.GetInstance().tCalCfg.Sy = double.Parse(CalNode.SelectSingleNode("Sy").Attributes["Sy"].InnerText);
                        ConfigInformation.GetInstance().tCalCfg.thea = double.Parse(CalNode.SelectSingleNode("thea").Attributes["thea"].InnerText);
                        ConfigInformation.GetInstance().tCalCfg.Tx = double.Parse(CalNode.SelectSingleNode("Tx").Attributes["Tx"].InnerText);
                        ConfigInformation.GetInstance().tCalCfg.Ty = double.Parse(CalNode.SelectSingleNode("Ty").Attributes["Ty"].InnerText);
                        if (ConfigInformation.GetInstance().tCalCfg.useThreePointCalibration)
                        {
                             double[][] a = { new double[] { double.Parse(CalNode.SelectSingleNode("mx1").Attributes["mx1"].InnerText)},
                             new double[] { double.Parse(CalNode.SelectSingleNode("mx2").Attributes["mx2"].InnerText)},
                             new double[] { double.Parse(CalNode.SelectSingleNode("mx3").Attributes["mx3"].InnerText)},
                             new double[] { double.Parse(CalNode.SelectSingleNode("mx4").Attributes["mx4"].InnerText)},
                             new double[] { double.Parse(CalNode.SelectSingleNode("mx5").Attributes["mx5"].InnerText)},
                             new double[] { double.Parse(CalNode.SelectSingleNode("mx6").Attributes["mx6"].InnerText)}
                           };
                            ConfigInformation.GetInstance().tCalCfg.mx = Matrix.Create(a);
                        }
                        
                    }
                }
                
            }
            catch (System.Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
