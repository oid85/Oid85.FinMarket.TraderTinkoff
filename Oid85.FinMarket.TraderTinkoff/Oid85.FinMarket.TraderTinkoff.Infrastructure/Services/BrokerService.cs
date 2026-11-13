using Grpc.Net.ClientFactory;
using Microsoft.Extensions.Configuration;
using Oid85.FinMarket.TraderTinkoff.Application.Interfaces.Repositories;
using Oid85.FinMarket.TraderTinkoff.Application.Interfaces.Services;
using Oid85.FinMarket.TraderTinkoff.Core.Requests;
using Oid85.FinMarket.TraderTinkoff.Core.Responses;
using Oid85.FinMarket.TraderTinkoff.Infrastructure.Helpers;
using Tinkoff.InvestApi;

namespace Oid85.FinMarket.TraderTinkoff.Infrastructure.Services
{
    /// <inheritdoc />
    public class BrokerService(        
        InvestApiClient client,
        IInstrumentRepository instrumentRepository,
        GetInstrumentsHelper getInstrumentsHelper)
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

            var instruments = await instrumentRepository.GetAsync();

            var accounts = await client.Users.GetAccountsAsync();
            string accountId = accounts.Accounts.FirstOrDefault(x => x.Name == request.AccountName)!.Id;

            var portfolio = await client.Operations.GetPortfolioAsync(
                new Tinkoff.InvestApi.V1.PortfolioRequest
                { 
                    AccountId = accountId
                });

            return new ();
        }

        /// <inheritdoc />
        public async Task LoadInstrumentsAsync()
        {
            await instrumentRepository.AddAsync(await getInstrumentsHelper.GetSharesAsync());
            await instrumentRepository.AddAsync(await getInstrumentsHelper.GetBondsAsync());
            await instrumentRepository.AddAsync(await getInstrumentsHelper.GetEtfsAsync());
            await instrumentRepository.AddAsync(await getInstrumentsHelper.GetFuturesAsync());
        }
    }
}
