using BookOrGetBooked.Core.Interfaces;
using BookOrGetBooked.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace BookOrGetBooked.Infrastructure.Data.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context) { }
    }
}
