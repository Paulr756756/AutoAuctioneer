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

    [HttpGet("getallbids")]
    public async Task<IActionResult> GetAllBids() {
        var response = await _bidService.GetAllBids();
        return Ok(response);
    }

    [HttpGet("getbidbyid")]
    public async Task<IActionResult> GetBidById(Guid id) {
        var response = await _bidService.GetBidById(id);
        if (response == null) return BadRequest("Bid doesn't exist");
        return Ok(response);
    }

    [HttpPost("addbid")]
    [Authorize(Roles = "Client")]
    public async Task<IActionResult> AddBid([FromBody] AddBidRequest request) {
        var response = await _bidService.PostBid(request);
        if (!response) return BadRequest("Error x2");

        return Ok();
    }

    [HttpDelete("deleteBid")]
    [Authorize(Roles = "Client")]
    public async Task<IActionResult> DeleteBid([FromBody]DeleteBidRequest request) {
        var response = await _bidService.DeleteBidService(request);
        if (!response) return BadRequest("Error times three");

        return Ok("Success");
    }

    [HttpPatch("updateBid")]
    [Authorize(Roles = "Client")]
    public async Task<IActionResult> UpdateBidAmt([FromBody]UpdateBidRequest request) {
        var response = await _bidService.UpdateBidAmt(request);
        if (!response) return BadRequest("Error maximo");
        return Ok("Successo");
    }

    //TODO("Get by ownership)
}