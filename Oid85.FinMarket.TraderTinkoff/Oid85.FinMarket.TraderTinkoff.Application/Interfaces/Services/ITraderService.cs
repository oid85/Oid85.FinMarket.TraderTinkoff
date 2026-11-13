using Oid85.FinMarket.TraderTinkoff.Core.Requests;
using Oid85.FinMarket.TraderTinkoff.Core.Responses;

namespace Oid85.FinMarket.TraderTinkoff.Application.Interfaces.Services
{
    /// <summary>
    /// Брокер сервис
    /// </summary>
    public interface ITraderService
    {
        Task<PortfolioInfoResponse> GetPortfolioInfoAsync(PortfolioInfoRequest request);
        Task<OutboxTaskListResponse> GetOutboxTaskListAsync(OutboxTaskListRequest request);
        Task LoadInstrumentsAsync();
    }
}
