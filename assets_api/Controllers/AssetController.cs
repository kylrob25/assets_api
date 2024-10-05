using assets_api.Models;
using assets_api.Services;
using Microsoft.AspNetCore.Mvc;

namespace assets_api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AssetController(AssetService assetService) : ControllerBase
{
    [HttpGet]
    public async Task<List<Asset>> GetAssets() =>
        await assetService.GetAssets();

    [HttpPost]
    public async Task<IActionResult> CreateAsset([FromBody] Asset asset)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest("One or more attributes are missing.");
        }

        if (string.IsNullOrWhiteSpace(asset.Manufacturer) ||
            !assetService.ExistsManufacturer(asset.Manufacturer))
        {
            return BadRequest($"Manufacturer '{asset.Manufacturer}' does not exist.");
        }

        if (string.IsNullOrWhiteSpace(asset.AssetType) ||
            !assetService.ExistsAssetType(asset.AssetType))
        {
            return BadRequest($"AssetType '{asset.AssetType}' does not exist.");
        }
        
        await assetService.CreateAsset(asset);
        
        return CreatedAtAction(nameof(GetAssets), new { asset.Id }, asset);
    }
}