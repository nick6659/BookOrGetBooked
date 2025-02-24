namespace BookOrGetBooked.Shared.DTOs
{
    public class BookingUpdateDTO
    {
        public int BookerId { get; set; }
        public int ServiceId { get; set; }
        public DateTime TimeSlot { get; set; }
        public int BookingStatusId { get; set; }
    }
}
