using BookOrGetBooked.Core.Models;
using BookOrGetBooked.Shared.Filters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookOrGetBooked.Core.Interfaces
{
    public interface IServiceRepository : IGenericRepository<Service>
    {
        Task<IEnumerable<Service>> GetServicesAsync(ServiceFilterParameters filters);
        Task<IEnumerable<Service>> GetServicesWithinDistanceAsync(ServiceFilterParameters filters, double userLat, double userLon);
    }
}
