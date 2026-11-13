using System.Xml.Linq;
using Microsoft.EntityFrameworkCore;
using Oid85.FinMarket.TraderTinkoff.Application.Interfaces.Repositories;
using Oid85.FinMarket.TraderTinkoff.Core.Models;
using Oid85.FinMarket.TraderTinkoff.Infrastructure.Database.Entities;

namespace Oid85.FinMarket.TraderTinkoff.Infrastructure.Database.Repositories
{
    public class InstrumentRepository(
        IDbContextFactory<TraderTinkoffContext> contextFactory) 
        : IInstrumentRepository
    {
        public async Task AddAsync(List<Instrument> models)
        {
            await using var context = await contextFactory.CreateDbContextAsync();

            var entities = await context.InstrumentEntities.AsNoTracking().ToListAsync();
            var instrumentIds = entities.Select(x => x.InstrumentId).ToList();
            var insertModels = models.Where(x => !instrumentIds.Contains(x.InstrumentId)).ToList();

            if (insertModels.Count == 0)
                return;

            var insertEntities = insertModels
                .Select(x =>
                new InstrumentEntity
                {
                    InstrumentId = x.InstrumentId,
                    Figi = x.Figi,
                    Ticker = x.Ticker,
                    Name = x.Name,
                    Type = x.Type,
                    Currency = x.Currency,
                    Lot = x.Lot
                })
                .ToList();

            await context.AddRangeAsync(insertEntities);
            await context.SaveChangesAsync();
        }

        public async Task<List<Instrument>> GetAsync()
        {
            await using var context = await contextFactory.CreateDbContextAsync();

            var entities = await context.InstrumentEntities.AsNoTracking().ToListAsync();

            return [.. entities
                .Select(x => 
                    new Instrument
                    {
                        InstrumentId = x.InstrumentId,
                        Figi = x.Figi,
                        Ticker = x.Ticker,
                        Name = x.Name,
                        Type = x.Type,
                        Currency = x.Currency,
                        Lot = x.Lot
                    })];
        }
    }
}
