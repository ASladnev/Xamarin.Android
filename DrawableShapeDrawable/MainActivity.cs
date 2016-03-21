using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace DrawableShapeDrawable
{
  [Activity (Label = "DrawableShapeDrawable", MainLauncher = true, Icon = "@drawable/icon")]
  public class MainActivity : Activity
  {
    int count = 1;

    protected override void OnCreate (Bundle bundle)
    {
      base.OnCreate (bundle);

      // Set our view from the "main" layout resource
      SetContentView (Resource.Layout.Main);

      var textView = FindViewById<TextView> (Resource.Id.shapeDrawableTextView);
      textView.SetBackgroundResource (Resource.Drawable.shape_rounded_blue_rect);


      var barView = new BarView (this);


    }
  }
}

