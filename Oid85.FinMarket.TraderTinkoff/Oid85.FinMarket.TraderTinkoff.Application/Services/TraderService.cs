using Oid85.FinMarket.TraderTinkoff.Application.Interfaces.Repositories;
using Oid85.FinMarket.TraderTinkoff.Application.Interfaces.Services;
using Oid85.FinMarket.TraderTinkoff.Core.Requests;
using Oid85.FinMarket.TraderTinkoff.Core.Responses;

namespace Oid85.FinMarket.TraderTinkoff.Application.Services
{
    /// <inheritdoc />
    public class TraderService(
        IOutboxRepository outboxRepository,
        IBrokerService finamService)
        : ITraderService
    {
        /// <inheritdoc />
        public Task<PortfolioInfoResponse> GetPortfolioInfoAsync(PortfolioInfoRequest request) => 
            finamService.GetPortfolioInfoAsync(request);

        /// <inheritdoc />
        public async Task<OutboxTaskListResponse> GetOutboxTaskListAsync(OutboxTaskListRequest request) =>
            new() { Tasks = await outboxRepository.GetAsync() };

        /// <inheritdoc />
        public Task<GetInstrumentPriceResponse> GetInstrumentPriceAsync(GetInstrumentPriceRequest request) =>
            finamService.GetInstrumentPriceAsync(request);
    }
}
