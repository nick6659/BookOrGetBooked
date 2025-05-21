using BookOrGetBooked.Shared.DTOs.Currency;

namespace BookOrGetBooked.App.Shared.Interfaces
{
    public interface ICurrencyService
    {
        Task<List<CurrencyResponseDTO>> GetAllAsync();
    }
}
