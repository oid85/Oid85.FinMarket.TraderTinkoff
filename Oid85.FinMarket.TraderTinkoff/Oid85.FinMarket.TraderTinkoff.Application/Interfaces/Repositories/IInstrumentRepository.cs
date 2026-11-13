using Oid85.FinMarket.TraderTinkoff.Core.Models;

namespace Oid85.FinMarket.TraderTinkoff.Application.Interfaces.Repositories
{
    public interface IInstrumentRepository
    {
        Task AddAsync(List<Instrument> models);
        Task<List<Instrument>> GetAsync();
    }
}
