
using Microsoft.AspNetCore.Identity;

namespace BookOrGetBooked.Infrastructure.Data.SeedData.SeedServices
{
    public class DataSeederService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public DataSeederService(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public void SeedAll()
        {
            var providerUserId = SeedDefaultUser();

            // Seed currencies
            var currencySeedingService = new CurrencySeedingService(_context);
            currencySeedingService.SeedCurrenciesFromJson();

            // Seed users
            //var userSeedingService = new UserSeedingService(_context);
            //userSeedingService.SeedUsersFromJson();

            // Seed phone numbers after users
            //var phoneNumberSeedingService = new PhoneNumberSeedingService(_context);
            //phoneNumberSeedingService.SeedPhoneNumbersFromJson();

            // Seed service types
            var serviceTypeSeedingService = new ServiceTypeSeedingService(_context);
            serviceTypeSeedingService.SeedServiceTypesFromJson();

            // Seed services
            var serviceSeedingService = new ServiceSeedingService(_context);
            serviceSeedingService.SeedServicesFromJson();

            // Seed booking statuses
            var bookingStatusSeeder = new BookingStatusSeedingService(_context);
            bookingStatusSeeder.SeedBookingStatusesFromJson();

            // Add more seeding services as needed
        }

        private string SeedDefaultUser()
        {
            var existingUser = _userManager.FindByEmailAsync("admin@example.com").Result;

            if (existingUser == null)
            {
                var newUser = new ApplicationUser
                {
                    Id = "b5d466dd-128d-426b-93ac-1177fe1f26fe", // 👈 Matches ProviderId in services.json
                    UserName = "admin",
                    Email = "admin@example.com",
                    EmailConfirmed = true
                };

                var result = _userManager.CreateAsync(newUser, "Admin@123").Result;
                if (!result.Succeeded)
                {
                    throw new Exception("Failed to create default admin user.");
                }

                return newUser.Id;
            }

            return existingUser.Id;
        }
    }

}
