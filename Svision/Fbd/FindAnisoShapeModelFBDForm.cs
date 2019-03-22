using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using HalconDotNet;
namespace Svision 
{
    public partial class FindAnisoShapeModelFBDForm : Form
    {
        private int currentIndex;
        private const int MAX_MODEL_NUM = 100;
        private const int MAX_MODEL_MUL_PARA_NUM = MAX_MODEL_NUM+1;
        private bool[] modelIfCkecked;

        private findAnisoShapeModelClass fSMC = new findAnisoShapeModelClass();
        private HTuple templateModel;
        private double[] rows, columns, angles,scaleR,scaleC, scores;
        int[] templateID;
        HTuple findShapeModelHWHandle, findShapeModelPartHWHandle;
        public HObject image, imageTemp, imageTempShow, imageTempShowWithOutPoint, imageRealRegion, imageDisplayPartRegion, img, imgPart, imgPartTemp, rectDomain;
        double resizerate = 1;
        int templateNumber = 0;
        int[] autoCreateShapeParametersArray;
        Point oriLocation;
        PointF centerLocation;
        HTuple tModelIDs;
        int rowNum, columnNum;
        int rowOriNum, columnOriNum;
        double resRat;
        PointF modelPoint;
        //List<HObject> templateModelRegions=new List<HObject>();
        //List<HObject> imageModelRegions = new List<HObject>();
        List<PointF> modelPoints = new List<PointF>();
        List<PointF> templateModelPoints = new List<PointF>();
        HTuple regionCenterRow, regionCenterColumn;
        const string path = "H:/vision_dll/trunk/C#_CODE/UI/Svision/";
        string modelFileSavePath;
        string modelFileReadPath;
        string str;

        int arrangeIndex = 0;
        HObject templateModelRegions, imageModelRegions, test;
        HObject templateModelImages;
        struct createAnisoPara
        {
            public int numberLevel;
            public double angleStart;
            public double angleExtent;
            public double angleStep;

            public double scaleRMin;
            public double scaleRMax;
            public double scaleRStep;
            public double scaleCMin;
            public double scaleCMax;
            public double scaleCStep;

            public int[] optimization;
            public int metric;
            public int[] contrast;
            public int minContrast;
            //public 
            //public void setPara(int numberLevelsTemp, double angleStartTemp, double angleExtentTemp, double angleStepTemp,
            //                            int[] optimizationTemp, int metricTemp, int[] contrastTemp, int minContrastTemp)
            //{
            //    numberLevel = numberLevelsTemp;
            //    angleStart = angleStartTemp;
            //    angleExtent = angleExtentTemp;
            //    angleStep = angleStepTemp;
            //    optimization = optimizationTemp;
            //}
        }
        private createAnisoPara cPara = new createAnisoPara();
        public struct findAnisoParas
        {
            public double[] angleStart;
            //Smallest rotation of the models.(rad)
            public double[] angleExtent;
            //Extent of the rotation angles.(rad)
            public double[] scaleRMin;
            public double[] scaleRMax;
            public double[] scaleCMin;
            public double[] scaleCMax;
            public double[] minScore;
            //Minimum score of the instances of the models to be found.
            public int[] numMatches;
            //Number of instances of the models to be found(or 0 for all matches).
            public double maxOverlap;
            //Maximum overlap of the instances of the models to be found.
            public int[] subPixel;
            //Subpixel accuracy if not equal to 'none'.
            public int[] numLevels;
            //Number of pyramid levels used in the matching (and lowest pyramid level to use if |NumLevels| = 2).
            public double[] greediness;

        }
        struct findAnisoPara
        {
            public double angleStart;
            //Smallest rotation of the models.(rad)
            public double angleExtent;
            //Extent of the rotation angles.(rad)
            public double  scaleRMin;
            public double  scaleRMax;
            public double  scaleCMin;
            public double  scaleCMax;
            public double minScore;
            //Minimum score of the instances of the models to be found.
            public int numMatches;
            //Number of instances of the models to be found(or 0 for all matches).
            public double maxOverlap;
            //Maximum overlap of the instances of the models to be found.
            public int[] subPixel;
            //Subpixel accuracy if not equal to 'none'.
            public int[] numLevels;
            //Number of pyramid levels used in the matching (and lowest pyramid level to use if |NumLevels| = 2).
            public double greediness;

        }
        private findAnisoParas fParas = new findAnisoParas();
        public findAnisoParas wFparas = new findAnisoParas();
        private findAnisoParas fParasone = new findAnisoParas();
        private findAnisoPara fPara = new findAnisoPara();
        public FindAnisoShapeModelFBDForm(int idx)
        {
            InitializeComponent();
            rowOriNum = Svision.GetMe().oriRowNumber;
            columnOriNum = Svision.GetMe().oriColumnNumber;
            //rowOriNum = 1234;
            //columnOriNum = 1624;
            double widRat = 256 / ((double)columnOriNum);
            double heiRat = 192 / ((double)rowOriNum);
            resRat = widRat < heiRat ? widRat : heiRat;

            currentIndex = idx;

        }

