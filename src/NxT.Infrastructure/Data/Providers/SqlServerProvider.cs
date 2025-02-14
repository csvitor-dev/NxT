using FluentMigrator.Runner;
using Microsoft.EntityFrameworkCore;

namespace NxT.Infrastructure.Data.Providers;

public class SqlServerProvider(string connectionString) : IDbProvider
{
    public void Configure(DbContextOptionsBuilder options)
    {
        options.UseSqlServer(connectionString, opt =>
        {
            opt.EnableRetryOnFailure();
            opt.MigrationsAssembly("NxT.Infrastructure");
        });
    }

    public IMigrationRunnerBuilder Migrate(IMigrationRunnerBuilder builder)
        => builder.AddSqlServer().WithGlobalConnectionString(connectionString);
}