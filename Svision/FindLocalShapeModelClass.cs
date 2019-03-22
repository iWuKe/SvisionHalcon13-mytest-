//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using HalconDotNet;
///*
//struct templateParaList
//{
//    public string[] numLevelsList = new string[11] {  "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "auto" };
//    public double angleStart;//Angle in rad. Suggested values: -3.14, -1.57, -0.79, -0.39, -0.20, 0.0 
//    public double angleExtent;//Angle in rad. Suggested values: 6.29, 3.14, 1.57, 0.79, 0.39. Restriction: AngleExtent >= 0 
//    public double angleStepValue;//Angle in rad. 0.0175, 0.0349, 0.0524, 0.0698, 0.0873. Restriction: (AngleStep >= 0) && (AngleStep <= (pi / 16)) 
//    public string angelStepAuto = "auto";
//    public string[] optimizationList1 = new string[5] { "auto","none", "point_reduction_high", "point_reduction_low", "point_reduction_medium"};

//    public string[] metric = new string[4] { "ignore_color_polarity", "ignore_global_polarity", "ignore_local_polarity", "use_polarity" };
//    public string[] contrastList = new string[3] { "auto", "auto_contrast_hyst", "auto_min_size" };
//    public int constrastValueHysteresisL;//(0--255)
//    public int constrastValueHysteresisH;//(0--255)

//    public string minContrastAuto = "auto";
//    public int minContrastValue;//MinContrast < Contrast 
//    public double minScore;//Typical range of values: 0 <= MinScore <= 1. Minimum increment: 0.01. Recommended increment: 0.05 
//    public int numMatches;
//    public double maxOverlap;//Typical range of values: 0 <=MaxOverlap<=1. Minimum increment: 0.01. Recommended increment: 0.05 
//    public string[] subPixelList1 = new string[5] { "none", "interpolation", "least_squares",  "least_squares_high", "least_squares_very_high"};
//    public string[] subPixelList2 = new string[33] { "max_deformation 0","max_deformation 1","max_deformation 2","max_deformation 3","max_deformation 4", "max_deformation 5","max_deformation 6","max_deformation 7","max_deformation 8", 
//                                                  "max_deformation 9","max_deformation 10","max_deformation 11","max_deformation 12","max_deformation 13","max_deformation 14","max_deformation 15","max_deformation 16", 
//                                                             "max_deformation 17","max_deformation 18","max_deformation 19","max_deformation 20","max_deformation 21","max_deformation 22", 
//                                                             "max_deformation 23","max_deformation 24","max_deformation 25","max_deformation 26","max_deformation 27","max_deformation 28", 
//                                                             "max_deformation 29","max_deformation 30","max_deformation 31","max_deformation 32" };
//    public int[] numLevelArray = new int[2];//Number of pyramid levels used in the matching (and lowest pyramid level to use if |NumLevels| = 2).
//    public double Greediness;//Typical range of values: 0 <= Greediness <= 1.Minimum increment: 0.01. Recommended increment: 0.05. 
    
//}*/

//namespace Svision
//{
//    class FindLocalShapeModelClass
//    {
//        //find shape model class
//        public static string[] paraNameList = null;
//        public static string[] paraValueList = null;
//        public static string[] optimizationList1 = null;

//        public static string[] metricList = null;
//        public static string[] resultTypeList = null;
//        public static string[] paramNameFingList = null;
//        public static string[] paramValueFindList = null;
//        //private int numberOfModel = 0;//The number of model
//        //public HTuple tModelID = null;//The model information of Single model situation
//        //private HTuple tModelIDs = null;//The model information of multiple model situation
//        private HTuple tModelResultIDs = null;//The model result of multiple model situation
//        public FindLocalShapeModelClass(/*int numberOfModelTemp*/)
//        {
//            paraNameList = new string[2] { "min_size", "part_size" };
//            paraValueList = new string[3] { "big", "medium", "small" };
//            optimizationList1 = new string[5] { "auto", "none", "point_reduction_high", "point_reduction_low", "point_reduction_medium" };
      
//            metricList = new string[4] { "ignore_color_polarity", "ignore_global_polarity", "ignore_local_polarity", "use_polarity" };
//            resultTypeList = new string[3]{ "deformed_contours","image_rectified","vector_field" };
//            paramNameFingList = new string[10] { "angle_step", "deformation_smoothness", "expand_border","expand_border_bottom", "expand_border_left",
//                                                    "expand_border_right","expand_border_top","scale_c_step","scale_r_step","subpixel" };
//            paramValueFindList = new string[4] { "least_squares","least_squares_high", "least_squares_very_high", "none" };
           
