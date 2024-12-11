using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace NxT.Infrastructure.Data.Services;

public class NxtContextFactory : IDesignTimeDbContextFactory<NxtContext>
{
    public NxtContext CreateDbContext(string[] args)
    {
        var builder = new DbContextOptionsBuilder<NxtContext>();
        var version = new MySqlServerVersion(new Version(0, 2, 0));

        builder.UseMySql("Server=localhost;Database=salesdb;User=user;Password=mVC#1246;Port=3306;",
            version, (opt) => opt.MigrationsAssembly("NxT.Infrastructure")
        );

        return new NxtContext(builder.Options);
    }
}