
namespace BookOrGetBooked.Infrastructure.Data.SeedData.SeedServices
{
    public class DataSeederService
    {
        private readonly ApplicationDbContext _context;

        public DataSeederService(ApplicationDbContext context)
        {
            _context = context;
        }

        public void SeedAll()
        {
            // Seed currencies
            var currencySeedingService = new CurrencySeedingService(_context);
            currencySeedingService.SeedCurrenciesFromJson();

            // Seed users
            var userSeedingService = new UserSeedingService(_context);
            userSeedingService.SeedUsersFromJson();

            // Seed services
            var serviceSeedingService = new ServiceSeedingService(_context);
            serviceSeedingService.SeedServicesFromJson();

            // Add more seeding services as needed
        }
    }

}
