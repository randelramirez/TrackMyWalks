using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ResolutionGroupName("MyCompanyName")]
[assembly: ExportEffect(typeof(TrackMyWalks.Droid.CustomEffects.ButtonShadowEffect), "ButtonShadowEffect")]
namespace TrackMyWalks.Droid.CustomEffects
{
    public class ButtonShadowEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            try
            {
                var control = Control as Android.Widget.Button;
                Android.Graphics.Color color = Android.Graphics.Color.Red;
                control.SetShadowLayer(12, 4, 4, color);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Cannot set property on attached control. Error: " + ex.Message);
            }
        }

        protected override void OnDetached()
        {
            //throw new NotImplementedException();
        }
    }
}