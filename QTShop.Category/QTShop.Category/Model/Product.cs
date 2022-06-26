using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace QTShop.Category.Model
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
        public string Price { get; set; }
        public string Quantity { get; set; }
    }
}