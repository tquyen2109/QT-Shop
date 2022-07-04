using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using QTShop.Common.Models;

namespace QTShop.Order.Query
{
  public class KafkaConsumer : IHostedService
    {
        private readonly IOrderRepository _orderRepository;

        public KafkaConsumer(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        private readonly string topic = "QTShop";
        public Task StartAsync(CancellationToken cancellationToken)
        {
            var conf = new ConsumerConfig
            {
                GroupId = "Order",
                BootstrapServers = "localhost:9092",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };
            using (var builder = new ConsumerBuilder<Ignore, 
                string>(conf).Build())
            {
                builder.Subscribe(topic);
                var cancelToken = new CancellationTokenSource();
                try
                {
                    while (true)
                    {
                        var consumer = builder.Consume(cancelToken.Token);
                        var eventType = JsonSerializer.Deserialize<KafkaMessage<object>>(consumer.Message.Value);
                        switch (eventType.EventType)
                        {
                            case nameof(EventType.OrderPlaced):
                                 var createMessage = JsonSerializer.Deserialize<KafkaMessage<OrderKafkaBody>>(consumer.Message.Value);
                                _orderRepository.CreateOrder(new OrderRepository.CreateOrderRequest
                                {
                                    OrderId = createMessage.Body.OrderId,
                                    OrderStatus = createMessage.Body.OrderStatus,
                                    Total = Int32.Parse(createMessage.Body.Total)
                                });
                                break;
                            default:
                                Console.WriteLine($"No event type match");
                                break;
                        }
                    }
                }
                catch (Exception e)
                {
                    builder.Close();
                }
            }
            return Task.CompletedTask;
        }
        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}