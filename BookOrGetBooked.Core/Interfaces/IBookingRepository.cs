using BookOrGetBooked.Core.Models;

namespace BookOrGetBooked.Core.Interfaces
{
    public interface IBookingRepository
    {
        Task<Booking> GetBookingByIdAsync(int bookingId);
        Task AddBookingAsync(Booking booking);
    }
}
