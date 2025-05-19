using BookOrGetBooked.Infrastructure.Data;

namespace BookOrGetBooked.Core.Models
{
    public class BookingStatus
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool IsSystemDefined { get; set; }
        public string? CreatedByUserId { get; set; }
        public ApplicationUser? CreatedByUser { get; set; }
    }
}
