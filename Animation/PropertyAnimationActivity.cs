using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Animation
{
  [Activity (Theme = "@android:style/Theme.Holo.Light.DarkActionBar", Label = "@string/title_propertyanimation")]
  public class PropertyAnimationActivity : Activity
  {
    private KarmaMeter _karmaMeter;
    private SeekBar _seekBar;

    protected override void OnCreate (Bundle savedInstanceState)
    {
      base.OnCreate (savedInstanceState);

      SetContentView (Resource.Layout.activity_propertyanimation);

      _seekBar = FindViewById<SeekBar> (Resource.Id.karmaSeeker);
      _seekBar.StopTrackingTouch += (s, e) => {
        var karmaValue = ((double)_seekBar.Progress) / _seekBar.Max;
        _karmaMeter.SetKarmaValue (karmaValue, true);
      };
      _karmaMeter = FindViewById<KarmaMeter> (Resource.Id.karmaMeter);
    }
  }
}