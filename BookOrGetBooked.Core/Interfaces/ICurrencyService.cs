using BookOrGetBooked.Core.Models;
using BookOrGetBooked.Shared.DTOs;

namespace BookOrGetBooked.Core.Interfaces
{
    public interface ICurrencyService : IGenericService<Currency, CurrencyCreateDTO, CurrencyResponseDTO, CurrencyUpdateDTO>
    {
        // Add any currency-specific service methods here, if needed
    }
}
