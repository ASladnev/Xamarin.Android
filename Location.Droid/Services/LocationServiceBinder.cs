using Android.OS;

namespace Location.Droid.Services
{
  public class LocationServiceBinder : Binder
  {
    public LocationService Service {
      get { return _service; }
    }

    protected LocationService _service;

    public bool IsBound { get; set; }


    public LocationServiceBinder (LocationService service)
    {
      _service = service;
    }
  }
}