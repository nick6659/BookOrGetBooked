﻿namespace BookOrGetBooked.Shared.DTOs.Auth
{
    public class TokenResponseDto
    {
        public string Token { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
    }
}
