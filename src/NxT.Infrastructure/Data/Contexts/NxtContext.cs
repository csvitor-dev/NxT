using NxT.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace NxT.Infrastructure.Data.Contexts;

public class NxtContext
    (DbContextOptions<NxtContext> options) : DbContext(options)
{
    public DbSet<Department> Departments { get; init; } = default!;
    public DbSet<Seller> Sellers { get; init; } = default!;
    public DbSet<SalesRecord> SalesRecords { get; init; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
        => modelBuilder.ApplyConfigurationsFromAssembly(typeof(NxtContext).Assembly);
}