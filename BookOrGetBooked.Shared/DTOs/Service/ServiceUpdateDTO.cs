namespace BookOrGetBooked.Shared.DTOs.Service
{
    public class ServiceUpdateDTO
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public required decimal Price { get; set; }
        public required int CurrencyId { get; set; }
        public required int ProviderId { get; set; }
    }
}
