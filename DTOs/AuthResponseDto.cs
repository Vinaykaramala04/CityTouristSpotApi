﻿namespace CityTouristSpots.DTOs
{
    public class AuthResponseDto
    {
        public int UserId { get; set; }
        public string Email { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty; // ✅ Added Token
        public string Role { get; internal set; }
    }
}
