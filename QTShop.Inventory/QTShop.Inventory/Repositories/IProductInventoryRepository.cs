using System.Threading.Tasks;

namespace QTShop.Inventory.Repositories
{
    public interface IProductInventoryRepository
    {
        public Task CreateProductInInventory(string productId, string productName);
    }
}