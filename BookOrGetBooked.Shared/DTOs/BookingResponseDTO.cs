namespace BookOrGetBooked.Shared.DTOs
{
    public class BookingResponseDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ServiceId { get; set; }
        public DateTime TimeSlot { get; set; }
        public required string Status { get; set; }  // Status (e.g., Pending, Confirmed, etc.)
    }
}
