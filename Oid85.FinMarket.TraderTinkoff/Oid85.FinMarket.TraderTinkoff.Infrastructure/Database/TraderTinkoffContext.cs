using Microsoft.EntityFrameworkCore;
using Oid85.FinMarket.TraderTinkoff.Common.KnownConstants;
using Oid85.FinMarket.TraderTinkoff.Infrastructure.Database.Entities;
using Oid85.FinMarket.TraderTinkoff.Infrastructure.Database.Schemas;

namespace Oid85.FinMarket.TraderTinkoff.Infrastructure.Database;

public class TraderTinkoffContext(DbContextOptions<TraderTinkoffContext> options) : DbContext(options)
{
    public DbSet<ParameterEntity> ParameterEntities { get; set; }
    public DbSet<OutboxTaskEntity> OutboxTaskEntities { get; set; }
    public DbSet<InstrumentEntity> InstrumentEntities { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder
            .HasDefaultSchema(KnownDatabaseSchemas.Default)
            .ApplyConfigurationsFromAssembly(
                typeof(TraderTinkoffContext).Assembly,
                type => type
                    .GetInterface(typeof(ITraderTinkoffSchema).ToString()) != null)
            .UseIdentityAlwaysColumns();
    }    
}