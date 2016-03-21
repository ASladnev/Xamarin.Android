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
  [Activity (Label = "@string/title_drawing", Theme = "@android:style/Theme.Holo.Light.DarkActionBar")]
  public class DrawingActivity : Activity
  {
    protected override void OnCreate (Bundle savedInstanceState)
    {
      base.OnCreate (savedInstanceState);

      SetContentView (Resource.Layout.activity_drawing);

      KarmaMeter meter = FindViewById<KarmaMeter> (Resource.Id.karmaMeter);

      meter.KarmaValue = 0.25d;
    }
  }
}