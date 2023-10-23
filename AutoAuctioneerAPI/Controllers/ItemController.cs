using API_AutoAuctioneer.Services.ItemService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API_AutoAuctioneer.Controllers; 

[ApiController, Route("api/[controller]")]
public class ItemController : ControllerBase {
    private readonly IItemService _itemService;

    public ItemController(IItemService itemService) {
        _itemService = itemService;
    }
    
    [HttpGet("getById"), Authorize(Roles = "Client")]
    public async Task<IActionResult> GetById([FromQuery] Guid id) {
        var item = await _itemService.GetById(id);
        if (item != null) return Ok(item);
        return BadRequest("ItemEntity Not Found");
    }

    [HttpGet("getowned"), Authorize(Roles = "Client")]
    public async Task<IActionResult> GetOwned([FromQuery] Guid id) {
        var items = await _itemService.GetOwned(id);
        return Ok(items);
    }
}