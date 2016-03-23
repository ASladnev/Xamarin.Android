using System.Collections.Generic;
using Android.App;
using Android.Views;
using Android.Widget;

namespace CustomRowView
{
  public class HomeScreenAdapter : BaseAdapter<TableItem>
  {
    private List<TableItem> _items;
    private Activity _context;

    public HomeScreenAdapter (Activity context, List<TableItem> items) : base ()
    {
      _context = context;
      _items = items;
    }

    public override long GetItemId (int position)
    {
      return position;
    }

    public override TableItem this[int position]
    {
      get {
        return _items[position];
      }
    }

    public override int Count
    {
      get {
        return _items.Count;
      }
    }

    public override View GetView (int position, View convertView, ViewGroup parent)
    {
      var item = _items[position];

      var view = convertView;
      if (view == null)
        view = _context.LayoutInflater.Inflate (Resource.Layout.CustomView, null);

      view.FindViewById<TextView> (Resource.Id.Text1).Text = item.Heading;
      view.FindViewById<TextView> (Resource.Id.Text2).Text = item.SubHeading;
      view.FindViewById<ImageView> (Resource.Id.Image).SetImageResource (item.ImageResourceId);

      return view;
      //throw new NotImplementedException ();
    }
  }
}