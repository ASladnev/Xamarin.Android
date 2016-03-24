using Android.App;
using System;
using Android.Content;
using Android.OS;
using System.Collections.Generic;
using System.Net;
using System.Json;
using System.Linq;
using Android.Util;
using Android.Runtime;

namespace StockService
{
  [Service]
  [IntentFilter (new String[] { "com.xamarin.StockService" })]
  public class StockService : IntentService
  {
    IBinder _binder;
    List<Stock> _stocks;
    public const string StockUpdatedAction = "StockUpdated";

    protected override void OnHandleIntent (Intent intent)
    {
      var stockSymbols = new List<string> () { "AMZN", "FB", "GOOG", "AAPL", "MSFT", "IBM", "EBMT", "GRF" };
      _stocks = UpdateStocks (stockSymbols);
      var stockIntent = new Intent (StockUpdatedAction);
      SendOrderedBroadcast (stockIntent, null);
    }

    public override IBinder OnBind (Intent intent)
    {
      _binder = new StockServiceBinder (this);
      return _binder;
    }

    public List<Stock> GetStocks ()
    {
      return _stocks;
    }


    //public override StartCommandResult OnStartCommand (Intent intent, [GeneratedEnum] StartCommandFlags flags, int startId)
    //{
    //  return base.OnStartCommand (intent, flags, startId);
    //}

    private List<Stock> UpdateStocks (List<string> symbols)
    {
      List<Stock> results = null;
      var array = symbols.ToArray ();
      string symbolsString = string.Join ("%22%2C%22", array);
      string uri = string.Format (
        "http://query.yahooapis.com/v1/public/yql?q=select%20*%20from%20yahoo.finance.quotes%20where%20symbol%20in%20(%22{0}%22)%0A%09%09&diagnostics=false&format=json&env=http%3A%2F%2Fdatatables.org%2Falltables.env",
       symbolsString);

      var httpRequest = (HttpWebRequest)WebRequest.Create (new Uri (uri));
      try {
        using (var httpResponse = (HttpWebResponse)httpRequest.GetResponse ()) {
          var response = httpResponse.GetResponseStream ();
          var json = (JsonObject)JsonObject.Load (response);
          results = (from result in (JsonArray)json["query"]["results"]["quote"]
                     let jResult = result as JsonObject
                     select new Stock { Symbol = jResult["Symbol"], LastPrice = (float)jResult["LastTradePriceOnly"] }).ToList ();
        }
      } 
      catch (Exception ex) {
        Log.Debug ("StockService", "error connecting to web service: " + ex.Message);
      }

      return results;
    }


  }
}