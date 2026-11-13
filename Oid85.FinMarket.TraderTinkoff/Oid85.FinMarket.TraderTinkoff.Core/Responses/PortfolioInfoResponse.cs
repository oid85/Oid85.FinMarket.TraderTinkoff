using Oid85.FinMarket.TraderTinkoff.Core.Models;

namespace Oid85.FinMarket.TraderTinkoff.Core.Responses
{
    public class PortfolioInfoResponse
    {
        public double TotalSum { get; set; }
        public double Money { get; set; }
        public double TotalDailyPnl { get; set; }
        public List<PositionData> Positions { get; set; } = [];
    }
}
