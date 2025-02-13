using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NxT.Infrastructure.Extensions;


namespace NxT.Infrastructure.Providers;

public class PostgrestProvider : IDbProvider
{
    public void Configure(DbContextOptionsBuilder options, IConfiguration configuration)
    {
        var connectionString = configuration.ConnectionString("psql");

        options.UseNpgsql(connectionString);
    }
}