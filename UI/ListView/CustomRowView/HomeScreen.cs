using Android.App;
using Android.Widget;
using Android.OS;
using System.Collections.Generic;
using System;

namespace CustomRowView
{
  [Activity (Label = "CustomRowView", MainLauncher = true, Icon = "@drawable/icon")]
  public class HomeScreen : Activity
  {
    List<TableItem> _tableItems = new List<TableItem> ();
    ListView _listView;

    protected override void OnCreate (Bundle bundle)
    {
      base.OnCreate (bundle);
      SetContentView (Resource.Layout.HomeScreen);

      _listView = FindViewById<ListView> (Resource.Id.List);

      _tableItems.Add (new TableItem { Heading = "Vegetables", SubHeading = "65 items", ImageResourceId = Resource.Drawable.Vegetables });
      _tableItems.Add (new TableItem { Heading = "Fruits", SubHeading = "17 items", ImageResourceId = Resource.Drawable.Fruits });
      _tableItems.Add (new TableItem { Heading = "Flower Buds", SubHeading = "5 items", ImageResourceId = Resource.Drawable.FlowerBuds });
      _tableItems.Add (new TableItem { Heading = "Legumes", SubHeading = "33 items", ImageResourceId = Resource.Drawable.Legumes });
      _tableItems.Add (new TableItem { Heading = "Bulbs", SubHeading = "18 items", ImageResourceId = Resource.Drawable.Bulbs });
      _tableItems.Add (new TableItem { Heading = "Tubers", SubHeading = "43 items", ImageResourceId = Resource.Drawable.Tubers });

      _tableItems.Add (new TableItem { Heading = "Vegetables", SubHeading = "65 items", ImageResourceId = Resource.Drawable.Vegetables });
      _tableItems.Add (new TableItem { Heading = "Fruits", SubHeading = "17 items", ImageResourceId = Resource.Drawable.Fruits });
      _tableItems.Add (new TableItem { Heading = "Flower Buds", SubHeading = "5 items", ImageResourceId = Resource.Drawable.FlowerBuds });
      _tableItems.Add (new TableItem { Heading = "Legumes", SubHeading = "33 items", ImageResourceId = Resource.Drawable.Legumes });
      _tableItems.Add (new TableItem { Heading = "Bulbs", SubHeading = "18 items", ImageResourceId = Resource.Drawable.Bulbs });
      _tableItems.Add (new TableItem { Heading = "Tubers", SubHeading = "43 items", ImageResourceId = Resource.Drawable.Tubers });

      _tableItems.Add (new TableItem { Heading = "Vegetables", SubHeading = "65 items", ImageResourceId = Resource.Drawable.Vegetables });
      _tableItems.Add (new TableItem { Heading = "Fruits", SubHeading = "17 items", ImageResourceId = Resource.Drawable.Fruits });
      _tableItems.Add (new TableItem { Heading = "Flower Buds", SubHeading = "5 items", ImageResourceId = Resource.Drawable.FlowerBuds });
      _tableItems.Add (new TableItem { Heading = "Legumes", SubHeading = "33 items", ImageResourceId = Resource.Drawable.Legumes });
      _tableItems.Add (new TableItem { Heading = "Bulbs", SubHeading = "18 items", ImageResourceId = Resource.Drawable.Bulbs });
      _tableItems.Add (new TableItem { Heading = "Tubers", SubHeading = "43 items", ImageResourceId = Resource.Drawable.Tubers });


      _listView.Adapter = new HomeScreenAdapter (this, _tableItems);

      _listView.ItemClick += (s, e) => {
        var item = _tableItems[e.Position];
        Toast.MakeText (this, item.Heading, ToastLength.Long).Show ();
        Console.WriteLine ("Clicked on " + item.Heading); 
      };

    }
  }
}

