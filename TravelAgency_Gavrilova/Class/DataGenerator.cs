using DataGrid.Contracts.Models;
using System;
using System.Windows.Forms;

namespace TravelAgency_Gavrilova
{
    internal class DataGenerator
    {
        public static Tour CreateTour(Action<Tour> settings = null)
        {
            var result = new Tour
            {
                ID = Guid.NewGuid(),
                Direction = Direction.Turckey,
                DeparturDate = DateTime.Now,
            };

            settings?.Invoke(result);

            return result;
        }
    }
}
