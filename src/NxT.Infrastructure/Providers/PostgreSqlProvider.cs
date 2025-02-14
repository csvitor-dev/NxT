using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using NxT.Infrastructure.Extensions;


namespace NxT.Infrastructure.Providers;

public class PostgreSqlProvider : IDbProvider
{
    public void Configure(DbContextOptionsBuilder options, IConfiguration configuration)
    {
        var connectionString = configuration.ConnectionString("psql");

        options.UseNpgsql(connectionString);
    }
}
