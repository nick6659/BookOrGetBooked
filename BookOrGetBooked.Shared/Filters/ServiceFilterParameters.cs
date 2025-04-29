namespace BookOrGetBooked.Shared.Filters
{
    public class ServiceFilterParameters
    {
        public int? ServiceTypeId { get; set; }
        public string? ProviderId { get; set; }
        public bool? IncludeDeleted { get; set; }
        public bool? IsInactive { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
