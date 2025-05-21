using BookOrGetBooked.Shared.DTOs.BookingStatus;
using BookOrGetBooked.Shared.DTOs.Service;

namespace BookOrGetBooked.Shared.DTOs.Booking
{
    public class BookingSummaryDTO
    {
        public required string Status { get; set; }
        public DateTime TimeSlot { get; set; }

        public required ServiceSummaryDTO Service { get; set; }

        public string StreetAddress { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string PostalCode { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;

        public required string BookerFullName { get; set; }
        public required string BookerPhoneNumber { get; set; }
        public required string BookerId { get; set; }
        public required int Id { get; set; }
    }
}
