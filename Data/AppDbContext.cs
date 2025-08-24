using CityTouristSpots.Models;
using Microsoft.EntityFrameworkCore;

namespace CityTouristSpots.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // DbSets
        public DbSet<User> Users { get; set; }
        public DbSet<UserType> UserTypes { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<TouristSpot> TouristSpots { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // UserTypes → Users
            modelBuilder.Entity<User>()
                .HasOne(u => u.UserType)
                .WithMany(ut => ut.Users)
                .HasForeignKey(u => u.UserTypeId)
                .OnDelete(DeleteBehavior.Cascade);

            // Users → Cities (CreatedBy)
            modelBuilder.Entity<City>()
                .HasOne(c => c.CreatedByUser)
                .WithMany()
                .HasForeignKey(c => c.CreatedBy)
                .OnDelete(DeleteBehavior.Restrict);

            // Cities → TouristSpots
            modelBuilder.Entity<TouristSpot>()
                .HasOne(ts => ts.City)
                .WithMany(c => c.TouristSpots)
                .HasForeignKey(ts => ts.CityId)
                .OnDelete(DeleteBehavior.Cascade);

            // Users → TouristSpots (CreatedBy)
            modelBuilder.Entity<TouristSpot>()
                .HasOne(ts => ts.CreatedByUser)
                .WithMany()
                .HasForeignKey(ts => ts.CreatedBy)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
