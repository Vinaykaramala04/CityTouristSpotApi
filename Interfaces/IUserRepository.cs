using CityTouristSpots.Models;

namespace CityTouristSpots.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task<User> GetByIdAsync(int id);
        Task<User> GetByEmailAsync(string email); // Added for authentication
        Task<User> CreateAsync(User user);
        Task UpdateAsync(User user);
        Task DeleteAsync(int id);
        //Task<IEnumerable<object>> SearchAsync(string keyword);
        Task<IEnumerable<User>> SearchAsync(string keyword);


    }
}