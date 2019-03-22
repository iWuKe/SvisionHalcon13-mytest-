using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HalconDotNet;
namespace Svision
{
    static class basicClass
    {
        public static void readImageHobject(out HObject hoImage,string fullFileName)
        {
            //Read image from file as HObject;
            //fullFileName: The full name of the image including the file path.
            //hoImage: The image read as HObjcet.
            HOperatorSet.ReadImage(out hoImage, (HTuple)fullFileName);
        }
        public static void writeImageHobject(HObject hoImage, string format, string fullFileName)
        {
            //Saves the input image Image in the file fullFileName in the format Format.
            //hoImage: The image of HObject.
            //format:  "ima", "tif","tiff", "bmp", "jpg", "jpeg","jp2","jxr","png"
            //fullFileName: The full name of the image including the file path.
            HOperatorSet.WriteImage(hoImage, format, 0, fullFileName);
        }
        public static void resizeImage(HObject hoImageSRC, out HObject hoImageDST, double rate)
        {
            //Scales the image. The hoImageDST is rate times  the size of hoImageSRC.
            //hoImageSRC: Input HObject image.
            //hoImageDST: Output HObject image.
            //rate: Times rate. 
            //      If the size of hoImageSRC is (x,y), 
            //      then the size of hoImageDST is (floor(x*rate),floor(y*rate)).
            HTuple hvWidth = null;
            HTuple hvHeight = null;
            HOperatorSet.GetImageSize(hoImageSRC, out hvWidth, out hvHeight);
            HOperatorSet.ZoomImageSize(hoImageSRC, out hoImageDST,System.Math.Max( System.Math.Floor(hvWidth[0].I * rate),1), System.Math.Max( System.Math.Floor(hvHeight[0].I * rate),1), "constant");
        }
        public static void resizeImage(HObject hoImageSRC, out HObject hoImageDST, int rowNumber,int columnNumber)
        {
            // Scales the image Image to the size given by rowNumber and columnNumber.
            //hoImageSRC: Input HObject image.
            //hoImageDST: Output HObject image.
          
            HOperatorSet.ZoomImageSize(hoImageSRC, out hoImageDST, (HTuple)columnNumber, (HTuple)rowNumber, "weighted");
        }
        public static void getImageSize(HObject hoImage,out int rowNumber,out int columnNumber)
        {
            // Return the size of an image.The output image has the same size as the input image. 
            HTuple hvWidth = null;
            HTuple hvHeight = null;
            HOperatorSet.GetImageSize(hoImage, out hvWidth, out hvHeight);
            columnNumber = hvWidth[0].I;
            rowNumber = hvHeight[0].I;
        }
        public static void rotateImage(HObject hoImageSRC, out HObject hoImageDST, int phi)
        {
            //Rotate an image about its center.
            //phi:   Typical range of values: 0 <= Phi <= 360 degrees
            //       Minimum increment: 0.001
            HOperatorSet.RotateImage(hoImageSRC, out hoImageDST, (HTuple)(phi % 360),"constant");
        }
        public static void resizeRegion(HObject hoRegionSRC, out HObject hoRegionDST, float rate)
        {
            //Scales the region. The hoRegionDST is rate times  the size of hoRegionSRC.
            HOperatorSet.ZoomRegion(hoRegionSRC, out hoRegionDST, (HTuple)rate, (HTuple)rate);
        }
        public static void displayhobject(HObject hobj,HTuple hWindowHandle)
        {
            //Display a hobject, including image/xld/region and so on.
            if (HDevWindowStack.IsOpen())
            {
                HOperatorSet.DispObj(hobj, hWindowHandle);
            }
        }
        public static void displayClear(HTuple hWindowHandle)
        {
            //Display a hobject, including image/xld/region and so on.
            if (HDevWindowStack.IsOpen())
            {
                HOperatorSet.ClearWindow(hWindowHandle);
            }
        }
        public static void thresholdImage(HObject hoImage, out HObject hoRegion, float minGrayValue, float maxGrayValue)
        {
            //Segment an image using global threshold.
            //threshold selects the pixels from the input image 
            //whose gray values g fulfill the following condition: 
            //           minGrayValue <= g <=  maxGrayValue .
            //All points of an image fulfilling the condition are returned as one region.
            HOperatorSet.Threshold(hoImage, out hoRegion, (HTuple)(minGrayValue), (HTuple)(maxGrayValue));
        }
        public static void reduceDomain(HObject hoImageSRC, HObject hoRegion, out HObject hoImageDST)
        {
            //reduces the definition domain of the given image to the indicated region hoRegion. 
            HOperatorSet.ReduceDomain(hoImageSRC, hoRegion, out hoImageDST);
        }
        public static void meanImageFliter(HObject hoImageSRC, out HObject hoImageDST, int maskWidth, int maskHeight)
        {
            //Smooth by averaging.
            //The filter matrix consists of ones (evaluated equally) and has the size MaskHeight x MaskWidth.
            HOperatorSet.MeanImage(hoImageSRC,out hoImageDST,(HTuple)maskWidth,(HTuple)maskHeight);
        }
        public static void colorImageToGray(HObject hoImageSRC, out HObject hoImageDST)
        {
            //Transform the color image to gray image.
            HOperatorSet.Rgb3ToGray(hoImageSRC, hoImageSRC, hoImageSRC, out hoImageDST);
        }
        public static void drawCircleMouse(HTuple hWindowHandle, out double row, out double column, out double radius)
        {
            //Interactive drawing of a circle by mouse on the screen.
            //To create a circle you have to press the mouse button at the location which is used as the center of that circle. 
            //While keeping the mouse button pressed, the Radius's length can be modified through moving the mouse. 
            //After another mouse click in the created circle center you can move it. 
            //A clicking close to the circular arc you can modify the Radius of the circle. 
            //Pressing the right mouse button terminates the procedure. 

            //After terminating the procedure the circle is not visible in the window any longer.
            //row: The row number of the center of the circle.
            //column:The column number of the center of the circle.
            //radius:The radius of the circle

            HTuple hRow = null;
            HTuple hColumn = null;
            HTuple hRadius = null;
            HOperatorSet.DrawCircle(hWindowHandle, out hRow, out hColumn, out hRadius);
            column = hColumn[0].D;
            row = hRow[0].D;
            radius = hRadius[0].D;
        }
        public static void genCircle(out HObject hRegionCircle, double row, double column, double radius)
        {
            //Create a circle.
            //The operator generates one or more circles described by the center and Radius.

            HOperatorSet.GenCircle(out hRegionCircle, (HTuple)row, (HTuple)column, (HTuple)radius);
        }
        public static void displayCircleScreen(HTuple hWindowHandle, double row, double column, double radius)
        {
            // Displays circles in a window.
            // A circle is described by the center (Row, Column) and the radius Radius.

            HOperatorSet.DispCircle(hWindowHandle, (HTuple)row, (HTuple)column, (HTuple)radius);
        }
        public static void displayCircleXldScreen(HTuple hWindowHandle, double row, double column, double radius)
        {
            // Displays circles XLD in a window.
            // A circle XLD is described by the center (Row, Column) and the radius Radius.

            HObject circleXld;
            HOperatorSet.GenCircleContourXld(out circleXld, (HTuple)row, (HTuple)column, (HTuple)radius, 0, 6.28318, "positive", 1.0);
            HOperatorSet.DispObj(circleXld, hWindowHandle);
            circleXld.Dispose();
        }
        public static void drawRectangle1Mouse(HTuple hWindowHandle,out double row1,out double column1,out double row2,out double column2)
        {
            //Draw a rectangle parallel to the coordinate axis by mouse.
            //The operator returns the parameter for a rectangle parallel to the coordinate axes, 
            //which has been created interactively by the user in the window. 
            //To create a rectangle you have to press the left mouse button determining   a corner of the rectangle. 
            //While keeping the button pressed you may “drag” the rectangle in any direction. 
            //After another mouse click in the middle of the created rectangle you can move it. 
            //A click close to one side “grips” it to modify the rectangle's dimension in perpendicular direction to this side. 
            //If you click on one corner of the created rectangle, you may move this corner. 
            //Pressing the right mouse button terminates the procedure. 

            HTuple hRow1,hRow2,hColumn1,hColumn2;
            HOperatorSet.DrawRectangle1(hWindowHandle, out hRow1, out hColumn1, out hRow2, out hColumn2);
            column1 = hColumn1[0].D;
            row1 = hRow1[0].D;
            column2 = hColumn2[0].D;
            row2 = hRow2[0].D;
        }
        public static void displayRectangle1Screen(HTuple hWindowHandle, double row1, double column1, double row2, double column2)
        {
            //Display of rectangles aligned to the coordinate axes.
            // A rectangle is described by the upper left corner (Row1,Column1) and the lower right corner (Row2,Column2).
            HOperatorSet.DispRectangle1(hWindowHandle, (HTuple)row1, (HTuple)column1, (HTuple)row2, (HTuple)column2);    
        }
        public static void genRectangle1(out HObject hRegionRectangle1, double row1, double column1, double row2,double column2)
        {
            // Create a rectangle parallel to the coordinate axes.
            // The operator generates one or more rectangles parallel to the coordinate axes which are described by the upper left corner (Row1, Column1) and the lower right corner (Row2, Column2).
            HOperatorSet.GenRectangle1(out hRegionRectangle1,(HTuple)row1,(HTuple)column1,(HTuple)row2,(HTuple)column2);
        }
        public static void drawRectangle2Mouse(HTuple hWindowHandle, out double row, out double column, out double phi, out double length1, out double length2)
        {
            //Interactive drawing of any orientated rectangle.
            //The operator  returns the parameter for any orientated rectangle, 
            //which has been created interactively by the user in the window. 
            //To create a rectangle you have to press the left mouse button for the center of the rectangle.
            //While keeping the button pressed you may dimension the length (Length1) and the orientation (Phi) of the first half axis. 
            //In doing so a temporary default length for the second half axis is assumed, which may be modified afterwards on demand. 
            //After another mouse click in the middle of the created rectangle, you can move it. 
            //A click close to one side “grips” it to modify the rectangle's dimension in perpendicular direction to this side. 
            //You only can modify the orientation, if you grip a side perpendicular to the first half axis. 
            //Pressing the right mouse button terminates the procedure. 


            HTuple hRow, hColumn,hphi, hlength1,hlength2;
            HOperatorSet.DrawRectangle2(hWindowHandle, out hRow, out hColumn,out hphi, out hlength1, out hlength2);
            column = hColumn[0].D;
            row = hRow[0].D;
            phi = hphi[0].D;
            length1 = hlength1[0].D;
            length2 = hlength2[0].D;

        }
        public static void displayRectangle2Screen(HTuple hWindowHandle, double row, double column, double phi, double length1, double length2)
        {
            //Displays arbitrarily oriented rectangles.
            //A rectangle is described by the center (CenterRow,CenterCol), the orientation Phi (in radians) and half the lengths of the edges Length1 and Length2. 
            HOperatorSet.DispRectangle2(hWindowHandle, (HTuple)row, (HTuple)column, (HTuple)phi, (HTuple)length1, (HTuple)length2);
        }
        public static void genRectangle2(out HObject hRegionRectangle2, double row, double column, double phi, double length1, double length2)
        {
            // Create a rectangle of any orientation.
            // The operator generates one or more rectangles with the center (Row, Column) , the orientation Phi and the half edge lengths Length1 and Length2.
            HOperatorSet.GenRectangle2(out hRegionRectangle2, (HTuple)row, (HTuple)column, (HTuple)phi, (HTuple)length1, (HTuple)length2);
        }
        public static void displayRectangle1XldScreen(HTuple hWindowHandle, double row1, double column1, double row2, double column2)
        {
            //Displays arbitrarily oriented rectangles XLD.
            //A rectangle XLD is described by the center (CenterRow,CenterCol), the orientation Phi (in radians) and half the lengths of the edges Length1 and Length2. 
            HObject hXldRectangle2, hRegionRectangle1;
            HOperatorSet.GenRectangle1(out hRegionRectangle1, (HTuple)row1, (HTuple)column1, (HTuple)row2, (HTuple)column2);
            HOperatorSet.GenContourRegionXld(hRegionRectangle1, out hXldRectangle2, "border");
            HOperatorSet.DispObj(hXldRectangle2, hWindowHandle);
            hXldRectangle2.Dispose();
        }
        public static void displayRectangle2XldScreen(HTuple hWindowHandle, double row, double column, double phi, double length1, double length2)
        {
            //Displays arbitrarily oriented rectangles XLD.
            //A rectangle XLD is described by the center (CenterRow,CenterCol), the orientation Phi (in radians) and half the lengths of the edges Length1 and Length2. 
            HObject hXldRectangle2;
            HOperatorSet.GenRectangle2ContourXld(out hXldRectangle2,(HTuple)row,(HTuple)column,(HTuple)phi,(HTuple)length1,(HTuple)length2);
            HOperatorSet.DispObj(hXldRectangle2, hWindowHandle);
            hXldRectangle2.Dispose();
        }
        public static void drawEllipseMouse(HTuple hWindowHandle, out double row, out double column, out double phi, out double radius1, out double radius2)
        {
            // Interactive drawing of an ellipse by mouse on the screen.
            //The operator returns the parameter for any orientated ellipse, 
            //which has been created interactively by the user in the window. 
            //The created ellipse is described by its center, its two half axes and the angle between the first half axis and the horizontal coordinate axis. 
            //To create an ellipse you have to determine the center of the ellipse with the left mouse button. 
            //Keeping the button pressed determines the length (Radius1) and the orientation (Phi) of the first half axis. 
            //In doing so a temporary default length for the second half axis is assumed, which may be modified afterwards on demand. 
            //After another mouse click in the center of the created ellipse you can move it. 
            //A mouse click close to a vertex “grips” it to modify the length of the appropriate half axis. 
            //You may modify the orientation only, if a vertex of the first half axis is gripped. 
            //Pressing the right mouse button terminates the procedure. 
            HTuple hRow, hColumn, hphi, hradius1, hradius2;
            HOperatorSet.DrawEllipse(hWindowHandle, out hRow, out hColumn, out hphi, out hradius1, out hradius2);
            column = hColumn[0].D;
            row = hRow[0].D;
            phi = hphi[0].D;
            radius1 = hradius1[0].D;
            radius2 = hradius2[0].D;
        }
        public static void displayEllipseScreen(HTuple hWindowHandle, double row, double column, double phi, double radius1, double radius2)
        {
            // Displays ellipses.
            // An ellipse is described by the center (CenterRow, CenterCol), the orientation Phi (in radians) and the radii of the major and the minor axis (Radius1 and Radius2). 
            HOperatorSet.DispEllipse(hWindowHandle, (HTuple)row, (HTuple)column, (HTuple)phi, (HTuple)radius1, (HTuple)radius2);
        }
        public static void genEllipse(out HObject hRegionEllipse, double row, double column, double phi, double radius1, double radius2)
        {
            //Create an ellipse.
            //The operator generates one or more ellipses with the center (Row, Column), the orientation Phi and the half-radii Radius1 and Radius2. 
            //The angle is indicated in arc measure according to the x axis in mathematically positive direction. 
            //More than one region can be created by passing tuples of parameter values. 
            HOperatorSet.GenEllipse(out hRegionEllipse, (HTuple)row, (HTuple)column, (HTuple)phi, (HTuple)radius1, (HTuple)radius2);
        }
        public static void drawPointMouse(HTuple hWindowHandle, out double row, out double column)
        {
            //Draw a point by mouse on screen.
            //The operator  returns the parameter for a point, which has been created interactively by the user in the window. 
            //To create a point you have to press the left mouse button. 
            //While keeping the button pressed you may “drag” the point in any direction. 
            //Pressing the right mouse button terminates the procedure. 

            HTuple hRow, hColumn;
            HOperatorSet.DrawPoint(hWindowHandle, out hRow, out hColumn);
            column = hColumn[0].D;
            row = hRow[0].D;
        }
        public static void displayCrossScreen(HTuple hWindowHandle, double row, double column, double size, double angel)
        {
            //Displays crosses in a window.
            //A cross is described by the coordinates of the center point (Row,Column), the length of its bars Size and the orientation Angle.
            HOperatorSet.DispCross(hWindowHandle,(HTuple)row, (HTuple)column,(HTuple)size,(HTuple)angel);
        }
        public static void drawRegionMouseScreen(out HObject hRegion,HTuple hWindowHangle)
        {
            //Interactive drawing of a closed region.
            //The region of that image spans exactly the image region entered interactively by mouse clicks (gray values remain undefined). 
            //Painting happens in the output window while keeping the left mouse button pressed.
            //The left mouse button even operates by clicking in the output window; 
            //through this a line between the previous clicked points is drawn. 
            //Clicking the right mouse button terminates input and closes the outline. 
            //Subsequently the image is “filled up”.
            //Also it contains the whole image area enclosed by the mouse. 
            //Pressing the right mouse button terminates the procedure.
            HOperatorSet.DrawRegion(out hRegion, hWindowHangle);

        }
    }
}
