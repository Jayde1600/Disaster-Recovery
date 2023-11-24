using DisastersRecovery.Controllers;
using DisastersRecovery.Data;
using DisastersRecovery.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace TestProject
{
    public class AvailableMoneysController_Index
    {
        [Fact]
        public async Task Index_ReturnsViewWithListOfAvailableMoney()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb" + System.Guid.NewGuid()) // Ensure a unique database name
                .Options;

            using var context = new ApplicationDbContext(options);
            var controller = new AvailableMoneysController(context);

            var availableMoneys = new List<AvailableMoney>
            {
                // Your AvailableMoney instances
            };

            // Add the availableMoneys to the in-memory database
            await context.AddRangeAsync(availableMoneys);
            await context.SaveChangesAsync();

            // Act
            var result = await controller.Index() as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Model); // Ensure there's a model returned
            var model = result.Model as List<AvailableMoney>;
            Assert.Equal(availableMoneys.Count, model.Count); // Ensure the view contains the expected number of availableMoneys
        }
    }
}
