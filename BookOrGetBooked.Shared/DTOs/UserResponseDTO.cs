namespace BookOrGetBooked.Shared.DTOs
{
    public class UserResponseDTO
    {
        public int Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }

        public List<PhoneNumberResponseDTO> PhoneNumbers { get; set; } = new List<PhoneNumberResponseDTO>();
        public List<ServiceSummaryDTO> ProvidedServices { get; set; } = new List<ServiceSummaryDTO>();
        public List<BookingSummaryDTO> Bookings { get; set; } = new List<BookingSummaryDTO>();
    }
}
