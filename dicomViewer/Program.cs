using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace dicomViewer
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// 
        public static Form1 mainForm;
        public static SetTempDir setTForm;
        public static string TempDir = "";
        public static Ini IniObj;
        [STAThread]

     
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            IniObj = new Ini(AppDomain.CurrentDomain.BaseDirectory + "Ini.ini");
            TempDir = IniObj.IniReadValue("Info", "temp_folder");
            mainForm = new Form1();
            setTForm = new SetTempDir();
            Application.Run(mainForm);
        }
    }
}
