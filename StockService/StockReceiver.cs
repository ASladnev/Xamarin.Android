using Android.Content;

namespace StockService
{
  public class StockReceiver : BroadcastReceiver
  {
    public override void OnReceive (Context context, Intent intent)
    {
      ((StockActivity)context).GetStocks ();
      InvokeAbortBroadcast ();
    }
  }
}