using BookOrGetBooked.Core.Models;
using BookOrGetBooked.Shared.DTOs;

namespace BookOrGetBooked.API.Mappings
{
    public class BookingMappingProfile : MappingProfileBase
    {
        public BookingMappingProfile()
        {
            // Mapping from BookingCreateDTO to Booking (for creating bookings)
            CreateMap<BookingCreateDTO, Booking>()
                .ForMember(dest => dest.StatusId, opt => opt.MapFrom(src => 1)); // Default to "Pending"

            // Mapping from BookingStatus to BookingStatusSummaryDTO
            CreateMap<BookingStatus, BookingStatusSummaryDTO>();

            // Mapping from Booking to BookingResponseDTO (AutoMapper automatically maps Status)
            CreateMap<Booking, BookingResponseDTO>();

            // Mapping from BookingUpdateDTO to Booking (for updating bookings)
            CreateMap<BookingUpdateDTO, Booking>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()); // ID should not be updated
        }
    }
}
