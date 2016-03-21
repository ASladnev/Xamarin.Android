using Android.App;
using Android.Widget;
using Android.OS;
using Android.Util;
using Android.Content;

namespace ActivityLifeCycle
{
  [Activity (Label = "Activity A", MainLauncher = true, Icon = "@drawable/icon")]
  public class MainActivity : Activity
  {
    void Llog (string state)
    {
      Log.Debug (GetType ().FullName, "Activity A - "+ state);
    }

    protected override void OnCreate (Bundle bundle)
    {
      Llog ("OnCreate");
      base.OnCreate (bundle);

      SetContentView (Resource.Layout.Main);

      Button button = FindViewById<Button> (Resource.Id.MyButton);
      button.Click += (s, e) =>
      {
        StartActivity (new Intent (this, typeof (SecondActivity)));
      };
    }

    protected override void OnDestroy ()
    {
      Llog ("OnDestroy");
      base.OnDestroy ();
    }

    protected override void OnPause ()
    {
      Llog ("OnPause");
      base.OnPause ();
    }

    protected override void OnRestart ()
    {
      Llog ("OnRestart");
      base.OnRestart ();
    }

    protected override void OnResume ()
    {
      Llog ("OnResume");
      base.OnResume ();
    }

    protected override void OnStart ()
    {
      Llog ("OnStart");
      base.OnStart ();
    }

    protected override void OnStop ()
    {
      Llog ("OnStop");
      base.OnStop ();
    }
    
  }
}

