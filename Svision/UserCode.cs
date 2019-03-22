/**************************************************************************************
**
**       Filename:  UserCode.cs
**
**    Description:  user code run in this file
**
**        Version:  1.0
**        Created:  2016-1-26
**       Revision:  v02.0007
**       Compiler:  vs2010
**        Company:  SIASUN
**
****************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HalconDotNet;
using System.Threading;
using System.Drawing;






namespace Svision
{

    public class ProCodeCls
    {
        public enum MainFunction : int
        {
            NullFBD,                                        //0

            InputCameraInputFBD,                            //1
            InputFileInputFBD,                              //2

            CalibrationThresholdFBD,                        //3
            CalibrationBackgroundRemoveFBD,                 //4
            CalibrationHistogramEqualizationFBD,            //5
            CalibrationSmoothFBD,                           //6
            CalibrationColorConversionFBD,                  //7
            CalibrationMedianFilterFBD,                     //8
            CalibrationFreqDomainFilterFBD,                 //9
            CalibrationSharpenFilterFBD,                    //10
            CalibrationMorphologyProcessingFBD,                     //11


            MeasureShapeSearchFBD,                          //12
            MeasureBlobAnalysisFBD,                         //13
            MeasureReadCodeFBD,                             //14
            MeasureEdgeScanFBD,                             //15
            MeasureDefectDetectFBD,                         //16
            MeasureCharacterFBD,                            //17
            MeasureAnisoShapeSearchFBD,               //18
            MeasureSurfaceBasedMatchFBD, //19


            OutputSerialOutputFBD                           //19//20
        }

        public MainFunction FuncID;
        public int FuncLen;

        public CameraInputFBD tCamInput;
        public FileInputFBD tFileInput;
        public ThresholdFBD tThreShold;
        public BackgroundRemoveFBD tBackRemove;
        public MedianFilterFBD tMeidanFilter;
        public MorphologyProcessingFBD tMorPro;
        public BlobAnalysisFBD tBlobAnalysis;
        public ShapeSearchFBD tShpSrh;
        public AnisoShapeSearchFBD tAShpSrh;
        public SurfaceBasedMatchFBD tSBMatch;
        public SerialOutputFBD tSrlOutput;

        //public bool[] boolData;
        //public short[] shortData;
        //public int[] intData;
        //public float[] floatData;
        public double[] doubleData;

        public const int MAX_DOUBLE_NUM = 100;
        public const int MAX_OTHER_NUM = 32;


        public static int Max_Row_Number;
        public class BackgroundRemovePara
        {
            public float[] grayValue;
            public bool isAllColor;

            public BackgroundRemovePara()
            {
                grayValue = new float[8] { 255, 0, 255, 0, 255, 0, 255, 0 };
                isAllColor = true;

            }
            public void clear()
            {
                grayValue[0] = 255;
                grayValue[1] = 0;
                grayValue[2] = 255;
                grayValue[3] = 0;
                grayValue[4] = 255;
                grayValue[5] = 0;
                grayValue[6] = 255;
                grayValue[7] = 0;

                isAllColor = true;
            }
        }
        public class MedianFilterPara
        {
            public int maskSize;
            public MedianFilterPara()
            {
                maskSize = 0;
            }
            public void clear()
            {
                maskSize = 0;
            }
        }
        public class MorphologyProcessingPara
        {

            public int processID;
            public int elementID;
            public int width;
            public int height;
            public double radius;
            public MorphologyProcessingPara()
            {
                processID = 0;
                elementID = 0;
                width = 11;
                height = 11;
                radius = 3.5;
            }
            public void clear()
            {
                processID = 0;
                elementID = 0;
                width = 11;
                height = 11;
                radius = 3.5;
            }
        }
        public class ThresholdPara
        {
            public float minValue;
            public float maxValue;
            public ThresholdPara()
            {
                maxValue = 200;
                minValue = 50;

            }
            public void clear()
            {
                maxValue = 200;
                minValue = 50;
            }
        }

        public class ShapeSearchPara
        {
            public int Max_Object_Num;
            public List<string> outPutStringList;
            public bool[] findShapeModelBatchOperationFlag;
            public string[] findShapeModelBatchOperationStr;
            public int arrangeIndex;
            public bool isBorderShapeModelChecked;
            public bool isMultiplePara;

            public const int MAX_SHAPE_MODEL = 100;
            public const int MAX_SHAPE_MUL_PARA_NUM = MAX_SHAPE_MODEL + 1;
            public string[] showOutputResultStr;
            public bool[] showOutputResultFlag;

            public HTuple shapeModel;//前MAX_SHAPE_MODEL套参数对应多模版多参数的极限情况，第MAX_SHAPE_MODEL+1套参数对应多模版单参数或单模版单参数情况
            public HObject shapeModelImage;
            public HObject shapeModelRegion;
            public List<PointF> shapeModelPoints;
            public bool[] modelIsChecked;
            public double[] angleStart;
            public double[] angleExtent;
            public double[] minScore;
            public int[] numMatches;
            public double maxOverlap;
            public int[] subPixel;
            public int[] numLevels;
            public double[] greediness;

            public ShapeSearchPara()
            {

                outPutStringList = new List<string>();
                Max_Object_Num = 0;
                findShapeModelBatchOperationFlag = new bool[5] { true, true, true, true, true };
                findShapeModelBatchOperationStr = new string[5] { "X坐标", "Y坐标", "种类", "角度", "得分" };
                arrangeIndex = 0;
                isBorderShapeModelChecked = false;
                isMultiplePara = false;

                showOutputResultStr = new string[5] { "结果轮廓", "位置", "种类", "角度", "得分" };
                showOutputResultFlag = new bool[5];
                for (int i = 0; i < 5; i++)
                {
                    showOutputResultFlag[i] = true;
                }

                shapeModelPoints = new List<PointF>();
                angleStart = new double[MAX_SHAPE_MUL_PARA_NUM];
                angleExtent = new double[MAX_SHAPE_MUL_PARA_NUM];
                minScore = new double[MAX_SHAPE_MUL_PARA_NUM];
                numMatches = new int[MAX_SHAPE_MUL_PARA_NUM];
                //maxOverlap = new double[16];
                subPixel = new int[MAX_SHAPE_MUL_PARA_NUM * 2];
                numLevels = new int[MAX_SHAPE_MUL_PARA_NUM * 2];
                greediness = new double[MAX_SHAPE_MUL_PARA_NUM];
                modelIsChecked = new bool[MAX_SHAPE_MUL_PARA_NUM];
                for (int i = 0; i < MAX_SHAPE_MUL_PARA_NUM; i++)
                {
                    angleStart[i] = -3.15;
                    angleExtent[i] = 6.29;
                    minScore[i] = 0.8;
                    numMatches[i] = 5;
                    subPixel[i * 2] = 2;
                    subPixel[i * 2 + 1] = 1;
                    numLevels[i * 2] = 0;
                    numLevels[i * 2 + 1] = 0;
                    greediness[i] = 0.8;
                    modelIsChecked[i] = false;
                }
                maxOverlap = 0.2;
            }

            public void clear()
            {
                if (shapeModel != null)
                {
                    for (int i = 0; i < shapeModel.TupleLength(); i++)
                    {
                        HOperatorSet.ClearShapeModel(shapeModel[i]);
                    }

                }

                shapeModel = null;
                arrangeIndex = 0;
                if (shapeModelImage != null)
                {
                    shapeModelImage.Dispose();
                    shapeModelImage = null;
                }

                shapeModelRegion = null;
                shapeModelPoints.Clear();
                outPutStringList.Clear();
                for (int i = 0; i < MAX_SHAPE_MUL_PARA_NUM; i++)
                {
                    angleStart[i] = -3.15;
                    angleExtent[i] = 6.29;
                    minScore[i] = 0.8;
                    numMatches[i] = 5;
                    subPixel[i * 2] = 2;
                    subPixel[i * 2 + 1] = 1;
                    numLevels[i * 2] = 0;
                    numLevels[i * 2 + 1] = 0;
                    greediness[i] = 0.7;
                    modelIsChecked[i] = false;
                }
                maxOverlap = 0.5;
            }
        }
        public class BlobPara
        {
            public bool[] blobAnalysisOperationFlag;
            public string[] blobAnalysisOperationStr;
            public List<string> outPutStringList;
            //Segment parameters
            public const int MAX_COLOR_NUMBER = 6;
            public bool isColor;
            public bool isAutoSegment;
            public bool[] selectedColor;
            public int[] redValue;
            public int[] greenValue;
            public int[] blueValue;
            public int[] grayValue;
            public int[] lengthValue;
            public bool[] isBesideThisColor;
            public int MaxGrayValue;
            public int MinGrayValue;
            public bool isAutoSegmentMethod1;
            public bool isAutoSegmentMethod2;
            public bool isAutoSegmentMethod3;
            public float autoSegmentMethod1Para1;
            public int autoSegmentMethod2Para1;
            public float autoSegmentMethod2Para2;
            public int autoSegmentMethod2Para3;
            public int autoSegmentMethod2Para4;
            public int segmentShow;

            //Morphology parameters
            public bool isFillUpHoles;
            public bool isConnectionBeforeFillUpHoles;
            public bool isErosion;
            public int erosionElementNUM;
            public int erosionRWidth;
            public int erosionRHeight;
            public double erosionCRadius;
            public bool isDilation;
            public int dilationElementNUM;
            public int dilationRWidth;
            public int dilationRHeight;
            public double dilationCRadius;
            public bool isOpening;
            public int openingElementNUM;
            public int openingRWidth;
            public int openingRHeight;
            public double openingCRadius;
            public bool isClosing;
            public int closingElementNUM;
            public int closingRWidth;
            public int closingRHeight;
            public double closingCRadius;

            //Select
            public string[] selectSTR;
            public bool[] selectIsChecked;
            public bool selectisAnd;
            public double[] selectMin;
            public double[] selectMax;
            public int selectArrangeItemIndex;
            public bool isArrangeLtoS;
            public int regionNum;
            public int selectItemCount;
            //Output
            public string[] outputShowStr;
            public bool[] outputIDIsChecked;


            //output show svision
            public bool[] showOutputResultFlag;
            public string[] showOutputResultStr;
            public BlobPara()
            {
                blobAnalysisOperationFlag = new bool[0];
                blobAnalysisOperationStr = new string[0];
                outPutStringList = new List<string>();
                //Segment parameters
                if (Svision.GetMe().baslerCamera.getChannelNumber() == 1)
                {
                    isColor = false;
                }
                else
                {
                    isColor = true;//isColor
                }
                isAutoSegment = true;
                selectedColor = new bool[MAX_COLOR_NUMBER];
                redValue = new int[MAX_COLOR_NUMBER];
                greenValue = new int[MAX_COLOR_NUMBER];
                blueValue = new int[MAX_COLOR_NUMBER];
                grayValue = new int[MAX_COLOR_NUMBER];
                lengthValue = new int[MAX_COLOR_NUMBER];
                isBesideThisColor = new bool[MAX_COLOR_NUMBER];
                MaxGrayValue = 200;
                MinGrayValue = 100;
                isAutoSegmentMethod1 = false;
                isAutoSegmentMethod2 = false;
                isAutoSegmentMethod3 = true;
                autoSegmentMethod1Para1 = 2.0F;
                autoSegmentMethod2Para1 = 15;
                autoSegmentMethod2Para2 = 0.2F;
                autoSegmentMethod2Para3 = 2;
                autoSegmentMethod2Para4 = 0;
                segmentShow = 0;
                for (int i = 0; i < MAX_COLOR_NUMBER; i++)
                {
                    selectedColor[i] = false;
                    redValue[i] = 0;
                    greenValue[i] = 0;
                    blueValue[i] = 0;
                    grayValue[i] = 0;
                    lengthValue[i] = 0;
                    isBesideThisColor[i] = false;

                }
                //Morphology parameters
                isFillUpHoles = false;
                isConnectionBeforeFillUpHoles = false;
                isErosion = false;
                erosionElementNUM = 0;
                erosionRWidth = 11;
                erosionRHeight = 11;
                erosionCRadius = 3.5;
                isDilation = false;
                dilationElementNUM = 0;
                dilationRWidth = 11;
                dilationRHeight = 11;
                dilationCRadius = 3.5;
                isOpening = false;
                openingElementNUM = 0;
                openingRWidth = 11;
                openingRHeight = 11;
                openingCRadius = 3.5;
                isClosing = false;
                closingElementNUM = 0;
                closingRWidth = 11;
                closingRHeight = 11;
                closingCRadius = 3.5;

                //Select 
                //面积 重心Y 重心X 区域宽度 区域高度 区域左上角Y坐标 区域左上角X坐标 区域右下角Y坐标 区域右下角X坐标 ;
                //圆形近似度 紧密度 轮廓长度 凸性 矩形近似度 等效椭圆长轴半径长度 等效椭圆短轴半径长度 等效椭圆方向 ;
                //椭圆参数：长短轴比值 最小外接圆半径 最大内接圆半径 最大内接矩形宽度 最大内接矩形高度 多边形边数 区域内洞数 ;
                //所有洞的面积 最大直径  区域方向 最小外接矩形方向 最小外接矩形长度 最小外接矩形宽度 区域灰度最小值 区域灰度最大值 区域灰度平均值 区域灰度标准差;
                selectSTR = new string[34]{/*select shape*/"area","row","column","width","height","row1","column1","row2","column2","circularity","compactness","contlength","convexity"
                             ,"rectangularity","ra","rb","phi","anisometry","outer_radius","inner_radius","inner_width","inner_height","num_sides","holes_num",
                             "area_holes","max_diameter","orientation","rect2_phi","rect2_len1","rect2_len2",/*select gray*/"min","max","mean","deviation"    };
                selectIsChecked = new bool[34];
                selectMin = new double[34];
                selectMax = new double[34];
                selectArrangeItemIndex = 0;
                selectisAnd = true;/* = new bool[34];*/
                isArrangeLtoS = true;
                regionNum = 10;
                selectItemCount = 0;
                for (int i = 0; i < 34; i++)
                {
                    selectIsChecked[i] = false;
                    //selectisAnd[i] = true;
                    selectMin[i] = 0;
                    selectMax[i] = 99999999.99;

                }
                //Output 
                outputShowStr = new string[34]{"面积","重心Y","重心X","区域宽度","区域高度","区域左上角Y坐标","区域左上角X坐标","区域右下角Y坐标","区域右下角X坐标",
                                                    "圆形近似度","紧密度","轮廓长度","凸性","矩形近似度","等效椭圆长轴半径长度","等效椭圆短轴半径长度","等效椭圆方向",
                                                    "椭圆参数：长短轴比值","最小外接圆半径","最大内接圆半径","最大内接矩形宽度","最大内接矩形高度","多边形边数","区域内洞数",
                                                       "所有洞的面积","最大直径"," 区域方向","最小外接矩形方向","最小外接矩形长度","最小外接矩形宽度",
                                                      "区域灰度最小值","区域灰度最大值","区域灰度平均值","区域灰度标准差"};
                outputIDIsChecked = new bool[34];
                for (int i = 0; i < 34; i++)
                {
                    outputIDIsChecked[i] = false;
                }



                //
                showOutputResultStr = new string[36] { "结果轮廓","目标个数","面积","重心Y","重心X","区域宽度","区域高度","区域左上角Y坐标","区域左上角X坐标","区域右下角Y坐标","区域右下角X坐标",
                                                    "圆形近似度","紧密度","轮廓长度","凸性","矩形近似度","等效椭圆长轴半径长度","等效椭圆短轴半径长度","等效椭圆方向",
                                                    "椭圆参数：长短轴比值","最小外接圆半径","最大内接圆半径","最大内接矩形宽度","最大内接矩形高度","多边形边数","区域内洞数",
                                                       "所有洞的面积","最大直径"," 区域方向","最小外接矩形方向","最小外接矩形长度","最小外接矩形宽度",
                                                      "区域灰度最小值","区域灰度最大值","区域灰度平均值","区域灰度标准差" };
                showOutputResultFlag = new bool[36];
                for (int i = 0; i < 36; i++)
                {
                    showOutputResultFlag[i] = true;
                }
            }

            public void clear()
            {
                outPutStringList.Clear();
                blobAnalysisOperationFlag = new bool[0];
                blobAnalysisOperationStr = new string[0];
                if (Svision.GetMe().baslerCamera.getChannelNumber() == 1)
                {
                    isColor = false;
                }
                else
                {
                    isColor = true;//isColor
                }
                isAutoSegment = true;
                MaxGrayValue = 200;
                MinGrayValue = 100;
                isAutoSegmentMethod1 = false;
                isAutoSegmentMethod2 = false;
                isAutoSegmentMethod3 = true;
                autoSegmentMethod1Para1 = 2.0F;
                autoSegmentMethod2Para1 = 15;
                autoSegmentMethod2Para2 = 0.2F;
                autoSegmentMethod2Para3 = 2;
                autoSegmentMethod2Para4 = 0;
                segmentShow = 0;
                for (int i = 0; i < MAX_COLOR_NUMBER; i++)
                {
                    selectedColor[i] = false;
                    redValue[i] = 0;
                    greenValue[i] = 0;
                    blueValue[i] = 0;
                    lengthValue[i] = 0;
                    isBesideThisColor[i] = false;

                }
                isFillUpHoles = false;
                isConnectionBeforeFillUpHoles = false;
                isErosion = false;
                erosionElementNUM = 0;
                erosionRWidth = 11;
                erosionRHeight = 11;
                erosionCRadius = 3.5;
                isDilation = false;
                dilationElementNUM = 0;
                dilationRWidth = 11;
                dilationRHeight = 11;
                dilationCRadius = 3.5;
                isOpening = false;
                openingElementNUM = 0;
                openingRWidth = 11;
                openingRHeight = 11;
                openingCRadius = 3.5;
                isClosing = false;
                closingElementNUM = 0;
                closingRWidth = 11;
                closingRHeight = 11;
                closingCRadius = 3.5;
                selectArrangeItemIndex = 0;
                selectisAnd = true;/* = new bool[34];*/
                isArrangeLtoS = true;
                regionNum = 10;
                selectItemCount = 0;
                for (int i = 0; i < 34; i++)
                {
                    selectIsChecked[i] = false;
                    //selectisAnd[i] = true;
                    selectMin[i] = 0;
                    selectMax[i] = 99999999.99;

                }
                for (int i = 0; i < 34; i++)
                {
                    outputIDIsChecked[i] = false;
                }

                for (int i = 0; i < 34; i++)
                {
                    showOutputResultFlag[i] = true;
                }
            }
        }
        public class AnisoShapeSearchPara
        {
            public int Max_Object_Num;
            public List<string> outPutStringList;
            public int arrangeIndex;
            public bool isBorderShapeModelChecked;
            public bool isMultiplePara;
            public const int MAX_SHAPE_MODEL = 100;
            public const int MAX_SHAPE_MUL_PARA_NUM = MAX_SHAPE_MODEL + 1;
            public string[] showOutputResultStr;
            public bool[] showOutputResultFlag;
            public bool[] findAnisoShapeModelBatchOperationFlag;
            public string[] findAnisoShapeModelBatchOperationStr;
            public HObject shapeModelImage;
            public HObject shapeModelRegion;
            public List<PointF> shapeModelPoints;
            public bool[] modelIsChecked;
            public HTuple shapeModel;//前MAX_SHAPE_MODEL套参数对应多模版多参数的极限情况，第MAX_SHAPE_MODEL+1套参数对应多模版单参数或单模版单参数情况
            public double[] angleStart;
            public double[] angleExtent;
            public double[] minScore;
            public int[] numMatches;
            public double maxOverlap;
            public int[] subPixel;
            public int[] numLevels;
            public double[] greediness;

            public double[] scaleRMin;
            public double[] scaleRMax;
            //public double[] scaleRStep;
            public double[] scaleCMin;
            public double[] scaleCMax;
            //public double[] scaleCStep;

            public AnisoShapeSearchPara()
            {
                findAnisoShapeModelBatchOperationFlag = new bool[7] { true, true, true, true, true, true, true };
                findAnisoShapeModelBatchOperationStr = new string[7] { "X坐标", "Y坐标", "X方向放大系数", "Y方向放大系数", "种类", "角度", "得分" };
                arrangeIndex = 0;
                outPutStringList = new List<string>();
                Max_Object_Num = 0;
                isBorderShapeModelChecked = false;
                isMultiplePara = false;
                showOutputResultStr = new string[7] { "结果轮廓", "位置", "X方向放大系数", "Y方向放大系数", "种类", "角度", "得分" };
                showOutputResultFlag = new bool[7];
                for (int i = 0; i < 7; i++)
                {
                    showOutputResultFlag[i] = true;
                }
                shapeModelPoints = new List<PointF>();
                angleStart = new double[MAX_SHAPE_MUL_PARA_NUM];
                angleExtent = new double[MAX_SHAPE_MUL_PARA_NUM];
                minScore = new double[MAX_SHAPE_MUL_PARA_NUM];
                numMatches = new int[MAX_SHAPE_MUL_PARA_NUM];
                //maxOverlap = new double[16];
                subPixel = new int[MAX_SHAPE_MUL_PARA_NUM * 2];
                numLevels = new int[MAX_SHAPE_MUL_PARA_NUM * 2];
                greediness = new double[MAX_SHAPE_MUL_PARA_NUM];
                modelIsChecked = new bool[MAX_SHAPE_MUL_PARA_NUM];

                scaleRMin = new double[MAX_SHAPE_MUL_PARA_NUM];
                scaleRMax = new double[MAX_SHAPE_MUL_PARA_NUM];

                scaleCMin = new double[MAX_SHAPE_MUL_PARA_NUM];
                scaleCMax = new double[MAX_SHAPE_MUL_PARA_NUM];


                for (int i = 0; i < MAX_SHAPE_MUL_PARA_NUM; i++)
                {
                    angleStart[i] = -3.15;
                    angleExtent[i] = 6.29;
                    minScore[i] = 0.8;
                    numMatches[i] = 5;
                    subPixel[i * 2] = 2;
                    subPixel[i * 2 + 1] = 1;
                    numLevels[i * 2] = 0;
                    numLevels[i * 2 + 1] = 0;
                    greediness[i] = 0.8;

                    scaleRMin[i] = 0.9;
                    scaleRMax[i] = 1.1;
                    scaleCMin[i] = 0.9;
                    scaleCMax[i] = 1.1;

                    modelIsChecked[i] = false;
                }
                maxOverlap = 0.2;
            }

            public void clear()
            {
                findAnisoShapeModelBatchOperationFlag = new bool[7] { true, true, true, true, true, true, true };
                findAnisoShapeModelBatchOperationStr = new string[7] { "X坐标", "Y坐标", "X方向放大系数", "Y方向放大系数", "种类", "角度", "得分" };
               
                if (shapeModel != null)
                {
                    for (int i = 0; i < shapeModel.TupleLength(); i++)
                    {
                        HOperatorSet.ClearShapeModel(shapeModel[i]);
                    }
                }
                shapeModel = null;
                arrangeIndex = 0;
                if (shapeModelImage != null)
                {
                    shapeModelImage.Dispose();
                    shapeModelImage = null;
                }

                shapeModelRegion = null;
                shapeModelPoints.Clear();
                outPutStringList.Clear();
                for (int i = 0; i < MAX_SHAPE_MUL_PARA_NUM; i++)
                {
                    angleStart[i] = -3.15;
                    angleExtent[i] = 6.29;
                    minScore[i] = 0.8;
                    numMatches[i] = 5;
                    subPixel[i * 2] = 2;
                    subPixel[i * 2 + 1] = 1;
                    numLevels[i * 2] = 0;
                    numLevels[i * 2 + 1] = 0;
                    greediness[i] = 0.8;
                    scaleRMin[i] = 0.9;
                    scaleRMax[i] = 1.1;
                    scaleCMin[i] = 0.9;
                    scaleCMax[i] = 1.1;
                    modelIsChecked[i] = false;
                }
                maxOverlap = 0.2;
            }
        }
        public class SerialOutputPara
        {
            public bool isGige;
            public bool outputForm;
            public short intBit;
            public short floatBit;
            public bool NegativeMinus;
            public bool EraseZeroYes;
            public short FieldSeparator;
            public short RecordSeparator;
            public class SendDataInfor
            {
                public int row;
                public int funcID;
                public int datatype;
                public int idx;
                public SendDataInfor(int row_, int funcID_, int datatype_, int idx_)
                {
                    row = row_;
                    funcID = funcID_;
                    datatype = datatype_;
                    idx = idx_;
                }
            }
            public List<SendDataInfor> sendDataInfoList;
            public SerialOutputPara()
            {
                isGige = true;             //以太网模式
                outputForm = true;             //ASCII
                NegativeMinus = true;             //' - '表示负号
                EraseZeroYes = false;            //消零-NO
                intBit = 4;               //整数位数
                floatBit = 2;               //小数位数
                FieldSeparator = 1;               //字段分隔符(逗号)
                RecordSeparator = 1;               //记录分隔符(逗号)

                sendDataInfoList = new List<SendDataInfor>();
            }
            public void addSendDataInfoListPara(int row_, int funcID_, int datatype_, int idx_)
            {
                SendDataInfor tSD = new SendDataInfor(row_, funcID_, datatype_, idx_);
                sendDataInfoList.Add(tSD);
            }
            public void clear()
            {
                sendDataInfoList.Clear();
                isGige = true;             //以太网模式
                outputForm = true;             //ASCII
                NegativeMinus = true;             //' - '表示负号
                EraseZeroYes = false;            //消零-NO
                intBit = 4;               //整数位数
                floatBit = 2;               //小数位数
                FieldSeparator = 1;               //字段分隔符(逗号)
                RecordSeparator = 1;               //记录分隔符(逗号)
            }

        }

        public class SurfaceBasedMatchPara
        {

        }


        public ProCodeCls()
        {
            //boolData = new bool[MAX_OTHER_NUM];
            //shortData = new short[MAX_OTHER_NUM];
            //intData = new int[MAX_OTHER_NUM];
            //floatData = new float[MAX_OTHER_NUM];
            // doubleData = new double[MAX_DOUBLE_NUM];
            gTP = new ThresholdPara();
            gBRP = new BackgroundRemovePara();
            gMPP = new MorphologyProcessingPara();
            gMFP = new MedianFilterPara();
            gBP = new BlobPara();
            gSSP = new ShapeSearchPara();
            gASSP = new AnisoShapeSearchPara();
            gSOP = new SerialOutputPara();
        }
        public HObject inImage;
        public HObject outImage;
        public HObject showImg;
        public ThresholdPara gTP;
        public BackgroundRemovePara gBRP;
        public MorphologyProcessingPara gMPP;
        public MedianFilterPara gMFP;
        public BlobPara gBP;
        public ShapeSearchPara gSSP;
        public AnisoShapeSearchPara gASSP;
        public SerialOutputPara gSOP;
        public SurfaceBasedMatchPara gSBMP;

        public void clear()
        {
            FuncID = MainFunction.NullFBD;
            gTP.clear();
            gBRP.clear();
            gMPP.clear();
            gMFP.clear();
            gSOP.clear();
            gBP.clear();
            gSSP.clear();
            gASSP.clear();
        }
    }



    public delegate void ShowImgHandler();

    class UserCode
    {
        public HObject fileImages;
        public bool importantFlagIsRunning;
        public HTuple seconds1, seconds2;
        public HTuple secondsTotal;
        public HTuple secondsCommunication1, secondsCommunication2;
        public HTuple secondsTotalCommunication;
        public HTuple secondsShowImage1, secondsShowImage2;
        public HTuple secondsTotalShowImage;
        public static UserCode gUsCd;
        public ProCodeCls[] gProCd;
        public Dictionary<string, ProCodeCls.MainFunction> codeInfo;
        public Dictionary<ProCodeCls.MainFunction, string> codeInfoValToKey;
        public ShowImgHandler mSIH;
        public int showCurIdx;
        public String SendBuf;
        public byte[] SendByte;
        public int cBufferLength;
        public bool OKNG = true;
        public bool firstRun = true;

        private Thread RunThread;
        public bool tRun = false;
        public int[] isOverFlag;
        public static UserCode GetInstance()
        {
            if (gUsCd == null)
            {
                gUsCd = new UserCode();
            }
            return gUsCd;
        }

        public UserCode()
        {
            showCurIdx = 0;
            importantFlagIsRunning = false;
            seconds1 = 0;
            seconds2 = 0;
            secondsTotal = 0;
            secondsCommunication1 = 0;
            secondsCommunication2 = 0;
            secondsTotalCommunication = 0;
            secondsShowImage1 = 0;
            secondsShowImage2 = 0;
            secondsTotalShowImage = 0;
            gProCd = new ProCodeCls[20];
            isOverFlag = new int[20] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            SendBuf = "";
            cBufferLength = 0;
            codeInfo = new Dictionary<string, ProCodeCls.MainFunction>();
            codeInfoValToKey = new Dictionary<ProCodeCls.MainFunction, string>();
            gProCd[0] = new ProCodeCls();
            gProCd[0].FuncID = ProCodeCls.MainFunction.InputCameraInputFBD;
            gProCd[0].inImage = new HObject();
            gProCd[0].outImage = new HObject();
            for (int i = 1; i < 20; i++)
            {
                gProCd[i] = new ProCodeCls();
                gProCd[i].FuncID = ProCodeCls.MainFunction.NullFBD;
                gProCd[i].inImage = new HObject();
                gProCd[i].outImage = new HObject();
            }

            SetDefaultParameter();
            mSIH += new ShowImgHandler(showImgTest);
            //mSIH.Invoke();
        }

        ~UserCode()
        {
            if (fileImages!=null)
            {
                fileImages.Dispose();
            }
            
            stopThread();
        }

        public void thRun()
        {
            while (tRun)
            {

                try
                {
                    HOperatorSet.CountSeconds(out seconds1);
                    if (firstRun)
                    {
                        for (int i = 0; i < 20; i++)
                        {
                            switch (gProCd[i].FuncID)
                            {
                                case ProCodeCls.MainFunction.NullFBD:
                                    if (gProCd[i].outImage != null)
                                    {
                                        gProCd[i].outImage.Dispose();
                                    }
                                    gProCd[i].outImage = gProCd[i].inImage;
                                    break;
                                case ProCodeCls.MainFunction.InputCameraInputFBD:
                                    {

                                        gProCd[i].tCamInput = new CameraInputFBD();
                                        if (gProCd[i].outImage != null)
                                        {
                                            gProCd[i].outImage.Dispose();
                                        }
                                        // //HOperatorSet.CountSeconds(out ss1);
                                        // gProCd[i].tCamInput.GetImage(out gProCd[i].outImage,out ss3);
                                        gProCd[i].outImage = gProCd[i].tCamInput.GetImage();
                                        // //HOperatorSet.CountSeconds(out ss2);
                                        //// ss3 = ss2 - ss1;
                                        if (gProCd[i].showImg != null)
                                        {
                                            gProCd[i].showImg.Dispose();
                                        }
                                        gProCd[i].showImg = gProCd[i].outImage;

                                    }
                                    break;
                                case ProCodeCls.MainFunction.CalibrationThresholdFBD:
                                    {

                                        gProCd[i].tThreShold = new ThresholdFBD(gProCd[i].inImage,
                                            gProCd[i].gTP.minValue, gProCd[i].gTP.maxValue);
                                        if (gProCd[i].outImage != null)
                                        {
                                            gProCd[i].outImage.Dispose();
                                        }
                                        gProCd[i].outImage = gProCd[i].tThreShold.Thresholding(gProCd[i].inImage);
                                        if (gProCd[i].showImg != null)
                                        {
                                            gProCd[i].showImg.Dispose();
                                        }
                                        gProCd[i].showImg = gProCd[i].outImage;

                                    }
                                    break;
                                case ProCodeCls.MainFunction.CalibrationBackgroundRemoveFBD:
                                    {

                                        gProCd[i].tBackRemove = new BackgroundRemoveFBD(gProCd[i].inImage,
                                            gProCd[i].gBRP.grayValue, gProCd[i].gBRP.isAllColor);
                                        if (gProCd[i].outImage != null)
                                        {
                                            gProCd[i].outImage.Dispose();
                                        }

                                        gProCd[i].outImage = gProCd[i].tBackRemove.BackThresholding(gProCd[i].inImage);
                                        if (gProCd[i].showImg != null)
                                        {
                                            gProCd[i].showImg.Dispose();
                                        }
                                        gProCd[i].showImg = gProCd[i].outImage;


                                    }
                                    break;
                                case ProCodeCls.MainFunction.CalibrationMedianFilterFBD:
                                    {

                                        gProCd[i].tMeidanFilter = new MedianFilterFBD(gProCd[i].inImage,
                                            gProCd[i].gMFP.maskSize);
                                        if (gProCd[i].outImage != null)
                                        {
                                            gProCd[i].outImage.Dispose();
                                        }

                                        gProCd[i].outImage = gProCd[i].tMeidanFilter.MedianFiltering(gProCd[i].inImage);
                                        if (gProCd[i].showImg != null)
                                        {
                                            gProCd[i].showImg.Dispose();
                                        }
                                        gProCd[i].showImg = gProCd[i].outImage;
                                    }
                                    break;
                                case ProCodeCls.MainFunction.CalibrationMorphologyProcessingFBD:
                                    {

                                        gProCd[i].tMorPro = new MorphologyProcessingFBD(gProCd[i].inImage,
                                            gProCd[i].gMPP);
                                        if (gProCd[i].outImage != null)
                                        {
                                            gProCd[i].outImage.Dispose();
                                        }

                                        gProCd[i].outImage = gProCd[i].tMorPro.MorphologyProcessing(gProCd[i].inImage);
                                        if (gProCd[i].showImg != null)
                                        {
                                            gProCd[i].showImg.Dispose();
                                        }
                                        gProCd[i].showImg = gProCd[i].outImage;
                                    }
                                    break;
                                case ProCodeCls.MainFunction.MeasureBlobAnalysisFBD:
                                    {


                                        gProCd[i].tBlobAnalysis = new BlobAnalysisFBD(gProCd[i].inImage,
                                            gProCd[i].gBP, i);
                                        if (gProCd[i].outImage != null)
                                        {
                                            gProCd[i].outImage.Dispose();
                                        }
                                        gProCd[i].outImage = gProCd[i].tBlobAnalysis.BlobAnalysis(gProCd[i].inImage);
                                        if (gProCd[i].showImg != null)
                                        {
                                            gProCd[i].showImg.Dispose();
                                        }
                                        gProCd[i].showImg = gProCd[i].outImage;
                                    }
                                    break;
                                case ProCodeCls.MainFunction.MeasureShapeSearchFBD:
                                    {

                                        gProCd[i].tShpSrh = new ShapeSearchFBD(gProCd[i].inImage,
                                            gProCd[i].gSSP, i);
                                        if (gProCd[i].outImage != null)
                                        {
                                            gProCd[i].outImage.Dispose();
                                        }
                                        gProCd[i].outImage = gProCd[i].tShpSrh.ShapeSearching();
                                        if (gProCd[i].showImg != null)
                                        {
                                            gProCd[i].showImg.Dispose();
                                        }
                                        gProCd[i].showImg = gProCd[i].outImage;
                                    }
                                    break;
                                case ProCodeCls.MainFunction.MeasureSurfaceBasedMatchFBD://三维模板匹配
                                    {
                                        gProCd[i].tSBMatch = new SurfaceBasedMatchFBD(gProCd[i].inImage, gProCd[i].gSBMP,i);
                                        if (gProCd[i].outImage != null)
                                        {
                                            gProCd[i].outImage.Dispose();

                                        }
                                        gProCd[i].outImage = gProCd[i].tSBMatch.SurfaceBasedSearching();
                                        if (gProCd[i].showImg != null)
                                        {
                                            gProCd[i].showImg.Dispose();
                                        }
                                        gProCd[i].showImg = gProCd[i].outImage;

                                    }
                                    break;
                                case ProCodeCls.MainFunction.MeasureAnisoShapeSearchFBD:
                                    {
                                        gProCd[i].tAShpSrh = new AnisoShapeSearchFBD(gProCd[i].inImage,
                                            gProCd[i].gASSP, i);
                                        if (gProCd[i].outImage != null)
                                        {
                                            gProCd[i].outImage.Dispose();
                                        }
                                        gProCd[i].outImage = gProCd[i].tAShpSrh.AnisoShapeSearching();
                                        if (gProCd[i].showImg != null)
                                        {
                                            gProCd[i].showImg.Dispose();
                                        }
                                        gProCd[i].showImg = gProCd[i].outImage;
                                    }
                                    break;
                                case ProCodeCls.MainFunction.OutputSerialOutputFBD:
                                    {

                                        gProCd[i].tSrlOutput = new SerialOutputFBD(gProCd[i].gSOP);
                                        if (gProCd[i].outImage != null)
                                        {
                                            gProCd[i].outImage.Dispose();
                                        }
                                        gProCd[i].outImage = gProCd[i].inImage;
                                    }
                                    break;
                            }
                            if (i < 19)
                            {
                                if (gProCd[i + 1].inImage != null)
                                {
                                    gProCd[i + 1].inImage.Dispose();
                                }
                                gProCd[i + 1].inImage = gProCd[i].outImage;
                            }
                        }
                        firstRun = false;
                    }
                    else
                    {
                        for (int i = 0; i < 20; i++)
                        {
                            switch (gProCd[i].FuncID)
                            {
                                case ProCodeCls.MainFunction.NullFBD:
                                    if (gProCd[i].outImage != null)
                                    {
                                        gProCd[i].outImage.Dispose();
                                    }

                                    gProCd[i].outImage = gProCd[i].inImage;
                                    break;
                                case ProCodeCls.MainFunction.InputCameraInputFBD:
                                    {
                                        if (gProCd[i].outImage != null)
                                        {
                                            gProCd[i].outImage.Dispose();
                                        }
                                        gProCd[i].outImage = gProCd[i].tCamInput.GetImage();
                                        //HOperatorSet.CountSeconds(out ss1);
                                        //gProCd[i].tCamInput.GetImage(out gProCd[i].outImage);
                                        //// gProCd[i].outImage = gProCd[i].tCamInput.GetImage();
                                        //HOperatorSet.CountSeconds(out ss2);
                                        // ss3 = ss2 - ss1;
                                        if (gProCd[i].showImg != null)
                                        {
                                            gProCd[i].showImg.Dispose();
                                        }

                                        gProCd[i].showImg = gProCd[i].outImage;
                                        break;
                                    }
                                case ProCodeCls.MainFunction.CalibrationThresholdFBD:
                                    {
                                        if (gProCd[i].outImage != null)
                                        {
                                            gProCd[i].outImage.Dispose();
                                        }
                                        gProCd[i].outImage = gProCd[i].tThreShold.Thresholding(gProCd[i].inImage);
                                        if (gProCd[i].showImg != null)
                                        {
                                            gProCd[i].showImg.Dispose();
                                        }

                                        gProCd[i].showImg = gProCd[i].outImage;
                                        //                                     if (gProCd[i].showImg != null)
                                        //                                     {
                                        //                                         gProCd[i].showImg.Dispose();
                                        //                                     }
                                        //                                     HOperatorSet.RegionToBin(gProCd[i].outImage, out gProCd[i].showImg, 255, 0,
                                        //                                         Svision.GetMe().columnNumber, Svision.GetMe().rowNumber);
                                    }
                                    break;
                                case ProCodeCls.MainFunction.CalibrationBackgroundRemoveFBD:
                                    {
                                        if (gProCd[i].outImage != null)
                                        {
                                            gProCd[i].outImage.Dispose();
                                        }
                                        gProCd[i].outImage = gProCd[i].tBackRemove.BackThresholding(gProCd[i].inImage);
                                        if (gProCd[i].showImg != null)
                                        {
                                            gProCd[i].showImg.Dispose();
                                        }

                                        gProCd[i].showImg = gProCd[i].outImage;
                                    }
                                    break;
                                case ProCodeCls.MainFunction.CalibrationMedianFilterFBD:
                                    {
                                        if (gProCd[i].outImage != null)
                                        {
                                            gProCd[i].outImage.Dispose();
                                        }
                                        gProCd[i].outImage = gProCd[i].tMeidanFilter.MedianFiltering(gProCd[i].inImage);
                                        if (gProCd[i].showImg != null)
                                        {
                                            gProCd[i].showImg.Dispose();
                                        }

                                        gProCd[i].showImg = gProCd[i].outImage;
                                    }
                                    break;
                                case ProCodeCls.MainFunction.CalibrationMorphologyProcessingFBD:
                                    {
                                        if (gProCd[i].outImage != null)
                                        {
                                            gProCd[i].outImage.Dispose();
                                        }
                                        gProCd[i].outImage = gProCd[i].tMorPro.MorphologyProcessing(gProCd[i].inImage);
                                        if (gProCd[i].showImg != null)
                                        {
                                            gProCd[i].showImg.Dispose();
                                        }

                                        gProCd[i].showImg = gProCd[i].outImage;
                                    }
                                    break;
                                case ProCodeCls.MainFunction.MeasureBlobAnalysisFBD:
                                    {
                                        if (gProCd[i].outImage != null)
                                        {
                                            gProCd[i].outImage.Dispose();
                                        }
                                        gProCd[i].outImage = gProCd[i].tBlobAnalysis.BlobAnalysis(gProCd[i].inImage);
                                        if (gProCd[i].showImg != null)
                                        {
                                            gProCd[i].showImg.Dispose();
                                        }

                                        gProCd[i].showImg = gProCd[i].outImage;
                                    }
                                    break;
                                case ProCodeCls.MainFunction.MeasureShapeSearchFBD:
                                    {
                                        if (gProCd[i].outImage != null)
                                        {
                                            gProCd[i].outImage.Dispose();
                                        }
                                        gProCd[i].outImage = gProCd[i].tShpSrh.ShapeSearching();
                                        if (gProCd[i].showImg != null)
                                        {
                                            gProCd[i].showImg.Dispose();
                                        }
                                        gProCd[i].showImg = gProCd[i].outImage;
                                    }
                                    break;
                                case ProCodeCls.MainFunction.MeasureAnisoShapeSearchFBD:
                                    {
                                        if (gProCd[i].outImage != null)
                                        {
                                            gProCd[i].outImage.Dispose();
                                        }
                                        gProCd[i].outImage = gProCd[i].tAShpSrh.AnisoShapeSearching();
                                        if (gProCd[i].showImg != null)
                                        {
                                            gProCd[i].showImg.Dispose();
                                        }
                                        gProCd[i].showImg = gProCd[i].outImage;
                                    }
                                    break;
                                case ProCodeCls.MainFunction.OutputSerialOutputFBD:
                                    {

                                        if (gProCd[i].outImage != null)
                                        {
                                            gProCd[i].outImage.Dispose();
                                        }
                                        gProCd[i].outImage = gProCd[i].inImage;
                                    }
                                    break;
                            }
                            if (i < 19)
                            {
                                if (gProCd[i + 1].inImage != null)
                                {
                                    gProCd[i + 1].inImage.Dispose();
                                }

                                gProCd[i + 1].inImage = gProCd[i].outImage;
                            }
                        }
                    }
                    
                    //HOperatorSet.CountSeconds(out seconds2);
                    //secondsTotal = seconds2 - seconds1;
                    //  HOperatorSet.CountSeconds(out seconds1);
                    //for (int i = 19; i >= 0; i--)
                    //{
                    //    if (gProCd[i].FuncID != ProCodeCls.MainFunction.NullFBD
                    //        && gProCd[i].FuncID != ProCodeCls.MainFunction.OutputSerialOutputFBD)
                    //    {
                    //        showCurIdx = i;
                    //        break;
                    //    }
                    //}
                    HOperatorSet.CountSeconds(out seconds2);
                    secondsTotal = seconds2 - seconds1;
                    // showImageTestHobject = gProCd[showCurIdx].showImg;
                    //HOperatorSet.CountSeconds(out seconds2);
                    //secondsTotal = seconds2 - seconds1;

                    Svision.GetMe().BeginInvoke(mSIH);
                    // HOperatorSet.CountSeconds(out secondsShowImage2);
                    Thread.Sleep(ConfigInformation.GetInstance().tComCfg.TrigTime);

                    if (importantFlagIsRunning)
                    {
                        throw new Exception("定时触发模式下触发间隔需长于显示时间，请重新设置!");
                    }
                }
                catch (System.Exception ex)
                {

                    UserCode.GetInstance().stopThread();
                    //Svision.GetMe().baslerCamera.stopGrab();
                    UserCode.GetInstance().firstRun = true;
                    Svision.GetMe().StopAutoRunAndShowMessage(ex.Message);
                    importantFlagIsRunning = false;
                }
            }
        }
        public void changeModelCodeRun(int[] idxNum)
        {

            try
            {
                importantFlagIsRunning = true;
                for (int i=0;i<gProCd[idxNum[0]].gSSP.modelIsChecked.Length;i++)
                {
                    gProCd[idxNum[0]].gSSP.modelIsChecked[i]=false;
                }
                for (int i=1;i<idxNum.Length;i++)
                {
                    gProCd[idxNum[0]].gSSP.modelIsChecked[idxNum[i]]=true;
                }

                for (int i = 0; i < 20; i++)
                {
                    switch (gProCd[i].FuncID)
                    {
                        case ProCodeCls.MainFunction.NullFBD:
                            if (gProCd[i].outImage != null)
                            {
                                gProCd[i].outImage.Dispose();
                            }
                            gProCd[i].outImage = gProCd[i].inImage;
                            break;
                        case ProCodeCls.MainFunction.InputCameraInputFBD:
                            {
                                gProCd[i].tCamInput = new CameraInputFBD();
                                if (gProCd[i].outImage != null)
                                {
                                    gProCd[i].outImage.Dispose();
                                }
                                gProCd[i].outImage = gProCd[i].tCamInput.GetImage();
                                
                            }
                            break;
                        case ProCodeCls.MainFunction.CalibrationThresholdFBD:
                            {
                                gProCd[i].tThreShold = new ThresholdFBD(gProCd[i].inImage,
                                    gProCd[i].gTP.minValue, gProCd[i].gTP.maxValue);
                                if (gProCd[i].outImage != null)
                                {
                                    gProCd[i].outImage.Dispose();
                                }
                                gProCd[i].outImage = gProCd[i].tThreShold.Thresholding(gProCd[i].inImage);
                                
                            }
                            break;
                        case ProCodeCls.MainFunction.CalibrationBackgroundRemoveFBD:
                            {
                                gProCd[i].tBackRemove = new BackgroundRemoveFBD(gProCd[i].inImage,
                                    gProCd[i].gBRP.grayValue, gProCd[i].gBRP.isAllColor);
                                if (gProCd[i].outImage != null)
                                {
                                    gProCd[i].outImage.Dispose();
                                }
                                gProCd[i].outImage = gProCd[i].tBackRemove.BackThresholding(gProCd[i].inImage);
                                
                            }
                            break;
                        case ProCodeCls.MainFunction.CalibrationMedianFilterFBD:
                            {
                                gProCd[i].tMeidanFilter = new MedianFilterFBD(gProCd[i].inImage,
                                    gProCd[i].gMFP.maskSize);
                                if (gProCd[i].outImage != null)
                                {
                                    gProCd[i].outImage.Dispose();
                                }
                                gProCd[i].outImage = gProCd[i].tMeidanFilter.MedianFiltering(gProCd[i].inImage);
                                
                            }
                            break;
                        case ProCodeCls.MainFunction.CalibrationMorphologyProcessingFBD:
                            {
                                gProCd[i].tMorPro = new MorphologyProcessingFBD(gProCd[i].inImage,
                                    gProCd[i].gMPP);
                                if (gProCd[i].outImage != null)
                                {
                                    gProCd[i].outImage.Dispose();
                                }
                                gProCd[i].outImage = gProCd[i].tMorPro.MorphologyProcessing(gProCd[i].inImage);
                                
                            }
                            break;
                        case ProCodeCls.MainFunction.MeasureBlobAnalysisFBD:
                            {
                                gProCd[i].tBlobAnalysis = new BlobAnalysisFBD(gProCd[i].inImage,
                                    gProCd[i].gBP, i);
                                if (gProCd[i].outImage != null)
                                {
                                    gProCd[i].outImage.Dispose();
                                }
                                gProCd[i].outImage = gProCd[i].inImage;

                            }
                            break;
                        case ProCodeCls.MainFunction.MeasureShapeSearchFBD:
                            {
                                gProCd[i].tShpSrh = new ShapeSearchFBD(gProCd[i].inImage,
                                    gProCd[i].gSSP, i);
                                if (gProCd[i].outImage != null)
                                {
                                    gProCd[i].outImage.Dispose();
                                }
                                gProCd[i].outImage = gProCd[i].inImage;
                            }
                            break;
                        case ProCodeCls.MainFunction.MeasureAnisoShapeSearchFBD:
                            {
                                gProCd[i].tAShpSrh = new AnisoShapeSearchFBD(gProCd[i].inImage,
                                    gProCd[i].gASSP, i);
                                if (gProCd[i].outImage != null)
                                {
                                    gProCd[i].outImage.Dispose();
                                }
                                gProCd[i].outImage = gProCd[i].inImage;
                                
                            }
                            break;

                        case ProCodeCls.MainFunction.OutputSerialOutputFBD:
                            {
                                gProCd[i].tSrlOutput = new SerialOutputFBD(gProCd[i].gSOP);
                                if (gProCd[i].outImage != null)
                                {
                                    gProCd[i].outImage.Dispose();
                                }
                                gProCd[i].outImage = gProCd[i].inImage;
                            }
                            break;
                    }
                    if (i < 19)
                    {
                        if (gProCd[i + 1].inImage != null)
                        {
                            gProCd[i + 1].inImage.Dispose();
                        }
                        gProCd[i + 1].inImage = gProCd[i].outImage;
                    }
                }

                importantFlagIsRunning = false;

            }
            catch (System.Exception ex)
            {
                importantFlagIsRunning = false;
                throw new Exception(ex.Message);
            }


        }
        public void changeAnisoModelCodeRun(int[] idxNum)
        {

            try
            {
                importantFlagIsRunning = true;
                for (int i = 0; i < gProCd[idxNum[0]].gASSP.modelIsChecked.Length; i++)
                {
                    gProCd[idxNum[0]].gASSP.modelIsChecked[i] = false;
                }
                for (int i = 1; i < idxNum.Length; i++)
                {
                    gProCd[idxNum[0]].gASSP.modelIsChecked[idxNum[i]] = true;
                }
                for (int i = 0; i < 20; i++)
                {
                    switch (gProCd[i].FuncID)
                    {
                        case ProCodeCls.MainFunction.NullFBD:
                            if (gProCd[i].outImage != null)
                            {
                                gProCd[i].outImage.Dispose();
                            }
                            gProCd[i].outImage = gProCd[i].inImage;
                            break;
                        case ProCodeCls.MainFunction.InputCameraInputFBD:
                            {
                                gProCd[i].tCamInput = new CameraInputFBD();
                                if (gProCd[i].outImage != null)
                                {
                                    gProCd[i].outImage.Dispose();
                                }
                                gProCd[i].outImage = gProCd[i].tCamInput.GetImage();
                                if (gProCd[i].showImg != null)
                                {
                                    gProCd[i].showImg.Dispose();
                                }
                                gProCd[i].showImg = gProCd[i].outImage;
                            }
                            break;
                        case ProCodeCls.MainFunction.CalibrationThresholdFBD:
                            {
                                gProCd[i].tThreShold = new ThresholdFBD(gProCd[i].inImage,
                                    gProCd[i].gTP.minValue, gProCd[i].gTP.maxValue);
                                if (gProCd[i].outImage != null)
                                {
                                    gProCd[i].outImage.Dispose();
                                }
                                gProCd[i].outImage = gProCd[i].tThreShold.Thresholding(gProCd[i].inImage);
                                if (gProCd[i].showImg != null)
                                {
                                    gProCd[i].showImg.Dispose();
                                }
                                gProCd[i].showImg = gProCd[i].outImage;
                            }
                            break;
                        case ProCodeCls.MainFunction.CalibrationBackgroundRemoveFBD:
                            {
                                gProCd[i].tBackRemove = new BackgroundRemoveFBD(gProCd[i].inImage,
                                    gProCd[i].gBRP.grayValue, gProCd[i].gBRP.isAllColor);
                                if (gProCd[i].outImage != null)
                                {
                                    gProCd[i].outImage.Dispose();
                                }
                                gProCd[i].outImage = gProCd[i].tBackRemove.BackThresholding(gProCd[i].inImage);
                                if (gProCd[i].showImg != null)
                                {
                                    gProCd[i].showImg.Dispose();
                                }
                                gProCd[i].showImg = gProCd[i].outImage;
                            }
                            break;
                        case ProCodeCls.MainFunction.CalibrationMedianFilterFBD:
                            {
                                gProCd[i].tMeidanFilter = new MedianFilterFBD(gProCd[i].inImage,
                                    gProCd[i].gMFP.maskSize);
                                if (gProCd[i].outImage != null)
                                {
                                    gProCd[i].outImage.Dispose();
                                }
                                gProCd[i].outImage = gProCd[i].tMeidanFilter.MedianFiltering(gProCd[i].inImage);
                                if (gProCd[i].showImg != null)
                                {
                                    gProCd[i].showImg.Dispose();
                                }
                                gProCd[i].showImg = gProCd[i].outImage;
                            }
                            break;
                        case ProCodeCls.MainFunction.CalibrationMorphologyProcessingFBD:
                            {
                                gProCd[i].tMorPro = new MorphologyProcessingFBD(gProCd[i].inImage,
                                    gProCd[i].gMPP);
                                if (gProCd[i].outImage != null)
                                {
                                    gProCd[i].outImage.Dispose();
                                }
                                gProCd[i].outImage = gProCd[i].tMorPro.MorphologyProcessing(gProCd[i].inImage);
                                if (gProCd[i].showImg != null)
                                {
                                    gProCd[i].showImg.Dispose();
                                }
                                gProCd[i].showImg = gProCd[i].outImage;
                            }
                            break;
                        case ProCodeCls.MainFunction.MeasureBlobAnalysisFBD:
                            {
                                gProCd[i].tBlobAnalysis = new BlobAnalysisFBD(gProCd[i].inImage,
                                    gProCd[i].gBP, i);
                                if (gProCd[i].outImage != null)
                                {
                                    gProCd[i].outImage.Dispose();
                                }
                                gProCd[i].outImage = gProCd[i].inImage;

                            }
                            break;
                        case ProCodeCls.MainFunction.MeasureShapeSearchFBD:
                            {
                                gProCd[i].tShpSrh = new ShapeSearchFBD(gProCd[i].inImage,
                                    gProCd[i].gSSP, i);
                                if (gProCd[i].outImage != null)
                                {
                                    gProCd[i].outImage.Dispose();
                                }
                                gProCd[i].outImage = gProCd[i].inImage;
                            }
                            break;
                        case ProCodeCls.MainFunction.MeasureAnisoShapeSearchFBD:
                            {
                                gProCd[i].tAShpSrh = new AnisoShapeSearchFBD(gProCd[i].inImage,
                                    gProCd[i].gASSP, i);
                                if (gProCd[i].outImage != null)
                                {
                                    gProCd[i].outImage.Dispose();
                                }
                                gProCd[i].outImage = gProCd[i].inImage;

                            }
                            break;

                        case ProCodeCls.MainFunction.OutputSerialOutputFBD:
                            {
                                gProCd[i].tSrlOutput = new SerialOutputFBD(gProCd[i].gSOP);
                                if (gProCd[i].outImage != null)
                                {
                                    gProCd[i].outImage.Dispose();
                                }
                                gProCd[i].outImage = gProCd[i].inImage;
                            }
                            break;
                    }
                    if (i < 19)
                    {
                        if (gProCd[i + 1].inImage != null)
                        {
                            gProCd[i + 1].inImage.Dispose();
                        }
                        gProCd[i + 1].inImage = gProCd[i].outImage;
                    }
                }

                importantFlagIsRunning = false;

            }
            catch (System.Exception ex)
            {
                importantFlagIsRunning = false;
                throw new Exception(ex.Message);
            }
        }
        public void mainCodeRun()
        {
            try
            {
                importantFlagIsRunning = true;
                HOperatorSet.CountSeconds(out seconds1);
                if (firstRun)
                {
                    for (int i = 0; i < 20; i++)
                    {
                        switch (gProCd[i].FuncID)
                        {
                            case ProCodeCls.MainFunction.NullFBD:
                                if (gProCd[i].outImage != null)
                                {
                                    gProCd[i].outImage.Dispose();
                                }
                                gProCd[i].outImage = gProCd[i].inImage;
                                break;
                            case ProCodeCls.MainFunction.InputCameraInputFBD:
                                {
                                    gProCd[i].tCamInput = new CameraInputFBD();
                                    if (gProCd[i].outImage != null)
                                    {
                                        gProCd[i].outImage.Dispose();
                                    }
                                    gProCd[i].outImage = gProCd[i].tCamInput.GetImage();
                                    if (gProCd[i].showImg != null)
                                    {
                                        gProCd[i].showImg.Dispose();
                                    }
                                    gProCd[i].showImg = gProCd[i].outImage;
                                }
                                break;
                            case ProCodeCls.MainFunction.CalibrationThresholdFBD:
                                {
                                    gProCd[i].tThreShold = new ThresholdFBD(gProCd[i].inImage,
                                        gProCd[i].gTP.minValue, gProCd[i].gTP.maxValue);
                                    if (gProCd[i].outImage != null)
                                    {
                                        gProCd[i].outImage.Dispose();
                                    }
                                    gProCd[i].outImage = gProCd[i].tThreShold.Thresholding(gProCd[i].inImage);
                                    if (gProCd[i].showImg != null)
                                    {
                                        gProCd[i].showImg.Dispose();
                                    }
                                    gProCd[i].showImg = gProCd[i].outImage;
                                    //    HOperatorSet.RegionToBin(gProCd[i].outImage, out gProCd[i].showImg, 255, 0,
                                    //    Svision.GetMe().columnNumber, Svision.GetMe().rowNumber);
                                }
                                break;
                            case ProCodeCls.MainFunction.CalibrationBackgroundRemoveFBD:
                                {
                                    gProCd[i].tBackRemove = new BackgroundRemoveFBD(gProCd[i].inImage,
                                        gProCd[i].gBRP.grayValue, gProCd[i].gBRP.isAllColor);
                                    if (gProCd[i].outImage != null)
                                    {
                                        gProCd[i].outImage.Dispose();
                                    }
                                    gProCd[i].outImage = gProCd[i].tBackRemove.BackThresholding(gProCd[i].inImage);
                                    if (gProCd[i].showImg != null)
                                    {
                                        gProCd[i].showImg.Dispose();
                                    }
                                    gProCd[i].showImg = gProCd[i].outImage;
                                }
                                break;
                            case ProCodeCls.MainFunction.CalibrationMedianFilterFBD:
                                {
                                    gProCd[i].tMeidanFilter = new MedianFilterFBD(gProCd[i].inImage,
                                        gProCd[i].gMFP.maskSize);
                                    if (gProCd[i].outImage != null)
                                    {
                                        gProCd[i].outImage.Dispose();
                                    }
                                    gProCd[i].outImage = gProCd[i].tMeidanFilter.MedianFiltering(gProCd[i].inImage);
                                    if (gProCd[i].showImg != null)
                                    {
                                        gProCd[i].showImg.Dispose();
                                    }
                                    gProCd[i].showImg = gProCd[i].outImage;
                                }
                                break;
                            case ProCodeCls.MainFunction.CalibrationMorphologyProcessingFBD:
                                {
                                    gProCd[i].tMorPro = new MorphologyProcessingFBD(gProCd[i].inImage,
                                        gProCd[i].gMPP);
                                    if (gProCd[i].outImage != null)
                                    {
                                        gProCd[i].outImage.Dispose();
                                    }
                                    gProCd[i].outImage = gProCd[i].tMorPro.MorphologyProcessing(gProCd[i].inImage);
                                    if (gProCd[i].showImg != null)
                                    {
                                        gProCd[i].showImg.Dispose();
                                    }
                                    gProCd[i].showImg = gProCd[i].outImage;
                                }
                                break;
                            case ProCodeCls.MainFunction.MeasureBlobAnalysisFBD:
                                {
                                    gProCd[i].tBlobAnalysis = new BlobAnalysisFBD(gProCd[i].inImage,
                                        gProCd[i].gBP, i);
                                    if (gProCd[i].outImage != null)
                                    {
                                        gProCd[i].outImage.Dispose();
                                    }
                                    gProCd[i].outImage = gProCd[i].tBlobAnalysis.BlobAnalysis(gProCd[i].inImage);
                                    if (gProCd[i].showImg != null)
                                    {
                                        gProCd[i].showImg.Dispose();
                                    }
                                    gProCd[i].showImg = gProCd[i].outImage;
                                }
                                break;
                            case ProCodeCls.MainFunction.MeasureShapeSearchFBD:
                                {
                                    gProCd[i].tShpSrh = new ShapeSearchFBD(gProCd[i].inImage,
                                        gProCd[i].gSSP, i);
                                    if (gProCd[i].outImage != null)
                                    {
                                        gProCd[i].outImage.Dispose();
                                    }
                                    gProCd[i].outImage = gProCd[i].tShpSrh.ShapeSearching();
                                    if (gProCd[i].showImg != null)
                                    {
                                        gProCd[i].showImg.Dispose();
                                    }
                                    gProCd[i].showImg = gProCd[i].outImage;
                                }
                                break;
                            case ProCodeCls.MainFunction.MeasureAnisoShapeSearchFBD:
                                {
                                    gProCd[i].tAShpSrh = new AnisoShapeSearchFBD(gProCd[i].inImage,
                                        gProCd[i].gASSP, i);
                                    if (gProCd[i].outImage != null)
                                    {
                                        gProCd[i].outImage.Dispose();
                                    }
                                    gProCd[i].outImage = gProCd[i].tAShpSrh.AnisoShapeSearching();
                                    if (gProCd[i].showImg != null)
                                    {
                                        gProCd[i].showImg.Dispose();
                                    }
                                    gProCd[i].showImg = gProCd[i].outImage;
                                }
                                break;

                            case ProCodeCls.MainFunction.OutputSerialOutputFBD:
                                {
                                    gProCd[i].tSrlOutput = new SerialOutputFBD(gProCd[i].gSOP);
                                    if (gProCd[i].outImage != null)
                                    {
                                        gProCd[i].outImage.Dispose();
                                    }
                                    gProCd[i].outImage = gProCd[i].inImage;
                                    if (gProCd[i].tSrlOutput.outputForm)
                                    {
                                        SendBuf += gProCd[i].tSrlOutput.SendAsciiData();
                                        cBufferLength += (gProCd[i].gSOP.floatBit + gProCd[i].gSOP.intBit + 2) * gProCd[i].gSOP.sendDataInfoList.Count - 1;
                                        if (gProCd[i].gSOP.RecordSeparator != 0)
                                        {
                                            if (SendBuf.Length == cBufferLength + 1)
                                            {
                                                OKNG = true;
                                                cBufferLength += 1;
                                            }
                                            else
                                            {
                                                OKNG = false;
                                            }
                                        }
                                        else
                                        {
                                            if (SendBuf.Length == cBufferLength)
                                            {
                                                OKNG = true;

                                            }
                                            else
                                            {
                                                OKNG = false;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        gProCd[i].tSrlOutput.SendBinData(out SendByte);
                                    }
                                }
                                break;
                        }
                        if (i < 19)
                        {
                            if (gProCd[i + 1].inImage != null)
                            {
                                gProCd[i + 1].inImage.Dispose();
                            }
                            gProCd[i + 1].inImage = gProCd[i].outImage;
                        }
                    }
                    firstRun = false;

                }
                else
                {
                    for (int i = 0; i < 20; i++)
                    {
                        switch (gProCd[i].FuncID)
                        {
                            case ProCodeCls.MainFunction.NullFBD:
                                gProCd[i].outImage = gProCd[i].inImage;
                                break;
                            case ProCodeCls.MainFunction.InputCameraInputFBD:
                                {
                                    if (gProCd[i].outImage != null)
                                    {
                                        gProCd[i].outImage.Dispose();
                                    }
                                    gProCd[i].outImage = gProCd[i].tCamInput.GetImage();
                                    if (gProCd[i].showImg != null)
                                    {
                                        gProCd[i].showImg.Dispose();
                                    }
                                    gProCd[i].showImg = gProCd[i].outImage;
                                    //                                 basicClass.displayClear(Svision.GetMe().hvWindowHandle);
                                    //                                 basicClass.displayhobject(gProCd[i].outImage, Svision.GetMe().hvWindowHandle);
                                }
                                break;
                            case ProCodeCls.MainFunction.CalibrationThresholdFBD:
                                {
                                    if (gProCd[i].outImage != null)
                                    {
                                        gProCd[i].outImage.Dispose();
                                    }
                                    gProCd[i].outImage = gProCd[i].tThreShold.Thresholding(gProCd[i].inImage);
                                    if (gProCd[i].showImg != null)
                                    {
                                        gProCd[i].showImg.Dispose();
                                    }
                                    gProCd[i].showImg = gProCd[i].outImage;
                                    //                                 HOperatorSet.RegionToBin(gProCd[i].outImage, out gProCd[i].showImg, 255, 0,
                                    //                                         Svision.GetMe().columnNumber, Svision.GetMe().rowNumber);
                                }
                                break;
                            case ProCodeCls.MainFunction.CalibrationBackgroundRemoveFBD:
                                {
                                    if (gProCd[i].outImage != null)
                                    {
                                        gProCd[i].outImage.Dispose();
                                    }
                                    gProCd[i].outImage = gProCd[i].tBackRemove.BackThresholding(gProCd[i].inImage);
                                    if (gProCd[i].showImg != null)
                                    {
                                        gProCd[i].showImg.Dispose();
                                    }
                                    gProCd[i].showImg = gProCd[i].outImage;
                                }
                                break;
                            case ProCodeCls.MainFunction.CalibrationMedianFilterFBD:
                                {
                                    if (gProCd[i].outImage != null)
                                    {
                                        gProCd[i].outImage.Dispose();
                                    }
                                    gProCd[i].outImage = gProCd[i].tMeidanFilter.MedianFiltering(gProCd[i].inImage);
                                    if (gProCd[i].showImg != null)
                                    {
                                        gProCd[i].showImg.Dispose();
                                    }
                                    gProCd[i].showImg = gProCd[i].outImage;
                                }
                                break;
                            case ProCodeCls.MainFunction.CalibrationMorphologyProcessingFBD:
                                {
                                    if (gProCd[i].outImage != null)
                                    {
                                        gProCd[i].outImage.Dispose();
                                    }
                                    gProCd[i].outImage = gProCd[i].tMorPro.MorphologyProcessing(gProCd[i].inImage);
                                    if (gProCd[i].showImg != null)
                                    {
                                        gProCd[i].showImg.Dispose();
                                    }
                                    gProCd[i].showImg = gProCd[i].outImage;
                                }
                                break;
                            case ProCodeCls.MainFunction.MeasureBlobAnalysisFBD:
                                {
                                    if (gProCd[i].outImage != null)
                                    {
                                        gProCd[i].outImage.Dispose();
                                    }
                                    gProCd[i].outImage = gProCd[i].tBlobAnalysis.BlobAnalysis(gProCd[i].inImage);
                                    if (gProCd[i].showImg != null)
                                    {
                                        gProCd[i].showImg.Dispose();
                                    }
                                    gProCd[i].showImg = gProCd[i].outImage;
                                }
                                break;
                            case ProCodeCls.MainFunction.MeasureShapeSearchFBD:
                                {
                                    if (gProCd[i].outImage != null)
                                    {
                                        gProCd[i].outImage.Dispose();
                                    }
                                    gProCd[i].outImage = gProCd[i].tShpSrh.ShapeSearching();
                                    if (gProCd[i].showImg != null)
                                    {
                                        gProCd[i].showImg.Dispose();
                                    }
                                    gProCd[i].showImg = gProCd[i].outImage;
                                }
                                break;
                            case ProCodeCls.MainFunction.MeasureAnisoShapeSearchFBD:
                                {
                                    if (gProCd[i].outImage != null)
                                    {
                                        gProCd[i].outImage.Dispose();
                                    }
                                    gProCd[i].outImage = gProCd[i].tAShpSrh.AnisoShapeSearching();
                                    if (gProCd[i].showImg != null)
                                    {
                                        gProCd[i].showImg.Dispose();
                                    }
                                    gProCd[i].showImg = gProCd[i].outImage;
                                }
                                break;
                            case ProCodeCls.MainFunction.OutputSerialOutputFBD:
                                {
                                    if (gProCd[i].outImage != null)
                                    {
                                        gProCd[i].outImage.Dispose();
                                    }
                                    gProCd[i].outImage = gProCd[i].inImage;
                                    if (gProCd[i].tSrlOutput.outputForm)
                                    {
                                        SendBuf += gProCd[i].tSrlOutput.SendAsciiData();

                                        cBufferLength += (gProCd[i].gSOP.floatBit + gProCd[i].gSOP.intBit + 2) * gProCd[i].gSOP.sendDataInfoList.Count - 1;
                                        if (gProCd[i].gSOP.RecordSeparator != 0)
                                        {
                                            if (SendBuf.Length == cBufferLength + 1)
                                            {
                                                OKNG = true;
                                                cBufferLength += 1;
                                            }
                                            else
                                            {
                                                OKNG = false;
                                            }
                                        }
                                        else
                                        {
                                            if (SendBuf.Length == cBufferLength)
                                            {
                                                OKNG = true;

                                            }
                                            else
                                            {
                                                OKNG = false;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        gProCd[i].tSrlOutput.SendBinData(out SendByte);
                                    }
                                }
                                break;
                        }
                        if (i < 19)
                        {
                            if (gProCd[i + 1].inImage != null)
                            {
                                gProCd[i + 1].inImage.Dispose();
                            }
                            gProCd[i + 1].inImage = gProCd[i].outImage;
                        }
                    }
                }
                HOperatorSet.CountSeconds(out seconds2);
                secondsTotal = seconds2 - seconds1;
               

                //for (int i = 19; i > 0; i--)
                //{
                //    if (gProCd[i].FuncID != ProCodeCls.MainFunction.NullFBD
                //        && gProCd[i].FuncID != ProCodeCls.MainFunction.OutputSerialOutputFBD)
                //    {
                //        showCurIdx = i;
                //        break;
                //    }
                //}
                Svision.GetMe().BeginInvoke(mSIH);

            }
            catch (System.Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        public void FileCodeRun()
        {
            try
            {
                importantFlagIsRunning = true;
                HOperatorSet.CountSeconds(out seconds1);
                if (firstRun)
                {
                    for (int i = 0; i < 20; i++)
                    {
                        switch (gProCd[i].FuncID)
                        {
                            case ProCodeCls.MainFunction.NullFBD:
                                if (gProCd[i].outImage != null)
                                {
                                    gProCd[i].outImage.Dispose();
                                }
                                gProCd[i].outImage = gProCd[i].inImage;
                                break;

                            case ProCodeCls.MainFunction.InputFileInputFBD:
                                {
                                    gProCd[i].tFileInput = new FileInputFBD();
                                    if (gProCd[i].outImage != null)
                                    {
                                        gProCd[i].outImage.Dispose();
                                    }
                                    gProCd[i].outImage = gProCd[i].tFileInput.GetImage();
                                    if (gProCd[i].showImg != null)
                                    {
                                        gProCd[i].showImg.Dispose();
                                    }
                                    gProCd[i].showImg = gProCd[i].outImage;
                                }
                                break;
                            case ProCodeCls.MainFunction.CalibrationThresholdFBD:
                                {
                                    gProCd[i].tThreShold = new ThresholdFBD(gProCd[i].inImage,
                                        gProCd[i].gTP.minValue, gProCd[i].gTP.maxValue);
                                    if (gProCd[i].outImage != null)
                                    {
                                        gProCd[i].outImage.Dispose();
                                    }
                                    gProCd[i].outImage = gProCd[i].tThreShold.Thresholding(gProCd[i].inImage);
                                    if (gProCd[i].showImg != null)
                                    {
                                        gProCd[i].showImg.Dispose();
                                    }
                                    gProCd[i].showImg = gProCd[i].outImage;
                                    //    HOperatorSet.RegionToBin(gProCd[i].outImage, out gProCd[i].showImg, 255, 0,
                                    //    Svision.GetMe().columnNumber, Svision.GetMe().rowNumber);
                                }
                                break;
                            case ProCodeCls.MainFunction.CalibrationBackgroundRemoveFBD:
                                {
                                    gProCd[i].tBackRemove = new BackgroundRemoveFBD(gProCd[i].inImage,
                                        gProCd[i].gBRP.grayValue, gProCd[i].gBRP.isAllColor);
                                    if (gProCd[i].outImage != null)
                                    {
                                        gProCd[i].outImage.Dispose();
                                    }
                                    gProCd[i].outImage = gProCd[i].tBackRemove.BackThresholding(gProCd[i].inImage);
                                    if (gProCd[i].showImg != null)
                                    {
                                        gProCd[i].showImg.Dispose();
                                    }
                                    gProCd[i].showImg = gProCd[i].outImage;
                                }
                                break;
                            case ProCodeCls.MainFunction.CalibrationMedianFilterFBD:
                                {
                                    gProCd[i].tMeidanFilter = new MedianFilterFBD(gProCd[i].inImage,
                                        gProCd[i].gMFP.maskSize);
                                    if (gProCd[i].outImage != null)
                                    {
                                        gProCd[i].outImage.Dispose();
                                    }
                                    gProCd[i].outImage = gProCd[i].tMeidanFilter.MedianFiltering(gProCd[i].inImage);
                                    if (gProCd[i].showImg != null)
                                    {
                                        gProCd[i].showImg.Dispose();
                                    }
                                    gProCd[i].showImg = gProCd[i].outImage;
                                }
                                break;
                            case ProCodeCls.MainFunction.CalibrationMorphologyProcessingFBD:
                                {
                                    gProCd[i].tMorPro = new MorphologyProcessingFBD(gProCd[i].inImage,
                                        gProCd[i].gMPP);
                                    if (gProCd[i].outImage != null)
                                    {
                                        gProCd[i].outImage.Dispose();
                                    }
                                    gProCd[i].outImage = gProCd[i].tMorPro.MorphologyProcessing(gProCd[i].inImage);
                                    if (gProCd[i].showImg != null)
                                    {
                                        gProCd[i].showImg.Dispose();
                                    }
                                    gProCd[i].showImg = gProCd[i].outImage;
                                }
                                break;
                            case ProCodeCls.MainFunction.MeasureBlobAnalysisFBD:
                                {
                                    gProCd[i].tBlobAnalysis = new BlobAnalysisFBD(gProCd[i].inImage,
                                        gProCd[i].gBP, i);
                                    if (gProCd[i].outImage != null)
                                    {
                                        gProCd[i].outImage.Dispose();
                                    }
                                    gProCd[i].outImage = gProCd[i].tBlobAnalysis.BlobAnalysis(gProCd[i].inImage);
                                    if (gProCd[i].showImg != null)
                                    {
                                        gProCd[i].showImg.Dispose();
                                    }
                                    gProCd[i].showImg = gProCd[i].outImage;
                                }
                                break;
                            case ProCodeCls.MainFunction.MeasureShapeSearchFBD:
                                {
                                    gProCd[i].tShpSrh = new ShapeSearchFBD(gProCd[i].inImage,
                                        gProCd[i].gSSP, i);
                                    if (gProCd[i].outImage != null)
                                    {
                                        gProCd[i].outImage.Dispose();
                                    }
                                    gProCd[i].outImage = gProCd[i].tShpSrh.ShapeSearching();
                                    if (gProCd[i].showImg != null)
                                    {
                                        gProCd[i].showImg.Dispose();
                                    }
                                    gProCd[i].showImg = gProCd[i].outImage;
                                }
                                break;
                            case ProCodeCls.MainFunction.MeasureAnisoShapeSearchFBD:
                                {
                                    gProCd[i].tAShpSrh = new AnisoShapeSearchFBD(gProCd[i].inImage,
                                        gProCd[i].gASSP, i);
                                    if (gProCd[i].outImage != null)
                                    {
                                        gProCd[i].outImage.Dispose();
                                    }
                                    gProCd[i].outImage = gProCd[i].tAShpSrh.AnisoShapeSearching();
                                    if (gProCd[i].showImg != null)
                                    {
                                        gProCd[i].showImg.Dispose();
                                    }
                                    gProCd[i].showImg = gProCd[i].outImage;
                                }
                                break;

                            case ProCodeCls.MainFunction.OutputSerialOutputFBD:
                                {
                                    gProCd[i].tSrlOutput = new SerialOutputFBD(gProCd[i].gSOP);
                                    if (gProCd[i].outImage != null)
                                    {
                                        gProCd[i].outImage.Dispose();
                                    }
                                    gProCd[i].outImage = gProCd[i].inImage;
                                   
                                }
                                break;
                        }
                        if (i < 19)
                        {
                            if (gProCd[i + 1].inImage != null)
                            {
                                gProCd[i + 1].inImage.Dispose();
                            }
                            gProCd[i + 1].inImage = gProCd[i].outImage;
                        }
                    }
                    firstRun = false;

                }
                else
                {
                    for (int i = 0; i < 20; i++)
                    {
                        switch (gProCd[i].FuncID)
                        {
                            case ProCodeCls.MainFunction.NullFBD:
                                gProCd[i].outImage = gProCd[i].inImage;
                                break;
                            case ProCodeCls.MainFunction.InputFileInputFBD:
                                {
                                    if (gProCd[i].outImage != null)
                                    {
                                        gProCd[i].outImage.Dispose();
                                    }
                                    gProCd[i].outImage = gProCd[i].tFileInput.GetImage();
                                    if (gProCd[i].showImg != null)
                                    {
                                        gProCd[i].showImg.Dispose();
                                    }
                                    gProCd[i].showImg = gProCd[i].outImage;
                                }
                                break;
                            case ProCodeCls.MainFunction.CalibrationThresholdFBD:
                                {
                                    if (gProCd[i].outImage != null)
                                    {
                                        gProCd[i].outImage.Dispose();
                                    }
                                    gProCd[i].outImage = gProCd[i].tThreShold.Thresholding(gProCd[i].inImage);
                                    if (gProCd[i].showImg != null)
                                    {
                                        gProCd[i].showImg.Dispose();
                                    }
                                    gProCd[i].showImg = gProCd[i].outImage;
                                    //                                 HOperatorSet.RegionToBin(gProCd[i].outImage, out gProCd[i].showImg, 255, 0,
                                    //                                         Svision.GetMe().columnNumber, Svision.GetMe().rowNumber);
                                }
                                break;
                            case ProCodeCls.MainFunction.CalibrationBackgroundRemoveFBD:
                                {
                                    if (gProCd[i].outImage != null)
                                    {
                                        gProCd[i].outImage.Dispose();
                                    }
                                    gProCd[i].outImage = gProCd[i].tBackRemove.BackThresholding(gProCd[i].inImage);
                                    if (gProCd[i].showImg != null)
                                    {
                                        gProCd[i].showImg.Dispose();
                                    }
                                    gProCd[i].showImg = gProCd[i].outImage;
                                }
                                break;
                            case ProCodeCls.MainFunction.CalibrationMedianFilterFBD:
                                {
                                    if (gProCd[i].outImage != null)
                                    {
                                        gProCd[i].outImage.Dispose();
                                    }
                                    gProCd[i].outImage = gProCd[i].tMeidanFilter.MedianFiltering(gProCd[i].inImage);
                                    if (gProCd[i].showImg != null)
                                    {
                                        gProCd[i].showImg.Dispose();
                                    }
                                    gProCd[i].showImg = gProCd[i].outImage;
                                }
                                break;
                            case ProCodeCls.MainFunction.CalibrationMorphologyProcessingFBD:
                                {
                                    if (gProCd[i].outImage != null)
                                    {
                                        gProCd[i].outImage.Dispose();
                                    }
                                    gProCd[i].outImage = gProCd[i].tMorPro.MorphologyProcessing(gProCd[i].inImage);
                                    if (gProCd[i].showImg != null)
                                    {
                                        gProCd[i].showImg.Dispose();
                                    }
                                    gProCd[i].showImg = gProCd[i].outImage;
                                }
                                break;
                            case ProCodeCls.MainFunction.MeasureBlobAnalysisFBD:
                                {
                                    if (gProCd[i].outImage != null)
                                    {
                                        gProCd[i].outImage.Dispose();
                                    }
                                    gProCd[i].outImage = gProCd[i].tBlobAnalysis.BlobAnalysis(gProCd[i].inImage);
                                    if (gProCd[i].showImg != null)
                                    {
                                        gProCd[i].showImg.Dispose();
                                    }
                                    gProCd[i].showImg = gProCd[i].outImage;
                                }
                                break;
                            case ProCodeCls.MainFunction.MeasureShapeSearchFBD:
                                {
                                    if (gProCd[i].outImage != null)
                                    {
                                        gProCd[i].outImage.Dispose();
                                    }
                                    gProCd[i].outImage = gProCd[i].tShpSrh.ShapeSearching();
                                    if (gProCd[i].showImg != null)
                                    {
                                        gProCd[i].showImg.Dispose();
                                    }
                                    gProCd[i].showImg = gProCd[i].outImage;
                                }
                                break;
                            case ProCodeCls.MainFunction.MeasureAnisoShapeSearchFBD:
                                {
                                    if (gProCd[i].outImage != null)
                                    {
                                        gProCd[i].outImage.Dispose();
                                    }
                                    gProCd[i].outImage = gProCd[i].tAShpSrh.AnisoShapeSearching();
                                    if (gProCd[i].showImg != null)
                                    {
                                        gProCd[i].showImg.Dispose();
                                    }
                                    gProCd[i].showImg = gProCd[i].outImage;
                                }
                                break;
                            case ProCodeCls.MainFunction.OutputSerialOutputFBD:
                                {
                                    if (gProCd[i].outImage != null)
                                    {
                                        gProCd[i].outImage.Dispose();
                                    }
                                    gProCd[i].outImage = gProCd[i].inImage;
                                    
                                }
                                break;
                        }
                        if (i < 19)
                        {
                            if (gProCd[i + 1].inImage != null)
                            {
                                gProCd[i + 1].inImage.Dispose();
                            }
                            gProCd[i + 1].inImage = gProCd[i].outImage;
                        }
                    }
                }
                HOperatorSet.CountSeconds(out seconds2);
                secondsTotal = seconds2 - seconds1;


                //for (int i = 19; i > 0; i--)
                //{
                //    if (gProCd[i].FuncID != ProCodeCls.MainFunction.NullFBD
                //        && gProCd[i].FuncID != ProCodeCls.MainFunction.OutputSerialOutputFBD)
                //    {
                //        showCurIdx = i;
                //        break;
                //    }
                //}
                Svision.GetMe().BeginInvoke(mSIH);

            }
            catch (System.Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        public void getImageFromProcess(out HObject img, int idx)
        {
            for (int i = 0; i < idx; i++)
            {
                switch (gProCd[i].FuncID)
                {
                    case ProCodeCls.MainFunction.NullFBD:
                        gProCd[i].outImage = gProCd[i].inImage;
                        break;
                    case ProCodeCls.MainFunction.InputCameraInputFBD:
                        {
                            gProCd[i].tCamInput = new CameraInputFBD();
                            if (gProCd[i].outImage != null)
                            {
                                gProCd[i].outImage.Dispose();
                            }
                            gProCd[i].outImage = gProCd[i].tCamInput.GetImage();
                            // gProCd[i].tCamInput.stopGrab();
                        }
                        break;
                    case ProCodeCls.MainFunction.InputFileInputFBD:
                        {
                            gProCd[i].tFileInput = new FileInputFBD();
                            if (gProCd[i].outImage != null)
                            {
                                gProCd[i].outImage.Dispose();
                            }
                            gProCd[i].outImage = gProCd[i].tFileInput.GetImage();
                            // gProCd[i].tCamInput.stopGrab();
                        }
                        break;
                    case ProCodeCls.MainFunction.CalibrationThresholdFBD:
                        {
                            gProCd[i].tThreShold = new ThresholdFBD(gProCd[i].inImage,
                                gProCd[i].gTP.minValue, gProCd[i].gTP.maxValue);
                            if (gProCd[i].outImage != null)
                            {
                                gProCd[i].outImage.Dispose();
                            }
                            gProCd[i].outImage = gProCd[i].tThreShold.Thresholding(gProCd[i].inImage);
                        }
                        break;
                    case ProCodeCls.MainFunction.CalibrationBackgroundRemoveFBD:
                        {
                            gProCd[i].tBackRemove = new BackgroundRemoveFBD(gProCd[i].inImage,
                                gProCd[i].gBRP.grayValue, gProCd[i].gBRP.isAllColor);
                            if (gProCd[i].outImage != null)
                            {
                                gProCd[i].outImage.Dispose();
                            }
                            gProCd[i].outImage = gProCd[i].tBackRemove.BackThresholding(gProCd[i].inImage);
                        }
                        break;
                    case ProCodeCls.MainFunction.CalibrationMedianFilterFBD:
                        {
                            gProCd[i].tMeidanFilter = new MedianFilterFBD(gProCd[i].inImage,
                                gProCd[i].gMFP.maskSize);
                            if (gProCd[i].outImage != null)
                            {
                                gProCd[i].outImage.Dispose();
                            }
                            gProCd[i].outImage = gProCd[i].tMeidanFilter.MedianFiltering(gProCd[i].inImage);
                        }
                        break;
                    case ProCodeCls.MainFunction.CalibrationMorphologyProcessingFBD:
                        {
                            gProCd[i].tMorPro = new MorphologyProcessingFBD(gProCd[i].inImage,
                                gProCd[i].gMPP);
                            if (gProCd[i].outImage != null)
                            {
                                gProCd[i].outImage.Dispose();
                            }
                            gProCd[i].outImage = gProCd[i].tMorPro.MorphologyProcessing(gProCd[i].inImage);
                        }
                        break;
                    case ProCodeCls.MainFunction.MeasureBlobAnalysisFBD:
                        {
                            gProCd[i].tBlobAnalysis = new BlobAnalysisFBD(gProCd[i].inImage,
                                gProCd[i].gBP, i);
                            if (gProCd[i].outImage != null)
                            {
                                gProCd[i].outImage.Dispose();
                            }
                            gProCd[i].outImage = gProCd[i].tBlobAnalysis.BlobAnalysis(gProCd[i].inImage);
                        }
                        break;
                    case ProCodeCls.MainFunction.MeasureShapeSearchFBD:
                        {
                            gProCd[i].tShpSrh = new ShapeSearchFBD(gProCd[i].inImage,
                                gProCd[i].gSSP, i);
                            if (gProCd[i].outImage != null)
                            {
                                gProCd[i].outImage.Dispose();
                            }
                            gProCd[i].outImage = gProCd[i].tShpSrh.ShapeSearching();
                        }
                        break;
                    case ProCodeCls.MainFunction.MeasureAnisoShapeSearchFBD:
                        {
                            gProCd[i].tAShpSrh = new AnisoShapeSearchFBD(gProCd[i].inImage,
                                gProCd[i].gASSP, i);
                            if (gProCd[i].outImage != null)
                            {
                                gProCd[i].outImage.Dispose();
                            }
                            gProCd[i].outImage = gProCd[i].tAShpSrh.AnisoShapeSearching();
                        }
                        break;

                    case ProCodeCls.MainFunction.OutputSerialOutputFBD:
                        {
                            gProCd[i].tSrlOutput = new SerialOutputFBD(gProCd[i].gSOP);
                            if (gProCd[i].tSrlOutput.outputForm)
                            {
                                SendBuf += gProCd[i].tSrlOutput.SendAsciiData();
                            }
                            else
                            {
                                gProCd[i].tSrlOutput.SendBinData(out SendByte);
                            }
                        }
                        break;
                }
                if (gProCd[i + 1].inImage != null)
                {
                    gProCd[i + 1].inImage.Dispose();
                }
                gProCd[i + 1].inImage = gProCd[i].outImage;
            }
            img = gProCd[idx - 1].outImage;
        }
        public void showImgTest()
        {
            importantFlagIsRunning = true;
            int tmpRowN, tmpColN;
            HObject tmpImg;
            try
            {
                secondsShowImage1 = 0;
                secondsShowImage2 = 0;
                secondsTotalShowImage = 0;
                HOperatorSet.CountSeconds(out secondsShowImage1);
                Svision.GetMe().listBoxProcess.SelectedIndex = UserCode.GetInstance().showCurIdx;
                basicClass.getImageSize(gProCd[showCurIdx].showImg,
                out tmpRowN, out tmpColN);
                Svision.GetMe().rowNumber = tmpRowN;
                Svision.GetMe().columnNumber = tmpColN;
                double widRat = Svision.GetMe().pictureBoxShowImage.Width / ((double)Svision.GetMe().columnNumber);
                double heiRat = Svision.GetMe().pictureBoxShowImage.Height / ((double)Svision.GetMe().rowNumber);
                double resizerate = widRat < heiRat ? widRat : heiRat;
                if (Svision.GetMe().checkBoxDoNotShowImage.Checked == false)
                {
              //      Svision.GetMe().image = gProCd[showCurIdx].showImg;
                    basicClass.resizeImage(gProCd[showCurIdx].showImg, out tmpImg, resizerate);
                    Svision.GetMe().img = tmpImg;
                    basicClass.displayhobject(Svision.GetMe().img, Svision.GetMe().hvWindowHandle);
                    tmpImg.Dispose();

                }
                else
                {
                    basicClass.displayClear(Svision.GetMe().hvWindowHandle);
                }
                if (Svision.GetMe().oriPictureBoxShowImageWidth != Svision.GetMe().pictureBoxShowImage.Width)
                {
                    basicClass.displayClear(Svision.GetMe().hvWindowHandle);
                }
                if (Svision.GetMe().oriPictureBoxShowImageHeight != Svision.GetMe().pictureBoxShowImage.Height)
                {
                    basicClass.displayClear(Svision.GetMe().hvWindowHandle);
                }
                // basicClass.displayClear(Svision.GetMe().hvWindowHandle);

                Svision.GetMe().textBoxResultShow.Text = " ";
                Svision.GetMe().textBoxTime.Text = System.String.Format("{0:f}", secondsTotal[0].D * 1000.0) + "ms";
                switch (UserCode.GetInstance().gProCd[showCurIdx].FuncID)
                {
                    case ProCodeCls.MainFunction.InputCameraInputFBD:
                        break;
                    case ProCodeCls.MainFunction.CalibrationThresholdFBD:
                        break;
                    case ProCodeCls.MainFunction.CalibrationBackgroundRemoveFBD:
                        // UserCode.GetInstance().gProCd[showCurIdx].tBackRemove.showImageFun();
                        break;
                    case ProCodeCls.MainFunction.CalibrationMedianFilterFBD:
                        break;
                    case ProCodeCls.MainFunction.MeasureBlobAnalysisFBD:

                        UserCode.GetInstance().gProCd[showCurIdx].tBlobAnalysis.ShowImageFunc();
                        break;
                    case ProCodeCls.MainFunction.MeasureShapeSearchFBD:
                        UserCode.GetInstance().gProCd[showCurIdx].tShpSrh.ShowImageFunc();
                        break;
                    case ProCodeCls.MainFunction.MeasureAnisoShapeSearchFBD:
                        UserCode.GetInstance().gProCd[showCurIdx].tAShpSrh.ShowImageFunc();
                        break;
                    default:
                        break;
                }
                HOperatorSet.CountSeconds(out secondsShowImage2);
                secondsTotalShowImage = secondsShowImage2 - secondsShowImage1;
                Svision.GetMe().textBoxShowTime.Text = System.String.Format("{0:f}", secondsTotalShowImage[0].D * 1000.0) + "ms";
                importantFlagIsRunning = false;
            }
            catch (System.Exception ex)
            {
                HOperatorSet.CountSeconds(out secondsShowImage2);
                secondsTotalShowImage = secondsShowImage2 - secondsShowImage1;
                Svision.GetMe().textBoxShowTime.Text = System.String.Format("{0:f}", secondsTotalShowImage[0].D * 1000.0) + "ms";
                importantFlagIsRunning = false;
                //throw new Exception(ex.Message);
            }

        }

        public void startThread()
        {
            tRun = true;
            RunThread = new Thread(thRun);
            RunThread.IsBackground = false;
            RunThread.Start();

        }

        public void stopThread()
        {
            tRun = false;
        }

        private void SetDefaultParameter()
        {
            KeyToValueInit();
            ValueToKeyInit();
        }

        private void KeyToValueInit()
        {
            codeInfo.Add(Properties.Resources.NullFBD, ProCodeCls.MainFunction.NullFBD);
            codeInfo.Add(Properties.Resources.InputUnitCameraInput, ProCodeCls.MainFunction.InputCameraInputFBD);
            codeInfo.Add(Properties.Resources.InputUnitFileInput, ProCodeCls.MainFunction.InputFileInputFBD);
            codeInfo.Add(Properties.Resources.CalibrationUnitThreshold, ProCodeCls.MainFunction.CalibrationThresholdFBD);
            codeInfo.Add(Properties.Resources.CalibrationUnitBackgroundRemove, ProCodeCls.MainFunction.CalibrationBackgroundRemoveFBD);
            codeInfo.Add(Properties.Resources.CalibrationUnitHistogramEqualization, ProCodeCls.MainFunction.CalibrationHistogramEqualizationFBD);
            codeInfo.Add(Properties.Resources.CalibrationUnitSmooth, ProCodeCls.MainFunction.CalibrationSmoothFBD);
            codeInfo.Add(Properties.Resources.CalibrationUnitColorConversion, ProCodeCls.MainFunction.CalibrationColorConversionFBD);
            codeInfo.Add(Properties.Resources.CalibrationUnitMedianFilter, ProCodeCls.MainFunction.CalibrationMedianFilterFBD);
            codeInfo.Add(Properties.Resources.CalibrationUnitFreqDomainFilter, ProCodeCls.MainFunction.CalibrationFreqDomainFilterFBD);
            codeInfo.Add(Properties.Resources.CalibrationUnitSharpenFilter, ProCodeCls.MainFunction.CalibrationSharpenFilterFBD);
            codeInfo.Add(Properties.Resources.CalibrationUnitMorphologyProcessing, ProCodeCls.MainFunction.CalibrationMorphologyProcessingFBD);
            codeInfo.Add(Properties.Resources.MeasureUnitAnisoShapeSearch, ProCodeCls.MainFunction.MeasureAnisoShapeSearchFBD);
            codeInfo.Add(Properties.Resources.MeasureUnitShapeSearch, ProCodeCls.MainFunction.MeasureShapeSearchFBD);
            codeInfo.Add(Properties.Resources.MeasureUnitBlobAnalasis, ProCodeCls.MainFunction.MeasureBlobAnalysisFBD);
            codeInfo.Add(Properties.Resources.MeasureUnitReadCode, ProCodeCls.MainFunction.MeasureReadCodeFBD);
            codeInfo.Add(Properties.Resources.MeasureUnitEdgeScan, ProCodeCls.MainFunction.MeasureEdgeScanFBD);
            codeInfo.Add(Properties.Resources.MeasureUnitDefectDetect, ProCodeCls.MainFunction.MeasureDefectDetectFBD);
            codeInfo.Add(Properties.Resources.MeasureUnitCharacter, ProCodeCls.MainFunction.MeasureCharacterFBD);
            codeInfo.Add(Properties.Resources.OutputUnitSerialOutput, ProCodeCls.MainFunction.OutputSerialOutputFBD);
        }

        public void CalibrationMedianFilterInit(int idx)
        {
            gProCd[idx].gMFP.maskSize = 0;                 //掩模大小  3*3
        }
        private void ValueToKeyInit()
        {
            codeInfoValToKey.Add(ProCodeCls.MainFunction.NullFBD, Properties.Resources.NullFBD);
            codeInfoValToKey.Add(ProCodeCls.MainFunction.InputCameraInputFBD, Properties.Resources.InputUnitCameraInput);
            codeInfoValToKey.Add(ProCodeCls.MainFunction.InputFileInputFBD, Properties.Resources.InputUnitFileInput);
            codeInfoValToKey.Add(ProCodeCls.MainFunction.CalibrationThresholdFBD, Properties.Resources.CalibrationUnitThreshold);
            codeInfoValToKey.Add(ProCodeCls.MainFunction.CalibrationBackgroundRemoveFBD, Properties.Resources.CalibrationUnitBackgroundRemove);
            codeInfoValToKey.Add(ProCodeCls.MainFunction.CalibrationHistogramEqualizationFBD, Properties.Resources.CalibrationUnitHistogramEqualization);
            codeInfoValToKey.Add(ProCodeCls.MainFunction.CalibrationSmoothFBD, Properties.Resources.CalibrationUnitSmooth);
            codeInfoValToKey.Add(ProCodeCls.MainFunction.CalibrationColorConversionFBD, Properties.Resources.CalibrationUnitColorConversion);
            codeInfoValToKey.Add(ProCodeCls.MainFunction.CalibrationMedianFilterFBD, Properties.Resources.CalibrationUnitMedianFilter);
            codeInfoValToKey.Add(ProCodeCls.MainFunction.CalibrationFreqDomainFilterFBD, Properties.Resources.CalibrationUnitFreqDomainFilter);
            codeInfoValToKey.Add(ProCodeCls.MainFunction.CalibrationSharpenFilterFBD, Properties.Resources.CalibrationUnitSharpenFilter);
            codeInfoValToKey.Add(ProCodeCls.MainFunction.CalibrationMorphologyProcessingFBD, Properties.Resources.CalibrationUnitMorphologyProcessing);
            codeInfoValToKey.Add(ProCodeCls.MainFunction.MeasureAnisoShapeSearchFBD, Properties.Resources.MeasureUnitAnisoShapeSearch);
            codeInfoValToKey.Add(ProCodeCls.MainFunction.MeasureShapeSearchFBD, Properties.Resources.MeasureUnitShapeSearch);
            codeInfoValToKey.Add(ProCodeCls.MainFunction.MeasureBlobAnalysisFBD, Properties.Resources.MeasureUnitBlobAnalasis);
            codeInfoValToKey.Add(ProCodeCls.MainFunction.MeasureReadCodeFBD, Properties.Resources.MeasureUnitReadCode);
            codeInfoValToKey.Add(ProCodeCls.MainFunction.MeasureEdgeScanFBD, Properties.Resources.MeasureUnitEdgeScan);
            codeInfoValToKey.Add(ProCodeCls.MainFunction.MeasureDefectDetectFBD, Properties.Resources.MeasureUnitDefectDetect);
            codeInfoValToKey.Add(ProCodeCls.MainFunction.MeasureCharacterFBD, Properties.Resources.MeasureUnitCharacter);

            codeInfoValToKey.Add(ProCodeCls.MainFunction.OutputSerialOutputFBD, Properties.Resources.OutputUnitSerialOutput);
        }

        public void CalibrationThresholdInit(int idx)
        {
            gProCd[idx].gTP.maxValue = 200;
            gProCd[idx].gTP.minValue = 50;


        }
        public void CalibrationBackgroundRemoveInit(int idx)
        {
            gProCd[idx].gBRP.grayValue[0] = 255;//MaxGray
            gProCd[idx].gBRP.grayValue[1] = 0;//MinGray
            gProCd[idx].gBRP.grayValue[2] = 255;//MaxGrayR
            gProCd[idx].gBRP.grayValue[3] = 0;//MinGrayR
            gProCd[idx].gBRP.grayValue[4] = 255;//MaxGrayG
            gProCd[idx].gBRP.grayValue[5] = 0;//MinGrayG
            gProCd[idx].gBRP.grayValue[6] = 255;//MaxGrayB
            gProCd[idx].gBRP.grayValue[7] = 0;//MinGrayB


            gProCd[idx].gBRP.isAllColor = true;

        }
        public void CalibrationMorphologyProcessingInit(int idx)
        {
            gProCd[idx].gMPP.processID = 0;
            gProCd[idx].gMPP.elementID = 0;
            gProCd[idx].gMPP.width = 11;
            gProCd[idx].gMPP.height = 11;
            gProCd[idx].gMPP.radius = 3.5;
        }
        public void BlobAnalysisInit(int idx)
        {
            gProCd[idx].gBP.showOutputResultStr = new string[36] { "结果轮廓","目标个数","面积","重心Y","重心X","区域宽度","区域高度","区域左上角Y坐标","区域左上角X坐标","区域右下角Y坐标","区域右下角X坐标",
                                                    "圆形近似度","紧密度","轮廓长度","凸性","矩形近似度","等效椭圆长轴半径长度","等效椭圆短轴半径长度","等效椭圆方向",
                                                    "椭圆参数：长短轴比值","最小外接圆半径","最大内接圆半径","最大内接矩形宽度","最大内接矩形高度","多边形边数","区域内洞数",
                                                       "所有洞的面积","最大直径"," 区域方向","最小外接矩形方向","最小外接矩形长度","最小外接矩形宽度",
                                                      "区域灰度最小值","区域灰度最大值","区域灰度平均值","区域灰度标准差"};
            // gProCd[idx].gBP.showOutputResultFlag = new bool[36];
            gProCd[idx].gBP.blobAnalysisOperationFlag = new bool[0];
            gProCd[idx].gBP.blobAnalysisOperationStr = new string[0];
            for (int i = 0; i < 36; i++)
            {
                gProCd[idx].gBP.showOutputResultFlag[i] = true;
            }
            gProCd[idx].gBP.outputIDIsChecked = new bool[34];
            for (int i = 0; i < 34; i++)
            {
                gProCd[idx].gBP.outputIDIsChecked[i] = true;
            }
            if (Svision.GetMe().baslerCamera.getChannelNumber() == 1)
            {
                gProCd[idx].gBP.isColor = false;
            }
            else
            {
                gProCd[idx].gBP.isColor = true;//isColor
            }
            gProCd[idx].gBP.isAutoSegment = true;
            gProCd[idx].gBP.MaxGrayValue = 200;
            gProCd[idx].gBP.MinGrayValue = 100;
            gProCd[idx].gBP.isAutoSegmentMethod1 = false;
            gProCd[idx].gBP.isAutoSegmentMethod2 = false;
            gProCd[idx].gBP.isAutoSegmentMethod3 = true;
            gProCd[idx].gBP.autoSegmentMethod1Para1 = 2.0F;
            gProCd[idx].gBP.autoSegmentMethod2Para1 = 15;
            gProCd[idx].gBP.autoSegmentMethod2Para2 = 0.2F;
            gProCd[idx].gBP.autoSegmentMethod2Para3 = 2;
            gProCd[idx].gBP.autoSegmentMethod2Para4 = 0;
            gProCd[idx].gBP.segmentShow = 0;
            for (int i = 0; i < ProCodeCls.BlobPara.MAX_COLOR_NUMBER; i++)
            {
                gProCd[idx].gBP.selectedColor[i] = false;
                gProCd[idx].gBP.redValue[i] = 0;
                gProCd[idx].gBP.greenValue[i] = 0;
                gProCd[idx].gBP.blueValue[i] = 0;
                gProCd[idx].gBP.grayValue[i] = 0;
                gProCd[idx].gBP.lengthValue[i] = 0;
                gProCd[idx].gBP.isBesideThisColor[i] = false;

            }
            gProCd[idx].gBP.isFillUpHoles = false;
            gProCd[idx].gBP.isConnectionBeforeFillUpHoles = false;
            gProCd[idx].gBP.isErosion = false;
            gProCd[idx].gBP.erosionElementNUM = 0;
            gProCd[idx].gBP.erosionRWidth = 11;
            gProCd[idx].gBP.erosionRHeight = 11;
            gProCd[idx].gBP.erosionCRadius = 3.5;
            gProCd[idx].gBP.isDilation = false;
            gProCd[idx].gBP.dilationElementNUM = 0;
            gProCd[idx].gBP.dilationRWidth = 11;
            gProCd[idx].gBP.dilationRHeight = 11;
            gProCd[idx].gBP.dilationCRadius = 3.5;
            gProCd[idx].gBP.isOpening = false;
            gProCd[idx].gBP.openingElementNUM = 0;
            gProCd[idx].gBP.openingRWidth = 11;
            gProCd[idx].gBP.openingRHeight = 11;
            gProCd[idx].gBP.openingCRadius = 3.5;
            gProCd[idx].gBP.isClosing = false;
            gProCd[idx].gBP.closingElementNUM = 0;
            gProCd[idx].gBP.closingRWidth = 11;
            gProCd[idx].gBP.closingRHeight = 11;
            gProCd[idx].gBP.closingCRadius = 3.5;

            //Select 
            //面积 重心Y 重心X 区域宽度 区域高度 区域左上角Y坐标 区域左上角X坐标 区域右下角Y坐标 区域右下角X坐标 ;
            //圆形近似度 紧密度 轮廓长度 凸性 矩形近似度 等效椭圆长轴半径长度 等效椭圆短轴半径长度 等效椭圆方向 ;
            //椭圆参数：长短轴比值 最小外接圆半径 最大内接圆半径 最大内接矩形宽度 最大内接矩形高度 多边形边数 区域内洞数 ;
            //所有洞的面积 最大直径  区域方向 最小外接矩形方向 最小外接矩形长度 最小外接矩形宽度 区域灰度最小值 区域灰度最大值 区域灰度平均值 区域灰度标准差;
            gProCd[idx].gBP.selectSTR = new string[34]{/*select shape*/"area","row","column","width","height","row1","column1","row2","column2","circularity","compactness","contlength","convexity"
                             ,"rectangularity","ra","rb","phi","anisometry","outer_radius","inner_radius","inner_width","inner_height","num_sides","holes_num",
                             "area_holes","max_diameter","orientation","rect2_phi","rect2_len1","rect2_len2",/*select gray*/"min","max","mean","deviation"    };
            gProCd[idx].gBP.selectisAnd = true;
            for (int i = 0; i < 34; i++)
            {
                gProCd[idx].gBP.selectIsChecked[i] = false;
                //gProCd[idx].gBP.selectisAnd[i] = true;
                gProCd[idx].gBP.selectMin[i] = 0;
                gProCd[idx].gBP.selectMax[i] = 99999999.99;
            }
            gProCd[idx].gBP.selectArrangeItemIndex = 0;
            gProCd[idx].gBP.isArrangeLtoS = true;
            gProCd[idx].gBP.regionNum = 10;
            gProCd[idx].gBP.selectItemCount = 0;
            //Output
            // gProCd[idx].gBP.outputIDIsChecked = new bool[34];
            gProCd[idx].gBP.outputShowStr = new string[34]{"面积","重心Y","重心X","区域宽度","区域高度","区域左上角Y坐标","区域左上角X坐标","区域右下角Y坐标","区域右下角X坐标",
                                                    "圆形近似度","紧密度","轮廓长度","凸性","矩形近似度","等效椭圆长轴半径长度","等效椭圆短轴半径长度","等效椭圆方向",
                                                    "椭圆参数：长短轴比值","最小外接圆半径","最大内接圆半径","最大内接矩形宽度","最大内接矩形高度","多边形边数","区域内洞数",
                                                       "所有洞的面积","最大直径"," 区域方向","最小外接矩形方向","最小外接矩形长度","最小外接矩形宽度",
                                                      "区域灰度最小值","区域灰度最大值","区域灰度平均值","区域灰度标准差"};
            for (int i = 0; i < 34; i++)
            {
                gProCd[idx].gBP.outputIDIsChecked[i] = false;
            }

        }
        public void OutputSerialOutputInit(int idx)
        {
            gProCd[idx].gSOP.isGige = true;             //以太网模式
            gProCd[idx].gSOP.outputForm = true;             //ASCII
            gProCd[idx].gSOP.NegativeMinus = true;             //' - '表示负号
            gProCd[idx].gSOP.EraseZeroYes = false;            //消零-NO
            gProCd[idx].gSOP.intBit = 4;               //整数位数
            gProCd[idx].gSOP.floatBit = 2;               //小数位数
            gProCd[idx].gSOP.FieldSeparator = 1;               //字段分隔符(逗号)
            gProCd[idx].gSOP.RecordSeparator = 1;               //记录分隔符(逗号)
        }

        public void ShapeSearchInit(int idx)
        {
            gProCd[idx].gSSP.Max_Object_Num = 0;
            gProCd[idx].gSSP.arrangeIndex = 0;
            gProCd[idx].gSSP.isBorderShapeModelChecked = false;
            gProCd[idx].gSSP.isMultiplePara = false;
            gProCd[idx].gSSP.findShapeModelBatchOperationFlag = new bool[5] { true, true, true, true, true };
            gProCd[idx].gSSP.findShapeModelBatchOperationStr = new string[5] { "X坐标", "Y坐标", "种类", "角度", "得分" };
            gProCd[idx].gSSP.showOutputResultStr = new string[5] { "结果轮廓", "位置", "种类", "角度", "得分" };
            for (int i = 0; i < 5; i++)
            {
                gProCd[idx].gSSP.showOutputResultFlag[i] = true;
            }



            for (int i = 0; i < ProCodeCls.ShapeSearchPara.MAX_SHAPE_MUL_PARA_NUM; i++)
            {
                gProCd[idx].gSSP.angleStart[i] = -3.15;
                gProCd[idx].gSSP.angleExtent[i] = 6.29;
                gProCd[idx].gSSP.minScore[i] = 0.8;
                gProCd[idx].gSSP.numMatches[i] = 5;
                gProCd[idx].gSSP.subPixel[i * 2] = 2;
                gProCd[idx].gSSP.subPixel[i * 2 + 1] = 1;
                gProCd[idx].gSSP.numLevels[i * 2] = 0;
                gProCd[idx].gSSP.numLevels[i * 2 + 1] = 0;
                gProCd[idx].gSSP.greediness[i] = 0.8;
                gProCd[idx].gASSP.modelIsChecked[i] = false;
            }
            gProCd[idx].gSSP.maxOverlap = 0.2;
        }
        public void AnisoShapeSearchInit(int idx)
        {
            gProCd[idx].gASSP.findAnisoShapeModelBatchOperationFlag = new bool[7] { true, true, true, true, true, true, true };
            gProCd[idx].gASSP.findAnisoShapeModelBatchOperationStr = new string[7] { "X坐标", "Y坐标", "X方向放大系数", "Y方向放大系数", "种类", "角度", "得分" };
            gProCd[idx].gASSP.Max_Object_Num = 0;
            gProCd[idx].gASSP.arrangeIndex = 0;
            gProCd[idx].gASSP.isBorderShapeModelChecked = false;
            gProCd[idx].gASSP.isMultiplePara = false;
            gProCd[idx].gASSP.showOutputResultStr = new string[7] { "结果轮廓", "位置", "X方向放大系数", "Y方向放大系数", "种类", "角度", "得分" };
            for (int i = 0; i < 7; i++)
            {
                gProCd[idx].gASSP.showOutputResultFlag[i] = true;
            }


            for (int i = 0; i < ProCodeCls.AnisoShapeSearchPara.MAX_SHAPE_MUL_PARA_NUM; i++)
            {
                gProCd[idx].gASSP.angleStart[i] = -3.15;
                gProCd[idx].gASSP.angleExtent[i] = 6.29;
                gProCd[idx].gASSP.minScore[i] = 0.8;
                gProCd[idx].gASSP.numMatches[i] = 5;
                gProCd[idx].gASSP.subPixel[i * 2] = 2;
                gProCd[idx].gASSP.subPixel[i * 2 + 1] = 1;
                gProCd[idx].gASSP.numLevels[i * 2] = 0;
                gProCd[idx].gASSP.numLevels[i * 2 + 1] = 0;
                gProCd[idx].gASSP.greediness[i] = 0.8;
                gProCd[idx].gASSP.scaleRMin[i] = 0.9;
                gProCd[idx].gASSP.scaleRMax[i] = 1.1;
                gProCd[idx].gASSP.scaleCMin[i] = 0.9;
                gProCd[idx].gASSP.scaleCMax[i] = 1.1;
                gProCd[idx].gASSP.modelIsChecked[i] = false;
            }
            gProCd[idx].gASSP.maxOverlap = 0.2;
        }

        public void SurfaceBasedMatchInit(int idx)//三维模板匹配初始化
        {

        }
    }
}
