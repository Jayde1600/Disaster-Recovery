using DisastersRecovery.Controllers;
using DisastersRecovery.Data;
using DisastersRecovery.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Xunit;

namespace TestProject
{
    public class CategoriesController_Delete
    {
        [Fact]
        public async Task Delete_ReturnsRedirectToAction()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "Categories_Delete_TestDb")
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
            var result = await controller.DeleteConfirmed(1) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName); // Check if redirected to Index action after successful deletion

            var deletedCategory = await context.Categories.FirstOrDefaultAsync();
            Assert.Null(deletedCategory); // Ensure the category was deleted successfully
        }
    }
}
