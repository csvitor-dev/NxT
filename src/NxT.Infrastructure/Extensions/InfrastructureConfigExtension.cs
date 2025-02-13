using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NxT.Infrastructure.Data.Contexts;

namespace NxT.Infrastructure.Extensions;


public static class InfrastructureConfigExtension
{
    public static string ConnectionString(this IConfiguration self, string connection)
        => self.GetConnectionString(connection)!;

    public static async Task ApplyMigrations(this IServiceCollection self)
    {
        using var scope = self.BuildServiceProvider().CreateScope();
        var provider = scope.ServiceProvider;

        try
        {
            await provider.GetService<NxtContext>()?.Database.MigrateAsync()!;
        }
        catch (System.Exception ex)
        {
            await Console.Error.WriteLineAsync($"Failed to migrate database.\n[{ex.Message}]");
        }
    }
}
