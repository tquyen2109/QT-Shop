using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QTShop.Inventory.Model.DTOs;
using QTShop.Inventory.Repositories;

namespace QTShop.Inventory.Controllers
{
    [Route("inventory/product")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductInventoryRepository _productInventoryRepository;

        public ProductsController(IProductInventoryRepository productInventoryRepository)
        {
            _productInventoryRepository = productInventoryRepository;
        }
        
        [HttpPut]
        public async Task UpdateProductQuantity([FromBody]UpdateProductQuantityDto body)
        {
            await _productInventoryRepository.UpdateProductQuantity(body.ProductId, body.Quantity);
        }
    }
}