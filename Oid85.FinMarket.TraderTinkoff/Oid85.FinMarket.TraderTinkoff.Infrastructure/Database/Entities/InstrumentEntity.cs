using Oid85.FinMarket.TraderTinkoff.Infrastructure.Database.Entities.Base;

namespace Oid85.FinMarket.TraderTinkoff.Infrastructure.Database.Entities
{
    /// <summary>
    /// Инструмент
    /// </summary>
    public class InstrumentEntity : BaseEntity
    {
        public Guid InstrumentId { get; set; }
        public string Figi { get; set; } = string.Empty;
        public string Ticker { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
    }
}
