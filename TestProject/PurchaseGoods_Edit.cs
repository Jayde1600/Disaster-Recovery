using DisastersRecovery.Controllers;
using DisastersRecovery.Data;
using DisastersRecovery.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Xunit;

namespace TestProject
{
    public class PurchaseGoodsController_Edit
    {
        [Fact]
        public async Task Edit_WithValidId_ReturnsViewResult()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb_PurchaseGoods")
                .Options;

            using var context = new ApplicationDbContext(options);
            var controller = new PurchaseGoodsController(context);

            var purchaseGood = new PurchaseGoods
            {
                Id = 1,
                ItemName = "Item 1",
                Quantity = 10
                // Add other necessary properties
            };

            await context.PurchaseGoods.AddAsync(purchaseGood);
            await context.SaveChangesAsync();

            // Act
            var result = await controller.Edit(1) as ViewResult;
            var model = result?.Model as PurchaseGoods;

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(model);
            Assert.Equal("Item 1", model.ItemName); // Check if the retrieved model is the expected one
        }
    }
}
