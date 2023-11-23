using DisastersRecovery.Controllers;
using DisastersRecovery.Data;
using DisastersRecovery.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Xunit;

namespace TestProject
{
    public class AvailableMoneysController_DeleteConfirmed
    {
        [Fact]
        public async Task DeleteConfirmed_RedirectsToIndexOnSuccess()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;

            using var context = new ApplicationDbContext(options);
            var controller = new AvailableMoneysController(context);

            var availableMoney = new AvailableMoney
            {
                // Set properties for the object you want to delete
                // Ensure the ID matches an object in the in-memory database
            };

            context.Add(availableMoney);
            await context.SaveChangesAsync();

            // Act
            var result = await controller.DeleteConfirmed(availableMoney.Id) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName); // Ensure it redirects to the Index action after successful deletion
        }
    }
}
