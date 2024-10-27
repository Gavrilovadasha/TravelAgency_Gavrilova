
namespace DataGrid.Contracts.Models
{
    /// <summary>
    /// Интерфейс, определяющий контракт для получения статистики по турам.
    /// </summary>
    public interface ITourStats
    {
        int CountTour { get; }

        int TotalAmountTours { get; }

        int CountToursSurcharges { get; }

        int TotalAmountSurcharges { get; }

    }
}
