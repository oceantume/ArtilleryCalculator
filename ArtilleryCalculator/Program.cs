using System;
using System.Net.Http;
using System.Windows.Forms;

namespace ArtilleryCalculator
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var httpClient = new HttpClient();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new CalculatorForm(httpClient));
        }
    }
}
