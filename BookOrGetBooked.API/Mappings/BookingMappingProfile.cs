using BookOrGetBooked.API.Mappings;
using BookOrGetBooked.Core.Models;
using BookOrGetBooked.Shared.DTOs.Booking;
using BookOrGetBooked.Shared.DTOs.BookingStatus;
using BookOrGetBooked.Shared.DTOs.Service;

public class BookingMappingProfile : MappingProfileBase
{
    public BookingMappingProfile()
    {
        CreateMap<BookingCreateDTO, Booking>()
            .ForMember(dest => dest.StatusId, opt => opt.MapFrom(src => 1)); // Default to "Pending"

        CreateMap<Booking, BookingResponseDTO>()
            .ForMember(dest => dest.Service, opt => opt.MapFrom(src => src.Service));

        CreateMap<BookingStatus, BookingStatusSummaryDTO>();
        CreateMap<Service, ServiceResponseDTO>();

        CreateMap<Booking, BookingSummaryDTO>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.Name))
            .ForMember(dest => dest.Service, opt => opt.MapFrom(src => src.Service))
            .ForMember(dest => dest.StreetAddress, opt => opt.MapFrom(src => src.StreetAddress))
            .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City))
            .ForMember(dest => dest.PostalCode, opt => opt.MapFrom(src => src.PostalCode))
            .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country))
            .ForMember(dest => dest.BookerFullName, opt => opt.MapFrom(src => $"{src.Booker.FirstName} {src.Booker.LastName}"))
            .ForMember(dest => dest.BookerPhoneNumber, opt => opt.MapFrom(src => src.Booker.PhoneNumber))
            .ForMember(dest => dest.BookerId, opt => opt.MapFrom(src => src.BookerId))
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));

        CreateMap<Service, ServiceSummaryDTO>()
            .ForMember(dest => dest.ServiceName, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description ?? ""))
            .ForMember(dest => dest.ProviderFullName, opt => opt.MapFrom(src => $"{src.Provider.FirstName} {src.Provider.LastName}"))
            .ForMember(dest => dest.ProviderPhoneNumber, opt => opt.MapFrom(src => src.Provider.PhoneNumber));
    }
}
