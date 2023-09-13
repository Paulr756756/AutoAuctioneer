using API_AutoAuctioneer.Models.PartRequestModels;
using API_AutoAuctioneer.Services.PartService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API_AutoAuctioneer.Controllers;

[ApiController, Route("api/[controller]")]
public class PartsController : ControllerBase {
    private readonly IPartService _carPartService;

    public PartsController(IPartService carPartService) {
        _carPartService = carPartService;
    }

    [HttpGet("getall")]
    public async Task<IActionResult> GetAllParts() {
        var partsList = await _carPartService.GetAllParts();
        return Ok(partsList);
    }

    [HttpGet("getowned"), Authorize(Roles = "Client")]
    public async Task<IActionResult> GetOwnedParts([FromQuery] Guid id) {

        var partsList = await _carPartService.GetOwnedPartsService(id);
        return Ok(partsList);
    }

    [HttpGet("getbyid")]
    public async Task<IActionResult> GetPartById(Guid guid) {
        var response = await _carPartService.GetPartById(guid);
        return Ok(response);
    }

    [HttpPost("post"), Authorize(Roles = "Client")]
    public async Task<IActionResult> PostCarPart([FromBody] AddPartRequest request) {
        var response = await _carPartService.AddPart(request);
        if (response) {
            return Ok(response);
        }

        return BadRequest(response);
    }

    [HttpPut("update"), Authorize(Roles = "Client")]
    public async Task<IActionResult> UpdatePart([FromBody] UpdatePartRequest request) {
        var response = await _carPartService.UpdatePart(request);
        if (response) {
            return Ok(response);
        }

        return BadRequest(response);
    }

    [HttpDelete("delete"), Authorize(Roles = "Client")]
    public async Task<IActionResult> DeleteCar([FromBody] DeletePartRequest request) {
        var response = await _carPartService.DeletePart(request);
        if (response) {
            return Ok(response);
        }
        return BadRequest(response);
    }
}