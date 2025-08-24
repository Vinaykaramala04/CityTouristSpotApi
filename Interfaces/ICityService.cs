using CityTouristSpots.DTOs;

namespace CityTouristSpots.Interfaces
{
    public interface ICityService
    {
        Task<IEnumerable<CityDto>> GetAllCitiesAsync();              // Get all cities
        Task<CityDto> GetCityByIdAsync(int id);                     // Get city by ID
        Task<CityDto> CreateCityAsync(CityDto dto);                 // Create a new city
        Task UpdateCityAsync(int id, CityDto dto);                  // Update city by ID
        Task DeleteCityAsync(int id);                               // Delete city by ID
    }
}
