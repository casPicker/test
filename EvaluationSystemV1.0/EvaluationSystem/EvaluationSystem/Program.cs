using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DevExpress.Skins;

namespace EvaluationSystem
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("zh-CN");
            SkinManager.EnableFormSkins();
            SkinManager.EnableMdiFormSkins();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new LoginForm());
            //Application.Run(new MainForm());
        }
    }
}
