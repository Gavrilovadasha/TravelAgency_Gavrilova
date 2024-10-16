using DataGrid.Contracts;
using DataGrid.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace TourManagement
{
    public class TourManagment : ITourManagment
    {
        private ITourStorage tourStorage;

        public TourManagment(ITourStorage tourStorage)
        {
            this.tourStorage = tourStorage;
        }

        public async Task<Tour> AddAsync(Tour tour)
        {
            var result = await tourStorage.AddAsync(tour);
            return result;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var result = await tourStorage.DeleteAsync(id);
            return result;
        }

        public Task EditAsync(Tour tour)
            => tourStorage.EditAsync(tour);

        public Task<IReadOnlyCollection<Tour>> GetAllAsync()
            => tourStorage.GetAllAsync();

        public async Task<ITourStats> GetStatsAsync()
        {
            var result = await tourStorage.GetAllAsync();
            return new TourStatsModel
            {
                CountTour = result.Count,
                TotalAmountTours = result.Count(),
                CountToursSurcharges = result.Count(),
                TotalAmountSurcharges = result.Count(),
            };
        }
    }

}

