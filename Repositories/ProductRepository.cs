using ECommerce_App.Data;
using ECommerce_App.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace ECommerce_App.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly StoreContext _context;

        public ProductRepository(StoreContext context) 
        
        {
            _context = context;
        }

        public async Task<ProductBrand> GetProductBrandByIdAsync(int id)
        {
            return await _context.ProductBrands.FindAsync(id);
        }

        public async Task<List<ProductBrand>> GetProductBrandsAsync()
        {
            return await _context.ProductBrands.ToListAsync();
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _context.Products
                .Include(p => p.ProductType)
                .Include(p => p.ProductBrand)
                .FirstOrDefaultAsync(p =>p.Id == id);
        }

        public async Task<List<Product>> GetProductsAsync()
        {
            var typeId = 1;

            var products = _context.Products.Where(x => x.ProductTypeId == typeId)
                .Include(x => x.ProductType).ToListAsync();


            return await _context.Products   
                .Include(p => p.ProductType)
                .Include(p => p.ProductBrand)
                .ToListAsync();
        }   

        public async Task<List<ProductType>> GetProductTypesAsync()
        {
            return await _context.ProductTypes.ToListAsync();
        }

        public async Task<ProductType> GetProductTypesByIdAsync(int id)
        {
            return await _context.ProductTypes.FindAsync(id);
        }
    }
}
