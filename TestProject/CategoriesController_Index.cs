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
    public class CategoriesController_Index
    {
        [Fact]
        public async Task Index_ReturnsViewWithCategoriesList()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "Categories_Index_TestDb")
                .Options;

            using var context = new ApplicationDbContext(options);
            var controller = new CategoriesController(context);

            var categories = new List<Categories>
            {
                new Categories { Id = 1, CategoryName = "Category 1" },
                new Categories { Id = 2, CategoryName = "Category 2" },
                // Add more categories as needed
            };

            context.Categories.AddRange(categories);
            await context.SaveChangesAsync();

            // Act
            var result = await controller.Index() as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Model); // Ensure there's a model returned
            Assert.IsType<List<Categories>>(result.Model); // Ensure the model type is as expected
            var model = result.Model as List<Categories>;
            Assert.Equal(categories.Count, model?.Count); // Ensure the view contains the expected number of categories
        }
    }
}
