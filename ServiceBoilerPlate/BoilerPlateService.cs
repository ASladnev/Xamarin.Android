
using Android.App;
using Android.Content;
using Android.OS;

namespace ServiceBoilerPlate
{
  [Service]
  [IntentFilter (new string[] { "com.xamarin.developmentServiceBoilerPlate" })]
  public class BoilerPlateService : Service
  {
    BoilerPlateServiceBinder _binder;

    public override IBinder OnBind (Intent intent)
    {
      _binder = new BoilerPlateServiceBinder (this);
      return _binder;
    }



  }
}