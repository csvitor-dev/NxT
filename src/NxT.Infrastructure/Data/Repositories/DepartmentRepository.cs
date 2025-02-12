using Microsoft.EntityFrameworkCore;
using NxT.Core.Models;
using NxT.Core.Repositories;
using NxT.Exception.Internal;
using NxT.Infrastructure.Data.Contexts;

namespace NxT.Infrastructure.Data.Repositories;

public class DepartmentRepository
    (NxtContext _context) : IReadOnlyRepository<Department>, IWriteOnlyRepository<Department>, IDeleteOnlyRepository
{
    public async Task<IList<Department>> FindAllAsync()
    {
        var orderQuery = from d in _context.Departments orderby d.Name select d;

        return await orderQuery.ToListAsync();
    }

    public async Task<Department?> FindByIdAsync(int? id)
        => await _context.Departments.FirstOrDefaultAsync(d => d.ID == id);

    public async Task InsertAsync(Department entity)
        => await _context.AddAsync(entity);
    // _context.AddAsync() || _context.Departments.AddAsync()
    public async Task UpdateAsync(Department entity)
    {
        var hasAny = await _context.Departments.AnyAsync(
            d => d.ID == entity.ID
        );

        if (hasAny is false)
            throw new NotFoundException($"The {entity.Name} department not exists");

        var _ = _context.Departments.Update(entity) ?? throw new DbConcurrencyException();

        // _context.Update() || _context.Departments.Update()
    }

    public async Task RemoveAsync(int id)
    {
        var entity = await _context.Departments.FindAsync(id);

        if (entity is null)
            throw new NotFoundException($"Department with ID: {id} not founded");
        var hasAnySeller = await _context.Sellers.AnyAsync(s => s.DepartmentID == entity.ID);

        if (hasAnySeller)
            throw new IntegrityException("This department contains sellers");
        _context.Departments.Remove(entity);
    }
}