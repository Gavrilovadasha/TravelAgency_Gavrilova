using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using DataGrid.Contracts.Models;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace TravelAgency_Gavrilova
{
    /// <summary>
    /// Форма для добавления информации о туре.
    /// </summary>
    public partial class AddTourForm : Form
    {
        private Tour tour;
        /// <summary>
        /// Конструктор формы добавления тура.
        /// </summary>
        public AddTourForm(Tour tour = null)
        {
            InitializeComponent();

            this.tour = tour == null ? DataGenerator.CreateTour(x =>
            {
                x.Direction = Direction.Turckey;
                x.DeparturDate = DateTime.Now;
            })
                : new Tour
                {
                    ID = tour.ID,
                    Direction = tour.Direction,
                    DeparturDate = tour.DeparturDate,
                    NumberNights = tour.NumberNights,
                    CostVacationers = tour.CostVacationers,
                    NumberVacationers = tour.NumberVacationers,
                    Surcharges = tour.Surcharges,
                    WIFI = tour.WIFI,
                };

            foreach (var item in Enum.GetValues(typeof(Direction)))
            {
                cmbDirection.Items.Add(item);
            }
            if (cmbDirection.Items.Count > 0)
            {
                cmbDirection.SelectedIndex = 0;
            }

            cmbDirection.AddBinding(x => x.SelectedItem, this.tour, x => x.Direction, errorProvider1);
            dTPDeparturDate.AddBinding(x => x.Value, this.tour, x => x.DeparturDate, errorProvider1);
            numUpDownNumberNights.AddBinding(x => x.Value, this.tour, x => x.NumberNights, errorProvider1);
            numUpDownCostVacationers.AddBinding(x => x.Value, this.tour, x => x.CostVacationers, errorProvider1);
            numUpDownNumberVacationers.AddBinding(x => x.Value, this.tour, x => x.NumberVacationers, errorProvider1);
            numUpDownSurcharges.AddBinding(x => x.Value, this.tour, x => x.Surcharges, errorProvider1);
            checkBoxWIFI.AddBinding(x => x.Checked, this.tour, x => x.WIFI, errorProvider1);
        }

        /// <summary>
        /// Получает текущий объект тура.
        /// </summary>
        public Tour Tour => tour;
        private void cmbDirection_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();

            if (e.Index > -1)
            {
                var value = (Direction)(sender as System.Windows.Forms.ComboBox).Items[e.Index];
                e.Graphics.DrawString(GetDisplayValue(value),
                    e.Font,
                    new SolidBrush(e.ForeColor),
                    e.Bounds.X,
                    e.Bounds.Y);
            }
        }
        private string GetDisplayValue(Direction value)
        {
            var field = value.GetType().GetField(value.ToString());
            var attributes = field.GetCustomAttributes<DescriptionAttribute>(false);
            return attributes.FirstOrDefault()?.Description;
        }
    }
}
