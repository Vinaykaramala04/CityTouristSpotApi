using CityTouristSpots.DTOs;
using CityTouristSpots.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace CityTouristSpots.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<AuthResponseDto>> Register(UserRegisterDto dto)
        {
            try
            {
                // Create user using existing UserService
                var userDto = new UserDto
                {
                    UserName = dto.UserName ?? dto.Email.Split('@')[0],
                    Email = dto.Email,
                    Password = dto.Password
                };

                var createdUser = await _userService.CreateUserAsync(userDto);

                // Authenticate the newly created user to get token
                var authResponse = await _userService.AuthenticateAsync(dto.Email, dto.Password);

                if (authResponse == null)
                    return StatusCode(500, new { message = "Error authenticating new user" });

                return Ok(authResponse);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                // Get the innermost exception for better debugging
                var innerException = ex;
                while (innerException.InnerException != null)
                {
                    innerException = innerException.InnerException;
                }

                return StatusCode(500, new
                {
                    message = "Error creating user",
                    error = ex.Message,
                    innerError = innerException.Message,
                    stackTrace = ex.StackTrace
                });
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthResponseDto>> Login(LoginRequestDto dto)
        {
            var authResponse = await _userService.AuthenticateAsync(dto.Email, dto.Password);

            if (authResponse == null)
                return Unauthorized(new { message = "Invalid credentials" });

            return Ok(authResponse);
        }
    }
}