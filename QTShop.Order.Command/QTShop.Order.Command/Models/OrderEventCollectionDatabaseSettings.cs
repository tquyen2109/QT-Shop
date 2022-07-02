namespace QTShop.Order.Command.Models
{
    public class OrderEventCollectionDatabaseSettings: IOrderEventCollectionDatabaseSettings
        {
            public string ConnectionString { get; set; } = null!;

            public string DatabaseName { get; set; } = null!;

            public string OrderEventCollectionName { get; set; } = null!;
        }
    
        public interface IOrderEventCollectionDatabaseSettings
        {
            string OrderEventCollectionName { get; set; }
            string ConnectionString { get; set; }
            string DatabaseName { get; set; }
        }
}