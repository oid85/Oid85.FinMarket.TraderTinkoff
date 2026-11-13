namespace Oid85.FinMarket.TraderTinkoff.Core.Models
{
    /// <summary>
    /// Инструмент
    /// </summary>
    public class Instrument
    {
        public Guid InstrumentId { get; set; }
        public string Figi { get; set; } = string.Empty;
        public string Ticker { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string Currency { get; set; }
        public int Lot { get; set; }
    }
}
