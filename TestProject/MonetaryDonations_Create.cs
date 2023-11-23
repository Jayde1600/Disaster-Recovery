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
    public class MonetaryDonations_Create
    {
        [Fact]
        public async Task Create_RedirectsToIndexOnSuccess()
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
                Amount = 100, // Provide an amount here
                IsAnonymous = false, // Specify whether it's anonymous
                DonorName = "John Doe" // Provide a donor name
            };

            // Act
            var result = await controller.Create(monetaryDonation) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result); // Ensure a redirect result is returned
            Assert.Equal("Index", result.ActionName); // Ensure it redirects to the Index action
        }
    }
}
