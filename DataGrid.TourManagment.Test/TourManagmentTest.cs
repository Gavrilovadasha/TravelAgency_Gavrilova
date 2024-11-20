using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moq;
using DataGrid.Contracts;
using DataGrid.Contracts.Models;
using Xunit;
using DataGrid.Contracts.Interface;
using DataGrid.TourManagment.Models;
using FluentAssertions;


namespace DataGrid.TourManagment.Test
{
    /// <summary>
    /// Тесты для <see cref="TourManagment"/>
    /// </summary>
    public class TourManagementTest
    {
        private readonly ITourManagment tourManagment;
        private readonly Mock<ITourStorage> tourStorageMock;
        private readonly Mock<ILogger> loggerMock;

        private readonly Task<IReadOnlyCollection<Tour>> filledDefaultNailList;

        public TourManagementTest()
        {
            tourStorageMock = new Mock<ITourStorage>();
            loggerMock = new Mock<ILogger>();
            loggerMock.Setup(x => x.Log(LogLevel.Information,
               It.IsAny<EventId>(),
               It.IsAny<It.IsAnyType>(),
               null,
               It.IsAny<Func<It.IsAnyType, Exception, string>>()
               ));

            tourManagment = new TourManagment(tourStorageMock.Object,
                loggerMock.Object);
        }

        /// <summary>
        /// Добавление в хранилище
        /// </summary>
        [Fact]
        public async Task AddShouldWork()
        {
            // Arrange
            var model = new Tour
            {
                ID = Guid.NewGuid(),
                Direction = Direction.Turckey,
                DeparturDate = DateTime.Today,
                NumberNights = 1,
                CostVacationers = 1,
                NumberVacationers = 1,
                Surcharges = 1,
                WIFI = true,
            };

            tourStorageMock.Setup(x => x.AddAsync(It.IsAny<Tour>()))
                .ReturnsAsync(model);

            //Act
            var result = await tourManagment.AddAsync(model);

            //Assert
            result.Should().NotBeNull()
                        .And.BeEquivalentTo(model);

            tourStorageMock.Verify(x => x.AddAsync(It.Is<Tour>(y => y.Direction == model.Direction &&
                                                                     y.DeparturDate == model.DeparturDate &&
                                                                     y.NumberNights == model.NumberNights &&
                                                                     y.CostVacationers == model.CostVacationers &&
                                                                     y.NumberVacationers == model.NumberVacationers &&
                                                                     y.Surcharges == model.Surcharges &&
                                                                     y.WIFI == model.WIFI)),
                Times.Once());

            loggerMock.Verify(x => x.Log(LogLevel.Information,
                It.IsAny<EventId>(),
                It.IsAny<It.IsAnyType>(),
                null,
                It.IsAny<Func<It.IsAnyType, Exception, string>>()), Times.Exactly(2));

            tourStorageMock.VerifyNoOtherCalls();
        }


        /// <summary>
        /// Удаление из хранилища
        /// </summary>
        [Fact]
        public async Task DeleteAsyncShouldWork()
        {
            // Arrange
            var model = new Tour
            {
                ID = Guid.NewGuid(),
                Direction = Direction.Turckey,
                DeparturDate = DateTime.Today,
                NumberNights = 1,
                CostVacationers = 1,
                NumberVacationers = 1,
                Surcharges = 1,
                WIFI = true,
            };

            tourStorageMock.Setup(x => x.DeleteAsync(model.ID))
              .ReturnsAsync(true);

            //Act
            var result = await tourManagment.DeleteAsync(model.ID);

            //Assert
            result.Should().BeTrue();

            tourStorageMock.Verify(x => x.DeleteAsync(It.Is<Guid>(y => y == model.ID)),
                Times.Once());

            tourStorageMock.VerifyNoOtherCalls();
        }

