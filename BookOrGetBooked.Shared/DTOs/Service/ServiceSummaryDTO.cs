namespace BookOrGetBooked.Shared.DTOs.Service
{
    public class ServiceSummaryDTO
    {
        public required string ServiceName { get; set; }
        public required string Description { get; set; }

        public required string ProviderFullName { get; set; }
        public string? ProviderPhoneNumber { get; set; }
    }
}
