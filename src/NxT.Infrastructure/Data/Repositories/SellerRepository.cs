using Microsoft.EntityFrameworkCore;
using NxT.Core.Contracts;
using NxT.Core.Models;
using NxT.Core.Repositories;
using NxT.Exception.Internal;
using NxT.Infrastructure.Data.Contexts;

namespace NxT.Infrastructure.Data.Repositories;

public class SellerRepository
    (NxtContext _context, DepartmentRepository _service) : IReadOnlyRepository<Seller>, IWriteOnlyRepository<Seller>,
    IDeleteOnlyRepository
{
    public async Task<IList<Seller>> FindAllAsync()
    {
        var result = await _context.Sellers
            .Include(seller => seller.Sales)
            .Include(seller => seller.Commission)
            .ToListAsync();
        result.ForEach(seller => seller.TotalCommission = seller.CalculateCommission());

        return result;
    }

    public async Task<Seller?> FindByIdAsync(int? id)
        => await _context.Sellers.Include(s => s.Department)
            .FirstOrDefaultAsync(s => s.ID == id);
    
    public async Task<IList<Department>> FindDepartments()
        => await _service.FindAllAsync();

    public async Task InsertAsync(Seller entity)
        => await _context.AddAsync(entity);

    public async Task UpdateAsync(Seller entity)
    {
        var hasAny = await _context.Sellers.AnyAsync(s => s.ID == entity.ID);

        if (hasAny is false)
            throw new NotFoundException($"The {entity.Name} department not exists");
        
        var _ = _context.Update(entity) ?? throw new DbConcurrencyException();
    }

    public async Task RemoveAsync(int id)
    {
        var entity = await _context.Sellers.FindAsync(id);

        if (entity is null)
            throw new NotFoundException($"Seller with ID: {id} not founded");
        var hasAnySale = await _context.SalesRecords.AnyAsync(s => s.SellerID == entity.ID);

        if (hasAnySale)
            throw new IntegrityException("Can't delete seller because he/she has sales");
        _context.Sellers.Remove(entity);
    }
}
