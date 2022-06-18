using System;

namespace LondonStock.API.Common.Models
{
    public class Stock
    {
        public Guid TransactionId { get; set; } = Guid.NewGuid();

        public string TickerSymbol { get; set; }

        public decimal Price { get; set; }

        public long TotalShare { get; set; }

        public string BrockerId { get; set; }

        public DateTime TransactionDate { get; set; } = DateTime.UtcNow;
    }
}