//            //numberOfModel = numberOfModelTemp;
//        }
//        ~FindLocalShapeModelClass()
//        {
//            //HOperatorSet.ClearDeformableModel()
//        }
//        public void clearLocalShapeModels(HTuple i)
//        {
//            //All the shape models will be clear.
//            HOperatorSet.ClearDeformableModel(i);
//        }
//        private struct createLocalShapeModelPara
//        {
//            // createLocalShapeModelPara struct:
//            // Including all the parameter of the createLocalShapeModel  function
//            public HTuple numberLevels;
//            //Maximum number of pyramid levels. 
//            public HTuple angleStart;
//            //Smallest rotation of the pattern.(rad)
//            public HTuple angleExtent;
//            //Extent of the rotation angles.(rad)
//            public HTuple angleStep;
//            //Step length of the angles (resolution).(rad)
//            public HTuple scaleRMin;
//            public HTuple scaleRMax;
            
//            public HTuple scaleRStep;

//            public HTuple scaleCMin; 
//            public HTuple scaleCMax;
            
//            public HTuple scaleCStep;



//            public HTuple optimization;
//            //Kind of optimization and optionally method used for generating the model.
//            public HTuple metric;
//            //Match metric.
//            public HTuple contrast;
//            //Threshold or hysteresis thresholds for the contrast of the object in the template image .
//            public HTuple minContrast;
//            //Minimum contrast of the objects in the search images.
//            public HTuple paramName;
//            public HTuple paramValue;



//            public createLocalShapeModelPara(int numberLevelsTemp, double angleStartTemp, double angleExtentTemp, double angleStepTemp,double scaleRMinTemp,double scaleRMaxTemp,
//                                       double scaleRStepTemp,double scaleCMinTemp,double scaleCMaxTemp,double scaleCStepTemp,
//                                        int optimizationTemp, int metricTemp, int[] contrastTemp, int minContrastTemp,int paraNameTemp,int paraValueTemp)
//            {
//                numberLevels = null;
//                angleStart = null;
//                angleExtent = null;
//                angleStep = null;
//                scaleRMin= null;
//                scaleRMax= null;
//                scaleRStep= null;
//                scaleCMin= null; 
//                scaleCMax= null;
//                scaleCStep= null;
//                optimization = null;
//                metric = null;
//                contrast = null;
//                minContrast = null;
//                paramName= null;
//                paramValue= null;
//                /*numberLevels*/
//                //List of values:  0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 'auto'
//                if (numberLevelsTemp >= 0 && numberLevelsTemp<11)
//                {
//                    numberLevels = (HTuple)numberLevelsTemp;
//                }
//                else 
//                {
//                    numberLevels = "auto";
//                }
//                /*angleStart*/
//                //Suggested values: -3.14, -1.57, -0.79, -0.39, -0.20, 0.0 
//                angleStart = (HTuple)angleStartTemp;
//                /*angleExtent*/
//                //Suggested values: 6.29, 3.14, 1.57, 0.79, 0.39
//                //Restriction: AngleExtent >= 0 
//                if (angleExtentTemp>=0)
//                {
//                    angleExtent = (HTuple)angleExtentTemp;
//                } 
//                else
//                {
//                    throw new Exception("numberLevels error!");
//                }
//                /*angelStep*/
//                //Suggested values: 'auto', 0.0175, 0.0349, 0.0524, 0.0698, 0.0873
//                //Restriction: (AngleStep >= 0) && (AngleStep <= (pi / 16)) 
//                if (angleStepTemp == 0)
//                {
//                    angleStep = "auto";
//                }
//                else if (angleStepTemp > 0 && angleStepTemp <= 0.1963)
//                {
//                    angleStep = (HTuple)angleStepTemp;
//                }
//                else
//                {
//                    angleStep = 0.1963;
//                }
//                if (scaleRMinTemp > 0)
//                {
//                    scaleRMin = (HTuple)scaleRMinTemp;
//                }
//                else
//                {
//                    throw new Exception("scaleRMin error!");
//                }
//                if (scaleRMaxTemp >= scaleRMinTemp)
//                {
//                    scaleRMax = (HTuple)scaleRMaxTemp;
//                }
//                else
//                {
//                    throw new Exception("scaleRMax error!");
//                }
//                if (scaleRStepTemp > 0)
//                {
//                    scaleRStep = (HTuple)scaleRStepTemp;
//                }
//                else if (scaleRStepTemp==0)
//                {
//                    scaleRStep = "auto";
//                }
//                else
//                {
//                    throw new Exception("scaleRStep error!");
//                }
//                if (scaleCMinTemp > 0)
//                {
//                    scaleCMin = (HTuple)scaleCMinTemp;
//                }
//                else
//                {
//                    throw new Exception("scaleCMin error!");
//                }
//                if (scaleCMaxTemp >= scaleCMinTemp)
//                {
//                    scaleCMax = (HTuple)scaleCMaxTemp;
//                }
//                else
//                {
//                    throw new Exception("scaleCMax error!");
//                }
//                if (scaleCStepTemp > 0)
//                {
//                    scaleCStep = (HTuple)scaleCStepTemp;
//                }
//                else if (scaleCStepTemp == 0)
//                {
//                    scaleCStep = "auto";
//                }
//                else
//                {
//                    throw new Exception("scaleCStep error!");
//                }



