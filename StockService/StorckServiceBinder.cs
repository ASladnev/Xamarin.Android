using Android.OS;

namespace StockService
{
  public class StockServiceBinder : Binder
  {
    StockService _stockService;

    public StockServiceBinder (StockService stockService)
    {
      _stockService = stockService;
    }

    public StockService GetStockService ()
    {
      return _stockService;
    }
  }
}