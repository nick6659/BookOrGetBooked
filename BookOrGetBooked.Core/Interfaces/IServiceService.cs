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
        Task<Result<IEnumerable<ServiceResponseDTO>>> GetServicesWithinDistanceAsync(ServiceFilterParameters filters, double userLat, double userLon);
        Task<Result<IEnumerable<ServiceResponseDTO>>> GetAllServicesAsync();
    }
}
