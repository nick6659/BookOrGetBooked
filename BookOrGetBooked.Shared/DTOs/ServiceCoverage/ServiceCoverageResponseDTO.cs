namespace BookOrGetBooked.Shared.DTOs.ServiceCoverage;

public class ServiceCoverageResponseDTO
{
    public double MaxDrivingDistanceKm { get; set; }
    public int MaxDrivingTimeMinutes { get; set; }
    public double ProviderLatitude { get; set; }
    public double ProviderLongitude { get; set; }
}
