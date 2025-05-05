using BookOrGetBooked.Shared.DTOs.Currency;
using BookOrGetBooked.Shared.DTOs.ServiceCoverage;
using BookOrGetBooked.Shared.DTOs.ServiceType;

namespace BookOrGetBooked.Shared.DTOs.Service
{
    public class ServiceResponseDTO
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public decimal Price { get; set; }
        public required string ProviderId { get; set; }

        public required ServiceTypeResponseDTO ServiceType { get; set; }
        public CurrencyResponseDTO Currency { get; set; } = null!;
        public ServiceCoverageResponseDTO? ServiceCoverage { get; set; }
    }
}
