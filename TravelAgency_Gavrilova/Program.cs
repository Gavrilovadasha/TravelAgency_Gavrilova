using System;
using System.Windows.Forms;
using DataGrid.Storage.Memory;
using DataGrid.TourManagment;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Extensions.Logging;


namespace TravelAgency_Gavrilova
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var serilogLogger = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .WriteTo.Seq("http://localhost:5341", apiKey: "UnT4YNRs687dCwJZa54N")
                .CreateLogger();

            var logger = new SerilogLoggerFactory(serilogLogger)
                .CreateLogger("dataGrid");

            var storage = new DBTourStorage();
            var manager = new TourManagment(storage, logger);

            Application.Run(new RegisterOfBurningTours(manager));
        }
    }
}
