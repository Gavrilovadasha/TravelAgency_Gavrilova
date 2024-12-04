
using System.Data.Entity;

using DataGrid.Contracts.Models;

namespace DataGrid.DataStorage.Entity
{
    public class DataGridContext : DbContext
    {

        /// <summary>
        /// Конструктор контекста базы данных
        /// </summary>
        public DataGridContext() : base("TourDataGridDB")
        { }

        /// <summary>
        /// Таблица <see cref="Tours"/> в базе данных
        /// </summary>
        public DbSet<Tour> Tours { get; set; }
    }
}

