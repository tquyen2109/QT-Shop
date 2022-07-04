using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace QTShop.Order.Command.Models
{
    public class Order
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string OrderId { get; set; }

        public int Total { get; set; }
        public string OrderStatus { get; set; }
      
    }
}