using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QTShop.Basket.Models;
using QTShop.Basket.Models.DTOs;
using QTShop.Basket.Repositories;
namespace QTShop.Basket.Controllers
{
    [Route("basket")]
    public class BasketsController : ControllerBase
    {
        private readonly IBasketRepository _basketRepository;

        public BasketsController(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }
        [HttpGet]
        public async Task<ActionResult<Models.Basket>> GetBasket(string id)
        {
            var basket = await _basketRepository.GetBasketById(id);
            return basket; 
        }
        
        [HttpPost]
        public async Task UpsertBasket([FromBody]UpsertBasketDto basketDto)
        {
           await _basketRepository.UpsertBasket(new Models.Basket
           {
               Id = basketDto.Id,
               Items = basketDto.Items.Select(i => new BasketItem
               {
                   ProductId = i.ProductId,
                   Name = i.Name,
                   Price = i.Price
               })
           });
        }
    }
}