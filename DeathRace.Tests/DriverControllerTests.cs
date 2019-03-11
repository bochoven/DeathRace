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
    public class DriverControllerTest
    {
        [Fact]
        public async Task GetAllDriversReturnsSuccessAndAListWithTwoEntries()
        {
            // Arrange
            var mockRepo = new Mock<IDriverRepository>();
            mockRepo.Setup(repo => repo.GetAllDrivers())
                .ReturnsAsync(GetTestDrivers());
            var controller = new DriverController(mockRepo.Object);

            // Act
            var actionResult = await controller.GetDrivers();

            // Assert
            Assert.NotNull(actionResult);
            var okObjectResult = actionResult.Result as OkObjectResult;
            Assert.NotNull(okObjectResult);
            var model = okObjectResult.Value as List<Driver>;
            Assert.NotNull(model);
            Assert.Equal(2, model.Count);
        }
        
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(10)]
        public void GetReturnsSuccesAndDriverWithSameId(int driverID)
        {
            // Arrange
            var mockRepo = new Mock<IDriverRepository>();
            mockRepo.Setup(repo => repo.GetById(driverID))
                .ReturnsAsync(GetTestDriver(driverID));
            var controller = new DriverController(mockRepo.Object);
        
            // Act
            var actionResult = controller.GetById(driverID);

            // Assert
            Assert.NotNull(actionResult);
            var okObjectResult = actionResult.Result as OkObjectResult;
            Assert.NotNull(okObjectResult);
            var model = okObjectResult.Value as Driver;
            Assert.NotNull(model);
            Assert.Equal(driverID, model.DriverId);
        }
        
        [Theory]
        [InlineData(1)]
        public void PostReturnsSucces(int driverID)
        {
            // Arrange
            var mockRepo = new Mock<IDriverRepository>();
            var driver = GetTestDriver(driverID);
            mockRepo.Setup(repo => repo.Add(driver));
            var controller = new DriverController(mockRepo.Object);
        
            // Act
            var createdResponse = controller.PostDriver(driver);

            // Assert
             Assert.IsType<Task<ActionResult<Driver>>>(createdResponse);
        }

        private List<Driver> GetTestDrivers()
        {
            var drivers = new List<Driver>();
            drivers.Add(new Driver()
            {
                DOB = new DateTime(2016, 7, 2),
                DriverId = 1,
                GivenName = "Test"
            });
            drivers.Add(new Driver()
            {
                DOB = new DateTime(2016, 7, 1),
                DriverId = 2,
                GivenName = "Test Two"
            });
            return drivers;
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
