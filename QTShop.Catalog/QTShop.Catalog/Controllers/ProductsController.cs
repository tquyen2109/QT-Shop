using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QTShop.Catalog.Model;
using QTShop.Catalog.Model.DTO;
using QTShop.Catalog.Repositories;

namespace QTShop.Catalog.Controllers
{
    [Route("product")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsRepository _productsRepository;

        public ProductsController(IProductsRepository productsRepository)
        {
            _productsRepository = productsRepository;
        }
        [Route("{id}")]
        [HttpGet]
        public async Task<Product> GetProductById(string id)
        {
            return await _productsRepository.GetProductById(id);
        }
        
        [HttpGet]
        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _productsRepository.GetProducts();
        }
        
        [HttpPost]
        public async Task CreateProduct([FromBody]ProductDto product)
        {
            await _productsRepository.CreateProduct(new Product
            {
                Name = product.Name,
                Description = product.Description,
                Type = product.Type,
                Brand = product.Brand,
                PictureUrl = product.PictureUrl,
                Price = product.Price
            });
        }
        
        [HttpPut]
        public async Task UpdateProduct([FromBody]ProductDto product)
        {
            await _productsRepository.UpdateProduct(new Product
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Type = product.Type,
                Brand = product.Brand,
                PictureUrl = product.PictureUrl,
                Price = product.Price
            });
        }
    }
}