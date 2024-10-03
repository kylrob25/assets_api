using assets_api.Models;
using assets_api.Services;
using Microsoft.AspNetCore.Mvc;

namespace assets_api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ManufacturerController : ControllerBase
{
    private readonly AssetService _assetService;

    public ManufacturerController(AssetService assetService)
    {
        _assetService = assetService;
    }

    [HttpGet]
    public async Task<List<Manufacturer>> GetManufacturers() =>
        await _assetService.GetManufacturers();

    [HttpPost]
    public async Task<IActionResult> Post(Manufacturer manufacturer)
    {
        await _assetService.CreateManufacturer(manufacturer);
        
        return CreatedAtAction(nameof(GetManufacturers), new { manufacturer.Id }, manufacturer);
    }

}