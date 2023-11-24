using DisastersRecovery.Controllers;
using DisastersRecovery.Data;
using DisastersRecovery.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace TestProject
{
    public class ActiveDisastersController_Index
    {
        [Fact]
        public async Task Index_ReturnsViewWithListOfActiveDisasters()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using var context = new ApplicationDbContext(options);

            var controller = new ActiveDisastersController(context);

            // Add mock data to the in-memory database
            // Add mock data to the in-memory database
            var activeDisasters = new List<DisasterCheck>
            {
                new DisasterCheck { Id = 1, Description = "Disaster 1", Location = "Location 1", EndDate = DateTime.UtcNow.AddDays(5), AidType = "Type A" },
                new DisasterCheck { Id = 2, Description = "Disaster 2", Location = "Location 2", EndDate = DateTime.UtcNow.AddDays(10), AidType = "Type B" }
                // Add more mock data as needed, ensuring all required properties are set
            };

            await context.DisasterCheck.AddRangeAsync(activeDisasters);
            await context.SaveChangesAsync();


            // Act
            var result = await controller.Index() as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Model); // Ensure there's a model returned
            var model = result.Model as List<ActiveDisasters>;
            Assert.Equal(activeDisasters.Count, model?.Count); // Ensure the view contains the expected number of ActiveDisasters
        }
    }
}
