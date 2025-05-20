using BookOrGetBooked.Infrastructure.Data;

namespace BookOrGetBooked.Core.Models
{
    public class Booking
    {
        public int Id { get; set; }  // Primary key

        // The user who made the booking (the booker)
        public required string BookerId { get; set; }
        public required ApplicationUser Booker { get; set; }

        // The service that is being booked
        public required int ServiceId { get; set; }
        public required Service? Service { get; set; }

        public required DateTime TimeSlot { get; set; }

        public required int StatusId { get; set; }
        public required BookingStatus Status { get; set; }

        public string StreetAddress { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string PostalCode { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;

        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
