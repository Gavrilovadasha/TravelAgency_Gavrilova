using DataGrid.Contracts.Models;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace TravelAgency_Gavrilova
{
    public partial class AddTourForm : Form
    {
        private Tour tour;
        public AddTourForm(Tour tour = null)
        {
            InitializeComponent();
            TotalCostCalculate();


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
                    TotalCost = tour.TotalCost,
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
            numeUpDownSurcharges.AddBinding(x => x.Value, this.tour, x => x.Surcharges, errorProvider1);
            checkBoxWIFI.AddBinding(x => x.Checked, this.tour, x => x.WIFI, errorProvider1);
            txtTotalCost.AddBinding(x => x.Text, this.tour, x => x.TotalCost, errorProvider1);
        }

        private void TotalCostCalculate()
        {
            txtTotalCost.Text = (((numUpDownCostVacationers.Value * numUpDownNumberVacationers.Value) + numeUpDownSurcharges.Text));
        }

        private bool isUpdating = false;
        private void TotalCostCalculate(object sender, EventArgs e)
        {
            if (isUpdating) return;

            isUpdating = true;

            try
            {
                TotalCostCalculate(sender,e);
            }
            finally
            {
                isUpdating = false;
            }
        }
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
                    e.Bounds.X + 20,
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
