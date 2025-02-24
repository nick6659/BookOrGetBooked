using Newtonsoft.Json;
using BookOrGetBooked.Core.Models;
using System.Reflection;
using BookOrGetBooked.Shared.DTOs;

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
            if (!_context.Services.Any()) // Seed only if no services exist
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
                        var serviceDtos = JsonConvert.DeserializeObject<List<ServiceCreateDTO>>(servicesJson);

                        if (serviceDtos != null)
                        {
                            var services = serviceDtos.Select(dto =>
                                Service.Create(dto.Name, dto.Description, dto.Price, dto.CurrencyId, dto.ProviderId)
                            ).ToList();

                            _context.Services.AddRange(services);
                            _context.SaveChanges();
                        }
                    }
                }
            }
        }
    }
}
