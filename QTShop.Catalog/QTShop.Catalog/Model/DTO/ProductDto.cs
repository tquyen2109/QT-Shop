using Microsoft.AspNetCore.Http;

namespace QTShop.Catalog.Model.DTO
{
    public class ProductDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string Brand { get; set; }
        public IFormFile PictureFileStream { get; set; }
        public int Price { get; set; }
    }
}