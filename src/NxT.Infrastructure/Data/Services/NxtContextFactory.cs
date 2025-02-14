using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using NxT.Infrastructure.Data.Contexts;

namespace NxT.Infrastructure.Data.Services;

public class NxtContextFactory : IDesignTimeDbContextFactory<NxtContext>
{
    public NxtContext CreateDbContext(string[] args)
    {
        var arg = args.Length > 0 ? args[0] : "mysql";

        var builder = new DbContextOptionsBuilder<NxtContext>();
        IConfiguration configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json").Build();

        var provider = InfraConfiguration.GetDbProvider(arg);
        provider.Configure(builder, configuration);

        return new NxtContext(builder.Options);
    }
}