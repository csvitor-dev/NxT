using FluentMigrator.Runner;
using Microsoft.EntityFrameworkCore;

namespace NxT.Infrastructure.Providers;

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
    {

        return builder.AddPostgres15_0().WithGlobalConnectionString(connectionString);
    }
}
