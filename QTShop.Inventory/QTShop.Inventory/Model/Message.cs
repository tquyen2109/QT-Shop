namespace QTShop.Inventory.Model
{
    public class Message
    {
        public string EventType { get; set; }
        public ProductInventory Body { get; set; }
    }
}