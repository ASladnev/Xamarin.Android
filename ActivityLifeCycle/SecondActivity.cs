using Android.App;
using Android.OS;
using Android.Util;
using Android.Widget;
using System.IO;

namespace ActivityLifeCycle
{
  [Activity (Label = "Activity B")]
  public class SecondActivity : Activity
  {
    void Llog (string state)
    {
      Log.Debug (GetType ().FullName, "Activity B - " + state);
    }


    protected override void OnCreate (Bundle savedInstanceState)
    {
      Llog ("OnCreate");
      base.OnCreate (savedInstanceState);

      //assets 
      var textView = new TextView (this);
      var content = "";
      // var assetManager = Assets; 
      using (var sr = new StreamReader (Assets.Open ("read_asset.txt")))
      {
        content = sr.ReadToEnd ();
      }
      textView.Text = content;
      SetContentView (textView);
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