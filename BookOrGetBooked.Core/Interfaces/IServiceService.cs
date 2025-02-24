using BookOrGetBooked.Shared.DTOs;
using BookOrGetBooked.Shared.Filters;
using BookOrGetBooked.Shared.Utilities;
using System.Threading.Tasks;

namespace BookOrGetBooked.Core.Interfaces
{
    public interface IServiceService
    {
        Task<Result<bool>> ServiceExistsAsync(int serviceId);
        Task<Result<ServiceResponseDTO>> GetServiceAsync(int serviceId);
        Task<Result<IEnumerable<ServiceResponseDTO>>> GetServicesAsync(ServiceFilterParameters filters);
    }
}
