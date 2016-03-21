using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Util;
using Android.Widget;

namespace StockService
{
  [Activity (Label = "Stock Service Demo", MainLauncher = true, Icon = "@drawable/Icon")]
  public class StockActivity : ListActivity
  {
    public bool _isBound = false;
    public StockServiceBinder _binder;
    StockServiceConnection _stockServiceConnection;
    StockReceiver _stockReceiver;
    Intent _stockServiceIntent;

    protected override void OnCreate (Bundle bundle)
    {
      base.OnCreate (bundle);

      _stockServiceIntent = new Intent ("com.xamarin.StockService");
      _stockReceiver = new StockReceiver ();
    }

    protected override void OnStart ()
    {
      base.OnStart ();

      var intentFilter = new IntentFilter (StockService.StockUpdatedAction) { Priority = (int)IntentFilterPriority.HighPriority };
      RegisterReceiver (_stockReceiver, intentFilter);
      _stockServiceConnection = new StockServiceConnection (this);
      BindService (_stockServiceIntent, _stockServiceConnection, Bind.AutoCreate);
      ScheduleStockUpdates ();
    }

    protected override void OnStop ()
    {
      base.OnStop ();

      if (_isBound) {
        UnbindService (_stockServiceConnection);
        _isBound = false;
      }
      UnregisterReceiver (_stockReceiver);
    }

    private void ScheduleStockUpdates ()
    {
      if (!IsAlarmSet ()) {
        var alarm = (AlarmManager)GetSystemService (AlarmService);
        var pendingServiceIntent = PendingIntent.GetService (this, 0, _stockServiceIntent, PendingIntentFlags.CancelCurrent);
        alarm.SetRepeating (AlarmType.Rtc, 0, 5000, pendingServiceIntent);
      } else {
        Console.WriteLine ("alarm already set");
      }

    }

    bool IsAlarmSet ()
    {
      return PendingIntent.GetBroadcast (this, 0, _stockServiceIntent, PendingIntentFlags.NoCreate) != null;
    }

    public void GetStocks ()
    {
      if (_isBound) {
        RunOnUiThread (() => {
          var stock = _binder.GetStockService ().GetStocks ();
          if (stock != null) {
            ListAdapter = new ArrayAdapter<Stock> (this, Resource.Layout.StockItemView, stock); 
          } else {
            Log.Debug ("StockService", "stocks is null");
          }
        });
      }
    }

  }
}

