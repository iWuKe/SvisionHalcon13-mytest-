using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HalconDotNet;
namespace Svision
{
    public partial class BlobAnalysisFBDForm : Form
    {
        private int currentIndex;
        HTuple BlobHWHandle, BlobPartHWHandle;
        public HObject image, image1, image2, image3, imageTemp, imageTempShow, imageTempShowWithOutPoint,/*,imageRealRegion, imageDisplayPartRegion,*/ img, imgPart, imgPartTemp, rectDomain;
        public HObject thresholdTestRegion, thresholdGrayRegion, thresholdColorRegion, segmentRegion;
        public HObject morphologyTestRegion, morphologyRegion, selectedTestRegion, selectedRegion;
        //HTuple   sortedFeatureValueIndices;
        public HObject thresholdGrayRegionPart1, thresholdGrayRegionPart2, thresholdGrayRegionPart3, thresholdGrayRegionPart4, thresholdGrayRegionPart5, thresholdGrayRegionPart6;
        public HObject thresholdColorRegionPart1, thresholdColorRegionPart2, thresholdColorRegionPart3, thresholdColorRegionPart4, thresholdColorRegionPart5, thresholdColorRegionPart6;
        double resizerate = 1;
        Point oriLocation;
        PointF centerLocation;
        int rowNum, columnNum;
        int rowOriNum, columnOriNum;
        double resRat;
        public BlobAnalysisFBDForm(int idx)
        {
            try
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
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        ~BlobAnalysisFBDForm()
        {
            if (image != null)
            {
                image.Dispose();
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

        }
        public class selectPara
        {
            public string[] selectSTR;
            public bool[] selectIsChecked = new bool[34];
            //public bool[] selectisAnd = new bool[34];
            public bool selectisAnd;
            public double[] selectMin = new double[34];
            public double[] selectMax = new double[34];
            public selectPara()
            {
                selectSTR = new string[34]{/*select shape*/"area","row","column","width","height","row1","column1","row2","column2","circularity","compactness","contlength","convexity"
                             ,"rectangularity","ra","rb","phi","anisometry","outer_radius","inner_radius","inner_width","inner_height","num_sides","holes_num",
                             "area_holes","max_diameter","orientation","rect2_phi","rect2_len1","rect2_len2",/*select gray*/"min","max","mean","deviation"    };
                selectisAnd = true;
                for (int i = 0; i < 34; i++)
                {
                    selectIsChecked[i] = false;

                    selectMin[i] = 0;
                    selectMax[i] = 99999999.99;
                }
            }
        }
        selectPara sP = new selectPara();
        public class ColorGraySegmentPara
        {
            public bool[] colorSelectNum = new bool[6];
            public int[] redColor = new int[6];
            public int[] greenColor = new int[6];
            public int[] blueColor = new int[6];
            public int[] grayValue = new int[6];
            public int[] colorLength = new int[6];
            public bool[] isBesideThisColor = new bool[6];
            public ColorGraySegmentPara()
            {
                for (int i = 0; i < 6; i++)
                {
                    colorSelectNum[i] = false;
                    redColor[i] = 0;
                    greenColor[i] = 0;
                    blueColor[i] = 0;
                    grayValue[i] = 0;
                    colorLength[i] = 0;
                    isBesideThisColor[i] = false;
                }
            }
        }
        ColorGraySegmentPara cSP = new ColorGraySegmentPara();
        private void BparaInit()
        {
            try
            {
                //Segment
                if (Svision.GetMe().baslerCamera.getChannelNumber() == 1)
                {
                    UserCode.GetInstance().gProCd[currentIndex].gBP.isColor = false;
                }
                else
                {
                    UserCode.GetInstance().gProCd[currentIndex].gBP.isColor = true;//isColor
                }
                for (int i = 0; i < 6; i++)
                {
                    if (UserCode.GetInstance().gProCd[currentIndex].gBP.selectedColor[i] == true)
                    {
                        cSP.colorSelectNum[i] = true;
                        checkedListBoxSelectColorGray.SetItemChecked(i, true);
                        cSP.redColor[i] = UserCode.GetInstance().gProCd[currentIndex].gBP.redValue[i];
                        cSP.greenColor[i] = UserCode.GetInstance().gProCd[currentIndex].gBP.greenValue[i];
                        cSP.blueColor[i] = UserCode.GetInstance().gProCd[currentIndex].gBP.blueValue[i];
                        cSP.grayValue[i] = UserCode.GetInstance().gProCd[currentIndex].gBP.grayValue[i];
                        cSP.colorLength[i] = UserCode.GetInstance().gProCd[currentIndex].gBP.lengthValue[i];
                        cSP.isBesideThisColor[i] = UserCode.GetInstance().gProCd[currentIndex].gBP.isBesideThisColor[i];

                    }
                }
                numericUpDownPara21.Maximum = (decimal)Math.Min(rowOriNum, columnOriNum);
                checkBoxAuto1.Checked = UserCode.GetInstance().gProCd[currentIndex].gBP.isAutoSegmentMethod1;
                if (checkBoxAuto1.Checked == true)
                {
                    numericUpDownPara11.Enabled = true;
                }
                else
                {
                    numericUpDownPara11.Enabled = false;
                }
                checkBoxAuto2.Checked = UserCode.GetInstance().gProCd[currentIndex].gBP.isAutoSegmentMethod2;
                if (checkBoxAuto2.Checked == true)
                {
                    panelAutoSegmentMethod2.Enabled = true;
                }
                else
                {
                    panelAutoSegmentMethod2.Enabled = false;
                }

                numericUpDownPara11.Value = (decimal)UserCode.GetInstance().gProCd[currentIndex].gBP.autoSegmentMethod1Para1;
                numericUpDownPara21.Value = (decimal)UserCode.GetInstance().gProCd[currentIndex].gBP.autoSegmentMethod2Para1;
                numericUpDownPara22.Value = (decimal)UserCode.GetInstance().gProCd[currentIndex].gBP.autoSegmentMethod2Para2;
                numericUpDownPara23.Value = (decimal)UserCode.GetInstance().gProCd[currentIndex].gBP.autoSegmentMethod2Para3;
                comboBoxPara24.SelectedIndex = (int)UserCode.GetInstance().gProCd[currentIndex].gBP.autoSegmentMethod2Para4;
                checkBoxAuto3.Checked = UserCode.GetInstance().gProCd[currentIndex].gBP.isAutoSegmentMethod3;
                if (UserCode.GetInstance().gProCd[currentIndex].gBP.isAutoSegment == true)
                {
                    checkBoxIsAutoSegment.Checked = true;
                    comboBoxSetColorGray.SelectedIndex = 0;
                    radioButtonSegmentShowWholeImage.Checked = true;
                    radioButtonSegmentShowPointImage.Checked = false;
                    radioButtonSegmentShowPointImage.Enabled = false;

                }
                else
                {
                    checkBoxIsAutoSegment.Checked = false;
                    groupBoxColorGraySegment.Enabled = true;
                    //groupBoxGraySegment.Enabled = false;

                    checkedListBoxSelectColorGray.SetSelected(0, true);
                    comboBoxSetColorGray.SelectedIndex = 0;
                    comboBoxPara24.SelectedIndex = 0;
                    radioButtonSegmentShowPointImage.Enabled = true;
                    radioButtonSegmentShowPointImage.Checked = true;
                    if (UserCode.GetInstance().gProCd[currentIndex].gBP.isColor == true)
                    {
                        panelRGB.Enabled = true;
                        panelGray.Enabled = false;
                        numericUpDownLength.Maximum =
                   Math.Min(Math.Min(Math.Min(Math.Abs(numericUpDownRed.Value - numericUpDownRed.Minimum), Math.Abs(numericUpDownRed.Value - numericUpDownRed.Maximum))
                   , Math.Min(Math.Abs(numericUpDownGreen.Value - numericUpDownGreen.Minimum), Math.Abs(numericUpDownGreen.Value - numericUpDownGreen.Maximum))),
                   Math.Min(Math.Abs(numericUpDownBlue.Value - numericUpDownBlue.Minimum), Math.Abs(numericUpDownBlue.Value - numericUpDownBlue.Maximum)));
                    }
                    else
                    {
                        panelRGB.Enabled = false;
                        panelGray.Enabled = true;
                        numericUpDownLength.Maximum = numericUpDownGray.Value;


                    }
                }
                //Morphology
                if (UserCode.GetInstance().gProCd[currentIndex].gBP.isFillUpHoles)
                {
                    checkBoxFillUpHoles.Checked = true;

                }
                else
                {
                    checkBoxFillUpHoles.Checked = false;
                }
                if (UserCode.GetInstance().gProCd[currentIndex].gBP.isConnectionBeforeFillUpHoles)
                {
                    radioButtonOnlyFillUpHoles.Checked = false;
                }
                else
                {
                    radioButtonOnlyFillUpHoles.Checked = true;
                }
                if (UserCode.GetInstance().gProCd[currentIndex].gBP.isErosion)
                {
                    checkBoxErosion.Checked = true;
                    if (UserCode.GetInstance().gProCd[currentIndex].gBP.erosionElementNUM == 0)
                    {
                        radioButtonRectangleErosion.Checked = true;
                        numericUpDownWidthErosion.Value = (decimal)UserCode.GetInstance().gProCd[currentIndex].gBP.erosionRWidth;
                        numericUpDownHeightErosion.Value = (decimal)UserCode.GetInstance().gProCd[currentIndex].gBP.erosionRHeight;
                    }
                    else
                    {
                        radioButtonCircleErosion.Checked = true;
                        numericUpDownRadiusErosion.Value = (decimal)UserCode.GetInstance().gProCd[currentIndex].gBP.erosionCRadius;
                    }
                }
                else
                {
                    checkBoxErosion.Checked = false;
                }
                if (UserCode.GetInstance().gProCd[currentIndex].gBP.isDilation)
                {
                    checkBoxDilation.Checked = true;
                    if (UserCode.GetInstance().gProCd[currentIndex].gBP.dilationElementNUM == 0)
                    {
                        radioButtonRectangleDilation.Checked = true;
                        numericUpDownWidthDilation.Value = (decimal)UserCode.GetInstance().gProCd[currentIndex].gBP.dilationRWidth;
                        numericUpDownHeightDilation.Value = (decimal)UserCode.GetInstance().gProCd[currentIndex].gBP.dilationRHeight;
                    }
                    else
                    {
                        radioButtonCircleDilation.Checked = true;
                        numericUpDownRadiusDilation.Value = (decimal)UserCode.GetInstance().gProCd[currentIndex].gBP.dilationCRadius;
                    }
                }
                else
                {
                    checkBoxDilation.Checked = false;
                }
                if (UserCode.GetInstance().gProCd[currentIndex].gBP.isOpening)
                {
                    checkBoxOpening.Checked = true;
                    if (UserCode.GetInstance().gProCd[currentIndex].gBP.openingElementNUM == 0)
                    {
                        radioButtonRectangleOpening.Checked = true;
                        numericUpDownWidthOpening.Value = (decimal)UserCode.GetInstance().gProCd[currentIndex].gBP.openingRWidth;
                        numericUpDownHeightOpening.Value = (decimal)UserCode.GetInstance().gProCd[currentIndex].gBP.openingRHeight;
                    }
                    else
                    {
                        radioButtonCircleOpening.Checked = true;
                        numericUpDownRadiusOpening.Value = (decimal)UserCode.GetInstance().gProCd[currentIndex].gBP.openingCRadius;
                    }
                }
                else
                {
                    checkBoxOpening.Checked = false;
                }
                if (UserCode.GetInstance().gProCd[currentIndex].gBP.isClosing)
                {
                    checkBoxClosing.Checked = true;
                    if (UserCode.GetInstance().gProCd[currentIndex].gBP.closingElementNUM == 0)
                    {
                        radioButtonRectangleClosing.Checked = true;
                        numericUpDownWidthClosing.Value = (decimal)UserCode.GetInstance().gProCd[currentIndex].gBP.closingRWidth;
                        numericUpDownHeightClosing.Value = (decimal)UserCode.GetInstance().gProCd[currentIndex].gBP.closingRHeight;
                    }
                    else
                    {
                        radioButtonCircleClosing.Checked = true;
                        numericUpDownRadiusClosing.Value = (decimal)UserCode.GetInstance().gProCd[currentIndex].gBP.closingCRadius;
                    }
                }
                else
                {
                    checkBoxClosing.Checked = false;
                }

                //Select

                if (UserCode.GetInstance().gProCd[currentIndex].gBP.selectisAnd)
                {
                    radioButtonSelectAnd.Checked = true;
                }
                else
                {
                    radioButtonSelectOR.Checked = true;
                }
                for (int i = 0; i < 34; i++)
                {
                    sP.selectIsChecked[i] = UserCode.GetInstance().gProCd[currentIndex].gBP.selectIsChecked[i];
                    sP.selectMin[i] = UserCode.GetInstance().gProCd[currentIndex].gBP.selectMin[i];
                    sP.selectMax[i] = UserCode.GetInstance().gProCd[currentIndex].gBP.selectMax[i];
                    //sP.selectisAnd[i] = UserCode.GetInstance().gProCd[currentIndex].gBP.selectisAnd[i];
                    if (sP.selectIsChecked[i])
                    {
                        checkedListBoxSelectItem.SetItemChecked(i, true);
                    }
                }
                checkedListBoxSelectItem.SetSelected(0, true);
                comboBoxSelectItem.SelectedIndex = 0;
                numericUpDownMaxSetValue.Value = (decimal)sP.selectMax[comboBoxSelectItem.SelectedIndex];
                numericUpDownMinSetValue.Value = (decimal)sP.selectMin[comboBoxSelectItem.SelectedIndex];
                comboBoxArrange.SelectedIndex = UserCode.GetInstance().gProCd[currentIndex].gBP.selectArrangeItemIndex;
                if (UserCode.GetInstance().gProCd[currentIndex].gBP.isArrangeLtoS)
                {

                }
                else
                {
                    radioButtonStoL.Checked = true;
                }
                numericUpDownRegionMaxNumber.Value = (decimal)UserCode.GetInstance().gProCd[currentIndex].gBP.regionNum;
                //Output
                for (int i = 0; i < 34; i++)
                {

                    if (UserCode.GetInstance().gProCd[currentIndex].gBP.outputIDIsChecked[i])
                    {
                        checkedListBoxOutputSet.SetItemChecked(i, true);
                    }
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);

            }

        }
        private void BlobAnalysisFBDForm_Load(object sender, EventArgs e)
        {
            try
            {
                HOperatorSet.OpenWindow(0, 0, 256, 192, BlobPictureBox.Handle, "visible", "", out BlobHWHandle);
                HOperatorSet.SetPart(BlobHWHandle, 0, 0, 191, 255);
                HDevWindowStack.Push(BlobHWHandle);
                //HOperatorSet.SetLineWidth(findShapeModelHWHandle, 1);
                //HOperatorSet.SetColor(findShapeModelHWHandle, "green");
                HOperatorSet.OpenWindow(0, 0, (BlobPartPictureBox.Width), (BlobPartPictureBox.Height), BlobPartPictureBox.Handle, "visible", "", out BlobPartHWHandle);
                HOperatorSet.SetPart(BlobPartHWHandle, 0, 0, (BlobPartPictureBox.Height - 1), (BlobPartPictureBox.Width - 1));
                HDevWindowStack.Push(BlobPartHWHandle);

                this.BlobPictureBox.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.BlobPictureBox_MouseWheel);

                BparaInit();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }
        private void BlobPictureBox_MouseWheel(object sender, MouseEventArgs e)
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
                    if (rowNum * resizerate < (BlobPartPictureBox.Height) || columnNum * resizerate < (BlobPartPictureBox.Width))
                    {
                        basicClass.displayClear(BlobPartHWHandle);
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
                    if (oriLocation.Y * resizerate / resRat - ((float)(BlobPartPictureBox.Height - 1 + 0) / 2.0) < 0 && oriLocation.X * resizerate / resRat - ((float)(BlobPartPictureBox.Width - 1 + 0) / 2.0) < 0)
                    {
                        HObject temp;
                        basicClass.genRectangle1(out rectDomain, 0, 0, (BlobPartPictureBox.Height - 1), (BlobPartPictureBox.Width - 1));
                        centerLocation.Y = (float)(((float)(System.Math.Min(BlobPartPictureBox.Height, rowNum * resizerate) - 1 + 0) / 2.0) * resRat / resizerate);
                        centerLocation.X = (float)(((float)(System.Math.Min(BlobPartPictureBox.Width, columnNum * resizerate) - 1 + 0) / 2.0) * resRat / resizerate);

                        basicClass.reduceDomain(imgPart, rectDomain, out temp);
                        if (imgPartTemp != null)
                        {
                            imgPartTemp.Dispose();
                        }
                        HOperatorSet.CropDomain(temp, out imgPartTemp);
                        HOperatorSet.DispObj(imgPartTemp, BlobPartHWHandle);
                        temp.Dispose();
                    }
                    else if (oriLocation.Y * resizerate / resRat - ((float)(BlobPartPictureBox.Height - 1 + 0) / 2.0) <= 0 && oriLocation.X * resizerate / resRat - ((float)(BlobPartPictureBox.Width - 1 + 0) / 2.0) >= 0 && oriLocation.X * resizerate / resRat + ((float)(BlobPartPictureBox.Width - 1 + 0) / 2.0) < columnNum * resizerate - 1)
                    {
                        HObject temp;
                        basicClass.genRectangle1(out rectDomain, 0, oriLocation.X * resizerate / resRat - ((float)(BlobPartPictureBox.Width - 1 + 0) / 2.0), (BlobPartPictureBox.Height - 1), oriLocation.X * resizerate / resRat + ((float)(BlobPartPictureBox.Width - 1 + 0) / 2.0));
                        centerLocation.Y = (float)(((float)(System.Math.Min(BlobPartPictureBox.Height, rowNum * resizerate) - 1 + 0) / 2.0) * resRat / resizerate);
                        centerLocation.X = (float)oriLocation.X;
                        basicClass.reduceDomain(imgPart, rectDomain, out temp);
                        if (imgPartTemp != null)
                        {
                            imgPartTemp.Dispose();
                        }
                        HOperatorSet.CropDomain(temp, out imgPartTemp);
                        HOperatorSet.DispObj(imgPartTemp, BlobPartHWHandle);
                        temp.Dispose();
                    }
                    else if (oriLocation.Y * resizerate / resRat - ((float)(BlobPartPictureBox.Height - 1 + 0) / 2.0) > 0 && oriLocation.Y * resizerate / resRat + ((float)(BlobPartPictureBox.Height - 1 + 0) / 2.0) <= rowNum * resizerate - 1 && oriLocation.X * resizerate / resRat - ((float)(BlobPartPictureBox.Width - 1 + 0) / 2.0) < 0)
                    {
                        HObject temp;
                        basicClass.genRectangle1(out rectDomain, oriLocation.Y * resizerate / resRat - ((float)(BlobPartPictureBox.Height - 1 + 0) / 2.0), 0, oriLocation.Y * resizerate / resRat + ((float)(BlobPartPictureBox.Height - 1 + 0) / 2.0), (BlobPartPictureBox.Width - 1));
                        centerLocation.Y = (float)oriLocation.Y;
                        centerLocation.X = (float)(((float)(System.Math.Min(BlobPartPictureBox.Width, columnNum * resizerate) - 1 + 0) / 2.0) * resRat / resizerate);
                        basicClass.reduceDomain(imgPart, rectDomain, out temp);
                        if (imgPartTemp != null)
                        {
                            imgPartTemp.Dispose();
                        }
                        HOperatorSet.CropDomain(temp, out imgPartTemp);
                        HOperatorSet.DispObj(imgPartTemp, BlobPartHWHandle);
                        temp.Dispose();
                    }
                    else if (oriLocation.Y * resizerate / resRat + ((float)(BlobPartPictureBox.Height - 1 + 0) / 2.0) > rowNum * resizerate - 1 && oriLocation.X * resizerate / resRat - ((float)(BlobPartPictureBox.Width - 1 + 0) / 2.0) < 0)
                    {
                        HObject temp;
                        basicClass.genRectangle1(out rectDomain, rowNum * resizerate - 1 - (BlobPartPictureBox.Height - 1), 0, rowNum * resizerate - 1, (BlobPartPictureBox.Width - 1));
                        centerLocation.Y = (float)((rowNum * resizerate - ((float)(System.Math.Min(BlobPartPictureBox.Height, rowNum * resizerate) - 1 + 0) / 2.0) - 1) * resRat / resizerate);
                        centerLocation.X = (float)(((float)(System.Math.Min(BlobPartPictureBox.Width, columnNum * resizerate) - 1 + 0) / 2.0) * resRat / resizerate);
                        basicClass.reduceDomain(imgPart, rectDomain, out temp);
                        if (imgPartTemp != null)
                        {
                            imgPartTemp.Dispose();
                        }
                        HOperatorSet.CropDomain(temp, out imgPartTemp);
                        HOperatorSet.DispObj(imgPartTemp, BlobPartHWHandle);
                        temp.Dispose();
                    }
                    else if (oriLocation.Y * resizerate / resRat + ((float)(BlobPartPictureBox.Height - 1 + 0) / 2.0) > rowNum * resizerate - 1 && oriLocation.X * resizerate / resRat - ((float)(BlobPartPictureBox.Width - 1 + 0) / 2.0) > 0 && oriLocation.X * resizerate / resRat + ((float)(BlobPartPictureBox.Width - 1 + 0) / 2.0) < columnNum * resizerate - 1)
                    {
                        HObject temp;
                        basicClass.genRectangle1(out rectDomain, rowNum * resizerate - (BlobPartPictureBox.Height), oriLocation.X * resizerate / resRat - ((float)(BlobPartPictureBox.Width - 1 + 0) / 2.0), rowNum * resizerate - 1, oriLocation.X * resizerate / resRat + ((float)(BlobPartPictureBox.Width - 1 + 0) / 2.0));
                        centerLocation.Y = (float)((rowNum * resizerate - ((float)(System.Math.Min(BlobPartPictureBox.Height, rowNum * resizerate) - 1 + 0) / 2.0 + 1)) * resRat / resizerate);
                        centerLocation.X = (float)oriLocation.X;
                        basicClass.reduceDomain(imgPart, rectDomain, out temp);
                        if (imgPartTemp != null)
                        {
                            imgPartTemp.Dispose();
                        }
                        HOperatorSet.CropDomain(temp, out imgPartTemp);
                        HOperatorSet.DispObj(imgPartTemp, BlobPartHWHandle);
                        temp.Dispose();
                    }
                    else if (oriLocation.Y * resizerate / resRat + ((float)(BlobPartPictureBox.Height - 1 + 0) / 2.0) >= rowNum * resizerate - 1 && oriLocation.X * resizerate / resRat + ((float)(BlobPartPictureBox.Width - 1 + 0) / 2.0) >= columnNum * resizerate - 1)
                    {
                        HObject temp;
                        basicClass.genRectangle1(out rectDomain, rowNum * resizerate - (BlobPartPictureBox.Height), columnNum * resizerate - (BlobPartPictureBox.Width), rowNum * resizerate - 1, columnNum * resizerate - 1);
                        centerLocation.Y = (float)((rowNum * resizerate - ((float)(System.Math.Min(BlobPartPictureBox.Height, rowNum * resizerate) - 1 + 0) / 2.0 + 1)) * resRat / resizerate);
                        centerLocation.X = (float)((columnNum * resizerate - ((float)(System.Math.Min(BlobPartPictureBox.Width, columnNum * resizerate) - 1 + 0) / 2.0 + 1)) * resRat / resizerate);
                        basicClass.reduceDomain(imgPart, rectDomain, out temp);
                        if (imgPartTemp != null)
                        {
                            imgPartTemp.Dispose();
                        }
                        HOperatorSet.CropDomain(temp, out imgPartTemp);
                        HOperatorSet.DispObj(imgPartTemp, BlobPartHWHandle);
                        temp.Dispose();
                    }
                    else if (oriLocation.Y * resizerate / resRat + ((float)(BlobPartPictureBox.Height - 1 + 0) / 2.0) < rowNum * resizerate - 1 && oriLocation.Y * resizerate / resRat - ((float)(BlobPartPictureBox.Height - 1 + 0) / 2.0) > 0 && oriLocation.X * resizerate / resRat + ((float)(BlobPartPictureBox.Width - 1 + 0) / 2.0) > columnNum * resizerate - 1)
                    {
                        HObject temp;
                        basicClass.genRectangle1(out rectDomain, oriLocation.Y * resizerate / resRat - ((float)(BlobPartPictureBox.Height - 1 + 0) / 2.0), columnNum * resizerate - (BlobPartPictureBox.Width), oriLocation.Y * resizerate / resRat + ((float)(BlobPartPictureBox.Height - 1 + 0) / 2.0), columnNum * resizerate - 1);
                        centerLocation.Y = (float)oriLocation.Y;
                        centerLocation.X = (float)((columnNum * resizerate - ((float)(System.Math.Min(BlobPartPictureBox.Width, columnNum * resizerate) - 1 + 0) / 2.0 + 1)) * resRat / resizerate);
                        basicClass.reduceDomain(imgPart, rectDomain, out temp);
                        if (imgPartTemp != null)
                        {
                            imgPartTemp.Dispose();
                        }
                        HOperatorSet.CropDomain(temp, out imgPartTemp);
                        HOperatorSet.DispObj(imgPartTemp, BlobPartHWHandle);
                        temp.Dispose();
                    }
                    else if (oriLocation.Y * resizerate / resRat - ((float)(BlobPartPictureBox.Height - 1 + 0) / 2.0) <= 0 && oriLocation.X * resizerate / resRat + ((float)(BlobPartPictureBox.Width - 1 + 0) / 2.0) >= columnNum * resizerate - 1)
                    {
                        HObject temp;
                        basicClass.genRectangle1(out rectDomain, 0, columnNum * resizerate - (BlobPartPictureBox.Width), (BlobPartPictureBox.Height - 1), columnNum * resizerate - 1);
                        centerLocation.Y = (float)(((float)(System.Math.Min(BlobPartPictureBox.Height, rowNum * resizerate) - 1 + 0) / 2.0) * resRat / resizerate);
                        centerLocation.X = (float)((columnNum * resizerate - ((float)(System.Math.Min(BlobPartPictureBox.Width, columnNum * resizerate) - 1 + 0) / 2.0 + 1)) * resRat / resizerate);
                        basicClass.reduceDomain(imgPart, rectDomain, out temp);
                        if (imgPartTemp != null)
                        {
                            imgPartTemp.Dispose();
                        }
                        HOperatorSet.CropDomain(temp, out imgPartTemp);
                        HOperatorSet.DispObj(imgPartTemp, BlobPartHWHandle);
                        temp.Dispose();
                    }
                    else
                    {
                        HObject temp;
                        basicClass.genRectangle1(out rectDomain, oriLocation.Y * resizerate / resRat - ((float)(BlobPartPictureBox.Height - 1 + 0) / 2.0), oriLocation.X * resizerate / resRat - ((float)(BlobPartPictureBox.Width - 1 + 0) / 2.0), oriLocation.Y * resizerate / resRat + ((float)(BlobPartPictureBox.Height - 1 + 0) / 2.0), oriLocation.X * resizerate / resRat + ((float)(BlobPartPictureBox.Width - 1 + 0) / 2.0));
                        basicClass.reduceDomain(imgPart, rectDomain, out temp);
                        if (imgPartTemp != null)
                        {
                            imgPartTemp.Dispose();
                        }
                        HOperatorSet.CropDomain(temp, out imgPartTemp);
                        HOperatorSet.DispObj(imgPartTemp, BlobPartHWHandle);
                        temp.Dispose();
                    }
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
         private void BlobSegmentImageShow()
        {
            if (image != null)
            {
                if (checkBoxIsAutoSegment.Checked == true)
                {
                    if (checkBoxAuto1.Checked == true)
                    {
                        if (radioButtonSegmentShowOriImage.Checked == true)
                        {
                            if (imageTempShow != null)
                            {
                                imageTempShow.Dispose();
                            }
                            HOperatorSet.CopyImage(image, out imageTempShow);
                        }
                        else
                        {
                            //HObject thresholdTestRegion;
                            HOperatorSet.AutoThreshold(image, out thresholdTestRegion, (double)numericUpDownPara11.Value);
                            if (imageTempShow != null)
                            {
                                imageTempShow.Dispose();
                            }
                            HOperatorSet.RegionToMean(thresholdTestRegion, image, out imageTempShow);

                            //HOperatorSet.BitAnd(image, thresholdTestRegionimg, out imageTempShow);
                            //if (thresholdTestRegionimg != null)
                            //{
                            //    thresholdTestRegionimg.Dispose();
                            //}
                        }
                    }
                    else if (checkBoxAuto2.Checked == true)
                    {
                        if (radioButtonSegmentShowOriImage.Checked == true)
                        {
                            if (imageTempShow != null)
                            {
                                imageTempShow.Dispose();
                            }
                            HOperatorSet.CopyImage(image, out imageTempShow);
                        }
                        else
                        {
                            HObject thresholdTestRegionimg, thresholdTestRegionimg3;

                            string[] Para34List = new string[4] { "dark", "equal", "light", "not_equal " };
                            HOperatorSet.VarThreshold(image, out thresholdTestRegion, (int)numericUpDownPara21.Value, (int)numericUpDownPara21.Value, (double)numericUpDownPara22.Value, (double)numericUpDownPara23.Value, (HTuple)Para34List[(int)comboBoxPara24.SelectedIndex]);
                            HOperatorSet.RegionToBin(thresholdTestRegion, out thresholdTestRegionimg, 255, 0, columnNum, rowNum);
                            HOperatorSet.Compose3(thresholdTestRegionimg, thresholdTestRegionimg, thresholdTestRegionimg, out thresholdTestRegionimg3);
                            if (imageTempShow != null)
                            {
                                imageTempShow.Dispose();
                            }
                            HOperatorSet.BitAnd(imageTemp, thresholdTestRegionimg3, out imageTempShow);
                            if (thresholdTestRegionimg != null)
                            {
                                thresholdTestRegionimg.Dispose();
                            }
                            if (thresholdTestRegionimg3 != null)
                            {
                                thresholdTestRegionimg3.Dispose();
                            }
                        }
                    }
                    else if (checkBoxAuto3.Checked == true)
                    {
                        if (radioButtonSegmentShowOriImage.Checked == true)
                        {
                            if (imageTempShow != null)
                            {
                                imageTempShow.Dispose();
                            }
                            HOperatorSet.CopyImage(image, out imageTempShow);
                        }
                        else
                        {
                            HObject thresholdTestRegionimg, thresholdTestRegionimg3;
                            HOperatorSet.BinThreshold(image, out thresholdTestRegion);
                            HOperatorSet.RegionToBin(thresholdTestRegion, out thresholdTestRegionimg, 255, 0, columnNum, rowNum);
                            HOperatorSet.Compose3(thresholdTestRegionimg, thresholdTestRegionimg, thresholdTestRegionimg, out thresholdTestRegionimg3);
                            if (imageTempShow != null)
                            {
                                imageTempShow.Dispose();
                            }
                            HOperatorSet.BitAnd(imageTemp, thresholdTestRegionimg3, out imageTempShow);
                            if (thresholdTestRegionimg != null)
                            {
                                thresholdTestRegionimg.Dispose();
                            }
                            if (thresholdTestRegionimg3 != null)
                            {
                                thresholdTestRegionimg3.Dispose();
                            }
                        }
                    }
                    else
                    {
                        //MessageBox.Show("Error!Wrong auto segment set!");
                    }
                }
                else
                {
                    if (UserCode.GetInstance().gProCd[currentIndex].gBP.isColor == true)
                    {
                        if (radioButtonSegmentShowOriImage.Checked == true)
                        {
                            if (imageTempShow != null)
                            {
                                imageTempShow.Dispose();
                            }
                            HOperatorSet.CopyImage(image, out imageTempShow);
                        }
                        else if (radioButtonSegmentShowWholeImage.Checked == true)
                        {


                            for (int i = 0; i < 6; i++)
                            {
                                if (checkedListBoxSelectColorGray.GetItemChecked(i))
                                {
                                    HObject thresholdColorTestRegion1, thresholdColorTestRegion2, thresholdColorTestRegion3;
                                    HOperatorSet.GenEmptyObj(out thresholdColorTestRegion1);
                                    HOperatorSet.GenEmptyObj(out thresholdColorTestRegion2);
                                    HOperatorSet.GenEmptyObj(out thresholdColorTestRegion3);
                                    switch (i)
                                    {
                                        case 0:

                                            HOperatorSet.Threshold(image1, out thresholdColorTestRegion1, cSP.redColor[0] - cSP.colorLength[0], cSP.redColor[0] + cSP.colorLength[0]);
                                            HOperatorSet.Threshold(image2, out thresholdColorTestRegion2, cSP.greenColor[0] - cSP.colorLength[0], cSP.greenColor[0] + cSP.colorLength[0]);
                                            HOperatorSet.Threshold(image3, out thresholdColorTestRegion3, cSP.blueColor[0] - cSP.colorLength[0], cSP.blueColor[0] + cSP.colorLength[0]);
                                            HOperatorSet.Intersection(thresholdColorTestRegion1, thresholdColorTestRegion2, out thresholdColorRegionPart1);
                                            HOperatorSet.Intersection(thresholdColorTestRegion3, thresholdColorRegionPart1, out thresholdColorRegionPart1);
                                            if (cSP.isBesideThisColor[0])
                                            {
                                                HOperatorSet.Complement(thresholdColorRegionPart1, out thresholdColorRegionPart1);
                                            }
                                            break;
                                        case 1:
                                            HOperatorSet.Threshold(image1, out thresholdColorTestRegion1, cSP.redColor[1] - cSP.colorLength[1], cSP.redColor[1] + cSP.colorLength[1]);
                                            HOperatorSet.Threshold(image2, out thresholdColorTestRegion2, cSP.greenColor[1] - cSP.colorLength[1], cSP.greenColor[1] + cSP.colorLength[1]);
                                            HOperatorSet.Threshold(image3, out thresholdColorTestRegion3, cSP.blueColor[1] - cSP.colorLength[1], cSP.blueColor[1] + cSP.colorLength[1]);
                                            HOperatorSet.Intersection(thresholdColorTestRegion1, thresholdColorTestRegion2, out thresholdColorRegionPart2);
                                            HOperatorSet.Intersection(thresholdColorTestRegion3, thresholdColorRegionPart2, out thresholdColorRegionPart2);
                                            if (cSP.isBesideThisColor[1])
                                            {
                                                HOperatorSet.Complement(thresholdColorRegionPart2, out thresholdColorRegionPart2);
                                            }
                                            break;
                                        case 2:
                                            HOperatorSet.Threshold(image1, out thresholdColorTestRegion1, cSP.redColor[2] - cSP.colorLength[2], cSP.redColor[2] + cSP.colorLength[2]);
                                            HOperatorSet.Threshold(image2, out thresholdColorTestRegion2, cSP.greenColor[2] - cSP.colorLength[2], cSP.greenColor[2] + cSP.colorLength[2]);
                                            HOperatorSet.Threshold(image3, out thresholdColorTestRegion3, cSP.blueColor[2] - cSP.colorLength[2], cSP.blueColor[2] + cSP.colorLength[2]);
                                            HOperatorSet.Intersection(thresholdColorTestRegion1, thresholdColorTestRegion2, out thresholdColorRegionPart3);
                                            HOperatorSet.Intersection(thresholdColorTestRegion3, thresholdColorRegionPart3, out thresholdColorRegionPart3);
                                            if (cSP.isBesideThisColor[2])
                                            {
                                                HOperatorSet.Complement(thresholdColorRegionPart3, out thresholdColorRegionPart3);
                                            }
                                            break;
                                        case 3:
                                            HOperatorSet.Threshold(image1, out thresholdColorTestRegion1, cSP.redColor[3] - cSP.colorLength[3], cSP.redColor[3] + cSP.colorLength[3]);
                                            HOperatorSet.Threshold(image2, out thresholdColorTestRegion2, cSP.greenColor[3] - cSP.colorLength[3], cSP.greenColor[3] + cSP.colorLength[3]);
                                            HOperatorSet.Threshold(image3, out thresholdColorTestRegion3, cSP.blueColor[3] - cSP.colorLength[3], cSP.blueColor[3] + cSP.colorLength[3]);
                                            HOperatorSet.Intersection(thresholdColorTestRegion1, thresholdColorTestRegion2, out thresholdColorRegionPart4);
                                            HOperatorSet.Intersection(thresholdColorTestRegion3, thresholdColorRegionPart4, out thresholdColorRegionPart4);
                                            if (cSP.isBesideThisColor[3])
                                            {
                                                HOperatorSet.Complement(thresholdColorRegionPart4, out thresholdColorRegionPart4);
                                            }
                                            break;
                                        case 4:
                                            HOperatorSet.Threshold(image1, out thresholdColorTestRegion1, cSP.redColor[4] - cSP.colorLength[4], cSP.redColor[4] + cSP.colorLength[4]);
                                            HOperatorSet.Threshold(image2, out thresholdColorTestRegion2, cSP.greenColor[4] - cSP.colorLength[4], cSP.greenColor[4] + cSP.colorLength[4]);
                                            HOperatorSet.Threshold(image3, out thresholdColorTestRegion3, cSP.blueColor[4] - cSP.colorLength[4], cSP.blueColor[4] + cSP.colorLength[4]);
                                            HOperatorSet.Intersection(thresholdColorTestRegion1, thresholdColorTestRegion2, out thresholdColorRegionPart5);
                                            HOperatorSet.Intersection(thresholdColorTestRegion3, thresholdColorRegionPart5, out thresholdColorRegionPart5);
                                            if (cSP.isBesideThisColor[4])
                                            {
                                                HOperatorSet.Complement(thresholdColorRegionPart5, out thresholdColorRegionPart5);
                                            }
                                            break;
                                        case 5:
                                            HOperatorSet.Threshold(image1, out thresholdColorTestRegion1, cSP.redColor[5] - cSP.colorLength[5], cSP.redColor[5] + cSP.colorLength[5]);
                                            HOperatorSet.Threshold(image2, out thresholdColorTestRegion2, cSP.greenColor[5] - cSP.colorLength[5], cSP.greenColor[5] + cSP.colorLength[5]);
                                            HOperatorSet.Threshold(image3, out thresholdColorTestRegion3, cSP.blueColor[5] - cSP.colorLength[5], cSP.blueColor[5] + cSP.colorLength[5]);
                                            HOperatorSet.Intersection(thresholdColorTestRegion1, thresholdColorTestRegion2, out thresholdColorRegionPart6);
                                            HOperatorSet.Intersection(thresholdColorTestRegion3, thresholdColorRegionPart6, out thresholdColorRegionPart6);
                                            if (cSP.isBesideThisColor[5])
                                            {
                                                HOperatorSet.Complement(thresholdColorRegionPart6, out thresholdColorRegionPart6);
                                            }
                                            break;

                                    }
                                    if (thresholdColorTestRegion1 != null)
                                    {
                                        thresholdColorTestRegion1.Dispose();
                                    }
                                    if (thresholdColorTestRegion2 != null)
                                    {
                                        thresholdColorTestRegion2.Dispose();
                                    }
                                    if (thresholdColorTestRegion3 != null)
                                    {
                                        thresholdColorTestRegion3.Dispose();
                                    }

                                }

                            }





                            HObject thresholdTestRegionimg, thresholdTestRegionimg3;
                            HOperatorSet.GenEmptyObj(out thresholdTestRegionimg);
                            if (thresholdTestRegionimg != null)
                            {
                                thresholdTestRegionimg.Dispose();
                            }
                            HOperatorSet.GenEmptyRegion(out thresholdColorRegion);
                            if (thresholdColorRegionPart1 != null && checkedListBoxSelectColorGray.GetItemChecked(0))
                            {
                                HOperatorSet.Union2(thresholdColorRegionPart1, thresholdColorRegion, out thresholdColorRegion);
                            }
                            if (thresholdColorRegionPart2 != null && checkedListBoxSelectColorGray.GetItemChecked(1))
                            {
                                HOperatorSet.Union2(thresholdColorRegionPart2, thresholdColorRegion, out thresholdColorRegion);
                            }
                            if (thresholdColorRegionPart3 != null && checkedListBoxSelectColorGray.GetItemChecked(2))
                            {
                                HOperatorSet.Union2(thresholdColorRegionPart3, thresholdColorRegion, out thresholdColorRegion);
                            }
                            if (thresholdColorRegionPart4 != null && checkedListBoxSelectColorGray.GetItemChecked(3))
                            {
                                HOperatorSet.Union2(thresholdColorRegionPart4, thresholdColorRegion, out thresholdColorRegion);
                            }
                            if (thresholdColorRegionPart5 != null && checkedListBoxSelectColorGray.GetItemChecked(4))
                            {
                                HOperatorSet.Union2(thresholdColorRegionPart5, thresholdColorRegion, out thresholdColorRegion);
                            }
                            if (thresholdColorRegionPart6 != null && checkedListBoxSelectColorGray.GetItemChecked(5))
                            {
                                HOperatorSet.Union2(thresholdColorRegionPart6, thresholdColorRegion, out thresholdColorRegion);
                            }


                            HOperatorSet.RegionToBin(thresholdColorRegion, out thresholdTestRegionimg, 255, 0, columnNum, rowNum);
                            HOperatorSet.Compose3(thresholdTestRegionimg, thresholdTestRegionimg, thresholdTestRegionimg, out thresholdTestRegionimg3);
                            if (imageTempShow != null)
                            {
                                imageTempShow.Dispose();
                            }
                            HOperatorSet.BitAnd(imageTemp, thresholdTestRegionimg3, out imageTempShow);
                            if (thresholdTestRegionimg != null)
                            {
                                thresholdTestRegionimg.Dispose();
                            }
                            if (thresholdTestRegionimg3 != null)
                            {
                                thresholdTestRegionimg3.Dispose();
                            }
                        }
                        else
                        {

                            HObject thresholdTestRegionimg, thresholdTestRegionimg3;
                            HObject thresholdColorTestRegion1, thresholdColorTestRegion2, thresholdColorTestRegion3;
                            HOperatorSet.GenEmptyObj(out thresholdColorTestRegion1);
                            HOperatorSet.GenEmptyObj(out thresholdColorTestRegion2);
                            HOperatorSet.GenEmptyObj(out thresholdColorTestRegion3);
                            HOperatorSet.GenEmptyObj(out thresholdTestRegionimg);
                            if (thresholdTestRegionimg != null)
                            {
                                thresholdTestRegionimg.Dispose();
                            }
                            switch (comboBoxSetColorGray.SelectedIndex)
                            {
                                case 0:

                                    HOperatorSet.Threshold(image1, out thresholdColorTestRegion1, cSP.redColor[0] - cSP.colorLength[0], cSP.redColor[0] + cSP.colorLength[0]);
                                    HOperatorSet.Threshold(image2, out thresholdColorTestRegion2, cSP.greenColor[0] - cSP.colorLength[0], cSP.greenColor[0] + cSP.colorLength[0]);
                                    HOperatorSet.Threshold(image3, out thresholdColorTestRegion3, cSP.blueColor[0] - cSP.colorLength[0], cSP.blueColor[0] + cSP.colorLength[0]);
                                    HOperatorSet.Intersection(thresholdColorTestRegion1, thresholdColorTestRegion2, out thresholdColorRegionPart1);
                                    HOperatorSet.Intersection(thresholdColorTestRegion3, thresholdColorRegionPart1, out thresholdColorRegionPart1);
                                    if (cSP.isBesideThisColor[0])
                                    {
                                        HOperatorSet.Complement(thresholdColorRegionPart1, out thresholdColorRegionPart1);
                                    }
                                    HOperatorSet.RegionToBin(thresholdColorRegionPart1, out thresholdTestRegionimg, 255, 0, columnNum, rowNum);
                                    break;
                                case 1:
                                    HOperatorSet.Threshold(image1, out thresholdColorTestRegion1, cSP.redColor[1] - cSP.colorLength[1], cSP.redColor[1] + cSP.colorLength[1]);
                                    HOperatorSet.Threshold(image2, out thresholdColorTestRegion2, cSP.greenColor[1] - cSP.colorLength[1], cSP.greenColor[1] + cSP.colorLength[1]);
                                    HOperatorSet.Threshold(image3, out thresholdColorTestRegion3, cSP.blueColor[1] - cSP.colorLength[1], cSP.blueColor[1] + cSP.colorLength[1]);
                                    HOperatorSet.Intersection(thresholdColorTestRegion1, thresholdColorTestRegion2, out thresholdColorRegionPart2);
                                    HOperatorSet.Intersection(thresholdColorTestRegion3, thresholdColorRegionPart2, out thresholdColorRegionPart2);
                                    if (cSP.isBesideThisColor[1])
                                    {
                                        HOperatorSet.Complement(thresholdColorRegionPart2, out thresholdColorRegionPart2);
                                    }
                                    HOperatorSet.RegionToBin(thresholdColorRegionPart2, out thresholdTestRegionimg, 255, 0, columnNum, rowNum);
                                    break;
                                case 2:
                                    HOperatorSet.Threshold(image1, out thresholdColorTestRegion1, cSP.redColor[2] - cSP.colorLength[2], cSP.redColor[2] + cSP.colorLength[2]);
                                    HOperatorSet.Threshold(image2, out thresholdColorTestRegion2, cSP.greenColor[2] - cSP.colorLength[2], cSP.greenColor[2] + cSP.colorLength[2]);
                                    HOperatorSet.Threshold(image3, out thresholdColorTestRegion3, cSP.blueColor[2] - cSP.colorLength[2], cSP.blueColor[2] + cSP.colorLength[2]);
                                    HOperatorSet.Intersection(thresholdColorTestRegion1, thresholdColorTestRegion2, out thresholdColorRegionPart3);
                                    HOperatorSet.Intersection(thresholdColorTestRegion3, thresholdColorRegionPart3, out thresholdColorRegionPart3);
                                    if (cSP.isBesideThisColor[2])
                                    {
                                        HOperatorSet.Complement(thresholdColorRegionPart3, out thresholdColorRegionPart3);
                                    }
                                    HOperatorSet.RegionToBin(thresholdColorRegionPart3, out thresholdTestRegionimg, 255, 0, columnNum, rowNum);
                                    break;
                                case 3:
                                    HOperatorSet.Threshold(image1, out thresholdColorTestRegion1, cSP.redColor[3] - cSP.colorLength[3], cSP.redColor[3] + cSP.colorLength[3]);
                                    HOperatorSet.Threshold(image2, out thresholdColorTestRegion2, cSP.greenColor[3] - cSP.colorLength[3], cSP.greenColor[3] + cSP.colorLength[3]);
                                    HOperatorSet.Threshold(image3, out thresholdColorTestRegion3, cSP.blueColor[3] - cSP.colorLength[3], cSP.blueColor[3] + cSP.colorLength[3]);
                                    HOperatorSet.Intersection(thresholdColorTestRegion1, thresholdColorTestRegion2, out thresholdColorRegionPart4);
                                    HOperatorSet.Intersection(thresholdColorTestRegion3, thresholdColorRegionPart4, out thresholdColorRegionPart4);
                                    if (cSP.isBesideThisColor[3])
                                    {
                                        HOperatorSet.Complement(thresholdColorRegionPart4, out thresholdColorRegionPart4);
                                    }
                                    HOperatorSet.RegionToBin(thresholdColorRegionPart4, out thresholdTestRegionimg, 255, 0, columnNum, rowNum);
                                    break;
                                case 4:
                                    HOperatorSet.Threshold(image1, out thresholdColorTestRegion1, cSP.redColor[4] - cSP.colorLength[4], cSP.redColor[4] + cSP.colorLength[4]);
                                    HOperatorSet.Threshold(image2, out thresholdColorTestRegion2, cSP.greenColor[4] - cSP.colorLength[4], cSP.greenColor[4] + cSP.colorLength[4]);
                                    HOperatorSet.Threshold(image3, out thresholdColorTestRegion3, cSP.blueColor[4] - cSP.colorLength[4], cSP.blueColor[4] + cSP.colorLength[4]);
                                    HOperatorSet.Intersection(thresholdColorTestRegion1, thresholdColorTestRegion2, out thresholdColorRegionPart5);
                                    HOperatorSet.Intersection(thresholdColorTestRegion3, thresholdColorRegionPart5, out thresholdColorRegionPart5);
                                    if (cSP.isBesideThisColor[4])
                                    {
                                        HOperatorSet.Complement(thresholdColorRegionPart5, out thresholdColorRegionPart5);
                                    }
                                    HOperatorSet.RegionToBin(thresholdColorRegionPart5, out thresholdTestRegionimg, 255, 0, columnNum, rowNum);
                                    break;
                                case 5:
                                    HOperatorSet.Threshold(image1, out thresholdColorTestRegion1, cSP.redColor[5] - cSP.colorLength[5], cSP.redColor[5] + cSP.colorLength[5]);
                                    HOperatorSet.Threshold(image2, out thresholdColorTestRegion2, cSP.greenColor[5] - cSP.colorLength[5], cSP.greenColor[5] + cSP.colorLength[5]);
                                    HOperatorSet.Threshold(image3, out thresholdColorTestRegion3, cSP.blueColor[5] - cSP.colorLength[5], cSP.blueColor[5] + cSP.colorLength[5]);
                                    HOperatorSet.Intersection(thresholdColorTestRegion1, thresholdColorTestRegion2, out thresholdColorRegionPart6);
                                    HOperatorSet.Intersection(thresholdColorTestRegion3, thresholdColorRegionPart6, out thresholdColorRegionPart6);
                                    if (cSP.isBesideThisColor[5])
                                    {
                                        HOperatorSet.Complement(thresholdColorRegionPart6, out thresholdColorRegionPart6);
                                    }
                                    HOperatorSet.RegionToBin(thresholdColorRegionPart6, out thresholdTestRegionimg, 255, 0, columnNum, rowNum);
                                    break;

                            }

                            HOperatorSet.Compose3(thresholdTestRegionimg, thresholdTestRegionimg, thresholdTestRegionimg, out thresholdTestRegionimg3);
                            if (imageTempShow != null)
                            {
                                imageTempShow.Dispose();
                            }
                            HOperatorSet.BitAnd(imageTemp, thresholdTestRegionimg3, out imageTempShow);
                            if (thresholdTestRegionimg != null)
                            {
                                thresholdTestRegionimg.Dispose();
                            }
                            if (thresholdTestRegionimg3 != null)
                            {
                                thresholdTestRegionimg3.Dispose();
                            }
                            if (thresholdColorTestRegion1 != null)
                            {
                                thresholdColorTestRegion1.Dispose();
                            }
                            if (thresholdColorTestRegion2 != null)
                            {
                                thresholdColorTestRegion2.Dispose();
                            }
                            if (thresholdColorTestRegion3 != null)
                            {
                                thresholdColorTestRegion3.Dispose();
                            }
                        }
                    }
                    else
                    {
                        if (radioButtonSegmentShowOriImage.Checked == true)
                        {
                            if (imageTempShow != null)
                            {
                                imageTempShow.Dispose();
                            }
                            HOperatorSet.CopyImage(image, out imageTempShow);
                        }
                        else if (radioButtonSegmentShowWholeImage.Checked == true)
                        {
                            for (int i = 0; i < 6; i++)
                            {
                                if (checkedListBoxSelectColorGray.GetItemChecked(i))
                                {
                                    switch (comboBoxSetColorGray.SelectedIndex)
                                    {
                                        case 0:
                                            HOperatorSet.Threshold(image, out thresholdGrayRegionPart1, cSP.grayValue[0] - cSP.colorLength[0], cSP.grayValue[0] + cSP.colorLength[0]);
                                            if (cSP.isBesideThisColor[0])
                                            {
                                                HOperatorSet.Complement(thresholdGrayRegionPart1, out thresholdGrayRegionPart1);
                                            }
                                            break;
                                        case 1:
                                            HOperatorSet.Threshold(image, out thresholdGrayRegionPart2, cSP.grayValue[1] - cSP.colorLength[1], cSP.grayValue[1] + cSP.colorLength[1]);
                                            if (cSP.isBesideThisColor[1])
                                            {
                                                HOperatorSet.Complement(thresholdGrayRegionPart2, out thresholdGrayRegionPart2);
                                            }
                                            break;
                                        case 2:
                                            HOperatorSet.Threshold(image, out thresholdGrayRegionPart3, cSP.grayValue[2] - cSP.colorLength[2], cSP.grayValue[2] + cSP.colorLength[2]);
                                            if (cSP.isBesideThisColor[2])
                                            {
                                                HOperatorSet.Complement(thresholdGrayRegionPart3, out thresholdGrayRegionPart3);
                                            }
                                            break;
                                        case 3:
                                            HOperatorSet.Threshold(image, out thresholdGrayRegionPart4, cSP.grayValue[3] - cSP.colorLength[3], cSP.grayValue[3] + cSP.colorLength[3]);
                                            if (cSP.isBesideThisColor[3])
                                            {
                                                HOperatorSet.Complement(thresholdGrayRegionPart4, out thresholdGrayRegionPart4);
                                            }
                                            break;
                                        case 4:
                                            HOperatorSet.Threshold(image, out thresholdGrayRegionPart5, cSP.grayValue[4] - cSP.colorLength[4], cSP.grayValue[4] + cSP.colorLength[4]);
                                            if (cSP.isBesideThisColor[4])
                                            {
                                                HOperatorSet.Complement(thresholdGrayRegionPart5, out thresholdGrayRegionPart5);
                                            }
                                            break;
                                        case 5:
                                            HOperatorSet.Threshold(image, out thresholdGrayRegionPart6, cSP.grayValue[5] - cSP.colorLength[5], cSP.grayValue[5] + cSP.colorLength[5]);
                                            if (cSP.isBesideThisColor[5])
                                            {
                                                HOperatorSet.Complement(thresholdGrayRegionPart6, out thresholdGrayRegionPart6);
                                            }
                                            break;

                                    }
                                }
                            }


                            HObject thresholdTestRegionimg, thresholdTestRegionimg3;
                            HOperatorSet.GenEmptyObj(out thresholdTestRegionimg);
                            if (thresholdTestRegionimg != null)
                            {
                                thresholdTestRegionimg.Dispose();
                            }
                            HOperatorSet.GenEmptyRegion(out thresholdGrayRegion);
                            if (thresholdGrayRegionPart1 != null && checkedListBoxSelectColorGray.GetItemChecked(0))
                            {
                                HOperatorSet.Union2(thresholdGrayRegionPart1, thresholdGrayRegion, out thresholdGrayRegion);
                            }
                            if (thresholdGrayRegionPart2 != null && checkedListBoxSelectColorGray.GetItemChecked(1))
                            {
                                HOperatorSet.Union2(thresholdGrayRegionPart2, thresholdGrayRegion, out thresholdGrayRegion);
                            }
                            if (thresholdGrayRegionPart3 != null && checkedListBoxSelectColorGray.GetItemChecked(2))
                            {
                                HOperatorSet.Union2(thresholdGrayRegionPart3, thresholdGrayRegion, out thresholdGrayRegion);
                            }
                            if (thresholdGrayRegionPart4 != null && checkedListBoxSelectColorGray.GetItemChecked(3))
                            {
                                HOperatorSet.Union2(thresholdGrayRegionPart4, thresholdGrayRegion, out thresholdGrayRegion);
                            }
                            if (thresholdGrayRegionPart5 != null && checkedListBoxSelectColorGray.GetItemChecked(4))
                            {
                                HOperatorSet.Union2(thresholdGrayRegionPart5, thresholdGrayRegion, out thresholdGrayRegion);
                            }
                            if (thresholdGrayRegionPart6 != null && checkedListBoxSelectColorGray.GetItemChecked(5))
                            {
                                HOperatorSet.Union2(thresholdGrayRegionPart6, thresholdGrayRegion, out thresholdGrayRegion);
                            }
                            //int j = thresholdGrayRegion.CountObj();

                            HOperatorSet.RegionToBin(thresholdGrayRegion, out thresholdTestRegionimg, 255, 0, columnNum, rowNum);
                            HOperatorSet.Compose3(thresholdTestRegionimg, thresholdTestRegionimg, thresholdTestRegionimg, out thresholdTestRegionimg3);
                            if (imageTempShow != null)
                            {
                                imageTempShow.Dispose();
                            }
                            HOperatorSet.BitAnd(imageTemp, thresholdTestRegionimg3, out imageTempShow);
                            if (thresholdTestRegionimg != null)
                            {
                                thresholdTestRegionimg.Dispose();
                            }
                            if (thresholdTestRegionimg3 != null)
                            {
                                thresholdTestRegionimg3.Dispose();
                            }
                        }
                        else
                        {
                            HObject thresholdTestRegionimg, thresholdTestRegionimg3;
                            HOperatorSet.GenEmptyObj(out thresholdTestRegionimg);
                            if (thresholdTestRegionimg != null)
                            {
                                thresholdTestRegionimg.Dispose();
                            }
                            switch (comboBoxSetColorGray.SelectedIndex)
                            {
                                case 0:
                                    HOperatorSet.Threshold(image, out thresholdGrayRegionPart1, cSP.grayValue[0] - cSP.colorLength[0], cSP.grayValue[0] + cSP.colorLength[0]);
                                    if (cSP.isBesideThisColor[0])
                                    {
                                        HOperatorSet.Complement(thresholdGrayRegionPart1, out thresholdGrayRegionPart1);
                                    }
                                    HOperatorSet.RegionToBin(thresholdGrayRegionPart1, out thresholdTestRegionimg, 255, 0, columnNum, rowNum);
                                    break;
                                case 1:
                                    HOperatorSet.Threshold(image, out thresholdGrayRegionPart2, cSP.grayValue[1] - cSP.colorLength[1], cSP.grayValue[1] + cSP.colorLength[1]);
                                    if (cSP.isBesideThisColor[1])
                                    {
                                        HOperatorSet.Complement(thresholdGrayRegionPart2, out thresholdGrayRegionPart2);
                                    }
                                    HOperatorSet.RegionToBin(thresholdGrayRegionPart2, out thresholdTestRegionimg, 255, 0, columnNum, rowNum);
                                    break;
                                case 2:
                                    HOperatorSet.Threshold(image, out thresholdGrayRegionPart3, cSP.grayValue[2] - cSP.colorLength[2], cSP.grayValue[2] + cSP.colorLength[2]);
                                    if (cSP.isBesideThisColor[2])
                                    {
                                        HOperatorSet.Complement(thresholdGrayRegionPart3, out thresholdGrayRegionPart3);
                                    }
                                    HOperatorSet.RegionToBin(thresholdGrayRegionPart3, out thresholdTestRegionimg, 255, 0, columnNum, rowNum);
                                    break;
                                case 3:
                                    HOperatorSet.Threshold(image, out thresholdGrayRegionPart4, cSP.grayValue[3] - cSP.colorLength[3], cSP.grayValue[3] + cSP.colorLength[3]);
                                    if (cSP.isBesideThisColor[3])
                                    {
                                        HOperatorSet.Complement(thresholdGrayRegionPart4, out thresholdGrayRegionPart4);
                                    }
                                    HOperatorSet.RegionToBin(thresholdGrayRegionPart4, out thresholdTestRegionimg, 255, 0, columnNum, rowNum);
                                    break;
                                case 4:
                                    HOperatorSet.Threshold(image, out thresholdGrayRegionPart5, cSP.grayValue[4] - cSP.colorLength[4], cSP.grayValue[4] + cSP.colorLength[4]);
                                    if (cSP.isBesideThisColor[4])
                                    {
                                        HOperatorSet.Complement(thresholdGrayRegionPart5, out thresholdGrayRegionPart5);
                                    }
                                    HOperatorSet.RegionToBin(thresholdGrayRegionPart5, out thresholdTestRegionimg, 255, 0, columnNum, rowNum);
                                    break;
                                case 5:
                                    HOperatorSet.Threshold(image, out thresholdGrayRegionPart6, cSP.grayValue[5] - cSP.colorLength[5], cSP.grayValue[5] + cSP.colorLength[5]);
                                    if (cSP.isBesideThisColor[5])
                                    {
                                        HOperatorSet.Complement(thresholdGrayRegionPart6, out thresholdGrayRegionPart6);
                                    }
                                    HOperatorSet.RegionToBin(thresholdGrayRegionPart6, out thresholdTestRegionimg, 255, 0, columnNum, rowNum);
                                    break;

                            }

                            HOperatorSet.Compose3(thresholdTestRegionimg, thresholdTestRegionimg, thresholdTestRegionimg, out thresholdTestRegionimg3);
                            if (imageTempShow != null)
                            {
                                imageTempShow.Dispose();
                            }
                            HOperatorSet.BitAnd(imageTemp, thresholdTestRegionimg3, out imageTempShow);
                            if (thresholdTestRegionimg != null)
                            {
                                thresholdTestRegionimg.Dispose();
                            }
                            if (thresholdTestRegionimg3 != null)
                            {
                                thresholdTestRegionimg3.Dispose();
                            }
                        }
                    }
                }

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
                basicClass.displayhobject(img, BlobHWHandle);
                partImageWindowShow(oriLocation);
            }




        }
        private void BlobPartPictureBox_SizeChanged(object sender, EventArgs e)
        {
            try
            {
                if (BlobPartHWHandle != null)
                {
                    HOperatorSet.SetWindowExtents(BlobPartHWHandle, 0, 0, (BlobPartPictureBox.Width), (BlobPartPictureBox.Height));
                    HOperatorSet.SetPart(BlobPartHWHandle, 0, 0, (BlobPartPictureBox.Height - 1), (BlobPartPictureBox.Width - 1));
                    if (imgPart != null)
                    {
                        partImageWindowShow(oriLocation);
                    }
                }
            }
            catch (System.Exception ex)
            {
                //MessageBox.Show(ex.Message);
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
                    if (oriLocation.Y * resizerate / resRat - ((float)(BlobPartPictureBox.Height - 1 + 0) / 2.0) < 0 && oriLocation.X * resizerate / resRat - ((float)(BlobPartPictureBox.Width - 1 + 0) / 2.0) < 0)
                    {
                        HObject temp;
                        basicClass.genRectangle1(out rectDomain, 0, 0, (BlobPartPictureBox.Height - 1), (BlobPartPictureBox.Width - 1));
                        centerLocation.Y = (float)(((float)(System.Math.Min(BlobPartPictureBox.Height, rowNum * resizerate) - 1 + 0) / 2.0) * resRat / resizerate);
                        centerLocation.X = (float)(((float)(System.Math.Min(BlobPartPictureBox.Width, columnNum * resizerate) - 1 + 0) / 2.0) * resRat / resizerate);

                        basicClass.reduceDomain(imgPart, rectDomain, out temp);
                        if (imgPartTemp != null)
                        {
                            imgPartTemp.Dispose();
                        }
                        HOperatorSet.CropDomain(temp, out imgPartTemp);
                        HOperatorSet.DispObj(imgPartTemp, BlobPartHWHandle);
                        temp.Dispose();
                    }
                    else if (oriLocation.Y * resizerate / resRat - ((float)(BlobPartPictureBox.Height - 1 + 0) / 2.0) <= 0 && oriLocation.X * resizerate / resRat - ((float)(BlobPartPictureBox.Width - 1 + 0) / 2.0) >= 0 && oriLocation.X * resizerate / resRat + ((float)(BlobPartPictureBox.Width - 1 + 0) / 2.0) < columnNum * resizerate - 1)
                    {
                        HObject temp;
                        basicClass.genRectangle1(out rectDomain, 0, oriLocation.X * resizerate / resRat - ((float)(BlobPartPictureBox.Width - 1 + 0) / 2.0), (BlobPartPictureBox.Height - 1), oriLocation.X * resizerate / resRat + ((float)(BlobPartPictureBox.Width - 1 + 0) / 2.0));
                        centerLocation.Y = (float)(((float)(System.Math.Min(BlobPartPictureBox.Height, rowNum * resizerate) - 1 + 0) / 2.0) * resRat / resizerate);
                        centerLocation.X = (float)oriLocation.X;
                        basicClass.reduceDomain(imgPart, rectDomain, out temp);
                        if (imgPartTemp != null)
                        {
                            imgPartTemp.Dispose();
                        }
                        HOperatorSet.CropDomain(temp, out imgPartTemp);
                        HOperatorSet.DispObj(imgPartTemp, BlobPartHWHandle);
                        temp.Dispose();
                    }
                    else if (oriLocation.Y * resizerate / resRat - ((float)(BlobPartPictureBox.Height - 1 + 0) / 2.0) > 0 && oriLocation.Y * resizerate / resRat + ((float)(BlobPartPictureBox.Height - 1 + 0) / 2.0) <= rowNum * resizerate - 1 && oriLocation.X * resizerate / resRat - ((float)(BlobPartPictureBox.Width - 1 + 0) / 2.0) < 0)
                    {
                        HObject temp;
                        basicClass.genRectangle1(out rectDomain, oriLocation.Y * resizerate / resRat - ((float)(BlobPartPictureBox.Height - 1 + 0) / 2.0), 0, oriLocation.Y * resizerate / resRat + ((float)(BlobPartPictureBox.Height - 1 + 0) / 2.0), (BlobPartPictureBox.Width - 1));
                        centerLocation.Y = (float)oriLocation.Y;
                        centerLocation.X = (float)(((float)(System.Math.Min(BlobPartPictureBox.Width, columnNum * resizerate) - 1 + 0) / 2.0) * resRat / resizerate);
                        basicClass.reduceDomain(imgPart, rectDomain, out temp);
                        if (imgPartTemp != null)
                        {
                            imgPartTemp.Dispose();
                        }
                        HOperatorSet.CropDomain(temp, out imgPartTemp);
                        HOperatorSet.DispObj(imgPartTemp, BlobPartHWHandle);
                        temp.Dispose();
                    }
                    else if (oriLocation.Y * resizerate / resRat + ((float)(BlobPartPictureBox.Height - 1 + 0) / 2.0) > rowNum * resizerate - 1 && oriLocation.X * resizerate / resRat - ((float)(BlobPartPictureBox.Width - 1 + 0) / 2.0) < 0)
                    {
                        HObject temp;
                        basicClass.genRectangle1(out rectDomain, rowNum * resizerate - 1 - (BlobPartPictureBox.Height - 1), 0, rowNum * resizerate - 1, (BlobPartPictureBox.Width - 1));
                        centerLocation.Y = (float)((rowNum * resizerate - ((float)(System.Math.Min(BlobPartPictureBox.Height, rowNum * resizerate) - 1 + 0) / 2.0) - 1) * resRat / resizerate);
                        centerLocation.X = (float)(((float)(System.Math.Min(BlobPartPictureBox.Width, columnNum * resizerate) - 1 + 0) / 2.0) * resRat / resizerate);
                        basicClass.reduceDomain(imgPart, rectDomain, out temp);
                        if (imgPartTemp != null)
                        {
                            imgPartTemp.Dispose();
                        }
                        HOperatorSet.CropDomain(temp, out imgPartTemp);
                        HOperatorSet.DispObj(imgPartTemp, BlobPartHWHandle);
                        temp.Dispose();
                    }
                    else if (oriLocation.Y * resizerate / resRat + ((float)(BlobPartPictureBox.Height - 1 + 0) / 2.0) > rowNum * resizerate - 1 && oriLocation.X * resizerate / resRat - ((float)(BlobPartPictureBox.Width - 1 + 0) / 2.0) > 0 && oriLocation.X * resizerate / resRat + ((float)(BlobPartPictureBox.Width - 1 + 0) / 2.0) < columnNum * resizerate - 1)
                    {
                        HObject temp;
                        basicClass.genRectangle1(out rectDomain, rowNum * resizerate - (BlobPartPictureBox.Height), oriLocation.X * resizerate / resRat - ((float)(BlobPartPictureBox.Width - 1 + 0) / 2.0), rowNum * resizerate - 1, oriLocation.X * resizerate / resRat + ((float)(BlobPartPictureBox.Width - 1 + 0) / 2.0));
                        centerLocation.Y = (float)((rowNum * resizerate - ((float)(System.Math.Min(BlobPartPictureBox.Height, rowNum * resizerate) - 1 + 0) / 2.0 + 1)) * resRat / resizerate);
                        centerLocation.X = (float)oriLocation.X;
                        basicClass.reduceDomain(imgPart, rectDomain, out temp);
                        if (imgPartTemp != null)
                        {
                            imgPartTemp.Dispose();
                        }
                        HOperatorSet.CropDomain(temp, out imgPartTemp);
                        HOperatorSet.DispObj(imgPartTemp, BlobPartHWHandle);
                        temp.Dispose();
                    }
                    else if (oriLocation.Y * resizerate / resRat + ((float)(BlobPartPictureBox.Height - 1 + 0) / 2.0) >= rowNum * resizerate - 1 && oriLocation.X * resizerate / resRat + ((float)(BlobPartPictureBox.Width - 1 + 0) / 2.0) >= columnNum * resizerate - 1)
                    {
                        HObject temp;
                        basicClass.genRectangle1(out rectDomain, rowNum * resizerate - (BlobPartPictureBox.Height), columnNum * resizerate - (BlobPartPictureBox.Width), rowNum * resizerate - 1, columnNum * resizerate - 1);
                        centerLocation.Y = (float)((rowNum * resizerate - ((float)(System.Math.Min(BlobPartPictureBox.Height, rowNum * resizerate) - 1 + 0) / 2.0 + 1)) * resRat / resizerate);
                        centerLocation.X = (float)((columnNum * resizerate - ((float)(System.Math.Min(BlobPartPictureBox.Width, columnNum * resizerate) - 1 + 0) / 2.0 + 1)) * resRat / resizerate);
                        basicClass.reduceDomain(imgPart, rectDomain, out temp);
                        if (imgPartTemp != null)
                        {
                            imgPartTemp.Dispose();
                        }
                        HOperatorSet.CropDomain(temp, out imgPartTemp);
                        HOperatorSet.DispObj(imgPartTemp, BlobPartHWHandle);
                        temp.Dispose();
                    }
                    else if (oriLocation.Y * resizerate / resRat + ((float)(BlobPartPictureBox.Height - 1 + 0) / 2.0) < rowNum * resizerate - 1 && oriLocation.Y * resizerate / resRat - ((float)(BlobPartPictureBox.Height - 1 + 0) / 2.0) > 0 && oriLocation.X * resizerate / resRat + ((float)(BlobPartPictureBox.Width - 1 + 0) / 2.0) > columnNum * resizerate - 1)
                    {
                        HObject temp;
                        basicClass.genRectangle1(out rectDomain, oriLocation.Y * resizerate / resRat - ((float)(BlobPartPictureBox.Height - 1 + 0) / 2.0), columnNum * resizerate - (BlobPartPictureBox.Width), oriLocation.Y * resizerate / resRat + ((float)(BlobPartPictureBox.Height - 1 + 0) / 2.0), columnNum * resizerate - 1);
                        centerLocation.Y = (float)oriLocation.Y;
                        centerLocation.X = (float)((columnNum * resizerate - ((float)(System.Math.Min(BlobPartPictureBox.Width, columnNum * resizerate) - 1 + 0) / 2.0 + 1)) * resRat / resizerate);
                        basicClass.reduceDomain(imgPart, rectDomain, out temp);
                        if (imgPartTemp != null)
                        {
                            imgPartTemp.Dispose();
                        }
                        HOperatorSet.CropDomain(temp, out imgPartTemp);
                        HOperatorSet.DispObj(imgPartTemp, BlobPartHWHandle);
                        temp.Dispose();
                    }
                    else if (oriLocation.Y * resizerate / resRat - ((float)(BlobPartPictureBox.Height - 1 + 0) / 2.0) <= 0 && oriLocation.X * resizerate / resRat + ((float)(BlobPartPictureBox.Width - 1 + 0) / 2.0) >= columnNum * resizerate - 1)
                    {
                        HObject temp;
                        basicClass.genRectangle1(out rectDomain, 0, columnNum * resizerate - (BlobPartPictureBox.Width), (BlobPartPictureBox.Height - 1), columnNum * resizerate - 1);
                        centerLocation.Y = (float)(((float)(System.Math.Min(BlobPartPictureBox.Height, rowNum * resizerate) - 1 + 0) / 2.0) * resRat / resizerate);
                        centerLocation.X = (float)((columnNum * resizerate - ((float)(System.Math.Min(BlobPartPictureBox.Width, columnNum * resizerate) - 1 + 0) / 2.0 + 1)) * resRat / resizerate);
                        basicClass.reduceDomain(imgPart, rectDomain, out temp);
                        if (imgPartTemp != null)
                        {
                            imgPartTemp.Dispose();
                        }
                        HOperatorSet.CropDomain(temp, out imgPartTemp);
                        HOperatorSet.DispObj(imgPartTemp, BlobPartHWHandle);
                        temp.Dispose();
                    }
                    else
                    {
                        HObject temp;
                        basicClass.genRectangle1(out rectDomain, oriLocation.Y * resizerate / resRat - ((float)(BlobPartPictureBox.Height - 1 + 0) / 2.0), oriLocation.X * resizerate / resRat - ((float)(BlobPartPictureBox.Width - 1 + 0) / 2.0), oriLocation.Y * resizerate / resRat + ((float)(BlobPartPictureBox.Height - 1 + 0) / 2.0), oriLocation.X * resizerate / resRat + ((float)(BlobPartPictureBox.Width - 1 + 0) / 2.0));
                        basicClass.reduceDomain(imgPart, rectDomain, out temp);
                        if (imgPartTemp != null)
                        {
                            imgPartTemp.Dispose();
                        }
                        HOperatorSet.CropDomain(temp, out imgPartTemp);
                        HOperatorSet.DispObj(imgPartTemp, BlobPartHWHandle);
                        temp.Dispose();
                    }
                    this.BlobPictureBox.Focus();
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }


        }
        private void BlobPictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Location.X >= 0 && e.Location.X <= columnNum * resRat/*columnNum * resRat - 320 * resRat / resizerate*/ && e.Location.Y >= 0 && e.Location.Y <= rowNum * resRat/*rowNum * resRat - 240 * resRat / resizerate*/)
            {
                try
                {
                    Cursor = Cursors.Hand;
                    oriLocation = e.Location;
                    centerLocation.Y = (float)oriLocation.Y;
                    centerLocation.X = (float)oriLocation.X;
                    if (oriLocation.Y * resizerate / resRat - ((float)(BlobPartPictureBox.Height - 1 + 0) / 2.0) < 0 && oriLocation.X * resizerate / resRat - ((float)(BlobPartPictureBox.Width - 1 + 0) / 2.0) < 0)
                    {
                        HObject temp;
                        basicClass.genRectangle1(out rectDomain, 0, 0, (BlobPartPictureBox.Height - 1), (BlobPartPictureBox.Width - 1));
                        centerLocation.Y = (float)(((float)(System.Math.Min(BlobPartPictureBox.Height, rowNum * resizerate) - 1 + 0) / 2.0) * resRat / resizerate);
                        centerLocation.X = (float)(((float)(System.Math.Min(BlobPartPictureBox.Width, columnNum * resizerate) - 1 + 0) / 2.0) * resRat / resizerate);

                        basicClass.reduceDomain(imgPart, rectDomain, out temp);
                        if (imgPartTemp != null)
                        {
                            imgPartTemp.Dispose();
                        }
                        HOperatorSet.CropDomain(temp, out imgPartTemp);
                        HOperatorSet.DispObj(imgPartTemp, BlobPartHWHandle);
                        temp.Dispose();
                    }
                    else if (oriLocation.Y * resizerate / resRat - ((float)(BlobPartPictureBox.Height - 1 + 0) / 2.0) <= 0 && oriLocation.X * resizerate / resRat - ((float)(BlobPartPictureBox.Width - 1 + 0) / 2.0) >= 0 && oriLocation.X * resizerate / resRat + ((float)(BlobPartPictureBox.Width - 1 + 0) / 2.0) < columnNum * resizerate - 1)
                    {
                        HObject temp;
                        basicClass.genRectangle1(out rectDomain, 0, oriLocation.X * resizerate / resRat - ((float)(BlobPartPictureBox.Width - 1 + 0) / 2.0), (BlobPartPictureBox.Height - 1), oriLocation.X * resizerate / resRat + ((float)(BlobPartPictureBox.Width - 1 + 0) / 2.0));
                        centerLocation.Y = (float)(((float)(System.Math.Min(BlobPartPictureBox.Height, rowNum * resizerate) - 1 + 0) / 2.0) * resRat / resizerate);
                        centerLocation.X = (float)oriLocation.X;
                        basicClass.reduceDomain(imgPart, rectDomain, out temp);
                        if (imgPartTemp != null)
                        {
                            imgPartTemp.Dispose();
                        }
                        HOperatorSet.CropDomain(temp, out imgPartTemp);
                        HOperatorSet.DispObj(imgPartTemp, BlobPartHWHandle);
                        temp.Dispose();
                    }
                    else if (oriLocation.Y * resizerate / resRat - ((float)(BlobPartPictureBox.Height - 1 + 0) / 2.0) > 0 && oriLocation.Y * resizerate / resRat + ((float)(BlobPartPictureBox.Height - 1 + 0) / 2.0) <= rowNum * resizerate - 1 && oriLocation.X * resizerate / resRat - ((float)(BlobPartPictureBox.Width - 1 + 0) / 2.0) < 0)
                    {
                        HObject temp;
                        basicClass.genRectangle1(out rectDomain, oriLocation.Y * resizerate / resRat - ((float)(BlobPartPictureBox.Height - 1 + 0) / 2.0), 0, oriLocation.Y * resizerate / resRat + ((float)(BlobPartPictureBox.Height - 1 + 0) / 2.0), (BlobPartPictureBox.Width - 1));
                        centerLocation.Y = (float)oriLocation.Y;
                        centerLocation.X = (float)(((float)(System.Math.Min(BlobPartPictureBox.Width, columnNum * resizerate) - 1 + 0) / 2.0) * resRat / resizerate);
                        basicClass.reduceDomain(imgPart, rectDomain, out temp);
                        if (imgPartTemp != null)
                        {
                            imgPartTemp.Dispose();
                        }
                        HOperatorSet.CropDomain(temp, out imgPartTemp);
                        HOperatorSet.DispObj(imgPartTemp, BlobPartHWHandle);
                        temp.Dispose();
                    }
                    else if (oriLocation.Y * resizerate / resRat + ((float)(BlobPartPictureBox.Height - 1 + 0) / 2.0) > rowNum * resizerate - 1 && oriLocation.X * resizerate / resRat - ((float)(BlobPartPictureBox.Width - 1 + 0) / 2.0) < 0)
                    {
                        HObject temp;
                        basicClass.genRectangle1(out rectDomain, rowNum * resizerate - 1 - (BlobPartPictureBox.Height - 1), 0, rowNum * resizerate - 1, (BlobPartPictureBox.Width - 1));
                        centerLocation.Y = (float)((rowNum * resizerate - ((float)(System.Math.Min(BlobPartPictureBox.Height, rowNum * resizerate) - 1 + 0) / 2.0) - 1) * resRat / resizerate);
                        centerLocation.X = (float)(((float)(System.Math.Min(BlobPartPictureBox.Width, columnNum * resizerate) - 1 + 0) / 2.0) * resRat / resizerate);
                        basicClass.reduceDomain(imgPart, rectDomain, out temp);
                        if (imgPartTemp != null)
                        {
                            imgPartTemp.Dispose();
                        }
                        HOperatorSet.CropDomain(temp, out imgPartTemp);
                        HOperatorSet.DispObj(imgPartTemp, BlobPartHWHandle);
                        temp.Dispose();
                    }
                    else if (oriLocation.Y * resizerate / resRat + ((float)(BlobPartPictureBox.Height - 1 + 0) / 2.0) > rowNum * resizerate - 1 && oriLocation.X * resizerate / resRat - ((float)(BlobPartPictureBox.Width - 1 + 0) / 2.0) > 0 && oriLocation.X * resizerate / resRat + ((float)(BlobPartPictureBox.Width - 1 + 0) / 2.0) < columnNum * resizerate - 1)
                    {
                        HObject temp;
                        basicClass.genRectangle1(out rectDomain, rowNum * resizerate - (BlobPartPictureBox.Height), oriLocation.X * resizerate / resRat - ((float)(BlobPartPictureBox.Width - 1 + 0) / 2.0), rowNum * resizerate - 1, oriLocation.X * resizerate / resRat + ((float)(BlobPartPictureBox.Width - 1 + 0) / 2.0));
                        centerLocation.Y = (float)((rowNum * resizerate - ((float)(System.Math.Min(BlobPartPictureBox.Height, rowNum * resizerate) - 1 + 0) / 2.0 + 1)) * resRat / resizerate);
                        centerLocation.X = (float)oriLocation.X;
                        basicClass.reduceDomain(imgPart, rectDomain, out temp);
                        if (imgPartTemp != null)
                        {
                            imgPartTemp.Dispose();
                        }
                        HOperatorSet.CropDomain(temp, out imgPartTemp);
                        HOperatorSet.DispObj(imgPartTemp, BlobPartHWHandle);
                        temp.Dispose();
                    }
                    else if (oriLocation.Y * resizerate / resRat + ((float)(BlobPartPictureBox.Height - 1 + 0) / 2.0) >= rowNum * resizerate - 1 && oriLocation.X * resizerate / resRat + ((float)(BlobPartPictureBox.Width - 1 + 0) / 2.0) >= columnNum * resizerate - 1)
                    {
                        HObject temp;
                        basicClass.genRectangle1(out rectDomain, rowNum * resizerate - (BlobPartPictureBox.Height), columnNum * resizerate - (BlobPartPictureBox.Width), rowNum * resizerate - 1, columnNum * resizerate - 1);
                        centerLocation.Y = (float)((rowNum * resizerate - ((float)(System.Math.Min(BlobPartPictureBox.Height, rowNum * resizerate) - 1 + 0) / 2.0 + 1)) * resRat / resizerate);
                        centerLocation.X = (float)((columnNum * resizerate - ((float)(System.Math.Min(BlobPartPictureBox.Width, columnNum * resizerate) - 1 + 0) / 2.0 + 1)) * resRat / resizerate);
                        basicClass.reduceDomain(imgPart, rectDomain, out temp);
                        if (imgPartTemp != null)
                        {
                            imgPartTemp.Dispose();
                        }
                        HOperatorSet.CropDomain(temp, out imgPartTemp);
                        HOperatorSet.DispObj(imgPartTemp, BlobPartHWHandle);
                        temp.Dispose();
                    }
                    else if (oriLocation.Y * resizerate / resRat + ((float)(BlobPartPictureBox.Height - 1 + 0) / 2.0) < rowNum * resizerate - 1 && oriLocation.Y * resizerate / resRat - ((float)(BlobPartPictureBox.Height - 1 + 0) / 2.0) > 0 && oriLocation.X * resizerate / resRat + ((float)(BlobPartPictureBox.Width - 1 + 0) / 2.0) > columnNum * resizerate - 1)
                    {
                        HObject temp;
                        basicClass.genRectangle1(out rectDomain, oriLocation.Y * resizerate / resRat - ((float)(BlobPartPictureBox.Height - 1 + 0) / 2.0), columnNum * resizerate - (BlobPartPictureBox.Width), oriLocation.Y * resizerate / resRat + ((float)(BlobPartPictureBox.Height - 1 + 0) / 2.0), columnNum * resizerate - 1);
                        centerLocation.Y = (float)oriLocation.Y;
                        centerLocation.X = (float)((columnNum * resizerate - ((float)(System.Math.Min(BlobPartPictureBox.Width, columnNum * resizerate) - 1 + 0) / 2.0 + 1)) * resRat / resizerate);
                        basicClass.reduceDomain(imgPart, rectDomain, out temp);
                        if (imgPartTemp != null)
                        {
                            imgPartTemp.Dispose();
                        }
                        HOperatorSet.CropDomain(temp, out imgPartTemp);
                        HOperatorSet.DispObj(imgPartTemp, BlobPartHWHandle);
                        temp.Dispose();
                    }
                    else if (oriLocation.Y * resizerate / resRat - ((float)(BlobPartPictureBox.Height - 1 + 0) / 2.0) <= 0 && oriLocation.X * resizerate / resRat + ((float)(BlobPartPictureBox.Width - 1 + 0) / 2.0) >= columnNum * resizerate - 1)
                    {
                        HObject temp;
                        basicClass.genRectangle1(out rectDomain, 0, columnNum * resizerate - (BlobPartPictureBox.Width), (BlobPartPictureBox.Height - 1), columnNum * resizerate - 1);
                        centerLocation.Y = (float)(((float)(System.Math.Min(BlobPartPictureBox.Height, rowNum * resizerate) - 1 + 0) / 2.0) * resRat / resizerate);
                        centerLocation.X = (float)((columnNum * resizerate - ((float)(System.Math.Min(BlobPartPictureBox.Width, columnNum * resizerate) - 1 + 0) / 2.0 + 1)) * resRat / resizerate);
                        basicClass.reduceDomain(imgPart, rectDomain, out temp);
                        if (imgPartTemp != null)
                        {
                            imgPartTemp.Dispose();
                        }
                        HOperatorSet.CropDomain(temp, out imgPartTemp);
                        HOperatorSet.DispObj(imgPartTemp, BlobPartHWHandle);
                        temp.Dispose();
                    }
                    else
                    {
                        HObject temp;
                        basicClass.genRectangle1(out rectDomain, oriLocation.Y * resizerate / resRat - ((float)(BlobPartPictureBox.Height - 1 + 0) / 2.0), oriLocation.X * resizerate / resRat - ((float)(BlobPartPictureBox.Width - 1 + 0) / 2.0), oriLocation.Y * resizerate / resRat + ((float)(BlobPartPictureBox.Height - 1 + 0) / 2.0), oriLocation.X * resizerate / resRat + ((float)(BlobPartPictureBox.Width - 1 + 0) / 2.0));
                        basicClass.reduceDomain(imgPart, rectDomain, out temp);
                        if (imgPartTemp != null)
                        {
                            imgPartTemp.Dispose();
                        }
                        HOperatorSet.CropDomain(temp, out imgPartTemp);
                        HOperatorSet.DispObj(imgPartTemp, BlobPartHWHandle);
                        temp.Dispose();
                    }
                    this.BlobPictureBox.Focus();
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        private void BlobPictureBox_MouseMove(object sender, MouseEventArgs e)
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
                        if (oriLocation.Y * resizerate / resRat - ((float)(BlobPartPictureBox.Height - 1 + 0) / 2.0) < 0 && oriLocation.X * resizerate / resRat - ((float)(BlobPartPictureBox.Width - 1 + 0) / 2.0) < 0)
                        {
                            HObject temp;
                            basicClass.genRectangle1(out rectDomain, 0, 0, (BlobPartPictureBox.Height - 1), (BlobPartPictureBox.Width - 1));
                            centerLocation.Y = (float)(((float)(System.Math.Min(BlobPartPictureBox.Height, rowNum * resizerate) - 1 + 0) / 2.0) * resRat / resizerate);
                            centerLocation.X = (float)(((float)(System.Math.Min(BlobPartPictureBox.Width, columnNum * resizerate) - 1 + 0) / 2.0) * resRat / resizerate);

                            basicClass.reduceDomain(imgPart, rectDomain, out temp);
                            if (imgPartTemp != null)
                            {
                                imgPartTemp.Dispose();
                            }
                            HOperatorSet.CropDomain(temp, out imgPartTemp);
                            HOperatorSet.DispObj(imgPartTemp, BlobPartHWHandle);
                            temp.Dispose();
                        }
                        else if (oriLocation.Y * resizerate / resRat - ((float)(BlobPartPictureBox.Height - 1 + 0) / 2.0) <= 0 && oriLocation.X * resizerate / resRat - ((float)(BlobPartPictureBox.Width - 1 + 0) / 2.0) >= 0 && oriLocation.X * resizerate / resRat + ((float)(BlobPartPictureBox.Width - 1 + 0) / 2.0) < columnNum * resizerate - 1)
                        {
                            HObject temp;
                            basicClass.genRectangle1(out rectDomain, 0, oriLocation.X * resizerate / resRat - ((float)(BlobPartPictureBox.Width - 1 + 0) / 2.0), (BlobPartPictureBox.Height - 1), oriLocation.X * resizerate / resRat + ((float)(BlobPartPictureBox.Width - 1 + 0) / 2.0));
                            centerLocation.Y = (float)(((float)(System.Math.Min(BlobPartPictureBox.Height, rowNum * resizerate) - 1 + 0) / 2.0) * resRat / resizerate);
                            centerLocation.X = (float)oriLocation.X;
                            basicClass.reduceDomain(imgPart, rectDomain, out temp);
                            if (imgPartTemp != null)
                            {
                                imgPartTemp.Dispose();
                            }
                            HOperatorSet.CropDomain(temp, out imgPartTemp);
                            HOperatorSet.DispObj(imgPartTemp, BlobPartHWHandle);
                            temp.Dispose();
                        }
                        else if (oriLocation.Y * resizerate / resRat - ((float)(BlobPartPictureBox.Height - 1 + 0) / 2.0) > 0 && oriLocation.Y * resizerate / resRat + ((float)(BlobPartPictureBox.Height - 1 + 0) / 2.0) <= rowNum * resizerate - 1 && oriLocation.X * resizerate / resRat - ((float)(BlobPartPictureBox.Width - 1 + 0) / 2.0) < 0)
                        {
                            HObject temp;
                            basicClass.genRectangle1(out rectDomain, oriLocation.Y * resizerate / resRat - ((float)(BlobPartPictureBox.Height - 1 + 0) / 2.0), 0, oriLocation.Y * resizerate / resRat + ((float)(BlobPartPictureBox.Height - 1 + 0) / 2.0), (BlobPartPictureBox.Width - 1));
                            centerLocation.Y = (float)oriLocation.Y;
                            centerLocation.X = (float)(((float)(System.Math.Min(BlobPartPictureBox.Width, columnNum * resizerate) - 1 + 0) / 2.0) * resRat / resizerate);
                            basicClass.reduceDomain(imgPart, rectDomain, out temp);
                            if (imgPartTemp != null)
                            {
                                imgPartTemp.Dispose();
                            }
                            HOperatorSet.CropDomain(temp, out imgPartTemp);
                            HOperatorSet.DispObj(imgPartTemp, BlobPartHWHandle);
                            temp.Dispose();
                        }
                        else if (oriLocation.Y * resizerate / resRat + ((float)(BlobPartPictureBox.Height - 1 + 0) / 2.0) > rowNum * resizerate - 1 && oriLocation.X * resizerate / resRat - ((float)(BlobPartPictureBox.Width - 1 + 0) / 2.0) < 0)
                        {
                            HObject temp;
                            basicClass.genRectangle1(out rectDomain, rowNum * resizerate - 1 - (BlobPartPictureBox.Height - 1), 0, rowNum * resizerate - 1, (BlobPartPictureBox.Width - 1));
                            centerLocation.Y = (float)((rowNum * resizerate - ((float)(System.Math.Min(BlobPartPictureBox.Height, rowNum * resizerate) - 1 + 0) / 2.0) - 1) * resRat / resizerate);
                            centerLocation.X = (float)(((float)(System.Math.Min(BlobPartPictureBox.Width, columnNum * resizerate) - 1 + 0) / 2.0) * resRat / resizerate);
                            basicClass.reduceDomain(imgPart, rectDomain, out temp);
                            if (imgPartTemp != null)
                            {
                                imgPartTemp.Dispose();
                            }
                            HOperatorSet.CropDomain(temp, out imgPartTemp);
                            HOperatorSet.DispObj(imgPartTemp, BlobPartHWHandle);
                            temp.Dispose();
                        }
                        else if (oriLocation.Y * resizerate / resRat + ((float)(BlobPartPictureBox.Height - 1 + 0) / 2.0) > rowNum * resizerate - 1 && oriLocation.X * resizerate / resRat - ((float)(BlobPartPictureBox.Width - 1 + 0) / 2.0) > 0 && oriLocation.X * resizerate / resRat + ((float)(BlobPartPictureBox.Width - 1 + 0) / 2.0) < columnNum * resizerate - 1)
                        {
                            HObject temp;
                            basicClass.genRectangle1(out rectDomain, rowNum * resizerate - (BlobPartPictureBox.Height), oriLocation.X * resizerate / resRat - ((float)(BlobPartPictureBox.Width - 1 + 0) / 2.0), rowNum * resizerate - 1, oriLocation.X * resizerate / resRat + ((float)(BlobPartPictureBox.Width - 1 + 0) / 2.0));
                            centerLocation.Y = (float)((rowNum * resizerate - ((float)(System.Math.Min(BlobPartPictureBox.Height, rowNum * resizerate) - 1 + 0) / 2.0 + 1)) * resRat / resizerate);
                            centerLocation.X = (float)oriLocation.X;
                            basicClass.reduceDomain(imgPart, rectDomain, out temp);
                            if (imgPartTemp != null)
                            {
                                imgPartTemp.Dispose();
                            }
                            HOperatorSet.CropDomain(temp, out imgPartTemp);
                            HOperatorSet.DispObj(imgPartTemp, BlobPartHWHandle);
                            temp.Dispose();
                        }
                        else if (oriLocation.Y * resizerate / resRat + ((float)(BlobPartPictureBox.Height - 1 + 0) / 2.0) >= rowNum * resizerate - 1 && oriLocation.X * resizerate / resRat + ((float)(BlobPartPictureBox.Width - 1 + 0) / 2.0) >= columnNum * resizerate - 1)
                        {
                            HObject temp;
                            basicClass.genRectangle1(out rectDomain, rowNum * resizerate - (BlobPartPictureBox.Height), columnNum * resizerate - (BlobPartPictureBox.Width), rowNum * resizerate - 1, columnNum * resizerate - 1);
                            centerLocation.Y = (float)((rowNum * resizerate - ((float)(System.Math.Min(BlobPartPictureBox.Height, rowNum * resizerate) - 1 + 0) / 2.0 + 1)) * resRat / resizerate);
                            centerLocation.X = (float)((columnNum * resizerate - ((float)(System.Math.Min(BlobPartPictureBox.Width, columnNum * resizerate) - 1 + 0) / 2.0 + 1)) * resRat / resizerate);
                            basicClass.reduceDomain(imgPart, rectDomain, out temp);
                            if (imgPartTemp != null)
                            {
                                imgPartTemp.Dispose();
                            }
                            HOperatorSet.CropDomain(temp, out imgPartTemp);
                            HOperatorSet.DispObj(imgPartTemp, BlobPartHWHandle);
                            temp.Dispose();
                        }
                        else if (oriLocation.Y * resizerate / resRat + ((float)(BlobPartPictureBox.Height - 1 + 0) / 2.0) < rowNum * resizerate - 1 && oriLocation.Y * resizerate / resRat - ((float)(BlobPartPictureBox.Height - 1 + 0) / 2.0) > 0 && oriLocation.X * resizerate / resRat + ((float)(BlobPartPictureBox.Width - 1 + 0) / 2.0) > columnNum * resizerate - 1)
                        {
                            HObject temp;
                            basicClass.genRectangle1(out rectDomain, oriLocation.Y * resizerate / resRat - ((float)(BlobPartPictureBox.Height - 1 + 0) / 2.0), columnNum * resizerate - (BlobPartPictureBox.Width), oriLocation.Y * resizerate / resRat + ((float)(BlobPartPictureBox.Height - 1 + 0) / 2.0), columnNum * resizerate - 1);
                            centerLocation.Y = (float)oriLocation.Y;
                            centerLocation.X = (float)((columnNum * resizerate - ((float)(System.Math.Min(BlobPartPictureBox.Width, columnNum * resizerate) - 1 + 0) / 2.0 + 1)) * resRat / resizerate);
                            basicClass.reduceDomain(imgPart, rectDomain, out temp);
                            if (imgPartTemp != null)
                            {
                                imgPartTemp.Dispose();
                            }
                            HOperatorSet.CropDomain(temp, out imgPartTemp);
                            HOperatorSet.DispObj(imgPartTemp, BlobPartHWHandle);
                            temp.Dispose();
                        }
                        else if (oriLocation.Y * resizerate / resRat - ((float)(BlobPartPictureBox.Height - 1 + 0) / 2.0) <= 0 && oriLocation.X * resizerate / resRat + ((float)(BlobPartPictureBox.Width - 1 + 0) / 2.0) >= columnNum * resizerate - 1)
                        {
                            HObject temp;
                            basicClass.genRectangle1(out rectDomain, 0, columnNum * resizerate - (BlobPartPictureBox.Width), (BlobPartPictureBox.Height - 1), columnNum * resizerate - 1);
                            centerLocation.Y = (float)(((float)(System.Math.Min(BlobPartPictureBox.Height, rowNum * resizerate) - 1 + 0) / 2.0) * resRat / resizerate);
                            centerLocation.X = (float)((columnNum * resizerate - ((float)(System.Math.Min(BlobPartPictureBox.Width, columnNum * resizerate) - 1 + 0) / 2.0 + 1)) * resRat / resizerate);
                            basicClass.reduceDomain(imgPart, rectDomain, out temp);
                            if (imgPartTemp != null)
                            {
                                imgPartTemp.Dispose();
                            }
                            HOperatorSet.CropDomain(temp, out imgPartTemp);
                            HOperatorSet.DispObj(imgPartTemp, BlobPartHWHandle);
                            temp.Dispose();
                        }
                        else
                        {
                            HObject temp;
                            basicClass.genRectangle1(out rectDomain, oriLocation.Y * resizerate / resRat - ((float)(BlobPartPictureBox.Height - 1 + 0) / 2.0), oriLocation.X * resizerate / resRat - ((float)(BlobPartPictureBox.Width - 1 + 0) / 2.0), oriLocation.Y * resizerate / resRat + ((float)(BlobPartPictureBox.Height - 1 + 0) / 2.0), oriLocation.X * resizerate / resRat + ((float)(BlobPartPictureBox.Width - 1 + 0) / 2.0));
                            basicClass.reduceDomain(imgPart, rectDomain, out temp);
                            if (imgPartTemp != null)
                            {
                                imgPartTemp.Dispose();
                            }
                            HOperatorSet.CropDomain(temp, out imgPartTemp);
                            HOperatorSet.DispObj(imgPartTemp, BlobPartHWHandle);
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
        private void buttonGetImage_Click(object sender, EventArgs e)
        {
            try
            {
                HObject imageCopy;
                UserCode.GetInstance().getImageFromProcess(out imageCopy, currentIndex);
                basicClass.getImageSize(imageCopy, out rowNum, out columnNum);

                    if (image != null)
                    {
                        image.Dispose();
                    }
                    HOperatorSet.CopyImage(imageCopy, out image);
                    HTuple channelOfImage;
                    HOperatorSet.CountChannels(image, out channelOfImage);
                    if (channelOfImage == 1)
                    {
                        HObject image11, image12;
                        HOperatorSet.CopyImage(image, out image11);
                        HOperatorSet.CopyImage(image, out image12);
                        if (imageTemp != null)
                        {
                            imageTemp.Dispose();
                        }
                        HOperatorSet.Compose3(image, image11, image12, out imageTemp);
                        if (imageTempShow != null)
                        {
                            imageTempShow.Dispose();
                        }
                        HOperatorSet.CopyImage(imageTemp, out imageTempShow);
                        image11.Dispose();
                        image12.Dispose();
                    }
                    else
                    {
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
                        HOperatorSet.Decompose3(image, out image1, out image2, out image3);
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
                    basicClass.displayhobject(img, BlobHWHandle);
                    if (imgPart != null)
                    {
                        imgPart.Dispose();
                    }
                    basicClass.resizeImage(imageTemp, out imgPart, resizerate);
                    HObject temp;
                    basicClass.genRectangle1(out rectDomain, 0, 0, (BlobPartPictureBox.Height - 1), (BlobPartPictureBox.Width - 1));
                    basicClass.reduceDomain(imgPart, rectDomain, out temp);
                    centerLocation.Y = (float)(((float)(System.Math.Min(BlobPartPictureBox.Height, rowNum * resizerate) - 1 + 0) / 2.0) * resRat / resizerate);
                    centerLocation.X = (float)(((float)(System.Math.Min(BlobPartPictureBox.Width, columnNum * resizerate) - 1 + 0) / 2.0) * resRat / resizerate);
                    if (imgPartTemp != null)
                    {
                        imgPartTemp.Dispose();
                    }
                    HOperatorSet.CropDomain(temp, out imgPartTemp);
                    HOperatorSet.DispObj(imgPartTemp, BlobPartHWHandle);
                    temp.Dispose();
                    oriLocation.X = (int)centerLocation.X;
                    oriLocation.Y = (int)centerLocation.Y;
                    BlobSegmentImageShow();
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
        private void checkedListBoxSelectColorGray_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < 6; i++)
                {
                    if (checkedListBoxSelectColorGray.GetSelected(i))
                    {
                        comboBoxSetColorGray.SelectedIndex = i;
                    }
                    if (checkedListBoxSelectColorGray.GetItemChecked(i))
                    {
                        cSP.colorSelectNum[i] = true;
                    }
                    else
                    {
                        cSP.colorSelectNum[i] = false;
                    }
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            //BlobSegmentImageShow();
        }
        private void checkBoxIsAutoSegment_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (checkBoxIsAutoSegment.Checked == true)
                {
                    groupBoxColorGraySegment.Enabled = false;
                    //groupBoxGraySegment.Enabled = false;
                    groupBoxAutoSegment.Enabled = true;
                    radioButtonSegmentShowWholeImage.Checked = true;
                    radioButtonSegmentShowPointImage.Checked = false;
                    radioButtonSegmentShowPointImage.Enabled = false;

                }
                else
                {

                    groupBoxAutoSegment.Enabled = false;
                    checkBoxAuto1.Checked = false;
                    checkBoxAuto2.Checked = false;
                    checkBoxAuto3.Checked = false;
                    groupBoxColorGraySegment.Enabled = true;
                    checkedListBoxSelectColorGray.SetSelected(0, true);
                    comboBoxSetColorGray.SelectedIndex = 0;
                    radioButtonSegmentShowPointImage.Enabled = true;
                    radioButtonSegmentShowPointImage.Checked = true;
                    if (UserCode.GetInstance().gProCd[currentIndex].gBP.isColor == true)
                    {
                        panelRGB.Enabled = true;
                        panelGray.Enabled = false;



                        //comboBoxPara24.SelectedIndex = 0;
                    }
                    else
                    {
                        panelRGB.Enabled = false;
                        panelGray.Enabled = true;
                    }
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void comboBoxSetColor_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (UserCode.GetInstance().gProCd[currentIndex].gBP.isColor == true)
                {
                    numericUpDownRed.Value = (decimal)cSP.redColor[comboBoxSetColorGray.SelectedIndex];
                    numericUpDownGreen.Value = (decimal)cSP.greenColor[comboBoxSetColorGray.SelectedIndex];
                    numericUpDownBlue.Value = (decimal)cSP.blueColor[comboBoxSetColorGray.SelectedIndex];
                }
                else
                {
                    numericUpDownGray.Value = (decimal)cSP.grayValue[comboBoxSetColorGray.SelectedIndex];

                }

                numericUpDownLength.Value = (decimal)cSP.colorLength[comboBoxSetColorGray.SelectedIndex];
                checkBoxBesideColorGray.Checked = cSP.isBesideThisColor[comboBoxSetColorGray.SelectedIndex];
                checkedListBoxSelectColorGray.SetSelected(comboBoxSetColorGray.SelectedIndex, true);
                BlobSegmentImageShow();
            }

            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }
        private void numericUpDownRed_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                numericUpDownLength.Maximum =
                    Math.Min(Math.Min(Math.Min(Math.Abs(numericUpDownRed.Value - numericUpDownRed.Minimum), Math.Abs(numericUpDownRed.Value - numericUpDownRed.Maximum))
                    , Math.Min(Math.Abs(numericUpDownGreen.Value - numericUpDownGreen.Minimum), Math.Abs(numericUpDownGreen.Value - numericUpDownGreen.Maximum))),
                    Math.Min(Math.Abs(numericUpDownBlue.Value - numericUpDownBlue.Minimum), Math.Abs(numericUpDownBlue.Value - numericUpDownBlue.Maximum)));

                cSP.redColor[comboBoxSetColorGray.SelectedIndex] = (int)numericUpDownRed.Value;
                //radioButtonSegmentShowPointImage.Checked = true;
                BlobSegmentImageShow();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void numericUpDownGreen_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                numericUpDownLength.Maximum =
                    Math.Min(Math.Min(Math.Min(Math.Abs(numericUpDownRed.Value - numericUpDownRed.Minimum), Math.Abs(numericUpDownRed.Value - numericUpDownRed.Maximum))
                    , Math.Min(Math.Abs(numericUpDownGreen.Value - numericUpDownGreen.Minimum), Math.Abs(numericUpDownGreen.Value - numericUpDownGreen.Maximum))),
                    Math.Min(Math.Abs(numericUpDownBlue.Value - numericUpDownBlue.Minimum), Math.Abs(numericUpDownBlue.Value - numericUpDownBlue.Maximum)));
                cSP.greenColor[comboBoxSetColorGray.SelectedIndex] = (int)numericUpDownGreen.Value;
                //radioButtonSegmentShowPointImage.Checked = true;
                BlobSegmentImageShow();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void numericUpDownBlue_ValueChanged(object sender, EventArgs e)
        {



            try
            {
                numericUpDownLength.Maximum =
                    Math.Min(Math.Min(Math.Min(Math.Abs(numericUpDownRed.Value - numericUpDownRed.Minimum), Math.Abs(numericUpDownRed.Value - numericUpDownRed.Maximum))
                    , Math.Min(Math.Abs(numericUpDownGreen.Value - numericUpDownGreen.Minimum), Math.Abs(numericUpDownGreen.Value - numericUpDownGreen.Maximum))),
                    Math.Min(Math.Abs(numericUpDownBlue.Value - numericUpDownBlue.Minimum), Math.Abs(numericUpDownBlue.Value - numericUpDownBlue.Maximum)));
                cSP.blueColor[comboBoxSetColorGray.SelectedIndex] = (int)numericUpDownBlue.Value;
                //radioButtonSegmentShowPointImage.Checked = true;
                BlobSegmentImageShow();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void numericUpDownLength_ValueChanged(object sender, EventArgs e)
        {

            try
            {
                cSP.colorLength[comboBoxSetColorGray.SelectedIndex] = (int)numericUpDownLength.Value;
                //radioButtonSegmentShowPointImage.Checked = true;
                BlobSegmentImageShow();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void checkBoxBesideColor_CheckedChanged(object sender, EventArgs e)
        {

            try
            {
                if (checkBoxBesideColorGray.Checked == true)
                {
                    cSP.isBesideThisColor[comboBoxSetColorGray.SelectedIndex] = true;
                }
                else
                {
                    cSP.isBesideThisColor[comboBoxSetColorGray.SelectedIndex] = false;
                }
                BlobSegmentImageShow();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void checkBoxAuto1_CheckedChanged(object sender, EventArgs e)
        {

            try
            {
                if (checkBoxAuto1.Checked == true)
                {
                    numericUpDownPara11.Enabled = true;
                    checkBoxAuto2.Checked = false;
                    checkBoxAuto3.Checked = false;
                    BlobSegmentImageShow();
                }
                else
                {
                    numericUpDownPara11.Enabled = false;
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void checkBoxAuto2_CheckedChanged(object sender, EventArgs e)
        {


            try
            {
                if (checkBoxAuto2.Checked == true)
                {
                    panelAutoSegmentMethod2.Enabled = true;
                    checkBoxAuto1.Checked = false;
                    checkBoxAuto3.Checked = false;
                    BlobSegmentImageShow();
                }
                else
                {
                    panelAutoSegmentMethod2.Enabled = false;
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonGetColorFromScreen_Click(object sender, EventArgs e)
        {
            try
            {
                if (imgPartTemp != null)
                {
                    radioButtonSegmentShowOriImage.Checked = true;
                    tabControlBlob.Enabled = false;
                    BlobPictureBox.Enabled = false;
                    this.ControlBox = false;
                    HTuple pointDisplayX, pointDisplayY;
                    this.BlobPartPictureBox.Focus();
                    HOperatorSet.SetColor(BlobPartHWHandle, "red");
                    HOperatorSet.SetLineWidth(BlobPartHWHandle, 4);
                    HOperatorSet.DrawPoint(BlobPartHWHandle, out pointDisplayY, out pointDisplayX);
                    HTuple hvHomMat2D;
                    HOperatorSet.HomMat2dIdentity(out hvHomMat2D);
                    HOperatorSet.VectorAngleToRigid(0, 0, 0, centerLocation.Y / resRat * resizerate + (-((float)(System.Math.Min(BlobPartPictureBox.Height, rowNum * resizerate) - 1 + 0) / 2.0)), centerLocation.X / resRat * resizerate + (-((float)(System.Math.Min(BlobPartPictureBox.Width, columnNum * resizerate) - 1 + 0) / 2.0)), 0, out hvHomMat2D);
                    HOperatorSet.HomMat2dScale(hvHomMat2D, 1.0 / resizerate, 1.0 / resizerate, 0, 0, out hvHomMat2D);
                    HOperatorSet.AffineTransPixel(hvHomMat2D, pointDisplayY, pointDisplayX, out pointDisplayY, out pointDisplayX);
                    if (pointDisplayY < rowNum && pointDisplayX < columnNum && pointDisplayX >= 0 && pointDisplayY >= 0)
                    {
                        HTuple grayval;
                        HOperatorSet.GetGrayval(image, pointDisplayY, pointDisplayX, out grayval);
                        if (UserCode.GetInstance().gProCd[currentIndex].gBP.isColor == true)
                        {
                            numericUpDownRed.Value = (decimal)grayval[0];
                            numericUpDownGreen.Value = (decimal)grayval[1];
                            numericUpDownBlue.Value = (decimal)grayval[2];
                        }
                        else
                        {
                            numericUpDownGray.Value = (decimal)grayval[0];

                        }

                    }
                    radioButtonSegmentShowPointImage.Checked = true;
                    BlobSegmentImageShow();
                    BlobPictureBox.Enabled = true;
                    this.ControlBox = true;
                    tabControlBlob.Enabled = true;
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void checkBoxAuto3_CheckedChanged(object sender, EventArgs e)
        {

            try
            {
                if (checkBoxAuto3.Checked == true)
                {
                    checkBoxAuto1.Checked = false;
                    checkBoxAuto2.Checked = false;
                    BlobSegmentImageShow();
                }
                else
                {
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void radioButtonSegmentShowOriImage_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                BlobSegmentImageShow();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void radioButtonSegmentShowWholeImage_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                BlobSegmentImageShow();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void radioButtonSegmentShowPointImage_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                BlobSegmentImageShow();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void numericUpDownMin_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                BlobSegmentImageShow();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void numericUpDownPara21_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                BlobSegmentImageShow();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void numericUpDownPara22_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                BlobSegmentImageShow();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void numericUpDownPara23_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                BlobSegmentImageShow();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void comboBoxPara24_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                BlobSegmentImageShow();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void numericUpDownPara11_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                BlobSegmentImageShow();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void numericUpDownGray_ValueChanged(object sender, EventArgs e)
        {

            try
            {
                numericUpDownLength.Maximum = Math.Min(Math.Abs(numericUpDownGray.Value - numericUpDownGray.Minimum), Math.Abs(numericUpDownGray.Value - numericUpDownGray.Maximum));

                cSP.grayValue[comboBoxSetColorGray.SelectedIndex] = (int)numericUpDownGray.Value;
                //radioButtonSegmentShowPointImage.Checked = true;
                BlobSegmentImageShow();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void checkedListBoxSelectColorGray_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {

                BlobSegmentImageShow();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void checkBoxOpening_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (checkBoxOpening.Checked == true)
                {
                    groupBoxOpening.Enabled = true;
                }
                else
                {
                    groupBoxOpening.Enabled = false;
                }
                BlobMorphologyImageShow();
            }

            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }



        private void buttonSegmentConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                radioButtonSegmentShowWholeImage.Checked = true;
                BlobMorphologyImageShow();
                if (segmentRegion != null)
                {
                    segmentRegion.Dispose();
                }
                if (checkBoxIsAutoSegment.Checked == true)
                {
                    HOperatorSet.CopyObj(thresholdTestRegion, out segmentRegion, 1, -1);
                }
                else
                {
                    if (UserCode.GetInstance().gProCd[currentIndex].gBP.isColor == true)
                    {
                        HOperatorSet.CopyObj(thresholdColorRegion, out segmentRegion, 1, -1);
                    }
                    else
                    {
                        HOperatorSet.CopyObj(thresholdGrayRegion, out segmentRegion, 1, -1);
                    }
                }
                for (int i = 0; i < 6; i++)
                {
                    if (checkedListBoxSelectColorGray.GetItemChecked(i))
                    {
                        cSP.colorSelectNum[i] = true;
                    }
                    else
                    {
                        cSP.colorSelectNum[i] = false;
                    }
                }
                panelSegment.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void buttonSegmentChange_Click(object sender, EventArgs e)
        {
            try
            {
                UserCode.GetInstance().isOverFlag[currentIndex] = 10;
                panelSegment.Enabled = true;
                if (segmentRegion != null)
                {
                    segmentRegion.Dispose();
                    segmentRegion = null;
                }

                if (morphologyRegion != null)
                {
                    morphologyRegion.Dispose();
                    morphologyRegion = null;
                }
                if (selectedRegion != null)
                {
                    selectedRegion.Dispose();
                    selectedRegion = null;
                }
                panelOkAndShow.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void checkBoxFillUpHoles_CheckedChanged(object sender, EventArgs e)
        {

            try
            {
                if (checkBoxFillUpHoles.Checked == true)
                {
                    groupBoxFillUpHoles.Enabled = true;
                }
                else
                {
                    groupBoxFillUpHoles.Enabled = false;
                }
                BlobMorphologyImageShow();
            }

            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
            
        }

        private void checkBoxErosion_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (checkBoxErosion.Checked == true)
                {
                    groupBoxErosion.Enabled = true;
                }
                else
                {
                    groupBoxErosion.Enabled = false;
                }
                BlobMorphologyImageShow();
            }

            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }

        private void checkBoxDilation_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (checkBoxDilation.Checked == true)
                {
                    groupBoxDilation.Enabled = true;
                }
                else
                {
                    groupBoxDilation.Enabled = false;
                }
                BlobMorphologyImageShow();
            }

            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }

        private void checkBoxClosing_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (checkBoxClosing.Checked == true)
                {
                    groupBoxClosing.Enabled = true;
                }
                else
                {
                    groupBoxClosing.Enabled = false;
                }
                BlobMorphologyImageShow();
            }

            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }

        private void radioButtonRectangleErosion_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (radioButtonRectangleErosion.Checked == true)
                {
                    panelRectangleErosion.Enabled = true;
                }
                else
                {
                    panelRectangleErosion.Enabled = false;
                }
                BlobMorphologyImageShow();
            }

            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }

        private void radioButtonCircleErosion_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (radioButtonCircleErosion.Checked == true)
                {
                    panelCircleErosion.Enabled = true;
                }
                else
                {
                    panelCircleErosion.Enabled = false;
                }
                BlobMorphologyImageShow();
            }

            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }

        private void radioButtonRectangleDilation_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (checkBoxClosing.Checked == true)
                {
                    panelRectangleDilation.Enabled = true;
                }
                else
                {
                    panelRectangleDilation.Enabled = false;
                }
                BlobMorphologyImageShow();
            }

            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }

        private void radioButtonCircleDilation_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (radioButtonCircleDilation.Checked == true)
                {
                    panelCircleDilation.Enabled = true;
                }
                else
                {
                    panelCircleDilation.Enabled = false;
                }
                BlobMorphologyImageShow();
            }

            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }

        private void radioButtonRectangleOpening_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (radioButtonRectangleOpening.Checked == true)
                {
                    panelRectangleOpening.Enabled = true;
                }
                else
                {
                    panelRectangleOpening.Enabled = false;
                }
                BlobMorphologyImageShow();
            }

            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }

        private void radioButtonCircleOpening_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (radioButtonCircleOpening.Checked == true)
                {
                    panelCircleOpening.Enabled = true;
                }
                else
                {
                    panelCircleOpening.Enabled = false;
                }
                BlobMorphologyImageShow();
            }

            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }

        private void radioButtonRectangleClosing_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (radioButtonRectangleClosing.Checked == true)
                {
                    panelRectangleClosing.Enabled = true;
                }
                else
                {
                    panelRectangleClosing.Enabled = false;
                }
                BlobMorphologyImageShow();
            }

            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }

        private void radioButtonCircleClosing_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (radioButtonCircleClosing.Checked == true)
                {
                    panelCircleClosing.Enabled = true;
                }
                else
                {
                    panelCircleClosing.Enabled = false;
                }
                BlobMorphologyImageShow();
            }

            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }
        private void BlobMorphologyImageShow()
        {
            if (segmentRegion != null)
            {
                HOperatorSet.CopyObj(segmentRegion, out morphologyTestRegion, 1, -1);
                if (checkBoxFillUpHoles.Checked == true)
                {
                    if (radioButtonConnectionsBeforeFillUpHoles.Checked)
                    {
                        HOperatorSet.Connection(morphologyTestRegion, out morphologyTestRegion);
                    }
                    HOperatorSet.FillUp(morphologyTestRegion, out morphologyTestRegion);
                }
                if (checkBoxErosion.Checked == true)
                {
                    if (radioButtonRectangleErosion.Checked == true)
                    {
                        HOperatorSet.ErosionRectangle1(morphologyTestRegion, out morphologyTestRegion, (double)numericUpDownWidthErosion.Value, (double)numericUpDownHeightErosion.Value);
                    }
                    else
                    {
                        HOperatorSet.ErosionCircle(morphologyTestRegion, out morphologyTestRegion, (double)numericUpDownRadiusErosion.Value);
                    }
                }
                if (checkBoxDilation.Checked == true)
                {
                    if (radioButtonRectangleDilation.Checked == true)
                    {
                        HOperatorSet.DilationRectangle1(morphologyTestRegion, out morphologyTestRegion, (double)numericUpDownWidthDilation.Value, (double)numericUpDownHeightDilation.Value);
                    }
                    else
                    {
                        HOperatorSet.DilationCircle(morphologyTestRegion, out morphologyTestRegion, (double)numericUpDownRadiusDilation.Value);
                    }
                }
                if (checkBoxOpening.Checked == true)
                {
                    if (radioButtonRectangleOpening.Checked == true)
                    {
                        HOperatorSet.ErosionRectangle1(morphologyTestRegion, out morphologyTestRegion, (double)numericUpDownWidthOpening.Value, (double)numericUpDownHeightOpening.Value);
                        HOperatorSet.Connection(morphologyTestRegion, out morphologyTestRegion);
                        HOperatorSet.DilationRectangle1(morphologyTestRegion, out morphologyTestRegion, (double)numericUpDownWidthOpening.Value, (double)numericUpDownHeightOpening.Value);
                    }
                    else
                    {
                        HOperatorSet.ErosionCircle(morphologyTestRegion, out morphologyTestRegion, (double)numericUpDownRadiusOpening.Value);
                        HOperatorSet.Connection(morphologyTestRegion, out morphologyTestRegion);
                        HOperatorSet.DilationCircle(morphologyTestRegion, out morphologyTestRegion, (double)numericUpDownRadiusOpening.Value);
                    }
                }
                if (checkBoxClosing.Checked == true)
                {
                    if (radioButtonRectangleClosing.Checked == true)
                    {
                        HOperatorSet.DilationRectangle1(morphologyTestRegion, out morphologyTestRegion, (double)numericUpDownWidthClosing.Value, (double)numericUpDownHeightClosing.Value);
                        HOperatorSet.ErosionRectangle1(morphologyTestRegion, out morphologyTestRegion, (double)numericUpDownWidthClosing.Value, (double)numericUpDownHeightClosing.Value);
                    }
                    else
                    {
                        HOperatorSet.DilationCircle(morphologyTestRegion, out morphologyTestRegion, (double)numericUpDownRadiusClosing.Value);
                        HOperatorSet.ErosionCircle(morphologyTestRegion, out morphologyTestRegion, (double)numericUpDownRadiusClosing.Value);

                    }
                }
                HOperatorSet.Connection(morphologyTestRegion, out morphologyTestRegion);
                if (imageTempShow != null)
                {
                    imageTempShow.Dispose();
                }
                //HOperatorSet.RegionToMean(morphologyTestRegion, image, out imageTempShow);
                HTuple hv_grayval = new HTuple();
                hv_grayval[0] = 0;
                hv_grayval[1] = 255;
                hv_grayval[2] = 0;
                HOperatorSet.PaintRegion(morphologyTestRegion, imageTemp, out imageTempShow, hv_grayval, "margin");
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
                basicClass.displayhobject(img, BlobHWHandle);
                partImageWindowShow(oriLocation);
            }




        }

        private void numericUpDownWidthErosion_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                BlobMorphologyImageShow();
            }

            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }

        private void numericUpDownHeightErosion_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                BlobMorphologyImageShow();
            }

            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }

        private void numericUpDownRadiusErosion_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                BlobMorphologyImageShow();
            }

            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }

