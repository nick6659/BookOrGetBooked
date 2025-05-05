using Newtonsoft.Json;
using BookOrGetBooked.Core.Models;
using System.Reflection;
using BookOrGetBooked.Shared.DTOs.ServiceType;

namespace BookOrGetBooked.Infrastructure.Data.SeedData.SeedServices
{
    public class ServiceTypeSeedingService
    {
        private readonly ApplicationDbContext _context;

        public ServiceTypeSeedingService(ApplicationDbContext context)
        {
            _context = context;
        }

        public void SeedServiceTypesFromJson()
        {
            if (!_context.ServiceTypes.Any())
            {
                var assembly = Assembly.GetExecutingAssembly();
                var resourceName = "BookOrGetBooked.Infrastructure.Data.SeedData.serviceTypes.json";

                using (var stream = assembly.GetManifestResourceStream(resourceName))
                {
                    if (stream == null)
                    {
                        throw new FileNotFoundException($"Resource '{resourceName}' not found.");
                    }

                    using (var reader = new StreamReader(stream))
                    {
                        var serviceTypesJson = reader.ReadToEnd();
                        var serviceTypeDtos = JsonConvert.DeserializeObject<List<ServiceTypeCreateDTO>>(serviceTypesJson);

                        if (serviceTypeDtos != null)
                        {
                            var serviceTypes = serviceTypeDtos.Select(dto =>
                                new ServiceType { Id = dto.Id, Name = dto.Name }
                            ).ToList();

                            _context.ServiceTypes.AddRange(serviceTypes);
                            _context.SaveChanges();
                        }
                    }
                }
            }
        }
    }
}
