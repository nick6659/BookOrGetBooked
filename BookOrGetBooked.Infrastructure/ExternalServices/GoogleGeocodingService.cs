using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace BookOrGetBooked.Infrastructure.ExternalServices
{
    public class GoogleGeocodingService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public GoogleGeocodingService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiKey = configuration["GoogleMaps:ApiKey"]
                ?? throw new ArgumentNullException("Google API key is missing");
        }

        public async Task<(double latitude, double longitude)> GetCoordinatesFromAddress(string address)
        {
            string url = $"https://maps.googleapis.com/maps/api/geocode/json?address={Uri.EscapeDataString(address)}&key={_apiKey}";

            HttpResponseMessage response = await _httpClient.GetAsync(url);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Google Geocoding API failed: {response.StatusCode}");
            }

            string jsonResponse = await response.Content.ReadAsStringAsync();
            var geocodeData = JsonSerializer.Deserialize<GoogleGeocodeResponse>(jsonResponse);

            if (geocodeData?.results?.Length > 0)
            {
                var location = geocodeData.results[0].geometry.location;
                return (location.lat, location.lng);
            }

            throw new Exception("Failed to retrieve geolocation data");
        }
    }

    public class GoogleGeocodeResponse
    {
        public GeocodeResult[] results { get; set; }
    }

    public class GeocodeResult
    {
        public GeocodeGeometry geometry { get; set; }
    }

    public class GeocodeGeometry
    {
        public GeocodeLocation location { get; set; }
    }

    public class GeocodeLocation
    {
        public double lat { get; set; }
        public double lng { get; set; }
    }
}
