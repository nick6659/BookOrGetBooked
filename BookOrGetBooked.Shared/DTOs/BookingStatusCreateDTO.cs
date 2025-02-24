namespace BookOrGetBooked.Shared.DTOs
{
    public class BookingStatusCreateDTO
    {
        public required string Name { get; set; }
        public required bool IsSystemDefined { get; set; }
        public int? CreatedByUserId { get; set; }
    }
}
