using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataGrid.Contracts;
using DataGrid.Contracts.Models;


namespace DataGrid.Storage.Memory
{
    /// <summary>
    /// Реализация хранилища туров в памяти.
    /// </summary>
    public class MemoryTourStorage : ITourStorage
    {
        private List<Tour> tours;
        /// <summary>
        /// Конструктор класса MemoryTourStorage.
        /// </summary>
        public MemoryTourStorage()
        {
            tours = new List<Tour>();
        }
        /// <summary>
        /// Добавляет новый тур в хранилище.
        /// </summary>
        public Task<Tour> AddAsync(Tour tour)
        {
            tours.Add(tour);
            return Task.FromResult(tour);
        }
        /// <summary>
        /// Удаляет тур из хранилища по его уникальному идентификатору.
        /// </summary>
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
        /// <summary>
        /// Обновляет информацию о существующем туре в хранилище.
        /// </summary>
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
        /// <summary>
        /// Получает все туры из хранилища.
        /// </summary>
        public Task<IReadOnlyCollection<Tour>> GetAllAsync()
            => Task.FromResult<IReadOnlyCollection<Tour>>(tours);
    }
}
