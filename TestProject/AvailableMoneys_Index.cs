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
    public class AvailableMoneysController_Index
    {
        [Fact]
        public async Task Index_ReturnsViewWithListOfAvailableMoney()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;

            using var context = new ApplicationDbContext(options);
            var controller = new AvailableMoneysController(context);

            var availableMoneys = new List<AvailableMoney>
        {
            new AvailableMoney
            {
                TotalAmount = 1000, // Set the total amount for the first object
                AmountUsed = 500 // Set the used amount for the first object
                // Other properties as needed for your AvailableMoney model
            },
            new AvailableMoney
            {
                TotalAmount = 2000, // Set the total amount for the second object
                AmountUsed = 1000 // Set the used amount for the second object
                // Other properties as needed for your AvailableMoney model
            },
            // Add more AvailableMoney objects as needed
        };

                // Add the availableMoneys to the in-memory database
                await context.AddRangeAsync(availableMoneys);
                await context.SaveChangesAsync();

                // Act
                var result = await controller.Index() as ViewResult;

                // Assert
                Assert.NotNull(result);
                Assert.NotNull(result.Model); // Ensure there's a model returned
                var model = result.Model as List<AvailableMoney>;
                Assert.Equal(availableMoneys.Count, model.Count); // Ensure the view contains the expected number of availableMoneys
            }
        }
    }
