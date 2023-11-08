using auc_client.Models.Entities;

namespace auc_client.Models;

public class ListingResponse {
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid ItemId { get; set; }

    public string UserName { get; set; }
    public long HighestBidAmt { get; set; }
    public int ItemType { get; set; }
    public int BidCount { get; set; }

    //Car Details
    public string? Make { get; set; }
    public string? Model { get; set; }
    public string? Year { get; set; }
    public string? ImageUrl { get; set; }
    //Part Details
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? Manufacturer { get; set; }


    public static explicit operator ListingResponse?(ListingEntity? entity) {
        return entity == null ? null : new ListingResponse {
            Id = entity.Id,
            UserId = entity.UserId,
            ItemId = entity.ItemId
        };
    }
}