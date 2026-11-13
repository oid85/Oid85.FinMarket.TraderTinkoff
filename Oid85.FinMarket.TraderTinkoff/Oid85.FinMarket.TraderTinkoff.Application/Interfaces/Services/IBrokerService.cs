using Oid85.FinMarket.TraderTinkoff.Core.Requests;
using Oid85.FinMarket.TraderTinkoff.Core.Responses;

namespace Oid85.FinMarket.TraderTinkoff.Application.Interfaces.Services
{
    /// <summary>
    /// Сервис взаимодействия с API брокера
    /// </summary>
    public interface IBrokerService
    {
        /// <summary>
        /// Получить цену по тикеру
        /// </summary>
        Task<GetInstrumentPriceResponse> GetInstrumentPriceAsync(GetInstrumentPriceRequest request);

        /// <summary>
        /// Получить текущее состояние портфеля
        /// </summary>
        Task<PortfolioInfoResponse> GetPortfolioInfoAsync(PortfolioInfoRequest request);
        
        /// <summary>
        /// Загрузить инструменты
        /// </summary>        
        Task LoadInstrumentsAsync();
    }
}
