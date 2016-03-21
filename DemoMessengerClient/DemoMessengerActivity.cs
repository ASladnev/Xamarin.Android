using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace DemoMessengerClient
{
  [Activity (Label = "Demo Messenger Client", MainLauncher = true, Icon = "@drawable/icon")]
  public class DemoMessengerActivity : Activity
  {

    bool _isBound = false;
    Messenger _demoMessenger;
    DemoServiceConnection _demoServiceConnection;

    protected override void OnCreate (Bundle bundle)
    {
      base.OnCreate (bundle);

      SetContentView (Resource.Layout.Main);

      var buttonCallMesseger = FindViewById<Button> (Resource.Id.callMessenger);
      buttonCallMesseger.Click += (s, e) => {
        if (!_isBound) return;

        var message = Message.Obtain ();
        var mesBundle = new Bundle ();
        mesBundle.PutString ("InputText", "text from client");
        message.Data = mesBundle;
        _demoMessenger.Send (message);
      };
    }


    protected override void OnStart ()
    {
      base.OnStart ();

      var demoServiceIntent = new Intent ("com.xamarin.DemoMessengerService");
      _demoServiceConnection = new DemoServiceConnection (this);
      BindService (demoServiceIntent, _demoServiceConnection, Bind.AutoCreate);
    }


    protected override void OnDestroy ()
    {
      base.OnDestroy ();
      if (!_isBound) return;
      UnbindService (_demoServiceConnection);
      _isBound = false;
    }


    class DemoServiceConnection : Java.Lang.Object, IServiceConnection
    {

      DemoMessengerActivity _activity;

      public DemoServiceConnection (DemoMessengerActivity activity)
      {
        _activity = activity;
      }


      public void OnServiceConnected (ComponentName name, IBinder service)
      {
        _activity._demoMessenger = new Messenger (service);
        _activity._isBound = true;
      }

      public void OnServiceDisconnected (ComponentName name)
      {
        _activity._demoMessenger.Dispose ();
        _activity._demoMessenger = null;
        _activity._isBound = false;
      }
    }

  }
}

