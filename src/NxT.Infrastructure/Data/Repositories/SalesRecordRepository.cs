using Microsoft.EntityFrameworkCore;
using NxT.Core.Models;
using NxT.Infrastructure.Data.Contexts;

namespace NxT.Infrastructure.Data.Repositories;

public class SalesRecordRepository(NxtContext _context)
{
    public async Task<DateTime> FindEarliestDateAsync()
    {
        var dateQuery = from sales in _context.SalesRecords select sales.Date;

        return await dateQuery.MinAsync();
    }

    public async Task<DateTime> FindLatestDateAsync()
    {
        var dateQuery = from sales in _context.SalesRecords select sales.Date;

        return await dateQuery.MaxAsync();
    }

    public async Task<List<SalesRecord>> FindByDateAsync(DateTime? minimumDate, DateTime? maximumDate)
    {
        var query = from sales in _context.SalesRecords select sales;

        if (minimumDate.HasValue)
            query = from minDateQuery in query where minDateQuery.Date >= minimumDate.Value select minDateQuery;

        if (maximumDate.HasValue)
            query = from maxDateQuery in query where maxDateQuery.Date <= maximumDate.Value select maxDateQuery;

        return await query
            .Include(sales => sales.Seller)
            .Include(sales => sales.Seller.Department)
            .OrderByDescending(sales => sales.Date)
            .ToListAsync();
    }

    public async Task<List<IGrouping<Department?, SalesRecord>>> 
        FindByGroupingDateAsync(DateTime? minimumDate, DateTime? maximumDate)
    {
        var query = from sales in _context.SalesRecords select sales;

        if (minimumDate.HasValue)
            query = from minDateQuery in query where minDateQuery.Date >= minimumDate.Value select minDateQuery;

        if (maximumDate.HasValue)
            query = from maxDateQuery in query where maxDateQuery.Date <= maximumDate.Value select maxDateQuery;

        return await query
            .Include(sales => sales.Seller)
            .Include(sales => sales.Seller.Department)
            .OrderByDescending(sales => sales.Date)
            .GroupBy(sales => sales.Seller.Department)
            .ToListAsync();
    }
}