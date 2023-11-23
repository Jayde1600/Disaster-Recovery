using DisastersRecovery.Controllers;
using DisastersRecovery.Data;
using DisastersRecovery.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Xunit;

namespace TestProject
{
    public class GoodsDonationsController_Edit
    {
        [Fact]
        public async Task Edit_ReturnsViewWithGoodsDonation()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "Edit_ReturnsView_TestDb")
                .Options;

            using var context = new ApplicationDbContext(options);

            var controller = new GoodsDonationsController(context);

            var goodsDonation = new GoodsDonation
            {
                Id = 1,
                Description = "Initial Description",
                DonorName = "John Doe",
                // Set other required properties 
            };

            context.Add(goodsDonation);
            await context.SaveChangesAsync();

            // Act
            var result = await controller.Edit(1) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Model);
            Assert.IsType<GoodsDonation>(result.Model);
        }

        [Fact]
        public async Task Edit_ReturnsNotFoundForInvalidId()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "Edit_ReturnsNotFound_TestDb")
                .Options;

            using var context = new ApplicationDbContext(options);

            var controller = new GoodsDonationsController(context);

            // Act
            var result = await controller.Edit(999) as NotFoundResult;

            // Assert
            Assert.NotNull(result);
        }
    }
}
