using BookOrGetBooked.Core.Models;
using BookOrGetBooked.Shared.DTOs;

namespace BookOrGetBooked.API.Mappings
{
    public class UserMappingProfile : MappingProfileBase
    {
        public UserMappingProfile()
        {
            // Mapping from User to UserResponseDTO
            CreateMap<User, UserResponseDTO>()
                .ForMember(dest => dest.ProvidedServices, opt => opt.MapFrom(src => src.ProvidedServices))
                .ForMember(dest => dest.Bookings, opt => opt.MapFrom(src => src.Bookings));

            // Mapping from Service to ServiceSummaryDTO (to summarize provided services)
            CreateMap<Service, ServiceSummaryDTO>()
                .ForMember(dest => dest.ServiceName, opt => opt.MapFrom(src => src.Name));

            // Mapping from Booking to BookingSummaryDTO (to summarize bookings)
            CreateMap<Booking, BookingSummaryDTO>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));
        }
    }
}
