using System;
using System.Text.Json;
using System.Threading.Tasks;
using Confluent.Kafka;
using Microsoft.Extensions.Logging;
using QTShop.Category.Helper;
using QTShop.Category.Model;
using QTShop.Category.Repositories;
using Quartz;

namespace QTShop.Category.ServiceWorker
{
    [DisallowConcurrentExecution]
    public class OutboxJob : IJob
    {
        private readonly ILogger<OutboxJob> logger;
        private readonly IOutboxRepository repository;
        private readonly IProducer<string, ProductKafkaMessage> _producer;
        
        public OutboxJob(ILogger<OutboxJob> logger,
            IOutboxRepository repository)
        {
            this.logger = logger;
            this.repository = repository;
            var config = new ProducerConfig()
            {
                BootstrapServers = "localhost:9092"
            };
            _producer = new ProducerBuilder<string, ProductKafkaMessage>(config).SetValueSerializer(new CustomerSerializer.CustomValueSerializer<ProductKafkaMessage>()).Build();
        }
        public async Task Execute(IJobExecutionContext context)
        {
            var readyToSendItems = await repository.GetAllReadyToSend();
            logger.LogInformation($"Outbox count {readyToSendItems.Count}:date : {DateTime.Now.ToLongTimeString()}");

            foreach (var item in readyToSendItems)
            {
                var eventMessage = JsonSerializer.Deserialize<ProductKafkaMessage>(item.Data);
                await _producer.ProduceAsync("QTShop",new Message<string, ProductKafkaMessage>()
                {
                    Key = eventMessage.Body.ProductId,
                    Value = eventMessage
                });
                await repository.UpdateOutboxMessageState(item.EventId, OutboxMessageState.Sent);
            }
        }
    }
}