namespace NxT.Core.Repositories;

public interface IReadOnlyRepository<T> where T : class
{
    public Task<IList<T>> FindAllAsync();
    public Task<T?> FindByIdAsync(int? id);
}