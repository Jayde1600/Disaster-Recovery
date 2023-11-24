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
    public class PurchaseGoodsController_Create
    {
        [Fact]
        public async Task Create_WithValidPurchase_ReturnsRedirectToAction()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb_PurchaseGoods")
                .Options;

            using var context = new ApplicationDbContext(options);

            // Clearing the database before each test run
            context.Database.EnsureDeleted();

            // Seed some mock data for available money and categories
            var availableMoney = new AvailableMoney
            {
                TotalAmount = 5000,
                AmountUsed = 2000
            };

            var category = new Categories { Id = 1, CategoryName = "TestCategory" };
            context.AvailableMoney.Add(availableMoney);
            context.Categories.Add(category);
            await context.SaveChangesAsync();

            var controller = new PurchaseGoodsController(context);
            var purchaseGoods = new PurchaseGoods
            {
                ItemName = "TestItem",
                Quantity = 5,
                AmountUsed = 250,
                CategoryId = 1 // Ensure CategoryId matches the seeded category
            };

            // Act
            var result = await controller.Create(purchaseGoods) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName); // Ensure redirection to Index action after successful creation

            // Retrieve the updated available money from the database after purchase
            var updatedAvailableMoney = await context.AvailableMoney.FirstAsync();

            // Assert the changes in the available funds after the purchase
            Assert.Equal(4750, updatedAvailableMoney.TotalAmount); // Ensure the total amount deduction is accurate
            Assert.Equal(2250, updatedAvailableMoney.AmountUsed); // Ensure the amount used is updated correctly
        }
    }
}
