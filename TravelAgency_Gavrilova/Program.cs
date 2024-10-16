using DataGrid.Storage.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using TourManagement;

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
            var storage = new MemoryTourStorage();
            var manager = new TourManagment(storage);

            Application.Run(new RegisterOfBurningTours(manager));
        }
    }
}
