using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace assets_api.Models;

public class Asset
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    
    [BsonElement("name")] 
    [Required]
    public string? Name { get; set; }
    
    [BsonElement("type")]
    [Required]
    public string? AssetType { get; set; }
    
    [BsonElement("manufacturer")]
    [Required]
    public string? Manufacturer { get; set; }
    
    [BsonElement("quantity")]
    [Required]
    public int? Quantity { get; set; }
}