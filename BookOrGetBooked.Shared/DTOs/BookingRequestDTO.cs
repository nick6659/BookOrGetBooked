namespace BookOrGetBooked.Shared.DTOs
{
    public class BookingRequestDTO
    {
        public int UserId { get; set; }
        public int ServiceId { get; set; }
        public DateTime TimeSlot { get; set; }
    }
}
