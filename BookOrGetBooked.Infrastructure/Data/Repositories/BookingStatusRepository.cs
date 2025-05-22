using BookOrGetBooked.Core.Interfaces;
using BookOrGetBooked.Core.Models;

namespace BookOrGetBooked.Infrastructure.Data.Repositories
{
    public class BookingStatusRepository : GenericRepository<BookingStatus>, IBookingStatusRepository
    {
        public BookingStatusRepository(ApplicationDbContext context) : base(context) { }
    }
}
