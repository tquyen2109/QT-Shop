using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using QTShop.Catalog.Model;

namespace QTShop.Catalog.Repositories
{
    public interface IProductsRepository
    {
        public Task<Product> GetProductById(string id);
        public Task<IEnumerable<Product>> GetProducts();
        public Task CreateProduct(Product product);
        public Task UpdateProduct(Product product);
        public Task UpdateProductQuantity(string id, int quantity);
    }
}