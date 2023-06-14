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
            
            if (!_dbContext.Listings.Any(s=> s.ListingId == request.ListingId))
            {
                return BadRequest("No such listing exists");
            }
            if(!_dbContext.Users.Any(u => u.UserId == request.UserId))
            {
                return BadRequest("No such user exists");
            }

            Bid bid = new Bid()
            {
                BidId = Guid.NewGuid(),
                UserId = request.UserId,
                ListingId = request.ListingId,
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