//                /*optimization*/
//                //optimizationTemp is the index of the element in optimizationList1 
//                //optimizationList1 = new string[5] { "auto", "none", "point_reduction_high", "point_reduction_low", "point_reduction_medium" };

                
//                    if (optimizationTemp>= 0 && optimizationTemp < 5)
//                    {
//                        optimization = (HTuple)optimizationList1[optimizationTemp];
//                    }
//                    else
//                    {
//                        throw new Exception("optimizationTemp error!");
//                    }
                    
//                /*metric*/
//                // metricTemp is the index of the element in metricList
//                // metricList = new string[4] { "ignore_color_polarity", "ignore_global_polarity", "ignore_local_polarity", "use_polarity" };
//                if (metricTemp >= 0 && metricTemp < 4)
//                {
//                    metric = (HTuple)metricList[metricTemp];
//                }
//                else
//                {
//                    throw new Exception("metricTemp error!");
//                }
//                /*contrast*/
                
//                //int constrastValueHysteresisLow;//(0--255)
//                //int constrastValueHysteresisHigh;//(0--255)
             
//                //[0,0]->["auto"]
//                //[VALUE_LOW,VALUE_HIGH]->[VALUE_LOW,VALUE_HIGH]
//                if (contrastTemp.Length == 2)
//                {
//                    if (contrastTemp[0] == 0 && contrastTemp[1] == 0)
//                    {
//                        contrast ="auto";
//                    }
//                    else if (contrastTemp[0] > 0 && contrastTemp[1] >= contrastTemp[0] && contrastTemp[1] <=255  )
//                    {
//                        contrast = (HTuple)contrastTemp[0];
//                        contrast = contrast.TupleConcat((HTuple)contrastTemp[1]);
//                    }
                    
//                    else
//                    {
//                        throw new Exception("contrastTemp error!");
//                    }
//                }
//                else
//                {
//                    throw new Exception("contrastTemp.Length error!");
//                }
//                /*minContrast*/
//                //Suggested values: 'auto', 1, 2, 3, 5, 7, 10, 20, 30, 40
//                //Restriction: 0< MinContrast < Contrast 
//                //MinContrastTemp should be less than constrastValueHysteresisLow.
//                //[0]->["auto"]
//                //[VALUE]->[VALUE]
//                if (contrastTemp[0] == 0)
//                {
//                    minContrast = "auto";
//                }
//                else
//                {
//                    if (minContrastTemp == 0)
//                    {
//                        minContrast = "auto";
//                    }
//                    else if (minContrastTemp > 0 && minContrastTemp <= contrastTemp[0])
//                    {
//                        minContrast = (HTuple)minContrastTemp;
//                    }
//                    else
//                    {
//                        throw new Exception("minContrastTemp error!");
//                    }
//                }

//                if (paraNameTemp >= 0 && paraNameTemp < 2)
//                {
//                    paramName = (HTuple)paraNameList[paraNameTemp];
//                }
//                else if (paraNameTemp==2)
//                {
//                    paramName = new HTuple();
//                }
//                else
//                {
//                    throw new Exception("paraName error!");
//                }
//                if (paraValueTemp >= 0 && paraValueTemp < 3)
//                {
//                    paramValue = (HTuple)paraValueList[paraValueTemp];
//                }
//                else if (paraValueTemp == 3)
//                {
//                    paramValue = new HTuple();
//                }
//                else
//                {
//                    throw new Exception("paraValue error!");
//                }

