using BookOrGetBooked.Shared.DTOs.Service;

namespace BookOrGetBooked.App.Shared.Interfaces
{
    public interface IServiceService
    {
        Task<List<ServiceResponseDTO>> GetServicesByProviderAsync(string providerId);
        Task<bool> CreateServiceAsync(ServiceCreateDTO dto);

    }
}
