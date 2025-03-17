using AutoMapper;
using BookOrGetBooked.Core.Models;
using BookOrGetBooked.Shared.DTOs;

namespace BookOrGetBooked.API.Mappings
{
    public class UserMappingProfile : MappingProfileBase
    {
        public UserMappingProfile()
        {
            // Full User Mapping (when fetching user)
            CreateMap<User, UserResponseDTO>()
                .ForMember(dest => dest.ProvidedServices, opt => opt.MapFrom(src => src.Services))
                .ForMember(dest => dest.Bookings, opt => opt.MapFrom(src => src.Bookings))
                .ForMember(dest => dest.PhoneNumbers, opt => opt.MapFrom(src => src.PhoneNumbers));

            // User Created Mapping (when returning after user creation)
            CreateMap<User, UserCreatedDTO>()
                .ForMember(dest => dest.PhoneNumbers, opt => opt.MapFrom(src => src.PhoneNumbers));

            // Phone Number Mapping
            CreateMap<PhoneNumber, PhoneNumberResponseDTO>();

            // Provided Services & Bookings
            CreateMap<Service, ServiceSummaryDTO>()
                .ForMember(dest => dest.ServiceName, opt => opt.MapFrom(src => src.Name));

            CreateMap<Booking, BookingSummaryDTO>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));
        }
    }
}
