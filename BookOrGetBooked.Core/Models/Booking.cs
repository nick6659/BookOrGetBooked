namespace BookOrGetBooked.Core.Models
{
    public class Booking
    {
        public int Id { get; set; }  // Primary key

        // The user who made the booking (the booker)
        public required int BookerId { get; set; }
        public required User Booker { get; set; }

        // The service that is being booked
        public required int ServiceId { get; set; }
        public required Service? Service { get; set; }

        public required DateTime TimeSlot { get; set; }

        public required int StatusId { get; set; }
        public required BookingStatus Status { get; set; }
    }
}
