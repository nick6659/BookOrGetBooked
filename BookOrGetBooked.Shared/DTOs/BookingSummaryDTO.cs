namespace BookOrGetBooked.Shared.DTOs
{
    public class BookingSummaryDTO
    {
        public int Id { get; set; }
        public DateTime BookingDate { get; set; }
        public required string Status { get; set; }
    }
}
