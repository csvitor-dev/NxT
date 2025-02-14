using FluentMigrator.Runner;
using Microsoft.EntityFrameworkCore;

namespace NxT.Infrastructure.Data.Providers;

public class MySqlProvider(string connectionString) : IDbProvider
{
    public void Configure(DbContextOptionsBuilder options)
    {
        MySqlServerVersion version = new(new Version(0, 2, 0));

        options.UseMySql(connectionString, version, opt => 
        {
            opt.EnableRetryOnFailure();
            opt.MigrationsAssembly("NxT.Infrastructure");
        });
    }

    public IMigrationRunnerBuilder Migrate(IMigrationRunnerBuilder builder) 
        => builder.AddMySql8().WithGlobalConnectionString(connectionString);
}