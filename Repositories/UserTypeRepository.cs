using CityTouristSpots.Data;
using CityTouristSpots.Interfaces;
using CityTouristSpots.Models;
using Microsoft.EntityFrameworkCore;

namespace CityTouristSpots.Repositories
{
    public class UserTypeRepository : IUserTypeRepository
    {
        private readonly AppDbContext _context;

        public UserTypeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UserType>> GetAllAsync()
        {
            return await _context.UserTypes
                .Include(ut => ut.Users)
                .ToListAsync();
        }

        public async Task<UserType> GetByIdAsync(int id)
        {
            return await _context.UserTypes
                .Include(ut => ut.Users)
                .FirstOrDefaultAsync(ut => ut.UserTypeId == id);
        }

        public async Task<UserType> GetByNameAsync(string name)
        {
            return await _context.UserTypes
                .FirstOrDefaultAsync(ut => ut.TypeName == name);
        }

        public async Task<UserType> CreateAsync(UserType userType)
        {
            _context.UserTypes.Add(userType);
            await _context.SaveChangesAsync();
            return userType;
        }

        public async Task UpdateAsync(UserType userType)
        {
            _context.Entry(userType).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var userType = await _context.UserTypes.FindAsync(id);
            if (userType != null)
            {
                _context.UserTypes.Remove(userType);
                await _context.SaveChangesAsync();
            }
        }
    }
}
