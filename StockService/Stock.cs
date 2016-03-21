namespace StockService
{
  public class Stock
  {
    public Stock ()
    {

    }

    public string Symbol { get; set; }
    public float LastPrice { get; set; }
    public override string ToString ()
    {
      return $"[Stock: Symbol={Symbol}, LastPrice={LastPrice}]";
    }

  }
}