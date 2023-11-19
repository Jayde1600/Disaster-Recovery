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
    }
}