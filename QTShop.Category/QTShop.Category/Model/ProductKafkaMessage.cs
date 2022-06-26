namespace QTShop.Category.Model
{
    public class ProductKafkaMessage
    {
        public string EventType { get; set; }
        public KafkaBody Body { get; set; }
    }

    public class KafkaBody
    {
        public string ProductId { get; set; }
        public string Price { get; set; }
        public string Quantity { get; set; }
        public string Name { get; set; }
    }
}