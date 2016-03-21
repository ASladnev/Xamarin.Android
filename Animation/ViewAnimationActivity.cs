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
using Android.Views.Animations;

namespace Animation
{
  [Activity (Theme = "@android:style/Theme.Holo.Light.DarkActionBar", Label = "@string/title_viewanimation")]
  public class ViewAnimationActivity : Activity
  {
    protected override void OnCreate (Bundle savedInstanceState)
    {
      base.OnCreate (savedInstanceState);
      SetContentView (Resource.Layout.activity_imageandbutton);

      FindViewById<ImageView> (Resource.Id.imageView1).SetImageResource (Resource.Drawable.ship2_2);
      var button = FindViewById<Button> (Resource.Id.button1);
      button.Text = Resources.GetString (Resource.String.title_hyperspace);
      button.Click += (s, e) => {
        var hyperspaceAnimation = AnimationUtils.LoadAnimation (this, Resource.Animation.hyperspace);
        FindViewById<ImageView> (Resource.Id.imageView1).StartAnimation (hyperspaceAnimation);
      };
    }
  }
}