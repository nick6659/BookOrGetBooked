namespace BookOrGetBooked.Shared.DTOs
{
    public class BookingCreateDTO
    {
        public int BookerId { get; set; }
        public int ServiceId { get; set; }
        public DateTime TimeSlot { get; set; }
    }
}
