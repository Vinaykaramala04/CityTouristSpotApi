using CityTouristSpots.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CityTouristSpots.Configurations
{
    public class TouristSpotConfiguration : IEntityTypeConfiguration<TouristSpot>
    {
        public void Configure(EntityTypeBuilder<TouristSpot> builder)
        {
            builder.HasKey(ts => ts.TouristSpotId);


            builder.Property(ts => ts.SpotName)
                   .IsRequired()
                   .HasMaxLength(150);

            builder.Property(ts => ts.Description)
                   .HasMaxLength(500);

            builder.Property(ts => ts.Location)
                   .HasMaxLength(200);

            builder.Property(ts => ts.EntryFee)
                   .HasColumnType("decimal(10,2)");

            builder.HasOne(ts => ts.City)
                   .WithMany(c => c.TouristSpots)
                   .HasForeignKey(ts => ts.CityId);
        }
    }
}
