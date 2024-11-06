using System;
using System.Windows.Forms;
using DataGrid.Storage.Memory;
using Microsoft.Extensions.Logging;


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
            ILoggerFactory factory = LoggerFactory.Create(builder => builder.AddConsole());
            ILogger logger = factory.CreateLogger("TravelAgency");

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var storage = new MemoryTourStorage();
            var manager = new TourManagment(storage, logger);

            Application.Run(new RegisterOfBurningTours(manager));
        }
    }
}
