using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace assets_api.Models;

public class AssetType
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    
    [BsonElement("Name")] public string? Name { get; set; }
}