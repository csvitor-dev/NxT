using NxT.Core.Repositories;
using NxT.Exception.Internal;
using NxT.Infrastructure.Data;

namespace NxT.Infrastructure.Services;

public class CommitPersistence(NxtContext _context) : IUnitOfWork
{
    public Task CommitAsync()
        => _context.SaveChangesAsync() ??
            throw new DbConcurrencyException();
}