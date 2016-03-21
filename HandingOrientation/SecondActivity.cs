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

namespace HandingOrientation
{
  [Activity (Label = "SecondActivity")]
  public class SecondActivity : Activity
  {
    protected override void OnCreate (Bundle savedInstanceState)
    {
      base.OnCreate (savedInstanceState);

      var rl = new RelativeLayout (this);

      var layoutParams = new RelativeLayout.LayoutParams (ViewGroup.LayoutParams.FillParent, ViewGroup.LayoutParams.FillParent);

      rl.LayoutParameters = layoutParams;

      var textView = new TextView (this);

      textView.LayoutParameters = layoutParams;
      textView.Text = "Programmatic layout";

      rl.AddView (textView);
      SetContentView (rl);

    }


    protected override void OnStart ()
    {
      base.OnStart ();

      var intent = new Intent (this, typeof (ThirdActivity));
      StartActivity (intent);
    }
  }
}