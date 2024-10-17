using DataGrid.Contracts;
using DataGrid.Contracts.Models;
using System;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TravelAgency_Gavrilova
{
    /// <summary>
    /// Форма регистра горящих туров.
    /// </summary>
    public partial class RegisterOfBurningTours : Form
    {
        private ITourManagment tourManagment;
        private BindingSource bindingSource;

        /// <summary>
        /// Инициализирует новый экземпляр класса RegisterOfBurningTours.
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

        /// <summary>
        /// Загружает данные из источника данных в DataGridView.
        /// </summary>
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

        private async void toolStripBtnDel_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count != 0)
            {
                var data = (Tour)dataGridView1.Rows[dataGridView1.SelectedRows[0].Index].DataBoundItem;
                if (MessageBox.Show($"Вы действительно хотите удалить {data.Direction}?", "Удаление записи", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    await tourManagment.DeleteAsync(data.ID);
                    bindingSource.ResetBindings(false);
                    await SetStats();
                }
            }
        }

        private async void toolStripBtnEdit_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count != 0)
            {
                var data = (Tour)dataGridView1.Rows[dataGridView1.SelectedRows[0].Index].DataBoundItem;
                var tourForm = new AddTourForm(data);
                if (tourForm.ShowDialog(this) == DialogResult.OK)
                {
                    await tourManagment.EditAsync(tourForm.Tour);
                    bindingSource.ResetBindings(false);
                    await SetStats();
                }
            }
        }
    }
}
