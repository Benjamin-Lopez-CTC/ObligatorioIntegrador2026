using Microsoft.EntityFrameworkCore;
using ObligatorioIntegrador2026.Models;

namespace ObligatorioIntegrador2026.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<LoginRecord> LoginRecords { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed inicial de usuarios
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Username = "b.lopez", Password = "Admin123!", FullName = "Benjamin Lopez", Role = "Admin" },
                new User { Id = 2, Username = "f.alvarez", Password = "Admin123!", FullName = "Felipe Alvarez", Role = "Admin" },
                new User { Id = 3, Username = "m.vergues", Password = "Matias123!", FullName = "Matías Vergues", Role = "Beekeeper" }
            );
        }
    }
}
