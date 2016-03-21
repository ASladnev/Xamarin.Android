using System;
using Android.App;
using Android.OS;
using Android.Widget;
using Android.Util;

namespace DatePickerDemo
{
  public class DatePickerFragment : DialogFragment,
                                    DatePickerDialog.IOnDateSetListener
  {
    // TAG can be any string of your choice.
    public static readonly string TAG = "X:" + typeof (DatePickerFragment).Name.ToUpper ();

    // Initialize this value to prevent NullReferenceExceptions.
    Action<DateTime> _dateSelectedHandler = d => { };

    public static DatePickerFragment NewInstance (Action<DateTime> onDateSelected)
    {
      return new DatePickerFragment { _dateSelectedHandler = onDateSelected };
    }

    public override Dialog OnCreateDialog (Bundle savedInstanceState)
    {
      return new DatePickerDialog (Activity,
                                            this,
                                            DateTime.Now.Year,
                                            DateTime.Now.Month-1,
                                            DateTime.Now.Day);
    }

    public void OnDateSet (DatePicker view, int year, int monthOfYear, int dayOfMonth)
    {
      // Note: monthOfYear is a value between 0 and 11, not 1 and 12!
      var selectedDate = new DateTime (year, monthOfYear + 1, dayOfMonth);
      Log.Debug (TAG, selectedDate.ToLongDateString ());
      _dateSelectedHandler (selectedDate);
    }
  }
}