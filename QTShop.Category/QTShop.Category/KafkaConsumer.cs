using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using QTShop.Category.Model;
using QTShop.Category.Repositories;

namespace QTShop.Category
{
    public class KafkaConsumer  : IHostedService
    {
        private readonly IProductsRepository _productRepository;

        public KafkaConsumer(IProductsRepository productRepository)
        {
            _productRepository = productRepository;
        }
        private readonly string topic = "QTShop";
        public Task StartAsync(CancellationToken cancellationToken)
        {
            var conf = new ConsumerConfig
            {
                GroupId = "Product",
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
                        var message = JsonSerializer.Deserialize<ProductKafkaMessage>(consumer.Message.Value);
                        switch (message.EventType.ToString())
                        {
                            case nameof(EventType.ProductQuantityUpdated):
                                _productRepository.UpdateProductQuantity(message.Body.ProductId,
                                    message.Body.Quantity);
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