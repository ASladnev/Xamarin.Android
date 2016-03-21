
using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;

namespace HandingOrientation
{
  [Activity (Label = "ThirdActivity")]
  public class ThirdActivity : Activity
  {
    protected override void OnCreate (Bundle savedInstanceState)
    {
      base.OnCreate (savedInstanceState);

      var rl = new RelativeLayout (this);

      var layoutParams = new RelativeLayout.LayoutParams (ViewGroup.LayoutParams.FillParent, ViewGroup.LayoutParams.FillParent);

      rl.LayoutParameters = layoutParams;

      var surfaceOrientation = WindowManager.DefaultDisplay.Rotation;

      RelativeLayout.LayoutParams tvLaoutParams;

      tvLaoutParams = new RelativeLayout.LayoutParams (ViewGroup.LayoutParams.FillParent, ViewGroup.LayoutParams.WrapContent);

      if (surfaceOrientation == SurfaceOrientation.Rotation0 || surfaceOrientation == SurfaceOrientation.Rotation180)  {
      } else
      {
        tvLaoutParams.LeftMargin = tvLaoutParams.TopMargin = 200;
      }


      var textView = new TextView (this);
      textView.LayoutParameters = tvLaoutParams;
      textView.Text = "Programatic Layout";

      rl.AddView (textView);
      SetContentView (rl);

    }

    protected override void OnStart ()
    {
      base.OnStart ();

      var intent = new Intent (this, typeof (CodeLayoutActivity));
      StartActivity (intent);
    }
  }
}