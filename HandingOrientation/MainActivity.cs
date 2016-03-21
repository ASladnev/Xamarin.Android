using Android.App;
using Android.Content;
using Android.OS;

namespace HandingOrientation
{
  [Activity (Label = "Handing Orientation", MainLauncher = true, Icon = "@drawable/icon")]
  public class MainActivity : Activity
  {

    protected override void OnCreate (Bundle bundle)
    {
      base.OnCreate (bundle);

      // Set our view from the "main" layout resource
      SetContentView (Resource.Layout.Main);
    }


    protected override void OnStart ()
    {
      base.OnStart ();

      var intent = new Intent (this, typeof (SecondActivity));
      StartActivity (intent);

    }

  }
}

