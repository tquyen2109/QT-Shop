namespace QTShop.Inventory.Model
{
    public class Message
    {
        public EventType EventType { get; set; }
        public ProductInventory Body { get; set; }
    }
}