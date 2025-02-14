using Microsoft.Extensions.Configuration;
using NxT.Infrastructure.Data.Migrations.DbCreator;
using NxT.Infrastructure.Extensions;
using NxT.Infrastructure.Providers;

namespace NxT.Infrastructure;

internal static class InfraConfiguration
{
    internal static IDbProvider GetDbProvider(string name, IConfiguration configuration)
        => name switch 
    {
        "mysql" => new MySqlProvider(configuration.ConnectionString("mysql")),
        "psql" => new PostgreSqlProvider(configuration.ConnectionString("psql")),
        _ => new MySqlProvider(configuration.ConnectionString("mysql")),
    };

    internal static IDbCreator GetDbCreator(string name, IConfiguration configuration) 
        => name switch
    {
        "mysql" => new MysqlDbCreator(configuration.ConnectionString("mysql")),
        "psql" => new PostgreSqlDbCreator(configuration.ConnectionString("psql")),
        _ => new MysqlDbCreator(configuration.ConnectionString("mysql")),
    };
}