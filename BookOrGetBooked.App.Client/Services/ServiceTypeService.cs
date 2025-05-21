using BookOrGetBooked.App.Shared.Interfaces;
using BookOrGetBooked.Shared.DTOs.ServiceType;
using System.Net.Http.Json;

namespace BookOrGetBooked.App.Client.Services
{
    public class ServiceTypeService : IServiceTypeService
    {
        private readonly HttpClient _httpClient;

        public ServiceTypeService(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient(nameof(IServiceTypeService));
        }

        public async Task<List<ServiceTypeResponseDTO>> GetAvailableForUserAsync(string userId)
        {
            return await _httpClient.GetFromJsonAsync<List<ServiceTypeResponseDTO>>($"api/servicetype/available?userId={userId}") ?? new();
        }

        public async Task<ServiceTypeResponseDTO?> CreateAsync(ServiceTypeCreateDTO dto)
        {
            var response = await _httpClient.PostAsJsonAsync("api/servicetype", dto);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<ServiceTypeResponseDTO>();
            }
            return null;
        }
    }
}
