using BookOrGetBooked.Core.Interfaces;
using BookOrGetBooked.Core.Models;

namespace BookOrGetBooked.Infrastructure.Data.Repositories
{
    public class UserTypeRepository : GenericRepository<UserType>, IUserTypeRepository
    {
        public UserTypeRepository(ApplicationDbContext context) : base(context)
        {

        }

        // Add user-type-specific repository methods here if needed
    }
}
