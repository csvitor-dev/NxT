using Dapper;
using Npgsql;

namespace NxT.Infrastructure.Data.Migrations.DbCreator;

public class PostgreSqlDbCreator(string connectionString) : IDbCreator
{
    public void EnsureDatabaseExists()
    {
        var builder = new NpgsqlConnectionStringBuilder(connectionString);
        var databaseName = builder.Database;
        
        builder.Database = "postgres";
        using var connection = new NpgsqlConnection(builder.ConnectionString);
        connection.Open();
        
        var exists = connection.ExecuteScalar<bool>(
            "SELECT EXISTS(SELECT 1 FROM pg_database WHERE datname = @dbName);",
            new { dbName = databaseName });
        
        if (!exists)
        {
            connection.Execute($"CREATE DATABASE \"{databaseName}\";");
        }
    }
}