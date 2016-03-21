using Android.App;
using Android.Widget;
using Android.OS;
using Android.Provider;
using Android.Content;

namespace CalendarDemo
{
  [Activity (Label = "Calendar Demo", MainLauncher = true, Icon = "@drawable/icon")]
  public class CalendarListActivity : ListActivity
  {
    protected override void OnCreate (Bundle bundle)
    {
      base.OnCreate (bundle);

      SetContentView (Resource.Layout.CalendarList);

      var calendarUri = CalendarContract.Calendars.ContentUri;
      string[] calendarsProjection = {
        CalendarContract.Calendars.InterfaceConsts.Id,
        CalendarContract.Calendars.InterfaceConsts.CalendarDisplayName,
        CalendarContract.Calendars.InterfaceConsts.AccountName,
      };


      var cursor = ManagedQuery (calendarUri, calendarsProjection, null, null, null);

      string[] sourceColumns = { CalendarContract.Calendars.InterfaceConsts.CalendarDisplayName, CalendarContract.Calendars.InterfaceConsts.AccountName };

      int [] targetResources = { Resource.Id.calDisplayName, Resource.Id.calAccountName};

      var adapter = new SimpleCursorAdapter (this, Resource.Layout.CalListItem, cursor, sourceColumns, targetResources);
      ListAdapter = adapter;

      ListView.ItemClick += (s, e) => {
        var i = (e as AdapterView.ItemClickEventArgs).Position;
        cursor.MoveToPosition (i);
        var calId = cursor.GetInt (cursor.GetColumnIndex (calendarsProjection[0]));

        var showEvents = new Intent (this, typeof (EventListActivity));

        showEvents.PutExtra ("calId", calId);
        StartActivity (showEvents);
      };

    }
  }
}

