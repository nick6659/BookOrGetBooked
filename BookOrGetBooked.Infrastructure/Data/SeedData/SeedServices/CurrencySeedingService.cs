using Newtonsoft.Json;
using BookOrGetBooked.Core.Models;
using System.Reflection;

namespace BookOrGetBooked.Infrastructure.Data.SeedData.SeedServices
{
    public class CurrencySeedingService
    {
        private readonly ApplicationDbContext _context;

        public CurrencySeedingService(ApplicationDbContext context)
        {
            _context = context;
        }

        public void SeedCurrenciesFromJson()
        {
            if (!_context.Currencies.Any())  // Seed only if no currencies exist
            {
                var assembly = Assembly.GetExecutingAssembly();

                var resourceName = "BookOrGetBooked.Infrastructure.Data.SeedData.currencies.json";

                using (var stream = assembly.GetManifestResourceStream(resourceName))
                {
                    if (stream == null)
                    {
                        throw new FileNotFoundException($"Resource '{resourceName}' not found.");
                    }

                    using (var reader = new StreamReader(stream))
                    {
                        var currenciesJson = reader.ReadToEnd();
                        var currencies = JsonConvert.DeserializeObject<List<Currency>>(currenciesJson);

                        if (currencies != null)
                        {
                            _context.Currencies.AddRange(currencies);
                            _context.SaveChanges();
                        }
                    }
                }
            }
        }
    }
}
