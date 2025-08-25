using CityTouristSpots.DTOs;
using CityTouristSpots.Models;
using CityTouristSpots.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CityTouristSpots.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IConfiguration _configuration;

        public UserService(IUserRepository userRepository, IPasswordHasher<User> passwordHasher, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _configuration = configuration;
        }

        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllAsync();
            return users.Select(MapToDto);
        }

        public async Task<UserDto?> GetUserByIdAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            return user == null ? null : MapToDto(user);
        }

        public async Task<UserDto> CreateUserAsync(UserDto dto)
        {
            var existingUser = await _userRepository.GetByEmailAsync(dto.Email ?? string.Empty);
            if (existingUser != null)
                throw new InvalidOperationException("User with this email already exists");

            var user = new User
            {
                UserName = dto.UserName,
                Email = dto.Email,
                UserTypeId = 1 // Default user type
            };

            if (!string.IsNullOrEmpty(dto.Password))
            {
                user.PasswordHash = _passwordHasher.HashPassword(user, dto.Password);
            }

            var createdUser = await _userRepository.CreateAsync(user);
            return MapToDto(createdUser);
        }

        public async Task UpdateUserAsync(int id, UserDto dto)
        {
            var existingUser = await _userRepository.GetByIdAsync(id);
            if (existingUser == null)
                throw new KeyNotFoundException($"User with ID {id} not found.");

            existingUser.UserName = dto.UserName ?? existingUser.UserName;
            existingUser.Email = dto.Email ?? existingUser.Email;

            if (!string.IsNullOrEmpty(dto.Password))
            {
                existingUser.PasswordHash = _passwordHasher.HashPassword(existingUser, dto.Password);
            }

            await _userRepository.UpdateAsync(existingUser);
        }

        public async Task DeleteUserAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
                throw new KeyNotFoundException($"User with ID {id} not found.");

            await _userRepository.DeleteAsync(id);
        }

        public async Task<AuthResponseDto?> AuthenticateAsync(string email, string password)
        {
            var user = await _userRepository.GetByEmailAsync(email);
            if (user == null)
                return null;

            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash ?? string.Empty, password);
            if (result != PasswordVerificationResult.Success)
                return null;

            var token = GenerateJwtToken(user);

            return new AuthResponseDto
            {
                UserId = user.UserId,
                Email = user.Email ?? string.Empty,
                UserName = user.UserName ?? string.Empty,
                Token = token,
                Role = user.UserType?.TypeName ?? "User"
            };
        }

        private string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"] ?? "YourSecretKeyHere");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                    new Claim(ClaimTypes.Email, user.Email ?? string.Empty),
                    new Claim(ClaimTypes.Name, user.UserName ?? string.Empty),
                    new Claim(ClaimTypes.Role, user.UserType?.TypeName ?? "User")
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"]
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        // ✅ Final Search implementation
        public async Task<IEnumerable<UserDto>> SearchUsersAsync(string keyword)
        {
            var users = await _userRepository.SearchAsync(keyword);
            return users.Select(MapToDto);
        }

        // ✅ Helper mapper
        private static UserDto MapToDto(User user)
        {
            return new UserDto
            {
                UserId = user.UserId,
                UserName = user.UserName,
                Email = user.Email,
                UserTypeName = user.UserType?.TypeName,
                CitiesCreated = user.CitiesCreated?.Select(c => new CityDto
                {
                    CityId = c.CityId,
                    CityName = c.CityName ?? string.Empty,
                    Country = c.Country,
                    Description = c.Description,
                    CreatedBy = c.CreatedBy
                }).ToList() ?? new List<CityDto>(),
                TouristSpotsCreated = user.TouristSpotsCreated?.Select(ts => new TouristSpotDto
                {
                    TouristSpotId = ts.TouristSpotId,
                    SpotName = ts.SpotName ?? string.Empty,
                    Location = ts.Location,
                    Description = ts.Description,
                    EntryFee = ts.EntryFee,
                    CityId = ts.CityId,
                    CreatedBy = ts.CreatedBy
                }).ToList() ?? new List<TouristSpotDto>()
            };
        }
    }
}
