namespace NxT.Core.Repositories;

public interface IWriteOnlyRepository<T> where T : class
{
    public Task InsertAsync(T entity);
    public Task UpdateAsync(T entity);
}