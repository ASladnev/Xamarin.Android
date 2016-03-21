using System;
using Android.App;
using Android.OS;
using Android.Provider;
using Android.Widget;
using Android.Content;
using Android.Database;
using Android.Views;
using Java.Util;

namespace CalendarDemo
{
  [Activity (Label = "EventListActivity")]
  public class EventListActivity : ListActivity
  {
    int _calId;

    protected override void OnCreate (Bundle bundle)
    {
      base.OnCreate (bundle);

      SetContentView (Resource.Layout.EventList);

      _calId = Intent.GetIntExtra ("calId", -1);

      ListEvents ();
      InitAddEvent ();
    }

    private void ListEvents ()
    {
      var eventsUri = CalendarContract.Events.ContentUri;

      string[] eventsProjection = {
        CalendarContract.Events.InterfaceConsts.Id,
        CalendarContract.Events.InterfaceConsts.Title, 
        CalendarContract.Events.InterfaceConsts.Dtstart
      };

      var cursor = ManagedQuery (eventsUri, eventsProjection, String.Format ("calendar_id={0}", _calId), null, "dtstart ASC");

      string[] sourceColumns = {
        CalendarContract.Events.InterfaceConsts.Title,
        CalendarContract.Events.InterfaceConsts.Dtstart
      };

      int[] targetResources = { Resource.Id.eventTitle, Resource.Id.eventStartDate};
      var adapter = new SimpleCursorAdapter (this, Resource.Layout.EventListItem, cursor, sourceColumns, targetResources);

      adapter.ViewBinder = new ViewBinder ();
    }


    private void InitAddEvent ()
    {
      var addSampleEvent = FindViewById<Button> (Resource.Id.addSampleEvent);
      addSampleEvent.Click += (s, e) => {
        var eventValues = new ContentValues ();
        eventValues.Put (CalendarContract.Events.InterfaceConsts.CalendarId, _calId);
        eventValues.Put (CalendarContract.Events.InterfaceConsts.Title, "Test event from M4F");
        eventValues.Put (CalendarContract.Events.InterfaceConsts.Description, "This is event has created from Mono for android");
        eventValues.Put (CalendarContract.Events.InterfaceConsts.Dtstart, GetDateTimeMS (2015, 12, 15, 10, 0));
        eventValues.Put (CalendarContract.Events.InterfaceConsts.Dtend, GetDateTimeMS (2015, 12, 15, 11, 0));

        eventValues.Put (CalendarContract.Events.InterfaceConsts.EventTimezone, "UTC");
        eventValues.Put (CalendarContract.Events.InterfaceConsts.EventTimezone, "UTC");

        var uri = ContentResolver.Insert (CalendarContract.Events.ContentUri, eventValues);
        Console.WriteLine ("Uri for new event: {0}", uri);
      };

    }


    class ViewBinder : Java.Lang.Object, SimpleCursorAdapter.IViewBinder
    {
      public bool SetViewValue (View view, ICursor cursor, int columnIndex)
      {
        if (columnIndex == 2) {
          var ms = cursor.GetLong (columnIndex);
          var date = new DateTime (1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddMilliseconds (ms).ToLocalTime ();

          var textView = (TextView)view;
          textView.Text = date.ToLongDateString ();
          return true;

        } return false;
      }
    }

    private long GetDateTimeMS (int yr, int month, int day, int hr, int min)
    {
      var c = Calendar.GetInstance (Java.Util.TimeZone.Default);

      c.Set (Calendar.DayOfMonth, 15);
      c.Set (Calendar.HourOfDay, hr);
      c.Set (Calendar.Minute, min);
      c.Set (Calendar.Month, Calendar.December);
      c.Set (Calendar.Year, 2011);

      return c.TimeInMillis;
    }
  }
}