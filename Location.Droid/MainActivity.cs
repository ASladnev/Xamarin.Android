using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Content.PM;
using Android.Util;
using Android.Locations;

namespace Location.Droid
{
  [Activity (Label = "LocationDroid", MainLauncher = true,
      ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.ScreenLayout)]
    
  public class MainActivity : Activity
  {
    readonly string logTag = "MainActivity";

    TextView latText;
    TextView longText;
    TextView altText;
    TextView speedText;
    TextView bearText;
    TextView accText;

    protected override void OnCreate (Bundle bundle)
    {
      base.OnCreate (bundle);
      Log.Debug (logTag, "OnCreate: Location app is becoming active");
      SetContentView (Resource.Layout.Main);

      App.Current.LocationServiceConnection += (s, e) => {
        Log.Debug (logTag, "ServiceConnected Event Raised");
        App.Current.LocationService.LocationChanged += HandleLocationChanged;
        App.Current.LocationService.ProviderDisabled += HandleProviderDisabled;
        App.Current.LocationService.ProviderEnabled += HandleProviderEnabled;
        App.Current.LocationService.StatusChanged += HandleStatusChanged;
      };

      latText = FindViewById<TextView> (Resource.Id.lat);
      longText = FindViewById<TextView> (Resource.Id.longx);
      altText = FindViewById<TextView> (Resource.Id.alt);
      speedText = FindViewById<TextView> (Resource.Id.speed);
      bearText = FindViewById<TextView> (Resource.Id.bear);
      accText = FindViewById<TextView> (Resource.Id.acc);

      altText.Text = "altitude";
      speedText.Text = "speed";
      bearText.Text = "bearing";
      accText.Text = "accuracy";
      App.StartLocationService ();
    }

    protected override void OnPause ()
    {
      base.OnPause ();
      Log.Debug (logTag, "OnPause: Location app is moving to background");
    }


    protected override void OnResume ()
    {
      Log.Debug (logTag, "OnResume: Location app is moving into foreground");
      base.OnResume ();
    }


    protected override void OnDestroy ()
    {
      Log.Debug (logTag, "OnDestroy: Location app is becoming inactive");
      base.OnDestroy ();
      App.StopLocationService ();
    }

    private void HandleLocationChanged (object sender, LocationChangedEventArgs e)
    {
      Android.Locations.Location location = e.Location;
      Log.Debug (logTag, "Foreground updating");
      RunOnUiThread (()=> {
        latText.Text = string.Format ("Latitude: {0}", location.Latitude);
        longText.Text = string.Format ("Longitude: {0}", location.Longitude);
        altText.Text = string.Format ("Altitude: {0}", location.Altitude);
        speedText.Text = string.Format ("Speed: {0}", location.Speed);
        accText.Text = string.Format ("Accuracy: {0}", location.Accuracy);
        bearText.Text = string.Format ("Bearing: {0}", location.Bearing);
      });
    }

    private void HandleProviderDisabled (object sender, ProviderDisabledEventArgs e)
    {
      Log.Debug (logTag, "Location provider disabled event raised");
    }

    private void HandleProviderEnabled (object sender, ProviderEnabledEventArgs e)
    {
      Log.Debug (logTag, "Location provider enabled event raised");
    }

    private void HandleStatusChanged (object sender, StatusChangedEventArgs e)
    {
      Log.Debug (logTag, "Location status changed, event raised"); throw new NotImplementedException ();
    }


  }
}

