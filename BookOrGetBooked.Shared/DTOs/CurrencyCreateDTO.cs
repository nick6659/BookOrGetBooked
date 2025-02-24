namespace BookOrGetBooked.Shared.DTOs
{
    public class CurrencyCreateDTO
    {
        public required string Code { get; set; }  // ISO 4217 currency code, e.g., USD, EUR
        public required string Name { get; set; }  // Full name of the currency, e.g., United States Dollar
        public required string Symbol { get; set; }  // Currency symbol, e.g., $, €, £
        public bool IsEnabled { get; set; } = true;  // Allow enabling/disabling currencies
    }
}
