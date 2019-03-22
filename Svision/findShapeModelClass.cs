using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HalconDotNet;



namespace Svision
{
    class findShapeModelClass
    {
        //find shape model class
        public static string[] optimizationList1 = null;
        public static string[] optimizationList2 = null;
        public static string[] metricList = null;
        public static string[] contrastList = null;
        public static string[] subPixelList1 = null;
        public static string[] subPixelList2 = null;
        //private int numberOfModel = 0;//The number of model
        //public HTuple tModelID = null;//The model information of Single model situation
        //private HTuple tModelIDs = null;//The model information of multiple model situation
        private HTuple tModelResultIDs = null;//The model result of multiple model situation
        public findShapeModelClass(/*int numberOfModelTemp*/)
        {
            optimizationList1 = new string[5] { "auto", "none", "point_reduction_high", "point_reduction_low", "point_reduction_medium" };
            optimizationList2 = new string[2] { "no_pregeneration", "pregeneration" };
            metricList = new string[4] { "ignore_color_polarity", "ignore_global_polarity", "ignore_local_polarity", "use_polarity" };
            contrastList = new string[3] { "auto","auto_contrast_hyst","auto_min_size" };
            subPixelList1 = new string[5] { "none","interpolation", "least_squares", "least_squares_high", "least_squares_very_high"};
            subPixelList2 = new string[33] { "max_deformation 0","max_deformation 1","max_deformation 2","max_deformation 3","max_deformation 4", "max_deformation 5","max_deformation 6","max_deformation 7","max_deformation 8", 
                                                  "max_deformation 9","max_deformation 10","max_deformation 11","max_deformation 12","max_deformation 13","max_deformation 14","max_deformation 15","max_deformation 16", 
                                                             "max_deformation 17","max_deformation 18","max_deformation 19","max_deformation 20","max_deformation 21","max_deformation 22", 
                                                             "max_deformation 23","max_deformation 24","max_deformation 25","max_deformation 26","max_deformation 27","max_deformation 28", 
                                                             "max_deformation 29","max_deformation 30","max_deformation 31","max_deformation 32" };
            //numberOfModel = numberOfModelTemp;
        }
        ~findShapeModelClass()
        {
            
        }
        public void clearAllShapeModels()
        {
            //All the shape models will be clear.
            HOperatorSet.ClearAllShapeModels();
        }
        private struct createShapeModelPara
        {
            // createShapeModelPara struct:
            // Including all the parameter of the createShapeModel function
            public HTuple numberLevels;
            //Maximum number of pyramid levels. 
            public HTuple angleStart;
            //Smallest rotation of the pattern.(rad)
            public HTuple angleExtent;
            //Extent of the rotation angles.(rad)
            public HTuple angleStep;
            //Step length of the angles (resolution).(rad)
            public HTuple optimization;
            //Kind of optimization and optionally method used for generating the model.
            public HTuple metric;
            //Match metric.
            public HTuple contrast;
            //Threshold or hysteresis thresholds for the contrast of the object in the template image and optionally minimum size of the object parts.
            public HTuple minContrast;
            //Minimum contrast of the objects in the search images.
            public createShapeModelPara(int numberLevelsTemp, double angleStartTemp, double angleExtentTemp, double angleStepTemp,
                                        int[] optimizationTemp, int metricTemp, int[] contrastTemp, int minContrastTemp)
            {
                numberLevels = null;
                angleStart = null;
                angleExtent = null;
                angleStep = null;
                optimization = null;
                metric = null;
                contrast = null;
                minContrast = null;
                /*numberLevels*/
                //List of values: 'auto' , 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 
                if (numberLevelsTemp > 0 )
                {
                    numberLevels = (HTuple)numberLevelsTemp;
                }
                else 
                {
                    numberLevels = "auto";
                }
                /*angleStart*/
                //Suggested values: -3.14, -1.57, -0.79, -0.39, -0.20, 0.0 
                angleStart = (HTuple)angleStartTemp;
                /*angleExtent*/
                //Suggested values: 6.29, 3.14, 1.57, 0.79, 0.39
                //Restriction: AngleExtent >= 0 
                if (angleExtentTemp>=0)
                {
                    angleExtent = (HTuple)angleExtentTemp;
                } 
                else
                {
                    throw new Exception("numberLevels error!");
                }
                /*angelStep*/
                //Suggested values: 'auto', 0.0175, 0.0349, 0.0524, 0.0698, 0.0873
                //Restriction: (AngleStep >= 0) && (AngleStep <= (pi / 16)) 
                if (angleStepTemp == 0)
                {
                    angleStep = "auto";
                }
                else if (angleStepTemp > 0 && angleStepTemp <= 0.1963)
                {
                    angleStep = (HTuple)angleStepTemp;
                }
                else
                {
                    angleStep = 0.1963;
                }
                /*optimization*/
                //optimizationTemp is the index of the element in optimizationList1 and optimizationList2
                //optimizationList1 = new string[5] { "auto", "none", "point_reduction_high", "point_reduction_low", "point_reduction_medium" };
                //optimizationList2 = new string[2] { "no_pregeneration", "pregeneration" };
                if (optimizationTemp.Length == 2)
                {
                    if (optimizationTemp[0] >= 0 && optimizationTemp[0] < 5)
                    {
                        optimization = (HTuple)optimizationList1[optimizationTemp[0]];
                    }
                    else
                    {
                        throw new Exception("optimizationTemp[0]error!");
                    }
                    if (optimizationTemp[1] >= 0 && optimizationTemp[1] < 2)
                    {
                        optimization = optimization.TupleConcat((HTuple)optimizationList2[optimizationTemp[1]]);
                    }
                    else
                    {
                        throw new Exception("optimizationTemp[1]error!");
                    }
                }
                else
                {
                    throw new Exception("optimizationTemp.Length error!");
                }
                /*metric*/
                // metricTemp is the index of the element in metricList
                // metricList = new string[4] { "ignore_color_polarity", "ignore_global_polarity", "ignore_local_polarity", "use_polarity" };
                if (metricTemp >= 0 && metricTemp < 4)
                {
                    metric = (HTuple)metricList[metricTemp];
                }
                else
                {
                    throw new Exception("metricTemp error!");
                }
                /*contrast*/
                //contrastList = new string[3] { "auto", "auto_contrast_hyst", "auto_min_size" };
                //int constrastValueHysteresisLow;//(0--255)
                //int constrastValueHysteresisHigh;//(0--255)
                //int constrastValueHysteresisSize;//(0--)
                //contrastTemp is an array corresponding to [constrastValueHysteresisLow,constrastValueHysteresisHigh,constrastValueHysteresisSize]
                //[0,0,VALUE_SIZE]->["auto_contrast_hyst",VALUE_SIZE]
                //[VALUE_LOW,VALUE_HIGH,0]->["auto_min_size",VALUE_LOW,VALUE_HIGH]
                //[0,0,0]->["auto"]
                //[VALUE_LOW,VALUE_HIGH,VALUE_SIZE]->[VALUE_LOW,VALUE_HIGH,VALUE_SIZE]
                if (contrastTemp.Length == 3)
                {
                    if (contrastTemp[0] == 0 && contrastTemp[1] == 0 && contrastTemp[2] == 0)
                    {
                        contrast = (HTuple)contrastList[0];
                    }
                    else if (contrastTemp[0] > 0 && contrastTemp[1] >= contrastTemp[0] && contrastTemp[1] <=255 && contrastTemp[2] == 0)
                    {
                        contrast = (HTuple)contrastList[2];
                        contrast = contrast.TupleConcat((HTuple)contrastTemp[0]);
                        contrast = contrast.TupleConcat((HTuple)contrastTemp[1]);
                    }
                    else if (contrastTemp[0] == 0 && contrastTemp[1] == 0 && contrastTemp[2] > 0)
                    {
                        contrast = (HTuple)contrastList[1];
                        contrast = contrast.TupleConcat((HTuple)contrastTemp[2]);
                    }
                    else if (contrastTemp[0] > 0 && contrastTemp[1] >= contrastTemp[0] && contrastTemp[1] < 255 && contrastTemp[2] > 0)
                    {
                        contrast = (HTuple)contrastTemp[0];
                        contrast = contrast.TupleConcat((HTuple)contrastTemp[1]);
                        contrast = contrast.TupleConcat((HTuple)contrastTemp[2]);
                    }
                    else
                    {
                        throw new Exception("contrastTemp error!");
                    }
                }
                else
                {
                    throw new Exception("contrastTemp.Length error!");
                }
                /*minContrast*/
                //Suggested values: 'auto', 1, 2, 3, 5, 7, 10, 20, 30, 40
                //Restriction: 0< MinContrast < Contrast 
                //MinContrastTemp should be less than constrastValueHysteresisLow.
                //[0]->["auto"]
                //[VALUE]->[VALUE]
                if (contrastTemp[0] == 0)
                {
                    minContrast = "auto";
                }
                else
                {
                    if (minContrastTemp == 0)
                    {
                        minContrast = "auto";
                    }
                    else if (minContrastTemp > 0 && minContrastTemp <= contrastTemp[0])
                    {
                        minContrast = (HTuple)minContrastTemp;
                    }
                    else
                    {
                        throw new Exception("minContrastTemp error!");
                    }
                }
            }
        }
        private createShapeModelPara createShapeModelP;
        public void setCreatShapeModelPara(int numberLevelsTemp, double angleStartTemp, double angleExtentTemp, double angleStepTemp,
                                           int[] optimizationTemp, int metricTemp, int[] contrastTemp, int minContrastTemp)
        {
            createShapeModelP = new createShapeModelPara(numberLevelsTemp, angleStartTemp, angleExtentTemp, angleStepTemp,
                                                         optimizationTemp, metricTemp, contrastTemp, minContrastTemp);
        }
        public void inspectShapeModel(HObject templateHImage, HTuple hvWindowHandle)
        {
            //Create the representation of a shape model and display it on the screen.
            //The operator is particularly useful in order to determine the parameters NumLevels and Contrast, which are used in createShapeModel
            try
            {
                HObject modelImage, modelregion;
                HOperatorSet.InspectShapeModel(templateHImage, out modelImage, out modelregion, createShapeModelP.numberLevels, createShapeModelP.contrast);
                if (HDevWindowStack.IsOpen())
                {
                    HOperatorSet.ClearWindow(hvWindowHandle);
                    HOperatorSet.SetColor(hvWindowHandle, "red");
                    HOperatorSet.SetLineWidth(hvWindowHandle, 4);
                    HOperatorSet.DispObj(templateHImage, hvWindowHandle);
                    HOperatorSet.DispObj(modelregion, hvWindowHandle);
                }
                modelregion.Dispose();
                modelImage.Dispose();
            }
            catch (HalconException HDevExpDefaultException)
            {
                throw HDevExpDefaultException;
            }
        }

