using BookOrGetBooked.API.Mappings;
using BookOrGetBooked.Core.Models;
using BookOrGetBooked.Shared.DTOs;

public class BookingMappingProfile : MappingProfileBase
{
    public BookingMappingProfile()
    {
        CreateMap<BookingCreateDTO, Booking>()
            .ForMember(dest => dest.StatusId, opt => opt.MapFrom(src => 1)); // Default to "Pending"

        // Mapping from Booking to BookingResponseDTO
        CreateMap<Booking, BookingResponseDTO>()
            .ForMember(dest => dest.Service, opt => opt.MapFrom(src => src.Service)); // Map full Service

        // Mapping from Service to ServiceResponseDTO
        CreateMap<Service, ServiceResponseDTO>();

        // Mapping from BookingStatus to BookingStatusSummaryDTO
        CreateMap<BookingStatus, BookingStatusSummaryDTO>();
    }
}