        ~FindAnisoShapeModelFBDForm()
        {
            // HOperatorSet.ClearAllShapeModels();
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
            if (imageTempShowWithOutPoint != null)
            {
                imageTempShowWithOutPoint.Dispose();
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
            //if (templateModelImages != null)
            //{
            //    templateModelImages.Dispose();


            //}

        }

        private void WFparaInit()
        {
            modelIfCkecked = new bool[MAX_MODEL_NUM];
            wFparas.angleStart = new double[MAX_MODEL_NUM];
            wFparas.angleExtent = new double[MAX_MODEL_NUM];
            wFparas.scaleRMin = new double[MAX_MODEL_NUM];
            wFparas.scaleRMax = new double[MAX_MODEL_NUM];
            wFparas.scaleCMin = new double[MAX_MODEL_NUM];
            wFparas.scaleCMax = new double[MAX_MODEL_NUM];
            wFparas.minScore = new double[MAX_MODEL_NUM];
            wFparas.numMatches = new int[MAX_MODEL_NUM];
            wFparas.subPixel = new int[MAX_MODEL_NUM * 2];
            wFparas.numLevels = new int[MAX_MODEL_NUM * 2];
            wFparas.greediness = new double[MAX_MODEL_NUM];

            for (int i = 0; i < MAX_MODEL_NUM; i++)
            {
                wFparas.angleStart[i] = UserCode.GetInstance().gProCd[currentIndex].gASSP.angleStart[i];
                wFparas.angleExtent[i] = UserCode.GetInstance().gProCd[currentIndex].gASSP.angleExtent[i];
                wFparas.scaleRMin[i] = UserCode.GetInstance().gProCd[currentIndex].gASSP.scaleRMin[i];
                wFparas.scaleRMax[i] = UserCode.GetInstance().gProCd[currentIndex].gASSP.scaleRMax[i];
                wFparas.scaleCMin[i] = UserCode.GetInstance().gProCd[currentIndex].gASSP.scaleCMin[i];
                wFparas.scaleCMax[i] = UserCode.GetInstance().gProCd[currentIndex].gASSP.scaleCMax[i];
                wFparas.minScore[i] = UserCode.GetInstance().gProCd[currentIndex].gASSP.minScore[i];
                wFparas.numMatches[i] = UserCode.GetInstance().gProCd[currentIndex].gASSP.numMatches[i];
                wFparas.subPixel[i * 2] = UserCode.GetInstance().gProCd[currentIndex].gASSP.subPixel[i * 2];
                wFparas.subPixel[i * 2 + 1] = UserCode.GetInstance().gProCd[currentIndex].gASSP.subPixel[i * 2 + 1];
                wFparas.numLevels[i * 2] = UserCode.GetInstance().gProCd[currentIndex].gASSP.numLevels[i * 2];
                wFparas.numLevels[i * 2 + 1] = UserCode.GetInstance().gProCd[currentIndex].gASSP.numLevels[i * 2 + 1];
                wFparas.greediness[i] = UserCode.GetInstance().gProCd[currentIndex].gASSP.greediness[i];
            }
            wFparas.maxOverlap = UserCode.GetInstance().gProCd[currentIndex].gASSP.maxOverlap;

            if (UserCode.GetInstance().gProCd[currentIndex].gASSP.shapeModel == null)
            {

            }
            else
            {

                templateNumber = UserCode.GetInstance().gProCd[currentIndex].gASSP.shapeModel.TupleLength();
                tModelIDs = UserCode.GetInstance().gProCd[currentIndex].gASSP.shapeModel;
                templateModelRegions = UserCode.GetInstance().gProCd[currentIndex].gASSP.shapeModelRegion;
                modelPoints.Clear();
                if (UserCode.GetInstance().gProCd[currentIndex].gASSP.shapeModelPoints != null)
                {
                    for (int io = 0; io < UserCode.GetInstance().gProCd[currentIndex].gASSP.shapeModelPoints.Count; io++)
                    {
                        PointF pf = new PointF(0, 0);
                        pf.X = UserCode.GetInstance().gProCd[currentIndex].gASSP.shapeModelPoints[io].X;
                        pf.Y = UserCode.GetInstance().gProCd[currentIndex].gASSP.shapeModelPoints[io].Y;
                        modelPoints.Add(pf);
                    }
                }
                if (templateModelImages != null)
                {
                    templateModelImages.Dispose();
                }
                templateModelImages = UserCode.GetInstance().gProCd[currentIndex].gASSP.shapeModelImage;
                // checkBoxMultiplePara.Checked = UserCode.GetInstance().gProCd[currentIndex].boolData[1];
                checkBoxBorderShapeModel.Checked = UserCode.GetInstance().gProCd[currentIndex].gASSP.isBorderShapeModelChecked;

                if (UserCode.GetInstance().gProCd[currentIndex].gASSP.shapeModel.TupleLength() == 1)         //单模板
                {
                    checkedListBoxTemplate.Items.Add("模版1", false);
                    if (UserCode.GetInstance().gProCd[currentIndex].gASSP.modelIsChecked[0])
                    {
                        checkedListBoxTemplate.SetItemChecked(0, true);
                        numericUpDownTestStartAngle.Value = (decimal)UserCode.GetInstance().gProCd[currentIndex].gASSP.angleStart[MAX_MODEL_MUL_PARA_NUM - 1];
                        numericUpDownTestExternAngle.Value = (decimal)UserCode.GetInstance().gProCd[currentIndex].gASSP.angleExtent[MAX_MODEL_MUL_PARA_NUM - 1];
                        numericUpDownTestScaleRMin.Value = (decimal)UserCode.GetInstance().gProCd[currentIndex].gASSP.scaleRMin[MAX_MODEL_MUL_PARA_NUM - 1];
                        numericUpDownTestScaleRMax.Value = (decimal)UserCode.GetInstance().gProCd[currentIndex].gASSP.scaleRMax[MAX_MODEL_MUL_PARA_NUM - 1];
                        numericUpDownTestScaleCMin.Value = (decimal)UserCode.GetInstance().gProCd[currentIndex].gASSP.scaleCMin[MAX_MODEL_MUL_PARA_NUM - 1];
                        numericUpDownTestScaleCMax.Value = (decimal)UserCode.GetInstance().gProCd[currentIndex].gASSP.scaleCMax[MAX_MODEL_MUL_PARA_NUM - 1];
                        numericUpDownTestMinScore.Value = (decimal)UserCode.GetInstance().gProCd[currentIndex].gASSP.minScore[MAX_MODEL_MUL_PARA_NUM - 1];
                        numericUpDownTestNumber.Value = (decimal)UserCode.GetInstance().gProCd[currentIndex].gASSP.numMatches[MAX_MODEL_MUL_PARA_NUM - 1];
                        numericUpDownTestOverLap.Value = (decimal)UserCode.GetInstance().gProCd[currentIndex].gASSP.maxOverlap;
                        comboBoxSubPixel1.SelectedIndex = (int)UserCode.GetInstance().gProCd[currentIndex].gASSP.subPixel[(MAX_MODEL_MUL_PARA_NUM - 1) * 2];
                        comboBoxSubPixel2.SelectedIndex = (int)UserCode.GetInstance().gProCd[currentIndex].gASSP.subPixel[(MAX_MODEL_MUL_PARA_NUM - 1) * 2 + 1];
                        numericUpDownTestGreediness.Value = (decimal)UserCode.GetInstance().gProCd[currentIndex].gASSP.greediness[MAX_MODEL_MUL_PARA_NUM - 1];
                        if (UserCode.GetInstance().gProCd[currentIndex].gASSP.numLevels[(MAX_MODEL_MUL_PARA_NUM - 1) * 2] == 0 && UserCode.GetInstance().gProCd[currentIndex].gASSP.numLevels[(MAX_MODEL_MUL_PARA_NUM - 1) * 2 + 1] == 0)
                        {
                            checkBoxTestNumberLevelsAuto.Checked = true;
                        }
                        else
                        {
                            checkBoxTestNumberLevelsAuto.Checked = false;
                            numericUpDownTestNumberLevelsLarge.Value = (decimal)UserCode.GetInstance().gProCd[currentIndex].gASSP.numLevels[(MAX_MODEL_MUL_PARA_NUM - 1) * 2];
                            numericUpDownTestNumberLevelsSmall.Value = (decimal)UserCode.GetInstance().gProCd[currentIndex].gASSP.numLevels[(MAX_MODEL_MUL_PARA_NUM - 1) * 2 + 1];
                        }

                        checkBoxModelIsOK.Checked = true;
                        checkBoxMultiplePara.Checked = false;
                    }




                }
                else
                {
                    int i_num = 0;
                    for (int i = 0; i < UserCode.GetInstance().gProCd[currentIndex].gASSP.shapeModel.TupleLength(); i++)
                    {
                        checkedListBoxTemplate.Items.Add("模版" + (i + 1).ToString(), false);
                        if (UserCode.GetInstance().gProCd[currentIndex].gASSP.modelIsChecked[i])
                        {
                            checkedListBoxTemplate.SetItemChecked(i, true);
                            i_num++;
                            // comboBoxModel.Items.Add("模版" + System.String.Format("{0:D5}", tmpIdx));
                        }
                        else
                        {
                            checkedListBoxTemplate.SetItemChecked(i, false);
                        }
                    }
                    if (i_num != 0)
                    {
                        if (UserCode.GetInstance().gProCd[currentIndex].gASSP.isMultiplePara)//多模版多参数
                        {

                            checkBoxModelIsOK.Checked = true;
                            checkBoxMultiplePara.Checked = true;
                            

                        }
                        else//多模版单参数
                        {



                            numericUpDownTestStartAngle.Value = (decimal)UserCode.GetInstance().gProCd[currentIndex].gASSP.angleStart[MAX_MODEL_MUL_PARA_NUM - 1];
                            numericUpDownTestExternAngle.Value = (decimal)UserCode.GetInstance().gProCd[currentIndex].gASSP.angleExtent[MAX_MODEL_MUL_PARA_NUM - 1];
                            numericUpDownTestScaleRMin.Value = (decimal)UserCode.GetInstance().gProCd[currentIndex].gASSP.scaleRMin[MAX_MODEL_MUL_PARA_NUM - 1];
                            numericUpDownTestScaleRMax.Value = (decimal)UserCode.GetInstance().gProCd[currentIndex].gASSP.scaleRMax[MAX_MODEL_MUL_PARA_NUM - 1];
                            numericUpDownTestScaleCMin.Value = (decimal)UserCode.GetInstance().gProCd[currentIndex].gASSP.scaleCMin[MAX_MODEL_MUL_PARA_NUM - 1];
                            numericUpDownTestScaleCMax.Value = (decimal)UserCode.GetInstance().gProCd[currentIndex].gASSP.scaleCMax[MAX_MODEL_MUL_PARA_NUM - 1];
                            numericUpDownTestMinScore.Value = (decimal)UserCode.GetInstance().gProCd[currentIndex].gASSP.minScore[MAX_MODEL_MUL_PARA_NUM - 1];
                            numericUpDownTestNumber.Value = (decimal)UserCode.GetInstance().gProCd[currentIndex].gASSP.numMatches[MAX_MODEL_MUL_PARA_NUM - 1];
                            numericUpDownTestOverLap.Value = (decimal)UserCode.GetInstance().gProCd[currentIndex].gASSP.maxOverlap;
                            comboBoxSubPixel1.SelectedIndex = (int)UserCode.GetInstance().gProCd[currentIndex].gASSP.subPixel[(MAX_MODEL_MUL_PARA_NUM - 1) * 2];
                            comboBoxSubPixel2.SelectedIndex = (int)UserCode.GetInstance().gProCd[currentIndex].gASSP.subPixel[(MAX_MODEL_MUL_PARA_NUM - 1) * 2 + 1];
                            numericUpDownTestGreediness.Value = (decimal)UserCode.GetInstance().gProCd[currentIndex].gASSP.greediness[MAX_MODEL_MUL_PARA_NUM - 1];
                            if (UserCode.GetInstance().gProCd[currentIndex].gASSP.numLevels[(MAX_MODEL_MUL_PARA_NUM - 1) * 2] == 0 && UserCode.GetInstance().gProCd[currentIndex].gASSP.numLevels[(MAX_MODEL_MUL_PARA_NUM - 1) * 2 + 1] == 0)
                            {
                                checkBoxTestNumberLevelsAuto.Checked = true;
                            }
                            else
                            {
                                numericUpDownTestNumberLevelsLarge.Value = (decimal)UserCode.GetInstance().gProCd[currentIndex].gASSP.numLevels[(MAX_MODEL_MUL_PARA_NUM - 1) * 2];
                                numericUpDownTestNumberLevelsSmall.Value = (decimal)UserCode.GetInstance().gProCd[currentIndex].gASSP.numLevels[(MAX_MODEL_MUL_PARA_NUM - 1) * 2 + 1];
                            }

                            checkBoxModelIsOK.Checked = true;
                            checkBoxMultiplePara.Checked = false;
                        }
                    }

                }
               
            }
            arrangeIndex = UserCode.GetInstance().gProCd[currentIndex].gASSP.arrangeIndex;
            switch (arrangeIndex)
            {
                case 0: radioButtonXLS.Checked = true;
                    break;
                case 1: radioButtonXSL.Checked = true;
                    break;
                case 2: radioButtonYLS.Checked = true;
                    break;
                case 3: radioButtonYSL.Checked = true;
                    break;
                default:
                    break;
            }
        }

        private void FindAnisoShapeModelFBDForm_Load(object sender, EventArgs e)
        {

            HOperatorSet.OpenWindow(0, 0, 256, 192, findShapeModelPictureBox.Handle, "visible", "", out findShapeModelHWHandle);
            HOperatorSet.SetPart(findShapeModelHWHandle, 0, 0, 191, 255);
            HDevWindowStack.Push(findShapeModelHWHandle);
            //HOperatorSet.SetLineWidth(findShapeModelHWHandle, 1);
            //HOperatorSet.SetColor(findShapeModelHWHandle, "green");
            HOperatorSet.OpenWindow(0, 0, (findShapeModelPartPictureBox.Width), (findShapeModelPartPictureBox.Height), findShapeModelPartPictureBox.Handle, "visible", "", out findShapeModelPartHWHandle);
            HOperatorSet.SetPart(findShapeModelPartHWHandle, 0, 0, (findShapeModelPartPictureBox.Height - 1), (findShapeModelPartPictureBox.Width - 1));
            HDevWindowStack.Push(findShapeModelPartHWHandle);

            this.findShapeModelPictureBox.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.findShapeModelPictureBox_MouseWheel);
            cPara.numberLevel = 0;
            cPara.angleStart = -3.15;
            cPara.angleExtent = 6.29;
            cPara.scaleRMin = 1;
            cPara.scaleRMax = 1;
            cPara.scaleCMin = 1;
            cPara.scaleCMax = 1;
            cPara.angleStep = 0;
            cPara.scaleRStep = 0;
            cPara.scaleCStep = 0;
            cPara.optimization = new int[2] { 0, 0 };
            if (Svision.GetMe().baslerCamera.getChannelNumber() == 1)
            {
                cPara.metric = 2;
            }
            else
            {
                cPara.metric = 0;
            }
            cPara.contrast = new int[3] { 0, 0, 0 };
            cPara.minContrast = 0;
            autoCreateShapeParametersArray = new int[8] { 0, 0, 0, 0, 0, 0, 0, 0 };


            HOperatorSet.SetSystem("border_shape_models", "false");

            //detective point
            numericUpDownPointX.Maximum = (decimal)columnOriNum;
            numericUpDownPointY.Maximum = (decimal)rowOriNum;
            //(['num_levels', 'angle_step', 'scale_step', 'optimization', 'contrast_low', 'contrast_high', 'min_size', 'min_contrast']) 
            //Create shape model para
            //numberLever
            if (cPara.numberLevel == 0)
            {
                checkBoxAutoNumLevel.Checked = true;
                autoCreateShapeParametersArray[0] = 1;
                numericUpDownNumberLevel.Enabled = false;
            }
            else
            {
                checkBoxAutoNumLevel.Checked = false;
                numericUpDownNumberLevel.Enabled = true;
                numericUpDownNumberLevel.Value = cPara.numberLevel;
            }

            //Angle start
            numericUpDownAngleStart.Value = (decimal)cPara.angleStart;
            //Angle extent
            numericUpDownAngleExtent.Value = (decimal)cPara.angleExtent;
            //scaleRMin
            numericUpDownScaleRMin.Value = (decimal)cPara.scaleRMin;
            //scaleRMax
            numericUpDownScaleRMax.Value = (decimal)cPara.scaleRMax;
            //scaleRMin
            numericUpDownScaleCMin.Value = (decimal)cPara.scaleCMin;
            //scaleRMax
            numericUpDownScaleCMax.Value = (decimal)cPara.scaleCMax;

            //Angle step
            if (cPara.angleStep == 0)
            {
                checkBoxAutoAngleStep.Checked = true;
                autoCreateShapeParametersArray[1] = 1;
                numericUpDownAngleStep.Enabled = false;
            }
            else
            {
                checkBoxAutoAngleStep.Checked = false;
                numericUpDownAngleStep.Enabled = true;
                numericUpDownAngleStep.Value = (decimal)cPara.angleStep;
            }

            //optimization
            comboBoxOptimization1.SelectedIndex = cPara.optimization[0];
            if (cPara.optimization[0] == 0)
            {
                autoCreateShapeParametersArray[3] = 1;
            }
            comboBoxOptimization2.SelectedIndex = cPara.optimization[1];
            //metric
            comboBoxMetric.SelectedIndex = cPara.metric;
            //contrast
            if (cPara.contrast[0] == 0 && cPara.contrast[1] == 0 && cPara.contrast[2] == 0)
            {
                checkBoxAutoContrast.Checked = true;
                autoCreateShapeParametersArray[4] = 1;
                autoCreateShapeParametersArray[5] = 1;
                panelContarst.Enabled = false;
                checkBoxAutoLength.Checked = true;
                autoCreateShapeParametersArray[6] = 1;
                numericUpDownMinLength.Enabled = false;
            }
            else if (cPara.contrast[0] == 0 && cPara.contrast[1] == 0 && cPara.contrast[2] != 0)
            {
                checkBoxAutoContrast.Checked = true;
                autoCreateShapeParametersArray[4] = 1;
                autoCreateShapeParametersArray[5] = 1;
                panelContarst.Enabled = false;
                checkBoxAutoLength.Checked = false;
                numericUpDownMinLength.Enabled = true;
                numericUpDownMinLength.Value = cPara.contrast[2];
            }
            else if (cPara.contrast[0] != 0 && cPara.contrast[1] != 0 && cPara.contrast[2] == 0)
            {
                checkBoxAutoContrast.Checked = false;
                panelContarst.Enabled = true;
                numericUpDownContrastHigh.Value = cPara.contrast[1];
                numericUpDownContrastLow.Value = cPara.contrast[0];
                checkBoxAutoLength.Checked = true;
                autoCreateShapeParametersArray[6] = 1;
                numericUpDownMinLength.Enabled = false;
            }
            else
            {
                numericUpDownContrastHigh.Value = cPara.contrast[1];
                numericUpDownContrastLow.Value = cPara.contrast[0];
                numericUpDownMinLength.Value = cPara.contrast[2];
            }
            //minContrast
            numericUpDownMinContrast.Value = cPara.minContrast;
            if (cPara.minContrast == 0)
            {
                autoCreateShapeParametersArray[7] = 1;
            }
            checkBoxDetail.Checked = false;
            panelDetail.Enabled = false;

            ////tModelIDs
            //if (tModelIDs != null)
            //{
            //    if (tModelIDs.TupleLength() == templateNumber)
            //    {
            //        for (int i = 0; i < templateNumber; i++)
            //        {
            //            checkedListBoxTemplate.Items.Add("模版" + i.ToString(), false);
            //        }
            //    }


            //}
            //subpixel
            comboBoxSubPixel1.SelectedIndex = 2;
            comboBoxSubPixel2.SelectedIndex = 0;
            WFparaInit();
        }
        private void findShapeModelPictureBox_MouseWheel(object sender, MouseEventArgs e)
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
                    if (rowNum * resizerate < (findShapeModelPartPictureBox.Height) || columnNum * resizerate < (findShapeModelPartPictureBox.Width))
                    {
                        basicClass.displayClear(findShapeModelPartHWHandle);
                    }
                    if (imgPart != null)
                    {
                        imgPart.Dispose();
                    }
                    basicClass.resizeImage(imageTempShow, out imgPart, resizerate);
                    // HOperatorSet.DispObj(imgPart, findShapeModelPartHWHandle);
                    oriLocation = e.Location;
                    centerLocation.Y = (float)oriLocation.Y;
                    centerLocation.X = (float)oriLocation.X;
                    if (oriLocation.Y * resizerate / resRat - ((float)(findShapeModelPartPictureBox.Height - 1 + 0) / 2.0) < 0 && oriLocation.X * resizerate / resRat - ((float)(findShapeModelPartPictureBox.Width - 1 + 0) / 2.0) < 0)
                    {
                        HObject temp;
                        basicClass.genRectangle1(out rectDomain, 0, 0, (findShapeModelPartPictureBox.Height - 1), (findShapeModelPartPictureBox.Width - 1));
                        centerLocation.Y = (float)(((float)(System.Math.Min(findShapeModelPartPictureBox.Height, rowNum * resizerate) - 1 + 0) / 2.0) * resRat / resizerate);
                        centerLocation.X = (float)(((float)(System.Math.Min(findShapeModelPartPictureBox.Width, columnNum * resizerate) - 1 + 0) / 2.0) * resRat / resizerate);

                        basicClass.reduceDomain(imgPart, rectDomain, out temp);
                        if (imgPartTemp != null)
                        {
                            imgPartTemp.Dispose();
                        }
                        HOperatorSet.CropDomain(temp, out imgPartTemp);
                        HOperatorSet.DispObj(imgPartTemp, findShapeModelPartHWHandle);
                        temp.Dispose();
                    }
                    else if (oriLocation.Y * resizerate / resRat - ((float)(findShapeModelPartPictureBox.Height - 1 + 0) / 2.0) <= 0 && oriLocation.X * resizerate / resRat - ((float)(findShapeModelPartPictureBox.Width - 1 + 0) / 2.0) >= 0 && oriLocation.X * resizerate / resRat + ((float)(findShapeModelPartPictureBox.Width - 1 + 0) / 2.0) < columnNum * resizerate - 1)
                    {
                        HObject temp;
                        basicClass.genRectangle1(out rectDomain, 0, oriLocation.X * resizerate / resRat - ((float)(findShapeModelPartPictureBox.Width - 1 + 0) / 2.0), (findShapeModelPartPictureBox.Height - 1), oriLocation.X * resizerate / resRat + ((float)(findShapeModelPartPictureBox.Width - 1 + 0) / 2.0));
                        centerLocation.Y = (float)(((float)(System.Math.Min(findShapeModelPartPictureBox.Height, rowNum * resizerate) - 1 + 0) / 2.0) * resRat / resizerate);
                        centerLocation.X = (float)oriLocation.X;
                        basicClass.reduceDomain(imgPart, rectDomain, out temp);
                        if (imgPartTemp != null)
                        {
                            imgPartTemp.Dispose();
                        }
                        HOperatorSet.CropDomain(temp, out imgPartTemp);
                        HOperatorSet.DispObj(imgPartTemp, findShapeModelPartHWHandle);
                        temp.Dispose();
                    }
                    else if (oriLocation.Y * resizerate / resRat - ((float)(findShapeModelPartPictureBox.Height - 1 + 0) / 2.0) > 0 && oriLocation.Y * resizerate / resRat + ((float)(findShapeModelPartPictureBox.Height - 1 + 0) / 2.0) <= rowNum * resizerate - 1 && oriLocation.X * resizerate / resRat - ((float)(findShapeModelPartPictureBox.Width - 1 + 0) / 2.0) < 0)
                    {
                        HObject temp;
                        basicClass.genRectangle1(out rectDomain, oriLocation.Y * resizerate / resRat - ((float)(findShapeModelPartPictureBox.Height - 1 + 0) / 2.0), 0, oriLocation.Y * resizerate / resRat + ((float)(findShapeModelPartPictureBox.Height - 1 + 0) / 2.0), (findShapeModelPartPictureBox.Width - 1));
                        centerLocation.Y = (float)oriLocation.Y;
                        centerLocation.X = (float)(((float)(System.Math.Min(findShapeModelPartPictureBox.Width, columnNum * resizerate) - 1 + 0) / 2.0) * resRat / resizerate);
                        basicClass.reduceDomain(imgPart, rectDomain, out temp);
                        if (imgPartTemp != null)
                        {
                            imgPartTemp.Dispose();
                        }
                        HOperatorSet.CropDomain(temp, out imgPartTemp);
                        HOperatorSet.DispObj(imgPartTemp, findShapeModelPartHWHandle);
                        temp.Dispose();
                    }
                    else if (oriLocation.Y * resizerate / resRat + ((float)(findShapeModelPartPictureBox.Height - 1 + 0) / 2.0) > rowNum * resizerate - 1 && oriLocation.X * resizerate / resRat - ((float)(findShapeModelPartPictureBox.Width - 1 + 0) / 2.0) < 0)
                    {
                        HObject temp;
                        basicClass.genRectangle1(out rectDomain, rowNum * resizerate - 1 - (findShapeModelPartPictureBox.Height - 1), 0, rowNum * resizerate - 1, (findShapeModelPartPictureBox.Width - 1));
                        centerLocation.Y = (float)((rowNum * resizerate - ((float)(System.Math.Min(findShapeModelPartPictureBox.Height, rowNum * resizerate) - 1 + 0) / 2.0) - 1) * resRat / resizerate);
                        centerLocation.X = (float)(((float)(System.Math.Min(findShapeModelPartPictureBox.Width, columnNum * resizerate) - 1 + 0) / 2.0) * resRat / resizerate);
                        basicClass.reduceDomain(imgPart, rectDomain, out temp);
                        if (imgPartTemp != null)
                        {
                            imgPartTemp.Dispose();
                        }
                        HOperatorSet.CropDomain(temp, out imgPartTemp);
                        HOperatorSet.DispObj(imgPartTemp, findShapeModelPartHWHandle);
                        temp.Dispose();
                    }
                    else if (oriLocation.Y * resizerate / resRat + ((float)(findShapeModelPartPictureBox.Height - 1 + 0) / 2.0) > rowNum * resizerate - 1 && oriLocation.X * resizerate / resRat - ((float)(findShapeModelPartPictureBox.Width - 1 + 0) / 2.0) > 0 && oriLocation.X * resizerate / resRat + ((float)(findShapeModelPartPictureBox.Width - 1 + 0) / 2.0) < columnNum * resizerate - 1)
                    {
                        HObject temp;
                        basicClass.genRectangle1(out rectDomain, rowNum * resizerate - (findShapeModelPartPictureBox.Height), oriLocation.X * resizerate / resRat - ((float)(findShapeModelPartPictureBox.Width - 1 + 0) / 2.0), rowNum * resizerate - 1, oriLocation.X * resizerate / resRat + ((float)(findShapeModelPartPictureBox.Width - 1 + 0) / 2.0));
                        centerLocation.Y = (float)((rowNum * resizerate - ((float)(System.Math.Min(findShapeModelPartPictureBox.Height, rowNum * resizerate) - 1 + 0) / 2.0 + 1)) * resRat / resizerate);
                        centerLocation.X = (float)oriLocation.X;
                        basicClass.reduceDomain(imgPart, rectDomain, out temp);
                        if (imgPartTemp != null)
                        {
                            imgPartTemp.Dispose();
                        }
                        HOperatorSet.CropDomain(temp, out imgPartTemp);
                        HOperatorSet.DispObj(imgPartTemp, findShapeModelPartHWHandle);
                        temp.Dispose();
                    }
                    else if (oriLocation.Y * resizerate / resRat + ((float)(findShapeModelPartPictureBox.Height - 1 + 0) / 2.0) >= rowNum * resizerate - 1 && oriLocation.X * resizerate / resRat + ((float)(findShapeModelPartPictureBox.Width - 1 + 0) / 2.0) >= columnNum * resizerate - 1)
                    {
                        HObject temp;
                        basicClass.genRectangle1(out rectDomain, rowNum * resizerate - (findShapeModelPartPictureBox.Height), columnNum * resizerate - (findShapeModelPartPictureBox.Width), rowNum * resizerate - 1, columnNum * resizerate - 1);
                        centerLocation.Y = (float)((rowNum * resizerate - ((float)(System.Math.Min(findShapeModelPartPictureBox.Height, rowNum * resizerate) - 1 + 0) / 2.0 + 1)) * resRat / resizerate);
                        centerLocation.X = (float)((columnNum * resizerate - ((float)(System.Math.Min(findShapeModelPartPictureBox.Width, columnNum * resizerate) - 1 + 0) / 2.0 + 1)) * resRat / resizerate);
                        basicClass.reduceDomain(imgPart, rectDomain, out temp);
                        if (imgPartTemp != null)
                        {
                            imgPartTemp.Dispose();
                        }
                        HOperatorSet.CropDomain(temp, out imgPartTemp);
                        HOperatorSet.DispObj(imgPartTemp, findShapeModelPartHWHandle);
                        temp.Dispose();
                    }
                    else if (oriLocation.Y * resizerate / resRat + ((float)(findShapeModelPartPictureBox.Height - 1 + 0) / 2.0) < rowNum * resizerate - 1 && oriLocation.Y * resizerate / resRat - ((float)(findShapeModelPartPictureBox.Height - 1 + 0) / 2.0) > 0 && oriLocation.X * resizerate / resRat + ((float)(findShapeModelPartPictureBox.Width - 1 + 0) / 2.0) > columnNum * resizerate - 1)
                    {
                        HObject temp;
                        basicClass.genRectangle1(out rectDomain, oriLocation.Y * resizerate / resRat - ((float)(findShapeModelPartPictureBox.Height - 1 + 0) / 2.0), columnNum * resizerate - (findShapeModelPartPictureBox.Width), oriLocation.Y * resizerate / resRat + ((float)(findShapeModelPartPictureBox.Height - 1 + 0) / 2.0), columnNum * resizerate - 1);
                        centerLocation.Y = (float)oriLocation.Y;
                        centerLocation.X = (float)((columnNum * resizerate - ((float)(System.Math.Min(findShapeModelPartPictureBox.Width, columnNum * resizerate) - 1 + 0) / 2.0 + 1)) * resRat / resizerate);
                        basicClass.reduceDomain(imgPart, rectDomain, out temp);
                        if (imgPartTemp != null)
                        {
                            imgPartTemp.Dispose();
                        }
                        HOperatorSet.CropDomain(temp, out imgPartTemp);
                        HOperatorSet.DispObj(imgPartTemp, findShapeModelPartHWHandle);
                        temp.Dispose();
                    }
                    else if (oriLocation.Y * resizerate / resRat - ((float)(findShapeModelPartPictureBox.Height - 1 + 0) / 2.0) <= 0 && oriLocation.X * resizerate / resRat + ((float)(findShapeModelPartPictureBox.Width - 1 + 0) / 2.0) >= columnNum * resizerate - 1)
                    {
                        HObject temp;
                        basicClass.genRectangle1(out rectDomain, 0, columnNum * resizerate - (findShapeModelPartPictureBox.Width), (findShapeModelPartPictureBox.Height - 1), columnNum * resizerate - 1);
                        centerLocation.Y = (float)(((float)(System.Math.Min(findShapeModelPartPictureBox.Height, rowNum * resizerate) - 1 + 0) / 2.0) * resRat / resizerate);
                        centerLocation.X = (float)((columnNum * resizerate - ((float)(System.Math.Min(findShapeModelPartPictureBox.Width, columnNum * resizerate) - 1 + 0) / 2.0 + 1)) * resRat / resizerate);
                        basicClass.reduceDomain(imgPart, rectDomain, out temp);
                        if (imgPartTemp != null)
                        {
                            imgPartTemp.Dispose();
                        }
                        HOperatorSet.CropDomain(temp, out imgPartTemp);
                        HOperatorSet.DispObj(imgPartTemp, findShapeModelPartHWHandle);
                        temp.Dispose();
                    }
                    else
                    {
                        HObject temp;
                        basicClass.genRectangle1(out rectDomain, oriLocation.Y * resizerate / resRat - ((float)(findShapeModelPartPictureBox.Height - 1 + 0) / 2.0), oriLocation.X * resizerate / resRat - ((float)(findShapeModelPartPictureBox.Width - 1 + 0) / 2.0), oriLocation.Y * resizerate / resRat + ((float)(findShapeModelPartPictureBox.Height - 1 + 0) / 2.0), oriLocation.X * resizerate / resRat + ((float)(findShapeModelPartPictureBox.Width - 1 + 0) / 2.0));
                        basicClass.reduceDomain(imgPart, rectDomain, out temp);
                        if (imgPartTemp != null)
                        {
                            imgPartTemp.Dispose();
                        }
                        HOperatorSet.CropDomain(temp, out imgPartTemp);
                        HOperatorSet.DispObj(imgPartTemp, findShapeModelPartHWHandle);
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
                //Svision.GetMe().baslerCamera.getFrameImage(out imageCopy);
                UserCode.GetInstance().getImageFromProcess(out imageCopy, currentIndex);
                basicClass.getImageSize(imageCopy, out rowNum, out columnNum);
                //if (rowNum == rowOriNum && columnNum == columnOriNum)
                //{
                if (rowNum != rowOriNum || columnNum != columnOriNum)
                {
                    HOperatorSet.ClearWindow(findShapeModelHWHandle);
                    HOperatorSet.ClearWindow(findShapeModelPartHWHandle);
                }
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
                    basicClass.displayhobject(img, findShapeModelHWHandle);
                    if (imgPart != null)
                    {
                        imgPart.Dispose();
                    }
                    basicClass.resizeImage(imageTemp, out imgPart, resizerate);
                    HObject temp;
                    basicClass.genRectangle1(out rectDomain, 0, 0, (findShapeModelPartPictureBox.Height - 1), (findShapeModelPartPictureBox.Width - 1));
                    basicClass.reduceDomain(imgPart, rectDomain, out temp);
                    centerLocation.Y = (float)(((float)(System.Math.Min(findShapeModelPartPictureBox.Height, rowNum * resizerate) - 1 + 0) / 2.0) * resRat / resizerate);
                    centerLocation.X = (float)(((float)(System.Math.Min(findShapeModelPartPictureBox.Width, columnNum * resizerate) - 1 + 0) / 2.0) * resRat / resizerate);
                    if (imgPartTemp != null)
                    {
                        imgPartTemp.Dispose();
                    }
                    HOperatorSet.CropDomain(temp, out imgPartTemp);
                    HOperatorSet.DispObj(imgPartTemp, findShapeModelPartHWHandle);
                    temp.Dispose();
                    oriLocation.X = (int)centerLocation.X;
                    oriLocation.Y = (int)centerLocation.Y;
                //}
                //else
                //{
                //    MessageBox.Show("原始图像行和列不可更改！");
                //}

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
                    if (oriLocation.Y * resizerate / resRat - ((float)(findShapeModelPartPictureBox.Height - 1 + 0) / 2.0) < 0 && oriLocation.X * resizerate / resRat - ((float)(findShapeModelPartPictureBox.Width - 1 + 0) / 2.0) < 0)
                    {
                        HObject temp;
                        basicClass.genRectangle1(out rectDomain, 0, 0, (findShapeModelPartPictureBox.Height - 1), (findShapeModelPartPictureBox.Width - 1));
                        centerLocation.Y = (float)(((float)(System.Math.Min(findShapeModelPartPictureBox.Height, rowNum * resizerate) - 1 + 0) / 2.0) * resRat / resizerate);
                        centerLocation.X = (float)(((float)(System.Math.Min(findShapeModelPartPictureBox.Width, columnNum * resizerate) - 1 + 0) / 2.0) * resRat / resizerate);

                        basicClass.reduceDomain(imgPart, rectDomain, out temp);
                        if (imgPartTemp != null)
                        {
                            imgPartTemp.Dispose();
                        }
                        HOperatorSet.CropDomain(temp, out imgPartTemp);
                        HOperatorSet.DispObj(imgPartTemp, findShapeModelPartHWHandle);
                        temp.Dispose();
                    }
                    else if (oriLocation.Y * resizerate / resRat - ((float)(findShapeModelPartPictureBox.Height - 1 + 0) / 2.0) <= 0 && oriLocation.X * resizerate / resRat - ((float)(findShapeModelPartPictureBox.Width - 1 + 0) / 2.0) >= 0 && oriLocation.X * resizerate / resRat + ((float)(findShapeModelPartPictureBox.Width - 1 + 0) / 2.0) < columnNum * resizerate - 1)
                    {
                        HObject temp;
                        basicClass.genRectangle1(out rectDomain, 0, oriLocation.X * resizerate / resRat - ((float)(findShapeModelPartPictureBox.Width - 1 + 0) / 2.0), (findShapeModelPartPictureBox.Height - 1), oriLocation.X * resizerate / resRat + ((float)(findShapeModelPartPictureBox.Width - 1 + 0) / 2.0));
                        centerLocation.Y = (float)(((float)(System.Math.Min(findShapeModelPartPictureBox.Height, rowNum * resizerate) - 1 + 0) / 2.0) * resRat / resizerate);
                        centerLocation.X = (float)oriLocation.X;
                        basicClass.reduceDomain(imgPart, rectDomain, out temp);
                        if (imgPartTemp != null)
                        {
                            imgPartTemp.Dispose();
                        }
                        HOperatorSet.CropDomain(temp, out imgPartTemp);
                        HOperatorSet.DispObj(imgPartTemp, findShapeModelPartHWHandle);
                        temp.Dispose();
                    }
                    else if (oriLocation.Y * resizerate / resRat - ((float)(findShapeModelPartPictureBox.Height - 1 + 0) / 2.0) > 0 && oriLocation.Y * resizerate / resRat + ((float)(findShapeModelPartPictureBox.Height - 1 + 0) / 2.0) <= rowNum * resizerate - 1 && oriLocation.X * resizerate / resRat - ((float)(findShapeModelPartPictureBox.Width - 1 + 0) / 2.0) < 0)
                    {
                        HObject temp;
                        basicClass.genRectangle1(out rectDomain, oriLocation.Y * resizerate / resRat - ((float)(findShapeModelPartPictureBox.Height - 1 + 0) / 2.0), 0, oriLocation.Y * resizerate / resRat + ((float)(findShapeModelPartPictureBox.Height - 1 + 0) / 2.0), (findShapeModelPartPictureBox.Width - 1));
                        centerLocation.Y = (float)oriLocation.Y;
                        centerLocation.X = (float)(((float)(System.Math.Min(findShapeModelPartPictureBox.Width, columnNum * resizerate) - 1 + 0) / 2.0) * resRat / resizerate);
                        basicClass.reduceDomain(imgPart, rectDomain, out temp);
                        if (imgPartTemp != null)
                        {
                            imgPartTemp.Dispose();
                        }
                        HOperatorSet.CropDomain(temp, out imgPartTemp);
                        HOperatorSet.DispObj(imgPartTemp, findShapeModelPartHWHandle);
                        temp.Dispose();
                    }
                    else if (oriLocation.Y * resizerate / resRat + ((float)(findShapeModelPartPictureBox.Height - 1 + 0) / 2.0) > rowNum * resizerate - 1 && oriLocation.X * resizerate / resRat - ((float)(findShapeModelPartPictureBox.Width - 1 + 0) / 2.0) < 0)
                    {
                        HObject temp;
                        basicClass.genRectangle1(out rectDomain, rowNum * resizerate - 1 - (findShapeModelPartPictureBox.Height - 1), 0, rowNum * resizerate - 1, (findShapeModelPartPictureBox.Width - 1));
                        centerLocation.Y = (float)((rowNum * resizerate - ((float)(System.Math.Min(findShapeModelPartPictureBox.Height, rowNum * resizerate) - 1 + 0) / 2.0) - 1) * resRat / resizerate);
                        centerLocation.X = (float)(((float)(System.Math.Min(findShapeModelPartPictureBox.Width, columnNum * resizerate) - 1 + 0) / 2.0) * resRat / resizerate);
                        basicClass.reduceDomain(imgPart, rectDomain, out temp);
                        if (imgPartTemp != null)
                        {
                            imgPartTemp.Dispose();
                        }
                        HOperatorSet.CropDomain(temp, out imgPartTemp);
                        HOperatorSet.DispObj(imgPartTemp, findShapeModelPartHWHandle);
                        temp.Dispose();
                    }
                    else if (oriLocation.Y * resizerate / resRat + ((float)(findShapeModelPartPictureBox.Height - 1 + 0) / 2.0) > rowNum * resizerate - 1 && oriLocation.X * resizerate / resRat - ((float)(findShapeModelPartPictureBox.Width - 1 + 0) / 2.0) > 0 && oriLocation.X * resizerate / resRat + ((float)(findShapeModelPartPictureBox.Width - 1 + 0) / 2.0) < columnNum * resizerate - 1)
                    {
                        HObject temp;
                        basicClass.genRectangle1(out rectDomain, rowNum * resizerate - (findShapeModelPartPictureBox.Height), oriLocation.X * resizerate / resRat - ((float)(findShapeModelPartPictureBox.Width - 1 + 0) / 2.0), rowNum * resizerate - 1, oriLocation.X * resizerate / resRat + ((float)(findShapeModelPartPictureBox.Width - 1 + 0) / 2.0));
                        centerLocation.Y = (float)((rowNum * resizerate - ((float)(System.Math.Min(findShapeModelPartPictureBox.Height, rowNum * resizerate) - 1 + 0) / 2.0 + 1)) * resRat / resizerate);
                        centerLocation.X = (float)oriLocation.X;
                        basicClass.reduceDomain(imgPart, rectDomain, out temp);
                        if (imgPartTemp != null)
                        {
                            imgPartTemp.Dispose();
                        }
                        HOperatorSet.CropDomain(temp, out imgPartTemp);
                        HOperatorSet.DispObj(imgPartTemp, findShapeModelPartHWHandle);
                        temp.Dispose();
                    }
                    else if (oriLocation.Y * resizerate / resRat + ((float)(findShapeModelPartPictureBox.Height - 1 + 0) / 2.0) >= rowNum * resizerate - 1 && oriLocation.X * resizerate / resRat + ((float)(findShapeModelPartPictureBox.Width - 1 + 0) / 2.0) >= columnNum * resizerate - 1)
                    {
                        HObject temp;
                        basicClass.genRectangle1(out rectDomain, rowNum * resizerate - (findShapeModelPartPictureBox.Height), columnNum * resizerate - (findShapeModelPartPictureBox.Width), rowNum * resizerate - 1, columnNum * resizerate - 1);
                        centerLocation.Y = (float)((rowNum * resizerate - ((float)(System.Math.Min(findShapeModelPartPictureBox.Height, rowNum * resizerate) - 1 + 0) / 2.0 + 1)) * resRat / resizerate);
                        centerLocation.X = (float)((columnNum * resizerate - ((float)(System.Math.Min(findShapeModelPartPictureBox.Width, columnNum * resizerate) - 1 + 0) / 2.0 + 1)) * resRat / resizerate);
                        basicClass.reduceDomain(imgPart, rectDomain, out temp);
                        if (imgPartTemp != null)
                        {
                            imgPartTemp.Dispose();
                        }
                        HOperatorSet.CropDomain(temp, out imgPartTemp);
                        HOperatorSet.DispObj(imgPartTemp, findShapeModelPartHWHandle);
                        temp.Dispose();
                    }
                    else if (oriLocation.Y * resizerate / resRat + ((float)(findShapeModelPartPictureBox.Height - 1 + 0) / 2.0) < rowNum * resizerate - 1 && oriLocation.Y * resizerate / resRat - ((float)(findShapeModelPartPictureBox.Height - 1 + 0) / 2.0) > 0 && oriLocation.X * resizerate / resRat + ((float)(findShapeModelPartPictureBox.Width - 1 + 0) / 2.0) > columnNum * resizerate - 1)
                    {
                        HObject temp;
                        basicClass.genRectangle1(out rectDomain, oriLocation.Y * resizerate / resRat - ((float)(findShapeModelPartPictureBox.Height - 1 + 0) / 2.0), columnNum * resizerate - (findShapeModelPartPictureBox.Width), oriLocation.Y * resizerate / resRat + ((float)(findShapeModelPartPictureBox.Height - 1 + 0) / 2.0), columnNum * resizerate - 1);
                        centerLocation.Y = (float)oriLocation.Y;
                        centerLocation.X = (float)((columnNum * resizerate - ((float)(System.Math.Min(findShapeModelPartPictureBox.Width, columnNum * resizerate) - 1 + 0) / 2.0 + 1)) * resRat / resizerate);
                        basicClass.reduceDomain(imgPart, rectDomain, out temp);
                        if (imgPartTemp != null)
                        {
                            imgPartTemp.Dispose();
                        }
                        HOperatorSet.CropDomain(temp, out imgPartTemp);
                        HOperatorSet.DispObj(imgPartTemp, findShapeModelPartHWHandle);
                        temp.Dispose();
                    }
                    else if (oriLocation.Y * resizerate / resRat - ((float)(findShapeModelPartPictureBox.Height - 1 + 0) / 2.0) <= 0 && oriLocation.X * resizerate / resRat + ((float)(findShapeModelPartPictureBox.Width - 1 + 0) / 2.0) >= columnNum * resizerate - 1)
                    {
                        HObject temp;
                        basicClass.genRectangle1(out rectDomain, 0, columnNum * resizerate - (findShapeModelPartPictureBox.Width), (findShapeModelPartPictureBox.Height - 1), columnNum * resizerate - 1);
                        centerLocation.Y = (float)(((float)(System.Math.Min(findShapeModelPartPictureBox.Height, rowNum * resizerate) - 1 + 0) / 2.0) * resRat / resizerate);
                        centerLocation.X = (float)((columnNum * resizerate - ((float)(System.Math.Min(findShapeModelPartPictureBox.Width, columnNum * resizerate) - 1 + 0) / 2.0 + 1)) * resRat / resizerate);
                        basicClass.reduceDomain(imgPart, rectDomain, out temp);
                        if (imgPartTemp != null)
                        {
                            imgPartTemp.Dispose();
                        }
                        HOperatorSet.CropDomain(temp, out imgPartTemp);
                        HOperatorSet.DispObj(imgPartTemp, findShapeModelPartHWHandle);
                        temp.Dispose();
                    }
                    else
                    {
                        HObject temp;
                        basicClass.genRectangle1(out rectDomain, oriLocation.Y * resizerate / resRat - ((float)(findShapeModelPartPictureBox.Height - 1 + 0) / 2.0), oriLocation.X * resizerate / resRat - ((float)(findShapeModelPartPictureBox.Width - 1 + 0) / 2.0), oriLocation.Y * resizerate / resRat + ((float)(findShapeModelPartPictureBox.Height - 1 + 0) / 2.0), oriLocation.X * resizerate / resRat + ((float)(findShapeModelPartPictureBox.Width - 1 + 0) / 2.0));
                        basicClass.reduceDomain(imgPart, rectDomain, out temp);
                        if (imgPartTemp != null)
                        {
                            imgPartTemp.Dispose();
                        }
                        HOperatorSet.CropDomain(temp, out imgPartTemp);
                        HOperatorSet.DispObj(imgPartTemp, findShapeModelPartHWHandle);
                        temp.Dispose();
                    }
                    this.findShapeModelPictureBox.Focus();
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }


        }
        private void findShapeModelPictureBoxMouseDown(object sender, MouseEventArgs e)
        {
            if (e.Location.X >= 0 && e.Location.X <= columnNum * resRat/*columnNum * resRat - 320 * resRat / resizerate*/ && e.Location.Y >= 0 && e.Location.Y <= rowNum * resRat/*rowNum * resRat - 240 * resRat / resizerate*/)
            {
                try
                {
                    Cursor = Cursors.Hand;
                    oriLocation = e.Location;
                    centerLocation.Y = (float)oriLocation.Y;
                    centerLocation.X = (float)oriLocation.X;
                    if (oriLocation.Y * resizerate / resRat - ((float)(findShapeModelPartPictureBox.Height - 1 + 0) / 2.0) < 0 && oriLocation.X * resizerate / resRat - ((float)(findShapeModelPartPictureBox.Width - 1 + 0) / 2.0) < 0)
                    {
                        HObject temp;
                        basicClass.genRectangle1(out rectDomain, 0, 0, (findShapeModelPartPictureBox.Height - 1), (findShapeModelPartPictureBox.Width - 1));
                        centerLocation.Y = (float)(((float)(System.Math.Min(findShapeModelPartPictureBox.Height, rowNum * resizerate) - 1 + 0) / 2.0) * resRat / resizerate);
                        centerLocation.X = (float)(((float)(System.Math.Min(findShapeModelPartPictureBox.Width, columnNum * resizerate) - 1 + 0) / 2.0) * resRat / resizerate);

                        basicClass.reduceDomain(imgPart, rectDomain, out temp);
                        if (imgPartTemp != null)
                        {
                            imgPartTemp.Dispose();
                        }
                        HOperatorSet.CropDomain(temp, out imgPartTemp);
                        HOperatorSet.DispObj(imgPartTemp, findShapeModelPartHWHandle);
                        temp.Dispose();
                    }
                    else if (oriLocation.Y * resizerate / resRat - ((float)(findShapeModelPartPictureBox.Height - 1 + 0) / 2.0) <= 0 && oriLocation.X * resizerate / resRat - ((float)(findShapeModelPartPictureBox.Width - 1 + 0) / 2.0) >= 0 && oriLocation.X * resizerate / resRat + ((float)(findShapeModelPartPictureBox.Width - 1 + 0) / 2.0) < columnNum * resizerate - 1)
                    {
                        HObject temp;
                        basicClass.genRectangle1(out rectDomain, 0, oriLocation.X * resizerate / resRat - ((float)(findShapeModelPartPictureBox.Width - 1 + 0) / 2.0), (findShapeModelPartPictureBox.Height - 1), oriLocation.X * resizerate / resRat + ((float)(findShapeModelPartPictureBox.Width - 1 + 0) / 2.0));
                        centerLocation.Y = (float)(((float)(System.Math.Min(findShapeModelPartPictureBox.Height, rowNum * resizerate) - 1 + 0) / 2.0) * resRat / resizerate);
                        centerLocation.X = (float)oriLocation.X;
                        basicClass.reduceDomain(imgPart, rectDomain, out temp);
                        if (imgPartTemp != null)
                        {
                            imgPartTemp.Dispose();
                        }
                        HOperatorSet.CropDomain(temp, out imgPartTemp);
                        HOperatorSet.DispObj(imgPartTemp, findShapeModelPartHWHandle);
                        temp.Dispose();
                    }
                    else if (oriLocation.Y * resizerate / resRat - ((float)(findShapeModelPartPictureBox.Height - 1 + 0) / 2.0) > 0 && oriLocation.Y * resizerate / resRat + ((float)(findShapeModelPartPictureBox.Height - 1 + 0) / 2.0) <= rowNum * resizerate - 1 && oriLocation.X * resizerate / resRat - ((float)(findShapeModelPartPictureBox.Width - 1 + 0) / 2.0) < 0)
                    {
                        HObject temp;
                        basicClass.genRectangle1(out rectDomain, oriLocation.Y * resizerate / resRat - ((float)(findShapeModelPartPictureBox.Height - 1 + 0) / 2.0), 0, oriLocation.Y * resizerate / resRat + ((float)(findShapeModelPartPictureBox.Height - 1 + 0) / 2.0), (findShapeModelPartPictureBox.Width - 1));
                        centerLocation.Y = (float)oriLocation.Y;
                        centerLocation.X = (float)(((float)(System.Math.Min(findShapeModelPartPictureBox.Width, columnNum * resizerate) - 1 + 0) / 2.0) * resRat / resizerate);
                        basicClass.reduceDomain(imgPart, rectDomain, out temp);
                        if (imgPartTemp != null)
                        {
                            imgPartTemp.Dispose();
                        }
                        HOperatorSet.CropDomain(temp, out imgPartTemp);
                        HOperatorSet.DispObj(imgPartTemp, findShapeModelPartHWHandle);
                        temp.Dispose();
                    }
                    else if (oriLocation.Y * resizerate / resRat + ((float)(findShapeModelPartPictureBox.Height - 1 + 0) / 2.0) > rowNum * resizerate - 1 && oriLocation.X * resizerate / resRat - ((float)(findShapeModelPartPictureBox.Width - 1 + 0) / 2.0) < 0)
                    {
                        HObject temp;
                        basicClass.genRectangle1(out rectDomain, rowNum * resizerate - 1 - (findShapeModelPartPictureBox.Height - 1), 0, rowNum * resizerate - 1, (findShapeModelPartPictureBox.Width - 1));
                        centerLocation.Y = (float)((rowNum * resizerate - ((float)(System.Math.Min(findShapeModelPartPictureBox.Height, rowNum * resizerate) - 1 + 0) / 2.0) - 1) * resRat / resizerate);
                        centerLocation.X = (float)(((float)(System.Math.Min(findShapeModelPartPictureBox.Width, columnNum * resizerate) - 1 + 0) / 2.0) * resRat / resizerate);
                        basicClass.reduceDomain(imgPart, rectDomain, out temp);
                        if (imgPartTemp != null)
                        {
                            imgPartTemp.Dispose();
                        }
                        HOperatorSet.CropDomain(temp, out imgPartTemp);
                        HOperatorSet.DispObj(imgPartTemp, findShapeModelPartHWHandle);
                        temp.Dispose();
                    }
                    else if (oriLocation.Y * resizerate / resRat + ((float)(findShapeModelPartPictureBox.Height - 1 + 0) / 2.0) > rowNum * resizerate - 1 && oriLocation.X * resizerate / resRat - ((float)(findShapeModelPartPictureBox.Width - 1 + 0) / 2.0) > 0 && oriLocation.X * resizerate / resRat + ((float)(findShapeModelPartPictureBox.Width - 1 + 0) / 2.0) < columnNum * resizerate - 1)
                    {
                        HObject temp;
                        basicClass.genRectangle1(out rectDomain, rowNum * resizerate - (findShapeModelPartPictureBox.Height), oriLocation.X * resizerate / resRat - ((float)(findShapeModelPartPictureBox.Width - 1 + 0) / 2.0), rowNum * resizerate - 1, oriLocation.X * resizerate / resRat + ((float)(findShapeModelPartPictureBox.Width - 1 + 0) / 2.0));
                        centerLocation.Y = (float)((rowNum * resizerate - ((float)(System.Math.Min(findShapeModelPartPictureBox.Height, rowNum * resizerate) - 1 + 0) / 2.0 + 1)) * resRat / resizerate);
                        centerLocation.X = (float)oriLocation.X;
                        basicClass.reduceDomain(imgPart, rectDomain, out temp);
                        if (imgPartTemp != null)
                        {
                            imgPartTemp.Dispose();
                        }
                        HOperatorSet.CropDomain(temp, out imgPartTemp);
                        HOperatorSet.DispObj(imgPartTemp, findShapeModelPartHWHandle);
                        temp.Dispose();
                    }
                    else if (oriLocation.Y * resizerate / resRat + ((float)(findShapeModelPartPictureBox.Height - 1 + 0) / 2.0) >= rowNum * resizerate - 1 && oriLocation.X * resizerate / resRat + ((float)(findShapeModelPartPictureBox.Width - 1 + 0) / 2.0) >= columnNum * resizerate - 1)
                    {
                        HObject temp;
                        basicClass.genRectangle1(out rectDomain, rowNum * resizerate - (findShapeModelPartPictureBox.Height), columnNum * resizerate - (findShapeModelPartPictureBox.Width), rowNum * resizerate - 1, columnNum * resizerate - 1);
                        centerLocation.Y = (float)((rowNum * resizerate - ((float)(System.Math.Min(findShapeModelPartPictureBox.Height, rowNum * resizerate) - 1 + 0) / 2.0 + 1)) * resRat / resizerate);
                        centerLocation.X = (float)((columnNum * resizerate - ((float)(System.Math.Min(findShapeModelPartPictureBox.Width, columnNum * resizerate) - 1 + 0) / 2.0 + 1)) * resRat / resizerate);
                        basicClass.reduceDomain(imgPart, rectDomain, out temp);
                        if (imgPartTemp != null)
                        {
                            imgPartTemp.Dispose();
                        }
                        HOperatorSet.CropDomain(temp, out imgPartTemp);
                        HOperatorSet.DispObj(imgPartTemp, findShapeModelPartHWHandle);
                        temp.Dispose();
                    }
                    else if (oriLocation.Y * resizerate / resRat + ((float)(findShapeModelPartPictureBox.Height - 1 + 0) / 2.0) < rowNum * resizerate - 1 && oriLocation.Y * resizerate / resRat - ((float)(findShapeModelPartPictureBox.Height - 1 + 0) / 2.0) > 0 && oriLocation.X * resizerate / resRat + ((float)(findShapeModelPartPictureBox.Width - 1 + 0) / 2.0) > columnNum * resizerate - 1)
                    {
                        HObject temp;
                        basicClass.genRectangle1(out rectDomain, oriLocation.Y * resizerate / resRat - ((float)(findShapeModelPartPictureBox.Height - 1 + 0) / 2.0), columnNum * resizerate - (findShapeModelPartPictureBox.Width), oriLocation.Y * resizerate / resRat + ((float)(findShapeModelPartPictureBox.Height - 1 + 0) / 2.0), columnNum * resizerate - 1);
                        centerLocation.Y = (float)oriLocation.Y;
                        centerLocation.X = (float)((columnNum * resizerate - ((float)(System.Math.Min(findShapeModelPartPictureBox.Width, columnNum * resizerate) - 1 + 0) / 2.0 + 1)) * resRat / resizerate);
                        basicClass.reduceDomain(imgPart, rectDomain, out temp);
                        if (imgPartTemp != null)
                        {
                            imgPartTemp.Dispose();
                        }
                        HOperatorSet.CropDomain(temp, out imgPartTemp);
                        HOperatorSet.DispObj(imgPartTemp, findShapeModelPartHWHandle);
                        temp.Dispose();
                    }
                    else if (oriLocation.Y * resizerate / resRat - ((float)(findShapeModelPartPictureBox.Height - 1 + 0) / 2.0) <= 0 && oriLocation.X * resizerate / resRat + ((float)(findShapeModelPartPictureBox.Width - 1 + 0) / 2.0) >= columnNum * resizerate - 1)
                    {
                        HObject temp;
                        basicClass.genRectangle1(out rectDomain, 0, columnNum * resizerate - (findShapeModelPartPictureBox.Width), (findShapeModelPartPictureBox.Height - 1), columnNum * resizerate - 1);
                        centerLocation.Y = (float)(((float)(System.Math.Min(findShapeModelPartPictureBox.Height, rowNum * resizerate) - 1 + 0) / 2.0) * resRat / resizerate);
                        centerLocation.X = (float)((columnNum * resizerate - ((float)(System.Math.Min(findShapeModelPartPictureBox.Width, columnNum * resizerate) - 1 + 0) / 2.0 + 1)) * resRat / resizerate);
                        basicClass.reduceDomain(imgPart, rectDomain, out temp);
                        if (imgPartTemp != null)
                        {
                            imgPartTemp.Dispose();
                        }
                        HOperatorSet.CropDomain(temp, out imgPartTemp);
                        HOperatorSet.DispObj(imgPartTemp, findShapeModelPartHWHandle);
                        temp.Dispose();
                    }
                    else
                    {
                        HObject temp;
                        basicClass.genRectangle1(out rectDomain, oriLocation.Y * resizerate / resRat - ((float)(findShapeModelPartPictureBox.Height - 1 + 0) / 2.0), oriLocation.X * resizerate / resRat - ((float)(findShapeModelPartPictureBox.Width - 1 + 0) / 2.0), oriLocation.Y * resizerate / resRat + ((float)(findShapeModelPartPictureBox.Height - 1 + 0) / 2.0), oriLocation.X * resizerate / resRat + ((float)(findShapeModelPartPictureBox.Width - 1 + 0) / 2.0));
                        basicClass.reduceDomain(imgPart, rectDomain, out temp);
                        if (imgPartTemp != null)
                        {
                            imgPartTemp.Dispose();
                        }
                        HOperatorSet.CropDomain(temp, out imgPartTemp);
                        HOperatorSet.DispObj(imgPartTemp, findShapeModelPartHWHandle);
                        temp.Dispose();
                    }
                    this.findShapeModelPictureBox.Focus();
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

        }
        private void findShapeModelPictureMouseMove(object sender, MouseEventArgs e)
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
                        if (oriLocation.Y * resizerate / resRat - ((float)(findShapeModelPartPictureBox.Height - 1 + 0) / 2.0) < 0 && oriLocation.X * resizerate / resRat - ((float)(findShapeModelPartPictureBox.Width - 1 + 0) / 2.0) < 0)
                        {
                            HObject temp;
                            basicClass.genRectangle1(out rectDomain, 0, 0, (findShapeModelPartPictureBox.Height - 1), (findShapeModelPartPictureBox.Width - 1));
                            centerLocation.Y = (float)(((float)(System.Math.Min(findShapeModelPartPictureBox.Height, rowNum * resizerate) - 1 + 0) / 2.0) * resRat / resizerate);
                            centerLocation.X = (float)(((float)(System.Math.Min(findShapeModelPartPictureBox.Width, columnNum * resizerate) - 1 + 0) / 2.0) * resRat / resizerate);

                            basicClass.reduceDomain(imgPart, rectDomain, out temp);
                            if (imgPartTemp != null)
                            {
                                imgPartTemp.Dispose();
                            }
                            HOperatorSet.CropDomain(temp, out imgPartTemp);
                            HOperatorSet.DispObj(imgPartTemp, findShapeModelPartHWHandle);
                            temp.Dispose();
                        }
                        else if (oriLocation.Y * resizerate / resRat - ((float)(findShapeModelPartPictureBox.Height - 1 + 0) / 2.0) <= 0 && oriLocation.X * resizerate / resRat - ((float)(findShapeModelPartPictureBox.Width - 1 + 0) / 2.0) >= 0 && oriLocation.X * resizerate / resRat + ((float)(findShapeModelPartPictureBox.Width - 1 + 0) / 2.0) < columnNum * resizerate - 1)
                        {
                            HObject temp;
                            basicClass.genRectangle1(out rectDomain, 0, oriLocation.X * resizerate / resRat - ((float)(findShapeModelPartPictureBox.Width - 1 + 0) / 2.0), (findShapeModelPartPictureBox.Height - 1), oriLocation.X * resizerate / resRat + ((float)(findShapeModelPartPictureBox.Width - 1 + 0) / 2.0));
                            centerLocation.Y = (float)(((float)(System.Math.Min(findShapeModelPartPictureBox.Height, rowNum * resizerate) - 1 + 0) / 2.0) * resRat / resizerate);
                            centerLocation.X = (float)oriLocation.X;
                            basicClass.reduceDomain(imgPart, rectDomain, out temp);
                            if (imgPartTemp != null)
                            {
                                imgPartTemp.Dispose();
                            }
                            HOperatorSet.CropDomain(temp, out imgPartTemp);
                            HOperatorSet.DispObj(imgPartTemp, findShapeModelPartHWHandle);
                            temp.Dispose();
                        }
                        else if (oriLocation.Y * resizerate / resRat - ((float)(findShapeModelPartPictureBox.Height - 1 + 0) / 2.0) > 0 && oriLocation.Y * resizerate / resRat + ((float)(findShapeModelPartPictureBox.Height - 1 + 0) / 2.0) <= rowNum * resizerate - 1 && oriLocation.X * resizerate / resRat - ((float)(findShapeModelPartPictureBox.Width - 1 + 0) / 2.0) < 0)
                        {
                            HObject temp;
                            basicClass.genRectangle1(out rectDomain, oriLocation.Y * resizerate / resRat - ((float)(findShapeModelPartPictureBox.Height - 1 + 0) / 2.0), 0, oriLocation.Y * resizerate / resRat + ((float)(findShapeModelPartPictureBox.Height - 1 + 0) / 2.0), (findShapeModelPartPictureBox.Width - 1));
                            centerLocation.Y = (float)oriLocation.Y;
                            centerLocation.X = (float)(((float)(System.Math.Min(findShapeModelPartPictureBox.Width, columnNum * resizerate) - 1 + 0) / 2.0) * resRat / resizerate);
                            basicClass.reduceDomain(imgPart, rectDomain, out temp);
                            if (imgPartTemp != null)
                            {
                                imgPartTemp.Dispose();
                            }
                            HOperatorSet.CropDomain(temp, out imgPartTemp);
                            HOperatorSet.DispObj(imgPartTemp, findShapeModelPartHWHandle);
                            temp.Dispose();
                        }
                        else if (oriLocation.Y * resizerate / resRat + ((float)(findShapeModelPartPictureBox.Height - 1 + 0) / 2.0) > rowNum * resizerate - 1 && oriLocation.X * resizerate / resRat - ((float)(findShapeModelPartPictureBox.Width - 1 + 0) / 2.0) < 0)
                        {
                            HObject temp;
                            basicClass.genRectangle1(out rectDomain, rowNum * resizerate - 1 - (findShapeModelPartPictureBox.Height - 1), 0, rowNum * resizerate - 1, (findShapeModelPartPictureBox.Width - 1));
                            centerLocation.Y = (float)((rowNum * resizerate - ((float)(System.Math.Min(findShapeModelPartPictureBox.Height, rowNum * resizerate) - 1 + 0) / 2.0) - 1) * resRat / resizerate);
                            centerLocation.X = (float)(((float)(System.Math.Min(findShapeModelPartPictureBox.Width, columnNum * resizerate) - 1 + 0) / 2.0) * resRat / resizerate);
                            basicClass.reduceDomain(imgPart, rectDomain, out temp);
                            if (imgPartTemp != null)
                            {
                                imgPartTemp.Dispose();
                            }
                            HOperatorSet.CropDomain(temp, out imgPartTemp);
                            HOperatorSet.DispObj(imgPartTemp, findShapeModelPartHWHandle);
                            temp.Dispose();
                        }
                        else if (oriLocation.Y * resizerate / resRat + ((float)(findShapeModelPartPictureBox.Height - 1 + 0) / 2.0) > rowNum * resizerate - 1 && oriLocation.X * resizerate / resRat - ((float)(findShapeModelPartPictureBox.Width - 1 + 0) / 2.0) > 0 && oriLocation.X * resizerate / resRat + ((float)(findShapeModelPartPictureBox.Width - 1 + 0) / 2.0) < columnNum * resizerate - 1)
                        {
                            HObject temp;
                            basicClass.genRectangle1(out rectDomain, rowNum * resizerate - (findShapeModelPartPictureBox.Height), oriLocation.X * resizerate / resRat - ((float)(findShapeModelPartPictureBox.Width - 1 + 0) / 2.0), rowNum * resizerate - 1, oriLocation.X * resizerate / resRat + ((float)(findShapeModelPartPictureBox.Width - 1 + 0) / 2.0));
                            centerLocation.Y = (float)((rowNum * resizerate - ((float)(System.Math.Min(findShapeModelPartPictureBox.Height, rowNum * resizerate) - 1 + 0) / 2.0 + 1)) * resRat / resizerate);
                            centerLocation.X = (float)oriLocation.X;
                            basicClass.reduceDomain(imgPart, rectDomain, out temp);
                            if (imgPartTemp != null)
                            {
                                imgPartTemp.Dispose();
                            }
                            HOperatorSet.CropDomain(temp, out imgPartTemp);
                            HOperatorSet.DispObj(imgPartTemp, findShapeModelPartHWHandle);
                            temp.Dispose();
                        }
                        else if (oriLocation.Y * resizerate / resRat + ((float)(findShapeModelPartPictureBox.Height - 1 + 0) / 2.0) >= rowNum * resizerate - 1 && oriLocation.X * resizerate / resRat + ((float)(findShapeModelPartPictureBox.Width - 1 + 0) / 2.0) >= columnNum * resizerate - 1)
                        {
                            HObject temp;
                            basicClass.genRectangle1(out rectDomain, rowNum * resizerate - (findShapeModelPartPictureBox.Height), columnNum * resizerate - (findShapeModelPartPictureBox.Width), rowNum * resizerate - 1, columnNum * resizerate - 1);
                            centerLocation.Y = (float)((rowNum * resizerate - ((float)(System.Math.Min(findShapeModelPartPictureBox.Height, rowNum * resizerate) - 1 + 0) / 2.0 + 1)) * resRat / resizerate);
                            centerLocation.X = (float)((columnNum * resizerate - ((float)(System.Math.Min(findShapeModelPartPictureBox.Width, columnNum * resizerate) - 1 + 0) / 2.0 + 1)) * resRat / resizerate);
                            basicClass.reduceDomain(imgPart, rectDomain, out temp);
                            if (imgPartTemp != null)
                            {
                                imgPartTemp.Dispose();
                            }
                            HOperatorSet.CropDomain(temp, out imgPartTemp);
                            HOperatorSet.DispObj(imgPartTemp, findShapeModelPartHWHandle);
                            temp.Dispose();
                        }
                        else if (oriLocation.Y * resizerate / resRat + ((float)(findShapeModelPartPictureBox.Height - 1 + 0) / 2.0) < rowNum * resizerate - 1 && oriLocation.Y * resizerate / resRat - ((float)(findShapeModelPartPictureBox.Height - 1 + 0) / 2.0) > 0 && oriLocation.X * resizerate / resRat + ((float)(findShapeModelPartPictureBox.Width - 1 + 0) / 2.0) > columnNum * resizerate - 1)
                        {
                            HObject temp;
                            basicClass.genRectangle1(out rectDomain, oriLocation.Y * resizerate / resRat - ((float)(findShapeModelPartPictureBox.Height - 1 + 0) / 2.0), columnNum * resizerate - (findShapeModelPartPictureBox.Width), oriLocation.Y * resizerate / resRat + ((float)(findShapeModelPartPictureBox.Height - 1 + 0) / 2.0), columnNum * resizerate - 1);
                            centerLocation.Y = (float)oriLocation.Y;
                            centerLocation.X = (float)((columnNum * resizerate - ((float)(System.Math.Min(findShapeModelPartPictureBox.Width, columnNum * resizerate) - 1 + 0) / 2.0 + 1)) * resRat / resizerate);
                            basicClass.reduceDomain(imgPart, rectDomain, out temp);
                            if (imgPartTemp != null)
                            {
                                imgPartTemp.Dispose();
                            }
                            HOperatorSet.CropDomain(temp, out imgPartTemp);
                            HOperatorSet.DispObj(imgPartTemp, findShapeModelPartHWHandle);
                            temp.Dispose();
                        }
                        else if (oriLocation.Y * resizerate / resRat - ((float)(findShapeModelPartPictureBox.Height - 1 + 0) / 2.0) <= 0 && oriLocation.X * resizerate / resRat + ((float)(findShapeModelPartPictureBox.Width - 1 + 0) / 2.0) >= columnNum * resizerate - 1)
                        {
                            HObject temp;
                            basicClass.genRectangle1(out rectDomain, 0, columnNum * resizerate - (findShapeModelPartPictureBox.Width), (findShapeModelPartPictureBox.Height - 1), columnNum * resizerate - 1);
                            centerLocation.Y = (float)(((float)(System.Math.Min(findShapeModelPartPictureBox.Height, rowNum * resizerate) - 1 + 0) / 2.0) * resRat / resizerate);
                            centerLocation.X = (float)((columnNum * resizerate - ((float)(System.Math.Min(findShapeModelPartPictureBox.Width, columnNum * resizerate) - 1 + 0) / 2.0 + 1)) * resRat / resizerate);
                            basicClass.reduceDomain(imgPart, rectDomain, out temp);
                            if (imgPartTemp != null)
                            {
                                imgPartTemp.Dispose();
                            }
                            HOperatorSet.CropDomain(temp, out imgPartTemp);
                            HOperatorSet.DispObj(imgPartTemp, findShapeModelPartHWHandle);
                            temp.Dispose();
                        }
                        else
                        {
                            HObject temp;
                            basicClass.genRectangle1(out rectDomain, oriLocation.Y * resizerate / resRat - ((float)(findShapeModelPartPictureBox.Height - 1 + 0) / 2.0), oriLocation.X * resizerate / resRat - ((float)(findShapeModelPartPictureBox.Width - 1 + 0) / 2.0), oriLocation.Y * resizerate / resRat + ((float)(findShapeModelPartPictureBox.Height - 1 + 0) / 2.0), oriLocation.X * resizerate / resRat + ((float)(findShapeModelPartPictureBox.Width - 1 + 0) / 2.0));
                            basicClass.reduceDomain(imgPart, rectDomain, out temp);
                            if (imgPartTemp != null)
                            {
                                imgPartTemp.Dispose();
                            }
                            HOperatorSet.CropDomain(temp, out imgPartTemp);
                            HOperatorSet.DispObj(imgPartTemp, findShapeModelPartHWHandle);
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

        private void buttonInspectShapeModel_Click(object sender, EventArgs e)
        {
            if (image != null && imageRealRegion != null)
            {
                try
                {
                    if (templateNumber >= MAX_MODEL_NUM)
                    {
                        MessageBox.Show("错误!最多可创建模版个数为" + MAX_MODEL_NUM.ToString() + "!");
                        return;
                    }
                    fSMC.setCreatAnisoShapeModelPara(cPara.numberLevel, cPara.angleStart, cPara.angleExtent, cPara.angleStep,cPara.scaleRMin,cPara.scaleRMax,cPara.scaleRStep,cPara.scaleCMin,cPara.scaleCMax,cPara.scaleCStep, cPara.optimization, cPara.metric, cPara.contrast, cPara.minContrast);
                    HObject imgtemp, imgtempR;
                    basicClass.reduceDomain(image, imageRealRegion, out imgtemp);
                    HTuple createShapeParaAuto;
                    //(['num_levels', 'angle_step', 'scale_step', 'optimization', 'contrast_low', 'contrast_high', 'min_size', 'min_contrast'
                    fSMC.determineShapeModelParameters(imgtemp, autoCreateShapeParametersArray, out createShapeParaAuto);
                    HTuple numberTemp, contrastTemp;
                    numberTemp = (HTuple)cPara.numberLevel;
                    contrastTemp = (HTuple)cPara.contrast[0];
                    contrastTemp = contrastTemp.TupleConcat(cPara.contrast[1]);
                    contrastTemp = contrastTemp.TupleConcat(cPara.contrast[2]);
                    if (autoCreateShapeParametersArray[0] == 1)
                    {

                        numericUpDownNumberLevel.Value = createShapeParaAuto[0].I;
                        numberTemp = (int)numericUpDownNumberLevel.Value;
                        cPara.numberLevel = (int)numericUpDownNumberLevel.Value;
                    }
                    if (autoCreateShapeParametersArray[1] == 1)
                    {
                        numericUpDownAngleStep.Value = (decimal)createShapeParaAuto[1].D;
                        cPara.angleStep = (double)numericUpDownAngleStep.Value;
                    }
                    if (autoCreateShapeParametersArray[3] == 1)
                    {
                        comboBoxOptimization1.SelectedIndex = (int)createShapeParaAuto[3].I;
                        cPara.optimization[0] = (int)createShapeParaAuto[3].I;
                    }
                    if (autoCreateShapeParametersArray[4] == 1 && autoCreateShapeParametersArray[5] == 1)
                    {
                        numericUpDownContrastLow.Value = numericUpDownContrastLow.Minimum;
                        numericUpDownContrastHigh.Value = (int)createShapeParaAuto[5].I;
                        numericUpDownContrastLow.Maximum = numericUpDownContrastHigh.Value - 1;
                        numericUpDownContrastLow.Value = (int)createShapeParaAuto[4].I;
                        contrastTemp[1] = (int)numericUpDownContrastHigh.Value;
                        contrastTemp[0] = (int)numericUpDownContrastLow.Value;
                        cPara.contrast[1] = (int)numericUpDownContrastHigh.Value;
                        cPara.contrast[0] = (int)numericUpDownContrastLow.Value;
                    }
                    if (autoCreateShapeParametersArray[6] == 1)
                    {
                        numericUpDownMinLength.Value = (int)createShapeParaAuto[6].I;
                        contrastTemp[2] = (int)createShapeParaAuto[6].I;
                        cPara.contrast[2] = (int)createShapeParaAuto[6].I;
                    }
                    if (autoCreateShapeParametersArray[7] == 1)
                    {
                        if ((int)createShapeParaAuto[7].I > (int)numericUpDownContrastLow.Value - 1)
                        {
                            createShapeParaAuto[7] = (int)numericUpDownContrastLow.Value - 1;

                        }
                        numericUpDownMinContrast.Value = numericUpDownMinContrast.Minimum;
                        numericUpDownMinContrast.Maximum = numericUpDownContrastLow.Value - 1;
                        numericUpDownMinContrast.Value = (int)createShapeParaAuto[7].I;

                        cPara.minContrast = (int)createShapeParaAuto[7].I;
                    }
                    fSMC.setCreatAnisoShapeModelPara(cPara.numberLevel, cPara.angleStart, cPara.angleExtent, cPara.angleStep, cPara.scaleRMin, cPara.scaleRMax, cPara.scaleRStep, cPara.scaleCMin, cPara.scaleCMax, cPara.scaleCStep, cPara.optimization, cPara.metric, cPara.contrast, cPara.minContrast);
                    fSMC.inspectShapeModel(imgtemp, numberTemp, contrastTemp, out imgtempR);
                    HTuple hv_grayval = new HTuple();
                    hv_grayval[0] = 255;
                    hv_grayval[1] = 0;
                    hv_grayval[2] = 0;
                    if (imageTempShow != null)
                    {
                        imageTempShow.Dispose();
                    }
                    HOperatorSet.PaintRegion(imgtempR, imageTemp, out imageTempShow, hv_grayval, "margin");
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
                    basicClass.displayhobject(img, findShapeModelHWHandle);
                    partImageWindowShow(oriLocation);
                    imgtemp.Dispose();
                }

                catch (System.Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("错误：未输入图像或未选择模版区域！");
            }

        }

        private void AutoNumLevelChanged(object sender, EventArgs e)
        {
            if (checkBoxAutoNumLevel.Checked == false)
            {
                numericUpDownNumberLevel.Enabled = true;
                autoCreateShapeParametersArray[0] = 0;
            }
            else
            {
                cPara.numberLevel = 0;
                autoCreateShapeParametersArray[0] = 1;
                numericUpDownNumberLevel.Enabled = false;
            }
        }

        private void AutoContrastChanged(object sender, EventArgs e)
        {
            if (checkBoxAutoContrast.Checked == false)
            {
                panelContarst.Enabled = true;
                autoCreateShapeParametersArray[4] = 0;
                autoCreateShapeParametersArray[5] = 0;
            }
            else
            {
                cPara.contrast[0] = 0;
                cPara.contrast[1] = 0;
                autoCreateShapeParametersArray[4] = 1;
                autoCreateShapeParametersArray[5] = 1;
                panelContarst.Enabled = false;
            }
        }

        private void AutoLengthChanged(object sender, EventArgs e)
        {
            if (checkBoxAutoLength.Checked == false)
            {
                numericUpDownMinLength.Enabled = true;
                autoCreateShapeParametersArray[6] = 0;
            }
            else
            {
                cPara.contrast[2] = 0;
                autoCreateShapeParametersArray[6] = 1;
                numericUpDownMinLength.Enabled = false;
            }
        }

        private void AutoAngleStepChanged(object sender, EventArgs e)
        {
            if (checkBoxAutoAngleStep.Checked == false)
            {
                numericUpDownAngleStep.Enabled = true;
                autoCreateShapeParametersArray[1] = 0;
            }
            else
            {
                cPara.angleStep = (double)0;
                autoCreateShapeParametersArray[1] = 1;
                numericUpDownAngleStep.Enabled = false;
            }
        }

        private void DetailChanged(object sender, EventArgs e)
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

        private void AngleStartChanged(object sender, EventArgs e)
        {
            cPara.angleStart = (double)numericUpDownAngleStart.Value;
        }

        private void AngleExtentChanged(object sender, EventArgs e)
        {
            cPara.angleExtent = (double)numericUpDownAngleExtent.Value;
        }
        private void NumberLevelChanged(object sender, EventArgs e)
        {
            cPara.numberLevel = (int)numericUpDownNumberLevel.Value;
        }

        private void ContrastLowChanged(object sender, EventArgs e)
        {
            if (numericUpDownContrastLow.Value > (numericUpDownContrastHigh.Value - 1))
            {
                numericUpDownContrastLow.Value = numericUpDownContrastHigh.Value - 1;
                MessageBox.Show("对比度区间（低）的值需小于对比度区间（高）");
            }
            cPara.contrast[0] = (int)numericUpDownContrastLow.Value;
            if (numericUpDownMinContrast.Value > (numericUpDownContrastLow.Value - 1))
            {
                numericUpDownMinContrast.Value = numericUpDownMinContrast.Minimum;
                numericUpDownMinContrast.Maximum = numericUpDownContrastLow.Value;
                if (numericUpDownContrastLow.Value == 1)
                {
                    numericUpDownMinContrast.Value = 0;
                }
                else
                {
                    numericUpDownMinContrast.Value = numericUpDownContrastLow.Value - 1;
                }

            }
            //numericUpDownContrastLow.Maximum = numericUpDownContrastHigh.Value - 1;
            //cPara.contrast[0] = (int)numericUpDownContrastLow.Value;
            //numericUpDownMinContrast.Maximum = numericUpDownContrastLow.Value;
        }

        private void ContrastHighChanged(object sender, EventArgs e)
        {
            if (numericUpDownContrastLow.Value > (numericUpDownContrastHigh.Value - 1))
            {
                numericUpDownContrastHigh.Value = numericUpDownContrastLow.Value + 1;
                MessageBox.Show("对比度区间（低）的值需小于对比度区间（高）");
            }
            cPara.contrast[1] = (int)numericUpDownContrastHigh.Value;
            //cPara.contrast[1] = (int)numericUpDownContrastHigh.Value;
            //if (numericUpDownContrastLow.Value > numericUpDownContrastHigh.Value - 1)
            //{
            //    numericUpDownContrastLow.Value = numericUpDownContrastHigh.Value - 1;
            //}
            //numericUpDownContrastLow.Maximum = numericUpDownContrastHigh.Value - 1;
        }

        private void MinLengthChanged(object sender, EventArgs e)
        {
            cPara.contrast[2] = (int)numericUpDownMinLength.Value;
        }

        private void AngleStepChanged(object sender, EventArgs e)
        {
            cPara.angleStep = (double)numericUpDownAngleStep.Value;
        }

        private void Optimization1Changed(object sender, EventArgs e)
        {
            cPara.optimization[0] = comboBoxOptimization1.SelectedIndex;
            if (cPara.optimization[0] == 0)
            {
                autoCreateShapeParametersArray[3] = 1;
            }
        }

        private void Optimization2Changed(object sender, EventArgs e)
        {
            cPara.optimization[1] = comboBoxOptimization2.SelectedIndex;
        }

        private void MetricChanged(object sender, EventArgs e)
        {
            cPara.metric = comboBoxMetric.SelectedIndex;
        }

        private void MinContrastChanged(object sender, EventArgs e)
        {
            numericUpDownMinContrast.Maximum = numericUpDownContrastLow.Value-1;
            cPara.minContrast = (int)numericUpDownMinContrast.Value;
            if (cPara.minContrast == 0)
            {
                autoCreateShapeParametersArray[7] = 1;
            }
            else
            {
                autoCreateShapeParametersArray[7] = 0;
            }
        }

        private void buttonPylon_Click(object sender, EventArgs e)
        {
            if (image != null)
            {
                tabControlFindShapeModel.Enabled = false;
                findShapeModelPictureBox.Enabled = false;
                buttonShapeSearchConfirm.Enabled = false;
                buttonShapeSearchCancel.Enabled = false;
                panelOKandCancel.Enabled = false;
                this.ControlBox = false;
                //this.MinimizeBox = false;
                //this.FormBorderStyle = FormBorderStyle.
                //this.FormBorderStyle = FormBorderStyle.FixedSingle;
                if (img != null)
                {
                    img.Dispose();
                }
                basicClass.resizeImage(imageTemp, out img, resRat);
                basicClass.displayhobject(img, findShapeModelHWHandle);
                if (imgPart != null)
                {
                    imgPart.Dispose();
                }
                basicClass.resizeImage(imageTemp, out imgPart, resizerate);
                partImageWindowShow(oriLocation);
                //findShapeModelPictureBoxMouseDown(object sender, MouseEventArgs e);

                HTuple regionArea;
                HOperatorSet.SetColor(findShapeModelPartHWHandle, "green");
                HOperatorSet.SetLineWidth(findShapeModelPartHWHandle, 4);
                basicClass.drawRegionMouseScreen(out imageDisplayPartRegion, findShapeModelPartHWHandle);
                HTuple hvHomMat2D;
                HOperatorSet.HomMat2dIdentity(out hvHomMat2D);
                //HOperatorSet.HomMat2dTranslate(hvHomMat2D, oriLocation.X / resRat + (-320 / resizerate), oriLocation.Y / resRat + (-240 / resizerate), out hvHomMat2D);
                HOperatorSet.VectorAngleToRigid(0, 0, 0, centerLocation.Y / resRat * resizerate + (-((float)(System.Math.Min(findShapeModelPartPictureBox.Height, rowNum * resizerate) - 1 + 0) / 2.0)), centerLocation.X / resRat * resizerate + (-((float)(System.Math.Min(findShapeModelPartPictureBox.Width, columnNum * resizerate) - 1 + 0) / 2.0)), 0, out hvHomMat2D);
                HOperatorSet.HomMat2dScale(hvHomMat2D, 1.0 / resizerate, 1.0 / resizerate, 0, 0, out hvHomMat2D);
                HOperatorSet.AffineTransRegion(imageDisplayPartRegion, out imageRealRegion, hvHomMat2D, "nearest_neighbor");
                //HObject imageContour;
                //HOperatorSet.GenContourRegionXld(imageRealRegion,out imageContour,"border")
                HTuple hv_grayval = new HTuple();
                hv_grayval[0] = 255;
                hv_grayval[1] = 0;
                hv_grayval[2] = 0;
                HObject regionLineh, regionLinev, regionCross;
                HOperatorSet.AreaCenter(imageRealRegion, out regionArea, out regionCenterRow, out regionCenterColumn);
                HOperatorSet.GenRectangle1(out regionLineh, regionCenterRow - 3, regionCenterColumn - 20, regionCenterRow + 3, regionCenterColumn + 20);
                HOperatorSet.GenRectangle1(out regionLinev, regionCenterRow - 20, regionCenterColumn - 3, regionCenterRow + 20, regionCenterColumn + 3);
                HOperatorSet.Union2(regionLineh, regionLinev, out regionCross);
                HObject hobimage, hobimage1, hobimage2, hobimage3, hobimage4;
                HOperatorSet.RegionToBin(imageRealRegion, out hobimage1, 0, 0, columnNum, rowNum);
                HOperatorSet.RegionToBin(imageRealRegion, out hobimage2, 255, 0, columnNum, rowNum);
                HOperatorSet.RegionToBin(imageRealRegion, out hobimage3, 0, 0, columnNum, rowNum);
                HOperatorSet.Compose3(hobimage1, hobimage2, hobimage3, out hobimage);

                HOperatorSet.ReduceDomain(imageTemp, imageRealRegion, out hobimage4);
                if (hobimage2 != null)
                {
                    hobimage2.Dispose();
                }
                HOperatorSet.AddImage(hobimage4, hobimage, out hobimage2, 0.5, 0);
                modelPoint.Y = (float)regionCenterRow[0].D;
                modelPoint.X = (float)regionCenterColumn[0].D;
                if (imageTempShowWithOutPoint != null)
                {
                    imageTempShowWithOutPoint.Dispose();
                }
                HOperatorSet.PaintGray(hobimage2, imageTemp, out imageTempShowWithOutPoint);
                if (imageTempShow != null)
                {
                    imageTempShow.Dispose();
                }
                HOperatorSet.PaintRegion(regionCross, imageTempShowWithOutPoint, out imageTempShow, hv_grayval, "fill");

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
                basicClass.displayhobject(img, findShapeModelHWHandle);
                partImageWindowShow(oriLocation);
                //HOperatorSet.DispObj(imgPart, findShapeModelPartHWHandle);
                findShapeModelPictureBox.Enabled = true;
                this.ControlBox = true;
                //this.MinimizeBox = true;
                //this.FormBorderStyle = FormBorderStyle.Sizable;
                imageDisplayPartRegion.Dispose();
                hobimage.Dispose();
                hobimage1.Dispose();
                hobimage2.Dispose();
                hobimage3.Dispose();
                numericUpDownPointX.Value = (decimal)regionCenterColumn[0].D;
                numericUpDownPointY.Value = (decimal)regionCenterRow[0].D;
                panelOKandCancel.Enabled = true;
                tabControlFindShapeModel.Enabled = true;
                buttonShapeSearchConfirm.Enabled = true;
                buttonShapeSearchCancel.Enabled = true;
            }
            else
            {
                MessageBox.Show("错误：未输入图像！");
            }
        }

        private void buttonCircle_Click(object sender, EventArgs e)
        {
            if (image != null)
            {
                tabControlFindShapeModel.Enabled = false;
                buttonShapeSearchConfirm.Enabled = false;
                buttonShapeSearchCancel.Enabled = false;
                panelOKandCancel.Enabled = false;
                double circleRow, circleColumn, circleRadius;
                findShapeModelPictureBox.Enabled = false;
                this.ControlBox = false;
                if (img != null)
                {
                    img.Dispose();
                }
                basicClass.resizeImage(imageTemp, out img, resRat);
                basicClass.displayhobject(img, findShapeModelHWHandle);
                if (imgPart != null)
                {
                    imgPart.Dispose();
                }
                basicClass.resizeImage(imageTemp, out imgPart, resizerate);
                partImageWindowShow(oriLocation);
                HTuple regionArea;
                HOperatorSet.SetColor(findShapeModelPartHWHandle, "green");
                HOperatorSet.SetLineWidth(findShapeModelPartHWHandle, 4);
                basicClass.drawCircleMouse(findShapeModelPartHWHandle, out circleRow, out circleColumn, out circleRadius);
                basicClass.genCircle(out imageDisplayPartRegion, circleRow, circleColumn, circleRadius);
                HTuple hvHomMat2D;
                HOperatorSet.HomMat2dIdentity(out hvHomMat2D);
                HOperatorSet.VectorAngleToRigid(0, 0, 0, centerLocation.Y / resRat * resizerate + (-((float)(System.Math.Min(findShapeModelPartPictureBox.Height, rowNum * resizerate) - 1 + 0) / 2.0)), centerLocation.X / resRat * resizerate + (-((float)(System.Math.Min(findShapeModelPartPictureBox.Width, columnNum * resizerate) - 1 + 0) / 2.0)), 0, out hvHomMat2D);
                HOperatorSet.HomMat2dScale(hvHomMat2D, 1.0 / resizerate, 1.0 / resizerate, 0, 0, out hvHomMat2D);
                HOperatorSet.AffineTransRegion(imageDisplayPartRegion, out imageRealRegion, hvHomMat2D, "nearest_neighbor");
                HTuple hv_grayval = new HTuple();
                hv_grayval[0] = 255;
                hv_grayval[1] = 0;
                hv_grayval[2] = 0;
                HObject regionLineh, regionLinev, regionCross;
                HOperatorSet.AreaCenter(imageRealRegion, out regionArea, out regionCenterRow, out regionCenterColumn);
                HOperatorSet.GenRectangle1(out regionLineh, regionCenterRow - 3, regionCenterColumn - 20, regionCenterRow + 3, regionCenterColumn + 20);
                HOperatorSet.GenRectangle1(out regionLinev, regionCenterRow - 20, regionCenterColumn - 3, regionCenterRow + 20, regionCenterColumn + 3);
                HOperatorSet.Union2(regionLineh, regionLinev, out regionCross);
                HObject hobimage, hobimage1, hobimage2, hobimage3, hobimage4;
                HOperatorSet.RegionToBin(imageRealRegion, out hobimage1, 0, 0, columnNum, rowNum);
                HOperatorSet.RegionToBin(imageRealRegion, out hobimage2, 255, 0, columnNum, rowNum);
                HOperatorSet.RegionToBin(imageRealRegion, out hobimage3, 0, 0, columnNum, rowNum);
                HOperatorSet.Compose3(hobimage1, hobimage2, hobimage3, out hobimage);

                HOperatorSet.ReduceDomain(imageTemp, imageRealRegion, out hobimage4);
                if (hobimage2 != null)
                {
                    hobimage2.Dispose();
                }
                HOperatorSet.AddImage(hobimage4, hobimage, out hobimage2, 0.5, 0);
                modelPoint.Y = (float)regionCenterRow[0].D;
                modelPoint.X = (float)regionCenterColumn[0].D;
                if (imageTempShowWithOutPoint != null)
                {
                    imageTempShowWithOutPoint.Dispose();
                }
                HOperatorSet.PaintGray(hobimage2, imageTemp, out imageTempShowWithOutPoint);
                //HOperatorSet.BitOr(hobimage, imageTemp, out imageTempShowWithOutPoint);
                //HOperatorSet.AddImage(hobimage, imageTemp, out imageTempShowWithOutPoint, 0.5, 0);
                if (imageTempShow != null)
                {
                    imageTempShow.Dispose();
                }
                HOperatorSet.PaintRegion(regionCross, imageTempShowWithOutPoint, out imageTempShow, hv_grayval, "fill");
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
                basicClass.displayhobject(img, findShapeModelHWHandle);
                partImageWindowShow(oriLocation);
                findShapeModelPictureBox.Enabled = true;
                this.ControlBox = true;
                imageDisplayPartRegion.Dispose();
                hobimage.Dispose();
                hobimage1.Dispose();
                hobimage2.Dispose();
                hobimage3.Dispose();
                hobimage4.Dispose();
                numericUpDownPointX.Value = (decimal)regionCenterColumn[0].D;
                numericUpDownPointY.Value = (decimal)regionCenterRow[0].D;
                panelOKandCancel.Enabled = true;
                tabControlFindShapeModel.Enabled = true;
                buttonShapeSearchConfirm.Enabled = true;
                buttonShapeSearchCancel.Enabled = true;
            }
            else
            {
                MessageBox.Show("错误：未输入图像！");
            }
        }

        private void buttonEllipse_Click(object sender, EventArgs e)
        {
            if (image != null)
            {
                tabControlFindShapeModel.Enabled = false;
                buttonShapeSearchConfirm.Enabled = false;
                buttonShapeSearchCancel.Enabled = false;
                panelOKandCancel.Enabled = false;
                double ellipseRow, ellipseColumn, ellipsephi, ellipseRadius1, ellipseRadius2;
                findShapeModelPictureBox.Enabled = false;
                this.ControlBox = false;
                if (img != null)
                {
                    img.Dispose();
                }
                basicClass.resizeImage(imageTemp, out img, resRat);
                basicClass.displayhobject(img, findShapeModelHWHandle);
                if (imgPart != null)
                {
                    imgPart.Dispose();
                }
                basicClass.resizeImage(imageTemp, out imgPart, resizerate);
                partImageWindowShow(oriLocation);
                HOperatorSet.SetColor(findShapeModelPartHWHandle, "green");
                HOperatorSet.SetLineWidth(findShapeModelPartHWHandle, 4);
                basicClass.drawEllipseMouse(findShapeModelPartHWHandle, out ellipseRow, out ellipseColumn, out ellipsephi, out ellipseRadius1, out ellipseRadius2);
                basicClass.genEllipse(out imageDisplayPartRegion, ellipseRow, ellipseColumn, ellipsephi, ellipseRadius1, ellipseRadius2);
                HTuple hvHomMat2D;
                HOperatorSet.HomMat2dIdentity(out hvHomMat2D);
                HTuple regionArea;
                HOperatorSet.VectorAngleToRigid(0, 0, 0, centerLocation.Y / resRat * resizerate + (-((float)(System.Math.Min(findShapeModelPartPictureBox.Height, rowNum * resizerate) - 1 + 0) / 2.0)), centerLocation.X / resRat * resizerate + (-((float)(System.Math.Min(findShapeModelPartPictureBox.Width, columnNum * resizerate) - 1 + 0) / 2.0)), 0, out hvHomMat2D);
                HOperatorSet.HomMat2dScale(hvHomMat2D, 1.0 / resizerate, 1.0 / resizerate, 0, 0, out hvHomMat2D);
                HOperatorSet.AffineTransRegion(imageDisplayPartRegion, out imageRealRegion, hvHomMat2D, "nearest_neighbor");
                HTuple hv_grayval = new HTuple();
                hv_grayval[0] = 255;
                hv_grayval[1] = 0;
                hv_grayval[2] = 0;
                HObject regionLineh, regionLinev, regionCross;
                HOperatorSet.AreaCenter(imageRealRegion, out regionArea, out regionCenterRow, out regionCenterColumn);
                HOperatorSet.GenRectangle1(out regionLineh, regionCenterRow - 3, regionCenterColumn - 20, regionCenterRow + 3, regionCenterColumn + 20);
                HOperatorSet.GenRectangle1(out regionLinev, regionCenterRow - 20, regionCenterColumn - 3, regionCenterRow + 20, regionCenterColumn + 3);
                HOperatorSet.Union2(regionLineh, regionLinev, out regionCross);
                HObject hobimage, hobimage1, hobimage2, hobimage3, hobimage4;
                HOperatorSet.RegionToBin(imageRealRegion, out hobimage1, 0, 0, columnNum, rowNum);
                HOperatorSet.RegionToBin(imageRealRegion, out hobimage2, 255, 0, columnNum, rowNum);
                HOperatorSet.RegionToBin(imageRealRegion, out hobimage3, 0, 0, columnNum, rowNum);
                HOperatorSet.Compose3(hobimage1, hobimage2, hobimage3, out hobimage);

                HOperatorSet.ReduceDomain(imageTemp, imageRealRegion, out hobimage4);
                if (hobimage2 != null)
                {
                    hobimage2.Dispose();
                }
                HOperatorSet.AddImage(hobimage4, hobimage, out hobimage2, 0.5, 0);
                modelPoint.Y = (float)regionCenterRow[0].D;
                modelPoint.X = (float)regionCenterColumn[0].D;
                if (imageTempShowWithOutPoint != null)
                {
                    imageTempShowWithOutPoint.Dispose();
                }
                HOperatorSet.PaintGray(hobimage2, imageTemp, out imageTempShowWithOutPoint);
                if (imageTempShow != null)
                {
                    imageTempShow.Dispose();
                }
                HOperatorSet.PaintRegion(regionCross, imageTempShowWithOutPoint, out imageTempShow, hv_grayval, "fill");
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
                basicClass.displayhobject(img, findShapeModelHWHandle);
                partImageWindowShow(oriLocation);
                findShapeModelPictureBox.Enabled = true;
                this.ControlBox = true;
                imageDisplayPartRegion.Dispose();
                hobimage.Dispose();
                hobimage1.Dispose();
                hobimage2.Dispose();
                hobimage3.Dispose();
                numericUpDownPointX.Value = (decimal)regionCenterColumn[0].D;
                numericUpDownPointY.Value = (decimal)regionCenterRow[0].D;
                panelOKandCancel.Enabled = true;
                tabControlFindShapeModel.Enabled = true;
                buttonShapeSearchConfirm.Enabled = true;
                buttonShapeSearchCancel.Enabled = true;
            }
            else
            {
                MessageBox.Show("错误：未输入图像！");
            }
        }

        private void buttonRectangle_Click(object sender, EventArgs e)
        {
            if (image != null)
            {
                tabControlFindShapeModel.Enabled = false;
                buttonShapeSearchConfirm.Enabled = false;
                buttonShapeSearchCancel.Enabled = false;
                panelOKandCancel.Enabled = false;
                double rectangleRow, rectangleColumn, rectanglephi, rectangleLength1, rectangleLength2;
                findShapeModelPictureBox.Enabled = false;
                this.ControlBox = false;
                if (img != null)
                {
                    img.Dispose();
                }
                basicClass.resizeImage(imageTemp, out img, resRat);
                basicClass.displayhobject(img, findShapeModelHWHandle);
                if (imgPart != null)
                {
                    imgPart.Dispose();
                }
                basicClass.resizeImage(imageTemp, out imgPart, resizerate);
                partImageWindowShow(oriLocation);
                HOperatorSet.SetColor(findShapeModelPartHWHandle, "green");
                HOperatorSet.SetLineWidth(findShapeModelPartHWHandle, 4);
                basicClass.drawRectangle2Mouse(findShapeModelPartHWHandle, out rectangleRow, out rectangleColumn, out rectanglephi, out rectangleLength1, out rectangleLength2);
                basicClass.genRectangle2(out imageDisplayPartRegion, rectangleRow, rectangleColumn, rectanglephi, rectangleLength1, rectangleLength2);
                HTuple hvHomMat2D;
                HOperatorSet.HomMat2dIdentity(out hvHomMat2D);
                HTuple regionArea;
                HOperatorSet.VectorAngleToRigid(0, 0, 0, centerLocation.Y / resRat * resizerate + (-((float)(System.Math.Min(findShapeModelPartPictureBox.Height, rowNum * resizerate) - 1 + 0) / 2.0)), centerLocation.X / resRat * resizerate + (-((float)(System.Math.Min(findShapeModelPartPictureBox.Width, columnNum * resizerate) - 1 + 0) / 2.0)), 0, out hvHomMat2D);
                HOperatorSet.HomMat2dScale(hvHomMat2D, 1.0 / resizerate, 1.0 / resizerate, 0, 0, out hvHomMat2D);
                HOperatorSet.AffineTransRegion(imageDisplayPartRegion, out imageRealRegion, hvHomMat2D, "nearest_neighbor");
                HTuple hv_grayval = new HTuple();
                hv_grayval[0] = 255;
                hv_grayval[1] = 0;
                hv_grayval[2] = 0;
                HObject regionLineh, regionLinev, regionCross;
                HOperatorSet.AreaCenter(imageRealRegion, out regionArea, out regionCenterRow, out regionCenterColumn);
                HOperatorSet.GenRectangle1(out regionLineh, regionCenterRow - 3, regionCenterColumn - 20, regionCenterRow + 3, regionCenterColumn + 20);
                HOperatorSet.GenRectangle1(out regionLinev, regionCenterRow - 20, regionCenterColumn - 3, regionCenterRow + 20, regionCenterColumn + 3);
                HOperatorSet.Union2(regionLineh, regionLinev, out regionCross);
                HObject hobimage, hobimage1, hobimage2, hobimage3, hobimage4;
                HOperatorSet.RegionToBin(imageRealRegion, out hobimage1, 0, 0, columnNum, rowNum);
                HOperatorSet.RegionToBin(imageRealRegion, out hobimage2, 255, 0, columnNum, rowNum);
                HOperatorSet.RegionToBin(imageRealRegion, out hobimage3, 0, 0, columnNum, rowNum);
                HOperatorSet.Compose3(hobimage1, hobimage2, hobimage3, out hobimage);

                HOperatorSet.ReduceDomain(imageTemp, imageRealRegion, out hobimage4);
                if (hobimage2 != null)
                {
                    hobimage2.Dispose();
                }
                HOperatorSet.AddImage(hobimage4, hobimage, out hobimage2, 0.5, 0);
                modelPoint.Y = (float)regionCenterRow[0].D;
                modelPoint.X = (float)regionCenterColumn[0].D;
                if (imageTempShowWithOutPoint != null)
                {
                    imageTempShowWithOutPoint.Dispose();
                }
                HOperatorSet.PaintGray(hobimage2, imageTemp, out imageTempShowWithOutPoint);
                if (imageTempShow != null)
                {
                    imageTempShow.Dispose();
                }
                HOperatorSet.PaintRegion(regionCross, imageTempShowWithOutPoint, out imageTempShow, hv_grayval, "fill");
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
                basicClass.displayhobject(img, findShapeModelHWHandle);
                partImageWindowShow(oriLocation);
                findShapeModelPictureBox.Enabled = true;
                this.ControlBox = true;
                imageDisplayPartRegion.Dispose();
                hobimage.Dispose();
                hobimage1.Dispose();
                hobimage2.Dispose();
                hobimage3.Dispose();
                numericUpDownPointX.Value = (decimal)regionCenterColumn[0].D;
                numericUpDownPointY.Value = (decimal)regionCenterRow[0].D;
                panelOKandCancel.Enabled = true;
                tabControlFindShapeModel.Enabled = true;
                buttonShapeSearchConfirm.Enabled = true;
                buttonShapeSearchCancel.Enabled = true;
            }
            else
            {
                MessageBox.Show("错误：未输入图像！");
            }
        }

        private void buttonAddROI_Click(object sender, EventArgs e)
        {
            if (image != null && imageRealRegion != null)
            {
                tabControlFindShapeModel.Enabled = false;
                buttonShapeSearchConfirm.Enabled = false;
                buttonShapeSearchCancel.Enabled = false;
                panelOKandCancel.Enabled = false;
                HObject imageRealTempRegion;
                findShapeModelPictureBox.Enabled = false;
                this.ControlBox = false;
                HTuple regionArea;
                // this.FormBorderStyle = FormBorderStyle.FixedSingle;
                HOperatorSet.SetColor(findShapeModelPartHWHandle, "green");
                HOperatorSet.SetLineWidth(findShapeModelPartHWHandle, 4);
                basicClass.drawRegionMouseScreen(out imageDisplayPartRegion, findShapeModelPartHWHandle);
                HTuple hvHomMat2D;
                HOperatorSet.HomMat2dIdentity(out hvHomMat2D);
                HOperatorSet.VectorAngleToRigid(0, 0, 0, centerLocation.Y / resRat * resizerate + (-((float)(System.Math.Min(findShapeModelPartPictureBox.Height, rowNum * resizerate) - 1 + 0) / 2.0)), centerLocation.X / resRat * resizerate + (-((float)(System.Math.Min(findShapeModelPartPictureBox.Width, columnNum * resizerate) - 1 + 0) / 2.0)), 0, out hvHomMat2D);
                HOperatorSet.HomMat2dScale(hvHomMat2D, 1.0 / resizerate, 1.0 / resizerate, 0, 0, out hvHomMat2D);
                HOperatorSet.AffineTransRegion(imageDisplayPartRegion, out imageRealTempRegion, hvHomMat2D, "nearest_neighbor");
                HOperatorSet.Union2(imageRealTempRegion, imageRealRegion, out imageRealRegion);
                HTuple hv_grayval = new HTuple();
                hv_grayval[0] = 255;
                hv_grayval[1] = 0;
                hv_grayval[2] = 0;
                HObject regionLineh, regionLinev, regionCross;
                HOperatorSet.AreaCenter(imageRealRegion, out regionArea, out regionCenterRow, out regionCenterColumn);
                HOperatorSet.GenRectangle1(out regionLineh, regionCenterRow - 3, regionCenterColumn - 20, regionCenterRow + 3, regionCenterColumn + 20);
                HOperatorSet.GenRectangle1(out regionLinev, regionCenterRow - 20, regionCenterColumn - 3, regionCenterRow + 20, regionCenterColumn + 3);
                HOperatorSet.Union2(regionLineh, regionLinev, out regionCross);
                HObject hobimage, hobimage1, hobimage2, hobimage3, hobimage4;
                HOperatorSet.RegionToBin(imageRealRegion, out hobimage1, 0, 0, columnNum, rowNum);
                HOperatorSet.RegionToBin(imageRealRegion, out hobimage2, 255, 0, columnNum, rowNum);
                HOperatorSet.RegionToBin(imageRealRegion, out hobimage3, 0, 0, columnNum, rowNum);
                HOperatorSet.Compose3(hobimage1, hobimage2, hobimage3, out hobimage);

                HOperatorSet.ReduceDomain(imageTemp, imageRealRegion, out hobimage4);
                if (hobimage2 != null)
                {
                    hobimage2.Dispose();
                }
                HOperatorSet.AddImage(hobimage4, hobimage, out hobimage2, 0.5, 0);
                modelPoint.Y = (float)regionCenterRow[0].D;
                modelPoint.X = (float)regionCenterColumn[0].D;
                if (imageTempShowWithOutPoint != null)
                {
                    imageTempShowWithOutPoint.Dispose();
                }
                HOperatorSet.PaintGray(hobimage2, imageTemp, out imageTempShowWithOutPoint);
                if (imageTempShow != null)
                {
                    imageTempShow.Dispose();
                }
                HOperatorSet.PaintRegion(regionCross, imageTempShowWithOutPoint, out imageTempShow, hv_grayval, "fill");
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
                basicClass.displayhobject(img, findShapeModelHWHandle);
                partImageWindowShow(oriLocation);
                findShapeModelPictureBox.Enabled = true;
                this.ControlBox = true;
                imageRealTempRegion.Dispose();
                hobimage.Dispose();
                hobimage1.Dispose();
                hobimage2.Dispose();
                hobimage3.Dispose();
                numericUpDownPointX.Value = (decimal)regionCenterColumn[0].D;
                numericUpDownPointY.Value = (decimal)regionCenterRow[0].D;
                panelOKandCancel.Enabled = true;
                tabControlFindShapeModel.Enabled = true;
                buttonShapeSearchConfirm.Enabled = true;
                buttonShapeSearchCancel.Enabled = true;
            }
            else
            {
                MessageBox.Show("错误：未输入图像或未选择模版区域！");
            }
        }

        private void buttonCropROI_Click(object sender, EventArgs e)
        {
            if (image != null && imageRealRegion != null)
            {
                tabControlFindShapeModel.Enabled = false;
                buttonShapeSearchConfirm.Enabled = false;
                buttonShapeSearchCancel.Enabled = false;
                panelOKandCancel.Enabled = false;
                HObject imageRealTempRegion;
                findShapeModelPictureBox.Enabled = false;
                this.ControlBox = false;
                HOperatorSet.SetColor(findShapeModelPartHWHandle, "green");
                HOperatorSet.SetLineWidth(findShapeModelPartHWHandle, 4);
                basicClass.drawRegionMouseScreen(out imageDisplayPartRegion, findShapeModelPartHWHandle);
                HTuple hvHomMat2D;
                HOperatorSet.HomMat2dIdentity(out hvHomMat2D);
                HTuple regionArea;
                HOperatorSet.VectorAngleToRigid(0, 0, 0, centerLocation.Y / resRat * resizerate + (-((float)(System.Math.Min(findShapeModelPartPictureBox.Height, rowNum * resizerate) - 1 + 0) / 2.0)), centerLocation.X / resRat * resizerate + (-((float)(System.Math.Min(findShapeModelPartPictureBox.Width, columnNum * resizerate) - 1 + 0) / 2.0)), 0, out hvHomMat2D);
                HOperatorSet.HomMat2dScale(hvHomMat2D, 1.0 / resizerate, 1.0 / resizerate, 0, 0, out hvHomMat2D);
                HOperatorSet.AffineTransRegion(imageDisplayPartRegion, out imageRealTempRegion, hvHomMat2D, "nearest_neighbor");
                HOperatorSet.Difference(imageRealRegion, imageRealTempRegion, out imageRealRegion);
                HTuple hv_grayval = new HTuple();
                hv_grayval[0] = 255;
                hv_grayval[1] = 0;
                hv_grayval[2] = 0;
                HObject regionLineh, regionLinev, regionCross;
                HOperatorSet.AreaCenter(imageRealRegion, out regionArea, out regionCenterRow, out regionCenterColumn);
                HOperatorSet.GenRectangle1(out regionLineh, regionCenterRow - 3, regionCenterColumn - 20, regionCenterRow + 3, regionCenterColumn + 20);
                HOperatorSet.GenRectangle1(out regionLinev, regionCenterRow - 20, regionCenterColumn - 3, regionCenterRow + 20, regionCenterColumn + 3);
                HOperatorSet.Union2(regionLineh, regionLinev, out regionCross);
                HObject hobimage, hobimage1, hobimage2, hobimage3, hobimage4;
                HOperatorSet.RegionToBin(imageRealRegion, out hobimage1, 0, 0, columnNum, rowNum);
                HOperatorSet.RegionToBin(imageRealRegion, out hobimage2, 255, 0, columnNum, rowNum);
                HOperatorSet.RegionToBin(imageRealRegion, out hobimage3, 0, 0, columnNum, rowNum);
                HOperatorSet.Compose3(hobimage1, hobimage2, hobimage3, out hobimage);

                HOperatorSet.ReduceDomain(imageTemp, imageRealRegion, out hobimage4);
                if (hobimage2 != null)
                {
                    hobimage2.Dispose();
                }
                HOperatorSet.AddImage(hobimage4, hobimage, out hobimage2, 0.5, 0);
                modelPoint.Y = (float)regionCenterRow[0].D;
                modelPoint.X = (float)regionCenterColumn[0].D;
                if (imageTempShowWithOutPoint != null)
                {
                    imageTempShowWithOutPoint.Dispose();
                }
                HOperatorSet.PaintGray(hobimage2, imageTemp, out imageTempShowWithOutPoint);
                if (imageTempShow != null)
                {
                    imageTempShow.Dispose();
                }
                HOperatorSet.PaintRegion(regionCross, imageTempShowWithOutPoint, out imageTempShow, hv_grayval, "fill");
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
                basicClass.displayhobject(img, findShapeModelHWHandle);
                partImageWindowShow(oriLocation);
                findShapeModelPictureBox.Enabled = true;
                this.ControlBox = true;
                imageRealTempRegion.Dispose();
                hobimage.Dispose();
                hobimage1.Dispose();
                hobimage2.Dispose();
                hobimage3.Dispose();
                numericUpDownPointX.Value = (decimal)regionCenterColumn[0].D;
                numericUpDownPointY.Value = (decimal)regionCenterRow[0].D;
                panelOKandCancel.Enabled = true;
                tabControlFindShapeModel.Enabled = true;
                buttonShapeSearchConfirm.Enabled = true;
                buttonShapeSearchCancel.Enabled = true;
            }
            else
            {
                MessageBox.Show("错误：未输入图像或未选择模版区域！");
            }
        }

        private void ButtonCreateModel_Click(object sender, EventArgs e)
        {
            if (image != null && imageRealRegion != null)
            {
                Cursor = Cursors.WaitCursor;
                try
                {
                    if (templateNumber >= MAX_MODEL_NUM)
                    {
                        Cursor = Cursors.Default;
                        MessageBox.Show("错误!最多可创建模版个数为" + MAX_MODEL_NUM.ToString() + "!");
                        return;
                    }
                    HTuple tModelID;

                    fSMC.setCreatAnisoShapeModelPara(cPara.numberLevel, cPara.angleStart, cPara.angleExtent, cPara.angleStep, cPara.scaleRMin, cPara.scaleRMax, cPara.scaleRStep, cPara.scaleCMin, cPara.scaleCMax, cPara.scaleCStep, cPara.optimization, cPara.metric, cPara.contrast, cPara.minContrast);
                    HObject imgtemp, imgtempR;
                    basicClass.reduceDomain(image, imageRealRegion, out imgtemp);
                    HTuple createShapeParaAuto;
                    //(['num_levels', 'angle_step', 'scale_step', 'optimization', 'contrast_low', 'contrast_high', 'min_size', 'min_contrast'
                    fSMC.determineShapeModelParameters(imgtemp, autoCreateShapeParametersArray, out createShapeParaAuto);
                    HTuple numberTemp, contrastTemp;
                    numberTemp = (HTuple)cPara.numberLevel;
                    contrastTemp = (HTuple)cPara.contrast[0];
                    contrastTemp = contrastTemp.TupleConcat(cPara.contrast[1]);
                    contrastTemp = contrastTemp.TupleConcat(cPara.contrast[2]);
                    if (autoCreateShapeParametersArray[0] == 1)
                    {

                        numericUpDownNumberLevel.Value = createShapeParaAuto[0].I;
                        numberTemp = (int)numericUpDownNumberLevel.Value;
                        cPara.numberLevel = (int)numericUpDownNumberLevel.Value;
                    }
                    if (autoCreateShapeParametersArray[1] == 1)
                    {
                        numericUpDownAngleStep.Value = (decimal)createShapeParaAuto[1].D;
                        cPara.angleStep = (double)numericUpDownAngleStep.Value;
                    }
                    if (autoCreateShapeParametersArray[3] == 1)
                    {
                        comboBoxOptimization1.SelectedIndex = (int)createShapeParaAuto[3].I;
                        cPara.optimization[0] = (int)createShapeParaAuto[3].I;
                    }
                    if (autoCreateShapeParametersArray[4] == 1 && autoCreateShapeParametersArray[5] == 1)
                    {

                        numericUpDownContrastLow.Value = numericUpDownContrastLow.Minimum;
                        numericUpDownContrastHigh.Value = (int)createShapeParaAuto[5].I;
                        numericUpDownContrastLow.Maximum = numericUpDownContrastHigh.Value - 1;
                        numericUpDownContrastLow.Value = (int)createShapeParaAuto[4].I;
                        contrastTemp[1] = (int)numericUpDownContrastHigh.Value;
                        contrastTemp[0] = (int)numericUpDownContrastLow.Value;
                        cPara.contrast[1] = (int)numericUpDownContrastHigh.Value;
                        cPara.contrast[0] = (int)numericUpDownContrastLow.Value;

                    }
                    if (autoCreateShapeParametersArray[6] == 1)
                    {
                        numericUpDownMinLength.Value = (int)createShapeParaAuto[6].I;
                        contrastTemp[2] = (int)createShapeParaAuto[6].I;
                        cPara.contrast[2] = (int)createShapeParaAuto[6].I;
                    }
                    if (autoCreateShapeParametersArray[7] == 1)
                    {
                        if ((int)createShapeParaAuto[7].I > (int)numericUpDownContrastLow.Value - 1)
                        {
                            createShapeParaAuto[7] = (int)numericUpDownContrastLow.Value - 1;

                        }
                        numericUpDownMinContrast.Value = numericUpDownMinContrast.Minimum;
                        numericUpDownMinContrast.Maximum = numericUpDownContrastLow.Value - 1;
                        numericUpDownMinContrast.Value = (int)createShapeParaAuto[7].I;

                        cPara.minContrast = (int)createShapeParaAuto[7].I;
                    }
                    fSMC.setCreatAnisoShapeModelPara(cPara.numberLevel, cPara.angleStart, cPara.angleExtent, cPara.angleStep, cPara.scaleRMin, cPara.scaleRMax, cPara.scaleRStep, cPara.scaleCMin, cPara.scaleCMax, cPara.scaleCStep, cPara.optimization, cPara.metric, cPara.contrast, cPara.minContrast);
                    fSMC.inspectShapeModel(imgtemp, numberTemp, contrastTemp, out imgtempR);
                    fSMC.createAnisoShapeModel(imgtemp, out tModelID);
                    HTuple ra, rcr, rcc;
                    HOperatorSet.AreaCenter(imageRealRegion, out ra, out rcr, out rcc);
                    HOperatorSet.SetShapeModelOrigin(tModelID, ((float)numericUpDownPointY.Value - rcr), ((float)numericUpDownPointX.Value - rcc));
                    HTuple hv_grayval = new HTuple();
                    hv_grayval[0] = 255;
                    hv_grayval[1] = 0;
                    hv_grayval[2] = 0;
                    HObject regionLineh, regionLinev, regionCross, temp;
                    HOperatorSet.GenRectangle1(out regionLineh, regionCenterRow - 3, regionCenterColumn - 20, regionCenterRow + 3, regionCenterColumn + 20);
                    HOperatorSet.GenRectangle1(out regionLinev, regionCenterRow - 20, regionCenterColumn - 3, regionCenterRow + 20, regionCenterColumn + 3);
                    HOperatorSet.Union2(regionLineh, regionLinev, out regionCross);
                    HOperatorSet.PaintRegion(regionCross, imageTempShowWithOutPoint, out temp, hv_grayval, "fill");
                    if (imageTempShow != null)
                    {
                        imageTempShow.Dispose();
                    }
                    HOperatorSet.PaintRegion(imgtempR.SelectObj(1), temp, out imageTempShow, hv_grayval, "margin");
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

                    basicClass.displayhobject(img, findShapeModelHWHandle);
                    partImageWindowShow(oriLocation);
                    templateNumber++;
                    if (templateNumber == 1)
                    {
                        tModelIDs = tModelID;
                        templateModelRegions = imgtempR.SelectObj(1);
                        modelPoints.Add(modelPoint);
                        HOperatorSet.CopyImage(imageTempShow, out templateModelImages);
                    }
                    else
                    {
                        tModelIDs = tModelIDs.TupleConcat(tModelID);
                        HOperatorSet.ConcatObj(templateModelRegions, imgtempR.SelectObj(1), out templateModelRegions);
                        modelPoints.Add(modelPoint);
                        HOperatorSet.ConcatObj(templateModelImages, imageTempShow, out templateModelImages);
                    }
                    checkedListBoxTemplate.Items.Add("模版" + templateNumber.ToString(), false);
                    checkBoxModelIsOK.Checked = false;
                    checkBoxSelectAll.Checked = false;
                    checkBoxSelectNone.Checked = false;
                    temp.Dispose();
                    imgtemp.Dispose();
                    UserCode.GetInstance().isOverFlag[currentIndex] = 12;
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show(ex.Message + "创建模版失败!");
                }
                Cursor = Cursors.Default;
            }
            else
            {
                MessageBox.Show("错误：未输入图像或未选择模版区域！");
            }

        }

        private void checkSelectAllChanged(object sender, EventArgs e)
        {
            checkBoxModelIsOK.Checked = false;
            if (checkBoxSelectAll.Checked == true)
            {
                for (int i = 0; i < templateNumber; i++)
                {
                    checkedListBoxTemplate.SetItemChecked(i, true);
                }
            }
           // UserCode.GetInstance().isOverFlag[currentIndex] = 12;
        }

        private void buttonCheckTemplate_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < templateNumber; i++)
                {
                    if (checkedListBoxTemplate.GetSelected(i))
                    {
                        //String tmpCurPath = Environment.CurrentDirectory;
                        //basicClass.readImageHobject(out imageTempShow, tmpCurPath + "模版图像" + System.String.Format("{0:D5}", i + 1) + ".bmp");
                        if (imageTempShow != null)
                        {
                            imageTempShow.Dispose();
                        }
                        HOperatorSet.CopyImage(templateModelImages[i + 1], out imageTempShow);
                        basicClass.getImageSize(imageTempShow, out rowNum, out columnNum);
                        //if (rowNum == rowOriNum && columnNum == columnOriNum)
                        //{
                        
                            HOperatorSet.ClearWindow(findShapeModelHWHandle);
                            HOperatorSet.ClearWindow(findShapeModelPartHWHandle);
                       
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
                            basicClass.displayhobject(img, findShapeModelHWHandle);
                            partImageWindowShow(oriLocation);
                            //HOperatorSet.DispObj(imgPart, findShapeModelPartHWHandle);
                        //}
                        //else
                        //{
                        //    HOperatorSet.GenEmptyObj(out image);

                        //    MessageBox.Show("模版图像行和列不可更改！");
                        //}
                    }
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }

        private void buttonSaveModel_Click(object sender, EventArgs e)
        {
            try
            {
                if (folderBrowserDialogSave.ShowDialog() == DialogResult.OK)
                {
                    modelFileSavePath = folderBrowserDialogSave.SelectedPath;
                }
                int j = 0;
                for (int i = 0; i < templateNumber; i++)
                {
                    if (checkedListBoxTemplate.GetItemChecked(i))
                    {
                        j++;
                        if (imageTempShow != null)
                        {
                            imageTempShow.Dispose();
                        }
                        HOperatorSet.CopyImage(templateModelImages[i + 1], out imageTempShow);
                        HOperatorSet.WriteImage(imageTempShow, "bmp", 0, modelFileSavePath + "//模版图像" + System.String.Format("{0:D5}", i + 1) + ".bmp");
                        HOperatorSet.WriteShapeModel(tModelIDs[i], modelFileSavePath + "//模版文件" + System.String.Format("{0:D5}", i + 1) + ".shm");
                        HTuple hv_FileHandle;
                        HOperatorSet.OpenFile(modelFileSavePath + "//模版文件" + System.String.Format("{0:D5}", i + 1) + ".dat", "output", out hv_FileHandle);
                        HOperatorSet.FwriteString(hv_FileHandle, modelPoints[i].X + "\n");
                        HOperatorSet.FwriteString(hv_FileHandle, modelPoints[i].Y + "\n");
                        HOperatorSet.CloseFile(hv_FileHandle);
                        HOperatorSet.WriteRegion(templateModelRegions[i + 1], modelFileSavePath + "//模版文件" + System.String.Format("{0:D5}", i + 1) + ".reg");

                    }

                }
                if (j==0)
                {
                    throw new Exception("未勾选模版！");
                }
                MessageBox.Show("保存模版成功!");
            }
            catch (Exception ee)
            {
                MessageBox.Show("保存模版失败!" + ee.Message);
            }


        }

        private void buttonReadModl_Click(object sender, EventArgs e)
        {
            List<PointF> modelPointsTemp = new List<PointF>(modelPoints);
            int templateNumberTemp = templateNumber;
            HObject templateModelImagesTemp;
            if (templateModelImages != null)
            {
                HOperatorSet.CopyImage(templateModelImages, out templateModelImagesTemp);
            }
            else
            {
                HOperatorSet.GenEmptyObj(out templateModelImagesTemp);
            }
            HObject templateModelRegionsTemp = templateModelRegions;
            HTuple tModelIDsTemp = tModelIDs;
            try
            {
                if (folderBrowserDialogSave.ShowDialog() == DialogResult.OK)
                {
                    modelFileReadPath = folderBrowserDialogSave.SelectedPath;
                }
                int jModel = templateNumber;
                int jImage = templateNumber;
                string[] fn = Directory.GetFiles(modelFileReadPath);
                for (int i = 0; i < fn.Length; i++)
                {
                    string fileExtension = System.IO.Path.GetExtension(fn[i]);
                    if (fileExtension == ".bmp")
                    {
                        HObject iTemp;
                        jImage++;
                        basicClass.readImageHobject(out iTemp, fn[i]);
                        basicClass.getImageSize(iTemp, out rowNum, out columnNum);
                        if (templateModelImages == null)
                        {
                            HOperatorSet.CopyImage(iTemp, out templateModelImages);
                        }
                        else
                        {
                            HOperatorSet.ConcatObj(templateModelImages, iTemp, out templateModelImages);
                        }
                        iTemp.Dispose();
                    }
                }
                int jPoint = templateNumber;
                for (int i = 0; i < fn.Length; i++)
                {
                    string fileExtension = System.IO.Path.GetExtension(fn[i]);
                    if (fileExtension == ".dat")
                    {
                        HTuple hv_FileHandle, pointX, pointY, hv_IsEOF;
                        jPoint++;
                        HOperatorSet.OpenFile(fn[i], "input", out hv_FileHandle);
                        HOperatorSet.FreadString(hv_FileHandle, out pointX, out hv_IsEOF);
                        HOperatorSet.FreadString(hv_FileHandle, out pointY, out hv_IsEOF);
                        modelPoint.X = float.Parse((string)pointX);
                        modelPoint.Y = float.Parse((string)pointY);
                        modelPoints.Add(modelPoint);
                        HOperatorSet.CloseFile(hv_FileHandle);
                    }
                }

                int jRegion = templateNumber;
                for (int i = 0; i < fn.Length; i++)
                {
                    string fileExtension = System.IO.Path.GetExtension(fn[i]);
                    if (fileExtension == ".reg")
                    {
                        jRegion++;
                        HOperatorSet.ReadRegion(out test, fn[i]);
                        if (templateModelRegions == null)
                        {
                            templateModelRegions = test;
                        }
                        else
                        {
                            HOperatorSet.ConcatObj(templateModelRegions, test, out templateModelRegions);
                        }
                    }
                }

                HTuple tModelTemp;
                for (int i = 0; i < fn.Length; i++)
                {
                    string fileExtension = System.IO.Path.GetExtension(fn[i]);
                    if (fileExtension == ".shm")
                    {
                        jModel++;
                        HOperatorSet.ReadShapeModel(fn[i], out tModelTemp);
                        checkBoxModelIsOK.Checked = false;
                        checkBoxSelectAll.Checked = false;
                        checkBoxSelectNone.Checked = false;
                        checkedListBoxTemplate.Items.Add("模版" + jModel.ToString(), false);
                        if (tModelIDs == null)
                        {
                            tModelIDs = tModelTemp;
                        }
                        else
                        {
                            tModelIDs = tModelIDs.TupleConcat(tModelTemp);
                        }
                    }
                }
                if (jImage == jModel && jImage == jPoint && jImage == jRegion)
                {
                    templateNumber = jImage;
                }
                else
                {
                    throw new Exception("模版文件有误！");
                }
                UserCode.GetInstance().isOverFlag[currentIndex] = 12;
            }
            catch (Exception ee)
            {
                MessageBox.Show("导入模版失败!" + ee.Message);
                modelPoints.Clear();
                modelPoints = modelPointsTemp;
                templateNumber = templateNumberTemp;
                if (templateModelImages != null)
                {
                    templateModelImages.Dispose();
                }
                HOperatorSet.CopyImage(templateModelImagesTemp, out templateModelImages);
                if (templateModelRegions != null)
                {
                    templateModelRegions.Dispose();
                }
                templateModelRegions = templateModelRegionsTemp;
                tModelIDs = tModelIDsTemp;
                for (int i = templateNumber; i < checkedListBoxTemplate.Items.Count; i++)
                {
                    checkedListBoxTemplate.Items.RemoveAt(i);
                }

            }
        }


        private void findShapeModelPartPictureBoxSizeChanged(object sender, EventArgs e)
        {
            try
            {
                if (findShapeModelPartHWHandle != null)
                {
                    HOperatorSet.SetWindowExtents(findShapeModelPartHWHandle, 0, 0, (findShapeModelPartPictureBox.Width), (findShapeModelPartPictureBox.Height));
                    HOperatorSet.SetPart(findShapeModelPartHWHandle, 0, 0, (findShapeModelPartPictureBox.Height - 1), (findShapeModelPartPictureBox.Width - 1));
                    if (imgPart != null)
                    {
                        partImageWindowShow(oriLocation);
                    }
                }
            }
            catch (System.Exception ex)
            {
            	
            }
            
           

        }

        private void buttonModify_Click(object sender, EventArgs e)
        {
            if (imageTempShowWithOutPoint != null && imageRealRegion != null)
            {
                tabControlFindShapeModel.Enabled = false;
                findShapeModelPictureBox.Enabled = false;
                buttonShapeSearchConfirm.Enabled = false;
                buttonShapeSearchCancel.Enabled = false;
                panelOKandCancel.Enabled = false;
                this.ControlBox = false;
                HTuple pointDisplayX, pointDisplayY;
                this.findShapeModelPartPictureBox.Focus();
                HOperatorSet.SetColor(findShapeModelPartHWHandle, "red");
                HOperatorSet.SetLineWidth(findShapeModelPartHWHandle, 4);
                HOperatorSet.DrawPoint(findShapeModelPartHWHandle, out pointDisplayY, out pointDisplayX);

                HTuple hvHomMat2D;
                HOperatorSet.HomMat2dIdentity(out hvHomMat2D);
                HOperatorSet.VectorAngleToRigid(0, 0, 0, centerLocation.Y / resRat * resizerate + (-((float)(System.Math.Min(findShapeModelPartPictureBox.Height, rowNum * resizerate) - 1 + 0) / 2.0)), centerLocation.X / resRat * resizerate + (-((float)(System.Math.Min(findShapeModelPartPictureBox.Width, columnNum * resizerate) - 1 + 0) / 2.0)), 0, out hvHomMat2D);
                HOperatorSet.HomMat2dScale(hvHomMat2D, 1.0 / resizerate, 1.0 / resizerate, 0, 0, out hvHomMat2D);
                HOperatorSet.AffineTransPixel(hvHomMat2D, pointDisplayY, pointDisplayX, out regionCenterRow, out regionCenterColumn);
                if (regionCenterColumn[0].D < columnNum && regionCenterRow[0].D < rowNum)
                {
                    HTuple hv_grayval = new HTuple();
                    hv_grayval[0] = 255;
                    hv_grayval[1] = 0;
                    hv_grayval[2] = 0;
                    HObject regionLineh, regionLinev, regionCross;
                    HOperatorSet.GenRectangle1(out regionLineh, regionCenterRow - 3, regionCenterColumn - 20, regionCenterRow + 3, regionCenterColumn + 20);
                    HOperatorSet.GenRectangle1(out regionLinev, regionCenterRow - 20, regionCenterColumn - 3, regionCenterRow + 20, regionCenterColumn + 3);
                    HOperatorSet.Union2(regionLineh, regionLinev, out regionCross);
                    modelPoint.Y = (float)regionCenterRow[0].D;
                    modelPoint.X = (float)regionCenterColumn[0].D;
                    if (imageTempShow != null)
                    {
                        imageTempShow.Dispose();
                    }
                    HOperatorSet.PaintRegion(regionCross, imageTempShowWithOutPoint, out imageTempShow, hv_grayval, "fill");
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
                    basicClass.displayhobject(img, findShapeModelHWHandle);
                    partImageWindowShow(oriLocation);
                    numericUpDownPointX.Value = (decimal)regionCenterColumn[0].D;
                    numericUpDownPointY.Value = (decimal)regionCenterRow[0].D;

                }
                findShapeModelPictureBox.Enabled = true;
                this.ControlBox = true;

                panelOKandCancel.Enabled = true;
                tabControlFindShapeModel.Enabled = true;
                buttonShapeSearchConfirm.Enabled = true;
                buttonShapeSearchCancel.Enabled = true;
            }
            else
            {
                MessageBox.Show("错误：未输入图像或未选择模版区域！");
            }
        }

        private void PointXChanged(object sender, EventArgs e)
        {
            if (imageTempShowWithOutPoint != null && imageRealRegion != null)
            {
                try
                {
                    findShapeModelPictureBox.Enabled = false;
                    HOperatorSet.SetColor(findShapeModelPartHWHandle, "red");
                    HOperatorSet.SetLineWidth(findShapeModelPartHWHandle, 4);
                    regionCenterColumn = ((double)numericUpDownPointX.Value);
                    modelPoint.X = (float)regionCenterColumn[0].D;
                    HTuple hv_grayval = new HTuple();
                    hv_grayval[0] = 255;
                    hv_grayval[1] = 0;
                    hv_grayval[2] = 0;
                    HObject regionLineh, regionLinev, regionCross;
                    HOperatorSet.GenRectangle1(out regionLineh, regionCenterRow - 3, regionCenterColumn - 20, regionCenterRow + 3, regionCenterColumn + 20);
                    HOperatorSet.GenRectangle1(out regionLinev, regionCenterRow - 20, regionCenterColumn - 3, regionCenterRow + 20, regionCenterColumn + 3);
                    HOperatorSet.Union2(regionLineh, regionLinev, out regionCross);
                    modelPoint.Y = (float)regionCenterRow[0].D;
                    modelPoint.X = (float)regionCenterColumn[0].D;
                    if (imageTempShow != null)
                    {
                        imageTempShow.Dispose();
                    }
                    HOperatorSet.PaintRegion(regionCross, imageTempShowWithOutPoint, out imageTempShow, hv_grayval, "fill");
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
                    basicClass.displayhobject(img, findShapeModelHWHandle);
                    partImageWindowShow(oriLocation);
                    findShapeModelPictureBox.Enabled = true;
                }

                catch (Exception ee)
                {
                    MessageBox.Show(ee.Message);
                }
            }
            else
            {
                MessageBox.Show("错误：未输入图像或未选择模版区域！");
            }
        }


        private void PointYChanged(object sender, EventArgs e)
        {
            if (imageTempShowWithOutPoint != null && imageRealRegion != null)
            {
                try
                {
                    findShapeModelPictureBox.Enabled = false;
                    HOperatorSet.SetColor(findShapeModelPartHWHandle, "red");
                    HOperatorSet.SetLineWidth(findShapeModelPartHWHandle, 4);
                    regionCenterRow[0].D = ((double)numericUpDownPointY.Value);
                    modelPoint.Y = (float)regionCenterRow[0].D;
                    HTuple hv_grayval = new HTuple();
                    hv_grayval[0] = 255;
                    hv_grayval[1] = 0;
                    hv_grayval[2] = 0;
                    HObject regionLineh, regionLinev, regionCross;
                    HOperatorSet.GenRectangle1(out regionLineh, regionCenterRow - 3, regionCenterColumn - 20, regionCenterRow + 3, regionCenterColumn + 20);
                    HOperatorSet.GenRectangle1(out regionLinev, regionCenterRow - 20, regionCenterColumn - 3, regionCenterRow + 20, regionCenterColumn + 3);
                    HOperatorSet.Union2(regionLineh, regionLinev, out regionCross);
                    modelPoint.Y = (float)regionCenterRow[0].D;
                    modelPoint.X = (float)regionCenterColumn[0].D;
                    if (imageTempShow != null)
                    {
                        imageTempShow.Dispose();
                    }
                    HOperatorSet.PaintRegion(regionCross, imageTempShowWithOutPoint, out imageTempShow, hv_grayval, "fill");
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
                    basicClass.displayhobject(img, findShapeModelHWHandle);
                    partImageWindowShow(oriLocation);
                    findShapeModelPictureBox.Enabled = true;
                }

                catch (Exception ee)
                {
                    MessageBox.Show(ee.Message);
                }
            }
            else
            {
                MessageBox.Show("错误：未输入图像或未选择模版区域！");
            }
        }

        private void buttonGetTestImage_Click(object sender, EventArgs e)
        {
            try
            {
                HObject imageCopy;
                UserCode.GetInstance().getImageFromProcess(out imageCopy, currentIndex);
                basicClass.getImageSize(imageCopy, out rowNum, out columnNum);
                //if (rowNum == rowOriNum && columnNum == columnOriNum)
                //{
                if (rowNum != rowOriNum || columnNum != columnOriNum)
                {
                    HOperatorSet.ClearWindow(findShapeModelHWHandle);
                    HOperatorSet.ClearWindow(findShapeModelPartHWHandle);
                }
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
                    basicClass.displayhobject(img, findShapeModelHWHandle);
                    if (imgPart != null)
                    {
                        imgPart.Dispose();
                    }
                    basicClass.resizeImage(imageTemp, out imgPart, resizerate);
                    HObject temp;
                    basicClass.genRectangle1(out rectDomain, 0, 0, (findShapeModelPartPictureBox.Height - 1), (findShapeModelPartPictureBox.Width - 1));
                    basicClass.reduceDomain(imgPart, rectDomain, out temp);
                    centerLocation.Y = (float)(((float)(System.Math.Min(findShapeModelPartPictureBox.Height, rowNum * resizerate) - 1 + 0) / 2.0) * resRat / resizerate);
                    centerLocation.X = (float)(((float)(System.Math.Min(findShapeModelPartPictureBox.Width, columnNum * resizerate) - 1 + 0) / 2.0) * resRat / resizerate);
                    if (imgPartTemp != null)
                    {
                        imgPartTemp.Dispose();
                    }
                    HOperatorSet.CropDomain(temp, out imgPartTemp);
                    HOperatorSet.DispObj(imgPartTemp, findShapeModelPartHWHandle);
                    oriLocation.X = (int)centerLocation.X;
                    oriLocation.Y = (int)centerLocation.Y;
                    temp.Dispose();
                //}
                //else
                //{
                //    MessageBox.Show("原始图像行和列不可更改！");
                //}

                imageCopy.Dispose();
            }

            catch (Exception ex)
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


        private void checkBoxModelIsOK_CheckedChanged(object sender, EventArgs e)
        {
            try
            {

                if (checkBoxModelIsOK.Checked == true)
                {
                    checkBoxSelectAll.Enabled = false;
                    checkBoxSelectNone.Enabled = false;
                    buttonReadModl.Enabled = false;
                    buttonSaveModel.Enabled = false;
                    buttonCheckTemplate.Enabled = false;
                    checkedListBoxTemplate.Enabled = false;
                    buttonShapeSearchConfirm.Enabled = false;
                    int numt = 0;
                    panelModelIsOK.Enabled = true;
                    for (int i = 0; i < checkedListBoxTemplate.Items.Count; i++)
                    {
                        if (checkedListBoxTemplate.GetItemChecked(i))
                        {
                            numt++;
                            comboBoxModel.Items.Add("模版" + System.String.Format("{0:D5}", numt));
                            templateModelPoints.Add(modelPoints[i]);
                            if (templateModel == null)
                            {
                                // HOperatorSet.SetShapeModelOrigin(tModelIDs[i], modelPoints[i].Y, modelPoints[i].X);
                                HOperatorSet.SetShapeModelParam(tModelIDs[i], "timeout", 3000);
                                templateModel = tModelIDs[i];
                                imageModelRegions = templateModelRegions[i + 1];

                            }
                            else
                            {
                                // HOperatorSet.SetShapeModelOrigin(tModelIDs[i], modelPoints[i].Y, modelPoints[i].X);
                                HOperatorSet.SetShapeModelParam(tModelIDs[i], "timeout", 3000);
                                templateModel = templateModel.TupleConcat(tModelIDs[i]);
                                HOperatorSet.ConcatObj(imageModelRegions, templateModelRegions[i + 1], out imageModelRegions);
                            }
                            modelIfCkecked[i] = true;
                        }
                        else
                        {
                            modelIfCkecked[i] = false;
                        }
                    }
                    if (templateModel == null)
                    {
                        MessageBox.Show("错误！未勾选模版!");
                        checkBoxModelIsOK.Checked = false;
                        panelModelIsOK.Enabled = false;
                    }
                    else
                    {
                        if (templateModel.TupleLength() == 1)
                        {
                            checkBoxMultiplePara.Enabled = false;
                        }
                        else
                        {
                            checkBoxMultiplePara.Enabled = true;
                            fParas.angleStart = new double[(int)templateModel.TupleLength()];
                            fParas.angleExtent = new double[(int)templateModel.TupleLength()];
                            fParas.scaleRMin = new double[(int)templateModel.TupleLength()];
                            fParas.scaleRMax = new double[(int)templateModel.TupleLength()];
                            fParas.scaleCMin = new double[(int)templateModel.TupleLength()];
                            fParas.scaleCMax = new double[(int)templateModel.TupleLength()];
                            fParas.minScore = new double[(int)templateModel.TupleLength()];
                            fParas.numMatches = new int[(int)templateModel.TupleLength()];
                            //fParas.maxOverlap = 0.5;
                            fParas.subPixel = new int[(int)templateModel.TupleLength() * 2];
                            fParas.numLevels = new int[(int)templateModel.TupleLength() * 2];
                            fParas.greediness = new double[(int)templateModel.TupleLength()];
                            fParas.maxOverlap = wFparas.maxOverlap;
                            int i_num = 0;
                            for (int i = 0; i < tModelIDs.TupleLength(); i++)
                            {

                                if (modelIfCkecked[i])
                                {
                                    fParas.angleStart[i_num] = wFparas.angleStart[i];
                                    fParas.angleExtent[i_num] = wFparas.angleExtent[i];
                                    fParas.scaleRMin[i_num] = wFparas.scaleRMin[i];
                                    fParas.scaleRMax[i_num] = wFparas.scaleRMax[i];
                                    fParas.scaleCMin[i_num] = wFparas.scaleCMin[i];
                                    fParas.scaleCMax[i_num] = wFparas.scaleCMax[i];
                                    fParas.minScore[i_num] = wFparas.minScore[i];
                                    fParas.numMatches[i_num] = wFparas.numMatches[i];

                                    fParas.subPixel[i_num * 2] = wFparas.subPixel[i * 2];
                                    fParas.subPixel[i_num * 2 + 1] = wFparas.subPixel[i * 2 + 1];
                                    fParas.numLevels[i_num * 2] = wFparas.numLevels[i * 2];
                                    fParas.numLevels[i_num * 2 + 1] = wFparas.numLevels[i * 2 + 1];
                                    fParas.greediness[i_num] = wFparas.greediness[i];
                                    i_num++;
                                }

                            }
                        }


                    }
                }
                else
                {
                   
                    checkBoxSelectAll.Enabled = true;
                    checkBoxSelectNone.Enabled = true;
                    buttonReadModl.Enabled = true;
                    buttonSaveModel.Enabled = true;
                    buttonCheckTemplate.Enabled = true;
                    checkedListBoxTemplate.Enabled = true;
                    //buttonShapeSearchConfirm.Enabled = true;
                    panelModelIsOK.Enabled = false;
                    comboBoxModel.Items.Clear();
                    templateModelPoints.Clear();
                    checkBoxMultiplePara.Checked = false;
                    imageModelRegions = null;
                    templateModel = null;
                    fParas.angleStart = null;
                    fParas.angleExtent = null;
                    fParas.scaleRMin = null;
                    fParas.scaleRMax = null;
                    fParas.scaleCMin = null;
                    fParas.scaleCMax = null;
                    fParas.minScore = null;
                    fParas.numMatches = null;
                    fParas.maxOverlap = 0;
                    fParas.subPixel = null;
                    fParas.numLevels = null;
                    fParas.greediness = null;
                }
            }

            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }

        private void checkBoxMultiplePara_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
               // UserCode.GetInstance().isOverFlag[currentIndex] = 12;
                if (checkBoxMultiplePara.Checked == true)
                {
                    panelMultiplePara.Enabled = true;
                    comboBoxModel.SelectedIndex = 0;
                    numericUpDownTestOverLap.Enabled = false;
                    //checkBoxTestNumberLevelsAuto.Checked = false;
                    //checkBoxTestNumberLevelsAuto.Enabled = false;

                }
                else
                {
                    //comboBoxModel.Items.Clear();
                    numericUpDownTestOverLap.Enabled = true;
                    panelMultiplePara.Enabled = false;
                    //checkBoxTestNumberLevelsAuto.Enabled = true;
                }
            }

            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }

        private void buttonSaveSingleModelPara_Click(object sender, EventArgs e)
        {
            try
            {
                fParas.angleStart[comboBoxModel.SelectedIndex] = (double)numericUpDownTestStartAngle.Value;
                fParas.angleExtent[comboBoxModel.SelectedIndex] = (double)numericUpDownTestExternAngle.Value;
                fParas.scaleRMin[comboBoxModel.SelectedIndex] = (double)numericUpDownTestScaleRMin.Value;
                fParas.scaleRMax[comboBoxModel.SelectedIndex] = (double)numericUpDownTestScaleRMax.Value;
                fParas.scaleCMin[comboBoxModel.SelectedIndex] = (double)numericUpDownTestScaleCMin.Value;
                fParas.scaleCMax[comboBoxModel.SelectedIndex] = (double)numericUpDownTestScaleCMax.Value;
                fParas.minScore[comboBoxModel.SelectedIndex] = (double)numericUpDownTestMinScore.Value;
                fParas.numMatches[comboBoxModel.SelectedIndex] = (int)numericUpDownTestNumber.Value;
                fParas.maxOverlap = (double)numericUpDownTestOverLap.Value;
                fParas.subPixel[(comboBoxModel.SelectedIndex) * 2] = (int)comboBoxSubPixel1.SelectedIndex;
                fParas.subPixel[(comboBoxModel.SelectedIndex) * 2 + 1] = (int)comboBoxSubPixel2.SelectedIndex;
                if (checkBoxTestNumberLevelsAuto.Checked == true)
                {
                    fParas.numLevels[(comboBoxModel.SelectedIndex) * 2] = 0;
                    fParas.numLevels[(comboBoxModel.SelectedIndex) * 2 + 1] = 0;
                }
                else
                {
                    fParas.numLevels[(comboBoxModel.SelectedIndex) * 2] = (int)numericUpDownTestNumberLevelsLarge.Value;
                    fParas.numLevels[(comboBoxModel.SelectedIndex) * 2 + 1] = (int)numericUpDownTestNumberLevelsSmall.Value;
                }

                fParas.greediness[comboBoxModel.SelectedIndex] = (double)numericUpDownTestGreediness.Value;

                int curId = 0;

                for (int i = 0; i < tModelIDs.TupleLength(); i++)
                {
                    if (modelIfCkecked[i])
                    {

                        if (curId == comboBoxModel.SelectedIndex)
                        {
                            wFparas.angleStart[i] = (double)numericUpDownTestStartAngle.Value;
                            wFparas.angleExtent[i] = (double)numericUpDownTestExternAngle.Value;
                            wFparas.scaleRMin[i] = (double)numericUpDownTestScaleRMin.Value;
                            wFparas.scaleRMax[i] = (double)numericUpDownTestScaleRMax.Value;
                            wFparas.scaleCMin[i] = (double)numericUpDownTestScaleCMin.Value;
                            wFparas.scaleCMax[i] = (double)numericUpDownTestScaleCMax.Value;
                            wFparas.minScore[i] = (double)numericUpDownTestMinScore.Value;
                            wFparas.numMatches[i] = (int)numericUpDownTestNumber.Value;
                            wFparas.subPixel[i * 2] = (int)comboBoxSubPixel1.SelectedIndex;
                            wFparas.subPixel[i * 2 + 1] = (int)comboBoxSubPixel2.SelectedIndex;
                            if (checkBoxTestNumberLevelsAuto.Checked == true)
                            {
                                wFparas.numLevels[i * 2] = 0;
                                wFparas.numLevels[i * 2 + 1] = 0;
                            }
                            else
                            {
                                wFparas.numLevels[i * 2] = (int)numericUpDownTestNumberLevelsLarge.Value;
                                wFparas.numLevels[i * 2 + 1] = (int)numericUpDownTestNumberLevelsSmall.Value;
                            }

                            wFparas.greediness[i] = (double)numericUpDownTestGreediness.Value;
                            break;
                        }
                        curId++;
                    }

                }
                wFparas.maxOverlap = (double)numericUpDownTestOverLap.Value;
            }

            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }

        private void checkBoxTestNumberLevelsAuto_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                //UserCode.GetInstance().isOverFlag[currentIndex] = 12;
                if (checkBoxTestNumberLevelsAuto.Checked == true)
                {
                    panelTestNumberLevels.Enabled = false;

                }
                else
                {
                    panelTestNumberLevels.Enabled = true;
                }
            }

            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }

