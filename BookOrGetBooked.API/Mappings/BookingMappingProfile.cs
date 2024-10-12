using BookOrGetBooked.Core.Models;
using BookOrGetBooked.Shared.DTOs;

namespace BookOrGetBooked.API.Mappings
{
    public class BookingMappingProfile : MappingProfileBase
    {
        public BookingMappingProfile()
        {
            // Mapping from BookingRequestDTO to Booking (for creating bookings)
            CreateMap<BookingRequestDTO, Booking>();

            // Mapping from Booking to BookingResponseDTO (for returning booking details)
            CreateMap<Booking, BookingResponseDTO>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));
        }
    }
}
