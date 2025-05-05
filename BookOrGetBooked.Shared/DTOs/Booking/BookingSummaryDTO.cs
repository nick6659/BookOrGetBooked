namespace BookOrGetBooked.Shared.DTOs.Booking
{
    public class BookingSummaryDTO
    {
        public int Id { get; set; }
        public DateTime BookingDate { get; set; }
        public required string Status { get; set; }
    }
}
