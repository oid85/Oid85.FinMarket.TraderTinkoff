using Oid85.FinMarket.TraderTinkoff.Core.Models;

namespace Oid85.FinMarket.TraderTinkoff.Application.Interfaces.Repositories
{
    public interface IOutboxRepository
    {
        Task<List<OutboxTask>> GetAsync();
        Task<Guid> AddAsync(OutboxTask model);
        Task UpdateStateAsync(Guid id, string state);
        Task DeleteAsync(Guid id);
    }
}
