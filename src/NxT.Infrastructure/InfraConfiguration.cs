using NxT.Infrastructure.Providers;

namespace NxT.Infrastructure;

internal static class InfraConfiguration
{
    internal static IDbProvider GetDbProvider(string name)
        => name switch {
            "mysql" => new MySqlProvider(),
            "psql" => new PostgreSqlProvider(),
            _ => new MySqlProvider(),
        };
}