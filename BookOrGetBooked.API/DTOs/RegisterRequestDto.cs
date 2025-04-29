namespace BookOrGetBooked.Shared.DTOs
{
    public class RegisterRequestDto
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public PhoneNumberCreateDTO PhoneNumber { get; set; } = new PhoneNumberCreateDTO();
    }
}
