using CityTouristSpots.DTOs;

public class UserDto
{
    public int UserId { get; set; }
    public string? UserName { get; set; } = string.Empty;
    public string? Email { get; set; } = string.Empty;

    // Only used for input, not returned in GET responses
    public string? Password { get; set; }

    public string? UserTypeName { get; set; } = string.Empty;
    public List<CityDto> CitiesCreated { get; set; } = new List<CityDto>();
    public List<TouristSpotDto> TouristSpotsCreated { get; set; } = new List<TouristSpotDto>();
    public string Role { get; internal set; }
}
