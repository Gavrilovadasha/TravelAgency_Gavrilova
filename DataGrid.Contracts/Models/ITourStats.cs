
namespace DataGrid.Contracts.Models
{
    /// <summary>
    /// Интерфейс, определяющий контракт для получения статистики по турам.
    /// </summary>
    public interface ITourStats
    {
        long CountTour { get; }

        long TotalAmountTours { get; }

        long CountToursSurcharges { get; }

        long TotalAmountSurcharges { get; }

    }
}
