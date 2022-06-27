namespace QTShop.Inventory.Model
{
    public class ProductKafkaMessage
    {
        public string EventType { get; set; }
        public KafkaBody Body { get; set; }
    }
}