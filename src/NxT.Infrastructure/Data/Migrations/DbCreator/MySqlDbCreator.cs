using Dapper;
using MySqlConnector;

namespace NxT.Infrastructure.Data.Migrations.DbCreator;

public class MysqlDbCreator(string connectionString) : IDbCreator
{
    public void EnsureDatabaseExists()
    {
        var builder = new MySqlConnectionStringBuilder(connectionString);
        var databaseName = builder.Database;
        
        builder.Database = "";
        using var connection = new MySqlConnection(builder.ConnectionString);
        connection.Open();
        
        var exists = connection.ExecuteScalar<int>(
            "SELECT COUNT(*) FROM INFORMATION_SCHEMA.SCHEMATA WHERE SCHEMA_NAME = @dbName;",
            new { dbName = databaseName });
        
        if (exists == 0)
        {
            connection.Execute($"CREATE DATABASE `{databaseName}`;");
        }
    }
}