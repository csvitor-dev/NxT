using Microsoft.Extensions.Configuration;

namespace NxT.Infrastructure.Extensions;

public static class InfrastructureConfigExtension
{
    public static string ConnectionString(this IConfiguration self)
        => self.GetConnectionString("db")!;
}