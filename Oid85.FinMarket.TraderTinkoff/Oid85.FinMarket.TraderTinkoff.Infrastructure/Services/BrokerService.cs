using Grpc.Net.ClientFactory;
using Oid85.FinMarket.TraderTinkoff.Application.Interfaces.Repositories;
using Oid85.FinMarket.TraderTinkoff.Application.Interfaces.Services;
using Oid85.FinMarket.TraderTinkoff.Core.Requests;
using Oid85.FinMarket.TraderTinkoff.Core.Responses;

namespace Oid85.FinMarket.TraderTinkoff.Infrastructure.Services
{
    /// <inheritdoc />
    public class BrokerService()
        : IBrokerService
    {
        /// <inheritdoc />
        public async Task<GetInstrumentPriceResponse> GetInstrumentPriceAsync(GetInstrumentPriceRequest request)
        {
            return new ();
        }

        /// <inheritdoc />
        public async Task<PortfolioInfoResponse> GetPortfolioInfoAsync(PortfolioInfoRequest request)
        {
            return new ();
        }
    }
}
