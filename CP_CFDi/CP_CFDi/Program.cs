using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace CP_CFDi
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string [] reciboDatosDelphi)
        {
            //MessageBox.Show(reciboDatosDelphi[0]);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            try
            {
                Application.Run(new CP_CFDi(reciboDatosDelphi[0]));
            }
            catch (Exception) {
                Application.Run(new CP_CFDi("197035")); 
            }
        }
    }
}
