namespace BookOrGetBooked.Shared.DTOs
{
    public class BookingResponseDTO
    {
        public int Id { get; set; }
        public int ProviderId { get; set; }
        public int BookerId { get; set; }
        public int ServiceId { get; set; }
        public DateTime TimeSlot { get; set; }
        public required BookingStatusSummaryDTO Status { get; set; }
    }
}
