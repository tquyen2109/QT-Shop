using System;
using System.Text.Json;
using System.Threading.Tasks;
using Confluent.Kafka;
using Microsoft.Extensions.Logging;
using QTShop.Catalog.Model;
using QTShop.Catalog.Repositories;
using QTShop.Common.Helper;
using QTShop.Common.Models;
using Quartz;

namespace QTShop.Catalog.ServiceWorker
{
    [DisallowConcurrentExecution]
    public class OutboxJob : IJob
    {
        private readonly ILogger<OutboxJob> logger;
        private readonly IOutboxRepository repository;
        private readonly IProducer<string, KafkaMessage<ProductKafkaBody>> _producer;
        
        public OutboxJob(ILogger<OutboxJob> logger,
            IOutboxRepository repository)
        {
            this.logger = logger;
            this.repository = repository;
            var config = new ProducerConfig()
            {
                BootstrapServers = "localhost:9092"
            };
            _producer = new ProducerBuilder<string, KafkaMessage<ProductKafkaBody>>(config).SetValueSerializer(new CustomSerializer.CustomValueSerializer<KafkaMessage<ProductKafkaBody>>()).Build();
        }
        public async Task Execute(IJobExecutionContext context)
        {
            var readyToSendItems = await repository.GetAllReadyToSend();
            logger.LogInformation($"Outbox count {readyToSendItems.Count}:date : {DateTime.Now.ToLongTimeString()}");

            foreach (var item in readyToSendItems)
            {
                var eventMessage = JsonSerializer.Deserialize<KafkaMessage<ProductKafkaBody>>(item.Data);
                eventMessage.EventId = item.EventId;
                await _producer.ProduceAsync("QTShop",new Message<string, KafkaMessage<ProductKafkaBody>>()
                {
                    Key = eventMessage.Body.ProductId,
                    Value = eventMessage
                });
                await repository.UpdateOutboxMessageState(item.EventId, OutboxMessageState.Sent);
            }
        }
    }
}