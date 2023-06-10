using BidStampAPI.Models;
using DataAccessLibrary_BidStamp;
using DataAccessLibrary_BidStamp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BidStampAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StampController : Controller
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
                EndDate  = request.EndDate
            };

            await _dbContext.Stamps.AddAsync(stamp);
            await _dbContext.SaveChangesAsync();
            return Ok(stamp);
        
        }

       /* // GET: Stamp/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Stamp/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Stamp/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Stamp/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }*/
    }
}
