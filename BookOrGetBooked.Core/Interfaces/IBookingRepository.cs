using BookOrGetBooked.Core.Models;
using BookOrGetBooked.Shared.Filters;

namespace BookOrGetBooked.Core.Interfaces
{
    public interface IBookingRepository : IGenericRepository<Booking>
    {
        Task<IEnumerable<Booking>> GetBookingsAsync(BookingFilterParameters filters);
    }
}
