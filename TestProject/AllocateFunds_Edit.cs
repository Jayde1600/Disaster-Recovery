using DisastersRecovery.Controllers;
using DisastersRecovery.Data;
using DisastersRecovery.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Xunit;

namespace TestProject
{
    public class AllocateFundsController_Edit
    {
        [Fact]
        public async Task Edit_WithValidAllocation_ReturnsRedirectToAction()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;

            using var context = new ApplicationDbContext(options);

            // Seed some mock data for allocated funds
            var allocateFunds = new AllocateFunds
            {
                Amount = 1500, // Set the initial allocation amount
                AllocationDate = DateTime.UtcNow,
                DisasterId = 1 // Set the DisasterId
            };

            context.AllocateFunds.Add(allocateFunds);
            await context.SaveChangesAsync();

            var controller = new AllocateFundsController(context);
            var updatedAllocateFunds = new AllocateFunds
            {
                Id = 1, // Set the ID of the allocated fund to update
                Amount = 2000, // Set the updated allocation amount
                AllocationDate = DateTime.UtcNow,
                DisasterId = 1 // Set the updated DisasterId
            };

            // Act
            var result = await controller.Edit(updatedAllocateFunds.Id, updatedAllocateFunds) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName); // Ensure redirection to Index action after successful update

            // Retrieve the updated allocated fund from the database after editing
            var editedAllocateFunds = await context.AllocateFunds.FindAsync(updatedAllocateFunds.Id);

            // Assert the changes in the allocated fund after editing
            Assert.Equal(updatedAllocateFunds.Amount, editedAllocateFunds.Amount); // Ensure the amount is updated correctly
            Assert.Equal(updatedAllocateFunds.DisasterId, editedAllocateFunds.DisasterId); // Ensure the DisasterId is updated correctly
        }
    }
}
