namespace CityTouristSpots.DTOs
{
    public class TouristSpotDto
    {
        public int TouristSpotId { get; set; }
        public string SpotName { get; set; } = null!;
        public string? Location { get; set; }
        public string? Description { get; set; }
        public decimal? EntryFee { get; set; }
        public int? CityId { get; set; }       // Keep as int? to match model
        public int? CreatedBy { get; set; }    // Keep as int? to match model
    }
}
