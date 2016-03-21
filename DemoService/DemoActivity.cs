using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace DemoService
{
  [Activity (Label = "DemoService", MainLauncher = true)]
  public class DemoActivity : Activity
  {
    bool _isBound = false;
    bool _isConfigurationChange = false;
    DemoServiceBinder _binder;
    DemoServiceConnection _demoServiceConnection;

    protected override void OnCreate (Bundle bundle)
    {
      base.OnCreate (bundle);

      SetContentView (Resource.Layout.Main);

      var buttonStart = FindViewById<Button> (Resource.Id.startService);
      buttonStart.Click += (s, e) => {
        StartService (new Intent ("com.xamarin.DemoService"));
      };

      var buttonStop = FindViewById<Button> (Resource.Id.stopService);
      buttonStop.Click += (s, e) => {
        var b  = StopService (new Intent ("com.xamarin.DemoService"));
      };

      var buttonCallService = FindViewById<Button> (Resource.Id.callService);
      buttonCallService.Click += (s, e) => {
        if (_isBound) {
          RunOnUiThread (() => {
            var text = _binder.GetDemoService ().GetText ();
            Console.WriteLine ("{0} returned from DemoService", text);
          });
        }
      };

      _demoServiceConnection = LastNonConfigurationInstance as DemoServiceConnection;

      if (_demoServiceConnection != null)
        _binder = _demoServiceConnection.Binder;
    }

    protected override void OnStart ()
    {
      base.OnStart ();

      var demoServiceIntent = new Intent ("com.xamarin.DemoService");
      _demoServiceConnection = new DemoServiceConnection (this);
      BindService (demoServiceIntent, _demoServiceConnection, Bind.AutoCreate);
    }

    protected override void OnDestroy ()
    {
      base.OnDestroy ();

      if (!_isConfigurationChange) {
        if (_isBound) {
          UnbindService (_demoServiceConnection);
          _isBound = false;
        }
      }

    }


    class DemoServiceConnection : Java.Lang.Object, IServiceConnection
    {
      DemoActivity _activity;
      DemoServiceBinder _binder;

      public DemoServiceBinder Binder
      {
        get { return _binder; }
      }

      public DemoServiceConnection (DemoActivity activity)
      {
        _activity = activity;
      }


      public void OnServiceConnected (ComponentName name, IBinder service)
      {
        var demoServiceBinder = service as DemoServiceBinder;

        if (demoServiceBinder != null ) {
          _activity._binder = demoServiceBinder;
          _activity._isBound = true;
          _binder = demoServiceBinder;
        }
      }

      public void OnServiceDisconnected (ComponentName name)
      {
        _activity._isBound = false;
      }
    }

  }
}

