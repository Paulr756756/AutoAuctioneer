/*using API_BidStamp.Models.StampRequestModels;
using API_BidStamp.Services.StampService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API_BidStamp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StampController : ControllerBase {
    private readonly IStampService _stamp_service;

    /*private readonly ILogger _logger;#1#
    public StampController(IStampService stamp_service) {
        _stamp_service = stamp_service;
        /*_logger = logger;#1#
    }

    [HttpGet]
    public async Task<IActionResult> GetAllStamps() {
        var response = await _stamp_service.getAllStamps();
        return Ok(response);
    }

    [HttpGet("getstampbyid")]
    [Authorize(Roles = "Client")]
    public async Task<IActionResult> GetStampById(Guid id) {
        var stamp = await _stamp_service.getStampById(id);

        return Ok(stamp);
    }

    [HttpPost("addstamp")]
    [Authorize(Roles = "Client")]
    public async Task<ActionResult> AddStamp(AddStampRequest request, Guid user_id) {
        if (!await _stamp_service.addStamp(request, user_id))
            return BadRequest("Couldn't add stamp");
        return Ok($"Added stamp successfully:{request.StampTitle}");
    }

    [HttpPut("updatestamp")]
    [Authorize(Roles = "Client")]
    public async Task<IActionResult> UpdateStamp
        (UpdateStampRequest request, Guid stamp_id, Guid user_id) {
        if (!await _stamp_service.updateStamp(request, stamp_id, user_id))
            return BadRequest("Stamp couldn't be updated");

        return Ok($"Stamp updated successfully : {request.StampTitle}");
    }

    [HttpDelete("deletestamp")]
    [Authorize(Roles = "Client")]
    public async Task<IActionResult> DeleteStamp(Guid stamp_id, Guid user_id) {
        if (!await _stamp_service.deleteStamp(stamp_id, user_id))
            return BadRequest("Stamp couldn't be deleted");
        return Ok($"Deleted stamp with id {stamp_id}");
    }
}*/

