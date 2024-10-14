using BookOrGetBooked.Core.Models;
using BookOrGetBooked.Shared.DTOs;

namespace BookOrGetBooked.API.Mappings
{
    public class ServiceMappingProfile : MappingProfileBase
    {
        public ServiceMappingProfile()
        {
            // Mapping from Service to ServiceResponseDTO
            CreateMap<Service, ServiceResponseDTO>();
        }
    }
}
