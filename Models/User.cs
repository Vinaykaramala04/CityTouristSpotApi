using CityTouristSpots.Models;

public class User
{
    public int UserId { get; set; }
    public string? UserName { get; set; }   // ✅ rename from Username
    public string? Email { get; set; }
    public string? PasswordHash { get; set; }  // ✅ add this

    public int UserTypeId { get; set; }
    public UserType? UserType { get; set; }
    public ICollection<City>? CitiesCreated { get; set; }
    public ICollection<TouristSpot>? TouristSpotsCreated { get; set; }
}
