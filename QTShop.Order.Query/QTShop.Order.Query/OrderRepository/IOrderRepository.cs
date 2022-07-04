using System.Threading.Tasks;

namespace QTShop.Order.Query
{
    public interface IOrderRepository
    {
        Task CreateOrder(OrderRepository.CreateOrderRequest request);
    }
}