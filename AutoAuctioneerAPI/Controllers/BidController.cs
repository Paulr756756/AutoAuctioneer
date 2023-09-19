using API_AutoAuctioneer.Models.BidRequestModels;
using API_AutoAuctioneer.Services.BidService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API_AutoAuctioneer.Controllers;

[ApiController, Route("api/[controller]")]
public class BidController : ControllerBase {
    private readonly IBidService _bidService;

    public BidController( IBidService bidService) {
        _bidService = bidService;
    }

    [HttpGet("getall")]
    public async Task<IActionResult> GetAllBids() {
        var response = await _bidService.GetAllBids();
        return Ok(response);
    }

    [HttpGet("getbyid")]
    public async Task<IActionResult> GetBidById(Guid id) {
        var response = await _bidService.GetBidById(id);
        if (response == null) return BadRequest("Bid doesn't exist");
        return Ok(response);
    }
    
    [HttpGet("getowned")]
    [Authorize(Roles = "Client")]
    public async Task<IActionResult> GetOwned([FromQuery] Guid id) {
        var response = await _bidService.GetOwned(id);
        return Ok(response);
    }

    [HttpPost("add")]
    [Authorize(Roles = "Client")]
    public async Task<IActionResult> AddBid([FromBody] AddBidRequest request) {
        var response = await _bidService.PostBid(request);
        if (!response) return BadRequest("Error x2");

        return Ok("Posted Bid");
    }

    [HttpDelete("delete")]
    [Authorize(Roles = "Client")]
    public async Task<IActionResult> DeleteBid([FromBody]DeleteBidRequest request) {
        var response = await _bidService.DeleteBidService(request);
        if (!response) return BadRequest("Error times three");

        return Ok("Success");
    }

    [HttpPatch("update")]
    [Authorize(Roles = "Client")]
    public async Task<IActionResult> UpdateBidAmt([FromBody]UpdateBidRequest request) {
        var response = await _bidService.UpdateBidAmt(request);
        return Ok(response);
    }
}