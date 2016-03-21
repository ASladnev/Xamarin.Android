using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Locations;

namespace LocationDemo
{
  [Activity (Label = "LocationDemo", MainLauncher = true, Icon = "@drawable/icon")]
  public class MainActivity : Activity, ILocationListener
  {

    private LocationManager _locationManager;

    private Button _button;

    protected override void OnCreate (Bundle bundle)
    {
      base.OnCreate (bundle);

      // Set our view from the "main" layout resource
      SetContentView (Resource.Layout.Main);

      _locationManager = GetSystemService (Context.LocationService) as LocationManager;
      _button = FindViewById<Button> (Resource.Id.MyButton);
    }

    protected override void OnResume ()
    {
      base.OnResume ();

      var locationCriteria = new Criteria ();
      locationCriteria.Accuracy = Accuracy.Fine;
      locationCriteria.PowerRequirement = Power.High;
      var locationProvider = _locationManager.GetBestProvider (locationCriteria, true);
      _locationManager.RequestLocationUpdates (locationProvider, 1000, 0, this);
      _button.Text = "Навигация началась!!!";
    }

    protected override void OnPause ()
    {
      base.OnPause ();
      _locationManager.RemoveUpdates (this);
      _button.Text = "Стоп навигация!";
    }

    public void OnLocationChanged (Location location)
    {
      _button.Text = "OnLocationChanged " + location.Altitude;

    }

    public void OnProviderDisabled (string provider)
    {
      _button.Text = "OnProviderDisabled";
    }

    public void OnProviderEnabled (string provider)
    {
      _button.Text = "OnProviderDisabled";
    }

    public void OnStatusChanged (string provider, [GeneratedEnum] Availability status, Bundle extras)
    {
      _button.Text = "OnStatusChanged";
    }


  }
}

