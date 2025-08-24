namespace CityTouristSpots.Models
{
    public class City
    {
        public int CityId { get; set; }
        public string? CityName { get; set; }
        public string? Country { get; set; }
        public string? Description { get; set; }

        // Foreign Key to User who created this record
        public int CreatedBy { get; set; }
        public User? CreatedByUser { get; set; }

        // Navigation property
        public ICollection<TouristSpot>? TouristSpots { get; set; }
    }
}
