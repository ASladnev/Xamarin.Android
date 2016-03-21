using Android.App;
using Android.Content;
using Android.OS;

namespace ServiceBoilerPlate
{
  [Activity (Label = "Service Boiler Plate", MainLauncher = true, Icon = "@drawable/icon")]
  public class BoilerPlateClientActivity : Activity
  {
    public BoilerPlateServiceConnection Connection {get; set;}
    public BoilerPlateServiceBinder Binder { get; set; }
    public bool IsBound { get; set; }
    private Intent ServiceIntent { get; set; } 

    protected override void OnCreate (Bundle bundle)
    {
      base.OnCreate (bundle);
      SetContentView (Resource.Layout.Main);

      ServiceIntent = new Intent ("com.xamarin.developmentServiceBoilerPlate");
    }

    protected override void OnStart ()
    {
      base.OnStart ();
      Connection = new BoilerPlateServiceConnection (this);
      BindService (ServiceIntent, Connection, Bind.AutoCreate);
    }

    protected override void OnStop ()
    {
      base.OnStop ();
      if (IsBound) {
        UnbindService (Connection);
        IsBound = false;
      } 
    }
  }
}

