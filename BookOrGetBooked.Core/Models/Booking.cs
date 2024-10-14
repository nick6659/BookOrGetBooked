namespace BookOrGetBooked.Core.Models
{
    public class Booking
    {
        public int Id { get; set; }  // Primary key

        // The user who made the booking (the booker)
        public int UserId { get; set; }
        public User? User { get; set; }

        // The service that is being booked
        public int ServiceId { get; set; }
        public Service? Service { get; set; }

        public DateTime TimeSlot { get; set; }
        public BookingStatus Status { get; set; }
    }

    public enum BookingStatus
    {
        Pending,
        Confirmed,
        Canceled
    }
}
