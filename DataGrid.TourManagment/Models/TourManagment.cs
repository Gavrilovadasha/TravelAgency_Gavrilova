using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Linq;
using DataGrid.Contracts;
using DataGrid.Contracts.Models;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using DataGrid.Contracts.Interface;
using DataGrid.TourManagment.Models;

namespace DataGrid.TourManagment
{
    /// <summary>
    /// Управляет операциями с турами.
    /// </summary>
    public class TourManagment : ITourManagment
    {
        private ITourStorage tourStorage;
        private readonly ILogger logger;

        public TourManagment(ITourStorage tourStorage, ILogger logger)
        {
            this.tourStorage = tourStorage;
            this.logger = logger;
        }

        /// <summary>
        /// Добавляет новый тур асинхронно.
        /// </summary>
        public async Task<Tour> AddAsync(Tour tour)
        {
            var timer = Stopwatch.StartNew();
            var result = await tourStorage.AddAsync(tour);
            result.TotalCost = (int)tour.CalculateTotalCost();

            timer.Stop();

            logger.LogInformation("Добавление произошло за {} мс", timer.ElapsedMilliseconds);
            logger.LogInformation($"Добавлен тур: {result.Direction}, Идентификатор: {result.ID},Дата вылета: {result.DeparturDate}, Количество ночей: {result.NumberNights}, Стоимость за отдыхающего {result.CostVacationers}, Количество отдыхающих {result.NumberVacationers}, Доплаты {result.Surcharges}, С WI-FI: {result.WIFI}, Итоговая стоимость: {result.TotalCost}");
            return result;


        }

        /// <summary>
        /// Удаляет тур по GUID идентификатору асинхронно.
        /// </summary>
        public async Task<bool> DeleteAsync(Guid id)
        {
            var timer = Stopwatch.StartNew();
            var result = await tourStorage.DeleteAsync(id);

            timer.Stop();

            logger.LogInformation("Удаление произошло за {} мс", timer.ElapsedMilliseconds);
            logger.LogInformation($"Удален тур: {id}");
            return result;
        }

        /// <summary>
        /// Изменяет существующий тур асинхронно.
        /// </summary>
        public Task EditAsync(Tour tour)
        {
            var timer = Stopwatch.StartNew();

            tour.TotalCost = (int)tour.CalculateTotalCost();
            timer.Stop();

            logger.LogInformation("Редактирование произошло за {} мс", timer.ElapsedMilliseconds);
            logger.LogInformation($"Отредактирован тур: {tour.Direction}, Идентификатор: {tour.ID},Дата вылета: {tour.DeparturDate}, Количество ночей: {tour.NumberNights}, Стоимость за отдыхающего {tour.CostVacationers}, Количество отдыхающих {tour.NumberVacationers}, Доплаты {tour.Surcharges}, С WI-FI: {tour.WIFI}, Итоговая стоимость: {tour.TotalCost}");

            return tourStorage.EditAsync(tour);
        }

        /// <summary>
        /// Получает все туры асинхронно.
        /// </summary>
        public async Task<IReadOnlyCollection<Tour>> GetAllAsync()
        {
            var tours = await tourStorage.GetAllAsync();
            foreach (var tour in tours)
            {
                tour.TotalCost = (int)tour.CalculateTotalCost();
            }
            return tours;
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
}