        public void determineShapeModelParameters(HObject templateHImage,int[] index ,out HTuple parameters)
        {
            //Create the representation of a shape model and display it on the screen.
            //The operator is particularly useful in order to determine the parameters NumLevels and Contrast, which are used in createShapeModel
            //(['num_levels', 'angle_step', 'scale_step', 'optimization', 'contrast_low', 'contrast_high', 'min_size', 'min_contrast']) 
            try
            {
                parameters = null;
                 HTuple hv_ParameterName = null;
                HTuple hv_ParameterValue = null;
                HOperatorSet.DetermineShapeModelParams(templateHImage, createShapeModelP.numberLevels, createShapeModelP.angleStart, createShapeModelP.angleExtent, 1,1,
                                createShapeModelP.optimization[0], createShapeModelP.metric, createShapeModelP.contrast, createShapeModelP.minContrast, "all", out hv_ParameterName, out hv_ParameterValue);
                //int length = index.GetLength(1);
                //parameters = hv_ParameterValue[index[0]];
                //if (length!=1)
                //{
                    for (int i = 0; i < 8;i++ )
                    {
                        if (i==0)
                        {
                            if (index[i]==1)
                            {
                                parameters =hv_ParameterValue[i];
                            }
                            else
                            {
                                parameters = 0;
                            }
                        } 
                        else
                        {
                            if (index[i] == 1)
                            {
                                parameters = parameters.TupleConcat(hv_ParameterValue[i]);
                            }
                            else
                            {
                                parameters = parameters.TupleConcat(0);
                            }
                        }
                        
                    }
                    if (parameters[3] == optimizationList1[1])
                {
                    parameters[3] = 1;
                }
                else if (parameters[3] == optimizationList1[2])
                {
                    parameters[3] = 2;
                }
                else if (parameters[3] == optimizationList1[3])
                {
                    parameters[3] = 3;
                }
                else
                {
                    parameters[3] = 4;
                }
                //}
            }
            catch (HalconException HDevExpDefaultException)
            {
                throw HDevExpDefaultException;
            }
        }
        public void inspectShapeModel(HObject templateHImage, HTuple numberLevels, HTuple contrast, out HObject modelregion)
        {
            //Create the representation of a shape model and display it on the screen.
            //The operator is particularly useful in order to determine the parameters NumLevels and Contrast, which are used in createShapeModel
            try
            {
                HObject modelImage;
                HOperatorSet.InspectShapeModel(templateHImage, out modelImage, out modelregion, numberLevels, contrast);
                modelImage.Dispose();
            }
            catch (HalconException HDevExpDefaultException)
            {
                throw HDevExpDefaultException;
            }
        }
        public void createShapeModel(HObject templateHImage, out HTuple tModelID)
        {
            //Prepare a shape model for matching.And display the contour of model on the screen.
            try
            {
                HOperatorSet.CreateShapeModel(templateHImage, createShapeModelP.numberLevels, createShapeModelP.angleStart, createShapeModelP.angleExtent, createShapeModelP.angleStep,
                                createShapeModelP.optimization, createShapeModelP.metric, createShapeModelP.contrast, createShapeModelP.minContrast, out tModelID);
                
            }
            catch (HalconException HDevExpDefaultException)
            {
                throw HDevExpDefaultException;
            }
            //if (numberOfModel != 1)
            //{
            //    if (tModelIDs == null)
            //    {
            //        tModelIDs = tModelID;
            //    }
            //    else
            //    {
            //        tModelIDs = tModelIDs.TupleConcat(tModelID);
            //    }
            //}
        }
        public void saveShapeModel(string fileName, string filePath, HTuple tModelID)
        {
            //Write the single shape model to a file with the name of fileName and the path filePath.
            string fullFileName = filePath + fileName + ".shm";
            HOperatorSet.WriteShapeModel(tModelID, (HTuple)fullFileName);
        }
       
        public void readShapeModel(string fileName, string filePath,out HTuple tModelID)
        {
            // Read the single shape model to a file with the name of fileName and the path filePath.
            string fullFileName = filePath + fileName + ".shm";
            HOperatorSet.ReadShapeModel((HTuple)fullFileName, out tModelID);
        }
        
