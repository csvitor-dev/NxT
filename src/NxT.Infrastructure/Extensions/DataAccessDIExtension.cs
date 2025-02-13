using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NxT.Infrastructure.Data.Contexts;
using NxT.Infrastructure.Data.Repositories;
using NxT.Infrastructure.Data.Services;
using NxT.Infrastructure.Providers;

namespace NxT.Infrastructure.Extensions;

public static class DataAccessDIExtension
{
    public static void AddInfrastructure(this IServiceCollection self, IConfiguration configuration, IDbProvider provider)
    {
        AddNxtDbContext(self, configuration, provider);
        AddRepositories(self);
        self.AddScoped<CommitPersistence>();
    }

    private static void AddNxtDbContext(this IServiceCollection self, IConfiguration configuration, IDbProvider provider)
    {
        self.AddDbContext<NxtContext>((options)
            => provider.Configure(options, configuration)
        );
    }
    
    private static void AddRepositories(this IServiceCollection self)
    {
        AddSellerRepository(self);
        AddDepartmentRepository(self);
        self.AddScoped<SalesRecordRepository>();
    }

    private static void AddSellerRepository(IServiceCollection services)
    {
        services.AddScoped<SellerRepository>();
    }

    private static void AddDepartmentRepository(IServiceCollection services)
    {
        services.AddScoped<DepartmentRepository>();
    }
}