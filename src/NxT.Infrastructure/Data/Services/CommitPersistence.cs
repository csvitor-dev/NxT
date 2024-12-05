using NxT.Core.Repositories;
using NxT.Exception.Internal;

namespace NxT.Infrastructure.Data.Services;

public class CommitPersistence(NxtContext _context) : IUnitOfWork
{
    public Task CommitAsync()
        => _context.SaveChangesAsync() ??
            throw new DbConcurrencyException();
}