        private void numericUpDownWidthDilation_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                BlobMorphologyImageShow();
            }

            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }

        private void numericUpDownHeightDilation_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                BlobMorphologyImageShow();
            }

            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }

        private void numericUpDownRadiusDilation_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                BlobMorphologyImageShow();
            }

            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }

        private void numericUpDownWidthOpening_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                BlobMorphologyImageShow();
            }

            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }

        private void numericUpDownHeightOpening_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                BlobMorphologyImageShow();
            }

            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }

        private void numericUpDownRadiusOpening_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                BlobMorphologyImageShow();
            }

            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }

        private void numericUpDownWidthClosing_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                BlobMorphologyImageShow();
            }

            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }

        private void numericUpDownHeightClosing_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                BlobMorphologyImageShow();
            }

            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }

        private void numericUpDownRadiusClosing_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                BlobMorphologyImageShow();
            }

            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }

        private void tabControlBlob_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (tabControlBlob.SelectedIndex == 0)
                {
                    BlobSegmentImageShow();
                }
                if (tabControlBlob.SelectedIndex == 1)
                {

                    if (segmentRegion == null)
                    {
                        panelMorphologyWhole.Enabled = false;

                        MessageBox.Show("错误！分割区域未生成！");

                    }
                    else
                    {
                        panelMorphologyWhole.Enabled = true;
                    }
                    BlobMorphologyImageShow();
                }
                if (tabControlBlob.SelectedIndex == 2)
                {

                    if (morphologyRegion == null)
                    {
                        panelSelectWhole.Enabled = false;

                        MessageBox.Show("错误！形态学处理操作未确认！");

                    }
                    else
                    {
                        panelSelectWhole.Enabled = true;
                    }
                    selectRegionFeatureValueShow();
                    BlobSelectRegionImageShow();
                }
                if (tabControlBlob.SelectedIndex == 3)
                {

                    if (selectedRegion == null)
                    {
                        panelOutputWhole.Enabled = false;
                    }
                    else
                    {
                        panelOutputWhole.Enabled = true;
                    }
                    // selectRegionFeatureValueShow();
                    outputRegionFeatureShow();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonMorphologyConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                BlobMorphologyImageShow();
                if (morphologyRegion != null)
                {
                    morphologyRegion.Dispose();
                }

                HOperatorSet.CopyObj(morphologyTestRegion, out morphologyRegion, 1, -1);

                panelMorphology.Enabled = false;
                
            }

            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }

        }

        private void buttonChange_Click(object sender, EventArgs e)
        {
            UserCode.GetInstance().isOverFlag[currentIndex] = 10;
            if (morphologyRegion != null)
            {
                morphologyRegion.Dispose();
                morphologyRegion = null;
            }
            if (selectedRegion != null)
            {
                selectedRegion.Dispose();
                selectedRegion = null;
            }
            panelMorphology.Enabled = true;
            panelOkAndShow.Enabled = false;
        }

        private void checkedListBoxSelectItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < 34; i++)
            {
                if (checkedListBoxSelectItem.GetSelected(i))
                {
                    comboBoxSelectItem.SelectedIndex = i;
                }
                if (checkedListBoxSelectItem.GetItemChecked(i))
                {
                    sP.selectIsChecked[i] = true;
                }
                else
                {
                    sP.selectIsChecked[i] = false;
                }
            }
            if (morphologyRegion != null)
            {
                BlobSelectRegionImageShow();
            }
        }
        private void selectRegionFeatureValueShow()
        {
            if (morphologyRegion != null)
            {
                HObject imgSelectTest;
                if (UserCode.GetInstance().gProCd[currentIndex].gBP.isColor == true)
                {
                    HOperatorSet.Rgb1ToGray(image, out imgSelectTest);
                }
                else
                {
                    HOperatorSet.CopyObj(image, out imgSelectTest, 1, -1);
                }
                HTuple testFeatureValue = null;
                if (comboBoxSelectItem.SelectedIndex < 30)
                {
                    HOperatorSet.RegionFeatures(morphologyRegion, (HTuple)sP.selectSTR[comboBoxSelectItem.SelectedIndex], out testFeatureValue);
                    numericUpDownMaxItemValue.Value = (decimal)testFeatureValue.TupleMax()[0].D;
                    numericUpDownMinItemValue.Value = (decimal)testFeatureValue.TupleMin()[0].D;
                }
                else
                {
                    HOperatorSet.GrayFeatures(morphologyRegion, imgSelectTest, (HTuple)sP.selectSTR[comboBoxSelectItem.SelectedIndex], out testFeatureValue);
                    numericUpDownMaxItemValue.Value = (decimal)testFeatureValue.TupleMax()[0].D;
                    numericUpDownMinItemValue.Value = (decimal)testFeatureValue.TupleMin()[0].D;
                }

                if (imgSelectTest != null)
                {
                    imgSelectTest.Dispose();
                }
            }
        }
        private void comboBoxSelectItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (morphologyRegion != null)
            {
                try
                {

                    numericUpDownMaxSetValue.Value = (decimal)sP.selectMax[comboBoxSelectItem.SelectedIndex];
                    numericUpDownMinSetValue.Value = (decimal)sP.selectMin[comboBoxSelectItem.SelectedIndex];
                    checkedListBoxSelectItem.SetSelected(comboBoxSelectItem.SelectedIndex, true);
                    selectRegionFeatureValueShow();
                    BlobSelectRegionImageShow();
                    //if (sP.selectisAnd[comboBoxSelectItem.SelectedIndex])
                    //{
                    //    radioButtonSelectAnd.Checked = true;
                    //} 
                    //else
                    //{
                    //    radioButtonSelectOR.Checked = true;
                    //}

                }

                catch (Exception ee)
                {
                    MessageBox.Show(ee.Message);
                }
            }

        }

        private void numericUpDownMinSetValue_ValueChanged(object sender, EventArgs e)
        {

            sP.selectMin[comboBoxSelectItem.SelectedIndex] = (double)numericUpDownMinSetValue.Value;
            BlobSelectRegionImageShow();
        }

        private void numericUpDownMaxSetValue_ValueChanged(object sender, EventArgs e)
        {
            numericUpDownMinSetValue.Maximum = (decimal)numericUpDownMaxSetValue.Value;
            sP.selectMax[comboBoxSelectItem.SelectedIndex] = (double)numericUpDownMaxSetValue.Value;
            BlobSelectRegionImageShow();
        }

        private void radioButtonSelectAnd_CheckedChanged(object sender, EventArgs e)
        {
            BlobSelectRegionImageShow();
            //if (radioButtonSelectAnd.Checked)
            //{
            //     sP.selectisAnd[comboBoxSelectItem.SelectedIndex]= true;
            //}
            //else
            //{
            //    sP.selectisAnd[comboBoxSelectItem.SelectedIndex] = false;
            //}

        }


        private void radioButtonSelectOR_CheckedChanged(object sender, EventArgs e)
        {
            BlobSelectRegionImageShow();
            //if (radioButtonSelectOR.Checked)
            //{
            //    sP.selectisAnd[comboBoxSelectItem.SelectedIndex] = false;
            //}
            //else
            //{
            //    sP.selectisAnd[comboBoxSelectItem.SelectedIndex] = true;
            //}

        }
        private void BlobSelectRegionImageShow()
        {
            try
            {
                if (morphologyRegion != null)
                {
                    HOperatorSet.CopyObj(morphologyRegion, out selectedTestRegion, 1, -1);

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
                        if (sP.selectIsChecked[i])
                        {
                            if (selectMin1 == null)
                            {
                                selectMin1 = sP.selectMin[i];
                                selectMax1 = sP.selectMax[i];
                                feature1 = (HTuple)sP.selectSTR[i];
                            }
                            else
                            {
                                selectMin1 = selectMin1.TupleConcat(sP.selectMin[i]);
                                selectMax1 = selectMax1.TupleConcat(sP.selectMax[i]);
                                feature1 = feature1.TupleConcat((HTuple)sP.selectSTR[i]);
                            }
                        }
                    }
                    if (feature1 != null)
                    {
                        if (radioButtonSelectAnd.Checked == true)
                        {
                            HOperatorSet.SelectShape(selectedTestRegion, out selectedTestRegion, feature1, "and", selectMin1, selectMax1);
                        }
                        else
                        {
                            HOperatorSet.SelectShape(selectedTestRegion, out selectedTestRegion, feature1, "or", selectMin1, selectMax1);
                        }
                    }

                    for (int i = 30; i < 34; i++)
                    {
                        if (sP.selectIsChecked[i])
                        {
                            if (selectMin2 == null)
                            {
                                selectMin2 = sP.selectMin[i];
                                selectMax2 = sP.selectMax[i];
                                feature2 = (HTuple)sP.selectSTR[i];
                            }
                            else
                            {
                                selectMin2 = selectMin2.TupleConcat(sP.selectMin[i]);
                                selectMax2 = selectMax2.TupleConcat(sP.selectMax[i]);
                                feature2 = feature2.TupleConcat((HTuple)sP.selectSTR[i]);
                            }
                        }
                    }
                    if (feature2 != null)
                    {
                        HObject imgSelectTest;
                        if (UserCode.GetInstance().gProCd[currentIndex].gBP.isColor == true)
                        {
                            HOperatorSet.Rgb1ToGray(image, out imgSelectTest);
                        }
                        else
                        {
                            HOperatorSet.CopyObj(image, out imgSelectTest, 1, -1);
                        }

                        if (radioButtonSelectAnd.Checked == true)
                        {
                            HOperatorSet.SelectGray(selectedTestRegion, imgSelectTest, out selectedTestRegion, feature2, "and", selectMin2, selectMax2);
                        }
                        else
                        {
                            HOperatorSet.SelectGray(selectedTestRegion, imgSelectTest, out selectedTestRegion, feature2, "or", selectMin2, selectMax2);
                        }
                        if (imgSelectTest != null)
                        {
                            imgSelectTest.Dispose();
                        }

                    }
                    string[] arrangeSelectStr = new string[6] { "row", "column", "row1", "column1", "row2", "column2" };
                    if (selectedTestRegion.CountObj() != 0)
                    {
                        HTuple featureValue, sortedFeatureValue, sortedFeatureValueIndices;
                        HOperatorSet.RegionFeatures(selectedTestRegion, arrangeSelectStr[comboBoxArrange.SelectedIndex], out featureValue);
                        if (radioButtonLtoS.Checked)
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
                        if (selectedTestRegion.CountObj() > (int)numericUpDownRegionMaxNumber.Value)
                        {
                            HOperatorSet.SelectObj(selectedTestRegion, out selectedTestRegion, sortedFeatureValueIndices.TupleSelectRange(0, (HTuple)numericUpDownRegionMaxNumber.Value - 1));
                        }
                        else
                        {
                            HOperatorSet.SelectObj(selectedTestRegion, out selectedTestRegion, sortedFeatureValueIndices);
                        }
                    }
                    numericUpDownCurNum.Value = (decimal)selectedTestRegion.CountObj();
                    if (selectedTestRegion.CountObj() != 0)
                    {
                        HTuple hv_grayval = new HTuple();
                        hv_grayval[0] = 0;
                        hv_grayval[1] = 255;
                        hv_grayval[2] = 0;

                        HObject im1test;
                        HOperatorSet.GenEmptyObj(out im1test);
                        if (imageTempShowWithOutPoint != null)
                        {
                            imageTempShowWithOutPoint.Dispose();
                        }
                       // HOperatorSet.RegionToMean(selectedTestRegion, imageTemp, out imageTempShowWithOutPoint);
                        HOperatorSet.PaintRegion(selectedTestRegion, imageTemp, out imageTempShowWithOutPoint, hv_grayval, "margin");
                        HTuple harea, hrow, hcolumn;
                        HOperatorSet.AreaCenter(selectedTestRegion, out harea, out hrow, out hcolumn);
                        HObject tempCross;
                        HOperatorSet.GenEmptyObj(out tempCross);
                        //HOperatorSet.GenEmptyObj(out regionCross);

                        for (int i = 0; i < hrow.TupleLength(); i++)
                        {

                            HObject regionLineh, regionLinev, regionCross;
                            HOperatorSet.GenRectangle1(out regionLineh, hrow[i] - 3, hcolumn[i] - 20, hrow[i] + 3, hcolumn[i] + 20);
                            HOperatorSet.GenRectangle1(out regionLinev, hrow[i] - 20, hcolumn[i] - 3, hrow[i] + 20, hcolumn[i] + 3);
                            HOperatorSet.Union2(regionLineh, regionLinev, out regionCross);
                            if (i == 0)
                            {
                                HOperatorSet.CopyObj(regionCross, out tempCross, 1, -1);

                            }
                            else
                            {
                                HOperatorSet.ConcatObj(tempCross, regionCross, out tempCross);
                            }
                            if (regionLineh != null)
                            {
                                regionLineh.Dispose();
                            }
                            if (regionLinev != null)
                            {
                                regionLinev.Dispose();
                            }
                            if (regionCross != null)
                            {
                                regionCross.Dispose();
                            }
                        }
                        if (imageTempShow != null)
                        {
                            imageTempShow.Dispose();
                        }
                        HOperatorSet.PaintRegion(tempCross, imageTempShowWithOutPoint, out imageTempShow, hv_grayval, "fill");
                        if (tempCross != null)
                        {
                            tempCross.Dispose();
                        }
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
                        basicClass.displayhobject(img, BlobHWHandle);
                        partImageWindowShow(oriLocation);
                    }
                    else
                    {
                        HObject ui;
                        HOperatorSet.Threshold(image, out ui, 128, 255);
                        HOperatorSet.RegionToBin(ui, out imageTempShow, 0, 0, columnNum, rowNum);
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
                        basicClass.displayhobject(img, BlobHWHandle);
                        partImageWindowShow(oriLocation);
                        ui.Dispose();
                    }


                }

            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }

        }

        private void checkedListBoxSelectItem_SelectedValueChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < 34; i++)
            {
                if (checkedListBoxSelectItem.GetSelected(i))
                {
                    comboBoxSelectItem.SelectedIndex = i;
                }
                if (checkedListBoxSelectItem.GetItemChecked(i))
                {
                    sP.selectIsChecked[i] = true;
                }
                else
                {
                    sP.selectIsChecked[i] = false;
                }
            }
            if (morphologyRegion != null)
            {
                BlobSelectRegionImageShow();
            }
        }



        private void numericUpDownRegionMaxNumber_ValueChanged(object sender, EventArgs e)
        {

            BlobSelectRegionImageShow();
        }

        private void radioButtonLtoS_CheckedChanged(object sender, EventArgs e)
        {
            BlobSelectRegionImageShow();
        }

        private void radioButtonStoL_CheckedChanged(object sender, EventArgs e)
        {
            BlobSelectRegionImageShow();
        }

        private void comboBoxArrange_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (morphologyRegion != null)
            {
                BlobSelectRegionImageShow();
            }
        }

        private void buttonSelectConfirm_Click(object sender, EventArgs e)
        {
            if (selectedRegion != null)
            {
                selectedRegion.Dispose();
            }

            HOperatorSet.CopyObj(selectedTestRegion, out selectedRegion, 1, -1);

            panelSelect.Enabled = false;
        }

        private void buttonSelectChange_Click(object sender, EventArgs e)
        {
            UserCode.GetInstance().isOverFlag[currentIndex] = 10;
            if (selectedRegion != null)
            {
                selectedRegion.Dispose();
                selectedRegion = null;
            }
            panelSelect.Enabled = true;
            panelOkAndShow.Enabled = false;
        }
        private void outputRegionFeatureShow()
        {
            textBoxOutputResult.Text = "";
            if (selectedRegion != null && selectedRegion.CountObj() != 0)
            {
                HObject imgSelectTest;
                if (UserCode.GetInstance().gProCd[currentIndex].gBP.isColor == true)
                {
                    HOperatorSet.Rgb1ToGray(image, out imgSelectTest);
                }
                else
                {
                    HOperatorSet.CopyObj(image, out imgSelectTest, 1, -1);
                }
                string[] outputShowStr = new string[34]{"面积","重心Y","重心X","区域宽度","区域高度","区域左上角Y坐标","区域左上角X坐标","区域右下角Y坐标","区域右下角X坐标",
                                                    "圆形近似度","紧密度","轮廓长度","凸性","矩形近似度","等效椭圆长轴半径长度","等效椭圆短轴半径长度","等效椭圆方向",
                                                    "椭圆参数：长短轴比值","最小外接圆半径","最大内接圆半径","最大内接矩形宽度","最大内接矩形高度","多边形边数","区域内洞数",
                                                       "所有洞的面积","最大直径","区域方向","最小外接矩形方向","最小外接矩形长度","最小外接矩形宽度",
                                                      "区域灰度最小值","区域灰度最大值","区域灰度平均值","区域灰度标准差"};

                HTuple checkedItemIndex = null;
          
                for (int i = 0; i < 34; i++)
                {
                    if (checkedListBoxOutputSet.GetItemChecked(i))
                    {
                        checkedListBoxOutputSet.SetItemChecked(i, true);
                        if (checkedItemIndex == null)
                        {
                            checkedItemIndex = (HTuple)i;
                        }
                        else
                        {
                            checkedItemIndex = checkedItemIndex.TupleConcat((HTuple)i);
                        }
                    }
                    else
                    {
                        checkedListBoxOutputSet.SetItemChecked(i, false);
                    }
                }
                string[] strtempArray = new string[selectedRegion.CountObj()];
                if (checkedItemIndex != null)
                {
                    for (int j = 0; j < checkedItemIndex.TupleLength(); j++)
                    {
                        HTuple testFeatureValue = null;
                        if (checkedItemIndex[j] < 30)
                        {
                            HOperatorSet.RegionFeatures(selectedRegion, (HTuple)sP.selectSTR[checkedItemIndex[j]], out testFeatureValue);
                        }
                        else
                        {
                            HOperatorSet.GrayFeatures(selectedRegion, imgSelectTest, (HTuple)sP.selectSTR[checkedItemIndex[j]], out testFeatureValue);
                        }


                        for (int i = 0; i < selectedRegion.CountObj(); i++)
                        {
                            string strc;
                            strc = System.String.Format("{0:f}", (float)testFeatureValue[i].D);
                            if (strtempArray[i] == null)
                            {
                                strtempArray[i] = outputShowStr[checkedItemIndex[j]] + ": " + strc + "\r\n";
                            }
                            else
                            {
                                strtempArray[i] = strtempArray[i] + outputShowStr[checkedItemIndex[j]] + ": " + strc + "\r\n";
                            }
                        }
                    }
                    string outputStr = "\r\n";
                    for (int i = 0; i < selectedRegion.CountObj(); i++)
                    {
                        outputStr = outputStr + "\r\n" + "区域" + i.ToString() + "\r\n" + strtempArray[i];
                    }
                    textBoxOutputResult.Text = outputStr;
                }

                if (imgSelectTest != null)
                {
                    imgSelectTest.Dispose();
                }

            }

        }
        private void buttonOutputConfirm_Click(object sender, EventArgs e)
        {
            checkedListBoxOutputSet.Enabled = false;
            panelOkAndShow.Enabled = true;
        }

        private void buttonOutputChange_Click(object sender, EventArgs e)
        {
            UserCode.GetInstance().isOverFlag[currentIndex] = 10;
            checkedListBoxOutputSet.Enabled = true;
            panelOkAndShow.Enabled = false;
        }

        private void checkedListBoxOutputSet_SelectedIndexChanged(object sender, EventArgs e)
        {
            outputRegionFeatureShow();
        }

        private void checkedListBoxOutputSet_SelectedValueChanged(object sender, EventArgs e)
        {
            outputRegionFeatureShow();
        }

        private void buttonRunOnce_Click(object sender, EventArgs e)
        {
            try
            {
                HObject imageCopy;
                //Svision.GetMe().baslerCamera.getFrameImage(out imageCopy);
                UserCode.GetInstance().getImageFromProcess(out imageCopy, currentIndex);
                basicClass.getImageSize(imageCopy, out rowNum, out columnNum);
                //if (rowNum == rowOriNum && columnNum == columnOriNum)
                //{
                    if (image != null)
                    {
                        image.Dispose();
                    }
                    HOperatorSet.CopyImage(imageCopy, out image);
                    HTuple channelOfImage;
                    HOperatorSet.CountChannels(image, out channelOfImage);
                    if (channelOfImage == 1)
                    {
                        HObject image11, image12;
                        HOperatorSet.CopyImage(image, out image11);
                        HOperatorSet.CopyImage(image, out image12);
                        if (imageTemp != null)
                        {
                            imageTemp.Dispose();
                        }
                        HOperatorSet.Compose3(image, image11, image12, out imageTemp);
                        if (imageTempShow != null)
                        {
                            imageTempShow.Dispose();
                        }
                        HOperatorSet.CopyImage(imageTemp, out imageTempShow);
                        image11.Dispose();
                        image12.Dispose();
                    }
                    else
                    {
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
                        HOperatorSet.Decompose3(image, out image1, out image2, out image3);
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
                    BlobSegmentImageShow();
                    if (segmentRegion != null)
                    {
                        segmentRegion.Dispose();
                    }
                    if (checkBoxIsAutoSegment.Checked == true)
                    {
                        HOperatorSet.CopyObj(thresholdTestRegion, out segmentRegion, 1, -1);
                    }
                    else
                    {
                        if (UserCode.GetInstance().gProCd[currentIndex].gBP.isColor == true)
                        {
                            HOperatorSet.CopyObj(thresholdColorRegion, out segmentRegion, 1, -1);
                        }
                        else
                        {
                            HOperatorSet.CopyObj(thresholdGrayRegion, out segmentRegion, 1, -1);
                        }
                    }
                    BlobMorphologyImageShow();
                    if (morphologyRegion != null)
                    {
                        morphologyRegion.Dispose();
                    }

                    HOperatorSet.CopyObj(morphologyTestRegion, out morphologyRegion, 1, -1);
                    BlobSelectRegionImageShow();
                    if (selectedRegion != null)
                    {
                        selectedRegion.Dispose();
                    }

                    HOperatorSet.CopyObj(selectedTestRegion, out selectedRegion, 1, -1);
                    outputRegionFeatureShow();
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

        private void buttonTotalCancel_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void buttonTotalConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                //Segment
                if (checkBoxIsAutoSegment.Checked)
                {
                    UserCode.GetInstance().gProCd[currentIndex].gBP.isAutoSegment = true;
                    if (checkBoxAuto1.Checked)
                    {
                        UserCode.GetInstance().gProCd[currentIndex].gBP.isAutoSegmentMethod1 = true;
                        UserCode.GetInstance().gProCd[currentIndex].gBP.autoSegmentMethod1Para1 = (float)numericUpDownPara11.Value;
                    }
                    else
                    {
                        UserCode.GetInstance().gProCd[currentIndex].gBP.isAutoSegmentMethod1 = false;
                    }
                    if (checkBoxAuto2.Checked)
                    {
                        UserCode.GetInstance().gProCd[currentIndex].gBP.isAutoSegmentMethod2 = true;
                        UserCode.GetInstance().gProCd[currentIndex].gBP.autoSegmentMethod2Para1 = (int)numericUpDownPara21.Value;
                        UserCode.GetInstance().gProCd[currentIndex].gBP.autoSegmentMethod2Para2 = (float)numericUpDownPara22.Value;
                        UserCode.GetInstance().gProCd[currentIndex].gBP.autoSegmentMethod2Para3 = (int)numericUpDownPara23.Value;
                        UserCode.GetInstance().gProCd[currentIndex].gBP.autoSegmentMethod2Para4 = (int)comboBoxPara24.SelectedIndex;
                    }
                    else
                    {
                        UserCode.GetInstance().gProCd[currentIndex].gBP.isAutoSegmentMethod2 = false;
                    }
                    if (checkBoxAuto3.Checked)
                    {
                        UserCode.GetInstance().gProCd[currentIndex].gBP.isAutoSegmentMethod3 = true;
                    }
                    else
                    {
                        UserCode.GetInstance().gProCd[currentIndex].gBP.isAutoSegmentMethod3 = false;
                    }
                }
                else
                {
                    for (int i = 0; i < 6; i++)
                    {
                        if (cSP.colorSelectNum[i])
                        {
                            UserCode.GetInstance().gProCd[currentIndex].gBP.selectedColor[i] = true;
                            if (UserCode.GetInstance().gProCd[currentIndex].gBP.isColor)
                            {
                                UserCode.GetInstance().gProCd[currentIndex].gBP.redValue[i] = cSP.redColor[i];
                                UserCode.GetInstance().gProCd[currentIndex].gBP.greenValue[i] = cSP.greenColor[i];
                                UserCode.GetInstance().gProCd[currentIndex].gBP.blueValue[i] = cSP.blueColor[i];
                            }
                            else
                            {
                                UserCode.GetInstance().gProCd[currentIndex].gBP.grayValue[i] = cSP.grayValue[i];
                            }
                            UserCode.GetInstance().gProCd[currentIndex].gBP.lengthValue[i] = cSP.colorLength[i];
                            UserCode.GetInstance().gProCd[currentIndex].gBP.isBesideThisColor[i] = cSP.isBesideThisColor[i];
                        }
                        else
                        {
                            UserCode.GetInstance().gProCd[currentIndex].gBP.selectedColor[i] = false;
                        }
                    }
                    UserCode.GetInstance().gProCd[currentIndex].gBP.isAutoSegment = false;

                }

                //Morphology
                if (checkBoxFillUpHoles.Checked)
                {
                    UserCode.GetInstance().gProCd[currentIndex].gBP.isFillUpHoles = true;
                }
                else
                {
                    UserCode.GetInstance().gProCd[currentIndex].gBP.isFillUpHoles = false;
                }
                if (radioButtonConnectionsBeforeFillUpHoles.Checked)
                {
                    UserCode.GetInstance().gProCd[currentIndex].gBP.isConnectionBeforeFillUpHoles = true;
                }
                else
                {
                    UserCode.GetInstance().gProCd[currentIndex].gBP.isConnectionBeforeFillUpHoles = false;
                }
                if (checkBoxErosion.Checked)
                {
                    UserCode.GetInstance().gProCd[currentIndex].gBP.isErosion = true;
                    if (radioButtonRectangleErosion.Checked)
                    {
                        UserCode.GetInstance().gProCd[currentIndex].gBP.erosionElementNUM = 0;
                        UserCode.GetInstance().gProCd[currentIndex].gBP.erosionRWidth = (int)numericUpDownWidthErosion.Value;
                        UserCode.GetInstance().gProCd[currentIndex].gBP.erosionRHeight = (int)numericUpDownHeightErosion.Value;
                    }
                    else
                    {
                        UserCode.GetInstance().gProCd[currentIndex].gBP.erosionElementNUM = 1;
                        UserCode.GetInstance().gProCd[currentIndex].gBP.erosionCRadius = (double)numericUpDownRadiusErosion.Value;
                    }
                }
                else
                {
                    UserCode.GetInstance().gProCd[currentIndex].gBP.isErosion = false;
                }
                if (checkBoxDilation.Checked)
                {
                    UserCode.GetInstance().gProCd[currentIndex].gBP.isDilation = true;
                    if (radioButtonRectangleDilation.Checked)
                    {
                        UserCode.GetInstance().gProCd[currentIndex].gBP.dilationElementNUM = 0;
                        UserCode.GetInstance().gProCd[currentIndex].gBP.dilationRWidth = (int)numericUpDownWidthDilation.Value;
                        UserCode.GetInstance().gProCd[currentIndex].gBP.dilationRHeight = (int)numericUpDownHeightDilation.Value;
                    }
                    else
                    {
                        UserCode.GetInstance().gProCd[currentIndex].gBP.dilationElementNUM = 1;
                        UserCode.GetInstance().gProCd[currentIndex].gBP.dilationCRadius = (double)numericUpDownRadiusDilation.Value;
                    }
                }
                else
                {
                    UserCode.GetInstance().gProCd[currentIndex].gBP.isDilation = false;
                }
                if (checkBoxOpening.Checked)
                {
                    UserCode.GetInstance().gProCd[currentIndex].gBP.isOpening = true;
                    if (radioButtonRectangleOpening.Checked)
                    {
                        UserCode.GetInstance().gProCd[currentIndex].gBP.openingElementNUM = 0;
                        UserCode.GetInstance().gProCd[currentIndex].gBP.openingRWidth = (int)numericUpDownWidthOpening.Value;
                        UserCode.GetInstance().gProCd[currentIndex].gBP.openingRHeight = (int)numericUpDownHeightOpening.Value;
                    }
                    else
                    {
                        UserCode.GetInstance().gProCd[currentIndex].gBP.openingElementNUM = 1;
                        UserCode.GetInstance().gProCd[currentIndex].gBP.openingCRadius = (double)numericUpDownRadiusOpening.Value;
                    }
                }
                else
                {
                    UserCode.GetInstance().gProCd[currentIndex].gBP.isOpening = false;
                }
                if (checkBoxClosing.Checked)
                {
                    UserCode.GetInstance().gProCd[currentIndex].gBP.isClosing = true;
                    if (radioButtonRectangleClosing.Checked)
                    {
                        UserCode.GetInstance().gProCd[currentIndex].gBP.closingElementNUM = 0;
                        UserCode.GetInstance().gProCd[currentIndex].gBP.closingRWidth = (int)numericUpDownWidthClosing.Value;
                        UserCode.GetInstance().gProCd[currentIndex].gBP.closingRHeight = (int)numericUpDownHeightClosing.Value;
                    }
                    else
                    {
                        UserCode.GetInstance().gProCd[currentIndex].gBP.closingElementNUM = 1;
                        UserCode.GetInstance().gProCd[currentIndex].gBP.closingCRadius = (double)numericUpDownRadiusClosing.Value;
                    }
                }
                else
                {
                    UserCode.GetInstance().gProCd[currentIndex].gBP.isClosing = false;
                }


                //Select
                if (radioButtonSelectAnd.Checked)
                {
                    UserCode.GetInstance().gProCd[currentIndex].gBP.selectisAnd = true;
                }
                else
                {
                    UserCode.GetInstance().gProCd[currentIndex].gBP.selectisAnd = false;
                }
                for (int i = 0; i < 34; i++)
                {
                    UserCode.GetInstance().gProCd[currentIndex].gBP.selectIsChecked[i] = sP.selectIsChecked[i];
                    UserCode.GetInstance().gProCd[currentIndex].gBP.selectMin[i] = sP.selectMin[i];
                    UserCode.GetInstance().gProCd[currentIndex].gBP.selectMax[i] = sP.selectMax[i];

                }
                UserCode.GetInstance().gProCd[currentIndex].gBP.selectArrangeItemIndex = comboBoxArrange.SelectedIndex;
                if (radioButtonLtoS.Checked)
                {
                    UserCode.GetInstance().gProCd[currentIndex].gBP.isArrangeLtoS = true;
                }
                else
                {
                    UserCode.GetInstance().gProCd[currentIndex].gBP.isArrangeLtoS = false;
                }
                bool isOutputIDChanged = false;
                bool isRegionNumChanged = false;
                bool isSelectItemCountChanged = false;
                int selectItemTemp = UserCode.GetInstance().gProCd[currentIndex].gBP.selectItemCount;
                if (UserCode.GetInstance().gProCd[currentIndex].gBP.regionNum != (int)numericUpDownRegionMaxNumber.Value)
                {
                    isRegionNumChanged = true;
                }
                UserCode.GetInstance().gProCd[currentIndex].gBP.regionNum = (int)numericUpDownRegionMaxNumber.Value;
                //Output

                UserCode.GetInstance().gProCd[currentIndex].gBP.selectItemCount = 0;
                
                for (int i = 0; i < 34; i++)
                {
                    if (checkedListBoxOutputSet.GetItemChecked(i) != UserCode.GetInstance().gProCd[currentIndex].gBP.outputIDIsChecked[i])
                        {
                            isOutputIDChanged = true;
                        }
                    if (checkedListBoxOutputSet.GetItemChecked(i))
                    {
                        
                        UserCode.GetInstance().gProCd[currentIndex].gBP.outputIDIsChecked[i] = true;
                        UserCode.GetInstance().gProCd[currentIndex].gBP.selectItemCount++;
                    }
                    else
                    {
                        UserCode.GetInstance().gProCd[currentIndex].gBP.outputIDIsChecked[i] = false;
                    }
                }
                if (selectItemTemp!=UserCode.GetInstance().gProCd[currentIndex].gBP.selectItemCount)
                {
                    isSelectItemCountChanged = true;
                }
                UserCode.GetInstance().gProCd[currentIndex].gBP.blobAnalysisOperationFlag = new bool[UserCode.GetInstance().gProCd[currentIndex].gBP.selectItemCount];
                UserCode.GetInstance().gProCd[currentIndex].gBP.blobAnalysisOperationStr = new string[UserCode.GetInstance().gProCd[currentIndex].gBP.selectItemCount];
                for (int k = 0; k < UserCode.GetInstance().gProCd[currentIndex].gBP.blobAnalysisOperationFlag.Length; k++)
                {
                    UserCode.GetInstance().gProCd[currentIndex].gBP.blobAnalysisOperationFlag[k] = true;
                }
                int iii = 0;
                for (int j = 0; j < 34; j++)
                {
                    if (UserCode.GetInstance().gProCd[currentIndex].gBP.outputIDIsChecked[j])
                    {
                        UserCode.GetInstance().gProCd[currentIndex].gBP.blobAnalysisOperationStr[iii] = UserCode.GetInstance().gProCd[currentIndex].gBP.outputShowStr[j];
                        iii = iii + 1;

                    }
                }
                UserCode.GetInstance().gProCd[currentIndex].gBP.outPutStringList.Clear();
                UserCode.GetInstance().gProCd[currentIndex].gBP.outPutStringList.Add("识别目标个数");
                for (int i = 0; i < UserCode.GetInstance().gProCd[currentIndex].gBP.regionNum; i++)
                {
                    for (int j = 0; j < 34; j++)
                    {
                        if (UserCode.GetInstance().gProCd[currentIndex].gBP.outputIDIsChecked[j])
                        {
                            UserCode.GetInstance().gProCd[currentIndex].gBP.outPutStringList.Add("第" + i.ToString() + "个目标" + UserCode.GetInstance().gProCd[currentIndex].gBP.outputShowStr[j]);
                        }
                    }
                }
                UserCode.GetInstance().isOverFlag[currentIndex] = 20;
                if (isSelectItemCountChanged == true || isOutputIDChanged == true || isRegionNumChanged == true)
                {
                    for (int i = 0; i < 20;i++ )
                    {
                        if (UserCode.GetInstance().isOverFlag[i]==32)
                        {
                            if ((ProCodeCls.MainFunction)UserCode.GetInstance().gProCd[i].gSOP.sendDataInfoList[0].funcID == ProCodeCls.MainFunction.MeasureBlobAnalysisFBD
                                &&UserCode.GetInstance().gProCd[i].gSOP.sendDataInfoList[0].row==currentIndex)
                            {
                                MessageBox.Show("最多区域个数或输出区域特征选项已被修改，请打开作业编辑中相应串行输出模块更新程序并确认，否则可能引起错误！");
                                if (isSelectItemCountChanged == false && isOutputIDChanged == true && isRegionNumChanged == false)
                                {
                                    UserCode.GetInstance().isOverFlag[i] = 31;
                                }
                            }                           
                        }
                    }
                }
                if ( UserCode.GetInstance().gProCd[currentIndex].gBP.selectItemCount==0)
                {
                    MessageBox.Show("未勾选任何blob分析功能块输出特征，本功能块设置不完全，请完成本功能块的输出设置！");
                    UserCode.GetInstance().isOverFlag[currentIndex] = 10;        
                }
                else
                {
                     this.Hide();
                }
                
            }

            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }

        private void radioButtonConnectionsBeforeFillUpHoles_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                BlobMorphologyImageShow();
            }

            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }

        private void radioButtonOnlyFillUpHoles_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                BlobMorphologyImageShow();
            }

            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }

        

    }
}