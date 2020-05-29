using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class ProductRepository : IProductRepository
    {
        private readonly StoreContext _context;

        public ProductRepository(StoreContext context)
        {
            _context = context;
        }

        public async Task<Product> GetProductByIdAsync(long id)
            => await _context.Products
                             .Include(z => z.ProductBrand)
                             .Include(z => z.ProductType)
                             .FirstOrDefaultAsync(z => z.Id == id);

        public async Task<IReadOnlyList<Product>> GetProductsAsync()
            => await _context.Products
                             .Include(z => z.ProductBrand)
                             .Include(z => z.ProductType)
                             .ToListAsync();

        public async Task<IReadOnlyList<ProductBrand>> GetProductsBrandsAsync()
            => await _context.ProductBrands.ToListAsync();

        public async Task<IReadOnlyList<ProductType>> GetProductsTypesAsync()
            => await _context.ProductTypes.ToListAsync();
    }
}