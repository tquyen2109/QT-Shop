namespace QTShop.Common.Models
{
    public class ProductKafkaMessage
    {
        public string EventType { get; set; }
        public string EventId { get; set; }
        public ProductKafkaBody Body { get; set; }
    }

    public class ProductKafkaBody
    {
        public string ProductId { get; set; }
        public string Price { get; set; }
        public string Quantity { get; set; }
        public string Name { get; set; }
    }
}