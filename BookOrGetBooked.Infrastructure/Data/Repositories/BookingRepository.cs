using BookOrGetBooked.Core.Interfaces;
using BookOrGetBooked.Core.Models;
using BookOrGetBooked.Shared.Filters;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookOrGetBooked.Infrastructure.Data.Repositories;

public class BookingRepository(
    ApplicationDbContext _context
    ) : GenericRepository<Booking>(_context), IBookingRepository
{
    public override async Task<Booking?> GetByIdAsync(int id)
    {
        return await Query()
            .Include(b => b.Status)
            .Include(b => b.Service)
                .ThenInclude(s => s!.Provider)
            .Include(b => b.Booker)
            .FirstOrDefaultAsync(b => b.Id == id);
    }

    public async Task<IEnumerable<Booking>> GetBookingsAsync(BookingFilterParameters filters)
    {
        // Start building the query
        var query = Query()
            .Include(b => b.Status)
            .Include(b => b.Service)
            .ThenInclude(s => s!.Provider)
            .Include(b => b.Booker)
            .AsNoTracking();

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

    private static IQueryable<Booking> ApplyFilters(IQueryable<Booking> query, BookingFilterParameters filters)
    {
        // Filter by BookerId if provided
        if (!string.IsNullOrEmpty(filters.BookerId))
        {
            query = query.Where(b => b.BookerId == filters.BookerId);
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
        if (filters.BookingIds != null && filters.BookingIds.Count > 0)
        {
            query = query.Where(b => filters.BookingIds.Contains(b.Id));
        }

        if (filters.ServiceId.HasValue)
        {
            query = query.Where(b => b.ServiceId == filters.ServiceId.Value);
        }

        return query;
    }
}