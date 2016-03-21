using Android.OS;

namespace ServiceBoilerPlate
{
  public class BoilerPlateServiceBinder : Binder
  {
    private BoilerPlateService Service  { get; set; }

    public BoilerPlateServiceBinder (BoilerPlateService boilderPlateService)
    {
      Service = boilderPlateService;
    }

    public BoilerPlateService GeBoilerPlateService ()
    {
      return Service;
    }

  }
}