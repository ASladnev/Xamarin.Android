using Android.App;
using Android.Content;
using Android.OS;
using Android.Util;

namespace DemoService
{
  [Service]
  [IntentFilter (new string[] { "com.xamarin.DemoMessengerService" })]
  public class DemoMessengerService : Service
  {
    Messenger _demoMessenger;


    public DemoMessengerService ()
    {
      _demoMessenger = new Messenger (new DemoHandler ());
    }

    public override IBinder OnBind (Intent intent)
    {
      Log.Debug ("StockMessengerService", "client bound to service");

      return _demoMessenger.Binder;
    }


    class DemoHandler : Handler
    {
      public override void HandleMessage (Message msg)
      {
        Log.Debug ("DemoMessengerService", msg.What.ToString ());

        string text = msg.Data.GetString ("InputText");

        Log.Debug ("DemoMessengerService", "InputText = " + text);
      }
    }

  }
}