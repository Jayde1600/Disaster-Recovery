using DisastersRecovery.Controllers;
using DisastersRecovery.Data;
using DisastersRecovery.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Xunit;

namespace TestProject
{
    public class GoodsDonationsController_Delete
    {
        [Fact]
        public async Task DeleteConfirmed_RedirectsToIndexOnSuccess()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;

            using var context = new ApplicationDbContext(options);

            var controller = new GoodsDonationsController(context);

            var goodsDonation = new GoodsDonation
            {
                // Initialize a goods donation for deletion
                // Make sure to have an existing ID for deletion
                DonationDate = DateTime.Now,
                NumberOfItems = 5,
                Description = "Test Donation",
                IsAnonymous = false,
                DonorName = "John Doe",
                CategoryId = 1 // Ensure this ID exists in your context
            };

            context.GoodsDonation.Add(goodsDonation);
            await context.SaveChangesAsync();

            // Act
            var result = await controller.DeleteConfirmed(goodsDonation.Id) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result); // Ensure a redirect result is returned
            Assert.Equal("Index", result.ActionName); // Ensure it redirects to the Index action
        }
    }
}
