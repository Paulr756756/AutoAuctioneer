using API.Models.RequestModels;
using API.Services.ListingService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

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

    //TODO(Give listing, Item, Username, Highest bid)
    [HttpGet("getresponselist")]
    public async Task<IActionResult> GetAllListingResponses() {
        var response= await _listingService.GetAllListingResponses();
        return Ok(response);
    }
    [HttpGet("getresponsebyid")]
    public async Task<IActionResult> GetResponseById([FromQuery] Guid guid) {
        var response = await _listingService.GetListingResponseById(guid);
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