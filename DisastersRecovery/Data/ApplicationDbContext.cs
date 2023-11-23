using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using DisastersRecovery.Models;

namespace DisastersRecovery.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, ILoggerFactory loggerFactory)
            : base(options)
        {
            // Use loggerFactory for logging purposes within the context
        }


        public DbSet<MonetaryDonation> MonetaryDonation { get; set; }
        public DbSet<Categories> Categories { get; set; }
        public DbSet<GoodsDonation> GoodsDonation { get; set; }
        public DbSet<DisasterCheck> DisasterCheck { get; set; }
        public DbSet<AllocateFunds> AllocateFunds { get; set; }
        public DbSet<AllocateGoods> AllocateGoods { get; set; }
        public DbSet<PurchaseGoods> PurchaseGoods { get; set; }
        public DbSet<AvailableGoods> AvailableGoods { get; set; }
        public DbSet<AvailableMoney> AvailableMoney { get; set; }
        public DbSet<ActiveDisasters> ActiveDisasters { get; set; }

        // Additional DbSet properties as needed for your application
    }
}
