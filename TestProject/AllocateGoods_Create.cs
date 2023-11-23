using DisastersRecovery.Controllers;
using DisastersRecovery.Data;
using DisastersRecovery.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Xunit;

namespace TestProject
{
    public class AllocateGoodsController_Create
    {
        [Fact]
        public async Task Create_WithValidAllocation_ReturnsRedirectToAction()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb" + nameof(Create_WithValidAllocation_ReturnsRedirectToAction)) // Unique name for the database
                .Options;

            using var context = new ApplicationDbContext(options);
            var controller = new AllocateGoodsController(context);

            var availableGoods = new AvailableGoods
            {
                Id = 1,
                CategoryId = 1,
                AvailableQuantity = 100,
                QuantityUsed = 20
                // Other properties as needed for AvailableGoods
            };

            context.Add(availableGoods);
            await context.SaveChangesAsync();

            var allocation = new AllocateGoods
            {
                Quantity = 15,
                CategoryId = 1,
                DisasterId = 1,
                AllocationDate = DateTime.Now // Set allocation date as needed
                // Other properties as needed for AllocateGoods
            };

            // Act
            var result = await controller.Create(allocation);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }
    }
}
