using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HalconDotNet;

namespace Svision
{
    public partial class SurfaceBasedMatchFBDForm : Form
    {
        HObject _3D_ImageCopy;
        HObject _3D_Xm;
        HObject _3D_Ym;
        HObject _3D_Zm;
        HObject _3D_ModelZ, _3D_ThrBin;
        HObject _3D_ConnectedModel;
        HObject _3D_ModelROI;
        HObject image, Img, imgPart, imgPartThr, imgPartTemp, imgPartTempThr, imageTemp, imageTempShow, rectDomain;


        HTuple hv_ObjectModel3DModel = null, hv_SFM = null, hv_Instructions = null, hv_Message = null;

        HTuple SBMWindowHandle, SBMPartWindowHandle;
        double resizerate = 1;
        Point oriLocation;
        PointF centerLocation;
        int rowNum, columnNum;
        private int currentIndex;

        int rowOriNum, columnOriNum;
        double resRat;
        bool CSMParaDefault;
        public SurfaceBasedMatchFBDForm(int idx)
        {
            InitializeComponent();
            rowOriNum = Svision.GetMe().oriRowNumber;
            columnOriNum = Svision.GetMe().oriColumnNumber;
            //rowOriNum = 1234;
            //columnOriNum = 1624;
            double widRat = 256 / ((double)columnOriNum);
            double heiRat = 192 / ((double)rowOriNum);
            resRat = widRat < heiRat ? widRat : heiRat;
            CSMParaDefault = true;
            currentIndex = idx;
        }



        private void loadImg_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Title = "加载图像";
            dlg.Filter = "jpg files *.jpg|(*.jpg)" + "|All files (*.*)|*.*";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    //ho_Image.Dispose();
                    if (_3D_ImageCopy != null)
                    {
                        _3D_ImageCopy.Dispose();
                    }
                    //HOperatorSet.ReadImage(out ho_Image, hv_ImagePath + "engine_cover_xyz_01");
                    HOperatorSet.ReadImage(out _3D_ImageCopy, dlg.FileName);
                    basicClass.getImageSize(_3D_ImageCopy, out rowNum, out columnNum);
                    if (rowNum != rowOriNum || columnNum != columnOriNum)
                    {
                        HOperatorSet.ClearWindow(SBMWindowHandle);
                        HOperatorSet.ClearWindow(SBMPartWindowHandle);
                    }
                    if (image != null)
                    {
                        image.Dispose();
                    }
                    HOperatorSet.CopyImage(_3D_ImageCopy, out image);
                    HTuple ChannelOfImage;
                    HOperatorSet.CountChannels(image, out ChannelOfImage);
                    if (ChannelOfImage == 1)
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
                        _3D_Xm.Dispose(); _3D_Ym.Dispose(); _3D_Zm.Dispose();
                        HOperatorSet.Decompose3(image, out _3D_Xm, out _3D_Ym, out _3D_Zm);
                        basicClass.displayhobject(_3D_Zm, SBMWindowHandle);
                        if (imageTemp != null)
                        {
                            imageTemp.Dispose();
                        }
                        HOperatorSet.CopyImage(_3D_Zm, out imageTemp);
                        if (imageTempShow != null)
                        {
                            imageTempShow.Dispose();
                        }
                        HOperatorSet.CopyImage(imageTemp, out imageTempShow);
                    }
                    //if (Img != null)
                    //{
                    //    Img.Dispose();
                    //}
                    //basicClass.resizeImage(imageTemp, out Img, resRat);
                    //basicClass.displayhobject(image, SBMWindowHandle);


                    if (imgPart != null)
                    {
                        imgPart.Dispose();
                    }
                    basicClass.resizeImage(imageTemp, out imgPart, resizerate);
                    HObject temp;

