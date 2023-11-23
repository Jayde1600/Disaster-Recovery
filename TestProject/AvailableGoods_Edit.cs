using DisastersRecovery.Controllers;
using DisastersRecovery.Data;
using DisastersRecovery.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace TestProject
{
    public class AvailableGoodsController_Edit
    {
        [Fact]
        public async Task Edit_ReturnsViewWithAvailableGoods()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb" + nameof(Edit_ReturnsViewWithAvailableGoods)) // Unique name for the database
                .Options;

            using var context = new ApplicationDbContext(options);
            var controller = new AvailableGoodsController(context);

            var availableGoods = new List<AvailableGoods>
            {
                new AvailableGoods
                {
                    Id = 1,
                    CategoryId = 1,
                    AvailableQuantity = 50,
                    QuantityUsed = 20
                    // Other properties as needed for your AvailableGoods model
                },
                new AvailableGoods
                {
                    Id = 2,
                    CategoryId = 2,
                    AvailableQuantity = 100,
                    QuantityUsed = 30
                    // Other properties as needed for your AvailableGoods model
                },
                // Add more AvailableGoods objects as needed
            };

            context.AddRange(availableGoods);
            await context.SaveChangesAsync();

            // Act
            var result = await controller.Edit(1) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Model); // Ensure there's a model returned
            var model = result.Model as AvailableGoods;
            Assert.Equal(1, model.Id); // Ensure the correct AvailableGoods object is returned for editing
        }
    }
}
