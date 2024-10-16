using DataGrid.Contracts;
using DataGrid.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataGrid.Storage.Memory
{
    public class MemoryTourStorage : ITourStorage
    {
        private List<Tour> tours;

        public MemoryTourStorage()
        {
            tours = new List<Tour>();
        }

        public Task<Tour> AddAsync(Tour tour)
        {
            tours.Add(tour);
            return Task.FromResult(tour);
        }

        public Task<bool> DeleteAsync(Guid id)
        {
            var tour = tours.FirstOrDefault(x => x.ID == id);
            if (tour != null)
            {
                tours.Remove(tour);
                return Task.FromResult(true);
            }

            return Task.FromResult(false);
        }

        public Task EditAsync(Tour tour)
        {
            var target = tours.FirstOrDefault(x => x.ID == tour.ID);
            if (tour != null)
            {
                target.Direction = tour.Direction;
                target.DeparturDate = tour.DeparturDate;
                target.NumberNights = tour.NumberNights;
                target.CostVacationers = tour.CostVacationers;
                target.NumberVacationers = tour.NumberVacationers;
                target.Surcharges = tour.Surcharges;
                target.WIFI = tour.WIFI;
            }

            return Task.CompletedTask;
        }

        public Task<IReadOnlyCollection<Tour>> GetAllAsync()
            => Task.FromResult<IReadOnlyCollection<Tour>>(tours);
    }
}
