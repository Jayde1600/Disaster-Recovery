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
    public class AllocateGoodsController_Edit
    {
        [Fact]
        public async Task Edit_WithValidAllocation_ReturnsRedirectToAction()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb" + nameof(Edit_WithValidAllocation_ReturnsRedirectToAction)) // Unique name for the database
                .Options;

            using var context = new ApplicationDbContext(options);
            var controller = new AllocateGoodsController(context);

            var existingAllocation = new AllocateGoods
            {
                Id = 1,
                Quantity = 15,
                CategoryId = 1,
                DisasterId = 1,
                AllocationDate = DateTime.Now // Set allocation date as needed
                // Other properties as needed for AllocateGoods
            };

            context.Add(existingAllocation);
            await context.SaveChangesAsync();

            var updatedAllocation = new AllocateGoods
            {
                Id = 1,
                Quantity = 10,
                CategoryId = 1,
                DisasterId = 1,
                AllocationDate = DateTime.Now // Set updated allocation date as needed
                // Other properties as needed for AllocateGoods
            };

            // Act
            var result = await controller.Edit(1, updatedAllocation);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }
    }
}
