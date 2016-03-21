using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace ActionBarDemo
{
  [Activity (Label = "ActionBar Demo", MainLauncher = true, Icon = "@drawable/icon")]
  public class MainActivity : Activity
  {
    protected override void OnCreate (Bundle bundle)
    {
      base.OnCreate (bundle);

      SetContentView (Resource.Layout.Main);

      ActionBar.NavigationMode = ActionBarNavigationMode.Tabs;
      AddTab ("Tab 1", Resource.Drawable.icon, new SimpleTabFragment ());
      AddTab ("Tab 2", Resource.Drawable.ic_tab_white, new SimpleTabFragment1 ());

      if (bundle != null)
        ActionBar.SelectTab (ActionBar.GetTabAt (bundle.GetInt ("tab")));
    }

    protected override void OnSaveInstanceState (Bundle outState)
    {
      outState.PutInt ("tab", ActionBar.SelectedNavigationIndex);
      base.OnSaveInstanceState (outState);
    }

    private void AddTab (string tabText, int iconResource, Fragment view)
    {
      var tab = ActionBar.NewTab ();
      tab.SetText (tabText);
      tab.SetIcon (iconResource);
      tab.TabSelected += (s, e) => {
        var fragment = FragmentManager.FindFragmentById (Resource.Id.fragmentContainer);
        if (fragment != null)
          e.FragmentTransaction.Remove (fragment);
        e.FragmentTransaction.Add (Resource.Id.fragmentContainer, view);
      };
      tab.TabUnselected += (s, e) => {
        e.FragmentTransaction.Remove (view);
      };

      ActionBar.AddTab (tab);
    }

    class SimpleTabFragment : Fragment
    {
      public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
      {
        base.OnCreateView (inflater, container, savedInstanceState);
        var view = inflater.Inflate (Resource.Layout.Tab, container, false);
        var sampleTextView = view.FindViewById<TextView> (Resource.Id.sampleTextView);
        sampleTextView.Text = "Sample fragment text";
        return view;
      }
    }

    class SimpleTabFragment1 : Fragment
    {
      public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
      {
        base.OnCreateView (inflater, container, savedInstanceState);
        var view = inflater.Inflate (Resource.Layout.Tab, container, false);
        var sampleTextView = view.FindViewById<TextView> (Resource.Id.sampleTextView);
        sampleTextView.Text = "Sample fragment text 2";
        return view;
      }
    }


  }
}

