using Newtonsoft.Json;
using BookOrGetBooked.Core.Models;
using System.Reflection;

namespace BookOrGetBooked.Infrastructure.Data.SeedData.SeedServices
{
    public class UserTypeSeedingService
    {
        private readonly ApplicationDbContext _context;

        public UserTypeSeedingService(ApplicationDbContext context)
        {
            _context = context;
        }

        public void SeedUserTypesFromJson()
        {
            if (!_context.UserTypes.Any()) // Seed only if no user types exist
            {
                var assembly = Assembly.GetExecutingAssembly();
                var resourceName = "BookOrGetBooked.Infrastructure.Data.SeedData.usertypes.json";

                using (var stream = assembly.GetManifestResourceStream(resourceName))
                {
                    if (stream == null)
                    {
                        throw new FileNotFoundException($"Resource '{resourceName}' not found.");
                    }

                    using (var reader = new StreamReader(stream))
                    {
                        var userTypesJson = reader.ReadToEnd();
                        var userTypes = JsonConvert.DeserializeObject<List<UserType>>(userTypesJson);

                        if (userTypes != null)
                        {
                            _context.UserTypes.AddRange(userTypes);
                            _context.SaveChanges();
                        }
                    }
                }
            }
        }
    }
}