        public struct findShapeModelPara
        {
            // findShapeModelPara struct:
            // Including all the parameters of the findShapeModel function
            public HTuple angleStart;
            //Smallest rotation of the model.(rad)
            public HTuple angleExtent;
            //Extent of the rotation angles.(rad)
            public HTuple minScore;
            //Minimum score of the instances of the model to be found.
            public HTuple numMatches;
            //Number of instances of the model to be found (or 0 for all matches).
            public HTuple maxOverlap;
            //Maximum overlap of the instances of the model to be found.
            public HTuple subPixel;
            //Subpixel accuracy if not equal to 'none'.
            public HTuple numLevels;
            //Number of pyramid levels used in the matching (and lowest pyramid level to use if |NumLevels| = 2).
            public HTuple greediness;
            //“Greediness” of the search heuristic (0: safe but slow; 1: fast but matches may be missed).
            public findShapeModelPara(double angleStartTemp, double angleExtentTemp, double minScoreTemp, int numMatchesTemp, double maxOverlapTemp, int[] subPixelTemp, int[] numLevelsTemp, double greedinessTemp)
            {
                angleStart = null;
                angleExtent = null;
                minScore = null;
                numMatches = null;
                maxOverlap = null;
                subPixel = null;
                numLevels = null;
                greediness = null;

                if (optimizationList1 == null)
                {
                    optimizationList1 = new string[5] { "auto", "none", "point_reduction_high", "point_reduction_low", "point_reduction_medium" };
                }
                if (optimizationList2 == null)
                {
                    optimizationList2 = new string[2] { "no_pregeneration", "pregeneration" };
                }
                if (metricList == null)
                {
                    metricList = new string[4] { "ignore_color_polarity", "ignore_global_polarity", "ignore_local_polarity", "use_polarity" };
                }
                if (contrastList == null)
                {
                    contrastList = new string[3] { "auto", "auto_contrast_hyst", "auto_min_size" };
                }
                if (subPixelList1 == null)
                {
                    subPixelList1 = new string[5] { "none", "interpolation", "least_squares", "least_squares_high", "least_squares_very_high" };
                }
                if (subPixelList2 == null)
                {
                    subPixelList2 = new string[33] { "max_deformation 0","max_deformation 1","max_deformation 2","max_deformation 3","max_deformation 4", "max_deformation 5","max_deformation 6","max_deformation 7","max_deformation 8", 
                                                  "max_deformation 9","max_deformation 10","max_deformation 11","max_deformation 12","max_deformation 13","max_deformation 14","max_deformation 15","max_deformation 16", 
                                                             "max_deformation 17","max_deformation 18","max_deformation 19","max_deformation 20","max_deformation 21","max_deformation 22", 
                                                             "max_deformation 23","max_deformation 24","max_deformation 25","max_deformation 26","max_deformation 27","max_deformation 28", 
                                                             "max_deformation 29","max_deformation 30","max_deformation 31","max_deformation 32" };
                }
                
                /*angleStart*/
                //Suggested values: -3.14, -1.57, -0.78, -0.39, -0.20, 0.0 
                angleStart = (HTuple)angleStartTemp;
                /*angleExtent*/
                //Suggested values: 6.29, 3.14, 1.57, 0.78, 0.39, 0.0
                //Restriction: AngleExtent >= 0 
                if (angleExtentTemp>=0)
                {
                    angleExtent = (HTuple)angleExtentTemp;
                } 
                else
                {
                    throw new Exception("angleExtentTemp error!");
                }
                /*minScore*/
                //Suggested values: 0.3, 0.4, 0.5, 0.6, 0.7, 0.8, 0.9, 1.0
                //Typical range of values: 0 ≤ MinScore ≤ 1
                //Minimum increment: 0.01
                //Recommended increment: 0.05 
                if (minScoreTemp >= 0 && minScoreTemp <= 1)
                {
                    minScore = (HTuple)minScoreTemp;
                }
                else
                {
                    throw new Exception("minScoreTemp error!");
                }
                /*numMatches*/
                //Suggested values: 0, 1, 2, 3, 4, 5, 10, 20 
                if (numMatchesTemp >0)
                {
                    numMatches = (HTuple)numMatchesTemp;
                }
                else
                {
                    throw new Exception("numMatchesTemp error!");
                }
                /*maxOverlap*/
                //Suggested values: 0.0, 0.1, 0.2, 0.3, 0.4, 0.5, 0.6, 0.7, 0.8, 0.9, 1.0
                //Typical range of values: 0 <= MaxOverlap <= 1
                //Minimum increment: 0.01
                //Recommended increment: 0.05 
                if (maxOverlapTemp >= 0 && maxOverlapTemp <= 1)
                {
                    maxOverlap = (HTuple)maxOverlapTemp;
                }
                else
                {
                    throw new Exception("maxOverlap error!");
                }
                /*subPixel*/
                //subPixelTemp is the index of the element in subPixelList1 and subPixelList2
                //subPixelList1 = new string[4] { "interpolation", "least_squares", "least_squares_high", "least_squares_very_high" };
                //subPixelList2 = new string[7] {  "max_deformation 1", "max_deformation 2", "max_deformation 3", "max_deformation 4","max_deformation 5", "max_deformation 6", "none" };
                if (subPixelTemp.Length == 2)
                {
                    if (subPixelTemp[0] >= 0 && subPixelTemp[0] < 5)
                    {
                        subPixel = (HTuple)subPixelList1[subPixelTemp[0]];
                    }
                    else
                    {
                        throw new Exception("subPixelTemp[0] error!");
                    }
                    if (subPixelTemp[1] >= 0 && subPixelTemp[1] < 33)
                    {
                        subPixel = subPixel.TupleConcat((HTuple)subPixelList2[subPixelTemp[1]]);
                    }
                    else
                    {
                        throw new Exception("subPixelTemp[1] error!");
                    }
                }
                else
                {
                    throw new Exception("subPixelTemp.Length error!");
                }
                /*numLevels*/
                // numLevelsTemp is an array corresponding to [HighPyramidLevels,lowPyramidLevels]
                // The lowest pyramid level is denoted by a value of 1. And it  starts at the HighPyramidLevels
                // pyramid level and tracks the matches to the lowPyramidLevels pyramid level.
                // [0,0]->["auto"]
                // [HighPyramidLevels,0]->[HighPyramidLevels,1]
                if (numLevelsTemp.Length == 2)
                {
                    if (numLevelsTemp[0] == 0 && numLevelsTemp[1] == 0)
                    {
                        numLevels = 0;
                    }
                    else if (numLevelsTemp[1] > 0 && numLevelsTemp[1] < numLevelsTemp[0])
                    {
                        numLevels = (HTuple)numLevelsTemp[0];
                        numLevels = numLevels.TupleConcat((HTuple)numLevelsTemp[1]);
                    }
                    else
                    {
                        throw new Exception("numLevelsTemp error!");
                    }
                }
                else
                {
                    throw new Exception("numLevelsTemp.Length error!");
                }
                /*greediness*/
                //Suggested values: 0.0, 0.1, 0.2, 0.3, 0.4, 0.5, 0.6, 0.7, 0.8, 0.9, 1.0
                //Typical range of values: 0 <= Greediness <= 1
                //Minimum increment: 0.01
                //Recommended increment: 0.05 
                if (greedinessTemp >= 0 && greedinessTemp <= 1)
                {
                    greediness = (HTuple)greedinessTemp;
                }
                else
                {
                    throw new Exception("greedinessTemp error!");
                }
            }

        //    public findShapeModelPara(bool test, double angleStartTemp, double angleExtentTemp, double minScoreTemp, int numMatchesTemp, double maxOverlapTemp, int[] subPixelTemp, int[] numLevelsTemp, double greedinessTemp)
        //    {
        //        angleStart = null;
        //        angleExtent = null;
        //        minScore = null;
        //        numMatches = null;
        //        maxOverlap = null;
        //        subPixel = null;
        //        numLevels = null;
        //        greediness = null;
        //        /*angleStart*/
        //        //Suggested values: -3.14, -1.57, -0.78, -0.39, -0.20, 0.0 
        //        angleStart = (HTuple)angleStartTemp;
        //        /*angleExtent*/
        //        //Suggested values: 6.29, 3.14, 1.57, 0.78, 0.39, 0.0
        //        //Restriction: AngleExtent >= 0 
        //        if (angleExtentTemp >= 0)
        //        {
        //            angleExtent = (HTuple)angleExtentTemp;
        //        }
        //        else
        //        {
        //            throw new Exception("angleExtentTemp error!");
        //        }
        //        /*minScore*/
        //        //Suggested values: 0.3, 0.4, 0.5, 0.6, 0.7, 0.8, 0.9, 1.0
        //        //Typical range of values: 0 ≤ MinScore ≤ 1
        //        //Minimum increment: 0.01
        //        //Recommended increment: 0.05 
        //        if (minScoreTemp >= 0 && minScoreTemp <= 1)
        //        {
        //            minScore = (HTuple)minScoreTemp;
        //        }
        //        else
        //        {
        //            throw new Exception("minScoreTemp error!");
        //        }
        //        /*numMatches*/
        //        //Suggested values: 0, 1, 2, 3, 4, 5, 10, 20 
        //        if (numMatchesTemp > 0)
        //        {
        //            numMatches = (HTuple)numMatchesTemp;
        //        }
        //        else
        //        {
        //            throw new Exception("numMatchesTemp error!");
        //        }
        //        /*maxOverlap*/
        //        //Suggested values: 0.0, 0.1, 0.2, 0.3, 0.4, 0.5, 0.6, 0.7, 0.8, 0.9, 1.0
        //        //Typical range of values: 0 <= MaxOverlap <= 1
        //        //Minimum increment: 0.01
        //        //Recommended increment: 0.05 
        //        if (maxOverlapTemp >= 0 && maxOverlapTemp <= 1)
        //        {
        //            maxOverlap = (HTuple)maxOverlapTemp;
        //        }
        //        else
        //        {
        //            throw new Exception("maxOverlap error!");
        //        }
        //        /*subPixel*/
        //        //subPixelTemp is the index of the element in subPixelList1 and subPixelList2
        //        //subPixelList1 = new string[4] { "interpolation", "least_squares", "least_squares_high", "least_squares_very_high" };
        //        //subPixelList2 = new string[7] {  "max_deformation 1", "max_deformation 2", "max_deformation 3", "max_deformation 4","max_deformation 5", "max_deformation 6", "none" };
        //        if (subPixelTemp[0] >= 0 && subPixelTemp[0] < 5)
        //        {
        //            subPixel = (HTuple)subPixelList1[subPixelTemp[0]];
        //        }
        //        else
        //        {
        //            throw new Exception("subPixelTemp[0] error!");
        //        }
        //        if (subPixelTemp[1] >= 0 && subPixelTemp[1] < 33)
        //        {
        //            subPixel = subPixel.TupleConcat((HTuple)subPixelList2[subPixelTemp[1]]);
        //        }
        //        else
        //        {
        //            throw new Exception("subPixelTemp[1] error!");
        //        }
        //        /*numLevels*/
        //        // numLevelsTemp is an array corresponding to [HighPyramidLevels,lowPyramidLevels]
        //        // The lowest pyramid level is denoted by a value of 1. And it  starts at the HighPyramidLevels
        //        // pyramid level and tracks the matches to the lowPyramidLevels pyramid level.
        //        // [0,0]->["auto"]
        //        // [HighPyramidLevels,0]->[HighPyramidLevels,1]
        //        if (numLevelsTemp[0] == 0 && numLevelsTemp[1] == 0)
        //        {
        //            numLevels = 0;
        //        }
        //        else if (numLevelsTemp[1] > 0 && numLevelsTemp[1] < numLevelsTemp[0])
        //        {
        //            numLevels = (HTuple)numLevelsTemp[0];
        //            numLevels = numLevels.TupleConcat((HTuple)numLevelsTemp[1]);
        //        }
        //        else
        //        {
        //            throw new Exception("numLevelsTemp error!");
        //        }
        //        /*greediness*/
        //        //Suggested values: 0.0, 0.1, 0.2, 0.3, 0.4, 0.5, 0.6, 0.7, 0.8, 0.9, 1.0
        //        //Typical range of values: 0 <= Greediness <= 1
        //        //Minimum increment: 0.01
        //        //Recommended increment: 0.05 
        //        if (greedinessTemp >= 0 && greedinessTemp <= 1)
        //        {
        //            greediness = (HTuple)greedinessTemp;
        //        }
        //        else
        //        {
        //            throw new Exception("greedinessTemp error!");
        //        }
        //    }
        }

