using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace QTShop.Catalog.Model
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string Brand { get; set; }
        public string PictureUrl { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }
    }
}