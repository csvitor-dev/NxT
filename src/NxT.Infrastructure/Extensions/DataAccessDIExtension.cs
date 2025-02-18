using System.Reflection;
using FluentMigrator.Runner;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NxT.Infrastructure.Data.Contexts;
using NxT.Infrastructure.Data.Providers;
using NxT.Infrastructure.Data.Repositories;
using NxT.Infrastructure.Data.Services;

namespace NxT.Infrastructure.Extensions;

public static class DataAccessDIExtension
{
    public static void AddInfrastructure(this IServiceCollection self, IConfiguration configuration, string providerName)
    {
        var provider = InfraConfiguration.GetDbProvider(providerName, configuration);
        
        AddNxtDbContext(self, provider);
        AddRepositories(self);
        AddMigrator(self, provider);
        self.AddScoped<CommitPersistence>();
    }

    private static void AddNxtDbContext(IServiceCollection services, IDbProvider provider) 
        => services.AddDbContext<NxtContext>(provider.Configure);

    private static void AddRepositories(IServiceCollection services)
    {
        AddSellerRepository(services);
        AddDepartmentRepository(services);
        services.AddScoped<SalesRecordRepository>();
    }

    private static void AddSellerRepository(IServiceCollection services)
    {
        services.AddScoped<SellerRepository>();
    }

    private static void AddDepartmentRepository(IServiceCollection services)
    {
        services.AddScoped<DepartmentRepository>();
    }

    private static void AddMigrator(IServiceCollection service, IDbProvider provider)
    {
        service.AddFluentMigratorCore().ConfigureRunner(builder =>
        {
            provider.Migrate(builder)
                .ScanIn(Assembly.Load("NxT.Infrastructure"))
                .For.All();
        }).AddLogging(config => config.AddFluentMigratorConsole());
    }
}