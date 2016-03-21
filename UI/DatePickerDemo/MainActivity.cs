using System;
using Android.App;
using Android.Widget;
using Android.OS;

namespace DatePickerDemo
{
  [Activity (Label = "@string/app_name", MainLauncher = true, Icon = "@drawable/icon")]
  public class MainActivity : Activity
  {
    TextView _dateDisplay;
    Button _dateSelectButton;

    protected override void OnCreate (Bundle bundle)
    {
      base.OnCreate (bundle);
      SetContentView (Resource.Layout.Main);
     
      _dateSelectButton = FindViewById<Button> (Resource.Id.date_select_button);
      _dateSelectButton.Click += (s, e) => 
      
      DatePickerFragment.NewInstance (d => _dateDisplay.Text = d.ToLongDateString ()).Show (FragmentManager, DatePickerFragment.TAG);

      _dateDisplay = FindViewById<TextView> (Resource.Id.date_display);
      _dateDisplay.Text = DateTime.Now.ToLongDateString ();
    }
 
  }
}

