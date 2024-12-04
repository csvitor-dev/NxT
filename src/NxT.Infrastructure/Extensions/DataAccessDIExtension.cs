using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NxT.Core.Models;
using NxT.Core.Repositories;
using NxT.Infrastructure.Data;
using NxT.Infrastructure.Data.Repositories;

namespace NxT.Infrastructure.Extensions;

public static class DataAccessDIExtension
{
    public static void AddInfrastructure(this IServiceCollection self, IConfiguration configuration)
    {
        AddNxtDbContext(self, configuration.ConnectionString());
        AddRepositories(self);
    }

    private static void AddNxtDbContext(this IServiceCollection self, string connection)
    {
        MySqlServerVersion version = new(new Version(0, 0, 2));

        self.AddDbContext<NxtContext>((options)
            => options.UseMySql(connection, version,
                (opt) => opt.MigrationsAssembly("NxT.Infrastructure")
        ));
    }
    
    private static void AddRepositories(this IServiceCollection self)
    {
        AddSellerRepository(self);
        AddDepartmentRepository(self);
        self.AddScoped<SalesRecordRepository>();
    }

    private static void AddSellerRepository(IServiceCollection services)
    {
        services.AddScoped<IReadOnlyRepository<Seller>, SellerRepository>();
        services.AddScoped<IWriteOnlyRepository<Seller>, SellerRepository>();
        services.AddScoped<IDeleteOnlyRepository, SellerRepository>();
    }

    private static void AddDepartmentRepository(IServiceCollection services)
    {
        services.AddScoped<IReadOnlyRepository<Department>, DepartmentRepository>();
        services.AddScoped<IWriteOnlyRepository<Department>, DepartmentRepository>();
        services.AddScoped<IDeleteOnlyRepository, DepartmentRepository>();
    }
}