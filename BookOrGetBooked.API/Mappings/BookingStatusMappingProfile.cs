using AutoMapper;
using BookOrGetBooked.Core.Models;
using BookOrGetBooked.Shared.DTOs.BookingStatus;

namespace BookOrGetBooked.API.Mappings
{
    public class BookingStatusMappingProfile : MappingProfileBase
    {
        public BookingStatusMappingProfile()
        {
            // Mapping from BookingStatus to BookingStatusSummaryDTO
            CreateMap<BookingStatus, BookingStatusSummaryDTO>();

            // Mapping from BookingStatusCreateDTO to BookingStatus
            CreateMap<BookingStatusCreateDTO, BookingStatus>();

            // Mapping from BookingStatusUpdateDTO to BookingStatus
            CreateMap<BookingStatusUpdateDTO, BookingStatus>();
        }
    }
}
