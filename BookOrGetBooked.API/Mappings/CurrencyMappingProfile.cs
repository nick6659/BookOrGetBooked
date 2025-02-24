using AutoMapper;
using BookOrGetBooked.Core.Models;
using BookOrGetBooked.Shared.DTOs;

namespace BookOrGetBooked.API.Mappings
{
    public class CurrencyMappingProfile : MappingProfileBase
    {
        public CurrencyMappingProfile()
        {
            CreateMap<Currency, CurrencyResponseDTO>();
        }
    }
}