        private findShapeModelPara findShapeModelP;
        public void setFindShapeModelPara(double angleStartTemp, double angleExtentTemp, double minScoreTemp,
                                        int numMatchesTemp, double maxOverlapTemp, int[] subPixelTemp, int[] numLevelsTemp, double greedinessTemp)
        {
            //if (numLevelsTemp[0] != 0 && numLevelsTemp[0] <= createShapeModelP.numberLevels[0].I)
            //{
                findShapeModelP = new findShapeModelPara(angleStartTemp, angleExtentTemp, minScoreTemp, numMatchesTemp, maxOverlapTemp, subPixelTemp, numLevelsTemp, greedinessTemp);
            //}
            //else
            //{
            //    throw new Exception("numLevelsTemp error:The numLevels parameter in findShapeModel should be smaller than that in createShapeModel!");
            //}
        }
        public void findShapeModel(HObject DSTHImage, HTuple tModelID, HTuple hvWindowHandle, double sizeRate, out double[] rows,
                                    out double[] columns, out double[] angles, out double[] scores)
        {
            // Find the best matches of a shape model in an image and display them on the screen.
            try
            {
                HTuple hRow = null;
                HTuple hColumn = null;
                HTuple hAngle = null;
                HTuple hScore = null;
                HOperatorSet.FindShapeModel(DSTHImage, tModelID, findShapeModelP.angleStart, findShapeModelP.angleExtent, findShapeModelP.minScore, findShapeModelP.numMatches, findShapeModelP.maxOverlap, findShapeModelP.subPixel, findShapeModelP.numLevels, findShapeModelP.greediness, out hRow, out hColumn, out hAngle, out hScore);
                HObject hoModelContours;
                HOperatorSet.GetShapeModelContours(out hoModelContours, tModelID, 1);
                for (int i = 0; i < hRow.TupleLength(); i++)
                {
                    HTuple hvHomMat2D;
                    HOperatorSet.GetShapeModelContours(out hoModelContours, tModelID, 1);
                    HOperatorSet.VectorAngleToRigid(0, 0, 0, hRow[i], hColumn[i], hAngle[i], out hvHomMat2D);
                    HOperatorSet.HomMat2dScale(hvHomMat2D, sizeRate, sizeRate, 0, 0, out hvHomMat2D);
                    HOperatorSet.AffineTransContourXld(hoModelContours, out hoModelContours, hvHomMat2D);
                    if (HDevWindowStack.IsOpen())
                    {
                        HOperatorSet.SetColor(hvWindowHandle, "green");
                        HOperatorSet.SetLineWidth(hvWindowHandle, 4);
                        HOperatorSet.DispObj(hoModelContours, hvWindowHandle);
                    }
                }
                hoModelContours.Dispose();
                rows = new double[hRow.TupleLength()];
                for (int i = 0; i < hRow.TupleLength(); i++)
                {
                    rows[i] = hRow[i].D;
                }
                columns = new double[hColumn.TupleLength()];
                for (int i = 0; i < hColumn.TupleLength(); i++)
                {
                    columns[i] = hColumn[i].D;
                }
                angles = new double[hAngle.TupleLength()];
                for (int i = 0; i < hAngle.TupleLength(); i++)
                {
                    angles[i] = hAngle[i].D;
                }
                scores = new double[hScore.TupleLength()];
                for (int i = 0; i < hScore.TupleLength(); i++)
                {
                    scores[i] = hScore[i].D;
                }
            }
            catch (HalconException HDevExpDefaultException)
            {
                throw HDevExpDefaultException;
            }
        }
        public void findShapeModel(HObject DSTHImage, HTuple tModelID, int arrangeIndexs, out double[] rows,
                                    out double[] columns, out double[] angles, out double[] scores)
        {
            // Find the best matches of a shape model in an image.
            try
            {
                HTuple hRow = null;
                HTuple hColumn = null;
                HTuple hAngle = null;
                HTuple hScore = null;
                HTuple sortedFeatureValueIndices = null;
                
                HOperatorSet.FindShapeModel(DSTHImage, tModelID, findShapeModelP.angleStart, findShapeModelP.angleExtent, 
                    findShapeModelP.minScore, findShapeModelP.numMatches, findShapeModelP.maxOverlap, findShapeModelP.subPixel,
                    findShapeModelP.numLevels, findShapeModelP.greediness, out hRow, out hColumn, out hAngle, out hScore);
                if (hRow.TupleLength() != 0 && hRow.TupleLength() > 1)
                {
                    switch (arrangeIndexs)
                    {
                        case 0:
                            {
                                HOperatorSet.TupleSortIndex(hColumn, out sortedFeatureValueIndices);
                                HOperatorSet.TupleSort(hColumn, out hColumn);

                                HOperatorSet.TupleInverse(hColumn, out hColumn);
                                HOperatorSet.TupleInverse(sortedFeatureValueIndices, out sortedFeatureValueIndices);


                                HOperatorSet.TupleSelect(hAngle, sortedFeatureValueIndices, out hAngle);
                                HOperatorSet.TupleSelect(hRow, sortedFeatureValueIndices, out hRow);
                                HOperatorSet.TupleSelect(hScore, sortedFeatureValueIndices, out hScore);

                            }
                            break;
                        case 1:
                            {
                                HOperatorSet.TupleSortIndex(hColumn, out sortedFeatureValueIndices);
                                HOperatorSet.TupleSort(hColumn, out hColumn);


                                HOperatorSet.TupleSelect(hAngle, sortedFeatureValueIndices, out hAngle);
                                HOperatorSet.TupleSelect(hRow, sortedFeatureValueIndices, out hRow);
                                HOperatorSet.TupleSelect(hScore, sortedFeatureValueIndices, out hScore);

                            }
                            break;
                        case 2:
                            {
                                HOperatorSet.TupleSortIndex(hRow, out sortedFeatureValueIndices);
                                HOperatorSet.TupleSort(hRow, out hRow);

                                HOperatorSet.TupleInverse(hRow, out hRow);
                                HOperatorSet.TupleInverse(sortedFeatureValueIndices, out sortedFeatureValueIndices);

                                HOperatorSet.TupleSelect(hColumn, sortedFeatureValueIndices, out hColumn);
                                HOperatorSet.TupleSelect(hAngle, sortedFeatureValueIndices, out hAngle);
                                HOperatorSet.TupleSelect(hScore, sortedFeatureValueIndices, out hScore);

                            }
                            break;
                        case 3:
                            {
                                HOperatorSet.TupleSortIndex(hRow, out sortedFeatureValueIndices);
                                HOperatorSet.TupleSort(hRow, out hRow);

                                HOperatorSet.TupleSelect(hColumn, sortedFeatureValueIndices, out hColumn);
                                HOperatorSet.TupleSelect(hAngle, sortedFeatureValueIndices, out hAngle);
                                HOperatorSet.TupleSelect(hScore, sortedFeatureValueIndices, out hScore);
                            }
                            break;
                        default:
                            break;
                    }
                }
                rows = new double[hRow.TupleLength()];
                for (int i = 0; i < hRow.TupleLength(); i++)
                {
                    rows[i] = hRow[i].D;
                }
                columns = new double[hColumn.TupleLength()];
                for (int i = 0; i < hColumn.TupleLength(); i++)
                {
                    columns[i] = hColumn[i].D;
                }
                angles = new double[hAngle.TupleLength()];
                for (int i = 0; i < hAngle.TupleLength(); i++)
                {
                    angles[i] = hAngle[i].D;
                }
                scores = new double[hScore.TupleLength()];
                for (int i = 0; i < hScore.TupleLength(); i++)
                {
                    scores[i] = hScore[i].D;
                }
            }
            catch (HalconException HDevExpDefaultException)
            {
                throw HDevExpDefaultException;
            }
        }
        public void displayModelResult(HObject DSTHImage,  HTuple tModelID,HTuple hvWindowHandle, double sizeRate, double[] rows,
                                    double[] columns, double[] angles, double[] scores)
        {
            // Display the best matches of a shape model in an image on the screen.
            try
            {
                HObject hoModelContours;
                HOperatorSet.GetShapeModelContours(out hoModelContours, tModelID, 1);
                for (int i = 0; i < rows.Length; i++)
                {
                    HOperatorSet.GetShapeModelContours(out hoModelContours, tModelID, 1);
                    HTuple hvHomMat2D;
                    HOperatorSet.VectorAngleToRigid(0, 0, 0, rows[i], columns[i], angles[i], out hvHomMat2D);
                    HOperatorSet.HomMat2dScale(hvHomMat2D, sizeRate, sizeRate, 0, 0, out hvHomMat2D);
                    HOperatorSet.AffineTransContourXld(hoModelContours, out hoModelContours, hvHomMat2D);
                    if (HDevWindowStack.IsOpen())
                    {
                        HOperatorSet.SetColor(hvWindowHandle, "green");
                        HOperatorSet.SetLineWidth(hvWindowHandle, 4);
                        HOperatorSet.DispObj(hoModelContours, hvWindowHandle);
                    }
                }
                hoModelContours.Dispose();
            }
            catch (HalconException HDevExpDefaultException)
            {
                throw HDevExpDefaultException;
            }
        }
        public struct findShapeModelsPara
        {
            // findShapeModelsPara struct:
            // Including all the parameters of the findShapeModels function.
            // All input parameters must either contain one element, 
            // in which case the parameter is used for all models, 
            // or must contain the same number of elements as ModelIDs, 
            // in which case each parameter element refers to the corresponding element in ModelIDs. 
            public HTuple angleStart;
            //Smallest rotation of the models.(rad)
            public HTuple angleExtent;
            //Extent of the rotation angles.(rad)
            public HTuple minScore;
            //Minimum score of the instances of the models to be found.
            public HTuple numMatches;
            //Number of instances of the models to be found(or 0 for all matches).
            public HTuple maxOverlap;
            //Maximum overlap of the instances of the models to be found.
            public HTuple subPixel;
            //Subpixel accuracy if not equal to 'none'.
            public HTuple numLevels;
            //Number of pyramid levels used in the matching (and lowest pyramid level to use if |NumLevels| = 2).
            public HTuple greediness;
            //“Greediness” of the search heuristic (0: safe but slow; 1: fast but matches may be missed).
            public findShapeModelsPara(int numberOfModelTemp, double[] angleStartTemp, double[] angleExtentTemp, double[] minScoreTemp, int[] numMatchesTemp, double maxOverlapTemp, int[] subPixelTemp, int[] numLevelsTemp, double[] greedinessTemp)
            {
                angleStart = null;
                angleExtent = null;
                minScore = null;
                numMatches = null;
                maxOverlap = null;
                subPixel = null;
                numLevels = null;
                greediness = null;

                if (optimizationList1 == null)
                {
                    optimizationList1 = new string[5] { "auto", "none", "point_reduction_high", "point_reduction_low", "point_reduction_medium" };
                }
                if (optimizationList2 == null)
                {
                    optimizationList2 = new string[2] { "no_pregeneration", "pregeneration" };
                }
                if (metricList == null)
                {
                    metricList = new string[4] { "ignore_color_polarity", "ignore_global_polarity", "ignore_local_polarity", "use_polarity" };
                }
                if (contrastList == null)
                {
                    contrastList = new string[3] { "auto", "auto_contrast_hyst", "auto_min_size" };
                }
                if (subPixelList1 == null)
                {
                    subPixelList1 = new string[5] { "none", "interpolation", "least_squares", "least_squares_high", "least_squares_very_high" };
                }
                if (subPixelList2 == null)
                {
                    subPixelList2 = new string[33] { "max_deformation 0","max_deformation 1","max_deformation 2","max_deformation 3","max_deformation 4", "max_deformation 5","max_deformation 6","max_deformation 7","max_deformation 8", 
                                                  "max_deformation 9","max_deformation 10","max_deformation 11","max_deformation 12","max_deformation 13","max_deformation 14","max_deformation 15","max_deformation 16", 
                                                             "max_deformation 17","max_deformation 18","max_deformation 19","max_deformation 20","max_deformation 21","max_deformation 22", 
                                                             "max_deformation 23","max_deformation 24","max_deformation 25","max_deformation 26","max_deformation 27","max_deformation 28", 
                                                             "max_deformation 29","max_deformation 30","max_deformation 31","max_deformation 32" };
                }
                
                /*angleStart*/
                //Suggested values: -3.14, -1.57, -0.78, -0.39, -0.20, 0.0 
                if (angleStartTemp.Length == 1)
                {
                    angleStart = (HTuple)angleStartTemp[0];
                }
                else if (angleStartTemp.Length == numberOfModelTemp)
                {
                    angleStart = (HTuple)angleStartTemp[0];
                    for (int i = 1; i < numberOfModelTemp; i++)
                    {
                        angleStart = angleStart.TupleConcat((HTuple)angleStartTemp[i]);
                    }
                }
                else
                {
                    throw new Exception("angleStartTemp error!");
                }
                /*angleExtent*/
                //Suggested values: 6.29, 3.14, 1.57, 0.78, 0.39, 0.0
                //Restriction: AngleExtent >= 0 
                if (angleExtentTemp.Length == 1)
                {
                    angleExtent = (HTuple)angleExtentTemp[0];
                }
                else if (angleExtentTemp.Length == numberOfModelTemp)
                {
                    angleExtent = (HTuple)angleExtentTemp[0];
                    for (int i = 1; i < numberOfModelTemp; i++)
                    {
                        angleExtent = angleExtent.TupleConcat((HTuple)angleExtentTemp[i]);
                    }
                }
                else
                {
                    throw new Exception("angleExtentTemp error!");
                }
                /*minScore*/
                //Suggested values: 0.3, 0.4, 0.5, 0.6, 0.7, 0.8, 0.9, 1.0
                //Typical range of values: 0 ≤ MinScore ≤ 1
                //Minimum increment: 0.01
                if (minScoreTemp.Length == 1)
                {
                    if (minScoreTemp[0] >= 0 && minScoreTemp[0] <= 1)
                    {
                        minScore = (HTuple)minScoreTemp[0];
                    }
                    else
                    {
                        throw new Exception("minScoreTemp error!");
                    }
                }
                else if (minScoreTemp.Length == numberOfModelTemp)
                {
                    if (minScoreTemp[0] >= 0 && minScoreTemp[0] <= 1)
                    {
                        minScore = (HTuple)minScoreTemp[0];
                    }
                    else
                    {
                        throw new Exception("minScoreTemp error!");
                    }
                    for (int i = 1; i < numberOfModelTemp; i++)
                    {
                        if (minScoreTemp[i] >= 0 && minScoreTemp[i] <= 1)
                        {
                            minScore = minScore.TupleConcat((HTuple)minScoreTemp[i]);
                        }
                        else
                        {
                            throw new Exception("minScoreTemp error!");
                        }
                    }
                }
                else
                {
                    throw new Exception("minScoreTemp error!");
                }
                /*numMatches*/
                //Suggested values: 0, 1, 2, 3, 4, 5, 10, 20 
                if (numMatchesTemp.Length == 1)
                {
                    numMatches = (HTuple)numMatchesTemp[0];
                }
                else if (numMatchesTemp.Length == numberOfModelTemp)
                {
                    numMatches = (HTuple)numMatchesTemp[0];
                    for (int i = 1; i < numberOfModelTemp; i++)
                    {
                        numMatches = numMatches.TupleConcat((HTuple)numMatchesTemp[i]);
                    }
                }
                else
                {
                    throw new Exception("numMatchesTemp error!");
                }
                /*maxOverlap*/
                //Suggested values: 0.0, 0.1, 0.2, 0.3, 0.4, 0.5, 0.6, 0.7, 0.8, 0.9, 1.0
                //Typical range of values: 0 <= MaxOverlap <= 1
                //Minimum increment: 0.01
                //Recommended increment: 0.05 
                if (maxOverlapTemp >= 0 && maxOverlapTemp <= 1)
                {
                    maxOverlap = (HTuple)maxOverlapTemp;
                }
                else
                {
                    throw new Exception("maxOverlap error!");
                }
                /*subPixel*/
                //subPixelTemp is the index of the element in subPixelList1 and subPixelList2
                //subPixelList1 = new string[4] { "interpolation", "least_squares", "least_squares_high", "least_squares_very_high" };
                //subPixelList2 = new string[7] {  "max_deformation 1", "max_deformation 2", "max_deformation 3", "max_deformation 4","max_deformation 5", "max_deformation 6", "none" };
                if (subPixelTemp.Length == 2)
                {
                    if (subPixelTemp[0] >= 0 && subPixelTemp[0] < 5)
                    {
                        subPixel = (HTuple)subPixelList1[subPixelTemp[0]];
                    }
                    else
                    {
                        throw new Exception("subPixelTemp[0] error!");
                    }
                    if (subPixelTemp[1] >= 0 && subPixelTemp[1] < 33)
                    {
                        subPixel = subPixel.TupleConcat((HTuple)subPixelList2[subPixelTemp[1]]);
                    }
                    else
                    {
                        throw new Exception("subPixelTemp[1] error!");
                    }
                }
                else if (subPixelTemp.Length == 2 * numberOfModelTemp)
                {
                    if (subPixelTemp[0] >= 0 && subPixelTemp[0] < 5 && subPixelTemp[1] >= 0 && subPixelTemp[1] < 33)
                    {
                        subPixel = ((HTuple)subPixelList1[subPixelTemp[0]]);
                        subPixel = subPixel.TupleConcat((HTuple)subPixelList2[subPixelTemp[1]]);
                    }
                    else
                    {
                        throw new Exception("subPixelTemp error!");
                    }
                    for (int i = 1; i < numberOfModelTemp; i ++)
                    {

                        if (subPixelTemp[i * 2] >= 0 && subPixelTemp[i * 2] < 5 && subPixelTemp[i * 2 + 1] >= 0 && subPixelTemp[i * 2 + 1] < 33)
                        {
                            subPixel = subPixel.TupleConcat((HTuple)subPixelList1[subPixelTemp[i * 2]]);
                            subPixel = subPixel.TupleConcat((HTuple)subPixelList2[subPixelTemp[i * 2 + 1]]);
                        }
                        else
                        {
                            throw new Exception("subPixelTemp error!");
                        }

                    }
                }
                else
                {
                    throw new Exception("subPixelTemp.Length error!");
                }
                /*numLevels*/
                // numLevelsTemp is an array corresponding to [HighPyramidLevels,lowPyramidLevels] or 
                // [HighPyramidLevels,lowPyramidLevels,HighPyramidLevels,lowPyramidLevels...HighPyramidLevels,lowPyramidLevels].
                // The lowest pyramid level is denoted by a value of 1. And it  starts at the HighPyramidLevels
                // pyramid level and tracks the matches to the lowPyramidLevels pyramid level.
                // [0,0]->["auto"]
                // [HighPyramidLevels,1]->[HighPyramidLevels,1]
                if (numLevelsTemp.Length == 2)
                {
                     if (numLevelsTemp[0] == 0 && numLevelsTemp[1] == 0)
                    {
                        numLevels = 0;
                    }
                    else if (numLevelsTemp[1] > 0 && numLevelsTemp[1] <= numLevelsTemp[0] )
                    {
                        numLevels = (HTuple)numLevelsTemp[0];
                        numLevels = numLevels.TupleConcat((HTuple)numLevelsTemp[1]);
                    }
                    else
                    {
                        throw new Exception("numLevelsTemp error!");
                    }
                }
                else if (numLevelsTemp.Length == 2 * numberOfModelTemp)
                {
                    if (numLevelsTemp[1] > 0 && numLevelsTemp[1] <= numLevelsTemp[0] )
                    {
                        numLevels = ((HTuple)numLevelsTemp[0]);
                        numLevels = numLevels.TupleConcat((HTuple)numLevelsTemp[1]);
                    }
                    else if (numLevelsTemp[0] == 0 && numLevelsTemp[1]==0)
                    {
                        numLevels = 0;
                    }
                    else
                    {
                        throw new Exception("numLevelsTemp error!");
                    }
                    for (int i = 1; i < numberOfModelTemp; i ++)
                    {
                        if (numLevelsTemp[i * 2] > 0 && numLevelsTemp[i * 2 + 1] <= numLevelsTemp[i * 2] && numLevelsTemp[i * 2 + 1] != 0)
                        {
                            numLevels = numLevels.TupleConcat((HTuple)numLevelsTemp[i * 2]);
                            numLevels = numLevels.TupleConcat((HTuple)numLevelsTemp[i * 2 + 1]);
                        }
                        else if (numLevelsTemp[i * 2] == 0 && numLevelsTemp[i * 2 + 1] == 0)
                        {
                            numLevels = numLevels.TupleConcat(0);
                        }
                        else
                        {
                            throw new Exception("numLevelsTemp error!");
                        }

                    }
                }
                else
                {
                    throw new Exception("numLevelsTemp.Length error!");
                }
                /*greediness*/
                //Suggested values: 0.0, 0.1, 0.2, 0.3, 0.4, 0.5, 0.6, 0.7, 0.8, 0.9, 1.0
                //Typical range of values: 0 <= Greediness <= 1
                //Minimum increment: 0.01
                //Recommended increment: 0.05 
                if (greedinessTemp.Length == 1)
                {
                    if (greedinessTemp[0] >= 0 && greedinessTemp[0] <= 1)
                    {
                        greediness = (HTuple)greedinessTemp[0];
                    }
                    else
                    {
                        throw new Exception("greedinessTemp error!");
                    }
                }
                else if (greedinessTemp.Length == numberOfModelTemp)
                {
                    greediness = (HTuple)greedinessTemp[0];
                    for (int i = 1; i < numberOfModelTemp; i++)
                    {
                        if (greedinessTemp[i] >= 0 && greedinessTemp[i] <= 1)
                        {
                            greediness = greediness.TupleConcat((HTuple)greedinessTemp[i]);
                        }
                        else
                        {
                            throw new Exception("greedinessTemp error!");
                        }
                    }
                }
                else
                {
                    throw new Exception("greedinessTemp error!");
                }
            }