                    basicClass.genRectangle1(out rectDomain, 0, 0, (SMPartptx.Height - 1), (SMPartptx.Width - 1));
                    basicClass.reduceDomain(imgPart, rectDomain, out temp);
                    centerLocation.Y = (float)(((float)(System.Math.Min(SMPartptx.Height, rowNum * resizerate) - 1 + 0) / 2.0) * resRat / resizerate);
                    centerLocation.X = (float)(((float)(System.Math.Min(SMPartptx.Width, columnNum * resizerate) - 1 + 0) / 2.0) * resRat / resizerate);
                    if (imgPartTemp != null)
                    {
                        imgPartTemp.Dispose();
                    }
                    HOperatorSet.CropDomain(temp, out imgPartTemp);
                    HOperatorSet.DispObj(imgPartTemp, SBMPartWindowHandle);
                    temp.Dispose();
                    oriLocation.X = (int)centerLocation.X;
                    oriLocation.Y = (int)centerLocation.Y;
                    _3D_ImageCopy.Dispose();
                }

                catch (ArgumentException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            dlg.Dispose();
        }

        private void buttonThreshold_Click(object sender, EventArgs e)
        {
            _3D_ModelZ.Dispose();

            HOperatorSet.Threshold(_3D_Zm, out _3D_ModelZ, (HTuple)this.numericUpDownMinGrey.Value, (HTuple)this.numericUpDownMaxGrey.Value);
            HOperatorSet.Connection(_3D_ModelZ, out _3D_ConnectedModel);
            HOperatorSet.RegionToBin(_3D_ConnectedModel, out _3D_ThrBin, 255, 0, rowNum, columnNum);
            if (HDevWindowStack.IsOpen())
            {
                HOperatorSet.SetDraw(HDevWindowStack.GetActive(), "fill");
            }
            if (HDevWindowStack.IsOpen())
            {
                HOperatorSet.SetColor(HDevWindowStack.GetActive(), "green");
            }
            basicClass.displayClear(SBMWindowHandle);
            basicClass.displayhobject(_3D_ConnectedModel, SBMWindowHandle);

            partImageWindowShow(oriLocation);
        }

        private void partImageWindowShow(Point oriLocation)
        {


            if (centerLocation.X >= 0 && centerLocation.X <= columnNum * resRat && centerLocation.Y >= 0 && centerLocation.Y <= rowNum * resRat)
            {
                try
                {
                    centerLocation.Y = (float)oriLocation.Y;
                    centerLocation.X = (float)oriLocation.X;
                    if (oriLocation.Y * resizerate / resRat - ((float)(SMPartptx.Height - 1 + 0) / 2.0) < 0 && oriLocation.X * resizerate / resRat - ((float)(SMPartptx.Width - 1 + 0) / 2.0) < 0)
                    {
                        HObject temp;
                        basicClass.genRectangle1(out rectDomain, 0, 0, (SMPartptx.Height - 1), (SMPartptx.Width - 1));
                        centerLocation.Y = (float)(((float)(System.Math.Min(SMPartptx.Height, rowNum * resizerate) - 1 + 0) / 2.0) * resRat / resizerate);
                        centerLocation.X = (float)(((float)(System.Math.Min(SMPartptx.Width, columnNum * resizerate) - 1 + 0) / 2.0) * resRat / resizerate);

                        basicClass.reduceDomain(imgPart, rectDomain, out temp);
                        if (imgPartTemp != null)
                        {
                            imgPartTemp.Dispose();
                        }
                        HOperatorSet.CropDomain(temp, out imgPartTemp);
                        HOperatorSet.DispObj(imgPartTemp, SBMPartWindowHandle);
                        temp.Dispose();
                    }
                    else if (oriLocation.Y * resizerate / resRat - ((float)(SMPartptx.Height - 1 + 0) / 2.0) <= 0 && oriLocation.X * resizerate / resRat - ((float)(SMPartptx.Width - 1 + 0) / 2.0) >= 0 && oriLocation.X * resizerate / resRat + ((float)(SMPartptx.Width - 1 + 0) / 2.0) < columnNum * resizerate - 1)
                    {
                        HObject temp;
                        basicClass.genRectangle1(out rectDomain, 0, oriLocation.X * resizerate / resRat - ((float)(SMPartptx.Width - 1 + 0) / 2.0), (SMPartptx.Height - 1), oriLocation.X * resizerate / resRat + ((float)(SMPartptx.Width - 1 + 0) / 2.0));
                        centerLocation.Y = (float)(((float)(System.Math.Min(SMPartptx.Height, rowNum * resizerate) - 1 + 0) / 2.0) * resRat / resizerate);
                        centerLocation.X = (float)oriLocation.X;
                        basicClass.reduceDomain(imgPart, rectDomain, out temp);
                        if (imgPartTemp != null)
                        {
                            imgPartTemp.Dispose();
                        }
                        HOperatorSet.CropDomain(temp, out imgPartTemp);
                        HOperatorSet.DispObj(imgPartTemp, SBMPartWindowHandle);
                        temp.Dispose();
                    }
                    else if (oriLocation.Y * resizerate / resRat - ((float)(SMPartptx.Height - 1 + 0) / 2.0) > 0 && oriLocation.Y * resizerate / resRat + ((float)(SMPartptx.Height - 1 + 0) / 2.0) <= rowNum * resizerate - 1 && oriLocation.X * resizerate / resRat - ((float)(SMPartptx.Width - 1 + 0) / 2.0) < 0)
                    {
                        HObject temp;
                        basicClass.genRectangle1(out rectDomain, oriLocation.Y * resizerate / resRat - ((float)(SMPartptx.Height - 1 + 0) / 2.0), 0, oriLocation.Y * resizerate / resRat + ((float)(SMPartptx.Height - 1 + 0) / 2.0), (SMPartptx.Width - 1));
                        centerLocation.Y = (float)oriLocation.Y;
                        centerLocation.X = (float)(((float)(System.Math.Min(SMPartptx.Width, columnNum * resizerate) - 1 + 0) / 2.0) * resRat / resizerate);
                        basicClass.reduceDomain(imgPart, rectDomain, out temp);
                        if (imgPartTemp != null)
                        {
                            imgPartTemp.Dispose();
                        }
                        HOperatorSet.CropDomain(temp, out imgPartTemp);
                        HOperatorSet.DispObj(imgPartTemp, SBMPartWindowHandle);
                        temp.Dispose();
                    }
                    else if (oriLocation.Y * resizerate / resRat + ((float)(SMPartptx.Height - 1 + 0) / 2.0) > rowNum * resizerate - 1 && oriLocation.X * resizerate / resRat - ((float)(SMPartptx.Width - 1 + 0) / 2.0) < 0)
                    {
                        HObject temp;
                        basicClass.genRectangle1(out rectDomain, rowNum * resizerate - 1 - (SMPartptx.Height - 1), 0, rowNum * resizerate - 1, (SMPartptx.Width - 1));
                        centerLocation.Y = (float)((rowNum * resizerate - ((float)(System.Math.Min(SMPartptx.Height, rowNum * resizerate) - 1 + 0) / 2.0) - 1) * resRat / resizerate);
                        centerLocation.X = (float)(((float)(System.Math.Min(SMPartptx.Width, columnNum * resizerate) - 1 + 0) / 2.0) * resRat / resizerate);
                        basicClass.reduceDomain(imgPart, rectDomain, out temp);
                        if (imgPartTemp != null)
                        {
                            imgPartTemp.Dispose();
                        }
                        HOperatorSet.CropDomain(temp, out imgPartTemp);
                        HOperatorSet.DispObj(imgPartTemp, SBMPartWindowHandle);
                        temp.Dispose();
                    }
                    else if (oriLocation.Y * resizerate / resRat + ((float)(SMPartptx.Height - 1 + 0) / 2.0) > rowNum * resizerate - 1 && oriLocation.X * resizerate / resRat - ((float)(SMPartptx.Width - 1 + 0) / 2.0) > 0 && oriLocation.X * resizerate / resRat + ((float)(SMPartptx.Width - 1 + 0) / 2.0) < columnNum * resizerate - 1)
                    {
                        HObject temp;
                        basicClass.genRectangle1(out rectDomain, rowNum * resizerate - (SMPartptx.Height), oriLocation.X * resizerate / resRat - ((float)(SMPartptx.Width - 1 + 0) / 2.0), rowNum * resizerate - 1, oriLocation.X * resizerate / resRat + ((float)(SMPartptx.Width - 1 + 0) / 2.0));
                        centerLocation.Y = (float)((rowNum * resizerate - ((float)(System.Math.Min(SMPartptx.Height, rowNum * resizerate) - 1 + 0) / 2.0 + 1)) * resRat / resizerate);
                        centerLocation.X = (float)oriLocation.X;
                        basicClass.reduceDomain(imgPart, rectDomain, out temp);
                        if (imgPartTemp != null)
                        {
                            imgPartTemp.Dispose();
                        }
                        HOperatorSet.CropDomain(temp, out imgPartTemp);
                        HOperatorSet.DispObj(imgPartTemp, SBMPartWindowHandle);
                        temp.Dispose();
                    }
                    else if (oriLocation.Y * resizerate / resRat + ((float)(SMPartptx.Height - 1 + 0) / 2.0) >= rowNum * resizerate - 1 && oriLocation.X * resizerate / resRat + ((float)(SMPartptx.Width - 1 + 0) / 2.0) >= columnNum * resizerate - 1)
                    {
                        HObject temp;
                        basicClass.genRectangle1(out rectDomain, rowNum * resizerate - (SMPartptx.Height), columnNum * resizerate - (SMPartptx.Width), rowNum * resizerate - 1, columnNum * resizerate - 1);
                        centerLocation.Y = (float)((rowNum * resizerate - ((float)(System.Math.Min(SMPartptx.Height, rowNum * resizerate) - 1 + 0) / 2.0 + 1)) * resRat / resizerate);
                        centerLocation.X = (float)((columnNum * resizerate - ((float)(System.Math.Min(SMPartptx.Width, columnNum * resizerate) - 1 + 0) / 2.0 + 1)) * resRat / resizerate);
                        basicClass.reduceDomain(imgPart, rectDomain, out temp);
                        if (imgPartTemp != null)
                        {
                            imgPartTemp.Dispose();
                        }
                        HOperatorSet.CropDomain(temp, out imgPartTemp);
                        HOperatorSet.DispObj(imgPartTemp, SBMPartWindowHandle);
                        temp.Dispose();
                    }
                    else if (oriLocation.Y * resizerate / resRat + ((float)(SMPartptx.Height - 1 + 0) / 2.0) < rowNum * resizerate - 1 && oriLocation.Y * resizerate / resRat - ((float)(SMPartptx.Height - 1 + 0) / 2.0) > 0 && oriLocation.X * resizerate / resRat + ((float)(SMPartptx.Width - 1 + 0) / 2.0) > columnNum * resizerate - 1)
                    {
                        HObject temp;
                        basicClass.genRectangle1(out rectDomain, oriLocation.Y * resizerate / resRat - ((float)(SMPartptx.Height - 1 + 0) / 2.0), columnNum * resizerate - (SMPartptx.Width), oriLocation.Y * resizerate / resRat + ((float)(SMPartptx.Height - 1 + 0) / 2.0), columnNum * resizerate - 1);
                        centerLocation.Y = (float)oriLocation.Y;
                        centerLocation.X = (float)((columnNum * resizerate - ((float)(System.Math.Min(SMPartptx.Width, columnNum * resizerate) - 1 + 0) / 2.0 + 1)) * resRat / resizerate);
                        basicClass.reduceDomain(imgPart, rectDomain, out temp);
                        if (imgPartTemp != null)
                        {
                            imgPartTemp.Dispose();
                        }
                        HOperatorSet.CropDomain(temp, out imgPartTemp);
                        HOperatorSet.DispObj(imgPartTemp, SBMPartWindowHandle);
                        temp.Dispose();
                    }
                    else if (oriLocation.Y * resizerate / resRat - ((float)(SMPartptx.Height - 1 + 0) / 2.0) <= 0 && oriLocation.X * resizerate / resRat + ((float)(SMPartptx.Width - 1 + 0) / 2.0) >= columnNum * resizerate - 1)
                    {
                        HObject temp;
                        basicClass.genRectangle1(out rectDomain, 0, columnNum * resizerate - (SMPartptx.Width), (SMPartptx.Height - 1), columnNum * resizerate - 1);
                        centerLocation.Y = (float)(((float)(System.Math.Min(SMPartptx.Height, rowNum * resizerate) - 1 + 0) / 2.0) * resRat / resizerate);
                        centerLocation.X = (float)((columnNum * resizerate - ((float)(System.Math.Min(SMPartptx.Width, columnNum * resizerate) - 1 + 0) / 2.0 + 1)) * resRat / resizerate);
                        basicClass.reduceDomain(imgPart, rectDomain, out temp);
                        if (imgPartTemp != null)
                        {
                            imgPartTemp.Dispose();
                        }
                        HOperatorSet.CropDomain(temp, out imgPartTemp);
                        HOperatorSet.DispObj(imgPartTemp, SBMPartWindowHandle);
                        temp.Dispose();
                    }
                    else
                    {
                        HObject temp;
                        basicClass.genRectangle1(out rectDomain, oriLocation.Y * resizerate / resRat - ((float)(SMPartptx.Height - 1 + 0) / 2.0), oriLocation.X * resizerate / resRat - ((float)(SMPartptx.Width - 1 + 0) / 2.0), oriLocation.Y * resizerate / resRat + ((float)(SMPartptx.Height - 1 + 0) / 2.0), oriLocation.X * resizerate / resRat + ((float)(SMPartptx.Width - 1 + 0) / 2.0));
                        basicClass.reduceDomain(imgPart, rectDomain, out temp);
                        if (imgPartTemp != null)
                        {
                            imgPartTemp.Dispose();
                        }
                        HOperatorSet.CropDomain(temp, out imgPartTemp);
                        HOperatorSet.DispObj(imgPartTemp, SBMPartWindowHandle);
                        temp.Dispose();
                    }
                    this.SurfaceMatchingPicture.Focus();
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }


        }

        private void selectSearchObjtest_Click(object sender, EventArgs e)
        {
            //Find connected regions
            _3D_ConnectedModel.Dispose();
            HOperatorSet.Connection(_3D_ModelZ, out _3D_ConnectedModel);
            //Select the regions for the ROI of the reference model
            _3D_ModelROI.Dispose();
            HOperatorSet.SelectObj(_3D_ConnectedModel, out _3D_ModelROI, (new HTuple(10)).TupleConcat(
                9));
            {
                HObject ExpTmpOutVar_0;
                HOperatorSet.Union1(_3D_ModelROI, out ExpTmpOutVar_0);
                _3D_ModelROI.Dispose();
                _3D_ModelROI = ExpTmpOutVar_0;
            }
            //Create the ROI
            {
                HObject ExpTmpOutVar_0;
                HOperatorSet.ReduceDomain(_3D_Xm, _3D_ModelROI, out ExpTmpOutVar_0);
                _3D_Xm.Dispose();
                _3D_Xm = ExpTmpOutVar_0;
            }
            if (HDevWindowStack.IsOpen())
            {
                HOperatorSet.SetDraw(HDevWindowStack.GetActive(), "fill");
            }
            if (HDevWindowStack.IsOpen())
            {
                HOperatorSet.SetColor(HDevWindowStack.GetActive(), "red");
            }
            basicClass.displayClear(SBMWindowHandle);
            basicClass.displayhobject(_3D_ModelROI, SBMWindowHandle);
        }

        private void SurfaceBasedMatchFBDForm_Load(object sender, EventArgs e)
        {
            try
            {
                HOperatorSet.OpenWindow(0, 0, 256, 192, SurfaceMatchingPicture.Handle, "visible", "", out SBMWindowHandle);
                HDevWindowStack.Push(SBMWindowHandle);

                HOperatorSet.OpenWindow(0, 0, 375, 328, SMPartptx.Handle, "visible", "", out SBMPartWindowHandle);
                HDevWindowStack.Push(SBMPartWindowHandle);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void surfaceModel_Click(object sender, EventArgs e)
        {
            HOperatorSet.XyzToObjectModel3d(_3D_Xm, _3D_Ym, _3D_Zm, out hv_ObjectModel3DModel);
            HOperatorSet.CreateSurfaceModel(hv_ObjectModel3DModel, 0.03, new HTuple(),
                new HTuple(), out hv_SFM);
        }
    }
}
