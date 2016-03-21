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
using Android.Util;
using System.Threading;

namespace DemoService
{

  [Service]
  [IntentFilter (new string[] { "com.xamarin.DemoService" })]
  public class DemoService : Service
  {
    DemoServiceBinder _binder;

    public override StartCommandResult OnStartCommand (Intent intent, StartCommandFlags flags, int startId)
    {
      Log.Debug ("DemoService", "DevoService started " + startId);
      StartServiceInForeground ();
      DoWork ();
      return StartCommandResult.NotSticky;
    }

    private void StartServiceInForeground ()
    {
      var onGoing = new Notification (Resource.Drawable.icon, "DemoService in foreground now");
      var pendingIntent = PendingIntent.GetActivity (this, 0, new Intent (this, typeof (DemoActivity)), 0);
      onGoing.SetLatestEventInfo (this, "DemoService", "DemoService is running in the foreground", pendingIntent);
      StartForeground ((int)NotificationFlags.ForegroundService, onGoing);
    }

    public override void OnDestroy ()
    {
      base.OnDestroy ();
      Log.Debug ("DemoService", "DemoService is stopped");
    }


    private void DoWork ()
    {
      Toast.MakeText (this, "The demo service has started", ToastLength.Long).Show ();

      var thread = new Thread (() => {
        SendNotification ();
        Thread.Sleep (5000);
        Log.Debug ("DemoService", "Stopping foreground");
        StopForeground (true);
        StopSelf ();
      });

      thread.Start ();
    }

    private void SendNotification ()
    {
      var nManager = (NotificationManager)GetSystemService (NotificationService);
      var notification = new Notification (Resource.Drawable.icon, "Message from demo service");
      var pendingIntent = PendingIntent.GetActivity (this, 0, new Intent (this, typeof (DemoActivity)), 0);
      notification.SetLatestEventInfo (this, "Demo Service Notification", "Message from demo service", pendingIntent);
      nManager.Notify (0, notification);
    }

    public override IBinder OnBind (Intent intent)
    {
      _binder = new DemoServiceBinder (this);
      return _binder;
    }


    public string GetText ()
    {
      return "Some text from the service";
    }
  }

  public class DemoServiceBinder : Binder
  {
    DemoService _demoService;

    public DemoServiceBinder (DemoService demoService)
    {
      _demoService = demoService;
    }


    public DemoService GetDemoService ()
    {
      return _demoService;
    }

  }
 
}