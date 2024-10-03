using assets_api.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace assets_api.Services;

public class AssetService
{
    private readonly IMongoCollection<Manufacturer> _manufacturersCollection;
    private readonly IMongoCollection<AssetType> _assetTypesCollection;
    private readonly IMongoCollection<Asset> _assetsCollection;

    public AssetService()
    {
        var mongoClient = new MongoClient("mongodb+srv://admin:Omp8mr1cGhWzDfxv@epos.dt9oe8x.mongodb.net/?retryWrites=true&w=majority");
        var mongoDatabase = mongoClient.GetDatabase("Assets");
        
        _manufacturersCollection = mongoDatabase.GetCollection<Manufacturer>("Manufacturers");
        _assetTypesCollection = mongoDatabase.GetCollection<AssetType>("AssetTypes");
        _assetsCollection = mongoDatabase.GetCollection<Asset>("Assets");
    }
    
    public async Task<List<Asset>> GetAssets() =>
        await _assetsCollection.Find(_ => true).ToListAsync();
    
    public async Task<List<AssetType>> GetAssetTypes() =>
        await _assetTypesCollection.Find(_ => true).ToListAsync();
    
    public async Task<List<Manufacturer>> GetManufacturers() =>
        await _manufacturersCollection.Find(m => true).ToListAsync();
    
    public async Task CreateAsset(Asset asset) =>
        await _assetsCollection.InsertOneAsync(asset);
    
    public async Task CreateAssetType(AssetType assetType) =>
        await _assetTypesCollection.InsertOneAsync(assetType);
    
    public async Task CreateManufacturer(Manufacturer manufacturer) =>
        await _manufacturersCollection.InsertOneAsync(manufacturer);
}