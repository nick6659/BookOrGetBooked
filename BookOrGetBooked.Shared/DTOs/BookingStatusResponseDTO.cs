namespace BookOrGetBooked.Shared.DTOs
{
    public class BookingStatusResponseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool IsSystemDefined { get; set; }
        public int? CreatedByUserId { get; set; }
        public string? CreatedByUserName { get; set; }
    }
}
