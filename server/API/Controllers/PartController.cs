using API.Models.RequestModels;
using API.Services.PartService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController, Route("api/[controller]")]
public class PartController : ControllerBase {
    private readonly IPartService _carPartService;
    public PartController(IPartService carPartService) {
        _carPartService = carPartService;
    }
    [HttpGet("getall")]
    public async Task<IActionResult> GetAllParts() {
        var partsList = await _carPartService.GetAllParts();
        return Ok(partsList);
    }
    [HttpGet("getowned"), Authorize(Roles = "Client")]
    public async Task<IActionResult> GetOwned([FromQuery] Guid id) {
        var partsList = await _carPartService.GetOwned(id);
        return Ok(partsList);
    }
    [HttpGet("getbyid")]
    public async Task<IActionResult> GetPartById([FromQuery] Guid id) {
        var response = await _carPartService.GetPartById(id);
        return Ok(response);
    }
    [HttpPost("add"), Authorize(Roles = "Client")]
    public async Task<IActionResult> AddPart([FromBody] AddPartRequest request) {
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
    public async Task<IActionResult> DeletePart([FromBody] DeletePartRequest request) {
        var response = await _carPartService.DeletePart(request);
        if (response) {
            return Ok(response);
        }
        return BadRequest(response);
    }
}