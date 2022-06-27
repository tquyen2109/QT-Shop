namespace QTShop.Inventory.Model.DTOs
{
    public class UpdateProductQuantityDto
    {
        public string ProductId { get; set; }
        public int Quantity { get; set; }
    }
}