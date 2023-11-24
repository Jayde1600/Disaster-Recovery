using DisastersRecovery.Controllers;
using DisastersRecovery.Data;
using DisastersRecovery.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using Xunit;

namespace TestProject
{
    public class AllocateGoodsController_Delete
    {
        [Fact]
        public async Task DeleteConfirmed_DeletesAllocateGoods()
        {
            // Arrange
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "Test_Db")
                .UseInternalServiceProvider(serviceProvider)
                .Options;

            using (var context = new ApplicationDbContext(options))
            {
                var allocateGoods = new AllocateGoods
                {
                    Id = 1,
                    // Set other properties as needed for testing
                };

                context.AllocateGoods.Add(allocateGoods);
                context.SaveChanges();
            }

            using (var context = new ApplicationDbContext(options))
            {
                var controller = new AllocateGoodsController(context);

                // Act
                var result = await controller.DeleteConfirmed(1);

                // Assert
                var deletedAllocateGoods = await context.AllocateGoods.FindAsync(1);
                Assert.Null(deletedAllocateGoods); // Assert that the allocateGoods with ID 1 is deleted
            }
        }
    }
}
