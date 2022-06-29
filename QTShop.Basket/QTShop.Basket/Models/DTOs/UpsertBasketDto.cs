using System.Collections.Generic;

namespace QTShop.Basket.Models.DTOs
{
    public class UpsertBasketDto
    {
        public string Id { get; set; }
        public List<BasketItem> Items { get; set; }
    }
}