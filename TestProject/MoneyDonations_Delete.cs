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
    public class MoneyDonations_Delete
    {
        [Fact]
        public async Task DeleteConfirmed_RedirectsToIndexOnSuccess()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;

            using var context = new ApplicationDbContext(options);
            var controller = new MonetaryDonationsController(context);

            var monetaryDonation = new MonetaryDonation
            {
                DonationDate = DateTime.Now,
                Amount = 100,
                IsAnonymous = false,
                DonorName = "John Doe" // Add necessary properties for the deletion
            };

            context.MonetaryDonation.Add(monetaryDonation);
            await context.SaveChangesAsync();

            // Act
            var result = await controller.DeleteConfirmed(monetaryDonation.Id) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result); // Ensure a redirect result is returned
            Assert.Equal("Index", result.ActionName); // Ensure it redirects to the Index action
        }
    }
}
