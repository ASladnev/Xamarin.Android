using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Locations;
using Android.Runtime;
using Android.Util;

namespace Location.Droid.Services
{
  [Service]
  public class LocationService : Service, ILocationListener
  {

    #region Events
    public event EventHandler<LocationChangedEventArgs> LocationChanged = delegate { };
    public event EventHandler<ProviderDisabledEventArgs> ProviderDisabled = delegate { };
    public event EventHandler<ProviderEnabledEventArgs> ProviderEnabled = delegate { };
    public event EventHandler<StatusChangedEventArgs> StatusChanged = delegate { };
    #endregion

    private const string logTag = "Location Service: ";

    //constructor
    public LocationService ()
    {

    }

    private LocationManager _locationManager = Application.Context.GetSystemService (Context.LocationService) as LocationManager;  

 
    #region Lifecycle methods

    public override void OnCreate ()
    {
      base.OnCreate ();
      Log.Debug (logTag, "OnCreate has been called");
    }

    public override StartCommandResult OnStartCommand (Intent intent, StartCommandFlags flags, int startId)
    {
      Log.Debug (logTag, "Service has started");
      return StartCommandResult.Sticky;
    }

    private IBinder _binder;

    public override IBinder OnBind (Intent intent)
    {
      Log.Debug (logTag, "A client now has bound to the service");
      _binder = new LocationServiceBinder (this);
      return _binder;
    }

    public override void OnDestroy ()
    {
      base.OnDestroy ();
      Log.Debug (logTag, "Service has been terminated");
      _locationManager.RemoveUpdates (this);
    }

    #endregion

    public void StartLocationUpdates ()
    {
      //we can set different location criteria based on requirements for our app -
      //for example, we might want to preserve power, or get extreme accuracy
      var locationCriteria = new Criteria ();

      locationCriteria.Accuracy = Accuracy.Fine;
      locationCriteria.PowerRequirement = Power.Medium;

      // get provider: GPS, Network, etc.
      var locationProvider = _locationManager.GetBestProvider (locationCriteria, true);
      Log.Debug (logTag, string.Format ("You are about to get location updates via {0}", locationProvider));

      // Get an initial fix on location
      _locationManager.RequestLocationUpdates (locationProvider, 2000, 0, this);

      Log.Debug (logTag, "Now sending location updates");
    }

    #region implementation of ILocationListner

    public void OnLocationChanged (Android.Locations.Location location)
    {
      LocationChanged (this, new LocationChangedEventArgs (location));

      Log.Debug (logTag, $"Latitude is {location.Latitude}");
      Log.Debug (logTag, $"Longitude is {location.Longitude}");
      Log.Debug (logTag, $"Altitude is {location.Altitude}");
      Log.Debug (logTag, $"Speed is {location.Speed}");
      Log.Debug (logTag, $"Accuracy is {location.Accuracy}");
      Log.Debug (logTag, $"Bearing is {location.Bearing}");
    }

    public void OnProviderDisabled (string provider)
    {
      ProviderDisabled (this, new ProviderDisabledEventArgs (provider));
    }

    public void OnProviderEnabled (string provider)
    {
      ProviderEnabled (this, new ProviderEnabledEventArgs (provider));
    }

    public void OnStatusChanged (string provider, [GeneratedEnum] Availability status, Bundle extras)
    {
      StatusChanged (this, new StatusChangedEventArgs (provider, status, extras));
    }

    #endregion
  }
}