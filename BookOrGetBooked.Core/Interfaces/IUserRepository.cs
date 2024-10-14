using BookOrGetBooked.Core.Models;
using System.Threading.Tasks;

namespace BookOrGetBooked.Core.Interfaces
{
    public interface IUserRepository
    {
        Task<bool> UserExistsAsync(int userId);
        Task<User?> GetUserByIdAsync(int userId);
    }
}
