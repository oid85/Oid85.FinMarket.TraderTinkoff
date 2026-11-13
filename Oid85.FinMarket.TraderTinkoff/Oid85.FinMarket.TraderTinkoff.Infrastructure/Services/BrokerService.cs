using Grpc.Net.ClientFactory;
using Microsoft.Extensions.Configuration;
using Oid85.FinMarket.TraderTinkoff.Application.Interfaces.Repositories;
using Oid85.FinMarket.TraderTinkoff.Application.Interfaces.Services;
using Oid85.FinMarket.TraderTinkoff.Core.Requests;
using Oid85.FinMarket.TraderTinkoff.Core.Responses;
using Tinkoff.InvestApi;

namespace Oid85.FinMarket.TraderTinkoff.Infrastructure.Services
{
    /// <inheritdoc />
    public class BrokerService(
        IConfiguration configuration,
        InvestApiClient client)
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
            request.AccountName = "Брокерский счёт 1";

            var accounts = await client.Users.GetAccountsAsync();
            string accountId = accounts.Accounts.FirstOrDefault(x => x.Name == request.AccountName)!.Id;

            var portfolio = await client.Operations.GetPortfolioAsync(
                new Tinkoff.InvestApi.V1.PortfolioRequest
                { 
                    AccountId = accountId
                });

            return new ();
        }
    }
}
