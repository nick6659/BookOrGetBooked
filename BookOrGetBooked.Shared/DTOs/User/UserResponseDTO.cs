using BookOrGetBooked.Shared.DTOs.Booking;
using BookOrGetBooked.Shared.DTOs.Service;

namespace BookOrGetBooked.Shared.DTOs.User
{
    public class UserResponseDTO
    {
        public string Id { get; set; } = string.Empty;
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public string PhoneNumber { get; set; } = string.Empty;

        public List<ServiceSummaryDTO> ProvidedServices { get; set; } = new List<ServiceSummaryDTO>();
        public List<BookingSummaryDTO> Bookings { get; set; } = new List<BookingSummaryDTO>();
    }
}
