using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using core.Entities;
using core.Interfaces;
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

        public async Task<Product> GetProductsByIdAsync(int id)
        {
           return await _context.Products
           
        .Include(p => p.ProductType)
        .Include(p => p.ProductBrand)
        .FirstOrDefaultAsync(p => p.Id  == id);           
           
        }


        public async Task<IReadOnlyList<Product>> GetProductsByAsync()
        {
           var typeId =1;
            
            var Products =_context.Products
              .Where(x => x.ProductTypeId == typeId ).Include(x => x.ProductType).ToListAsync();
             
           return await _context.Products
           .Include(p => p.ProductType)
           .Include(p => p.ProductBrand)               
           .ToListAsync();
        }

        public  async Task<IReadOnlyList<ProductBrand>> GetProductsBrandsByAsync()
        {
            return await _context.ProductBrands.ToListAsync();
        }

        public async  Task<IReadOnlyList<ProductType>> GetProductsTypeByAsync()
        {
            return await _context.ProductTypes.ToListAsync();
        }
    }
}