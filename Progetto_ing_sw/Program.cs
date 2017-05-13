using Progetto_ing_sw.Model;
using Progetto_ing_sw.Utils;
using Progetto_ing_sw.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;
using System.Configuration;
using Progetto_ing_sw.Properties;

namespace Progetto_ing_sw
{
    static class Program
    {
        public static Form CurrentForm;
        public static CultureInfo Culture = new CultureInfo("it-IT");

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Drawing.init();
            ISpiaggia spiaggia = Spiaggia.GetInstance();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            string spiaggiaPath = Settings.Default["spiaggia"].ToString();
            string ordiniPath = Settings.Default["ordini"].ToString();
            
            CurrentForm = new LoaderForm();
            Application.CurrentCulture = Culture;
            while (CurrentForm != null && !CurrentForm.IsDisposed)
            {
                Application.Run(CurrentForm);
            }
        }
    }
}
