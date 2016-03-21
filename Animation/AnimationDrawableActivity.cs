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
using Android.Graphics.Drawables;

namespace Animation
{
  [Activity (Theme = "@android:style/Theme.Holo.Light.DarkActionBar", Label = "@string/title_animationdrawable")]
  public class AnimationDrawableActivity : Activity
  {
    private AnimationDrawable _asteroidDrawable;


    protected override void OnCreate (Bundle savedInstanceState)
    {
      base.OnCreate (savedInstanceState);

      SetContentView (Resource.Layout.activity_imageandbutton);

      _asteroidDrawable = (AnimationDrawable)Resources.GetDrawable (Resource.Drawable.spinning_asteroid);

      var imageView = FindViewById<ImageView> (Resource.Id.imageView1);
      imageView.SetImageDrawable (_asteroidDrawable);

      var spinAsteroidButton = FindViewById<Button> (Resource.Id.button1);
      spinAsteroidButton.Text = Resources.GetString (Resource.String.title_spinasteroid);
      spinAsteroidButton.Click += (s, e) => _asteroidDrawable.Start ();

    }
  }
}