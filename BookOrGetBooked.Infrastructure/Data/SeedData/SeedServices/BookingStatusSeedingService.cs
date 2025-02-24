using Newtonsoft.Json;
using BookOrGetBooked.Core.Models;
using System.Reflection;

namespace BookOrGetBooked.Infrastructure.Data.SeedData.SeedServices
{
    public class BookingStatusSeedingService
    {
        private readonly ApplicationDbContext _context;

        public BookingStatusSeedingService(ApplicationDbContext context)
        {
            _context = context;
        }

        public void SeedBookingStatusesFromJson()
        {
            if (!_context.BookingStatuses.Any())  // Seed only if no booking statuses exist
            {
                var assembly = Assembly.GetExecutingAssembly();

                var resourceName = "BookOrGetBooked.Infrastructure.Data.SeedData.bookingstatuses.json";

                using (var stream = assembly.GetManifestResourceStream(resourceName))
                {
                    if (stream == null)
                    {
                        throw new FileNotFoundException($"Resource '{resourceName}' not found.");
                    }

                    using (var reader = new StreamReader(stream))
                    {
                        var bookingStatusesJson = reader.ReadToEnd();
                        var bookingStatuses = JsonConvert.DeserializeObject<List<BookingStatus>>(bookingStatusesJson);

                        if (bookingStatuses != null)
                        {
                            _context.BookingStatuses.AddRange(bookingStatuses);
                            _context.SaveChanges();
                        }
                    }
                }
            }
        }
    }
}
