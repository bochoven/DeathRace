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
        // DriverController _controller;
        // private readonly Mock<DriverRepository> DriverRepo;
        //
        // public DriverControllerTest()
        // {
        //     DriverRepo = new Mock<IDriverRepository>();
        //     _controller = new DriverController(DriverRepo);
        // }

        [Fact]
        public async Task GetAllDriversReturnsAListWithTwoEntries()
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



        // [Fact]
        // public void Get_WhenCalled_ReturnsOkResult()
        // {
        //     // Act
        //     var okResult = _controller.Get();
        //
        //     // Assert
        //     Assert.IsType<OkObjectResult>(okResult.Result);
        // }
        //
        // [Fact]
        // public void Get_WhenCalled_ReturnsAllItems()
        // {
        //     // Act
        //     var okResult = _controller.GetAllDrivers().Result as OkObjectResult;
        //
        //     // Assert
        //     var items = Assert.IsType<List<ShoppingItem>>(okResult.Value);
        //     Assert.Equal(3, items.Count);
        // }
        //
        // [Fact]
        // public void GetReturnsDriverWithSameId()
        // {
        //     // Arrange
        //     Mock<DriverRepository> mockRepository = new Mock<DriverRepository>();
        //     mockRepository.Setup(x => x.GetById(42))
        //         .Returns(new Driver { DriverId = 42 });
        //
        //     DriverController controller = new DriverController(mockRepository.Object);
        //
        //     // Act
        //     IHttpActionResult actionResult = controller.Get(42);
        //     var contentResult = actionResult as OkNegotiatedContentResult<Product>;
        //
        //     // Assert
        //     Assert.NotNull(contentResult);
        //     Assert.NotNull(contentResult.Content);
        //     Assert.AreEqual(42, contentResult.Content.Id);
        // }

    }

}
