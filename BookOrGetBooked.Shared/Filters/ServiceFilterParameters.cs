namespace BookOrGetBooked.Shared.Filters
{
    public class ServiceFilterParameters
    {
        public int UserId { get; set; }
        public bool IncludeDeleted { get; set; } = false;
        public bool? IsInactive { get; set; } = null;
        public DateTime? StartDate { get; set; } = null;
        public DateTime? EndDate { get; set; } = null;
    }
}
