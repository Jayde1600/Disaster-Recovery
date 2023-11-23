using DisastersRecovery.Controllers;
using DisastersRecovery.Data;
using DisastersRecovery.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Xunit;

namespace TestProject
{
    public class AvailableGoodsController_Delete
    {
        [Fact]
        public async Task Delete_ReturnsViewWithAvailableGoods()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb" + nameof(Delete_ReturnsViewWithAvailableGoods)) // Unique name for the database
                .Options;

            using var context = new ApplicationDbContext(options);
            var controller = new AvailableGoodsController(context);

            var availableGood = new AvailableGoods
            {
                Id = 1,
                CategoryId = 1,
                AvailableQuantity = 50,
                QuantityUsed = 20
                // Add other necessary properties for your AvailableGoods model
            };

            context.Add(availableGood);
            await context.SaveChangesAsync();

            // Act
            var result = await controller.DeleteConfirmed(1);

            // Assert
            Assert.IsType<RedirectToActionResult>(result); // Ensure the action redirects after deletion
        }
    }
}
