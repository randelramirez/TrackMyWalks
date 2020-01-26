using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreLocation;
using Foundation;
using MapKit;
using ObjCRuntime;
using TrackMyWalks.iOS.CustomMapRenderer;
using TrackMyWalks.Views.MapOverlay;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Maps.iOS;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomMapOverlay), typeof(CustomMapRenderer))]
namespace TrackMyWalks.iOS.CustomMapRenderer
{
    public class CustomMapRenderer : MapRenderer
    {
        MKPolylineRenderer polylineRenderer;

        protected override void OnElementChanged(ElementChangedEventArgs<View> e)
        {
            base.OnElementChanged(e);
            // Redraw the map whenever the RouteCoordinates property has
            // changed to draw the line from PointA to PointB
            if (e.OldElement == null)
            {
                var formsMap = (CustomMapOverlay)e.NewElement;
                var nativeMap = Control as MKMapView;
                nativeMap.OverlayRenderer = GetOverlayRenderer;
                CLLocationCoordinate2D[] coords = new CLLocationCoordinate2D[
                formsMap.RouteCoordinates.Count];
                int index = 0;
                foreach (var position in formsMap.RouteCoordinates)
                {
                    coords[index] = new CLLocationCoordinate2D(
                    position.Latitude, position.Longitude);
                    index++;
                }
                var routeOverlay = MKPolyline.FromCoordinates(coords);
                nativeMap.AddOverlay(routeOverlay);
            }
        }

        // Customize the rendering of our Polyline overlay within the map
        MKOverlayRenderer GetOverlayRenderer(MKMapView mapView, IMKOverlay overlayWrapper)
        {
            if (polylineRenderer == null && !Equals(overlayWrapper, null))
            {
                var overlay = Runtime.GetNSObject(overlayWrapper.Handle) as IMKOverlay;
                polylineRenderer = new MKPolylineRenderer(overlay as MKPolyline)
                {
                    FillColor = UIColor.Red,
                    StrokeColor = UIColor.Red,
                    LineWidth = 3,
                    Alpha = 0.4f
                };
            }
            return polylineRenderer;
        }

    }
}