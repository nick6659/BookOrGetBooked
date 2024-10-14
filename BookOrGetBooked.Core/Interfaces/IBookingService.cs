using BookOrGetBooked.Shared.DTOs;
using BookOrGetBooked.Shared.Utilities;
using System.Threading.Tasks;

namespace BookOrGetBooked.Core.Interfaces
{
    public interface IBookingService
    {
        Task<Result<BookingResponseDTO>> CreateBookingAsync(BookingRequestDTO bookingRequest);
        Task<Result<BookingResponseDTO>> GetBookingByIdAsync(int bookingId);
    }
}
