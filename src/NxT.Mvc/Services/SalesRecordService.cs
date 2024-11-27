namespace NxT.Mvc.Services
{
    public class SalesRecordService(SalesWebMvcContext context)
    {
        private readonly SalesWebMvcContext _context = context;

        public async Task<DateTime> FindEarliestDateAsync()
        {
            var queryDate = from sales in _context.SalesRecord select sales.Date;

            return await queryDate.MinAsync();
        }
        public async Task<DateTime> FindLatestDateAsync()
        {
            var queryDate = from sales in _context.SalesRecord select sales.Date;

            return await queryDate.MaxAsync();
        }

        public async Task<List<SalesRecord>> FindByDateAsync(DateTime? minimumDate, DateTime? maximumDate)
        {
            var query = from sales in _context.SalesRecord select sales;

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

        public async Task<List<IGrouping<Department?, SalesRecord>>> FindByDataGroupingAsync(DateTime? minimumDate, DateTime? maximumDate)
        {
            var query = from sales in _context.SalesRecord select sales;

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
}
