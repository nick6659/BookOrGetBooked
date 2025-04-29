using BookOrGetBooked.Core.Models;
using BookOrGetBooked.Shared.DTOs;

namespace BookOrGetBooked.API.Mappings;

public class ServiceMappingProfile : MappingProfileBase
{
    public ServiceMappingProfile()
    {
        // AutoMapper will handle nested object mapping automatically!
        CreateMap<Service, ServiceResponseDTO>();

        // Ensure related entities are mapped correctly
        CreateMap<ServiceType, ServiceTypeResponseDTO>();
        CreateMap<Currency, CurrencyResponseDTO>();

        // If using ServiceCoverage:
        CreateMap<ServiceCoverage, ServiceCoverageResponseDTO>();
    }
}
