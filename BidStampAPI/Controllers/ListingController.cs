using API_BidStamp.Models.ListingRequestModels;
using DataAccessLibrary_BidStamp;
using DataAccessLibrary_BidStamp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_BidStamp.Controllers
{
    public class ListingController : ControllerBase
    {
        private readonly DatabaseContext _dbContext;
        public ListingController(DatabaseContext dbContext) {
            _dbContext = dbContext;
        }

        [HttpGet("geteverylistings")]
        public async Task<IActionResult> GetAllListings()
        {
            var response =  await _dbContext.Listings.ToListAsync();
            return Ok(response);
        }

        [HttpDelete("deleteListing")]
        public async Task<IActionResult> DeleteListing(ListingDeleteRequest request)
        {
            var listing = await _dbContext.Listings.FirstOrDefaultAsync(l=> l.ListingId == request.ListingId);
            if (listing == null)
            {
                return BadRequest("No such listing exists");
            }
            if (listing.UserId != request.UserId)
            {
                return BadRequest("You do not have the ownership of this listing");
            }

            _dbContext.Listings.Remove(listing);
            await _dbContext.SaveChangesAsync();
            return Ok($"Listing Removed with id:{request.ListingId}");
        }

        [HttpPost("addlisting")]
        public async Task<IActionResult> AddListing(ListingRegisterRequest request)
        {
            if(_dbContext.Listings.Any(l=> l.StampId == request.StampId))
            {
                return BadRequest("This stamp already present in another listing");
            }
            if (!_dbContext.Users.Any(u => u.UserId == request.UserId))
            {
                return BadRequest("No such user exists");
            }
            if (!_dbContext.Stamps.Any(s => s.StampId == request.StampId))
            {
                return BadRequest("No such stamp exists");
            }

            var listing = new Listing()
            {
                ListingId = Guid.NewGuid(),
                StampId = request.StampId,
                UserId = request.UserId,
            };
            _dbContext.Listings.Add(listing);
            await _dbContext.SaveChangesAsync();
            return Ok($"Listing Added with Id:{listing.ListingId}");
        }
    }
}
