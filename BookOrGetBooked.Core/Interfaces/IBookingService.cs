using BookOrGetBooked.Shared.DTOs.Booking;
using BookOrGetBooked.Shared.Filters;
using BookOrGetBooked.Shared.Utilities;
using System.Threading.Tasks;

namespace BookOrGetBooked.Core.Interfaces
{
    public interface IBookingService
    {
        Task<Result<BookingResponseDTO>> CreateBookingAsync(BookingCreateDTO bookingRequest);
        Task<Result<BookingResponseDTO>> GetBookingAsync(int bookingId);
        Task<Result<IEnumerable<BookingResponseDTO>>> GetBookingsAsync(BookingFilterParameters filters);
        Task<Result<BookingResponseDTO>> UpdateBookingAsync(int id, BookingUpdateDTO bookingUpdateRequest);
    }
}
