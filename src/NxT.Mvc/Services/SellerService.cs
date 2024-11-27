namespace NxT.Mvc.Services
{
    public class SellerService(SalesWebMvcContext context)
    {
        private readonly SalesWebMvcContext _context = context;

        public async Task<List<Seller>> FindAllAsync() => 
            await _context.Seller.ToListAsync();
        public async Task<Seller?> FindByIDAsync(int? id) => 
            await _context.Seller.Include(seller => seller.Department).FirstOrDefaultAsync(seller => seller.ID == id);

        public async Task InsertAsync(Seller seller)
        {
            _context.Add(seller);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAsync(int id)
        {
            var seller = await _context.Seller.FindAsync(id);

            if (seller == null)
                throw new NotFoundException("ID not found");

            var hasAnySale = await _context.SalesRecord.AnyAsync(sale => sale.SellerID == seller.ID);
            if (hasAnySale)
                throw new IntegrityException("Can't delete seller because he/she has sales");

            try
            {
                _context.Seller.Remove(seller);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new IntegrityException(ex.Message);
            }
        }

        public async Task UpdateAsync(Seller seller)
        {
            bool hasAny = await _context.Seller.AnyAsync(dbSeller => dbSeller.ID == seller.ID);

            if (hasAny == false)
                throw new NotFoundException("ID not found");
            try
            {
                _context.Update(seller);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new DbConcurrencyException(ex.Message);
            }
        }
    }
}
