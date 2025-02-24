using Newtonsoft.Json;
using BookOrGetBooked.Core.Models;
using System.Reflection;
using BookOrGetBooked.Shared.DTOs;

namespace BookOrGetBooked.Infrastructure.Data.SeedData.SeedServices
{
    public class PhoneNumberSeedingService
    {
        private readonly ApplicationDbContext _context;

        public PhoneNumberSeedingService(ApplicationDbContext context)
        {
            _context = context;
        }

        public void SeedPhoneNumbersFromJson()
        {
            if (!_context.Set<PhoneNumber>().Any()) // Seed only if no phone numbers exist
            {
                var assembly = Assembly.GetExecutingAssembly();
                var resourceName = "BookOrGetBooked.Infrastructure.Data.SeedData.phonenumbers.json";

                using (var stream = assembly.GetManifestResourceStream(resourceName))
                {
                    if (stream == null)
                    {
                        throw new FileNotFoundException($"Resource '{resourceName}' not found.");
                    }

                    using (var reader = new StreamReader(stream))
                    {
                        var phoneNumbersJson = reader.ReadToEnd();
                        var phoneNumberDtos = JsonConvert.DeserializeObject<List<PhoneNumberCreateDTO>>(phoneNumbersJson);

                        if (phoneNumberDtos != null)
                        {
                            var phoneNumbers = phoneNumberDtos.Select(dto =>
                            {
                                // Ensure that the UserId exists in the Users table
                                var userExists = _context.Users.Any(u => u.Id == dto.UserId);
                                if (!userExists)
                                {
                                    throw new InvalidOperationException(
                                        $"User with ID {dto.UserId} does not exist in the Users table."
                                    );
                                }

                                // Use factory method to create the PhoneNumber
                                return PhoneNumber.Create(dto.Prefix, dto.Number, dto.UserId);
                            }).ToList();

                            _context.Set<PhoneNumber>().AddRange(phoneNumbers);
                            _context.SaveChanges();
                        }
                    }
                }
            }
        }
    }
}
