using assets_api.Models;
using assets_api.Services;
using Microsoft.AspNetCore.Mvc;

namespace assets_api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AssetController : ControllerBase
{
    private readonly AssetService _assetService;

    public AssetController(AssetService assetService)
    {
        _assetService = assetService;
    }
    
    [HttpGet]
    public async Task<List<Asset>> GetAssets() =>
        await _assetService.GetAssets();

    [HttpPost]
    public async Task<IActionResult> CreateAsset(Asset asset)
    {
        // TODO: Check valid variables e.g manufacturer
        await _assetService.CreateAsset(asset);
        
        return CreatedAtAction(nameof(GetAssets), new { asset.Id }, asset);
    }
}