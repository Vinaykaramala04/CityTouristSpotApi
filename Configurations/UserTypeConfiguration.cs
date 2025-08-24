using CityTouristSpots.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CityTouristSpots.Configurations
{
    public class UserTypeConfiguration : IEntityTypeConfiguration<UserType>
    {
        public void Configure(EntityTypeBuilder<UserType> builder)
        {
            builder.HasKey(ut => ut.UserTypeId);

            builder.Property(ut => ut.TypeName)
                   .IsRequired()
                   .HasMaxLength(50);
        }
    }
}
