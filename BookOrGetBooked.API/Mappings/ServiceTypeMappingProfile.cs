using BookOrGetBooked.API.Mappings;
using BookOrGetBooked.Core.Models;
using BookOrGetBooked.Shared.DTOs;

public class ServiceTypeMappingProfile : MappingProfileBase
{
    public ServiceTypeMappingProfile()
    {
        CreateMap<ServiceType, ServiceTypeResponseDTO>();

        CreateMap<ServiceTypeResponseDTO, ServiceType>();
    }
}
