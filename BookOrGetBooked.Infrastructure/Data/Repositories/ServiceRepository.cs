using BookOrGetBooked.Core.Interfaces;
using BookOrGetBooked.Core.Models;
using Microsoft.EntityFrameworkCore;
using BookOrGetBooked.Shared.Filters;

namespace BookOrGetBooked.Infrastructure.Data.Repositories
{
    public class ServiceRepository : GenericRepository<Service>, IServiceRepository
    {
        public ServiceRepository(ApplicationDbContext context) : base(context) { }

        public async Task<IEnumerable<Service>> GetServicesAsync(ServiceFilterParameters filters)
        {
            var query = Query();

            // Include or exclude deleted services
            if (!filters.IncludeDeleted)
            {
                query = query.Where(s => !s.IsDeleted);
            }
            else
            {
                query = query.IgnoreQueryFilters();
            }

            // Filter by user
            query = query.Where(s => s.ProviderId == filters.UserId);

            // Filter by IsInactive flag
            if (filters.IsInactive.HasValue)
            {
                query = query.Where(s => s.IsInactive == filters.IsInactive.Value);
            }

            // Filter by date range
            if (filters.StartDate.HasValue)
            {
                query = query.Where(s => s.Bookings.Any(b => b.TimeSlot >= filters.StartDate.Value));
            }

            if (filters.EndDate.HasValue)
            {
                query = query.Where(s => s.Bookings.Any(b => b.TimeSlot <= filters.EndDate.Value));
            }

            return await query.ToListAsync();
        }
    }
}