        private void numericUpDownTestNumberLevelsLarge_ValueChanged(object sender, EventArgs e)
        {
            numericUpDownTestNumberLevelsSmall.Maximum = numericUpDownTestNumberLevelsLarge.Value;
            numericUpDownTestNumberLevelsSmall.Value = numericUpDownTestNumberLevelsSmall.Value - 1;

        }

        private void numericUpDownTestNumberLevelsSmall_ValueChanged(object sender, EventArgs e)
        {
            numericUpDownTestNumberLevelsSmall.Maximum = numericUpDownTestNumberLevelsLarge.Value;
        }

        private void checkBoxTestDetail_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (checkBoxTestDetail.Checked == true)
                {
                    panelTestDetail.Enabled = true;

                }
                else
                {
                    panelTestDetail.Enabled = false;
                }
            }

            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }

        }

        private void comboBoxModel_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //UserCode.GetInstance().isOverFlag[currentIndex] = 12;
                numericUpDownTestStartAngle.Value = (decimal)fParas.angleStart[comboBoxModel.SelectedIndex];
                numericUpDownTestExternAngle.Value = (decimal)fParas.angleExtent[comboBoxModel.SelectedIndex];
                numericUpDownTestScaleRMin.Value = (decimal)fParas.scaleRMin[comboBoxModel.SelectedIndex];
                numericUpDownTestScaleRMax.Value = (decimal)fParas.scaleRMax[comboBoxModel.SelectedIndex];
                numericUpDownTestScaleCMin.Value = (decimal)fParas.scaleCMin[comboBoxModel.SelectedIndex];
                numericUpDownTestScaleCMax.Value = (decimal)fParas.scaleCMax[comboBoxModel.SelectedIndex];

                numericUpDownTestMinScore.Value = (decimal)fParas.minScore[comboBoxModel.SelectedIndex];
                numericUpDownTestNumber.Value = (decimal)fParas.numMatches[comboBoxModel.SelectedIndex];
                numericUpDownTestOverLap.Value = (decimal)fParas.maxOverlap;
                comboBoxSubPixel1.SelectedIndex = (int)fParas.subPixel[(comboBoxModel.SelectedIndex) * 2];
                comboBoxSubPixel2.SelectedIndex = (int)fParas.subPixel[(comboBoxModel.SelectedIndex) * 2 + 1];
                //numericUpDownTestNumberLevelsLarge.Value = (decimal)fParas.numLevels[(comboBoxModel.SelectedIndex) * 2];
                //numericUpDownTestNumberLevelsSmall.Value = (decimal)fParas.numLevels[(comboBoxModel.SelectedIndex) * 2 + 1];

                if (fParas.numLevels[(comboBoxModel.SelectedIndex) * 2] == 0 && fParas.numLevels[(comboBoxModel.SelectedIndex) * 2 + 1] == 0)
                {
                    checkBoxTestNumberLevelsAuto.Checked = true;
                }
                else
                {
                    checkBoxTestNumberLevelsAuto.Checked = false;
                    numericUpDownTestNumberLevelsLarge.Value = (decimal)fParas.numLevels[(comboBoxModel.SelectedIndex) * 2];
                    numericUpDownTestNumberLevelsSmall.Value = (decimal)fParas.numLevels[(comboBoxModel.SelectedIndex) * 2 + 1];
                }
                numericUpDownTestGreediness.Value = (decimal)fParas.greediness[comboBoxModel.SelectedIndex];
            }

            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }

        private void buttonFindModel_Click(object sender, EventArgs e)
        {
            if (image != null)
            {
                Cursor = Cursors.WaitCursor;
                buttonShapeSearchConfirm.Enabled = true;
                if (checkBoxBorderShapeModel.Checked == true)
                {
                    HOperatorSet.SetSystem("border_shape_models", "true");

                }
                else
                {
                    HOperatorSet.SetSystem("border_shape_models", "false");
                }
                try
                {
                    if (checkBoxModelIsOK.Checked == true)
                    {
                        if (checkBoxMultiplePara.Checked == true && templateModel.TupleLength() != 1)
                        {
                            bool[] flagAutoNum = new bool[fParas.angleStart.Length];
                            for (int i = 0; i < fParas.angleStart.Length; i++)
                            {
                                if (fParas.numLevels[i * 2] == 0 && fParas.numLevels[i * 2 + 1] == 0)
                                {
                                    flagAutoNum[i] = true;
                                }
                                else
                                {
                                    flagAutoNum[i] = false;
                                }
                            }
                            for (int i = 1; i < flagAutoNum.Length; i++)
                            {
                                if (flagAutoNum[i - 1] != flagAutoNum[i])
                                {
                                    MessageBox.Show("错误！多模版状态下若为每个模版分别配置模版参数，则在配置金字塔层数时需满足或均为自动设置模式，或均为非自动设置模式!请修改！");
                                    return;
                                }
                            }
                            HTuple seconds1, seconds2, secondsTotal;
                            HOperatorSet.CountSeconds(out seconds1);
                            fSMC.setFindAnisoShapeModelsPara((int)templateModel.TupleLength(), fParas.angleStart, fParas.angleExtent,fParas.scaleRMin,fParas.scaleRMax,fParas.scaleCMin,fParas.scaleCMax, fParas.minScore, fParas.numMatches, fParas.maxOverlap, fParas.subPixel, fParas.numLevels, fParas.greediness);//Set findShapeModels parameters.
                            //fsmc.findShapeModels(hoTestImage,hvWindowHandle, 1, out rows, out columns, out angles, out scores,out templateID);// Find the Multiple models and display the results.
                            fSMC.findAnisoShapeModels(templateModel, image,arrangeIndex, out rows, out columns, out angles,out scaleR,out scaleC,out scores, out templateID);// Find the Multiple models without display the results.
                            HOperatorSet.CountSeconds(out seconds2);
                            if (rows.Length != 0)
                            {
                                HObject tempContour, tempRegion, tempCross, imageTempShowWithOutPoint;
                                HOperatorSet.GenEmptyObj(out tempContour);
                                HOperatorSet.GenEmptyObj(out tempCross);
                                for (int i = 0; i < rows.Length; i++)
                                {
                                    HTuple hvHomMat2D;
                                    HOperatorSet.VectorAngleToRigid(templateModelPoints[templateID[i]].Y, templateModelPoints[templateID[i]].X, 0, 0, 0, 0, out hvHomMat2D);
                                    HOperatorSet.HomMat2dScale(hvHomMat2D, scaleR[i], scaleC[i], 0, 0, out hvHomMat2D);
                                    HOperatorSet.HomMat2dRotate(hvHomMat2D, angles[i], 0, 0, out hvHomMat2D);
                                    HOperatorSet.HomMat2dTranslate(hvHomMat2D, rows[i], columns[i], out hvHomMat2D);    
                                    HOperatorSet.AffineTransRegion(imageModelRegions[templateID[i] + 1], out tempRegion, hvHomMat2D, "nearest_neighbor");
                                    if (i == 0)
                                    {
                                        tempContour = tempRegion;
                                    }
                                    else
                                    {
                                        HOperatorSet.ConcatObj(tempContour, tempRegion, out tempContour);
                                    }
                                    HObject regionLineh, regionLinev, regionCross;
                                    HOperatorSet.GenRectangle1(out regionLineh, rows[i] - 3, columns[i] - 20, rows[i] + 3, columns[i] + 20);
                                    HOperatorSet.GenRectangle1(out regionLinev, rows[i] - 20, columns[i] - 3, rows[i] + 20, columns[i] + 3);
                                    HOperatorSet.Union2(regionLineh, regionLinev, out regionCross);
                                    if (i == 0)
                                    {
                                        tempCross = regionCross;
                                    }
                                    else
                                    {
                                        HOperatorSet.ConcatObj(tempCross, regionCross, out tempCross);
                                    }
                                }
                                HTuple hv_grayval = new HTuple();
                                HTuple hv_grayvalp = new HTuple();
                                hv_grayvalp[0] = 255;
                                hv_grayvalp[1] = 0;
                                hv_grayvalp[2] = 0;
                                hv_grayval[0] = 0;
                                hv_grayval[1] = 255;
                                hv_grayval[2] = 0;
                                HOperatorSet.PaintRegion(tempCross, imageTemp, out imageTempShowWithOutPoint, hv_grayvalp, "fill");
                                if (imageTempShow != null)
                                {
                                    imageTempShow.Dispose();
                                }
                                HOperatorSet.PaintRegion(tempContour, imageTempShowWithOutPoint, out imageTempShow, hv_grayval, "margin");
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
                                basicClass.displayhobject(img, findShapeModelHWHandle);
                                partImageWindowShow(oriLocation);
                                imageTempShowWithOutPoint.Dispose();
                            }
                            else
                            {
                                if (imgPart != null)
                                {
                                    imgPart.Dispose();
                                }
                                basicClass.resizeImage(imageTemp, out imgPart, resizerate);
                                if (img != null)
                                {
                                    img.Dispose();
                                }
                                basicClass.resizeImage(imageTemp, out img, resRat);
                                basicClass.displayhobject(img, findShapeModelHWHandle);
                                partImageWindowShow(oriLocation);
                            }

                            string strtemp = "\r\n";
                            for (int i_object = 0; i_object < rows.Length; i_object++)
                            {


                                string strc, stri, strx, stry, strad,strsrd,strscd, strsd;

                                strc = System.String.Format("{0:d}", templateID[i_object]);
                                stri = System.String.Format("{0:d}", i_object);
                                strx = System.String.Format("{0:f}", columns[i_object]);
                                stry = System.String.Format("{0:f}", rows[i_object]);

                                strad = System.String.Format("{0:f}", angles[i_object] / 3.14159 * 180);
                                strsrd = System.String.Format("{0:f}", scaleR[i_object]);
                                strscd = System.String.Format("{0:f}", scaleC[i_object]);
                                strsd = System.String.Format("{0:f}", scores[i_object]);
                                strtemp = strtemp + "Point" + stri + ":(" + strx + " , " + stry + ") " + "\r\n" + "Model: " + strc + "\r\n" +
                                    "Angle to template:" + strad + "\r\n" + "ScaleR to template:" + strsrd + "\r\n" +
                                    "ScaleC to template:" + strscd + "\r\n" + "Score:" + strsd + "\r\n" + "\r\n";

                            }

                            secondsTotal = seconds2 - seconds1;

                            string strtd;
                            strtd = System.String.Format("{0:f}", secondsTotal[0].D * 1000.0);

                            str = "Time:" + strtd + "ms" + "\r\n" + strtemp;


                            textBoxResult.Text = str;
                        }
                        else if (checkBoxMultiplePara.Checked == false && templateModel.TupleLength() != 1)
                        {

                            HTuple seconds1, seconds2, secondsTotal;
                            HOperatorSet.CountSeconds(out seconds1);
                            fParasone.angleStart = new double[1];
                            fParasone.angleExtent = new double[1];
                            fParasone.scaleRMin = new double[1];
                            fParasone.scaleRMax = new double[1];
                            fParasone.scaleCMin = new double[1];
                            fParasone.scaleCMax = new double[1];
                            fParasone.minScore = new double[1];
                            fParasone.numMatches = new int[1];
                            //fParas.maxOverlap = 0.5;
                            fParasone.subPixel = new int[2];
                            fParasone.numLevels = new int[2];
                            fParasone.greediness = new double[1];
                            fParasone.angleStart[0] = (double)numericUpDownTestStartAngle.Value;
                            fParasone.angleExtent[0] = (double)numericUpDownTestExternAngle.Value;
                            fParasone.scaleRMin[0] = (double)numericUpDownTestScaleRMin.Value;
                            fParasone.scaleRMax[0] = (double)numericUpDownTestScaleRMax.Value;
                            fParasone.scaleCMin[0] = (double)numericUpDownTestScaleCMin.Value;
                            fParasone.scaleCMax[0] = (double)numericUpDownTestScaleCMax.Value;
                            fParasone.minScore[0] = (double)numericUpDownTestMinScore.Value;
                            fParasone.numMatches[0] = (int)numericUpDownTestNumber.Value;
                            fParasone.maxOverlap = (double)numericUpDownTestOverLap.Value;
                            fParasone.subPixel[0] = (int)comboBoxSubPixel1.SelectedIndex;
                            fParasone.subPixel[1] = (int)comboBoxSubPixel2.SelectedIndex;
                            if (checkBoxTestNumberLevelsAuto.Checked == true)
                            {
                                fParasone.numLevels[0] = 0;
                                fParasone.numLevels[1] = 0;
                            }
                            else
                            {
                                fParasone.numLevels[0] = (int)numericUpDownTestNumberLevelsLarge.Value;
                                fParasone.numLevels[1] = (int)numericUpDownTestNumberLevelsSmall.Value;
                            }
                            fParasone.greediness[0] = (double)numericUpDownTestGreediness.Value;
                            fSMC.setFindAnisoShapeModelsPara((int)templateModel.TupleLength(), fParasone.angleStart, fParasone.angleExtent,fParasone.scaleRMin,fParasone.scaleRMax,fParasone.scaleCMin,fParasone.scaleCMax, fParasone.minScore, fParasone.numMatches, fParasone.maxOverlap, fParasone.subPixel, fParasone.numLevels, fParasone.greediness);//Set findShapeModels parameters.
                            //fsmc.findShapeModels(hoTestImage,hvWindowHandle, 1, out rows, out columns, out angles, out scores,out templateID);// Find the Multiple models and display the results.
                            fSMC.findAnisoShapeModels(templateModel, image, arrangeIndex, out rows, out columns, out angles, out scaleR, out scaleC, out scores, out templateID);// Find the Multiple models without display the results.
                            HOperatorSet.CountSeconds(out seconds2);
                            if (rows.Length != 0)
                            {
                                HObject tempContour, tempRegion, tempCross, imageTempShowWithOutPoint;
                                HOperatorSet.GenEmptyObj(out tempContour);
                                HOperatorSet.GenEmptyObj(out tempCross);
                                for (int i = 0; i < rows.Length; i++)
                                {
                                    HTuple hvHomMat2D;
                                    HOperatorSet.VectorAngleToRigid(templateModelPoints[templateID[i]].Y, templateModelPoints[templateID[i]].X, 0, 0, 0, 0, out hvHomMat2D);
                                    HOperatorSet.HomMat2dScale(hvHomMat2D, scaleR[i], scaleC[i], 0, 0, out hvHomMat2D);
                                    HOperatorSet.HomMat2dRotate(hvHomMat2D, angles[i], 0, 0, out hvHomMat2D);
                                    HOperatorSet.HomMat2dTranslate(hvHomMat2D, rows[i], columns[i], out hvHomMat2D);                                 
                                    HOperatorSet.AffineTransRegion(imageModelRegions[templateID[i] + 1], out tempRegion, hvHomMat2D, "nearest_neighbor");
                                    if (i == 0)
                                    {
                                        tempContour = tempRegion;
                                    }
                                    else
                                    {
                                        HOperatorSet.ConcatObj(tempContour, tempRegion, out tempContour);
                                    }
                                    HObject regionLineh, regionLinev, regionCross;
                                    HOperatorSet.GenRectangle1(out regionLineh, rows[i] - 3, columns[i] - 20, rows[i] + 3, columns[i] + 20);
                                    HOperatorSet.GenRectangle1(out regionLinev, rows[i] - 20, columns[i] - 3, rows[i] + 20, columns[i] + 3);
                                    HOperatorSet.Union2(regionLineh, regionLinev, out regionCross);
                                    if (i == 0)
                                    {
                                        tempCross = regionCross;
                                    }
                                    else
                                    {
                                        HOperatorSet.ConcatObj(tempCross, regionCross, out tempCross);
                                    }
                                }
                                HTuple hv_grayval = new HTuple();
                                HTuple hv_grayvalp = new HTuple();
                                hv_grayvalp[0] = 255;
                                hv_grayvalp[1] = 0;
                                hv_grayvalp[2] = 0;
                                hv_grayval[0] = 0;
                                hv_grayval[1] = 255;
                                hv_grayval[2] = 0;
                                HOperatorSet.PaintRegion(tempCross, imageTemp, out imageTempShowWithOutPoint, hv_grayvalp, "fill");
                                if (imageTempShow != null)
                                {
                                    imageTempShow.Dispose();
                                }
                                HOperatorSet.PaintRegion(tempContour, imageTempShowWithOutPoint, out imageTempShow, hv_grayval, "margin");
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
                                basicClass.displayhobject(img, findShapeModelHWHandle);
                                partImageWindowShow(oriLocation);
                                imageTempShowWithOutPoint.Dispose();
                            }
                            else
                            {
                                if (imgPart != null)
                                {
                                    imgPart.Dispose();
                                }
                                basicClass.resizeImage(imageTemp, out imgPart, resizerate);
                                if (img != null)
                                {
                                    img.Dispose();
                                }
                                basicClass.resizeImage(imageTemp, out img, resRat);
                                basicClass.displayhobject(img, findShapeModelHWHandle);
                                partImageWindowShow(oriLocation);
                            }

                            string strtemp = "\r\n";
                            for (int i_object = 0; i_object < rows.Length; i_object++)
                            {


                                string strc, stri, strx, stry, strad, strsd, strsrd, strscd;

                                strc = System.String.Format("{0:d}", templateID[i_object]);
                                stri = System.String.Format("{0:d}", i_object);
                                strx = System.String.Format("{0:f}", columns[i_object]);
                                stry = System.String.Format("{0:f}", rows[i_object]);

                                strad = System.String.Format("{0:f}", angles[i_object] / 3.14159 * 180);
                                strsrd = System.String.Format("{0:f}", scaleR[i_object]);
                                strscd = System.String.Format("{0:f}", scaleC[i_object]);
                                strsd = System.String.Format("{0:f}", scores[i_object]);
                                strtemp = strtemp + "Point" + stri + ":(" + strx + " , " + stry + ") " + "\r\n" + "Model: " + strc + "\r\n" + 
                                    "Angle to template:" + strad + "\r\n" + "ScaleR to template:" + strsrd + "\r\n" +
                                    "ScaleC to template:" + strscd + "\r\n" + "Score:" + strsd + "\r\n" + "\r\n";

                            }


                            secondsTotal = seconds2 - seconds1;

                            string strtd;
                            strtd = System.String.Format("{0:f}", secondsTotal[0].D * 1000.0);

                            str = "Time:" + strtd + "ms" + "\r\n" + strtemp;
                            textBoxResult.Text = str;


                        }
                        else if (templateModel.TupleLength() == 1)
                        {
                            HTuple seconds1, seconds2, secondsTotal;
                            //HTuple seconds3, seconds3Total,
                            //    seconds4, seconds4Total,
                            //    seconds5, seconds5Total,
                            //    seconds6, seconds6Total;
                            HOperatorSet.CountSeconds(out seconds1);

                            fPara.subPixel = new int[2];
                            fPara.numLevels = new int[2];

                            fPara.angleStart = (double)numericUpDownTestStartAngle.Value;
                            fPara.angleExtent = (double)numericUpDownTestExternAngle.Value;
                            fPara.scaleRMin = (double)numericUpDownTestScaleRMin.Value;
                            fPara.scaleRMax = (double)numericUpDownTestScaleRMax.Value;
                            fPara.scaleCMin = (double)numericUpDownTestScaleCMin.Value;
                            fPara.scaleCMax = (double)numericUpDownTestScaleCMax.Value;
                            fPara.minScore = (double)numericUpDownTestMinScore.Value;
                            fPara.numMatches = (int)numericUpDownTestNumber.Value;
                            fPara.maxOverlap = (double)numericUpDownTestOverLap.Value;
                            fPara.subPixel[0] = (int)comboBoxSubPixel1.SelectedIndex;
                            fPara.subPixel[1] = (int)comboBoxSubPixel2.SelectedIndex;
                            if (checkBoxTestNumberLevelsAuto.Checked == true)
                            {
                                fPara.numLevels[0] = 0;
                                fPara.numLevels[1] = 0;
                            }
                            else
                            {
                                fPara.numLevels[0] = (int)numericUpDownTestNumberLevelsLarge.Value;
                                fPara.numLevels[1] = (int)numericUpDownTestNumberLevelsSmall.Value;
                            }
                            fPara.greediness = (double)numericUpDownTestGreediness.Value;
                            fSMC.setFindAnisoShapeModelPara(fPara.angleStart, fPara.angleExtent,fPara.scaleRMin,fPara.scaleRMax,fPara.scaleCMin,fPara.scaleCMax, fPara.minScore, fPara.numMatches, fPara.maxOverlap, fPara.subPixel, fPara.numLevels, fPara.greediness);//Set findShapeModels parameters.
                            //fsmc.findShapeModels(hoTestImage,hvWindowHandle, 1, out rows, out columns, out angles, out scores,out templateID);// Find the Multiple models and display the results.
                            fSMC.findAnisoShapeModel(image, templateModel, arrangeIndex, out rows, out columns, out angles, out scaleR, out scaleC, out scores);// Find the Multiple models without display the results.
                            HOperatorSet.CountSeconds(out seconds2);
                            if (rows.Length != 0)
                            {
                                HObject tempContour, tempRegion, tempCross, imageTempShowWithOutPoint;
                                HOperatorSet.GenEmptyObj(out tempContour);
                                HOperatorSet.GenEmptyObj(out tempCross);
                                for (int i = 0; i < rows.Length; i++)
                                {
                                    HTuple hvHomMat2D;
                                    HOperatorSet.VectorAngleToRigid(templateModelPoints[0].Y, templateModelPoints[0].X, 0, 0, 0, 0, out hvHomMat2D);
                                    HOperatorSet.HomMat2dScale(hvHomMat2D, scaleR[i], scaleC[i], 0, 0, out hvHomMat2D);
                                    HOperatorSet.HomMat2dRotate(hvHomMat2D, angles[i], 0, 0, out hvHomMat2D);
                                    HOperatorSet.HomMat2dTranslate(hvHomMat2D, rows[i], columns[i], out hvHomMat2D);
                                    HOperatorSet.AffineTransRegion(imageModelRegions[1], out tempRegion, hvHomMat2D, "nearest_neighbor");
                                    if (i == 0)
                                    {
                                        tempContour = tempRegion;
                                    }
                                    else
                                    {
                                        HOperatorSet.ConcatObj(tempContour, tempRegion, out tempContour);
                                    }
                                    HObject regionLineh, regionLinev, regionCross;
                                    HOperatorSet.GenRectangle1(out regionLineh, rows[i] - 3, columns[i] - 20, rows[i] + 3, columns[i] + 20);
                                    HOperatorSet.GenRectangle1(out regionLinev, rows[i] - 20, columns[i] - 3, rows[i] + 20, columns[i] + 3);
                                    HOperatorSet.Union2(regionLineh, regionLinev, out regionCross);
                                    if (i == 0)
                                    {
                                        tempCross = regionCross;
                                    }
                                    else
                                    {
                                        HOperatorSet.ConcatObj(tempCross, regionCross, out tempCross);
                                    }
                                }

                                HTuple hv_grayval = new HTuple();
                                HTuple hv_grayvalp = new HTuple();
                                hv_grayvalp[0] = 255;
                                hv_grayvalp[1] = 0;
                                hv_grayvalp[2] = 0;
                                hv_grayval[0] = 0;
                                hv_grayval[1] = 255;
                                hv_grayval[2] = 0;

                                HOperatorSet.PaintRegion(tempCross, imageTemp, out imageTempShowWithOutPoint, hv_grayvalp, "fill");

                                if (imageTempShow != null)
                                {
                                    imageTempShow.Dispose();
                                }
                                //HOperatorSet.CountSeconds(out seconds3);
                                //seconds3Total = seconds3 - seconds1;
                                HOperatorSet.PaintRegion(tempContour, imageTempShowWithOutPoint, out imageTempShow, hv_grayval, "margin");
                                //HOperatorSet.CountSeconds(out seconds4);
                                //seconds4Total = seconds4 - seconds3;
                                if (imgPart != null)
                                {
                                    imgPart.Dispose();
                                }
                                basicClass.resizeImage(imageTempShow, out imgPart, resizerate);

                                if (img != null)
                                {
                                    img.Dispose();
                                }
                                //HOperatorSet.CountSeconds(out seconds5);
                                //seconds5Total = seconds5 - seconds4;
                                basicClass.resizeImage(imageTempShow, out img, resRat);
                                basicClass.displayhobject(img, findShapeModelHWHandle);
                                partImageWindowShow(oriLocation);
                                imageTempShowWithOutPoint.Dispose();
                                //HOperatorSet.CountSeconds(out seconds6);
                                //seconds6Total = seconds6 - seconds5;
                            }
                            else
                            {
                                if (imgPart != null)
                                {
                                    imgPart.Dispose();
                                }
                                basicClass.resizeImage(imageTemp, out imgPart, resizerate);
                                if (img != null)
                                {
                                    img.Dispose();
                                }
                                basicClass.resizeImage(imageTemp, out img, resRat);
                                basicClass.displayhobject(img, findShapeModelHWHandle);
                                partImageWindowShow(oriLocation);
                            }

                            string strtemp = "\r\n";
                            for (int i_object = 0; i_object < rows.Length; i_object++)
                            {


                                string strc, stri, strx, stry, strad, strsd, strsrd, strscd;

                                strc = System.String.Format("{0:d}", 0);
                                stri = System.String.Format("{0:d}", i_object);
                                strx = System.String.Format("{0:f}", columns[i_object]);
                                stry = System.String.Format("{0:f}", rows[i_object]);

                                strad = System.String.Format("{0:f}", angles[i_object] / 3.14159 * 180);
                                strsrd = System.String.Format("{0:f}", scaleR[i_object]);
                                strscd = System.String.Format("{0:f}", scaleC[i_object]);
                                strsd = System.String.Format("{0:f}", scores[i_object]);
                                strtemp = strtemp + "Point" + stri + ":(" + strx + " , " + stry + ") " + "\r\n" + "Model: " + strc + "\r\n" +
                                    "Angle to template:" + strad + "\r\n"  + "ScaleR to template:" + strsrd + "\r\n" +
                                    "ScaleC to template:" + strscd + "\r\n" + "Score:" + strsd + "\r\n" + "\r\n";

                            }



                            secondsTotal = seconds2 - seconds1;

                            string strtd;
                            strtd = System.String.Format("{0:f}", secondsTotal[0].D * 1000.0);

                            str = "Time:" + strtd + "ms" + "\r\n" + strtemp;
                            textBoxResult.Text = str;
                        }
                        else
                        {

                        }
                    }
                    UserCode.GetInstance().isOverFlag[currentIndex] = 22;
                }

                catch (System.Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                Cursor = Cursors.Default;
            }
            else
            {
                MessageBox.Show("错误！未输入图像！");
            }

        }

        private void checkBoxBorderShapeModel_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (checkBoxBorderShapeModel.Checked == true)
                {
                    HOperatorSet.SetSystem("border_shape_models", "true");

                }
                else
                {
                    HOperatorSet.SetSystem("border_shape_models", "false");
                }
            }

            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }

        private void buttonShapeSearchConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                if ((tModelIDs == null) || (tModelIDs.TupleLength() <= 0))
                {
                    MessageBox.Show("No Shape Model!");
                    return;
                }
                UserCode.GetInstance().gProCd[currentIndex].gASSP.Max_Object_Num = (int)numericUpDownTestNumber.Value;
                UserCode.GetInstance().gProCd[currentIndex].gASSP.arrangeIndex = arrangeIndex;
                UserCode.GetInstance().gProCd[currentIndex].gASSP.isBorderShapeModelChecked = checkBoxBorderShapeModel.Checked;
                UserCode.GetInstance().gProCd[currentIndex].gASSP.isMultiplePara = checkBoxMultiplePara.Checked;
                UserCode.GetInstance().gProCd[currentIndex].gASSP.shapeModel = tModelIDs;
                UserCode.GetInstance().gProCd[currentIndex].gASSP.shapeModelImage = templateModelImages;
                UserCode.GetInstance().gProCd[currentIndex].gASSP.shapeModelRegion = templateModelRegions;
                UserCode.GetInstance().gProCd[currentIndex].gASSP.shapeModelPoints.Clear();
                if (modelPoints != null)
                {
                    for (int io = 0; io < modelPoints.Count; io++)
                    {
                        PointF pf = new PointF(0, 0);
                        pf.X = modelPoints[io].X;
                        pf.Y = modelPoints[io].Y;
                        UserCode.GetInstance().gProCd[currentIndex].gASSP.shapeModelPoints.Add(pf);
                    }
                }

                int Max_Object_Num_Temp = UserCode.GetInstance().gProCd[currentIndex].gASSP.Max_Object_Num;
                UserCode.GetInstance().gProCd[currentIndex].gASSP.Max_Object_Num = (int)numericUpDownTestNumber.Value;


                for (int i = 0; i < tModelIDs.TupleLength(); i++)
                {
                    if (modelIfCkecked[i])
                    {
                        UserCode.GetInstance().gProCd[currentIndex].gASSP.modelIsChecked[i] = true;
                    }
                    else
                        UserCode.GetInstance().gProCd[currentIndex].gASSP.modelIsChecked[i] = false;

                }
                if (templateModel != null)
                {

                    if (1 == templateModel.TupleLength())               //单模板
                    {
                        UserCode.GetInstance().gProCd[currentIndex].gASSP.angleStart[MAX_MODEL_MUL_PARA_NUM - 1] = fPara.angleStart;
                        UserCode.GetInstance().gProCd[currentIndex].gASSP.angleExtent[MAX_MODEL_MUL_PARA_NUM - 1] = fPara.angleExtent;
                        UserCode.GetInstance().gProCd[currentIndex].gASSP.scaleRMin[MAX_MODEL_MUL_PARA_NUM - 1] = fPara.scaleRMin;
                        UserCode.GetInstance().gProCd[currentIndex].gASSP.scaleRMax[MAX_MODEL_MUL_PARA_NUM - 1] = fPara.scaleRMax;
                        UserCode.GetInstance().gProCd[currentIndex].gASSP.scaleCMin[MAX_MODEL_MUL_PARA_NUM - 1] = fPara.scaleCMin;
                        UserCode.GetInstance().gProCd[currentIndex].gASSP.scaleCMax[MAX_MODEL_MUL_PARA_NUM - 1] = fPara.scaleCMax;
                        UserCode.GetInstance().gProCd[currentIndex].gASSP.minScore[MAX_MODEL_MUL_PARA_NUM - 1] = fPara.minScore;
                        UserCode.GetInstance().gProCd[currentIndex].gASSP.numMatches[MAX_MODEL_MUL_PARA_NUM - 1] = fPara.numMatches;
                        UserCode.GetInstance().gProCd[currentIndex].gASSP.subPixel[(MAX_MODEL_MUL_PARA_NUM - 1) * 2] = fPara.subPixel[0];
                        UserCode.GetInstance().gProCd[currentIndex].gASSP.subPixel[(MAX_MODEL_MUL_PARA_NUM - 1) * 2 + 1] = fPara.subPixel[1];
                        UserCode.GetInstance().gProCd[currentIndex].gASSP.numLevels[(MAX_MODEL_MUL_PARA_NUM - 1) * 2] = fPara.numLevels[0];
                        UserCode.GetInstance().gProCd[currentIndex].gASSP.numLevels[(MAX_MODEL_MUL_PARA_NUM - 1) * 2 + 1] = fPara.numLevels[1];
                        UserCode.GetInstance().gProCd[currentIndex].gASSP.greediness[MAX_MODEL_MUL_PARA_NUM - 1] = fPara.greediness;
                        UserCode.GetInstance().gProCd[currentIndex].gASSP.maxOverlap = fPara.maxOverlap;
                        UserCode.GetInstance().gProCd[currentIndex].gASSP.Max_Object_Num = fPara.numMatches;

                    }
                    else                                                //多模板;
                    {
                        if (checkBoxMultiplePara.Checked)
                        {
                            UserCode.GetInstance().gProCd[currentIndex].gASSP.maxOverlap = wFparas.maxOverlap;
                            for (int i = 0; i < tModelIDs.TupleLength(); i++)
                            {
                                UserCode.GetInstance().gProCd[currentIndex].gASSP.angleStart[i] = wFparas.angleStart[i];
                                UserCode.GetInstance().gProCd[currentIndex].gASSP.angleExtent[i] = wFparas.angleExtent[i];
                                UserCode.GetInstance().gProCd[currentIndex].gASSP.scaleRMin[i] = wFparas.scaleRMin[i];
                                UserCode.GetInstance().gProCd[currentIndex].gASSP.scaleRMax[i] = wFparas.scaleRMax[i];
                                UserCode.GetInstance().gProCd[currentIndex].gASSP.scaleCMin[i] = wFparas.scaleCMin[i];
                                UserCode.GetInstance().gProCd[currentIndex].gASSP.scaleCMax[i] = wFparas.scaleCMax[i];
                                UserCode.GetInstance().gProCd[currentIndex].gASSP.minScore[i] = wFparas.minScore[i];
                                UserCode.GetInstance().gProCd[currentIndex].gASSP.numMatches[i] = wFparas.numMatches[i];
                                UserCode.GetInstance().gProCd[currentIndex].gASSP.subPixel[2 * i] = wFparas.subPixel[i];
                                UserCode.GetInstance().gProCd[currentIndex].gASSP.subPixel[2 * i + 1] = wFparas.subPixel[2 * i + 1];
                                UserCode.GetInstance().gProCd[currentIndex].gASSP.numLevels[2 * i] = wFparas.numLevels[2 * i];
                                UserCode.GetInstance().gProCd[currentIndex].gASSP.numLevels[2 * i + 1] = wFparas.numLevels[2 * i + 1];
                                UserCode.GetInstance().gProCd[currentIndex].gASSP.greediness[i] = wFparas.greediness[i];

                            }
                            UserCode.GetInstance().gProCd[currentIndex].gASSP.isMultiplePara = true;
                            UserCode.GetInstance().gProCd[currentIndex].gASSP.Max_Object_Num = wFparas.numMatches.Sum();
                        }
                        else
                        {
                            UserCode.GetInstance().gProCd[currentIndex].gASSP.angleStart[MAX_MODEL_MUL_PARA_NUM - 1] = fParasone.angleStart[0];
                            UserCode.GetInstance().gProCd[currentIndex].gASSP.angleExtent[MAX_MODEL_MUL_PARA_NUM - 1] = fParasone.angleExtent[0];
                            UserCode.GetInstance().gProCd[currentIndex].gASSP.scaleRMin[MAX_MODEL_MUL_PARA_NUM - 1] = fParasone.scaleRMin[0];
                            UserCode.GetInstance().gProCd[currentIndex].gASSP.scaleRMax[MAX_MODEL_MUL_PARA_NUM - 1] = fParasone.scaleRMax[0];
                            UserCode.GetInstance().gProCd[currentIndex].gASSP.scaleCMin[MAX_MODEL_MUL_PARA_NUM - 1] = fParasone.scaleCMin[0];
                            UserCode.GetInstance().gProCd[currentIndex].gASSP.scaleCMax[MAX_MODEL_MUL_PARA_NUM - 1] = fParasone.scaleCMax[0];
                            UserCode.GetInstance().gProCd[currentIndex].gASSP.minScore[MAX_MODEL_MUL_PARA_NUM - 1] = fParasone.minScore[0];
                            UserCode.GetInstance().gProCd[currentIndex].gASSP.numMatches[MAX_MODEL_MUL_PARA_NUM - 1] = fParasone.numMatches[0];
                            UserCode.GetInstance().gProCd[currentIndex].gASSP.subPixel[(MAX_MODEL_MUL_PARA_NUM - 1) * 2] = fParasone.subPixel[0];
                            UserCode.GetInstance().gProCd[currentIndex].gASSP.subPixel[(MAX_MODEL_MUL_PARA_NUM - 1) * 2 + 1] = fParasone.subPixel[1];
                            UserCode.GetInstance().gProCd[currentIndex].gASSP.numLevels[(MAX_MODEL_MUL_PARA_NUM - 1) * 2] = fParasone.numLevels[0];
                            UserCode.GetInstance().gProCd[currentIndex].gASSP.numLevels[(MAX_MODEL_MUL_PARA_NUM - 1) * 2 + 1] = fParasone.numLevels[1];
                            UserCode.GetInstance().gProCd[currentIndex].gASSP.greediness[MAX_MODEL_MUL_PARA_NUM - 1] = fParasone.greediness[0];
                            UserCode.GetInstance().gProCd[currentIndex].gASSP.maxOverlap = fParasone.maxOverlap;
                            UserCode.GetInstance().gProCd[currentIndex].gASSP.isMultiplePara = false;
                            UserCode.GetInstance().gProCd[currentIndex].gASSP.Max_Object_Num = fParasone.numMatches[0];
                        }


                    }
                }
                //for (int i = 0; i < checkedListBoxTemplate.Items.Count; i++)
                //{
                //    if (checkedListBoxTemplate.GetItemChecked(i))
                //    {
                //        UserCode.GetInstance().gProCd[currentIndex].gASSP.modelIsChecked[i] = true;
                //    }
                //    else
                //    {
                //        UserCode.GetInstance().gProCd[currentIndex].gASSP.modelIsChecked[i] = false;
                //    }
                //}

                UserCode.GetInstance().gProCd[currentIndex].gASSP.outPutStringList.Clear();
                UserCode.GetInstance().gProCd[currentIndex].gASSP.outPutStringList.Add("识别目标个数");
                for (int i = 0; i < UserCode.GetInstance().gProCd[currentIndex].gASSP.Max_Object_Num; i++)
                {


                    UserCode.GetInstance().gProCd[currentIndex].gASSP.outPutStringList.Add("第" + i.ToString() + "个目标X坐标");
                    UserCode.GetInstance().gProCd[currentIndex].gASSP.outPutStringList.Add("第" + i.ToString() + "个目标Y坐标");
                    UserCode.GetInstance().gProCd[currentIndex].gASSP.outPutStringList.Add("第" + i.ToString() + "个目标X方向放大系数");
                    UserCode.GetInstance().gProCd[currentIndex].gASSP.outPutStringList.Add("第" + i.ToString() + "个目标Y方向放大系数");
                    UserCode.GetInstance().gProCd[currentIndex].gASSP.outPutStringList.Add("第" + i.ToString() + "个目标种类");
                    UserCode.GetInstance().gProCd[currentIndex].gASSP.outPutStringList.Add("第" + i.ToString() + "个目标角度");
                    UserCode.GetInstance().gProCd[currentIndex].gASSP.outPutStringList.Add("第" + i.ToString() + "个目标得分");

                }
                if (Max_Object_Num_Temp != UserCode.GetInstance().gProCd[currentIndex].gASSP.Max_Object_Num)
                {
                    for (int i = 0; i < 20;i++ )
                    {
                        if (UserCode.GetInstance().isOverFlag[i]==32)
                        {
                            if ((ProCodeCls.MainFunction)UserCode.GetInstance().gProCd[i].gSOP.sendDataInfoList[0].funcID == ProCodeCls.MainFunction.MeasureAnisoShapeSearchFBD
                                &&UserCode.GetInstance().gProCd[i].gSOP.sendDataInfoList[0].row==currentIndex)
                            {
                                MessageBox.Show("最多目标个数已被修改，请打开作业编辑中相应串行输出模块更新程序并确认，否则可能引起错误！");
                            }
                        }
                    }
                }
                this.Hide();
            }

            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }

        private void buttonShapeSearchCancel_Click(object sender, EventArgs e)
        {
            this.Hide();
        }


        private void checkBoxSelectNone_CheckedChanged(object sender, EventArgs e)
        {
            checkBoxModelIsOK.Checked = false;
            if (checkBoxSelectNone.Checked == true)
            {
                for (int i = 0; i < templateNumber; i++)
                {
                    checkedListBoxTemplate.SetItemChecked(i, false);
                }
            }
            //UserCode.GetInstance().isOverFlag[currentIndex] = 12;
        }

        private void checkedListBoxTemplate_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (e.CurrentValue == CheckState.Checked && e.NewValue == CheckState.Unchecked)
            {
                checkBoxSelectAll.Checked = false;
            }
            if (e.NewValue == CheckState.Checked && e.CurrentValue == CheckState.Unchecked)
            {
                checkBoxSelectNone.Checked = false;
            }
            checkBoxModelIsOK.Checked = false;
            //UserCode.GetInstance().isOverFlag[currentIndex] = 12;

        }

        private void numericUpDownScaleRMin_ValueChanged(object sender, EventArgs e)
        {
            cPara.scaleRMin = (double)numericUpDownScaleRMin.Value;
            
        }

        private void numericUpDownScaleRMax_ValueChanged(object sender, EventArgs e)
        {
            cPara.scaleRMax = (double)numericUpDownScaleRMax.Value;
            
        }

        

        private void numericUpDownScaleCMin_ValueChanged(object sender, EventArgs e)
        {
            cPara.scaleCMin = (double)numericUpDownScaleCMin.Value;
        }

        private void numericUpDownScaleCMax_ValueChanged(object sender, EventArgs e)
        {
            cPara.scaleCMax = (double)numericUpDownScaleCMax.Value;
        }

        private void radioButtonXLS_CheckedChanged(object sender, EventArgs e)
        {
            //UserCode.GetInstance().isOverFlag[currentIndex] = 12;
            if (radioButtonXLS.Checked)
            {
                arrangeIndex = 0;
            }
        }
        private void radioButtonXSL_CheckedChanged(object sender, EventArgs e)
        {
            //UserCode.GetInstance().isOverFlag[currentIndex] = 12;
            if (radioButtonXSL.Checked)
            {
                arrangeIndex = 1;
            }
        }
        private void radioButtonYLS_CheckedChanged(object sender, EventArgs e)
        {
            //UserCode.GetInstance().isOverFlag[currentIndex] = 12;
            if (radioButtonYLS.Checked)
            {
                arrangeIndex = 2;
            }
        }

        private void radioButtonYSL_CheckedChanged(object sender, EventArgs e)
        {
            //UserCode.GetInstance().isOverFlag[currentIndex] = 12;
            if (radioButtonYSL.Checked)
            {
                arrangeIndex = 3;
            }
        }

        private void numericUpDownTestStartAngle_ValueChanged(object sender, EventArgs e)
        {
           // UserCode.GetInstance().isOverFlag[currentIndex] = 12;
        }

        private void numericUpDownTestExternAngle_ValueChanged(object sender, EventArgs e)
        {
           // UserCode.GetInstance().isOverFlag[currentIndex] = 12;
        }

        private void numericUpDownTestNumber_ValueChanged(object sender, EventArgs e)
        {
           // UserCode.GetInstance().isOverFlag[currentIndex] = 12;
        }

        private void numericUpDownTestMinScore_ValueChanged(object sender, EventArgs e)
        {
            //UserCode.GetInstance().isOverFlag[currentIndex] = 12;
        }

        private void numericUpDownTestScaleRMin_ValueChanged(object sender, EventArgs e)
        {
           // UserCode.GetInstance().isOverFlag[currentIndex] = 12;
        }

        private void numericUpDownTestScaleCMin_ValueChanged(object sender, EventArgs e)
        {
           // UserCode.GetInstance().isOverFlag[currentIndex] = 12;
        }

        private void numericUpDownTestScaleRMax_ValueChanged(object sender, EventArgs e)
        {
           // UserCode.GetInstance().isOverFlag[currentIndex] = 12;
        }

        private void numericUpDownTestScaleCMax_ValueChanged(object sender, EventArgs e)
        {
           // UserCode.GetInstance().isOverFlag[currentIndex] = 12;
        }

        private void numericUpDownTestOverLap_ValueChanged(object sender, EventArgs e)
        {
          //  UserCode.GetInstance().isOverFlag[currentIndex] = 12;
        }

        private void numericUpDownTestGreediness_ValueChanged(object sender, EventArgs e)
        {
           // UserCode.GetInstance().isOverFlag[currentIndex] = 12;
        }

        private void comboBoxSubPixel1_SelectedIndexChanged(object sender, EventArgs e)
        {
           // UserCode.GetInstance().isOverFlag[currentIndex] = 12;
        }

        private void comboBoxSubPixel2_SelectedIndexChanged(object sender, EventArgs e)
        {
           // UserCode.GetInstance().isOverFlag[currentIndex] = 12;
        }

        private void buttonDeleteTemplate_Click(object sender, EventArgs e)
        {
            try
            {
                bool isSelected = false;
                for (int i = 0; i < templateNumber; i++)
                {
                    if (checkedListBoxTemplate.GetSelected(i))
                    {
                        isSelected = true;
                    }
                }
                if (isSelected)
                {
                    HTuple idxNew = null;
                    for (int i = 0; i < templateNumber; i++)
                    {
                        if (checkedListBoxTemplate.GetSelected(i))
                        {
                            modelPoints.RemoveAt(i);
                            tModelIDs = tModelIDs.TupleRemove(i);
                        }
                        else
                        {
                            if (idxNew == null)
                            {
                                idxNew = i + 1;
                            }
                            else
                            {
                                idxNew = idxNew.TupleConcat(i + 1);
                            }
                        }
                    }
                    checkedListBoxTemplate.Items.RemoveAt(templateNumber - 1);
                    templateNumber = templateNumber - 1;
                    if (idxNew == null)
                    {
                        if (templateModelImages != null)
                        {
                            templateModelImages.Dispose();
                        }
                        if (templateModelRegions != null)
                        {
                            templateModelRegions.Dispose();
                        }
                    }
                    else
                    {
                        HOperatorSet.SelectObj(templateModelImages, out templateModelImages, idxNew);
                        HOperatorSet.SelectObj(templateModelRegions, out templateModelRegions, idxNew);
                    }
                    HOperatorSet.ClearWindow(findShapeModelHWHandle);
                    HOperatorSet.ClearWindow(findShapeModelPartHWHandle);
                    UserCode.GetInstance().isOverFlag[currentIndex] = 12;
                }
                else
                {
                    throw new Exception("未选中待删除模版！");
                }

            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }








        


    }
}
