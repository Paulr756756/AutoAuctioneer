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


    [HttpPost("addlisting")]
    [Authorize(Roles = "Client")]
    public async Task<IActionResult> AddListing(ListingRegisterRequest request) {
        var response = await _listingService.addListingService(request);

        if (!response) return BadRequest("Error");
        /*if (_dbContext.Listings.Any(l => l.Stamp.StampId == request.StampId))
            return BadRequest("This stamp already present in another listing");
        var stamp = await _dbContext.Stamps.FirstOrDefaultAsync(s => s.StampId == request.StampId);
        if (stamp == null)
            return BadRequest("No such stamp exists");
        if (stamp.UserId != request.UserId)
            return BadRequest("This stamp is not in your collection");

        var listing = new Listing {
            ListingId = Guid.NewGuid(),
            Stamp = stamp,
            //TODO() : use stamp instead of fetching user again
            User = await _dbContext.Users.FirstOrDefaultAsync(u => u.UserId == request.UserId),
            UserId = request.UserId
        };

        listing.Stamp.Listing = listing;
        listing.StampId = request.StampId;

        _dbContext.Listings.Add(listing);
        await _dbContext.SaveChangesAsync();*/
        return Ok("New listing posted.");
    }

    [HttpDelete("deleteListing")]
    [Authorize(Roles = "Client")]
    public async Task<IActionResult> DeleteListing(ListingDeleteRequest request) {
        var response = await _listingService.deleteListingService(request);
        if (!response) return BadRequest("More error");

        /*var listing =
            await _dbContext.Listings.FirstOrDefaultAsync(l => l.ListingId == request.ListingId);
        if (listing == null) return BadRequest("No such listing exists");
        if (listing.UserId != request.UserId)
            return BadRequest("You do not have the authority to delete  this listing");

        _dbContext.Listings.Remove(listing);
        await _dbContext.SaveChangesAsync();*/
        return Ok($"Listing Removed with id:{request.ListingId}");
    }
}