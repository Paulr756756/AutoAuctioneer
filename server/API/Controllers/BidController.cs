using API.Models.RequestModels;
using API.Services.BidService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BidController : ControllerBase
{
    private readonly IBidService _bidService;

    public BidController(IBidService bidService)
    {
        _bidService = bidService;
    }

    [HttpGet("getall")]
    public async Task<IActionResult> GetAllBids()
    {
        var response = await _bidService.GetAllBids();
        return Ok(response);
    }

    [HttpGet("get")]
    public async Task<IActionResult> GetBidById([FromQuery] Guid id)
    {
        var response = await _bidService.GetBidById(id);
        if (response == null) return BadRequest("Bid doesn't exist");
        return Ok(response);
    }

    [HttpGet("get/user")]
    [Authorize(Roles = "Client")]
    public async Task<IActionResult> GetOwned([FromQuery] Guid id)
    {
        var response = await _bidService.GetOwned(id);
        return Ok(response);
    }

    [HttpGet("get/listing")]
    public async Task<IActionResult> GetBidsPerListing([FromQuery] Guid listingid)
    {
        var response = await _bidService.GetBidsPerListing(listingid);
        return Ok(response);
    }

    [HttpPost("get/listing/user")]
    public async Task<IActionResult> GetBidsPerListingOfSingleUser([FromBody] BidsListingUserRequest request)
    {
        var response = await _bidService.GetBidsOfSingleUserPerListing(request);
        return Ok(response);
    }

    [HttpPost("add")]
    [Authorize(Roles = "Client")]
    public async Task<IActionResult> AddBid([FromBody] AddBidRequest request)
    {
        var response = await _bidService.PostBid(request);
        if (!response) return BadRequest("Couldn't Add bid");

        return Ok("Posted Bid");
    }

    [HttpDelete("delete")]
    [Authorize(Roles = "Client")]
    public async Task<IActionResult> DeleteBid([FromBody] DeleteBidRequest request)
    {
        var response = await _bidService.DeleteBidService(request);
        if (!response) return BadRequest("Couldn't delete bid");

        return Ok("Success");
    }

    [HttpPatch("update")]
    [Authorize(Roles = "Client")]
    public async Task<IActionResult> UpdateBidAmt([FromBody] UpdateBidRequest request)
    {
        var response = await _bidService.UpdateBidAmt(request);
        return Ok(response);
    }
}