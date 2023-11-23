using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DisastersRecovery.Controllers;
using DisastersRecovery.Data;
using DisastersRecovery.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace TestProject
{
    public class MonetaryDonationsController_Index
    {
        [Fact]
        public async Task Index_ReturnsViewWithMonetaryDonations()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Unique name for each test
                .Options;

            using (var context = new ApplicationDbContext(options))
            {
                // Adding sample MonetaryDonations to the in-memory database
                var monetaryDonations = new List<MonetaryDonation>
                {
                    new MonetaryDonation { DonationDate = DateTime.Now, Amount = 100, IsAnonymous = false, DonorName = "Donor 1" },
                    new MonetaryDonation { DonationDate = DateTime.Now, Amount = 200, IsAnonymous = true, DonorName = "Anonymous Donor" },
                };

                context.AddRange(monetaryDonations);
                await context.SaveChangesAsync();
            }

            using (var context = new ApplicationDbContext(options))
            {
                var controller = new MonetaryDonationsController(context);

                // Act
                var result = await controller.Index() as ViewResult;

                // Assert
                Assert.NotNull(result);
                Assert.NotNull(result.Model); // Ensure there's a model returned
                Assert.IsType<List<MonetaryDonation>>(result.Model); // Ensure the model type is as expected
                var model = result.Model as List<MonetaryDonation>;
                Assert.Equal(2, model.Count); // Ensure that the view contains the expected number of donations

                // Optional: Output the actual data to check for any unexpected records
                if (model != null)
                {
                    foreach (var donation in model)
                    {
                        Console.WriteLine($"Donation ID: {donation.Id}, Donation Amount: {donation.Amount}");
                    }
                }
            }
        }
    }
}
