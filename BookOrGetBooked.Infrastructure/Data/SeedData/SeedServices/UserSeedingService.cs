using Newtonsoft.Json;
using BookOrGetBooked.Core.Models;
using System.Reflection;
using BookOrGetBooked.Shared.DTOs;

namespace BookOrGetBooked.Infrastructure.Data.SeedData.SeedServices
{
    public class UserSeedingService
    {
        /*
        private readonly ApplicationDbContext _context;

        public UserSeedingService(ApplicationDbContext context)
        {
            _context = context;
        }

        public void SeedUsersFromJson()
        {
            if (!_context.Users.Any())
            {
                var assembly = Assembly.GetExecutingAssembly();
                var resourceName = "BookOrGetBooked.Infrastructure.Data.SeedData.users.json";

                using (var stream = assembly.GetManifestResourceStream(resourceName))
                {
                    if (stream == null)
                    {
                        throw new FileNotFoundException($"Resource '{resourceName}' not found.");
                    }

                    using (var reader = new StreamReader(stream))
                    {
                        var usersJson = reader.ReadToEnd();
                        var userDtos = JsonConvert.DeserializeObject<List<UserCreateDTO>>(usersJson);

                        if (userDtos != null)
                        {
                            var users = userDtos.Select(dto =>
                                User.Create(dto.FirstName, dto.LastName, dto.Email)
                            ).ToList();

                            _context.Users.AddRange(users);
                            _context.SaveChanges();
                        }
                    }
                }
            }
        }
        */
    }
}
