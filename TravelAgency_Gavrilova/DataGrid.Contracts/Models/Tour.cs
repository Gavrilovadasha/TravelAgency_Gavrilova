using System;

namespace DataGrid.Contracts.Models
{
    /// <summary>
    /// Поля для ввода информации о туре
    /// </summary>
    public class Tour 
    {
        public Guid ID { get; set; }

        /// <summary>
        /// Направление
        /// </summary>
        public Direction Direction { get; set; }

        /// <summary>
        /// Дата вылета
        /// </summary>
        public DateTime DeparturDate { get; set; }

        /// <summary>
        /// Количество ночей
        /// </summary>
        public decimal NumberNights { get; set; }

        /// <summary>
        /// Стоимость для одного отдыхающего
        /// </summary>
        public decimal CostVacationers { get; set; }

        /// <summary>
        /// Количество отдыхающих
        /// </summary>
        public decimal NumberVacationers { get; set; }

        /// <summary>
        /// Доплаты
        /// </summary>
        public decimal Surcharges { get; set; }

        /// <summary>
        /// Наличие интернета
        /// </summary>
        public bool WIFI { get; set; }

        public decimal TotalCost { get; set; }
    }
}
