using DisastersRecovery.Controllers;
using DisastersRecovery.Data;
using DisastersRecovery.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace TestProject
{
    public class AvailableGoodsController_Create
    {
        [Fact]
        public void Create_ReturnsViewWithAvailableGoods()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb" + nameof(Create_ReturnsViewWithAvailableGoods)) // Unique name for the database
                .Options;

            using var context = new ApplicationDbContext(options);
            var controller = new AvailableGoodsController(context);

            // Act
            var result = controller.Create();

            // Assert
            Assert.IsType<ViewResult>(result); // Ensure the action returns a ViewResult
        }
    }
}
