using DataGrid.Contracts.Models;
using DataGrid.Contracts;
using DataGrid.DataStorage.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace DataGrid.Storage.Memory
{
    public class DBTourStorage : ITourStorage
    {
        public async Task<Tour> AddAsync(Tour tour)
        {
            using (var context = new DataGridContext())
            {
                context.Tours.Add(tour);
                await context.SaveChangesAsync();
            }
            return tour;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            using (var context = new DataGridContext())
            {
                var item = await context.Tours.FirstOrDefaultAsync(x => x.ID == id);
                if (item != null)
                {
                    context.Tours.Remove(item);
                    await context.SaveChangesAsync();
                    return true;
                }
            }
            return false;
        }

        public async Task EditAsync(Tour tour)
        {
            using (var context = new DataGridContext())

            {
                var target = await context.Tours.FirstOrDefaultAsync(x => x.ID == tour.ID);
                if (target != null)
                {
                    target.Direction = tour.Direction;
                    target.DeparturDate = tour.DeparturDate;
                    target.NumberNights = tour.NumberNights;
                    target.CostVacationers = tour.CostVacationers;
                    target.NumberVacationers = tour.NumberVacationers;
                    target.WIFI = tour.WIFI;
                    target.Surcharges = tour.Surcharges;

                }
                await context.SaveChangesAsync();

            }
        }

        public async Task<IReadOnlyCollection<Tour>> GetAllAsync()
        {
            using (var context = new DataGridContext())
            {
                var items = await context.Tours
                    .OrderBy(x => x.Direction)
                    .ToListAsync();
                return items;
            }
        }
    }
}
