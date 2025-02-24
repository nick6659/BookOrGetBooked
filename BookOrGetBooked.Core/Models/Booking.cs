namespace BookOrGetBooked.Core.Models
{
    public class Booking
    {
        public int Id { get; set; }  // Primary key

        // The user who made the booking (the booker)
        public int BookerId { get; set; }
        public User? Booker { get; set; }

        // The service that is being booked
        public required int ServiceId { get; set; }
        public Service? Service { get; set; }

        public DateTime TimeSlot { get; set; }

        public required int StatusId { get; set; }
        public BookingStatus? Status { get; set; }
    }
}
