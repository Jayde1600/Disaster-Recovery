using DisastersRecovery.Controllers;
using DisastersRecovery.Data;
using DisastersRecovery.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Xunit;

namespace TestProject
{
    public class AvailableMoneysController_Create
    {
        [Fact]
        public async Task Create_RedirectsToIndexOnSuccess()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;

            using var context = new ApplicationDbContext(options);
            var controller = new AvailableMoneysController(context);

            var availableMoney = new AvailableMoney
            {
                TotalAmount = 1000, // Set a test total amount
                AmountUsed = 200 // Set a test amount used
                // Set other properties as required for your model
            };

            // Act
            var result = await controller.Create(availableMoney) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName); // Ensure it redirects to the Index action after successful creation
        }
    }
}
