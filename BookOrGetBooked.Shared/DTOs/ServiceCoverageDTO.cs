namespace BookOrGetBooked.Shared.DTOs;

public class ServiceCoverageDTO
{
    public double MaxDrivingDistanceKm { get; set; } // ✅ Maximum driving distance
    public int MaxDrivingTimeMinutes { get; set; }   // ✅ Maximum driving time
    public double ProviderLatitude { get; set; }     // ✅ Provider location
    public double ProviderLongitude { get; set; }
}
