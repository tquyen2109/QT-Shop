namespace QTShop.Catalog.Model
{
    public class ProductCollectionDatabaseSettings : IProductCollectionDatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;

        public string ProductCollectionName { get; set; } = null!;
    }
    
    public interface IProductCollectionDatabaseSettings
    {
        string ProductCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}