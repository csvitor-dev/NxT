using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace NxT.Infrastructure.Providers;

public interface IDbProvider
{
    public void Configure(DbContextOptionsBuilder options, IConfiguration configuration);
}