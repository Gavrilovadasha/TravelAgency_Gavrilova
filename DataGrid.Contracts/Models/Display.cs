using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Linq;

namespace DataGrid.Contracts.Models
{
    internal class Display
    {
        [Display(Name = "Направление")]
        public string Direction { get; set; }

        [Display(Name = "Дата отправления")]
        public DateTime DeparturDate { get; set; }

        [Display(Name = "Количество ночей")]
        public int NumberNights { get; set; }

        [Display(Name = "Стоимость для отдыхающих")]
        public decimal CostVacationers { get; set; }

        [Display(Name = "Количество отдыхающих")]
        public int NumberVacationers { get; set; }

        [Display(Name = "Дополнительные сборы")]
        public decimal Surcharges { get; set; }

        [Display(Name = "Wi-Fi доступ")]
        public bool WIFI { get; set; }
    }
}
