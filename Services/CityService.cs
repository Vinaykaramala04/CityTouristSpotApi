using CityTouristSpots.DTOs;
using CityTouristSpots.Models;
using CityTouristSpots.Interfaces;

namespace CityTouristSpots.Services
{
    public class CityService : ICityService
    {
        private readonly ICityRepository _cityRepository;

        public CityService(ICityRepository cityRepository)
        {
            _cityRepository = cityRepository;
        }

        public async Task<IEnumerable<CityDto>> GetAllCitiesAsync()
        {
            var cities = await _cityRepository.GetAllAsync();
            return cities.Select(city => new CityDto
            {
                CityId = city.CityId,
                CityName = city.CityName ?? string.Empty,
                Country = city.Country,
                Description = city.Description,
                CreatedBy = city.CreatedBy
            });
        }

        public async Task<CityDto> GetCityByIdAsync(int id)
        {
            var city = await _cityRepository.GetByIdAsync(id);
            if (city == null)
                throw new KeyNotFoundException($"City with ID {id} not found.");

            return new CityDto
            {
                CityId = city.CityId,
                CityName = city.CityName ?? string.Empty,
                Country = city.Country,
                Description = city.Description,
                CreatedBy = city.CreatedBy
            };
        }

        public async Task<CityDto> CreateCityAsync(CityDto dto)
        {
            var city = new City
            {
                CityName = dto.CityName,
                Country = dto.Country,
                Description = dto.Description,
                CreatedBy = dto.CreatedBy ?? 0
            };

            var createdCity = await _cityRepository.CreateAsync(city);

            return new CityDto
            {
                CityId = createdCity.CityId,
                CityName = createdCity.CityName ?? string.Empty,
                Country = createdCity.Country,
                Description = createdCity.Description,
                CreatedBy = createdCity.CreatedBy
            };
        }

        public async Task UpdateCityAsync(int id, CityDto dto)
        {
            var existingCity = await _cityRepository.GetByIdAsync(id);
            if (existingCity == null)
                throw new KeyNotFoundException($"City with ID {id} not found.");

            existingCity.CityName = dto.CityName;
            existingCity.Country = dto.Country;
            existingCity.Description = dto.Description;
            existingCity.CreatedBy = dto.CreatedBy ?? existingCity.CreatedBy;

            await _cityRepository.UpdateAsync(existingCity);
        }

        public async Task DeleteCityAsync(int id)
        {
            var city = await _cityRepository.GetByIdAsync(id);
            if (city == null)
                throw new KeyNotFoundException($"City with ID {id} not found.");

            await _cityRepository.DeleteAsync(id);
        }
    }
}