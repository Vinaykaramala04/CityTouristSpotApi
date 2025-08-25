using CityTouristSpots.DTOs;
using CityTouristSpots.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CityTouristSpots.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin,RegularUser")]
    public class CitiesController : ControllerBase
    {
        private readonly ICityService _cityService;

        public CitiesController(ICityService cityService)
        {
            _cityService = cityService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin,RegularUser")]
        public async Task<ActionResult<IEnumerable<CityDto>>> GetCities()
        {
            var cities = await _cityService.GetAllCitiesAsync();
            return Ok(cities);
        }

        [HttpGet("count")]
        [Authorize(Roles = "Admin,RegularUser")]
        public async Task<ActionResult<int>> GetCitiesCount()
        {
            var count = await _cityService.GetCitiesCountAsync();
            return Ok(new { TotalCities = count });
        }

        [HttpGet("search")]
        [Authorize(Roles = "Admin,RegularUser")]
        public async Task<ActionResult<IEnumerable<CityDto>>> SearchCities([FromQuery] string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
                return BadRequest("Search keyword cannot be empty.");

            var cities = await _cityService.SearchCitiesAsync(keyword);
            return Ok(cities);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,RegularUser")]
        public async Task<ActionResult<CityDto>> GetCity(int id)
        {
            var city = await _cityService.GetCityByIdAsync(id);
            if (city == null) return NotFound();
            return Ok(city);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<CityDto>> PostCity(CityDto dto)
        {
            var created = await _cityService.CreateCityAsync(dto);
            return CreatedAtAction(nameof(GetCity), new { id = created.CityId }, created);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutCity(int id, CityDto dto)
        {
            await _cityService.UpdateCityAsync(id, dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteCity(int id)
        {
            await _cityService.DeleteCityAsync(id);
            return NoContent();
        }
    }
}
