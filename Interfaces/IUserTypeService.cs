using CityTouristSpots.Models;

namespace CityTouristSpots.Services
{
    public interface IUserTypeService
    {
        Task<IEnumerable<UserType>> GetAllUserTypesAsync();
        Task<UserType> GetUserTypeByIdAsync(int id);
        Task<UserType> GetUserTypeByNameAsync(string name);
        Task<UserType> CreateUserTypeAsync(UserType userType);
        Task UpdateUserTypeAsync(int id, UserType userType);
        Task DeleteUserTypeAsync(int id);
    }
}