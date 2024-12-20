﻿
using DataGrid.Contracts.Models;

namespace DataGrid.TourManagment.Models
{
    /// <summary>
    /// Модель статистики туров.
    /// </summary>
    public class TourStatsModel : ITourStats
    {
        /// <summary>
        /// Количество туров.
        /// </summary>
        public int CountTour { get; set; }

        /// <summary>
        /// Общий количество туров.
        /// </summary>
        public int TotalAmountTours { get; set; }
        /// <summary>
        /// Количество туров с дополнительными сборами.
        /// </summary>
        public int CountToursSurcharges { get; set; }

        /// <summary>
        /// Общий суммарный объем дополнительных сборов.
        /// </summary>
        public int TotalAmountSurcharges { get; set; }

        long ITourStats.CountTour => throw new System.NotImplementedException();

        long ITourStats.TotalAmountTours => throw new System.NotImplementedException();

        long ITourStats.CountToursSurcharges => throw new System.NotImplementedException();

        long ITourStats.TotalAmountSurcharges => throw new System.NotImplementedException();
    }
}
