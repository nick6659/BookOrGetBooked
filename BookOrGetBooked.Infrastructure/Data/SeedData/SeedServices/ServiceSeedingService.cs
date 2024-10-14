using Newtonsoft.Json;
using BookOrGetBooked.Core.Models;
using System.Reflection;

namespace BookOrGetBooked.Infrastructure.Data.SeedData.SeedServices
{
    public class ServiceSeedingService
    {
        private readonly ApplicationDbContext _context;

        public ServiceSeedingService(ApplicationDbContext context)
        {
            _context = context;
        }

        public void SeedServicesFromJson()
        {
            if (!_context.Services.Any())  // Seed only if no services exist
            {
                var assembly = Assembly.GetExecutingAssembly();
                var resourceName = "BookOrGetBooked.Infrastructure.Data.SeedData.services.json";

                using (var stream = assembly.GetManifestResourceStream(resourceName))
                {
                    if (stream == null)
                    {
                        throw new FileNotFoundException($"Resource '{resourceName}' not found.");
                    }

                    using (var reader = new StreamReader(stream))
                    {
                        var servicesJson = reader.ReadToEnd();
                        var services = JsonConvert.DeserializeObject<List<Service>>(servicesJson);

                        if (services != null)
                        {
                            _context.Services.AddRange(services);
                            _context.SaveChanges();
                        }
                    }
                }
            }
        }
    }
}
