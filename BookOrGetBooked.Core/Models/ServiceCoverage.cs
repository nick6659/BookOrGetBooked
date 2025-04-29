namespace BookOrGetBooked.Core.Models;

public class ServiceCoverage
{
    public int Id { get; set; }
    public int ServiceId { get; set; }
    public double MaxDrivingDistanceKm { get; set; } // Provider covers up to X km of driving
    public int MaxDrivingTimeMinutes { get; set; }  // Provider covers up to Y minutes of driving
    public double ProviderLatitude { get; set; }
    public double ProviderLongitude { get; set; }

    public Service Service { get; set; } = null!;
}
