using BookOrGetBooked.Core.Interfaces;
using BookOrGetBooked.Core.Models;

namespace BookOrGetBooked.Infrastructure.Data.Repositories
{
    public class ServiceTypeRepository : GenericRepository<ServiceType>, IServiceTypeRepository
    {
        public ServiceTypeRepository(ApplicationDbContext context) : base(context) { }
    }
}
