using DisastersRecovery.Controllers;
using DisastersRecovery.Data;
using DisastersRecovery.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using Xunit;

namespace TestProject
{
    /** UNIT TEST FOR CREATE DISASTERCHECK */
    public class DisasterCheck_Create
    {
        [Fact]
        public async Task Create_DisasterCheck_ReturnsRedirectToActionResult()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;

            using (var context = new ApplicationDbContext(options))
            {
                var controller = new DisasterChecksController(context);

                var newDisasterCheck = new DisasterCheck
                {
                    // Populate your new disaster check object
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now.AddDays(5),
                    Location = "Test Location",
                    Description = "Test Description",
                    AidType = "Test Aid Type"
                    // Add other necessary properties
                };

                // Act
                var result = await controller.Create(newDisasterCheck) as RedirectToActionResult;

                // Assert
                Assert.NotNull(result);
                Assert.Equal("Index", result.ActionName); // Verify if it redirects to Index action
            }
        }
    }
}
