using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
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

