using CityTouristSpots.DTOs;

public interface ICityService
{
    Task<IEnumerable<CityDto>> GetAllCitiesAsync();
    Task<CityDto> GetCityByIdAsync(int id);
    Task<CityDto> CreateCityAsync(CityDto dto);
    Task UpdateCityAsync(int id, CityDto dto);
    Task DeleteCityAsync(int id);

    // ➕ New methods
    Task<int> GetCitiesCountAsync();                      // Get total number of cities
    Task<IEnumerable<CityDto>> SearchCitiesAsync(string keyword); // Search cities by keyword
}
