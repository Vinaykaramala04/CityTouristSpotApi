using CityTouristSpots.Models;

namespace CityTouristSpots.Interfaces
{
    public interface IUserTypeRepository
    {
        Task<IEnumerable<UserType>> GetAllAsync();
        Task<UserType> GetByIdAsync(int id);
        Task<UserType> GetByNameAsync(string name);
        Task<UserType> CreateAsync(UserType userType);
        Task UpdateAsync(UserType userType);
        Task DeleteAsync(int id);
    }
}
