using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Confluent.Kafka;
using MongoDB.Driver;
using QTShop.Common.Helper;
using QTShop.Common.Models;
using QTShop.Order.Command.Models;

namespace QTShop.Order.Command.Repositories
{
    public class OrdersRepository : IOrdersRepository
    {
        private readonly IMongoCollection<Models.Order> _orderEventCollection;
        private readonly IProducer<string, KafkaMessage<OrderKafkaBody>> _producer;
        public OrdersRepository(IOrderEventCollectionDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _orderEventCollection = database.GetCollection<Models.Order>(settings.OrderEventCollectionName);
            var config = new ProducerConfig()
            {
                BootstrapServers = "localhost:9092"
            };
            _producer = new ProducerBuilder<string, KafkaMessage<OrderKafkaBody>>(config).SetValueSerializer(new CustomSerializer.CustomValueSerializer<KafkaMessage<OrderKafkaBody>>()).Build();

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
                OrderId = Guid.NewGuid().ToString(),
                OrderStatus = OrderStatus.Pending.ToString()
            };
            await _orderEventCollection.InsertOneAsync(order);
            var orderMessage = new KafkaMessage<OrderKafkaBody>
            {
                EventType = EventType.OrderPlaced.ToString(),
                EventId = Guid.NewGuid().ToString(),
                Body = new OrderKafkaBody
                {
                    OrderId = order.OrderId,
                    Total = order.Total.ToString(),
                    BasketId = request.BasketId,
                    OrderStatus = order.OrderStatus
                }
            };

            await _producer.ProduceAsync("QTShop",new Message<string, KafkaMessage<OrderKafkaBody>>()
            {
                Key = orderMessage.Body.OrderId,
                Value = orderMessage
            });
        }
        
        public class Item
        {
            public string ProductId { get; set; }
            public int Price { get; set; }
            public int Quantity { get; set; }
        }
    }
}