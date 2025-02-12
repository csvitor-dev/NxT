using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NxT.Infrastructure.Data.Contexts;
using NxT.Infrastructure.Data.Repositories;
using NxT.Infrastructure.Data.Services;

namespace NxT.Infrastructure.Extensions;

public static class DataAccessDIExtension
{
    public static void AddInfrastructure(this IServiceCollection self, IConfiguration configuration)
    {
        AddNxtDbContext(self, configuration.ConnectionString());
        AddRepositories(self);
        self.AddScoped<CommitPersistence>();
    }

    private static void AddNxtDbContext(this IServiceCollection self, string connection)
    {
        MySqlServerVersion version = new(new Version(0, 2, 0));

        self.AddDbContext<NxtContext>((options)
            => options.UseMySql(connection, version)
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