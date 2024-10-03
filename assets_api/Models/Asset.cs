using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace assets_api.Models;

public class Asset
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    
    [BsonElement("name")] public string? Name { get; set; }
    
    [BsonElement("type")] public string? Type { get; set; }
    
    [BsonElement("manufacturer")] public string? Manufacturer { get; set; }
    
    [BsonElement("quantity")] public int? Quantity { get; set; }
}