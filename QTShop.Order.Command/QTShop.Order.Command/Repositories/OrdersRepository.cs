using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using QTShop.Order.Command.Models;

namespace QTShop.Order.Command.Repositories
{
    public class OrdersRepository : IOrdersRepository
    {
        private readonly IMongoCollection<Models.Order> _orderEventCollection;

        public OrdersRepository(IOrderEventCollectionDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _orderEventCollection = database.GetCollection<Models.Order>(settings.OrderEventCollectionName);
        }
        public class CreateOrderRequest
        {
            public string BasketId { get; set; }
            public IEnumerable<Item> Items { get; set; }
        }

        public async Task CreateOrder(CreateOrderRequest request)
        {
            var total = 0;
            foreach (var item in request.Items)
            {
                total += item.Price * item.Quantity;
            };
            var order = new Models.Order
            {
                Total = total,
                OrderStatus = OrderStatus.Pending.ToString()
            };
            await _orderEventCollection.InsertOneAsync(order);
            //Publish event for order query
        }
        
        public class Item
        {
            public string ProductId { get; set; }
            public int Price { get; set; }
            public int Quantity { get; set; }
        }
    }
}