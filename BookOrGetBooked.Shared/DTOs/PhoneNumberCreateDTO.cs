namespace BookOrGetBooked.Shared.DTOs
{
    public class PhoneNumberCreateDTO
    {
        public required int UserId { get; set; } // Associated User ID
        public required string Prefix { get; set; } = string.Empty; // Country code, e.g., +1
        public required string Number { get; set; } = string.Empty; // Phone number
    }
}
