using assets_api.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace assets_api.Services;

public class AssetService
{
    private readonly IMongoCollection<Manufacturer> _manufacturersCollection;

    public AssetService()
    {
        var mongoClient = new MongoClient("mongodb+srv://admin:Omp8mr1cGhWzDfxv@epos.dt9oe8x.mongodb.net/?retryWrites=true&w=majority");
        var mongoDatabase = mongoClient.GetDatabase("Assets");
        
        _manufacturersCollection = mongoDatabase.GetCollection<Manufacturer>("Manufacturers");
    }
    
    public async Task<List<Manufacturer>> GetManufacturers() =>
        await _manufacturersCollection.Find(m => true).ToListAsync();
    
    public async Task CreateManufacturer(Manufacturer manufacturer) =>
        await _manufacturersCollection.InsertOneAsync(manufacturer);
}