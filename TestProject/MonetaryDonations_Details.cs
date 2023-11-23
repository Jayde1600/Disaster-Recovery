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
    public class MonetaryDonationsController_Details
    {
        [Fact]
        public async Task Details_ReturnsViewWithMonetaryDonation()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;

            using var context = new ApplicationDbContext(options);

            // Adding a sample MonetaryDonation to the in-memory database
            var monetaryDonation = new MonetaryDonation
            {
                DonationDate = DateTime.Now,
                Amount = 100,
                IsAnonymous = false,
                DonorName = "Donor 1"
            };

            context.MonetaryDonation.Add(monetaryDonation);
            await context.SaveChangesAsync();

            var controller = new MonetaryDonationsController(context);

            // Act
            var result = await controller.Details(monetaryDonation.Id) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Model); // Ensure there's a model returned
            Assert.IsType<MonetaryDonation>(result.Model); // Ensure the model type is as expected
            var model = result.Model as MonetaryDonation;
            Assert.Equal(monetaryDonation.Id, model.Id); // Ensure that the correct donation is retrieved
        }

        [Fact]
        public async Task Details_ReturnsNotFoundForInvalidId()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;

            using var context = new ApplicationDbContext(options);
            var controller = new MonetaryDonationsController(context);

            // Act
            var result = await controller.Details(999) as NotFoundResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(404, result.StatusCode); // Ensure a 404 status code is returned for invalid ID
        }
    }
}
