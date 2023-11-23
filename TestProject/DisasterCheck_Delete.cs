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
    public class DisasterCheck_Delete
    {
        [Fact]
        public async Task Delete_ReturnsViewWithDisasterCheckForValidId()
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
            var result = await controller.Delete(disasterCheck.Id) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Model); // Ensure there's a model returned
            Assert.IsType<DisasterCheck>(result.Model); // Ensure the model type is as expected

            var model = result.Model as DisasterCheck;
            Assert.Equal(disasterCheck.Id, model.Id); // Ensure the IDs match for the retrieved and expected models
            // Add additional assertions comparing other properties if needed
        }
    }
}
