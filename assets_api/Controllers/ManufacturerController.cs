using System.Collections.Concurrent;
using assets_api.Models;
using assets_api.Services;
using Microsoft.AspNetCore.Mvc;

namespace assets_api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ManufacturerController(AssetService assetService) : ControllerBase
{
    [HttpGet]
    public async Task<ConcurrentBag<string>> GetManufacturers() =>
        await assetService.GetManufacturers();

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Manufacturer manufacturer)
    {
        if (!ModelState.IsValid) return BadRequest("One or more attributes are missing.");
        if (string.IsNullOrWhiteSpace(manufacturer.Name)) return BadRequest("Name attribute is required.");
        if (assetService.ExistsManufacturer(manufacturer.Name)) return BadRequest("Manufacturer already exists.");
        
        await assetService.CreateManufacturer(manufacturer);
        
        return CreatedAtAction(nameof(GetManufacturers), new { manufacturer.Id }, manufacturer);
    }

}