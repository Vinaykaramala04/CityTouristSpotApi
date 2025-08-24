using CityTouristSpots.Data;
using CityTouristSpots.Interfaces;
using CityTouristSpots.Models;
using Microsoft.EntityFrameworkCore;

namespace CityTouristSpots.Repositories
{
    public class TouristSpotRepository : ITouristSpotRepository
    {
        private readonly AppDbContext _context;

        public TouristSpotRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TouristSpot>> GetAllAsync()
        {
            return await _context.TouristSpots
                .Include(ts => ts.City)
                .Include(ts => ts.CreatedByUser)
                .ToListAsync();
        }

        public async Task<TouristSpot> GetByIdAsync(int id)
        {
            return await _context.TouristSpots
                .Include(ts => ts.City)
                .Include(ts => ts.CreatedByUser)
                .FirstOrDefaultAsync(ts => ts.TouristSpotId == id);
        }

        public async Task<TouristSpot> CreateAsync(TouristSpot spot)
        {
            _context.TouristSpots.Add(spot);
            await _context.SaveChangesAsync();
            return spot;
        }

        public async Task UpdateAsync(TouristSpot spot)
        {
            _context.Entry(spot).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var spot = await _context.TouristSpots.FindAsync(id);
            if (spot != null)
            {
                _context.TouristSpots.Remove(spot);
                await _context.SaveChangesAsync();
            }
        }
    }
}