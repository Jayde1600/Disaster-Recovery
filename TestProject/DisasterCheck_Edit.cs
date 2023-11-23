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
    public class DisasterCheck_Edit
    {
        [Fact]
        public async Task Edit_ReturnsNotFoundForInvalidId()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;

            using var context = new ApplicationDbContext(options);

            var controller = new DisasterChecksController(context);

            // Act
            var result = await controller.Edit(999) as NotFoundResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(404, result.StatusCode);
        }

        [Fact]
        public async Task Edit_ReturnsViewWithDisasterCheckForValidId()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;

            using var context = new ApplicationDbContext(options);

            // Adding a sample DisasterCheck to the in-memory database
            var disasterCheck = new DisasterCheck
            {
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(5),
                Location = "Test Location",
                Description = "Test Description",
                AidType = "Test Aid Type"
                // Add other necessary properties
            };

            context.DisasterCheck.Add(disasterCheck);
            context.SaveChanges();

            var controller = new DisasterChecksController(context);

            // Act
            var result = await controller.Edit(disasterCheck.Id) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Model); // Ensure there's a model returned
            Assert.IsType<DisasterCheck>(result.Model); // Ensure the model type is as expected
        }
    }
}
