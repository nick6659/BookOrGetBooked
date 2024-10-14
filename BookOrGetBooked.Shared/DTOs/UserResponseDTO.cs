namespace BookOrGetBooked.Shared.DTOs
{
    public class UserResponseDTO
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public string? PhoneNumber { get; set; }

        // Optionally: Include a summary of provided services
        public List<ServiceSummaryDTO> ProvidedServices { get; set; } = new List<ServiceSummaryDTO>();

        // Optionally: Include a summary of bookings
        public List<BookingSummaryDTO> Bookings { get; set; } = new List<BookingSummaryDTO>();
    }
}
