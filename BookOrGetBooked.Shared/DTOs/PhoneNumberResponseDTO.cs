namespace BookOrGetBooked.Shared.DTOs
{
    public class PhoneNumberResponseDTO
    {
        public required string Prefix { get; set; } = string.Empty; // Country code, e.g., +1
        public required string Number { get; set; } = string.Empty; // Phone number
    }
}
