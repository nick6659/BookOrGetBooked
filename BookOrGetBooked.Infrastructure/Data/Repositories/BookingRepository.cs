using BookOrGetBooked.Core.Interfaces;
using BookOrGetBooked.Core.Models;
using BookOrGetBooked.Shared.Filters;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookOrGetBooked.Infrastructure.Data.Repositories
{
    public class BookingRepository : GenericRepository<Booking>, IBookingRepository
    {
        public BookingRepository(ApplicationDbContext context) : base(context) { }

        public override async Task<Booking?> GetByIdAsync(int id)
        {
            return await Query()
                .Include(b => b.Status) // Eagerly load the Status property
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<IEnumerable<Booking>> GetBookingsAsync(BookingFilterParameters filters)
        {
            // Start building the query
            var query = Query().AsNoTracking();

            // Apply filters
            query = ApplyFilters(query, filters);

            // Add sorting (if applicable)
            query = query.OrderBy(b => b.TimeSlot);

            // Optional: Apply pagination
            if (filters.PageNumber.HasValue && filters.PageSize.HasValue)
            {
                var skip = (filters.PageNumber.Value - 1) * filters.PageSize.Value;
                query = query.Skip(skip).Take(filters.PageSize.Value);
            }

            // Execute the query and return results
            return await query.ToListAsync();
        }

        private IQueryable<Booking> ApplyFilters(IQueryable<Booking> query, BookingFilterParameters filters)
        {
            // Filter by BookerId if provided
            if (filters.BookerId.HasValue)
            {
                query = query.Where(b => b.BookerId == filters.BookerId.Value);
            }

            // Filter by ProviderId if provided
            if (filters.ProviderId.HasValue)
            {
                query = query.Where(b => b.ProviderId == filters.ProviderId.Value);
            }

            // Filter by date range
            if (filters.StartDate.HasValue)
            {
                query = query.Where(b => b.TimeSlot >= filters.StartDate.Value);
            }

            if (filters.EndDate.HasValue)
            {
                query = query.Where(b => b.TimeSlot <= filters.EndDate.Value);
            }

            // Filter by booking status
            if (filters.BookingStatusId.HasValue)
            {
                query = query.Where(b => b.Status.Id == filters.BookingStatusId.Value);
            }

            // Filter by BookingIds
            if (filters.BookingIds != null && filters.BookingIds.Any())
            {
                query = query.Where(b => filters.BookingIds.Contains(b.Id));
            }

            return query;
        }
    }
}
