using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Confluent.Kafka;
using Dapper;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using QTShop.Category.Helper;
using QTShop.Category.Model;

namespace QTShop.Category.Repositories
{
    public class ProductsRepository : IProductsRepository
    {
        private readonly IOutboxRepository _outboxRepository;
        private readonly IMongoCollection<Product> _productCollection;

        public ProductsRepository(IProductCollectionDatabaseSettings settings, IOutboxRepository outboxRepository)
        {
            _outboxRepository = outboxRepository;
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _productCollection = database.GetCollection<Product>(settings.ProductCollectionName);
        }
        public async Task<Product> GetProductById(string id)
        {
            return await _productCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _productCollection.Find(_ => true).ToListAsync();
        }

        public async Task CreateProduct(Product product)
        {
            await _productCollection.InsertOneAsync(product);
            var message = new ProductKafkaMessage()
            {
                EventType = EventType.ProductCreated.ToString(),
                Body = new KafkaBody()
                {
                    ProductId = product.Id,
                    Quantity = "0",
                    Price = product.Price,
                    Name = product.Name
                }
            };
            await _outboxRepository.CreateOutboxMessage(new OutboxMessage
            (
                message,
               Guid.NewGuid().ToString(),
                DateTime.Now
            ));
        }

        public async Task UpdateProduct(Product product)
        {
            var currentProduct = await _productCollection.Find(x => x.Id == product.Id).FirstOrDefaultAsync();
            currentProduct.Brand = product.Brand;
            currentProduct.Name = product.Name;
            currentProduct.Description = product.Description;
            currentProduct.Type = product.Type;
            currentProduct.PictureUrl = product.PictureUrl;
            currentProduct.Name = product.Name;
            await _productCollection.ReplaceOneAsync(p=> p.Id == product.Id,currentProduct);
            var message = new ProductKafkaMessage()
            {
                EventType = EventType.ProductUpdated.ToString(),
                Body = new KafkaBody()
                {
                    ProductId = product.Id,
                    Name = product.Name
                }
            };
            await _outboxRepository.CreateOutboxMessage(new OutboxMessage
            (
                message,
                Guid.NewGuid().ToString(),
                DateTime.Now
            ));
        }

        public async Task UpdateProductQuantity(string id, string quantity)
        {
            try
            {
                var currentProduct = await _productCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
                currentProduct.Quantity = quantity;
                await _productCollection.ReplaceOneAsync(p=> p.Id == id, currentProduct);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
           
        }
    }
}