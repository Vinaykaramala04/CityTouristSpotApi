namespace CityTouristSpots.DTOs
{
    public class CityDto
    {
        public int CityId { get; set; }
        public string CityName { get; set; } = null!;
        public string? Country { get; set; }
        public string? Description { get; set; }
        public int? CreatedBy { get; set; }   // Keep as int? if referencing UserId
    }
}
