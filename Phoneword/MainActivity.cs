using Android.App;
using Android.Content;
using Android.Widget;
using Android.OS;
using System.Collections.Generic;
//using Code;

namespace Phoneword
{
  [Activity (Label = "Phone Word", MainLauncher = true, Icon = "@drawable/icon")]
  public class MainActivity : Activity
  {
    private static readonly List<string> phoneNumbers = new List<string> ();


    protected override void OnCreate (Bundle bundle)
    {
      base.OnCreate (bundle);

      // Set our view from the "main" layout resource
      SetContentView (Resource.Layout.Main);

      var phoneTextNumnrt = FindViewById<EditText> (Resource.Id.plainTextNumber);
      var buttonTranslate = FindViewById<Button> (Resource.Id.buttonTranslate);
      var buttonCall = FindViewById<Button> (Resource.Id.buttonCall);
      var buttonCallHistory = FindViewById<Button> (Resource.Id.buttonCallHistory);

      buttonCall.Enabled = false;
      var trancslatedNumber = string.Empty;

      buttonTranslate.Click += (s, e) =>
      {
        trancslatedNumber = Core.PhonewordTranslator.ToNumber (phoneTextNumnrt.Text);
        buttonCall.Enabled = !string.IsNullOrWhiteSpace (trancslatedNumber);
        buttonCall.Text = "Call" + (buttonCall.Enabled ? "" : " " + trancslatedNumber);
      };

      buttonCallHistory.Click += (s, e) =>
      {
        var intent = new Intent (this, typeof (CallHistoryActivity));
        intent.PutStringArrayListExtra ("phone_numbers", phoneNumbers);
        StartActivity (intent);
      };

      buttonCall.Click += (s, e) =>
      {
        var callDialog = new AlertDialog.Builder (this);
        callDialog.SetMessage ("Call " + trancslatedNumber + "?");

        callDialog.SetNeutralButton ("Call", delegate
        {
          phoneNumbers.Add (trancslatedNumber);
          buttonCallHistory.Enabled = true;
          var callIntent = new Intent (Intent.ActionCall);
          callIntent.SetData (Android.Net.Uri.Parse ("tel:" + trancslatedNumber));
          StartActivity (callIntent);
        });

        callDialog.SetNegativeButton ("Cancel", delegate { });

        callDialog.Show ();

      };

    }

  }
}

