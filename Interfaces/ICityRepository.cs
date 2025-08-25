using CityTouristSpots.Models;

public interface ICityRepository
{
    Task<IEnumerable<City>> GetAllAsync();
    Task<City> GetByIdAsync(int id);
    Task<City> CreateAsync(City city);
    Task UpdateAsync(City city);
    Task DeleteAsync(int id);

    // ➕ New methods
    Task<IEnumerable<City>> SearchAsync(string keyword);  // Search cities
    Task<int> CountAsync();                               // Count cities
}
