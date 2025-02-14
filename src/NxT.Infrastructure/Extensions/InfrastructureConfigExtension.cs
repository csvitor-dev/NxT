using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace NxT.Infrastructure.Extensions;


public static class InfrastructureConfigExtension
{
    public static string ConnectionString(this IConfiguration self, string connection)
        => self.GetConnectionString(connection)!;
}
