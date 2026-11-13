namespace Oid85.FinMarket.TraderTinkoff.Core.Models
{
    public class PositionData
    {
        public string Ticker { get; set; } = string.Empty;        
        public double Size { get; set; }
        public double Cost { get; set; }
        public double CurrentPrice { get; set; }
        public double DailyPnl { get; set; }
    }
}
