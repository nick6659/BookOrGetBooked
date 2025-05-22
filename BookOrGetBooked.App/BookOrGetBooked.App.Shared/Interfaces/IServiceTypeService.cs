using BookOrGetBooked.Shared.DTOs.ServiceType;

namespace BookOrGetBooked.App.Shared.Interfaces
{
    public interface IServiceTypeService
    {
        Task<List<ServiceTypeResponseDTO>> GetAvailableForUserAsync();
        Task<ServiceTypeResponseDTO?> CreateAsync(ServiceTypeCreateDTO dto);
    }
}
