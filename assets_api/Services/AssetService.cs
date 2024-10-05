using System.Collections.Concurrent;
using assets_api.Models;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using System.Linq;
using MongoDB.Driver;

namespace assets_api.Services;

public class AssetService
{
    private readonly IMongoCollection<Manufacturer> _manufacturersCollection;
    private readonly IMongoCollection<AssetType> _assetTypesCollection;
    private readonly IMongoCollection<Asset> _assetsCollection;

    private readonly ConcurrentBag<string> _manufacturers;
    private readonly ConcurrentBag<string> _assetTypes;

    public AssetService()
    {
        var mongoClient = new MongoClient("mongodb+srv://admin:Omp8mr1cGhWzDfxv@epos.dt9oe8x.mongodb.net/?retryWrites=true&w=majority");
        var mongoDatabase = mongoClient.GetDatabase("Assets");
        
        _manufacturersCollection = mongoDatabase.GetCollection<Manufacturer>("Manufacturers");
        _assetTypesCollection = mongoDatabase.GetCollection<AssetType>("AssetTypes");
        _assetsCollection = mongoDatabase.GetCollection<Asset>("Assets");

        _manufacturers = _assetTypes = [];
    }
    
    public async Task<List<Asset>> GetAssets() =>
        await _assetsCollection.Find(_ => true).ToListAsync();
    
    public async Task<ConcurrentBag<string>> GetAssetTypes()
    {
        if (!_assetTypes.IsEmpty) return _assetTypes;
        var documents = await _assetTypesCollection.Find(_ => true).ToListAsync();
        foreach (var document in documents)
        {
            if (document.Name != null) _assetTypes.Add(document.Name);
        }

        return _assetTypes;
    }

    public async Task<ConcurrentBag<string>> GetManufacturers()
    {
        if (!_manufacturers.IsEmpty) return _manufacturers;
        var documents = await _manufacturersCollection.Find(_ => true).ToListAsync();
        foreach (var document in documents)
        {
            if (document.Name != null) _manufacturers.Add(document.Name);
        }

        return _manufacturers;
    }
    
    public async Task CreateAsset(Asset asset) =>
        await _assetsCollection.InsertOneAsync(asset);
    
    public async Task CreateAssetType(AssetType assetType) =>
        await _assetTypesCollection.InsertOneAsync(assetType);
    
    public async Task CreateManufacturer(Manufacturer manufacturer) =>
        await _manufacturersCollection.InsertOneAsync(manufacturer);
    
    public bool ExistsManufacturer(string name)
    {
        return _manufacturers.Contains(name);
    }

    public bool ExistsAssetType(string name)
    {
        return _assetTypes.Contains(name);
    }
}