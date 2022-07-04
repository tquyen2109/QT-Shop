using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using QTShop.Basket.Repositories;
using QTShop.Common.Models;

namespace QTShop.Basket
{
    public class KafkaConsumer :IHostedService
    {
        private readonly IBasketRepository _basketRepository;
        private readonly string topic = "QTShop";

        public KafkaConsumer(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            var conf = new ConsumerConfig
            {
                GroupId = "Basket",
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
                            case nameof(EventType.ProductUpdated):
                                var productUpdatedMessage = JsonSerializer.Deserialize<KafkaMessage<ProductKafkaBody>>(consumer.Message.Value);
                                _basketRepository.UpdatePrice(productUpdatedMessage.Body.ProductId, Int32.Parse(productUpdatedMessage.Body.Price), productUpdatedMessage.Body.Name);
                                break;
                            case nameof(EventType.OrderPlaced):
                                var orderPlacedMessage = JsonSerializer.Deserialize<KafkaMessage<OrderKafkaBody>>(consumer.Message.Value);
                                _basketRepository.DeleteBasket(orderPlacedMessage.Body.BasketId);
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