using CityTouristSpots.DTOs;

namespace CityTouristSpots.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAllUsersAsync();
        Task<UserDto?> GetUserByIdAsync(int id);
        Task<UserDto> CreateUserAsync(UserDto dto);
        Task UpdateUserAsync(int id, UserDto dto);
        Task DeleteUserAsync(int id);

        // Authentication
        Task<AuthResponseDto?> AuthenticateAsync(string email, string password);
        Task<IEnumerable<User>> SearchUsersAsync(string keyword);

    }
}
