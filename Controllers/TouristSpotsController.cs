using CityTouristSpots.DTOs;
using CityTouristSpots.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CityTouristSpots.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin,RegularUser")]

    public class TouristSpotsController : ControllerBase
    {
        private readonly ITouristSpotService _spotService;

        public TouristSpotsController(ITouristSpotService spotService)
        {
            _spotService = spotService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin,RegularUser")]
        public async Task<ActionResult<IEnumerable<TouristSpotDto>>> GetTouristSpots()
        {
            var spots = await _spotService.GetAllTouristSpotsAsync();
            return Ok(spots);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,RegularUser")]
        public async Task<ActionResult<TouristSpotDto>> GetTouristSpot(int id)
        {
            var spot = await _spotService.GetTouristSpotByIdAsync(id);
            if (spot == null) return NotFound();
            return Ok(spot);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<TouristSpotDto>> PostTouristSpot(TouristSpotDto dto)
        {
            var created = await _spotService.CreateTouristSpotAsync(dto);
            return CreatedAtAction(nameof(GetTouristSpot), new { id = created.TouristSpotId }, created);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutTouristSpot(int id, TouristSpotDto dto)
        {
            await _spotService.UpdateTouristSpotAsync(id, dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteTouristSpot(int id)
        {
            await _spotService.DeleteTouristSpotAsync(id);
            return NoContent();
        }
    }
}
