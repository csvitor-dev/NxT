using Dapper;
using Microsoft.Data.SqlClient;

namespace NxT.Infrastructure.Data.Migrations.DbCreator;

public class SqlServerDbCreator(string connectionString) : IDbCreator
{
    public void EnsureDatabaseExists()
    {
        var builder = new SqlConnectionStringBuilder(connectionString);
        var databaseName = builder.InitialCatalog;
        
        builder.InitialCatalog = "master";
        using var connection = new SqlConnection(builder.ConnectionString);
        connection.Open();
        
        var exists = connection.ExecuteScalar<int>(
            "SELECT COUNT(*) FROM sys.databases WHERE name = @dbName;",
            new { dbName = databaseName });
        
        if (exists == 0)
            connection.Execute($"CREATE DATABASE [{databaseName}];");
    }
}