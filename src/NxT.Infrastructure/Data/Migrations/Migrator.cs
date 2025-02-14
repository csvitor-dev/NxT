using FluentMigrator.Runner;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace NxT.Infrastructure.Data.Migrations;

public static class Migrator
{
    public static void Migrate(IServiceProvider serviceProvider, IConfiguration configuration, string serviceName)
    {
        var runner = serviceProvider.GetRequiredService<IMigrationRunner>();
        var creator = InfraConfiguration.GetDbCreator(serviceName, configuration);
        
        creator.EnsureDatabaseExists();

        runner.ListMigrations();
        runner.MigrateUp();
    }
}