        //    public findShapeModelsPara(short[] idx, int numberOfModelTemp, double[] angleStartTemp, double[] angleExtentTemp, double[] minScoreTemp, int[] numMatchesTemp, double maxOverlapTemp, int[] subPixelTemp, int[] numLevelsTemp, double[] greedinessTemp)
        //    {
        //        angleStart = null;
        //        angleExtent = null;
        //        minScore = null;
        //        numMatches = null;
        //        maxOverlap = null;
        //        subPixel = null;
        //        numLevels = null;
        //        greediness = null;

        //             /*angleStart*/
        //            //Suggested values: -3.14, -1.57, -0.78, -0.39, -0.20, 0.0 
        //            for (int i = 0; i < ProCodeCls.ShapeSearchPara.MAX_SHAPE_MODEL; i++)
        //            {
        //                if (1 == idx[i])
        //                {
        //                    if (angleStart == null)
        //                    {
        //                        angleStart = (HTuple)angleStartTemp[i];
        //                    }
        //                    else
        //                    {
        //                        angleStart = angleStart.TupleConcat((HTuple)angleStartTemp[i]);
        //                    }
        //                }
        //            }
        //            /*angleExtent*/
        //            //Suggested values: 6.29, 3.14, 1.57, 0.78, 0.39, 0.0
        //            //Restriction: AngleExtent >= 0 
        //            for (int i = 0; i < ProCodeCls.ShapeSearchPara.MAX_SHAPE_MODEL; i++)
        //            {
        //                if (1 == idx[i])
        //                {
        //                    if (angleExtent == null)
        //                    {
        //                        angleExtent = (HTuple)angleExtentTemp[i];
        //                    }
        //                    else
        //                    {
        //                        angleExtent = angleExtent.TupleConcat((HTuple)angleExtentTemp[i]);
        //                    }
        //                }
        //            }
        //            /*minScore*/
        //            //Suggested values: 0.3, 0.4, 0.5, 0.6, 0.7, 0.8, 0.9, 1.0
        //            //Typical range of values: 0 ≤ MinScore ≤ 1
        //            //Minimum increment: 0.01

