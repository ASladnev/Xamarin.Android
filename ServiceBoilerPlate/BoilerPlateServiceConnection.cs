using Android.Content;
using Android.OS;

namespace ServiceBoilerPlate
{
  public class BoilerPlateServiceConnection : Java.Lang.Object, IServiceConnection
  {
    private BoilerPlateClientActivity Activity { get; set; }

    public BoilerPlateServiceConnection (BoilerPlateClientActivity boilerPlateClietnActivity)
    {
      Activity = boilerPlateClietnActivity;
    }

    public void OnServiceConnected (ComponentName name, IBinder service)
    {
      var boilerPlateServiceBinder = service as BoilerPlateServiceBinder;
      if (boilerPlateServiceBinder == null) return;
      Activity.Binder = boilerPlateServiceBinder;
      Activity.IsBound = true;
    }

    public void OnServiceDisconnected (ComponentName name)
    {
      Activity.IsBound = false;
    }
  }
}