//            }
//        }
//        private createLocalShapeModelPara createLocalShapeModelP;
//        public void setCreatLocalShapeModelPara(int numberLevelsTemp, double angleStartTemp, double angleExtentTemp, double angleStepTemp, double scaleRMinTemp, double scaleRMaxTemp,
//                                       double scaleRStepTemp, double scaleCMinTemp, double scaleCMaxTemp, double scaleCStepTemp,
//                                        int optimizationTemp, int metricTemp, int[] contrastTemp, int minContrastTemp, int paraNameTemp, int paraValueTemp)
//        {
//            createLocalShapeModelP = new createLocalShapeModelPara(numberLevelsTemp,angleStartTemp, angleExtentTemp,angleStepTemp,scaleRMinTemp, scaleRMaxTemp,
//                                        scaleRStepTemp,scaleCMinTemp,scaleCMaxTemp, scaleCStepTemp,
//                                        optimizationTemp, metricTemp, contrastTemp,minContrastTemp, paraNameTemp,paraValueTemp);
//        }
//        //public void inspectShapeModel(HObject templateHImage, HTuple hvWindowHandle)
//        //{
//        //    //Create the representation of a shape model and display it on the screen.
//        //    //The operator is particularly useful in order to determine the parameters NumLevels and Contrast, which are used in createShapeModel
//        //    try
//        //    {
//        //        HObject modelImage, modelregion;
//        //        HOperatorSet.InspectShapeModel(templateHImage, out modelImage, out modelregion, createShapeModelP.numberLevels, createShapeModelP.contrast);
//        //        if (HDevWindowStack.IsOpen())
//        //        {
//        //            HOperatorSet.ClearWindow(hvWindowHandle);
//        //            HOperatorSet.SetColor(hvWindowHandle, "red");
//        //            HOperatorSet.SetLineWidth(hvWindowHandle, 4);
//        //            HOperatorSet.DispObj(templateHImage, hvWindowHandle);
//        //            HOperatorSet.DispObj(modelregion, hvWindowHandle);
//        //        }
//        //        modelregion.Dispose();
//        //        modelImage.Dispose();
//        //    }
//        //    catch (HalconException HDevExpDefaultException)
//        //    {
//        //        throw HDevExpDefaultException;
//        //    }
//        //}

//        public void determineShapeModelParameters(HObject templateHImage,int[] index ,out HTuple parameters)
//        {
//            //Create the representation of a shape model and display it on the screen.
//            //The operator is particularly useful in order to determine the parameters NumLevels and Contrast, which are used in createShapeModel
//            //(['num_levels', 'angle_step', 'scale_step', 'optimization', 'contrast_low', 'contrast_high', 'min_size', 'min_contrast']) 
//            try
//            {
//                parameters = null;
//                 HTuple hv_ParameterName = null;
//                HTuple hv_ParameterValue = null;
//                HOperatorSet.DetermineDeformableModelParams(templateHImage, createLocalShapeModelP.numberLevels, createLocalShapeModelP.angleStart, 
//                                                createLocalShapeModelP.angleExtent, (createLocalShapeModelP.scaleRMin).TupleConcat(createLocalShapeModelP.scaleCMin),
//                                            (createLocalShapeModelP.scaleRMax).TupleConcat(createLocalShapeModelP.scaleCMax), createLocalShapeModelP.optimization,
//                                            createLocalShapeModelP.metric, createLocalShapeModelP.contrast, createLocalShapeModelP.minContrast,
//                                            createLocalShapeModelP.paramName, createLocalShapeModelP.paramValue, "all", out hv_ParameterName, out hv_ParameterValue);
//                //int length = index.GetLength(1);
//                //parameters = hv_ParameterValue[index[0]];
//                //if (length!=1)
//                //{
//                    for (int i = 0; i < 8;i++ )
//                    {
//                        if (i==0)
//                        {
//                            if (index[i]==1)
//                            {
//                                parameters =hv_ParameterValue[i];
//                            }
//                            else
//                            {
//                                parameters = 0;
//                            }
//                        } 
//                        else
//                        {
//                            if (index[i] == 1)
//                            {
//                                parameters = parameters.TupleConcat(hv_ParameterValue[i]);
//                            }
//                            else
//                            {
//                                parameters = parameters.TupleConcat(0);
//                            }
//                        }
                        
