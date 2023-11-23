using DisastersRecovery.Controllers;
using DisastersRecovery.Data;
using DisastersRecovery.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Xunit;

namespace TestProject
{
    public class AvailableMoneysController_Edit
    {
        [Fact]
        public async Task Edit_ReturnsViewWithAvailableMoney()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;

            using var context = new ApplicationDbContext(options);
            var controller = new AvailableMoneysController(context);

            var availableMoney = new AvailableMoney
            {
                // Set properties for the object you want to edit
                // Ensure the ID matches an object in the in-memory database
            };

            context.Add(availableMoney);
            await context.SaveChangesAsync();

            // Act
            var result = await controller.Edit(availableMoney.Id) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<AvailableMoney>(result.Model); // Ensure the model returned is of type AvailableMoney
        }
    }
}
