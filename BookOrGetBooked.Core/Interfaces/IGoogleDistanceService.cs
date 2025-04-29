using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookOrGetBooked.Core.Interfaces
{
    public interface IGoogleDistanceService
    {
        Task<(double distanceKm, int durationMinutes)> GetDrivingDistanceAsync(
            double originLat, double originLon, double destinationLat, double destinationLon);
    }
}