//                    }
//                    if (parameters[3] == optimizationList1[1])
//                {
//                    parameters[3] = 1;
//                }
//                else if (parameters[3] == optimizationList1[2])
//                {
//                    parameters[3] = 2;
//                }
//                else if (parameters[3] == optimizationList1[3])
//                {
//                    parameters[3] = 3;
//                }
//                else
//                {
//                    parameters[3] = 4;
//                }
//                //}
//            }
//            catch (HalconException HDevExpDefaultException)
//            {
//                throw HDevExpDefaultException;
//            }
//        }
//        //public void inspectShapeModel(HObject templateHImage, HTuple numberLevels, HTuple contrast, out HObject modelregion)
//        //{
//        //    //Create the representation of a shape model and display it on the screen.
//        //    //The operator is particularly useful in order to determine the parameters NumLevels and Contrast, which are used in createShapeModel
//        //    try
//        //    {
//        //        HObject modelImage;
//        //        HOperatorSet.InspectShapeModel(templateHImage, out modelImage, out modelregion, numberLevels, contrast);
//        //        modelImage.Dispose();
//        //    }
//        //    catch (HalconException HDevExpDefaultException)
//        //    {
//        //        throw HDevExpDefaultException;
//        //    }
//        //}
//        public void createLocalShapeModel(HObject templateHImage, out HTuple tModelID)
//        {
//            //Prepare a shape model for matching.And display the contour of model on the screen.
//            try
//            {
//                HOperatorSet.CreateLocalDeformableModel(templateHImage, createLocalShapeModelP.numberLevels, createLocalShapeModelP.angleStart,
//                                                createLocalShapeModelP.angleExtent, createLocalShapeModelP.angleStep, createLocalShapeModelP.scaleRMin,
//                                                createLocalShapeModelP.scaleRMax,createLocalShapeModelP.scaleRStep,createLocalShapeModelP.scaleCMin,
//                                                createLocalShapeModelP.scaleCMax,createLocalShapeModelP.scaleCStep,createLocalShapeModelP.optimization,
//                                                createLocalShapeModelP.metric,createLocalShapeModelP.contrast,createLocalShapeModelP.minContrast, 
//                                                createLocalShapeModelP.paramName,createLocalShapeModelP.paramValue, out tModelID);

//            }
//            catch (HalconException HDevExpDefaultException)
//            {
//                throw HDevExpDefaultException;
//            }
            
//        }
        
//        public struct findLocalShapeModelPara
//        {
//            // findShapeModelPara struct:
//            // Including all the parameters of the findShapeModel function
//            public HTuple angleStart;
//            //Smallest rotation of the model.(rad)
//            public HTuple angleExtent;
//            //Extent of the rotation angles.(rad)
//            public HTuple scaleRMin;
//            //Restriction: ScaleRMin > 0 
//            public HTuple scaleRMax;
//            //Restriction: ScaleRMax >= ScaleRMin 
//            public HTuple scaleCMin;
//            //Restriction: ScaleCMin > 0 
//            public HTuple scaleCMax;

//            //Restriction: ScaleCMax >= ScaleCMin 


//            public HTuple minScore;
//            //Typical range of values: 0 ≤ MinScore ≤ 1
//            //Minimum increment: 0.01
//            //Recommended increment: 0.05 
//            //Minimum score of the instances of the model to be found.
//            public HTuple numMatches;
//            //Number of instances of the model to be found (or 0 for all matches).
//            public HTuple maxOverlap;
//            // Typical range of values: 0 ≤ MaxOverlap ≤ 1
//            //Minimum increment: 0.01
//            //Recommended increment: 0.05 
//            //Maximum overlap of the instances of the model to be found.

//            public HTuple numLevels;
//            //List of values: 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 
//            public HTuple greediness;
//            //“Greediness” of the search heuristic (0: safe but slow; 1: fast but matches may be missed).
//            public HTuple resultType;

//            //List of values: [], 'deformed_contours', 'image_rectified', 'vector_field' 
//            public HTuple paramName;
//            //List of values: [], 'angle_step', 'deformation_smoothness', 'expand_border', 'expand_border_bottom', 'expand_border_left', 
//            //'expand_border_right', 'expand_border_top', 'scale_c_step', 'scale_r_step', 'subpixel' 
//            public HTuple paramValue；
//            public findShapeModelPara(double angleStartTemp, double angleExtentTemp, double minScoreTemp, int numMatchesTemp, double maxOverlapTemp, int[] subPixelTemp, int[] numLevelsTemp, double greedinessTemp)
//            {
//                angleStart = null;
//                angleExtent = null;
//                minScore = null;
//                numMatches = null;
//                maxOverlap = null;
//                subPixel = null;
//                numLevels = null;
//                greediness = null;

