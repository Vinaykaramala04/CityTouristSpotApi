using CityTouristSpots.Interfaces;
using CityTouristSpots.Models;
using CityTouristSpots.Repositories;

namespace CityTouristSpots.Services
{
    public class UserTypeService : IUserTypeService
    {
        private readonly IUserTypeRepository _userTypeRepository;

        public UserTypeService(IUserTypeRepository userTypeRepository)
        {
            _userTypeRepository = userTypeRepository;
        }

        public async Task<IEnumerable<UserType>> GetAllUserTypesAsync()
        {
            return await _userTypeRepository.GetAllAsync();
        }

        public async Task<UserType> GetUserTypeByIdAsync(int id)
        {
            var userType = await _userTypeRepository.GetByIdAsync(id);
            if (userType == null)
                throw new KeyNotFoundException($"UserType with ID {id} not found.");

            return userType;
        }

        public async Task<UserType> GetUserTypeByNameAsync(string name)
        {
            var userType = await _userTypeRepository.GetByNameAsync(name);
            if (userType == null)
                throw new KeyNotFoundException($"UserType with name '{name}' not found.");

            return userType;
        }

        public async Task<UserType> CreateUserTypeAsync(UserType userType)
        {
            return await _userTypeRepository.CreateAsync(userType);
        }

        public async Task UpdateUserTypeAsync(int id, UserType userType)
        {
            var existingUserType = await _userTypeRepository.GetByIdAsync(id);
            if (existingUserType == null)
                throw new KeyNotFoundException($"UserType with ID {id} not found.");

            existingUserType.TypeName = userType.TypeName;

            await _userTypeRepository.UpdateAsync(existingUserType);
        }

        public async Task DeleteUserTypeAsync(int id)
        {
            var userType = await _userTypeRepository.GetByIdAsync(id);
            if (userType == null)
                throw new KeyNotFoundException($"UserType with ID {id} not found.");

            // Check if any users are associated with this user type
            if (userType.Users != null && userType.Users.Any())
            {
                throw new InvalidOperationException($"Cannot delete UserType with ID {id} because it has associated users.");
            }

            await _userTypeRepository.DeleteAsync(id);
        }
    }
}