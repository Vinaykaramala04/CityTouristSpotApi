using CityTouristSpots.Models;

namespace CityTouristSpots.Interfaces
{
    public interface ICityRepository
    {
        Task<IEnumerable<City>> GetAllAsync();
        Task<City> GetByIdAsync(int id);
        Task<City> CreateAsync(City city);
        Task UpdateAsync(City city);
        Task DeleteAsync(int id);
    }
}
