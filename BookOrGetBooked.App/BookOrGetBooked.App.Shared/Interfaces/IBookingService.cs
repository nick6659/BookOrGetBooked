using BookOrGetBooked.Shared.DTOs.Booking;
using BookOrGetBooked.Shared.DTOs.Service;

namespace BookOrGetBooked.App.Shared.Interfaces
{
    public interface IBookingService
    {
        Task<List<ServiceResponseDTO>> GetAvailableServicesAsync();
        Task CreateBookingAsync(BookingCreateDTO booking);
    }
}
