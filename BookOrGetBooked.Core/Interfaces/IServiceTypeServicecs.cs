using BookOrGetBooked.Core.Models;
using BookOrGetBooked.Shared.DTOs.ServiceType;

namespace BookOrGetBooked.Core.Interfaces
{
    public interface IServiceTypeService : IGenericService<ServiceType, ServiceTypeCreateDTO, ServiceTypeResponseDTO, ServiceTypeUpdateDTO>
    {
        Task<List<ServiceTypeResponseDTO>> GetAvailableForUserAsync(string userId);
    }
}
