namespace QTShop.Inventory.Model
{
    public class KafkaBody
    {
        public string ProductId { get; set; }
        public string Quantity { get; set; }
        public string Name { get; set; }
        public string Price { get; set; }
    }
}