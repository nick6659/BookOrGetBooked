using BookOrGetBooked.Core.Interfaces;
using BookOrGetBooked.Core.Models;
using Microsoft.EntityFrameworkCore;
using BookOrGetBooked.Shared.Filters;
using BookOrGetBooked.Shared.Utilities;

namespace BookOrGetBooked.Infrastructure.Data.Repositories
{
    public class ServiceRepository : GenericRepository<Service>, IServiceRepository
    {
        public ServiceRepository(ApplicationDbContext context) : base(context) { }

        public async Task<IEnumerable<Service>> GetServicesAsync(ServiceFilterParameters filters)
        {
            var query = Query()
                .Include(s => s.ServiceType)
                .Include(s => s.Currency)
                .Include(s => s.ServiceCoverage)
                .AsQueryable();

            if (filters.IncludeDeleted.HasValue)
            {
                if (!filters.IncludeDeleted.Value)
                {
                    query = query.Where(s => !s.IsDeleted);
                }
                else
                {
                    query = query.IgnoreQueryFilters();
                }
            }

            if (filters.ServiceTypeId.HasValue)
            {
                query = query.Where(s => s.ServiceTypeId == filters.ServiceTypeId.Value);
            }

            if (!string.IsNullOrWhiteSpace(filters.ProviderId))
            {
                query = query.Where(s => s.ProviderId == filters.ProviderId);
            }

            if (filters.IsInactive.HasValue)
            {
                query = query.Where(s => s.IsInactive == filters.IsInactive.Value);
            }

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

        public async Task<IEnumerable<Service>> GetServicesWithinDistanceAsync(ServiceFilterParameters filters, double userLat, double userLon)
        {
            var services = await GetServicesAsync(filters);

            var filteredServices = services
                .Where(service =>
                    service.ServiceCoverage != null &&
                    DistanceHelper.CalculateDistanceKm(
                        service.ServiceCoverage.ProviderLatitude,
                        service.ServiceCoverage.ProviderLongitude,
                        userLat, userLon
                    ) <= service.ServiceCoverage.MaxDrivingDistanceKm
                )
                .ToList();

            return filteredServices;
        }

        public override async Task<IEnumerable<Service>> GetAllAsync()
        {
            return await Query()
                .Include(s => s.ServiceType)
                .Include(s => s.Currency)
                .ToListAsync();
        }

    }
}
