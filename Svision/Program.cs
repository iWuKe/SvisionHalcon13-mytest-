#define SVISION_VERSION_MAIN_02
#define SVISION_VERSION_SUB_0008

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using HalconDotNet;
using Basler.Pylon;
namespace Svision
{
    static class Program
    {
        public static int MainVersion  =   0x02;
        public static int SubVersion   =   0x0008;

        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Svision());

            //Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
        }

        static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            while (true)
            {
            }
        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            while (true)
            {
            }
        }
     
    }
}
