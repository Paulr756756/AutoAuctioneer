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
        private readonly BidStampApiDbContext _dbContext;
        /*private readonly ILogger _logger;*/
        public StampController(BidStampApiDbContext dbContext)
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

        /*// GET: Stamp/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Stamp/Create
        public ActionResult Create()
        {
            return View();
        }*/


        [HttpPost]
        public async Task<ActionResult> AddStamp(AddStampRequest request)
        {
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
                EndDate = request.EndDate
            };

            await _dbContext.Stamps.AddAsync(stamp);
            await _dbContext.SaveChangesAsync();
            return Ok(stamp);

        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateStamp([FromRoute] Guid id, UpdateStampRequest request)
        {
            var stamp = _dbContext.Stamps.Find(id);
            if (stamp != null)
            {
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


                await _dbContext.SaveChangesAsync();
                return Ok(stamp);
            }

            return NotFound();
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteStamp([FromRoute] Guid id)
        {
            var stamp = _dbContext.Stamps.Find(id);
            if (stamp != null)
            {
                _dbContext.Stamps.Remove(stamp);
                await _dbContext.SaveChangesAsync();
                return Ok();
            }

            return NotFound();
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetStampById([FromRoute] Guid id)
        {
            var stamp = _dbContext.Stamps.Find(id);
            if (stamp != null) return Ok(stamp);

            return NotFound();
        }

    }

}
