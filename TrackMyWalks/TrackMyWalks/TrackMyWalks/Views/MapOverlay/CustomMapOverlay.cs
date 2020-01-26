using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.Maps;

namespace TrackMyWalks.Views.MapOverlay
{
    public class CustomMapOverlay : Map
    {
        public List<Position> RouteCoordinates { get; set; }
        public CustomMapOverlay()
        {
            RouteCoordinates = new List<Position>();
        }
    }
}
