using System;
using Android.Content;
using Android.OS;
using Android.Util;

namespace Location.Droid.Services
{
  public class LocationServiceConnection : Java.Lang.Object, IServiceConnection
  {
    public event EventHandler<ServiceConnectedEventArgs> ServiceConnected = delegate { };

    public LocationServiceBinder Binder
    {
      get { return _binder; }
      set { _binder = value; }
    }

    protected LocationServiceBinder _binder; 

    public LocationServiceConnection (LocationServiceBinder binder)
    {
      if (binder == null) return;
      _binder = binder;
    }

    public void OnServiceConnected (ComponentName name, IBinder service)
    {
      var serviceBinder = service as LocationServiceBinder;
      if (serviceBinder == null) return;
      _binder = serviceBinder;
      _binder.IsBound = true;
      Log.Debug ("Location Service Connection:", "OnServiceConnected has been called");
      ServiceConnected (this, new ServiceConnectedEventArgs () { Binder = service });
      serviceBinder.Service.StartLocationUpdates ();
    }

    public void OnServiceDisconnected (ComponentName name)
    {
      _binder.IsBound = false;
      Log.Debug ("Location Service Connection:", "OnServiceDisconnected has been called, service has been unbound");
    }
  }
}