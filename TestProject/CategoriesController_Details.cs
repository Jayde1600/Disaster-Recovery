using DisastersRecovery.Controllers;
using DisastersRecovery.Data;
using DisastersRecovery.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Xunit;

namespace TestProject
{
    public class CategoriesController_Details
    {
        [Fact]
        public async Task Details_ReturnsViewWithCategory()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "Categories_Details_TestDb")
                .Options;

            using var context = new ApplicationDbContext(options);
            var controller = new CategoriesController(context);

            var category = new Categories
            {
                Id = 1,
                CategoryName = "Test Category",
                // Set other required properties 
            };

            context.Add(category);
            await context.SaveChangesAsync();

            // Act
            var result = await controller.Details(1) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Model); // Ensure there's a model returned
            Assert.IsType<Categories>(result.Model); // Ensure the model type is as expected
        }
    }
}
