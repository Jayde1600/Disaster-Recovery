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
        public DbSet<DisastersRecovery.Models.MonetaryDonation>? MonetaryDonation { get; set; }
        public DbSet<DisastersRecovery.Models.Categories>? Categories { get; set; }
        public DbSet<DisastersRecovery.Models.GoodsDonation>? GoodsDonation { get; set; }
        public DbSet<DisastersRecovery.Models.DisasterCheck>? DisasterCheck { get; set; }
        public DbSet<DisastersRecovery.Models.AllocateFunds>? AllocateFunds { get; set; }
        public DbSet<DisastersRecovery.Models.AllocateGoods>? AllocateGoods { get; set; }
        public DbSet<DisastersRecovery.Models.PurchaseGoods>? PurchaseGoods { get; set; }
        public DbSet<DisastersRecovery.Models.AvailableGoods>? AvailableGoods { get; set; }
        public DbSet<DisastersRecovery.Models.AvailableMoney>? AvailableMoney { get; set; }
        public DbSet<DisastersRecovery.Models.ActiveDisasters>? ActiveDisasters { get; set; }
    }
}