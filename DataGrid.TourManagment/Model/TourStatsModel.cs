using DataGrid.Contracts.Models;

namespace TourManagement
{
    public class TourStatsModel : ITourStats
    {
        public int CountTour { get; set; }

        public int TotalAmountTours { get; set; }

        public int CountToursSurcharges { get; set; }

        public int TotalAmountSurcharges { get; set; }

    }
}
