namespace BookOrGetBooked.Shared.DTOs
{
    public class BookingRequestDTO
    {
        public int UserId { get; set; }  // Required for associating the booking with a user
        public int ServiceId { get; set; }  // Service the user is booking
        public DateTime TimeSlot { get; set; }  // The time slot the user is booking
    }
}
