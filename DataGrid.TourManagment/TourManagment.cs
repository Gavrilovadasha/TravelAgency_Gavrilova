using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using TourManagement;
using System.Linq;
using DataGrid.Contracts;
using DataGrid.Contracts.Models;

/// <summary>
/// Управляет операциями с турами.
/// </summary>
public class TourManagment : ITourManagment
{
    private ITourStorage tourStorage;

    public TourManagment(ITourStorage tourStorage)
    {
        this.tourStorage = tourStorage;
    }

    /// <summary>
    /// Добавляет новый тур асинхронно.
    /// </summary>
    public async Task<Tour> AddAsync(Tour tour)
    {
        var result = await tourStorage.AddAsync(tour);
        result.TotalCost = (int)tour.CalculateTotalCost();
        return result;
    }

    /// <summary>
    /// Удаляет тур по GUID идентификатору асинхронно.
    /// </summary>
    public async Task<bool> DeleteAsync(Guid id)
    {
        var result = await tourStorage.DeleteAsync(id);
        return result;
    }

    /// <summary>
    /// Изменяет существующий тур асинхронно.
    /// </summary>
    public Task EditAsync(Tour tour)
    {
        tour.TotalCost = (int)tour.CalculateTotalCost();
        return tourStorage.EditAsync(tour);
    }

    /// <summary>
    /// Получает все туры асинхронно.
    /// </summary>
    public Task<IReadOnlyCollection<Tour>> GetAllAsync()
    {
        var tours = tourStorage.GetAllAsync().Result;

        foreach (var tour in tours)
        {
            tour.TotalCost = (int)tour.CalculateTotalCost();
        }
        return Task.FromResult(tours);
    }

    /// <summary>
    /// Получает статистику по турам асинхронно.
    /// </summary>
    public async Task<ITourStats> GetStatsAsync()
    {
        var result = await tourStorage.GetAllAsync();

        foreach (var tour in result)
        {
            tour.TotalCost = (int)tour.CalculateTotalCost();
        }

        return new TourStatsModel
        {
            CountTour = result.Count,
            TotalAmountTours = result.Sum(x => x.TotalCost),
            CountToursSurcharges = result.Where(x => x.Surcharges > 0).Count(),
            TotalAmountSurcharges = result.Sum(x => x.Surcharges),
        };
    }
}
