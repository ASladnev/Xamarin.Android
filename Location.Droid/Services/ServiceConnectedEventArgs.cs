using Android.OS;
using System;

namespace Location.Droid.Services
{
  public class ServiceConnectedEventArgs : EventArgs
  {
    public IBinder Binder { get; set; }
  }
}