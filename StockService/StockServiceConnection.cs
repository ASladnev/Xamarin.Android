using Android.Content;
using Android.OS;

namespace StockService
{
  public class StockServiceConnection : Java.Lang.Object, IServiceConnection
  {
    StockActivity _activity;

    public StockServiceConnection (StockActivity activity)
    {
      _activity = activity;
    }

    public void OnServiceConnected (ComponentName name, IBinder service)
    {
      var stockServiceBinder = service as StockServiceBinder;
      if (stockServiceBinder == null) return;
      var binder = (StockServiceBinder)service;
      _activity._binder = binder;
      _activity._isBound = true;
    }

    public void OnServiceDisconnected (ComponentName name)
    {
      _activity._isBound = false;
    }
  }
}