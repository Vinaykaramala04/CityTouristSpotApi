using CityTouristSpots.DTOs;

namespace CityTouristSpots.Interfaces
{
    public interface ITouristSpotService
    {
        Task<IEnumerable<TouristSpotDto>> GetAllTouristSpotsAsync();      // Get all spots
        Task<TouristSpotDto> GetTouristSpotByIdAsync(int id);             // Get spot by ID
        Task<TouristSpotDto> CreateTouristSpotAsync(TouristSpotDto dto);  // Create a new spot
        Task UpdateTouristSpotAsync(int id, TouristSpotDto dto);          // Update spot by ID
        Task DeleteTouristSpotAsync(int id);                               // Delete spot by ID
    }
}
