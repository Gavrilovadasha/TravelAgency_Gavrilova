using DataGrid.Contracts.Models;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace TravelAgency_Gavrilova
{
    public partial class AddTourForm : Form
    {
        private Tour tour;
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
                comboBox1.Items.Add(item);
            }
            if (comboBox1.Items.Count > 0)
            {
                comboBox1.SelectedIndex = 0;
            }

            comboBox1.AddBinding(x => x.SelectedItem, this.tour, x => x.Direction, errorProvider1);
            dateTimePicker1.AddBinding(x => x.Value, this.tour, x => x.DeparturDate, errorProvider1);
            numericUpDown1.AddBinding(x => x.Value, this.tour, x => x.NumberNights, errorProvider1);
            textBox1.AddBinding(x => x.Text, this.tour, x => x.CostVacationers, errorProvider1);
            numericUpDown2.AddBinding(x => x.Value, this.tour, x => x.NumberVacationers, errorProvider1);
            textBox2.AddBinding(x => x.Text, this.tour, x => x.Surcharges, errorProvider1);
            checkBox1.AddBinding(x => x.Checked, this.tour, x => x.WIFI, errorProvider1);
        }

        public Tour Tour => tour;

        private void comboBox1_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();

            if (e.Index > -1)
            {
                var value = (Direction)(sender as ComboBox).Items[e.Index];
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
