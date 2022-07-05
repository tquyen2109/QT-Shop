namespace QTShop.Common.Models
{
    public class KafkaMessage<T>
    {
        public string EventType { get; set; }
        public string EventId { get; set; }
        public T Body { get; set; }
    }

    public class ProductKafkaBody
    {
        public string ProductId { get; set; }
        public string Price { get; set; }
        public string Quantity { get; set; }
        public string Name { get; set; }
    }
    
    public class OrderKafkaBody
    {
        public string OrderId { get; set; }
        public string Total { get; set; }
        public string BasketId { get; set; }
        public string OrderStatus { get; set; }
    }
    
    public class OrderCancelledKafkaBody
    {
        public string OrderId { get; set; }
        public string OrderStatus { get; set; }
        public string Reason { get; set; }
    }
}