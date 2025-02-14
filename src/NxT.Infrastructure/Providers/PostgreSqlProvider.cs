using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NxT.Infrastructure.Extensions;


namespace NxT.Infrastructure.Providers;

public class PostgreSqlProvider : IDbProvider
{
    public void Configure(DbContextOptionsBuilder options, IConfiguration configuration)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        var connectionString = configuration.ConnectionString("psql");

        options.UseNpgsql(connectionString, opt => 
        {
            opt.EnableRetryOnFailure();
            opt.MigrationsAssembly("NxT.Infrastructure");
        });
    }
}
