namespace BookOrGetBooked.Shared.DTOs.Service
{
    public class ServiceSummaryDTO
    {
        public int Id { get; set; }
        public required string ServiceName { get; set; }
        public required string Description { get; set; }
    }
}
