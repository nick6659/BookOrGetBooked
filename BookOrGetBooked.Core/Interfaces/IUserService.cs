using BookOrGetBooked.Core.Models;
using BookOrGetBooked.Shared.DTOs;

namespace BookOrGetBooked.Core.Interfaces
{
    public interface IUserService : IGenericService<User, UserCreateDTO, UserResponseDTO, UserUpdateDTO>
    {
        
    }
}
