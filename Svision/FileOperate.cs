/**************************************************************************************
**
**       Filename:  FileOperate.cs
**
**    Description:  Save the code program which user created
**
**        Version:  1.0
**        Created:  2016-4-21
**       Revision:  v02.0007
**       Compiler:  vs2010
**        Company:  SIASUN
**
****************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Xml;
using System.IO;

//using Microsoft.Office.Core;
//using Microsoft.Office.Interop.Excel;
//using Excel = Microsoft.Office.Interop.Excel;
using HalconDotNet;
using System.Drawing;


namespace Svision
{

    public class FileOperate
    {
        //Excel.Worksheet gMainSheet;

        //private bool fileOpened;        //文件是否被打开
        public string mFilename;
        public string mFilenameOnly;
        public FileOperate()
        {
            ////gExcelEdit = new ExcelEdit();
            //fileOpened = false;


            tXmlDoc = new XmlDocument();

        }
        public XmlDocument tXmlDoc;

        //public ExcelEdit gExcelEdit;

        public bool SaveProjToXml(String tPath, String tPathOnly, String tFilename)
        {
            try
            {
                XmlDeclaration xmldel;
                xmldel = tXmlDoc.CreateXmlDeclaration("1.0", null, null);
                tXmlDoc.AppendChild(xmldel);
                XmlElement xmlelem = tXmlDoc.CreateElement("", "Program", "");
                tXmlDoc.AppendChild(xmlelem);
                mFilename = tPathOnly;
                mFilenameOnly = tFilename.Split('.')[0];
                //gExcelEdit.mFilename = tPath;
                //gExcelEdit.Create();
                //gMainSheet = gExcelEdit.AddSheet("Program");

                //     int tmpShapeModelIdx = 0, tmpAnsioShapeModelIdx = 0;
                //gExcelEdit.SetCellValue(gMainSheet, 3, 4, UserCode.GetInstance().gProCd[0].FuncID);
                XmlElement xmlelemShowCurIdx = tXmlDoc.CreateElement("ShowCurIdx");
                xmlelemShowCurIdx.SetAttribute("ShowCurIdx", Svision.GetMe().listBoxProcess.SelectedIndex.ToString());
                xmlelem.AppendChild(xmlelemShowCurIdx);
                XmlElement xmlelemDoNotShowImage = tXmlDoc.CreateElement("DoNotShowImage");
                xmlelemDoNotShowImage.SetAttribute("DoNotShowImage", Svision.GetMe().checkBoxDoNotShowImage.Checked.ToString());
                xmlelem.AppendChild(xmlelemDoNotShowImage);
                XmlElement xmlelemOrder = tXmlDoc.CreateElement("Order");
                xmlelem.AppendChild(xmlelemOrder);
                XmlElement xmlelemIsOverFlag = tXmlDoc.CreateElement("isOverFlag");
                xmlelem.AppendChild(xmlelemIsOverFlag);
                for (int i = 0; i < 20; i++)
                {
                    XmlElement xmlelemOneOrder = tXmlDoc.CreateElement("Order" + i.ToString());
                    xmlelemOneOrder.SetAttribute("Order" + i.ToString(), UserCode.GetInstance().codeInfoValToKey[UserCode.GetInstance().gProCd[i].FuncID]);
                    xmlelemOrder.AppendChild(xmlelemOneOrder);

                    XmlElement xmlelemOneIsOverFlag = tXmlDoc.CreateElement("isOverFlag" + i.ToString());
                    xmlelemOneIsOverFlag.SetAttribute("isOverFlag" + i.ToString(), UserCode.GetInstance().isOverFlag[i].ToString());
                    xmlelemIsOverFlag.AppendChild(xmlelemOneIsOverFlag);
                    switch (UserCode.GetInstance().gProCd[i].FuncID)
                    {
                        case ProCodeCls.MainFunction.NullFBD:
                            XmlElement rootNUllFBD;
                            SaveNullFBDXML(i, out rootNUllFBD);
                            xmlelem.AppendChild(rootNUllFBD);
                            break;
                        case ProCodeCls.MainFunction.InputCameraInputFBD:
                            XmlElement rootCameraInputFBD;
                            SaveCameraInputFBDXML(i, out  rootCameraInputFBD);
                            xmlelem.AppendChild(rootCameraInputFBD);
                            break;
                        case ProCodeCls.MainFunction.CalibrationThresholdFBD:
                            XmlElement rootThresholdFBD;
                            SaveThresholdFBDXML(i, out rootThresholdFBD);
                            xmlelem.AppendChild(rootThresholdFBD);
                            break;
                        case ProCodeCls.MainFunction.CalibrationMedianFilterFBD:
                            XmlElement rootMedianFilterFBD;
                            SaveMedianFilterFBDXML(i, out rootMedianFilterFBD);
                            xmlelem.AppendChild(rootMedianFilterFBD);
                            break;
                        case ProCodeCls.MainFunction.CalibrationBackgroundRemoveFBD:
                            XmlElement rootBackgroundRemoveFBD;
                            SaveBackgroundRemoveFBDXML(i, out rootBackgroundRemoveFBD);
                            xmlelem.AppendChild(rootBackgroundRemoveFBD);
                            break;
                        case ProCodeCls.MainFunction.CalibrationMorphologyProcessingFBD:
                            XmlElement rootMorphologyProcessing;
                            SaveMorphologyProcessingFBDXML(i, out rootMorphologyProcessing);
                            xmlelem.AppendChild(rootMorphologyProcessing);
                            break;
                        case ProCodeCls.MainFunction.MeasureBlobAnalysisFBD:
                            XmlElement rootBlobAnalysisFBD;
                            SaveBlobAnalysisFBDXML(i, out rootBlobAnalysisFBD);
                            xmlelem.AppendChild(rootBlobAnalysisFBD);
                            break;
                        case ProCodeCls.MainFunction.MeasureShapeSearchFBD:
                            XmlElement rootShapeSearchFBD;
                            SaveShapeSearchFBDXML(i, out rootShapeSearchFBD);
                            xmlelem.AppendChild(rootShapeSearchFBD);
                            break;
                        case ProCodeCls.MainFunction.MeasureAnisoShapeSearchFBD:
                            XmlElement rootAnisoShapeSearchFBD;
                            SaveAnisoShapeSearchFBDXML(i, out rootAnisoShapeSearchFBD);
                            xmlelem.AppendChild(rootAnisoShapeSearchFBD);
                            break;
                        case ProCodeCls.MainFunction.OutputSerialOutputFBD:
                            XmlElement rootSerialOutputFBD;
                            SaveSerialOutputFBDXML(i, out rootSerialOutputFBD);
                            xmlelem.AppendChild(rootSerialOutputFBD);
                            break;
                    }
                }

                //gExcelEdit.Save();
                //gExcelEdit.Close();

                String tmpPath = tPath.Substring(0, tPath.IndexOf(tFilename, 0));
                String tmpFileName = tFilename.Substring(0, tFilename.IndexOf("."));
                tmpPath = tmpPath + tmpFileName + "_CfgInfoOperate.xml";
                tXmlDoc.Save(tPath);
                ConfigInformation.GetInstance().tCfgInfoOprt.SaveCfgInfo(tmpPath);
            }
            catch (System.Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return true;

        }
        private void SaveNullFBDXML(int idx, out XmlElement rootNUllFBD)
        {

            rootNUllFBD = tXmlDoc.CreateElement(UserCode.GetInstance().codeInfoValToKey[UserCode.GetInstance().gProCd[idx].FuncID] + "_" + idx.ToString());

        }
        private void SaveCameraInputFBDXML(int idx, out XmlElement rootCameraInputFBD)
        {
            rootCameraInputFBD = tXmlDoc.CreateElement(UserCode.GetInstance().codeInfoValToKey[UserCode.GetInstance().gProCd[idx].FuncID] + "_" + idx.ToString());
        }
        private void SaveThresholdFBDXML(int idx, out XmlElement rootThresholdFBD)
        {
            rootThresholdFBD = tXmlDoc.CreateElement(UserCode.GetInstance().codeInfoValToKey[UserCode.GetInstance().gProCd[idx].FuncID] + "_" + idx.ToString());
            XmlElement MinValueElement = tXmlDoc.CreateElement("MinValue");
            MinValueElement.SetAttribute("MinValue", UserCode.GetInstance().gProCd[idx].gTP.minValue.ToString());
            XmlElement MaxValueElement = tXmlDoc.CreateElement("MaxValue");
            MaxValueElement.SetAttribute("MaxValue", UserCode.GetInstance().gProCd[idx].gTP.maxValue.ToString());
            rootThresholdFBD.AppendChild(MinValueElement);
            rootThresholdFBD.AppendChild(MaxValueElement);

        }
        private void SaveMedianFilterFBDXML(int idx, out XmlElement rootMedianFilterFBD)
        {
            rootMedianFilterFBD = tXmlDoc.CreateElement(UserCode.GetInstance().codeInfoValToKey[UserCode.GetInstance().gProCd[idx].FuncID] + "_" + idx.ToString());

            XmlElement MaskSizeElement = tXmlDoc.CreateElement("MaskSize");
            MaskSizeElement.SetAttribute("MaskSize", UserCode.GetInstance().gProCd[idx].gMFP.maskSize.ToString());
            rootMedianFilterFBD.AppendChild(MaskSizeElement);

        }
        private void SaveBackgroundRemoveFBDXML(int idx, out XmlElement rootBackgroundRemoveFBD)
        {
            rootBackgroundRemoveFBD = tXmlDoc.CreateElement(UserCode.GetInstance().codeInfoValToKey[UserCode.GetInstance().gProCd[idx].FuncID] + "_" + idx.ToString());

            XmlElement IsAllColorElement = tXmlDoc.CreateElement("IsAllColor");
            IsAllColorElement.SetAttribute("IsAllColor", UserCode.GetInstance().gProCd[idx].gBRP.isAllColor.ToString());
            rootBackgroundRemoveFBD.AppendChild(IsAllColorElement);

            XmlElement GrayValue0Element = tXmlDoc.CreateElement("GrayValue0");
            GrayValue0Element.SetAttribute("GrayValue0", UserCode.GetInstance().gProCd[idx].gBRP.grayValue[0].ToString());
            rootBackgroundRemoveFBD.AppendChild(GrayValue0Element);

            XmlElement GrayValue1Element = tXmlDoc.CreateElement("GrayValue1");
            GrayValue1Element.SetAttribute("GrayValue1", UserCode.GetInstance().gProCd[idx].gBRP.grayValue[1].ToString());
            rootBackgroundRemoveFBD.AppendChild(GrayValue1Element);

            XmlElement GrayValue2Element = tXmlDoc.CreateElement("GrayValue2");
            GrayValue2Element.SetAttribute("GrayValue2", UserCode.GetInstance().gProCd[idx].gBRP.grayValue[2].ToString());
            rootBackgroundRemoveFBD.AppendChild(GrayValue2Element);

            XmlElement GrayValue3Element = tXmlDoc.CreateElement("GrayValue3");
            GrayValue3Element.SetAttribute("GrayValue3", UserCode.GetInstance().gProCd[idx].gBRP.grayValue[3].ToString());
            rootBackgroundRemoveFBD.AppendChild(GrayValue3Element);

            XmlElement GrayValue4Element = tXmlDoc.CreateElement("GrayValue4");
            GrayValue4Element.SetAttribute("GrayValue4", UserCode.GetInstance().gProCd[idx].gBRP.grayValue[4].ToString());
            rootBackgroundRemoveFBD.AppendChild(GrayValue4Element);

            XmlElement GrayValue5Element = tXmlDoc.CreateElement("GrayValue5");
            GrayValue5Element.SetAttribute("GrayValue5", UserCode.GetInstance().gProCd[idx].gBRP.grayValue[5].ToString());
            rootBackgroundRemoveFBD.AppendChild(GrayValue5Element);

            XmlElement GrayValue6Element = tXmlDoc.CreateElement("GrayValue6");
            GrayValue6Element.SetAttribute("GrayValue6", UserCode.GetInstance().gProCd[idx].gBRP.grayValue[6].ToString());
            rootBackgroundRemoveFBD.AppendChild(GrayValue6Element);

            XmlElement GrayValue7Element = tXmlDoc.CreateElement("GrayValue7");
            GrayValue7Element.SetAttribute("GrayValue7", UserCode.GetInstance().gProCd[idx].gBRP.grayValue[7].ToString());
            rootBackgroundRemoveFBD.AppendChild(GrayValue7Element);


        }
        private void SaveMorphologyProcessingFBDXML(int idx, out XmlElement rootMorphologyProcessing)
        {
            rootMorphologyProcessing = tXmlDoc.CreateElement(UserCode.GetInstance().codeInfoValToKey[UserCode.GetInstance().gProCd[idx].FuncID] + "_" + idx.ToString());

            XmlElement processIDElement = tXmlDoc.CreateElement("ProcessID");
            processIDElement.SetAttribute("ProcessID", UserCode.GetInstance().gProCd[idx].gMPP.processID.ToString());
            rootMorphologyProcessing.AppendChild(processIDElement);

            XmlElement elementIDElement = tXmlDoc.CreateElement("ElementID");
            elementIDElement.SetAttribute("ElementID", UserCode.GetInstance().gProCd[idx].gMPP.elementID.ToString());
            rootMorphologyProcessing.AppendChild(elementIDElement);

            XmlElement widthElement = tXmlDoc.CreateElement("Width");
            widthElement.SetAttribute("Width", UserCode.GetInstance().gProCd[idx].gMPP.width.ToString());
            rootMorphologyProcessing.AppendChild(widthElement);

            XmlElement heightElement = tXmlDoc.CreateElement("Height");
            heightElement.SetAttribute("Height", UserCode.GetInstance().gProCd[idx].gMPP.height.ToString());
            rootMorphologyProcessing.AppendChild(heightElement);

            XmlElement radiusElement = tXmlDoc.CreateElement("Radius");
            radiusElement.SetAttribute("Radius", UserCode.GetInstance().gProCd[idx].gMPP.radius.ToString());
            rootMorphologyProcessing.AppendChild(radiusElement);



        }
        private void SaveBlobAnalysisFBDXML(int idx, out XmlElement rootBlobAnalysisFBD)
        {
            rootBlobAnalysisFBD = tXmlDoc.CreateElement(UserCode.GetInstance().codeInfoValToKey[UserCode.GetInstance().gProCd[idx].FuncID] + "_" + idx.ToString());

            #region SegmentPara

            //isColor
            XmlElement isColorElement = tXmlDoc.CreateElement("IsColor");
            isColorElement.SetAttribute("IsColor", UserCode.GetInstance().gProCd[idx].gBP.isColor.ToString());
            rootBlobAnalysisFBD.AppendChild(isColorElement);

            //isAutoSegment
            XmlElement isAutoSegmentElement = tXmlDoc.CreateElement("IsAutoSegment");
            isAutoSegmentElement.SetAttribute("IsAutoSegment", UserCode.GetInstance().gProCd[idx].gBP.isAutoSegment.ToString());
            rootBlobAnalysisFBD.AppendChild(isAutoSegmentElement);

            //SegmentSelectPara
            XmlElement SegmentSelectParaElement = tXmlDoc.CreateElement("SegmentSelectPara");
            rootBlobAnalysisFBD.AppendChild(SegmentSelectParaElement);

            XmlElement SegmentSelectParaLengthElement = tXmlDoc.CreateElement("SegmentSelectParaLength");
            SegmentSelectParaLengthElement.SetAttribute("SegmentSelectParaLength", "6");
            SegmentSelectParaElement.AppendChild(SegmentSelectParaLengthElement);

            XmlElement selectedColorElement = tXmlDoc.CreateElement("selectedColor");
            SegmentSelectParaElement.AppendChild(selectedColorElement);

            XmlElement redValueElement = tXmlDoc.CreateElement("redValue");
            SegmentSelectParaElement.AppendChild(redValueElement);

            XmlElement greenValueElement = tXmlDoc.CreateElement("greenValue");
            SegmentSelectParaElement.AppendChild(greenValueElement);

            XmlElement blueValueElement = tXmlDoc.CreateElement("blueValue");
            SegmentSelectParaElement.AppendChild(blueValueElement);

            XmlElement grayValueElement = tXmlDoc.CreateElement("grayValue");
            SegmentSelectParaElement.AppendChild(grayValueElement);

            XmlElement lengthValueElement = tXmlDoc.CreateElement("lengthValue");
            SegmentSelectParaElement.AppendChild(lengthValueElement);

            XmlElement isBesideThisColorElement = tXmlDoc.CreateElement("isBesideThisColor");
            SegmentSelectParaElement.AppendChild(isBesideThisColorElement);

            for (int i = 0; i < 6; i++)
            {
                XmlElement selectedColorOneElement = tXmlDoc.CreateElement("selectedColor" + i.ToString());
                selectedColorOneElement.SetAttribute("selectedColor" + i.ToString(), UserCode.GetInstance().gProCd[idx].gBP.selectedColor[i].ToString());
                selectedColorElement.AppendChild(selectedColorOneElement);

                XmlElement redValueOneElement = tXmlDoc.CreateElement("redValue" + i.ToString());
                redValueOneElement.SetAttribute("redValue" + i.ToString(), UserCode.GetInstance().gProCd[idx].gBP.redValue[i].ToString());
                redValueElement.AppendChild(redValueOneElement);

                XmlElement greenValueOneElement = tXmlDoc.CreateElement("greenValue" + i.ToString());
                greenValueOneElement.SetAttribute("greenValue" + i.ToString(), UserCode.GetInstance().gProCd[idx].gBP.greenValue[i].ToString());
                greenValueElement.AppendChild(greenValueOneElement);

                XmlElement blueValueOneElement = tXmlDoc.CreateElement("blueValue" + i.ToString());
                blueValueOneElement.SetAttribute("blueValue" + i.ToString(), UserCode.GetInstance().gProCd[idx].gBP.blueValue[i].ToString());
                blueValueElement.AppendChild(blueValueOneElement);

                XmlElement grayValueOneElement = tXmlDoc.CreateElement("grayValue" + i.ToString());
                grayValueOneElement.SetAttribute("grayValue" + i.ToString(), UserCode.GetInstance().gProCd[idx].gBP.grayValue[i].ToString());
                grayValueElement.AppendChild(grayValueOneElement);

                XmlElement lengthValueOneElement = tXmlDoc.CreateElement("lengthValue" + i.ToString());
                lengthValueOneElement.SetAttribute("lengthValue" + i.ToString(), UserCode.GetInstance().gProCd[idx].gBP.lengthValue[i].ToString());
                lengthValueElement.AppendChild(lengthValueOneElement);

                XmlElement isBesideThisColorOneElement = tXmlDoc.CreateElement("isBesideThisColor" + i.ToString());
                isBesideThisColorOneElement.SetAttribute("isBesideThisColor" + i.ToString(), UserCode.GetInstance().gProCd[idx].gBP.isBesideThisColor[i].ToString());
                isBesideThisColorElement.AppendChild(isBesideThisColorOneElement);
            }

            //MaxGrayValue
            XmlElement MaxGrayValueElement = tXmlDoc.CreateElement("MaxGrayValue");
            MaxGrayValueElement.SetAttribute("MaxGrayValue", UserCode.GetInstance().gProCd[idx].gBP.MaxGrayValue.ToString());
            rootBlobAnalysisFBD.AppendChild(MaxGrayValueElement);

            //MinGrayValue
            XmlElement MinGrayValueElement = tXmlDoc.CreateElement("MinGrayValue");
            MinGrayValueElement.SetAttribute("MinGrayValue", UserCode.GetInstance().gProCd[idx].gBP.MinGrayValue.ToString());
            rootBlobAnalysisFBD.AppendChild(MinGrayValueElement);

            //isAutoSegmentMethod1
            XmlElement isAutoSegmentMethod1Element = tXmlDoc.CreateElement("IsAutoSegmentMethod1");
            isAutoSegmentMethod1Element.SetAttribute("IsAutoSegmentMethod1", UserCode.GetInstance().gProCd[idx].gBP.isAutoSegmentMethod1.ToString());
            rootBlobAnalysisFBD.AppendChild(isAutoSegmentMethod1Element);

            //isAutoSegmentMethod2
            XmlElement isAutoSegmentMethod2Element = tXmlDoc.CreateElement("IsAutoSegmentMethod2");
            isAutoSegmentMethod2Element.SetAttribute("IsAutoSegmentMethod2", UserCode.GetInstance().gProCd[idx].gBP.isAutoSegmentMethod2.ToString());
            rootBlobAnalysisFBD.AppendChild(isAutoSegmentMethod2Element);

            //isAutoSegmentMethod3
            XmlElement isAutoSegmentMethod3Element = tXmlDoc.CreateElement("IsAutoSegmentMethod3");
            isAutoSegmentMethod3Element.SetAttribute("IsAutoSegmentMethod3", UserCode.GetInstance().gProCd[idx].gBP.isAutoSegmentMethod3.ToString());
            rootBlobAnalysisFBD.AppendChild(isAutoSegmentMethod3Element);

            //autoSegmentMethod1Para1
            XmlElement autoSegmentMethod1Para1Element = tXmlDoc.CreateElement("AutoSegmentMethod1Para1");
            autoSegmentMethod1Para1Element.SetAttribute("AutoSegmentMethod1Para1", UserCode.GetInstance().gProCd[idx].gBP.autoSegmentMethod1Para1.ToString());
            rootBlobAnalysisFBD.AppendChild(autoSegmentMethod1Para1Element);

            //autoSegmentMethod2Para1
            XmlElement autoSegmentMethod2Para1Element = tXmlDoc.CreateElement("AutoSegmentMethod2Para1");
            autoSegmentMethod2Para1Element.SetAttribute("AutoSegmentMethod2Para1", UserCode.GetInstance().gProCd[idx].gBP.autoSegmentMethod2Para1.ToString());
            rootBlobAnalysisFBD.AppendChild(autoSegmentMethod2Para1Element);

            //autoSegmentMethod2Para2
            XmlElement autoSegmentMethod2Para2Element = tXmlDoc.CreateElement("AutoSegmentMethod2Para2");
            autoSegmentMethod2Para2Element.SetAttribute("AutoSegmentMethod2Para2", UserCode.GetInstance().gProCd[idx].gBP.autoSegmentMethod2Para2.ToString());
            rootBlobAnalysisFBD.AppendChild(autoSegmentMethod2Para2Element);

            //autoSegmentMethod2Para3
            XmlElement autoSegmentMethod2Para3Element = tXmlDoc.CreateElement("AutoSegmentMethod2Para3");
            autoSegmentMethod2Para3Element.SetAttribute("AutoSegmentMethod2Para3", UserCode.GetInstance().gProCd[idx].gBP.autoSegmentMethod2Para3.ToString());
            rootBlobAnalysisFBD.AppendChild(autoSegmentMethod2Para3Element);

            //autoSegmentMethod2Para4
            XmlElement autoSegmentMethod2Para4Element = tXmlDoc.CreateElement("AutoSegmentMethod2Para4");
            autoSegmentMethod2Para4Element.SetAttribute("AutoSegmentMethod2Para4", UserCode.GetInstance().gProCd[idx].gBP.autoSegmentMethod2Para4.ToString());
            rootBlobAnalysisFBD.AppendChild(autoSegmentMethod2Para4Element);

            //segmentShow
            XmlElement segmentShowElement = tXmlDoc.CreateElement("SegmentShow");
            segmentShowElement.SetAttribute("SegmentShow", UserCode.GetInstance().gProCd[idx].gBP.segmentShow.ToString());
            rootBlobAnalysisFBD.AppendChild(segmentShowElement);
            #endregion

            #region MorphologyPara

            //isFillUpHoles
            XmlElement isFillUpHolesElement = tXmlDoc.CreateElement("isFillUpHoles");
            isFillUpHolesElement.SetAttribute("isFillUpHoles", UserCode.GetInstance().gProCd[idx].gBP.isFillUpHoles.ToString());
            rootBlobAnalysisFBD.AppendChild(isFillUpHolesElement);

            //isConnectionBeforeFillUpHoles
            XmlElement isConnectionBeforeFillUpHolesElement = tXmlDoc.CreateElement("isConnectionBeforeFillUpHoles");
            isConnectionBeforeFillUpHolesElement.SetAttribute("isConnectionBeforeFillUpHoles", UserCode.GetInstance().gProCd[idx].gBP.isConnectionBeforeFillUpHoles.ToString());
            rootBlobAnalysisFBD.AppendChild(isConnectionBeforeFillUpHolesElement);

            //isErosion
            XmlElement isErosionElement = tXmlDoc.CreateElement("isErosion");
            isErosionElement.SetAttribute("isErosion", UserCode.GetInstance().gProCd[idx].gBP.isErosion.ToString());
            rootBlobAnalysisFBD.AppendChild(isErosionElement);

            //erosionElementNUMElement
            XmlElement erosionElementNUMElement = tXmlDoc.CreateElement("erosionElementNUM");
            erosionElementNUMElement.SetAttribute("erosionElementNUM", UserCode.GetInstance().gProCd[idx].gBP.erosionElementNUM.ToString());
            rootBlobAnalysisFBD.AppendChild(erosionElementNUMElement);

            //erosionRWidth
            XmlElement erosionRWidthElement = tXmlDoc.CreateElement("erosionRWidth");
            erosionRWidthElement.SetAttribute("erosionRWidth", UserCode.GetInstance().gProCd[idx].gBP.erosionRWidth.ToString());
            rootBlobAnalysisFBD.AppendChild(erosionRWidthElement);

            //erosionRHeight
            XmlElement erosionRHeightElement = tXmlDoc.CreateElement("erosionRHeight");
            erosionRHeightElement.SetAttribute("erosionRHeight", UserCode.GetInstance().gProCd[idx].gBP.erosionRHeight.ToString());
            rootBlobAnalysisFBD.AppendChild(erosionRHeightElement);

            //erosionCRadius
            XmlElement erosionCRadiusElement = tXmlDoc.CreateElement("erosionCRadius");
            erosionCRadiusElement.SetAttribute("erosionCRadius", UserCode.GetInstance().gProCd[idx].gBP.erosionCRadius.ToString());
            rootBlobAnalysisFBD.AppendChild(erosionCRadiusElement);

            //isDilation
            XmlElement isDilationElement = tXmlDoc.CreateElement("isDilation");
            isDilationElement.SetAttribute("isDilation", UserCode.GetInstance().gProCd[idx].gBP.isDilation.ToString());
            rootBlobAnalysisFBD.AppendChild(isDilationElement);

            //dilationElementNUM
            XmlElement dilationElementNUMElement = tXmlDoc.CreateElement("dilationElementNUM");
            dilationElementNUMElement.SetAttribute("dilationElementNUM", UserCode.GetInstance().gProCd[idx].gBP.dilationElementNUM.ToString());
            rootBlobAnalysisFBD.AppendChild(dilationElementNUMElement);

            //dilationRWidth
            XmlElement dilationRWidthElement = tXmlDoc.CreateElement("dilationRWidth");
            dilationRWidthElement.SetAttribute("dilationRWidth", UserCode.GetInstance().gProCd[idx].gBP.dilationRWidth.ToString());
            rootBlobAnalysisFBD.AppendChild(dilationRWidthElement);

            //dilationRHeight
            XmlElement dilationRHeightElement = tXmlDoc.CreateElement("dilationRHeight");
            dilationRHeightElement.SetAttribute("dilationRHeight", UserCode.GetInstance().gProCd[idx].gBP.dilationRHeight.ToString());
            rootBlobAnalysisFBD.AppendChild(dilationRHeightElement);

            //dilationCRadius
            XmlElement dilationCRadiusElement = tXmlDoc.CreateElement("dilationCRadius");
            dilationCRadiusElement.SetAttribute("dilationCRadius", UserCode.GetInstance().gProCd[idx].gBP.dilationCRadius.ToString());
            rootBlobAnalysisFBD.AppendChild(dilationCRadiusElement);

            //isOpening
            XmlElement isOpeningElement = tXmlDoc.CreateElement("isOpening");
            isOpeningElement.SetAttribute("isOpening", UserCode.GetInstance().gProCd[idx].gBP.isOpening.ToString());
            rootBlobAnalysisFBD.AppendChild(isOpeningElement);

            //openingElementNUM
            XmlElement openingElementNUMElement = tXmlDoc.CreateElement("openingElementNUM");
            openingElementNUMElement.SetAttribute("openingElementNUM", UserCode.GetInstance().gProCd[idx].gBP.openingElementNUM.ToString());
            rootBlobAnalysisFBD.AppendChild(openingElementNUMElement);

            //openingRWidth
            XmlElement openingRWidthElement = tXmlDoc.CreateElement("openingRWidth");
            openingRWidthElement.SetAttribute("openingRWidth", UserCode.GetInstance().gProCd[idx].gBP.openingRWidth.ToString());
            rootBlobAnalysisFBD.AppendChild(openingRWidthElement);

            //openingRHeight
            XmlElement openingRHeightElement = tXmlDoc.CreateElement("openingRHeight");
            openingRHeightElement.SetAttribute("openingRHeight", UserCode.GetInstance().gProCd[idx].gBP.openingRHeight.ToString());
            rootBlobAnalysisFBD.AppendChild(openingRHeightElement);

            //openingCRadius
            XmlElement openingCRadiusElement = tXmlDoc.CreateElement("openingCRadius");
            openingCRadiusElement.SetAttribute("openingCRadius", UserCode.GetInstance().gProCd[idx].gBP.openingCRadius.ToString());
            rootBlobAnalysisFBD.AppendChild(openingCRadiusElement);

            //isClosing
            XmlElement isClosingElement = tXmlDoc.CreateElement("isClosing");
            isClosingElement.SetAttribute("isClosing", UserCode.GetInstance().gProCd[idx].gBP.isClosing.ToString());
            rootBlobAnalysisFBD.AppendChild(isClosingElement);

            //closingElementNUM
            XmlElement closingElementNUMElement = tXmlDoc.CreateElement("closingElementNUM");
            closingElementNUMElement.SetAttribute("closingElementNUM", UserCode.GetInstance().gProCd[idx].gBP.closingElementNUM.ToString());
            rootBlobAnalysisFBD.AppendChild(closingElementNUMElement);

            //closingRWidth
            XmlElement closingRWidthElement = tXmlDoc.CreateElement("closingRWidth");
            closingRWidthElement.SetAttribute("closingRWidth", UserCode.GetInstance().gProCd[idx].gBP.closingRWidth.ToString());
            rootBlobAnalysisFBD.AppendChild(closingRWidthElement);

            //closingRHeight
            XmlElement closingRHeightElement = tXmlDoc.CreateElement("closingRHeight");
            closingRHeightElement.SetAttribute("closingRHeight", UserCode.GetInstance().gProCd[idx].gBP.closingRHeight.ToString());
            rootBlobAnalysisFBD.AppendChild(closingRHeightElement);

            //closingCRadius
            XmlElement closingCRadiusElement = tXmlDoc.CreateElement("closingCRadius");
            closingCRadiusElement.SetAttribute("closingCRadius", UserCode.GetInstance().gProCd[idx].gBP.closingCRadius.ToString());
            rootBlobAnalysisFBD.AppendChild(closingCRadiusElement);
            #endregion

            #region SelectPara
            //SelectedPara
            XmlElement SelectedParaElement = tXmlDoc.CreateElement("SelectedPara");
            rootBlobAnalysisFBD.AppendChild(SelectedParaElement);

            XmlElement SelectedParaLengthElement = tXmlDoc.CreateElement("SelectedParaLength");
            SelectedParaLengthElement.SetAttribute("SelectedParaLength", "34");
            SelectedParaElement.AppendChild(SelectedParaLengthElement);

            XmlElement selectSTRElement = tXmlDoc.CreateElement("selectSTR");
            SelectedParaElement.AppendChild(selectSTRElement);

            XmlElement selectIsCheckedElement = tXmlDoc.CreateElement("selectIsChecked");
            SelectedParaElement.AppendChild(selectIsCheckedElement);

            XmlElement selectMinElement = tXmlDoc.CreateElement("selectMin");
            SelectedParaElement.AppendChild(selectMinElement);

            XmlElement selectMaxElement = tXmlDoc.CreateElement("selectMax");
            SelectedParaElement.AppendChild(selectMaxElement);

            for (int i = 0; i < 34; i++)
            {
                XmlElement selectSTROneElement = tXmlDoc.CreateElement("selectSTR" + i.ToString());
                selectSTROneElement.SetAttribute("selectSTR" + i.ToString(), UserCode.GetInstance().gProCd[idx].gBP.selectSTR[i].ToString());
                selectSTRElement.AppendChild(selectSTROneElement);

                XmlElement selectIsCheckedOneElement = tXmlDoc.CreateElement("selectIsChecked" + i.ToString());
                selectIsCheckedOneElement.SetAttribute("selectIsChecked" + i.ToString(), UserCode.GetInstance().gProCd[idx].gBP.selectIsChecked[i].ToString());
                selectIsCheckedElement.AppendChild(selectIsCheckedOneElement);

                XmlElement selectMinOneElement = tXmlDoc.CreateElement("selectMin" + i.ToString());
                selectMinOneElement.SetAttribute("selectMin" + i.ToString(), UserCode.GetInstance().gProCd[idx].gBP.selectMin[i].ToString());
                selectMinElement.AppendChild(selectMinOneElement);

                XmlElement selectMaxOneElement = tXmlDoc.CreateElement("selectMax" + i.ToString());
                selectMaxOneElement.SetAttribute("selectMax" + i.ToString(), UserCode.GetInstance().gProCd[idx].gBP.selectMax[i].ToString());
                selectMaxElement.AppendChild(selectMaxOneElement);

            }
            //selectisAnd
            XmlElement selectisAndElement = tXmlDoc.CreateElement("selectisAnd");
            selectisAndElement.SetAttribute("selectisAnd", UserCode.GetInstance().gProCd[idx].gBP.selectisAnd.ToString());
            rootBlobAnalysisFBD.AppendChild(selectisAndElement);

            //selectArrangeItemIndex
            XmlElement selectArrangeItemIndexElement = tXmlDoc.CreateElement("selectArrangeItemIndex");
            selectArrangeItemIndexElement.SetAttribute("selectArrangeItemIndex", UserCode.GetInstance().gProCd[idx].gBP.selectArrangeItemIndex.ToString());
            rootBlobAnalysisFBD.AppendChild(selectArrangeItemIndexElement);

            //isArrangeLtoS
            XmlElement isArrangeLtoSElement = tXmlDoc.CreateElement("isArrangeLtoS");
            isArrangeLtoSElement.SetAttribute("isArrangeLtoS", UserCode.GetInstance().gProCd[idx].gBP.isArrangeLtoS.ToString());
            rootBlobAnalysisFBD.AppendChild(isArrangeLtoSElement);

            //regionNum
            XmlElement regionNumElement = tXmlDoc.CreateElement("regionNum");
            regionNumElement.SetAttribute("regionNum", UserCode.GetInstance().gProCd[idx].gBP.regionNum.ToString());
            rootBlobAnalysisFBD.AppendChild(regionNumElement);

            //selectItemCount
            XmlElement selectItemCountElement = tXmlDoc.CreateElement("selectItemCount");
            selectItemCountElement.SetAttribute("selectItemCount", UserCode.GetInstance().gProCd[idx].gBP.selectItemCount.ToString());
            rootBlobAnalysisFBD.AppendChild(selectItemCountElement);

            #endregion

            #region OutputPara
            //OutputPara
            XmlElement OutputParaElement = tXmlDoc.CreateElement("OutputPara");
            rootBlobAnalysisFBD.AppendChild(OutputParaElement);

            XmlElement OutputParaLengthElement = tXmlDoc.CreateElement("OutputParaLength");
            OutputParaLengthElement.SetAttribute("OutputParaLength", "34");
            OutputParaElement.AppendChild(OutputParaLengthElement);

            XmlElement outputShowStrElement = tXmlDoc.CreateElement("outputShowStr");
            OutputParaElement.AppendChild(outputShowStrElement);

            XmlElement outputIDIsCheckedElement = tXmlDoc.CreateElement("outputIDIsChecked");
            OutputParaElement.AppendChild(outputIDIsCheckedElement);

            for (int i = 0; i < 34; i++)
            {
                XmlElement outputShowStrOneElement = tXmlDoc.CreateElement("outputShowStr" + i.ToString());
                outputShowStrOneElement.SetAttribute("outputShowStr" + i.ToString(), UserCode.GetInstance().gProCd[idx].gBP.outputShowStr[i]);
                outputShowStrElement.AppendChild(outputShowStrOneElement);

                XmlElement outputIDIsCheckedOneElement = tXmlDoc.CreateElement("outputIDIsChecked" + i.ToString());
                outputIDIsCheckedOneElement.SetAttribute("outputIDIsChecked" + i.ToString(), UserCode.GetInstance().gProCd[idx].gBP.outputIDIsChecked[i].ToString());
                outputIDIsCheckedElement.AppendChild(outputIDIsCheckedOneElement);
            }
            #endregion

            //showOutputResultStr 
            XmlElement showOutputResultStrRootElement = tXmlDoc.CreateElement("ShowOutputResultStr");
            rootBlobAnalysisFBD.AppendChild(showOutputResultStrRootElement);
            XmlElement showOutputResultStrLengthElement = tXmlDoc.CreateElement("ShowOutputResultStrLength");
            showOutputResultStrLengthElement.SetAttribute("ShowOutputResultStrLength", UserCode.GetInstance().gProCd[idx].gBP.showOutputResultStr.Length.ToString());
            showOutputResultStrRootElement.AppendChild(showOutputResultStrLengthElement);
            for (int i = 0; i < UserCode.GetInstance().gProCd[idx].gBP.showOutputResultStr.Length; i++)
            {
                XmlElement showOutputResultStrOneElement = tXmlDoc.CreateElement("ShowOutputResultStr" + i.ToString());
                showOutputResultStrOneElement.SetAttribute("ShowOutputResultStr" + i.ToString(), UserCode.GetInstance().gProCd[idx].gBP.showOutputResultStr[i]);
                showOutputResultStrRootElement.AppendChild(showOutputResultStrOneElement);
            }

            //showOutputResultFlag  
            XmlElement showOutputResultFlagRootElement = tXmlDoc.CreateElement("ShowOutputResultFlag");
            rootBlobAnalysisFBD.AppendChild(showOutputResultFlagRootElement);
            XmlElement showOutputResultFlagLengthElement = tXmlDoc.CreateElement("ShowOutputResultFlagLength");
            showOutputResultFlagLengthElement.SetAttribute("ShowOutputResultFlagLength", UserCode.GetInstance().gProCd[idx].gBP.showOutputResultFlag.Length.ToString());
            showOutputResultFlagRootElement.AppendChild(showOutputResultFlagLengthElement);
            for (int i = 0; i < UserCode.GetInstance().gProCd[idx].gBP.showOutputResultFlag.Length; i++)
            {
                XmlElement showOutputResultFlagOneElement = tXmlDoc.CreateElement("ShowOutputResultFlag" + i.ToString());
                showOutputResultFlagOneElement.SetAttribute("ShowOutputResultFlag" + i.ToString(), UserCode.GetInstance().gProCd[idx].gBP.showOutputResultFlag[i].ToString());
                showOutputResultFlagRootElement.AppendChild(showOutputResultFlagOneElement);
            }

            //blobAnalysisOperationStr 
            XmlElement blobAnalysisOperationStrRootElement = tXmlDoc.CreateElement("blobAnalysisOperationStr");
            rootBlobAnalysisFBD.AppendChild(blobAnalysisOperationStrRootElement);
            XmlElement blobAnalysisOperationStrLengthElement = tXmlDoc.CreateElement("blobAnalysisOperationStrLength");
            blobAnalysisOperationStrLengthElement.SetAttribute("blobAnalysisOperationStrLength", UserCode.GetInstance().gProCd[idx].gBP.blobAnalysisOperationStr.Length.ToString());
            blobAnalysisOperationStrRootElement.AppendChild(blobAnalysisOperationStrLengthElement);
            for (int i = 0; i < UserCode.GetInstance().gProCd[idx].gBP.blobAnalysisOperationStr.Length; i++)
            {
                XmlElement blobAnalysisOperationStrOneElement = tXmlDoc.CreateElement("blobAnalysisOperationStr" + i.ToString());
                blobAnalysisOperationStrOneElement.SetAttribute("blobAnalysisOperationStr" + i.ToString(), UserCode.GetInstance().gProCd[idx].gBP.blobAnalysisOperationStr[i]);
                blobAnalysisOperationStrRootElement.AppendChild(blobAnalysisOperationStrOneElement);
            }

            //blobAnalysisOperationFlag  
            XmlElement blobAnalysisOperationFlagRootElement = tXmlDoc.CreateElement("blobAnalysisOperationFlag");
            rootBlobAnalysisFBD.AppendChild(blobAnalysisOperationFlagRootElement);
            XmlElement blobAnalysisOperationFlagLengthElement = tXmlDoc.CreateElement("blobAnalysisOperationFlagLength");
            blobAnalysisOperationFlagLengthElement.SetAttribute("blobAnalysisOperationFlagLength", UserCode.GetInstance().gProCd[idx].gBP.blobAnalysisOperationFlag.Length.ToString());
            blobAnalysisOperationFlagRootElement.AppendChild(blobAnalysisOperationFlagLengthElement);
            for (int i = 0; i < UserCode.GetInstance().gProCd[idx].gBP.blobAnalysisOperationFlag.Length; i++)
            {
                XmlElement blobAnalysisOperationFlagOneElement = tXmlDoc.CreateElement("blobAnalysisOperationFlag" + i.ToString());
                blobAnalysisOperationFlagOneElement.SetAttribute("blobAnalysisOperationFlag" + i.ToString(), UserCode.GetInstance().gProCd[idx].gBP.blobAnalysisOperationFlag[i].ToString());
                blobAnalysisOperationFlagRootElement.AppendChild(blobAnalysisOperationFlagOneElement);
            }
            //outPutStringList ChildNode
            XmlElement outPutStringListRootElement = tXmlDoc.CreateElement("OutPutStringList");
            rootBlobAnalysisFBD.AppendChild(outPutStringListRootElement);
            XmlElement outPutStringListLengthElement = tXmlDoc.CreateElement("OutPutStringListLength");
            outPutStringListLengthElement.SetAttribute("OutPutStringListLength", UserCode.GetInstance().gProCd[idx].gBP.outPutStringList.Count.ToString());
            outPutStringListRootElement.AppendChild(outPutStringListLengthElement);
            for (int i = 0; i < UserCode.GetInstance().gProCd[idx].gBP.outPutStringList.Count; i++)
            {
                XmlElement outPutStringListOneElement = tXmlDoc.CreateElement("OutPutStringList" + i.ToString());
                outPutStringListOneElement.SetAttribute("OutPutStringList" + i.ToString(), UserCode.GetInstance().gProCd[idx].gBP.outPutStringList[i]);
                outPutStringListRootElement.AppendChild(outPutStringListOneElement);
            }
        }
        private void SaveShapeSearchFBDXML(int idx, out XmlElement rootShapeSearchFBD)
        {
            rootShapeSearchFBD = tXmlDoc.CreateElement(UserCode.GetInstance().codeInfoValToKey[UserCode.GetInstance().gProCd[idx].FuncID] + "_" + idx.ToString());
            //Shape Model And Image ChildNode
            XmlElement ShapeModelAndImageRootElement = tXmlDoc.CreateElement("ShapeModelAndImage");
            rootShapeSearchFBD.AppendChild(ShapeModelAndImageRootElement);

            if (UserCode.GetInstance().gProCd[idx].gSSP.shapeModel!=null)
            {
                XmlElement ShapeModelAndImageLengthElement = tXmlDoc.CreateElement("ShapeModelAndImageLength");
                ShapeModelAndImageLengthElement.SetAttribute("ShapeModelAndImageLength", UserCode.GetInstance().gProCd[idx].gSSP.shapeModel.TupleLength().ToString());
                ShapeModelAndImageRootElement.AppendChild(ShapeModelAndImageLengthElement);


                string fullFileName;
                string partFileName;
                for (int i = 0; i < UserCode.GetInstance().gProCd[idx].gSSP.shapeModel.TupleLength(); i++)
                {
                    partFileName = mFilenameOnly + "Model " + System.String.Format("{0:D2}", idx) + " " + System.String.Format("{0:D3}", i) + ".shm";
                    fullFileName = mFilename + "//" + partFileName;
                    HOperatorSet.WriteShapeModel(UserCode.GetInstance().gProCd[idx].gSSP.shapeModel[i], (HTuple)fullFileName);

                    XmlElement ShapeModelOneElement = tXmlDoc.CreateElement("ShapeModel" + i.ToString());
                    ShapeModelOneElement.SetAttribute("ShapeModel" + i.ToString(), partFileName);
                    ShapeModelAndImageRootElement.AppendChild(ShapeModelOneElement);

                    partFileName = mFilenameOnly + "Pic " + System.String.Format("{0:D2}", idx) + " " + System.String.Format("{0:D3}", i) + ".bmp";
                    fullFileName = mFilename + "//" + partFileName;
                    HOperatorSet.WriteImage(UserCode.GetInstance().gProCd[idx].gSSP.shapeModelImage[i + 1], "bmp", 0, fullFileName);

                    XmlElement ShapeModelImageOneElement = tXmlDoc.CreateElement("ShapeModelImage" + i.ToString());
                    ShapeModelImageOneElement.SetAttribute("ShapeModelImage" + i.ToString(), partFileName);
                    ShapeModelAndImageRootElement.AppendChild(ShapeModelImageOneElement);
                }
                //Shape Model Region ChildNode
                partFileName = mFilenameOnly + "Region " + System.String.Format("{0:D2}", idx) + ".reg";
                fullFileName = mFilename + "//" + partFileName;
                HOperatorSet.WriteRegion(UserCode.GetInstance().gProCd[idx].gSSP.shapeModelRegion, fullFileName);
                XmlElement ShapeModelRegionRootElement = tXmlDoc.CreateElement("ShapeModelRegion");
                ShapeModelRegionRootElement.SetAttribute("ShapeModelRegion", partFileName);
                rootShapeSearchFBD.AppendChild(ShapeModelRegionRootElement);

                //Shape Model Point ChildNode

                partFileName = mFilenameOnly + "Points " + System.String.Format("{0:D2}", idx) + ".dat";
                fullFileName = mFilename + "//" + partFileName;
                HTuple hv_FileHandle;
                HOperatorSet.OpenFile(fullFileName, "output", out hv_FileHandle);

                for (int i = 0; i < UserCode.GetInstance().gProCd[idx].gSSP.shapeModel.TupleLength(); i++)
                {
                    HOperatorSet.FwriteString(hv_FileHandle, UserCode.GetInstance().gProCd[idx].gSSP.shapeModelPoints[i].X + "\n");
                    HOperatorSet.FwriteString(hv_FileHandle, UserCode.GetInstance().gProCd[idx].gSSP.shapeModelPoints[i].Y + "\n");
                }
                HOperatorSet.CloseFile(hv_FileHandle);
                XmlElement ShapeModelPointRootElement = tXmlDoc.CreateElement("ShapeModelPoint");
                ShapeModelPointRootElement.SetAttribute("ShapeModelPoint", partFileName);
                rootShapeSearchFBD.AppendChild(ShapeModelPointRootElement);
            }


            //arrangeIndex ChildNode
            XmlElement arrangeIndexRootElement = tXmlDoc.CreateElement("arrangeIndex");
            arrangeIndexRootElement.SetAttribute("arrangeIndex", UserCode.GetInstance().gProCd[idx].gSSP.arrangeIndex.ToString());
            rootShapeSearchFBD.AppendChild(arrangeIndexRootElement);


            //Max_Object_Num ChildNode
            XmlElement Max_Object_NumRootElement = tXmlDoc.CreateElement("Max_Object_Num");
            Max_Object_NumRootElement.SetAttribute("Max_Object_Num", UserCode.GetInstance().gProCd[idx].gSSP.Max_Object_Num.ToString());
            rootShapeSearchFBD.AppendChild(Max_Object_NumRootElement);

            //IsBorderShapeModelChecked ChildNode
            XmlElement IsBorderShapeModelCheckedRootElement = tXmlDoc.CreateElement("IsBorderShapeModelChecked");
            IsBorderShapeModelCheckedRootElement.SetAttribute("IsBorderShapeModelChecked", UserCode.GetInstance().gProCd[idx].gSSP.isBorderShapeModelChecked.ToString());
            rootShapeSearchFBD.AppendChild(IsBorderShapeModelCheckedRootElement);

            //IsMultiplePara ChildNode
            XmlElement IsMultipleParaRootElement = tXmlDoc.CreateElement("IsMultiplePara");
            IsMultipleParaRootElement.SetAttribute("IsMultiplePara", UserCode.GetInstance().gProCd[idx].gSSP.isMultiplePara.ToString());
            rootShapeSearchFBD.AppendChild(IsMultipleParaRootElement);

            //outPutStringList ChildNode
            XmlElement outPutStringListRootElement = tXmlDoc.CreateElement("OutPutStringList");
            rootShapeSearchFBD.AppendChild(outPutStringListRootElement);
            XmlElement outPutStringListLengthElement = tXmlDoc.CreateElement("OutPutStringListLength");
            outPutStringListLengthElement.SetAttribute("OutPutStringListLength", UserCode.GetInstance().gProCd[idx].gSSP.outPutStringList.Count.ToString());
            outPutStringListRootElement.AppendChild(outPutStringListLengthElement);
            for (int i = 0; i < UserCode.GetInstance().gProCd[idx].gSSP.outPutStringList.Count; i++)
            {
                XmlElement outPutStringListOneElement = tXmlDoc.CreateElement("OutPutStringList" + i.ToString());
                outPutStringListOneElement.SetAttribute("OutPutStringList" + i.ToString(), UserCode.GetInstance().gProCd[idx].gSSP.outPutStringList[i]);
                outPutStringListRootElement.AppendChild(outPutStringListOneElement);
            }

            //showOutputResultStr ChildNode
            XmlElement showOutputResultStrRootElement = tXmlDoc.CreateElement("ShowOutputResultStr");
            rootShapeSearchFBD.AppendChild(showOutputResultStrRootElement);
            XmlElement showOutputResultStrLengthElement = tXmlDoc.CreateElement("ShowOutputResultStrLength");
            showOutputResultStrLengthElement.SetAttribute("ShowOutputResultStrLength", UserCode.GetInstance().gProCd[idx].gSSP.showOutputResultStr.Length.ToString());
            showOutputResultStrRootElement.AppendChild(showOutputResultStrLengthElement);
            for (int i = 0; i < UserCode.GetInstance().gProCd[idx].gSSP.showOutputResultStr.Length; i++)
            {
                XmlElement showOutputResultStrOneElement = tXmlDoc.CreateElement("ShowOutputResultStr" + i.ToString());
                showOutputResultStrOneElement.SetAttribute("ShowOutputResultStr" + i.ToString(), UserCode.GetInstance().gProCd[idx].gSSP.showOutputResultStr[i]);
                showOutputResultStrRootElement.AppendChild(showOutputResultStrOneElement);
            }

            //showOutputResultFlag ChildNode
            XmlElement showOutputResultFlagRootElement = tXmlDoc.CreateElement("ShowOutputResultFlag");
            rootShapeSearchFBD.AppendChild(showOutputResultFlagRootElement);
            XmlElement showOutputResultFlagLengthElement = tXmlDoc.CreateElement("ShowOutputResultFlagLength");
            showOutputResultFlagLengthElement.SetAttribute("ShowOutputResultFlagLength", UserCode.GetInstance().gProCd[idx].gSSP.showOutputResultFlag.Length.ToString());
            showOutputResultFlagRootElement.AppendChild(showOutputResultFlagLengthElement);
            for (int i = 0; i < UserCode.GetInstance().gProCd[idx].gSSP.showOutputResultFlag.Length; i++)
            {
                XmlElement showOutputResultFlagOneElement = tXmlDoc.CreateElement("ShowOutputResultFlag" + i.ToString());
                showOutputResultFlagOneElement.SetAttribute("ShowOutputResultFlag" + i.ToString(), UserCode.GetInstance().gProCd[idx].gSSP.showOutputResultFlag[i].ToString());
                showOutputResultFlagRootElement.AppendChild(showOutputResultFlagOneElement);
            }
            //findShapeModelBatchOperationStr ChildNode
            XmlElement findShapeModelBatchOperationStrRootElement = tXmlDoc.CreateElement("findShapeModelBatchOperationStr");
            rootShapeSearchFBD.AppendChild(findShapeModelBatchOperationStrRootElement);
            XmlElement findShapeModelBatchOperationStrLengthElement = tXmlDoc.CreateElement("findShapeModelBatchOperationStrLength");
            findShapeModelBatchOperationStrLengthElement.SetAttribute("findShapeModelBatchOperationStrLength", UserCode.GetInstance().gProCd[idx].gSSP.findShapeModelBatchOperationStr.Length.ToString());
            findShapeModelBatchOperationStrRootElement.AppendChild(findShapeModelBatchOperationStrLengthElement);
            for (int i = 0; i < UserCode.GetInstance().gProCd[idx].gSSP.findShapeModelBatchOperationStr.Length; i++)
            {
                XmlElement findShapeModelBatchOperationStrOneElement = tXmlDoc.CreateElement("findShapeModelBatchOperationStr" + i.ToString());
                findShapeModelBatchOperationStrOneElement.SetAttribute("findShapeModelBatchOperationStr" + i.ToString(), UserCode.GetInstance().gProCd[idx].gSSP.findShapeModelBatchOperationStr[i]);
                findShapeModelBatchOperationStrRootElement.AppendChild(findShapeModelBatchOperationStrOneElement);
            }

            //findShapeModelBatchOperationFlag ChildNode
            XmlElement findShapeModelBatchOperationFlagRootElement = tXmlDoc.CreateElement("findShapeModelBatchOperationFlag");
            rootShapeSearchFBD.AppendChild(findShapeModelBatchOperationFlagRootElement);
            XmlElement findShapeModelBatchOperationFlagLengthElement = tXmlDoc.CreateElement("findShapeModelBatchOperationFlagLength");
            findShapeModelBatchOperationFlagLengthElement.SetAttribute("findShapeModelBatchOperationFlagLength", UserCode.GetInstance().gProCd[idx].gSSP.findShapeModelBatchOperationFlag.Length.ToString());
            findShapeModelBatchOperationFlagRootElement.AppendChild(findShapeModelBatchOperationFlagLengthElement);
            for (int i = 0; i < UserCode.GetInstance().gProCd[idx].gSSP.findShapeModelBatchOperationFlag.Length; i++)
            {
                XmlElement findShapeModelBatchOperationFlagOneElement = tXmlDoc.CreateElement("findShapeModelBatchOperationFlag" + i.ToString());
                findShapeModelBatchOperationFlagOneElement.SetAttribute("findShapeModelBatchOperationFlag" + i.ToString(), UserCode.GetInstance().gProCd[idx].gSSP.findShapeModelBatchOperationFlag[i].ToString());
                findShapeModelBatchOperationFlagRootElement.AppendChild(findShapeModelBatchOperationFlagOneElement);
            }
            //modelIsChecked ChildNode
            XmlElement modelIsCheckedRootElement = tXmlDoc.CreateElement("ModelIsChecked");
            rootShapeSearchFBD.AppendChild(modelIsCheckedRootElement);
            XmlElement modelIsCheckedLengthElement = tXmlDoc.CreateElement("ModelIsCheckedLength");
            modelIsCheckedLengthElement.SetAttribute("ModelIsCheckedLength", UserCode.GetInstance().gProCd[idx].gSSP.modelIsChecked.Length.ToString());
            modelIsCheckedRootElement.AppendChild(modelIsCheckedLengthElement);
            for (int i = 0; i < UserCode.GetInstance().gProCd[idx].gSSP.modelIsChecked.Length; i++)
            {
                XmlElement modelIsCheckedOneElement = tXmlDoc.CreateElement("ModelIsChecked" + i.ToString());
                modelIsCheckedOneElement.SetAttribute("ModelIsChecked" + i.ToString(), UserCode.GetInstance().gProCd[idx].gSSP.modelIsChecked[i].ToString());
                modelIsCheckedRootElement.AppendChild(modelIsCheckedOneElement);
            }

            //AllShapeModelMultiplePara ChildNode
            XmlElement AllShapeModelMultipleParaRootElement = tXmlDoc.CreateElement("AllShapeModelMultiplePara");
            rootShapeSearchFBD.AppendChild(AllShapeModelMultipleParaRootElement);
            XmlElement allShapeModelMultipleParaLengthElement = tXmlDoc.CreateElement("AllShapeModelMultipleParaLength");
            allShapeModelMultipleParaLengthElement.SetAttribute("AllShapeModelMultipleParaLength",UserCode.GetInstance().gProCd[idx].gSSP.angleStart.Length.ToString());
            AllShapeModelMultipleParaRootElement.AppendChild(allShapeModelMultipleParaLengthElement);

            XmlElement angleStartRootElement = tXmlDoc.CreateElement("AngleStart");
            AllShapeModelMultipleParaRootElement.AppendChild(angleStartRootElement);

            XmlElement angleExtentRootElement = tXmlDoc.CreateElement("AngleExtent");
            AllShapeModelMultipleParaRootElement.AppendChild(angleExtentRootElement);

            XmlElement minScoreRootElement = tXmlDoc.CreateElement("MinScore");
            AllShapeModelMultipleParaRootElement.AppendChild(minScoreRootElement);

            XmlElement numMatchesRootElement = tXmlDoc.CreateElement("NumMatches");
            AllShapeModelMultipleParaRootElement.AppendChild(numMatchesRootElement);

            XmlElement maxOverlapRootElement = tXmlDoc.CreateElement("MaxOverlap");
            maxOverlapRootElement.SetAttribute("MaxOverlap", UserCode.GetInstance().gProCd[idx].gSSP.maxOverlap.ToString());
            AllShapeModelMultipleParaRootElement.AppendChild(maxOverlapRootElement);

            XmlElement subPixelRootElement = tXmlDoc.CreateElement("SubPixel");
            AllShapeModelMultipleParaRootElement.AppendChild(subPixelRootElement);

            XmlElement numLevelsRootElement = tXmlDoc.CreateElement("NumLevels");
            AllShapeModelMultipleParaRootElement.AppendChild(numLevelsRootElement);

            XmlElement greedinessRootElement = tXmlDoc.CreateElement("Greediness");
            AllShapeModelMultipleParaRootElement.AppendChild(greedinessRootElement);


            for (int i = 0; i < UserCode.GetInstance().gProCd[idx].gSSP.angleStart.Length; i++)
            {
                XmlElement angleStartOneElement = tXmlDoc.CreateElement("AngleStart" + i.ToString());
                angleStartOneElement.SetAttribute("AngleStart" + i.ToString(), UserCode.GetInstance().gProCd[idx].gSSP.angleStart[i].ToString());
                angleStartRootElement.AppendChild(angleStartOneElement);

                XmlElement angleExtentOneElement = tXmlDoc.CreateElement("AngleExtent" + i.ToString());
                angleExtentOneElement.SetAttribute("AngleExtent" + i.ToString(), UserCode.GetInstance().gProCd[idx].gSSP.angleExtent[i].ToString());
                angleExtentRootElement.AppendChild(angleExtentOneElement);

                XmlElement minScoreOneElement = tXmlDoc.CreateElement("MinScore" + i.ToString());
                minScoreOneElement.SetAttribute("MinScore" + i.ToString(), UserCode.GetInstance().gProCd[idx].gSSP.minScore[i].ToString());
                minScoreRootElement.AppendChild(minScoreOneElement);

                XmlElement numMatchesOneElement = tXmlDoc.CreateElement("NumMatches" + i.ToString());
                numMatchesOneElement.SetAttribute("NumMatches" + i.ToString(), UserCode.GetInstance().gProCd[idx].gSSP.numMatches[i].ToString());
                numMatchesRootElement.AppendChild(numMatchesOneElement);

                XmlElement subPixelOneElement = tXmlDoc.CreateElement("SubPixel" + (i * 2).ToString());
                subPixelOneElement.SetAttribute("SubPixel" + (i * 2).ToString(), UserCode.GetInstance().gProCd[idx].gSSP.subPixel[2 * i].ToString());
                subPixelRootElement.AppendChild(subPixelOneElement);
                XmlElement subPixelTwoElement = tXmlDoc.CreateElement("SubPixel" + (i * 2 + 1).ToString());
                subPixelTwoElement.SetAttribute("SubPixel" + (i * 2 + 1).ToString(), UserCode.GetInstance().gProCd[idx].gSSP.subPixel[2 * i + 1].ToString());
                subPixelRootElement.AppendChild(subPixelTwoElement);

                XmlElement numLevelsOneElement = tXmlDoc.CreateElement("NumLevels" + (i * 2).ToString());
                numLevelsOneElement.SetAttribute("NumLevels" + (i * 2).ToString(), UserCode.GetInstance().gProCd[idx].gSSP.numLevels[2 * i].ToString());
                numLevelsRootElement.AppendChild(numLevelsOneElement);
                XmlElement numLevelsTwoElement = tXmlDoc.CreateElement("NumLevels" + (i * 2 + 1).ToString());
                numLevelsTwoElement.SetAttribute("NumLevels" + (i * 2 + 1).ToString(), UserCode.GetInstance().gProCd[idx].gSSP.numLevels[2 * i + 1].ToString());
                numLevelsRootElement.AppendChild(numLevelsTwoElement);

                XmlElement greedinessOneElement = tXmlDoc.CreateElement("Greediness" + i.ToString());
                greedinessOneElement.SetAttribute("Greediness" + i.ToString(), UserCode.GetInstance().gProCd[idx].gSSP.greediness[i].ToString());
                greedinessRootElement.AppendChild(greedinessOneElement);
            }


        }
        private void SaveAnisoShapeSearchFBDXML(int idx, out XmlElement rootAnisoShapeSearchFBD)
        {
            rootAnisoShapeSearchFBD = tXmlDoc.CreateElement(UserCode.GetInstance().codeInfoValToKey[UserCode.GetInstance().gProCd[idx].FuncID] + "_" + idx.ToString());
            //Shape Model And Image ChildNode
            XmlElement AnisoShapeModelAndImageRootElement = tXmlDoc.CreateElement("AnisoShapeModelAndImage");
            rootAnisoShapeSearchFBD.AppendChild(AnisoShapeModelAndImageRootElement);

            if (UserCode.GetInstance().gProCd[idx].gASSP.shapeModel!=null)
            {
                XmlElement AnisoShapeModelAndImageLengthElement = tXmlDoc.CreateElement("AnisoShapeModelAndImageLength");
                AnisoShapeModelAndImageLengthElement.SetAttribute("AnisoShapeModelAndImageLength", UserCode.GetInstance().gProCd[idx].gASSP.shapeModel.TupleLength().ToString());
                AnisoShapeModelAndImageRootElement.AppendChild(AnisoShapeModelAndImageLengthElement);

                string partFileName;
                string fullFileName;

                for (int i = 0; i < UserCode.GetInstance().gProCd[idx].gASSP.shapeModel.TupleLength(); i++)
                {
                    partFileName = mFilenameOnly + "AnisoModel " + System.String.Format("{0:D2}", idx) + " " + System.String.Format("{0:D3}", i) + ".shm";
                    fullFileName = mFilename + "//" + partFileName;

                    HOperatorSet.WriteShapeModel(UserCode.GetInstance().gProCd[idx].gASSP.shapeModel[i], (HTuple)fullFileName);

                    XmlElement AnisoShapeModelOneElement = tXmlDoc.CreateElement("AnisoShapeModel" + i.ToString());
                    AnisoShapeModelOneElement.SetAttribute("AnisoShapeModel" + i.ToString(), partFileName);
                    AnisoShapeModelAndImageRootElement.AppendChild(AnisoShapeModelOneElement);


                    partFileName = mFilenameOnly + "AnisoPic " + System.String.Format("{0:D2}", idx) + " " + System.String.Format("{0:D3}", i) + ".bmp";
                    fullFileName = mFilename + "//" + partFileName;
                    HOperatorSet.WriteImage(UserCode.GetInstance().gProCd[idx].gASSP.shapeModelImage[i + 1], "bmp", 0, fullFileName);

                    XmlElement AnisoShapeModelImageOneElement = tXmlDoc.CreateElement("AnisoShapeModelImage" + i.ToString());
                    AnisoShapeModelImageOneElement.SetAttribute("AnisoShapeModelImage" + i.ToString(), partFileName);
                    AnisoShapeModelAndImageRootElement.AppendChild(AnisoShapeModelImageOneElement);
                }
                //Shape Model Region ChildNode
                partFileName = mFilenameOnly + "AnisoRegion " + System.String.Format("{0:D2}", idx) + ".reg";
                fullFileName = mFilename + "//" + partFileName;

                HOperatorSet.WriteRegion(UserCode.GetInstance().gProCd[idx].gASSP.shapeModelRegion, fullFileName);
                XmlElement AnisoShapeModelRegionRootElement = tXmlDoc.CreateElement("AnisoShapeModelRegion");
                AnisoShapeModelRegionRootElement.SetAttribute("AnisoShapeModelRegion", partFileName);
                rootAnisoShapeSearchFBD.AppendChild(AnisoShapeModelRegionRootElement);

                //Shape Model Point ChildNode

                partFileName = mFilenameOnly + "AnisoPoints " + System.String.Format("{0:D2}", idx) + ".dat";
                fullFileName = mFilename + "//" + partFileName;
                HTuple hv_FileHandle;
                HOperatorSet.OpenFile(fullFileName, "output", out hv_FileHandle);

                for (int i = 0; i < UserCode.GetInstance().gProCd[idx].gASSP.shapeModel.TupleLength(); i++)
                {
                    HOperatorSet.FwriteString(hv_FileHandle, UserCode.GetInstance().gProCd[idx].gASSP.shapeModelPoints[i].X + "\n");
                    HOperatorSet.FwriteString(hv_FileHandle, UserCode.GetInstance().gProCd[idx].gASSP.shapeModelPoints[i].Y + "\n");
                }
                HOperatorSet.CloseFile(hv_FileHandle);
                XmlElement AnisoShapeModelPointRootElement = tXmlDoc.CreateElement("AnisoShapeModelPoint");
                AnisoShapeModelPointRootElement.SetAttribute("AnisoShapeModelPoint", partFileName);
                rootAnisoShapeSearchFBD.AppendChild(AnisoShapeModelPointRootElement);
            }
            

            //arrangeIndex ChildNode
            XmlElement arrangeIndexRootElement = tXmlDoc.CreateElement("arrangeIndex");
            arrangeIndexRootElement.SetAttribute("arrangeIndex", UserCode.GetInstance().gProCd[idx].gASSP.arrangeIndex.ToString());
            rootAnisoShapeSearchFBD.AppendChild(arrangeIndexRootElement);

            //Max_Object_Num ChildNode
            XmlElement Max_Object_NumRootElement = tXmlDoc.CreateElement("Max_Object_Num");
            Max_Object_NumRootElement.SetAttribute("Max_Object_Num", UserCode.GetInstance().gProCd[idx].gASSP.Max_Object_Num.ToString());
            rootAnisoShapeSearchFBD.AppendChild(Max_Object_NumRootElement);

            //IsBorderShapeModelChecked ChildNode
            XmlElement IsBorderShapeModelCheckedRootElement = tXmlDoc.CreateElement("IsBorderShapeModelChecked");
            IsBorderShapeModelCheckedRootElement.SetAttribute("IsBorderShapeModelChecked", UserCode.GetInstance().gProCd[idx].gASSP.isBorderShapeModelChecked.ToString());
            rootAnisoShapeSearchFBD.AppendChild(IsBorderShapeModelCheckedRootElement);

            //IsMultiplePara ChildNode
            XmlElement IsMultipleParaRootElement = tXmlDoc.CreateElement("IsMultiplePara");
            IsMultipleParaRootElement.SetAttribute("IsMultiplePara", UserCode.GetInstance().gProCd[idx].gASSP.isMultiplePara.ToString());
            rootAnisoShapeSearchFBD.AppendChild(IsMultipleParaRootElement);

            //outPutStringList ChildNode
            XmlElement outPutStringListRootElement = tXmlDoc.CreateElement("OutPutStringList");
            rootAnisoShapeSearchFBD.AppendChild(outPutStringListRootElement);
            XmlElement outPutStringListLengthElement = tXmlDoc.CreateElement("OutPutStringListLength");
            outPutStringListLengthElement.SetAttribute("OutPutStringListLength", UserCode.GetInstance().gProCd[idx].gASSP.outPutStringList.Count.ToString());
            outPutStringListRootElement.AppendChild(outPutStringListLengthElement);
            for (int i = 0; i < UserCode.GetInstance().gProCd[idx].gASSP.outPutStringList.Count; i++)
            {
                XmlElement outPutStringListOneElement = tXmlDoc.CreateElement("OutPutStringList" + i.ToString());
                outPutStringListOneElement.SetAttribute("OutPutStringList" + i.ToString(), UserCode.GetInstance().gProCd[idx].gASSP.outPutStringList[i]);
                outPutStringListRootElement.AppendChild(outPutStringListOneElement);
            }

            //showOutputResultStr ChildNode
            XmlElement showOutputResultStrRootElement = tXmlDoc.CreateElement("ShowOutputResultStr");
            rootAnisoShapeSearchFBD.AppendChild(showOutputResultStrRootElement);
            XmlElement showOutputResultStrLengthElement = tXmlDoc.CreateElement("ShowOutputResultStrLength");
            showOutputResultStrLengthElement.SetAttribute("ShowOutputResultStrLength", UserCode.GetInstance().gProCd[idx].gASSP.showOutputResultStr.Length.ToString());
            showOutputResultStrRootElement.AppendChild(showOutputResultStrLengthElement);
            for (int i = 0; i < UserCode.GetInstance().gProCd[idx].gASSP.showOutputResultStr.Length; i++)
            {
                XmlElement showOutputResultStrOneElement = tXmlDoc.CreateElement("ShowOutputResultStr" + i.ToString());
                showOutputResultStrOneElement.SetAttribute("ShowOutputResultStr" + i.ToString(), UserCode.GetInstance().gProCd[idx].gASSP.showOutputResultStr[i]);
                showOutputResultStrRootElement.AppendChild(showOutputResultStrOneElement);
            }

            //showOutputResultFlag ChildNode
            XmlElement showOutputResultFlagRootElement = tXmlDoc.CreateElement("ShowOutputResultFlag");
            rootAnisoShapeSearchFBD.AppendChild(showOutputResultFlagRootElement);
            XmlElement showOutputResultFlagLengthElement = tXmlDoc.CreateElement("ShowOutputResultFlagLength");
            showOutputResultFlagLengthElement.SetAttribute("ShowOutputResultFlagLength", UserCode.GetInstance().gProCd[idx].gASSP.showOutputResultFlag.Length.ToString());
            showOutputResultFlagRootElement.AppendChild(showOutputResultFlagLengthElement);
            for (int i = 0; i < UserCode.GetInstance().gProCd[idx].gASSP.showOutputResultFlag.Length; i++)
            {
                XmlElement showOutputResultFlagOneElement = tXmlDoc.CreateElement("ShowOutputResultFlag" + i.ToString());
                showOutputResultFlagOneElement.SetAttribute("ShowOutputResultFlag" + i.ToString(), UserCode.GetInstance().gProCd[idx].gASSP.showOutputResultFlag[i].ToString());
                showOutputResultFlagRootElement.AppendChild(showOutputResultFlagOneElement);
            }
            //findAnisoShapeModelBatchOperationStr ChildNode
            XmlElement findAnisoShapeModelBatchOperationStrRootElement = tXmlDoc.CreateElement("findAnisoShapeModelBatchOperationStr");
            rootAnisoShapeSearchFBD.AppendChild(findAnisoShapeModelBatchOperationStrRootElement);
            XmlElement findAnisoShapeModelBatchOperationStrLengthElement = tXmlDoc.CreateElement("findAnisoShapeModelBatchOperationStrLength");
            findAnisoShapeModelBatchOperationStrLengthElement.SetAttribute("findAnisoShapeModelBatchOperationStrLength", UserCode.GetInstance().gProCd[idx].gASSP.findAnisoShapeModelBatchOperationStr.Length.ToString());
            findAnisoShapeModelBatchOperationStrRootElement.AppendChild(findAnisoShapeModelBatchOperationStrLengthElement);
            for (int i = 0; i < UserCode.GetInstance().gProCd[idx].gASSP.findAnisoShapeModelBatchOperationStr.Length; i++)
            {
                XmlElement findAnisoShapeModelBatchOperationStrOneElement = tXmlDoc.CreateElement("findAnisoShapeModelBatchOperationStr" + i.ToString());
                findAnisoShapeModelBatchOperationStrOneElement.SetAttribute("findAnisoShapeModelBatchOperationStr" + i.ToString(), UserCode.GetInstance().gProCd[idx].gASSP.findAnisoShapeModelBatchOperationStr[i]);
                findAnisoShapeModelBatchOperationStrRootElement.AppendChild(findAnisoShapeModelBatchOperationStrOneElement);
            }

            //findAnisoShapeModelBatchOperationFlag ChildNode
            XmlElement findAnisoShapeModelBatchOperationFlagRootElement = tXmlDoc.CreateElement("findAnisoShapeModelBatchOperationFlag");
            rootAnisoShapeSearchFBD.AppendChild(findAnisoShapeModelBatchOperationFlagRootElement);
            XmlElement findAnisoShapeModelBatchOperationFlagLengthElement = tXmlDoc.CreateElement("findAnisoShapeModelBatchOperationFlagLength");
            findAnisoShapeModelBatchOperationFlagLengthElement.SetAttribute("findAnisoShapeModelBatchOperationFlagLength", UserCode.GetInstance().gProCd[idx].gASSP.findAnisoShapeModelBatchOperationFlag.Length.ToString());
            findAnisoShapeModelBatchOperationFlagRootElement.AppendChild(findAnisoShapeModelBatchOperationFlagLengthElement);
            for (int i = 0; i < UserCode.GetInstance().gProCd[idx].gASSP.findAnisoShapeModelBatchOperationFlag.Length; i++)
            {
                XmlElement findAnisoShapeModelBatchOperationFlagOneElement = tXmlDoc.CreateElement("findAnisoShapeModelBatchOperationFlag" + i.ToString());
                findAnisoShapeModelBatchOperationFlagOneElement.SetAttribute("findAnisoShapeModelBatchOperationFlag" + i.ToString(), UserCode.GetInstance().gProCd[idx].gASSP.findAnisoShapeModelBatchOperationFlag[i].ToString());
                findAnisoShapeModelBatchOperationFlagRootElement.AppendChild(findAnisoShapeModelBatchOperationFlagOneElement);
            }
            //modelIsChecked ChildNode
            XmlElement modelIsCheckedRootElement = tXmlDoc.CreateElement("ModelIsChecked");
            rootAnisoShapeSearchFBD.AppendChild(modelIsCheckedRootElement);
            XmlElement modelIsCheckedLengthElement = tXmlDoc.CreateElement("ModelIsCheckedLength");
            modelIsCheckedLengthElement.SetAttribute("ModelIsCheckedLength", UserCode.GetInstance().gProCd[idx].gASSP.modelIsChecked.Length.ToString());
            modelIsCheckedRootElement.AppendChild(modelIsCheckedLengthElement);
            for (int i = 0; i < UserCode.GetInstance().gProCd[idx].gASSP.modelIsChecked.Length; i++)
            {
                XmlElement modelIsCheckedOneElement = tXmlDoc.CreateElement("ModelIsChecked" + i.ToString());
                modelIsCheckedOneElement.SetAttribute("ModelIsChecked" + i.ToString(), UserCode.GetInstance().gProCd[idx].gASSP.modelIsChecked[i].ToString());
                modelIsCheckedRootElement.AppendChild(modelIsCheckedOneElement);
            }

            //AllShapeModelMultiplePara ChildNode
            XmlElement AllShapeModelMultipleParaRootElement = tXmlDoc.CreateElement("AllShapeModelMultiplePara");
            rootAnisoShapeSearchFBD.AppendChild(AllShapeModelMultipleParaRootElement);
            XmlElement allShapeModelMultipleParaLengthElement = tXmlDoc.CreateElement("AllShapeModelMultipleParaLength");
            allShapeModelMultipleParaLengthElement.SetAttribute("AllShapeModelMultipleParaLength", UserCode.GetInstance().gProCd[idx].gASSP.angleStart.Length.ToString());
            AllShapeModelMultipleParaRootElement.AppendChild(allShapeModelMultipleParaLengthElement);

            XmlElement angleStartRootElement = tXmlDoc.CreateElement("AngleStart");
            AllShapeModelMultipleParaRootElement.AppendChild(angleStartRootElement);

            XmlElement angleExtentRootElement = tXmlDoc.CreateElement("AngleExtent");
            AllShapeModelMultipleParaRootElement.AppendChild(angleExtentRootElement);

            XmlElement minScoreRootElement = tXmlDoc.CreateElement("MinScore");
            AllShapeModelMultipleParaRootElement.AppendChild(minScoreRootElement);

            XmlElement numMatchesRootElement = tXmlDoc.CreateElement("NumMatches");
            AllShapeModelMultipleParaRootElement.AppendChild(numMatchesRootElement);

            XmlElement maxOverlapRootElement = tXmlDoc.CreateElement("MaxOverlap");
            maxOverlapRootElement.SetAttribute("MaxOverlap", UserCode.GetInstance().gProCd[idx].gASSP.maxOverlap.ToString());
            AllShapeModelMultipleParaRootElement.AppendChild(maxOverlapRootElement);

            XmlElement subPixelRootElement = tXmlDoc.CreateElement("SubPixel");
            AllShapeModelMultipleParaRootElement.AppendChild(subPixelRootElement);

            XmlElement numLevelsRootElement = tXmlDoc.CreateElement("NumLevels");
            AllShapeModelMultipleParaRootElement.AppendChild(numLevelsRootElement);

            XmlElement greedinessRootElement = tXmlDoc.CreateElement("Greediness");
            AllShapeModelMultipleParaRootElement.AppendChild(greedinessRootElement);

            XmlElement scaleRMinRootElement = tXmlDoc.CreateElement("ScaleRMin");
            AllShapeModelMultipleParaRootElement.AppendChild(scaleRMinRootElement);

            XmlElement scaleRMaxRootElement = tXmlDoc.CreateElement("ScaleRMax");
            AllShapeModelMultipleParaRootElement.AppendChild(scaleRMaxRootElement);

            //XmlElement scaleRStepRootElement = tXmlDoc.CreateElement("ScaleRStep");
            //AllShapeModelMultipleParaRootElement.AppendChild(scaleRStepRootElement);

            XmlElement scaleCMinRootElement = tXmlDoc.CreateElement("ScaleCMin");
            AllShapeModelMultipleParaRootElement.AppendChild(scaleCMinRootElement);

            XmlElement scaleCMaxRootElement = tXmlDoc.CreateElement("ScaleCMax");
            AllShapeModelMultipleParaRootElement.AppendChild(scaleCMaxRootElement);

            //XmlElement scaleCStepRootElement = tXmlDoc.CreateElement("ScaleCStep");
            //AllShapeModelMultipleParaRootElement.AppendChild(scaleCStepRootElement);


            for (int i = 0; i < UserCode.GetInstance().gProCd[idx].gASSP.angleStart.Length; i++)
            {
                XmlElement angleStartOneElement = tXmlDoc.CreateElement("AngleStart" + i.ToString());
                angleStartOneElement.SetAttribute("AngleStart" + i.ToString(), UserCode.GetInstance().gProCd[idx].gASSP.angleStart[i].ToString());
                angleStartRootElement.AppendChild(angleStartOneElement);

                XmlElement angleExtentOneElement = tXmlDoc.CreateElement("AngleExtent" + i.ToString());
                angleExtentOneElement.SetAttribute("AngleExtent" + i.ToString(), UserCode.GetInstance().gProCd[idx].gASSP.angleExtent[i].ToString());
                angleExtentRootElement.AppendChild(angleExtentOneElement);

                XmlElement minScoreOneElement = tXmlDoc.CreateElement("MinScore" + i.ToString());
                minScoreOneElement.SetAttribute("MinScore" + i.ToString(), UserCode.GetInstance().gProCd[idx].gASSP.minScore[i].ToString());
                minScoreRootElement.AppendChild(minScoreOneElement);

                XmlElement numMatchesOneElement = tXmlDoc.CreateElement("NumMatches" + i.ToString());
                numMatchesOneElement.SetAttribute("NumMatches" + i.ToString(), UserCode.GetInstance().gProCd[idx].gASSP.numMatches[i].ToString());
                numMatchesRootElement.AppendChild(numMatchesOneElement);



                XmlElement subPixelOneElement = tXmlDoc.CreateElement("SubPixel" + (i * 2).ToString());
                subPixelOneElement.SetAttribute("SubPixel" + (i * 2).ToString(), UserCode.GetInstance().gProCd[idx].gASSP.subPixel[2 * i].ToString());
                subPixelRootElement.AppendChild(subPixelOneElement);
                XmlElement subPixelTwoElement = tXmlDoc.CreateElement("SubPixel" + (i * 2 + 1).ToString());
                subPixelTwoElement.SetAttribute("SubPixel" + (i * 2 + 1).ToString(), UserCode.GetInstance().gProCd[idx].gASSP.subPixel[2 * i + 1].ToString());
                subPixelRootElement.AppendChild(subPixelTwoElement);

                XmlElement numLevelsOneElement = tXmlDoc.CreateElement("NumLevels" + (i * 2).ToString());
                numLevelsOneElement.SetAttribute("NumLevels" + (i * 2).ToString(), UserCode.GetInstance().gProCd[idx].gASSP.numLevels[2 * i].ToString());
                numLevelsRootElement.AppendChild(numLevelsOneElement);
                XmlElement numLevelsTwoElement = tXmlDoc.CreateElement("NumLevels" + (i * 2 + 1).ToString());
                numLevelsTwoElement.SetAttribute("NumLevels" + (i * 2 + 1).ToString(), UserCode.GetInstance().gProCd[idx].gASSP.numLevels[2 * i + 1].ToString());
                numLevelsRootElement.AppendChild(numLevelsTwoElement);

                XmlElement greedinessOneElement = tXmlDoc.CreateElement("Greediness" + i.ToString());
                greedinessOneElement.SetAttribute("Greediness" + i.ToString(), UserCode.GetInstance().gProCd[idx].gASSP.greediness[i].ToString());
                greedinessRootElement.AppendChild(greedinessOneElement);

                XmlElement scaleRMinOneElement = tXmlDoc.CreateElement("ScaleRMin" + i.ToString());
                scaleRMinOneElement.SetAttribute("ScaleRMin" + i.ToString(), UserCode.GetInstance().gProCd[idx].gASSP.scaleRMin[i].ToString());
                scaleRMinRootElement.AppendChild(scaleRMinOneElement);

                XmlElement scaleRMaxOneElement = tXmlDoc.CreateElement("ScaleRMax" + i.ToString());
                scaleRMaxOneElement.SetAttribute("ScaleRMax" + i.ToString(), UserCode.GetInstance().gProCd[idx].gASSP.scaleRMax[i].ToString());
                scaleRMaxRootElement.AppendChild(scaleRMaxOneElement);

                //XmlElement scaleRStepOneElement = tXmlDoc.CreateElement("ScaleRStep" + i.ToString());
                //scaleRStepOneElement.SetAttribute("ScaleRStep" + i.ToString(), UserCode.GetInstance().gProCd[idx].gASSP.scaleRStep[i].ToString());
                //scaleRStepRootElement.AppendChild(scaleRStepOneElement);

                XmlElement scaleCMinOneElement = tXmlDoc.CreateElement("ScaleCMin" + i.ToString());
                scaleCMinOneElement.SetAttribute("ScaleCMin" + i.ToString(), UserCode.GetInstance().gProCd[idx].gASSP.scaleCMin[i].ToString());
                scaleCMinRootElement.AppendChild(scaleCMinOneElement);

                XmlElement scaleCMaxOneElement = tXmlDoc.CreateElement("ScaleCMax" + i.ToString());
                scaleCMaxOneElement.SetAttribute("ScaleCMax" + i.ToString(), UserCode.GetInstance().gProCd[idx].gASSP.scaleCMax[i].ToString());
                scaleCMaxRootElement.AppendChild(scaleCMaxOneElement);

                //XmlElement scaleCStepOneElement = tXmlDoc.CreateElement("ScaleCStep" + i.ToString());
                //scaleCStepOneElement.SetAttribute("ScaleCStep" + i.ToString(), UserCode.GetInstance().gProCd[idx].gASSP.scaleCStep[i].ToString());
                //scaleCStepRootElement.AppendChild(scaleCStepOneElement);
            }


        }
        private void SaveSerialOutputFBDXML(int idx, out XmlElement rootSerialOutputFBD)
        {
            rootSerialOutputFBD = tXmlDoc.CreateElement(UserCode.GetInstance().codeInfoValToKey[UserCode.GetInstance().gProCd[idx].FuncID] + "_" + idx.ToString());
            //IsGige childNode
            XmlElement isGigeRootElement = tXmlDoc.CreateElement("IsGige");
            isGigeRootElement.SetAttribute("IsGige", UserCode.GetInstance().gProCd[idx].gSOP.isGige.ToString());
            rootSerialOutputFBD.AppendChild(isGigeRootElement);
            //OutputForm childNode
            XmlElement outputFormRootElement = tXmlDoc.CreateElement("OutputForm");
            outputFormRootElement.SetAttribute("OutputForm", UserCode.GetInstance().gProCd[idx].gSOP.outputForm.ToString());
            rootSerialOutputFBD.AppendChild(outputFormRootElement);
            //IntBit childNode
            XmlElement intBitRootElement = tXmlDoc.CreateElement("IntBit");
            intBitRootElement.SetAttribute("IntBit", UserCode.GetInstance().gProCd[idx].gSOP.intBit.ToString());
            rootSerialOutputFBD.AppendChild(intBitRootElement);
            //FloatBit childNode
            XmlElement floatBitRootElement = tXmlDoc.CreateElement("FloatBit");
            floatBitRootElement.SetAttribute("FloatBit", UserCode.GetInstance().gProCd[idx].gSOP.floatBit.ToString());
            rootSerialOutputFBD.AppendChild(floatBitRootElement);
            //NegativeMinus childNode
            XmlElement NegativeMinusRootElement = tXmlDoc.CreateElement("NegativeMinus");
            NegativeMinusRootElement.SetAttribute("NegativeMinus", UserCode.GetInstance().gProCd[idx].gSOP.NegativeMinus.ToString());
            rootSerialOutputFBD.AppendChild(NegativeMinusRootElement);
            //EraseZeroYes childNode
            XmlElement EraseZeroYesRootElement = tXmlDoc.CreateElement("EraseZeroYes");
            EraseZeroYesRootElement.SetAttribute("EraseZeroYes", UserCode.GetInstance().gProCd[idx].gSOP.EraseZeroYes.ToString());
            rootSerialOutputFBD.AppendChild(EraseZeroYesRootElement);
            //FieldSeparator childNode
            XmlElement FieldSeparatorRootElement = tXmlDoc.CreateElement("FieldSeparator");
            FieldSeparatorRootElement.SetAttribute("FieldSeparator", UserCode.GetInstance().gProCd[idx].gSOP.FieldSeparator.ToString());
            rootSerialOutputFBD.AppendChild(FieldSeparatorRootElement);
            //RecordSeparator childNode
            XmlElement RecordSeparatorRootElement = tXmlDoc.CreateElement("RecordSeparator");
            RecordSeparatorRootElement.SetAttribute("RecordSeparator", UserCode.GetInstance().gProCd[idx].gSOP.RecordSeparator.ToString());
            rootSerialOutputFBD.AppendChild(RecordSeparatorRootElement);


            //sendDataInfoList childNode
            XmlElement sendDataInfoListRootElement = tXmlDoc.CreateElement("sendDataInfoList");
            rootSerialOutputFBD.AppendChild(sendDataInfoListRootElement);

            XmlElement sendDataInfoListLengthRootElement = tXmlDoc.CreateElement("sendDataInfoListLength");
            sendDataInfoListLengthRootElement.SetAttribute("sendDataInfoListLength", UserCode.GetInstance().gProCd[idx].gSOP.sendDataInfoList.Count.ToString());
            sendDataInfoListRootElement.AppendChild(sendDataInfoListLengthRootElement);

            XmlElement sendDataInfoListRowRootElement = tXmlDoc.CreateElement("sendDataInfoListRow");
            sendDataInfoListRootElement.AppendChild(sendDataInfoListRowRootElement);

            XmlElement sendDataInfoListFuncIDRootElement = tXmlDoc.CreateElement("sendDataInfoListFuncID");
            sendDataInfoListRootElement.AppendChild(sendDataInfoListFuncIDRootElement);

            XmlElement sendDataInfoListDatatypeRootElement = tXmlDoc.CreateElement("sendDataInfoListDatatype");
            sendDataInfoListRootElement.AppendChild(sendDataInfoListDatatypeRootElement);

            XmlElement sendDataInfoListIdxRootElement = tXmlDoc.CreateElement("sendDataInfoListIdx");
            sendDataInfoListRootElement.AppendChild(sendDataInfoListIdxRootElement);

            for (int i = 0; i < UserCode.GetInstance().gProCd[idx].gSOP.sendDataInfoList.Count; i++)
            {

                XmlElement sendDataInfoListRowOneRootElement = tXmlDoc.CreateElement("sendDataInfoListRow" + i.ToString());
                sendDataInfoListRowOneRootElement.SetAttribute("sendDataInfoListRow" + i.ToString(), UserCode.GetInstance().gProCd[idx].gSOP.sendDataInfoList[i].row.ToString());
                sendDataInfoListRowRootElement.AppendChild(sendDataInfoListRowOneRootElement);

                XmlElement sendDataInfoListFuncIDOneRootElement = tXmlDoc.CreateElement("sendDataInfoListFuncID" + i.ToString());
                sendDataInfoListFuncIDOneRootElement.SetAttribute("sendDataInfoListFuncID" + i.ToString(), UserCode.GetInstance().gProCd[idx].gSOP.sendDataInfoList[i].funcID.ToString());
                sendDataInfoListFuncIDRootElement.AppendChild(sendDataInfoListFuncIDOneRootElement);

                XmlElement sendDataInfoListDatatypeOneRootElement = tXmlDoc.CreateElement("sendDataInfoListDatatype" + i.ToString());
                sendDataInfoListDatatypeOneRootElement.SetAttribute("sendDataInfoListDatatype" + i.ToString(), UserCode.GetInstance().gProCd[idx].gSOP.sendDataInfoList[i].datatype.ToString());
                sendDataInfoListDatatypeRootElement.AppendChild(sendDataInfoListDatatypeOneRootElement);

                XmlElement sendDataInfoListIdxOneRootElement = tXmlDoc.CreateElement("sendDataInfoListIdx" + i.ToString());
                sendDataInfoListIdxOneRootElement.SetAttribute("sendDataInfoListIdx" + i.ToString(), UserCode.GetInstance().gProCd[idx].gSOP.sendDataInfoList[i].idx.ToString());
                sendDataInfoListIdxRootElement.AppendChild(sendDataInfoListIdxOneRootElement);
            }

        }


        public void OpenXmlToProj(String tPath, String tPathOnly, String tFilename)
        {
            try
            {
                XmlDocument tXmlDoc = new XmlDocument();

                tXmlDoc.Load(tPath);
                XmlNode rootNode = tXmlDoc.SelectSingleNode("Program");

                mFilename = tPathOnly;
                //  mFilenameOnly = tFilename.Split('.')[0];
                //gExcelEdit.Open(tPath);
                //gMainSheet = gExcelEdit.GetSheet("Program");
                //tXmlDoc.get
                Svision.GetMe().listBoxProcess.Items.Clear();
                Svision.GetMe().listBoxProcess.Items.Insert(0, "0.相机输入");
                Svision.GetMe().checkBoxDoNotShowImage.Checked = bool.Parse(rootNode.SelectSingleNode("DoNotShowImage").Attributes["DoNotShowImage"].InnerText);
                XmlNode OrderNode = rootNode.SelectSingleNode("Order");
                XmlNode IsOverFlagNode = rootNode.SelectSingleNode("isOverFlag");
                for (int i = 0; i < 20; i++)
                {
                    String strTest = OrderNode.SelectSingleNode("Order" + i.ToString()).Attributes["Order" + i.ToString()].InnerText;
                    XmlNode ProgramNode = rootNode.SelectSingleNode(strTest + "_" + i.ToString());
                    UserCode.GetInstance().isOverFlag[i] = int.Parse(IsOverFlagNode.SelectSingleNode("isOverFlag" + i.ToString()).Attributes["isOverFlag" + i.ToString()].InnerText);
                    switch (UserCode.GetInstance().codeInfo[OrderNode.SelectSingleNode("Order" + i.ToString()).Attributes["Order" + i.ToString()].InnerText])
                    {
                        case ProCodeCls.MainFunction.NullFBD:
                            UserCode.GetInstance().gProCd[i].FuncID = ProCodeCls.MainFunction.NullFBD;
                            Svision.GetMe().listBoxProcess.Items.Insert(i,"");
                            LoadNullFBDXML(i);
                            break;
                        case ProCodeCls.MainFunction.InputCameraInputFBD:
                            UserCode.GetInstance().gProCd[i].FuncID = ProCodeCls.MainFunction.InputCameraInputFBD;
                            LoadCameraInputFBDXML(i);
                            break;
                        case ProCodeCls.MainFunction.CalibrationThresholdFBD:
                            UserCode.GetInstance().gProCd[i].FuncID = ProCodeCls.MainFunction.CalibrationThresholdFBD;
                            Svision.GetMe().listBoxProcess.Items.Insert(i, i.ToString() + ".二值化");
                            LoadThresholdFBDXML(i, ProgramNode);
                            break;
                        case ProCodeCls.MainFunction.CalibrationMedianFilterFBD:
                            UserCode.GetInstance().gProCd[i].FuncID = ProCodeCls.MainFunction.CalibrationMedianFilterFBD;
                            Svision.GetMe().listBoxProcess.Items.Insert(i, i.ToString() + ".中值滤波");
                            LoadMedianFilterFBDXML(i, ProgramNode);
                            break;
                        case ProCodeCls.MainFunction.CalibrationBackgroundRemoveFBD:
                            UserCode.GetInstance().gProCd[i].FuncID = ProCodeCls.MainFunction.CalibrationBackgroundRemoveFBD;
                            Svision.GetMe().listBoxProcess.Items.Insert(i, i.ToString() + ".背景消除");
                            LoadBackgroundRemoveFBDXML(i, ProgramNode);
                            break;
                        case ProCodeCls.MainFunction.CalibrationMorphologyProcessingFBD:
                            UserCode.GetInstance().gProCd[i].FuncID = ProCodeCls.MainFunction.CalibrationMorphologyProcessingFBD;
                            Svision.GetMe().listBoxProcess.Items.Insert(i, i.ToString() + ".图像形态学处理");
                            LoadMorphologyProcessingFBDXML(i, ProgramNode);
                            break;
                        case ProCodeCls.MainFunction.MeasureBlobAnalysisFBD:
                            UserCode.GetInstance().gProCd[i].FuncID = ProCodeCls.MainFunction.MeasureBlobAnalysisFBD;
                            Svision.GetMe().listBoxProcess.Items.Insert(i, i.ToString() + ".Blob分析");
                            LoadBlobAnalysisFBDXML(i, ProgramNode);
                            break;
                        case ProCodeCls.MainFunction.MeasureShapeSearchFBD:
                            UserCode.GetInstance().gProCd[i].FuncID = ProCodeCls.MainFunction.MeasureShapeSearchFBD;
                            Svision.GetMe().listBoxProcess.Items.Insert(i, i.ToString() + ".形状搜索");
                            LoadShapeSearchFBDXML(i, ProgramNode);
                            break;
                        case ProCodeCls.MainFunction.MeasureAnisoShapeSearchFBD:
                            UserCode.GetInstance().gProCd[i].FuncID = ProCodeCls.MainFunction.MeasureAnisoShapeSearchFBD;
                            Svision.GetMe().listBoxProcess.Items.Insert(i, i.ToString() + ".可变形状搜索");
                            LoadAnisoShapeSearchFBDXML(i, ProgramNode);
                            break;
                        case ProCodeCls.MainFunction.OutputSerialOutputFBD:
                            UserCode.GetInstance().gProCd[i].FuncID = ProCodeCls.MainFunction.OutputSerialOutputFBD;
                            Svision.GetMe().listBoxProcess.Items.Insert(i, i.ToString() + ".串行输出");
                            LoadSerialOutputFBDXML(i, ProgramNode);
                            break;
                    }
                }
                XmlNode showCurrentIDNode = null;
                bool isshowCurrentIDOK = true;
                try
                {
                    showCurrentIDNode = rootNode.SelectSingleNode("ShowCurIdx");
                }
                catch (System.Exception ex)
                {
                    isshowCurrentIDOK = false;
                }
                if (isshowCurrentIDOK && showCurrentIDNode != null)
                {
                    Svision.GetMe().listBoxProcess.SelectedIndex = int.Parse(showCurrentIDNode.Attributes["ShowCurIdx"].InnerText);
                    
                }
                String tmpPath = tPath.Substring(0, tPath.IndexOf(tFilename, 0));
                String tmpFileName = tFilename.Substring(0, tFilename.IndexOf("."));
                tmpPath = tmpPath + tmpFileName + "_CfgInfoOperate.xml";

                ConfigInformation.GetInstance().tCfgInfoOprt.LoadCfgInfo(tmpPath);


            }
            catch (System.Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        private void LoadNullFBDXML(int idx)
        {

        }
        private void LoadCameraInputFBDXML(int idx)
        {

        }
        private void LoadThresholdFBDXML(int idx, XmlNode ProgramNode)
        {

            UserCode.GetInstance().gProCd[idx].gTP.maxValue = float.Parse(ProgramNode.SelectSingleNode("MaxValue").Attributes["MaxValue"].InnerText);
            UserCode.GetInstance().gProCd[idx].gTP.minValue = float.Parse(ProgramNode.SelectSingleNode("MinValue").Attributes["MinValue"].InnerText);

        }
        private void LoadMedianFilterFBDXML(int idx, XmlNode ProgramNode)
        {
            UserCode.GetInstance().gProCd[idx].gMFP.maskSize = int.Parse(ProgramNode.SelectSingleNode("MaskSize").Attributes["MaskSize"].InnerText);
        }
        private void LoadBackgroundRemoveFBDXML(int idx, XmlNode ProgramNode)
        {
            UserCode.GetInstance().gProCd[idx].gBRP.isAllColor = bool.Parse(ProgramNode.SelectSingleNode("IsAllColor").Attributes["IsAllColor"].InnerText);
            UserCode.GetInstance().gProCd[idx].gBRP.grayValue[0] = float.Parse(ProgramNode.SelectSingleNode("GrayValue0").Attributes["GrayValue0"].InnerText);
            UserCode.GetInstance().gProCd[idx].gBRP.grayValue[1] = float.Parse(ProgramNode.SelectSingleNode("GrayValue1").Attributes["GrayValue1"].InnerText);
            UserCode.GetInstance().gProCd[idx].gBRP.grayValue[2] = float.Parse(ProgramNode.SelectSingleNode("GrayValue2").Attributes["GrayValue2"].InnerText);
            UserCode.GetInstance().gProCd[idx].gBRP.grayValue[3] = float.Parse(ProgramNode.SelectSingleNode("GrayValue3").Attributes["GrayValue3"].InnerText);
            UserCode.GetInstance().gProCd[idx].gBRP.grayValue[4] = float.Parse(ProgramNode.SelectSingleNode("GrayValue4").Attributes["GrayValue4"].InnerText);
            UserCode.GetInstance().gProCd[idx].gBRP.grayValue[5] = float.Parse(ProgramNode.SelectSingleNode("GrayValue5").Attributes["GrayValue5"].InnerText);
            UserCode.GetInstance().gProCd[idx].gBRP.grayValue[6] = float.Parse(ProgramNode.SelectSingleNode("GrayValue6").Attributes["GrayValue6"].InnerText);
            UserCode.GetInstance().gProCd[idx].gBRP.grayValue[7] = float.Parse(ProgramNode.SelectSingleNode("GrayValue7").Attributes["GrayValue7"].InnerText);

        }
        private void LoadMorphologyProcessingFBDXML(int idx, XmlNode ProgramNode)
        {
            UserCode.GetInstance().gProCd[idx].gMPP.processID = int.Parse(ProgramNode.SelectSingleNode("ProcessID").Attributes["ProcessID"].InnerText);
            UserCode.GetInstance().gProCd[idx].gMPP.elementID = int.Parse(ProgramNode.SelectSingleNode("ElementID").Attributes["ElementID"].InnerText);
            UserCode.GetInstance().gProCd[idx].gMPP.width = int.Parse(ProgramNode.SelectSingleNode("Width").Attributes["Width"].InnerText);
            UserCode.GetInstance().gProCd[idx].gMPP.height = int.Parse(ProgramNode.SelectSingleNode("Height").Attributes["Height"].InnerText);
            UserCode.GetInstance().gProCd[idx].gMPP.radius = double.Parse(ProgramNode.SelectSingleNode("Radius").Attributes["Radius"].InnerText);

        }
        private void LoadBlobAnalysisFBDXML(int idx, XmlNode ProgramNode)
        {

            #region SegmentPara

            //isColor
            UserCode.GetInstance().gProCd[idx].gBP.isColor = bool.Parse(ProgramNode.SelectSingleNode("IsColor").Attributes["IsColor"].InnerText);

            //isAutoSegment
            UserCode.GetInstance().gProCd[idx].gBP.isAutoSegment = bool.Parse(ProgramNode.SelectSingleNode("IsAutoSegment").Attributes["IsAutoSegment"].InnerText);

            //SegmentSelectPara
            XmlNode SegmentSelectParaRootNode = ProgramNode.SelectSingleNode("SegmentSelectPara");
            for (int i = 0; i < int.Parse(SegmentSelectParaRootNode.SelectSingleNode("SegmentSelectParaLength").Attributes["SegmentSelectParaLength"].InnerText); i++)
            {
                UserCode.GetInstance().gProCd[idx].gBP.selectedColor[i] = bool.Parse(SegmentSelectParaRootNode.SelectSingleNode("selectedColor").SelectSingleNode("selectedColor" + i.ToString()).Attributes["selectedColor" + i.ToString()].InnerText);
                UserCode.GetInstance().gProCd[idx].gBP.redValue[i] = int.Parse(SegmentSelectParaRootNode.SelectSingleNode("redValue").SelectSingleNode("redValue" + i.ToString()).Attributes["redValue" + i.ToString()].InnerText);
                UserCode.GetInstance().gProCd[idx].gBP.greenValue[i] = int.Parse(SegmentSelectParaRootNode.SelectSingleNode("greenValue").SelectSingleNode("greenValue" + i.ToString()).Attributes["greenValue" + i.ToString()].InnerText);
                UserCode.GetInstance().gProCd[idx].gBP.blueValue[i] = int.Parse(SegmentSelectParaRootNode.SelectSingleNode("blueValue").SelectSingleNode("blueValue" + i.ToString()).Attributes["blueValue" + i.ToString()].InnerText);
                UserCode.GetInstance().gProCd[idx].gBP.grayValue[i] = int.Parse(SegmentSelectParaRootNode.SelectSingleNode("grayValue").SelectSingleNode("grayValue" + i.ToString()).Attributes["grayValue" + i.ToString()].InnerText);
                UserCode.GetInstance().gProCd[idx].gBP.lengthValue[i] = int.Parse(SegmentSelectParaRootNode.SelectSingleNode("lengthValue").SelectSingleNode("lengthValue" + i.ToString()).Attributes["lengthValue" + i.ToString()].InnerText);
                UserCode.GetInstance().gProCd[idx].gBP.isBesideThisColor[i] = bool.Parse(SegmentSelectParaRootNode.SelectSingleNode("isBesideThisColor").SelectSingleNode("isBesideThisColor" + i.ToString()).Attributes["isBesideThisColor" + i.ToString()].InnerText);
            }

            //MaxGrayValue
            UserCode.GetInstance().gProCd[idx].gBP.MaxGrayValue = int.Parse(ProgramNode.SelectSingleNode("MaxGrayValue").Attributes["MaxGrayValue"].InnerText);

            //MinGrayValue
            UserCode.GetInstance().gProCd[idx].gBP.MinGrayValue = int.Parse(ProgramNode.SelectSingleNode("MinGrayValue").Attributes["MinGrayValue"].InnerText);

            //isAutoSegmentMethod1
            UserCode.GetInstance().gProCd[idx].gBP.isAutoSegmentMethod1 = bool.Parse(ProgramNode.SelectSingleNode("IsAutoSegmentMethod1").Attributes["IsAutoSegmentMethod1"].InnerText);

            //isAutoSegmentMethod2
            UserCode.GetInstance().gProCd[idx].gBP.isAutoSegmentMethod2 = bool.Parse(ProgramNode.SelectSingleNode("IsAutoSegmentMethod2").Attributes["IsAutoSegmentMethod2"].InnerText);

            //isAutoSegmentMethod3
            UserCode.GetInstance().gProCd[idx].gBP.isAutoSegmentMethod3 = bool.Parse(ProgramNode.SelectSingleNode("IsAutoSegmentMethod3").Attributes["IsAutoSegmentMethod3"].InnerText);

            //autoSegmentMethod1Para1
            UserCode.GetInstance().gProCd[idx].gBP.autoSegmentMethod1Para1 = float.Parse(ProgramNode.SelectSingleNode("AutoSegmentMethod1Para1").Attributes["AutoSegmentMethod1Para1"].InnerText);

            //autoSegmentMethod2Para1
            UserCode.GetInstance().gProCd[idx].gBP.autoSegmentMethod2Para1 = int.Parse(ProgramNode.SelectSingleNode("AutoSegmentMethod2Para1").Attributes["AutoSegmentMethod2Para1"].InnerText);

            //autoSegmentMethod2Para2
            UserCode.GetInstance().gProCd[idx].gBP.autoSegmentMethod2Para2 = float.Parse(ProgramNode.SelectSingleNode("AutoSegmentMethod2Para2").Attributes["AutoSegmentMethod2Para2"].InnerText);

            //autoSegmentMethod2Para3
            UserCode.GetInstance().gProCd[idx].gBP.autoSegmentMethod2Para3 = int.Parse(ProgramNode.SelectSingleNode("AutoSegmentMethod2Para3").Attributes["AutoSegmentMethod2Para3"].InnerText);

            //autoSegmentMethod2Para4
            UserCode.GetInstance().gProCd[idx].gBP.autoSegmentMethod2Para4 = int.Parse(ProgramNode.SelectSingleNode("AutoSegmentMethod2Para4").Attributes["AutoSegmentMethod2Para4"].InnerText);

            //segmentShow
            UserCode.GetInstance().gProCd[idx].gBP.segmentShow = int.Parse(ProgramNode.SelectSingleNode("SegmentShow").Attributes["SegmentShow"].InnerText);

            #endregion

            #region MorphologyPara

            //isFillUpHoles
            UserCode.GetInstance().gProCd[idx].gBP.isFillUpHoles = bool.Parse(ProgramNode.SelectSingleNode("isFillUpHoles").Attributes["isFillUpHoles"].InnerText);

            //isConnectionBeforeFillUpHoles
            UserCode.GetInstance().gProCd[idx].gBP.isConnectionBeforeFillUpHoles = bool.Parse(ProgramNode.SelectSingleNode("isConnectionBeforeFillUpHoles").Attributes["isConnectionBeforeFillUpHoles"].InnerText);

            //isErosion
            UserCode.GetInstance().gProCd[idx].gBP.isErosion = bool.Parse(ProgramNode.SelectSingleNode("isErosion").Attributes["isErosion"].InnerText);

            //erosionElementNUMElement
            UserCode.GetInstance().gProCd[idx].gBP.erosionElementNUM = int.Parse(ProgramNode.SelectSingleNode("erosionElementNUM").Attributes["erosionElementNUM"].InnerText);

            //erosionRWidth
            UserCode.GetInstance().gProCd[idx].gBP.erosionRWidth = int.Parse(ProgramNode.SelectSingleNode("erosionRWidth").Attributes["erosionRWidth"].InnerText);

            //erosionRHeight
            UserCode.GetInstance().gProCd[idx].gBP.erosionRHeight = int.Parse(ProgramNode.SelectSingleNode("erosionRHeight").Attributes["erosionRHeight"].InnerText);

            //erosionCRadius
            UserCode.GetInstance().gProCd[idx].gBP.erosionCRadius = double.Parse(ProgramNode.SelectSingleNode("erosionCRadius").Attributes["erosionCRadius"].InnerText);

            //isDilation
            UserCode.GetInstance().gProCd[idx].gBP.isDilation = bool.Parse(ProgramNode.SelectSingleNode("isDilation").Attributes["isDilation"].InnerText);

            //dilationElementNUM
            UserCode.GetInstance().gProCd[idx].gBP.dilationElementNUM = int.Parse(ProgramNode.SelectSingleNode("dilationElementNUM").Attributes["dilationElementNUM"].InnerText);

            //dilationRWidth
            UserCode.GetInstance().gProCd[idx].gBP.dilationRWidth = int.Parse(ProgramNode.SelectSingleNode("dilationRWidth").Attributes["dilationRWidth"].InnerText);

            //dilationRHeight
            UserCode.GetInstance().gProCd[idx].gBP.dilationRHeight = int.Parse(ProgramNode.SelectSingleNode("dilationRHeight").Attributes["dilationRHeight"].InnerText);

            //dilationCRadius
            UserCode.GetInstance().gProCd[idx].gBP.dilationCRadius = double.Parse(ProgramNode.SelectSingleNode("dilationCRadius").Attributes["dilationCRadius"].InnerText);

            //isOpening
            UserCode.GetInstance().gProCd[idx].gBP.isOpening = bool.Parse(ProgramNode.SelectSingleNode("isOpening").Attributes["isOpening"].InnerText);

            //openingElementNUM
            UserCode.GetInstance().gProCd[idx].gBP.openingElementNUM = int.Parse(ProgramNode.SelectSingleNode("openingElementNUM").Attributes["openingElementNUM"].InnerText);

            //openingRWidth
            UserCode.GetInstance().gProCd[idx].gBP.openingRWidth = int.Parse(ProgramNode.SelectSingleNode("openingRWidth").Attributes["openingRWidth"].InnerText);

            //openingRHeight
            UserCode.GetInstance().gProCd[idx].gBP.openingRHeight = int.Parse(ProgramNode.SelectSingleNode("openingRHeight").Attributes["openingRHeight"].InnerText);

            //openingCRadius
            UserCode.GetInstance().gProCd[idx].gBP.openingCRadius = double.Parse(ProgramNode.SelectSingleNode("openingCRadius").Attributes["openingCRadius"].InnerText);

            //isClosing
            UserCode.GetInstance().gProCd[idx].gBP.isClosing = bool.Parse(ProgramNode.SelectSingleNode("isClosing").Attributes["isClosing"].InnerText);

            //closingElementNUM
            UserCode.GetInstance().gProCd[idx].gBP.closingElementNUM = int.Parse(ProgramNode.SelectSingleNode("closingElementNUM").Attributes["closingElementNUM"].InnerText);

            //closingRWidth
            UserCode.GetInstance().gProCd[idx].gBP.closingRWidth = int.Parse(ProgramNode.SelectSingleNode("closingRWidth").Attributes["closingRWidth"].InnerText);

            //closingRHeight
            UserCode.GetInstance().gProCd[idx].gBP.closingRHeight = int.Parse(ProgramNode.SelectSingleNode("closingRHeight").Attributes["closingRHeight"].InnerText);

            //closingCRadius
            UserCode.GetInstance().gProCd[idx].gBP.closingCRadius = double.Parse(ProgramNode.SelectSingleNode("closingCRadius").Attributes["closingCRadius"].InnerText);
            #endregion

            #region SelectPara
            //SelectedPara
            XmlNode SelectedParaRootNode = ProgramNode.SelectSingleNode("SelectedPara");
            for (int i = 0; i < int.Parse(SelectedParaRootNode.SelectSingleNode("SelectedParaLength").Attributes["SelectedParaLength"].InnerText); i++)
            {
                UserCode.GetInstance().gProCd[idx].gBP.selectSTR[i] = SelectedParaRootNode.SelectSingleNode("selectSTR").SelectSingleNode("selectSTR" + i.ToString()).Attributes["selectSTR" + i.ToString()].InnerText;
                UserCode.GetInstance().gProCd[idx].gBP.selectIsChecked[i] = bool.Parse(SelectedParaRootNode.SelectSingleNode("selectIsChecked").SelectSingleNode("selectIsChecked" + i.ToString()).Attributes["selectIsChecked" + i.ToString()].InnerText);
                UserCode.GetInstance().gProCd[idx].gBP.selectMin[i] = double.Parse(SelectedParaRootNode.SelectSingleNode("selectMin").SelectSingleNode("selectMin" + i.ToString()).Attributes["selectMin" + i.ToString()].InnerText);
                UserCode.GetInstance().gProCd[idx].gBP.selectMax[i] = double.Parse(SelectedParaRootNode.SelectSingleNode("selectMax").SelectSingleNode("selectMax" + i.ToString()).Attributes["selectMax" + i.ToString()].InnerText);
            }


            //selectisAnd
            UserCode.GetInstance().gProCd[idx].gBP.selectisAnd = bool.Parse(ProgramNode.SelectSingleNode("selectisAnd").Attributes["selectisAnd"].InnerText);

            //selectArrangeItemIndex
            UserCode.GetInstance().gProCd[idx].gBP.selectArrangeItemIndex = int.Parse(ProgramNode.SelectSingleNode("selectArrangeItemIndex").Attributes["selectArrangeItemIndex"].InnerText);

            //isArrangeLtoS
            UserCode.GetInstance().gProCd[idx].gBP.isArrangeLtoS = bool.Parse(ProgramNode.SelectSingleNode("isArrangeLtoS").Attributes["isArrangeLtoS"].InnerText);

            //regionNum
            UserCode.GetInstance().gProCd[idx].gBP.regionNum = int.Parse(ProgramNode.SelectSingleNode("regionNum").Attributes["regionNum"].InnerText);

            //selectItemCount
            UserCode.GetInstance().gProCd[idx].gBP.selectItemCount = int.Parse(ProgramNode.SelectSingleNode("selectItemCount").Attributes["selectItemCount"].InnerText);
            #endregion

            #region OutputPara
            //OutputPara
            XmlNode OutputParaRootNode = ProgramNode.SelectSingleNode("OutputPara");
            for (int i = 0; i < int.Parse(OutputParaRootNode.SelectSingleNode("OutputParaLength").Attributes["OutputParaLength"].InnerText); i++)
            {
                UserCode.GetInstance().gProCd[idx].gBP.outputShowStr[i] = OutputParaRootNode.SelectSingleNode("outputShowStr").SelectSingleNode("outputShowStr" + i.ToString()).Attributes["outputShowStr" + i.ToString()].InnerText;
                UserCode.GetInstance().gProCd[idx].gBP.outputIDIsChecked[i] = bool.Parse(OutputParaRootNode.SelectSingleNode("outputIDIsChecked").SelectSingleNode("outputIDIsChecked" + i.ToString()).Attributes["outputIDIsChecked" + i.ToString()].InnerText);

            }

            #endregion
            //outPutStringList ChildNode
            UserCode.GetInstance().gProCd[idx].gBP.outPutStringList.Clear();
            XmlNode outPutStringListRootNode = ProgramNode.SelectSingleNode("OutPutStringList");
            for (int i = 0; i < int.Parse(outPutStringListRootNode.SelectSingleNode("OutPutStringListLength").Attributes["OutPutStringListLength"].InnerText); i++)
            {
                UserCode.GetInstance().gProCd[idx].gBP.outPutStringList.Add(outPutStringListRootNode.SelectSingleNode("OutPutStringList" + i.ToString()).Attributes["OutPutStringList" + i.ToString()].InnerText);
            }

            //showOutputResultStr ChildNode
            XmlNode showOutputResultStrRootNode = ProgramNode.SelectSingleNode("ShowOutputResultStr");
            for (int i = 0; i < int.Parse(showOutputResultStrRootNode.SelectSingleNode("ShowOutputResultStrLength").Attributes["ShowOutputResultStrLength"].InnerText); i++)
            {
                UserCode.GetInstance().gProCd[idx].gBP.showOutputResultStr[i] = showOutputResultStrRootNode.SelectSingleNode("ShowOutputResultStr" + i.ToString()).Attributes["ShowOutputResultStr" + i.ToString()].InnerText;
            }

            //showOutputResultFlag ChildNode 
            XmlNode showOutputResultFlagRootNode = ProgramNode.SelectSingleNode("ShowOutputResultFlag");
            for (int i = 0; i < int.Parse(showOutputResultFlagRootNode.SelectSingleNode("ShowOutputResultFlagLength").Attributes["ShowOutputResultFlagLength"].InnerText); i++)
            {
                UserCode.GetInstance().gProCd[idx].gBP.showOutputResultFlag[i] = bool.Parse(showOutputResultFlagRootNode.SelectSingleNode("ShowOutputResultFlag" + i.ToString()).Attributes["ShowOutputResultFlag" + i.ToString()].InnerText);
            }

            //blobAnalysisOperationStr ChildNode
            XmlNode blobAnalysisOperationStrRootNode = ProgramNode.SelectSingleNode("blobAnalysisOperationStr");
            UserCode.GetInstance().gProCd[idx].gBP.blobAnalysisOperationStr = new string[int.Parse(blobAnalysisOperationStrRootNode.SelectSingleNode("blobAnalysisOperationStrLength").Attributes["blobAnalysisOperationStrLength"].InnerText)];

            for (int i = 0; i < int.Parse(blobAnalysisOperationStrRootNode.SelectSingleNode("blobAnalysisOperationStrLength").Attributes["blobAnalysisOperationStrLength"].InnerText); i++)
            {
                UserCode.GetInstance().gProCd[idx].gBP.blobAnalysisOperationStr[i] = blobAnalysisOperationStrRootNode.SelectSingleNode("blobAnalysisOperationStr" + i.ToString()).Attributes["blobAnalysisOperationStr" + i.ToString()].InnerText;
            }

            //blobAnalysisOperationFlag ChildNode 
            XmlNode blobAnalysisOperationFlagRootNode = ProgramNode.SelectSingleNode("blobAnalysisOperationFlag");
            UserCode.GetInstance().gProCd[idx].gBP.blobAnalysisOperationFlag = new bool[int.Parse(blobAnalysisOperationFlagRootNode.SelectSingleNode("blobAnalysisOperationFlagLength").Attributes["blobAnalysisOperationFlagLength"].InnerText)];
            
            for (int i = 0; i < int.Parse(blobAnalysisOperationFlagRootNode.SelectSingleNode("blobAnalysisOperationFlagLength").Attributes["blobAnalysisOperationFlagLength"].InnerText); i++)
            {
                UserCode.GetInstance().gProCd[idx].gBP.blobAnalysisOperationFlag[i] = bool.Parse(blobAnalysisOperationFlagRootNode.SelectSingleNode("blobAnalysisOperationFlag" + i.ToString()).Attributes["blobAnalysisOperationFlag" + i.ToString()].InnerText);
            }

        }
        private void LoadShapeSearchFBDXML(int idx, XmlNode ProgramNode)
        {
            string fullFileName;
            string partFileName;
            UserCode.GetInstance().gProCd[idx].gSSP.shapeModel = null;
            if (UserCode.GetInstance().gProCd[idx].gSSP.shapeModelImage != null)
            {
                UserCode.GetInstance().gProCd[idx].gSSP.shapeModelImage.Dispose();
                UserCode.GetInstance().gProCd[idx].gSSP.shapeModelImage = null;
            }

            //Shape Model And Image ChildNode
            bool isShapeModelAndImageOK = true;
            XmlNode ShapeModelAndImageRootNode = ProgramNode.SelectSingleNode("ShapeModelAndImage");
            try
            {
                int test = int.Parse(ShapeModelAndImageRootNode.SelectSingleNode("ShapeModelAndImageLength").Attributes["ShapeModelAndImageLength"].InnerText); 
            }
            catch (System.Exception ex)
            {
                isShapeModelAndImageOK = false;
            }
            if (isShapeModelAndImageOK)
            {
                for (int i = 0; i < int.Parse(ShapeModelAndImageRootNode.SelectSingleNode("ShapeModelAndImageLength").Attributes["ShapeModelAndImageLength"].InnerText); i++)
                {
                    if (i == 0)
                    {
                        partFileName = ShapeModelAndImageRootNode.SelectSingleNode("ShapeModel" + i.ToString()).Attributes["ShapeModel" + i.ToString()].InnerText;
                        fullFileName = mFilename + "//" + partFileName;
                        HOperatorSet.ReadShapeModel((HTuple)fullFileName, out UserCode.GetInstance().gProCd[idx].gSSP.shapeModel);

                        partFileName = ShapeModelAndImageRootNode.SelectSingleNode("ShapeModelImage" + i.ToString()).Attributes["ShapeModelImage" + i.ToString()].InnerText;
                        fullFileName = mFilename + "//" + partFileName;
                        HOperatorSet.ReadImage(out UserCode.GetInstance().gProCd[idx].gSSP.shapeModelImage, (HTuple)fullFileName);
                    }
                    else
                    {
                        HTuple tmpHt;
                        partFileName = ShapeModelAndImageRootNode.SelectSingleNode("ShapeModel" + i.ToString()).Attributes["ShapeModel" + i.ToString()].InnerText;
                        fullFileName = mFilename + "//" + partFileName;
                        HOperatorSet.ReadShapeModel((HTuple)fullFileName, out tmpHt);
                        UserCode.GetInstance().gProCd[idx].gSSP.shapeModel = UserCode.GetInstance().gProCd[idx].gSSP.shapeModel.TupleConcat(tmpHt);

                        HObject tmpHo;
                        partFileName = ShapeModelAndImageRootNode.SelectSingleNode("ShapeModelImage" + i.ToString()).Attributes["ShapeModelImage" + i.ToString()].InnerText;
                        fullFileName = mFilename + "//" + partFileName;
                        HOperatorSet.ReadImage(out tmpHo, (HTuple)fullFileName);
                        //UserCode.GetInstance().gProCd[idx].gSSP.shapeModelImage.ConcatObj(tmpHo);
                        HOperatorSet.ConcatObj(UserCode.GetInstance().gProCd[idx].gSSP.shapeModelImage, tmpHo, out UserCode.GetInstance().gProCd[idx].gSSP.shapeModelImage);
                    }
                }
                if (UserCode.GetInstance().gProCd[idx].gSSP.shapeModelRegion != null)
                {
                    UserCode.GetInstance().gProCd[idx].gSSP.shapeModelRegion.Dispose();
                    UserCode.GetInstance().gProCd[idx].gSSP.shapeModelRegion = null;
                }
                //Shape Model Region ChildNode
                partFileName = ProgramNode.SelectSingleNode("ShapeModelRegion").Attributes["ShapeModelRegion"].InnerText;
                fullFileName = mFilename + "//" + partFileName;
                HOperatorSet.ReadRegion(out UserCode.GetInstance().gProCd[idx].gSSP.shapeModelRegion, (HTuple)fullFileName);

                //Shape Model Point ChildNode
                UserCode.GetInstance().gProCd[idx].gSSP.shapeModelPoints.Clear();
                partFileName = ProgramNode.SelectSingleNode("ShapeModelPoint").Attributes["ShapeModelPoint"].InnerText;
                fullFileName = mFilename + "//" + partFileName;
                HTuple hv_FileHandle, pointX, pointY, hv_IsEOF;
                PointF modelPoint = new PointF(0, 0);
                HOperatorSet.OpenFile((HTuple)fullFileName, "input", out hv_FileHandle);
                for (int i = 0; i < int.Parse(ShapeModelAndImageRootNode.SelectSingleNode("ShapeModelAndImageLength").Attributes["ShapeModelAndImageLength"].InnerText); i++)
                {
                    HOperatorSet.FreadString(hv_FileHandle, out pointX, out hv_IsEOF);
                    HOperatorSet.FreadString(hv_FileHandle, out pointY, out hv_IsEOF);
                    modelPoint.X = float.Parse((string)pointX);
                    modelPoint.Y = float.Parse((string)pointY);
                    UserCode.GetInstance().gProCd[idx].gSSP.shapeModelPoints.Add(modelPoint);
                }
                HOperatorSet.CloseFile(hv_FileHandle);
            }
            

            //arrangeIndex ChildNode
            UserCode.GetInstance().gProCd[idx].gSSP.arrangeIndex = int.Parse(ProgramNode.SelectSingleNode("arrangeIndex").Attributes["arrangeIndex"].InnerText);

            //Max_Object_Num ChildNode
            UserCode.GetInstance().gProCd[idx].gSSP.Max_Object_Num = int.Parse(ProgramNode.SelectSingleNode("Max_Object_Num").Attributes["Max_Object_Num"].InnerText);

            //IsBorderShapeModelChecked ChildNode
            UserCode.GetInstance().gProCd[idx].gSSP.isBorderShapeModelChecked = bool.Parse(ProgramNode.SelectSingleNode("IsBorderShapeModelChecked").Attributes["IsBorderShapeModelChecked"].InnerText);

            //IsMultiplePara ChildNode
            UserCode.GetInstance().gProCd[idx].gSSP.isMultiplePara = bool.Parse(ProgramNode.SelectSingleNode("IsMultiplePara").Attributes["IsMultiplePara"].InnerText);

            //outPutStringList ChildNode
            UserCode.GetInstance().gProCd[idx].gSSP.outPutStringList.Clear();
            XmlNode outPutStringListRootNode = ProgramNode.SelectSingleNode("OutPutStringList");
            for (int i = 0; i < int.Parse(outPutStringListRootNode.SelectSingleNode("OutPutStringListLength").Attributes["OutPutStringListLength"].InnerText); i++)
            {
                UserCode.GetInstance().gProCd[idx].gSSP.outPutStringList.Add(outPutStringListRootNode.SelectSingleNode("OutPutStringList" + i.ToString()).Attributes["OutPutStringList" + i.ToString()].InnerText);
            }

            //showOutputResultStr ChildNode
            //   UserCode.GetInstance().gProCd[idx].gSSP.showOutputResultStr = null;
            XmlNode showOutputResultStrRootNode = ProgramNode.SelectSingleNode("ShowOutputResultStr");
            for (int i = 0; i < int.Parse(showOutputResultStrRootNode.SelectSingleNode("ShowOutputResultStrLength").Attributes["ShowOutputResultStrLength"].InnerText); i++)
            {
                UserCode.GetInstance().gProCd[idx].gSSP.showOutputResultStr[i] = showOutputResultStrRootNode.SelectSingleNode("ShowOutputResultStr" + i.ToString()).Attributes["ShowOutputResultStr" + i.ToString()].InnerText;
            }

            //showOutputResultFlag ChildNode
            //    UserCode.GetInstance().gProCd[idx].gSSP.showOutputResultFlag = null;
            XmlNode showOutputResultFlagRootNode = ProgramNode.SelectSingleNode("ShowOutputResultFlag");
            for (int i = 0; i < int.Parse(showOutputResultFlagRootNode.SelectSingleNode("ShowOutputResultFlagLength").Attributes["ShowOutputResultFlagLength"].InnerText); i++)
            {
                UserCode.GetInstance().gProCd[idx].gSSP.showOutputResultFlag[i] = bool.Parse(showOutputResultFlagRootNode.SelectSingleNode("ShowOutputResultFlag" + i.ToString()).Attributes["ShowOutputResultFlag" + i.ToString()].InnerText);
            }

            //findShapeModelBatchOperationStr ChildNode
            //   UserCode.GetInstance().gProCd[idx].gSSP.showOutputResultStr = null;
            XmlNode findShapeModelBatchOperationStrRootNode = ProgramNode.SelectSingleNode("findShapeModelBatchOperationStr");
            for (int i = 0; i < int.Parse(findShapeModelBatchOperationStrRootNode.SelectSingleNode("findShapeModelBatchOperationStrLength").Attributes["findShapeModelBatchOperationStrLength"].InnerText); i++)
            {
                UserCode.GetInstance().gProCd[idx].gSSP.findShapeModelBatchOperationStr[i] = findShapeModelBatchOperationStrRootNode.SelectSingleNode("findShapeModelBatchOperationStr" + i.ToString()).Attributes["findShapeModelBatchOperationStr" + i.ToString()].InnerText;
            }

            //findShapeModelBatchOperationFlag ChildNode
            //    UserCode.GetInstance().gProCd[idx].gSSP.showOutputResultFlag = null;
            XmlNode findShapeModelBatchOperationFlagRootNode = ProgramNode.SelectSingleNode("findShapeModelBatchOperationFlag");
            for (int i = 0; i < int.Parse(findShapeModelBatchOperationFlagRootNode.SelectSingleNode("findShapeModelBatchOperationFlagLength").Attributes["findShapeModelBatchOperationFlagLength"].InnerText); i++)
            {
                UserCode.GetInstance().gProCd[idx].gSSP.findShapeModelBatchOperationFlag[i] = bool.Parse(findShapeModelBatchOperationFlagRootNode.SelectSingleNode("findShapeModelBatchOperationFlag" + i.ToString()).Attributes["findShapeModelBatchOperationFlag" + i.ToString()].InnerText);
            }
            //modelIsChecked ChildNode
            //    UserCode.GetInstance().gProCd[idx].gSSP.modelIsChecked = null;
            XmlNode modelIsCheckedRootNode = ProgramNode.SelectSingleNode("ModelIsChecked");
            for (int i = 0; i < int.Parse(modelIsCheckedRootNode.SelectSingleNode("ModelIsCheckedLength").Attributes["ModelIsCheckedLength"].InnerText); i++)
            {
                UserCode.GetInstance().gProCd[idx].gSSP.modelIsChecked[i] = bool.Parse(modelIsCheckedRootNode.SelectSingleNode("ModelIsChecked" + i.ToString()).Attributes["ModelIsChecked" + i.ToString()].InnerText);
            }

            //AllShapeModelMultiplePara ChildNode
            XmlNode AllShapeModelMultipleParaRootNode = ProgramNode.SelectSingleNode("AllShapeModelMultiplePara");
            UserCode.GetInstance().gProCd[idx].gSSP.maxOverlap = double.Parse(AllShapeModelMultipleParaRootNode.SelectSingleNode("MaxOverlap").Attributes["MaxOverlap"].InnerText);

            for (int i = 0; i < int.Parse(AllShapeModelMultipleParaRootNode.SelectSingleNode("AllShapeModelMultipleParaLength").Attributes["AllShapeModelMultipleParaLength"].InnerText); i++)
            {
                UserCode.GetInstance().gProCd[idx].gSSP.angleStart[i] = double.Parse(AllShapeModelMultipleParaRootNode.SelectSingleNode("AngleStart").SelectSingleNode("AngleStart" + i.ToString()).Attributes["AngleStart" + i.ToString()].InnerText);
                UserCode.GetInstance().gProCd[idx].gSSP.angleExtent[i] = double.Parse(AllShapeModelMultipleParaRootNode.SelectSingleNode("AngleExtent").SelectSingleNode("AngleExtent" + i.ToString()).Attributes["AngleExtent" + i.ToString()].InnerText);
                UserCode.GetInstance().gProCd[idx].gSSP.numMatches[i] = int.Parse(AllShapeModelMultipleParaRootNode.SelectSingleNode("NumMatches").SelectSingleNode("NumMatches" + i.ToString()).Attributes["NumMatches" + i.ToString()].InnerText);
                UserCode.GetInstance().gProCd[idx].gSSP.minScore[i] = double.Parse(AllShapeModelMultipleParaRootNode.SelectSingleNode("MinScore").SelectSingleNode("MinScore" + i.ToString()).Attributes["MinScore" + i.ToString()].InnerText);
                UserCode.GetInstance().gProCd[idx].gSSP.subPixel[2 * i] = int.Parse(AllShapeModelMultipleParaRootNode.SelectSingleNode("SubPixel").SelectSingleNode("SubPixel" + (i * 2).ToString()).Attributes["SubPixel" + (i * 2).ToString()].InnerText);
                UserCode.GetInstance().gProCd[idx].gSSP.subPixel[2 * i + 1] = int.Parse(AllShapeModelMultipleParaRootNode.SelectSingleNode("SubPixel").SelectSingleNode("SubPixel" + (i * 2 + 1).ToString()).Attributes["SubPixel" + (i * 2 + 1).ToString()].InnerText);
                UserCode.GetInstance().gProCd[idx].gSSP.numLevels[2 * i] = int.Parse(AllShapeModelMultipleParaRootNode.SelectSingleNode("NumLevels").SelectSingleNode("NumLevels" + (i * 2).ToString()).Attributes["NumLevels" + (i * 2).ToString()].InnerText);
                UserCode.GetInstance().gProCd[idx].gSSP.numLevels[2 * i + 1] = int.Parse(AllShapeModelMultipleParaRootNode.SelectSingleNode("NumLevels").SelectSingleNode("NumLevels" + (i * 2 + 1).ToString()).Attributes["NumLevels" + (i * 2 + 1).ToString()].InnerText);
                UserCode.GetInstance().gProCd[idx].gSSP.greediness[i] = double.Parse(AllShapeModelMultipleParaRootNode.SelectSingleNode("Greediness").SelectSingleNode("Greediness" + i.ToString()).Attributes["Greediness" + i.ToString()].InnerText);

            }
        }
        private void LoadAnisoShapeSearchFBDXML(int idx, XmlNode ProgramNode)
        {


            string fullFileName;
            string partFileName;
            UserCode.GetInstance().gProCd[idx].gASSP.shapeModel = null;
            if (UserCode.GetInstance().gProCd[idx].gASSP.shapeModelImage != null)
            {
                UserCode.GetInstance().gProCd[idx].gASSP.shapeModelImage.Dispose();
                UserCode.GetInstance().gProCd[idx].gASSP.shapeModelImage = null;
            }
            //Shape Model And Image ChildNode
            bool isShapeModelAndImageOK = true;
            XmlNode ShapeModelAndImageRootNode = ProgramNode.SelectSingleNode("AnisoShapeModelAndImage");
            try
            {
                int test = int.Parse(ShapeModelAndImageRootNode.SelectSingleNode("AnisoShapeModelAndImageLength").Attributes["AnisoShapeModelAndImageLength"].InnerText);
            }
            catch (System.Exception ex)
            {
                isShapeModelAndImageOK = false;
            }
            if (isShapeModelAndImageOK)
            {
                for (int i = 0; i < int.Parse(ShapeModelAndImageRootNode.SelectSingleNode("AnisoShapeModelAndImageLength").Attributes["AnisoShapeModelAndImageLength"].InnerText); i++)
                {
                    if (i == 0)
                    {
                        partFileName = ShapeModelAndImageRootNode.SelectSingleNode("AnisoShapeModel" + i.ToString()).Attributes["AnisoShapeModel" + i.ToString()].InnerText;
                        fullFileName = mFilename + "//" + partFileName;
                        HOperatorSet.ReadShapeModel((HTuple)fullFileName, out UserCode.GetInstance().gProCd[idx].gASSP.shapeModel);

                        partFileName = ShapeModelAndImageRootNode.SelectSingleNode("AnisoShapeModelImage" + i.ToString()).Attributes["AnisoShapeModelImage" + i.ToString()].InnerText;
                        fullFileName = mFilename + "//" + partFileName;
                        HOperatorSet.ReadImage(out UserCode.GetInstance().gProCd[idx].gASSP.shapeModelImage, (HTuple)fullFileName);
                    }
                    else
                    {
                        HTuple tmpHt;
                        partFileName = ShapeModelAndImageRootNode.SelectSingleNode("AnisoShapeModel" + i.ToString()).Attributes["AnisoShapeModel" + i.ToString()].InnerText;
                        fullFileName = mFilename + "//" + partFileName;
                        HOperatorSet.ReadShapeModel((HTuple)fullFileName, out tmpHt);
                        UserCode.GetInstance().gProCd[idx].gASSP.shapeModel.TupleConcat(tmpHt);

                        HObject tmpHo;
                        partFileName = ShapeModelAndImageRootNode.SelectSingleNode("AnisoShapeModelImage" + i.ToString()).Attributes["AnisoShapeModelImage" + i.ToString()].InnerText;
                        fullFileName = mFilename + "//" + partFileName;
                        HOperatorSet.ReadImage(out tmpHo, (HTuple)fullFileName);
                        UserCode.GetInstance().gProCd[idx].gASSP.shapeModelImage.ConcatObj(tmpHo);
                    }
                }
                //Shape Model Region ChildNode
                if (UserCode.GetInstance().gProCd[idx].gASSP.shapeModelRegion != null)
                {
                    UserCode.GetInstance().gProCd[idx].gASSP.shapeModelRegion.Dispose();
                    UserCode.GetInstance().gProCd[idx].gASSP.shapeModelRegion = null;
                }
                partFileName = ProgramNode.SelectSingleNode("AnisoShapeModelRegion").Attributes["AnisoShapeModelRegion"].InnerText;
                fullFileName = mFilename + "//" + partFileName;
                HOperatorSet.ReadRegion(out UserCode.GetInstance().gProCd[idx].gASSP.shapeModelRegion, (HTuple)fullFileName);

                //Shape Model Point ChildNode
                UserCode.GetInstance().gProCd[idx].gASSP.shapeModelPoints.Clear();
                partFileName = ProgramNode.SelectSingleNode("AnisoShapeModelPoint").Attributes["AnisoShapeModelPoint"].InnerText;
                fullFileName = mFilename + "//" + partFileName;
                HTuple hv_FileHandle, pointX, pointY, hv_IsEOF;
                PointF modelPoint = new PointF(0, 0);
                HOperatorSet.OpenFile((HTuple)fullFileName, "input", out hv_FileHandle);
                for (int i = 0; i < int.Parse(ShapeModelAndImageRootNode.SelectSingleNode("AnisoShapeModelAndImageLength").Attributes["AnisoShapeModelAndImageLength"].InnerText); i++)
                {
                    HOperatorSet.FreadString(hv_FileHandle, out pointX, out hv_IsEOF);
                    HOperatorSet.FreadString(hv_FileHandle, out pointY, out hv_IsEOF);
                    modelPoint.X = float.Parse((string)pointX);
                    modelPoint.Y = float.Parse((string)pointY);
                    UserCode.GetInstance().gProCd[idx].gASSP.shapeModelPoints.Add(modelPoint);
                }
                HOperatorSet.CloseFile(hv_FileHandle);
            }
            

            //arrangeIndex ChildNode
            UserCode.GetInstance().gProCd[idx].gASSP.arrangeIndex = int.Parse(ProgramNode.SelectSingleNode("arrangeIndex").Attributes["arrangeIndex"].InnerText);

            //Max_Object_Num ChildNode
            UserCode.GetInstance().gProCd[idx].gASSP.Max_Object_Num = int.Parse(ProgramNode.SelectSingleNode("Max_Object_Num").Attributes["Max_Object_Num"].InnerText);

            //IsBorderShapeModelChecked ChildNode
            UserCode.GetInstance().gProCd[idx].gASSP.isBorderShapeModelChecked = bool.Parse(ProgramNode.SelectSingleNode("IsBorderShapeModelChecked").Attributes["IsBorderShapeModelChecked"].InnerText);

            //IsMultiplePara ChildNode
            UserCode.GetInstance().gProCd[idx].gASSP.isMultiplePara = bool.Parse(ProgramNode.SelectSingleNode("IsMultiplePara").Attributes["IsMultiplePara"].InnerText);

            //outPutStringList ChildNode
            UserCode.GetInstance().gProCd[idx].gASSP.outPutStringList.Clear();
            XmlNode outPutStringListRootNode = ProgramNode.SelectSingleNode("OutPutStringList");
            for (int i = 0; i < int.Parse(outPutStringListRootNode.SelectSingleNode("OutPutStringListLength").Attributes["OutPutStringListLength"].InnerText); i++)
            {
                UserCode.GetInstance().gProCd[idx].gASSP.outPutStringList.Add(outPutStringListRootNode.SelectSingleNode("OutPutStringList" + i.ToString()).Attributes["OutPutStringList" + i.ToString()].InnerText);
            }

            //showOutputResultStr ChildNode
            XmlNode showOutputResultStrRootNode = ProgramNode.SelectSingleNode("ShowOutputResultStr");
            for (int i = 0; i < int.Parse(showOutputResultStrRootNode.SelectSingleNode("ShowOutputResultStrLength").Attributes["ShowOutputResultStrLength"].InnerText); i++)
            {
                UserCode.GetInstance().gProCd[idx].gASSP.showOutputResultStr[i] = showOutputResultStrRootNode.SelectSingleNode("ShowOutputResultStr" + i.ToString()).Attributes["ShowOutputResultStr" + i.ToString()].InnerText;
            }

            //showOutputResultFlag ChildNode
            XmlNode showOutputResultFlagRootNode = ProgramNode.SelectSingleNode("ShowOutputResultFlag");
            for (int i = 0; i < int.Parse(showOutputResultFlagRootNode.SelectSingleNode("ShowOutputResultFlagLength").Attributes["ShowOutputResultFlagLength"].InnerText); i++)
            {
                UserCode.GetInstance().gProCd[idx].gASSP.showOutputResultFlag[i] = bool.Parse(showOutputResultFlagRootNode.SelectSingleNode("ShowOutputResultFlag" + i.ToString()).Attributes["ShowOutputResultFlag" + i.ToString()].InnerText);
            }
            //findAnisoShapeModelBatchOperationStr ChildNode
            XmlNode findAnisoShapeModelBatchOperationStrRootNode = ProgramNode.SelectSingleNode("findAnisoShapeModelBatchOperationStr");
            for (int i = 0; i < int.Parse(findAnisoShapeModelBatchOperationStrRootNode.SelectSingleNode("findAnisoShapeModelBatchOperationStrLength").Attributes["findAnisoShapeModelBatchOperationStrLength"].InnerText); i++)
            {
                UserCode.GetInstance().gProCd[idx].gASSP.findAnisoShapeModelBatchOperationStr[i] = findAnisoShapeModelBatchOperationStrRootNode.SelectSingleNode("findAnisoShapeModelBatchOperationStr" + i.ToString()).Attributes["findAnisoShapeModelBatchOperationStr" + i.ToString()].InnerText;
            }

            //findAnisoShapeModelBatchOperationFlag ChildNode
            XmlNode findAnisoShapeModelBatchOperationFlagRootNode = ProgramNode.SelectSingleNode("findAnisoShapeModelBatchOperationFlag");
            for (int i = 0; i < int.Parse(findAnisoShapeModelBatchOperationFlagRootNode.SelectSingleNode("findAnisoShapeModelBatchOperationFlagLength").Attributes["findAnisoShapeModelBatchOperationFlagLength"].InnerText); i++)
            {
                UserCode.GetInstance().gProCd[idx].gASSP.findAnisoShapeModelBatchOperationFlag[i] = bool.Parse(findAnisoShapeModelBatchOperationFlagRootNode.SelectSingleNode("findAnisoShapeModelBatchOperationFlag" + i.ToString()).Attributes["findAnisoShapeModelBatchOperationFlag" + i.ToString()].InnerText);
            }
            //modelIsChecked ChildNode
            XmlNode modelIsCheckedRootNode = ProgramNode.SelectSingleNode("ModelIsChecked");
            for (int i = 0; i < int.Parse(modelIsCheckedRootNode.SelectSingleNode("ModelIsCheckedLength").Attributes["ModelIsCheckedLength"].InnerText); i++)
            {
                UserCode.GetInstance().gProCd[idx].gASSP.modelIsChecked[i] = bool.Parse(modelIsCheckedRootNode.SelectSingleNode("ModelIsChecked" + i.ToString()).Attributes["ModelIsChecked" + i.ToString()].InnerText);
            }

            //AllShapeModelMultiplePara ChildNode
            XmlNode AllShapeModelMultipleParaRootNode = ProgramNode.SelectSingleNode("AllShapeModelMultiplePara");
            UserCode.GetInstance().gProCd[idx].gASSP.maxOverlap = double.Parse(AllShapeModelMultipleParaRootNode.SelectSingleNode("MaxOverlap").Attributes["MaxOverlap"].InnerText);

            for (int i = 0; i < int.Parse(AllShapeModelMultipleParaRootNode.SelectSingleNode("AllShapeModelMultipleParaLength").Attributes["AllShapeModelMultipleParaLength"].InnerText); i++)
            {
                UserCode.GetInstance().gProCd[idx].gASSP.angleStart[i] = double.Parse(AllShapeModelMultipleParaRootNode.SelectSingleNode("AngleStart").SelectSingleNode("AngleStart" + i.ToString()).Attributes["AngleStart" + i.ToString()].InnerText);
                UserCode.GetInstance().gProCd[idx].gASSP.angleExtent[i] = double.Parse(AllShapeModelMultipleParaRootNode.SelectSingleNode("AngleExtent").SelectSingleNode("AngleExtent" + i.ToString()).Attributes["AngleExtent" + i.ToString()].InnerText);
                UserCode.GetInstance().gProCd[idx].gASSP.numMatches[i] = int.Parse(AllShapeModelMultipleParaRootNode.SelectSingleNode("NumMatches").SelectSingleNode("NumMatches" + i.ToString()).Attributes["NumMatches" + i.ToString()].InnerText);
                UserCode.GetInstance().gProCd[idx].gASSP.minScore[i] = double.Parse(AllShapeModelMultipleParaRootNode.SelectSingleNode("MinScore").SelectSingleNode("MinScore" + i.ToString()).Attributes["MinScore" + i.ToString()].InnerText);
                UserCode.GetInstance().gProCd[idx].gASSP.subPixel[2 * i] = int.Parse(AllShapeModelMultipleParaRootNode.SelectSingleNode("SubPixel").SelectSingleNode("SubPixel" + (i * 2).ToString()).Attributes["SubPixel" + (i * 2).ToString()].InnerText);
                UserCode.GetInstance().gProCd[idx].gASSP.subPixel[2 * i + 1] = int.Parse(AllShapeModelMultipleParaRootNode.SelectSingleNode("SubPixel").SelectSingleNode("SubPixel" + (i * 2 + 1).ToString()).Attributes["SubPixel" + (i * 2 + 1).ToString()].InnerText);
                UserCode.GetInstance().gProCd[idx].gASSP.numLevels[2 * i] = int.Parse(AllShapeModelMultipleParaRootNode.SelectSingleNode("NumLevels").SelectSingleNode("NumLevels" + (i * 2).ToString()).Attributes["NumLevels" + (i * 2).ToString()].InnerText);
                UserCode.GetInstance().gProCd[idx].gASSP.numLevels[2 * i + 1] = int.Parse(AllShapeModelMultipleParaRootNode.SelectSingleNode("NumLevels").SelectSingleNode("NumLevels" + (i * 2 + 1).ToString()).Attributes["NumLevels" + (i * 2 + 1).ToString()].InnerText);
                UserCode.GetInstance().gProCd[idx].gASSP.greediness[i] = double.Parse(AllShapeModelMultipleParaRootNode.SelectSingleNode("Greediness").SelectSingleNode("Greediness" + i.ToString()).Attributes["Greediness" + i.ToString()].InnerText);
                UserCode.GetInstance().gProCd[idx].gASSP.scaleRMin[i] = double.Parse(AllShapeModelMultipleParaRootNode.SelectSingleNode("ScaleRMin").SelectSingleNode("ScaleRMin" + i.ToString()).Attributes["ScaleRMin" + i.ToString()].InnerText);
                UserCode.GetInstance().gProCd[idx].gASSP.scaleRMax[i] = double.Parse(AllShapeModelMultipleParaRootNode.SelectSingleNode("ScaleRMax").SelectSingleNode("ScaleRMax" + i.ToString()).Attributes["ScaleRMax" + i.ToString()].InnerText);
                UserCode.GetInstance().gProCd[idx].gASSP.scaleCMin[i] = double.Parse(AllShapeModelMultipleParaRootNode.SelectSingleNode("ScaleCMin").SelectSingleNode("ScaleCMin" + i.ToString()).Attributes["ScaleCMin" + i.ToString()].InnerText);
                UserCode.GetInstance().gProCd[idx].gASSP.scaleCMax[i] = double.Parse(AllShapeModelMultipleParaRootNode.SelectSingleNode("ScaleCMax").SelectSingleNode("ScaleCMax" + i.ToString()).Attributes["ScaleCMax" + i.ToString()].InnerText);

            }


        }
        private void LoadSerialOutputFBDXML(int idx, XmlNode ProgramNode)
        {

            //IsGige childNode
            UserCode.GetInstance().gProCd[idx].gSOP.isGige = bool.Parse(ProgramNode.SelectSingleNode("IsGige").Attributes["IsGige"].InnerText);
            //OutputForm childNode
            UserCode.GetInstance().gProCd[idx].gSOP.outputForm = bool.Parse(ProgramNode.SelectSingleNode("OutputForm").Attributes["OutputForm"].InnerText);
            //IntBit childNode
            UserCode.GetInstance().gProCd[idx].gSOP.intBit = short.Parse(ProgramNode.SelectSingleNode("IntBit").Attributes["IntBit"].InnerText);
            //FloatBit childNode
            UserCode.GetInstance().gProCd[idx].gSOP.floatBit = short.Parse(ProgramNode.SelectSingleNode("FloatBit").Attributes["FloatBit"].InnerText);
            //NegativeMinus childNode
            UserCode.GetInstance().gProCd[idx].gSOP.NegativeMinus = bool.Parse(ProgramNode.SelectSingleNode("NegativeMinus").Attributes["NegativeMinus"].InnerText);
            //EraseZeroYes childNode
            UserCode.GetInstance().gProCd[idx].gSOP.EraseZeroYes = bool.Parse(ProgramNode.SelectSingleNode("EraseZeroYes").Attributes["EraseZeroYes"].InnerText);
            //FieldSeparator childNode
            UserCode.GetInstance().gProCd[idx].gSOP.FieldSeparator = short.Parse(ProgramNode.SelectSingleNode("FieldSeparator").Attributes["FieldSeparator"].InnerText);
            //RecordSeparator childNode
            UserCode.GetInstance().gProCd[idx].gSOP.RecordSeparator = short.Parse(ProgramNode.SelectSingleNode("RecordSeparator").Attributes["RecordSeparator"].InnerText);
            //sendDataInfoList childNode
            UserCode.GetInstance().gProCd[idx].gSOP.sendDataInfoList.Clear();
            XmlNode sendDataInfoListRootNode = ProgramNode.SelectSingleNode("sendDataInfoList");
            for (int i = 0; i < int.Parse(sendDataInfoListRootNode.SelectSingleNode("sendDataInfoListLength").Attributes["sendDataInfoListLength"].InnerText); i++)
            {
                UserCode.GetInstance().gProCd[idx].gSOP.addSendDataInfoListPara(
                    int.Parse(sendDataInfoListRootNode.SelectSingleNode("sendDataInfoListRow").SelectSingleNode("sendDataInfoListRow" + i.ToString()).Attributes["sendDataInfoListRow" + i.ToString()].InnerText),
                    int.Parse(sendDataInfoListRootNode.SelectSingleNode("sendDataInfoListFuncID").SelectSingleNode("sendDataInfoListFuncID" + i.ToString()).Attributes["sendDataInfoListFuncID" + i.ToString()].InnerText),
                    int.Parse(sendDataInfoListRootNode.SelectSingleNode("sendDataInfoListDatatype").SelectSingleNode("sendDataInfoListDatatype" + i.ToString()).Attributes["sendDataInfoListDatatype" + i.ToString()].InnerText),
                    int.Parse(sendDataInfoListRootNode.SelectSingleNode("sendDataInfoListIdx").SelectSingleNode("sendDataInfoListIdx" + i.ToString()).Attributes["sendDataInfoListIdx" + i.ToString()].InnerText));

            }


        }

        #region OldSaveCodeByEXCEL
        /*
        public bool SaveProjToExcel(String tPath, String tFilename)
        {
            gExcelEdit.mFilename = tPath;
            gExcelEdit.Create();
            gMainSheet = gExcelEdit.AddSheet("Program");

            int tmpShapeModelIdx = 0, tmpAnsioShapeModelIdx = 0;
            //gExcelEdit.SetCellValue(gMainSheet, 3, 4, UserCode.GetInstance().gProCd[0].FuncID);

            for (int i = 0; i < 20; i++)
            {
                switch (UserCode.GetInstance().gProCd[i].FuncID)
                {
                    case ProCodeCls.MainFunction.NullFBD:
                        SaveNullFBD(i);
                        break;
                    case ProCodeCls.MainFunction.InputCameraInputFBD:
                        SaveCameraInputFBD(i);
                        break;
                    case ProCodeCls.MainFunction.CalibrationThresholdFBD:
                        SaveThresholdFBD(i);
                        break;
                    case ProCodeCls.MainFunction.CalibrationMedianFilterFBD:
                        SaveMedianFilterFBD(i);
                        break;
                    case ProCodeCls.MainFunction.CalibrationBackgroundRemoveFBD:
                        SaveBackgroundRemoveFBD(i);
                        break;
                    case ProCodeCls.MainFunction.MeasureShapeSearchFBD:
                        SaveShapeSearchFBD(i, tmpShapeModelIdx);
                        tmpShapeModelIdx++;
                        break;
                    case ProCodeCls.MainFunction.MeasureAnisoShapeSearchFBD:
                        SaveAnisoShapeSearchFBD(i, tmpAnsioShapeModelIdx);
                        tmpAnsioShapeModelIdx++;
                        break;
                    case ProCodeCls.MainFunction.OutputSerialOutputFBD:
                        SaveSerialOutputFBD(i);
                        break;
                }
            }

            gExcelEdit.Save();
            gExcelEdit.Close();

            String tmpPath = tPath.Substring(0, tPath.IndexOf(tFilename, 0));
            String tmpFileName = tFilename.Substring(0, tFilename.IndexOf("."));
            tmpPath = tmpPath + tmpFileName + ".xml";

            ConfigInformation.GetInstance().tCfgInfoOprt.SaveCfgInfo(tmpPath);
            return true;

        }
        private void SaveNullFBD(int idx)
        {
            SetCellValuePriv(1, idx + 1, UserCode.GetInstance().codeInfoValToKey[UserCode.GetInstance().gProCd[idx].FuncID]);
        }
        private void SaveCameraInputFBD(int idx)
        {
            SetCellValuePriv(1, idx + 1, UserCode.GetInstance().codeInfoValToKey[UserCode.GetInstance().gProCd[idx].FuncID]);
        }
        private void SaveThresholdFBD(int idx)
        {
            SetCellValuePriv(1, idx + 1, UserCode.GetInstance().codeInfoValToKey[UserCode.GetInstance().gProCd[idx].FuncID]);
            SetCellValuePriv(2, idx + 1, UserCode.GetInstance().gProCd[idx].floatData[0]);      //max gray
            SetCellValuePriv(3, idx + 1, UserCode.GetInstance().gProCd[idx].floatData[1]);      //max gray
        }
        private void SaveMedianFilterFBD(int idx)
        {
            SetCellValuePriv(1, idx + 1, UserCode.GetInstance().codeInfoValToKey[UserCode.GetInstance().gProCd[idx].FuncID]);
            SetCellValuePriv(2, idx + 1, UserCode.GetInstance().gProCd[idx].intData[0]);
        }
        private void SaveBackgroundRemoveFBD(int idx)
        {
            SetCellValuePriv(1, idx + 1, UserCode.GetInstance().codeInfoValToKey[UserCode.GetInstance().gProCd[idx].FuncID]);
            SetCellValuePriv(2, idx + 1, UserCode.GetInstance().gProCd[idx].boolData[0]);
            for (int i = 0; i < 8; i++)
            {
                SetCellValuePriv(i + 3, idx + 1, UserCode.GetInstance().gProCd[idx].floatData[i]);
            }
        }
        private void SaveShapeSearchFBD(int idx , int tMidx)
        {
            //  1
            SetCellValuePriv(1, idx + 1, UserCode.GetInstance().codeInfoValToKey[UserCode.GetInstance().gProCd[idx].FuncID]);
            //  2
            SetCellValuePriv(2, idx + 1, UserCode.GetInstance().gProCd[idx].gSSP.shapeModel.TupleLength());
            string fullFileName;

            for (int i = 0; i < UserCode.GetInstance().gProCd[idx].gSSP.shapeModel.TupleLength(); i++)
            {
                fullFileName = gExcelEdit.mFilename + "Model " + tMidx.ToString() + " " + i.ToString() + ".shm";
                HOperatorSet.WriteShapeModel(UserCode.GetInstance().gProCd[idx].gSSP.shapeModel[i], (HTuple)fullFileName);
                SetCellValuePriv(3 + 2 * i, idx + 1, fullFileName);

                fullFileName = gExcelEdit.mFilename + "Pic " + tMidx.ToString() + " " + i.ToString() + ".bmp";
                HOperatorSet.WriteImage(UserCode.GetInstance().gProCd[idx].gSSP.shapeModelImage[i + 1], "bmp", 0, fullFileName);
                SetCellValuePriv(4 + 2 * i, idx + 1, fullFileName);
            }

            fullFileName = gExcelEdit.mFilename + "Region " + tMidx.ToString() + ".reg";
            HOperatorSet.WriteRegion(UserCode.GetInstance().gProCd[idx].gSSP.shapeModelRegion, fullFileName);
            SetCellValuePriv(5 + 2 * (UserCode.GetInstance().gProCd[idx].gSSP.shapeModel.TupleLength() - 1), idx + 1, fullFileName);

            fullFileName = gExcelEdit.mFilename + "Points " + tMidx.ToString() + ".dat";
            HTuple hv_FileHandle;
            HOperatorSet.OpenFile(fullFileName, "output", out hv_FileHandle);

            for (int i = 0; i < UserCode.GetInstance().gProCd[idx].gSSP.shapeModel.TupleLength(); i++ )
            {
                HOperatorSet.FwriteString(hv_FileHandle, UserCode.GetInstance().gProCd[idx].gSSP.shapeModelPoints[i].X + "\n");
                HOperatorSet.FwriteString(hv_FileHandle, UserCode.GetInstance().gProCd[idx].gSSP.shapeModelPoints[i].Y + "\n");
            }
            HOperatorSet.CloseFile(hv_FileHandle);
            SetCellValuePriv(6 + 2 * (UserCode.GetInstance().gProCd[idx].gSSP.shapeModel.TupleLength() - 1), idx + 1, fullFileName);

*/
        /*         
                    {
                        SetCellValuePriv(2, idx + 1, fullFileName);
                        //图像信息
                
                        SetCellValuePriv(3, idx + 1, fullFileName);
                        //region 信息
                        fullFileName = gExcelEdit.mFilename + "Point " + tMidx.ToString() + ".shm";
                        HOperatorSet.WriteRegion(UserCode.GetInstance().gProCd[idx].gSSP.shapeModelRegion, fullFileName);
                        SetCellValuePriv(4, idx + 1, fullFileName);
                        //点信息

                        for (int i = 0; i < UserCode.GetInstance().gProCd[idx].gSSP.shapeModel.TupleLength(); i++)
                        {
                            string fullFileName = gExcelEdit.mFilename + "//Model" +  + tMidx.ToString() + ".shm";
                            HOperatorSet.WriteShapeModel(UserCode.GetInstance().gProCd[idx].gSSP.shapeModel[i], (HTuple)fullFileName);

                            fullFileName = gExcelEdit.mFilename + "Image " + tMidx.ToString() + ".bmp";
                            HOperatorSet.WriteImage(UserCode.GetInstance().gProCd[idx].gSSP.shapeModelImage[i], "bmp", 0, fullFileName);
                        }
                        HOperatorSet.WriteImage(imageTempShow, "bmp", 0, modelFileSavePath + "//模版图像" + System.String.Format("{0:D5}", i + 1) + ".bmp");
                
                        HTuple hv_FileHandle;
                        HOperatorSet.OpenFile(modelFileSavePath + "//模版文件" + System.String.Format("{0:D5}", i + 1) + ".dat", "output", out hv_FileHandle);
                        HOperatorSet.FwriteString(hv_FileHandle, modelPoints[i].X + "\n");
                        HOperatorSet.FwriteString(hv_FileHandle, modelPoints[i].Y + "\n");
                        HOperatorSet.CloseFile(hv_FileHandle);
                        HOperatorSet.WriteRegion(templateModelRegions[i + 1], modelFileSavePath + "//模版文件" + System.String.Format("{0:D5}", i + 1) + ".reg");

                    }
        */
        /*
            //  3
            SetCellValuePriv(7 + +2 * (UserCode.GetInstance().gProCd[idx].gSSP.shapeModel.TupleLength() - 1)
                , idx + 1, UserCode.GetInstance().gProCd[idx].boolData[0]);
            //  4
            SetCellValuePriv(8 + +2 * (UserCode.GetInstance().gProCd[idx].gSSP.shapeModel.TupleLength() - 1)
                , idx + 1, UserCode.GetInstance().gProCd[idx].boolData[1]);
            //  5
            SetCellValuePriv(9 + +2 * (UserCode.GetInstance().gProCd[idx].gSSP.shapeModel.TupleLength() - 1)
                , idx + 1, UserCode.GetInstance().gProCd[idx].boolData[2]);
            //  6
            for (int i=0; i<ProCodeCls.ShapeSearchPara.MAX_SHAPE_MODEL; i++)
            {
                if (UserCode.GetInstance().gProCd[idx].shortData[i] == 1)
                {
                    SetCellValuePriv(10 + i + 2 * (UserCode.GetInstance().gProCd[idx].gSSP.shapeModel.TupleLength() - 1), idx + 1, "TRUE");
                }
                else
                {
                    SetCellValuePriv(10 + i + 2 * (UserCode.GetInstance().gProCd[idx].gSSP.shapeModel.TupleLength() - 1), idx + 1, "FALSE");
                }
            }
            //  7
            //^*^*^*save model image here
            //  8
            if (UserCode.GetInstance().gProCd[idx].boolData[2])                 //多模板
            {
                if (UserCode.GetInstance().gProCd[idx].boolData[1])             //为每套模板创建参数
                {
                    for (int i = 0; i < UserCode.GetInstance().gProCd[idx].gSSP.shapeModel.TupleLength(); i++)
                    {
                        SetCellValuePriv(27 + i * 10 + 2 * (UserCode.GetInstance().gProCd[idx].gSSP.shapeModel.TupleLength() - 1)
                            , idx + 1, UserCode.GetInstance().gProCd[idx].gSSP.angleStart[i]);
                        SetCellValuePriv(28 + i * 10 + 2 * (UserCode.GetInstance().gProCd[idx].gSSP.shapeModel.TupleLength() - 1)
                            , idx + 1, UserCode.GetInstance().gProCd[idx].gSSP.angleExtent[i]);
                        SetCellValuePriv(29 + i * 10 + 2 * (UserCode.GetInstance().gProCd[idx].gSSP.shapeModel.TupleLength() - 1)
                            , idx + 1, UserCode.GetInstance().gProCd[idx].gSSP.minScore[i]);
                        SetCellValuePriv(30 + i * 10 + 2 * (UserCode.GetInstance().gProCd[idx].gSSP.shapeModel.TupleLength() - 1)
                            , idx + 1, UserCode.GetInstance().gProCd[idx].gSSP.numMatches[i]);
                        SetCellValuePriv(31 + i * 10 + 2 * (UserCode.GetInstance().gProCd[idx].gSSP.shapeModel.TupleLength() - 1)
                            , idx + 1, UserCode.GetInstance().gProCd[idx].gSSP.maxOverlap);
                        SetCellValuePriv(32 + i * 10 + 2 * (UserCode.GetInstance().gProCd[idx].gSSP.shapeModel.TupleLength() - 1)
                            , idx + 1, UserCode.GetInstance().gProCd[idx].gSSP.subPixel[2 * i]);
                        SetCellValuePriv(33 + i * 10 + 2 * (UserCode.GetInstance().gProCd[idx].gSSP.shapeModel.TupleLength() - 1)
                            , idx + 1, UserCode.GetInstance().gProCd[idx].gSSP.subPixel[2 * i + 1]);
                        SetCellValuePriv(34 + i * 10 + 2 * (UserCode.GetInstance().gProCd[idx].gSSP.shapeModel.TupleLength() - 1)
                            , idx + 1, UserCode.GetInstance().gProCd[idx].gSSP.numLevels[2 * i]);
                        SetCellValuePriv(35 + i * 10 + 2 * (UserCode.GetInstance().gProCd[idx].gSSP.shapeModel.TupleLength() - 1)
                            , idx + 1, UserCode.GetInstance().gProCd[idx].gSSP.numLevels[2 * i + 1]);
                        SetCellValuePriv(36 + i * 10 + 2 * (UserCode.GetInstance().gProCd[idx].gSSP.shapeModel.TupleLength() - 1)
                            , idx + 1, UserCode.GetInstance().gProCd[idx].gSSP.greediness[i]);
                    }
                }
                else                                                            //仅使用一套参数
                {
                    SetCellValuePriv(27 + 2 * (UserCode.GetInstance().gProCd[idx].gSSP.shapeModel.TupleLength() - 1)
                        , idx + 1, UserCode.GetInstance().gProCd[idx].gSSP.angleStart[ProCodeCls.ShapeSearchPara.MAX_SHAPE_MUL_PARA_NUM - 1]);
                    SetCellValuePriv(28 + 2 * (UserCode.GetInstance().gProCd[idx].gSSP.shapeModel.TupleLength() - 1)
                        , idx + 1, UserCode.GetInstance().gProCd[idx].gSSP.angleExtent[ProCodeCls.ShapeSearchPara.MAX_SHAPE_MUL_PARA_NUM - 1]);
                    SetCellValuePriv(29 + 2 * (UserCode.GetInstance().gProCd[idx].gSSP.shapeModel.TupleLength() - 1)
                        , idx + 1, UserCode.GetInstance().gProCd[idx].gSSP.minScore[ProCodeCls.ShapeSearchPara.MAX_SHAPE_MUL_PARA_NUM - 1]);
                    SetCellValuePriv(30 + 2 * (UserCode.GetInstance().gProCd[idx].gSSP.shapeModel.TupleLength() - 1)
                        , idx + 1, UserCode.GetInstance().gProCd[idx].gSSP.numMatches[ProCodeCls.ShapeSearchPara.MAX_SHAPE_MUL_PARA_NUM - 1]);
                    SetCellValuePriv(31 + 2 * (UserCode.GetInstance().gProCd[idx].gSSP.shapeModel.TupleLength() - 1)
                        , idx + 1, UserCode.GetInstance().gProCd[idx].gSSP.maxOverlap);
                    SetCellValuePriv(32 + 2 * (UserCode.GetInstance().gProCd[idx].gSSP.shapeModel.TupleLength() - 1)
                        , idx + 1, UserCode.GetInstance().gProCd[idx].gSSP.subPixel[(ProCodeCls.ShapeSearchPara.MAX_SHAPE_MUL_PARA_NUM - 1) * 2]);
                    SetCellValuePriv(33 + 2 * (UserCode.GetInstance().gProCd[idx].gSSP.shapeModel.TupleLength() - 1)
                        , idx + 1, UserCode.GetInstance().gProCd[idx].gSSP.subPixel[(ProCodeCls.ShapeSearchPara.MAX_SHAPE_MUL_PARA_NUM - 1) * 2 + 1]);
                    SetCellValuePriv(34 + 2 * (UserCode.GetInstance().gProCd[idx].gSSP.shapeModel.TupleLength() - 1)
                        , idx + 1, UserCode.GetInstance().gProCd[idx].gSSP.numLevels[(ProCodeCls.ShapeSearchPara.MAX_SHAPE_MUL_PARA_NUM - 1) * 2]);
                    SetCellValuePriv(35 + 2 * (UserCode.GetInstance().gProCd[idx].gSSP.shapeModel.TupleLength() - 1)
                        , idx + 1, UserCode.GetInstance().gProCd[idx].gSSP.numLevels[(ProCodeCls.ShapeSearchPara.MAX_SHAPE_MUL_PARA_NUM - 1) * 2 + 1]);
                    SetCellValuePriv(36 + 2 * (UserCode.GetInstance().gProCd[idx].gSSP.shapeModel.TupleLength() - 1)
                        , idx + 1, UserCode.GetInstance().gProCd[idx].gSSP.greediness[ProCodeCls.ShapeSearchPara.MAX_SHAPE_MUL_PARA_NUM - 1]);
                }
            }
            else                                                                //单模板
            {
                SetCellValuePriv(27 + 2 * (UserCode.GetInstance().gProCd[idx].gSSP.shapeModel.TupleLength() - 1)
                    , idx + 1, UserCode.GetInstance().gProCd[idx].gSSP.angleStart[ProCodeCls.ShapeSearchPara.MAX_SHAPE_MUL_PARA_NUM - 1]);
                SetCellValuePriv(28 + 2 * (UserCode.GetInstance().gProCd[idx].gSSP.shapeModel.TupleLength() - 1)
                    , idx + 1, UserCode.GetInstance().gProCd[idx].gSSP.angleExtent[ProCodeCls.ShapeSearchPara.MAX_SHAPE_MUL_PARA_NUM - 1]);
                SetCellValuePriv(29 + 2 * (UserCode.GetInstance().gProCd[idx].gSSP.shapeModel.TupleLength() - 1)
                    , idx + 1, UserCode.GetInstance().gProCd[idx].gSSP.minScore[ProCodeCls.ShapeSearchPara.MAX_SHAPE_MUL_PARA_NUM - 1]);
                SetCellValuePriv(30 + 2 * (UserCode.GetInstance().gProCd[idx].gSSP.shapeModel.TupleLength() - 1)
                    , idx + 1, UserCode.GetInstance().gProCd[idx].gSSP.numMatches[ProCodeCls.ShapeSearchPara.MAX_SHAPE_MUL_PARA_NUM - 1]);
                SetCellValuePriv(31 + 2 * (UserCode.GetInstance().gProCd[idx].gSSP.shapeModel.TupleLength() - 1)
                    , idx + 1, UserCode.GetInstance().gProCd[idx].gSSP.maxOverlap);
                SetCellValuePriv(32 + 2 * (UserCode.GetInstance().gProCd[idx].gSSP.shapeModel.TupleLength() - 1)
                    , idx + 1, UserCode.GetInstance().gProCd[idx].gSSP.subPixel[(ProCodeCls.ShapeSearchPara.MAX_SHAPE_MUL_PARA_NUM - 1) * 2]);
                SetCellValuePriv(33 + 2 * (UserCode.GetInstance().gProCd[idx].gSSP.shapeModel.TupleLength() - 1)
                    , idx + 1, UserCode.GetInstance().gProCd[idx].gSSP.subPixel[(ProCodeCls.ShapeSearchPara.MAX_SHAPE_MUL_PARA_NUM - 1) * 2 + 1]);
                SetCellValuePriv(34 + 2 * (UserCode.GetInstance().gProCd[idx].gSSP.shapeModel.TupleLength() - 1)
                    , idx + 1, UserCode.GetInstance().gProCd[idx].gSSP.numLevels[(ProCodeCls.ShapeSearchPara.MAX_SHAPE_MUL_PARA_NUM - 1) * 2]);
                SetCellValuePriv(35 + 2 * (UserCode.GetInstance().gProCd[idx].gSSP.shapeModel.TupleLength() - 1)
                    , idx + 1, UserCode.GetInstance().gProCd[idx].gSSP.numLevels[(ProCodeCls.ShapeSearchPara.MAX_SHAPE_MUL_PARA_NUM - 1) * 2 + 1]);
                SetCellValuePriv(36 + 2 * (UserCode.GetInstance().gProCd[idx].gSSP.shapeModel.TupleLength() - 1)
                    , idx + 1, UserCode.GetInstance().gProCd[idx].gSSP.greediness[ProCodeCls.ShapeSearchPara.MAX_SHAPE_MUL_PARA_NUM - 1]);
            }
        }
        private void SaveAnisoShapeSearchFBD(int idx, int tMidx)
        {
            //  1
            SetCellValuePriv(1, idx + 1, UserCode.GetInstance().codeInfoValToKey[UserCode.GetInstance().gProCd[idx].FuncID]);
            //  2
            SetCellValuePriv(2, idx + 1, UserCode.GetInstance().gProCd[idx].gASSP.shapeModel.TupleLength());
            string fullFileName;

            for (int i = 0; i < UserCode.GetInstance().gProCd[idx].gASSP.shapeModel.TupleLength(); i++)
            {
                fullFileName = gExcelEdit.mFilename + "Model " + tMidx.ToString() + " " + i.ToString() + ".shm";
                HOperatorSet.WriteShapeModel(UserCode.GetInstance().gProCd[idx].gASSP.shapeModel[i], (HTuple)fullFileName);
                SetCellValuePriv(3 + 2 * i, idx + 1, fullFileName);

                fullFileName = gExcelEdit.mFilename + "Pic " + tMidx.ToString() + " " + i.ToString() + ".bmp";
                HOperatorSet.WriteImage(UserCode.GetInstance().gProCd[idx].gASSP.shapeModelImage[i + 1], "bmp", 0, fullFileName);
                SetCellValuePriv(4 + 2 * i, idx + 1, fullFileName);
            }

            fullFileName = gExcelEdit.mFilename + "Region " + tMidx.ToString() + ".reg";
            HOperatorSet.WriteRegion(UserCode.GetInstance().gProCd[idx].gASSP.shapeModelRegion, fullFileName);
            SetCellValuePriv(5 + 2 * (UserCode.GetInstance().gProCd[idx].gASSP.shapeModel.TupleLength() - 1), idx + 1, fullFileName);

            fullFileName = gExcelEdit.mFilename + "Points " + tMidx.ToString() + ".dat";
            HTuple hv_FileHandle;
            HOperatorSet.OpenFile(fullFileName, "output", out hv_FileHandle);

            for (int i = 0; i < UserCode.GetInstance().gProCd[idx].gASSP.shapeModel.TupleLength(); i++)
            {
                HOperatorSet.FwriteString(hv_FileHandle, UserCode.GetInstance().gProCd[idx].gASSP.shapeModelPoints[i].X + "\n");
                HOperatorSet.FwriteString(hv_FileHandle, UserCode.GetInstance().gProCd[idx].gASSP.shapeModelPoints[i].Y + "\n");
            }
            HOperatorSet.CloseFile(hv_FileHandle);
            SetCellValuePriv(6 + 2 * (UserCode.GetInstance().gProCd[idx].gASSP.shapeModel.TupleLength() - 1), idx + 1, fullFileName);
            //  3
            SetCellValuePriv(7 + +2 * (UserCode.GetInstance().gProCd[idx].gASSP.shapeModel.TupleLength() - 1)
                , idx + 1, UserCode.GetInstance().gProCd[idx].boolData[0]);
            //  4
            SetCellValuePriv(8 + +2 * (UserCode.GetInstance().gProCd[idx].gASSP.shapeModel.TupleLength() - 1)
                , idx + 1, UserCode.GetInstance().gProCd[idx].boolData[1]);
            //  5
            SetCellValuePriv(9 + +2 * (UserCode.GetInstance().gProCd[idx].gASSP.shapeModel.TupleLength() - 1)
                , idx + 1, UserCode.GetInstance().gProCd[idx].boolData[2]);
            //  6
            for (int i = 0; i < ProCodeCls.AnisoShapeSearchPara.MAX_SHAPE_MODEL; i++)
            {
                if (UserCode.GetInstance().gProCd[idx].shortData[i] == 1)
                {
                    SetCellValuePriv(10 + i + 2 * (UserCode.GetInstance().gProCd[idx].gASSP.shapeModel.TupleLength() - 1), idx + 1, "TRUE");
                }
                else
                {
                    SetCellValuePriv(10 + i + 2 * (UserCode.GetInstance().gProCd[idx].gASSP.shapeModel.TupleLength() - 1), idx + 1, "FALSE");
                }
            }
            //  7
            //^*^*^*save model image here
            //  8
            if (UserCode.GetInstance().gProCd[idx].boolData[2])                 //多模板
            {
                if (UserCode.GetInstance().gProCd[idx].boolData[1])             //为每套模板创建参数
                {
                    for (int i = 0; i < UserCode.GetInstance().gProCd[idx].gASSP.shapeModel.TupleLength(); i++)
                    {
                        SetCellValuePriv(27 + i * 14 + 2 * (UserCode.GetInstance().gProCd[idx].gASSP.shapeModel.TupleLength() - 1)
                            , idx + 1, UserCode.GetInstance().gProCd[idx].gASSP.angleStart[i]);
                        SetCellValuePriv(28 + i * 14 + 2 * (UserCode.GetInstance().gProCd[idx].gASSP.shapeModel.TupleLength() - 1)
                            , idx + 1, UserCode.GetInstance().gProCd[idx].gASSP.angleExtent[i]);
                        SetCellValuePriv(29 + i * 14 + 2 * (UserCode.GetInstance().gProCd[idx].gASSP.shapeModel.TupleLength() - 1)
                            , idx + 1, UserCode.GetInstance().gProCd[idx].gASSP.minScore[i]);
                        SetCellValuePriv(30 + i * 14 + 2 * (UserCode.GetInstance().gProCd[idx].gASSP.shapeModel.TupleLength() - 1)
                            , idx + 1, UserCode.GetInstance().gProCd[idx].gASSP.numMatches[i]);
                        SetCellValuePriv(31 + i * 14 + 2 * (UserCode.GetInstance().gProCd[idx].gASSP.shapeModel.TupleLength() - 1)
                            , idx + 1, UserCode.GetInstance().gProCd[idx].gASSP.maxOverlap);
                        SetCellValuePriv(32 + i * 14 + 2 * (UserCode.GetInstance().gProCd[idx].gASSP.shapeModel.TupleLength() - 1)
                            , idx + 1, UserCode.GetInstance().gProCd[idx].gASSP.subPixel[2 * i]);
                        SetCellValuePriv(33 + i * 14 + 2 * (UserCode.GetInstance().gProCd[idx].gASSP.shapeModel.TupleLength() - 1)
                            , idx + 1, UserCode.GetInstance().gProCd[idx].gASSP.subPixel[2 * i + 1]);
                        SetCellValuePriv(34 + i * 14 + 2 * (UserCode.GetInstance().gProCd[idx].gASSP.shapeModel.TupleLength() - 1)
                            , idx + 1, UserCode.GetInstance().gProCd[idx].gASSP.numLevels[2 * i]);
                        SetCellValuePriv(35 + i * 14 + 2 * (UserCode.GetInstance().gProCd[idx].gASSP.shapeModel.TupleLength() - 1)
                            , idx + 1, UserCode.GetInstance().gProCd[idx].gASSP.numLevels[2 * i + 1]);
                        SetCellValuePriv(36 + i * 14 + 2 * (UserCode.GetInstance().gProCd[idx].gASSP.shapeModel.TupleLength() - 1)
                            , idx + 1, UserCode.GetInstance().gProCd[idx].gASSP.greediness[i]);
                        SetCellValuePriv(37 + i * 14 + 2 * (UserCode.GetInstance().gProCd[idx].gASSP.shapeModel.TupleLength() - 1)
                            , idx + 1, UserCode.GetInstance().gProCd[idx].gASSP.scaleRMax[i]);
                        SetCellValuePriv(38 + i * 14 + 2 * (UserCode.GetInstance().gProCd[idx].gASSP.shapeModel.TupleLength() - 1)
                            , idx + 1, UserCode.GetInstance().gProCd[idx].gASSP.scaleRMin[i]);
                        SetCellValuePriv(39 + i * 14 + 2 * (UserCode.GetInstance().gProCd[idx].gASSP.shapeModel.TupleLength() - 1)
                            , idx + 1, UserCode.GetInstance().gProCd[idx].gASSP.scaleCMax[i]);
                        SetCellValuePriv(40 + i * 14 + 2 * (UserCode.GetInstance().gProCd[idx].gASSP.shapeModel.TupleLength() - 1)
                            , idx + 1, UserCode.GetInstance().gProCd[idx].gASSP.scaleCMin[i]);
                    }
                }
                else                                                            //仅使用一套参数
                {
                    SetCellValuePriv(27 + 2 * (UserCode.GetInstance().gProCd[idx].gASSP.shapeModel.TupleLength() - 1)
                        , idx + 1, UserCode.GetInstance().gProCd[idx].gASSP.angleStart[ProCodeCls.ShapeSearchPara.MAX_SHAPE_MUL_PARA_NUM - 1]);
                    SetCellValuePriv(28 + 2 * (UserCode.GetInstance().gProCd[idx].gASSP.shapeModel.TupleLength() - 1)
                        , idx + 1, UserCode.GetInstance().gProCd[idx].gASSP.angleExtent[ProCodeCls.ShapeSearchPara.MAX_SHAPE_MUL_PARA_NUM - 1]);
                    SetCellValuePriv(29 + 2 * (UserCode.GetInstance().gProCd[idx].gASSP.shapeModel.TupleLength() - 1)
                        , idx + 1, UserCode.GetInstance().gProCd[idx].gASSP.minScore[ProCodeCls.ShapeSearchPara.MAX_SHAPE_MUL_PARA_NUM - 1]);
                    SetCellValuePriv(30 + 2 * (UserCode.GetInstance().gProCd[idx].gASSP.shapeModel.TupleLength() - 1)
                        , idx + 1, UserCode.GetInstance().gProCd[idx].gASSP.numMatches[ProCodeCls.ShapeSearchPara.MAX_SHAPE_MUL_PARA_NUM - 1]);
                    SetCellValuePriv(31 + 2 * (UserCode.GetInstance().gProCd[idx].gASSP.shapeModel.TupleLength() - 1)
                        , idx + 1, UserCode.GetInstance().gProCd[idx].gASSP.maxOverlap);
                    SetCellValuePriv(32 + 2 * (UserCode.GetInstance().gProCd[idx].gASSP.shapeModel.TupleLength() - 1)
                        , idx + 1, UserCode.GetInstance().gProCd[idx].gASSP.subPixel[(ProCodeCls.ShapeSearchPara.MAX_SHAPE_MUL_PARA_NUM - 1) * 2]);
                    SetCellValuePriv(33 + 2 * (UserCode.GetInstance().gProCd[idx].gASSP.shapeModel.TupleLength() - 1)
                        , idx + 1, UserCode.GetInstance().gProCd[idx].gASSP.subPixel[(ProCodeCls.ShapeSearchPara.MAX_SHAPE_MUL_PARA_NUM - 1) * 2 + 1]);
                    SetCellValuePriv(34 + 2 * (UserCode.GetInstance().gProCd[idx].gASSP.shapeModel.TupleLength() - 1)
                        , idx + 1, UserCode.GetInstance().gProCd[idx].gASSP.numLevels[(ProCodeCls.ShapeSearchPara.MAX_SHAPE_MUL_PARA_NUM - 1) * 2]);
                    SetCellValuePriv(35 + 2 * (UserCode.GetInstance().gProCd[idx].gASSP.shapeModel.TupleLength() - 1)
                        , idx + 1, UserCode.GetInstance().gProCd[idx].gASSP.numLevels[(ProCodeCls.ShapeSearchPara.MAX_SHAPE_MUL_PARA_NUM - 1) * 2 + 1]);
                    SetCellValuePriv(36 + 2 * (UserCode.GetInstance().gProCd[idx].gASSP.shapeModel.TupleLength() - 1)
                        , idx + 1, UserCode.GetInstance().gProCd[idx].gASSP.greediness[ProCodeCls.ShapeSearchPara.MAX_SHAPE_MUL_PARA_NUM - 1]);
                    SetCellValuePriv(37 + 2 * (UserCode.GetInstance().gProCd[idx].gASSP.shapeModel.TupleLength() - 1)
                            , idx + 1, UserCode.GetInstance().gProCd[idx].gASSP.scaleRMax[ProCodeCls.ShapeSearchPara.MAX_SHAPE_MUL_PARA_NUM - 1]);
                    SetCellValuePriv(38 + 2 * (UserCode.GetInstance().gProCd[idx].gASSP.shapeModel.TupleLength() - 1)
                        , idx + 1, UserCode.GetInstance().gProCd[idx].gASSP.scaleRMin[ProCodeCls.ShapeSearchPara.MAX_SHAPE_MUL_PARA_NUM - 1]);
                    SetCellValuePriv(39 + 2 * (UserCode.GetInstance().gProCd[idx].gASSP.shapeModel.TupleLength() - 1)
                        , idx + 1, UserCode.GetInstance().gProCd[idx].gASSP.scaleCMax[ProCodeCls.ShapeSearchPara.MAX_SHAPE_MUL_PARA_NUM - 1]);
                    SetCellValuePriv(40 + 2 * (UserCode.GetInstance().gProCd[idx].gASSP.shapeModel.TupleLength() - 1)
                        , idx + 1, UserCode.GetInstance().gProCd[idx].gASSP.scaleCMin[ProCodeCls.ShapeSearchPara.MAX_SHAPE_MUL_PARA_NUM - 1]);
                }
            }
            else                                                                //单模板
            {
                SetCellValuePriv(27 + 2 * (UserCode.GetInstance().gProCd[idx].gASSP.shapeModel.TupleLength() - 1)
                    , idx + 1, UserCode.GetInstance().gProCd[idx].gASSP.angleStart[ProCodeCls.ShapeSearchPara.MAX_SHAPE_MUL_PARA_NUM - 1]);
                SetCellValuePriv(28 + 2 * (UserCode.GetInstance().gProCd[idx].gASSP.shapeModel.TupleLength() - 1)
                    , idx + 1, UserCode.GetInstance().gProCd[idx].gASSP.angleExtent[ProCodeCls.ShapeSearchPara.MAX_SHAPE_MUL_PARA_NUM - 1]);
                SetCellValuePriv(29 + 2 * (UserCode.GetInstance().gProCd[idx].gASSP.shapeModel.TupleLength() - 1)
                    , idx + 1, UserCode.GetInstance().gProCd[idx].gASSP.minScore[ProCodeCls.ShapeSearchPara.MAX_SHAPE_MUL_PARA_NUM - 1]);
                SetCellValuePriv(30 + 2 * (UserCode.GetInstance().gProCd[idx].gASSP.shapeModel.TupleLength() - 1)
                    , idx + 1, UserCode.GetInstance().gProCd[idx].gASSP.numMatches[ProCodeCls.ShapeSearchPara.MAX_SHAPE_MUL_PARA_NUM - 1]);
                SetCellValuePriv(31 + 2 * (UserCode.GetInstance().gProCd[idx].gASSP.shapeModel.TupleLength() - 1)
                    , idx + 1, UserCode.GetInstance().gProCd[idx].gASSP.maxOverlap);
                SetCellValuePriv(32 + 2 * (UserCode.GetInstance().gProCd[idx].gASSP.shapeModel.TupleLength() - 1)
                    , idx + 1, UserCode.GetInstance().gProCd[idx].gASSP.subPixel[(ProCodeCls.ShapeSearchPara.MAX_SHAPE_MUL_PARA_NUM - 1) * 2]);
                SetCellValuePriv(33 + 2 * (UserCode.GetInstance().gProCd[idx].gASSP.shapeModel.TupleLength() - 1)
                    , idx + 1, UserCode.GetInstance().gProCd[idx].gASSP.subPixel[(ProCodeCls.ShapeSearchPara.MAX_SHAPE_MUL_PARA_NUM - 1) * 2 + 1]);
                SetCellValuePriv(34 + 2 * (UserCode.GetInstance().gProCd[idx].gASSP.shapeModel.TupleLength() - 1)
                    , idx + 1, UserCode.GetInstance().gProCd[idx].gASSP.numLevels[(ProCodeCls.ShapeSearchPara.MAX_SHAPE_MUL_PARA_NUM - 1) * 2]);
                SetCellValuePriv(35 + 2 * (UserCode.GetInstance().gProCd[idx].gASSP.shapeModel.TupleLength() - 1)
                    , idx + 1, UserCode.GetInstance().gProCd[idx].gASSP.numLevels[(ProCodeCls.ShapeSearchPara.MAX_SHAPE_MUL_PARA_NUM - 1) * 2 + 1]);
                SetCellValuePriv(36 + 2 * (UserCode.GetInstance().gProCd[idx].gASSP.shapeModel.TupleLength() - 1)
                    , idx + 1, UserCode.GetInstance().gProCd[idx].gASSP.greediness[ProCodeCls.ShapeSearchPara.MAX_SHAPE_MUL_PARA_NUM - 1]);
                SetCellValuePriv(37 + 2 * (UserCode.GetInstance().gProCd[idx].gASSP.shapeModel.TupleLength() - 1)
                    , idx + 1, UserCode.GetInstance().gProCd[idx].gASSP.scaleRMax[ProCodeCls.ShapeSearchPara.MAX_SHAPE_MUL_PARA_NUM - 1]);
                SetCellValuePriv(38 + 2 * (UserCode.GetInstance().gProCd[idx].gASSP.shapeModel.TupleLength() - 1)
                    , idx + 1, UserCode.GetInstance().gProCd[idx].gASSP.scaleRMin[ProCodeCls.ShapeSearchPara.MAX_SHAPE_MUL_PARA_NUM - 1]);
                SetCellValuePriv(39 + 2 * (UserCode.GetInstance().gProCd[idx].gASSP.shapeModel.TupleLength() - 1)
                    , idx + 1, UserCode.GetInstance().gProCd[idx].gASSP.scaleCMax[ProCodeCls.ShapeSearchPara.MAX_SHAPE_MUL_PARA_NUM - 1]);
                SetCellValuePriv(40 + 2 * (UserCode.GetInstance().gProCd[idx].gASSP.shapeModel.TupleLength() - 1)
                    , idx + 1, UserCode.GetInstance().gProCd[idx].gASSP.scaleCMin[ProCodeCls.ShapeSearchPara.MAX_SHAPE_MUL_PARA_NUM - 1]);
            }
        }
        private void SaveSerialOutputFBD(int idx)
        {
            SetCellValuePriv(1, idx + 1, UserCode.GetInstance().codeInfoValToKey[UserCode.GetInstance().gProCd[idx].FuncID]);
            SetCellValuePriv(2, idx + 1, UserCode.GetInstance().gProCd[idx].boolData[0]);           //通讯方式
            SetCellValuePriv(3, idx + 1, UserCode.GetInstance().gProCd[idx].boolData[1]);           //输出格式
            SetCellValuePriv(4, idx + 1, UserCode.GetInstance().gProCd[idx].shortData[0]);          //整数位式
            SetCellValuePriv(5, idx + 1, UserCode.GetInstance().gProCd[idx].shortData[1]);          //小数位数
            SetCellValuePriv(6, idx + 1, UserCode.GetInstance().gProCd[idx].boolData[2]);           //负号表示
            SetCellValuePriv(7, idx + 1, UserCode.GetInstance().gProCd[idx].boolData[3]);           //消零标志
            SetCellValuePriv(8, idx + 1, UserCode.GetInstance().gProCd[idx].shortData[2]);          //字段分隔符
            SetCellValuePriv(9, idx + 1, UserCode.GetInstance().gProCd[idx].shortData[3]);          //记录分隔符

            for (int i = 0; i < ProCodeCls.MAX_OTHER_NUM; i++)
            {
                SetCellValuePriv(10 + i, idx + 1, UserCode.GetInstance().gProCd[idx].intData[i]);   //数据
            }

        }
        private void SetCellValuePriv(int x, int y, object value)
        {
            gExcelEdit.SetCellValue(gMainSheet, x, y, value);
        }
        */
        #endregion
        #region OldOpenCodeByEXCEL
        /*
         public void OpenExcelToProj(String tPath, String tFilename)
        {
            try
            {
                gExcelEdit.Open(tPath);
                gMainSheet = gExcelEdit.GetSheet("Program");
                for (int i = 0; i < 20; i++)
                {
                    switch (UserCode.GetInstance().codeInfo[gExcelEdit.GetCellValue(gMainSheet, 1, i + 1)])
                    {
                        case ProCodeCls.MainFunction.NullFBD:
                            LoadNullFBD(i);
                            break;
                        case ProCodeCls.MainFunction.InputCameraInputFBD:
                            LoadCameraInputFBD(i);
                            break;
                        case ProCodeCls.MainFunction.CalibrationThresholdFBD:
                            LoadThresholdFBD(i);
                            break;
                        case ProCodeCls.MainFunction.CalibrationMedianFilterFBD:
                            LoadMedianFilterFBD(i);
                            break;
                        case ProCodeCls.MainFunction.CalibrationBackgroundRemoveFBD:
                            LoadBackgroundRemoveFBD(i);
                            break;
                        case ProCodeCls.MainFunction.MeasureShapeSearchFBD:
                            LoadShapeSearchFBD(i);
                            break;
                        case ProCodeCls.MainFunction.MeasureAnisoShapeSearchFBD:
                            LoadAnisoShapeSearchFBD(i);
                            break;
                        case ProCodeCls.MainFunction.OutputSerialOutputFBD:
                            LoadSerialOutputFBD(i);
                            break;
                    }
                }
                String tmpPath = tPath.Substring(0, tPath.IndexOf(tFilename, 0));
                String tmpFileName = tFilename.Substring(0, tFilename.IndexOf("."));
                tmpPath = tmpPath + tmpFileName + ".xml";

                ConfigInformation.GetInstance().tCfgInfoOprt.LoadCfgInfo(tmpPath);
                gExcelEdit.Save();
                gExcelEdit.Close();
            }
            catch (System.Exception ex)
            {

            }
        }
        private void LoadNullFBD(int idx)
        {

        }

        private void LoadCameraInputFBD(int idx)
        {
            UserCode.GetInstance().gProCd[idx].FuncID = UserCode.GetInstance().codeInfo
                [GetCellValuePriv(1, idx + 1)];
        }

        private void LoadThresholdFBD(int idx)
        {
            UserCode.GetInstance().gProCd[idx].FuncID = UserCode.GetInstance().codeInfo[GetCellValuePriv(1, idx + 1)];
            UserCode.GetInstance().gProCd[idx].floatData[0] = float.Parse(GetCellValuePriv(2, idx + 1));
            UserCode.GetInstance().gProCd[idx].floatData[1] = float.Parse(GetCellValuePriv(3, idx + 1));
        }
         
        private void LoadMedianFilterFBD(int idx)
        {
            UserCode.GetInstance().gProCd[idx].FuncID = UserCode.GetInstance().codeInfo[GetCellValuePriv(1, idx + 1)];
            UserCode.GetInstance().gProCd[idx].intData[0] = int.Parse(GetCellValuePriv(2, idx + 1));
        } 
                private void LoadBackgroundRemoveFBD(int idx)
        {
            UserCode.GetInstance().gProCd[idx].FuncID = UserCode.GetInstance().codeInfo[GetCellValuePriv(1, idx + 1)];
            UserCode.GetInstance().gProCd[idx].boolData[0] = bool.Parse(GetCellValuePriv(2, idx + 1));

            for (int i = 0; i < 8; i++)
            {
                UserCode.GetInstance().gProCd[idx].floatData[i] = float.Parse(GetCellValuePriv(i + 3, idx + 1));
            }
        }

        private void LoadShapeSearchFBD(int idx)
        {
            UserCode.GetInstance().gProCd[idx].FuncID = UserCode.GetInstance().codeInfo[GetCellValuePriv(1, idx + 1)];
            //  2
            int tmpShapeNum= int.Parse(GetCellValuePriv(2, idx + 1));
            string fullFileName;

            for (int i = 0; i < tmpShapeNum; i++)
            {
                if (i == 0)
                {
                    //shape model .shm
                    fullFileName = GetCellValuePriv(3 + 2 * i, idx + 1);
                    HOperatorSet.ReadShapeModel((HTuple)fullFileName, out UserCode.GetInstance().gProCd[idx].gSSP.shapeModel);
                    //picture .bmp
                    fullFileName = GetCellValuePriv(4 + 2 * i, idx + 1);
                    HOperatorSet.ReadImage(out UserCode.GetInstance().gProCd[idx].gSSP.shapeModelImage, (HTuple)fullFileName);
                }
                else
                {
                    HTuple tmpHt;
                    HObject tmpHo;
                    //shape model .shm
                    fullFileName = GetCellValuePriv(3 + 2 * i, idx + 1);
                    HOperatorSet.ReadShapeModel((HTuple)fullFileName, out tmpHt);
                    UserCode.GetInstance().gProCd[idx].gSSP.shapeModel = 
                        UserCode.GetInstance().gProCd[idx].gSSP.shapeModel.TupleConcat(tmpHt);
                    //picture .bmp
                    fullFileName = GetCellValuePriv(4 + 2 * i, idx + 1);
                    HOperatorSet.ReadImage(out tmpHo, (HTuple)fullFileName);
                    UserCode.GetInstance().gProCd[idx].gSSP.shapeModelImage.ConcatObj(tmpHo);
                }
            }
//region .reg
            fullFileName = GetCellValuePriv(5 + 2 * (tmpShapeNum - 1), idx + 1);
            HOperatorSet.ReadRegion(out UserCode.GetInstance().gProCd[idx].gSSP.shapeModelRegion, (HTuple)fullFileName);
//point .dat
            fullFileName = GetCellValuePriv(6 + 2 * (tmpShapeNum - 1), idx + 1);
            HTuple hv_FileHandle, pointX, pointY, hv_IsEOF;
            PointF modelPoint = new PointF(0,0);
            HOperatorSet.OpenFile((HTuple)fullFileName, "input", out hv_FileHandle);
            for (int i = 0; i < tmpShapeNum; i++)
            {
                HOperatorSet.FreadString(hv_FileHandle, out pointX, out hv_IsEOF);
                HOperatorSet.FreadString(hv_FileHandle, out pointY, out hv_IsEOF);
                modelPoint.X = float.Parse((string)pointX);
                modelPoint.Y = float.Parse((string)pointY);
                UserCode.GetInstance().gProCd[idx].gSSP.shapeModelPoints.Add(modelPoint);
            }
            HOperatorSet.CloseFile(hv_FileHandle);

            //  3
            UserCode.GetInstance().gProCd[idx].boolData[0] = bool.Parse(GetCellValuePriv(7 + 2 * (tmpShapeNum - 1), idx + 1));
            //  4
            UserCode.GetInstance().gProCd[idx].boolData[1] = bool.Parse(GetCellValuePriv(8 + 2 * (tmpShapeNum - 1), idx + 1));
            //  5
            UserCode.GetInstance().gProCd[idx].boolData[2] = bool.Parse(GetCellValuePriv(9 + 2 * (tmpShapeNum - 1), idx + 1));
            //  6
            for (int i = 0; i < ProCodeCls.ShapeSearchPara.MAX_SHAPE_MODEL; i++)
            {
                if (bool.Parse(GetCellValuePriv(10 + i + 2 * (tmpShapeNum - 1), idx + 1)))
                {
                    UserCode.GetInstance().gProCd[idx].shortData[i] = 1;
                }
                else
                {
                    UserCode.GetInstance().gProCd[idx].shortData[i] = 0;
                }
                //UserCode.GetInstance().gProCd[idx].gSSP.modelIsChecked[i] = bool.Parse(GetCellValuePriv(9 + i, idx + 1));
            }
            //  7
            //^*^*^*save model image here
            //  8
            if (UserCode.GetInstance().gProCd[idx].boolData[2])                 //多模板
            {
                if (UserCode.GetInstance().gProCd[idx].boolData[1])             //为每套模板创建参数
                {
                    for (int i = 0; i < UserCode.GetInstance().gProCd[idx].gSSP.shapeModel.TupleLength(); i++)
                    {
                        UserCode.GetInstance().gProCd[idx].gSSP.angleStart[i] =
                            double.Parse(GetCellValuePriv(27 + i * 10 + 2 * (tmpShapeNum - 1), idx + 1));
                        UserCode.GetInstance().gProCd[idx].gSSP.angleExtent[i] =
                            double.Parse(GetCellValuePriv(28 + i * 10 + 2 * (tmpShapeNum - 1), idx + 1));
                        UserCode.GetInstance().gProCd[idx].gSSP.minScore[i] =
                            double.Parse(GetCellValuePriv(29 + i * 10 + 2 * (tmpShapeNum - 1), idx + 1));
                        UserCode.GetInstance().gProCd[idx].gSSP.numMatches[i] =
                            int.Parse(GetCellValuePriv(30 + i * 10 + 2 * (tmpShapeNum - 1), idx + 1));
                        UserCode.GetInstance().gProCd[idx].gSSP.maxOverlap =
                            double.Parse(GetCellValuePriv(31 + i * 10 + 2 * (tmpShapeNum - 1), idx + 1));
                        UserCode.GetInstance().gProCd[idx].gSSP.subPixel[2*i] =
                            int.Parse(GetCellValuePriv(32 + i * 10 + 2 * (tmpShapeNum - 1), idx + 1));
                        UserCode.GetInstance().gProCd[idx].gSSP.subPixel[2*i + 1] =
                            int.Parse(GetCellValuePriv(33 + i * 10 + 2 * (tmpShapeNum - 1), idx + 1));
                        UserCode.GetInstance().gProCd[idx].gSSP.numLevels[2*i] =
                            int.Parse(GetCellValuePriv(34 + i * 10 + 2 * (tmpShapeNum - 1), idx + 1));
                        UserCode.GetInstance().gProCd[idx].gSSP.numLevels[2*i + 1] =
                            int.Parse(GetCellValuePriv(35 + i * 10 + 2 * (tmpShapeNum - 1), idx + 1));
                        UserCode.GetInstance().gProCd[idx].gSSP.greediness[i] =
                            double.Parse(GetCellValuePriv(36 + i * 10 + 2 * (tmpShapeNum - 1), idx + 1));
                    }
                }
                else                                                            //仅使用一套参数
                {
                    UserCode.GetInstance().gProCd[idx].gSSP.angleStart[ProCodeCls.ShapeSearchPara.MAX_SHAPE_MUL_PARA_NUM - 1]
                        = double.Parse(GetCellValuePriv(27 + 2 * (tmpShapeNum - 1), idx + 1));
                    UserCode.GetInstance().gProCd[idx].gSSP.angleExtent[ProCodeCls.ShapeSearchPara.MAX_SHAPE_MUL_PARA_NUM - 1]
                        = double.Parse(GetCellValuePriv(28 + 2 * (tmpShapeNum - 1), idx + 1));
                    UserCode.GetInstance().gProCd[idx].gSSP.minScore[ProCodeCls.ShapeSearchPara.MAX_SHAPE_MUL_PARA_NUM - 1]
                        = double.Parse(GetCellValuePriv(29 + 2 * (tmpShapeNum - 1), idx + 1));
                    UserCode.GetInstance().gProCd[idx].gSSP.numMatches[ProCodeCls.ShapeSearchPara.MAX_SHAPE_MUL_PARA_NUM - 1]
                        = int.Parse(GetCellValuePriv(30 + 2 * (tmpShapeNum - 1), idx + 1));
                    UserCode.GetInstance().gProCd[idx].gSSP.maxOverlap
                        = double.Parse(GetCellValuePriv(31 + 2 * (tmpShapeNum - 1), idx + 1));
                    UserCode.GetInstance().gProCd[idx].gSSP.subPixel[(ProCodeCls.ShapeSearchPara.MAX_SHAPE_MUL_PARA_NUM - 1) * 2]
                        = int.Parse(GetCellValuePriv(32 + 2 * (tmpShapeNum - 1), idx + 1));
                    UserCode.GetInstance().gProCd[idx].gSSP.subPixel[(ProCodeCls.ShapeSearchPara.MAX_SHAPE_MUL_PARA_NUM - 1) * 2 + 1]
                        = int.Parse(GetCellValuePriv(33 + 2 * (tmpShapeNum - 1), idx + 1));
                    UserCode.GetInstance().gProCd[idx].gSSP.numLevels[(ProCodeCls.ShapeSearchPara.MAX_SHAPE_MUL_PARA_NUM - 1) * 2]
                        = int.Parse(GetCellValuePriv(34 + 2 * (tmpShapeNum - 1), idx + 1));
                    UserCode.GetInstance().gProCd[idx].gSSP.numLevels[(ProCodeCls.ShapeSearchPara.MAX_SHAPE_MUL_PARA_NUM - 1) * 2 + 1]
                        = int.Parse(GetCellValuePriv(35 + 2 * (tmpShapeNum - 1), idx + 1));
                    UserCode.GetInstance().gProCd[idx].gSSP.greediness[ProCodeCls.ShapeSearchPara.MAX_SHAPE_MUL_PARA_NUM - 1]
                        = double.Parse(GetCellValuePriv(36 + 2 * (tmpShapeNum - 1), idx + 1));
                }
            }
            else                                                                //单模板
            {
                UserCode.GetInstance().gProCd[idx].gSSP.angleStart[ProCodeCls.ShapeSearchPara.MAX_SHAPE_MUL_PARA_NUM - 1]
                        = double.Parse(GetCellValuePriv(27 + 2 * (tmpShapeNum - 1), idx + 1));
                UserCode.GetInstance().gProCd[idx].gSSP.angleExtent[ProCodeCls.ShapeSearchPara.MAX_SHAPE_MUL_PARA_NUM - 1]
                    = double.Parse(GetCellValuePriv(28 + 2 * (tmpShapeNum - 1), idx + 1));
                UserCode.GetInstance().gProCd[idx].gSSP.minScore[ProCodeCls.ShapeSearchPara.MAX_SHAPE_MUL_PARA_NUM - 1]
                    = double.Parse(GetCellValuePriv(29 + 2 * (tmpShapeNum - 1), idx + 1));
                UserCode.GetInstance().gProCd[idx].gSSP.numMatches[ProCodeCls.ShapeSearchPara.MAX_SHAPE_MUL_PARA_NUM - 1]
                    = int.Parse(GetCellValuePriv(30 + 2 * (tmpShapeNum - 1), idx + 1));
                UserCode.GetInstance().gProCd[idx].gSSP.maxOverlap
                    = double.Parse(GetCellValuePriv(31 + 2 * (tmpShapeNum - 1), idx + 1));
                UserCode.GetInstance().gProCd[idx].gSSP.subPixel[(ProCodeCls.ShapeSearchPara.MAX_SHAPE_MUL_PARA_NUM - 1) * 2]
                    = int.Parse(GetCellValuePriv(32 + 2 * (tmpShapeNum - 1), idx + 1));
                UserCode.GetInstance().gProCd[idx].gSSP.subPixel[(ProCodeCls.ShapeSearchPara.MAX_SHAPE_MUL_PARA_NUM - 1) * 2 + 1]
                    = int.Parse(GetCellValuePriv(33 + 2 * (tmpShapeNum - 1), idx + 1));
                UserCode.GetInstance().gProCd[idx].gSSP.numLevels[(ProCodeCls.ShapeSearchPara.MAX_SHAPE_MUL_PARA_NUM - 1) * 2]
                    = int.Parse(GetCellValuePriv(34 + 2 * (tmpShapeNum - 1), idx + 1));
                UserCode.GetInstance().gProCd[idx].gSSP.numLevels[(ProCodeCls.ShapeSearchPara.MAX_SHAPE_MUL_PARA_NUM - 1) * 2 + 1]
                    = int.Parse(GetCellValuePriv(35 + 2 * (tmpShapeNum - 1), idx + 1));
                UserCode.GetInstance().gProCd[idx].gSSP.greediness[ProCodeCls.ShapeSearchPara.MAX_SHAPE_MUL_PARA_NUM - 1]
                    = double.Parse(GetCellValuePriv(36 + 2 * (tmpShapeNum - 1), idx + 1));
            }
        }

        private void LoadAnisoShapeSearchFBD(int idx)
        {
            UserCode.GetInstance().gProCd[idx].FuncID = UserCode.GetInstance().codeInfo[GetCellValuePriv(1, idx + 1)];
            //  2
            int tmpShapeNum = int.Parse(GetCellValuePriv(2, idx + 1));
            string fullFileName;

            for (int i = 0; i < tmpShapeNum; i++)
            {
                if (i == 0)
                {
                    //shape model .shm
                    fullFileName = GetCellValuePriv(3 + 2 * i, idx + 1);
                    HOperatorSet.ReadShapeModel((HTuple)fullFileName, out UserCode.GetInstance().gProCd[idx].gASSP.shapeModel);
                    //picture .bmp
                    fullFileName = GetCellValuePriv(4 + 2 * i, idx + 1);
                    HOperatorSet.ReadImage(out UserCode.GetInstance().gProCd[idx].gASSP.shapeModelImage, (HTuple)fullFileName);
                }
                else
                {
                    HTuple tmpHt;
                    HObject tmpHo;
                    //shape model .shm
                    fullFileName = GetCellValuePriv(3 + 2 * i, idx + 1);
                    HOperatorSet.ReadShapeModel((HTuple)fullFileName, out tmpHt);
                    UserCode.GetInstance().gProCd[idx].gASSP.shapeModel =
                        UserCode.GetInstance().gProCd[idx].gASSP.shapeModel.TupleConcat(tmpHt);
                    //picture .bmp
                    fullFileName = GetCellValuePriv(4 + 2 * i, idx + 1);
                    HOperatorSet.ReadImage(out tmpHo, (HTuple)fullFileName);
                    UserCode.GetInstance().gProCd[idx].gASSP.shapeModelImage.ConcatObj(tmpHo);
                }
            }
            //region .reg
            fullFileName = GetCellValuePriv(5 + 2 * (tmpShapeNum - 1), idx + 1);
            HOperatorSet.ReadRegion(out UserCode.GetInstance().gProCd[idx].gASSP.shapeModelRegion, (HTuple)fullFileName);
            //point .dat
            fullFileName = GetCellValuePriv(6 + 2 * (tmpShapeNum - 1), idx + 1);
            HTuple hv_FileHandle, pointX, pointY, hv_IsEOF;
            PointF modelPoint = new PointF(0, 0);
            HOperatorSet.OpenFile((HTuple)fullFileName, "input", out hv_FileHandle);
            for (int i = 0; i < tmpShapeNum; i++)
            {
                HOperatorSet.FreadString(hv_FileHandle, out pointX, out hv_IsEOF);
                HOperatorSet.FreadString(hv_FileHandle, out pointY, out hv_IsEOF);
                modelPoint.X = float.Parse((string)pointX);
                modelPoint.Y = float.Parse((string)pointY);
                UserCode.GetInstance().gProCd[idx].gASSP.shapeModelPoints.Add(modelPoint);
            }
            HOperatorSet.CloseFile(hv_FileHandle);

            //  3
            UserCode.GetInstance().gProCd[idx].boolData[0] = bool.Parse(GetCellValuePriv(7 + 2 * (tmpShapeNum - 1), idx + 1));
            //  4
            UserCode.GetInstance().gProCd[idx].boolData[1] = bool.Parse(GetCellValuePriv(8 + 2 * (tmpShapeNum - 1), idx + 1));
            //  5
            UserCode.GetInstance().gProCd[idx].boolData[2] = bool.Parse(GetCellValuePriv(9 + 2 * (tmpShapeNum - 1), idx + 1));
            //  6
            for (int i = 0; i < ProCodeCls.ShapeSearchPara.MAX_SHAPE_MODEL; i++)
            {
                if (bool.Parse(GetCellValuePriv(10 + i + 2 * (tmpShapeNum - 1), idx + 1)))
                {
                    UserCode.GetInstance().gProCd[idx].shortData[i] = 1;
                }
                else
                {
                    UserCode.GetInstance().gProCd[idx].shortData[i] = 0;
                }
                //UserCode.GetInstance().gProCd[idx].gASSP.modelIsChecked[i] = bool.Parse(GetCellValuePriv(9 + i, idx + 1));
            }
            //  7
            //^*^*^*save model image here
            //  8
            if (UserCode.GetInstance().gProCd[idx].boolData[2])                 //多模板
            {
                if (UserCode.GetInstance().gProCd[idx].boolData[1])             //为每套模板创建参数
                {
                    for (int i = 0; i < UserCode.GetInstance().gProCd[idx].gASSP.shapeModel.TupleLength(); i++)
                    {
                        UserCode.GetInstance().gProCd[idx].gASSP.angleStart[i] =
                            double.Parse(GetCellValuePriv(27 + i * 14 + 2 * (tmpShapeNum - 1), idx + 1));
                        UserCode.GetInstance().gProCd[idx].gASSP.angleExtent[i] =
                            double.Parse(GetCellValuePriv(28 + i * 14 + 2 * (tmpShapeNum - 1), idx + 1));
                        UserCode.GetInstance().gProCd[idx].gASSP.minScore[i] =
                            double.Parse(GetCellValuePriv(29 + i * 14 + 2 * (tmpShapeNum - 1), idx + 1));
                        UserCode.GetInstance().gProCd[idx].gASSP.numMatches[i] =
                            int.Parse(GetCellValuePriv(30 + i * 14 + 2 * (tmpShapeNum - 1), idx + 1));
                        UserCode.GetInstance().gProCd[idx].gASSP.maxOverlap =
                            double.Parse(GetCellValuePriv(31 + i * 14 + 2 * (tmpShapeNum - 1), idx + 1));
                        UserCode.GetInstance().gProCd[idx].gASSP.subPixel[2 * i] =
                            int.Parse(GetCellValuePriv(32 + i * 14 + 2 * (tmpShapeNum - 1), idx + 1));
                        UserCode.GetInstance().gProCd[idx].gASSP.subPixel[2 * i + 1] =
                            int.Parse(GetCellValuePriv(33 + i * 14 + 2 * (tmpShapeNum - 1), idx + 1));
                        UserCode.GetInstance().gProCd[idx].gASSP.numLevels[2 * i] =
                            int.Parse(GetCellValuePriv(34 + i * 14 + 2 * (tmpShapeNum - 1), idx + 1));
                        UserCode.GetInstance().gProCd[idx].gASSP.numLevels[2 * i + 1] =
                            int.Parse(GetCellValuePriv(35 + i * 14 + 2 * (tmpShapeNum - 1), idx + 1));
                        UserCode.GetInstance().gProCd[idx].gASSP.greediness[i] =
                            double.Parse(GetCellValuePriv(36 + i * 14 + 2 * (tmpShapeNum - 1), idx + 1));
                        UserCode.GetInstance().gProCd[idx].gASSP.scaleRMax[i] =
                            double.Parse(GetCellValuePriv(37 + i * 14 + 2 * (tmpShapeNum - 1), idx + 1));
                        UserCode.GetInstance().gProCd[idx].gASSP.scaleRMin[i] =
                            double.Parse(GetCellValuePriv(38 + i * 14 + 2 * (tmpShapeNum - 1), idx + 1));
                        UserCode.GetInstance().gProCd[idx].gASSP.scaleCMax[i] =
                            double.Parse(GetCellValuePriv(39 + i * 14 + 2 * (tmpShapeNum - 1), idx + 1));
                        UserCode.GetInstance().gProCd[idx].gASSP.scaleCMin[i] =
                            double.Parse(GetCellValuePriv(40 + i * 14 + 2 * (tmpShapeNum - 1), idx + 1));
                    }
                }
                else                                                            //仅使用一套参数
                {
                    UserCode.GetInstance().gProCd[idx].gASSP.angleStart[ProCodeCls.ShapeSearchPara.MAX_SHAPE_MUL_PARA_NUM - 1]
                        = double.Parse(GetCellValuePriv(27 + 2 * (tmpShapeNum - 1), idx + 1));
                    UserCode.GetInstance().gProCd[idx].gASSP.angleExtent[ProCodeCls.ShapeSearchPara.MAX_SHAPE_MUL_PARA_NUM - 1]
                        = double.Parse(GetCellValuePriv(28 + 2 * (tmpShapeNum - 1), idx + 1));
                    UserCode.GetInstance().gProCd[idx].gASSP.minScore[ProCodeCls.ShapeSearchPara.MAX_SHAPE_MUL_PARA_NUM - 1]
                        = double.Parse(GetCellValuePriv(29 + 2 * (tmpShapeNum - 1), idx + 1));
                    UserCode.GetInstance().gProCd[idx].gASSP.numMatches[ProCodeCls.ShapeSearchPara.MAX_SHAPE_MUL_PARA_NUM - 1]
                        = int.Parse(GetCellValuePriv(30 + 2 * (tmpShapeNum - 1), idx + 1));
                    UserCode.GetInstance().gProCd[idx].gASSP.maxOverlap
                        = double.Parse(GetCellValuePriv(31 + 2 * (tmpShapeNum - 1), idx + 1));
                    UserCode.GetInstance().gProCd[idx].gASSP.subPixel[(ProCodeCls.ShapeSearchPara.MAX_SHAPE_MUL_PARA_NUM - 1) * 2]
                        = int.Parse(GetCellValuePriv(32 + 2 * (tmpShapeNum - 1), idx + 1));
                    UserCode.GetInstance().gProCd[idx].gASSP.subPixel[(ProCodeCls.ShapeSearchPara.MAX_SHAPE_MUL_PARA_NUM - 1) * 2 + 1]
                        = int.Parse(GetCellValuePriv(33 + 2 * (tmpShapeNum - 1), idx + 1));
                    UserCode.GetInstance().gProCd[idx].gASSP.numLevels[(ProCodeCls.ShapeSearchPara.MAX_SHAPE_MUL_PARA_NUM - 1) * 2]
                        = int.Parse(GetCellValuePriv(34 + 2 * (tmpShapeNum - 1), idx + 1));
                    UserCode.GetInstance().gProCd[idx].gASSP.numLevels[(ProCodeCls.ShapeSearchPara.MAX_SHAPE_MUL_PARA_NUM - 1) * 2 + 1]
                        = int.Parse(GetCellValuePriv(35 + 2 * (tmpShapeNum - 1), idx + 1));
                    UserCode.GetInstance().gProCd[idx].gASSP.greediness[ProCodeCls.ShapeSearchPara.MAX_SHAPE_MUL_PARA_NUM - 1]
                        = double.Parse(GetCellValuePriv(36 + 2 * (tmpShapeNum - 1), idx + 1));

                    UserCode.GetInstance().gProCd[idx].gASSP.scaleRMax[ProCodeCls.ShapeSearchPara.MAX_SHAPE_MUL_PARA_NUM - 1]
                        = double.Parse(GetCellValuePriv(37 + 2 * (tmpShapeNum - 1), idx + 1));
                    UserCode.GetInstance().gProCd[idx].gASSP.scaleRMin[ProCodeCls.ShapeSearchPara.MAX_SHAPE_MUL_PARA_NUM - 1]
                        = double.Parse(GetCellValuePriv(38 + 2 * (tmpShapeNum - 1), idx + 1));
                    UserCode.GetInstance().gProCd[idx].gASSP.scaleCMax[ProCodeCls.ShapeSearchPara.MAX_SHAPE_MUL_PARA_NUM - 1]
                        = double.Parse(GetCellValuePriv(39 + 2 * (tmpShapeNum - 1), idx + 1));
                    UserCode.GetInstance().gProCd[idx].gASSP.scaleCMin[ProCodeCls.ShapeSearchPara.MAX_SHAPE_MUL_PARA_NUM - 1]
                        = double.Parse(GetCellValuePriv(40 + 2 * (tmpShapeNum - 1), idx + 1));
                }
            }
            else                                                                //单模板
            {
                UserCode.GetInstance().gProCd[idx].gASSP.angleStart[ProCodeCls.ShapeSearchPara.MAX_SHAPE_MUL_PARA_NUM - 1]
                        = double.Parse(GetCellValuePriv(27 + 2 * (tmpShapeNum - 1), idx + 1));
                UserCode.GetInstance().gProCd[idx].gASSP.angleExtent[ProCodeCls.ShapeSearchPara.MAX_SHAPE_MUL_PARA_NUM - 1]
                    = double.Parse(GetCellValuePriv(28 + 2 * (tmpShapeNum - 1), idx + 1));
                UserCode.GetInstance().gProCd[idx].gASSP.minScore[ProCodeCls.ShapeSearchPara.MAX_SHAPE_MUL_PARA_NUM - 1]
                    = double.Parse(GetCellValuePriv(29 + 2 * (tmpShapeNum - 1), idx + 1));
                UserCode.GetInstance().gProCd[idx].gASSP.numMatches[ProCodeCls.ShapeSearchPara.MAX_SHAPE_MUL_PARA_NUM - 1]
                    = int.Parse(GetCellValuePriv(30 + 2 * (tmpShapeNum - 1), idx + 1));
                UserCode.GetInstance().gProCd[idx].gASSP.maxOverlap
                    = double.Parse(GetCellValuePriv(31 + 2 * (tmpShapeNum - 1), idx + 1));
                UserCode.GetInstance().gProCd[idx].gASSP.subPixel[(ProCodeCls.ShapeSearchPara.MAX_SHAPE_MUL_PARA_NUM - 1) * 2]
                    = int.Parse(GetCellValuePriv(32 + 2 * (tmpShapeNum - 1), idx + 1));
                UserCode.GetInstance().gProCd[idx].gASSP.subPixel[(ProCodeCls.ShapeSearchPara.MAX_SHAPE_MUL_PARA_NUM - 1) * 2 + 1]
                    = int.Parse(GetCellValuePriv(33 + 2 * (tmpShapeNum - 1), idx + 1));
                UserCode.GetInstance().gProCd[idx].gASSP.numLevels[(ProCodeCls.ShapeSearchPara.MAX_SHAPE_MUL_PARA_NUM - 1) * 2]
                    = int.Parse(GetCellValuePriv(34 + 2 * (tmpShapeNum - 1), idx + 1));
                UserCode.GetInstance().gProCd[idx].gASSP.numLevels[(ProCodeCls.ShapeSearchPara.MAX_SHAPE_MUL_PARA_NUM - 1) * 2 + 1]
                    = int.Parse(GetCellValuePriv(35 + 2 * (tmpShapeNum - 1), idx + 1));
                UserCode.GetInstance().gProCd[idx].gASSP.greediness[ProCodeCls.ShapeSearchPara.MAX_SHAPE_MUL_PARA_NUM - 1]
                    = double.Parse(GetCellValuePriv(36 + 2 * (tmpShapeNum - 1), idx + 1));
                UserCode.GetInstance().gProCd[idx].gASSP.scaleRMax[ProCodeCls.ShapeSearchPara.MAX_SHAPE_MUL_PARA_NUM - 1]
                    = double.Parse(GetCellValuePriv(37 + 2 * (tmpShapeNum - 1), idx + 1));
                UserCode.GetInstance().gProCd[idx].gASSP.scaleRMin[ProCodeCls.ShapeSearchPara.MAX_SHAPE_MUL_PARA_NUM - 1]
                    = double.Parse(GetCellValuePriv(38 + 2 * (tmpShapeNum - 1), idx + 1));
                UserCode.GetInstance().gProCd[idx].gASSP.scaleCMax[ProCodeCls.ShapeSearchPara.MAX_SHAPE_MUL_PARA_NUM - 1]
                    = double.Parse(GetCellValuePriv(39 + 2 * (tmpShapeNum - 1), idx + 1));
                UserCode.GetInstance().gProCd[idx].gASSP.scaleCMin[ProCodeCls.ShapeSearchPara.MAX_SHAPE_MUL_PARA_NUM - 1]
                    = double.Parse(GetCellValuePriv(40 + 2 * (tmpShapeNum - 1), idx + 1));
            }
        }

        private void LoadSerialOutputFBD(int idx)
        {
            UserCode.GetInstance().gProCd[idx].FuncID = UserCode.GetInstance().codeInfo[GetCellValuePriv(1, idx + 1)];

            
            UserCode.GetInstance().gProCd[idx].boolData[0] = bool.Parse(GetCellValuePriv(2, idx + 1));           //通讯方式
            UserCode.GetInstance().gProCd[idx].boolData[1] = bool.Parse(GetCellValuePriv(3, idx + 1));           //输出格式
            UserCode.GetInstance().gProCd[idx].shortData[0] = short.Parse(GetCellValuePriv(4, idx + 1));         //整数位式
            UserCode.GetInstance().gProCd[idx].shortData[1] = short.Parse(GetCellValuePriv(5, idx + 1));         //小数位数
            UserCode.GetInstance().gProCd[idx].boolData[2] = bool.Parse(GetCellValuePriv(6, idx + 1));           //负号表示
            UserCode.GetInstance().gProCd[idx].boolData[3] = bool.Parse(GetCellValuePriv(7, idx + 1));           //消零标志
            UserCode.GetInstance().gProCd[idx].shortData[2] = short.Parse(GetCellValuePriv(8, idx + 1));         //字段分隔符
            UserCode.GetInstance().gProCd[idx].shortData[3] = short.Parse(GetCellValuePriv(9, idx + 1));         //记录分隔符

            for (int i = 0; i < ProCodeCls.MAX_OTHER_NUM; i++)
            {
                UserCode.GetInstance().gProCd[idx].intData[i] = int.Parse(GetCellValuePriv(10 + i, idx + 1));   //数据
            }
        }

        private string GetCellValuePriv(int x, int y)
        {
            return gExcelEdit.GetCellValue(gMainSheet, x, y);
        }
         
         
         
         
         
         */
        #endregion
    }
    #region ExcelEdit Class
    /*
    public class ExcelEdit
    {
        public string mFilename;
        public Excel.Application app;
        public Excel.Workbooks wbs;
        public Excel.Workbook wb;
        public Excel.Worksheets wss;
        public Excel.Worksheet ws;

        public ExcelEdit()
        {
            
        }

        public void Create()//创建一个Excel对象
        {
            app = new Excel.Application();
            wbs = app.Workbooks;
            wb = wbs.Add(true);
            app.DisplayAlerts = false;
            app.AlertBeforeOverwriting = false;
        }

        public void Open(string FileName)//打开一个Excel文件
        {
            app = new Excel.Application();
            wbs = app.Workbooks;
            wb = wbs.Add(FileName);
            //wb = wbs.Open(FileName, 0, true, 5,"", "", true, Excel.XlPlatform.xlWindows, "t", false, false, 0, true,Type.Missing,Type.Missing);
            //wb = wbs.Open(FileName,Type.Missing,Type.Missing,Type.Missing,Type.Missing,Type.Missing,Type.Missing,Excel.XlPlatform.xlWindows,Type.Missing,Type.Missing,Type.Missing,Type.Missing,Type.Missing,Type.Missing,Type.Missing);
            mFilename = FileName;
            app.DisplayAlerts = false;
            app.AlertBeforeOverwriting = false;
        }

        public Excel.Worksheet GetSheet(string SheetName)
        //获取一个工作表
        {
            Excel.Worksheet s = (Excel.Worksheet)wb.Worksheets[SheetName];
            return s;
        }

        public Excel.Worksheet AddSheet(string SheetName)
        //添加一个工作表
        {
            Excel.Worksheet s = (Excel.Worksheet)wb.Worksheets.Add(Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            s.Name = SheetName;
            return s;
        }

        public void DelSheet(string SheetName)//删除一个工作表
        {
            ((Excel.Worksheet)wb.Worksheets[SheetName]).Delete();
        }

        public Excel.Worksheet ReNameSheet(string OldSheetName, string NewSheetName)//重命名一个工作表一
        {
            Excel.Worksheet s = (Excel.Worksheet)wb.Worksheets[OldSheetName];
            s.Name = NewSheetName;
            return s;
        }

        public Excel.Worksheet ReNameSheet(Excel.Worksheet Sheet, string NewSheetName)//重命名一个工作表二
        {
            Sheet.Name = NewSheetName;
            return Sheet;
        }

        public void SetCellValue(Excel.Worksheet ws, int x, int y, object value)        //ws：要设值的工作表     X行Y列     value   值
        {
            try
            {
                ws.Cells[x, y] = value;
            }
            catch (System.Exception ex)
            {
                String err = ex.ToString();
            }
            
        }
        public void SetCellValue(string ws, int x, int y, object value)                 //ws：要设值的工作表的名称 X行Y列 value 值
        {

            GetSheet(ws).Cells[x, y] = value;
        }

        public String GetCellValue(Excel.Worksheet ws, int x, int y)
        {
            return ((Excel.Range)ws.Cells[x, y]).Text;
        }

        public void SetCellProperty(Excel.Worksheet ws, int Startx, int Starty, int Endx, int Endy, int size, string name, Excel.Constants color, Excel.Constants HorizontalAlignment)      //设置一个单元格的属性   字体，   大小，颜色   ，对齐方式
        {
            name = "宋体";
            size = 12;
            color = Excel.Constants.xlAutomatic;
            HorizontalAlignment = Excel.Constants.xlRight;
            ws.get_Range(ws.Cells[Startx, Starty], ws.Cells[Endx, Endy]).Font.Name = name;
            ws.get_Range(ws.Cells[Startx, Starty], ws.Cells[Endx, Endy]).Font.Size = size;
            ws.get_Range(ws.Cells[Startx, Starty], ws.Cells[Endx, Endy]).Font.Color = color;
            ws.get_Range(ws.Cells[Startx, Starty], ws.Cells[Endx, Endy]).HorizontalAlignment = HorizontalAlignment;
        }

        public void SetCellProperty(string wsn, int Startx, int Starty, int Endx, int Endy, int size, string name, Excel.Constants color, Excel.Constants HorizontalAlignment)
        {
            //name = "宋体";
            //size = 12;
            //color = Excel.Constants.xlAutomatic;
            //HorizontalAlignment = Excel.Constants.xlRight;

            Excel.Worksheet ws = GetSheet(wsn);
            ws.get_Range(ws.Cells[Startx, Starty], ws.Cells[Endx, Endy]).Font.Name = name;
            ws.get_Range(ws.Cells[Startx, Starty], ws.Cells[Endx, Endy]).Font.Size = size;
            ws.get_Range(ws.Cells[Startx, Starty], ws.Cells[Endx, Endy]).Font.Color = color;

            ws.get_Range(ws.Cells[Startx, Starty], ws.Cells[Endx, Endy]).HorizontalAlignment = HorizontalAlignment;
        }


        public void UniteCells(Excel.Worksheet ws, int x1, int y1, int x2, int y2)          //合并单元格
        {
            ws.get_Range(ws.Cells[x1, y1], ws.Cells[x2, y2]).Merge(Type.Missing);
        }

        public void UniteCells(string ws, int x1, int y1, int x2, int y2)                   //合并单元格
        {
            GetSheet(ws).get_Range(GetSheet(ws).Cells[x1, y1], GetSheet(ws).Cells[x2, y2]).Merge(Type.Missing);

        }
        public bool Save()                  //保存文档
        {
            if (mFilename == "")
            {
                return false;
            }
            else
            {
                try
                {
                    wb.Save();
                    app.Save(mFilename);
                    app.SaveWorkspace(mFilename);
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }
        public bool SaveAs(object FileName)         //文档另存为
        {
            try
            {
                wb.SaveAs(FileName, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public void Close()                         //关闭一个Excel对象，销毁对象
        {
            //wb.Save();
            wb.Close(Type.Missing, Type.Missing, Type.Missing);
            wbs.Close();
            app.Quit();
            wb = null;
            wbs = null;
            app = null;
            GC.Collect();
        }
    }
 */
    /*
            public void InsertTable(System.Data.DataTable dt, string ws, int startX, int startY)            //将内存中数据表格插入到Excel指定工作表的指定位置 为在使用模板时控制格式时使用一
            {

                for (int i = 0; i &lt;= dt.Rows.Count - 1; i++)
                {
                    for (int j = 0; j &lt;= dt.Columns.Count - 1; j++)
                    {
                        GetSheet(ws).Cells[startX+i, j + startY] = dt.Rows[i][j].ToString();

                    }

                }

            }
            public void InsertTable(System.Data.DataTable dt, Excel.Worksheet ws, int startX, int startY)   //将内存中数据表格插入到Excel指定工作表的指定位置二
            {

                for (int i = 0; i &lt;= dt.Rows.Count - 1; i++)
                {
                    for (int j = 0; j &lt;= dt.Columns.Count - 1; j++)
                    {

                        ws.Cells[startX+i, j + startY] = dt.Rows[i][j];

                    }

                }

            }


            public void AddTable(System.Data.DataTable dt, string ws, int startX, int startY)               //将内存中数据表格添加到Excel指定工作表的指定位置一
            {

                for (int i = 0; i &lt;= dt.Rows.Count - 1; i++)
                {
                    for (int j = 0; j &lt;= dt.Columns.Count - 1; j++)
                    {

                        GetSheet(ws).Cells[i + startX, j + startY] = dt.Rows[i][j];

                    }

                }

            }
            public void AddTable(System.Data.DataTable dt, Excel.Worksheet ws, int startX, int startY)      //将内存中数据表格添加到Excel指定工作表的指定位置二
            {


                for (int i = 0; i &lt;= dt.Rows.Count - 1; i++)
                {
                    for (int j = 0; j &lt;= dt.Columns.Count - 1; j++)
                    {

                        ws.Cells[i + startX, j + startY] = dt.Rows[i][j];

                    }
                }

            }
            public void InsertPictures(string Filename, string ws)                                              //插入图片操作一
            {
                GetSheet(ws).Shapes.AddPicture(Filename, MsoTriState.msoFalse, MsoTriState.msoTrue, 10, 10, 150, 150);
                //后面的数字表示位置
            }

            //public void InsertPictures(string Filename, string ws, int Height, int Width)
            //插入图片操作二
            //{
            //    GetSheet(ws).Shapes.AddPicture(Filename, MsoTriState.msoFalse, MsoTriState.msoTrue, 10, 10, 150, 150);
            //    GetSheet(ws).Shapes.get_Range(Type.Missing).Height = Height;
            //    GetSheet(ws).Shapes.get_Range(Type.Missing).Width = Width;
            //}
            //public void InsertPictures(string Filename, string ws, int left, int top, int Height, int Width)
            //插入图片操作三
            //{

            //    GetSheet(ws).Shapes.AddPicture(Filename, MsoTriState.msoFalse, MsoTriState.msoTrue, 10, 10, 150, 150);
            //    GetSheet(ws).Shapes.get_Range(Type.Missing).IncrementLeft(left);
            //    GetSheet(ws).Shapes.get_Range(Type.Missing).IncrementTop(top);
            //    GetSheet(ws).Shapes.get_Range(Type.Missing).Height = Height;
            //    GetSheet(ws).Shapes.get_Range(Type.Missing).Width = Width;
            //}

            public void InsertActiveChart(Excel.XlChartType ChartType, string ws, int DataSourcesX1, int DataSourcesY1, int DataSourcesX2, int DataSourcesY2, Excel.XlRowCol ChartDataType)         //插入图表操作
            {
                ChartDataType = Excel.XlRowCol.xlColumns;
                wb.Charts.Add(Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                {
                    wb.ActiveChart.ChartType = ChartType;
                    wb.ActiveChart.SetSourceData(GetSheet(ws).get_Range(GetSheet(ws).Cells[DataSourcesX1, DataSourcesY1], GetSheet(ws).Cells[DataSourcesX2, DataSourcesY2]), ChartDataType);
                    wb.ActiveChart.Location(Excel.XlChartLocation.xlLocationAsObject, ws);
                }
            }
    */

    #endregion

}
