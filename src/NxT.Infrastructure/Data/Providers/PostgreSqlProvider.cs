using FluentMigrator.Runner;
using Microsoft.EntityFrameworkCore;

namespace NxT.Infrastructure.Data.Providers;

public class PostgreSqlProvider(string connectionString) : IDbProvider
{
    public void Configure(DbContextOptionsBuilder options)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        options.UseNpgsql(connectionString, opt => 
        {
            opt.EnableRetryOnFailure();
            opt.MigrationsAssembly("NxT.Infrastructure");
        });
    }

    public IMigrationRunnerBuilder Migrate(IMigrationRunnerBuilder builder) 
        => builder.AddPostgres15_0().WithGlobalConnectionString(connectionString);
}