        //            if (minScoreTemp[0] >= 0 && minScoreTemp[0] <= 1)
        //            {
        //                minScore = (HTuple)minScoreTemp[0];
        //            }
        //            else
        //            {
        //                throw new Exception("minScoreTemp error!");
        //            }
        //            for (int i = 0; i < ProCodeCls.ShapeSearchPara.MAX_SHAPE_MODEL; i++)
        //            {
        //                if (1 == idx[i])
        //                {
        //                    if (minScoreTemp[i] >= 0 && minScoreTemp[i] <= 1)
        //                    {
        //                        if (minScore == null)
        //                        {
        //                            minScore = (HTuple)minScoreTemp[i];
        //                        }
        //                        else
        //                        {
        //                            minScore = minScore.TupleConcat((HTuple)minScoreTemp[i]);
        //                        }
        //                    }
        //                    else
        //                    {
        //                        throw new Exception("minScoreTemp error!");
        //                    }
        //                }
        //            }
        //            /*numMatches*/
        //            //Suggested values: 0, 1, 2, 3, 4, 5, 10, 20 
        //            for (int i = 0; i < ProCodeCls.ShapeSearchPara.MAX_SHAPE_MODEL; i++)
        //            {
        //                if (1 == idx[i])
        //                {
        //                    if (numMatches == null)
        //                    {
        //                        numMatches = (HTuple)numMatchesTemp[i];
        //                    }
        //                    else
        //                    {
        //                        numMatches = numMatches.TupleConcat((HTuple)numMatchesTemp[i]);
        //                    }
        //                }
        //            }
        //            /*maxOverlap*/
        //            //Suggested values: 0.0, 0.1, 0.2, 0.3, 0.4, 0.5, 0.6, 0.7, 0.8, 0.9, 1.0
        //            //Typical range of values: 0 <= MaxOverlap <= 1
        //            //Minimum increment: 0.01
        //            //Recommended increment: 0.05 
        //            if (maxOverlapTemp >= 0 && maxOverlapTemp <= 1)
        //            {
        //                maxOverlap = (HTuple)maxOverlapTemp;
        //            }
        //            else
        //            {
        //                throw new Exception("maxOverlap error!");
        //            }
        //            /*subPixel*/
        //            //subPixelTemp is the index of the element in subPixelList1 and subPixelList2
        //            //subPixelList1 = new string[4] { "interpolation", "least_squares", "least_squares_high", "least_squares_very_high" };
        //            //subPixelList2 = new string[7] {  "max_deformation 1", "max_deformation 2", "max_deformation 3", "max_deformation 4","max_deformation 5", "max_deformation 6", "none" };
        //            for (int i = 0; i < ProCodeCls.ShapeSearchPara.MAX_SHAPE_MODEL; i++)
        //            {
        //                if (1 == idx[i])
        //                {
        //                    if (subPixelTemp[i * 2] >= 0 && subPixelTemp[i * 2] < 5 && subPixelTemp[i * 2 + 1] >= 0 && subPixelTemp[i * 2 + 1] < 33)
        //                    {
        //                        if (subPixel == null)
        //                        {
        //                            subPixel = (HTuple)subPixelList1[subPixelTemp[2 * i]];
        //                            subPixel = subPixel.TupleConcat((HTuple)subPixelList2[subPixelTemp[2 * i + 1]]);
        //                        }
        //                        else
        //                        {
        //                            subPixel = subPixel.TupleConcat((HTuple)subPixelList1[subPixelTemp[i * 2]]);
        //                            subPixel = subPixel.TupleConcat((HTuple)subPixelList2[subPixelTemp[i * 2 + 1]]);
        //                        }
        //                    }
        //                    else
        //                    {
        //                        throw new Exception("subPixelTemp.Length error!");
        //                    }
        //                }
        //            }
        //            /*numLevels*/
        //            // numLevelsTemp is an array corresponding to [HighPyramidLevels,lowPyramidLevels] or 
        //            // [HighPyramidLevels,lowPyramidLevels,HighPyramidLevels,lowPyramidLevels...HighPyramidLevels,lowPyramidLevels].
        //            // The lowest pyramid level is denoted by a value of 1. And it  starts at the HighPyramidLevels
        //            // pyramid level and tracks the matches to the lowPyramidLevels pyramid level.
        //            // [0,0]->["auto"]
        //            // [HighPyramidLevels,1]->[HighPyramidLevels,1]
        //            for (int i = 0; i < ProCodeCls.ShapeSearchPara.MAX_SHAPE_MODEL; i++)
        //            {
        //                if (1 == idx[i])
        //                {
        //                    if (numLevelsTemp[i * 2] > 0 && numLevelsTemp[i * 2 + 1] <= numLevelsTemp[i * 2] && numLevelsTemp[i * 2 + 1] != 0)
        //                    {
        //                        if (numLevels == null)
        //                        {
        //                            numLevels = ((HTuple)numLevelsTemp[i * 2]);
        //                            numLevels = numLevels.TupleConcat((HTuple)numLevelsTemp[i * 2 + 1]);
        //                        }
        //                        else
        //                        {
        //                            numLevels = numLevels.TupleConcat((HTuple)numLevelsTemp[i * 2]);
        //                            numLevels = numLevels.TupleConcat((HTuple)numLevelsTemp[i * 2 + 1]);
        //                        }
        //                    }
        //                    else
        //                    {
        //                        throw new Exception("numLevelsTemp error!");
        //                    }
        //                }
        //            }
        //            /*greediness*/
        //            //Suggested values: 0.0, 0.1, 0.2, 0.3, 0.4, 0.5, 0.6, 0.7, 0.8, 0.9, 1.0
        //            //Typical range of values: 0 <= Greediness <= 1
        //            //Minimum increment: 0.01
        //            //Recommended increment: 0.05 
        //            for (int i = 0; i < ProCodeCls.ShapeSearchPara.MAX_SHAPE_MODEL; i++)
        //            {
        //                if (1 == idx[i])
        //                {
        //                    if (greedinessTemp[i] >= 0 && greedinessTemp[i] <= 1)
        //                    {
        //                        if (greediness == null)
        //                        {
        //                            greediness = (HTuple)greedinessTemp[i];
        //                        }
        //                        else
        //                        {
        //                            greediness = greediness.TupleConcat((HTuple)greedinessTemp[i]);
        //                        }
        //                    }
        //                    else
        //                    {
        //                        throw new Exception("greedinessTemp error!");
        //                    }
        //                }
        //            }
                
