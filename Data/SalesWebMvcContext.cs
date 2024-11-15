namespace SalesApp.Data
{
    public class SalesWebMvcContext(DbContextOptions<SalesWebMvcContext> options) : DbContext(options)
    {
        public DbSet<Department> Department { get; init; } = default!;
        public DbSet<Seller> Seller { get; init; } = default!;
        public DbSet<SalesRecord> SalesRecord { get; init; } = default!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL(Configuration.ConnectionString);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Department>(entity =>
            {
                entity.HasKey(e => e.ID);

                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            });

            modelBuilder.Entity<Seller>(entity =>
            {
                entity.HasKey(e => e.ID);

                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
                entity.Property(e => e.BirthDate).IsRequired();
                entity.Property(e => e.BaseSalary).IsRequired();

                entity.HasOne(e => e.Department).WithMany(d => d.Sellers)
                .HasForeignKey(e => e.DepartmentID).IsRequired();
            });

            modelBuilder.Entity<SalesRecord>(entity =>
            {
                entity.HasKey(e => e.ID);

                entity.Property(e => e.Date).IsRequired();
                entity.Property(e => e.Amount).IsRequired();
                entity.Property(e => e.Status).IsRequired();

                entity.HasOne(e => e.Seller).WithMany(s => s.Sales)
                .HasForeignKey(e => e.SellerID).IsRequired();
            });
        }
    }
}
