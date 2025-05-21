using BookOrGetBooked.Shared.DTOs.BookingStatus;
using BookOrGetBooked.Shared.DTOs.Service;

namespace BookOrGetBooked.Shared.DTOs.Booking
{
    public class BookingResponseDTO
    {
        public string Id { get; set; } = string.Empty;
        public int BookerId { get; set; }
        public DateTime TimeSlot { get; set; }
        public required ServiceResponseDTO Service { get; set; }
        public required BookingStatusSummaryDTO Status { get; set; }
    }
}
