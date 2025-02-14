using Microsoft.Extensions.Configuration;
using NxT.Infrastructure.Data.Migrations.DbCreator;
using NxT.Infrastructure.Data.Providers;
using NxT.Infrastructure.Extensions;

namespace NxT.Infrastructure;

internal static class InfraConfiguration
{
    internal static IDbProvider GetDbProvider(string name, IConfiguration configuration)
        => name switch 
    {
        "mysql" => new MySqlProvider(configuration.ConnectionString("mysql")),
        "psql" => new PostgreSqlProvider(configuration.ConnectionString("psql")),
        "mssql" => new SqlServerProvider(configuration.ConnectionString("mssql")),
        _ => new MySqlProvider(configuration.ConnectionString("mysql")),
    };

    internal static IDbCreator GetDbCreator(string name, IConfiguration configuration) 
        => name switch
    {
        "mysql" => new MysqlDbCreator(configuration.ConnectionString("mysql")),
        "psql" => new PostgreSqlDbCreator(configuration.ConnectionString("psql")),
        "mssql" => new SqlServerDbCreator(configuration.ConnectionString("mssql")),
        _ => new MysqlDbCreator(configuration.ConnectionString("mysql")),
    };
}