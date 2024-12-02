namespace NxT.Core.Repositories;

public interface IDeleteOnlyRepository
{
    public Task RemoveAsync(int id);
}