using System.Threading.Tasks;

namespace QTShop.Inventory.Repositories
{
    public interface IProductInventoryRepository
    {
        public Task CreateProductInInventory(string productId, string productName);
        public Task UpdateProductInInventory(string productId, string productName);
        public Task UpdateProductQuantity(string productId, int quantity);
    }
}