using BookOrGetBooked.Shared.DTOs.ServiceCoverage;

namespace BookOrGetBooked.Shared.DTOs.Service
{
    public class ServiceCreateDTO
    {
        public required string Name { get; set; }
        public string? Description { get; set; }
        public required int ServiceTypeId { get; set; }
        public required decimal Price { get; set; }
        public required int CurrencyId { get; set; }
        public required string ProviderId { get; set; }

        public ServiceCoverageDTO? ServiceCoverage { get; set; }
    }
}
