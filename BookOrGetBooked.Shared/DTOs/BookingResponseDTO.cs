namespace BookOrGetBooked.Shared.DTOs
{
    public class BookingResponseDTO
    {
        public int Id { get; set; }
        public int BookerId { get; set; }
        public DateTime TimeSlot { get; set; }
        public required ServiceResponseDTO Service { get; set; }
        public required BookingStatusSummaryDTO Status { get; set; }
    }
}
