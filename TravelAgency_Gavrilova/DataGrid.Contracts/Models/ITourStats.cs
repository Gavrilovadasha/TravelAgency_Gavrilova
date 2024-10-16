
namespace DataGrid.Contracts.Models
{
    public interface ITourStats
    {
        int CountTour { get; }

        int TotalAmountTours { get; }

        int CountToursSurcharges { get; }

        int TotalAmountSurcharges { get; }

    }
}
