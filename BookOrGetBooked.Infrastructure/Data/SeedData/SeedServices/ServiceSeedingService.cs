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
            if (!_context.Services.Any())
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
                                Service.Create(dto.Name, dto.Description, dto.ServiceTypeId, dto.Price, dto.CurrencyId, "b5d466dd-128d-426b-93ac-1177fe1f26fe")
                            ).ToList();

                            Console.WriteLine($"Seeding {services.Count} services...");

                            _context.Services.AddRange(services);
                            _context.SaveChanges();

                            Console.WriteLine("Services seeding completed.");
                        }
                    }
                }
            }
        }
    }
}