        /// <summary>
        /// Изменение данных в хранилище
        /// </summary>
        [Fact]
        public async Task EditAsyncShouldWork()
        {
            // Arrange
            var model = new Tour
            {
                ID = Guid.NewGuid(),
                Direction = Direction.Turckey,
                DeparturDate = DateTime.Today,
                NumberNights = 1,
                CostVacationers = 1,
                NumberVacationers = 1,
                Surcharges = 1,
                WIFI = true,

            };

            tourStorageMock.Setup(x => x.EditAsync(It.IsAny<Tour>()))
                .Returns(Task.CompletedTask);

            //Act
            await tourManagment.EditAsync(model);

            //Assert
            loggerMock.Verify(x => x.Log(LogLevel.Information,
               It.IsAny<EventId>(),
               It.IsAny<It.IsAnyType>(),
               null,
               It.IsAny<Func<It.IsAnyType, Exception, string>>()), Times.Exactly(2));

            tourStorageMock.Verify(x => x.EditAsync(It.Is<Tour>(y => y.ID == model.ID)),
                Times.Once());

            tourStorageMock.VerifyNoOtherCalls();
        }

        /// <summary>
        /// Тест: Метод <see cref="TourManagment.GetAllAsync"/>
        /// </summary>
        [Fact]
        public async Task GetAllShouldWork()
        {
            var tours = new List<Tour>
            {
                new Tour {
                    ID = Guid.NewGuid(),
                    Direction = Direction.Turckey,
                    DeparturDate = DateTime.Today,
                    NumberNights = 5,
                    CostVacationers = 10000,
                    NumberVacationers = 1,
                    Surcharges = 500,
                    WIFI = true
                },
                new Tour {
                    ID = Guid.NewGuid(),
                    Direction = Direction.France,
                    DeparturDate = DateTime.Today,
                    NumberNights = 10,
                    CostVacationers = 15000,
                    NumberVacationers = 2,
                    Surcharges = 0,
                    WIFI = true
                },
                new Tour {
                    ID = Guid.NewGuid(),
                    Direction = Direction.Spain,
                    DeparturDate = DateTime.Today,
                    NumberNights = 3,
                    CostVacationers = 5000,
                    NumberVacationers = 4,
                    Surcharges = 0,
                    WIFI = true
                }
            };
            tourStorageMock.Setup(x => x.GetAllAsync()).ReturnsAsync(tours);

            //Act
            var result = await tourManagment.GetAllAsync();

            //Assert
            result.Should().NotBeNull()
                .And.HaveCount(tours.Count)
                .And.BeEquivalentTo(tours);

            tourStorageMock.Verify(x => x.GetAllAsync(), Times.Once());
            tourStorageMock.VerifyNoOtherCalls();
        }

        /// <summary>
        /// Получить 3 стандартных гвоздя из хранилища
        /// </summary>
        [Fact]
        public async Task GetStatsShouldWork()
        {
            var tours = new List<Tour>
            {
                new Tour {
                    ID = Guid.NewGuid(),
                    Direction = Direction.Turckey,
                    DeparturDate = DateTime.Today,
                    NumberNights = 5,
                    CostVacationers = 10000,
                    NumberVacationers = 1,
                    Surcharges = 500,
                    WIFI = true
                },
                new Tour {
                    ID = Guid.NewGuid(),
                    Direction = Direction.France,
                    DeparturDate = DateTime.Today,
                    NumberNights = 10,
                    CostVacationers = 15000,
                    NumberVacationers = 2,
                    Surcharges = 0,
                    WIFI = true
                },
                new Tour {
                    ID = Guid.NewGuid(),
                    Direction = Direction.Spain,
                    DeparturDate = DateTime.Today,
                    NumberNights = 3,
                    CostVacationers = 5000,
                    NumberVacationers = 4,
                    Surcharges = 0,
                    WIFI = true
                }
            };
            tourStorageMock.Setup(x => x.GetAllAsync())
                .ReturnsAsync(tours);

            // Act
            var result = await tourManagment.GetStatsAsync();

            // Assert
            result.Should().NotBeNull();


            tourStorageMock.Verify(x => x.GetAllAsync(), Times.Once);
            tourStorageMock.VerifyNoOtherCalls();

        }
    }
}
