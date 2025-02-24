using BookOrGetBooked.Core.Models;
using BookOrGetBooked.Shared.DTOs;

namespace BookOrGetBooked.Core.Interfaces
{
    public interface IUserTypeService : IGenericService<UserType, UserTypeCreateDTO, UserTypeResponseDTO, UserUpdateDTO>
    {
        // Add user-type-specific methods if required
    }
}
