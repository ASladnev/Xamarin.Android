using Android.App;
using Android.Content.Res;
using Android.OS;
using Android.Views;
using Android.Widget;

namespace HandingOrientation
{
  [Activity (Label = "Code Layout Activity", ConfigurationChanges = Android.Content.PM.ConfigChanges.Orientation | Android.Content.PM.ConfigChanges.ScreenSize)]
  public class CodeLayoutActivity : Activity
  {
    TextView _textView;
    RelativeLayout.LayoutParams _layoutParamsPortrait;
    RelativeLayout.LayoutParams _layoutParamsLandscape;

    protected override void OnCreate (Bundle savedInstanceState)
    {
      base.OnCreate (savedInstanceState);

      // Create your application here
      var rl = new RelativeLayout (this);

      _layoutParamsPortrait = new RelativeLayout.LayoutParams (ViewGroup.LayoutParams.FillParent, ViewGroup.LayoutParams.WrapContent);

      _layoutParamsLandscape = new RelativeLayout.LayoutParams (ViewGroup.LayoutParams.FillParent, ViewGroup.LayoutParams.WrapContent);

      _layoutParamsLandscape.LeftMargin = _layoutParamsLandscape.TopMargin = 150;

      _textView = new TextView (this);

      var surfaceOrientation = WindowManager.DefaultDisplay.Rotation;

      if (surfaceOrientation == SurfaceOrientation.Rotation0 || surfaceOrientation == SurfaceOrientation.Rotation180) {
        _textView.LayoutParameters = _layoutParamsPortrait;
      } else {
        _textView.LayoutParameters = _layoutParamsLandscape;
      }

      _textView.Text = "Programatic layout 4";
      rl.AddView (_textView);
      SetContentView (rl);
    }


    public override void OnConfigurationChanged (Configuration newConfig)
    {
      base.OnConfigurationChanged (newConfig);

      _textView.LayoutParameters = (newConfig.Orientation == Android.Content.Res.Orientation.Portrait) ? _layoutParamsPortrait : _layoutParamsLandscape;
      _textView.Text = "Change to " + ((newConfig.Orientation == Android.Content.Res.Orientation.Portrait) ? "Portrait" : "Landscape");

    }
  }
}