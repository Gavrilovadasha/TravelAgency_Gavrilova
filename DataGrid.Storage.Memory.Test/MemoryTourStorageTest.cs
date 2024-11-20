using Xunit;
using FluentAssertions;
using DataGrid.Contracts;
using System.Diagnostics.Contracts;
using System.Threading.Tasks;
using System;
using DataGrid.Storage.Memory;
using DataGrid.Contracts.Models;
using System.Linq;

namespace datagridview.Tour.Storage.Tests
{
    /// <summary>
    /// Тесты для класса <see cref="TourStorage"/>.
    /// </summary>
    public class TourStorageTests
    {
        public readonly ITourStorage tourStorage;

        /// <summary>
        /// Конструтор 
        /// </summary>
        public TourStorageTests()
        {
            tourStorage = new MemoryTourStorage();
        }

        /// <summary>
        /// Добавление тура корректно
        /// </summary>
        [Fact]
        public async Task AddTourNeedToBeCorrect()
        {
            var tour = new DataGrid.Contracts.Models.Tour
            {
                ID = Guid.NewGuid(),
                Direction = Direction.Turckey,
                DeparturDate = DateTime.Today,
                NumberNights = 5,
                CostVacationers = 10000,
                NumberVacationers = 1,
                Surcharges = 500,
                WIFI = false
            };

            var result = await tourStorage.AddAsync(tour);

            result.Should()
                .NotBeNull()
                .And.BeEquivalentTo(new
                {
                    tour.ID,
                    tour.Direction,
                    tour.DeparturDate,
                    tour.NumberNights,
                    tour.CostVacationers,
                    tour.NumberVacationers,
                    tour.Surcharges,
                    tour.WIFI
                });
        }

        /// <summary>
        /// Удаление тура корректно
        /// </summary>
        [Fact]
        public async Task DeleteTourNeedToBeCorrect()
        {
            var tour = new DataGrid.Contracts.Models.Tour
            {
                ID = Guid.NewGuid(),
                Direction = Direction.Turckey,
                DeparturDate = DateTime.Now,
                NumberNights = 5,
                CostVacationers = 5,
                NumberVacationers = 5,
                Surcharges = 10000,
                WIFI = false,
            };

            // Act
            await tourStorage.AddAsync(tour);
            var result = await tourStorage.DeleteAsync(tour.ID);
            var dellRes = await tourStorage.GetAllAsync();

            // Assert
            result.Should().BeTrue();
            dellRes.Should().NotContain(p => p.ID == tour.ID);
        }

        /// <summary>
        /// Изменение списка туров корректно
        /// </summary>
        [Fact]
        public async Task EditTaskNeedToBeCorrect()
        {
            var tourId = Guid.NewGuid();
            var tour = new DataGrid.Contracts.Models.Tour
            {
                ID = tourId,
                Direction = Direction.Turckey,
                DeparturDate = DateTime.Now,
                NumberNights = 5,
                CostVacationers = 5,
                NumberVacationers = 5,
                Surcharges = 10000,
                WIFI = false,
            };

            var newTour = new DataGrid.Contracts.Models.Tour
            {
                ID = tourId,
                Direction = Direction.Turckey,
                DeparturDate = DateTime.Now,
                NumberNights = 3,
                CostVacationers = 100000,
                NumberVacationers = 5,
                Surcharges = 500,
                WIFI = false,
            };

            // Act
            await tourStorage.AddAsync(tour);
            await tourStorage.EditAsync(newTour);
            var result = await tourStorage.GetAllAsync();
            var product = result.FirstOrDefault(x => x.ID == tourId);

            // Assert
            product.Should().BeEquivalentTo(new
            {
                newTour.Direction,
                newTour.DeparturDate,
                newTour.NumberNights,
                newTour.CostVacationers,
                newTour.Surcharges,
                newTour.WIFI,
            });
        }

        /// <summary>
        /// Получение туров в пустом контейнере возвращает ничего
        /// </summary>
        [Fact]
        public async Task GetAllEmptyToursNeedToReturnEmpty()
        {
            var result = await tourStorage.GetAllAsync();
            result.Should()
                .NotBeNull()
                .And.BeEmpty();
        }
    }
}
