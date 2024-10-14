using BookOrGetBooked.Shared.DTOs;
using BookOrGetBooked.Shared.Utilities;
using System.Threading.Tasks;

namespace BookOrGetBooked.Core.Interfaces
{
    public interface IUserService
    {
        // Check if the user exists by ID
        Task<Result<bool>> UserExistsAsync(int userId);

        // Retrieve user details by ID using UserResponseDTO
        Task<Result<UserResponseDTO>> GetUserByIdAsync(int userId);
    }
}
