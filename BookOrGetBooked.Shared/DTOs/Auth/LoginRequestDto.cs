namespace BookOrGetBooked.Shared.DTOs.Auth
{
    // main login DTO
    public class LoginRequestDto
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}
