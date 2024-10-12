using BookOrGetBooked.Shared.DTOs;
using System.Threading.Tasks;

namespace BookOrGetBooked.Core.Interfaces
{
    public interface IBookingService
    {
        // Return BookingResponseDTO in the interface
        Task<BookingResponseDTO> CreateBookingAsync(BookingRequestDTO booking);
        Task<BookingResponseDTO> GetBookingByIdAsync(int bookingId);
    }
}