//                if (optimizationList1 == null)
//                {
//                    optimizationList1 = new string[5] { "auto", "none", "point_reduction_high", "point_reduction_low", "point_reduction_medium" };
//                }
//                if (optimizationList2 == null)
//                {
//                    optimizationList2 = new string[2] { "no_pregeneration", "pregeneration" };
//                }
//                if (metricList == null)
//                {
//                    metricList = new string[4] { "ignore_color_polarity", "ignore_global_polarity", "ignore_local_polarity", "use_polarity" };
//                }
//                if (contrastList == null)
//                {
//                    contrastList = new string[3] { "auto", "auto_contrast_hyst", "auto_min_size" };
//                }
//                if (subPixelList1 == null)
//                {
//                    subPixelList1 = new string[5] { "none", "interpolation", "least_squares", "least_squares_high", "least_squares_very_high" };
//                }
//                if (subPixelList2 == null)
//                {
//                    subPixelList2 = new string[33] { "max_deformation 0","max_deformation 1","max_deformation 2","max_deformation 3","max_deformation 4", "max_deformation 5","max_deformation 6","max_deformation 7","max_deformation 8", 
//                                                  "max_deformation 9","max_deformation 10","max_deformation 11","max_deformation 12","max_deformation 13","max_deformation 14","max_deformation 15","max_deformation 16", 
//                                                             "max_deformation 17","max_deformation 18","max_deformation 19","max_deformation 20","max_deformation 21","max_deformation 22", 
//                                                             "max_deformation 23","max_deformation 24","max_deformation 25","max_deformation 26","max_deformation 27","max_deformation 28", 
//                                                             "max_deformation 29","max_deformation 30","max_deformation 31","max_deformation 32" };
//                }
                
//                /*angleStart*/
//                //Suggested values: -3.14, -1.57, -0.78, -0.39, -0.20, 0.0 
//                angleStart = (HTuple)angleStartTemp;
//                /*angleExtent*/
//                //Suggested values: 6.29, 3.14, 1.57, 0.78, 0.39, 0.0
//                //Restriction: AngleExtent >= 0 
//                if (angleExtentTemp>=0)
//                {
//                    angleExtent = (HTuple)angleExtentTemp;
//                } 
//                else
//                {
//                    throw new Exception("angleExtentTemp error!");
//                }
//                /*minScore*/
//                //Suggested values: 0.3, 0.4, 0.5, 0.6, 0.7, 0.8, 0.9, 1.0
//                //Typical range of values: 0 ≤ MinScore ≤ 1
//                //Minimum increment: 0.01
//                //Recommended increment: 0.05 
//                if (minScoreTemp >= 0 && minScoreTemp <= 1)
//                {
//                    minScore = (HTuple)minScoreTemp;
//                }
//                else
//                {
//                    throw new Exception("minScoreTemp error!");
//                }
//                /*numMatches*/
//                //Suggested values: 0, 1, 2, 3, 4, 5, 10, 20 
//                if (numMatchesTemp >0)
//                {
//                    numMatches = (HTuple)numMatchesTemp;
//                }
//                else
//                {
//                    throw new Exception("numMatchesTemp error!");
//                }
//                /*maxOverlap*/
//                //Suggested values: 0.0, 0.1, 0.2, 0.3, 0.4, 0.5, 0.6, 0.7, 0.8, 0.9, 1.0
//                //Typical range of values: 0 <= MaxOverlap <= 1
//                //Minimum increment: 0.01
//                //Recommended increment: 0.05 
//                if (maxOverlapTemp >= 0 && maxOverlapTemp <= 1)
//                {
//                    maxOverlap = (HTuple)maxOverlapTemp;
//                }
//                else
//                {
//                    throw new Exception("maxOverlap error!");
//                }
//                /*subPixel*/
//                //subPixelTemp is the index of the element in subPixelList1 and subPixelList2
//                //subPixelList1 = new string[4] { "interpolation", "least_squares", "least_squares_high", "least_squares_very_high" };
//                //subPixelList2 = new string[7] {  "max_deformation 1", "max_deformation 2", "max_deformation 3", "max_deformation 4","max_deformation 5", "max_deformation 6", "none" };
//                if (subPixelTemp.Length == 2)
//                {
//                    if (subPixelTemp[0] >= 0 && subPixelTemp[0] < 5)
//                    {
//                        subPixel = (HTuple)subPixelList1[subPixelTemp[0]];
//                    }
//                    else
//                    {
//                        throw new Exception("subPixelTemp[0] error!");
//                    }
//                    if (subPixelTemp[1] >= 0 && subPixelTemp[1] < 33)
//                    {
//                        subPixel = subPixel.TupleConcat((HTuple)subPixelList2[subPixelTemp[1]]);
//                    }
//                    else
//                    {
//                        throw new Exception("subPixelTemp[1] error!");
//                    }
//                }
//                else
//                {
//                    throw new Exception("subPixelTemp.Length error!");
//                }
//                /*numLevels*/
//                // numLevelsTemp is an array corresponding to [HighPyramidLevels,lowPyramidLevels]
//                // The lowest pyramid level is denoted by a value of 1. And it  starts at the HighPyramidLevels
//                // pyramid level and tracks the matches to the lowPyramidLevels pyramid level.
//                // [0,0]->["auto"]
//                // [HighPyramidLevels,0]->[HighPyramidLevels,1]
//                if (numLevelsTemp.Length == 2)
//                {
//                    if (numLevelsTemp[0] == 0 && numLevelsTemp[1] == 0)
//                    {
//                        numLevels = 0;
//                    }
//                    else if (numLevelsTemp[1] > 0 && numLevelsTemp[1] < numLevelsTemp[0])
//                    {
//                        numLevels = (HTuple)numLevelsTemp[0];
//                        numLevels = numLevels.TupleConcat((HTuple)numLevelsTemp[1]);
//                    }
//                    else
//                    {
//                        throw new Exception("numLevelsTemp error!");
//                    }
//                }
//                else
//                {
//                    throw new Exception("numLevelsTemp.Length error!");
//                }
//                /*greediness*/
//                //Suggested values: 0.0, 0.1, 0.2, 0.3, 0.4, 0.5, 0.6, 0.7, 0.8, 0.9, 1.0
//                //Typical range of values: 0 <= Greediness <= 1
//                //Minimum increment: 0.01
//                //Recommended increment: 0.05 
//                if (greedinessTemp >= 0 && greedinessTemp <= 1)
//                {
//                    greediness = (HTuple)greedinessTemp;
//                }
//                else
//                {
//                    throw new Exception("greedinessTemp error!");
//                }
//            }

