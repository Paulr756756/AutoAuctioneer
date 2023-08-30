using API_AutoAuctioneer.Models.CarPartRequestModels;
using API_AutoAuctioneer.Services.CarPartService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API_AutoAuctioneer.Controllers;

[ApiController, Route("api/[controller]")]
public class PartsController : ControllerBase{
    private readonly ICarPartService _carPartService;
    
    public PartsController(ICarPartService carPartService) {
        _carPartService = carPartService;
    }

    [HttpGet("getall")]
    public async Task<IActionResult> GetAllParts() {
        var partsList = await _carPartService.GetAllCarPartsService();
        return Ok(partsList);
    }

    [HttpGet("getowned"), Authorize(Roles = "Client")]
    public async Task<IActionResult> GetOwnedParts([FromQuery] Guid id) {

        var partsList = await _carPartService.GetOwnedCarPartsService(id);
        return Ok(partsList);
    }

    [HttpGet("getbyid")]
    public async Task<IActionResult> GetPartById(Guid guid) {
        var response = await _carPartService.GetCarPartById(guid);
        return Ok(response);
    }

    [HttpPost("post"), Authorize(Roles = "Client")]
    public async Task<IActionResult> PostCarPart([FromBody] AddCarPartRequest request) {
        var response = await _carPartService.AddCarPart(request);
        if (response) {
            return Ok(response);
        }

        return BadRequest(response);
    }

    [HttpPut("update"), Authorize(Roles = "Client")]
    public async Task<IActionResult> UpdatePart([FromBody] UpdateCarPartRequest request) {
        var response = await _carPartService.UpdateCarPart(request);
        if (response) {
            return Ok(response);
        }

        return BadRequest(response);
    }

    [HttpDelete("delete"), Authorize(Roles = "Client")]
    public async Task<IActionResult> DeleteCar([FromBody] DeleteCarPartRequest request) {
        var response = await _carPartService.DeleteCarPart(request);
        if (response) {
            return Ok(response);
        }
        return BadRequest(response);
    }
}