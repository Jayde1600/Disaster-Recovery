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
    public class DisasterCheckControllerTests
    {
        [Fact]
        public async Task Index_ReturnsViewWithDisasterChecks()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;

            using var context = new ApplicationDbContext(options);

            // Adding a sample DisasterCheck with required properties
            context.DisasterCheck.Add(new DisasterCheck
            {
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(5),
                Location = "Test Location",
                Description = "Test Description",
                AidType = "Test Aid Type" // Adding AidType as a required property
                // Add other necessary properties
            });

            context.SaveChanges();

            var controller = new DisasterChecksController(context);

            // Act
            var result = await controller.Index() as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Model); // Ensure there's a model returned
            Assert.IsType<List<DisasterCheck>>(result.Model); // Ensure the model type is as expected
        }
    }
}
