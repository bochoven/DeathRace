using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeathRace.Controllers;
using DeathRace.Models;
using DeathRace.Repository;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace DeathRace.Tests
{
    public class CarControllerTest
    {
        [Theory]
        [InlineData(null, 2)]
        [InlineData(2002, 1)]
        public async Task GetAllCarsReturnsSuccessAndAListWithTwoEntries(int? startyear, int count)
        {
            // Arrange
            var mockRepo = new Mock<ICarRepository>();
            mockRepo.Setup(repo => repo.GetAllCars(startyear))
                .ReturnsAsync(GetTestCars(startyear));
            var mockDriverRepo = new Mock<IDriverRepository>();
            var controller = new CarController(mockRepo.Object, mockDriverRepo.Object);

            // Act
            var actionResult = await controller.GetCars(startyear);

            // Assert
            Assert.NotNull(actionResult);
            var okObjectResult = actionResult.Result as OkObjectResult;
            Assert.NotNull(okObjectResult);
            var model = okObjectResult.Value as List<Car>;
            Assert.NotNull(model);
            Assert.Equal(count, model.Count);
        }
        
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(10)]
        public void GetReturnsSuccesAndCarWithSameId(int carID)
        {
            // Arrange
            var mockRepo = new Mock<ICarRepository>();
            mockRepo.Setup(repo => repo.GetById(carID))
                .ReturnsAsync(GetTestCar(carID));
            var mockDriverRepo = new Mock<IDriverRepository>();
            var controller = new CarController(mockRepo.Object, mockDriverRepo.Object);
        
            // Act
            var actionResult = controller.GetById(carID);

            // Assert
            Assert.NotNull(actionResult);
            var okObjectResult = actionResult.Result as OkObjectResult;
            Assert.NotNull(okObjectResult);
            var model = okObjectResult.Value as Car;
            Assert.NotNull(model);
            Assert.Equal(carID, model.CarId);
        }
        
        [Theory]
        [InlineData(1)]
        public void PostReturnsSucces(int driverID)
        {
            // Arrange
            var mockRepo = new Mock<ICarRepository>();
            var driver = GetTestCar(driverID);
            mockRepo.Setup(repo => repo.Add(driver));
            var mockDriverRepo = new Mock<IDriverRepository>();
            var controller = new CarController(mockRepo.Object, mockDriverRepo.Object);
        
            // Act
            var createdResponse = controller.PostCar(driver);

            // Assert
             Assert.IsType<Task<ActionResult<Car>>>(createdResponse);
        }

        private List<Car> GetTestCars(int? startyear)
        {
            var cars = new List<Car>();
            if (startyear == null || startyear <= 2001)
            {
                cars.Add(new Car()
                {
                    CarId = 1,
                    Brand = "TestBrand 1",
                    Model = "TestModel 1",
                    Type = "TestType 1",
                    Year = 2001,
                    DriverId = 1
                });
            }
            if (startyear == null || startyear <= 2002)
            {
                cars.Add(new Car()
                {
                    CarId = 2,
                    Brand = "TestBrand 2",
                    Model = "TestModel 2",
                    Type = "TestType 2",
                    Year = 2002,
                    DriverId = 2
                });
            }
            return cars;
        }

        private Car GetTestCar(int id)
        {
            return new Car()
            {
                CarId = id,
                Brand = "TestBrand 1",
                Model = "TestModel 1",
                Type = "TestType 1",
                Year = 2001,
                DriverId = 1
            };
        }
        
        private Driver GetTestDriver(int id)
        {
            return new Driver()
            {
                DOB = new DateTime(2016, 7, 2),
                DriverId = id,
                GivenName = "Test"
            };
        }

    }

}
