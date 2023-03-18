using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using core.Entities;
using core.Interfaces;

namespace core.Interfaces
{
    public interface IProductRepository
    {
        Task<Product> GetProductsByIdAsync(int id);

        Task<IReadOnlyList<Product>> GetProductsByAsync();
        Task<IReadOnlyList<ProductBrand>> GetProductsBrandsByAsync();
        Task<IReadOnlyList<ProductType>> GetProductsTypeByAsync();


        

    } 
    
}