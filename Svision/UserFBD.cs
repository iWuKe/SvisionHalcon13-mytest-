/**************************************************************************************
**
**       Filename:  UserFBD.cs
**
**    Description:  this file save UserFBD struct and realize the function
**
**        Version:  1.0
**        Created:  2016-1-29
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
using Svision;
using System.Drawing;
using System.Reflection;

namespace Svision
{
    /*
    class UserFBD
    {
        public static UserFBD userFbd;

        public UserFBD()
        {
            
        }

        ~UserFBD()
        {

        }

        public UserFBD GetInstance()
        {
            if (userFbd == null)
            {
                userFbd = new UserFBD();
            }
            return userFbd;
        }
    }
    */
    public class CameraInputFBD
    {
        public CameraInputFBD()
        {
            // Svision.GetMe().baslerCamera.stopGrab();

        }
        ~CameraInputFBD()
        {

        }
        public void stopGrab()
        {
            Svision.GetMe().baslerCamera.stopGrab();
        }

        public HObject GetImage()
        {
            HObject img;
            Svision.GetMe().baslerCamera.getFrameImageWithStart(out img);
            if (ConfigInformation.GetInstance().tCalCfg.calibrationIsRadialDistortionFlag)
            {
                HOperatorSet.MapImage(img, ConfigInformation.GetInstance().tCalCfg.ho_MapFixed, out img);
            }
            return img;
        }
        public HObject GetImage1()
        {
            HObject img;
            Svision.GetMe().baslerCamera.getFrameImage(out img);
            if (ConfigInformation.GetInstance().tCalCfg.calibrationIsRadialDistortionFlag)
            {
                HOperatorSet.MapImage(img, ConfigInformation.GetInstance().tCalCfg.ho_MapFixed, out img);
            }
            return img;
        }
        public void ShowImageFunc()
        {

        }
    }
    public class FileInputFBD
    {
        public FileInputFBD()
        {
            // Svision.GetMe().baslerCamera.stopGrab();

        }
        ~FileInputFBD()
        {

        }
        public HObject GetImage()
        {
            HObject img;
            //HOperatorSet.SelectObj(Svision.GetMe().fileImages, out imgg, Svision.GetMe().fileNumIdx);
            HOperatorSet.CopyImage(UserCode.GetInstance().fileImages, out img);
            //if (Svision.GetMe().img != null)
            //{
            //    Svision.GetMe().image.Dispose();
            //}
            //Svision.GetMe().image = UserCode.GetInstance().fileImages;

            //imgg.Dispose();
            //return Svision.GetMe().image;
            //HObject img,imgg;
            //HOperatorSet.SelectObj(Svision.GetMe().fileImages, out imgg, Svision.GetMe().fileNumIdx);
            //HOperatorSet.CopyImage(imgg, out img);
            //imgg.Dispose();
            return img;
        }

        public void ShowImageFunc()
        {

        }
    }
    public class ThresholdFBD
    {
        private float minGrayValue;
        private float maxGrayValue;
        public HObject inputImg;
        public HObject outputImg;

        public ThresholdFBD(HObject ipImg, float tMinGv, float tMaxGv)
        {
            // outputImg = new HObject();
            if (inputImg != null)
            {
                inputImg.Dispose();
            }
            inputImg = ipImg;
            minGrayValue = tMinGv;
            maxGrayValue = tMaxGv;
        }

        ~ThresholdFBD()
        {
            if (inputImg != null)
            {
                inputImg.Dispose();
            }
            if (outputImg != null)
            {
                outputImg.Dispose();
            }

        }

        public HObject Thresholding(HObject iptImg)
        {
            HObject tmpImg;

            if (outputImg != null)
            {
                outputImg.Dispose();
            }
            basicClass.thresholdImage(iptImg, out tmpImg, minGrayValue, maxGrayValue);

            HOperatorSet.RegionToBin(tmpImg, out outputImg, 255, 0,
                                    Svision.GetMe().oriColumnNumber, Svision.GetMe().oriRowNumber);
            if (tmpImg != null)
            {
                tmpImg.Dispose();
            }
            return outputImg;
        }
    }
    public class BackgroundRemoveFBD
    {
        private float[] GrayValue;
        private bool isAllcolor;
        public HObject inputImg;
        public HObject outputImg;

        public BackgroundRemoveFBD(HObject ipImg, float[] tMinGv, bool boolDate)
        {
            outputImg = new HObject();
            inputImg = ipImg;
            GrayValue = new float[8];
            GrayValue = tMinGv;
            isAllcolor = boolDate;
        }

        public HObject BackThresholding(HObject iptImg)
        {


            if (Svision.GetMe().baslerCamera.getChannelNumber() == 3)
            {
                HObject image1, test, testimg, testtest, image2, image3, img1RegionResult, img2RegionResult, img3RegionResult;
                HOperatorSet.Decompose3(iptImg, out image1, out image2, out image3);
                HOperatorSet.GenEmptyObj(out testtest);
                HOperatorSet.GenEmptyObj(out testimg);
                HOperatorSet.GenEmptyObj(out test);
                basicClass.thresholdImage(image1, out img1RegionResult, GrayValue[3], GrayValue[2]);
                basicClass.thresholdImage(image2, out img2RegionResult, GrayValue[5], GrayValue[4]);
                basicClass.thresholdImage(image3, out img3RegionResult, GrayValue[7], GrayValue[6]);
                HOperatorSet.Intersection(img1RegionResult, img2RegionResult, out test);
                HOperatorSet.Intersection(img3RegionResult, test, out test);
                if (testimg != null)
                {
                    testimg.Dispose();
                }
                HOperatorSet.RegionToBin(test, out testimg, 255, 0,
                                    Svision.GetMe().oriColumnNumber, Svision.GetMe().oriRowNumber);
                HOperatorSet.Compose3(testimg, testimg, testimg, out testtest);
                if (outputImg != null)
                {
                    outputImg.Dispose();
                }
                HOperatorSet.BitAnd(iptImg, testtest, out outputImg);
                img1RegionResult.Dispose();
                img2RegionResult.Dispose();
                img3RegionResult.Dispose();
                testtest.Dispose();
                test.Dispose();
                testimg.Dispose();
                image1.Dispose();
                image2.Dispose();
                image3.Dispose();

            }
            else
            {
                HObject imgRegionResult, test;
                basicClass.thresholdImage(iptImg, out imgRegionResult, GrayValue[1], GrayValue[0]);
                HOperatorSet.GenEmptyObj(out test);
                if (test != null)
                {
                    test.Dispose();
                }
                HOperatorSet.RegionToBin(imgRegionResult, out test, 255, 0,
                                    Svision.GetMe().oriColumnNumber, Svision.GetMe().oriRowNumber);
                if (outputImg != null)
                {
                    outputImg.Dispose();
                }
                HOperatorSet.BitAnd(iptImg, test, out outputImg);
                test.Dispose();
                imgRegionResult.Dispose();

            }

            return outputImg;
        }
    }
    public class MedianFilterFBD
    {
        public HObject inputImg;
        public HObject outputImg;
        private int maskSize;

        public MedianFilterFBD(HObject ipImg, int mskSz)
        {
            outputImg = new HObject();
            inputImg = ipImg;
            maskSize = mskSz;
        }

        ~MedianFilterFBD()
        {
            if (inputImg != null)
            {
                inputImg.Dispose();
            }
            if (outputImg != null)
            {
                outputImg.Dispose();
            }
        }

        public HObject MedianFiltering(HObject iptImg)
        {
            if (outputImg != null)
            {
                outputImg.Dispose();
            }
            switch (maskSize)
            {
                case 0:
                    HOperatorSet.MedianImage(iptImg, out outputImg, "square", 3, "mirrored");
                    break;
                case 1:
                    HOperatorSet.MedianImage(iptImg, out outputImg, "square", 5, "mirrored");
                    break;
                case 2:
                    HOperatorSet.MedianImage(iptImg, out outputImg, "square", 7, "mirrored");
                    break;
                case 3:
                    HOperatorSet.MedianImage(iptImg, out outputImg, "square", 9, "mirrored");
                    break;
                case 4:
                    HOperatorSet.MedianImage(iptImg, out outputImg, "square", 11, "mirrored");
                    break;
                default:
                    break;
            }
            return outputImg;
        }
    }
    public class BlobAnalysisFBD
    {
        //public bool[] showOutputResultFlag;
        public HObject inputImg;
        public HObject outputImg;
        public HObject thresholdGrayRegionPart1, thresholdGrayRegionPart2, thresholdGrayRegionPart3, thresholdGrayRegionPart4, thresholdGrayRegionPart5, thresholdGrayRegionPart6;
        public HObject thresholdColorRegionPart1, thresholdColorRegionPart2, thresholdColorRegionPart3, thresholdColorRegionPart4, thresholdColorRegionPart5, thresholdColorRegionPart6;
        public HObject thresholdTestRegion, thresholdGrayRegion, thresholdColorRegion, blobRegion;

        public ProCodeCls.BlobPara tBP;

        public List<double[]> resultList = new List<double[]>();
        int idx;
        public BlobAnalysisFBD(HObject ipImg, ProCodeCls.BlobPara tBP_, int index)
        {

            outputImg = new HObject();
            tBP = new ProCodeCls.BlobPara();
            inputImg = ipImg;
            tBP = tBP_;
            idx = index;
            UserCode.GetInstance().gProCd[idx].doubleData = new double[tBP.regionNum * tBP.selectItemCount + 1];

        }

        ~BlobAnalysisFBD()
        {
            if (thresholdGrayRegionPart1 != null)
            {
                thresholdGrayRegionPart1.Dispose();
            }
            if (thresholdGrayRegionPart2 != null)
            {
                thresholdGrayRegionPart2.Dispose();
            }
            if (thresholdGrayRegionPart3 != null)
            {
                thresholdGrayRegionPart3.Dispose();
            }
            if (thresholdGrayRegionPart4 != null)
            {
                thresholdGrayRegionPart4.Dispose();
            }
            if (thresholdGrayRegionPart5 != null)
            {
                thresholdGrayRegionPart5.Dispose();
            }
            if (thresholdGrayRegionPart6 != null)
            {
                thresholdGrayRegionPart6.Dispose();
            }
            if (thresholdColorRegionPart1 != null)
            {
                thresholdColorRegionPart1.Dispose();
            }
            if (thresholdColorRegionPart2 != null)
            {
                thresholdColorRegionPart2.Dispose();
            }
            if (thresholdColorRegionPart3 != null)
            {
                thresholdColorRegionPart3.Dispose();
            }
            if (thresholdColorRegionPart4 != null)
            {
                thresholdColorRegionPart4.Dispose();
            }
            if (thresholdColorRegionPart5 != null)
            {
                thresholdColorRegionPart5.Dispose();
            }
            if (thresholdColorRegionPart6 != null)
            {
                thresholdColorRegionPart6.Dispose();
            }
            if (thresholdTestRegion != null)
            {
                thresholdTestRegion.Dispose();
            }
            if (thresholdGrayRegion != null)
            {
                thresholdGrayRegion.Dispose();
            }
            if (thresholdColorRegion != null)
            {
                thresholdColorRegion.Dispose();
            }
            if (inputImg != null)
            {
                inputImg.Dispose();
            }
            if (outputImg != null)
            {
                outputImg.Dispose();
            }

            resultList.Clear();
        }


        public HObject BlobAnalysis(HObject iptImg)
        {
            try
            {

                inputImg = iptImg;
                resultList.Clear();
                if (tBP.isAutoSegment)
                {
                    if (tBP.isAutoSegmentMethod1)
                    {
                        HOperatorSet.AutoThreshold(iptImg, out thresholdTestRegion, (double)tBP.autoSegmentMethod1Para1);
                    }
                    else if (tBP.isAutoSegmentMethod2)
                    {
                        string[] Para34List = new string[4] { "dark", "equal", "light", "not_equal " };
                        HOperatorSet.VarThreshold(iptImg, out thresholdTestRegion, (int)tBP.autoSegmentMethod2Para1, (int)tBP.autoSegmentMethod2Para1, (double)tBP.autoSegmentMethod2Para2, (double)tBP.autoSegmentMethod2Para3, (HTuple)Para34List[tBP.autoSegmentMethod2Para4]);
                    }
                    else if (tBP.isAutoSegmentMethod3)
                    {
                        HOperatorSet.BinThreshold(iptImg, out thresholdTestRegion);

                    }
                    else
                    {
                        //MessageBox.Show("Error!Wrong auto segment set!");
                    }
                }
                else
                {
                    if (tBP.isColor)
                    {
                        for (int i = 0; i < 6; i++)
                        {
                            HObject thresholdColorTestRegion1, thresholdColorTestRegion2, thresholdColorTestRegion3;
                            HObject image1, image2, image3;
                            HOperatorSet.Decompose3(iptImg, out image1, out image2, out image3);
                            if (tBP.selectedColor[i])
                            {
                                switch (i)
                                {
                                    case 0:

                                        HOperatorSet.Threshold(image1, out thresholdColorTestRegion1, tBP.redValue[0] - tBP.lengthValue[0], tBP.redValue[0] + tBP.lengthValue[0]);
                                        HOperatorSet.Threshold(image2, out thresholdColorTestRegion2, tBP.greenValue[0] - tBP.lengthValue[0], tBP.greenValue[0] + tBP.lengthValue[0]);
                                        HOperatorSet.Threshold(image3, out thresholdColorTestRegion3, tBP.blueValue[0] - tBP.lengthValue[0], tBP.blueValue[0] + tBP.lengthValue[0]);
                                        HOperatorSet.Intersection(thresholdColorTestRegion1, thresholdColorTestRegion2, out thresholdColorRegionPart1);
                                        HOperatorSet.Intersection(thresholdColorTestRegion3, thresholdColorRegionPart1, out thresholdColorRegionPart1);
                                        if (tBP.isBesideThisColor[0])
                                        {
                                            HOperatorSet.Complement(thresholdColorRegionPart1, out thresholdColorRegionPart1);
                                        }
                                        break;
                                    case 1:
                                        HOperatorSet.Threshold(image1, out thresholdColorTestRegion1, tBP.redValue[1] - tBP.lengthValue[1], tBP.redValue[1] + tBP.lengthValue[1]);
                                        HOperatorSet.Threshold(image2, out thresholdColorTestRegion2, tBP.greenValue[1] - tBP.lengthValue[1], tBP.greenValue[1] + tBP.lengthValue[1]);
                                        HOperatorSet.Threshold(image3, out thresholdColorTestRegion3, tBP.blueValue[1] - tBP.lengthValue[1], tBP.blueValue[1] + tBP.lengthValue[1]);
                                        HOperatorSet.Intersection(thresholdColorTestRegion1, thresholdColorTestRegion2, out thresholdColorRegionPart2);
                                        HOperatorSet.Intersection(thresholdColorTestRegion3, thresholdColorRegionPart2, out thresholdColorRegionPart2);
                                        if (tBP.isBesideThisColor[1])
                                        {
                                            HOperatorSet.Complement(thresholdColorRegionPart2, out thresholdColorRegionPart2);
                                        }
                                        break;
                                    case 2:
                                        HOperatorSet.Threshold(image1, out thresholdColorTestRegion1, tBP.redValue[2] - tBP.lengthValue[2], tBP.redValue[2] + tBP.lengthValue[2]);
                                        HOperatorSet.Threshold(image2, out thresholdColorTestRegion2, tBP.greenValue[2] - tBP.lengthValue[2], tBP.greenValue[2] + tBP.lengthValue[2]);
                                        HOperatorSet.Threshold(image3, out thresholdColorTestRegion3, tBP.blueValue[2] - tBP.lengthValue[2], tBP.blueValue[2] + tBP.lengthValue[2]);
                                        HOperatorSet.Intersection(thresholdColorTestRegion1, thresholdColorTestRegion2, out thresholdColorRegionPart3);
                                        HOperatorSet.Intersection(thresholdColorTestRegion3, thresholdColorRegionPart3, out thresholdColorRegionPart3);
                                        if (tBP.isBesideThisColor[2])
                                        {
                                            HOperatorSet.Complement(thresholdColorRegionPart3, out thresholdColorRegionPart3);
                                        }
                                        break;
                                    case 3:
                                        HOperatorSet.Threshold(image1, out thresholdColorTestRegion1, tBP.redValue[3] - tBP.lengthValue[3], tBP.redValue[3] + tBP.lengthValue[3]);
                                        HOperatorSet.Threshold(image2, out thresholdColorTestRegion2, tBP.greenValue[3] - tBP.lengthValue[3], tBP.greenValue[3] + tBP.lengthValue[3]);
                                        HOperatorSet.Threshold(image3, out thresholdColorTestRegion3, tBP.blueValue[3] - tBP.lengthValue[3], tBP.blueValue[3] + tBP.lengthValue[3]);
                                        HOperatorSet.Intersection(thresholdColorTestRegion1, thresholdColorTestRegion2, out thresholdColorRegionPart4);
                                        HOperatorSet.Intersection(thresholdColorTestRegion3, thresholdColorRegionPart4, out thresholdColorRegionPart4);
                                        if (tBP.isBesideThisColor[3])
                                        {
                                            HOperatorSet.Complement(thresholdColorRegionPart4, out thresholdColorRegionPart4);
                                        }
                                        break;
                                    case 4:
                                        HOperatorSet.Threshold(image1, out thresholdColorTestRegion1, tBP.redValue[4] - tBP.lengthValue[4], tBP.redValue[4] + tBP.lengthValue[4]);
                                        HOperatorSet.Threshold(image2, out thresholdColorTestRegion2, tBP.greenValue[4] - tBP.lengthValue[4], tBP.greenValue[4] + tBP.lengthValue[4]);
                                        HOperatorSet.Threshold(image3, out thresholdColorTestRegion3, tBP.blueValue[4] - tBP.lengthValue[4], tBP.blueValue[4] + tBP.lengthValue[4]);
                                        HOperatorSet.Intersection(thresholdColorTestRegion1, thresholdColorTestRegion2, out thresholdColorRegionPart5);
                                        HOperatorSet.Intersection(thresholdColorTestRegion3, thresholdColorRegionPart5, out thresholdColorRegionPart5);
                                        if (tBP.isBesideThisColor[4])
                                        {
                                            HOperatorSet.Complement(thresholdColorRegionPart5, out thresholdColorRegionPart5);
                                        }
                                        break;
                                    case 5:
                                        HOperatorSet.Threshold(image1, out thresholdColorTestRegion1, tBP.redValue[5] - tBP.lengthValue[5], tBP.redValue[5] + tBP.lengthValue[5]);
                                        HOperatorSet.Threshold(image2, out thresholdColorTestRegion2, tBP.greenValue[5] - tBP.lengthValue[5], tBP.greenValue[5] + tBP.lengthValue[5]);
                                        HOperatorSet.Threshold(image3, out thresholdColorTestRegion3, tBP.blueValue[5] - tBP.lengthValue[5], tBP.blueValue[5] + tBP.lengthValue[5]);
                                        HOperatorSet.Intersection(thresholdColorTestRegion1, thresholdColorTestRegion2, out thresholdColorRegionPart6);
                                        HOperatorSet.Intersection(thresholdColorTestRegion3, thresholdColorRegionPart6, out thresholdColorRegionPart6);
                                        if (tBP.isBesideThisColor[5])
                                        {
                                            HOperatorSet.Complement(thresholdColorRegionPart6, out thresholdColorRegionPart6);
                                        }
                                        break;

                                }
                            }
                            if (image1 != null)
                            {
                                image1.Dispose();
                            }
                            if (image2 != null)
                            {
                                image2.Dispose();
                            }
                            if (image3 != null)
                            {
                                image3.Dispose();
                            }
                        }

                        HOperatorSet.GenEmptyRegion(out thresholdColorRegion);
                        if (thresholdColorRegionPart1 != null && tBP.selectedColor[0])
                        {
                            HOperatorSet.Union2(thresholdColorRegionPart1, thresholdColorRegion, out thresholdColorRegion);
                        }
                        if (thresholdColorRegionPart2 != null && tBP.selectedColor[1])
                        {
                            HOperatorSet.Union2(thresholdColorRegionPart2, thresholdColorRegion, out thresholdColorRegion);
                        }
                        if (thresholdColorRegionPart3 != null && tBP.selectedColor[2])
                        {
                            HOperatorSet.Union2(thresholdColorRegionPart3, thresholdColorRegion, out thresholdColorRegion);
                        }
                        if (thresholdColorRegionPart4 != null && tBP.selectedColor[3])
                        {
                            HOperatorSet.Union2(thresholdColorRegionPart4, thresholdColorRegion, out thresholdColorRegion);
                        }
                        if (thresholdColorRegionPart5 != null && tBP.selectedColor[4])
                        {
                            HOperatorSet.Union2(thresholdColorRegionPart5, thresholdColorRegion, out thresholdColorRegion);
                        }
                        if (thresholdColorRegionPart6 != null && tBP.selectedColor[5])
                        {
                            HOperatorSet.Union2(thresholdColorRegionPart6, thresholdColorRegion, out thresholdColorRegion);
                        }
                    }
                    else
                    {
                        for (int i = 0; i < 6; i++)
                        {
                            if (tBP.selectedColor[i])
                            {
                                switch (i)
                                {
                                    case 0:
                                        HOperatorSet.Threshold(iptImg, out thresholdGrayRegionPart1, tBP.grayValue[0] - tBP.lengthValue[0], tBP.grayValue[0] + tBP.lengthValue[0]);
                                        if (tBP.isBesideThisColor[0])
                                        {
                                            HOperatorSet.Complement(thresholdGrayRegionPart1, out thresholdGrayRegionPart1);
                                        }
                                        break;
                                    case 1:
                                        HOperatorSet.Threshold(iptImg, out thresholdGrayRegionPart2, tBP.grayValue[1] - tBP.lengthValue[1], tBP.grayValue[1] + tBP.lengthValue[1]);
                                        if (tBP.isBesideThisColor[1])
                                        {
                                            HOperatorSet.Complement(thresholdGrayRegionPart2, out thresholdGrayRegionPart2);
                                        }
                                        break;
                                    case 2:
                                        HOperatorSet.Threshold(iptImg, out thresholdGrayRegionPart3, tBP.grayValue[2] - tBP.lengthValue[2], tBP.grayValue[2] + tBP.lengthValue[2]);
                                        if (tBP.isBesideThisColor[2])
                                        {
                                            HOperatorSet.Complement(thresholdGrayRegionPart3, out thresholdGrayRegionPart3);
                                        }
                                        break;
                                    case 3:
                                        HOperatorSet.Threshold(iptImg, out thresholdGrayRegionPart4, tBP.grayValue[3] - tBP.lengthValue[3], tBP.grayValue[3] + tBP.lengthValue[3]);
                                        if (tBP.isBesideThisColor[3])
                                        {
                                            HOperatorSet.Complement(thresholdGrayRegionPart4, out thresholdGrayRegionPart4);
                                        }
                                        break;
                                    case 4:
                                        HOperatorSet.Threshold(iptImg, out thresholdGrayRegionPart5, tBP.grayValue[4] - tBP.lengthValue[4], tBP.grayValue[4] + tBP.lengthValue[4]);
                                        if (tBP.isBesideThisColor[4])
                                        {
                                            HOperatorSet.Complement(thresholdGrayRegionPart5, out thresholdGrayRegionPart5);
                                        }
                                        break;
                                    case 5:
                                        HOperatorSet.Threshold(iptImg, out thresholdGrayRegionPart6, tBP.grayValue[5] - tBP.lengthValue[5], tBP.grayValue[5] + tBP.lengthValue[5]);
                                        if (tBP.isBesideThisColor[5])
                                        {
                                            HOperatorSet.Complement(thresholdGrayRegionPart6, out thresholdGrayRegionPart6);
                                        }
                                        break;

                                }
                            }
                        }

                        HOperatorSet.GenEmptyRegion(out thresholdGrayRegion);
                        if (thresholdGrayRegionPart1 != null && tBP.selectedColor[0])
                        {
                            HOperatorSet.Union2(thresholdGrayRegionPart1, thresholdGrayRegion, out thresholdGrayRegion);
                        }
                        if (thresholdGrayRegionPart2 != null && tBP.selectedColor[1])
                        {
                            HOperatorSet.Union2(thresholdGrayRegionPart2, thresholdGrayRegion, out thresholdGrayRegion);
                        }
                        if (thresholdGrayRegionPart3 != null && tBP.selectedColor[2])
                        {
                            HOperatorSet.Union2(thresholdGrayRegionPart3, thresholdGrayRegion, out thresholdGrayRegion);
                        }
                        if (thresholdGrayRegionPart4 != null && tBP.selectedColor[3])
                        {
                            HOperatorSet.Union2(thresholdGrayRegionPart4, thresholdGrayRegion, out thresholdGrayRegion);
                        }
                        if (thresholdGrayRegionPart5 != null && tBP.selectedColor[4])
                        {
                            HOperatorSet.Union2(thresholdGrayRegionPart5, thresholdGrayRegion, out thresholdGrayRegion);
                        }
                        if (thresholdGrayRegionPart6 != null && tBP.selectedColor[5])
                        {
                            HOperatorSet.Union2(thresholdGrayRegionPart6, thresholdGrayRegion, out thresholdGrayRegion);
                        }
                    }
                }
                if (blobRegion != null)
                {
                    blobRegion.Dispose();
                }
                if (tBP.isAutoSegment)
                {
                    HOperatorSet.CopyObj(thresholdTestRegion, out blobRegion, 1, -1);
                }
                else
                {
                    if (tBP.isColor)
                    {
                        HOperatorSet.CopyObj(thresholdColorRegion, out blobRegion, 1, -1);
                    }
                    else
                    {
                        HOperatorSet.CopyObj(thresholdGrayRegion, out blobRegion, 1, -1);
                    }
                }

                if (tBP.isFillUpHoles)
                {
                    if (tBP.isConnectionBeforeFillUpHoles)
                    {
                        HOperatorSet.Connection(blobRegion, out blobRegion);
                    }
                    HOperatorSet.FillUp(blobRegion, out blobRegion);
                }
                if (tBP.isErosion)
                {
                    if (tBP.erosionElementNUM == 0)
                    {
                        HOperatorSet.ErosionRectangle1(blobRegion, out blobRegion, (double)tBP.erosionRWidth, (double)tBP.erosionRHeight);
                    }
                    else
                    {
                        HOperatorSet.ErosionCircle(blobRegion, out blobRegion, (double)tBP.erosionCRadius);
                    }
                }
                if (tBP.isDilation)
                {
                    if (tBP.dilationElementNUM == 0)
                    {
                        HOperatorSet.DilationRectangle1(blobRegion, out blobRegion, (double)tBP.dilationRWidth, (double)tBP.dilationRHeight);
                    }
                    else
                    {
                        HOperatorSet.DilationCircle(blobRegion, out blobRegion, (double)tBP.dilationCRadius);
                    }
                }
                if (tBP.isOpening)
                {
                    if (tBP.openingElementNUM == 0)
                    {
                        HOperatorSet.ErosionRectangle1(blobRegion, out blobRegion, (double)tBP.openingRWidth, (double)tBP.openingRHeight);
                        HOperatorSet.Connection(blobRegion, out blobRegion);
                        HOperatorSet.DilationRectangle1(blobRegion, out blobRegion, (double)tBP.openingRWidth, (double)tBP.openingRHeight);
                    }
                    else
                    {
                        HOperatorSet.ErosionCircle(blobRegion, out blobRegion, (double)tBP.openingCRadius);
                        HOperatorSet.Connection(blobRegion, out blobRegion);
                        HOperatorSet.DilationCircle(blobRegion, out blobRegion, (double)tBP.openingCRadius);
                    }
                }
                if (tBP.isClosing)
                {
                    if (tBP.closingElementNUM == 0)
                    {
                        HOperatorSet.DilationRectangle1(blobRegion, out blobRegion, (double)tBP.closingRWidth, (double)tBP.closingRHeight);
                        HOperatorSet.ErosionRectangle1(blobRegion, out blobRegion, (double)tBP.closingRWidth, (double)tBP.closingRHeight);
                    }
                    else
                    {
                        HOperatorSet.DilationCircle(blobRegion, out blobRegion, (double)tBP.closingCRadius);
                        HOperatorSet.ErosionCircle(blobRegion, out blobRegion, (double)tBP.closingCRadius);

                    }
                }
                HOperatorSet.Connection(blobRegion, out blobRegion);
                HTuple selectMin1, selectMax1, feature1;
                selectMin1 = null;
                selectMax1 = null;
                feature1 = null;
                HTuple selectMin2, selectMax2, feature2;
                selectMin2 = null;
                selectMax2 = null;
                feature2 = null;
                for (int i = 0; i < 30; i++)
                {
                    if (tBP.selectIsChecked[i])
                    {
                        if (selectMin1 == null)
                        {
                            selectMin1 = tBP.selectMin[i];
                            selectMax1 = tBP.selectMax[i];
                            feature1 = (HTuple)tBP.selectSTR[i];
                        }
                        else
                        {
                            selectMin1 = selectMin1.TupleConcat(tBP.selectMin[i]);
                            selectMax1 = selectMax1.TupleConcat(tBP.selectMax[i]);
                            feature1 = feature1.TupleConcat((HTuple)tBP.selectSTR[i]);
                        }
                    }
                }
                if (feature1 != null)
                {
                    if (tBP.selectisAnd)
                    {
                        HOperatorSet.SelectShape(blobRegion, out blobRegion, feature1, "and", selectMin1, selectMax1);
                    }
                    else
                    {
                        HOperatorSet.SelectShape(blobRegion, out blobRegion, feature1, "or", selectMin1, selectMax1);
                    }
                }

                for (int i = 30; i < 34; i++)
                {
                    if (tBP.selectIsChecked[i])
                    {
                        if (selectMin2 == null)
                        {
                            selectMin2 = tBP.selectMin[i];
                            selectMax2 = tBP.selectMax[i];
                            feature2 = (HTuple)tBP.selectSTR[i];
                        }
                        else
                        {
                            selectMin2 = selectMin2.TupleConcat(tBP.selectMin[i]);
                            selectMax2 = selectMax2.TupleConcat(tBP.selectMax[i]);
                            feature2 = feature2.TupleConcat((HTuple)tBP.selectSTR[i]);
                        }
                    }
                }
                if (feature2 != null)
                {
                    HObject imgSelectTest;
                    if (tBP.isColor)
                    {
                        HOperatorSet.Rgb1ToGray(iptImg, out imgSelectTest);
                    }
                    else
                    {
                        HOperatorSet.CopyObj(iptImg, out imgSelectTest, 1, -1);
                    }

                    if (tBP.selectisAnd)
                    {
                        HOperatorSet.SelectGray(blobRegion, imgSelectTest, out blobRegion, feature2, "and", selectMin2, selectMax2);
                    }
                    else
                    {
                        HOperatorSet.SelectGray(blobRegion, imgSelectTest, out blobRegion, feature2, "or", selectMin2, selectMax2);
                    }
                    if (imgSelectTest != null)
                    {
                        imgSelectTest.Dispose();
                    }

                }
                string[] arrangeSelectStr = new string[6] { "row", "column", "row1", "column1", "row2", "column2" };
                if (blobRegion.CountObj() != 0)
                {
                    HTuple featureValue, sortedFeatureValue, sortedFeatureValueIndices;
                    HOperatorSet.RegionFeatures(blobRegion, arrangeSelectStr[tBP.selectArrangeItemIndex], out featureValue);
                    if (tBP.isArrangeLtoS)
                    {
                        HOperatorSet.TupleSort(featureValue, out sortedFeatureValue);
                        HOperatorSet.TupleSortIndex(featureValue, out sortedFeatureValueIndices);
                        HOperatorSet.TupleInverse(sortedFeatureValue, out sortedFeatureValue);
                        HOperatorSet.TupleInverse(sortedFeatureValueIndices, out sortedFeatureValueIndices);
                    }
                    else
                    {
                        HOperatorSet.TupleSort(featureValue, out sortedFeatureValue);
                        HOperatorSet.TupleSortIndex(featureValue, out sortedFeatureValueIndices);
                    }
                    sortedFeatureValueIndices = sortedFeatureValueIndices + 1;
                    if (blobRegion.CountObj() > tBP.regionNum)
                    {
                        HOperatorSet.SelectObj(blobRegion, out blobRegion, sortedFeatureValueIndices.TupleSelectRange(0, (HTuple)tBP.regionNum - 1));
                    }
                    else
                    {
                        HOperatorSet.SelectObj(blobRegion, out blobRegion, sortedFeatureValueIndices);
                    }
                }
                HObject imgSelectTest1;
                if (tBP.isColor == true)
                {
                    HOperatorSet.Rgb1ToGray(iptImg, out imgSelectTest1);
                }
                else
                {
                    HOperatorSet.CopyObj(iptImg, out imgSelectTest1, 1, -1);
                }
                int[] checkedItemIndex = new int[tBP.selectItemCount];
                int ii = 0;
                for (int i = 0; i < 34; i++)
                {

                    if (tBP.outputIDIsChecked[i])
                    {

                        checkedItemIndex[ii] = i;
                        ii++;
                    }
                }
                for (int i = 0; i < UserCode.GetInstance().gProCd[idx].doubleData.Length; i++)
                {
                    UserCode.GetInstance().gProCd[idx].doubleData[i] = 0;
                }
                double[] doubleArray = new double[blobRegion.CountObj()];
                UserCode.GetInstance().gProCd[idx].doubleData[0] = blobRegion.CountObj();
                for (int j = 0; j < tBP.selectItemCount; j++)
                {
                    HTuple testFeatureValue = null;
                    HTuple testFeatureValue1 = null;
                    HTuple realFeatureValue = null;
                    double[] testFeatureValueDouble = new double[blobRegion.CountObj()];
                    if (ConfigInformation.GetInstance().tCalCfg.mx != null && ConfigInformation.GetInstance().tCalCfg.useThreePointCalibration == true)
                    {
                        if (checkedItemIndex[j] < 30)
                        {
                            switch (checkedItemIndex[j])
                            {
                                case 1:
                                    {
                                        HOperatorSet.RegionFeatures(blobRegion, (HTuple)tBP.selectSTR[2], out testFeatureValue);
                                        HOperatorSet.RegionFeatures(blobRegion, (HTuple)tBP.selectSTR[1], out testFeatureValue1);
                                        XYToReal(checkedItemIndex[j], testFeatureValue, testFeatureValue1, out  realFeatureValue);
                                        break;
                                    }
                                case 2:
                                    {
                                        HOperatorSet.RegionFeatures(blobRegion, (HTuple)tBP.selectSTR[2], out testFeatureValue);
                                        HOperatorSet.RegionFeatures(blobRegion, (HTuple)tBP.selectSTR[1], out testFeatureValue1);
                                        XYToReal(checkedItemIndex[j], testFeatureValue, testFeatureValue1, out  realFeatureValue);
                                        break;
                                    }
                                case 5:
                                    {
                                        HOperatorSet.RegionFeatures(blobRegion, (HTuple)tBP.selectSTR[6], out testFeatureValue);
                                        HOperatorSet.RegionFeatures(blobRegion, (HTuple)tBP.selectSTR[5], out testFeatureValue1);
                                        XYToReal(checkedItemIndex[j], testFeatureValue, testFeatureValue1, out  realFeatureValue);
                                        break;
                                    }
                                case 6:
                                    {
                                        HOperatorSet.RegionFeatures(blobRegion, (HTuple)tBP.selectSTR[6], out testFeatureValue);
                                        HOperatorSet.RegionFeatures(blobRegion, (HTuple)tBP.selectSTR[5], out testFeatureValue1);
                                        XYToReal(checkedItemIndex[j], testFeatureValue, testFeatureValue1, out  realFeatureValue);
                                        break;
                                    }
                                case 7:
                                    {
                                        HOperatorSet.RegionFeatures(blobRegion, (HTuple)tBP.selectSTR[8], out testFeatureValue);
                                        HOperatorSet.RegionFeatures(blobRegion, (HTuple)tBP.selectSTR[7], out testFeatureValue1);
                                        XYToReal(checkedItemIndex[j], testFeatureValue, testFeatureValue1, out  realFeatureValue);
                                        break;
                                    }
                                case 8:
                                    {
                                        HOperatorSet.RegionFeatures(blobRegion, (HTuple)tBP.selectSTR[8], out testFeatureValue);
                                        HOperatorSet.RegionFeatures(blobRegion, (HTuple)tBP.selectSTR[7], out testFeatureValue1);
                                        XYToReal(checkedItemIndex[j], testFeatureValue, testFeatureValue1, out  realFeatureValue);
                                        break;
                                    }
                                default:
                                    {
                                        HOperatorSet.RegionFeatures(blobRegion, (HTuple)tBP.selectSTR[checkedItemIndex[j]], out testFeatureValue);
                                        featureToReal(checkedItemIndex[j], testFeatureValue, out  realFeatureValue);
                                        break;
                                    }
                            }


                        }
                        else
                        {
                            HOperatorSet.GrayFeatures(blobRegion, imgSelectTest1, (HTuple)tBP.selectSTR[checkedItemIndex[j]], out testFeatureValue);
                            featureToReal(checkedItemIndex[j], testFeatureValue, out  realFeatureValue);
                        }
                    }
                    else
                    {
                        if (checkedItemIndex[j] < 30)
                        {
                            HOperatorSet.RegionFeatures(blobRegion, (HTuple)tBP.selectSTR[checkedItemIndex[j]], out realFeatureValue);
                        }
                        else
                        {
                            HOperatorSet.GrayFeatures(blobRegion, imgSelectTest1, (HTuple)tBP.selectSTR[checkedItemIndex[j]], out realFeatureValue);
                        }
                    }

                    
                    for (int i = 0; i < blobRegion.CountObj(); i++)
                    {
                        testFeatureValueDouble[i] = realFeatureValue[i].D;
                        UserCode.GetInstance().gProCd[idx].doubleData[i * tBP.selectItemCount + j + 1] = testFeatureValueDouble[i];
                    }
                    resultList.Add(testFeatureValueDouble);

                }


                if (imgSelectTest1 != null)
                {
                    imgSelectTest1.Dispose();
                }
                if (outputImg != null)
                {
                    outputImg.Dispose();
                }

                HOperatorSet.CopyImage(iptImg, out outputImg);
            }
            catch (System.Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return outputImg;
        }
        private void XYToReal(int i, HTuple testFeatureValue, HTuple testFeatureValue1, out HTuple realFeatureValue)
        {
            if (i==1||i==5||i==7)
            {
                realFeatureValue = ConfigInformation.GetInstance().tCalCfg.mx[2, 0] * testFeatureValue + ConfigInformation.GetInstance().tCalCfg.mx[3, 0] * testFeatureValue1 + ConfigInformation.GetInstance().tCalCfg.mx[5, 0];
            } 
            else
            {
                realFeatureValue = ConfigInformation.GetInstance().tCalCfg.mx[0, 0] * testFeatureValue + ConfigInformation.GetInstance().tCalCfg.mx[1, 0] * testFeatureValue1 + ConfigInformation.GetInstance().tCalCfg.mx[4, 0];
            }                            
        }

        private void featureToReal(int i, HTuple intputFeature, out HTuple outputFeature)
        {
            //"面积","重心Y","重心X","区域宽度","区域高度","区域左上角Y坐标","区域左上角X坐标","区域右下角Y坐标","区域右下角X坐标",
            //"圆形近似度","紧密度","轮廓长度","凸性","矩形近似度","等效椭圆长轴半径长度","等效椭圆短轴半径长度","等效椭圆方向",
            //"椭圆参数：长短轴比值","最小外接圆半径","最大内接圆半径","最大内接矩形宽度","最大内接矩形高度","多边形边数","区域内洞数",
            //  "所有洞的面积","最大直径"," 区域方向","最小外接矩形方向","最小外接矩形长度","最小外接矩形宽度",
            //  "区域灰度最小值","区域灰度最大值","区域灰度平均值","区域灰度标准差"

            // "area","row","column","width","height","row1","column1","row2","column2","circularity","compactness","contlength","convexity"
            // ,"rectangularity","ra","rb","phi","anisometry","outer_radius","inner_radius","inner_width","inner_height","num_sides","holes_num",
            //  "area_holes","max_diameter","orientation","rect2_phi","rect2_len1","rect2_len2",/*select gray*/"min","max","mean","deviation";

            outputFeature = intputFeature;
            switch (i)
            {
                case 0: //面积;
                    outputFeature = System.Math.Abs(ConfigInformation.GetInstance().tCalCfg.Sx * ConfigInformation.GetInstance().tCalCfg.Sy) * intputFeature;
                    break;
                case 1://重心Y;
                    break;
                case 2://重心X;
                    break;
                case 3://区域宽度;
                    outputFeature = System.Math.Abs(ConfigInformation.GetInstance().tCalCfg.Sx) * intputFeature;
                    break;
                case 4://区域高度;
                    outputFeature = System.Math.Abs(ConfigInformation.GetInstance().tCalCfg.Sy) * intputFeature;
                    break;
                case 5://区域左上角Y坐标;
                    break;
                case 6://区域左上角X坐标;
                    break;
                case 7://区域右下角Y坐标;
                    break;
                case 8://区域右下角X坐标;
                    break;
                case 9://圆形近似度;
                    outputFeature = intputFeature;
                    break;
                case 10://紧密度;
                    outputFeature = intputFeature;
                    break;
                case 11://轮廓长度;
                    outputFeature = (System.Math.Abs(ConfigInformation.GetInstance().tCalCfg.Sx) + System.Math.Abs(ConfigInformation.GetInstance().tCalCfg.Sy)) / 2.0 * intputFeature;
                    break;
                case 12://凸性;
                    outputFeature = intputFeature;
                    break;
                case 13://矩形近似度;
                    outputFeature = intputFeature;
                    break;
                case 14://等效椭圆长轴半径长度;
                    outputFeature = (System.Math.Abs(ConfigInformation.GetInstance().tCalCfg.Sx) + System.Math.Abs(ConfigInformation.GetInstance().tCalCfg.Sy)) / 2.0 * intputFeature;
                    break;
                case 15://等效椭圆短轴半径长度;
                    outputFeature = (System.Math.Abs(ConfigInformation.GetInstance().tCalCfg.Sx) + System.Math.Abs(ConfigInformation.GetInstance().tCalCfg.Sy)) / 2.0 * intputFeature;
                    break;
                case 16://等效椭圆方向;
                    outputFeature = intputFeature;
                    break;
                case 17://椭圆参数：长短轴比值;
                    outputFeature = intputFeature;
                    break;
                case 18://最小外接圆半径;
                    outputFeature = (System.Math.Abs(ConfigInformation.GetInstance().tCalCfg.Sx) + System.Math.Abs(ConfigInformation.GetInstance().tCalCfg.Sy)) / 2.0 * intputFeature;
                    break;
                case 19://最大内接圆半径;
                    outputFeature = (System.Math.Abs(ConfigInformation.GetInstance().tCalCfg.Sx) + System.Math.Abs(ConfigInformation.GetInstance().tCalCfg.Sy)) / 2.0 * intputFeature;
                    break;
                case 20:// 最大内接矩形宽度;
                    outputFeature = System.Math.Abs(ConfigInformation.GetInstance().tCalCfg.Sx) * intputFeature;
                    break;
                case 21:// 最大内接矩形高度;
                    outputFeature = System.Math.Abs(ConfigInformation.GetInstance().tCalCfg.Sy) * intputFeature;
                    break;
                case 22:// 多边形边数;
                    outputFeature = intputFeature;
                    break;
                case 23:// 区域内洞数;
                    outputFeature = intputFeature;
                    break;
                case 24:// 所有洞的面积;
                    outputFeature = System.Math.Abs(ConfigInformation.GetInstance().tCalCfg.Sx * ConfigInformation.GetInstance().tCalCfg.Sy) * intputFeature;
                    break;
                case 25://最大直径;
                    outputFeature = (System.Math.Abs(ConfigInformation.GetInstance().tCalCfg.Sx) + System.Math.Abs(ConfigInformation.GetInstance().tCalCfg.Sy)) / 2.0 * intputFeature;
                    break;
                case 26:// 区域方向;
                    outputFeature = intputFeature;
                    break;
                case 27:// 最小外接矩形方向;
                    outputFeature = intputFeature;
                    break;
                case 28:// 最小外接矩形长度;
                    outputFeature = (System.Math.Abs(ConfigInformation.GetInstance().tCalCfg.Sx) + System.Math.Abs(ConfigInformation.GetInstance().tCalCfg.Sy)) / 2.0 * intputFeature;
                    break;
                case 29:// 最小外接矩形宽度;
                    outputFeature = (System.Math.Abs(ConfigInformation.GetInstance().tCalCfg.Sx) + System.Math.Abs(ConfigInformation.GetInstance().tCalCfg.Sy)) / 2.0 * intputFeature;
                    break;
                case 30://区域灰度最小值; 
                    outputFeature = intputFeature;
                    break;
                case 31:// 区域灰度最大值; 
                    outputFeature = intputFeature;
                    break;
                case 32:// 区域灰度平均值;
                    outputFeature = intputFeature;
                    break;
                case 33:// 区域灰度标准差;
                    outputFeature = intputFeature;
                    break;
            }
        }
        private void featureToRealShow(int i, string intputStr, out string outputStr)
        {
            //"面积","重心Y","重心X","区域宽度","区域高度","区域左上角Y坐标","区域左上角X坐标","区域右下角Y坐标","区域右下角X坐标",
            //"圆形近似度","紧密度","轮廓长度","凸性","矩形近似度","等效椭圆长轴半径长度","等效椭圆短轴半径长度","等效椭圆方向",
            //"椭圆参数：长短轴比值","最小外接圆半径","最大内接圆半径","最大内接矩形宽度","最大内接矩形高度","多边形边数","区域内洞数",
            //  "所有洞的面积","最大直径"," 区域方向","最小外接矩形方向","最小外接矩形长度","最小外接矩形宽度",
            //  "区域灰度最小值","区域灰度最大值","区域灰度平均值","区域灰度标准差"

            // "area","row","column","width","height","row1","column1","row2","column2","circularity","compactness","contlength","convexity"
            // ,"rectangularity","ra","rb","phi","anisometry","outer_radius","inner_radius","inner_width","inner_height","num_sides","holes_num",
            //  "area_holes","max_diameter","orientation","rect2_phi","rect2_len1","rect2_len2",/*select gray*/"min","max","mean","deviation";


            switch (i)
            {
                case 0: //面积;
                    outputStr = intputStr + "(mm^2)";
                    break;
                case 1://重心Y;
                    outputStr = intputStr + "(mm)";
                    break;
                case 2://重心X;
                    outputStr = intputStr + "(mm)";
                    break;
                case 3://区域宽度;
                    outputStr = intputStr + "(mm)";
                    break;
                case 4://区域高度;
                    outputStr = intputStr + "(mm)";
                    break;
                case 5://区域左上角Y坐标;
                    outputStr = intputStr + "(mm)";
                    break;
                case 6://区域左上角X坐标;
                    outputStr = intputStr + "(mm)";
                    break;
                case 7://区域右下角Y坐标;
                    outputStr = intputStr + "(mm)";
                    break;
                case 8://区域右下角X坐标;
                    outputStr = intputStr + "(mm)";
                    break;
                case 9://圆形近似度;
                    outputStr = intputStr;
                    break;
                case 10://紧密度;
                    outputStr = intputStr;
                    break;
                case 11://轮廓长度;
                    outputStr = intputStr + "(mm)";
                    break;
                case 12://凸性;
                    outputStr = intputStr;
                    break;
                case 13://矩形近似度;
                    outputStr = intputStr;
                    break;
                case 14://等效椭圆长轴半径长度;
                    outputStr = intputStr + "(mm)";
                    break;
                case 15://等效椭圆短轴半径长度;
                    outputStr = intputStr + "(mm)";
                    break;
                case 16://等效椭圆方向;
                    outputStr = intputStr;
                    break;
                case 17://椭圆参数：长短轴比值;
                    outputStr = intputStr;
                    break;
                case 18://最小外接圆半径;
                    outputStr = intputStr + "(mm)";
                    break;
                case 19://最大内接圆半径;
                    outputStr = intputStr + "(mm)";
                    break;
                case 20:// 最大内接矩形宽度;
                    outputStr = intputStr + "(mm)";
                    break;
                case 21:// 最大内接矩形高度;
                    outputStr = intputStr + "(mm)";
                    break;
                case 22:// 多边形边数;
                    outputStr = intputStr;
                    break;
                case 23:// 区域内洞数;
                    outputStr = intputStr;
                    break;
                case 24:// 所有洞的面积;
                    outputStr = intputStr + "(mm^2)";
                    break;
                case 25://最大直径;
                    outputStr = intputStr + "(mm)";
                    break;
                case 26:// 区域方向;
                    outputStr = intputStr;
                    break;
                case 27:// 最小外接矩形方向;
                    outputStr = intputStr;
                    break;
                case 28:// 最小外接矩形长度;
                    outputStr = intputStr + "(mm)";
                    break;
                case 29:// 最小外接矩形宽度;
                    outputStr = intputStr + "(mm)";
                    break;
                case 30://区域灰度最小值; 
                    outputStr = intputStr;
                    break;
                case 31:// 区域灰度最大值; 
                    outputStr = intputStr;
                    break;
                case 32:// 区域灰度平均值;
                    outputStr = intputStr;
                    break;
                case 33:// 区域灰度标准差;
                    outputStr = intputStr;
                    break;
                default:
                    outputStr = intputStr;
                    break;
            }
        }
        public void ShowImageFunc()
        {
            if (blobRegion != null && blobRegion.CountObj() != 0 && tBP.showOutputResultFlag[0])
            {
                double widRat = Svision.GetMe().pictureBoxShowImage.Width / ((double)Svision.GetMe().columnNumber);
                double heiRat = Svision.GetMe().pictureBoxShowImage.Height / ((double)Svision.GetMe().rowNumber);
                double resizerate = widRat < heiRat ? widRat : heiRat;

                HTuple harea, hrow, hcolumn;
                HOperatorSet.AreaCenter(blobRegion, out harea, out hrow, out hcolumn);

                for (int i = 0; i < hrow.TupleLength(); i++)
                {
                    HObject zoomBlobRegion;
                    HOperatorSet.ZoomRegion(blobRegion, out zoomBlobRegion, resizerate, resizerate);
                    if (HDevWindowStack.IsOpen())
                    {
                        HOperatorSet.SetColor(Svision.GetMe().hvWindowHandle, "green");
                        HOperatorSet.SetLineWidth(Svision.GetMe().hvWindowHandle, 4);
                        HOperatorSet.SetDraw(Svision.GetMe().hvWindowHandle, "margin");
                        HOperatorSet.DispObj(zoomBlobRegion[i + 1], Svision.GetMe().hvWindowHandle);
                        HOperatorSet.SetColor(Svision.GetMe().hvWindowHandle, "red");
                        HOperatorSet.DispCross(Svision.GetMe().hvWindowHandle, hrow[i] * resizerate, hcolumn[i] * resizerate, 10, 0);
                        HOperatorSet.SetTposition(Svision.GetMe().hvWindowHandle, hrow[i] * resizerate - 25, hcolumn[i] * resizerate - 15);
                        HOperatorSet.WriteString(Svision.GetMe().hvWindowHandle, ("重心" + i));

                    }
                    if (zoomBlobRegion != null)
                    {
                        zoomBlobRegion.Dispose();
                    }
                }
            }

            int[] checkedItemIndex = new int[tBP.selectItemCount];
            int ii = 0;
            for (int i = 0; i < 34; i++)
            {

                if (tBP.outputIDIsChecked[i])
                {

                    checkedItemIndex[ii] = i;
                    ii++;
                }
            }
            string[] strtempArray = new string[blobRegion.CountObj()];
            for (int i = 0; i < blobRegion.CountObj(); i++)
            {
                for (int j = 0; j < tBP.selectItemCount; j++)
                {
                    string strc;
                    strc = System.String.Format("{0:f}", (float)resultList[j][i]);
                    if (tBP.showOutputResultFlag[checkedItemIndex[j] + 2])
                    {
                        if (ConfigInformation.GetInstance().tCalCfg.mx != null && ConfigInformation.GetInstance().tCalCfg.useThreePointCalibration == true)
                        {
                            string outStr;
                            featureToRealShow(checkedItemIndex[j], " ", out outStr);
                            if (strtempArray[i] == null)
                            {
                                strtempArray[i] = tBP.outputShowStr[checkedItemIndex[j]] + ": " +strc +outStr+  "\r\n";
                            }
                            else
                            {
                                strtempArray[i] = strtempArray[i] + tBP.outputShowStr[checkedItemIndex[j]] + ": "+ strc +outStr + "\r\n";
                            }
                        }
                        else
                        {
                            if (strtempArray[i] == null)
                            {
                                strtempArray[i] = tBP.outputShowStr[checkedItemIndex[j]] + ": " + strc + "\r\n";
                            }
                            else
                            {
                                strtempArray[i] = strtempArray[i] + tBP.outputShowStr[checkedItemIndex[j]] + ": " + strc + "\r\n";
                            }
                        }
                        
                    }

                }
            }
            string outputStr = "\r\n";
            if (tBP.showOutputResultFlag[1])
            {
                outputStr = tBP.showOutputResultStr[1] + ": " + blobRegion.CountObj() + "\r\n" + outputStr;
            }
            for (int i = 0; i < blobRegion.CountObj(); i++)
            {
                outputStr = outputStr + "\r\n" + "区域" + i.ToString() + "\r\n" + strtempArray[i];
            }
            Svision.GetMe().textBoxResultShow.Text = outputStr;
        }
    }
    public class MorphologyProcessingFBD
    {
        HObject imgRegionResult;
        HObject imgRegion;
        public HObject inputImg;
        public HObject outputImg;
        private int processID;
        private int elementID;
        private int width;
        private int height;
        private double radius;

        public MorphologyProcessingFBD(HObject ipImg, ProCodeCls.MorphologyProcessingPara tmpp)
        {
            // outputImg = new HObject();
            inputImg = ipImg;
            processID = tmpp.processID;
            elementID = tmpp.elementID;
            width = tmpp.width;
            height = tmpp.height;
            radius = tmpp.radius;
        }

        ~MorphologyProcessingFBD()
        {
            if (imgRegionResult != null)
            {
                imgRegionResult.Dispose();
            }
            if (imgRegion != null)
            {
                imgRegion.Dispose();
            }
            if (inputImg != null)
            {
                inputImg.Dispose();
            }
            if (outputImg != null)
            {
                outputImg.Dispose();
            }
        }

        public HObject MorphologyProcessing(HObject iptImg)
        {
            basicClass.thresholdImage(iptImg, out imgRegionResult, 128, 255);
            switch (processID)
            {
                case 0:
                    if (elementID == 0)
                    {
                        HOperatorSet.ErosionRectangle1(imgRegionResult, out imgRegion, width, height);
                    }
                    else
                    {
                        HOperatorSet.ErosionCircle(imgRegionResult, out imgRegion, radius);
                    }

                    break;
                case 1:
                    if (elementID == 0)
                    {
                        HOperatorSet.DilationRectangle1(imgRegionResult, out imgRegion, width, height);
                    }
                    else
                    {
                        HOperatorSet.DilationCircle(imgRegionResult, out imgRegion, radius);
                    }
                    break;
                case 2:
                    if (elementID == 0)
                    {
                        HOperatorSet.OpeningRectangle1(imgRegionResult, out imgRegion, width, height);
                    }
                    else
                    {
                        HOperatorSet.OpeningCircle(imgRegionResult, out imgRegion, radius);
                    }
                    break;
                case 3:
                    if (elementID == 0)
                    {
                        HOperatorSet.ClosingRectangle1(imgRegionResult, out imgRegion, width, height);
                    }
                    else
                    {
                        HOperatorSet.ClosingCircle(imgRegionResult, out imgRegion, radius);
                    }
                    break;

                default:
                    break;
            }
            if (outputImg != null)
            {
                outputImg.Dispose();
            }
            HOperatorSet.RegionToBin(imgRegion, out outputImg, 255, 0,
                                Svision.GetMe().oriColumnNumber, Svision.GetMe().oriRowNumber);
            return outputImg;
        }
    }
    public class ShapeSearchFBD
    {
        public string[] showOutputResultStr;
        public bool[] showOutputResultFlag;
        public HObject inputImg;
        public HObject outputImg;
        public int arrangeIndex;
        public HTuple tShpMdl;
        public bool ifMulMode;
        public int idx;
        HTuple hRow, hColumn, hAngle, hScore, hTemplateID, sortedFeatureValueIndices;
        findShapeModelClass.findShapeModelPara tFndShpMdlPara;
        findShapeModelClass.findShapeModelsPara tFndShpSMdlPara;
        public ShapeSearchFBD(HObject ipImg, ProCodeCls.ShapeSearchPara tSSP, int index)
        {
            inputImg = ipImg;
            outputImg = new HObject();
            idx = index;
            arrangeIndex = tSSP.arrangeIndex;
            UserCode.GetInstance().gProCd[idx].doubleData = new double[tSSP.Max_Object_Num * 5 + 1];
            showOutputResultStr = tSSP.showOutputResultStr;
            showOutputResultFlag = tSSP.showOutputResultFlag;

            if (tSSP.shapeModel != null)
            {

                if (tSSP.shapeModel.TupleLength() <= 0)
                    return;
                if (tSSP.shapeModel.TupleLength() > ProCodeCls.ShapeSearchPara.MAX_SHAPE_MODEL)
                    return;

                // ifMulMode = UserCode.GetInstance().gProCd[idx].boolData[2];
                int i_num = 0;
                for (int i = 0; i < ProCodeCls.ShapeSearchPara.MAX_SHAPE_MODEL; i++)
                {
                    if (tSSP.modelIsChecked[i])
                    {
                        //tshapeModelPoints.Add(tSSP.shapeModelPoints[i]);
                        if (tShpMdl == null)
                        {
                            tShpMdl = tSSP.shapeModel[i];
                            //tshapeModelRegion = tSSP.shapeModelRegion[i+1];
                        }
                        else
                        {
                            tShpMdl = tShpMdl.TupleConcat(tSSP.shapeModel[i]);
                            // HOperatorSet.ConcatObj(tshapeModelRegion, tSSP.shapeModelRegion[i + 1], out tshapeModelRegion);
                        }
                        i_num++;
                    }
                }
                if (tShpMdl.TupleLength() == 1)     //单模板;
                {
                    int[] numberLevelsTemp = new int[2];
                    int[] subPixelTemp = new int[2];
                    numberLevelsTemp[0] = tSSP.numLevels[(ProCodeCls.ShapeSearchPara.MAX_SHAPE_MUL_PARA_NUM - 1) * 2];
                    numberLevelsTemp[1] = tSSP.numLevels[(ProCodeCls.ShapeSearchPara.MAX_SHAPE_MUL_PARA_NUM - 1) * 2 + 1];
                    subPixelTemp[0] = tSSP.subPixel[(ProCodeCls.ShapeSearchPara.MAX_SHAPE_MUL_PARA_NUM - 1) * 2];
                    subPixelTemp[1] = tSSP.subPixel[(ProCodeCls.ShapeSearchPara.MAX_SHAPE_MUL_PARA_NUM - 1) * 2 + 1];
                    tFndShpMdlPara = new findShapeModelClass.findShapeModelPara(tSSP.angleStart[ProCodeCls.ShapeSearchPara.MAX_SHAPE_MUL_PARA_NUM - 1]
                        , tSSP.angleExtent[ProCodeCls.ShapeSearchPara.MAX_SHAPE_MUL_PARA_NUM - 1], tSSP.minScore[ProCodeCls.ShapeSearchPara.MAX_SHAPE_MUL_PARA_NUM - 1], tSSP.numMatches[ProCodeCls.ShapeSearchPara.MAX_SHAPE_MUL_PARA_NUM - 1], tSSP.maxOverlap, subPixelTemp,
                        numberLevelsTemp, tSSP.greediness[ProCodeCls.ShapeSearchPara.MAX_SHAPE_MUL_PARA_NUM - 1]);
                }
                else                                        //多模板
                {


                    if (tSSP.isMultiplePara)       //多套模板参数;
                    {
                        double[] angleStartTemp = new double[tShpMdl.TupleLength()];
                        double[] angleExtentTemp = new double[tShpMdl.TupleLength()];
                        double[] minScoreTemp = new double[tShpMdl.TupleLength()];
                        int[] numMatchesTemp = new int[tShpMdl.TupleLength()];
                        double maxOverlapTemp = tSSP.maxOverlap;
                        double[] greedinessTemp = new double[tShpMdl.TupleLength()];
                        int[] numberLevelsTemp = new int[2 * tShpMdl.TupleLength()];
                        int[] subPixelTemp = new int[2 * tShpMdl.TupleLength()];
                        int i_test = 0;
                        for (int i = 0; i < tSSP.shapeModel.TupleLength(); i++)
                        {
                            if (tSSP.modelIsChecked[i])
                            {
                                angleStartTemp[i_test] = tSSP.angleStart[i];
                                angleExtentTemp[i_test] = tSSP.angleExtent[i];
                                minScoreTemp[i_test] = tSSP.minScore[i];
                                numMatchesTemp[i_test] = tSSP.numMatches[i];

                                greedinessTemp[i_test] = tSSP.greediness[i];
                                numberLevelsTemp[i_test * 2] = tSSP.numLevels[i * 2];
                                numberLevelsTemp[i_test * 2 + 1] = tSSP.numLevels[i * 2 + 1];
                                subPixelTemp[i_test * 2] = tSSP.subPixel[i * 2];
                                subPixelTemp[i_test * 2 + 1] = tSSP.subPixel[i * 2 + 1];
                                i_test++;
                            }

                        }

                        tFndShpSMdlPara = new findShapeModelClass.findShapeModelsPara(tShpMdl.TupleLength(), angleStartTemp, angleExtentTemp,
                                              minScoreTemp, numMatchesTemp, maxOverlapTemp, subPixelTemp, numberLevelsTemp, greedinessTemp);
                    }
                    else                                                        //单套模板参数;
                    {

                        double[] angleStartTemp = new double[1];
                        double[] angleExtentTemp = new double[1];
                        double[] minScoreTemp = new double[1];
                        int[] numMatchesTemp = new int[1];
                        double maxOverlapTemp;
                        double[] greedinessTemp = new double[1];
                        angleStartTemp[0] = tSSP.angleStart[(ProCodeCls.ShapeSearchPara.MAX_SHAPE_MUL_PARA_NUM - 1)];
                        angleExtentTemp[0] = tSSP.angleExtent[(ProCodeCls.ShapeSearchPara.MAX_SHAPE_MUL_PARA_NUM - 1)];
                        minScoreTemp[0] = tSSP.minScore[(ProCodeCls.ShapeSearchPara.MAX_SHAPE_MUL_PARA_NUM - 1)];
                        numMatchesTemp[0] = tSSP.numMatches[(ProCodeCls.ShapeSearchPara.MAX_SHAPE_MUL_PARA_NUM - 1)];
                        maxOverlapTemp = tSSP.maxOverlap;
                        greedinessTemp[0] = tSSP.greediness[(ProCodeCls.ShapeSearchPara.MAX_SHAPE_MUL_PARA_NUM - 1)];
                        int[] numberLevelsTemp = new int[2];
                        int[] subPixelTemp = new int[2];
                        numberLevelsTemp[0] = tSSP.numLevels[(ProCodeCls.ShapeSearchPara.MAX_SHAPE_MUL_PARA_NUM - 1) * 2];
                        numberLevelsTemp[1] = tSSP.numLevels[(ProCodeCls.ShapeSearchPara.MAX_SHAPE_MUL_PARA_NUM - 1) * 2 + 1];
                        subPixelTemp[0] = tSSP.subPixel[(ProCodeCls.ShapeSearchPara.MAX_SHAPE_MUL_PARA_NUM - 1) * 2];
                        subPixelTemp[1] = tSSP.subPixel[(ProCodeCls.ShapeSearchPara.MAX_SHAPE_MUL_PARA_NUM - 1) * 2 + 1];
                        tFndShpSMdlPara = new findShapeModelClass.findShapeModelsPara(tShpMdl.TupleLength(), angleStartTemp, angleExtentTemp,
                                                    minScoreTemp, numMatchesTemp, maxOverlapTemp, subPixelTemp, numberLevelsTemp, greedinessTemp);
                    }
                }
            }
        }
        ~ShapeSearchFBD()
        {
            if (inputImg != null)
            {
                inputImg.Dispose();
            }
            if (outputImg != null)
            {
                outputImg.Dispose();
            }


        }
        public HObject ShapeSearching()
        {

            inputImg = UserCode.GetInstance().gProCd[idx].inImage;
            if (tShpMdl != null)
            {
                if (1 == tShpMdl.TupleLength())     //单模板
                {
                    try
                    {
                        hRow = null;
                        hColumn = null;
                        hAngle = null;
                        hScore = null;
                        sortedFeatureValueIndices = null;
                        HOperatorSet.FindShapeModel(inputImg, tShpMdl, tFndShpMdlPara.angleStart,
                            tFndShpMdlPara.angleExtent, tFndShpMdlPara.minScore, tFndShpMdlPara.numMatches,
                            tFndShpMdlPara.maxOverlap, tFndShpMdlPara.subPixel, tFndShpMdlPara.numLevels,
                            tFndShpMdlPara.greediness, out hRow, out hColumn, out hAngle, out hScore);
                        if (hRow.TupleLength() != 0 && hRow.TupleLength() > 1)
                        {
                            switch (arrangeIndex)
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
                        
                        for (int i = 0; i < UserCode.GetInstance().gProCd[idx].doubleData.Length; i++)
                        {
                            UserCode.GetInstance().gProCd[idx].doubleData[i] = 0;
                        }

                        for (int i = 0; i < hRow.TupleLength(); i++)
                        {
                            if (ConfigInformation.GetInstance().tCalCfg.mx != null && ConfigInformation.GetInstance().tCalCfg.useThreePointCalibration == true)
                            {
                                double x = hColumn[i].D;
                                double y = hRow[i].D;
                                UserCode.GetInstance().gProCd[idx].doubleData[i * 5 + 1] = ConfigInformation.GetInstance().tCalCfg.mx[0, 0] * x + ConfigInformation.GetInstance().tCalCfg.mx[1, 0] * y + ConfigInformation.GetInstance().tCalCfg.mx[4, 0];
                                UserCode.GetInstance().gProCd[idx].doubleData[i * 5 + 2] = ConfigInformation.GetInstance().tCalCfg.mx[2, 0] * x + ConfigInformation.GetInstance().tCalCfg.mx[3, 0] * y + ConfigInformation.GetInstance().tCalCfg.mx[5, 0];

                            }
                            else
                            {
                                UserCode.GetInstance().gProCd[idx].doubleData[i * 5 + 1] = hColumn[i].D;
                                UserCode.GetInstance().gProCd[idx].doubleData[i * 5 + 2] = hRow[i].D;
                            }
                            UserCode.GetInstance().gProCd[idx].doubleData[i * 5 + 3] = 0.0;

                            UserCode.GetInstance().gProCd[idx].doubleData[i * 5 + 4] = (hAngle[i].D * 180 / 3.14159);             //单模板的模板ID为0
                            UserCode.GetInstance().gProCd[idx].doubleData[i * 5 + 5] = hScore[i].D;
                        }
                        UserCode.GetInstance().gProCd[idx].doubleData[0] = hRow.TupleLength();
                        //if (UserCode.GetInstance().gProCd[idx].showImg != null)
                        //{
                        //    UserCode.GetInstance().gProCd[idx].showImg.Dispose();
                        //}
                        //UserCode.GetInstance().gProCd[idx].showImg = inputImg;
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                }
                else                                //多模板
                {
                    ShapeSearchMulModel();
                }
            }
            return inputImg;
        }

        public void ShowImageFunc()
        {
            if (tShpMdl != null)
            {

                double widRat = Svision.GetMe().pictureBoxShowImage.Width / ((double)Svision.GetMe().columnNumber);
                double heiRat = Svision.GetMe().pictureBoxShowImage.Height / ((double)Svision.GetMe().rowNumber);
                double resizerate = widRat < heiRat ? widRat : heiRat;


                if (tShpMdl.TupleLength() == 1)
                {
                    if (hRow.TupleLength() != 0 && showOutputResultFlag[0])
                    {
                        HObject hoModelContours;
                        for (int i = 0; i < hRow.TupleLength(); i++)
                        {
                            HOperatorSet.GetShapeModelContours(out hoModelContours, tShpMdl[0], 1);
                            HTuple hvHomMat2D;
                            HOperatorSet.VectorAngleToRigid(0, 0, 0, hRow[i], hColumn[i], hAngle[i], out hvHomMat2D);
                            HOperatorSet.HomMat2dScale(hvHomMat2D, resizerate, resizerate, 0, 0, out hvHomMat2D);
                            HOperatorSet.AffineTransContourXld(hoModelContours, out hoModelContours, hvHomMat2D);
                            if (HDevWindowStack.IsOpen())
                            {
                                HOperatorSet.SetColor(Svision.GetMe().hvWindowHandle, "green");
                                HOperatorSet.SetLineWidth(Svision.GetMe().hvWindowHandle, 2);
                                HOperatorSet.DispObj(hoModelContours, Svision.GetMe().hvWindowHandle);
                                HOperatorSet.SetColor(Svision.GetMe().hvWindowHandle, "red");
                                HOperatorSet.SetLineWidth(Svision.GetMe().hvWindowHandle, 4);
                                HOperatorSet.DispCross(Svision.GetMe().hvWindowHandle, hRow[i] * resizerate, hColumn[i] * resizerate, 10, 0);
                                HOperatorSet.SetTposition(Svision.GetMe().hvWindowHandle, hRow[i] * resizerate - 25, hColumn[i] * resizerate - 15);
                                HOperatorSet.WriteString(Svision.GetMe().hvWindowHandle, ("Point" + i));

                            }
                            if (hoModelContours != null)
                            {
                                hoModelContours.Dispose();
                            }
                        }


                    }
                    string strtemp = "\r\n";
                    for (int i_object = 0; i_object < hRow.Length; i_object++)
                    {


                        string strc, stri, strx, stry, strad, strsd;

                        strc = System.String.Format("{0:d}", 0);
                        stri = System.String.Format("{0:d}", i_object);
                        strx = System.String.Format("{0:f}", UserCode.GetInstance().gProCd[idx].doubleData[i_object * 5 + 1]);
                        stry = System.String.Format("{0:f}", UserCode.GetInstance().gProCd[idx].doubleData[i_object * 5 + 2]);

                        strad = System.String.Format("{0:f}", hAngle[i_object].D * 180 / 3.14159);
                        strsd = System.String.Format("{0:f}", hScore[i_object].D);
                        if (showOutputResultFlag[1])
                        {
                            if (ConfigInformation.GetInstance().tCalCfg.mx != null && ConfigInformation.GetInstance().tCalCfg.useThreePointCalibration == true)
                            {
                                strtemp = strtemp + "Point" + stri + ":(" + strx + " , " + stry + ")(mm) " + "\r\n";
                            }
                            else
                            {
                                strtemp = strtemp + "Point" + stri + ":(" + strx + " , " + stry + ") " + "\r\n";
                            }
                            
                        }
                        if (showOutputResultFlag[2])
                        {
                            strtemp = strtemp + "Model: " + strc + "\r\n";
                        }
                        if (showOutputResultFlag[3])
                        {
                            strtemp = strtemp + "Angle to template（角度）:" + strad + "\r\n";
                        }
                        if (showOutputResultFlag[4])
                        {
                            strtemp = strtemp + "Score:" + strsd + "\r\n" + "\r\n";
                        }

                    }

                   Svision.GetMe().textBoxResultShow.Text = strtemp;

                }
                else
                {
                    if (hRow.TupleLength() != 0 && showOutputResultFlag[0])
                    {
                        HObject hoModelContours/*, tempContour, tempRegion;
                    HOperatorSet.GenEmptyObj(out tempRegion)*/;
                        //HOperatorSet.GenEmptyObj(out tempCross);
                        for (int i = 0; i < hRow.TupleLength(); i++)
                        {
                            HOperatorSet.GetShapeModelContours(out hoModelContours, tShpMdl[hTemplateID[i].I], 1);
                            HTuple hvHomMat2D;
                            HOperatorSet.VectorAngleToRigid(0, 0, 0, hRow[i], hColumn[i], hAngle[i], out hvHomMat2D);
                            HOperatorSet.HomMat2dScale(hvHomMat2D, resizerate, resizerate, 0, 0, out hvHomMat2D);
                            HOperatorSet.AffineTransContourXld(hoModelContours, out hoModelContours, hvHomMat2D);
                            if (HDevWindowStack.IsOpen())
                            {
                                HOperatorSet.SetColor(Svision.GetMe().hvWindowHandle, "green");
                                HOperatorSet.SetLineWidth(Svision.GetMe().hvWindowHandle, 4);
                                HOperatorSet.DispObj(hoModelContours, Svision.GetMe().hvWindowHandle);
                                HOperatorSet.SetColor(Svision.GetMe().hvWindowHandle, "red");
                                HOperatorSet.DispCross(Svision.GetMe().hvWindowHandle, hRow[i] * resizerate, hColumn[i] * resizerate, 10, 0);
                                HOperatorSet.SetTposition(Svision.GetMe().hvWindowHandle, hRow[i] * resizerate - 25, hColumn[i] * resizerate - 15);
                                HOperatorSet.WriteString(Svision.GetMe().hvWindowHandle, ("Point" + i + "," + hTemplateID[i]));

                            }
                            if (hoModelContours != null)
                            {
                                hoModelContours.Dispose();
                            }
                        }


                    }
                    string strtemp = "\r\n";
                    for (int i_object = 0; i_object < hRow.Length; i_object++)
                    {


                        string strc, stri, strx, stry, strad, strsd;

                        strc = System.String.Format("{0:d}", hTemplateID[i_object].I);
                        stri = System.String.Format("{0:d}", i_object);
                        strx = System.String.Format("{0:f}", hColumn[i_object].D);
                        stry = System.String.Format("{0:f}", hRow[i_object].D);

                        strad = System.String.Format("{0:f}", hAngle[i_object].D / 3.14159 * 180);
                        strsd = System.String.Format("{0:f}", hScore[i_object].D);
                        if (showOutputResultFlag[1])
                        {
                            strtemp = strtemp + "Point" + stri + ":(" + strx + " , " + stry + ") " + "\r\n";
                        }
                        if (showOutputResultFlag[2])
                        {
                            strtemp = strtemp + "Model: " + strc + "\r\n";
                        }
                        if (showOutputResultFlag[3])
                        {
                            strtemp = strtemp + "Angle to template:" + strad + "\r\n";
                        }
                        if (showOutputResultFlag[4])
                        {
                            strtemp = strtemp + "Score:" + strsd + "\r\n" + "\r\n";
                        }
                    }

                    Svision.GetMe().textBoxResultShow.Text = strtemp;
                }
            }

        }

        private void ShapeSearchMulModel()
        {
            try
            {
                hRow = null;
                hColumn = null;
                hAngle = null;
                hScore = null;
                hTemplateID = null;
                sortedFeatureValueIndices = null;
                double[] rows, columns, angles, scores;
                int[] models;
                HOperatorSet.FindShapeModels(inputImg, tShpMdl, tFndShpSMdlPara.angleStart,
                    tFndShpSMdlPara.angleExtent, tFndShpSMdlPara.minScore, tFndShpSMdlPara.numMatches,
                    tFndShpSMdlPara.maxOverlap, tFndShpSMdlPara.subPixel, tFndShpSMdlPara.numLevels,
                    tFndShpSMdlPara.greediness, out hRow, out hColumn, out hAngle, out hScore, out hTemplateID);

                if (hRow.TupleLength() != 0 && hRow.TupleLength() > 1)
                {
                    switch (arrangeIndex)
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
                                HOperatorSet.TupleSelect(hTemplateID, sortedFeatureValueIndices, out hTemplateID);

                            }
                            break;
                        case 1:
                            {
                                HOperatorSet.TupleSortIndex(hColumn, out sortedFeatureValueIndices);
                                HOperatorSet.TupleSort(hColumn, out hColumn);

                                HOperatorSet.TupleSelect(hRow, sortedFeatureValueIndices, out hRow);
                                HOperatorSet.TupleSelect(hAngle, sortedFeatureValueIndices, out hAngle);
                                HOperatorSet.TupleSelect(hScore, sortedFeatureValueIndices, out hScore);
                                HOperatorSet.TupleSelect(hTemplateID, sortedFeatureValueIndices, out hTemplateID);

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
                                HOperatorSet.TupleSelect(hTemplateID, sortedFeatureValueIndices, out hTemplateID);
                            }
                            break;
                        case 3:
                            {
                                HOperatorSet.TupleSortIndex(hRow, out sortedFeatureValueIndices);
                                HOperatorSet.TupleSort(hRow, out hRow);

                                HOperatorSet.TupleSelect(hColumn, sortedFeatureValueIndices, out hColumn);
                                HOperatorSet.TupleSelect(hAngle, sortedFeatureValueIndices, out hAngle);
                                HOperatorSet.TupleSelect(hScore, sortedFeatureValueIndices, out hScore);
                                HOperatorSet.TupleSelect(hTemplateID, sortedFeatureValueIndices, out hTemplateID);
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
                models = new int[hTemplateID.TupleLength()];
                if (hTemplateID != null)
                {
                    for (int i = 0; i < hTemplateID.TupleLength(); i++)
                    {
                        models[i] = hTemplateID[i].I;
                    }
                }
                for (int i = 0; i < UserCode.GetInstance().gProCd[idx].doubleData.Length; i++)
                {
                    UserCode.GetInstance().gProCd[idx].doubleData[i] = 0;
                }
                UserCode.GetInstance().gProCd[idx].doubleData[0] = hRow.TupleLength();

                for (int i = 0; i < hRow.TupleLength(); i++)
                {
                    if (ConfigInformation.GetInstance().tCalCfg.mx != null && ConfigInformation.GetInstance().tCalCfg.useThreePointCalibration == true)
                    {
                        double x = columns[i];
                        double y = rows[i];
                        UserCode.GetInstance().gProCd[idx].doubleData[i * 5 + 1] = ConfigInformation.GetInstance().tCalCfg.mx[0, 0] * x + ConfigInformation.GetInstance().tCalCfg.mx[1, 0] * y + ConfigInformation.GetInstance().tCalCfg.mx[4, 0];
                        UserCode.GetInstance().gProCd[idx].doubleData[i * 5 + 2] = ConfigInformation.GetInstance().tCalCfg.mx[2, 0] * x + ConfigInformation.GetInstance().tCalCfg.mx[3, 0] * y + ConfigInformation.GetInstance().tCalCfg.mx[5, 0];

                    }
                    else
                    {
                        UserCode.GetInstance().gProCd[idx].doubleData[i * 5 + 1] = columns[i];
                        UserCode.GetInstance().gProCd[idx].doubleData[i * 5 + 2] = rows[i];
                    }

                    UserCode.GetInstance().gProCd[idx].doubleData[i * 5 + 3] = models[i];
                    UserCode.GetInstance().gProCd[idx].doubleData[i * 5 + 4] = angles[i] / 3.14159 * 180;
                    UserCode.GetInstance().gProCd[idx].doubleData[i * 5 + 5] = scores[i];
                }
                //if (UserCode.GetInstance().gProCd[idx].showImg != null)
                //{
                //    UserCode.GetInstance().gProCd[idx].showImg.Dispose();
                //}
                //UserCode.GetInstance().gProCd[idx].showImg = inputImg;// ShapeSearchMulShow(inputImg, tShpMdl, hRow, hColumn, hAngle, hTemplateID);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
    public class AnisoShapeSearchFBD
    {
        public string[] showOutputResultStr;
        public bool[] showOutputResultFlag;
        public HObject inputImg;
        public HObject outputImg;
        public HTuple tAShpMdl;
        public bool ifMulMode;
        public int idx;
        HTuple hRow, hColumn, hAngle, hScaleR, hScaleC, hScore, hTemplateID, sortedFeatureValueIndices;
        findAnisoShapeModelClass.findAnisoShapeModelPara tFndAShpMdlPara;
        findAnisoShapeModelClass.findAnisoShapeModelsPara tFndAShpSMdlPara;
        public int arrangeIndex;
        public AnisoShapeSearchFBD(HObject ipImg, ProCodeCls.AnisoShapeSearchPara tASSP, int index)
        {
            arrangeIndex = tASSP.arrangeIndex;
            showOutputResultStr = tASSP.showOutputResultStr;
            showOutputResultFlag = tASSP.showOutputResultFlag;
            inputImg = ipImg;
            outputImg = new HObject();
            idx = index;

            UserCode.GetInstance().gProCd[idx].doubleData = new double[tASSP.Max_Object_Num * 7 + 1];
            if (tASSP.shapeModel != null)
            {

                if (tASSP.shapeModel.TupleLength() <= 0)
                    return;
                if (tASSP.shapeModel.TupleLength() > ProCodeCls.AnisoShapeSearchPara.MAX_SHAPE_MODEL)
                    return;

                int i_num = 0;
                for (int i = 0; i < ProCodeCls.AnisoShapeSearchPara.MAX_SHAPE_MODEL; i++)
                {
                    if (tASSP.modelIsChecked[i])
                    {
                        //tshapeModelPoints.Add(tSSP.shapeModelPoints[i]);
                        if (tAShpMdl == null)
                        {
                            tAShpMdl = tASSP.shapeModel[i];
                            //tshapeModelRegion = tSSP.shapeModelRegion[i+1];
                        }
                        else
                        {
                            tAShpMdl = tAShpMdl.TupleConcat(tASSP.shapeModel[i]);
                            // HOperatorSet.ConcatObj(tshapeModelRegion, tSSP.shapeModelRegion[i + 1], out tshapeModelRegion);
                        }
                        i_num++;
                    }
                }
                if (tAShpMdl.TupleLength() == 1)     //单模板;
                {
                    int[] numberLevelsTemp = new int[2];
                    int[] subPixelTemp = new int[2];
                    numberLevelsTemp[0] = tASSP.numLevels[(ProCodeCls.AnisoShapeSearchPara.MAX_SHAPE_MUL_PARA_NUM - 1) * 2];
                    numberLevelsTemp[1] = tASSP.numLevels[(ProCodeCls.AnisoShapeSearchPara.MAX_SHAPE_MUL_PARA_NUM - 1) * 2 + 1];
                    subPixelTemp[0] = tASSP.subPixel[(ProCodeCls.AnisoShapeSearchPara.MAX_SHAPE_MUL_PARA_NUM - 1) * 2];
                    subPixelTemp[1] = tASSP.subPixel[(ProCodeCls.AnisoShapeSearchPara.MAX_SHAPE_MUL_PARA_NUM - 1) * 2 + 1];
                    tFndAShpMdlPara = new findAnisoShapeModelClass.findAnisoShapeModelPara(tASSP.angleStart[ProCodeCls.AnisoShapeSearchPara.MAX_SHAPE_MUL_PARA_NUM - 1]
                        , tASSP.angleExtent[ProCodeCls.AnisoShapeSearchPara.MAX_SHAPE_MUL_PARA_NUM - 1], tASSP.scaleRMin[ProCodeCls.AnisoShapeSearchPara.MAX_SHAPE_MUL_PARA_NUM - 1],
                        tASSP.scaleRMax[ProCodeCls.AnisoShapeSearchPara.MAX_SHAPE_MUL_PARA_NUM - 1], tASSP.scaleCMin[ProCodeCls.AnisoShapeSearchPara.MAX_SHAPE_MUL_PARA_NUM - 1],
                        tASSP.scaleCMax[ProCodeCls.AnisoShapeSearchPara.MAX_SHAPE_MUL_PARA_NUM - 1], tASSP.minScore[ProCodeCls.AnisoShapeSearchPara.MAX_SHAPE_MUL_PARA_NUM - 1],
                        tASSP.numMatches[ProCodeCls.AnisoShapeSearchPara.MAX_SHAPE_MUL_PARA_NUM - 1], tASSP.maxOverlap, subPixelTemp,
                        numberLevelsTemp, tASSP.greediness[ProCodeCls.AnisoShapeSearchPara.MAX_SHAPE_MUL_PARA_NUM - 1]);
                }
                else                                        //多模板
                {


                    if (tASSP.isMultiplePara)       //多套模板参数;
                    {
                        double[] angleStartTemp = new double[tAShpMdl.TupleLength()];
                        double[] angleExtentTemp = new double[tAShpMdl.TupleLength()];
                        double[] scaleRMinTemp = new double[tAShpMdl.TupleLength()];
                        double[] scaleRMaxTemp = new double[tAShpMdl.TupleLength()];
                        double[] scaleCMinTemp = new double[tAShpMdl.TupleLength()];
                        double[] scaleCMaxTemp = new double[tAShpMdl.TupleLength()];
                        double[] minScoreTemp = new double[tAShpMdl.TupleLength()];
                        int[] numMatchesTemp = new int[tAShpMdl.TupleLength()];
                        double maxOverlapTemp = tASSP.maxOverlap;
                        double[] greedinessTemp = new double[tAShpMdl.TupleLength()];
                        int[] numberLevelsTemp = new int[2 * tAShpMdl.TupleLength()];
                        int[] subPixelTemp = new int[2 * tAShpMdl.TupleLength()];
                        int i_test = 0;
                        for (int i = 0; i < tASSP.shapeModel.TupleLength(); i++)
                        {
                            if (tASSP.modelIsChecked[i])
                            {
                                angleStartTemp[i_test] = tASSP.angleStart[i];
                                angleExtentTemp[i_test] = tASSP.angleExtent[i];
                                scaleRMinTemp[i_test] = tASSP.scaleRMin[i];
                                scaleRMaxTemp[i_test] = tASSP.scaleRMax[i];
                                scaleCMinTemp[i_test] = tASSP.scaleCMin[i];
                                scaleCMaxTemp[i_test] = tASSP.scaleCMax[i];
                                minScoreTemp[i_test] = tASSP.minScore[i];
                                numMatchesTemp[i_test] = tASSP.numMatches[i];

                                greedinessTemp[i_test] = tASSP.greediness[i];
                                numberLevelsTemp[i_test * 2] = tASSP.numLevels[i * 2];
                                numberLevelsTemp[i_test * 2 + 1] = tASSP.numLevels[i * 2 + 1];
                                subPixelTemp[i_test * 2] = tASSP.subPixel[i * 2];
                                subPixelTemp[i_test * 2 + 1] = tASSP.subPixel[i * 2 + 1];
                                i_test++;
                            }

                        }

                        tFndAShpSMdlPara = new findAnisoShapeModelClass.findAnisoShapeModelsPara(tAShpMdl.TupleLength(), angleStartTemp, angleExtentTemp,
                                              scaleRMinTemp, scaleRMaxTemp, scaleCMinTemp, scaleCMaxTemp, minScoreTemp, numMatchesTemp, maxOverlapTemp, subPixelTemp, numberLevelsTemp, greedinessTemp);
                    }
                    else                                                        //单套模板参数;
                    {

                        double[] angleStartTemp = new double[1];
                        double[] angleExtentTemp = new double[1];
                        double[] scaleRMinTemp = new double[1];
                        double[] scaleRMaxTemp = new double[1];
                        double[] scaleCMinTemp = new double[1];
                        double[] scaleCMaxTemp = new double[1];
                        double[] minScoreTemp = new double[1];
                        int[] numMatchesTemp = new int[1];
                        double maxOverlapTemp;
                        double[] greedinessTemp = new double[1];
                        angleStartTemp[0] = tASSP.angleStart[(ProCodeCls.AnisoShapeSearchPara.MAX_SHAPE_MUL_PARA_NUM - 1)];
                        angleExtentTemp[0] = tASSP.angleExtent[(ProCodeCls.AnisoShapeSearchPara.MAX_SHAPE_MUL_PARA_NUM - 1)];
                        scaleRMinTemp[0] = tASSP.scaleRMin[(ProCodeCls.AnisoShapeSearchPara.MAX_SHAPE_MUL_PARA_NUM - 1)];
                        scaleRMaxTemp[0] = tASSP.scaleRMax[(ProCodeCls.AnisoShapeSearchPara.MAX_SHAPE_MUL_PARA_NUM - 1)];
                        scaleCMinTemp[0] = tASSP.scaleCMin[(ProCodeCls.AnisoShapeSearchPara.MAX_SHAPE_MUL_PARA_NUM - 1)];
                        scaleCMaxTemp[0] = tASSP.scaleCMax[(ProCodeCls.AnisoShapeSearchPara.MAX_SHAPE_MUL_PARA_NUM - 1)];
                        minScoreTemp[0] = tASSP.minScore[(ProCodeCls.AnisoShapeSearchPara.MAX_SHAPE_MUL_PARA_NUM - 1)];
                        numMatchesTemp[0] = tASSP.numMatches[(ProCodeCls.AnisoShapeSearchPara.MAX_SHAPE_MUL_PARA_NUM - 1)];
                        maxOverlapTemp = tASSP.maxOverlap;
                        greedinessTemp[0] = tASSP.greediness[(ProCodeCls.AnisoShapeSearchPara.MAX_SHAPE_MUL_PARA_NUM - 1)];
                        int[] numberLevelsTemp = new int[2];
                        int[] subPixelTemp = new int[2];
                        numberLevelsTemp[0] = tASSP.numLevels[(ProCodeCls.AnisoShapeSearchPara.MAX_SHAPE_MUL_PARA_NUM - 1) * 2];
                        numberLevelsTemp[1] = tASSP.numLevels[(ProCodeCls.AnisoShapeSearchPara.MAX_SHAPE_MUL_PARA_NUM - 1) * 2 + 1];
                        subPixelTemp[0] = tASSP.subPixel[(ProCodeCls.AnisoShapeSearchPara.MAX_SHAPE_MUL_PARA_NUM - 1) * 2];
                        subPixelTemp[1] = tASSP.subPixel[(ProCodeCls.AnisoShapeSearchPara.MAX_SHAPE_MUL_PARA_NUM - 1) * 2 + 1];
                        tFndAShpSMdlPara = new findAnisoShapeModelClass.findAnisoShapeModelsPara(tAShpMdl.TupleLength(), angleStartTemp, angleExtentTemp,
                                                    scaleRMinTemp, scaleRMaxTemp, scaleCMinTemp, scaleCMaxTemp, minScoreTemp, numMatchesTemp, maxOverlapTemp, subPixelTemp, numberLevelsTemp, greedinessTemp);
                    }
                }
            }

        }
        ~AnisoShapeSearchFBD()
        {
            if (inputImg != null)
            {
                inputImg.Dispose();
            }
            if (outputImg != null)
            {
                outputImg.Dispose();
            }


        }
        public HObject AnisoShapeSearching()
        {

            inputImg = UserCode.GetInstance().gProCd[idx].inImage;
            if (tAShpMdl != null)
            {
               if (1 == tAShpMdl.TupleLength())     //单模板
                {

                    try
                    {
                        hRow = null;
                        hColumn = null;
                        hAngle = null;
                        hScaleR = null;
                        hScaleC = null;
                        hScore = null;
                        sortedFeatureValueIndices = null;
                        HOperatorSet.FindAnisoShapeModel(inputImg, tAShpMdl, tFndAShpMdlPara.angleStart,
                            tFndAShpMdlPara.angleExtent, tFndAShpMdlPara.scaleRMin, tFndAShpMdlPara.scaleRMax, tFndAShpMdlPara.scaleCMin,
                            tFndAShpMdlPara.scaleCMax, tFndAShpMdlPara.minScore, tFndAShpMdlPara.numMatches,
                            tFndAShpMdlPara.maxOverlap, tFndAShpMdlPara.subPixel, tFndAShpMdlPara.numLevels,
                            tFndAShpMdlPara.greediness, out hRow, out hColumn, out hAngle, out hScaleR, out hScaleC, out hScore);
                        if (hRow.TupleLength() != 0 && hRow.TupleLength() > 1)
                        {
                            switch (arrangeIndex)
                            {
                                case 0:
                                    {
                                        HOperatorSet.TupleSortIndex(hColumn, out sortedFeatureValueIndices);
                                        HOperatorSet.TupleSort(hColumn, out hColumn);

                                        HOperatorSet.TupleInverse(hColumn, out hColumn);
                                        HOperatorSet.TupleInverse(sortedFeatureValueIndices, out sortedFeatureValueIndices);
                                        HOperatorSet.TupleSelect(hScaleR, sortedFeatureValueIndices, out hScaleR);
                                        HOperatorSet.TupleSelect(hScaleC, sortedFeatureValueIndices, out hScaleC);
                                        HOperatorSet.TupleSelect(hAngle, sortedFeatureValueIndices, out hAngle);
                                        HOperatorSet.TupleSelect(hRow, sortedFeatureValueIndices, out hRow);
                                        HOperatorSet.TupleSelect(hScore, sortedFeatureValueIndices, out hScore);

                                    }
                                    break;
                                case 1:
                                    {
                                        HOperatorSet.TupleSortIndex(hColumn, out sortedFeatureValueIndices);
                                        HOperatorSet.TupleSort(hColumn, out hColumn);

                                        HOperatorSet.TupleSelect(hScaleR, sortedFeatureValueIndices, out hScaleR);
                                        HOperatorSet.TupleSelect(hScaleC, sortedFeatureValueIndices, out hScaleC);
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
                                        HOperatorSet.TupleSelect(hScaleR, sortedFeatureValueIndices, out hScaleR);
                                        HOperatorSet.TupleSelect(hScaleC, sortedFeatureValueIndices, out hScaleC);
                                        HOperatorSet.TupleSelect(hColumn, sortedFeatureValueIndices, out hColumn);
                                        HOperatorSet.TupleSelect(hAngle, sortedFeatureValueIndices, out hAngle);
                                        HOperatorSet.TupleSelect(hScore, sortedFeatureValueIndices, out hScore);

                                    }
                                    break;
                                case 3:
                                    {
                                        HOperatorSet.TupleSortIndex(hRow, out sortedFeatureValueIndices);
                                        HOperatorSet.TupleSort(hRow, out hRow);
                                        HOperatorSet.TupleSelect(hScaleR, sortedFeatureValueIndices, out hScaleR);
                                        HOperatorSet.TupleSelect(hScaleC, sortedFeatureValueIndices, out hScaleC);
                                        HOperatorSet.TupleSelect(hColumn, sortedFeatureValueIndices, out hColumn);
                                        HOperatorSet.TupleSelect(hAngle, sortedFeatureValueIndices, out hAngle);
                                        HOperatorSet.TupleSelect(hScore, sortedFeatureValueIndices, out hScore);
                                    }
                                    break;
                                default:
                                    break;
                            }
                        }
                        
                        for (int i = 0; i < UserCode.GetInstance().gProCd[idx].doubleData.Length; i++)
                        {
                            UserCode.GetInstance().gProCd[idx].doubleData[i] = 0;
                        }
                        for (int i = 0; i < hRow.TupleLength(); i++)
                        {
                            if (ConfigInformation.GetInstance().tCalCfg.mx != null && ConfigInformation.GetInstance().tCalCfg.useThreePointCalibration == true)
                            {
                                double x = hColumn[i].D;
                                double y = hRow[i].D;
                                UserCode.GetInstance().gProCd[idx].doubleData[i * 7 + 1]= ConfigInformation.GetInstance().tCalCfg.mx[0, 0] * x + ConfigInformation.GetInstance().tCalCfg.mx[1, 0] * y + ConfigInformation.GetInstance().tCalCfg.mx[4, 0];
                                UserCode.GetInstance().gProCd[idx].doubleData[i * 7 + 2] = ConfigInformation.GetInstance().tCalCfg.mx[2, 0] * x + ConfigInformation.GetInstance().tCalCfg.mx[3, 0] * y + ConfigInformation.GetInstance().tCalCfg.mx[5, 0];

                            }
                            else
                            {
                                UserCode.GetInstance().gProCd[idx].doubleData[i * 7 + 1] = hColumn[i].D;
                                UserCode.GetInstance().gProCd[idx].doubleData[i * 7 + 2] = hRow[i].D;
                            }
                            
                            UserCode.GetInstance().gProCd[idx].doubleData[i * 7 + 3] = hScaleC[i].D;
                            UserCode.GetInstance().gProCd[idx].doubleData[i * 7 + 4] = hScaleR[i].D;
                            UserCode.GetInstance().gProCd[idx].doubleData[i * 7 + 5] = 0.0;
                            UserCode.GetInstance().gProCd[idx].doubleData[i * 7 + 6] = (hAngle[i].D * 180 / 3.14159);


                            UserCode.GetInstance().gProCd[idx].doubleData[i * 7 + 7] = hScore[i].D;
                        }
                        UserCode.GetInstance().gProCd[idx].doubleData[0] = hRow.TupleLength();
                        //UserCode.GetInstance().gProCd[idx].showImg = inputImg;
                    }
                    catch (System.Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                }
                else                                //多模板
                {
                    AnisoShapeSearchMulModel();
                }
            }

            return inputImg;
        }

        public void ShowImageFunc()
        {
            if (tAShpMdl != null)
            {

                double widRat = Svision.GetMe().pictureBoxShowImage.Width / ((double)Svision.GetMe().columnNumber);
                double heiRat = Svision.GetMe().pictureBoxShowImage.Height / ((double)Svision.GetMe().rowNumber);
                double resizerate = widRat < heiRat ? widRat : heiRat;


                if (tAShpMdl.TupleLength() == 1)
                {
                    if (hRow.TupleLength() != 0 && showOutputResultFlag[0])
                    {
                        HObject hoModelContours;
                        for (int i = 0; i < hRow.TupleLength(); i++)
                        {
                            HOperatorSet.GetShapeModelContours(out hoModelContours, tAShpMdl[0], 1);
                            HTuple hvHomMat2D;
                            HOperatorSet.HomMat2dIdentity(out hvHomMat2D);
                            HOperatorSet.HomMat2dScale(hvHomMat2D, hScaleR[i], hScaleC[i], 0, 0, out hvHomMat2D);
                            HOperatorSet.HomMat2dRotate(hvHomMat2D, hAngle[i], 0, 0, out hvHomMat2D);
                            HOperatorSet.HomMat2dTranslate(hvHomMat2D, hRow[i], hColumn[i], out hvHomMat2D);
                            HOperatorSet.HomMat2dScale(hvHomMat2D, resizerate, resizerate, 0, 0, out hvHomMat2D);
                            HOperatorSet.AffineTransContourXld(hoModelContours, out hoModelContours, hvHomMat2D);
                            if (HDevWindowStack.IsOpen())
                            {
                                HOperatorSet.SetColor(Svision.GetMe().hvWindowHandle, "green");
                                HOperatorSet.SetLineWidth(Svision.GetMe().hvWindowHandle, 2);
                                HOperatorSet.DispObj(hoModelContours, Svision.GetMe().hvWindowHandle);
                                HOperatorSet.SetColor(Svision.GetMe().hvWindowHandle, "red");
                                HOperatorSet.SetLineWidth(Svision.GetMe().hvWindowHandle, 4);
                                HOperatorSet.DispCross(Svision.GetMe().hvWindowHandle, hRow[i] * resizerate, hColumn[i] * resizerate, 10, 0);
                                HOperatorSet.SetTposition(Svision.GetMe().hvWindowHandle, hRow[i] * resizerate - 25, hColumn[i] * resizerate - 15);
                                HOperatorSet.WriteString(Svision.GetMe().hvWindowHandle, ("Point" + i));

                            }
                            if (hoModelContours != null)
                            {
                                hoModelContours.Dispose();
                            }
                        }


                    }
                    string strtemp = "\r\n";
                    for (int i_object = 0; i_object < hRow.Length; i_object++)
                    {


                        string strc, stri, strx, stry, strsx, strsy, strad, strsd;

                        strc = System.String.Format("{0:d}", 0);
                        stri = System.String.Format("{0:d}", i_object);
                        strx = System.String.Format("{0:f}", UserCode.GetInstance().gProCd[idx].doubleData[i_object * 5 + 1]);
                        stry = System.String.Format("{0:f}", UserCode.GetInstance().gProCd[idx].doubleData[i_object * 5 + 2]);
                        strsx = System.String.Format("{0:f}", hScaleC[i_object].D);
                        strsy = System.String.Format("{0:f}", hScaleR[i_object].D);

                        strad = System.String.Format("{0:f}", hAngle[i_object].D / 3.14159 * 180);
                        strsd = System.String.Format("{0:f}", hScore[i_object].D);
                        if (showOutputResultFlag[1])
                        {
                            strtemp = strtemp + "Point" + stri + ":(" + strx + " , " + stry + ") " + "\r\n";
                        }
                        if (showOutputResultFlag[2])
                        {
                            strtemp = strtemp + "X Scale" + strsx + "\r\n";
                        }
                        if (showOutputResultFlag[3])
                        {
                            strtemp = strtemp + "Y Scale" + strsy + "\r\n";
                        }
                        if (showOutputResultFlag[4])
                        {
                            strtemp = strtemp + "Model: " + strc + "\r\n";
                        }
                        if (showOutputResultFlag[5])
                        {
                            strtemp = strtemp + "Angle to template（角度）:" + strad + "\r\n";
                        }
                        if (showOutputResultFlag[6])
                        {
                            strtemp = strtemp + "Score:" + strsd + "\r\n" + "\r\n";
                        }
                    }

                    Svision.GetMe().textBoxResultShow.Text = strtemp;

                }
                else
                {
                    if (hRow.TupleLength() != 0 && showOutputResultFlag[0])
                    {
                        HObject hoModelContours;
                        for (int i = 0; i < hRow.TupleLength(); i++)
                        {
                            HOperatorSet.GetShapeModelContours(out hoModelContours, tAShpMdl[hTemplateID[i].I], 1);
                            HTuple hvHomMat2D;
                            HOperatorSet.HomMat2dIdentity(out hvHomMat2D);
                            HOperatorSet.HomMat2dScale(hvHomMat2D, hScaleR[i], hScaleC[i], 0, 0, out hvHomMat2D);
                            HOperatorSet.HomMat2dRotate(hvHomMat2D, hAngle[i], 0, 0, out hvHomMat2D);
                            HOperatorSet.HomMat2dTranslate(hvHomMat2D, hRow[i], hColumn[i], out hvHomMat2D);
                            HOperatorSet.HomMat2dScale(hvHomMat2D, resizerate, resizerate, 0, 0, out hvHomMat2D);
                            HOperatorSet.AffineTransContourXld(hoModelContours, out hoModelContours, hvHomMat2D);
                            if (HDevWindowStack.IsOpen())
                            {
                                HOperatorSet.SetColor(Svision.GetMe().hvWindowHandle, "green");
                                HOperatorSet.SetLineWidth(Svision.GetMe().hvWindowHandle, 4);
                                HOperatorSet.DispObj(hoModelContours, Svision.GetMe().hvWindowHandle);
                                HOperatorSet.SetColor(Svision.GetMe().hvWindowHandle, "red");
                                HOperatorSet.DispCross(Svision.GetMe().hvWindowHandle, hRow[i] * resizerate, hColumn[i] * resizerate, 10, 0);
                                HOperatorSet.SetTposition(Svision.GetMe().hvWindowHandle, hRow[i] * resizerate - 25, hColumn[i] * resizerate - 15);
                                HOperatorSet.WriteString(Svision.GetMe().hvWindowHandle, ("Point" + i + "," + hTemplateID[i]));

                            }
                            if (hoModelContours != null)
                            {
                                hoModelContours.Dispose();
                            }
                        }


                    }
                    string strtemp = "\r\n";
                    for (int i_object = 0; i_object < hRow.Length; i_object++)
                    {


                        string strc, stri, strx, stry, strsx, strsy, strad, strsd;

                        strc = System.String.Format("{0:d}", hTemplateID[i_object].I);
                        stri = System.String.Format("{0:d}", i_object);
                        strx = System.String.Format("{0:f}", hColumn[i_object].D);
                        stry = System.String.Format("{0:f}", hRow[i_object].D);
                        strsx = System.String.Format("{0:f}", hScaleC[i_object].D);
                        strsy = System.String.Format("{0:f}", hScaleR[i_object].D);

                        strad = System.String.Format("{0:f}", hAngle[i_object].D / 3.14159 * 180);
                        strsd = System.String.Format("{0:f}", hScore[i_object].D);
                        if (showOutputResultFlag[1])
                        {
                            if (ConfigInformation.GetInstance().tCalCfg.mx != null && ConfigInformation.GetInstance().tCalCfg.useThreePointCalibration == true)
                            {
                                strtemp = strtemp + "Point" + stri + ":(" + strx + " , " + stry + ")(mm) " + "\r\n";
                            }
                            else
                            {
                                strtemp = strtemp + "Point" + stri + ":(" + strx + " , " + stry + ") " + "\r\n";
                            }
                            
                        }
                        if (showOutputResultFlag[2])
                        {
                            strtemp = strtemp + "X Scale" + strsx + "\r\n";
                        }
                        if (showOutputResultFlag[3])
                        {
                            strtemp = strtemp + "Y Scale" + strsy + "\r\n";
                        }
                        if (showOutputResultFlag[4])
                        {
                            strtemp = strtemp + "Model: " + strc + "\r\n";
                        }
                        if (showOutputResultFlag[5])
                        {
                            strtemp = strtemp + "Angle to template:" + strad + "\r\n";
                        }
                        if (showOutputResultFlag[6])
                        {
                            strtemp = strtemp + "Score:" + strsd + "\r\n" + "\r\n";
                        }
                        // strtemp = strtemp + "Point" + stri + ":(" + strx + " , " + stry + ") " + "\r\n" + "Model: " + strc + "\r\n" + "Angle to template:" + strad + "\r\n" + "Score:" + strsd + "\r\n" + "\r\n";

                    }
                    Svision.GetMe().textBoxResultShow.Text = strtemp;
                }
            }


        }

        private void AnisoShapeSearchMulModel()
        {

            try
            {
                hRow = null;
                hColumn = null;
                hAngle = null;
                hScore = null;
                hTemplateID = null;
                sortedFeatureValueIndices = null;
                double[] rows, columns, rScale, cScale, angles, scores;
                int[] models;
                HOperatorSet.FindAnisoShapeModels(inputImg, tAShpMdl, tFndAShpSMdlPara.angleStart,
                    tFndAShpSMdlPara.angleExtent, tFndAShpSMdlPara.scaleRMin, tFndAShpSMdlPara.scaleRMax, tFndAShpSMdlPara.scaleCMin, tFndAShpSMdlPara.scaleCMax,
                    tFndAShpSMdlPara.minScore, tFndAShpSMdlPara.numMatches, tFndAShpSMdlPara.maxOverlap, tFndAShpSMdlPara.subPixel, tFndAShpSMdlPara.numLevels,
                    tFndAShpSMdlPara.greediness, out hRow, out hColumn, out hAngle, out hScaleR, out hScaleC, out hScore, out hTemplateID);

                if (hRow.TupleLength() != 0 && hRow.TupleLength() > 1)
                {
                    switch (arrangeIndex)
                    {
                        case 0:
                            {
                                HOperatorSet.TupleSortIndex(hColumn, out sortedFeatureValueIndices);
                                HOperatorSet.TupleSort(hColumn, out hColumn);

                                HOperatorSet.TupleInverse(hColumn, out hColumn);
                                HOperatorSet.TupleInverse(sortedFeatureValueIndices, out sortedFeatureValueIndices);

                                HOperatorSet.TupleSelect(hScaleR, sortedFeatureValueIndices, out hScaleR);
                                HOperatorSet.TupleSelect(hScaleC, sortedFeatureValueIndices, out hScaleC);
                                HOperatorSet.TupleSelect(hRow, sortedFeatureValueIndices, out hRow);
                                HOperatorSet.TupleSelect(hAngle, sortedFeatureValueIndices, out hAngle);
                                HOperatorSet.TupleSelect(hScore, sortedFeatureValueIndices, out hScore);
                                HOperatorSet.TupleSelect(hTemplateID, sortedFeatureValueIndices, out hTemplateID);

                            }
                            break;
                        case 1:
                            {
                                HOperatorSet.TupleSortIndex(hColumn, out sortedFeatureValueIndices);
                                HOperatorSet.TupleSort(hColumn, out hColumn);
                                HOperatorSet.TupleSelect(hScaleR, sortedFeatureValueIndices, out hScaleR);
                                HOperatorSet.TupleSelect(hScaleC, sortedFeatureValueIndices, out hScaleC);
                                HOperatorSet.TupleSelect(hRow, sortedFeatureValueIndices, out hRow);
                                HOperatorSet.TupleSelect(hAngle, sortedFeatureValueIndices, out hAngle);
                                HOperatorSet.TupleSelect(hScore, sortedFeatureValueIndices, out hScore);
                                HOperatorSet.TupleSelect(hTemplateID, sortedFeatureValueIndices, out hTemplateID);

                            }
                            break;
                        case 2:
                            {
                                HOperatorSet.TupleSortIndex(hRow, out sortedFeatureValueIndices);
                                HOperatorSet.TupleSort(hRow, out hRow);

                                HOperatorSet.TupleInverse(hRow, out hRow);
                                HOperatorSet.TupleInverse(sortedFeatureValueIndices, out sortedFeatureValueIndices);
                                HOperatorSet.TupleSelect(hScaleR, sortedFeatureValueIndices, out hScaleR);
                                HOperatorSet.TupleSelect(hScaleC, sortedFeatureValueIndices, out hScaleC);
                                HOperatorSet.TupleSelect(hColumn, sortedFeatureValueIndices, out hColumn);
                                HOperatorSet.TupleSelect(hAngle, sortedFeatureValueIndices, out hAngle);
                                HOperatorSet.TupleSelect(hScore, sortedFeatureValueIndices, out hScore);
                                HOperatorSet.TupleSelect(hTemplateID, sortedFeatureValueIndices, out hTemplateID);
                            }
                            break;
                        case 3:
                            {
                                HOperatorSet.TupleSortIndex(hRow, out sortedFeatureValueIndices);
                                HOperatorSet.TupleSort(hRow, out hRow);
                                HOperatorSet.TupleSelect(hScaleR, sortedFeatureValueIndices, out hScaleR);
                                HOperatorSet.TupleSelect(hScaleC, sortedFeatureValueIndices, out hScaleC);
                                HOperatorSet.TupleSelect(hColumn, sortedFeatureValueIndices, out hColumn);
                                HOperatorSet.TupleSelect(hAngle, sortedFeatureValueIndices, out hAngle);
                                HOperatorSet.TupleSelect(hScore, sortedFeatureValueIndices, out hScore);
                                HOperatorSet.TupleSelect(hTemplateID, sortedFeatureValueIndices, out hTemplateID);
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
                rScale = new double[hScaleR.TupleLength()];
                for (int i = 0; i < hScaleR.TupleLength(); i++)
                {
                    rScale[i] = hScaleR[i].D;
                }
                cScale = new double[hScaleC.TupleLength()];
                for (int i = 0; i < hScaleC.TupleLength(); i++)
                {
                    cScale[i] = hScaleC[i].D;
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
                models = new int[hTemplateID.TupleLength()];
                if (hTemplateID != null)
                {
                    for (int i = 0; i < hTemplateID.TupleLength(); i++)
                    {
                        models[i] = hTemplateID[i].I;
                    }
                }
                for (int i = 0; i < UserCode.GetInstance().gProCd[idx].doubleData.Length; i++)
                {
                    UserCode.GetInstance().gProCd[idx].doubleData[i] = 0;
                }
                UserCode.GetInstance().gProCd[idx].doubleData[0] = hRow.TupleLength();

                for (int i = 0; i < hRow.TupleLength(); i++)
                {
                    if (ConfigInformation.GetInstance().tCalCfg.mx != null && ConfigInformation.GetInstance().tCalCfg.useThreePointCalibration == true)
                    {
                        double x = columns[i];
                        double y = rows[i];
                        UserCode.GetInstance().gProCd[idx].doubleData[i * 7 + 1] = ConfigInformation.GetInstance().tCalCfg.mx[0, 0] * x + ConfigInformation.GetInstance().tCalCfg.mx[1, 0] * y + ConfigInformation.GetInstance().tCalCfg.mx[4, 0];
                        UserCode.GetInstance().gProCd[idx].doubleData[i * 7 + 2] = ConfigInformation.GetInstance().tCalCfg.mx[2, 0] * x + ConfigInformation.GetInstance().tCalCfg.mx[3, 0] * y + ConfigInformation.GetInstance().tCalCfg.mx[5, 0];

                    }
                    else
                    {
                        UserCode.GetInstance().gProCd[idx].doubleData[i * 7 + 1] = columns[i];
                        UserCode.GetInstance().gProCd[idx].doubleData[i * 7 + 2] = rows[i];
                    }


                    UserCode.GetInstance().gProCd[idx].doubleData[i * 7 + 3] = cScale[i];
                    UserCode.GetInstance().gProCd[idx].doubleData[i * 7 + 4] = rScale[i];
                    UserCode.GetInstance().gProCd[idx].doubleData[i * 7 + 5] = (double)models[i];
                    UserCode.GetInstance().gProCd[idx].doubleData[i * 7 + 6] = angles[i] / 3.14159 * 180;
                    UserCode.GetInstance().gProCd[idx].doubleData[i * 7 + 7] = scores[i];
                }
                //    UserCode.GetInstance().gProCd[idx].doubleData[0] = tMinNum;
                //UserCode.GetInstance().gProCd[idx].showImg = inputImg;// ShapeSearchMulShow(inputImg, tShpMdl, hRow, hColumn, hAngle, hTemplateID);
                // ShapeSearchShow(inputImg, tShpMdl, hRow, hColumn, hAngle);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
    }

    public class SurfaceBasedMatchFBD
    {
        HObject inputImg;
        public int idx;
        public SurfaceBasedMatchFBD(HObject ipImg, ProCodeCls.SurfaceBasedMatchPara tSBMP, int index)
        {

        }
        public HObject SurfaceBasedSearching()
        {
            inputImg = UserCode.GetInstance().gProCd[idx].inImage;
            return inputImg;



        }

    }
    public class SerialOutputFBD
    {

        // public int dataLength;
        public bool outputForm;             //true----ascii     false-----bin
        private short intBit;
        private short floatBit;
        private bool NegativeMinus;         //true---- -        false-----8
        private bool EraseZeroYes;          //true---- 消零      false-----不消零
        private short FieldSeparator;       //字段分隔符
        private short RecordSeparator;      //记录分隔符
        private ProCodeCls.SerialOutputPara tSOP;

        private String FormatInfoPositiveStr;
        private String FormatInfoNegativeStr;
        private String PositiveLargestStr;
        private String NegativeSmallestStr;
        private String FieldSepStr;
        private String RecordSepStr;

        public SerialOutputFBD(ProCodeCls.SerialOutputPara tSOP_)
        {
           tSOP = new ProCodeCls.SerialOutputPara();
            tSOP = tSOP_;
            outputForm = tSOP.outputForm;
            intBit = tSOP.intBit;
            floatBit = tSOP.floatBit;
            NegativeMinus = tSOP.NegativeMinus;
            EraseZeroYes = tSOP.EraseZeroYes;
            FieldSeparator = tSOP.FieldSeparator;
            RecordSeparator = tSOP.RecordSeparator;
            InitDataFormat();
        }

        ~SerialOutputFBD()
        {
            //tSOP.clear();
        }

        public String SendAsciiData(/*int i*/)
        {
            String retStr = "";

            for (int i = 0; i < tSOP.sendDataInfoList.Count; i++)
            {
                if (UserCode.GetInstance().gProCd[tSOP.sendDataInfoList[i].row].FuncID ==
                    (ProCodeCls.MainFunction)tSOP.sendDataInfoList[i].funcID)
                {
                    if (ProCodeCls.MainFunction.NullFBD == ((ProCodeCls.MainFunction)tSOP.sendDataInfoList[i].funcID))
                        break;

                    switch ((DataType)tSOP.sendDataInfoList[i].datatype)
                    {
                        //case DataType.BOOL_TYPE:
                        //    retStr += String.Format(FormatInfoStr,
                        //        UserCode.GetInstance().gProCd[tSOP.sendDataInfoList[i].row].boolData[tSOP.sendDataInfoList[i].idx]);
                        //    break;
                        //case DataType.SHORT_TYPE:
                        //    //if (UserCode.GetInstance().gProCd[tSOP.sendDataInfoList[i].row].shortData[tSOP.sendDataInfoList[i].idx]
                        //    //    > Math.Pow(10,intBit))
                        //    //{

                        //    //}
                        //    retStr += String.Format(FormatInfoStr,
                        //        UserCode.GetInstance().gProCd[tSOP.sendDataInfoList[i].row].shortData[tSOP.sendDataInfoList[i].idx]);
                        //    break;
                        //case DataType.INT_TYPE:
                        //    retStr += String.Format(FormatInfoStr,
                        //        UserCode.GetInstance().gProCd[tSOP.sendDataInfoList[i].row].intData[tSOP.sendDataInfoList[i].idx]);
                        //    break;
                        //case DataType.FLOAT_TYPE:
                        //    retStr += String.Format(FormatInfoStr,
                        //        UserCode.GetInstance().gProCd[tSOP.sendDataInfoList[i].row].floatData[tSOP.sendDataInfoList[i].idx]);
                        //    break;
                        case DataType.DOUBLE_TYPE:
                            if (UserCode.GetInstance().gProCd[tSOP.sendDataInfoList[i].row].doubleData[tSOP.sendDataInfoList[i].idx] < float.Parse(PositiveLargestStr)
                                && UserCode.GetInstance().gProCd[tSOP.sendDataInfoList[i].row].doubleData[tSOP.sendDataInfoList[i].idx] > float.Parse(NegativeSmallestStr))
                            {
                                if ( float.Parse(String.Format(FormatInfoPositiveStr,
                                UserCode.GetInstance().gProCd[tSOP.sendDataInfoList[i].row].doubleData[tSOP.sendDataInfoList[i].idx])) >= 0)
                                {
                                    retStr += String.Format(FormatInfoPositiveStr,
                                UserCode.GetInstance().gProCd[tSOP.sendDataInfoList[i].row].doubleData[tSOP.sendDataInfoList[i].idx]);
                                }
                                else
                                {
                                    retStr += String.Format(FormatInfoNegativeStr,
                                UserCode.GetInstance().gProCd[tSOP.sendDataInfoList[i].row].doubleData[tSOP.sendDataInfoList[i].idx]);
                                }

                            }
                            else
                            {
                                throw new Exception("发送数据超出设定位数上限！请修改串行输出整数位数参数！");
                            }

                            break;
                        default:
                            break;
                    }
                    if (i == tSOP.sendDataInfoList.Count-1)
                    {
                        retStr += RecordSepStr;
                    } 
                    else
                    {
                        retStr += FieldSepStr;
                    }
                   
                }
            }

            //retStr = retStr.Substring(0, retStr.Length - 1);

            return retStr;
        }

        public int SendBinData(out byte[] sendByte)
        {
            int sendLen = 0;
            sendByte = new byte[1024];

            for (int i = 0; i < tSOP.sendDataInfoList.Count; i++)
            {
                if (UserCode.GetInstance().gProCd[tSOP.sendDataInfoList[i].row].FuncID ==
                    (ProCodeCls.MainFunction)tSOP.sendDataInfoList[i].funcID)
                {
                    if (ProCodeCls.MainFunction.NullFBD == ((ProCodeCls.MainFunction)tSOP.sendDataInfoList[i].funcID))
                        break;

                    switch ((DataType)tSOP.sendDataInfoList[i].datatype)
                    {
                        //case DataType.BOOL_TYPE:
                        //    sendByte[sendLen] = BitConverter.GetBytes(
                        //        UserCode.GetInstance().gProCd[tSOP.sendDataInfoList[i].row].boolData[tSOP.sendDataInfoList[i].idx])[0];
                        //    sendLen += 1;
                        //    break;
                        //case DataType.SHORT_TYPE:
                        //    byte[] tmpByteShort = BitConverter.GetBytes(
                        //        UserCode.GetInstance().gProCd[tSOP.sendDataInfoList[i].row].shortData[tSOP.sendDataInfoList[i].idx]);
                        //    sendByte[sendLen] = tmpByteShort[0];
                        //    sendByte[sendLen + 1] = tmpByteShort[1];
                        //    sendLen += 2;
                        //    break;
                        //case DataType.INT_TYPE:
                        //    byte[] tmpByteInt = BitConverter.GetBytes(
                        //        UserCode.GetInstance().gProCd[tSOP.sendDataInfoList[i].row].intData[tSOP.sendDataInfoList[i].idx]);
                        //    sendByte[sendLen] = tmpByteInt[0];
                        //    sendByte[sendLen + 1] = tmpByteInt[1];
                        //    sendByte[sendLen + 2] = tmpByteInt[2];
                        //    sendByte[sendLen + 3] = tmpByteInt[3];
                        //    sendLen += 4;
                        //    break;
                        //case DataType.FLOAT_TYPE:
                        //    byte[] tmpByteFloat = BitConverter.GetBytes(
                        //        UserCode.GetInstance().gProCd[tSOP.sendDataInfoList[i].row].floatData[tSOP.sendDataInfoList[i].idx]);
                        //    sendByte[sendLen] = tmpByteFloat[0];
                        //    sendByte[sendLen + 1] = tmpByteFloat[1];
                        //    sendByte[sendLen + 2] = tmpByteFloat[2];
                        //    sendByte[sendLen + 3] = tmpByteFloat[3];
                        //    sendLen += 4;
                        //    break;
                        case DataType.DOUBLE_TYPE:
                            byte[] tmpByteDouble = BitConverter.GetBytes(
                                UserCode.GetInstance().gProCd[tSOP.sendDataInfoList[i].row].doubleData[tSOP.sendDataInfoList[i].idx]);
                            sendByte[sendLen] = tmpByteDouble[0];
                            sendByte[sendLen + 1] = tmpByteDouble[1];
                            sendByte[sendLen + 2] = tmpByteDouble[2];
                            sendByte[sendLen + 3] = tmpByteDouble[3];
                            sendByte[sendLen + 4] = tmpByteDouble[4];
                            sendByte[sendLen + 5] = tmpByteDouble[5];
                            sendByte[sendLen + 6] = tmpByteDouble[6];
                            sendByte[sendLen + 7] = tmpByteDouble[7];
                            sendLen += 8;
                            break;
                        default:
                            break;
                    }
                }
            }
            return sendLen;
        }

        private void InitDataFormat()
        {
            #region OldCode
            /*
                         FormatInfoStr = "{0:";

            for (int i = 0; i < intBit; i++ )
            {
                if (!EraseZeroYes)
                {
                    FormatInfoStr += "0";
                }
                else
                {
                    FormatInfoStr += "#";
                }
            }
            FormatInfoStr += ".";
            for (int i = 0; i < floatBit; i++ )
            {
                if (!EraseZeroYes)
                {
                    FormatInfoStr += "0";
                }
                else
                {
                    FormatInfoStr += "#";
                }
            }
            FormatInfoStr += "}";

            switch(FieldSeparator)
            {
                case 0:                 //停止
                    break;
                case 1:                 //逗号
                    FieldSepStr = ",";
                    break;
                case 2:                 //退格
                    FieldSepStr = "\b";
                    break;
                case 3:                 //空格
                    FieldSepStr = " ";
                    break;
                case 4:                 //分隔符
                    break;
                default:
                    break;
            } 
             */
            #endregion
            PositiveLargestStr = "9";
            NegativeSmallestStr = "-";
            FormatInfoPositiveStr = "{0:";
            FormatInfoNegativeStr = "{0:";
            for (int i = 0; i < intBit; i++)
            {
                FormatInfoPositiveStr += "0";

            }
            for (int i = 0; i < intBit - 1; i++)
            {
                FormatInfoNegativeStr += "0";
                PositiveLargestStr += "9";
                NegativeSmallestStr += "9";
            }
            FormatInfoPositiveStr += ".";
            FormatInfoNegativeStr += ".";
            PositiveLargestStr += ".";
            NegativeSmallestStr += ".";
            for (int i = 0; i < floatBit; i++)
            {
                FormatInfoPositiveStr += "0";
                FormatInfoNegativeStr += "0";
                PositiveLargestStr += "9";
                NegativeSmallestStr += "9";
            }
            PositiveLargestStr += "5";
            NegativeSmallestStr += "5";
            FormatInfoPositiveStr += "}";
            FormatInfoNegativeStr += "}";

            switch (FieldSeparator)
            {
                //case 0:                 //停止
                //    break;
                case 0:                 //逗号
                    FieldSepStr = ",";
                    break;
                //case 2:                 //退格
                //    FieldSepStr = "\b";
                //    break;
                case 1:                 //空格
                    FieldSepStr = " ";
                    break;
                //case 4:                 //分隔符
                //    break;
                default:
                    break;
            }
            switch (RecordSeparator)
            {
                case 0:                 //无
                    break;
                case 1:                 //逗号
                    RecordSepStr = ",";
                    break;
                case 2:                 //空格
                    RecordSepStr = " ";
                    break;
                //case 4:                 //分隔符
                //    break;
                default:
                    break;
            }
        }
    }
}
