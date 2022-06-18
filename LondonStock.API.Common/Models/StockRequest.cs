namespace LondonStock.API.Common.Models
{
    public class StockRequest
    {
        public decimal Price { get; set; }

        public long TotalShare { get; set; }

        public string BrockerId { get; set; }
    }
}
