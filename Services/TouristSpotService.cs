using CityTouristSpots.DTOs;
using CityTouristSpots.Models;
using CityTouristSpots.Interfaces;

namespace CityTouristSpots.Services
{
    public class TouristSpotService : ITouristSpotService
    {
        private readonly ITouristSpotRepository _touristSpotRepository;

        public TouristSpotService(ITouristSpotRepository touristSpotRepository)
        {
            _touristSpotRepository = touristSpotRepository;
        }

        public async Task<IEnumerable<TouristSpotDto>> GetAllTouristSpotsAsync()
        {
            var spots = await _touristSpotRepository.GetAllAsync();
            return spots.Select(spot => new TouristSpotDto
            {
                TouristSpotId = spot.TouristSpotId,
                SpotName = spot.SpotName ?? string.Empty,
                Location = spot.Location,
                Description = spot.Description,
                EntryFee = spot.EntryFee,
                CityId = spot.CityId,
                CreatedBy = spot.CreatedBy
            });
        }

        public async Task<TouristSpotDto> GetTouristSpotByIdAsync(int id)
        {
            var spot = await _touristSpotRepository.GetByIdAsync(id);
            if (spot == null)
                throw new KeyNotFoundException($"Tourist Spot with ID {id} not found.");

            return new TouristSpotDto
            {
                TouristSpotId = spot.TouristSpotId,
                SpotName = spot.SpotName ?? string.Empty,
                Location = spot.Location,
                Description = spot.Description,
                EntryFee = spot.EntryFee,
                CityId = spot.CityId,
                CreatedBy = spot.CreatedBy
            };
        }

        public async Task<TouristSpotDto> CreateTouristSpotAsync(TouristSpotDto dto)
        {
            var spot = new TouristSpot
            {
                SpotName = dto.SpotName,
                Location = dto.Location,
                Description = dto.Description,
                EntryFee = dto.EntryFee,
                CityId = dto.CityId ?? 0,
                CreatedBy = dto.CreatedBy
            };

            var createdSpot = await _touristSpotRepository.CreateAsync(spot);

            return new TouristSpotDto
            {
                TouristSpotId = createdSpot.TouristSpotId,
                SpotName = createdSpot.SpotName ?? string.Empty,
                Location = createdSpot.Location,
                Description = createdSpot.Description,
                EntryFee = createdSpot.EntryFee,
                CityId = createdSpot.CityId,
                CreatedBy = createdSpot.CreatedBy
            };
        }

        public async Task UpdateTouristSpotAsync(int id, TouristSpotDto dto)
        {
            var existingSpot = await _touristSpotRepository.GetByIdAsync(id);
            if (existingSpot == null)
                throw new KeyNotFoundException($"Tourist Spot with ID {id} not found.");

            existingSpot.SpotName = dto.SpotName;
            existingSpot.Location = dto.Location;
            existingSpot.Description = dto.Description;
            existingSpot.EntryFee = dto.EntryFee;
            existingSpot.CityId = dto.CityId ?? existingSpot.CityId;
            existingSpot.CreatedBy = dto.CreatedBy ?? existingSpot.CreatedBy;

            await _touristSpotRepository.UpdateAsync(existingSpot);
        }

        public async Task DeleteTouristSpotAsync(int id)
        {
            var spot = await _touristSpotRepository.GetByIdAsync(id);
            if (spot == null)
                throw new KeyNotFoundException($"Tourist Spot with ID {id} not found.");

            await _touristSpotRepository.DeleteAsync(id);
        }
    }
}