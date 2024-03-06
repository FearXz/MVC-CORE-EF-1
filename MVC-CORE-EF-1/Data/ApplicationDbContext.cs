using Microsoft.EntityFrameworkCore;
using MVC_CORE_EF_1.Models;

namespace MVC_CORE_EF_1.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Shipping> Shippings { get; set; }
        public DbSet<ShippingDetail> ShippingDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();
        }
    }

}
