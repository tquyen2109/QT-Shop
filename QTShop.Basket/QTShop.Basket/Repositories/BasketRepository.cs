using System;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using QTShop.Basket.Models;


namespace QTShop.Basket.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IMongoCollection<Models.Basket> _basketCollection;

        public BasketRepository(IBasketCollectionDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _basketCollection = database.GetCollection<Models.Basket>(settings.BasketCollectionName);
        }
        public async Task<Models.Basket> GetBasketById(string id)
        {
            return await _basketCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task UpsertBasket(Models.Basket basket)
        {
            var basketFromDb = await _basketCollection.Find(x => x.Id == basket.Id).FirstOrDefaultAsync();
            if (basketFromDb is null)
            {
                await _basketCollection.InsertOneAsync(basket);
            }
            else
            {
                await _basketCollection.ReplaceOneAsync(p=> p.Id == basket.Id, basket);
            }
        }

        public async Task UpdatePrice(string productId, int price, string name)
        {
            try
            {
                var basketsWithProduct = await _basketCollection.Find(x => x.Items.Any(i => i.ProductId == productId)).ToListAsync();
                foreach (var basket in basketsWithProduct)
                {
                    var newItem = new BasketItem()
                    {
                        ProductId = productId,
                        Name = name,
                        Price = price,
                        Quantity = basket.Items.FirstOrDefault(i => i.ProductId == productId).Quantity
                    };
                    basket.Items.Remove(basket.Items.FirstOrDefault(i => i.ProductId == productId));
                    basket.Items.Add(newItem);
                    await _basketCollection.ReplaceOneAsync(p=> p.Id == basket.Id, basket);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task DeleteBasket(string basketId)
        {
            try
            {
                await _basketCollection.DeleteOneAsync(b => b.Id == basketId);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}