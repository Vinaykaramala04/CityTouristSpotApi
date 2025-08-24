namespace CityTouristSpots.Models
{
    public class UserType
    {
        public int? UserTypeId { get; set; }
        public string? TypeName { get; set; }

        // Navigation property
        public ICollection<User>? Users { get; set; }

    }
}
