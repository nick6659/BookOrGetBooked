
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

            // Seed user types first, as users depend on them
            var userTypeSeedingService = new UserTypeSeedingService(_context);
            userTypeSeedingService.SeedUserTypesFromJson();

            // Seed users
            var userSeedingService = new UserSeedingService(_context);
            userSeedingService.SeedUsersFromJson();

            // Seed phone numbers after users
            var phoneNumberSeedingService = new PhoneNumberSeedingService(_context);
            phoneNumberSeedingService.SeedPhoneNumbersFromJson();

            // Seed services
            var serviceSeedingService = new ServiceSeedingService(_context);
            serviceSeedingService.SeedServicesFromJson();

            // Seed booking statuses
            var bookingStatusSeeder = new BookingStatusSeedingService(_context);
            bookingStatusSeeder.SeedBookingStatusesFromJson();

            // Add more seeding services as needed
        }
    }

}
