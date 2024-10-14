using BookOrGetBooked.Core.Interfaces;
using BookOrGetBooked.Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BookOrGetBooked.Infrastructure.Data.Repositories
{
    public class ServiceRepository : IServiceRepository
    {
        private readonly ApplicationDbContext _context;

        public ServiceRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Check if a service exists by its ID
        public async Task<bool> ServiceExistsAsync(int serviceId)
        {
            return await _context.Services.AnyAsync(s => s.Id == serviceId);
        }

        // Get a service by its ID
        public async Task<Service?> GetServiceByIdAsync(int serviceId)
        {
            return await _context.Services.FindAsync(serviceId);
        }
    }
}
