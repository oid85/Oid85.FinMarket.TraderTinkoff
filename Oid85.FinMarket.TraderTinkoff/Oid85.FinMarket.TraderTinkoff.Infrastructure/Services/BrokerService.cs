using System.Timers;
using Grpc.Net.ClientFactory;
using Microsoft.Extensions.Configuration;
using Oid85.FinMarket.TraderTinkoff.Application.Interfaces.Repositories;
using Oid85.FinMarket.TraderTinkoff.Application.Interfaces.Services;
using Oid85.FinMarket.TraderTinkoff.Common.Extensions;
using Oid85.FinMarket.TraderTinkoff.Core.Models;
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
        public async Task<PortfolioInfoResponse> GetPortfolioInfoAsync(PortfolioInfoRequest request)
        {
            var instruments = await instrumentRepository.GetAsync();

            var accounts = await client.Users.GetAccountsAsync();
            string accountId = accounts.Accounts.FirstOrDefault(x => x.Name == request.AccountName)!.Id;

            var portfolioResponse = await client.Operations.GetPortfolioAsync(
                new Tinkoff.InvestApi.V1.PortfolioRequest
                { 
                    AccountId = accountId
                });

            var positions = new List<PositionData>();            

            foreach (var item in portfolioResponse.Positions)
            {
                if (item.Figi == "USD800UTSTOM") continue; // USD
                if (item.Figi == "RUB000UTSTOM") continue; // RUB

                string ticker = item.Figi switch
                {
                    "BBG000VJ5YR4" => "GOLD",
                    _ => instruments.Find(x => x.Figi == item.Figi)?.Ticker ?? string.Empty
                };

                string name = item.Figi switch
                {
                    "BBG000VJ5YR4" => "Золото",
                    _ => instruments.Find(x => x.Figi == item.Figi)?.Name ?? string.Empty
                };

                double size = ConvertHelper.QuotationToDouble(item.Quantity).RoundTo(2);
                double price = ConvertHelper.MoneyValueToDouble(item.CurrentPrice).RoundTo(2);
                double cost = (size * price).RoundTo(2);
                double dailyPnl = ConvertHelper.MoneyValueToDouble(item.DailyYield).RoundTo(2);

                var positionData = new PositionData
                {             
                    Name = name,
                    Ticker = ticker,
                    Size = Convert.ToInt32(size),
                    CurrentPrice = Convert.ToDecimal(price),
                    Cost = Convert.ToDecimal(cost),
                    DailyPnl = Convert.ToDecimal(dailyPnl)
                };

                positions.Add(positionData);
            }

            double totalDailyPnl = ConvertHelper.MoneyValueToDouble(portfolioResponse.DailyYield).RoundTo(2);
            double totalSum = ConvertHelper.MoneyValueToDouble(portfolioResponse.TotalAmountPortfolio).RoundTo(2);
            double money = ConvertHelper.QuotationToDouble(portfolioResponse.Positions.FirstOrDefault(x => x.Figi == "RUB000UTSTOM")?.Quantity ?? 0).RoundTo(2);

            return new PortfolioInfoResponse
            {
                TotalSum = Convert.ToDecimal(totalSum),
                Money = Convert.ToDecimal(money),
                TotalDailyPnl = Convert.ToDecimal(totalDailyPnl),
                Positions = positions
            };
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
