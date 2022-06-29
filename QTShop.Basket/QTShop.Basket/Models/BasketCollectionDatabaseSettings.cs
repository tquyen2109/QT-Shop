namespace QTShop.Basket.Models
{
    public class BasketCollectionDatabaseSettings: IBasketCollectionDatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;

        public string BasketCollectionName { get; set; } = null!;
    }
    
    public interface IBasketCollectionDatabaseSettings
    {
        string BasketCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}