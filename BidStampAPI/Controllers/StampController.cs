using API_BidStamp.Models.StampRequestModels;
using DataAccessLibrary_BidStamp;
using DataAccessLibrary_BidStamp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;

namespace API_BidStamp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StampController : ControllerBase
    {
        private readonly DatabaseContext _dbContext;
        /*private readonly ILogger _logger;*/
        public StampController(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
            /*_logger = logger;*/
        }

        [HttpGet]
        public async Task<IActionResult> GetAllStamps()
        {
            var response = Ok(await _dbContext.Stamps.ToListAsync());
            return response;
        }

        [HttpPost("addstamp")]
        public async Task<ActionResult> AddStamp(AddStampRequest request, Guid UserId)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.UserId == UserId);
            if (user == null) {
                return BadRequest("No such user present. Signup first");
            }

            Stamp stamp = new Stamp()
            {
                StampId = Guid.NewGuid(),
                StampTitle = request.StampTitle,
                Description = request.Description,
                ImageUrl = request.ImageUrl,
                Year = request.Year,
                Country = request.Country,
                Condition = request.Condition,
                CatalogNumber = request.CatalogNumber,
                StartingBid = request.StartingBid,
                EndingBid = request.EndingBid,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                User = user,
                UserId = UserId,

            };
            /*user.Stamps.Add(stamp);*/

            await _dbContext.Stamps.AddAsync(stamp);
            await _dbContext.SaveChangesAsync();
            return Ok(stamp);
        }

        [HttpPut("updatestamp")]
        public async Task<IActionResult> UpdateStamp(UpdateStampRequest request, Guid StampId, Guid UserId)
        {
            var stamp = await _dbContext.Stamps.FirstOrDefaultAsync(s => s.StampId == StampId);
            
            if (stamp != null)
            {
                if (UserId != stamp.UserId)
                {
                    return BadRequest("You are not authorized to update this stamp");
                }
                stamp.StampTitle = request.StampTitle;
                stamp.Description = request.Description;
                stamp.ImageUrl = request.ImageUrl;
                stamp.Year = request.Year;
                stamp.Country = request.Country;
                stamp.Condition = request.Condition;
                stamp.CatalogNumber = request.CatalogNumber;
                stamp.StartingBid = request.StartingBid;
                stamp.EndingBid = request.EndingBid;
                stamp.StartDate = request.StartDate;
                stamp.EndDate = request.EndDate;
                
                /*if (ListingId != null)
                {   
                    stamp.Listing = await _dbContext.Listings.FirstOrDefaultAsync(l => l.ListingId == ListingId);
                }*/

                await _dbContext.SaveChangesAsync();
                return Ok(stamp);
            }

            return NotFound();
        }

        [HttpDelete("deletestamp")]
        public async Task<IActionResult> DeleteStamp(Guid UserId, Guid StampId)
        {
            var stamp = await _dbContext.Stamps.FirstOrDefaultAsync(s => s.StampId == StampId);
            if (stamp != null)
            {
               /* if (stamp.Listing != null)
                {
                    _dbContext.Listings.Remove(stamp.Listing);
                }*/
               if(UserId != stamp.UserId)
                {
                    return BadRequest("You are not authorized to delete this stamp");
                }else
                {
                    var listing = await _dbContext.Listings.FirstOrDefaultAsync(l=> l.Stamp == stamp);
                    _dbContext.Stamps.Remove(stamp);

                    await _dbContext.SaveChangesAsync();
                }
                return Ok();
            }
            return NotFound();
        }

        [HttpGet("getstampbyid")]
        public async Task<IActionResult> GetStampById(Guid id)
        {
            var stamp = await _dbContext.Stamps.FirstOrDefaultAsync(s=> s.StampId==id);
            if (stamp != null) return Ok(stamp);

            return NotFound();
        }

    }

}
