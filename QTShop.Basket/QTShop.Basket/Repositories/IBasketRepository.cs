using System.Threading.Tasks;
using QTShop.Basket.Models.DTOs;

namespace QTShop.Basket.Repositories
{
    public interface IBasketRepository
    {
        Task<Models.Basket> GetBasketById(string id);
        Task UpsertBasket(Models.Basket basket);
    }
}