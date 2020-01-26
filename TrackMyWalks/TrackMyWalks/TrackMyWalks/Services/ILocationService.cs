using Plugin.Geolocator.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TrackMyWalks.Services
{
    public interface ILocationService
    {
        // Asynchronously gets the current GPS location from the device.
        Task<Position> GetCurrentPosition();
        // Asynchronously listens for changes in the GPS coordinates
        Task StartListening();
        // Stops listening for changes in GPS location updates
        void StopListening();
    }
}
