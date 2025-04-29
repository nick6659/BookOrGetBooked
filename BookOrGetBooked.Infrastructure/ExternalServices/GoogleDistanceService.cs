using BookOrGetBooked.Core.Interfaces;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace BookOrGetBooked.Infrastructure.ExternalServices
{
    public class GoogleDistanceService : IGoogleDistanceService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public GoogleDistanceService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiKey = configuration["GoogleMaps:ApiKey"]
                ?? throw new ArgumentNullException("Google API key is missing");
        }

        public async Task<(double distanceKm, int durationMinutes)> GetDrivingDistanceAsync(
            double originLat, double originLon, double destinationLat, double destinationLon)
        {
            string url = $"https://maps.googleapis.com/maps/api/distancematrix/json?origins={originLat},{originLon}&destinations={destinationLat},{destinationLon}&key={_apiKey}";

            HttpResponseMessage response = await _httpClient.GetAsync(url);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Google Distance Matrix API failed: {response.StatusCode}");
            }

            string jsonResponse = await response.Content.ReadAsStringAsync();
            var distanceData = JsonSerializer.Deserialize<GoogleDistanceResponse>(jsonResponse);

            if (distanceData?.rows?[0]?.elements?[0]?.status == "OK")
            {
                double distanceMeters = distanceData.rows[0].elements[0].distance.value;
                int durationSeconds = distanceData.rows[0].elements[0].duration.value;

                return (distanceMeters / 1000, durationSeconds / 60);
            }

            throw new Exception("Failed to retrieve distance data");
        }
    }

    public class GoogleDistanceResponse
    {
        public GoogleDistanceRow[] rows { get; set; }
    }

    public class GoogleDistanceRow
    {
        public GoogleDistanceElement[] elements { get; set; }
    }

    public class GoogleDistanceElement
    {
        public DistanceValue distance { get; set; }
        public DurationValue duration { get; set; }
        public string status { get; set; }
    }

    public class DistanceValue
    {
        public double value { get; set; }
    }

    public class DurationValue
    {
        public int value { get; set; }
    }
}
