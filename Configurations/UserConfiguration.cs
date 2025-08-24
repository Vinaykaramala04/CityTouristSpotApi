using CityTouristSpots.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CityTouristSpots.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            // Primary Key
            builder.HasKey(u => u.UserId);

            // Required fields
            builder.Property(u => u.UserName)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(u => u.Email)
                   .IsRequired()
                   .HasMaxLength(150);

            builder.Property(u => u.PasswordHash)
                   .IsRequired();

            // Relationship: User → UserType (Many-to-One)
            builder.HasOne(u => u.UserType)      // each User has one UserType
                   .WithMany(ut => ut.Users)    // UserType has many Users
                   .HasForeignKey(u => u.UserTypeId)
                   .OnDelete(DeleteBehavior.Restrict); // prevent cascade delete
        }
    }
}
