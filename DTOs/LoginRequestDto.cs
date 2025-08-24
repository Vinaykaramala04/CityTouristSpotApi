namespace CityTouristSpots.DTOs
{
    public class LoginRequestDto
    {
        public string? Email { get; set; }     // Nullable to avoid null issues
        public string? Password { get; set; }
    }
}