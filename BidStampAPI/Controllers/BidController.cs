﻿using API_AutoAuctioneer.Models.BidRequestModels;
using API_AutoAuctioneer.Services;
using DataAccessLibrary_AutoAuctioneer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API_AutoAuctioneer.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BidController : ControllerBase
{
    private readonly IBidService _bidService;
    private readonly DatabaseContext _dbContext;

    public BidController(DatabaseContext dbContext, IBidService bidService)
    {
        _dbContext = dbContext;
        _bidService = bidService;
    }

    [HttpGet("getallbids")]
    public async Task<IActionResult> GetAllBids()
    {
        var response = await _bidService.getAllBidsService();
        return Ok(response);
    }

    [HttpGet("getbidbyid")]
    public async Task<IActionResult> GetBidById(Guid id)
    {
        var response = await _bidService.getBidByIdService(id);
        if (response == null) return BadRequest("Bid doesn't exist");
        return Ok(response);
    }

    [HttpPost("addbid")]
    [Authorize(Roles = "Client")]
    public async Task<IActionResult> AddBid(AddBidRequest request)
    {
        var response = await _bidService.postBidService(request);
        if (!response) return BadRequest("Error x2");

        return Ok();
    }

    [HttpDelete("deleteBid")]
    [Authorize(Roles = "Client")]
    public async Task<IActionResult> DeleteBid(DeleteBidRequest request)
    {
        var response = await _bidService.deleteBidService(request);
        if (!response) return BadRequest("Error times three");

        return Ok("Success");
    }

    [HttpPatch("updateBid")]
    [Authorize(Roles = "Client")]
    public async Task<IActionResult> UpdateBidAmt(UpdateBidRequest request)
    {
        var response = await _bidService.updateBidAmtService(request);
        if (!response) return BadRequest("Error maximo");
        return Ok("Successo");
    }
}