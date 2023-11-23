using DisastersRecovery.Controllers;
using DisastersRecovery.Data;
using DisastersRecovery.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Xunit;

namespace TestProject
{
    public class CategoriesController_Create
    {
        [Fact]
        public async Task Create_ReturnsViewWithCategory()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "Categories_Create_TestDb")
                .Options;

            using var context = new ApplicationDbContext(options);
            var controller = new CategoriesController(context);

            var category = new Categories
            {
                Id = 1,
                CategoryName = "Test Category",
                // Set other required properties 
            };

            // Act
            var result = await controller.Create(category) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName); // Check if redirected to Index action after successful creation

            var createdCategory = await context.Categories.FirstOrDefaultAsync();
            Assert.NotNull(createdCategory);
            Assert.Equal("Test Category", createdCategory.CategoryName); // Check if the category was created successfully
        }
    }
}
