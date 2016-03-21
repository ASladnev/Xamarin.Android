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
using Location.Droid.Services;
using Android.Util;
using System.Threading.Tasks;

namespace Location.Droid
{
  public class App
  {
    public event EventHandler<ServiceConnectedEventArgs> LocationServiceConnection = delegate { };

    protected readonly string logTag = "App";
    protected static LocationServiceConnection _locationServiceConnection;

    public static App Current
    {
      get { return _current;}
    }

    private static App _current;

    public LocationService LocationService
    {
      get {
        if (_locationServiceConnection.Binder == null)
          throw new Exception ("Service isn't bound to app yet");
        return _locationServiceConnection.Binder.Service;
      }
    }

    static App ()
    {
      _current = new App ();
    }

    protected App ()
    {
      _locationServiceConnection = new LocationServiceConnection (null);
      _locationServiceConnection.ServiceConnected += (s, e) => {
        Log.Debug (logTag, "Service Connected");
        LocationServiceConnection (this, e);
      };
    }

    public static void StartLocationService ()
    {
      new Task (() => {
        Log.Debug ("App", "Calling StartService");
        Application.Context.StartService (new Intent (Application.Context, typeof (LocationService)));
        var locationServiceIntent = new Intent (Application.Context, typeof (LocationService));
        Log.Debug ("App", "Calling service binding");
        Application.Context.BindService (locationServiceIntent, _locationServiceConnection, Bind.AutoCreate);
      }).Start (); 
    }

    public static void StopLocationService ()
    {
      Log.Debug ("App", "StopLocationService");
      if (_locationServiceConnection != null) {
        Log.Debug ("App", "Unbinding from LocationService");
        Application.Context.UnbindService (_locationServiceConnection);
      }

      if (Current.LocationService != null) {
        Log.Debug ("App", "Stopping the LocationService");
        Current.LocationService.StopSelf ();
      }
    }
  }
}