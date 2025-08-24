using CityTouristSpots.Models;

public class TouristSpot
{
    public int TouristSpotId { get; set; }   // keep this, don't rename
    public string? SpotName { get; set; }
    public string? Location { get; set; }
    public string? Description { get; set; }
    public decimal? EntryFee { get; set; }    // ✅ add this

    public int CityId { get; set; }
    public City? City { get; set; }

    public int? CreatedBy { get; set; }
    public User? CreatedByUser { get; set; }
}
