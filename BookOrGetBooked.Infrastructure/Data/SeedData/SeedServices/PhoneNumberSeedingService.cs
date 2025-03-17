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
                            // Fetch all users
                            var users = _context.Users.ToList();

                            var phoneNumbers = phoneNumberDtos.Select((dto, index) =>
                            {
                                // Assign phone numbers to users in order (you may want a better way)
                                var user = users.ElementAtOrDefault(index);
                                if (user == null)
                                {
                                    throw new InvalidOperationException($"Not enough users in the database for phone numbers.");
                                }

                                // Use factory method to create the PhoneNumber
                                var phoneNumber = PhoneNumber.Create(dto.Prefix, dto.Number);
                                user.PhoneNumbers.Add(phoneNumber); // Associate phone number with user
                                return phoneNumber;
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
