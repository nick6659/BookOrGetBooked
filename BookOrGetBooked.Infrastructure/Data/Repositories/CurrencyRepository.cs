using BookOrGetBooked.Core.Interfaces;
using BookOrGetBooked.Core.Models;

namespace BookOrGetBooked.Infrastructure.Data.Repositories
{
    public class CurrencyRepository : GenericRepository<Currency>, ICurrencyRepository
    {
        public CurrencyRepository(ApplicationDbContext context) : base(context)
        {
        }

        // Add currency-specific repository methods here if needed
    }
}
