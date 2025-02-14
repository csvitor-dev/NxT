namespace NxT.Infrastructure.Data.Migrations.DbCreator;

public interface IDbCreator
{
    public void EnsureDatabaseExists();
}