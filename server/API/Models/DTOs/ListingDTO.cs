using DataAccessLayer_AutoAuctioneer.Models;

namespace API.Models.DTOs;

public class ListingDTO {
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid ItemId { get; set; }


/*    public string UserName { get; set; }
    public long HighestBidAmt { get; set; }

    //Car Details
    public string? Make { get; set; }
    public string? Model { get; set; }
    public string? Year { get; set; }

    //Part Details
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? Manufacturer { get; set; }*/


    public static explicit operator ListingDTO? (ListingEntity? entity){
        return entity==null?null: new ListingDTO {
            Id = entity.Id,
            UserId = entity.UserId,
            ItemId = entity.ItemId
        };
    }
}