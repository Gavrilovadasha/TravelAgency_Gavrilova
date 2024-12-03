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
        public int NumberNights { get; set; }

        /// <summary>
        /// Стоимость для одного отдыхающего
        /// </summary>
        public int CostVacationers { get; set; }

        /// <summary>
        /// Количество отдыхающих
        /// </summary>
        public int NumberVacationers { get; set; }

        /// <summary>
        /// Доплаты
        /// </summary>
        public int Surcharges { get; set; }

        /// <summary>
        /// Наличие интернета
        /// </summary>
        public bool WIFI { get; set; }

        public int TotalCost { get; set; }

        /// <summary>
        /// Конструктор по умолчанию для класса Tour.
        /// </summary>
        public Tour()
        {
            TotalCost = 0;
        }
        /// <summary>
        /// Вычисляет общую стоимость тура.
        /// </summary>
        public decimal CalculateTotalCost()
        {
            return CostVacationers * NumberVacationers + Surcharges;
        }
    }
}
