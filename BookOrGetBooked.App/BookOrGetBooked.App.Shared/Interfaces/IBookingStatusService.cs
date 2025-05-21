using BookOrGetBooked.Shared.DTOs.BookingStatus;

namespace BookOrGetBooked.App.Shared.Interfaces
{
    public interface IBookingStatusService
    {
        Task<List<BookingStatusSummaryDTO>> GetAllAsync();
    }
}
