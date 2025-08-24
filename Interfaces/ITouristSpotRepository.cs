using CityTouristSpots.Models;

namespace CityTouristSpots.Interfaces
{
    public interface ITouristSpotRepository
    {
        Task<IEnumerable<TouristSpot>> GetAllAsync();
        Task<TouristSpot> GetByIdAsync(int id);
        Task<TouristSpot> CreateAsync(TouristSpot spot);
        Task UpdateAsync(TouristSpot spot);
        Task DeleteAsync(int id);
    }
}