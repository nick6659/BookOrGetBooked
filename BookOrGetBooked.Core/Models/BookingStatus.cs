namespace BookOrGetBooked.Core.Models
{
    public class BookingStatus
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool IsSystemDefined { get; set; }
        public int? CreatedByUserId { get; set; }
        public User? CreatedByUser { get; set; }
    }
}
