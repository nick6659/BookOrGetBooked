using BookOrGetBooked.Shared.DTOs.Booking;
using BookOrGetBooked.Shared.Filters;
using BookOrGetBooked.Shared.Utilities;
using System.Threading.Tasks;

namespace BookOrGetBooked.Core.Interfaces
{
    public interface IBookingService
    {
        Task<Result<BookingSummaryDTO>> CreateBookingAsync(BookingCreateDTO bookingRequest);
        Task<Result<BookingResponseDTO>> GetBookingAsync(int bookingId);
        Task<Result<IEnumerable<BookingSummaryDTO>>> GetBookingsAsync(BookingFilterParameters filters);
        Task<Result<BookingResponseDTO>> UpdateBookingAsync(int id, BookingUpdateDTO bookingUpdateRequest);
        Task<Result<BookingResponseDTO>> UpdateBookingByProviderAsync(int bookingId, ServiceProviderBookingUpdateDTO dto);
    }
}
