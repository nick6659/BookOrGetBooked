namespace BookOrGetBooked.Shared.DTOs.Booking
{
    public class BookingCreateDTO
    {
        public string BookerId { get; set; } = string.Empty;
        public int ServiceId { get; set; }
        public DateTime TimeSlot { get; set; }

        public string StreetAddress { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string PostalCode { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
    }
}
