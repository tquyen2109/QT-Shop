using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using QTShop.Catalog.Model;
using QTShop.Common.Models;

namespace QTShop.Catalog.Repositories
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
            var message = new KafkaMessage<ProductKafkaBody>()
            {
                EventType = EventType.ProductCreated.ToString(),
                Body = new ProductKafkaBody()
                {
                    ProductId = product.Id,
                    Quantity = "0",
                    Price = product.Price.ToString(),
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
            var message = new KafkaMessage<ProductKafkaBody>()
            {
                EventType = EventType.ProductUpdated.ToString(),
                Body = new ProductKafkaBody()
                {
                    ProductId = product.Id,
                    Name = product.Name,
                    Price = product.Price.ToString()
                }
            };
            await _outboxRepository.CreateOutboxMessage(new OutboxMessage
            (
                message,
                Guid.NewGuid().ToString(),
                DateTime.Now
            ));
        }

        public async Task UpdateProductQuantity(string id, int quantity)
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