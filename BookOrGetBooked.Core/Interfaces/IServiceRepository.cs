using BookOrGetBooked.Core.Models;
using System.Threading.Tasks;

namespace BookOrGetBooked.Core.Interfaces
{
    public interface IServiceRepository
    {
        Task<bool> ServiceExistsAsync(int serviceId);

        Task<Service?> GetServiceByIdAsync(int serviceId);
    }
}
