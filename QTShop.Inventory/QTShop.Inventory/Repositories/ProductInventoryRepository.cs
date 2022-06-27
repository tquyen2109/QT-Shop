using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Confluent.Kafka;
using Dapper;
using Microsoft.Extensions.Configuration;
using QTShop.Inventory.Helper;
using QTShop.Inventory.Model;


namespace QTShop.Inventory.Repositories
{
    public class ProductInventoryRepository : IProductInventoryRepository
    {
        private readonly string connectionString;
        private readonly IProducer<string, ProductKafkaMessage> _producer;
        public ProductInventoryRepository(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
            var config = new ProducerConfig()
            {
                BootstrapServers = "localhost:9092"
            };
            _producer = new ProducerBuilder<string, ProductKafkaMessage>(config).SetValueSerializer(new CustomerSerializer.CustomValueSerializer<ProductKafkaMessage>()).Build();

        }

        public async Task CreateProductInInventory(string productId, string productName)
        {
            try
            {
                await using var db = new SqlConnection(connectionString);
                await db.OpenAsync();
                var sql =
                    $"INSERT INTO dbo.ProductInventory (ProductId, ProductName, Quantity) VALUES ('{productId}', '{productName}', 0)";
                await db.ExecuteAsync(sql);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        
        public async Task UpdateProductInInventory(string productId, string productName)
        {
            try
            {
                await using var db = new SqlConnection(connectionString);
                await db.OpenAsync();
                var sql =
                    $"UPDATE dbo.ProductInventory SET ProductName = '{productName}' WHERE ProductId = '{productId}'";
                await db.ExecuteAsync(sql);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task UpdateProductQuantity(string productId, int quantity)
        {
            try
            {
                await using var db = new SqlConnection(connectionString);
                await db.OpenAsync();
                var sql =
                    $"UPDATE dbo.ProductInventory SET Quantity = {quantity} WHERE ProductId = '{productId}'";
                await db.ExecuteAsync(sql);
                var message = new ProductKafkaMessage()
                {
                    EventType = EventType.ProductQuantityUpdated.ToString(),
                    Body = new KafkaBody()
                    {
                        ProductId = productId,
                        Quantity = quantity.ToString()
                    }
                };
                await _producer.ProduceAsync("QTShop", new Message<string, ProductKafkaMessage>()
                {
                    Key = productId,
                    Value =  message
                });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}