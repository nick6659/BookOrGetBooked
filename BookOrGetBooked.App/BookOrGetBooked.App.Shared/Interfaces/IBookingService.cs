using BookOrGetBooked.Shared.DTOs.Booking;
using BookOrGetBooked.Shared.DTOs.General;
using BookOrGetBooked.Shared.DTOs.Service;
using BookOrGetBooked.Shared.Utilities;

namespace BookOrGetBooked.App.Shared.Interfaces
{
    public interface IBookingService
    {
        Task<List<ServiceResponseDTO>> GetAvailableServicesAsync();
        Task<ResultDto<BookingSummaryDTO>> CreateBookingAsync(BookingCreateDTO booking);
        Task<List<BookingSummaryDTO>> GetBookingsForServiceAsync(int serviceId);
        Task<ResultDto<BookingSummaryDTO>> UpdateBookingAsync(int id, BookingUpdateDTO updateDto);
        Task UpdateBookingAsProviderAsync(int bookingId, ServiceProviderBookingUpdateDTO updateDto);

    }
}
