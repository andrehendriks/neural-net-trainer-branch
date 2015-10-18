using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NeuroSky.ThinkGear;
using NeuroSky.ThinkGear.Algorithms;
using System.Windows.Forms;
namespace WindowsFormsApplication1
{
    static class ondatareceived
    {
        
        /// <summary>
        /// 
        /// </summary>
        [STAThread]
        static void btn_connect_click()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form2());
        }
    }
}