//         }

//        private findShapeModelPara findShapeModelP;
//        public void setFindShapeModelPara(double angleStartTemp, double angleExtentTemp, double minScoreTemp,
//                                        int numMatchesTemp, double maxOverlapTemp, int[] subPixelTemp, int[] numLevelsTemp, double greedinessTemp)
//        {
//            //if (numLevelsTemp[0] != 0 && numLevelsTemp[0] <= createShapeModelP.numberLevels[0].I)
//            //{
//                findShapeModelP = new findShapeModelPara(angleStartTemp, angleExtentTemp, minScoreTemp, numMatchesTemp, maxOverlapTemp, subPixelTemp, numLevelsTemp, greedinessTemp);
//            //}
//            //else
//            //{
//            //    throw new Exception("numLevelsTemp error:The numLevels parameter in findShapeModel should be smaller than that in createShapeModel!");
//            //}
//        }
//        public void findShapeModel(HObject DSTHImage, HTuple tModelID, HTuple hvWindowHandle, double sizeRate, out double[] rows,
//                                    out double[] columns, out double[] angles, out double[] scores)
//        {
//            // Find the best matches of a shape model in an image and display them on the screen.
//            try
//            {
//                HTuple hRow = null;
//                HTuple hColumn = null;
//                HTuple hAngle = null;
//                HTuple hScore = null;
//                HOperatorSet.FindShapeModel(DSTHImage, tModelID, findShapeModelP.angleStart, findShapeModelP.angleExtent, findShapeModelP.minScore, findShapeModelP.numMatches, findShapeModelP.maxOverlap, findShapeModelP.subPixel, findShapeModelP.numLevels, findShapeModelP.greediness, out hRow, out hColumn, out hAngle, out hScore);
//                HObject hoModelContours;
//                HOperatorSet.GetShapeModelContours(out hoModelContours, tModelID, 1);
//                for (int i = 0; i < hRow.TupleLength(); i++)
//                {
//                    HTuple hvHomMat2D;
//                    HOperatorSet.GetShapeModelContours(out hoModelContours, tModelID, 1);
//                    HOperatorSet.VectorAngleToRigid(0, 0, 0, hRow[i], hColumn[i], hAngle[i], out hvHomMat2D);
//                    HOperatorSet.HomMat2dScale(hvHomMat2D, sizeRate, sizeRate, 0, 0, out hvHomMat2D);
//                    HOperatorSet.AffineTransContourXld(hoModelContours, out hoModelContours, hvHomMat2D);
//                    if (HDevWindowStack.IsOpen())
//                    {
//                        HOperatorSet.SetColor(hvWindowHandle, "green");
//                        HOperatorSet.SetLineWidth(hvWindowHandle, 4);
//                        HOperatorSet.DispObj(hoModelContours, hvWindowHandle);
//                    }
//                }
//                hoModelContours.Dispose();
//                rows = new double[hRow.TupleLength()];
//                for (int i = 0; i < hRow.TupleLength(); i++)
//                {
//                    rows[i] = hRow[i].D;
//                }
//                columns = new double[hColumn.TupleLength()];
//                for (int i = 0; i < hColumn.TupleLength(); i++)
//                {
//                    columns[i] = hColumn[i].D;
//                }
//                angles = new double[hAngle.TupleLength()];
//                for (int i = 0; i < hAngle.TupleLength(); i++)
//                {
//                    angles[i] = hAngle[i].D;
//                }
//                scores = new double[hScore.TupleLength()];
//                for (int i = 0; i < hScore.TupleLength(); i++)
//                {
//                    scores[i] = hScore[i].D;
//                }
//            }
//            catch (HalconException HDevExpDefaultException)
//            {
//                throw HDevExpDefaultException;
//            }
//        }
//        public void findShapeModel(HObject DSTHImage, HTuple tModelID, out double[] rows,
//                                    out double[] columns, out double[] angles, out double[] scores)
//        {
//            // Find the best matches of a shape model in an image.
//            try
//            {
//                HTuple hRow = null;
//                HTuple hColumn = null;
//                HTuple hAngle = null;
//                HTuple hScore = null;
//                HOperatorSet.FindShapeModel(DSTHImage, tModelID, findShapeModelP.angleStart, findShapeModelP.angleExtent, findShapeModelP.minScore, findShapeModelP.numMatches, findShapeModelP.maxOverlap, findShapeModelP.subPixel, findShapeModelP.numLevels, findShapeModelP.greediness, out hRow, out hColumn, out hAngle, out hScore);
//                rows = new double[hRow.TupleLength()];
//                for (int i = 0; i < hRow.TupleLength(); i++)
//                {
//                    rows[i] = hRow[i].D;
//                }
//                columns = new double[hColumn.TupleLength()];
//                for (int i = 0; i < hColumn.TupleLength(); i++)
//                {
//                    columns[i] = hColumn[i].D;
//                }
//                angles = new double[hAngle.TupleLength()];
//                for (int i = 0; i < hAngle.TupleLength(); i++)
//                {
//                    angles[i] = hAngle[i].D;
//                }
//                scores = new double[hScore.TupleLength()];
//                for (int i = 0; i < hScore.TupleLength(); i++)
//                {
//                    scores[i] = hScore[i].D;
//                }
//            }
//            catch (HalconException HDevExpDefaultException)
//            {
//                throw HDevExpDefaultException;
//            }
//        }
//        public void displayModelResult(HObject DSTHImage,  HTuple tModelID,HTuple hvWindowHandle, double sizeRate, double[] rows,
//                                    double[] columns, double[] angles, double[] scores)
//        {
//            // Display the best matches of a shape model in an image on the screen.
//            try
//            {
//                HObject hoModelContours;
//                HOperatorSet.GetShapeModelContours(out hoModelContours, tModelID, 1);
//                for (int i = 0; i < rows.Length; i++)
//                {
//                    HOperatorSet.GetShapeModelContours(out hoModelContours, tModelID, 1);
//                    HTuple hvHomMat2D;
//                    HOperatorSet.VectorAngleToRigid(0, 0, 0, rows[i], columns[i], angles[i], out hvHomMat2D);
//                    HOperatorSet.HomMat2dScale(hvHomMat2D, sizeRate, sizeRate, 0, 0, out hvHomMat2D);
//                    HOperatorSet.AffineTransContourXld(hoModelContours, out hoModelContours, hvHomMat2D);
//                    if (HDevWindowStack.IsOpen())
//                    {
//                        HOperatorSet.SetColor(hvWindowHandle, "green");
//                        HOperatorSet.SetLineWidth(hvWindowHandle, 4);
//                        HOperatorSet.DispObj(hoModelContours, hvWindowHandle);
//                    }
//                }
//                hoModelContours.Dispose();
//            }
//            catch (HalconException HDevExpDefaultException)
//            {
//                throw HDevExpDefaultException;
//            }
//        }

//    }
//}
