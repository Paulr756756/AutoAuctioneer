using API_BidStamp.Models.ListingRequestModels;
using API_BidStamp.Services.ListingService;
using DataAccessLibrary_BidStamp;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API_BidStamp.Controllers;

public class ListingController : ControllerBase {
    private readonly DatabaseContext _dbContext;
    private readonly IListingService _listingService;

    public ListingController(DatabaseContext dbContext, IListingService listingService) {
        _dbContext = dbContext;
        _listingService = listingService;
    }

    [HttpGet("geteverylistings")]
    public async Task<IActionResult> GetAllListings() {
        var response = await _listingService.getAlListingsService();
        return Ok(response);
    }

    [HttpGet("getlistingbyid")]
    public async Task<IActionResult> getListingById(Guid id) {
        var response = await _listingService.getListingyId(id);
        if (response == null) return BadRequest("No such listing");

        return Ok(response);
    }


    /*[HttpPost("addlisting")]
    [Authorize(Roles = "Client")]
    public async Task<IActionResult> AddListing(ListingRegisterRequest request) {
        var response = await _listingService.addListingService(request);

        if (!response) return BadRequest("Error");
        await _dbContext.SaveChangesAsync();#1#
        return Ok("New listing posted.");
    }*/

    [HttpDelete("deleteListing")]
    [Authorize(Roles = "Client")]
    public async Task<IActionResult> DeleteListing(ListingDeleteRequest request) {
        var response = await _listingService.deleteListingService(request);
        if (!response) return BadRequest("More error");
        return Ok($"Listing Removed with id:{request.ListingId}");
    }
}