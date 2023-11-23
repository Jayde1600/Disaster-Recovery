using System;
using System.Threading.Tasks;
using DisastersRecovery.Controllers;
using DisastersRecovery.Data;
using DisastersRecovery.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace TestProject
{
    public class DisasterCheck_Details
    {
        [Fact]
        public async Task Details_ReturnsViewWithDisasterCheck()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Unique name for each test
                .Options;

            using var context = new ApplicationDbContext(options);

            // Add a sample DisasterCheck to the in-memory database
            var sampleDisasterCheck = new DisasterCheck
            {
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(5),
                Location = "Test Location",
                Description = "Test Description",
                AidType = "Test Aid Type"
                // Add other necessary properties with valid values
            };


            context.DisasterCheck.Add(sampleDisasterCheck);
            await context.SaveChangesAsync();

            var controller = new DisasterChecksController(context);

            // Act
            var result = await controller.Details(sampleDisasterCheck.Id) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Model); // Ensure there's a model returned
            Assert.IsType<DisasterCheck>(result.Model); // Ensure the model type is as expected

            // You can add more specific assertions based on your expectations for the Details view.
        }
    }
}