        //    }
        }

        private findShapeModelsPara findShapeModelsP;
        public void setFindShapeModelsPara(int numberOfModelTemp, double[] angleStartTemp, double[] angleExtentTemp, double[] minScoreTemp,
                                        int[] numMatchesTemp, double maxOverlapTemp, int[] subPixelTemp, int[] numLevelsTemp, double[] greedinessTemp)
        {
            findShapeModelsP = new findShapeModelsPara(numberOfModelTemp, angleStartTemp, angleExtentTemp, minScoreTemp, numMatchesTemp, maxOverlapTemp, subPixelTemp, numLevelsTemp, greedinessTemp);
        }
        public void findShapeModels(HTuple tModelIDs,HObject DSTHImage, HTuple hvWindowHandle, double sizeRate, out double[] rows,
                                  out double[] columns, out double[] angles, out double[] scores, out int[] templateClassID)
        {
            // Find the best matches of multiple shape models and display them on the screen.
            try
            {
                HTuple hRow = null;
                HTuple hColumn = null;
                HTuple hAngle = null;
                HTuple hScore = null;
                HOperatorSet.FindShapeModels(DSTHImage, tModelIDs, findShapeModelsP.angleStart, findShapeModelsP.angleExtent, findShapeModelsP.minScore, findShapeModelsP.numMatches, findShapeModelsP.maxOverlap, findShapeModelsP.subPixel, findShapeModelsP.numLevels, findShapeModelsP.greediness, out hRow, out hColumn, out hAngle, out hScore, out tModelResultIDs);
                rows = new double[hRow.TupleLength()];
                for (int i = 0; i < hRow.TupleLength(); i++)
                {
                    rows[i] = hRow[i].D;
                }
                columns = new double[hColumn.TupleLength()];
                for (int i = 0; i < hColumn.TupleLength(); i++)
                {
                    columns[i] = hColumn[i].D;
                }
                angles = new double[hAngle.TupleLength()];
                for (int i = 0; i < hAngle.TupleLength(); i++)
                {
                    angles[i] = hAngle[i].D;
                }
                scores = new double[hScore.TupleLength()];
                for (int i = 0; i < hScore.TupleLength(); i++)
                {
                    scores[i] = hScore[i].D;
                }
                templateClassID = new int[tModelResultIDs.TupleLength()];
                if (tModelResultIDs != null)
                {
                    for (int i = 0; i < tModelResultIDs.TupleLength(); i++)
                    {
                        templateClassID[i] = tModelResultIDs[i].I;
                    }
                }
                HObject hoModelContours;
                HOperatorSet.GetShapeModelContours(out hoModelContours, tModelIDs[0], 1);
                for (int i = 0; i < rows.Length; i++)
                {
                    HOperatorSet.GetShapeModelContours(out hoModelContours, tModelIDs[templateClassID[i]], 1);
                    HTuple hvHomMat2D;
                    HOperatorSet.VectorAngleToRigid(0, 0, 0, rows[i], columns[i], angles[i], out hvHomMat2D);
                    HOperatorSet.HomMat2dScale(hvHomMat2D, sizeRate, sizeRate, 0, 0, out hvHomMat2D);
                    HOperatorSet.AffineTransContourXld(hoModelContours, out hoModelContours, hvHomMat2D);
                    if (HDevWindowStack.IsOpen())
                    {
                        HOperatorSet.SetColor(hvWindowHandle, "green");
                        HOperatorSet.SetLineWidth(hvWindowHandle, 4);
                        HOperatorSet.DispObj(hoModelContours, hvWindowHandle);
                    }
                }
                hoModelContours.Dispose();
            }
            catch (HalconException HDevExpDefaultException)
            {
                throw HDevExpDefaultException;
            }

        }
        public void findShapeModels(HTuple tModelIDs, HObject DSTHImage,int arrangeIndexs, out double[] rows,
                                 out double[] columns, out double[] angles, out double[] scores, out int[] templateClassID)
        {
            
            try
            {
                HTuple hRow = null;
                HTuple hColumn = null;
                HTuple hAngle = null;
                HTuple hScore = null;
                HTuple sortedFeatureValueIndices = null;
                HOperatorSet.FindShapeModels(DSTHImage, tModelIDs, findShapeModelsP.angleStart, findShapeModelsP.angleExtent, 
                    findShapeModelsP.minScore, findShapeModelsP.numMatches, findShapeModelsP.maxOverlap, 
                    findShapeModelsP.subPixel, findShapeModelsP.numLevels, findShapeModelsP.greediness, 
                    out hRow, out hColumn, out hAngle, out hScore, out tModelResultIDs);
                if (hRow.TupleLength() != 0 && hRow.TupleLength() > 1)
                {
                    switch (arrangeIndexs)
                    {
                        case 0:
                            {
                                HOperatorSet.TupleSortIndex(hColumn, out sortedFeatureValueIndices);
                                HOperatorSet.TupleSort(hColumn, out hColumn);

                                HOperatorSet.TupleInverse(hColumn, out hColumn);
                                HOperatorSet.TupleInverse(sortedFeatureValueIndices, out sortedFeatureValueIndices);

                                HOperatorSet.TupleSelect(hRow, sortedFeatureValueIndices, out hRow);
                                HOperatorSet.TupleSelect(hAngle, sortedFeatureValueIndices, out hAngle);
                                HOperatorSet.TupleSelect(hScore, sortedFeatureValueIndices, out hScore);
                                HOperatorSet.TupleSelect(tModelResultIDs, sortedFeatureValueIndices, out tModelResultIDs);

                            }
                            break;
                        case 1:
                            {
                                HOperatorSet.TupleSortIndex(hColumn, out sortedFeatureValueIndices);
                                HOperatorSet.TupleSort(hColumn, out hColumn);

                                HOperatorSet.TupleSelect(hRow, sortedFeatureValueIndices, out hRow);
                                HOperatorSet.TupleSelect(hAngle, sortedFeatureValueIndices, out hAngle);
                                HOperatorSet.TupleSelect(hScore, sortedFeatureValueIndices, out hScore);
                                HOperatorSet.TupleSelect(tModelResultIDs, sortedFeatureValueIndices, out tModelResultIDs);

                            }
                            break;
                        case 2:
                            {
                                HOperatorSet.TupleSortIndex(hRow, out sortedFeatureValueIndices);
                                HOperatorSet.TupleSort(hRow, out hRow);

                                HOperatorSet.TupleInverse(hRow, out hRow);
                                HOperatorSet.TupleInverse(sortedFeatureValueIndices, out sortedFeatureValueIndices);

                                HOperatorSet.TupleSelect(hColumn, sortedFeatureValueIndices, out hColumn);
                                HOperatorSet.TupleSelect(hAngle, sortedFeatureValueIndices, out hAngle);
                                HOperatorSet.TupleSelect(hScore, sortedFeatureValueIndices, out hScore);
                                HOperatorSet.TupleSelect(tModelResultIDs, sortedFeatureValueIndices, out tModelResultIDs);
                            }
                            break;
                        case 3:
                            {
                                HOperatorSet.TupleSortIndex(hRow, out sortedFeatureValueIndices);
                                HOperatorSet.TupleSort(hRow, out hRow);

                                HOperatorSet.TupleSelect(hColumn, sortedFeatureValueIndices, out hColumn);
                                HOperatorSet.TupleSelect(hAngle, sortedFeatureValueIndices, out hAngle);
                                HOperatorSet.TupleSelect(hScore, sortedFeatureValueIndices, out hScore);
                                HOperatorSet.TupleSelect(tModelResultIDs, sortedFeatureValueIndices, out tModelResultIDs);
                            }
                            break;
                        default:
                            break;
                    }
                }
                



                rows = new double[hRow.TupleLength()];
                for (int i = 0; i < hRow.TupleLength(); i++)
                {
                    rows[i] = hRow[i].D;
                }
                columns = new double[hColumn.TupleLength()];
                for (int i = 0; i < hColumn.TupleLength(); i++)
                {
                    columns[i] = hColumn[i].D;
                }
                angles = new double[hAngle.TupleLength()];
                for (int i = 0; i < hAngle.TupleLength(); i++)
                {
                    angles[i] = hAngle[i].D;
                }
                scores = new double[hScore.TupleLength()];
                for (int i = 0; i < hScore.TupleLength(); i++)
                {
                    scores[i] = hScore[i].D;
                }
                templateClassID = new int[tModelResultIDs.TupleLength()];
                if (tModelResultIDs != null)
                {
                    for (int i = 0; i < tModelResultIDs.TupleLength(); i++)
                    {
                        templateClassID[i] = tModelResultIDs[i].I;
                    }
                }
            }
            catch (HalconException HDevExpDefaultException)
            {
                throw HDevExpDefaultException;
            }
        }
        public void displayModelsResult(HTuple tModelIDs, HObject DSTHImage, HTuple hvWindowHandle, double sizeRate, double[] rows,
                                  double[] columns, double[] angles, double[] scores, int[] templateClassID)
        {
            // Display the best matches of multiple shape models on the screen.
            try
            {
                HObject hoModelContours;
                HOperatorSet.GetShapeModelContours(out hoModelContours, tModelIDs[0], 1);
                for (int i = 0; i < rows.Length; i++)
                {
                    HOperatorSet.GetShapeModelContours(out hoModelContours, tModelIDs[templateClassID[i]], 1);
                    HTuple hvHomMat2D;
                    HOperatorSet.VectorAngleToRigid(0, 0, 0, rows[i], columns[i], angles[i], out hvHomMat2D);
                    HOperatorSet.HomMat2dScale(hvHomMat2D, sizeRate, sizeRate, 0, 0, out hvHomMat2D);
                    HOperatorSet.AffineTransContourXld(hoModelContours, out hoModelContours, hvHomMat2D);
                    if (HDevWindowStack.IsOpen())
                    {
                        HOperatorSet.SetColor(hvWindowHandle, "green");
                        HOperatorSet.SetLineWidth(hvWindowHandle, 4);
                        HOperatorSet.DispObj(hoModelContours, hvWindowHandle);
                    }
                }
                hoModelContours.Dispose();
            }
            catch (HalconException HDevExpDefaultException)
            {
                throw HDevExpDefaultException;
            }
        }
    }
}
