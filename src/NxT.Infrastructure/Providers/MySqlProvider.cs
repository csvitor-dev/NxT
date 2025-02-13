using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NxT.Infrastructure.Data.Contexts;
using NxT.Infrastructure.Extensions;

namespace NxT.Infrastructure.Providers;

public class MySqlProvider : IDbProvider
{
    public void Configure(DbContextOptionsBuilder options, IConfiguration configuration)
    {
        var connectionString = configuration.ConnectionString("mysql");
        MySqlServerVersion version = new(new Version(0, 2, 0));

        options.UseMySql(connectionString, version);
    }
}