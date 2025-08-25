using CityTouristSpots.Data;
using CityTouristSpots.Interfaces;
using CityTouristSpots.Models;
using Microsoft.EntityFrameworkCore;

namespace CityTouristSpots.Repositories
{
    public class CityRepository : ICityRepository
    {
        private readonly AppDbContext _context;

        public CityRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<City>> GetAllAsync()
        {
            return await _context.Cities
                .Include(c => c.CreatedByUser)
                .Include(c => c.TouristSpots)
                .ToListAsync();
        }

        public async Task<City> GetByIdAsync(int id)
        {
            return await _context.Cities
                .Include(c => c.CreatedByUser)
                .Include(c => c.TouristSpots)
                .FirstOrDefaultAsync(c => c.CityId == id);
        }

        public async Task<City> CreateAsync(City city)
        {
            _context.Cities.Add(city);
            await _context.SaveChangesAsync();
            return city;
        }

        public async Task UpdateAsync(City city)
        {
            _context.Entry(city).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var city = await _context.Cities.FindAsync(id);
            if (city != null)
            {
                _context.Cities.Remove(city);
                await _context.SaveChangesAsync();
            }
        }

        // ✅ Implement SearchAsync here
        public async Task<IEnumerable<City>> SearchAsync(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
                return await GetAllAsync();

            return await _context.Cities
                .Include(c => c.CreatedByUser)
                .Include(c => c.TouristSpots)
                .Where(c => c.CityName.Contains(keyword) ||
                            c.Country.Contains(keyword) ||
                            c.Description.Contains(keyword))
                .ToListAsync();
        }

        // ✅ Efficient count
        public async Task<int> GetCountAsync()
        {
            return await _context.Cities.CountAsync();
        }

        public Task<int> CountAsync()
        {
            throw new NotImplementedException();
        }
    }
}
