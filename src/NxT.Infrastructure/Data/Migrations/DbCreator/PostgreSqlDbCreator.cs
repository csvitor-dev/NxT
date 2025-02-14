using Dapper;
using Npgsql;

namespace NxT.Infrastructure.Data.Migrations.DbCreator;

public class PostgreSqlDbCreator(string connectionString) : IDbCreator
{
    public void EnsureDatabaseExists()
    {
        var builder = new NpgsqlConnectionStringBuilder(connectionString);
        var databaseName = builder.Database;

        // Conecta ao banco "postgres"
        builder.Database = "postgres";
        using var connection = new NpgsqlConnection(builder.ConnectionString);
        connection.Open();

        // Verifica se o banco já existe
        var exists = connection.ExecuteScalar<bool>(
            "SELECT EXISTS(SELECT 1 FROM pg_database WHERE datname = @dbName);",
            new { dbName = databaseName });

        // Cria o banco se não existir
        if (!exists)
        {
            connection.Execute($"CREATE DATABASE \"{databaseName}\";");
        }
    }
}