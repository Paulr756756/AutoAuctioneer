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
        private readonly BidStampApiDbContext _dbContext;
        public BidController(BidStampApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBids()
        {
            var response = await _dbContext.Bids.ToListAsync();
            return Ok(response);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetBidById([FromRoute] Guid id)
        {
            var response = _dbContext.Bids.Find(id);
            return Ok(response);

        }

        [HttpPost]
        public async Task<IActionResult> AddBid(AddBidRequest request)
        {
            if (_dbContext.Stamps.Find(request.StampId) != null && _dbContext.Users.Find(request.UserId) != null)
            {
                Bid bid = new Bid()
                {
                    BidId = Guid.NewGuid(),
                    UserId = request.UserId,
                    StampId = request.StampId,
                    BidAmount = request.BidAmount,
                    BidTime = request.BidTime
                };

                await _dbContext.Bids.AddAsync(bid);
                await _dbContext.SaveChangesAsync();
                return Ok(bid);
            }

            return NotFound();
        }
    }
}
