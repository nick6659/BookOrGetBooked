namespace BookOrGetBooked.Shared.DTOs.Booking
{
    public class BookingUpdateDTO
    {
        public string BookerId { get; set; } = string.Empty;
        public int ServiceId { get; set; }
        public DateTime TimeSlot { get; set; }
        public int BookingStatusId { get; set; }
    }
}
