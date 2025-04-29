using BookOrGetBooked.Core.Models;
using BookOrGetBooked.Shared.DTOs;

namespace BookOrGetBooked.Core.Interfaces
{
    public interface IServiceTypeService : IGenericService<ServiceType, ServiceTypeCreateDTO, ServiceTypeResponseDTO, ServiceTypeUpdateDTO>
    {
    }
}
