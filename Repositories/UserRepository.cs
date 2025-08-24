using CityTouristSpots.Data;
using CityTouristSpots.Interfaces;
using CityTouristSpots.Models;
using Microsoft.EntityFrameworkCore;

namespace CityTouristSpots.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users
                .Include(u => u.UserType)
                .Include(u => u.CitiesCreated)
                .Include(u => u.TouristSpotsCreated)
                .ToListAsync();
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await _context.Users
                .Include(u => u.UserType)
                .Include(u => u.CitiesCreated)
                .Include(u => u.TouristSpotsCreated)
                .FirstOrDefaultAsync(u => u.UserId == id);
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            return await _context.Users
                .Include(u => u.UserType)
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User> CreateAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task UpdateAsync(User user)
        {
            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<User>> SearchAsync(string keyword)
        {
            return await _context.Users
                .Where(u => u.UserName.Contains(keyword) ||
                            u.Email.Contains(keyword) ||
                            u.UserType.TypeName.Contains(keyword))
                .ToListAsync();
        }

       
    }
}