using System;
using System.Threading;
using System.Threading.Tasks;
using System.Text.Json;
using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using QTShop.Inventory.Model;
using QTShop.Inventory.Repositories;

namespace QTShop.Inventory
{
    public class KafkaConsumer : IHostedService
    {
        private readonly IProductInventoryRepository _productInventoryRepository;

        public KafkaConsumer(IProductInventoryRepository productInventoryRepository)
        {
            _productInventoryRepository = productInventoryRepository;
        }
        private readonly string topic = "QTShop";
        public Task StartAsync(CancellationToken cancellationToken)
        {
            var conf = new ConsumerConfig
            {
                GroupId = "Inventory",
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
                        var message = JsonSerializer.Deserialize<Message>(consumer.Message.Value);
                        switch (message.EventType.ToString())
                        {
                            case nameof(EventType.ProductCreated):
                                _productInventoryRepository.CreateProductInInventory(message.Body.ProductId,
                                    message.Body.Name);
                                break;
                            case nameof(EventType.ProductUpdated):
                                Console.WriteLine($"ProductUpdated -- Message: {consumer.Message.Value} received from {consumer.TopicPartitionOffset}");
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