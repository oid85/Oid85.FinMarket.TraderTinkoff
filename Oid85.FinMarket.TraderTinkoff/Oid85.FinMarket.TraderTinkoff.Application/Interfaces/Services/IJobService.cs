namespace Oid85.FinMarket.TraderTinkoff.Application.Interfaces.Services
{
    /// <summary>
    /// Сервис работы с запланированными задачами
    /// </summary>
    public interface IJobService
    {
        Task CheckOutboxAsync();
    }
}
