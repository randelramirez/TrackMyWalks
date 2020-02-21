using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreGraphics;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportEffect(typeof(TrackMyWalks.iOS.CustomEffects.LabelShadowEffect), "LabelShadowEffect")]
namespace TrackMyWalks.iOS.CustomEffects
{
    public class LabelShadowEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            try
            {
                Control.Layer.CornerRadius = 5;
                Control.Layer.ShadowColor = Color.Black.ToCGColor();
                Control.Layer.ShadowOffset = new CGSize(4, 4);
                Control.Layer.ShadowOpacity = 0.5f;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Cannot set property on attached control. Error: " + ex.Message);
            }
        }
        protected override void OnDetached()
        {
        }
    }
}