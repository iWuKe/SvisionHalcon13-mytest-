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
    public partial class TemplateMatch : Form
    {
        HObject Mimg;
        HTuple MhvWindowHandle = null;
        double rx1, rx2, ry1, ry2;
        public TemplateMatch()
        {
            InitializeComponent();
        }
        private void TemplateMatch_Load(object sender, EventArgs e)
        {
            if (HDevWindowStack.IsOpen())
                HOperatorSet.CloseWindow(HDevWindowStack.Pop());
            HOperatorSet.SetWindowAttr("border_width", 0);
            HOperatorSet.SetWindowAttr("background_color", "black");
            HOperatorSet.OpenWindow(0, 0, 1024, 768, TMImage.Handle, "visible", "", out MhvWindowHandle);
            HOperatorSet.SetPart(MhvWindowHandle, 0, 0, 767, 1023);
            HDevWindowStack.Push(MhvWindowHandle);
           
        }
        private void LoadImage_Click(object sender, EventArgs e)
        {
            Mimg = DataPass.imgpass;
            HOperatorSet.DispObj(Mimg, MhvWindowHandle);
        }

        private void TMregion_Click(object sender, EventArgs e)
        {
            basicClass.drawRectangle1Mouse(MhvWindowHandle, out rx1, out ry1, out rx2, out ry2);
            basicClass.displayRectangle1Screen(MhvWindowHandle, rx1, ry1, rx2, ry2);
        }

        private void TMImage_Click(object sender, EventArgs e)
        {

        }

       
    }
}
