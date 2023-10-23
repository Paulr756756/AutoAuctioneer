using API_AutoAuctioneer.Models.RequestModels;
using API_AutoAuctioneer.Services.ListingService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API_AutoAuctioneer.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ListingController : ControllerBase {
    private readonly IListingService _listingService;

    public ListingController(IListingService listingService) {
        _listingService = listingService;
    }

    [HttpGet("getall")]
    public async Task<IActionResult> GetAllListings() {
        var response = await _listingService.GetAlListingsService();
        return Ok(response);
    }

    [HttpGet("getbyid"), Authorize(Roles = "Client")]
    public async Task<IActionResult> GetListingById([FromQuery] Guid id) {
        var response = await _listingService.GetListingyId(id);
        if (response == null) return BadRequest("No such listing");

        return Ok(response);
    }

    [HttpGet("getowned"), Authorize(Roles = "Client")]
    public async Task<IActionResult> GetOwnedListings([FromQuery] Guid id) {
        var response = await _listingService.GetOwnedListings(id);
        return Ok(response);
    }


    [HttpPost("add")]
    [Authorize(Roles = "Client")]
    public async Task<IActionResult> AddListing([FromBody]AddListingRequest request) {
        var response = await _listingService.AddListingService(request);
        if (response) return Ok("New listing posted");
        return BadRequest();
    }

    [HttpDelete("delete")]
    [Authorize(Roles = "Client")]
    public async Task<IActionResult> DeleteListing([FromBody]DeleteListingRequest request) {
        var response = await _listingService.DeleteListingService(request);
        if (!response) return BadRequest("More error");
        return Ok($"ListingEntity Removed with id:{request.Id}");
    }
}