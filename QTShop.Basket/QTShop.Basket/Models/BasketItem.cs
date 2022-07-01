namespace QTShop.Basket.Models
{
    public class BasketItem
    {
        public string ProductId { get; set; }
        public string Name { get; set; }
        public long Price { get; set; }
        public int Quantity { get; set; }
    }
}