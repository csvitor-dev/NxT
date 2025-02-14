using Dapper;
using MySqlConnector;

namespace NxT.Infrastructure.Data.Migrations.DbCreator;

public class MysqlDbCreator(string connectionString) : IDbCreator
{
    public void EnsureDatabaseExists()
    {
        var builder = new MySqlConnectionStringBuilder(connectionString);
        var databaseName = builder.Database;

        // Conecta ao MySQL sem banco de dados
        builder.Database = "";
        using var connection = new MySqlConnection(builder.ConnectionString);
        connection.Open();

        // Verifica se o banco já existe
        var exists = connection.ExecuteScalar<int>(
            "SELECT COUNT(*) FROM INFORMATION_SCHEMA.SCHEMATA WHERE SCHEMA_NAME = @dbName;",
            new { dbName = databaseName });

        // Cria o banco se não existir
        if (exists == 0)
        {
            connection.Execute($"CREATE DATABASE `{databaseName}`;");
        }
    }
}