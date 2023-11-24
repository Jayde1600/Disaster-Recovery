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
    public class AllocateFundsController_Create
    {
        [Fact]
        public async Task Create_WithValidAllocation_ReturnsRedirectToAction()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Unique database name for each test run
                .Options;

            using var context = new ApplicationDbContext(options);

            // Create and seed the required data
            var availableMoney = new AvailableMoney
            {
                TotalAmount = 5000,
                AmountUsed = 2000
            };

            context.AvailableMoney.Add(availableMoney);
            await context.SaveChangesAsync();

            var controller = new AllocateFundsController(context);
            var allocateFunds = new AllocateFunds
            {
                Amount = 1500,
                AllocationDate = DateTime.UtcNow,
                DisasterId = 1
            };

            // Act
            var result = await controller.Create(allocateFunds) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);

            // Retrieve the updated available money from the database after allocation
            var updatedAvailableMoney = await context.AvailableMoney.FirstAsync();

            // Assert the changes in the available funds after the allocation
            Assert.Equal(3500, updatedAvailableMoney.TotalAmount);
            Assert.Equal(3500, updatedAvailableMoney.AmountUsed);
        }
    }
}