namespace NxT.Core.Repositories;

public interface IUnitOfWork
{
    public Task CommitAsync();
}