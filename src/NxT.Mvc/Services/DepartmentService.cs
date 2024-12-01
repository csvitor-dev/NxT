using NxT.Core.Models;
using NxT.Mvc.Data;
using NxT.Mvc.Services.Exceptions;

namespace NxT.Mvc.Services;

public class DepartmentService(SalesWebMvcContext context)
{
    private readonly SalesWebMvcContext _context = context;

    public async Task<List<Department>> FindAllAsync() 
        => await _context.Department.OrderBy(department 
            => department.Name).ToListAsync();

    public async Task<Department?> FindByIDAsync(int? id) 
        => await _context.Department.FirstOrDefaultAsync(department 
            => department.ID == id);

    public async Task InsertAsync(Department department)
    {
        _context.Add(department);
        await _context.SaveChangesAsync();
    }

    public async Task RemoveAsync(int id)
    {
        var department = await _context.Department.FindAsync(id);

        if (department == null)
            throw new NotFoundException("ID not found");

        var hasAnySeller = await _context.Seller.AnyAsync(seller => seller.DepartmentID == department.ID);
        if (hasAnySeller)
            throw new IntegrityException("This department contains sellers");

        try
        {
            _context.Department.Remove(department);
            await _context.SaveChangesAsync();
        }
        catch(DbUpdateException ex)
        {
            throw new IntegrityException(ex.Message);
        }
    }

    public async Task UpdateAsync(Department department)
    {
        bool hasAny = await _context.Department.AnyAsync(dbDepartment => dbDepartment.ID == department.ID);

        if (hasAny == false)
            throw new NotFoundException("ID not found");
        try
        {
            _context.Update(department);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException ex)
        {
            throw new DbConcurrencyException(ex.Message);
        }
    }
}
