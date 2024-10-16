using DataGrid.Contracts;
using DataGrid.Contracts.Models;
using System;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TravelAgency_Gavrilova
{
    /// <summary>
    /// 
    /// </summary>
    public partial class RegisterOfBurningTours : Form
    {
        private ITourManagment tourManagment;
        private BindingSource bindingSource;

        /// <summary>
        /// 
        /// </summary>
        public RegisterOfBurningTours(ITourManagment tourManagment)
        {
            this.tourManagment = tourManagment;
            bindingSource = new BindingSource();

            InitializeComponent();

            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.DataSource = bindingSource;
        }

        private async void toolStripBtnAdd_Click(object sender, EventArgs e)
        {
            var addTourForm = new AddTourForm();
            if (addTourForm.ShowDialog(this) == DialogResult.OK)
            {
                await tourManagment.AddAsync(addTourForm.Tour);
                bindingSource.ResetBindings(false);
                await SetStats();
            }
        }
        public async Task SetStats()
        {
            var result = await tourManagment.GetStatsAsync();
            toolStripStatusLabel1.Text = $"Всего туров: {result.CountTour}";
            toolStripStatusLabel2.Text = $"Общая сумма за все туры:{result.TotalAmountTours}";
            toolStripStatusLabel3.Text = $"Кол-во туров с доплатами: {result.CountToursSurcharges}";
            toolStripStatusLabel4.Text = $"Общая сумма доплат: {result.TotalAmountSurcharges}";
        }

        private async void RegisterOfBurningTours_Load(object sender, EventArgs e)
        {
            bindingSource.DataSource = await tourManagment.GetAllAsync();
            await SetStats();
        }
    }
}
