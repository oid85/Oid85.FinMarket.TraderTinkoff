using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Oid85.FinMarket.TraderTinkoff.Application.Interfaces.Repositories;
using Oid85.FinMarket.TraderTinkoff.Application.Interfaces.Services;
using Oid85.FinMarket.TraderTinkoff.Common.KnownConstants;
using Oid85.FinMarket.TraderTinkoff.Common.Utils;
using Oid85.FinMarket.TraderTinkoff.Infrastructure.Database;
using Oid85.FinMarket.TraderTinkoff.Infrastructure.Database.Repositories;
using Oid85.FinMarket.TraderTinkoff.Infrastructure.Helpers;
using Oid85.FinMarket.TraderTinkoff.Infrastructure.Services;

namespace Oid85.FinMarket.TraderTinkoff.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static void ConfigureDatabase(
        this IServiceCollection services,
        IConfiguration configuration)
    {    
        services.AddDbContextPool<TraderTinkoffContext>((serviceProvider, options) =>
        {  
            options.UseNpgsql(configuration.GetValue<string>(KnownSettingsKeys.PostgresFinMarketTraderTinkoffConnectionString));
        });

        services.AddPooledDbContextFactory<TraderTinkoffContext>(options =>
            options
                .UseNpgsql(configuration.GetValue<string>(KnownSettingsKeys.PostgresFinMarketTraderTinkoffConnectionString))
                .EnableServiceProviderCaching(false), poolSize: 32);

        services.AddTransient<IParameterRepository, ParameterRepository>();
        services.AddTransient<IOutboxRepository, OutboxRepository>();
    }

    public static void ConfigureInvestApiClient(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddInvestApiClient((_, settings) =>
        {
            settings.AccessToken = StringUtils.Base64Decode(
                configuration.GetValue<string>(KnownSettingsKeys.TinkoffToken)!);
        });

        services.AddTransient<GetPricesHelper>();
    }

    public static void ConfigureInfrastructureServices(
    this IServiceCollection services)
    {
        services.AddTransient<IBrokerService, BrokerService>();
    }

    public static async Task ApplyMigrations(this IHost host)
    {
        var scopeFactory = host.Services.GetRequiredService<IServiceScopeFactory>();
        await using var scope = scopeFactory.CreateAsyncScope();
        await using var context = scope.ServiceProvider.GetRequiredService<TraderTinkoffContext>();
        await context.Database.MigrateAsync();
    }
}