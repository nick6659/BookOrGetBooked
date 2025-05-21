using BookOrGetBooked.App.Shared.Interfaces;
using BookOrGetBooked.Shared.DTOs.Currency;
using System.Net.Http.Json;

namespace BookOrGetBooked.App.Client.Services
{
    public class CurrencyService : ICurrencyService
    {
        private readonly HttpClient _httpClient;

        public CurrencyService(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient(nameof(ICurrencyService));
        }

        public async Task<List<CurrencyResponseDTO>> GetAllAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<CurrencyResponseDTO>>("api/currency") ?? new();
        }
    }
}
