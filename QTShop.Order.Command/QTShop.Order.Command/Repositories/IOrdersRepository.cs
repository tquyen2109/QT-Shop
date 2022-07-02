using System.Threading.Tasks;

namespace QTShop.Order.Command.Repositories
{
    public interface IOrdersRepository
    {
        Task CreateOrder(OrdersRepository.CreateOrderRequest request);
    }
}