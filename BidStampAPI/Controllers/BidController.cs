using API_BidStamp.Models.BidRequestModels;
using DataAccessLibrary_BidStamp;
using DataAccessLibrary_BidStamp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_BidStamp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BidController : ControllerBase
    {
        private readonly DatabaseContext _dbContext;
        public BidController(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("getallbids")]
        public async Task<IActionResult> GetAllBids()
        {
            var response = await _dbContext.Bids.ToListAsync();
            return Ok(response);
        }

        [HttpGet("getbidbyid")]
        public async Task<IActionResult> GetBidById(Guid id)
        {
            var response = await _dbContext.Bids.FirstOrDefaultAsync(b=> b.BidId == id);
            if(response == null)
            {
                return BadRequest("Bid doesn't exist");
            }
            return Ok(response);

        }

        [HttpPost("addbid")]
        public async Task<IActionResult> AddBid(AddBidRequest request)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(e => e.UserId == request.UserId);
            if (user==null)
            {
                return BadRequest("No such user exists");
            }
            var listing = await _dbContext.Listings.FirstOrDefaultAsync(e => e.ListingId == request.ListingId);
            if (listing==null)
            {
                return BadRequest("No such listing exists");
            }else if(listing.UserId==request.UserId)
            {
                return BadRequest("You are trying to Bid on a Listing You posted");
            }
            
            Bid bid = new Bid()
            {
                BidId = Guid.NewGuid(),
                UserId = request.UserId,
                User=user,
                ListingId = request.ListingId,
                Listing=listing,
                BidAmount = request.BidAmount,
                BidTime = DateTime.UtcNow,
            };

            await _dbContext.Bids.AddAsync(bid);
            await _dbContext.SaveChangesAsync();
            return Ok(bid);
        }

        [HttpDelete("deleteBids")]
        public async Task<IActionResult> DeleteBid(DeleteBidRequest request)
        {
            var bid = await _dbContext.Bids.FirstOrDefaultAsync(bid => bid.UserId == request.UserId);
            if (bid == null)
            {
                return BadRequest("Bid doesn't exist");
            } else if(request.UserId != bid.UserId)
            {
                return BadRequest("You don't have ownership of the bid");
            }

            _dbContext.Bids.Remove(bid);
            await _dbContext.SaveChangesAsync();
            return Ok($"bid removed with bid Id:{bid.BidId}");
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateBidAmt(UpdateBidRequest request)
        {
            var bid = _dbContext.Bids.FirstOrDefault(b=> b.BidId == request.BidId);
            if (bid == null)
            {
                return BadRequest("Bid Does not exist");
            }
            if(request.UserId != bid.UserId)
            {
                return BadRequest("You do not have ownership of the bid");
            }

            int prevAmt = bid.BidAmount;
            bid.BidAmount=request.BidAmount;

            await _dbContext.SaveChangesAsync();

            return Ok($"Bid with id:{bid.BidId} amount:{prevAmt} updated to amount:{bid.BidAmount} ");
        }
    }
}
