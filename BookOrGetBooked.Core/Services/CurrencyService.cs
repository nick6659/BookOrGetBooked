using AutoMapper;
using BookOrGetBooked.Core.Interfaces;
using BookOrGetBooked.Core.Models;
using BookOrGetBooked.Shared.DTOs.Currency;

namespace BookOrGetBooked.Core.Services
{
    public class CurrencyService : GenericService<Currency, CurrencyCreateDTO, CurrencyResponseDTO, CurrencyUpdateDTO>, ICurrencyService
    {
        public CurrencyService(ICurrencyRepository currencyRepository, IMapper mapper)
            : base(currencyRepository, mapper)
        {

        }

        // Add any currency-specific logic here if needed
    }
}
